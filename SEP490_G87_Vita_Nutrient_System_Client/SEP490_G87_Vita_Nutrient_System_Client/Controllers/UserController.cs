using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SEP490_G87_Vita_Nutrient_System_Client.Models;
using System.Collections.Generic;
using System.Data;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SEP490_G87_Vita_Nutrient_System_Client.Controllers
{
    public class UserController : Controller
    {

        ////////////////////////////////////////////////////////////
        /// Tân
        ////////////////////////////////////////////////////////////
        ///

        private readonly HttpClient client = null;
        public UserController()
        {
            Uri URIBase = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetValue<Uri>("myUri");
            client = new HttpClient();
            client.BaseAddress = URIBase;
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }


        [HttpGet]
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
                if (myDay == null || myDay <= DateTime.Now)
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
                else return RedirectToAction("PageUpgratePremium");
            }


            List<DataFoodAllDayOfWeekModify> dataFoodAllDayOfWeekModify = new List<DataFoodAllDayOfWeekModify>();
            foreach (var item in rootObjectFoodListWeek)
            {
                List<SlotBranch> slotBranchesData = GetListCollection(item.dataListFoodMealOfTheDay.ToList());
                dataFoodAllDayOfWeekModify.Add(new DataFoodAllDayOfWeekModify { DayOfTheWeekId = item.DayOfTheWeekId, DayOfTheWeekIdStart = item.DayOfTheWeekIdStart, DayOfWeek = item.DayOfWeek, NameDayOfWeek = item.NameDayOfWeek, dataListFoodMealDayOfTheWeek = slotBranchesData.ToArray(), TotalCaloriesAllDay = slotBranchesData.Sum(x => x.TotalCaloriesPerMeal) });
            }
            ViewBag.myDay = myDay;
            ViewBag.userId = userId;
            return View(dataFoodAllDayOfWeekModify);
        }


        public List<SlotBranch> GetListCollection(List<DataFoodListMealOfTheDay> rootObjectFoodList)
        {
            List<SlotBranch> slotBranchesData = new List<SlotBranch>();
            var numberSlot = rootObjectFoodList.Select(x => new
            {
                x.SlotOfTheDay,
                x.NameSlotOfTheDay
            }).Distinct().ToList();

            foreach (var item in numberSlot)
            {
                SlotBranch slotBranch = new SlotBranch()
                {
                    SlotOfTheDay = item.SlotOfTheDay,
                    NameSlotOfTheDay = item.NameSlotOfTheDay,
                    TotalCaloriesPerMeal = (float)Math.Round(rootObjectFoodList.Where(x => x.SlotOfTheDay == item.SlotOfTheDay).OrderBy(x => x.SettingDetail).ToArray().Sum(x => x.foodIdData.Sum(x => x.foodData.IngredientDetails100gReduceDTO.Energy)), 2),
                    foodDataOfSlot = rootObjectFoodList.Where(x => x.SlotOfTheDay == item.SlotOfTheDay).OrderBy(x => x.OrderSettingDetail).ToArray()
                };
                slotBranchesData.Add(slotBranch);
            }
            return slotBranchesData;
        }


        [HttpGet]
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
                else return RedirectToAction("PageUpgratePremium");
            }

            List<SlotBranch> slotBranchesData = GetListCollection(rootObjectFoodList);

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
                        IngredientDetails100gReduceDTO = new Ingredientdetails100greducedto {  },
                        KeyNote = new KeyNote { } ,
                        ScaleAmounts = new ScaleAmounts {  } } };

            ViewBag.myDay = myDay;
            ViewBag.userId = userId;
            ViewBag.foodListTotaAllCalculated = foodListTotaAll.Count() > 0 ? TotalAllTheIngredientsOfTheDish(foodListTotaAll) : TotalAllTheIngredientsOfTheDish(nullData);
            ViewBag.foodListNotEatenCalculated = foodListNotEaten.Count() > 0 ? TotalAllTheIngredientsOfTheDish(foodListNotEaten) : TotalAllTheIngredientsOfTheDish(nullData);
            ViewBag.foodListEatenCalculated = foodListEaten.Count() > 0 ? TotalAllTheIngredientsOfTheDish(foodListEaten) : TotalAllTheIngredientsOfTheDish(nullData);
            ViewBag.foodListMissedCalculated = foodListMissed.Count() > 0 ? TotalAllTheIngredientsOfTheDish(foodListMissed) : TotalAllTheIngredientsOfTheDish(nullData);

            return View(slotBranchesData.OrderBy(x => x.SlotOfTheDay));


        }




        public FoodList TotalAllTheIngredientsOfTheDish(IEnumerable<FoodList> dataFood)
        {
            FoodList totalfoodListDTO = new FoodList()
            {
                FoodListId = dataFood.First().FoodListId,
                Name = dataFood.First().Name,
                Describe = dataFood.First().Describe,
                Rate = dataFood.First().Rate,
                NumberRate = dataFood.First().NumberRate,
                Urlimage = dataFood.First().Urlimage,
                FoodTypeId = dataFood.First().FoodTypeId,
                KeyNoteId = dataFood.First().KeyNoteId,
                IsActive = dataFood.First().IsActive,
                PreparationTime = dataFood.First().PreparationTime,
                CookingTime = dataFood.First().CookingTime,
                CookingDifficultyId = dataFood.First().CookingDifficultyId,
                IngredientDetails100gReduceDTO = new Ingredientdetails100greducedto()
                {
                    Id = -1,
                    KeyNoteId = -1,
                    Name = "SummaryOfTheEntireList",
                    Describe = "SummaryOfTheEntireList",
                    Urlimage = "SummaryOfTheEntireList",
                    TypeOfCalculationId = -1,
                    Energy = dataFood.Sum(x => x.IngredientDetails100gReduceDTO.Energy),
                    Protein = dataFood.Sum(x => x.IngredientDetails100gReduceDTO.Protein),
                    Fat = dataFood.Sum(x => x.IngredientDetails100gReduceDTO.Fat),
                    Carbohydrate = dataFood.Sum(x => x.IngredientDetails100gReduceDTO.Carbohydrate),
                    Fiber = dataFood.Sum(x => x.IngredientDetails100gReduceDTO.Fiber),
                    Sodium = dataFood.Sum(x => x.IngredientDetails100gReduceDTO.Sodium),
                    Cholesterol = dataFood.Sum(x => x.IngredientDetails100gReduceDTO.Cholesterol)
                },
                KeyNote = new KeyNote
                {
                    Id = dataFood.First().KeyNote.Id,
                    KeyList = dataFood.First().KeyNote.KeyList
                },
                ScaleAmounts = new ScaleAmounts
                {
                    FoodListId = dataFood.First().FoodListId,
                    IngredientDetailsId = -1,
                    ScaleAmount = -1
                }
            };
            return totalfoodListDTO;
        }



        [HttpPost]
        public async Task<IActionResult> RefreshTheMeal(DateTime myDay)
        {

            int userId = int.Parse(User.FindFirst("UserId")?.Value);
            string role = User.FindFirst(ClaimTypes.Role)?.Value;

            if (!role.Equals("UserPremium"))
            {
                if (!(myDay <= DateTime.Now)) return RedirectToAction("PageUpgratePremium");
            }

            HttpResponseMessage res = await client.GetAsync(client.BaseAddress + $"/GenerateMeal/APIRefreshTheMeal?myDay={myDay}&idUser={userId}");

            if (res.StatusCode == System.Net.HttpStatusCode.OK)
            {
                HttpContent content = res.Content;
                return Redirect($"PlanUser?myDay={myDay}");
            }
            else
            {
                return Redirect("Loi me roi");
            }

        }


        [HttpPost]
        public async Task<IActionResult> RefreshTheMealWeek(DateTime myDay)
        {

            int userId = int.Parse(User.FindFirst("UserId")?.Value);
            string role = User.FindFirst(ClaimTypes.Role)?.Value;

            if (!role.Equals("UserPremium"))
            {
                if (!(myDay <= DateTime.Now)) return RedirectToAction("PageUpgratePremium");
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



        [HttpGet]
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

                    ////////////////////////////////////////////////////////////
                    /// Sơn
                    ////////////////////////////////////////////////////////////
                    ///

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
                HttpResponseMessage response =
                    await client.GetAsync(client.BaseAddress + "/Food/GetFoodById/" + foodId);

                HttpResponseMessage response1 =
                    await client.GetAsync(client.BaseAddress + "/Food/GetFoodRecipe/" + foodId);

                HttpResponseMessage response2 =
                    await client.GetAsync(client.BaseAddress + "/Ingredient/GetPreparationIngredientsByFoodId/" + foodId);

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

        ////////////////////////////////////////////////////////////
        /// Tùng
        ////////////////////////////////////////////////////////////
        ///

        ////////////////////////////////////////////////////////////
        /// Chiến
        ////////////////////////////////////////////////////////////
        ///
        [HttpGet("UserPhysicalStatistics")]
        public async Task<IActionResult> UserPhysicalStatistics()
        {
            int userId = int.Parse(User.FindFirst("UserId")?.Value);
            HttpResponseMessage userRes = await client.GetAsync(client.BaseAddress + $"/Users/GetUserById/{userId}");
            HttpResponseMessage userDetailsRes = await client.GetAsync(client.BaseAddress + $"/Users/GetOnlyUserDetail/{userId}");

            if (userDetailsRes.IsSuccessStatusCode)
            {
                var user = JsonConvert.DeserializeObject<User>(await userRes.Content.ReadAsStringAsync());
                var userPhysicalStatistics = JsonConvert.DeserializeObject<UserPhysicalStatistics>(await userDetailsRes.Content.ReadAsStringAsync());
                var model = new UserPhysicalStatistics
                {
                      UserId = userPhysicalStatistics.UserId,
                      Gender = user.Gender,
                      Height = userPhysicalStatistics.Height,
                      Weight = userPhysicalStatistics.Weight,
                      Age = userPhysicalStatistics.Age,
                      ActivityLevel = userPhysicalStatistics.ActivityLevel
    };
                return View(userPhysicalStatistics);
            }

            return View("Error");
        }
        [HttpPost("UserPhysicalStatistics")]
        public async Task<IActionResult> UserPhysicalStatistics(UserPhysicalStatistics model)
        {
            int userId = int.Parse(User.FindFirst("UserId")?.Value);
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Dữ liệu không hợp lệ." });
            }

            var userDetailsDTO = new UserPhysicalStatistics
            {
                UserId = userId,
                Gender = model.Gender,
                Height = model.Height,
                Weight = model.Weight,
                Age = model.Age,
                ActivityLevel = model.ActivityLevel
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


        [HttpGet("NutritionalGoals")]
        public async Task<IActionResult> NutritionalGoals()
        {
            int userId = int.Parse(User.FindFirst("UserId")?.Value);
            HttpResponseMessage userDetailsRes = await client.GetAsync(client.BaseAddress + $"/Users/GetOnlyUserDetail/{userId}");

            if (userDetailsRes.IsSuccessStatusCode)
            {
                var nutritionalGoals = JsonConvert.DeserializeObject<NutritionalGoals>(await userDetailsRes.Content.ReadAsStringAsync());
                    var model = new NutritionalGoals
                    {
                        Calo = nutritionalGoals.Calo,
                        Carbs = (int)(nutritionalGoals.Calo * 0.4 / 4),  // 40% calo từ carbs (4 calo mỗi gram)
                        Fats = (int)(nutritionalGoals.Calo * 0.3 / 9),   // 30% calo từ chất béo (9 calo mỗi gram)
                        Proteins = (int)(nutritionalGoals.Calo * 0.3 / 4) // 30% calo từ protein (4 calo mỗi gram)
                    };
                    return View(model);
            }

            return View("Error");
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
    }
}
