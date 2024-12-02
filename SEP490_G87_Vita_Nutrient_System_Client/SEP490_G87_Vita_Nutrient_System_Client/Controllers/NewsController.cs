using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SEP490_G87_Vita_Nutrient_System_Client.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace SEP490_G87_Vita_Nutrient_System_Client.Controllers
{
    public class NewsController : Controller
    {
        private readonly HttpClient client;

        public NewsController(IConfiguration configuration)
        {
            Uri URIBase = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetValue<Uri>("myUri");
            client = new HttpClient();
            client.BaseAddress = URIBase;
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }

        // GET: List of all articles
        [HttpGet, Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index(string searchTitle, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var response = await client.GetAsync("api/news");

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var articles = JsonConvert.DeserializeObject<List<ArticlesNews>>(data);

                    // Tìm kiếm theo tiêu đề
                    if (!string.IsNullOrEmpty(searchTitle))
                    {
                        articles = articles.Where(a => a.Title != null && a.Title.Contains(searchTitle, StringComparison.OrdinalIgnoreCase)).ToList();
                        ViewData["searchTitle"] = searchTitle;
                    }

                    // Phân trang
                    int totalItems = articles.Count;
                    var paginatedArticles = articles.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

                    // Thông tin phân trang
                    int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
                    ViewData["CurrentPage"] = pageNumber;
                    // Chỉ hiển thị phân trang nếu có từ 2 trang trở lên
                    if (totalPages > 1)
                    {
                        ViewData["TotalPages"] = totalPages;
                    }


                    return View(paginatedArticles);
                }

                ModelState.AddModelError(string.Empty, "Error retrieving data from server.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
            }

            return View(new List<ArticlesNews>());
        }



        // GET: List of all articles for users
        [HttpGet]        
        public async Task<IActionResult> IndexForUsers(string searchTitle, int pageNumber = 1, int pageSize = 2)
        {
            try
            {
                var response = await client.GetAsync("api/news");

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var articles = JsonConvert.DeserializeObject<List<ArticlesNews>>(data);

                    // Chỉ hiển thị bài viết đang hoạt động
                    articles = articles.Where(a => a.IsActive == true).ToList();

                    // Tìm kiếm theo tiêu đề
                    if (!string.IsNullOrEmpty(searchTitle))
                    {
                        articles = articles.Where(a => a.Title != null && a.Title.Contains(searchTitle, StringComparison.OrdinalIgnoreCase)).ToList();
                        ViewData["searchTitle"] = searchTitle;
                    }

                    // Phân trang
                    int totalItems = articles.Count;
                    var paginatedArticles = articles.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

                    // Thông tin phân trang
                    int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
                    ViewData["CurrentPage"] = pageNumber;
                    // Chỉ hiển thị phân trang nếu có từ 2 trang trở lên
                    if (totalPages > 1)
                    {
                        ViewData["TotalPages"] = totalPages;
                    }

                    return View(paginatedArticles);
                }

                ModelState.AddModelError(string.Empty, "Error retrieving data from server.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
            }
            return View(new List<ArticlesNews>());
        }



        // GET: Details of a single article
        [HttpGet, Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var response = await client.GetAsync($"api/news/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var article = JsonConvert.DeserializeObject<ArticlesNews>(data);
                    return View(article);
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
                return View();
            }
        }


        // GET: Details of a single article for users
        [HttpGet]
        public async Task<IActionResult> DetailsForUsers(int id)
        {
            ViewData["ApiBaseUrl"] = client.BaseAddress;

            try
            {
                var articleResponse = await client.GetAsync($"api/news/{id}");
                if (!articleResponse.IsSuccessStatusCode)
                {
                    return NotFound();
                }

                var articleData = await articleResponse.Content.ReadAsStringAsync();
                var article = JsonConvert.DeserializeObject<ArticlesNews>(articleData);

                // Lấy đánh giá của người dùng hiện tại nếu người dùng đã đăng nhập
                if (User.Identity.IsAuthenticated)
                {
                    var userId = int.Parse(User.FindFirst("UserId")?.Value);
                    var evaluationResponse = await client.GetAsync($"api/news/{id}/evaluations/{userId}");
                    if (evaluationResponse.IsSuccessStatusCode)
                    {
                        var evaluationData = await evaluationResponse.Content.ReadAsStringAsync();
                        var evaluation = JsonConvert.DeserializeObject<NewsEvaluation>(evaluationData);
                        article.UserRate = evaluation.Ratting; // Gán đánh giá của người dùng vào bài viết
                    }
                }

                return View(article);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
                return View();
            }
        }


        [HttpGet, Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Create a new article
        [HttpPost]
        public async Task<IActionResult> Create(ArticlesNews article, IFormFile HeaderImage)
        {
            if (!ModelState.IsValid)
            {
                return View(article);
            }

            try
            {
                int userId = int.Parse(User.FindFirst("UserId")?.Value); // Giả sử UserId là 1

                // Kiểm tra và xử lý tệp hình ảnh
                if (HeaderImage != null && HeaderImage.Length > 0)
                {
                    // Kiểm tra định dạng file (chỉ chấp nhận .jpg, .jpeg, .png, .gif)
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    var fileExtension = Path.GetExtension(HeaderImage.FileName).ToLower();

                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        ModelState.AddModelError("HeaderImage", "Chỉ chấp nhận các định dạng hình ảnh: .jpg, .jpeg, .png, .gif.");
                        return View(article); // Trả về trang tạo bài viết với thông báo lỗi
                    }

                    // Kiểm tra kích thước file (giới hạn 5MB)
                    if (HeaderImage.Length > 5 * 1024 * 1024) // 5MB
                    {
                        ModelState.AddModelError("HeaderImage", "Kích thước ảnh không được vượt quá 5MB.");
                        return View(article); // Trả về trang tạo bài viết với thông báo lỗi
                    }

                    var fileName = Path.GetFileName(HeaderImage.FileName);
                    var filePath = Path.Combine("wwwroot/images/news", fileName);

                    // Tạo thư mục "images/news" nếu chưa tồn tại
                    if (!Directory.Exists("wwwroot/images/news"))
                    {
                        Directory.CreateDirectory("wwwroot/images/news");
                    }

                    // Lưu tệp ảnh vào thư mục
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await HeaderImage.CopyToAsync(stream);
                    }

                    // Lưu đường dẫn ảnh tương đối vào thuộc tính HeaderImage của article
                    article.HeaderImage = "/images/news/" + fileName;
                }

                // Tạo dữ liệu DTO để gửi qua API mà không có ID tự tăng
                var createData = new ArticlesNews()
                {
                    UserId = userId,
                    NameCreater = article.NameCreater,
                    Title = article.Title,
                    Content = article.Content,
                    IsActive = article.IsActive ?? true,
                    DateCreated = DateTime.Today,
                    HeaderImage = article.HeaderImage
                };

                // Gửi yêu cầu POST đến API và xử lý phản hồi
                HttpResponseMessage response = await client.PostAsJsonAsync("api/news", createData);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Bài viết đã được tạo thành công!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "Đã có lỗi xảy ra khi tạo bài viết. Vui lòng thử lại!";
                    return View(article);
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi khi xử lý yêu cầu. Vui lòng thử lại sau.";
                return View(article);
            }
        }


        // GET: Edit an article by id
        [HttpGet, Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                // Gửi yêu cầu GET để lấy dữ liệu bài viết qua id
                HttpResponseMessage response = await client.GetAsync(client.BaseAddress + $"/news/{id}");
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var article = JsonConvert.DeserializeObject<ArticlesNews>(data);
                    return View(article);
                }

                return RedirectToAction("Error"); // Chuyển hướng đến trang lỗi nếu không thành công
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
                return RedirectToAction("Error");
            }
        }

        // POST: Update an article
        [HttpPost]
        public async Task<IActionResult> Edit(ArticlesNews article, IFormFile HeaderImage)
        {
            ModelState.Remove("HeaderImage");
            if (!ModelState.IsValid)
            {
                return View(article); // Trả về view nếu model không hợp lệ
            }

            try
            {
                int userId = int.Parse(User.FindFirst("UserId")?.Value); // Giả sử UserId là 1

                // Kiểm tra xem người dùng có chọn hình ảnh mới không
                if (HeaderImage != null && HeaderImage.Length > 0)
                {
                    // Kiểm tra định dạng file (chỉ chấp nhận .jpg, .jpeg, .png, .gif)
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    var fileExtension = Path.GetExtension(HeaderImage.FileName).ToLower();
                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        ModelState.AddModelError("HeaderImage", "Chỉ chấp nhận các định dạng hình ảnh: .jpg, .jpeg, .png, .gif.");
                        return View(article); // Trả về trang tạo bài viết với thông báo lỗi
                    }
                    // Kiểm tra kích thước file (giới hạn 5MB)
                    if (HeaderImage.Length > 5 * 1024 * 1024) // 5MB
                    {
                        ModelState.AddModelError("HeaderImage", "Kích thước ảnh không được vượt quá 5MB.");
                        return View(article); // Trả về trang tạo bài viết với thông báo lỗi
                    }
                    var fileName = Path.GetFileName(HeaderImage.FileName);
                    var filePath = Path.Combine("wwwroot/images/news", fileName);

                    // Tạo thư mục "images/news" nếu chưa tồn tại
                    if (!Directory.Exists("wwwroot/images/news"))
                    {
                        Directory.CreateDirectory("wwwroot/images/news");
                    }

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await HeaderImage.CopyToAsync(stream);
                    }

                    // Cập nhật đường dẫn hình ảnh mới
                    article.HeaderImage = "/images/news/" + fileName;
                }
                else
                {
                    // Giữ nguyên đường dẫn hình ảnh hiện tại nếu không có hình ảnh mới
                    var existingArticle = await GetArticleById(article.Id); // Lấy bài viết hiện tại từ DB
                    article.HeaderImage = existingArticle.HeaderImage;
                }

                ArticlesNews modifyData = new ArticlesNews()
                {
                    Id = article.Id,
                    UserId = userId,
                    NameCreater = article.NameCreater,
                    Title = article.Title,
                    Content = article.Content,
                    IsActive = article.IsActive,
                    DateCreated = article.DateCreated,
                    HeaderImage = article.HeaderImage,
                };

                // Gửi yêu cầu PUT để cập nhật bài modifyData
                HttpResponseMessage response = await client.PutAsJsonAsync(client.BaseAddress + $"/news/{article.Id}", modifyData);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
            }

            return View(article); // Trả về view nếu có lỗi
        }

        // Phương thức phụ để lấy bài viết hiện tại từ cơ sở dữ liệu
        private async Task<ArticlesNews> GetArticleById(int id)
        {
            var response = await client.GetAsync($"api/news/{id}");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ArticlesNews>(data);
            }
            return null;
        }


        // GET: Delete an article by id (hiển thị để xác nhận xóa)
        [HttpGet, Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                // Lấy bài viết qua id để hiển thị
                HttpResponseMessage response = await client.GetAsync(client.BaseAddress + $"/news/{id}");
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var article = JsonConvert.DeserializeObject<ArticlesNews>(data);
                    return View(article); // Hiển thị bài viết để xác nhận xóa
                }

                return RedirectToAction("Error");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
                return RedirectToAction("Error");
            }
        }

        // POST: Delete the article (xác nhận xóa)
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                // Gửi yêu cầu DELETE để xóa bài viết
                HttpResponseMessage response = await client.DeleteAsync(client.BaseAddress + $"/news/{id}");

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
            }

            return RedirectToAction("Error");
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdateEvaluation(int articleId, int rating)
        {
            var userId = int.Parse(User.FindFirst("UserId")?.Value);

            // Kiểm tra xem người dùng đã đánh giá bài viết này chưa
            var existingEvaluation = await CheckUserEvaluation(articleId, userId);
            if (existingEvaluation != null)
            {
                // Nếu đã có đánh giá, cập nhật đánh giá mới
                existingEvaluation.Ratting = (short)rating;
                var jsonContent = JsonConvert.SerializeObject(existingEvaluation);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync($"/api/news/{articleId}/evaluations/{existingEvaluation.Id}", content);

                if (response.IsSuccessStatusCode)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Lỗi khi cập nhật đánh giá.");
                }
            }
            else
            {
                // Tạo đánh giá mới nếu chưa có
                var evaluationDto = new NewsEvaluation
                {
                    ArticlesNewsId = articleId,
                    UserId = userId,
                    Ratting = (short)rating
                };

                var jsonContent = JsonConvert.SerializeObject(evaluationDto);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync($"/api/news/{articleId}/evaluations", content);

                if (response.IsSuccessStatusCode)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Lỗi khi gửi đánh giá.");
                }
            }
        }


        // Phương thức phụ để kiểm tra xem người dùng đã đánh giá bài viết chưa
        private async Task<NewsEvaluation> CheckUserEvaluation(int articleId, int userId)
        {
            try
            {
                var response = await client.GetAsync($"/api/news/{articleId}/evaluations/{userId}");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<NewsEvaluation>(data);
                }
                else
                {
                    Console.WriteLine($"API Response Error: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking user evaluation: {ex.Message}");
            }
            return null;
        }


    }
}
