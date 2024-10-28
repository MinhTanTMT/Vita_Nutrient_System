using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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

            // https://localhost:7045/api/GenerateMeal/APGenerateMealController?MealSettingsDetailsId=3

            //https://localhost:7045/api/GenerateMeal/APIListMealOfTheDay?myDay=2024-10-29T00%3A00%3A00&idUser=1


            HttpResponseMessage res = await client.GetAsync(client.BaseAddress + "/GenerateMeal/APIListMealOfTheDay?myDay=2024-10-29T00%3A00%3A00&idUser=1");

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
                            foodDataOfSlot = rootObjectFoodList.Where(x => x.SlotOfTheDay == item.SlotOfTheDay).OrderBy(x => x.SettingDetail).ToArray()
                        };
                        slotBranchesData.Add(slotBranch);
                    }
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

            HttpResponseMessage res = await client.GetAsync(client.BaseAddress + "/GenerateMeal/APIRefreshTheMeal?myDay=2024-10-29T00%3A00%3A00&idUser=1");

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
