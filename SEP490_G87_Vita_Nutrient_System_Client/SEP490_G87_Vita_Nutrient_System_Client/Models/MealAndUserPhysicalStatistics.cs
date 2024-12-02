namespace SEP490_G87_Vita_Nutrient_System_Client.Models
{
    public class MealAndUserPhysicalStatistics
    {
        public int UserId { get; set; }
        public bool? Gender { get; set; }
        public short? Height { get; set; }
        public short? Weight { get; set; }
        public short? Age { get; set; }
        public double? ActivityLevel { get; set; }
        public short FoodTypeIdWant { get; set; }
        public short? NumberFood { get; set; }
        public DateTime? TimeUpdate { get; set; }
        public int? WeightGoal { get; set; }
    }
}
