using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SEP490_G87_Vita_Nutrient_System_Client.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SEP490_G87_Vita_Nutrient_System_Client.Controllers
{
    public class ChatController : Controller
    {
        private readonly HttpClient client;

        public ChatController()
        {
            Uri URIBase = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetValue<Uri>("myUri");
            client = new HttpClient();
            client.BaseAddress = URIBase;
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }

        // Hiển thị danh sách phòng chat
        [HttpGet]
        public async Task<IActionResult> Index()
        
        {

            int userId = int.Parse(User.FindFirst("UserId")?.Value);    
            HttpResponseMessage response = await client.GetAsync($"api/Chat/GetRoomsByNutrition/{userId}");

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var rooms = JsonConvert.DeserializeObject<List<RoomModel>>(data);
                return View(rooms);
            }

            return View("Error"); // Nếu API trả lỗi
        }

        [HttpGet]
        public async Task<IActionResult> Room(int roomId)
        {
            int userId = int.Parse(User.FindFirst("UserId")?.Value);

            // Gọi API để lấy tin nhắn của room
            HttpResponseMessage response = await client.GetAsync($"{client.BaseAddress}/Chat/GetMessagesByRoom/{roomId}");
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var messages = JsonConvert.DeserializeObject<List<MessageModel>>(data);

                // Lấy thông tin tên của từng user
                foreach (var message in messages)
                {
                    HttpResponseMessage userResponse = await client.GetAsync($"{client.BaseAddress}/Users/GetUserById/{message.FromUserId}");
                    if (userResponse.IsSuccessStatusCode)
                    {
                        string userData = await userResponse.Content.ReadAsStringAsync();
                        var user = JsonConvert.DeserializeObject<User>(userData);
                        message.FromUserName = $"{user.FirstName} {user.LastName}";
                    }
                    else
                    {
                        message.FromUserName = "Unknown User"; // Gán giá trị mặc định nếu không tìm thấy user
                    }
                }

                ViewData["RoomId"] = roomId;
                ViewData["UserId"] = userId;

                  return View(messages); // Trả danh sách tin nhắn qua View
            }

            return View("Error");
        }



        // Gửi tin nhắn
        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] MessageModel message)
        {
            var jsonContent = new StringContent(JsonConvert.SerializeObject(message), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync($"{client.BaseAddress}/Chat/SendMessage", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }

            return Json(new { success = false, message = "Failed to send message" });
        }
    }
}
