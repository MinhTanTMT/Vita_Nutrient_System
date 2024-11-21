using System.Collections.Generic;
using System.Threading.Tasks;
using SEP490_G87_Vita_Nutrient_System_API.Dtos;

namespace SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces
{
    public interface INutritionRouteRepositories
    {
        Task<NutritionRouteDTO> GetNutritionRouteByIdAsync(int id);
        Task<bool> CreateNutritionRouteAsync(NutritionRouteDTO nutritionRouteDto);
        Task UpdateNutritionRouteAsync(NutritionRouteDTO nutritionRouteDto);
        Task DeleteNutritionRouteAsync(int id);
        Task<IEnumerable<NutritionRouteDTO>> GetNutritionRoutesByCreateByIdAndUserIdAsync(int createById, int userId);
        Task<IEnumerable<UserDTO>> GetAllUsersByCreateIdAsync(int createById);


    }
}
