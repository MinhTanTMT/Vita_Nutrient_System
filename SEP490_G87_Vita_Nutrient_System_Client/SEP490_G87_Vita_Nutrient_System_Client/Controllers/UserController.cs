using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SEP490_G87_Vita_Nutrient_System_Client.Models;
using System.Collections.Generic;
using System.Net.Http.Headers;
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


        [HttpGet, Authorize(Roles = "User, UserPremium")]
        public async Task<IActionResult> cAsync()
        {
            int userId = int.Parse(User.FindFirst("UserId")?.Value);

            HttpResponseMessage res = await client.GetAsync(client.BaseAddress + "/Users/GetUserById/" + userId);

            if (res.StatusCode == System.Net.HttpStatusCode.OK)
            {
                HttpContent content = res.Content;
                string data = await content.ReadAsStringAsync();

                TempData["user"] = data;

                return View();
            }
            return RedirectToAction("Error");
        }



        [HttpGet]
        public async Task<IActionResult> PlanUserAsync()
        {

            //DateTime? myDay = DateTime.ParseExact("30/10/2024 00:00:00", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            DateTime? myDay = DateTime.Now;

            int userId = int.Parse(User.FindFirst("UserId")?.Value);

            // /GenerateMeal/APIListMealOfTheDay?myDay=2024-10-30T00%3A00%3A00&idUser=1

            if (userId == 1)
            {
                HttpResponseMessage res = await client.GetAsync(client.BaseAddress + $"/GenerateMeal/APIListMealOfTheDay?myDay={myDay}&idUser={userId}");

                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    HttpContent content = res.Content;
                    string data = await content.ReadAsStringAsync();

                    IEnumerable<DataFoodListMealOfTheDay> rootObjectFoodList = JsonConvert.DeserializeObject<IEnumerable<DataFoodListMealOfTheDay>>(data);

                    if (rootObjectFoodList.Count() > 0)
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

                        List<FoodList> foodListTotaAll = rootObjectFoodList
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
                    else
                    {
                        return RedirectToAction("Error");
                    }
                }
      
            }

            return RedirectToAction("Error");
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
        public async Task<IActionResult> RefreshTheMeal()
        {

            HttpResponseMessage res = await client.GetAsync(client.BaseAddress + "/GenerateMeal/APIRefreshTheMeal?myDay=2024-10-30T00%3A00%3A00&idUser=1");

            if (res.StatusCode == System.Net.HttpStatusCode.OK)
            {
                HttpContent content = res.Content;
                return Redirect("PlanUser");
            }

            return Redirect("PlanUser");
        }


        [HttpGet]
        public async Task<IActionResult> ChangeAnotherDish(int SlotOfTheDay, int SettingDetail, int OrderSettingDetail)
        {
            DateTime? myDay = DateTime.ParseExact("30/10/2024 00:00:00", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

            int userId = 1;

            if (userId == 1)
            {
                HttpResponseMessage res = await client.GetAsync(client.BaseAddress + $"/GenerateMeal/APIListMealOfTheDay?myDay={myDay}&idUser={userId}");

                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    HttpContent content = res.Content;
                    string data = await content.ReadAsStringAsync();

                    IEnumerable<DataFoodListMealOfTheDay> rootObjectFoodList = JsonConvert.DeserializeObject<IEnumerable<DataFoodListMealOfTheDay>>(data);

                    if (rootObjectFoodList.Count() > 0)
                    {
                        DataFoodListMealOfTheDay dataFoodListMealOfTheDays = rootObjectFoodList.FirstOrDefault(x => x.SlotOfTheDay == SlotOfTheDay && x.SettingDetail == SettingDetail && x.OrderSettingDetail == OrderSettingDetail);

                        if(dataFoodListMealOfTheDays != null)
                        {
                            return View(dataFoodListMealOfTheDays);
                        }
                        
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
            try
            {
                HttpResponseMessage response = foodTypeId == 0?
                                    await client.GetAsync(client.BaseAddress + "/Food/GetFoods/")
                                    :
                                    await client.GetAsync(client.BaseAddress + "/Food/GetFoods?foodTypeId=" + foodTypeId);

                HttpResponseMessage response1 =
                                    await client.GetAsync(client.BaseAddress + "/Food/GetFoodTypes");

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    HttpContent content = response.Content;
                    string data = await content.ReadAsStringAsync();
                    List<FoodList> foods = JsonConvert.DeserializeObject<List<FoodList>>(data);

                    //remove foods that are not active
                    foods.RemoveAll(f => f.IsActive == false);

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
                ViewBag.foodType = ft ?? new FoodType { FoodTypeId = 0, Name = "All Types"};
                ViewBag.foodTypeId = foodTypeId;
                ViewBag.searchQuery = searchQuery;

                return View("~/Views/User/FoodList.cshtml");
            }
            catch(Exception ex)
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

                if (response.StatusCode == System.Net.HttpStatusCode.OK
                    && response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    HttpContent content = response.Content;
                    string data = await content.ReadAsStringAsync();
                    FoodList food = JsonConvert.DeserializeObject<FoodList>(data);

                    HttpContent content1 = response1.Content;
                    string data1 = await content1.ReadAsStringAsync();
                    List<FoodRecipe> recipes = JsonConvert.DeserializeObject<List<FoodRecipe>>(data1);

                    ViewBag.food = food;
                    ViewBag.recipes = recipes;
                }
                else
                {
                    ViewBag.AlertMessage = "Cannot get food details! Please try again!";
                }

                return View("~/Views/User/FoodDetail.cshtml");
            }
            catch(Exception e)
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
        public async Task<IActionResult> GetUserPhysicalStatistics()
        {
            int userId = int.Parse(User.FindFirst("UserId")?.Value);

            HttpResponseMessage userRes = await client.GetAsync(client.BaseAddress + $"/Users/GetUserById/{userId}");
            HttpResponseMessage userDetailsRes = await client.GetAsync(client.BaseAddress + $"/Users/GetUserDetail/{userId}");

            if (userRes.IsSuccessStatusCode && userDetailsRes.IsSuccessStatusCode)
            {
                var user = JsonConvert.DeserializeObject<User>(await userRes.Content.ReadAsStringAsync());
                var userDetails = JsonConvert.DeserializeObject<UserDetail>(await userDetailsRes.Content.ReadAsStringAsync());

                var model = new UserPhysicalStatistics
                {
                    UserId = user.UserId,
                    Gender = user.Gender, // Lấy từ User
                    Height = userDetails?.Height,
                    Weight = userDetails?.Weight,
                    Age = userDetails?.Age,
                    DescribeYourself = userDetails?.DescribeYourself,
                    IsPremium = userDetails?.IsPremium
                };

                return View("UserPhysicalStatistics", model);
            }

            return View("Error");
        }


    }
}
