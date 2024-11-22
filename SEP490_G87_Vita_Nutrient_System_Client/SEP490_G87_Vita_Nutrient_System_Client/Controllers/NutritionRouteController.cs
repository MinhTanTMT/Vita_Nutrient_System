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

       /* public async Task<IActionResult> GetNutritionists(string search, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var roleId = 2;
                // Gửi yêu cầu đến API
                HttpResponseMessage response = await client.GetAsync($"api/users/GetUserByRole/{roleId}");
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
            return View();
        }*/

        public async Task<IActionResult> GetUsersByCreateId(string search, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                // Lấy CreateById từ thông tin người dùng hiện tại
                var nutritionistId = int.Parse(User.FindFirst("UserId")?.Value);

                // Gửi yêu cầu đến API
                HttpResponseMessage response = await client.GetAsync($"api/nutritionroute/{nutritionistId}/users");
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
                var nutritionistId = int.Parse(User.FindFirst("UserId")?.Value);
                HttpResponseMessage response = await client.GetAsync($"api/nutritionroute/{nutritionistId}/user/{userId}");

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var routes = JsonSerializer.Deserialize<List<UserListManagement>>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    if (routes == null || !routes.Any())
                    {
                        ViewData["TotalPages"] = 0;
                        ViewData["CurrentPage"] = pageNumber;
                        return View(new List<UserListManagement>());
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
                    ViewData["UserId"] = userId;

                    return View(paginatedRoutes);                     
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Có lỗi xảy ra: {ex.Message}");
            }

            return View(new List<UserListManagement>()); 
        }


        // GET: NutritionRoute/Details/{id}
        public async Task<IActionResult> Details(int id)
        {
            HttpResponseMessage response = await client.GetAsync($"api/nutritionroute/{id}");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var route = JsonSerializer.Deserialize<UserListManagement>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(route);
            }
            return NotFound();
        }

        // GET: NutritionRoute/Create
        public async Task<IActionResult> CreateAsync(int userId)
        {
            var nutritionistId = int.Parse(User.FindFirst("UserId")?.Value);
            // Gửi yêu cầu đến API để kiểm tra lộ trình chưa hoàn thành
            HttpResponseMessage response = await client.GetAsync($"api/nutritionroute/{nutritionistId}/user/{userId}/unfinished");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var hasUnfinishedRoute = JsonSerializer.Deserialize<bool>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (hasUnfinishedRoute)
                {
                    ModelState.AddModelError(string.Empty, "Bạn phải hoàn thành lộ trình hiện tại trước khi tạo lộ trình mới.");
                    return RedirectToAction("GetRouteByUser", new { userId });
                }
            }

            // Nếu không có lộ trình chưa hoàn thành, cho phép tạo
            var user = await GetUserById(userId);
            if (user != null)
            {
                var userListManagement = new UserListManagement
                {
                    UserId = userId,
                    UserName = $"{user.FirstName} {user.LastName}"
                };

                return View(userListManagement);
            }

            ModelState.AddModelError(string.Empty, "Không thể lấy thông tin người dùng.");
            return RedirectToAction("GetRouteByUser", new { userId });
        }

        private async Task<User> GetUserById(int userId)
        {
            HttpResponseMessage response = await client.GetAsync($"api/users/GetUserById/{userId}");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<User>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return null;
        }




        // POST: NutritionRoute/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserListManagement userListManagement)
        {
            if (ModelState.IsValid)
            {
                userListManagement.NutritionistId = int.Parse(User.FindFirst("UserId")?.Value);
                userListManagement.IsDone = false;

                var content = new StringContent(JsonSerializer.Serialize(userListManagement), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync($"api/nutritionroute", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(GetRouteByUser), new { userId = userListManagement.UserId });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Có lỗi xảy ra khi tạo lộ trình dinh dưỡng.");
                }
            }
            return View(userListManagement);
        }



        // GET: NutritionRoute/Edit/{id}
        public async Task<IActionResult> Edit(int id)
        {
            HttpResponseMessage response = await client.GetAsync($"api/nutritionroute/{id}");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var route = JsonSerializer.Deserialize<UserListManagement>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(route);
            }
            return NotFound();
        }

        // POST: NutritionRoute/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UserListManagement userListManagement)
        {
            if (id != userListManagement.Id)
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
                    var existingRoute = JsonSerializer.Deserialize<UserListManagement>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    if (existingRoute != null)
                    {
                        // Đảm bảo UserName không bị sửa đổi
                        userListManagement.UserName = existingRoute.UserName;

                        // Chỉ cập nhật những trường cần thiết
                        var content = new StringContent(JsonSerializer.Serialize(userListManagement), Encoding.UTF8, "application/json");
                        HttpResponseMessage responsePut = await client.PutAsync($"api/nutritionroute/{id}", content);
                        if (responsePut.IsSuccessStatusCode)
                        {
                            return RedirectToAction("GetRouteByUser", new { userId = userListManagement.UserId });
                        }
                    }
                }
            }
            return View(userListManagement);
        }

        // GET: NutritionRoute/Delete/{id}
        public async Task<IActionResult> Delete(int id)
        {
            HttpResponseMessage response = await client.GetAsync($"api/nutritionroute/{id}");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var route = JsonSerializer.Deserialize<UserListManagement>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
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
