using Microsoft.AspNetCore.Mvc;
using SEP490_G87_Vita_Nutrient_System_Client.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SEP490_G87_Vita_Nutrient_System_Client.Controllers
{
    public class NutritionRouteController : Controller
    {
        private readonly HttpClient client;

        public NutritionRouteController(IConfiguration configuration)
        {
            Uri URIBase = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetValue<Uri>("myUri");
            client = new HttpClient();
            client.BaseAddress = URIBase;
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }

        // GET: NutritionRoute/GetAll/{createById}
        public async Task<IActionResult> GetAll()
        {
            var createById = int.Parse(User.FindFirst("UserId")?.Value);
            HttpResponseMessage response = await client.GetAsync($"api/nutritionroute/user/{createById}");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var routes = JsonSerializer.Deserialize<List<NutritionRoute>>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(routes);
            }
            return View("Error");
        }


        // GET: NutritionRoute/Details/{id}
        public async Task<IActionResult> Details(int id)
        {
            HttpResponseMessage response = await client.GetAsync($"api/nutritionroute/{id}");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var route = JsonSerializer.Deserialize<NutritionRoute>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(route);
            }
            return NotFound();
        }

        // GET: NutritionRoute/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NutritionRoute/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NutritionRoute nutritionRoute)
        {
            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonSerializer.Serialize(nutritionRoute), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("api/nutritionroute", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(GetAll));
                }
            }
            return View(nutritionRoute);
        }

        // GET: NutritionRoute/Edit/{id}
        public async Task<IActionResult> Edit(int id)
        {
            HttpResponseMessage response = await client.GetAsync($"api/nutritionroute/{id}");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var route = JsonSerializer.Deserialize<NutritionRoute>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(route);
            }
            return NotFound();
        }

        // POST: NutritionRoute/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, NutritionRoute nutritionRoute)
        {
            if (id != nutritionRoute.Id)
            {
                return BadRequest("ID không khớp.");
            }

            if (ModelState.IsValid)
            {
                // Lấy thông tin lộ trình dinh dưỡng hiện tại từ API
                HttpResponseMessage responseGet = await client.GetAsync($"api/nutritionroute/{id}");
                if (responseGet.IsSuccessStatusCode)
                {
                    var data = await responseGet.Content.ReadAsStringAsync();
                    var existingRoute = JsonSerializer.Deserialize<NutritionRoute>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    if (existingRoute != null)
                    {
                        // Đảm bảo UserName không bị sửa đổi
                        nutritionRoute.UserName = existingRoute.UserName;

                        // Chỉ cập nhật những trường cần thiết
                        var content = new StringContent(JsonSerializer.Serialize(nutritionRoute), Encoding.UTF8, "application/json");
                        HttpResponseMessage responsePut = await client.PutAsync($"api/nutritionroute/{id}", content);
                        if (responsePut.IsSuccessStatusCode)
                        {
                            return RedirectToAction(nameof(GetAll));
                        }
                    }
                }
            }
            return View(nutritionRoute);
        }

        // GET: NutritionRoute/Delete/{id}
        public async Task<IActionResult> Delete(int id)
        {
            HttpResponseMessage response = await client.GetAsync($"api/nutritionroute/{id}");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var route = JsonSerializer.Deserialize<NutritionRoute>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(route);
            }
            return NotFound();
        }

        // POST: NutritionRoute/DeleteConfirmed/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync($"api/nutritionroute/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(GetAll));
            }
            return View("Error");
        }
    }
}
