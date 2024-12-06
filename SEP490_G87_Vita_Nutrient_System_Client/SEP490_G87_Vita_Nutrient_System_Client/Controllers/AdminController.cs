using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SEP490_G87_Vita_Nutrient_System_Client.Domain.Enums;
using SEP490_G87_Vita_Nutrient_System_Client.Models;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using System.Security.Principal;
using static System.Net.WebRequestMethods;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics.Metrics;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography.Xml;
using SEP490_G87_Vita_Nutrient_System_Client.Domain.Attributes;

namespace SEP490_G87_Vita_Nutrient_System_Client.Controllers
{
    public class AdminController : Controller
    {
        ////////////////////////////////////////////////////////////
        /// Tân
        ////////////////////////////////////////////////////////////
        ///

        private readonly HttpClient client = null;
        private readonly AdminSevices adminSevices;
        public AdminController()
        {
            adminSevices = new AdminSevices();
            Uri URIBase = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetValue<Uri>("myUri");
            client = new HttpClient();
            client.BaseAddress = URIBase;
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }


        [HttpGet, Authorize(Roles = "User")]
        public async Task<IActionResult> QRCodePaymentPageAsync()
        {
            try
            {
                int userId = int.Parse(User.FindFirst("UserId")?.Value);
                int typeInsert = Int32.Parse(HttpContext.Session.GetString("TypeInsert"));
                string? accountNumber = HttpContext.Session.GetString("accountNumberQRPay");
                int? limit = Int32.Parse(HttpContext.Session.GetString("limitQRPay"));
                decimal? amountInPay = decimal.Parse(HttpContext.Session.GetString("amountInPayQRPay"));
                string? contentBankPay = HttpContext.Session.GetString("contentBankPayQRPay");
                string? contentBankImg = HttpContext.Session.GetString("contentBankImgQRPay");

                int? amountWithoutDecimal = HttpContext.Session.GetString("amountInPayQRPay") is string amountInPayString &&
                    decimal.TryParse(amountInPayString, out decimal amountInImg)
                ? (int)amountInPay
                : 0;

                int? NutritionistId = Int32.Parse(HttpContext.Session.GetString("NutritionistId"));
                string? Describe = HttpContext.Session.GetString("Describe");
                int? Duration = Int32.Parse(HttpContext.Session.GetString("Duration"));


                if (accountNumber != null && limit != null && amountInPay != null && contentBankPay != null && amountWithoutDecimal != null && contentBankImg != null)
                {
                    HttpContext.Session.Remove("accountNumberQRPay");
                    HttpContext.Session.Remove("limitQRPay");
                    HttpContext.Session.Remove("amountInPayQRPay");
                    HttpContext.Session.Remove("contentBankPayQRPay");
                    HttpContext.Session.Remove("amountInImgQRPay");
                    HttpContext.Session.Remove("contentBankImgQRPay");

                    HttpContext.Session.Remove("TypeInsert");
                    HttpContext.Session.Remove("NutritionistId");
                    HttpContext.Session.Remove("Describe");
                    HttpContext.Session.Remove("Duration");


                    string checkQRPaySuccess = client.BaseAddress + $"/BankPayment/APICheckQRPaySuccessful?accountNumber={accountNumber}&limit={limit}&content={contentBankPay}&amountIn={amountInPay}";
                    string insertPaidPersonData = client.BaseAddress + $"/BankPayment/APIInsertPaidPersonData?typeInsert={typeInsert}";
                    HttpResponseMessage res = await client.GetAsync(client.BaseAddress + $"/BankPayment/APIGetQRPayDefaultSystem?amount={amountWithoutDecimal}&content={contentBankImg}");

                    if (res.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        HttpContent content = res.Content;
                        string data = await content.ReadAsStringAsync();
                        string linkQRImage = JsonConvert.DeserializeObject<string>(data);

                        int roleAdmin = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetValue<int>("roleAdmin");
                        TransactionsSystem transactionsSystem = new TransactionsSystem()
                        {
                            UserPayId = userId,
                            PayeeId = roleAdmin,
                            AccountNumber = accountNumber,
                            AmountIn = amountInPay,
                            TransactionContent = contentBankPay
                        };

                        if (typeInsert == 1) ViewData["UserListManagement"] = new UserListManagement { NutritionistId = NutritionistId ?? 0, UserId = userId, Describe = Describe, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(Duration ?? 0), IsDone = false };
                        else ViewData["UserListManagement"] = new UserListManagement { NutritionistId = roleAdmin, UserId = userId, Describe = Describe, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(Duration ?? 0), IsDone = false };

                        HttpResponseMessage res2 = await client.PostAsJsonAsync(client.BaseAddress + $"/BankPayment/APIModifyDataTransactionsSystem", transactionsSystem);
                        if (res.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            HttpContent content2 = res2.Content;
                            string data2 = await content2.ReadAsStringAsync();
                            TransactionsSystem dataTransactionsSystem = JsonConvert.DeserializeObject<TransactionsSystem>(data2);
                            ViewBag.dataTransactionsSystem = dataTransactionsSystem;
                            ViewBag.CheckQRPaySuccess = checkQRPaySuccess;
                            ViewBag.InsertPaidPersonData = insertPaidPersonData;
                            ViewBag.LinkQRImage = linkQRImage;
                            return View();
                        }
                        ViewBag.AlertMessage = "Error";
                        return Redirect("Error ABC");
                    }

                }
                ViewBag.AlertMessage = "Error";
                return Redirect($"Error CDE {accountNumber}== {limit}== {amountInPay}== {contentBankPay}== {amountWithoutDecimal}== {contentBankImg}==");
            }
            catch (Exception ex)
            {
                ViewBag.AlertMessage = "An unexpected error occurred. Please try again.";
                return Redirect("Error EFG");
            }
        }


        [HttpPost, Authorize(Roles = "User")]
        public IActionResult PaymentForPaidServices(int NutritionistId, string? Describe, decimal Price, short Duration, int TypeInsert)
        {
            var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
            string? accountNumber = configuration.GetValue<string>("accountNumberQRPay");
            int? limit = configuration.GetValue<int>("limitQRPay");

            HttpContext.Session.SetString("NutritionistId", NutritionistId.ToString());
            HttpContext.Session.SetString("Describe", Describe ?? "");
            HttpContext.Session.SetString("TypeInsert", TypeInsert.ToString());
            HttpContext.Session.SetString("Duration", Duration.ToString());

            HttpContext.Session.SetString("accountNumberQRPay", accountNumber ?? "");
            HttpContext.Session.SetString("limitQRPay", limit.ToString() ?? "20");
            HttpContext.Session.SetString("amountInPayQRPay", Price.ToString());
            HttpContext.Session.SetString("amountInImgQRPay", Price.ToString());

            string contentGeneratePassword = adminSevices.GeneratePassword(12);
            HttpContext.Session.SetString("contentBankPayQRPay", contentGeneratePassword);
            HttpContext.Session.SetString("contentBankImgQRPay", contentGeneratePassword);
            return Redirect("QRCodePaymentPage");
        }


        //[HttpGet, Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult AdminStatistics()
        {
            return View();
        }


        //[HttpGet]
        //public IActionResult PremiumUpgradeSuggestion()
        //{

        //    // Lấy chuỗi JSON từ appsettings.json
        //    var jsonString = new ConfigurationBuilder()
        //        .AddJsonFile("appsettings.json")
        //        .Build()
        //        .GetValue<string>("SystemPremiumPackagesJson");

        //    // Deserialize JSON thành danh sách đối tượng
        //    var systemPremiumPackages = JsonConvert.DeserializeObject<List<SystemPremiumPackage>>(jsonString);

        //    // Truyền danh sách lên view
        //    return View(systemPremiumPackages);
        //}


        [HttpGet, Authorize(Roles = "User")]
        public async Task<IActionResult> NutritionistServicesAsync()
        {
            try
            {
                int month = DateTime.Now.Month;
                int year = DateTime.Now.Year;
                Decimal[][] graphData;
                HttpResponseMessage res = await client.GetAsync(client.BaseAddress + $"/BankPayment/APIGetAllNutritionistServices");

                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    HttpContent content = res.Content;
                    string data = await content.ReadAsStringAsync();
                    IEnumerable<AllNutritionistServices> transactionsSystemData = JsonConvert.DeserializeObject<IEnumerable<AllNutritionistServices>>(data);

                    return View(transactionsSystemData);
                }

                ViewBag.AlertMessage = "An unexpected error occurred. Please try again.";
                return View(null);
            }
            catch (Exception ex)
            {
                ViewBag.AlertMessage = "An unexpected error occurred. Please try again.";
                return View();
            }
        }


        //[HttpPost]
        //public IActionResult PaymentForPaidServices(int NutritionistId, string? Describe, decimal Price, short Duration)
        //{
        //    var configuration = new ConfigurationBuilder()
        //    .AddJsonFile("appsettings.json")
        //    .Build();
        //    string? accountNumber = configuration.GetValue<string>("accountNumberQRPay");
        //    int? limit = configuration.GetValue<int>("limitQRPay");

        //    HttpContext.Session.SetString("NutritionistId", NutritionistId.ToString());
        //    HttpContext.Session.SetString("Describe", Describe ?? "");
        //    HttpContext.Session.SetString("Price", Price.ToString());
        //    HttpContext.Session.SetString("Duration", Duration.ToString());

        //    HttpContext.Session.SetString("accountNumberQRPay", accountNumber ?? "");
        //    HttpContext.Session.SetString("limitQRPay", limit.ToString() ?? "20");
        //    HttpContext.Session.SetString("amountInPayQRPay", Price.ToString());
        //    HttpContext.Session.SetString("amountInImgQRPay", Price.ToString());

        //    string contentGeneratePassword = GeneratePassword(6);
        //    HttpContext.Session.SetString("contentBankPayQRPay", contentGeneratePassword);
        //    HttpContext.Session.SetString("contentBankImgQRPay", contentGeneratePassword);

        //    return Redirect("QRCodePaymentPage");
        //}



        [HttpPost]
        public IActionResult PaymentTransferSuccessful()
        {



            return RedirectToAction("QRCodePaymentPage");
        }



        [HttpGet, Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminDashboardAsync()
        {
            try
            {

                int month = DateTime.Now.Month;
                int year = DateTime.Now.Year;
                Decimal[][] graphData;
                HttpResponseMessage res = await client.GetAsync(client.BaseAddress + $"/BankPayment/APIGetAllTransactionsSystemOfMonth?month={month}&year={year}&userMainId=1");

                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    HttpContent content = res.Content;
                    string data = await content.ReadAsStringAsync();
                    IEnumerable<TransactionsSystem> transactionsSystemData = JsonConvert.DeserializeObject<IEnumerable<TransactionsSystem>>(data);

                    var MoneyInThisMonth = transactionsSystemData.Sum(t => t.AmountIn ?? 0);
                    var MoneyOutThisMonth = transactionsSystemData.Sum(x => x.AmountOut ?? 0);
                    var BalanceThisMonth = MoneyInThisMonth - MoneyOutThisMonth;


                    HttpResponseMessage res2 = await client.GetAsync(client.BaseAddress + $"/BankPayment/APIGetAllTransactionsSystemForGraphData?year={year}&userMainId=1");

                    if (res.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        HttpContent content2 = res2.Content;
                        string data2 = await content2.ReadAsStringAsync();
                        graphData = JsonConvert.DeserializeObject<Decimal[][]>(data2);


                        ViewBag.GraphDataString0 = JsonConvert.SerializeObject(graphData[0]);
                        ViewBag.GraphDataString1 = JsonConvert.SerializeObject(graphData[1]);
                        ViewBag.GraphDataString2 = JsonConvert.SerializeObject(graphData[2]);
                        ViewBag.GraphDataString3 = JsonConvert.SerializeObject((graphData.SelectMany(row => row).Max() / 100) * 111);
                    }

                    ViewBag.MoneyInThisMonth = MoneyInThisMonth;
                    ViewBag.MoneyOutThisMonth = MoneyOutThisMonth;
                    ViewBag.BalanceThisMonth = BalanceThisMonth;

                    return View(transactionsSystemData);
                }


                ViewBag.AlertMessage = "An unexpected error occurred. Please try again.";
                return View(null);

            }
            catch (Exception ex)
            {
                ViewBag.AlertMessage = "An unexpected error occurred. Please try again.";
                return View();
            }
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

        [HttpGet("admin/usermanagement/listuser")]
        public async Task<IActionResult> ListUser(int page = 1, int pageSize = 10, string searchQuery = "")
        {
            try
            {
                //get users
                HttpResponseMessage normalUserResponse =
                    await client.GetAsync(client.BaseAddress + "/Users/GetUserByRole/" + (int)UserRoles.USER);
                //get users premium
                HttpResponseMessage premiumUserResponse =
                    await client.GetAsync(client.BaseAddress + "/Users/GetUserByRole/" + (int)UserRoles.USERPREMIUM);

                if (normalUserResponse.StatusCode == System.Net.HttpStatusCode.OK &&
                    premiumUserResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    HttpContent normalUserContent = normalUserResponse.Content;
                    string data = await normalUserContent.ReadAsStringAsync();
                    List<dynamic> normalUsersData = JsonConvert.DeserializeObject<List<dynamic>>(data);

                    HttpContent premiumUserContent = premiumUserResponse.Content;
                    string data1 = await premiumUserContent.ReadAsStringAsync();
                    List<dynamic> premiumUsersData = JsonConvert.DeserializeObject<List<dynamic>>(data1);

                    var users = normalUsersData.Select(
                        ud => new User
                        {
                            UserId = ud.id,
                            FirstName = ud.firstName,
                            LastName = ud.lastName,
                            Urlimage = ud.urlimage,
                            Dob = ud.dob,
                            Gender = ud.gender ?? false,
                            Address = ud.address,
                            Phone = ud.phone,
                            UserRole = new UserRole
                            {
                                RoleId = ud.role.roleId,
                                RoleName = ud.role.roleName,
                            },
                            IsActive = ud.isActive,
                            Account = ud.account,
                        }
                        );

                    var premiumUsers = premiumUsersData.Select(
                        ud => new User
                        {
                            UserId = ud.id,
                            FirstName = ud.firstName,
                            LastName = ud.lastName,
                            Urlimage = ud.urlimage,
                            Dob = ud.dob,
                            Gender = ud.gender ?? false,
                            Address = ud.address,
                            Phone = ud.phone,
                            UserRole = new UserRole
                            {
                                RoleId = ud.role.roleId,
                                RoleName = ud.role.roleName,
                            },
                            IsActive = ud.isActive,
                            Account = ud.account,
                        }
                        ).ToList();

                    users = users.Concat(premiumUsers).ToList();

                    // Search logic
                    if (!string.IsNullOrEmpty(searchQuery))
                    {
                        users = users.Where(u =>
                            (u.FirstName + " " + u.LastName).ToLower().Contains(searchQuery.ToLower())
                        ).ToList();
                    }

                    // Pagination logic
                    int totalUsers = users.Count();
                    var paginatedUsers = users.Skip((page - 1) * pageSize).Take(pageSize).ToList();

                    ViewBag.listUsers = paginatedUsers;
                    ViewBag.CurrentPage = page;
                    ViewBag.TotalPages = (int)Math.Ceiling(totalUsers / (double)pageSize);
                }
                else
                {
                    ViewBag.AlertMessage = "Cannot get list users! Please try again!";
                }

                ViewBag.SearchQuery = searchQuery;

                return View("~/Views/Admin/UserManagement/ListUser.cshtml");
            }
            catch (Exception ex)
            {
                ViewBag.AlertMessage = "An unexpected error occurred. Please try again!";
                return View("~/Views/Admin/UserManagement/ListUser.cshtml");
            }
        }

        [HttpGet("admin/usermanagement/userdetail/{userId}")]
        public async Task<IActionResult> UserDetail(int userId)
        {
            try
            {
                //get user
                HttpResponseMessage response =
                    await client.GetAsync(client.BaseAddress + "/Users/GetUserDetail/" + userId);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    HttpContent content = response.Content;
                    string data = await content.ReadAsStringAsync();
                    dynamic userData = JsonConvert.DeserializeObject<dynamic>(data);

                    User user = new()
                    {
                        UserId = userData.id,
                        FirstName = userData.firstName,
                        LastName = userData.lastName,
                        Urlimage = userData.urlimage,
                        Dob = userData.dob,
                        Gender = userData.gender ?? false,
                        Address = userData.address,
                        Phone = userData.phone,
                        UserRole = new UserRole
                        {
                            RoleId = userData.role.roleId,
                            RoleName = userData.role.roleName,
                        },
                        UserDetail = new UserDetail
                        {
                            UserId = userData.id,
                            DescribeYourself = userData.detailsInformation.description,
                            Height = userData.detailsInformation.height,
                            Weight = userData.detailsInformation.weight,
                            Age = userData.detailsInformation.age,
                            WantImprove = userData.detailsInformation.wantImprove,
                            UnderlyingDisease = userData.detailsInformation.underlyingDisease,
                            InforConfirmGood = userData.detailsInformation.inforConfirmGood,
                            InforConfirmBad = userData.detailsInformation.inforConfirmBad,
                            IsPremium = userData.detailsInformation.isPremium
                        },
                        IsActive = userData.isActive,
                        Account = userData.account,
                    };

                    ViewBag.user = user;

                    return View("~/Views/Admin/UserManagement/UserDetail.cshtml");
                }
                else
                {
                    ViewBag.AlertMessage = "Cannot get user detail information! Please try again!";
                    return View("~/Views/Admin/UserManagement/UserDetail.cshtml");
                }
            }
            catch (Exception)
            {
                ViewBag.AlertMessage = "An unexpected error occurred. Please try again!";
                return View("~/Views/Admin/UserManagement/UserDetail.cshtml");
            }
        }

        [HttpGet("admin/nutritionistmanagement/listnutritionist")]
        public async Task<IActionResult> ListNutritionist(int page = 1, int pageSize = 10, string searchQuery = "")
        {
            try
            {
                //get nutritionists
                HttpResponseMessage response =
                    await client.GetAsync(client.BaseAddress + "/Users/GetUserByRole/" + (int)UserRoles.NUTRITIONIST);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    HttpContent content = response.Content;
                    string data = await content.ReadAsStringAsync();
                    List<dynamic> nutritionistData = JsonConvert.DeserializeObject<List<dynamic>>(data);

                    var nutritionists = nutritionistData.Select(
                        ud => new User
                        {
                            UserId = ud.id,
                            FirstName = ud.firstName,
                            LastName = ud.lastName,
                            Urlimage = ud.urlimage,
                            Dob = ud.dob,
                            Gender = ud.gender ?? false,
                            Address = ud.address,
                            Phone = ud.phone,
                            UserRole = new UserRole
                            {
                                RoleId = ud.role.roleId,
                                RoleName = ud.role.roleName,
                            },
                            IsActive = ud.isActive,
                            Account = ud.account,
                        }).ToList();

                    // Search logic
                    if (!string.IsNullOrEmpty(searchQuery))
                    {
                        nutritionists = nutritionists.Where(u =>
                            (u.FirstName + " " + u.LastName).ToLower().Contains(searchQuery.ToLower())
                        ).ToList();
                    }

                    // Pagination logic
                    int totalNutritionists = nutritionists.Count();
                    var paginatedNutritionists = nutritionists.Skip((page - 1) * pageSize).Take(pageSize).ToList();

                    ViewBag.listNutritionists = paginatedNutritionists;
                    ViewBag.CurrentPage = page;
                    ViewBag.TotalPages = (int)Math.Ceiling(totalNutritionists / (double)pageSize);
                }
                else
                {
                    ViewBag.AlertMessage = "Cannot get list nutritionists! Please try again!";
                }

                ViewBag.SearchQuery = searchQuery;

                return View("~/Views/Admin/NutritionistManagement/ListNutritionist.cshtml");
            }
            catch (Exception ex)
            {
                ViewBag.AlertMessage = "An unexpected error occurred. Please try again!";
                return View("~/Views/Admin/NutritionistManagement/ListNutritionist.cshtml");
            }
        }

        [HttpGet("admin/nutritionistmanagement/nutritionistdetail/{nutritionistId}")]
        public async Task<IActionResult> NutritionistDetail(int nutritionistId)
        {
            try
            {
                //get nutritionist
                HttpResponseMessage response =
                    await client.GetAsync(client.BaseAddress + "/Users/GetNutritionistDetail/" + nutritionistId);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    HttpContent content = response.Content;
                    string data = await content.ReadAsStringAsync();
                    dynamic userData = JsonConvert.DeserializeObject<dynamic>(data);

                    User nutritionist = new()
                    {
                        UserId = userData.id,
                        FirstName = userData.firstName,
                        LastName = userData.lastName,
                        Urlimage = userData.urlimage,
                        Dob = userData.dob,
                        Gender = userData.gender ?? false,
                        Address = userData.address,
                        Phone = userData.phone,
                        UserRole = new UserRole
                        {
                            RoleId = userData.role.roleId,
                            RoleName = userData.role.roleName,
                        },
                        NutritionistDetail = new NutritionistDetail
                        {
                            Id = userData.detailsInformation.id,
                            NutritionistId = userData.id,
                            DescribeYourself = userData.detailsInformation.description,
                            Height = userData.detailsInformation.height,
                            Weight = userData.detailsInformation.weight,
                            Age = userData.detailsInformation.age,
                            Rate = userData.detailsInformation.rate,
                            NumberRate = userData.detailsInformation.numberRate,
                        },
                        IsActive = userData.isActive,
                        Account = userData.account,
                    };

                    ViewBag.nutritionist = nutritionist;

                    //get nutritionist expert packages
                    HttpResponseMessage response1 =
                        await client.GetAsync(client.BaseAddress + "/Users/GetNutritionistPackages/" + nutritionist.NutritionistDetail.Id);

                    if (response1.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        HttpContent content1 = response1.Content;
                        string data1 = await content1.ReadAsStringAsync();
                        dynamic packagesData = JsonConvert.DeserializeObject<dynamic>(data1);

                        ExpertPackage package = new ExpertPackage
                        {
                            Id = packagesData.id,
                            NutritionistDetailsId = packagesData.nutritionistDetailsId,
                            Name = packagesData.name,
                            Describe = packagesData.describe,
                            Price = packagesData.price,
                            Duration = packagesData.duration
                        };

                        ViewBag.package = package;
                    }

                    return View("~/Views/Admin/NutritionistManagement/NutritionistDetail.cshtml");
                }
                else
                {
                    ViewBag.AlertMessage = "Cannot get nutritionist detail information! Please try again!";
                    return View("~/Views/Admin/NutritionistManagement/NutritionistDetail.cshtml");
                }
            }
            catch (Exception)
            {
                ViewBag.AlertMessage = "An unexpected error occurred. Please try again!";
                return View("~/Views/Admin/NutritionistManagement/NutritionistDetail.cshtml");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUserStatus(int userId, int status, string returnUrl)
        {
            try
            {
                var data = new
                {
                    userId = userId,
                    status = status == 1
                };

                string jsonData = JsonConvert.SerializeObject(data);

                HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response =
                    await client.PostAsync(client.BaseAddress + "/Users/UpdateUserStatus", content);

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    ViewBag.AlertMessage = "Cannot update user status! Please try again!";
                }
            }
            catch (Exception e)
            {
                ViewBag.AlertMessage = "An unexpected error occurred. Please try again!";
            }

            return Redirect(returnUrl);
        }

        [HttpGet("admin/ingredientmanagement/ingredientlist")]
        public async Task<IActionResult> IngredientsList(int page = 1, int pageSize = 10, string searchQuery = "")
        {
            try
            {
                //get ingredients
                HttpResponseMessage response =
                    await client.GetAsync(client.BaseAddress + "/Ingredient/GetAllIngredients");

                //get keynotes
                HttpResponseMessage response1 =
                    await client.GetAsync(client.BaseAddress + "/KeyNote/GetKeyNotes");
                HttpContent content1 = response1.Content;
                string data1 = await content1.ReadAsStringAsync();
                List<KeyNote> keyNotes = JsonConvert.DeserializeObject<List<KeyNote>>(data1);

                //get types of calculation
                HttpResponseMessage response2 =
                    await client.GetAsync(client.BaseAddress + "/TypeOfCalculation/GetTypesOfCalculation");
                HttpContent content2 = response2.Content;
                string data2 = await content2.ReadAsStringAsync();
                List<TypeOfCalculation> typesOfCalculation = JsonConvert.DeserializeObject<List<TypeOfCalculation>>(data2);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    HttpContent content = response.Content;
                    string data = await content.ReadAsStringAsync();
                    List<IngredientDetails100g> ingredients = JsonConvert.DeserializeObject<List<IngredientDetails100g>>(data);

                    // Search logic
                    if (!string.IsNullOrEmpty(searchQuery))
                    {
                        ingredients = ingredients.Where(u =>
                            (u.Name).ToLower().Contains(searchQuery.ToLower())
                        ).ToList();
                    }

                    // Pagination logic
                    int totalIngredients = ingredients.Count();
                    var paginatedIngredients = ingredients.Skip((page - 1) * pageSize).Take(pageSize).ToList();

                    ViewBag.listIngredients = paginatedIngredients;
                    ViewBag.CurrentPage = page;
                    ViewBag.TotalPages = (int)Math.Ceiling(totalIngredients / (double)pageSize);
                }
                else
                {
                    ViewBag.AlertMessage = "Cannot get list ingredients! Please try again!";
                }

                ViewBag.SearchQuery = searchQuery;
                ViewBag.listKeynotes = keyNotes;
                ViewBag.listTypesOfCalculation = typesOfCalculation;

                return View("~/Views/Admin/IngredientManagement/IngredientList.cshtml");
            }
            catch (Exception e)
            {
                ViewBag.AlertMessage = "An unexpected error occurred. Please try again!";
                return View("~/Views/Admin/IngredientManagement/IngredientList.cshtml");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddIngredient(string in_name, string in_desc, string in_imgurl, int keynoteId, short typeOfCalculationId)
        {
            try
            {
                var data = new
                {
                    keyNoteId = keynoteId,
                    name = in_name,
                    describe = in_desc,
                    urlimage = in_imgurl,
                    typeOfCalculationId = typeOfCalculationId
                };

                string jsonData = JsonConvert.SerializeObject(data);

                HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response =
                    await client.PostAsync(client.BaseAddress + "/Ingredient/AddIngredient", content);

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    ViewBag.AlertMessage = "Add ingredient failed! Please try again!";
                }
                else
                {
                    ViewBag.SuccessMessage = "Add ingredient successfully!";
                }
            }
            catch (Exception e)
            {
                ViewBag.AlertMessage = "An unexpected error occurred. Please try again!";
            }

            return await IngredientsList();
        }

        [HttpGet("admin/ingredientmanagement/updateingredient/{Id}")]
        public async Task<IActionResult> UpdateIngredient(int Id)
        {
            try
            {
                //get ingredients
                HttpResponseMessage response =
                    await client.GetAsync(client.BaseAddress + "/Ingredient/GetIngredient/" + Id);

                //get keynotes
                HttpResponseMessage response1 =
                    await client.GetAsync(client.BaseAddress + "/KeyNote/GetKeyNotes");
                HttpContent content1 = response1.Content;
                string data1 = await content1.ReadAsStringAsync();
                List<KeyNote> keyNotes = JsonConvert.DeserializeObject<List<KeyNote>>(data1);

                //get types of calculation
                HttpResponseMessage response2 =
                    await client.GetAsync(client.BaseAddress + "/TypeOfCalculation/GetTypesOfCalculation");
                HttpContent content2 = response2.Content;
                string data2 = await content2.ReadAsStringAsync();
                List<TypeOfCalculation> typesOfCalculation = JsonConvert.DeserializeObject<List<TypeOfCalculation>>(data2);

                ViewBag.keyNotes = keyNotes;
                ViewBag.typesOfCalculation = typesOfCalculation;

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    HttpContent content = response.Content;
                    string data = await content.ReadAsStringAsync();
                    IngredientDetails100g ingredients = JsonConvert.DeserializeObject<IngredientDetails100g>(data);

                    return View("~/Views/Admin/IngredientManagement/EditIngredient.cshtml", ingredients);
                }
                else
                {
                    ViewBag.AlertMessage = "Cannot get ingredient! Please try again!";
                    return View("~/Views/Admin/IngredientManagement/EditIngredient.cshtml");
                }
            }
            catch (Exception e)
            {
                ViewBag.AlertMessage = "An unexpected error occurred. Please try again!";
                return View("~/Views/Admin/IngredientManagement/EditIngredient.cshtml");
            }
        }

        [HttpPost("admin/ingredientmanagement/updateingredient")]
        public async Task<IActionResult> DoUpdateIngredient(IngredientDetails100g model)
        {
            try
            {
                var data = new
                {
                    id = model.Id,
                    keyNoteId = model.KeyNoteId,
                    name = model.Name,
                    describe = model.Describe,
                    urlimage = model.Urlimage,
                    typeOfCalculationId = model.TypeOfCalculationId,
                    energy = model.Energy,
                    water = model.Water,
                    protein = model.Protein,
                    fat = model.Fat,
                    carbohydrate = model.Carbohydrate,
                    fiber = model.Fiber,
                    ash = model.Ash,
                    sugar = model.Sugar,
                    galactose = model.Galactose,
                    maltose = model.Maltose,
                    lactose = model.Lactose,
                    fructose = model.Fructose,
                    glucose = model.Glucose,
                    sucrose = model.Sucrose,
                    calcium = model.Calcium,
                    iron = model.Iron,
                    magnesium = model.Magnesium,
                    manganese = model.Manganese,
                    phosphorous = model.Phosphorous,
                    potassium = model.Potassium,
                    sodium = model.Sodium,
                    zinc = model.Zinc,
                    copper = model.Copper,
                    selenium = model.Selenium,
                    vitaminC = model.VitaminC,
                    vitaminB1 = model.VitaminB1,
                    vitaminB2 = model.VitaminB2,
                    vitaminPp = model.VitaminPp,
                    vitaminB5 = model.VitaminB5,
                    vitaminB6 = model.VitaminB6,
                    folat = model.Folat,
                    vitaminB9 = model.VitaminB9,
                    vitaminH = model.VitaminH,
                    vitaminB12 = model.VitaminB12,
                    vitaminA = model.VitaminA,
                    vitaminD = model.VitaminD,
                    vitaminE = model.VitaminE,
                    vitaminK = model.VitaminK,
                    betaCaroten = model.BetaCaroten,
                    alphaCaroten = model.AlphaCaroten,
                    betaCryptoxanthin = model.BetaCryptoxanthin,
                    lycopen = model.Lycopen,
                    luteinVsZeaxanthin = model.LuteinVsZeaxanthin,
                    purin = model.Purin,
                    totalIsoflavone = model.TotalIsoflavone,
                    daidzein = model.Daidzein,
                    genistein = model.Genistein,
                    glycetin = model.Glycetin,
                    totalSaturatedFattyAcid = model.TotalSaturatedFattyAcid,
                    palmiticC160 = model.PalmiticC160,
                    margaricC170 = model.MargaricC170,
                    stearicC180 = model.StearicC180,
                    arachidicC200 = model.ArachidicC200,
                    behenicC220 = model.BehenicC220,
                    lignocericC240 = model.LignocericC240,
                    totalMonounsaturatedFattyAcid = model.TotalMonounsaturatedFattyAcid,
                    myristoleicC141 = model.MyristoleicC141,
                    palmitoleicC161 = model.PalmitoleicC161,
                    oleicC181 = model.OleicC181,
                    totalPolyunsaturatedFattyAcid = model.TotalPolyunsaturatedFattyAcid,
                    linoleicC182N6 = model.LinoleicC182N6,
                    linolenicC182N3 = model.LinolenicC182N3,
                    arachidonicC204 = model.ArachidonicC204,
                    eicosapentaenoicC205N3 = model.EicosapentaenoicC205N3,
                    docosahexaenoicC226N3 = model.DocosahexaenoicC226N3,
                    totalTransFattyAcid = model.TotalTransFattyAcid,
                    cholesterol = model.Cholesterol,
                    phytosterol = model.Phytosterol,
                    lysin = model.Lysin,
                    methionin = model.Methionin,
                    tryptophan = model.Tryptophan,
                    phenylalanin = model.Phenylalanin,
                    threonin = model.Threonin,
                    valin = model.Valin,
                    leucin = model.Leucin,
                    isoleucin = model.Isoleucin,
                    arginin = model.Arginin,
                    histidin = model.Histidin,
                    cystin = model.Cystin,
                    tyrosin = model.Tyrosin,
                    alanin = model.Alanin,
                    acidAspartic = model.AcidAspartic,
                    acidGlutamic = model.AcidGlutamic,
                    glycin = model.Glycin,
                    prolin = model.Prolin,
                    serin = model.Serin
                };

                string jsonData = JsonConvert.SerializeObject(data);

                HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response =
                    await client.PutAsync(client.BaseAddress + "/Ingredient/UpdateIngredient", content);

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    ViewBag.AlertMessage = "Update ingredient failed! Please try again!";
                }
                else
                {
                    ViewBag.SuccessMessage = "Update ingredient successfully!";
                }
            }
            catch (Exception e)
            {
                ViewBag.AlertMessage = "An unexpected error occurred. Please try again!";
            }
            return await UpdateIngredient(model.Id);
        }

        [HttpGet("admin/ingredientmanagement/deleteingredient/{Id}")]
        public async Task<IActionResult> DeleteIngredient(int Id)
        {
            try
            {
                HttpResponseMessage response =
                    await client.DeleteAsync(client.BaseAddress + "/Ingredient/RemoveIngredient/" + Id);
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    ViewBag.AlertMessage = "Cannot delete ingredient! Please try again!";
                }
                else
                {
                    ViewBag.SuccessMessage = "Delete ingredient successfully!";
                }
            }
            catch (Exception e)
            {
                ViewBag.AlertMessage = "An unexpected error occurred. Please try again!";
            }

            return await IngredientsList();
        }

        [HttpGet("admin/expertpackagemanagement/listpackages")]
        public async Task<IActionResult> ListPackages(int page = 1, int pageSize = 10, string searchQuery = "")
        {
            try
            {
                // get list packages
                HttpResponseMessage response =
                        await client.GetAsync(client.BaseAddress + "/ExpertPackage/GetAllExpertPackage");

                //get list nutritionist
                HttpResponseMessage response1 =
                        await client.GetAsync(client.BaseAddress + "/Users/GetUserByRole/" + (int)UserRoles.NUTRITIONIST);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    HttpContent content = response.Content;
                    string data = await content.ReadAsStringAsync();
                    List<ExpertPackageResponse> packagesData = JsonConvert.DeserializeObject<List<ExpertPackageResponse>>(data);

                    HttpContent content1 = response1.Content;
                    string data1 = await content1.ReadAsStringAsync();
                    List<dynamic> nutritionists = JsonConvert.DeserializeObject<List<dynamic>>(data1);

                    List<ExpertPackageResponse.User> nutritionists1 = nutritionists.Select(
                        n => new ExpertPackageResponse.User
                        {
                            Id = n.id,
                            Name = n.firstName + " " + n.lastName,
                            Account = n.account
                        }).ToList();

                    // Search logic
                    if (!string.IsNullOrEmpty(searchQuery))
                    {
                        packagesData = packagesData.Where(u =>
                            u.Package.Name.ToLower().Contains(searchQuery.ToLower())
                        ).ToList();
                    }

                    // Pagination logic
                    int totalPackages = packagesData.Count();
                    var paginatedPackages = packagesData.Skip((page - 1) * pageSize).Take(pageSize).ToList();

                    ViewBag.packages = paginatedPackages;
                    ViewBag.nutritionists = nutritionists1;
                    ViewBag.CurrentPage = page;
                    ViewBag.TotalPages = (int)Math.Ceiling(totalPackages / (double)pageSize);


                }
                else
                {
                    ViewBag.AlertMessage = "Cannot get list packages! Please try again!";
                }

                ViewBag.SearchQuery = searchQuery;
                return View("~/Views/Admin/ExpertPackageManagement/ListPackages.cshtml");
            }
            catch (Exception ex)
            {
                ViewBag.AlertMessage = "An unexpected error occurred. Please try again!";
                return View("~/Views/Admin/ExpertPackageManagement/ListPackages.cshtml");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddPackage(string p_name, string p_desc, decimal p_price, short p_duration)
        {
            try
            {
                var data = new
                {
                    id = 0,
                    name = p_name,
                    describe = p_desc,
                    price = p_price,
                    duration = p_duration
                };

                string jsonData = JsonConvert.SerializeObject(data);

                HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response =
                    await client.PostAsync(client.BaseAddress + "/ExpertPackage/AddExpertPackage", content);

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    ViewBag.AlertMessage = "Add package failed! Please try again!";
                }
                else
                {
                    ViewBag.SuccessMessage = "Add package successfully!";
                }
            }
            catch (Exception e)
            {
                ViewBag.AlertMessage = "An unexpected error occurred. Please try again!";
            }

            return await ListPackages();
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePackage(int p_id, string p_name, string p_desc, decimal p_price, short p_duration)
        {
            try
            {
                var data = new
                {
                    id = p_id,
                    name = p_name,
                    describe = p_desc,
                    price = p_price,
                    duration = p_duration
                };

                string jsonData = JsonConvert.SerializeObject(data);

                HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response =
                    await client.PutAsync(client.BaseAddress + "/ExpertPackage/UpdateExpertPackage", content);

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    ViewBag.AlertMessage = "Update package failed! Please try again!";
                }
                else
                {
                    ViewBag.SuccessMessage = "Update package successfully!";
                }
            }
            catch (Exception e)
            {
                ViewBag.AlertMessage = "An unexpected error occurred. Please try again!";
            }

            return await ListPackages();
        }

        [HttpGet("admin/expertpackagemanagement/deletepackage/{Id}")]
        public async Task<IActionResult> DeletePackage(int Id)
        {
            try
            {
                HttpResponseMessage response =
                    await client.DeleteAsync(client.BaseAddress + "/ExpertPackage/DeleteExpertPackage/" + Id);
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    HttpContent content = response.Content;
                    string data = await content.ReadAsStringAsync();
                    string errMsg = JsonConvert.DeserializeObject<string>(data);

                    ViewBag.AlertMessage = errMsg;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    ViewBag.SuccessMessage = "Delete package successfully!";
                }
                else
                {
                    ViewBag.AlertMessage = "Cannot delete package! Please try again!";
                }
            }
            catch (Exception e)
            {
                ViewBag.AlertMessage = "An unexpected error occurred. Please try again!";
            }

            return await ListPackages();
        }

        [HttpGet("admin/foodmanagement/foodingredient/{foodId}")]
        public async Task<IActionResult> FoodListIngredients(int foodId)
        {
            try
            {
                // get food
                HttpResponseMessage response =
                        await client.GetAsync(client.BaseAddress + "/Food/GetFoodById/" + foodId);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    HttpContent content = response.Content;
                    string data = await content.ReadAsStringAsync();
                    dynamic result = JsonConvert.DeserializeObject<dynamic>(data);
                    FoodList food = result.food.ToObject<FoodList>();

                    // get all ingredients
                    HttpResponseMessage response1 =
                            await client.GetAsync(client.BaseAddress + "/Ingredient/GetAllIngredients");
                    HttpContent content1 = response1.Content;
                    string data1 = await content1.ReadAsStringAsync();
                    List<IngredientDetails100g> ingredients = JsonConvert.DeserializeObject<List<IngredientDetails100g>>(data1);

                    //get food ingredients
                    HttpResponseMessage response2 =
                            await client.GetAsync(client.BaseAddress + "/Food/GetFoodIngredient/" + foodId);
                    HttpContent content2 = response2.Content;
                    string data2 = await content2.ReadAsStringAsync();
                    List<IngredientDetails100g> foodIngredients = JsonConvert.DeserializeObject<List<IngredientDetails100g>>(data2);

                    ingredients.RemoveAll(item => foodIngredients.Any(fi => fi.Id == item.Id));

                    ViewBag.food = food;
                    ViewBag.allIngredients = ingredients;
                    ViewBag.foodIngredients = foodIngredients;
                }
            }
            catch (Exception ex)
            {
                ViewBag.AlertMessage = "An unexpected error occurred. Please try again!";
            }

            return View("~/Views/Admin/FoodIngredients.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> AddIngredientToFood(int foodId, int ingreId, double amount)
        {
            try
            {
                var data = new
                {
                    foodid = foodId,
                    ingredientId = ingreId,
                    amount = amount
                };

                string jsonData = JsonConvert.SerializeObject(data);

                HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response =
                    await client.PostAsync(client.BaseAddress + "/Food/AddIngredientToFood", content);

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    ViewBag.AlertMessage = "Add ingredient failed! Please try again!";
                }
                else
                {
                    ViewBag.SuccessMessage = "Add ingredient successfully!";
                }
            }
            catch (Exception ex)
            {
                ViewBag.AlertMessage = "An unexpected error occurred. Please try again!";
            }

            return await FoodListIngredients(foodId);
        }

        [HttpGet("admin/foodmanagement/deleteingredient/{foodId}/{ingreId}")]
        public async Task<IActionResult> RemoveIngredientFromFood(int foodId, int ingreId)
        {
            try
            {
                HttpResponseMessage response =
                    await client.DeleteAsync(client.BaseAddress + "/Food/DeleteIngredientFromFood/" + foodId + "/" + ingreId);
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    ViewBag.AlertMessage = "Cannot remove ingredient! Please try again!";
                }
                else
                {
                    ViewBag.SuccessMessage = "Remove ingredient successfully!";
                }
            }
            catch (Exception e)
            {
                ViewBag.AlertMessage = "An unexpected error occurred. Please try again!";
            }

            return await FoodListIngredients(foodId);
        }

        ////////////////////////////////////////////////////////////
        /// Tùng
        ////////////////////////////////////////////////////////////
        ///
        [HttpGet, Authorize(Roles = "Admin, Nutritionist")]
        public async Task<IActionResult> FoodList(string search, string diseaseSearch, int foodPage = 1, int diseasePage = 1, int fAndDPage = 1)
        {
            var foodList = await GetFoodList(search, foodPage);

            var foodTypes = await GetFoodTypes();
            var keyNotes = await GetKeyNotes();
            var cookingDifficulty = await GetCookingDifficulty();
            var listOfDisease = await GetDiseaseList(diseaseSearch, diseasePage);
            var foodAndDiseases = await GetFoodAndDiseaseList(fAndDPage);

            // Tạo dictionary chứa danh sách recipe cho từng món ăn
            var recipesByFood = new Dictionary<int, List<RecipeDT>>();

            foreach (var food in foodList)
            {
                var recipes = await GetFoodRecipes(food.FoodListId);
                recipesByFood[food.FoodListId] = recipes;
            }

            var viewModel = new FoodListViewModel
            {
                Foods = foodList,
                FoodTypes = foodTypes,
                KeyNotes = keyNotes,
                CookingDifficulties = cookingDifficulty,
                ListOfDiseases = listOfDisease,
                FoodAndDiseases = foodAndDiseases,
                RecipesByFood = recipesByFood,
                apiUrl = client.BaseAddress.ToString()
            };

            return View(viewModel);
        }

        [HttpGet, Authorize(Roles = "Admin, Nutritionist")]
        public async Task<List<RecipeDT>> GetFoodRecipes(int foodId)
        {
            var requestUrl = $"{client.BaseAddress}/Food/GetFoodRecipe/{foodId}";

            var response = await client.GetAsync(requestUrl);
            if (!response.IsSuccessStatusCode)
            {
                return new List<RecipeDT>();
            }

            var responseData = await response.Content.ReadAsStringAsync();
            var recipes = JsonConvert.DeserializeObject<List<RecipeDT>>(responseData);

            return recipes ?? new List<RecipeDT>();
        }

        [HttpGet, Authorize(Roles = "Admin, Nutritionist")]
        public async Task<List<Food>> GetFoodList(string search, int foodPage = 1, int foodPageSize = 10)
        {
            var requestUrl = client.BaseAddress + $"/Nutrition/get-food-list?search={search}&Page={foodPage}&pageSize={foodPageSize}";
            Console.WriteLine("foodPage: "+ foodPage);

            var response = await client.GetAsync(requestUrl);

            if (!response.IsSuccessStatusCode)
            {
                return new List<Food>();
            }
            var responseData = await response.Content.ReadAsStringAsync();
            var foodListResponse = JsonConvert.DeserializeObject<PagedResult<Food>>(responseData);
            ViewBag.FoodSearch = search;
            ViewBag.FoodTotalPages = foodListResponse.TotalPages;
            ViewBag.FoodCurrentPage = foodListResponse.CurrentPage;

            Console.WriteLine("Food count: "+foodListResponse.Items.Count());

            return foodListResponse.Items ?? new List<Food>();
        }

        [HttpPost, Authorize(Roles = "Admin, Nutritionist")]
        public async Task<IActionResult> SaveFood(Food food, IFormFile ImageFile)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CreateFoodAlert = "Dữ liệu không hợp lệ";
            }

            try
            {
                // Xử lý file ảnh upload
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    var fileExtension = Path.GetExtension(ImageFile.FileName).ToLower();

                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        return Json(new { success = false, message = "Chỉ chấp nhận các định dạng hình ảnh: .jpg, .jpeg, .png, .gif." });
                    }

                    if (ImageFile.Length > 5 * 1024 * 1024) // 5MB
                    {
                        return Json(new { success = false, message = "Kích thước ảnh không được vượt quá 5MB." });
                    }

                    var fileName = Path.GetFileName(ImageFile.FileName);
                    var filePath = Path.Combine("wwwroot/images/foods", fileName);

                    if (!Directory.Exists("wwwroot/images/foods"))
                    {
                        Directory.CreateDirectory("wwwroot/images/foods");
                    }

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(stream);
                    }

                    food.UrlImage = "/images/foods/" + fileName;
                }

                // Chuẩn bị dữ liệu gửi qua API
                var apiUrl = client.BaseAddress + "/Nutrition/save-food";
                var foodData = new
                {
                    food.FoodListId,
                    food.Name,
                    food.Describe,
                    food.Rate,
                    food.NumberRate,
                    food.UrlImage,
                    food.FoodTypeId,
                    food.KeyNoteId,
                    food.IsActive,
                    food.PreparationTime,
                    food.CookingTime,
                    food.CookingDifficultyId
                };

                var content = new StringContent(JsonConvert.SerializeObject(foodData), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    ViewBag.CreateFoodAlert = "Lưu thức ăn thành công!";
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Có lỗi xảy ra khi tạo/cập nhật food.");
                    Console.WriteLine("Status Code: " + response.StatusCode);
                    Console.WriteLine("Error Content: " + errorContent);

                    ViewBag.CreateFoodAlert = "Có lỗi xảy ra khi lưu thức ăn";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                ViewBag.CreateFoodAlert = "Có lỗi xảy ra khi xử lý yêu cầu của bạn!";
            }
            return RedirectToAction("FoodList");
        }

        [HttpGet, Authorize(Roles = "Admin, Nutritionist")]
        public async Task<List<FoodType>> GetFoodTypes()
        {
            var response = await client.GetAsync($"{client.BaseAddress}/Food/GetFoodTypes");
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var foodTypesResponse = JsonConvert.DeserializeObject<List<FoodType>>(responseData);
                return foodTypesResponse;
            }
            return new List<FoodType>();
        }

        [HttpGet, Authorize(Roles = "Admin, Nutritionist")]
        public async Task<List<KeyNote>> GetKeyNotes()
        {
            var requestUrl = client.BaseAddress + "/KeyNote/GetKeyNotes";

            var response = await client.GetAsync(requestUrl);

            if (!response.IsSuccessStatusCode)
            {
                return new List<KeyNote>();
            }

            var responseData = await response.Content.ReadAsStringAsync();
            var keyNoteResponse = JsonConvert.DeserializeObject<List<KeyNote>>(responseData);

            foreach (var item in keyNoteResponse)
            {
                if (string.IsNullOrEmpty(item.KeyList))
                    item.KeyList = "";

                // Chia nhỏ chuỗi dựa trên dấu #
                var parts = item.KeyList.Split('#', StringSplitOptions.RemoveEmptyEntries);

                // Ghép các phần tử lại với dấu xuống dòng
                item.KeyList = string.Join("\n", parts);
            }

            return keyNoteResponse ?? new List<KeyNote>();
        }

        [HttpGet, Authorize(Roles = "Admin, Nutritionist")]
        public async Task<List<CookingDifficulty>> GetCookingDifficulty()
        {
            var requestUrl = client.BaseAddress + "/CookingDifficulties/GetAllCookingDifficulties";
            var response = await client.GetAsync(requestUrl);

            if (!response.IsSuccessStatusCode)
            {
                return new List<CookingDifficulty>();
            }

            var responseData = await response.Content.ReadAsStringAsync();
            var cookingDifficultyResponse = JsonConvert.DeserializeObject<List<CookingDifficulty>>(responseData);

            return cookingDifficultyResponse ?? new List<CookingDifficulty>();
        }

        [HttpGet, Authorize(Roles = "Admin, Nutritionist")]
        public async Task<List<ListOfDisease>> GetDiseaseList(string search, int page = 1, int pageSize = 10)
        {
            var requestUrl = client.BaseAddress + $"/Nutrition/list-disease?search={search}&page={page}&pageSize={pageSize}";

            var response = await client.GetAsync(requestUrl);

            if (!response.IsSuccessStatusCode)
            {
                return new List<ListOfDisease>();
            }
            var responseData = await response.Content.ReadAsStringAsync();
            var diseaseListResponse = JsonConvert.DeserializeObject<PagedResult<ListOfDisease>>(responseData);
            ViewBag.DiseaseSearch = search;
            ViewBag.DiseaseTotalPages = diseaseListResponse.TotalPages;
            ViewBag.DiseaseCurrentPage = diseaseListResponse.CurrentPage;

            return diseaseListResponse.Items ?? new List<ListOfDisease>();
        }

        [HttpPost, Authorize(Roles = "Admin, Nutritionist")]
        public async Task<IActionResult> SaveDisease(ListOfDisease disease)
        {
            using (var httpClient = new HttpClient())
            {
                var apiUrl = client.BaseAddress + "/Nutrition/save-disease";
                var diseaseData = new
                {
                    disease.Id,
                    disease.Name,
                    disease.Describe
                };

                var content = new StringContent(JsonConvert.SerializeObject(diseaseData), Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Tạo/Cập nhật bệnh thành công!";
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    TempData["Message"] = "Có lỗi xảy ra khi tạo/cập nhật bệnh.";
                    Console.WriteLine("Có lỗi xảy ra khi tạo/cập nhật bệnh.");
                    Console.WriteLine("Status Code: " + response.StatusCode);
                    Console.WriteLine("Error Content: " + errorContent);
                }
            }

            return RedirectToAction("FoodList");
        }

        [HttpPost, Authorize(Roles = "Admin, Nutritionist")]
        public async Task<IActionResult> SaveFoodAndDisease(FoodAndDisease model)
        {
            var apiUrl = client.BaseAddress + "/Nutrition/create-food-and-disease";
            var diseaseData = new
            {
                model.FoodListId,
                model.ListOfDiseasesId,
                model.Describe,
                model.IsGoodOrBad
            };
            var content = new StringContent(JsonConvert.SerializeObject(diseaseData), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                TempData["Message"] = "Tạo/Cập nhật bảng so sánh thành công!";
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                TempData["Message"] = "Có lỗi xảy ra khi tạo/cập nhật bảng so sánh.";
                Console.WriteLine("Có lỗi xảy ra khi tạo/cập nhật bảng so sánh.");
                Console.WriteLine("Status Code: " + response.StatusCode);
                Console.WriteLine("Error Content: " + errorContent);
            }

            return RedirectToAction("FoodList");
        }

        [HttpGet, Authorize(Roles = "Admin, Nutritionist")]
        public async Task<List<ListFoodAndDisease>> GetFoodAndDiseaseList(int page = 1, int pageSize = 10)
        {
            var requestUrl = client.BaseAddress + $"/Nutrition/get-all-food-and-disease?page={page}&pageSize={pageSize}";

            var response = await client.GetAsync(requestUrl);

            if (!response.IsSuccessStatusCode)
            {
                return new List<ListFoodAndDisease>();
            }
            var responseData = await response.Content.ReadAsStringAsync();
            var diseaseListResponse = JsonConvert.DeserializeObject<PagedResult<ListFoodAndDisease>>(responseData);
            ViewBag.FandDTotalPages = diseaseListResponse.TotalPages;
            ViewBag.FandDCurrentPage = diseaseListResponse.CurrentPage;
            return diseaseListResponse.Items ?? new List<ListFoodAndDisease>();
        }

        [HttpPost, Authorize(Roles = "Admin, Nutritionist")]
        public async Task<IActionResult> SaveFoodRecipe([FromBody] SaveFoodRecipeDTO model)
        {
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{client.BaseAddress}/Food/SaveFoodRecipes", content);

            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true, message = "Recipe saved successfully!" });
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                return Json(new { success = false, message = $"Failed to save recipe: {errorContent}" });
            }

        }

    }
}

