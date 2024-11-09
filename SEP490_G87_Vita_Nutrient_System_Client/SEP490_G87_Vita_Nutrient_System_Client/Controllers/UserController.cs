using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SEP490_G87_Vita_Nutrient_System_Client.Models;
using System.Net.Http;
using System.Net.Http.Headers;

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
        [HttpGet, Authorize(Roles = "User, UserPremium")]
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10, string search = "")
        {
            int userId = int.Parse(User.FindFirst("UserId")?.Value); // Assuming UserId is in claims

            // Call the API to get liked foods
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

        [HttpGet, Authorize(Roles = "User, UserPremium")]
        public async Task<IActionResult> ListBlockedFoods(int page = 1, int pageSize = 10, string search = "")
        {
            int userId = int.Parse(User.FindFirst("UserId")?.Value); // Assuming UserId is in claims

            // Call the API to get blocked foods
            var response = await client.GetAsync($"Users/{userId}/blocked-foods?Search={search}&Page={page}&PageSize={pageSize}");
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var blockedFoods = JsonConvert.DeserializeObject<LikedFoodsResponse>(responseData);
                ViewBag.Search = search;
                ViewBag.TotalPages = blockedFoods.TotalPages;
                ViewBag.CurrentPage = blockedFoods.CurrentPage;
                return View(blockedFoods.Items);
            }

            return View("Error"); // Show an error view if the API call fails
        }

        [HttpPost]
        public async Task<IActionResult> Unblock(int foodId)
        {
            int userId = int.Parse(User.FindFirst("UserId")?.Value); // Assuming UserId is in claims

            // Call the API to unblock the food
            var response = await client.PostAsync($"Users/{userId}/unblock-food/{foodId}", null);
            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }

            return Json(new { success = false, message = "Failed to unblock food" });
        }
    }
}
