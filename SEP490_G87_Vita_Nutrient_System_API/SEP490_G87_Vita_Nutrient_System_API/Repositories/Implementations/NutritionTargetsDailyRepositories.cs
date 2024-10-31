using Microsoft.EntityFrameworkCore;
using SEP490_G87_Vita_Nutrient_System_API.Models;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces;

namespace SEP490_G87_Vita_Nutrient_System_API.Repositories.Implementations
{
    public class NutritionTargetsDailyRepositories : INutritionTargetsDailyRepositories
    {
        private readonly Sep490G87VitaNutrientSystemContext _context = new Sep490G87VitaNutrientSystemContext();

        public NutritionTargetsDailyRepositories()
        {

        }

        ////////////////////////////////////////////////////////////
        /// Chiến
        ////////////////////////////////////////////////////////////
        ///
        public async Task<List<NutritionTargetsDaily>> GetAllNutritionTargetsDailyAsync()
        {
            return await _context.NutritionTargetsDailies.ToListAsync();
        }

    }
}
