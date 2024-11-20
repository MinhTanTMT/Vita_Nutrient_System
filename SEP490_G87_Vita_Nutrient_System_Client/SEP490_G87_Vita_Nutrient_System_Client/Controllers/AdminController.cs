using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SEP490_G87_Vita_Nutrient_System_Client.Domain.Enums;
using SEP490_G87_Vita_Nutrient_System_Client.Models;
using System.Net.Http.Headers;
using System.Text;
﻿using Microsoft.AspNetCore.Authorization;
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
        private AdminSevices adminSevices;
        public AdminController()
        {
            adminSevices = new AdminSevices();
            Uri URIBase = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetValue<Uri>("myUri");
            client = new HttpClient();
            client.BaseAddress = URIBase;
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }


        //[HttpGet, Authorize]
        public async Task<IActionResult> QRCodePaymentPageAsync()
        {
            try
            {
                int userId = int.Parse(User.FindFirst("UserId")?.Value);
                int typeInsert = 1;
                string? accountNumber = HttpContext.Session.GetString("accountNumberQRPay");
                int? limit = Int32.Parse(HttpContext.Session.GetString("limitQRPay"));
                decimal? amountInPay = decimal.Parse(HttpContext.Session.GetString("amountInPayQRPay"));
                string? contentBankPay = HttpContext.Session.GetString("contentBankPayQRPay");
                string? contentBankImg = HttpContext.Session.GetString("contentBankImgQRPay");

                //int amountWithoutDecimal = HttpContext.Session.GetString("amountInPayQRPay") is string amountInPayString &&
                //           decimal.TryParse(amountInPayString, out decimal amountInImg)
                //? (int)(amountInPay * 1000) // Nhân 1000 để chuyển "100.0000" thành "100000"
                //: 0;

                int? amountWithoutDecimal = HttpContext.Session.GetString("amountInPayQRPay") is string amountInPayString &&
                    decimal.TryParse(amountInPayString, out decimal amountInImg)
                ? (int)amountInPay // Chỉ lấy phần nguyên, bỏ qua phần thập phân
                : 0;

                int? NutritionistId = Int32.Parse(HttpContext.Session.GetString("NutritionistId"));
                string? Describe = HttpContext.Session.GetString("Describe");
                //decimal? Price = decimal.Parse(HttpContext.Session.GetString("Price"));
                int? Duration = Int32.Parse(HttpContext.Session.GetString("Duration"));


                if (accountNumber != null && limit != null && amountInPay != null && contentBankPay != null && amountWithoutDecimal != null && contentBankImg != null)
                {
                    HttpContext.Session.Remove("accountNumberQRPay");
                    HttpContext.Session.Remove("limitQRPay");
                    HttpContext.Session.Remove("amountInPayQRPay");
                    HttpContext.Session.Remove("contentBankPayQRPay");
                    HttpContext.Session.Remove("amountInImgQRPay");
                    HttpContext.Session.Remove("contentBankImgQRPay");


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


                        if (typeInsert == 1)
                        {
                            ViewData["UserListManagement"] = new UserListManagement { NutritionistId = NutritionistId ?? 0, UserId = userId, Describe = Describe, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(Duration ?? 0) , IsDone = false };
                        }
                        else
                        {
                            ViewData["UserListManagement"] = new UserListManagement { NutritionistId = NutritionistId ?? 0, UserId = userId, Describe = Describe, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(Duration ?? 0), IsDone = false };
                        }

                        int roleUser = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetValue<int>("roleAdmin");
                        TransactionsSystem transactionsSystem = new TransactionsSystem() 
                        {
                            UserPayId = userId,
                            PayeeId = roleUser,
                            AccountNumber = accountNumber,
                            AmountIn = amountInPay,
                            TransactionContent = contentBankPay
                        };

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
                        return Redirect("Error");
                    }

                }
                ViewBag.AlertMessage = "Error";
                return Redirect("Error");
            }
            catch (Exception ex)
            {
                ViewBag.AlertMessage = "An unexpected error occurred. Please try again.";
                return Redirect("Error");
            }
        }


        [HttpPost]
        public IActionResult PaymentForPaidServices(int NutritionistId, string? Describe, decimal Price, short Duration)
        {
            var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
            string? accountNumber = configuration.GetValue<string>("accountNumberQRPay");
            int? limit = configuration.GetValue<int>("limitQRPay");

            HttpContext.Session.SetString("NutritionistId", NutritionistId.ToString());
            HttpContext.Session.SetString("Describe", Describe ?? "");
            //HttpContext.Session.SetString("Price", Price.ToString());
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


        [HttpGet]
        public IActionResult PremiumUpgradeSuggestion()
        {
            return View();
        }
        

        [HttpGet]
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





        [HttpPost]
        public IActionResult PaymentTransferSuccessful()
        {



            return RedirectToAction("QRCodePaymentPage");
        }


        [HttpGet]
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
                        ViewBag.GraphDataString3 = JsonConvert.SerializeObject((graphData.SelectMany(row => row).Max()/100)*111); 
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
                    List<dynamic> normalUsersData = JsonConvert.DeserializeObject< List<dynamic>>(data);

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

                    users = users.Concat( premiumUsers ).ToList();

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

                if(response.StatusCode == System.Net.HttpStatusCode.OK)
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
                        List<dynamic> packagesData = JsonConvert.DeserializeObject<List<dynamic>>(data1);

                        List<ExpertPackage> packages = packagesData.Select(
                            p => new ExpertPackage
                            {
                                Id = p.id,
                                NutritionistDetailsId = p.nutritionistDetailsId,
                                Name = p.name,
                                Describe = p.describe,
                                Price = p.price,
                                Duration = p.duration
                            })
                            .ToList();

                        ViewBag.packages = packages;
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
            catch(Exception e)
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
            catch(Exception e)
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
            catch(Exception e)
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
            catch(Exception e)
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
            catch(Exception e)
            {
                ViewBag.AlertMessage = "An unexpected error occurred. Please try again!";
            }

            return await IngredientsList();
        }

        ////////////////////////////////////////////////////////////
        /// Tùng
        ////////////////////////////////////////////////////////////
        ///

    }
}
