using Microsoft.EntityFrameworkCore;
using SEP490_G87_Vita_Nutrient_System_API.Models;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces;

namespace SEP490_G87_Vita_Nutrient_System_API.Repositories.Implementations
{
    public class MessageRepositories : IMessageRepositories
    {
        private readonly Sep490G87VitaNutrientSystemContext _context = new Sep490G87VitaNutrientSystemContext();

        public MessageRepositories()
        {
        }
        public async Task<IEnumerable<Message>> GetMessagesByRoomIdAsync(int roomId)
        {
            return await _context.Messages.Where(m => m.ToRoomId == roomId).OrderBy(m => m.Timestamp).ToListAsync();
        }
        public async Task AddMessageAsync(Message message)
        {
            await _context.Messages.AddAsync(message); // Thêm Message vào DbContext
            await _context.SaveChangesAsync(); // Lưu thay đổi vào cơ sở dữ liệu
        }
    }
}
