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
        public async Task<IActionResult> ProfileUserAsync()
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


        //[HttpGet, Authorize(Roles = "User, UserPremium, Admin")]

        [HttpGet]
        public async Task<IActionResult> PlanUserAsync()
        {
            //int userId = int.Parse(User.FindFirst("UserId")?.Value);

            //https://localhost:7045/api/GenerateMeal/APIDailyTargetTotal?myDay=2020-09-29T00%3A00%3A00&idUser=1

            //https://localhost:7045/api/GenerateMeal/APIDailyTargetTotal?myDay=2020-09-29T00%3A00%3A00&idUser=1&status=-

            HttpResponseMessage res = await client.GetAsync(client.BaseAddress + "/GenerateMeal/APIListMealOfTheDay?myDay=2024-10-30T00%3A00%3A00&idUser=1");
            
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
                            TotalCaloriesPerMeal = (float)Math.Round(rootObjectFoodList.Where(x => x.SlotOfTheDay == item.SlotOfTheDay).OrderBy(x => x.SettingDetail).ToArray().Sum(x => x.foodIdData.Sum(x => x.foodData.ingredientDetails100gReduceDTO.energy)), 2) ,
                            foodDataOfSlot = rootObjectFoodList.Where(x => x.SlotOfTheDay == item.SlotOfTheDay).OrderBy(x => x.SettingDetail).ToArray()
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
                        ingredientDetails100gReduceDTO = new Ingredientdetails100greducedto {  },
                        keyNote = new Keynote { } ,
                        scaleAmounts = new Scaleamounts {  } } };

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
            return RedirectToAction("Error");
        }


        public FoodList TotalAllTheIngredientsOfTheDish(IEnumerable<FoodList> dataFood)
        {
            FoodList totalfoodListDTO = new FoodList()
            {
                foodListId = dataFood.First().foodListId,
                name = dataFood.First().name,
                describe = dataFood.First().describe,
                rate = dataFood.First().rate,
                numberRate = dataFood.First().numberRate,
                urlimage = dataFood.First().urlimage,
                foodTypeId = dataFood.First().foodTypeId,
                keyNoteId = dataFood.First().keyNoteId,
                isActive = dataFood.First().isActive,
                preparationTime = dataFood.First().preparationTime,
                cookingTime = dataFood.First().cookingTime,
                cookingDifficultyId = dataFood.First().cookingDifficultyId,
                ingredientDetails100gReduceDTO = new Ingredientdetails100greducedto()
                {
                    id = -1,
                    keyNoteId = -1,
                    name = "SummaryOfTheEntireList",
                    describe = "SummaryOfTheEntireList",
                    urlimage = "SummaryOfTheEntireList",
                    typeOfCalculationId = -1,
                    energy = dataFood.Sum(x => x.ingredientDetails100gReduceDTO.energy),
                    protein = dataFood.Sum(x => x.ingredientDetails100gReduceDTO.protein),
                    fat = dataFood.Sum(x => x.ingredientDetails100gReduceDTO.fat),
                    carbohydrate = dataFood.Sum(x => x.ingredientDetails100gReduceDTO.carbohydrate),
                    fiber = dataFood.Sum(x => x.ingredientDetails100gReduceDTO.fiber),
                    sodium = dataFood.Sum(x => x.ingredientDetails100gReduceDTO.sodium),
                    cholesterol = dataFood.Sum(x => x.ingredientDetails100gReduceDTO.cholesterol)
                },
                keyNote = new Keynote
                {
                    id = dataFood.First().keyNote.id,
                    keyList = dataFood.First().keyNote.keyList
                },
                scaleAmounts = new Scaleamounts
                {
                    foodListId = dataFood.First().foodListId,
                    ingredientDetailsId = -1,
                    scaleAmount1 = -1
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

            return Redirect("");
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
