namespace SEP490_G87_Vita_Nutrient_System_API.Dtos
{
    public class NutritionistDetailDTO
    {
        public int Id { get; set; }

        public int NutritionistId { get; set; }

        public string? DescribeYourself { get; set; }

        public short? Height { get; set; }

        public short? Weight { get; set; }

        public short? Age { get; set; }

        public double? Rate { get; set; }

        public int? NumberRate { get; set; }

        public virtual ICollection<ExpertPackageDTO> ExpertPackages { get; set; } = new List<ExpertPackageDTO>();
    }
}
