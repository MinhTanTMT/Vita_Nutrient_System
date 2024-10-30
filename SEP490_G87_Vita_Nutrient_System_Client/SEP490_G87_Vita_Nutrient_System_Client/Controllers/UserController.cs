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


                    //IEnumerable<DataFoodListMealOfTheDay> rootObjectFoodList = JsonConvert.DeserializeObject<IEnumerable<DataFoodListMealOfTheDay>>(data);
                    //double totaAllCalories = 0;
                    //double caloriesEaten = 0;
                    //double caloriesRemaining = 0;

                    //foreach (var item in rootObjectFoodList)
                    //{
                    //    foreach (var item1 in item.foodIdData)
                    //    {
                    //        if (item1.statusSymbol.Equals("-"))
                    //        {
                    //            totaAllCalories += item1.foodData.ingredientDetails100gReduceDTO.energy;
                    //        }
                    //        else if (item1.statusSymbol.Equals("+"))
                    //        {
                    //            caloriesEaten += item1.foodData.ingredientDetails100gReduceDTO.energy;
                    //        }
                    //        else if (item1.statusSymbol.Equals("!"))
                    //        {
                    //            caloriesRemaining += item1.foodData.ingredientDetails100gReduceDTO.energy;
                    //        }
                    //    }
                    //}


                    double totaAllCalories = rootObjectFoodList
                        .SelectMany(item => item.foodIdData)
                        .Where(item1 => item1.statusSymbol == "-")
                        .Sum(item1 => item1.foodData.ingredientDetails100gReduceDTO.energy);

                    double caloriesEaten = rootObjectFoodList
                        .SelectMany(item => item.foodIdData)
                        .Where(item1 => item1.statusSymbol == "+")
                        .Sum(item1 => item1.foodData.ingredientDetails100gReduceDTO.energy);

                    double caloriesMissed = rootObjectFoodList
                        .SelectMany(item => item.foodIdData)
                        .Where(item1 => item1.statusSymbol == "!")
                        .Sum(item1 => item1.foodData.ingredientDetails100gReduceDTO.energy);


                    double allCarbohydrate = rootObjectFoodList
                        .SelectMany(item => item.foodIdData)
                        .Where(item1 => item1.statusSymbol == "+")
                        .Sum(item1 => item1.foodData.ingredientDetails100gReduceDTO.carbohydrate);

                    double allFat = rootObjectFoodList
                        .SelectMany(item => item.foodIdData)
                        .Where(item1 => item1.statusSymbol == "!")
                        .Sum(item1 => item1.foodData.ingredientDetails100gReduceDTO.fat);

                    double allProtein = rootObjectFoodList
                        .SelectMany(item => item.foodIdData)
                        .Where(item1 => item1.statusSymbol == "!")
                        .Sum(item1 => item1.foodData.ingredientDetails100gReduceDTO.protein);


                    ViewBag.totaAllCalories = Math.Round(totaAllCalories, 2);
                    ViewBag.caloriesEaten = Math.Round(caloriesEaten, 2);
                    ViewBag.caloriesMissed = Math.Round(caloriesMissed, 2);
                    ViewBag.allCarbohydrate = Math.Round(allCarbohydrate, 2);
                    ViewBag.allFat = Math.Round(allFat, 2);
                    ViewBag.allProtein = Math.Round(allProtein, 2);



                    return View(slotBranchesData.OrderBy(x => x.SlotOfTheDay));
                }
                else
                {
                    return RedirectToAction("Error");
                }
            }
            return RedirectToAction("Error");
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
