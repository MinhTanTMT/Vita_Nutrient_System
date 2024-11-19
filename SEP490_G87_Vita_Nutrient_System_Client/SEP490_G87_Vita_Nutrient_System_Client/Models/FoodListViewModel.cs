namespace SEP490_G87_Vita_Nutrient_System_Client.Models
{
    public class FoodListViewModel
    {
        public List<Food> Foods { get; set; }
        public List<FoodType> FoodTypes { get; set; }
        public List<KeyNote> KeyNotes { get; set; }
        public List<CookingDifficulty> CookingDifficulties { get; set; }
        public List<ListOfDisease> ListOfDiseases { get; set; }
        public List<ListFoodAndDisease> FoodAndDiseases { get; set; }
    }
}
