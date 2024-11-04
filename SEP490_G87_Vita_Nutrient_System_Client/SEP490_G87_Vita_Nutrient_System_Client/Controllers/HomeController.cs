using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Security.Claims;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using SEP490_G87_Vita_Nutrient_System_Client.Models;
using Microsoft.AspNetCore.Http;


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

                        return RedirectToAction("Index", "Home");
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
        public IActionResult NutritionCheck()
        {
            return View();
        }

        [HttpPost]
        public IActionResult NutritionCheck(DateTime birthDate, double weight, double height, string gender)
        {
            int age = DateTime.Today.Year - birthDate.Year;
            if (birthDate > DateTime.Today.AddYears(-age)) age--;

            // Nếu tuổi dưới 2 hoặc chiều cao, cân nặng không hợp lý, trả về lỗi
            if (age < 2 || height <= 0 || weight <= 0)
            {
                ViewBag.AgeError = "Phần mềm này chỉ áp dụng cho người từ 2 tuổi trở lên với chiều cao và cân nặng hợp lý.";
                return View();
            }

            double bmi = weight / Math.Pow(height / 100, 2);
            string status;
            string evaluation;

            if (age >= 19) // Tiêu chí cho người trưởng thành
            {
                if (gender == "Nam")
                {
                    if (bmi < 16) { status = "GẦY ĐỘ III"; evaluation = "<16Kg/m²"; }
                    else if (bmi < 17) { status = "GẦY ĐỘ II"; evaluation = "16Kg/m² - <17Kg/m²"; }
                    else if (bmi < 18.5) { status = "GẦY ĐỘ I"; evaluation = "17Kg/m² - <18.5Kg/m²"; }
                    else if (bmi < 23) { status = "BÌNH THƯỜNG"; evaluation = "18.5Kg/m² - <23Kg/m²"; }
                    else if (bmi < 25) { status = "THỪA CÂN"; evaluation = "23Kg/m² - <25Kg/m²"; }
                    else if (bmi < 30) { status = "BÉO PHÌ ĐỘ I"; evaluation = "25Kg/m² - <30Kg/m²"; }
                    else if (bmi < 35) { status = "BÉO PHÌ ĐỘ II"; evaluation = "30Kg/m² - <35Kg/m²"; }
                    else { status = "BÉO PHÌ ĐỘ III"; evaluation = "≥35Kg/m²"; }
                }
                else // Nữ
                {
                    if (bmi < 15.5) { status = "GẦY ĐỘ III"; evaluation = "<15.5Kg/m²"; }
                    else if (bmi < 16.5) { status = "GẦY ĐỘ II"; evaluation = "15.5Kg/m² - <16.5Kg/m²"; }
                    else if (bmi < 18) { status = "GẦY ĐỘ I"; evaluation = "16.5Kg/m² - <18Kg/m²"; }
                    else if (bmi < 22.5) { status = "BÌNH THƯỜNG"; evaluation = "18Kg/m² - <22.5Kg/m²"; }
                    else if (bmi < 24.5) { status = "THỪA CÂN"; evaluation = "22.5Kg/m² - <24.5Kg/m²"; }
                    else if (bmi < 29.5) { status = "BÉO PHÌ ĐỘ I"; evaluation = "24.5Kg/m² - <29.5Kg/m²"; }
                    else if (bmi < 34.5) { status = "BÉO PHÌ ĐỘ II"; evaluation = "29.5Kg/m² - <34.5Kg/m²"; }
                    else { status = "BÉO PHÌ ĐỘ III"; evaluation = "≥34.5Kg/m²"; }
                }
            }
            else // Tiêu chí cho trẻ em và thanh thiếu niên
            {
                // Ví dụ đánh giá cơ bản cho trẻ em/thanh thiếu niên (theo phần trăm phân vị hoặc khuyến nghị từ tổ chức y tế)
                if (bmi < 5) { status = "THIẾU CÂN"; evaluation = "Dưới mức bình thường"; }
                else if (bmi <= 85) { status = "BÌNH THƯỜNG"; evaluation = "Trong phạm vi bình thường"; }
                else if (bmi <= 95) { status = "NGUY CƠ THỪA CÂN"; evaluation = "Trên mức bình thường"; }
                else { status = "THỪA CÂN"; evaluation = "Cao hơn mức bình thường"; }
            }

            var result = new BMIResult
            {
                BMI = Math.Round(bmi, 2),
                Status = status,
                Evaluation = evaluation,
                Gender = gender
            };

            ViewBag.BirthDate = birthDate.ToString("yyyy-MM-dd");
            ViewBag.Weight = weight;
            ViewBag.Height = height;
            ViewBag.Gender = gender;
            ViewBag.Age = age;

            return View("NutritionCheck", result);
        }




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
