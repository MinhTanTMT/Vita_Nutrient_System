using SEP490_G87_Vita_Nutrient_System_API.Dtos;
using SEP490_G87_Vita_Nutrient_System_API.Models;

namespace SEP490_G87_Vita_Nutrient_System_API.Domain.DataFoodList
{
    public class DataFoodListMealOfTheDay
    {
        public short SlotOfTheDay { get; set; }
        public int SettingDetail { get; set; }
        public string? NameSlotOfTheDay { get; set; }

        public FoodIdData[] foodIdData { get; set; }
    }


    public class FoodIdData
    {
        public int idFood { get; set; }
        public string statusSymbol { get; set; }
        public FoodListDTO foodData { get; set; }
    }

}
