using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using SEP490_G87_Vita_Nutrient_System_Client.Models;

namespace SEP490_G87_Vita_Nutrient_System_Client.Controllers
{
	public class NutritionistController : Controller
	{
        ////////////////////////////////////////////////////////////
        /// Tân
        ////////////////////////////////////////////////////////////
        ///

        private readonly HttpClient client = null;
        public NutritionistController()
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


        [HttpGet()]
        public async Task<IActionResult> NutritionistProfile()
        {






                return View();
          
        }





        ////////////////////////////////////////////////////////////
        /// Sơn
        ////////////////////////////////////////////////////////////
        ///





        ////////////////////////////////////////////////////////////
        /// Tùng
        ////////////////////////////////////////////////////////////
        ///
        [HttpGet, Authorize(Roles = "Nutritionist")]
        public async Task<IActionResult> ListUser(string search = "", int page = 1, int pageSize = 10)
        {
            int userId = int.Parse(User.FindFirst("UserId")?.Value);
            var route = $"{client.BaseAddress}/Nutrition/user?userId={userId}&search={search}&page={page}&pageSize={pageSize}";

            HttpResponseMessage response = await client.GetAsync(route);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ApiResponse>(responseData);
                ViewBag.Search = search;
                ViewBag.CurrentPage = page;
                ViewBag.TotalPages = result.TotalPages;
                return View(result);
            }

            return View("Error"); // Đổi sang view lỗi nếu API không thành công
        }
    }
}

