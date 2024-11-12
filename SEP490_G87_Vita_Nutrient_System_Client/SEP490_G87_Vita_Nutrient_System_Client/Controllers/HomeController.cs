﻿using Microsoft.AspNetCore.Authentication.Cookies;
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
        public async Task<IActionResult> Index()
        {
            List<ArticlesNewsDTO> latestArticles = new List<ArticlesNewsDTO>();

            // Gọi API để lấy 3 bài viết mới nhất
            HttpResponseMessage response = await client.GetAsync("/api/news/latest");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                latestArticles = JsonConvert.DeserializeObject<List<ArticlesNewsDTO>>(data);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error fetching latest articles from API.");
            }

            // Truyền dữ liệu bài viết mới nhất đến View
            return View(latestArticles);
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

                    HttpContext.Session.SetString("UserId", (string)u.userId);

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
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(999)
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
                        HttpResponseMessage res2 = await client.GetAsync(client.BaseAddress + $"/GenerateMeal/APIFirstMealSetting?idUser={u.userId}");

                        if (res2.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            return RedirectToAction("Error", "Home");
                        }
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

                            HttpResponseMessage res2 = await client.GetAsync(client.BaseAddress + $"/GenerateMeal/APIFirstMealSetting?idUser={u.userId}");
                            if (res2.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                return RedirectToAction("Index", "Home");
                            }
                            else
                            {
                                return RedirectToAction("Error", "Home");
                            }
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
            bool isValid = true;
            // Kiểm tra ngày sinh không được lớn hơn ngày hiện tại
            if (birthDate > DateTime.Today)
            {
                ViewBag.BirthDateError = "Ngày sinh không hợp lệ. Vui lòng chọn một ngày trong quá khứ.";
                isValid = false;
            }

            // Nếu tuổi dưới 5 hoặc chiều cao, cân nặng không hợp lý, trả về lỗi
            if (age < 5 || age > 100 || height <= 0 || weight <= 0)
            {
                ViewBag.AgeError = "Phần mềm này chỉ áp dụng cho người từ 5 đến 100 tuổi với chiều cao và cân nặng hợp lý.";
                isValid = false;
            }

            if (weight <= 0 || weight > 300)
            {
                ViewBag.WeightError = "Cân nặng không hợp lệ (phải nằm trong khoảng 1 - 300 kg).";
                isValid = false;
            }

            if (height < 50 || height > 250)
            {
                ViewBag.HeightError = "Chiều cao không hợp lệ (phải nằm trong khoảng 50 - 250 cm).";
                isValid = false;
            }
            if (!isValid)
            {
                ViewBag.BirthDate = birthDate.ToString("yyyy-MM-dd");
                ViewBag.Weight = weight;
                ViewBag.Height = height;
                ViewBag.Gender = gender;
                return View();
            }

            double bmi = weight / Math.Pow(height / 100, 2);
            int percentile = 0;
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
                percentile = GetBMIPercentileForAgeAndGender(age, bmi, gender);
                if (percentile < 5)
                {
                    status = "THIẾU CÂN";
                    evaluation = $"Hãy cải thiện chế độ dinh dưỡng để đạt mức cân nặng phù hợp.";
                }
                else if (percentile <= 85)
                {
                    status = "BÌNH THƯỜNG";
                    evaluation = $"Hãy duy trì chế độ ăn uống và luyện tập hiện tại.";
                }
                else if (percentile <= 95)
                {
                    status = "NGUY CƠ THỪA CÂN";
                    evaluation = $"Cần điều chỉnh chế độ ăn uống và luyện tập để tránh thừa cân.";
                }
                else
                {
                    status = "THỪA CÂN";
                    evaluation = $"Cần có chế độ ăn uống và vận động phù hợp để cải thiện sức khỏe.";
                }

            }

            var result = new BMIResult
            {
                BMI = Math.Round(bmi, 2),
                Percentile = percentile,
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

        private int GetBMIPercentileForAgeAndGender(int age, double bmi, string gender)
        {
            // Dữ liệu từ tài liệu WHO (bạn có thể mở rộng thêm dữ liệu từ file PDF)
            var boysBmiPercentiles = new Dictionary<int, List<(double Bmi, int Percentile)>>
            {
                { 5, new List<(double, int)> { (12.7, 5), (13.1, 15), (13.4, 25), (15.3, 50), (16.7, 75), (18.1, 95) } },
                { 6, new List<(double, int)> { (12.7, 5), (13.2, 15), (13.4, 25), (15.3, 50), (16.8, 75), (18.4, 95) } },
                { 7, new List<(double, int)> { (12.9, 5), (13.4, 15), (13.7, 25), (15.7, 50), (17.2, 75), (18.9, 95) } },
                { 8, new List<(double, int)> { (13.1, 5), (13.6, 15), (14.0, 25), (16.1, 50), (17.6, 75), (19.3, 95) } },
                { 9, new List<(double, int)> { (13.4, 5), (14.0, 15), (14.4, 25), (16.7, 50), (18.2, 75), (20.0, 95) } },
                { 10, new List<(double, int)> { (13.7, 5), (14.4, 15), (14.9, 25), (17.3, 50), (18.9, 75), (20.7, 95) } },
                { 11, new List<(double, int)> { (14.0, 5), (14.8, 15), (15.4, 25), (18.0, 50), (19.6, 75), (21.4, 95) } },
                { 12, new List<(double, int)> { (14.3, 5), (15.3, 15), (15.9, 25), (18.7, 50), (20.3, 75), (22.1, 95) } },
                { 13, new List<(double, int)> { (14.7, 5), (15.7, 15), (16.4, 25), (19.3, 50), (21.0, 75), (22.9, 95) } },
                { 14, new List<(double, int)> { (15.0, 5), (16.1, 15), (16.9, 25), (19.9, 50), (21.7, 75), (23.6, 95) } },
                { 15, new List<(double, int)> { (15.3, 5), (16.5, 15), (17.3, 25), (20.4, 50), (22.3, 75), (24.3, 95) } },
                { 16, new List<(double, int)> { (15.6, 5), (16.9, 15), (17.8, 25), (20.9, 50), (22.9, 75), (25.0, 95) } },
                { 17, new List<(double, int)> { (15.9, 5), (17.2, 15), (18.2, 25), (21.4, 50), (23.5, 75), (25.6, 95) } },
                { 18, new List<(double, int)> { (16.1, 5), (17.5, 15), (18.6, 25), (21.9, 50), (24.0, 75), (26.2, 95) } },
                { 19, new List<(double, int)> { (16.4, 5), (17.8, 15), (19.0, 25), (22.3, 50), (24.5, 75), (26.7, 95) } }
            };

            var girlsBmiPercentiles = new Dictionary<int, List<(double Bmi, int Percentile)>>
            {
                { 5, new List<(double, int)> { (12.4, 5), (12.9, 15), (13.1, 25), (15.2, 50), (16.9, 75), (18.6, 95) } },
                { 6, new List<(double, int)> { (12.4, 5), (12.9, 15), (13.1, 25), (15.3, 50), (17.1, 75), (19.0, 95) } },
                { 7, new List<(double, int)> { (12.8, 5), (13.3, 15), (13.6, 25), (15.8, 50), (17.6, 75), (19.6, 95) } },
                { 8, new List<(double, int)> { (13.1, 5), (13.6, 15), (14.0, 25), (16.3, 50), (18.2, 75), (20.2, 95) } },
                { 9, new List<(double, int)> { (13.4, 5), (14.0, 15), (14.5, 25), (16.9, 50), (18.9, 75), (20.9, 95) } },
                { 10, new List<(double, int)> { (13.8, 5), (14.4, 15), (15.0, 25), (17.5, 50), (19.6, 75), (21.6, 95) } },
                { 11, new List<(double, int)> { (14.1, 5), (14.8, 15), (15.5, 25), (18.1, 50), (20.3, 75), (22.3, 95) } },
                { 12, new List<(double, int)> { (14.4, 5), (15.2, 15), (16.0, 25), (18.7, 50), (21.0, 75), (23.0, 95) } },
                { 13, new List<(double, int)> { (14.8, 5), (15.6, 15), (16.5, 25), (19.3, 50), (21.6, 75), (23.7, 95) } },
                { 14, new List<(double, int)> { (15.1, 5), (16.0, 15), (17.0, 25), (19.9, 50), (22.3, 75), (24.4, 95) } },
                { 15, new List<(double, int)> { (15.4, 5), (16.4, 15), (17.5, 25), (20.4, 50), (22.9, 75), (25.1, 95) } },
                { 16, new List<(double, int)> { (15.7, 5), (16.8, 15), (18.0, 25), (20.9, 50), (23.5, 75), (25.8, 95) } },
                { 17, new List<(double, int)> { (16.0, 5), (17.2, 15), (18.4, 25), (21.4, 50), (24.1, 75), (26.5, 95) } },
                { 18, new List<(double, int)> { (16.2, 5), (17.5, 15), (18.8, 25), (21.9, 50), (24.6, 75), (27.1, 95) } },
                { 19, new List<(double, int)> { (16.5, 5), (17.9, 15), (19.2, 25), (22.4, 50), (25.1, 75), (27.7, 95) } }
            };

            List<(double Bmi, int Percentile)> bmiPercentiles;
            if (gender == "Nam")
            {
                if (!boysBmiPercentiles.TryGetValue(age, out bmiPercentiles))
                {
                    throw new ArgumentException("Tuổi không hợp lệ hoặc không có dữ liệu cho tuổi này.");
                }
            }
            else if (gender == "Nữ")
            {
                if (!girlsBmiPercentiles.TryGetValue(age, out bmiPercentiles))
                {
                    throw new ArgumentException("Tuổi không hợp lệ hoặc không có dữ liệu cho tuổi này.");
                }
            }
            else
            {
                throw new ArgumentException("Giới tính không hợp lệ.");
            }

            // Tìm percentile phù hợp với chỉ số BMI của trẻ
            int percentile = 0;
            foreach (var (Bmi, Percentile) in bmiPercentiles)
            {
                if (bmi < Bmi)
                {
                    percentile = Percentile;
                    break;
                }
                percentile = Percentile; // Cập nhật nếu BMI lớn hơn tất cả các giá trị trong danh sách
            }

            return percentile;
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