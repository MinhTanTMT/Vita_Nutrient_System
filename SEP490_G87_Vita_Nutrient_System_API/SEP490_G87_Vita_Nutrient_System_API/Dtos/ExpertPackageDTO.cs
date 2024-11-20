namespace SEP490_G87_Vita_Nutrient_System_API.Dtos
{
    public class ExpertPackageDTO
    {
        public short Id { get; set; }

        public string? Name { get; set; }

        public string? Describe { get; set; }

        public decimal? Price { get; set; }

        public short? Duration { get; set; }

        public virtual ICollection<NutritionistDetailDTO> NutritionistDetails { get; set; } = new List<NutritionistDetailDTO>();
    }
}
