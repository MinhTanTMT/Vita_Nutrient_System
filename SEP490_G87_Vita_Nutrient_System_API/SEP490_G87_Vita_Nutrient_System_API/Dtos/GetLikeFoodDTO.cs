namespace SEP490_G87_Vita_Nutrient_System_API.Dtos
{
    public class GetLikeFoodDTO
    {
        public int UserId { get; set; }
        public string? Search { get; set; } = null;
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
