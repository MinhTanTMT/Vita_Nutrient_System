namespace SEP490_G87_Vita_Nutrient_System_Client.Models
{
    public partial class NutritionistDetail
    {
        public int Id { get; set; }

        public int NutritionistId { get; set; }

        public string? DescribeYourself { get; set; }

        public short? Height { get; set; }

        public short? Weight { get; set; }

        public short? Age { get; set; }

        public double? Rate { get; set; }

        public int? NumberRate { get; set; }

        public short? ExpertPackagesId { get; set; }
    }
}
