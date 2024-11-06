namespace SEP490_G87_Vita_Nutrient_System_Client.Models
{
    public class MealSetting
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public short DayOfTheWeekStartId { get; set; }

        public bool SameScheduleEveryDay { get; set; }

    }
}
