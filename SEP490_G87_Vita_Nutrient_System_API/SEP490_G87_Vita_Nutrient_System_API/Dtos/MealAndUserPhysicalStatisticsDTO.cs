namespace SEP490_G87_Vita_Nutrient_System_API.Dtos
{
    public class MealAndUserPhysicalStatisticsDTO
    {
        public int UserId { get; set; }
        public bool? Gender { get; set; }
        public short? Height { get; set; }
        public short? Weight { get; set; }
        public short? Age { get; set; }
        public double? ActivityLevel { get; set; }
        public short FoodTypeIdWant { get; set; }
        public short? NumberFood  { get;set; }
    }
}
