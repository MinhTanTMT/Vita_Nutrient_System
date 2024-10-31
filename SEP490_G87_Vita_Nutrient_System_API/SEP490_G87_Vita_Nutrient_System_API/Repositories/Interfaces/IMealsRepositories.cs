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



        Task AddMealSettingsDetailAsync(CreateMealSettingsDetailDto mealSettingsDetail);
        Task<CreateMealSettingsDetailDto?> EditMealSettingsDetailAsync(int id, CreateMealSettingsDetailDto model);

        Task UpdateMealSettingAsync(int id, short? dayOfTheWeekStartId, bool? sameScheduleEveryDay);
        Task<CreateMealSettingsDetailDto> RemoveMealToListAsync(int mealId);
        Task DeleteMealSettingsDetailAsync(int id);
        Task<List<CreateMealSettingsDetailDto>> GetAllMealAsync();
        Task<MealSetting> GetMealSettingByUserIdAsync(int userId);
        Task<List<CreateMealSettingsDetailDto>> GetAllMealSettingByUserIdAsync(int userId);
        Task<List<CreateMealSettingsDetailDto>> GetAllMealSettingBySelectedAsync(int userId);
        Task<CreateMealSettingsDetailDto> FindMealSettingsDetailByIdAsync(int id);
        Task<bool> ChangeOrderNumberAsync(int mealId, string direction);

    }
}
