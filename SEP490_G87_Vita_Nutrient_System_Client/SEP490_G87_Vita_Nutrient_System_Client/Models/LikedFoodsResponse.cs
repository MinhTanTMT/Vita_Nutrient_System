namespace SEP490_G87_Vita_Nutrient_System_Client.Models
{
    public class LikedFoodsResponse
    {
        public List<FoodItem> Items { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
    }

    public class FoodItem
    {
        public int FoodListId { get; set; }
        public string Name { get; set; }
        public string Describe { get; set; }
        public double Rate { get; set; }
        public int NumberRate { get; set; }
        public string UrlImage { get; set; }
        public int PreparationTime { get; set; }
        public int CookingTime { get; set; }
        public int CookingDifficultyId { get; set; }
    }

}
