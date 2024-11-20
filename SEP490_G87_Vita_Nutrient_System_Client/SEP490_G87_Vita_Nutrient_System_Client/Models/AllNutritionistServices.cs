namespace SEP490_G87_Vita_Nutrient_System_Client.Models
{
    public class AllNutritionistServices
    {
        public short Id { get; set; }

        public string? Name { get; set; }

        public string? Describe { get; set; }

        public decimal? Price { get; set; }

        public short? Duration { get; set; }

        public virtual ICollection<NutritionistDetail> NutritionistDetails { get; set; } = new List<NutritionistDetail>();
    }


}
