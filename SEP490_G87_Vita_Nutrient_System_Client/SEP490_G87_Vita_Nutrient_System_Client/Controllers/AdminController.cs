using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SEP490_G87_Vita_Nutrient_System_Client.Domain.Enums;
using SEP490_G87_Vita_Nutrient_System_Client.Models;
using System.Net.Http.Headers;
using System.Text;
﻿using Microsoft.AspNetCore.Authorization;
using System.Security.Principal;
using static System.Net.WebRequestMethods;

namespace SEP490_G87_Vita_Nutrient_System_Client.Controllers
{
	public class AdminController : Controller
	{
        ////////////////////////////////////////////////////////////
        /// Tân
        ////////////////////////////////////////////////////////////
        ///

        private readonly HttpClient client = null;
        public AdminController()
        {
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

                string? accountNumber = HttpContext.Session.GetString("accountNumberQRPay");
                int? limit = Int32.Parse(HttpContext.Session.GetString("limitQRPay"));
                decimal? amountInPay = decimal.Parse(HttpContext.Session.GetString("amountInPayQRPay"));
                string? contentBankPay = HttpContext.Session.GetString("contentBankPayQRPay");
                decimal? amountInImg = decimal.Parse(HttpContext.Session.GetString("amountInImgQRPay"));
                string? contentBankImg = HttpContext.Session.GetString("contentBankImgQRPay");

                if (accountNumber != null && limit != null && amountInPay != null && contentBankPay != null && amountInImg != null && contentBankImg != null)
                {
                    HttpContext.Session.Remove("accountNumberQRPay");
                    HttpContext.Session.Remove("limitQRPay");
                    HttpContext.Session.Remove("amountInPayQRPay");
                    HttpContext.Session.Remove("contentBankPayQRPay");
                    HttpContext.Session.Remove("amountInImgQRPay");
                    HttpContext.Session.Remove("contentBankImgQRPay");

                    string checkQRPaySuccess = client.BaseAddress + $"/BankPayment/APICheckQRPaySuccessful?accountNumber={accountNumber}&limit={limit}&content={contentBankPay}&amountIn={amountInPay}";
                    HttpResponseMessage res = await client.GetAsync(client.BaseAddress + $"/BankPayment/APIGetQRPayDefaultSystem?amount={amountInImg}&content={contentBankImg}");

                    if (res.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        HttpContent content = res.Content;
                        string data = await content.ReadAsStringAsync();
                        string linkQRImage = JsonConvert.DeserializeObject<string>(data);

                        ViewBag.CheckQRPaySuccess = checkQRPaySuccess;
                        ViewBag.LinkQRImage = linkQRImage;
                        return View();
                    }
                    
                }
                ViewBag.AlertMessage = "Error";
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.AlertMessage = "An unexpected error occurred. Please try again.";
                return View();
            }
        }

        //[HttpGet, Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult AdminStatistics()
        {



            return View();
        }


        [HttpGet]
        public async Task<IActionResult> AdminDashboardAsync()
        {
            try
            {
                HttpResponseMessage res = await client.GetAsync(client.BaseAddress + $"/BankPayment/APIGetAllTransactionsSystem?userMainId=1");

                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    HttpContent content = res.Content;
                    string data = await content.ReadAsStringAsync();
                    IEnumerable<TransactionsSystem> transactionsSystemData = JsonConvert.DeserializeObject<IEnumerable<TransactionsSystem>>(data);

                    var MoneyInThisMonth = transactionsSystemData.Sum(t => t.AmountIn ?? 0);
                    var MoneyOutThisMonth = transactionsSystemData.Sum(x => x.AmountOut ?? 0);
                    var BalanceThisMonth = MoneyInThisMonth - MoneyOutThisMonth;

                    ViewBag.MoneyInThisMonth = MoneyInThisMonth;
                    ViewBag.MoneyOutThisMonth = MoneyOutThisMonth;
                    ViewBag.BalanceThisMonth = BalanceThisMonth;
                    //TempData["transactionsSystemData"] = data;

                    return View(transactionsSystemData);
                }

                ViewBag.AlertMessage = "An unexpected error occurred. Please try again.";
                return View();

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
        public async Task<IActionResult> ListUser(int page = 1, int pageSize = 10)
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
        public async Task<IActionResult> ListNutritionist(int page = 1, int pageSize = 10)
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
        public async Task<IActionResult> UpdateUserStatus(int userId, int status)
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

            return await UserDetail(userId);
        }

        ////////////////////////////////////////////////////////////
        /// Tùng
        ////////////////////////////////////////////////////////////
        ///

    }
}
