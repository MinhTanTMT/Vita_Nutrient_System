namespace SEP490_G87_Vita_Nutrient_System_API.Dtos
{
    public class NutritionRouteDTO
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int CreateById { get; set; }

        public string? Name { get; set; }

        public string? Describe { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool? IsDone { get; set; }
        public string? UserName { get; set; }
        public string? CreateByName { get; set; }
    }
}
