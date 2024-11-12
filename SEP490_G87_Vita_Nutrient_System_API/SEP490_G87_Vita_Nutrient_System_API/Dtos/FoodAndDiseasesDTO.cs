using SEP490_G87_Vita_Nutrient_System_API.Models;

namespace SEP490_G87_Vita_Nutrient_System_API.Dtos
{
    public class FoodAndDiseasesDTO
    {
        public List<FoodListDTO> FoodLists { get; set; }
        public List<ListOfDisease> Diseases { get; set; }
        public List<FoodAndDiseaseDTO> FoodAndDiseases { get; set; }
    }
}
