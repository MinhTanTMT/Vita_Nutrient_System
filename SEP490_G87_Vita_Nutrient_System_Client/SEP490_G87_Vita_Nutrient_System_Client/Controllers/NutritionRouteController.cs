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

        // GET: NutritionRoute/GetUsersByCreateId
        public async Task<IActionResult> GetUsersByCreateId(string search, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                // Lấy CreateById từ thông tin người dùng hiện tại
                var createById = int.Parse(User.FindFirst("UserId")?.Value);

                // Gửi yêu cầu đến API
                HttpResponseMessage response = await client.GetAsync($"api/nutritionroute/createBy/{createById}/users");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var users = JsonSerializer.Deserialize<List<User>>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    // Tìm kiếm từ khóa nếu có
                    if (!string.IsNullOrEmpty(search))
                    {
                        users = users.Where(u =>
                            (u.FullName != null && u.FullName.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                            (u.Phone != null && u.Phone.Contains(search, StringComparison.OrdinalIgnoreCase))
                        ).ToList();
                        ViewData["search"] = search;
                    }

                    // Phân trang
                    int totalItems = users.Count;
                    var paginatedUsers = users.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

                    // Truyền thông tin phân trang vào ViewData
                    ViewData["TotalPages"] = (int)Math.Ceiling((double)totalItems / pageSize);
                    ViewData["CurrentPage"] = pageNumber;

                    return View(paginatedUsers);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Có lỗi xảy ra: {ex.Message}");
            }

            // Trả về danh sách rỗng nếu có lỗi
            return View(new List<User>());
        }

        public async Task<IActionResult> GetRouteByUser(int userId, string search, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var createById = int.Parse(User.FindFirst("UserId")?.Value);
                HttpResponseMessage response = await client.GetAsync($"api/nutritionroute/createBy/{createById}/user/{userId}");

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var routes = JsonSerializer.Deserialize<List<NutritionRoute>>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    if (routes == null || !routes.Any())
                    {
                        ViewData["TotalPages"] = 0;
                        ViewData["CurrentPage"] = pageNumber;
                        return View(new List<NutritionRoute>());
                    }


                    /*// Tìm kiếm theo từ khóa
                    if (!string.IsNullOrEmpty(search))
                    {
                        routes = routes.Where(r =>
                            (r.Name != null && r.Name.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                            (r.FullName != null && r.FullName.Contains(search, StringComparison.OrdinalIgnoreCase))
                        ).ToList();

                        ViewData["search"] = search;
                    }*/

                    // Phân trang
                    int totalItems = routes.Count;
                    var paginatedRoutes = routes.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

                    // Truyền thông tin phân trang vào ViewData
                    ViewData["TotalPages"] = (int)Math.Ceiling((double)totalItems / pageSize);
                    ViewData["CurrentPage"] = pageNumber;

                    return View(paginatedRoutes);                     
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Có lỗi xảy ra: {ex.Message}");
            }

            return View(new List<NutritionRoute>()); 
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
                // Thêm CreateById dựa trên người đang đăng nhập
                nutritionRoute.CreateById = int.Parse(User.FindFirst("UserId")?.Value);

                var content = new StringContent(JsonSerializer.Serialize(nutritionRoute), Encoding.UTF8, "application/json");

                // Gọi API tạo NutritionRoute, bao gồm cả userPhoneNumber trong query string
                HttpResponseMessage response = await client.PostAsync($"api/nutritionroute", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(GetRouteByUser));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Có lỗi xảy ra khi tạo lộ trình dinh dưỡng.");
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
                        nutritionRoute.FullName = existingRoute.FullName;

                        // Chỉ cập nhật những trường cần thiết
                        var content = new StringContent(JsonSerializer.Serialize(nutritionRoute), Encoding.UTF8, "application/json");
                        HttpResponseMessage responsePut = await client.PutAsync($"api/nutritionroute/{id}", content);
                        if (responsePut.IsSuccessStatusCode)
                        {
                            return RedirectToAction("GetRouteByUser", new { userId = nutritionRoute.UserId });
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

        // POST: NutritionRoute/Delete/{id}
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int userId)
        {
            HttpResponseMessage response = await client.DeleteAsync($"api/nutritionroute/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("GetRouteByUser", new { userId = userId });
            }
            return View("Error");
        }
    }
}
