using SEP490_G87_Vita_Nutrient_System_API.Dtos;
using SEP490_G87_Vita_Nutrient_System_API.Models;

namespace SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces
{
    public interface INutritionTargetsDailyRepositories
    {
        ////////////////////////////////////////////////////////////
        /// Chiến
        ////////////////////////////////////////////////////////////
        ///
        Task<List<NutritionTargetsDaily>> GetAllNutritionTargetsDailyAsync();
        Task<NutritionTargetsDailyDTO> CreateNutritionTargetsDailyAsync(int userId, short calories);
        Task<NutritionTargetOfMealDTO> GetNutritionTargetOfMealByIdAsync(int id);
        Task<NutritionTargetsDailyDTO> GetNutritionTargetByIdAsync(int id);
        Task<NutritionTargetOfMealDTO> UpdateNutritionTargetAsync(NutritionTargetOfMealDTO nutritionTargetDTO);
    }
}
