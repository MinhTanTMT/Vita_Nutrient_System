using System.Collections.Generic;
using System.Threading.Tasks;
using SEP490_G87_Vita_Nutrient_System_API.Dtos;

namespace SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces
{
    public interface INutritionRouteRepositories
    {
        Task<UserListManagementDTO> GetNutritionRouteByIdAsync(int id);
        Task<bool> CreateNutritionRouteAsync(UserListManagementDTO userListManagementDto);
        Task UpdateNutritionRouteAsync(UserListManagementDTO userListManagementDto);
        Task DeleteNutritionRouteAsync(int id);
        Task<IEnumerable<UserListManagementDTO>> GetNutritionRoutesByNutritionistIdAndUserIdAsync(int nutritionistId, int userId);
        Task<IEnumerable<UserDTO>> GetAllUsersByCreateIdAsync(int nutritionistId);
        /*Task<IEnumerable<UserListManagementDTO>> GetUsersWithUnfinishedRoutesAsync(int nutritionistId, int userId);*/
        Task<bool> HasUnfinishedRouteAsync(int nutritionistId, int userId);
    }
}
