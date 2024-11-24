using Microsoft.EntityFrameworkCore;
using SEP490_G87_Vita_Nutrient_System_API.Models;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces;

namespace SEP490_G87_Vita_Nutrient_System_API.Repositories.Implementations
{
    public class UserListManagementRepositories : IUserListManagementRepositories
    {
        private readonly Sep490G87VitaNutrientSystemContext _context = new Sep490G87VitaNutrientSystemContext();

        public UserListManagementRepositories()
        {
        }
        public async Task<IEnumerable<UserListManagement>> GetUsersByNutritionIdAsync(int nutritionId)
        {
            return await _context.UserListManagements
                .Where(ul => ul.NutritionistId == nutritionId)
                .ToListAsync();
        }
    }
}
