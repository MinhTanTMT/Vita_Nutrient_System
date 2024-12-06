using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SEP490_G87_Vita_Nutrient_System_Client.Domain.Attributes;
using SEP490_G87_Vita_Nutrient_System_Client.Models;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace SEP490_G87_Vita_Nutrient_System_Client.Controllers
{
    public class UserController : Controller
    {

        ////////////////////////////////////////////////////////////
        /// Tân
        ////////////////////////////////////////////////////////////
        ///

        private readonly HttpClient client = null;
        private readonly UserSevices userSevices;
        public UserController()
        {
            userSevices = new UserSevices();   
            Uri URIBase = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetValue<Uri>("myUri");
            client = new HttpClient();
            client.BaseAddress = URIBase;
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }


        [HttpGet,Authorize(Roles = "User,UserPremium")]
        public async Task<IActionResult> PlanUserWeekAsync(DateTime? myDay)
        {

            string role = User.FindFirst(ClaimTypes.Role)?.Value;
            int userId = int.Parse(User.FindFirst("UserId")?.Value);

            List <DataFoodAllDayOfWeek> rootObjectFoodListWeek = new List<DataFoodAllDayOfWeek>();

            if (role.Equals("UserPremium"))
            {
                if (myDay == null) myDay = DateTime.Now;
                HttpResponseMessage res = await client.GetAsync(client.BaseAddress + $"/GenerateMeal/APIListMealOfTheWeek?myDay={myDay}&idUser={userId}");
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    HttpContent content = res.Content;
                    string data = await content.ReadAsStringAsync();

                    rootObjectFoodListWeek = JsonConvert.DeserializeObject<List<DataFoodAllDayOfWeek>>(data);
                }
                else return RedirectToAction("Error2");
            }
            else
            {
                //if (myDay == null || myDay <= DateTime.Now)
                //{
                //    if (myDay == null) myDay = DateTime.Now;
                //    HttpResponseMessage res = await client.GetAsync(client.BaseAddress + $"/GenerateMeal/APIListMealOfTheDay?myDay={myDay}&idUser={userId}");
                //    if (res.StatusCode == System.Net.HttpStatusCode.OK)
                //    {
                //        HttpContent content = res.Content;
                //        string data = await content.ReadAsStringAsync();
                //        rootObjectFoodListWeek = JsonConvert.DeserializeObject<List<DataFoodAllDayOfWeek>>(data);
                //    }
                //    else return RedirectToAction("Error2");
                //}
                return RedirectToAction("NutritionistServices", "Admin");
            }


            List<DataFoodAllDayOfWeekModify> dataFoodAllDayOfWeekModify = new List<DataFoodAllDayOfWeekModify>();
            foreach (var item in rootObjectFoodListWeek)
            {
                List<SlotBranch> slotBranchesData = userSevices.GetListCollection(item.dataListFoodMealOfTheDay.ToList());
                dataFoodAllDayOfWeekModify.Add(new DataFoodAllDayOfWeekModify { DayOfTheWeekId = item.DayOfTheWeekId, DayOfTheWeekIdStart = item.DayOfTheWeekIdStart, DayOfWeek = item.DayOfWeek, NameDayOfWeek = item.NameDayOfWeek, dataListFoodMealDayOfTheWeek = slotBranchesData.ToArray(), TotalCaloriesAllDay = slotBranchesData.Sum(x => x.TotalCaloriesPerMeal) });
            }
            ViewBag.myDay = myDay;
            ViewBag.userId = userId;
            return View(dataFoodAllDayOfWeekModify);
        }


        
        [HttpGet, Authorize(Roles = "User,UserPremium")]
        public async Task<IActionResult> PlanUserAsync(DateTime? myDay)
        {
            string role = User.FindFirst(ClaimTypes.Role)?.Value;
            int userId = int.Parse(User.FindFirst("UserId")?.Value);

            List<DataFoodListMealOfTheDay> rootObjectFoodList = new List<DataFoodListMealOfTheDay>();

            if (role.Equals("UserPremium"))
            {
                if(myDay == null) myDay = DateTime.Now;
                HttpResponseMessage res = await client.GetAsync(client.BaseAddress + $"/GenerateMeal/APIListMealOfTheDay?myDay={myDay}&idUser={userId}");
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    HttpContent content = res.Content;
                    string data = await content.ReadAsStringAsync();

                    rootObjectFoodList = JsonConvert.DeserializeObject<List<DataFoodListMealOfTheDay>>(data);
                }
                else return RedirectToAction("Error2");
            }
            else
            {
                if (myDay == null || myDay <= DateTime.Now)
                {
                    if(myDay == null) myDay = DateTime.Now;
                    HttpResponseMessage res = await client.GetAsync(client.BaseAddress + $"/GenerateMeal/APIListMealOfTheDay?myDay={myDay}&idUser={userId}");
                    if (res.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        HttpContent content = res.Content;
                        string data = await content.ReadAsStringAsync();
                        rootObjectFoodList = JsonConvert.DeserializeObject<List<DataFoodListMealOfTheDay>>(data);
                    }
                    else return RedirectToAction("Error2");
                }
                else return RedirectToAction("NutritionistServices", "Admin");

            }

            List<SlotBranch> slotBranchesData = userSevices.GetListCollection(rootObjectFoodList);

            List <FoodList> foodListTotaAll = rootObjectFoodList
                .SelectMany(item => item.foodIdData)
                .Select(item1 => item1.foodData)
                .ToList();

            List<FoodList> foodListNotEaten = rootObjectFoodList
                .SelectMany(item => item.foodIdData)
                .Where(item1 => item1.statusSymbol == "-")
                .Select(item1 => item1.foodData)
                .ToList();

            List<FoodList> foodListEaten = rootObjectFoodList
                .SelectMany(item => item.foodIdData)
                .Where(item1 => item1.statusSymbol == "+")
                .Select(item1 => item1.foodData)
                .ToList();

            List<FoodList> foodListMissed = rootObjectFoodList
                .SelectMany(item => item.foodIdData)
                .Where(item1 => item1.statusSymbol == "!")
                .Select(item1 => item1.foodData)
                .ToList();

            List<FoodList> nullData = new List<FoodList> { new FoodList {
                        IngredientDetails100gDTO = new IngredientDetails100gDTO {  },
                        KeyNote = new KeyNote { } ,
                        ScaleAmounts = new ScaleAmounts {  } } };

            ViewBag.myDay = myDay;
            ViewBag.userId = userId;
            ViewBag.APICompleteTheDish = client.BaseAddress + $"/GenerateMeal/APICompleteTheDish";
            ViewBag.foodListTotaAllCalculated = foodListTotaAll.Count() > 0 ? userSevices.TotalAllTheIngredientsOfTheDish(foodListTotaAll) : userSevices.TotalAllTheIngredientsOfTheDish(nullData);
            ViewBag.foodListNotEatenCalculated = foodListNotEaten.Count() > 0 ? userSevices.TotalAllTheIngredientsOfTheDish(foodListNotEaten) : userSevices.TotalAllTheIngredientsOfTheDish(nullData);
            ViewBag.foodListEatenCalculated = foodListEaten.Count() > 0 ? userSevices.TotalAllTheIngredientsOfTheDish(foodListEaten) : userSevices.TotalAllTheIngredientsOfTheDish(nullData);
            ViewBag.foodListMissedCalculated = foodListMissed.Count() > 0 ? userSevices.TotalAllTheIngredientsOfTheDish(foodListMissed) : userSevices.TotalAllTheIngredientsOfTheDish(nullData);

            return View(slotBranchesData.OrderBy(x => x.SlotOfTheDay));
        }



        [HttpPost, Authorize(Roles = "User,UserPremium")]
        public async Task<IActionResult> RefreshTheMeal(DateTime myDay)
        {

            int userId = int.Parse(User.FindFirst("UserId")?.Value);
            string role = User.FindFirst(ClaimTypes.Role)?.Value;

            if (!role.Equals("UserPremium"))
            {
                if (!(myDay <= DateTime.Now)) return RedirectToAction("NutritionistServices", "Admin");
            }

            HttpResponseMessage res = await client.GetAsync(client.BaseAddress + $"/GenerateMeal/APIRefreshTheMeal?myDay={myDay}&idUser={userId}");

            if (res.StatusCode == System.Net.HttpStatusCode.OK)
            {
                HttpContent content = res.Content;
                return Redirect($"PlanUser?myDay={myDay}");
            }
            else
            {
                return Redirect("Loi me roi" + myDay);
            }

        }


        [HttpPost, Authorize(Roles = "User,UserPremium")]
        public async Task<IActionResult> RefreshTheMealWeek(DateTime myDay)
        {

            int userId = int.Parse(User.FindFirst("UserId")?.Value);
            string role = User.FindFirst(ClaimTypes.Role)?.Value;

            if (!role.Equals("UserPremium"))
            {
                if (!(myDay <= DateTime.Now)) return RedirectToAction("NutritionistServices", "Admin");
            }

            HttpResponseMessage res = await client.GetAsync(client.BaseAddress + $"/GenerateMeal/APIRefreshTheMeal?myDay={myDay}&idUser={userId}");

            if (res.StatusCode == System.Net.HttpStatusCode.OK)
            {
                HttpContent content = res.Content;
                return Redirect($"PlanUserWeek?myDay={myDay}");
            }
            else
            {
                return Redirect("Loi me roi");
            }

        }

        [HttpPost, Authorize(Roles = "User,UserPremium")]
        public async Task<IActionResult> RefreshTheMealAllWeek(DateTime myDay)
        {

            int userId = int.Parse(User.FindFirst("UserId")?.Value);
            string role = User.FindFirst(ClaimTypes.Role)?.Value;

            if (!role.Equals("UserPremium"))
            {
                if (!(myDay <= DateTime.Now)) return RedirectToAction("NutritionistServices", "Admin");
            }

            HttpResponseMessage res = await client.GetAsync(client.BaseAddress + $"/GenerateMeal/APIRefreshTheAllMeal?myDay={myDay}&idUser={userId}");

            if (res.StatusCode == System.Net.HttpStatusCode.OK)
            {
                HttpContent content = res.Content;
                return Redirect($"PlanUserWeek?myDay={myDay}");
            }
            else
            {
                return Redirect("Loi me roi");
            }

        }


        [HttpGet, Authorize(Roles = "User,UserPremium")]
        public async Task<IActionResult> ChangeAnotherDish(int SlotOfTheDay, int SettingDetail, int OrderSettingDetail, DateTime myDaySelect)
        {
            int userId = int.Parse(User.FindFirst("UserId")?.Value);

            HttpResponseMessage res = await client.GetAsync(client.BaseAddress + $"/GenerateMeal/APIListMealOfTheDay?myDay={myDaySelect}&idUser={userId}");

            if (res.StatusCode == System.Net.HttpStatusCode.OK)
            {
                HttpContent content = res.Content;
                string data = await content.ReadAsStringAsync();

                IEnumerable<DataFoodListMealOfTheDay> rootObjectFoodList = JsonConvert.DeserializeObject<IEnumerable<DataFoodListMealOfTheDay>>(data);

                if (rootObjectFoodList.Count() > 0)
                {
                    DataFoodListMealOfTheDay dataFoodListMealOfTheDays = rootObjectFoodList.FirstOrDefault(x => x.SlotOfTheDay == SlotOfTheDay && x.SettingDetail == SettingDetail && x.OrderSettingDetail == OrderSettingDetail);

                    if (dataFoodListMealOfTheDays != null)
                    {
                        ViewBag.clientBaseAddress = client.BaseAddress.ToString();
                        ViewBag.myDay = myDaySelect;
                        ViewBag.userId = userId;
                        ViewBag.APIgetThisListOfDishes = client.BaseAddress + $"/GenerateMeal/APIgetThisListOfDishes?userId={userId}&myDay={myDaySelect}";
                        ViewBag.APISelectReplaceCurrentFood = client.BaseAddress + $"/GenerateMeal/APISelectReplaceCurrentFood?idFoodSelect=";

                        return View(dataFoodListMealOfTheDays);

                    }

                }
            }
            return View("PlanUser");
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

        [HttpGet("foodsList")]
        public async Task<IActionResult> FoodList(
            string searchQuery = "",
            int foodTypeId = 0,
            int page = 1,
            int pageSize = 10)
        {
            int userId = int.Parse(User.FindFirst("UserId")?.Value);
            try
            {
                HttpResponseMessage response = foodTypeId == 0 ?
                                    await client.GetAsync(client.BaseAddress + "/Food/GetFoods/")
                                    :
                                    await client.GetAsync(client.BaseAddress + "/Food/GetFoods?foodTypeId=" + foodTypeId);

                HttpResponseMessage response1 =
                                    await client.GetAsync(client.BaseAddress + "/Food/GetFoodTypes");

                HttpResponseMessage response2 =
                                    await client.GetAsync(client.BaseAddress + "/Food/GetBlockFoodOfUser/" + userId);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    HttpContent content = response.Content;
                    string data = await content.ReadAsStringAsync();
                    List<FoodList> foods = JsonConvert.DeserializeObject<List<FoodList>>(data);

                    HttpContent content2 = response2.Content;
                    string data2 = await content2.ReadAsStringAsync();
                    List<int> foodIds = JsonConvert.DeserializeObject<List<int>>(data2);

                    //remove foods that are not active
                    foods.RemoveAll(f => f.IsActive == false);
                    //remove foods that are blocked
                    foods.RemoveAll(food => foodIds.Contains(food.FoodListId));

                    // Search logic
                    if (!string.IsNullOrEmpty(searchQuery))
                    {
                        foods = foods.Where(u =>
                            u.Name.ToLower().Contains(searchQuery.ToLower())
                        ).ToList();
                    }

                    // Pagination logic
                    int totalFoods = foods.Count();
                    var paginatedFoods = foods.Skip((page - 1) * pageSize).Take(pageSize).ToList();

                    ViewBag.foods = foods;
                    ViewBag.CurrentPage = page;
                    ViewBag.TotalPages = (int)Math.Ceiling(totalFoods / (double)pageSize);
                }
                else
                {
                    ViewBag.AlertMessage = "Cannot get list foods! Please try again!";
                }

                HttpContent content1 = response1.Content;
                string data1 = await content1.ReadAsStringAsync();
                List<FoodType> foodTypes = JsonConvert.DeserializeObject<List<FoodType>>(data1);
                ViewBag.foodTypes = foodTypes;
                FoodType ft = foodTypes.FirstOrDefault(f => f.FoodTypeId == foodTypeId);
                ViewBag.foodType = ft ?? new FoodType { FoodTypeId = 0, Name = "All Types" };
                ViewBag.foodTypeId = foodTypeId;
                ViewBag.searchQuery = searchQuery;

                return View("~/Views/User/FoodList.cshtml");
            }
            catch (Exception ex)
            {
                ViewBag.AlertMessage = "An unexpected error occurred. Please try again!";
                return View("~/Views/User/FoodList.cshtml");
            }
        }

        [HttpGet("foodDetails/{foodId}")]
        public async Task<IActionResult> FoodDetails(int foodId)
        {
            try
            {
                int userId = int.Parse(User.FindFirst("UserId")?.Value);

                HttpResponseMessage response =
                    await client.GetAsync(client.BaseAddress + "/Food/GetFoodById/" + foodId);

                HttpResponseMessage response1 =
                    await client.GetAsync(client.BaseAddress + "/Food/GetFoodRecipe/" + foodId);

                HttpResponseMessage response2 =
                    await client.GetAsync(client.BaseAddress + "/Ingredient/GetPreparationIngredientsByFoodId/" + foodId);

                HttpResponseMessage response3 =
                    await client.GetAsync(client.BaseAddress + "/UserFoodAction/GetUserFoodAction?UserId=" + userId + "&FoodId=" + foodId);
                HttpContent content3 = response3.Content;
                string data3 = await content3.ReadAsStringAsync();
                FoodSelection fs = JsonConvert.DeserializeObject<FoodSelection>(data3);

                if(fs != null && fs.IsBlock == true)
                {
                    return await FoodList();
                }
                else if(fs is null)
                {
                    fs = new FoodSelection
                    {
                        UserId = userId,
                        FoodListId = foodId,
                        IsBlock = false,
                        IsCollection = false,
                        IsLike = false,
                        Rate = null,
                        RecurringId = null
                    };
                }

                ViewBag.fs = fs;

                if (response2.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    HttpContent content2 = response2.Content;
                    string data2 = await content2.ReadAsStringAsync();
                    List<dynamic> foodIngredients = JsonConvert.DeserializeObject<List<dynamic>>(data2);

                    ViewBag.ingredients = foodIngredients;
                }
                else
                {
                    ViewBag.ingredients = new List<dynamic>();
                }

                if (response.StatusCode == System.Net.HttpStatusCode.OK
                    && response1.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    HttpContent content = response.Content;
                    string data = await content.ReadAsStringAsync();
                    dynamic result = JsonConvert.DeserializeObject<dynamic>(data);
                    FoodList food = result.food.ToObject<FoodList>();
                    List<SlotOfTheDay> slots = result.slots.ToObject<List<SlotOfTheDay>>();

                    HttpContent content1 = response1.Content;
                    string data1 = await content1.ReadAsStringAsync();
                    List<FoodRecipe> recipes = JsonConvert.DeserializeObject<List<FoodRecipe>>(data1);

                    List<string> foodSlots = slots.Select(s => s.Slot).ToList();
                    ViewBag.foodSlots = string.Join(", ", foodSlots.Take(foodSlots.Count - 1)) +
                                    (foodSlots.Count > 1 ? " và " : "") +
                                    foodSlots.LastOrDefault();
                    ViewBag.food = food;
                    ViewBag.recipes = recipes;
                }
                else
                {
                    ViewBag.AlertMessage = "Cannot get food details! Please try again!";
                }

                return View("~/Views/User/FoodDetail.cshtml");
            }
            catch (Exception e)
            {
                ViewBag.AlertMessage = "An unexpected error occurred. Please try again!";
                return View("~/Views/User/FoodDetail.cshtml");
            }
        }

        [HttpGet("Profile")]
        public async Task<IActionResult> UserProfileSon()
        {
            try
            {
                int userId = int.Parse(User.FindFirst("UserId")?.Value);

                // Lấy thông tin người dùng
                HttpResponseMessage response = await client.GetAsync(
                    client.BaseAddress + "/Users/GetUserDetail/" + userId);

                // Lấy danh sách bệnh lý
                HttpResponseMessage response1 = await client.GetAsync(
                    client.BaseAddress + "/Disease/GetAllDiseases");

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    HttpContent content = response.Content;
                    string data = await content.ReadAsStringAsync();
                    dynamic userData = JsonConvert.DeserializeObject<dynamic>(data);

                    HttpContent content1 = response1.Content;
                    string data1 = await content1.ReadAsStringAsync();
                    List<ListOfDisease> diseases = JsonConvert.DeserializeObject<List<ListOfDisease>>(data1);

                    // Tách UnderlyingDisease thành danh sách ID
                    string underlyingDiseaseIds = userData.detailsInformation.underlyingDisease;
                    List<int> diseaseIds = !string.IsNullOrEmpty(underlyingDiseaseIds)
                        ? underlyingDiseaseIds.Split(';').Select(int.Parse).ToList()
                        : new List<int>();

                    // Lấy tên các bệnh lý
                    var diseaseNames = diseases.Where(d => diseaseIds.Contains(d.Id)).Select(d => d.Name).ToList();

                    //HttpContent content1 = response1.Content;
                    //string data1 = await content1.ReadAsStringAsync();
                    //List<ListOfDisease> diseases = JsonConvert.DeserializeObject<List<ListOfDisease>>(data1);

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
                            UnderlyingDisease = string.Join(", ", diseaseNames), // Gán tên bệnh lý
                            InforConfirmGood = userData.detailsInformation.inforConfirmGood,
                            InforConfirmBad = userData.detailsInformation.inforConfirmBad,
                            IsPremium = userData.detailsInformation.isPremium
                        },
                        IsActive = userData.isActive,
                        Account = userData.account,
                        AccountGoogle = userData.accountGoogle
                    };

                    ViewBag.user = user;
                }
                else
                {
                    ViewBag.AlertMessage = "Cannot get user profile! Please try later!";
                }

                return View("~/Views/User/UserInfo.cshtml");
            }
            catch (Exception ex)
            {
                ViewBag.AlertMessage = "An unexpected error occurred. Please try again!";
                return View("~/Views/User/UserInfo.cshtml");
            }
        }

        [HttpGet("AdminProfile")]
        public async Task<IActionResult> AdminProfile()
        {
            try
            {
                int userId = int.Parse(User.FindFirst("UserId")?.Value);

                HttpResponseMessage response = await client.GetAsync(
                    client.BaseAddress + "/Users/GetUserById/" + userId);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    HttpContent content = response.Content;
                    string data = await content.ReadAsStringAsync();
                    dynamic userData = JsonConvert.DeserializeObject<dynamic>(data);

                    User user = new()
                    {
                        UserId = userData.userId,
                        FirstName = userData.firstName,
                        LastName = userData.lastName,
                        Urlimage = userData.urlimage,
                        Dob = userData.dob,
                        Gender = userData.gender ?? false,
                        Address = userData.address,
                        Phone = userData.phone,
                        Account = userData.account,
                        AccountGoogle = userData.accountGoogle
                    };

                    ViewBag.admin = user;
                }
            }catch(Exception ex)
            {
                ViewBag.AlertMessage = "An unexpected error occurred. Please try again!";
            }

            return View("~/Views/User/AdminInfo.cshtml");
        }

        [HttpGet("NutritionistProfile")]
        public async Task<IActionResult> NutritionistProfile()
        {
            try
            {
                int userId = int.Parse(User.FindFirst("UserId")?.Value);

                HttpResponseMessage response = await client.GetAsync(
                    client.BaseAddress + "/Users/GetNutritionistDetail/" + userId);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    HttpContent content = response.Content;
                    string data = await content.ReadAsStringAsync();
                    dynamic nutritionistData = JsonConvert.DeserializeObject<dynamic>(data);

                    User user = new()
                    {
                        UserId = nutritionistData.id,
                        FirstName = nutritionistData.firstName,
                        LastName = nutritionistData.lastName,
                        Urlimage = nutritionistData.urlimage,
                        Dob = nutritionistData.dob,
                        Gender = nutritionistData.gender ?? false,
                        Address = nutritionistData.address,
                        Phone = nutritionistData.phone,
                        NutritionistDetail = new NutritionistDetail
                        {
                            NutritionistId = nutritionistData.id,
                            DescribeYourself = nutritionistData.detailsInformation.description,
                            Height = nutritionistData.detailsInformation.height,
                            Weight = nutritionistData.detailsInformation.weight,
                            Age = nutritionistData.detailsInformation.age,
                            ExpertPackagesId = nutritionistData.detailsInformation.expertPackagesId,
                        },
                        Account = nutritionistData.account,
                        AccountGoogle = nutritionistData.accountGoogle,
                    };

                    ViewBag.user = user;

                    HttpResponseMessage response1 =
                        await client.GetAsync(client.BaseAddress + "/Users/GetNutritionistPackages/" + 
                        user.NutritionistDetail.ExpertPackagesId);

                    if (response1.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        HttpContent content1 = response1.Content;
                        string data1 = await content1.ReadAsStringAsync();
                        dynamic packagesData = JsonConvert.DeserializeObject<dynamic>(data1);

                        ExpertPackage package = new ExpertPackage
                        {
                            Id = packagesData.id,
                            Name = packagesData.name,
                            Describe = packagesData.describe,
                            Price = packagesData.price,
                            Duration = packagesData.duration
                        };

                        ViewBag.package = package;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.AlertMessage = "An unexpected error occurred. Please try again!";
            }

            return View("~/Views/User/NutritionistInfo.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUserInfo(string page ,int uid, string uacc, string uaccgg, string ufn, string uln, int user_gender, DateTime udob, string uadd, string uphone)
        {
            try
            {
                var data = new
                {
                    userId= uid,
                    firstName= ufn,
                    lastName= uln,
                    dob= udob,
                    gender= user_gender == 1? true : false,
                    address= uadd,
                    phone= uphone?? ""
                };

                string jsonData = JsonConvert.SerializeObject(data);

                HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response =
                    await client.PostAsync(client.BaseAddress + "/Users/UpdateUserInfo", content);

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    ViewBag.AlertMessage = "Update user failed! Please try again!";
                }
                else
                {
                    ViewBag.SuccessMessage = "Update user successfully!";
                }
            }
            catch(Exception ex)
            {
                ViewBag.AlertMessage = "An unexpected error occurred. Please try again!";
            }
            return page switch
            {
                "user" => await UserProfileSon(),
                "admin" => await AdminProfile(),
                "nutritionist" => await NutritionistProfile()
            };
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUserDetails(int uid, string udesc, short uheight, short uage, short uweight, string uwi)
        {
            try
            {
                var data = new
                {
                    userId = uid,
                    describe = udesc,
                    height = uheight,
                    weight = uweight,
                    age = uage,
                    wantImprove = uwi
                   
                };

                string jsonData = JsonConvert.SerializeObject(data);

                HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response =
                    await client.PostAsync(client.BaseAddress + "/Users/UpdateUserDetails", content);

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    ViewBag.AlertMessage = "Update profile failed! Please try again!";
                }
                else
                {
                    ViewBag.SuccessMessage = "Update profile successfully!";
                }
            }
            catch (Exception ex)
            {
                ViewBag.AlertMessage = "An unexpected error occurred. Please try again!";
            }
            return await UserProfileSon();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateNutritionistDetails(int uid, string udesc, short uheight, short uage, short uweight)
        {
            try
            {
                var data = new
                {
                    userId = uid,
                    describe = udesc,
                    height = uheight,
                    weight = uweight,
                    age = uage
                };

                string jsonData = JsonConvert.SerializeObject(data);

                HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response =
                    await client.PostAsync(client.BaseAddress + "/Users/UpdateNutritionistDetails", content);

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    ViewBag.AlertMessage = "Update profile failed! Please try again!";
                }
                else
                {
                    ViewBag.SuccessMessage = "Update profile successfully!";
                }
            }
            catch (Exception ex)
            {
                ViewBag.AlertMessage = "An unexpected error occurred. Please try again!";
            }
            return await NutritionistProfile();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(string page, int uid, string uopw, string unpw, string ucpw)
        {
            try
            {
                var data = new
                {
                    userId = uid,
                    oldPassword = uopw,
                    newPassword = unpw,
                    confirmPassword = ucpw
                };

                string jsonData = JsonConvert.SerializeObject(data);

                HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response =
                    await client.PutAsync(client.BaseAddress + "/Users/ChangePassword", content);
                HttpContent rContent = response.Content;
                string message = await rContent.ReadAsStringAsync();

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    ViewBag.AlertMessage = message;
                }
                else
                {
                    ViewBag.SuccessMessage = "Change password successfully!";
                }
            }
            catch (Exception ex)
            {
                ViewBag.AlertMessage = "An unexpected error occurred. Please try again!";
            }
            return page switch
            {
                "user" => await UserProfileSon(),
                "admin" => await AdminProfile(),
                "nutritionist" => await NutritionistProfile()
            };
        }

        ////////////////////////////////////////////////////////////
        /// Tùng
        ////////////////////////////////////////////////////////////
        ///
        [HttpGet, Authorize(Roles = "User, UserPremium")]
        public async Task<IActionResult> ListLikedFoods(int page = 1, int pageSize = 10, string search = "")
        {
            int userId = int.Parse(User.FindFirst("UserId")?.Value); // Assuming UserId is in claims

            var response = await client.GetAsync($"{client.BaseAddress}/Users/{userId}/liked-foods?Search={search}&Page={page}&PageSize={pageSize}");
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var likedFoods = JsonConvert.DeserializeObject<LikedFoodsResponse>(responseData);
                ViewBag.Search = search;
                ViewBag.TotalPages = likedFoods.TotalPages;
                ViewBag.CurrentPage = likedFoods.CurrentPage;
                return View(likedFoods.Items);
            }
            return View("Error"); // Show an error view if the API call fails
        }

        [HttpPost, Authorize(Roles = "User, UserPremium")]
        public async Task<IActionResult> UnlikeFood([FromQuery] int foodId)
        {
            int userId = int.Parse(User.FindFirst("UserId")?.Value);

            var response = await client.PostAsync($"{client.BaseAddress}/Users/{userId}/unlike-food/{foodId}", null);
            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }

            return Json(new { success = false, message = "Failed to unlike food" });
        }

        [HttpGet, Authorize(Roles = "User, UserPremium")]
        public async Task<IActionResult> ListCollectionFoods(int page = 1, int pageSize = 10, string search = "")
        {
            int userId = int.Parse(User.FindFirst("UserId")?.Value);

            var response = await client.GetAsync($"{client.BaseAddress}/Users/{userId}/collection-foods?Search={search}&Page={page}&PageSize={pageSize}");
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var likedFoods = JsonConvert.DeserializeObject<LikedFoodsResponse>(responseData);
                ViewBag.Search = search;
                ViewBag.TotalPages = likedFoods.TotalPages;
                ViewBag.CurrentPage = likedFoods.CurrentPage;
                return View(likedFoods.Items);
            }
            return View("Error"); // Show an error view if the API call fails
        }

        [HttpPost]
        [Authorize(Roles = "User, UserPremium")]
        public async Task<IActionResult> SaveFoodCollection(int foodId)
        {
            int userId = int.Parse(User.FindFirst("UserId")?.Value);

            var apiUrl = $"{client.BaseAddress}/Users/{userId}/save-food-collection/{foodId}";

            var response = await client.PostAsync(apiUrl, null);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Food item saved to your collection!";
                return RedirectToAction("ListCollectionFoods");
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to save the food item.";
                return RedirectToAction("ListCollectionFoods");
            }
        }



        ////////////////////////////////////////////////////////////
        /// Chiến
        ////////////////////////////////////////////////////////////
        ///
        [HttpGet("UserWeightGoal")]
        public async Task<IActionResult> UserWeightGoal()
        {
            int userId = int.Parse(User.FindFirst("UserId")?.Value);

            // Lấy thông tin người dùng và thông số vật lý
            HttpResponseMessage userRes = await client.GetAsync(client.BaseAddress + $"/Users/GetUserById/{userId}");
            HttpResponseMessage userDetailsRes = await client.GetAsync(client.BaseAddress + $"/Users/GetUserPhysicalStatisticsDTOByUserIdAsync/{userId}");

            if (userDetailsRes.IsSuccessStatusCode)
            {
                // Deserialize dữ liệu
                var userDetails = JsonConvert.DeserializeObject<UserPhysicalStatistics>(await userDetailsRes.Content.ReadAsStringAsync());

                var model = new UserPhysicalStatistics
                {
                    UserId = userDetails.UserId,
                    Gender = userDetails.Gender,
                    Age = userDetails.Age,
                    ActivityLevel = userDetails.ActivityLevel,
                    Height = userDetails.Height,
                    Weight = userDetails.Weight,
                    TimeUpdate = userDetails.TimeUpdate,
                    WeightGoal = userDetails.WeightGoal,
                };
                return View(model);
            }
            return View("Error");
        }
        [HttpPost("UserWeightGoal")]
        public async Task<IActionResult> UserWeightGoal(UserPhysicalStatistics model)
        {

            int userId = int.Parse(User.FindFirst("UserId")?.Value);
            try
            {
                model.TimeUpdate = DateTime.UtcNow;
                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(client.BaseAddress + "/Users/UpdateUserWeightGoal", content);

                // Kiểm tra phản hồi từ API
                if (response.IsSuccessStatusCode)
                {
                    // Lấy MealSettingsDetail gần nhất của user
                    HttpResponseMessage mealSettingResponse = await client.GetAsync($"{client.BaseAddress}/Meals/GetMealSettingByUserId/{userId}");
                    if (mealSettingResponse.IsSuccessStatusCode)
                    {
                        var mealSettingData = await mealSettingResponse.Content.ReadAsStringAsync();
                        var mealSetting = JsonConvert.DeserializeObject<MealSetting>(mealSettingData);
                        HttpResponseMessage mealSettingDetailResponse = await client.GetAsync($"{client.BaseAddress}/Meals/GetMealSettingDetailByMealSettingId/{mealSetting.Id}");
                        if (mealSettingDetailResponse.IsSuccessStatusCode)
                        {
                            var mealSettingDetailData = await mealSettingDetailResponse.Content.ReadAsStringAsync();
                            var mealSettingDetail = JsonConvert.DeserializeObject<MealSetting>(mealSettingDetailData);
                            if (mealSettingDetail != null)
                            {
                                // Gọi UpdateCalo cho MealSettingsDetail ID đầu tiên
                                await client.PutAsync($"{client.BaseAddress}/Meals/UpdateCalo/{mealSettingDetail.Id}", null);
                            }
                        }
                    }

                    // Trả về kết quả thành công sau khi cập nhật
                    return Json(new { success = true, message = "Thông tin cá nhân đã được lưu thành công và calo đã được cập nhật." });
                }
                else
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    return Json(new { success = false, message = "Có lỗi xảy ra trong quá trình lưu thông tin: " + errorResponse });
                }
            }
            catch (Exception ex)
            {
                // Trả về lỗi nếu có exception
                return Json(new { success = false, message = $"Lỗi trong quá trình gọi API: {ex.Message}" });
            }
        }

        [HttpGet("UserPhysicalStatistics")]
        public async Task<IActionResult> UserPhysicalStatistics()
        {
            int userId = int.Parse(User.FindFirst("UserId")?.Value);

            // Lấy thông tin người dùng và thông số vật lý
            HttpResponseMessage userRes = await client.GetAsync(client.BaseAddress + $"/Users/GetUserById/{userId}");
            HttpResponseMessage userDetailsRes = await client.GetAsync(client.BaseAddress + $"/Users/GetOnlyUserDetail/{userId}");
            HttpResponseMessage userDetailPhisicalsRes = await client.GetAsync(client.BaseAddress + $"/Users/GetUserPhysicalStatisticsDTOByUserIdAsync/{userId}");
            HttpResponseMessage diseaseRes = await client.GetAsync(client.BaseAddress + "/Disease/GetAllDiseases");

            if (userDetailsRes.IsSuccessStatusCode && diseaseRes.IsSuccessStatusCode)
            {
                // Deserialize dữ liệu
                var user = JsonConvert.DeserializeObject<User>(await userRes.Content.ReadAsStringAsync());
                var userDetails = JsonConvert.DeserializeObject<UserPhysicalStatistics>(await userDetailsRes.Content.ReadAsStringAsync());
                var diseases = JsonConvert.DeserializeObject<List<ListOfDisease>>(await diseaseRes.Content.ReadAsStringAsync());
                var userDetailsPhisical = JsonConvert.DeserializeObject<UserPhysicalStatistics>(await userDetailPhisicalsRes.Content.ReadAsStringAsync());
                // Xử lý bệnh lý
                string underlyingDiseaseIds = userDetails?.UnderlyingDisease; // Dữ liệu từ API
                List<int> diseaseIds = !string.IsNullOrEmpty(underlyingDiseaseIds)
                    ? underlyingDiseaseIds.Split(';').Select(int.Parse).ToList()
                    : new List<int>();

                var diseaseNames = diseases.Where(d => diseaseIds.Contains(d.Id)).Select(d => d.Name).ToList();

                // Cập nhật model với thông tin bệnh lý
                var model = new UserPhysicalStatistics
                {
                    UserId = userDetails.UserId,
                    Gender = userDetailsPhisical.Gender,
                    Height = userDetails.Height,
                    Weight = userDetails.Weight,
                    Age = userDetails.Age,
                    ActivityLevel = userDetails.ActivityLevel,
                    WeightGoal = userDetails.WeightGoal,
                    UnderlyingDisease = userDetails.UnderlyingDisease,
                    UnderlyingDiseaseNames = diseaseNames
                };

                // Đưa danh sách bệnh lý vào ViewBag nếu cần hiển thị toàn bộ
                ViewBag.Diseases = diseases;

                return View(model);
            }

            return View("Error");
        }
 
        [HttpPost("UserPhysicalStatistics")]
        public async Task<IActionResult> UserPhysicalStatistics(UserPhysicalStatistics model)
        {
            int userId = int.Parse(User.FindFirst("UserId")?.Value);
            var userDetailsDTO = new UserPhysicalStatistics
            {
                UserId = userId,
                Gender = model.Gender,
                Height = model.Height,
                Weight = model.Weight,
                Age = model.Age,
                ActivityLevel = model.ActivityLevel,
                WeightGoal = model.WeightGoal,
                TimeUpdate = DateTime.UtcNow
            };

            var jsonContent = new StringContent(JsonConvert.SerializeObject(userDetailsDTO), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(client.BaseAddress + "/Users/UpdateUserPhysicalStatistics", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                // Lấy MealSettingsDetail gần nhất của user
                HttpResponseMessage mealSettingResponse = await client.GetAsync($"{client.BaseAddress}/Meals/GetMealSettingByUserId/{userId}");
                if (mealSettingResponse.IsSuccessStatusCode)
                {
                    var mealSettingData = await mealSettingResponse.Content.ReadAsStringAsync();
                    var mealSetting = JsonConvert.DeserializeObject<MealSetting>(mealSettingData);
                    HttpResponseMessage mealSettingDetailResponse = await client.GetAsync($"{client.BaseAddress}/Meals/GetMealSettingDetailByMealSettingId/{mealSetting.Id}");
                    if (mealSettingDetailResponse.IsSuccessStatusCode)
                    {
                        var mealSettingDetailData = await mealSettingDetailResponse.Content.ReadAsStringAsync();
                        var mealSettingDetail = JsonConvert.DeserializeObject<MealSetting>(mealSettingDetailData);
                        if (mealSettingDetail != null)
                        {
                            // Gọi UpdateCalo cho MealSettingsDetail ID đầu tiên
                            await client.PutAsync($"{client.BaseAddress}/Meals/UpdateCalo/{mealSettingDetail.Id}", null);
                        }
                    }
                }

                return Json(new { success = true, message = "Thông tin cá nhân đã được lưu thành công và calo đã được cập nhật." });
            }
            else
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                return Json(new { success = false, message = "Có lỗi xảy ra trong quá trình lưu thông tin: " + errorResponse });
            }
        }
        // Call the API to get liked foods

        [HttpGet("NutritionalGoals")]
        public async Task<IActionResult> NutritionalGoals()
        {
            try
            {
                int userId = int.Parse(User.FindFirst("UserId")?.Value);

                // Gọi API để lấy chi tiết mục tiêu dinh dưỡng
                HttpResponseMessage userDetailsRes = await client.GetAsync(client.BaseAddress + $"/Users/GetOnlyUserDetail/{userId}");

                if (userDetailsRes.IsSuccessStatusCode)
                {
                    var nutritionalGoals = JsonConvert.DeserializeObject<NutritionalGoals>(await userDetailsRes.Content.ReadAsStringAsync());

                    // Kiểm tra nếu dữ liệu dinh dưỡng bị null hoặc không đầy đủ
                    if (nutritionalGoals == null || !nutritionalGoals.Calo.HasValue)
                    {
                        return RedirectToAction("UserPhysicalStatistics");
                    }

                    // Tính toán mục tiêu dinh dưỡng nếu dữ liệu hợp lệ
                    var model = new NutritionalGoals
                    {
                        Calo = nutritionalGoals.Calo, 
                        Carbs = (int)(nutritionalGoals.Calo.Value * 0.4 / 4),  // 40% calo từ carbs (4 calo mỗi gram)
                        Fats = (int)(nutritionalGoals.Calo.Value * 0.3 / 9),   // 30% calo từ chất béo (9 calo mỗi gram)
                        Proteins = (int)(nutritionalGoals.Calo.Value * 0.3 / 4) // 30% calo từ protein (4 calo mỗi gram)
                    };

                    return View(model);
                }
                return RedirectToAction("UserPhysicalStatistics");
            }
            catch (Exception ex)
            {
                return RedirectToAction("UserPhysicalStatistics");
            }
        }





        [HttpGet()]
        public async Task<IActionResult> UserProfile()
        {

            /// dùng bao nhiêu thì dùng 
            int userId = int.Parse(User.FindFirst("UserId")?.Value);

            HttpResponseMessage res = await client.GetAsync(client.BaseAddress + "/Users/GetUserById/" + userId);

            if (res.StatusCode == System.Net.HttpStatusCode.OK)
            {
                HttpContent content = res.Content;
                string data = await content.ReadAsStringAsync();

                return View();
            }
            return RedirectToAction("Error");
        }



        [HttpGet, Authorize(Roles = "User, UserPremium")]
        public async Task<IActionResult> ListBlockedFoods(int page = 1, int pageSize = 10, string search = "")
        {
            int userId = int.Parse(User.FindFirst("UserId")?.Value); // Assuming UserId is in claims

            // Call the API to get blocked foods
            var response = await client.GetAsync(client.BaseAddress + $"/Users/{userId}/blocked-foods?Search={search}&Page={page}&PageSize={pageSize}");
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var blockedFoods = JsonConvert.DeserializeObject<LikedFoodsResponse>(responseData);
                ViewBag.Search = search;
                ViewBag.TotalPages = blockedFoods.TotalPages;
                ViewBag.CurrentPage = blockedFoods.CurrentPage;
                return View(blockedFoods.Items);
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error: {response.StatusCode}, Content: {errorContent}");
                return View("Error");
            }

            // Show an error view if the API call fails
        }

        [HttpPost, Authorize(Roles = "User, UserPremium")]
        public async Task<IActionResult> Unblock([FromQuery] int foodId)
        {
            int userId = int.Parse(User.FindFirst("UserId")?.Value);

            var response = await client.PostAsync($"{client.BaseAddress}/Users/{userId}/unblock-food/{foodId}", null);
            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }

            return Json(new { success = false, message = "Failed to unblock food" });
        }

    }
}
