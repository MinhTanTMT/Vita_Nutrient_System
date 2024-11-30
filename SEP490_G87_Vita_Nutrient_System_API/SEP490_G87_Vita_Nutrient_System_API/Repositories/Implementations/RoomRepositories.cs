using Microsoft.EntityFrameworkCore;
using SEP490_G87_Vita_Nutrient_System_API.Dtos;
using SEP490_G87_Vita_Nutrient_System_API.Models;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces;

namespace SEP490_G87_Vita_Nutrient_System_API.Repositories.Implementations
{
    public class RoomRepositories : IRoomRepositories
    {
        private readonly Sep490G87VitaNutrientSystemContext _context = new Sep490G87VitaNutrientSystemContext();

        public RoomRepositories()
        {
        }

        public async Task<IEnumerable<Room>> GetRoomsByUserIdAsync(int userId)
        {
            return await _context.Rooms
                .Where(r => r.UserId == userId)
                .ToListAsync();
        }
        public async Task<IEnumerable<Room>> GetRoomsByNutritionIdAsync(int nutritionID)
        {
            // Lấy các phòng theo nutritionID
            var rooms = await _context.Rooms
                .Where(r => r.NutritionId == nutritionID)
                .Include(r => r.User) 
                .ToListAsync();

            if (rooms == null || !rooms.Any())
            {
                return Enumerable.Empty<Room>();
            }       
            var filteredRooms = rooms.Where(r => r.User.Role == 3).ToList();

            return filteredRooms;
        }

        public async Task<Room?> GetRoomByUserAndNutritionIdAsync(int userId, int nutritionId)
        {
            return await _context.Rooms
                .FirstOrDefaultAsync(r => r.UserId == userId && r.NutritionId == nutritionId);
        }
        public async Task AddRoomAsync(Room room)
        {
            await _context.Rooms.AddAsync(room); // Thêm Room vào DbContext
            await _context.SaveChangesAsync(); // Lưu thay đổi vào cơ sở dữ liệu
        }
    }
}
