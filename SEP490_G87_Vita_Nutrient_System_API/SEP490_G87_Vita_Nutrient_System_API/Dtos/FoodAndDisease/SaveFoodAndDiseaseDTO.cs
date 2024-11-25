namespace SEP490_G87_Vita_Nutrient_System_API.Dtos.FoodAndDisease
{
    public class SaveFoodAndDiseaseDTO
    {
        public int ListOfDiseasesId { get; set; }

        public int FoodListId { get; set; }

        public string? Describe { get; set; }

        public bool? IsGoodOrBad { get; set; }
    }
}
