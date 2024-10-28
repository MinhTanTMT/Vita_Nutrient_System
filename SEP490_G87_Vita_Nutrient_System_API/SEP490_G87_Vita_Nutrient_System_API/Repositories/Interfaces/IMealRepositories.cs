using SEP490_G87_Vita_Nutrient_System_API.Dtos;

namespace SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces
{
    public interface IMealRepositories
    {
        Task<IEnumerable<MealSettingsDto>> GetAllMealSettingsAsync(int userId);
        Task<MealSettingsDto> GetMealSettingByIdAsync(int mealSettingsId);
        Task<MealSettingsDto> AddMealSettingAsync(MealSettingsCreateDto newMealSetting);
        Task<MealSettingsDto> UpdateMealSettingAsync(int id, MealSettingsUpdateDto updatedMealSetting);
        Task DeleteMealSettingAsync(int id);
    }
}
