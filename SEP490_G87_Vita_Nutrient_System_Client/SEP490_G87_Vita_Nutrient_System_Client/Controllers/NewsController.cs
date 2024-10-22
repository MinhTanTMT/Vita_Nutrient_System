using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SEP490_G87_Vita_Nutrient_System_Client.Models;
using System.Net.Http.Headers;
using System.Text;

namespace SEP490_G87_Vita_Nutrient_System_Client.Controllers
{
    public class NewsController : Controller
    {
        private readonly HttpClient _client;
        private readonly string _newsApiUrl;

        public NewsController(IConfiguration configuration)
        {
            var uriBase = configuration.GetValue<string>("myUri");
            _client = new HttpClient
            {
                BaseAddress = new Uri(uriBase)
            };
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _newsApiUrl = "api/news";
        }

        // GET: /News
        public async Task<IActionResult> Index()
        {
            var response = await _client.GetAsync(_newsApiUrl);
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var articles = JsonConvert.DeserializeObject<IEnumerable<ArticlesNews>>(jsonData);
                return View(articles);
            }
            ModelState.AddModelError(string.Empty, "Lỗi khi lấy danh sách bài viết.");
            return View(new List<ArticlesNews>());
        }

        // GET: /News/Details/{id}
        public async Task<IActionResult> Details(int id)
        {
            var response = await _client.GetAsync($"{_newsApiUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var article = JsonConvert.DeserializeObject<ArticlesNews>(jsonData);
                return View(article);
            }
            ModelState.AddModelError(string.Empty, "Không tìm thấy bài viết.");
            return NotFound();
        }

        // GET: /News/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /News/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ArticlesNews article)
        {
            if (!ModelState.IsValid)
            {
                return View(article);
            }

            var jsonContent = new StringContent(JsonConvert.SerializeObject(article), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(_newsApiUrl, jsonContent);
            if (response.IsSuccessStatusCode)
            {
                // Sau khi tạo mới thành công, chuyển hướng về trang Index
                return RedirectToAction(nameof(Index));
            }
            // Nếu có lỗi khi tạo mới, hiển thị lỗi trên giao diện
            var errorContent = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError(string.Empty, $"Lỗi khi tạo bài viết: {errorContent}");
            return View(article);
        }

        // GET: /News/Edit/{id}
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _client.GetAsync($"{_newsApiUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var article = JsonConvert.DeserializeObject<ArticlesNews>(jsonData);
                return View(article);
            }
            ModelState.AddModelError(string.Empty, "Không tìm thấy bài viết.");
            return NotFound();
        }

        // POST: /News/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ArticlesNews article)
        {
            if (id != article.Id || !ModelState.IsValid)
            {
                return View(article);
            }

            var jsonContent = new StringContent(JsonConvert.SerializeObject(article), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"{_newsApiUrl}/{id}", jsonContent);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            var errorContent = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError(string.Empty, $"Lỗi khi cập nhật bài viết: {errorContent}");
            return View(article);
        }

        // GET: /News/Delete/{id}
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _client.GetAsync($"{_newsApiUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var article = JsonConvert.DeserializeObject<ArticlesNews>(jsonData);
                return View(article);
            }
            ModelState.AddModelError(string.Empty, "Không tìm thấy bài viết.");
            return NotFound();
        }

        // POST: /News/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _client.DeleteAsync($"{_newsApiUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            var errorContent = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError(string.Empty, $"Lỗi khi xóa bài viết: {errorContent}");
            return View();
        }
    }
}
