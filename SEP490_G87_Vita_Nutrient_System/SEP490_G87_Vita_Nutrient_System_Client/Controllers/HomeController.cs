using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SEP490_G87_Vita_Nutrient_System_Client.Interfaces;
using System.Net.Http.Headers;
using System.Security.Claims;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using SEP490_G87_Vita_Nutrient_System_Client.Models;


namespace SEP490_G87_Vita_Nutrient_System_Client.Controllers
{
    public class HomeController : Controller, HomeInterface
    {

        Uri baseAdd = new Uri("https://localhost:7034/api");

        private readonly HttpClient client = null;
        public HomeController()
        {
           
            client = new HttpClient();
            client.BaseAddress = baseAdd;
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string account, string password)
        {
            HttpResponseMessage res = await client.GetAsync(client.BaseAddress + "/Users/Login?account=" + account + "&password=" + password);

            if (res.StatusCode == System.Net.HttpStatusCode.OK)
            {
                HttpContent content = res.Content;
                string data = await content.ReadAsStringAsync();
                dynamic u = JsonConvert.DeserializeObject<dynamic>(data);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,(string) u.account),
                    new Claim(ClaimTypes.Role,(string) u.roleName),
                    new Claim("UserId", (string)u.userId)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                if (((string)u.roleName).Equals("Admin"))
                {
                    return RedirectToAction("AdminHome", "Admin");
                }
                else if (((string)u.roleName).Equals("Nutritionist"))
                {
                    return RedirectToAction("NutritionistHome", "Nutritionist");
                }
                else
                {
                    return RedirectToAction("Plan", "User");
                }

            }
            else
            {
                ViewBag.AlertMessage = "Incorrect account password entered.";
                return View();
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(string account, string password, string confirm)
        {

            if (!password.Equals(confirm))
            {
                ViewBag.AlertMessage = "Invalid login attempt. 1";
                return View();
            }
            if (await checkExsitAsync(account))
            {
                ViewBag.AlertMessage = "Account already exists.";
                return View();
            }
            else
            {
                //short roleUser = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetValue<short>("roleUser");
                User user = new User()
                {
                    Account = account,
                    Password = password,
                    Role = 4,
                    IsActive = true
                };
                HttpResponseMessage res = await client.PostAsJsonAsync(client.BaseAddress + "/Users/Register", user);
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    HttpContent content = res.Content;
                    string data = await content.ReadAsStringAsync();
                    dynamic u = JsonConvert.DeserializeObject<dynamic>(data);

                    var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name,(string) u.account),
                            new Claim(ClaimTypes.Role,(string) u.roleName),
                            //new Claim("UserId", (string)u.userId)

                        };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                    return RedirectToAction("Login", "Home");
                }
                else
                {
                    ViewBag.AlertMessage = "Invalid login attempt. 2";
                    return View();
                }
            }
        }


        [Authorize]
        public async Task<bool> checkExsitAsync(string account)
        {
            HttpResponseMessage respone = await client.GetAsync(client.BaseAddress + "/Users/checkExit?account=" + account);
            if (respone.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return false;
            }
            else
            {
                return true;
                
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Home");
        }

    }
}
