using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEP490_G87_Vita_Nutrient_System_API.Dtos;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces;

namespace SEP490_G87_Vita_Nutrient_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatRepositories _chatService;

        public ChatController(IChatRepositories chatService)
        {
            _chatService = chatService;
        }
        [HttpGet("GetRoomsByUser/{userId}")]
        public async Task<IActionResult> GetRoomsByUser(int userId)
        {
            var rooms = await _chatService.GetRoomsByUserIdAsync(userId);
            return Ok(rooms);
        }
        [HttpGet("GetRoomsByNutrition/{nutritionId}")]
        public async Task<IActionResult> GetRoomsByNutrition(int nutritionId)
        {
            var rooms = await _chatService.GetRoomsByNutritionIdAsync(nutritionId);
            return Ok(rooms);
        }
        [HttpGet("GetMessagesByRoom/{roomId}")]
        public async Task<IActionResult> GetMessagesByRoom(int roomId)
        {
            var messages = await _chatService.GetMessagesByRoomIdAsync(roomId);
            return Ok(messages);
        }

        [HttpPost("CreateOrGetRoom")]
        public async Task<IActionResult> CreateOrGetRoom([FromBody] RoomDTO roomDto)
        {
            var roomId = await _chatService.CreateOrGetRoomAsync(roomDto.UserId, roomDto.NutritionId);
            return Ok(new { RoomId = roomId });
        }

        [HttpPost("SendMessage")]
        public async Task<IActionResult> SendMessage([FromBody] MessageDTO messageDto)
        {
            await _chatService.SendMessageAsync(messageDto);
            return Ok();
        }
    }
}
