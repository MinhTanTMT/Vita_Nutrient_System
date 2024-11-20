namespace SEP490_G87_Vita_Nutrient_System_API.Dtos
{
    public class GetLikeFoodDTO
    {
        public string? Search { get; set; } = null;
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
