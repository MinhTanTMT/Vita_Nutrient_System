using Microsoft.EntityFrameworkCore;
using SEP490_G87_Vita_Nutrient_System_API.Dtos;
using SEP490_G87_Vita_Nutrient_System_API.Models;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces;

namespace SEP490_G87_Vita_Nutrient_System_API.Repositories.Implementations
{
    public class MealsRepositories : IMealRepositories
    {
        private readonly Sep490G87VitaNutrientSystemContext _context = new Sep490G87VitaNutrientSystemContext();

        public MealsRepositories()
        {
        }

        public async Task<IEnumerable<MealSettingsDto>> GetAllMealSettingsAsync(int userId)
        {
            var mealSettings = await _context.MealSettings
                .Where(m => m.UserId == userId)
                .Include(m => m.MealSettingsDetails)
                .ThenInclude(d => d.DayOfTheWeek)
                .Include(m => m.MealSettingsDetails)
                .ThenInclude(d => d.SlotOfTheDay)
                .Include(m => m.MealSettingsDetails)
                .ThenInclude(d => d.CookingDifficulty)
                .Select(m => new MealSettingsDto
                {
                    Id = m.Id,
                    DayOfTheWeekStartId = m.DayOfTheWeekStartId,
                    SameScheduleEveryDay = m.SameScheduleEveryDay,
                    MealDetails = m.MealSettingsDetails.Select(d => new MealSettingsDetailsDto
                    {
                        Id = d.Id,
                        SlotOfTheDayId = d.SlotOfTheDayId,
                        DayOfTheWeekId = d.DayOfTheWeekId,
                        CookingDifficultyId = d.CookingDifficultyId,
                        Size = d.Size,
                        NutritionFocus = d.NutritionFocus,
                        TypeFavoriteFood = d.TypeFavoriteFood
                    }).ToList()
                })
                .ToListAsync();

            return mealSettings;
        }

        public async Task<MealSettingsDto> GetMealSettingByIdAsync(int mealSettingsId)
        {
            var mealSetting = await _context.MealSettings
                .Where(m => m.Id == mealSettingsId)
                .Include(m => m.MealSettingsDetails)
                .Select(m => new MealSettingsDto
                {
                    Id = m.Id,
                    DayOfTheWeekStartId = m.DayOfTheWeekStartId,
                    SameScheduleEveryDay = m.SameScheduleEveryDay,
                    MealDetails = m.MealSettingsDetails.Select(d => new MealSettingsDetailsDto
                    {
                        Id = d.Id,
                        SlotOfTheDayId = d.SlotOfTheDayId,
                        DayOfTheWeekId = d.DayOfTheWeekId,
                        CookingDifficultyId = d.CookingDifficultyId,
                        Size = d.Size,
                        NutritionFocus = d.NutritionFocus,
                        TypeFavoriteFood = d.TypeFavoriteFood
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            return mealSetting ?? throw new KeyNotFoundException("Meal setting not found.");
        }

        public async Task<MealSettingsDto> AddMealSettingAsync(MealSettingsCreateDto newMealSetting)
        {
            var mealSetting = new MealSetting
            {
                UserId = newMealSetting.UserId,
                DayOfTheWeekStartId = newMealSetting.DayOfTheWeekStartId,
                SameScheduleEveryDay = newMealSetting.SameScheduleEveryDay
            };

            _context.MealSettings.Add(mealSetting);
            await _context.SaveChangesAsync();

            foreach (var detail in newMealSetting.MealDetails)
            {
                var mealDetail = new MealSettingsDetail
                {
                    MealSettingsId = mealSetting.Id,
                    SlotOfTheDayId = detail.SlotOfTheDayId,
                    DayOfTheWeekId = detail.DayOfTheWeekId,
                    CookingDifficultyId = detail.CookingDifficultyId,
                    Size = detail.Size,
                    NutritionFocus = detail.NutritionFocus,
                    TypeFavoriteFood = detail.TypeFavoriteFood
                };
                _context.MealSettingsDetails.Add(mealDetail);
            }
            await _context.SaveChangesAsync();

            return await GetMealSettingByIdAsync(mealSetting.Id);
        }

        public async Task<MealSettingsDto> UpdateMealSettingAsync(int id, MealSettingsUpdateDto updatedMealSetting)
        {
            var mealSetting = await _context.MealSettings
                .Include(m => m.MealSettingsDetails)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (mealSetting == null) throw new KeyNotFoundException("Meal setting not found.");

            mealSetting.DayOfTheWeekStartId = updatedMealSetting.DayOfTheWeekStartId;
            mealSetting.SameScheduleEveryDay = updatedMealSetting.SameScheduleEveryDay;

            foreach (var detailDto in updatedMealSetting.MealDetails)
            {
                var detail = mealSetting.MealSettingsDetails.FirstOrDefault(d => d.Id == detailDto.Id);
                if (detail != null)
                {
                    detail.SlotOfTheDayId = detailDto.SlotOfTheDayId;
                    detail.DayOfTheWeekId = detailDto.DayOfTheWeekId;
                    detail.CookingDifficultyId = detailDto.CookingDifficultyId;
                    detail.Size = detailDto.Size;
                    detail.NutritionFocus = detailDto.NutritionFocus;
                    detail.TypeFavoriteFood = detailDto.TypeFavoriteFood;
                }
            }
            await _context.SaveChangesAsync();

            return await GetMealSettingByIdAsync(id);
        }

        public async Task DeleteMealSettingAsync(int id)
        {
            var mealSetting = await _context.MealSettings
                .Include(m => m.MealSettingsDetails)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (mealSetting == null) throw new KeyNotFoundException("Meal setting not found.");

            _context.MealSettingsDetails.RemoveRange(mealSetting.MealSettingsDetails);
            _context.MealSettings.Remove(mealSetting);

            await _context.SaveChangesAsync();
        }
    }
}
