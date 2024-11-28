using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SEP490_G87_Vita_Nutrient_System_Client.Domain.Attributes;
using SEP490_G87_Vita_Nutrient_System_Client.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

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

        [HttpGet, Authorize(Roles = "Nutritionist")]
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

        [HttpGet, Authorize(Roles = "Nutritionist")]
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

        [HttpGet, Authorize(Roles = "Nutritionist")]
        public async Task<IActionResult> GetNutritionRoutes(int userId, int userListManagementId, string packageName, int pageNumber = 1, int pageSize = 10)
        {
           
            try
            {
                var nutritionistId = int.Parse(User.FindFirst("UserId")?.Value);
                ViewData["UserId"] = userId;
                ViewData["UserListManagementId"] = userListManagementId;
                ViewData["PackageName"] = packageName;

                // Kiểm tra xem gói đăng ký có lộ trình nào chưa hoàn thành không
                HttpResponseMessage unfinishedResponse = await client.GetAsync($"api/nutritionroute/{nutritionistId}/user/{userId}/unfinished/{userListManagementId}");
                bool hasUnfinishedRoute = false;

                if (unfinishedResponse.IsSuccessStatusCode)
                {
                    var unfinishedData = await unfinishedResponse.Content.ReadAsStringAsync();
                    hasUnfinishedRoute = JsonSerializer.Deserialize<bool>(unfinishedData);
                }
                ViewData["HasUnfinishedRoute"] = hasUnfinishedRoute;

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
        [HttpGet, Authorize(Roles = "Nutritionist")]
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
        [HttpGet, Authorize(Roles = "Nutritionist")]
        public async Task<IActionResult> Create(int userId, int userListManagementId, string packageName)
        {
            ViewData["UserId"] = userId;
            ViewData["UserListManagementId"] = userListManagementId;
            ViewData["PackageName"] = packageName;

            var nutritionRoute = new NutritionRoute
            {
                UserId = userId,
                CreateById = int.Parse(User.FindFirst("UserId")?.Value),
                StartDate = DateTime.Today
            };

            return View(nutritionRoute);
        }

        // POST: NutritionRoute/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NutritionRoute nutritionRoute, int userId, int userListManagementId, string packageName)
        {
            if (nutritionRoute.StartDate == null)
            {
                nutritionRoute.StartDate = DateTime.Today; // Thiết lập mặc định
            }
            try
            {
                ViewData["UserId"] = userId;
                ViewData["UserListManagementId"] = userListManagementId;
                ViewData["PackageName"] = packageName;

                if (ModelState.IsValid)
                {
                    // Lấy thông tin các lộ trình trong gói đăng ký
                    var nutritionistId = int.Parse(User.FindFirst("UserId")?.Value);
                    HttpResponseMessage response = await client.GetAsync($"api/nutritionroute/{nutritionistId}/user/{userId}/route/{userListManagementId}");

                    if (!response.IsSuccessStatusCode)
                    {
                        ModelState.AddModelError(string.Empty, "Không thể lấy thông tin các lộ trình trong gói đăng ký.");
                        return View(nutritionRoute);
                    }

                    var data = await response.Content.ReadAsStringAsync();
                    var existingRoutes = JsonSerializer.Deserialize<List<NutritionRoute>>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    // Lấy thông tin gói đăng ký
                    HttpResponseMessage userListManagementResponse = await client.GetAsync($"api/nutritionroute/{nutritionistId}/user/{userId}");
                    if (!userListManagementResponse.IsSuccessStatusCode)
                    {
                        ModelState.AddModelError(string.Empty, "Không thể lấy thông tin gói đăng ký.");
                        return View(nutritionRoute);
                    }

                    var userListManagementData = await userListManagementResponse.Content.ReadAsStringAsync();
                    var userListManagement = JsonSerializer.Deserialize<List<UserListManagement>>(userListManagementData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    var selectedPackage = userListManagement.FirstOrDefault(p => p.Id == userListManagementId);
                    if (selectedPackage == null)
                    {
                        ModelState.AddModelError(string.Empty, "Gói đăng ký không tồn tại.");
                        return View(nutritionRoute);
                    }

                    if (!existingRoutes.Any())
                    {
                        // Nếu không có lộ trình trước đó, kiểm tra thời gian với gói
                        if (nutritionRoute.StartDate.Value.Date < selectedPackage.StartDate.Value.Date 
                            || nutritionRoute.EndDate.Value.Date > selectedPackage.EndDate.Value.Date)
                        {
                            ModelState.AddModelError(string.Empty, $"Thời gian của lộ trình phải nằm trong khoảng từ {selectedPackage.StartDate?.ToString("dd/MM/yyyy")} đến {selectedPackage.EndDate?.ToString("dd/MM/yyyy")}.");
                            return View(nutritionRoute);
                        }

                        // Cập nhật IsDone cho lộ trình cũ của CreateById = 1
                        var updateOldRouteResponse = await client.PutAsync(
                            $"api/nutritionroute/updateIsDone?createById=1&userId={userId}",
                            new StringContent("", Encoding.UTF8, "application/json")
                        );

                        if (!updateOldRouteResponse.IsSuccessStatusCode)
                        {
                            ModelState.AddModelError(string.Empty, "Không thể cập nhật trạng thái IsDone cho lộ trình cũ.");
                            return View(nutritionRoute);
                        }
                    }
                    else
                    {
                        var lastRoute = existingRoutes.OrderBy(r => r.EndDate).Last();

                        // Ngày bắt đầu phải lớn hơn ngày kết thúc của lộ trình trước
                        if (nutritionRoute.StartDate.Value.Date <= lastRoute.EndDate.Value.Date)
                        {
                            ModelState.AddModelError(string.Empty, $"Ngày bắt đầu của lộ trình phải lớn hơn ngày kết thúc của lộ trình trước đó ({lastRoute.EndDate?.ToString("dd/MM/yyyy")}).");
                            return View(nutritionRoute);
                        }

                        // Ngày kết thúc phải nằm trong thời gian của gói
                        if (nutritionRoute.EndDate.Value.Date > selectedPackage.EndDate.Value.Date)
                        {
                            ModelState.AddModelError(string.Empty, $"Ngày kết thúc của lộ trình phải nằm trong khoảng từ {selectedPackage.StartDate?.ToString("dd/MM/yyyy")} đến {selectedPackage.EndDate?.ToString("dd/MM/yyyy")}.");
                            return View(nutritionRoute);
                        }
                    }

                    // Kiểm tra trùng tên lộ trình
                    if (existingRoutes.Any(r => string.Equals(r.Name.Trim(), nutritionRoute.Name.Trim(), StringComparison.OrdinalIgnoreCase)))
                    {
                        ModelState.AddModelError(string.Empty, $"Tên lộ trình \"{nutritionRoute.Name}\" đã tồn tại trong gói đăng ký này. Vui lòng chọn tên khác.");
                        return View(nutritionRoute);
                    }

                    // Gán thông tin khác
                    nutritionRoute.CreateById = nutritionistId;
                    nutritionRoute.IsDone = false;

                    // Gửi yêu cầu tạo lộ trình mới đến API
                    var content = new StringContent(JsonSerializer.Serialize(nutritionRoute), Encoding.UTF8, "application/json");
                    var createResponse = await client.PostAsync($"api/nutritionroute/{userListManagementId}", content);

                    if (createResponse.IsSuccessStatusCode)
                    {
                        return RedirectToAction("GetNutritionRoutes", new { userId, userListManagementId, packageName });
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Lỗi khi tạo lộ trình dinh dưỡng.");
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Lỗi: {ex.Message}");
            }

            return View(nutritionRoute);
        }


        // GET: NutritionRoute/Edit/{id}
        [HttpGet, Authorize(Roles = "Nutritionist")]
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
            try
            {
                ViewData["UserId"] = userId;
                ViewData["UserListManagementId"] = userListManagementId;
                ViewData["PackageName"] = packageName;

                if (id != nutritionRoute.Id)
                {
                    return BadRequest("ID không khớp.");
                }

                if (ModelState.IsValid)
                {
                    var nutritionistId = int.Parse(User.FindFirst("UserId")?.Value);

                    // Lấy danh sách các lộ trình trong gói đăng ký
                    HttpResponseMessage response = await client.GetAsync($"api/nutritionroute/{nutritionistId}/user/{userId}/route/{userListManagementId}");

                    if (!response.IsSuccessStatusCode)
                    {
                        ModelState.AddModelError(string.Empty, "Không thể lấy thông tin các lộ trình trong gói đăng ký.");
                        return View(nutritionRoute);
                    }

                    var data = await response.Content.ReadAsStringAsync();
                    var existingRoutes = JsonSerializer.Deserialize<List<NutritionRoute>>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    // Lấy thông tin gói đăng ký
                    HttpResponseMessage userListManagementResponse = await client.GetAsync($"api/nutritionroute/{nutritionistId}/user/{userId}");
                    if (!userListManagementResponse.IsSuccessStatusCode)
                    {
                        ModelState.AddModelError(string.Empty, "Không thể lấy thông tin gói đăng ký.");
                        return View(nutritionRoute);
                    }

                    var userListManagementData = await userListManagementResponse.Content.ReadAsStringAsync();
                    var userListManagement = JsonSerializer.Deserialize<List<UserListManagement>>(userListManagementData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    var selectedPackage = userListManagement.FirstOrDefault(p => p.Id == userListManagementId);
                    if (selectedPackage == null)
                    {
                        ModelState.AddModelError(string.Empty, "Gói đăng ký không tồn tại.");
                        return View(nutritionRoute);
                    }

                    // Kiểm tra vị trí lộ trình (là đầu tiên hay không)
                    var sortedRoutes = existingRoutes.OrderBy(r => r.StartDate).ToList();
                    var currentIndex = sortedRoutes.FindIndex(r => r.Id == nutritionRoute.Id);

                    if (currentIndex == 0)
                    {
                        // Lộ trình đầu tiên: kiểm tra so với gói đăng ký
                        if (nutritionRoute.StartDate.Value.Date < selectedPackage.StartDate.Value.Date 
                            || nutritionRoute.StartDate.Value.Date > selectedPackage.EndDate.Value.Date)
                        {
                            ModelState.AddModelError(string.Empty, $"Ngày bắt đầu của lộ trình phải nằm trong khoảng từ {selectedPackage.StartDate?.ToString("dd/MM/yyyy")} đến {selectedPackage.EndDate?.ToString("dd/MM/yyyy")}.");
                            return View(nutritionRoute);
                        }
                    }
                    else if (currentIndex > 0)
                    {
                        // Lộ trình sau: kiểm tra so với lộ trình trước
                        var previousRoute = sortedRoutes[currentIndex - 1];
                        if (nutritionRoute.StartDate.Value.Date <= previousRoute.EndDate.Value.Date)
                        {
                            ModelState.AddModelError(string.Empty, $"Ngày bắt đầu của lộ trình phải lớn hơn ngày kết thúc của lộ trình trước đó ({previousRoute.EndDate?.ToString("dd/MM/yyyy")}).");
                            return View(nutritionRoute);
                        }
                    }

                    // Kiểm tra ngày kết thúc của lộ trình với gói đăng ký
                    if (nutritionRoute.EndDate.Value.Date > selectedPackage.EndDate.Value.Date)
                    {
                        ModelState.AddModelError(string.Empty, $"Ngày kết thúc của lộ trình phải nằm trong khoảng từ {selectedPackage.StartDate?.ToString("dd/MM/yyyy")} đến {selectedPackage.EndDate?.ToString("dd/MM/yyyy")}.");
                        return View(nutritionRoute);
                    }

                    // Kiểm tra trùng tên lộ trình
                    if (existingRoutes.Any(r => r.Id != nutritionRoute.Id &&
                                                string.Equals(r.Name.Trim(), nutritionRoute.Name.Trim(), StringComparison.OrdinalIgnoreCase)))
                    {
                        ModelState.AddModelError(string.Empty, $"Tên lộ trình \"{nutritionRoute.Name}\" đã tồn tại trong gói đăng ký này. Vui lòng chọn tên khác.");
                        return View(nutritionRoute);
                    }

                    // Gửi yêu cầu cập nhật lộ trình đến API
                    nutritionRoute.CreateById = nutritionistId; // Gán lại người chỉnh sửa
                    var content = new StringContent(JsonSerializer.Serialize(nutritionRoute), Encoding.UTF8, "application/json");
                    HttpResponseMessage editResponse = await client.PutAsync($"api/nutritionroute/{id}/{userListManagementId}", content);

                    if (editResponse.IsSuccessStatusCode)
                    {
                        return RedirectToAction("GetNutritionRoutes", new { userId, userListManagementId, packageName });
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Lỗi khi cập nhật lộ trình dinh dưỡng.");
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Lỗi: {ex.Message}");
            }

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
                var route = JsonSerializer.Deserialize<NutritionRoute>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
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

        // Lấy danh sách tất cả các bệnh
        [HttpGet]
        public async Task<JsonResult> GetAllDiseases()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"api/disease/GetAllDiseases");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var diseases = JsonSerializer.Deserialize<List<ListOfDisease>>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    return Json(diseases);
                }
            }
            catch (Exception ex)
            {
                return Json(new { error = $"Có lỗi xảy ra: {ex.Message}" });
            }

            return Json(new List<ListOfDisease>());
        }

        // Lấy danh sách bệnh theo người dùng
        [HttpGet]
        public async Task<JsonResult> GetDiseasesByUserId(int userId)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"api/nutritionroute/user/{userId}/diseases");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var diseases = JsonSerializer.Deserialize<List<ListOfDisease>>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    return Json(diseases);
                }
            }
            catch (Exception ex)
            {
                return Json(new { error = $"Có lỗi xảy ra: {ex.Message}" });
            }

            return Json(new List<ListOfDisease>());
        }

        [HttpPost]
        public async Task<IActionResult> AddDisease([FromBody] AddDiseaseRequest request)
        {
            if (request.UserId <= 0 || request.DiseaseId <= 0)
            {
                return Json(new { success = false, message = "User ID hoặc Disease ID không hợp lệ." });
            }

            try
            {
                // Kiểm tra bệnh đã tồn tại
                HttpResponseMessage checkResponse = await client.GetAsync($"api/NutritionRoute/user/{request.UserId}/diseases");
                if (checkResponse.IsSuccessStatusCode)
                {
                    var existingDiseasesData = await checkResponse.Content.ReadAsStringAsync();
                    var existingDiseases = JsonSerializer.Deserialize<List<ListOfDisease>>(existingDiseasesData);

                    // Bỏ qua kiểm tra nếu danh sách rỗng
                    if (existingDiseases != null && existingDiseases.Any(d => d.Id == request.DiseaseId))
                    {
                        return Json(new { success = false, message = "Bệnh lý đã tồn tại với người dùng." });
                    }
                }

                // Nếu chưa tồn tại, thêm bệnh lý
                HttpResponseMessage response = await client.PostAsJsonAsync($"api/NutritionRoute/AddDiseasesOfUser", request);

                if (response.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = "Thêm bệnh lý thành công." });
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return Json(new { success = false, message = $"Không thể thêm bệnh lý: {errorMessage}" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Lỗi hệ thống: {ex.Message}" });
            }
        }





        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteDisease([FromBody]AddDiseaseRequest request)
        {
            if (request.UserId <= 0 || request.DiseaseId <= 0)
            {
                return Json(new { success = false, message = "User ID hoặc Disease ID không hợp lệ." });
            }

            try
            {
                HttpResponseMessage response = await client.DeleteAsync($"api/NutritionRoute/user/{request.UserId}/diseases/{request.DiseaseId}");

                if (response.IsSuccessStatusCode)
                {

                    return Json(new { success = true, message = "Xóa bệnh lý thành công." });
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return Json(new { success = false, message = $"Không thể xóa bệnh lý: {errorMessage}" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Lỗi hệ thống: {ex.Message}" });
            }
        }

        [HttpGet, Authorize(Roles = "User,UserPremium")]
        public async Task<IActionResult> GetDetailsAllPremiumUserByUser(int userId, string search, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                userId = int.Parse(User.FindFirst("UserId")?.Value);
                HttpResponseMessage response = await client.GetAsync($"api/nutritionroute/user/{userId}/details-premium");

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
        [HttpPost]
        public async Task<JsonResult> Rate([FromBody] RateRequest request)
        {
            if (request.UserId <= 0 || request.NutritionistId <= 0 || request.UserListManagementId <= 0 || request.Rate < 1 || request.Rate > 5)
            {
                return Json(new { success = false, message = "Dữ liệu không hợp lệ." });
            }

            try
            {
                // Gửi yêu cầu PUT tới API
                var response = await client.PutAsync(
                    $"api/nutritionroute/rate?userId={request.UserId}&nutritionistId={request.NutritionistId}&userListManagementId={request.UserListManagementId}&rate={request.Rate}",
                    null // Không cần nội dung body
                );

                if (response.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = "Đánh giá thành công!" });
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return Json(new { success = false, message = errorMessage });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Lỗi hệ thống: {ex.Message}" });
            }
        }

        






    }

    public class AddDiseaseRequest
    {
        public int UserId { get; set; }
        public int DiseaseId { get; set; }
    }
    public class RateRequest
    {
        public int UserId { get; set; }
        public int NutritionistId { get; set; }
        public int UserListManagementId { get; set; }
        public short Rate { get; set; }
    }
}
