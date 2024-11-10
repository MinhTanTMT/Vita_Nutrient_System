namespace SEP490_G87_Vita_Nutrient_System_API.Dtos
{
    public class MealSettingDTO
    {
        public short? FoodTypeIdWant { get; set; }
        public short? DayOfTheWeekStartId { get; set; }

        public bool? SameScheduleEveryDay { get; set; }
    }
}
