using SEP490_G87_Vita_Nutrient_System_API.Dtos;
using SEP490_G87_Vita_Nutrient_System_API.Models;

namespace SEP490_G87_Vita_Nutrient_System_API.Domain.DataFoodList
{

    public class DataFoodAllDayOfWeek
    {      
        public int DayOfTheWeekId { get; set; }
        public short? DayOfTheWeekIdStart { get; set; }
        public DateTime DayOfWeek { get; set; }
        public string? NameDayOfWeek { get; set; }
        public DataFoodListMealOfTheDay[] dataListFoodMealOfTheDay { get; set; }
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
        public FoodListDTO foodData { get; set; }
    }

    public class DataFoodListMealOfTheDayComparer : IEqualityComparer<DataFoodListMealOfTheDay>
    {
        public bool Equals(DataFoodListMealOfTheDay x, DataFoodListMealOfTheDay y)
        {
            if (x == null || y == null) return false;

            // Kiểm tra các thuộc tính quan trọng, ví dụ như SlotOfTheDay, SettingDetail, và tập hợp idFood
            return x.SlotOfTheDay == y.SlotOfTheDay &&
                   x.SettingDetail == y.SettingDetail &&
                   x.foodIdData.Select(f => f.idFood).OrderBy(id => id)
                   .SequenceEqual(y.foodIdData.Select(f => f.idFood).OrderBy(id => id));
        }

        public int GetHashCode(DataFoodListMealOfTheDay obj)
        {
            if (obj == null) return 0;

            // Kết hợp các giá trị từ các thuộc tính để tạo mã băm duy nhất
            int hash = obj.SlotOfTheDay.GetHashCode() ^ obj.SettingDetail.GetHashCode();
            foreach (var id in obj.foodIdData.Select(f => f.idFood).OrderBy(id => id))
            {
                hash ^= id.GetHashCode();
            }
            return hash;
        }
    }


}
