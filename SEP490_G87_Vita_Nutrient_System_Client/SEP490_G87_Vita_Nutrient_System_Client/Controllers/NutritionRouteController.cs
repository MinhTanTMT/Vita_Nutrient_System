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

        // GET: NutritionRoute/GetAll
        public async Task<IActionResult> GetAll()
        {
            HttpResponseMessage response = await client.GetAsync("api/nutritionroute");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var routes = JsonSerializer.Deserialize<List<NutritionRouteDTO>>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
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
                var route = JsonSerializer.Deserialize<NutritionRouteDTO>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
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
        public async Task<IActionResult> Create(NutritionRouteDTO nutritionRoute)
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
                var route = JsonSerializer.Deserialize<NutritionRouteDTO>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(route);
            }
            return NotFound();
        }

        // POST: NutritionRoute/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, NutritionRouteDTO nutritionRoute)
        {
            if (id != nutritionRoute.Id)
            {
                return BadRequest("ID không khớp.");
            }

            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonSerializer.Serialize(nutritionRoute), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync($"api/nutritionroute/{id}", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(GetAll));
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
                var route = JsonSerializer.Deserialize<NutritionRouteDTO>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
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
