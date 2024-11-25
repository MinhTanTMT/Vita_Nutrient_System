using System.ComponentModel.DataAnnotations;

namespace SEP490_G87_Vita_Nutrient_System_Client.Models
{
    public class MessageModel
    {
        public int Id { get; set; }
        [MaxLength(2000)]
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
        public int FromUserId { get; set; }
        public int ToRoomId { get; set; }
        // Thêm thuộc tính này
        public string FromUserName { get; set; } // Tên người gửi
    }
}
