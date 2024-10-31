using Microsoft.EntityFrameworkCore;
using SEP490_G87_Vita_Nutrient_System_API.Models;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces;

namespace SEP490_G87_Vita_Nutrient_System_API.Repositories.Implementations
{
    public class DayOfTheWeekRepositories : IDayOfTheWeekRepositories
    {
        private readonly SEP490_G87_VitaNutrientSystemContext _context = new SEP490_G87_VitaNutrientSystemContext();

        public DayOfTheWeekRepositories()
        {
        }

        ////////////////////////////////////////////////////////////
        /// Chiến
        ////////////////////////////////////////////////////////////
        ///
        public async Task<List<DayOfTheWeek>> GetAllDayOfTheWeekAsync()
        {
            return await _context.DayOfTheWeeks.ToListAsync();
        }
    }
}
