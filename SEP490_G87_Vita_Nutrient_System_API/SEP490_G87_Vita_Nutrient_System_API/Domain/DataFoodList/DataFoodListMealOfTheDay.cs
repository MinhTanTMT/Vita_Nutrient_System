namespace SEP490_G87_Vita_Nutrient_System_API.Domain.DataFoodList
{
    public class DataFoodListMealOfTheDay
    {
        public int SlotOfTheDay { get; set; }
        public int SettingDetail { get; set; }
        public FoodIdData[] foodIdData { get; set; }
    }

    public class FoodIdData
    {
        public int idFood { get; set; }
        public string statusSymbol { get; set; }

    }
}
