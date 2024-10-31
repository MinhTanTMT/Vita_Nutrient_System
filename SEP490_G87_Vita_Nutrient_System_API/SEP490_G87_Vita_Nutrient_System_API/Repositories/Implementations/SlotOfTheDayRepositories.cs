using Microsoft.EntityFrameworkCore;
using SEP490_G87_Vita_Nutrient_System_API.Models;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces;

namespace SEP490_G87_Vita_Nutrient_System_API.Repositories.Implementations
{
    public class SlotOfTheDayRepositories : ISlotOfTheDayRepositories
    {
        private readonly SEP490_G87_VitaNutrientSystemContext _context = new SEP490_G87_VitaNutrientSystemContext();

        public SlotOfTheDayRepositories()
        {

        }

        ////////////////////////////////////////////////////////////
        /// Chiến
        ////////////////////////////////////////////////////////////
        ///
        public async Task<List<SlotOfTheDay>> GetAllSlotOfTheDayAsync()
        {
            return await _context.SlotOfTheDays.ToListAsync();
        }

    }
}
