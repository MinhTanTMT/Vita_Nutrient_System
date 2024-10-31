using Microsoft.EntityFrameworkCore;
using SEP490_G87_Vita_Nutrient_System_API.Models;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces;

namespace SEP490_G87_Vita_Nutrient_System_API.Repositories.Implementations
{
    public class CookingDifficultiesRepositories : ICookingDifficultiesRepositories
    {
        private readonly SEP490_G87_VitaNutrientSystemContext _context = new SEP490_G87_VitaNutrientSystemContext();

        public CookingDifficultiesRepositories()
        {
        }

        ////////////////////////////////////////////////////////////
        /// Chiến
        ////////////////////////////////////////////////////////////
        ///
        public async Task<List<CookingDifficulty>> GetAllCookingDifficultiesAsync()
        {
            return await _context.CookingDifficulties.ToListAsync();
        }
    }
}
