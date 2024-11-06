namespace SEP490_G87_Vita_Nutrient_System_API.Domain.DataFoodList
{
    public class FoodStatusUpdateModel
    {
        public int UserId { get; set; }
        public DateTime MyDay { get; set; }
        public int SlotOfTheDay { get; set; }
        public int SettingDetail { get; set; }
        public int IdFood { get; set; }
        public string StatusSymbol { get; set; }
        public int PositionFood { get; set; }
        public int OrderNumber { get; set; }
    }
}
