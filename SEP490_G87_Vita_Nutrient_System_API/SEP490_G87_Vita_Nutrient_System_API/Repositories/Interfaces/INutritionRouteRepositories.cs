using System.Collections.Generic;
using System.Threading.Tasks;
using SEP490_G87_Vita_Nutrient_System_API.Dtos;

namespace SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces
{
    public interface INutritionRouteRepositories
    {
        Task<IEnumerable<NutritionRouteDTO>> GetAllNutritionRoutesByCreateByIdAsync(int userId);
        Task<NutritionRouteDTO> GetNutritionRouteByIdAsync(int id);
        Task CreateNutritionRouteAsync(NutritionRouteDTO nutritionRouteDto);
        Task UpdateNutritionRouteAsync(NutritionRouteDTO nutritionRouteDto);
        Task DeleteNutritionRouteAsync(int id);
    }
}
