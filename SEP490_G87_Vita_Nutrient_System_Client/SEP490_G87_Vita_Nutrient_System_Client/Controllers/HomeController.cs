using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Security.Claims;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using SEP490_G87_Vita_Nutrient_System_Client.Models;


namespace SEP490_G87_Vita_Nutrient_System_Client.Controllers
{
    public class HomeController : Controller
    {

        ////////////////////////////////////////////////////////////
        /// Tân
        ////////////////////////////////////////////////////////////
        ///

        private readonly HttpClient client = null;
        public HomeController()
        {
            Uri URIBase = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetValue<Uri>("myUri");
            client = new HttpClient();
            client.BaseAddress = URIBase;
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
            try
            {
                HttpResponseMessage res = await client.GetAsync(client.BaseAddress + "/Users/Login?account=" + account + "&password=" + password);

                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    HttpContent content = res.Content;
                    string data = await content.ReadAsStringAsync();
                    dynamic u = JsonConvert.DeserializeObject<dynamic>(data);

                    importStringToSession("takeFullName", u.fullName, "string");
                    importStringToSession("imageUrl", u.urlimage, "URL");

                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,(string) u.account),
                    new Claim(ClaimTypes.Role,(string) u.roleName),
                    new Claim("UserId", (string)u.userId)
                };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = false,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                    if (((string)u.roleName).Equals("Admin"))
                    {
                        return RedirectToAction("AdminDashboard", "Admin");
                    }
                    else if (((string)u.roleName).Equals("Nutritionist"))
                    {
                        return RedirectToAction("NutritionistHome", "Nutritionist");
                    }
                    else
                    {
<<<<<<< Updated upstream
                        return RedirectToAction("Plan", "User");
=======
                        return RedirectToAction("PlanUser", "User");
                        return RedirectToAction("Index", "Home");
>>>>>>> Stashed changes
                    }

                }
                else
                {
                    ViewBag.AlertMessage = "Incorrect account password entered.";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.AlertMessage = "An unexpected error occurred. Please try again.";
                return View();
            }
        }


        public void importStringToSession(string assignToValue, dynamic? data, string type)
        {
            if (type.Equals("URL"))
            {
                if (data != null && !string.IsNullOrEmpty((string)data))
                {
                    var takeFullName = Url.Content((string)data);
                    if (HttpContext.Session != null)
                    {
                        HttpContext.Session.SetString(assignToValue, takeFullName);
                    }
                }
            }
            else if (type.Equals("string"))
            {
                if (data != null && !string.IsNullOrEmpty((string)data))
                {
                    if (HttpContext.Session != null)
                    {
                        HttpContext.Session.SetString(assignToValue, (string)data);
                    }
                }
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
            try
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
                    short roleUser = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetValue<short>("roleUser");
                    User user = new User()
                    {
                        Account = account,
                        Password = password,
                        Role = roleUser,
                        IsActive = true
                    };

                    HttpResponseMessage res = await client.PostAsJsonAsync(client.BaseAddress + "/Users/Register", user);
                    if (res.StatusCode == System.Net.HttpStatusCode.OK)
                    {

                        HttpResponseMessage resLogin = await client.GetAsync(client.BaseAddress + "/Users/Login?account=" + account + "&password=" + password);

                        if (res.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            HttpContent content = resLogin.Content;
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
                                IsPersistent = false,
                                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
                            };

                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ViewBag.AlertMessage = "Invalid login attempt. 3";
                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.AlertMessage = "Invalid login attempt. 2";
                        return View();
                    }
                }
            }catch (Exception ex)
            {
                ViewBag.AlertMessage = "An unexpected error occurred. Please try again.";
                return View();
            }
        }


        [Authorize]
        public async Task<bool> checkExsitAsync(string account)
        {
            


            HttpResponseMessage respone = await client.GetAsync(client.BaseAddress + "/Users/checkExit?account=" + account);
            if (respone.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return true;
            }
            else
            {
                return false;

            }
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Home");
        }


        public async Task<IActionResult> ToCopyTryCatch()
        {
            try
            {

            }
            catch (Exception ex)
            {
                ViewBag.AlertMessage = "An unexpected error occurred. Please try again.";
                return View();
            }

            return RedirectToAction("Login", "Home");
        }



        ////////////////////////////////////////////////////////////
        /// Dũng
        ////////////////////////////////////////////////////////////
        ///






        ////////////////////////////////////////////////////////////
        /// Chiến
        ////////////////////////////////////////////////////////////
        ///





        ////////////////////////////////////////////////////////////
        /// Sơn
        ////////////////////////////////////////////////////////////
        ///





        ////////////////////////////////////////////////////////////
        /// Tùng
        ////////////////////////////////////////////////////////////
        ///


    }
}
