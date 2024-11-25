using Newtonsoft.Json;

namespace SEP490_G87_Vita_Nutrient_System_Client.Models
{
    public class Food
    {
        public int FoodListId { get; set; }
        public int FoodTypeId { get; set; }
        public int KeyNoteId { get; set; }
        public int CookingDifficultyId { get; set; }
        public string Name { get; set; }
        public string Describe { get; set; }
        public double Rate { get; set; }
        public int NumberRate { get; set; }
        public string UrlImage { get; set; }
        public string FoodTypeName { get; set; }
        public string KeyNoteName { get; set; }
        public bool IsActive { get; set; }
        public int PreparationTime { get; set; }
        public int CookingTime { get; set; }
        public string CookingDifficultyName { get; set; }
    }
}
