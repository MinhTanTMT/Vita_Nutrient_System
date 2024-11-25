using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace SEP490_G87_Vita_Nutrient_System_Client.Hubs
{
    public class ChatHub : Hub
    {
        

        // Người dùng tham gia phòng chat
        public async Task JoinRoom(string roomId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
            await Clients.Group(roomId).SendAsync("Notify", $"{Context.ConnectionId} has joined the room.");
        }

        // Người dùng rời khỏi phòng chat
        public async Task LeaveRoom(string roomId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId);
            await Clients.Group(roomId).SendAsync("Notify", $"{Context.ConnectionId} has left the room.");
        }

        // Gửi tin nhắn trong thời gian thực
        public async Task SendMessageToRoom(string roomId, string userName, string message, int userId)
        {
            await Clients.Group(roomId).SendAsync("ReceiveMessage", userName, message, userId);
        }
    }
}
