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

        [HttpGet]
        public async Task<IActionResult> GetInfoAllPremiumUserByNutritionist(string search, int pageNumber = 1, int pageSize = 10)
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

        [HttpGet]
        public async Task<IActionResult> GetDetailsAllPremiumUserByNutritionist(int userId, string search, int pageNumber = 1, int pageSize = 10)
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

        [HttpGet]
        public async Task<IActionResult> GetNutritionRoutes(int userId, int userListManagementId, string packageName, int pageNumber = 1, int pageSize = 10)
        {
           
            try
            {
                var nutritionistId = int.Parse(User.FindFirst("UserId")?.Value);

                // Gửi yêu cầu đến API
                HttpResponseMessage response = await client.GetAsync($"api/nutritionroute/{nutritionistId}/user/{userId}/route/{userListManagementId}");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var routes = JsonSerializer.Deserialize<List<NutritionRoute>>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    // Nếu không có lộ trình, trả danh sách rỗng
                    if (routes == null || !routes.Any())
                    {
                        // Gán giá trị mặc định cho ViewData
                        ViewData["TotalPages"] = 0;
                        ViewData["CurrentPage"] = pageNumber;
                        return View(new List<NutritionRoute>());
                    }

                    // Phân trang
                    int totalItems = routes.Count;
                    var paginatedRoutes = routes.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

                    // Gán giá trị vào ViewData
                    ViewData["TotalPages"] = (int)Math.Ceiling((double)totalItems / pageSize);
                    ViewData["CurrentPage"] = pageNumber;
                    ViewData["UserId"] = userId;
                    ViewData["UserListManagementId"] = userListManagementId;
                    ViewData["PackageName"] = packageName;

                    return View(paginatedRoutes);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Có lỗi xảy ra: {ex.Message}");
            }

            // Trả về danh sách rỗng trong trường hợp lỗi
            return View(new List<NutritionRoute>());
        }


        // GET: NutritionRoute/Details/{id}
        [HttpGet]
        public async Task<IActionResult> Details(int id, int userId, int userListManagementId, string packageName)
        {
            HttpResponseMessage response = await client.GetAsync($"api/nutritionroute/{id}");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var route = JsonSerializer.Deserialize<NutritionRoute>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                ViewData["UserId"] = userId;
                ViewData["UserListManagementId"] = userListManagementId; 
                ViewData["PackageName"] = packageName;
                return View(route);
            }
            return NotFound("Không tìm thấy thông tin lộ trình dinh dưỡng.");
        }


        // GET: NutritionRoute/Create
        [HttpGet]
        public async Task<IActionResult> Create(int userId, int userListManagementId, string packageName)
        {
            ViewData["UserId"] = userId;
            ViewData["UserListManagementId"] = userListManagementId;
            ViewData["PackageName"] = packageName;

            var nutritionRoute = new NutritionRoute
            {
                UserId = userId,
                CreateById = int.Parse(User.FindFirst("UserId")?.Value)
            };

            return View(nutritionRoute);
        }

        // POST: NutritionRoute/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NutritionRoute nutritionRoute, int userId, int userListManagementId, string packageName)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Gán CreateById từ người dùng đang đăng nhập
                    nutritionRoute.CreateById = int.Parse(User.FindFirst("UserId")?.Value);
                    nutritionRoute.IsDone = false;

                    // Tạo JSON payload và gửi đến API
                    var content = new StringContent(JsonSerializer.Serialize(nutritionRoute), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync($"api/nutritionroute/{userListManagementId}", content);

                    if (response.IsSuccessStatusCode)
                    {
                        // Chuyển hướng về trang danh sách sau khi tạo thành công
                        return RedirectToAction("GetNutritionRoutes", new
                        {
                            userId,
                            userListManagementId,
                            packageName
                        });
                    }
                    else
                    {
                        // Ghi lỗi nếu API trả về lỗi
                        var errorContent = await response.Content.ReadAsStringAsync();
                        ModelState.AddModelError(string.Empty, $"Lỗi từ API: {errorContent}");
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Lỗi: {ex.Message}");
            }

            // Nếu có lỗi, quay lại view Create và giữ lại thông tin
            ViewData["UserId"] = userId;
            ViewData["UserListManagementId"] = userListManagementId;
            ViewData["PackageName"] = packageName;
            return View(nutritionRoute);
        }




        // GET: NutritionRoute/Edit/{id}
        [HttpGet]
        public async Task<IActionResult> Edit(int id, int userId, int userListManagementId, string packageName)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"api/nutritionroute/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var route = JsonSerializer.Deserialize<NutritionRoute>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    // Gán giá trị vào ViewData
                    ViewData["ID"] = id;
                    ViewData["UserId"] = userId;
                    ViewData["UserListManagementId"] = userListManagementId;
                    ViewData["PackageName"] = packageName;

                    return View(route);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Có lỗi xảy ra: {ex.Message}");
            }

            return NotFound("Không tìm thấy thông tin lộ trình dinh dưỡng.");
        }


        // POST: NutritionRoute/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, NutritionRoute nutritionRoute, int userId, int userListManagementId, string packageName)
        {
            nutritionRoute.CreateById = int.Parse(User.FindFirst("UserId")?.Value);
            if (id != nutritionRoute.Id)
            {
                return BadRequest("ID không khớp.");
            }

            if (ModelState.IsValid)
            {
                
                var content = new StringContent(JsonSerializer.Serialize(nutritionRoute), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync($"api/nutritionroute/{id}/{userListManagementId}", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetNutritionRoutes", new
                    {
                        userId,
                        userListManagementId,
                        packageName
                    });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Có lỗi xảy ra khi cập nhật lộ trình dinh dưỡng.");
                }
            }

            // Gán lại giá trị ViewData để hiển thị đúng
            ViewData["ID"] = id;
            ViewData["UserId"] = userId;
            ViewData["UserListManagementId"] = userListManagementId;
            ViewData["PackageName"] = packageName;

            return View(nutritionRoute);
        }



        // GET: NutritionRoute/Delete/{id}
        [HttpGet]
        public async Task<IActionResult> Delete(int id, int userId, int userListManagementId, string packageName)
        {
            HttpResponseMessage response = await client.GetAsync($"api/nutritionroute/{id}");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var route = JsonSerializer.Deserialize<UserListManagement>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                // Gán giá trị vào ViewData
                ViewData["ID"] = id;
                ViewData["UserId"] = userId;
                ViewData["UserListManagementId"] = userListManagementId;
                ViewData["PackageName"] = packageName;
                return View(route);
            }
            return NotFound();
        }

        // POST: NutritionRoute/Delete/{id}
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int userId, int userListManagementId, string packageName)
        {
            try
            {
                // Gửi yêu cầu DELETE đến API
                HttpResponseMessage response = await client.DeleteAsync($"api/nutritionroute/{id}");

                if (response.IsSuccessStatusCode)
                {
                    // Chuyển hướng về trang GetNutritionRoutes sau khi xóa thành công
                    return RedirectToAction("GetNutritionRoutes", new
                    {
                        userId,
                        userListManagementId,
                        packageName
                    });
                }
                else
                {
                    // Ghi lại lỗi nếu DELETE không thành công
                    var errorContent = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, $"Không thể xóa lộ trình dinh dưỡng: {errorContent}");
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có ngoại lệ xảy ra
                ModelState.AddModelError(string.Empty, $"Lỗi xảy ra khi xóa lộ trình: {ex.Message}");
            }

            // Nếu lỗi xảy ra, hiển thị lại trang xóa với thông báo lỗi
            return RedirectToAction("Delete", new { id, userId, userListManagementId, packageName });
        }

    }
}
