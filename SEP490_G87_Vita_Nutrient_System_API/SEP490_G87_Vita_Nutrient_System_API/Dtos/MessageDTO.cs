namespace SEP490_G87_Vita_Nutrient_System_API.Dtos
{
    public class MessageDTO
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
        public int FromUserId { get; set; }
        public int ToRoomId { get; set; }
    }
}
