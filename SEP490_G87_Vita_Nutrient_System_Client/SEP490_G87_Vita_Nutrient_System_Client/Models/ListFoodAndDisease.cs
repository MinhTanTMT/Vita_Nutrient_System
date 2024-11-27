namespace SEP490_G87_Vita_Nutrient_System_Client.Models
{
    public class ListFoodAndDisease
    {
        public int FoodListId { get; set; }
        public int ListOfDiseasesId { get; set; }
        public string? DiseaseName { get; set; }
        public string? DiseaseDescribe { get; set; }
        public string? FoodName { get; set; }
        public string? FoodDescribe { get; set; }
        public string? Describe { get; set; }
        public bool IsGoodOrBad { get; set; }
    }
}
