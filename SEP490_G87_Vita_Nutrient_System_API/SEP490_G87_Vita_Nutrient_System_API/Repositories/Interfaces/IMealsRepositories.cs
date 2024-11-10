using SEP490_G87_Vita_Nutrient_System_API.Dtos;
using SEP490_G87_Vita_Nutrient_System_API.Models;

namespace SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces
{
    public interface IMealsRepositories
    {



        Task<IEnumerable<MealSettingsDto>> GetAllMealSettingsAsync(int userId);
        Task<MealSettingsDto> GetMealSettingByIdAsync(int mealSettingsId);

        Task<MealSettingsDto> AddMealSettingAsync(MealSettingsCreateDto newMealSetting);
        Task<MealSettingsDto> UpdateMealSettingAsync(int id, MealSettingsUpdateDto updatedMealSetting);

        Task DeleteMealSettingAsync(int id);

         ////////////////////////////////////////////////////////////
        /// Chiến
        ////////////////////////////////////////////////////////////
        ///
        Task<MealSettingsDetail> AddMealToListAsync(int mealId);



        Task AddMealSettingsDetailAsync(MealSettingsDetail mealSettingsDetail);
        Task<MealSettingsDetail?> EditMealSettingsDetailAsync(int id, MealSettingsDetailDTO model);
        Task UpdateCalo(int id);
        Task UpdateMealSettingForMealAsync(int id, MealSettingDTO dto);
        Task<MealSettingsDetail> RemoveMealToListAsync(int mealId);
        Task DeleteMealSettingsDetailAsync(int id);
        Task<List<MealSettingsDetail>> GetAllMealAsync();
        Task<MealSetting> GetMealSettingByUserIdAsync(int userId);
        Task<List<CreateMealSettingsDetailDto>> GetAllMealSettingByUserIdAsync(int userId);
        Task<List<CreateMealSettingsDetailDto>> GetAllMealSettingBySelectedAsync(int userId);
        Task<MealSettingsDetail> GetMealSettingDetailByMealSettingIdAsync(int mealSettingsId);
        Task<MealSettingsDetail > FindMealSettingsDetailByIdAsync(int id);
        Task<MealSettingsDetail> FindMealSettingsDetailByNutritionTargetsDailyIdAsync(int nutritionTargetsDailyId);
        Task<bool> ChangeOrderNumberAsync(int mealId, string direction);

    }
}
