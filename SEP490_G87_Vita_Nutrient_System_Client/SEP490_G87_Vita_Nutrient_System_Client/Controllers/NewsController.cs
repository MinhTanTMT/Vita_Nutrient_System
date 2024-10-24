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
                    var articles = JsonConvert.DeserializeObject<List<ArticlesNews>>(data);
                    return View(articles);
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
        [HttpGet]
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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Create a new article
        [HttpPost]

        public async Task<IActionResult> Create(ArticlesNews article)
        {
            if (!ModelState.IsValid)
            {
                return Redirect("Error"); // Trả về view nếu dữ liệu không hợp lệ
            }

            try
            {
               /* int userId = int.Parse(User.FindFirst("UserId")?.Value);*/
                int userId = 1; // Giả sử UserId là 1
                ArticlesNews createData = new ArticlesNews()
                {
                    UserId = userId,
                    NameCreater = article.NameCreater,
                    Title = article.Title,
                    Content = article.Content,
                    IsActive = article.IsActive,
                    DateCreated = article.DateCreated,
                    HeaderImage = article.HeaderImage,
                };

                // Gửi yêu cầu POST đến API để tạo bài viết
                HttpResponseMessage response = await client.PostAsJsonAsync(client.BaseAddress + "/news", createData);

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

            return View(article);
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
        public async Task<IActionResult> Edit(ArticlesNews article)
        {
            if (!ModelState.IsValid)
            {
                return View(article); // Trả về view nếu model không hợp lệ
            }

            try
            {
                int userId = 1; // Giả sử UserId là 1
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
    }
}
