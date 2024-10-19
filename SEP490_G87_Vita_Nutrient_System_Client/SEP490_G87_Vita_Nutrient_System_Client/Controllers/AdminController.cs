using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SEP490_G87_Vita_Nutrient_System_Client.Domain.Enums;
using SEP490_G87_Vita_Nutrient_System_Client.Models;
using System.Net.Http.Headers;

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
        [HttpGet("admin/dashboard")]
        public async Task<IActionResult> Dashboard()
        {
            return View();
        }

        [HttpGet("admin/usermanagement/listuser")]
        public async Task<IActionResult> ListUser()
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

                    ViewBag.listUsers = users;
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
                    await client.GetAsync(client.BaseAddress + "/Users/GetUserDetailInfo/" + userId);

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
                            DescribeYourself = userData.description,
                            Height = userData.height,
                            Weight = userData.weight,
                            Age = userData.age,
                            WantImprove = userData.wantImprove,
                            UnderlyingDisease = userData.underlyingDisease,
                            InforConfirmGood = userData.inforConfirmGood,
                            InforConfirmBad = userData.inforConfirmBad,
                            IsPremium = userData.isPremium
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


        ////////////////////////////////////////////////////////////
        /// Tùng
        ////////////////////////////////////////////////////////////
        ///

    }
}
