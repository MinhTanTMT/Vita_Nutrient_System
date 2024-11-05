namespace SEP490_G87_Vita_Nutrient_System_API.Dtos
{
    public class NewsEvaluationDTO
    {
        public int Id { get; set; }
        public int ArticlesNewsId { get; set; }
        public int UserId { get; set; }
        public short? Ratting { get; set; }
    }
}
