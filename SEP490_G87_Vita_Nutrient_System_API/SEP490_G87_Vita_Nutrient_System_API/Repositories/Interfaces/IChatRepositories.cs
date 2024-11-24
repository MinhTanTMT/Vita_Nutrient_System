using SEP490_G87_Vita_Nutrient_System_API.Dtos;

namespace SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces
{
    public interface IChatRepositories
    {
        Task<IEnumerable<RoomDTO>> GetRoomsByUserIdAsync(int userId);
        Task<IEnumerable<RoomDTO>> GetRoomsByNutritionIdAsync(int nutritionId);
        Task<IEnumerable<MessageDTO>> GetMessagesByRoomIdAsync(int roomId);
        Task<int> CreateOrGetRoomAsync(int userId, int nutritionId);
        Task SendMessageAsync(MessageDTO messageDto);
    }
}
