using SEP490_G87_Vita_Nutrient_System_API.Dtos;

namespace SEP490_G87_Vita_Nutrient_System_API.Domain.DataFoodList
{
    public class DataFoodListMealOfTheDay
    {
        public short SlotOfTheDay { get; set; }
        public int SettingDetail { get; set; }

        public string? NameSlotOfTheDay { get; set; }

        public FoodListDTO[] foodIdData { get; set; }
    }
}
