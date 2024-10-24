using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SEP490_G87_Vita_Nutrient_System_Client.Models;
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
            HttpResponseMessage res = await client.GetAsync(client.BaseAddress + "/GenerateMeal/APGenerateMealController?MealSettingsDetailsId=" + 1);

            if (res.StatusCode == System.Net.HttpStatusCode.OK)
            {
                HttpContent content = res.Content;
                string data = await content.ReadAsStringAsync();

                List<FoodList> rootObjectFoodList = JsonConvert.DeserializeObject<List<FoodList>>(data);

                //TempData["user"] = data;

                ViewBag.FoodListCalo = rootObjectFoodList.Sum(x => x.ingredientDetails100gReduceDTO.energy);

                return View(rootObjectFoodList);
            }
            return RedirectToAction("Error");

            return View();
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
