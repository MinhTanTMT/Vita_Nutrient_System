﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using SEP490_G87_Vita_Nutrient_System_Client.Hubs;
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
        private readonly IHubContext<ChatHub> _chatHubContext;

        public ChatController(IHubContext<ChatHub> chatHubContext)
        {
            Uri URIBase = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetValue<Uri>("myUri");
            client = new HttpClient { BaseAddress = URIBase };
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            _chatHubContext = chatHubContext;
        }

        // Hiển thị danh sách phòng chat
        [HttpGet, Authorize(Roles = "Nutritionist")]
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

        [HttpGet, Authorize(Roles = "Nutritionist,UserPremium")]
        public async Task<IActionResult> Room(int? roomId)
        {
            int userId = int.Parse(User.FindFirst("UserId")?.Value);
            // Nếu roomId không có giá trị, gọi API GetRoomsByUser để lấy phòng chat của người dùng
            if (roomId == null)
            {
                HttpResponseMessage responseRoom = await client.GetAsync($"api/Chat/GetRoomsByUser/{userId}");

                if (responseRoom.IsSuccessStatusCode)
                {
                    string data = await responseRoom.Content.ReadAsStringAsync();
                    var rooms = JsonConvert.DeserializeObject<List<RoomModel>>(data);

                    if (rooms != null && rooms.Count == 1)
                    {
                        // Lấy phòng đầu tiên và chuyển tới phòng đó
                        roomId = rooms[0].Id;
                    }
                    else
                    {
                        return RedirectToAction("NutritionistServices", "Admin");
                    }
                }
                else
                {
                    return RedirectToAction("NutritionistServices", "Admin");
                }
            }
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
        [HttpPost, Authorize(Roles = "Nutritionist,UserPremium")]
        public async Task<IActionResult> SendMessage([FromBody] MessageModel message)
        {
            // Lấy thông tin người dùng (tên) từ API hoặc dịch vụ
            HttpResponseMessage userResponse = await client.GetAsync($"{client.BaseAddress}/Users/GetUserById/{message.FromUserId}");
            if (userResponse.IsSuccessStatusCode)
            {
                string userData = await userResponse.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<User>(userData);
                message.FromUserName = $"{user.FirstName} {user.LastName}";
            }
            else
            {
                message.FromUserName = "Unknown User"; // Trường hợp không tìm thấy thông tin người dùng
            }

            // Gửi tin nhắn tới API
            var jsonContent = new StringContent(JsonConvert.SerializeObject(message), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync($"{client.BaseAddress}/Chat/SendMessage", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                // Phát tin nhắn qua SignalR đến group
                await _chatHubContext.Clients.Group(message.ToRoomId.ToString())
                    .SendAsync("ReceiveMessage", message.FromUserName, message.Content, message.FromUserId);
                return Json(new { success = true });
            }

            return Json(new { success = false, message = "Không thể gửi tin nhắn" });
        }


    }
}
