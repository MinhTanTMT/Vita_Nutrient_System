using SEP490_G87_Vita_Nutrient_System_API.Models;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces;

namespace SEP490_G87_Vita_Nutrient_System_API.Repositories.Implementations
{
    public class DiseaseRepositories : IDiseaseRepositories
    {
        public DiseaseRepositories() { }

        Sep490G87VitaNutrientSystemContext _context = new Sep490G87VitaNutrientSystemContext();
        public List<ListOfDisease> GetAllDiseases()
        {
            return _context.ListOfDiseases.ToList();
        }

        public ListOfDisease GetDisease(int id)
        {
            return _context.ListOfDiseases.Find(id);
        }
    }
}
