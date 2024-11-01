namespace SEP490_G87_Vita_Nutrient_System_Client.Models
{
    public class SlotBranch
    {
        public short SlotOfTheDay { get; set; }
        public string? NameSlotOfTheDay { get; set; }
        public float TotalCaloriesPerMeal { get; set; }
        public DataFoodListMealOfTheDay[] foodDataOfSlot { get; set; }
    }

    public class DataFoodListMealOfTheDay
    {
        public short SlotOfTheDay { get; set; }
        public int SettingDetail { get; set; }
        public int OrderSettingDetail { get; set; }
        public string? NameSlotOfTheDay { get; set; }
        public FoodIdData[] foodIdData { get; set; }
    }

    public class FoodIdData
    {
        public int idFood { get; set; }
        public string statusSymbol { get; set; }
        public int positionFood { get; set; }
        public FoodList foodData { get; set; }
    }

}


