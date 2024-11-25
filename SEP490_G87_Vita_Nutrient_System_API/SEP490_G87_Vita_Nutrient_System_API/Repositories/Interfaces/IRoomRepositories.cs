using SEP490_G87_Vita_Nutrient_System_API.Models;

namespace SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces
{
    public interface IRoomRepositories 
    {
        Task<IEnumerable<Room>> GetRoomsByUserIdAsync(int userId);
        Task<IEnumerable<Room>> GetRoomsByNutritionIdAsync(int nutritionID);
        Task<Room?> GetRoomByUserAndNutritionIdAsync(int userId, int nutritionId);
        Task AddRoomAsync(Room room);
    }
}
