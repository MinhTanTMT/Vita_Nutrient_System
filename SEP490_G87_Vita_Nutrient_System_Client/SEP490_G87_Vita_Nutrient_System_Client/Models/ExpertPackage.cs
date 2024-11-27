namespace SEP490_G87_Vita_Nutrient_System_Client.Models
{
    public partial class ExpertPackage
    {
        public short? Id { get; set; }

        public int NutritionistDetailsId { get; set; }

        public string? Name { get; set; }

        public string? Describe { get; set; }

        public decimal? Price { get; set; }

        public short? Duration { get; set; }
    }

    public partial class ExpertPackageResponse
    {
        public ExpertPackage Package { get; set; }
        public List<User> Nutritionists { get; set; }

        public class User
        {
            public int Id { get; set; }
            public string? Name { get; set; }
            public string? Account { get; set; }
        }
    }
}
