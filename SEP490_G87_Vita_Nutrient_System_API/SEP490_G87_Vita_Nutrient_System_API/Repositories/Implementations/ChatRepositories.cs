using SEP490_G87_Vita_Nutrient_System_API.Dtos;
using SEP490_G87_Vita_Nutrient_System_API.Models;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces;

namespace SEP490_G87_Vita_Nutrient_System_API.Repositories.Implementations
{
    public class ChatRepositories : IChatRepositories
    {
        private readonly IRoomRepositories _roomRepository;
        private readonly IMessageRepositories _messageRepository;

        public ChatRepositories(IRoomRepositories roomRepository, IMessageRepositories messageRepository)
        {
            _roomRepository = roomRepository;
            _messageRepository = messageRepository;
        }
        public async Task<IEnumerable<RoomDTO>> GetRoomsByUserIdAsync(int userId)
        {
            var rooms = await _roomRepository.GetRoomsByUserIdAsync(userId);
            return rooms.Select(r => new RoomDTO { Id = r.Id, Name = r.Name, NutritionId = r.NutritionId, UserId = r.UserId });
        }
        public async Task<IEnumerable<RoomDTO>> GetRoomsByNutritionIdAsync(int NutritionId)
        {
            var rooms = await _roomRepository.GetRoomsByNutritionIdAsync(NutritionId);
            return rooms.Select(r => new RoomDTO { Id = r.Id, Name = r.Name, NutritionId = r.NutritionId, UserId = r.UserId });
        }
        
        public async Task<IEnumerable<MessageDTO>> GetMessagesByRoomIdAsync(int roomId)
        {
            var messages = await _messageRepository.GetMessagesByRoomIdAsync(roomId);
            return messages.Select(m => new MessageDTO { Id = m.Id, Content = m.Content, Timestamp = m.Timestamp.Value, FromUserId = m.FromUserId, ToRoomId = m.ToRoomId });
        }

        public async Task<int> CreateOrGetRoomAsync(int userId, int nutritionId)
        {
            var room = await _roomRepository.GetRoomByUserAndNutritionIdAsync(userId, nutritionId);
            if (room != null) return room.Id;

            var newRoom = new Room
            {
                Name = $"Chat {userId}-{nutritionId}",
                UserId = userId,
                NutritionId = nutritionId
            };

            await _roomRepository.AddRoomAsync(newRoom); // Sử dụng AddRoomAsync
            return newRoom.Id;
        }

        public async Task SendMessageAsync(MessageDTO messageDto)
        {
            var message = new Message
            {
                Content = messageDto.Content,
                Timestamp = DateTime.Now,
                FromUserId = messageDto.FromUserId,
                ToRoomId = messageDto.ToRoomId
            };

            await _messageRepository.AddMessageAsync(message); // Sử dụng AddMessageAsync
        }
    }
}
