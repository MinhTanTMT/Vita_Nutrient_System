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
        private readonly HttpClient client = null;

        public NewsController(IConfiguration configuration)
        {
            Uri URIBase = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetValue<Uri>("myUri");
            client = new HttpClient();
            client.BaseAddress = URIBase;
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }

        // GET: List of all articles
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await client.GetAsync("api/news");

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var articles = JsonConvert.DeserializeObject<List<ArticlesNewsDTO>>(data);
                    return View(articles);
                }

                ModelState.AddModelError(string.Empty, "Error retrieving data from server.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
            }
            return View(new List<ArticlesNewsDTO>());
        }

        // GET: Details of a single article
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var response = await client.GetAsync($"api/news/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var article = JsonConvert.DeserializeObject<ArticlesNewsDTO>(data);
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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Create a new article
        [HttpPost]
        public async Task<IActionResult> Create(ArticlesNewsDTO article, IFormFile HeaderImage)
        {
            if (!ModelState.IsValid)
            {
                return View(article);
            }

            try
            {
                int userId = 1; // Giả sử UserId là 1

                // Kiểm tra và xử lý tệp hình ảnh
                if (HeaderImage != null && HeaderImage.Length > 0)
                {
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
                var createData = new ArticlesNewsDTO()
                {
                    UserId = userId,
                    NameCreater = article.NameCreater,
                    Title = article.Title,
                    Content = article.Content,
                    IsActive = article.IsActive ?? true,
                    DateCreated = article.DateCreated ?? DateTime.Now,
                    HeaderImage = article.HeaderImage
                };

                // Gửi yêu cầu POST đến API và xử lý phản hồi
                HttpResponseMessage response = await client.PostAsJsonAsync("api/news", createData);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index"); // Chuyển hướng về trang Index khi thành công
                }
                else
                {
                    // Ghi lại thông tin lỗi từ API
                    string errorDetails = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, "Lỗi từ API: " + errorDetails);
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi bất ngờ
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
            }

            return View(article); // Trả về view cùng dữ liệu khi có lỗi
        }

        // GET: Edit an article by id
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                // Gửi yêu cầu GET để lấy dữ liệu bài viết qua id
                HttpResponseMessage response = await client.GetAsync(client.BaseAddress + $"/news/{id}");
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var article = JsonConvert.DeserializeObject<ArticlesNewsDTO>(data);
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
        public async Task<IActionResult> Edit(ArticlesNewsDTO article, IFormFile HeaderImage)
        {
            if (!ModelState.IsValid)
            {
                return View(article); // Trả về view nếu model không hợp lệ
            }

            try
            {
                int userId = 1; // Giả sử UserId là 1
                // Xử lý file upload
                if (HeaderImage != null && HeaderImage.Length > 0)
                {
                    var fileName = Path.GetFileName(HeaderImage.FileName);
                    var filePath = Path.Combine("wwwroot/images/news", fileName);

                    // Tạo thư mục "images" nếu chưa tồn tại
                    if (!Directory.Exists("wwwroot/images/news"))
                    {
                        Directory.CreateDirectory("wwwroot/images/news");
                    }

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await HeaderImage.CopyToAsync(stream);
                    }

                    article.HeaderImage = "/images/news/" + fileName;
                }
                ArticlesNewsDTO modifyData = new ArticlesNewsDTO()
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

        // GET: Delete an article by id (hiển thị để xác nhận xóa)
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                // Lấy bài viết qua id để hiển thị
                HttpResponseMessage response = await client.GetAsync(client.BaseAddress + $"/news/{id}");
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var article = JsonConvert.DeserializeObject<ArticlesNewsDTO>(data);
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
    }
}
