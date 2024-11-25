using System.Collections.Generic;
using System.Threading.Tasks;
using SEP490_G87_Vita_Nutrient_System_API.Dtos;

namespace SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces
{
    public interface INutritionRouteRepositories
    {
        Task<NutritionRouteDTO> GetNutritionRouteByIdAsync(int id);
        Task<bool> CreateNutritionRouteAsync(NutritionRouteDTO nutritionRouteDto, int userListManagementId);
        Task<bool> UpdateNutritionRouteAsync(NutritionRouteDTO nutritionRouteDto, int userListManagementId);
        Task DeleteNutritionRouteAsync(int id);
        Task<IEnumerable<UserListManagementDTO>> GetPremiumUserByNutritionistIdAndUserIdAsync(int nutritionistId, int userId);
        Task<IEnumerable<UserDTO>> GetAllPremiumUserAsync(int nutritionistId);
        Task<bool> HasUnfinishedRouteAsync(int nutritionistId, int userId);
        Task<IEnumerable<NutritionRouteDTO>> GetNutritionRoutesAsync(int nutritionistId, int userId, int userListManagementId);

    }
}
