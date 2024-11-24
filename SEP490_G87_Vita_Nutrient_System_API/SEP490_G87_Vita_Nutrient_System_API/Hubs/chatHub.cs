using Microsoft.AspNetCore.SignalR;

namespace SEP490_G87_Vita_Nutrient_System_API.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessageToRoom(string roomId, string userName, string message)
        {
            await Clients.Group(roomId).SendAsync("ReceiveMessage", userName, message);
        }

        public async Task JoinRoom(string roomId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
        }

        public async Task LeaveRoom(string roomId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId);
        }
    }
}
