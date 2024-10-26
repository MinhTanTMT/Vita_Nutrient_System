using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SEP490_G87_Vita_Nutrient_System_Client.Models;
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

        [HttpGet("foodsList")]
        public async Task<IActionResult> FoodList(
            string searchQuery = "", 
            int foodTypeId = 0,
            int page = 1, 
            int pageSize = 10)
        {
            try
            {
                HttpResponseMessage response =
                                    await client.GetAsync(client.BaseAddress + "/Food/GetFoods/");

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    HttpContent content = response.Content;
                    string data = await content.ReadAsStringAsync();
                    List<FoodList> foods = JsonConvert.DeserializeObject<List<FoodList>>(data);

                    //remove foods that are not active
                    foods.RemoveAll(f => f.IsActive == false);

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

                ViewBag.foodTypeId = foodTypeId;

                return View("~/Views/User/FoodList.cshtml");
            }
            catch(Exception ex)
            {
                ViewBag.AlertMessage = "An unexpected error occurred. Please try again!";
                return View("~/Views/User/FoodList.cshtml");
            }
        }


        ////////////////////////////////////////////////////////////
        /// Tùng
        ////////////////////////////////////////////////////////////
        ///



    }
}
