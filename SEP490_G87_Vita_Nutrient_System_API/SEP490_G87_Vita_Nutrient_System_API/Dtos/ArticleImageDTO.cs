namespace SEP490_G87_Vita_Nutrient_System_API.Dtos
{
    public class ArticleImageDTO
    {
        public int Id { get; set; }

        public int ArticleId { get; set; }

        public string ImagePath { get; set; } = null!;
    }
}
