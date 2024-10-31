using SEP490_G87_Vita_Nutrient_System_API.Dtos;
using SEP490_G87_Vita_Nutrient_System_API.Models;

namespace SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces
{
    public interface IMealsRepositories
    {
        ////////////////////////////////////////////////////////////
        /// Chiến
        ////////////////////////////////////////////////////////////
        ///
        Task<MealSettingsDetail> AddMealToListAsync(int mealId);
        Task AddMealSettingsDetailAsync(MealSettingsDetail mealSettingsDetail);
        Task<MealSettingsDetail?> EditMealSettingsDetailAsync(int id, CreateMealSettingsDetailDto model);

        Task UpdateMealSettingAsync(int id, short? dayOfTheWeekStartId, bool? sameScheduleEveryDay);
        Task<MealSettingsDetail> RemoveMealToListAsync(int mealId);
        Task DeleteMealSettingsDetailAsync(int id);
        Task<List<MealSettingsDetail>> GetAllMealAsync();
        Task<MealSetting> GetMealSettingByUserIdAsync(int userId);
        Task<List<MealSettingsDetail>> GetAllMealSettingByUserIdAsync(int userId);
        Task<List<MealSettingsDetail>> GetAllMealSettingBySelectedAsync(int userId);
        Task<MealSettingsDetail> FindMealSettingsDetailByIdAsync(int id);
        Task<bool> ChangeOrderNumberAsync(int mealId, string direction);

    }
}
