using Humanizer;
using Microsoft.EntityFrameworkCore;
using SEP490_G87_Vita_Nutrient_System_API.Dtos;
using SEP490_G87_Vita_Nutrient_System_API.Models;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces;

namespace SEP490_G87_Vita_Nutrient_System_API.Repositories.Implementations
{
    public class MealsRepositories : IMealsRepositories
    {

        private readonly SEP490_G87_VitaNutrientSystemContext _context = new SEP490_G87_VitaNutrientSystemContext();

        public MealsRepositories()
        {

        }

        ////////////////////////////////////////////////////////////
        /// Chiến
        ////////////////////////////////////////////////////////////
        ///

        ///// Create 
        public async Task<MealSettingsDetail> AddMealToListAsync(int mealId)
        {
            var mealSettingsDetail = await FindMealSettingsDetailByIdAsync(mealId);
            if (mealSettingsDetail == null)
            {
                return null;
            }

            mealSettingsDetail.IsActive = true;
            var existingMeals = await GetAllMealSettingBySelectedAsync(mealSettingsDetail.MealSettingsId);

            bool isMealAlreadyAdded = existingMeals.Any(m => m.Id == mealSettingsDetail.Id && (m.IsActive ?? false));
            if (isMealAlreadyAdded)
            {
                throw new InvalidOperationException("Bữa ăn đã có trong danh sách.");
            }

            var maxOrderNumber = existingMeals
                .Where(m => m.SlotOfTheDayId == mealSettingsDetail.SlotOfTheDayId && (m.IsActive ?? false) && m.DayOfTheWeekId == mealSettingsDetail.DayOfTheWeekId)
                .Max(m => (int?)m.OrderNumber) ?? 0;

            mealSettingsDetail.OrderNumber = (short?)(maxOrderNumber + 1);

            _context.MealSettingsDetails.Update(mealSettingsDetail);
            await _context.SaveChangesAsync();

            return mealSettingsDetail;
        }


        public async Task AddMealSettingsDetailAsync(MealSettingsDetail mealSettingsDetail)
        {
            await _context.MealSettingsDetails.AddAsync(mealSettingsDetail);
            await _context.SaveChangesAsync();
        }

        ///// Update
       public async Task<MealSettingsDetail?> EditMealSettingsDetailAsync(int id, CreateMealSettingsDetailDto model)
{
    var mealSettingsDetail = await FindMealSettingsDetailByIdAsync(id);
    if (mealSettingsDetail == null) return null;

    if (model.SlotOfTheDayId != mealSettingsDetail.SlotOfTheDayId || model.IsActive != mealSettingsDetail.IsActive)
    {
        if (model.IsActive == true)
        {
            var existingMeals = await GetAllMealSettingBySelectedAsync(model.MealSettingsId);

            var maxOrderNumber = existingMeals
                .Where(m => m.SlotOfTheDayId == model.SlotOfTheDayId && m.IsActive.GetValueOrDefault())
                .Max(m => (int?)m.OrderNumber) ?? 0;

            model.OrderNumber = (short)(maxOrderNumber + 1);
        }
        else
        {
            model.OrderNumber = null;
        }
    }

    mealSettingsDetail.MealSettingsId = model.MealSettingsId;
    mealSettingsDetail.SlotOfTheDayId = model.SlotOfTheDayId;
    mealSettingsDetail.NutritionTargetsDailyId = model.NutritionTargetsDailyId;
    mealSettingsDetail.DayOfTheWeekId = model.DayOfTheWeekId;
    mealSettingsDetail.SkipCreationProcess = model.SkipCreationProcess;
    mealSettingsDetail.Size = model.Size;
    mealSettingsDetail.NutritionFocus = model.NutritionFocus;
    mealSettingsDetail.NumberOfDishes = model.NumberOfDishes;
    mealSettingsDetail.TypeFavoriteFood = model.TypeFavoriteFood;
    mealSettingsDetail.WantCookingId = model.WantCookingId;
    mealSettingsDetail.TimeAvailable = model.TimeAvailable;
    mealSettingsDetail.CookingDifficultyId = model.CookingDifficultyId;
    mealSettingsDetail.IsActive = model.IsActive;
    mealSettingsDetail.OrderNumber = model.OrderNumber;
    mealSettingsDetail.Name = model.Name;

    _context.MealSettingsDetails.Update(mealSettingsDetail);
    await _context.SaveChangesAsync();

    return mealSettingsDetail;
}


        public async Task UpdateMealSettingAsync(int userId, short? dayOfTheWeekStartId, bool? sameScheduleEveryDay)
        {
            var mealSetting = await _context.MealSettings.FirstOrDefaultAsync(ms => ms.UserId == userId);
            if (mealSetting != null)
            {
                mealSetting.DayOfTheWeekStartId = dayOfTheWeekStartId;
                mealSetting.SameScheduleEveryDay = sameScheduleEveryDay;
                _context.Entry(mealSetting).Property(ms => ms.DayOfTheWeekStartId).IsModified = true;
                _context.Entry(mealSetting).Property(ms => ms.SameScheduleEveryDay).IsModified = true;

                await _context.SaveChangesAsync();
            }
        }

        ///// Find 

        public async Task<MealSettingsDetail> FindMealSettingsDetailByIdAsync(int id)
        {
            return await _context.MealSettingsDetails.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<MealSetting> GetMealSettingByUserIdAsync(int userId)
        {
            return await _context.MealSettings.FirstOrDefaultAsync(ms => ms.UserId == userId);
        }

        public async Task<List<MealSettingsDetail>> GetAllMealSettingByUserIdAsync(int userId)
        {
            var mealSetting = await _context.MealSettings.FirstOrDefaultAsync(ms => ms.UserId == userId);
            if (mealSetting != null)
            {
                return await _context.MealSettingsDetails
                                     .Where(msd => msd.MealSettingsId == mealSetting.Id)
                                     .ToListAsync();
            }
            return new List<MealSettingsDetail>();
        }

        public async Task<List<MealSettingsDetail>> GetAllMealSettingBySelectedAsync(int userId)
        {
            var mealSetting = await _context.MealSettings.FirstOrDefaultAsync(ms => ms.UserId == userId);
            if (mealSetting != null)
            {
                return await _context.MealSettingsDetails
                                     .Where(msd => msd.MealSettingsId == mealSetting.Id && msd.IsActive == true )
                                     .ToListAsync();
            }
            return new List<MealSettingsDetail>();
        }
        public async Task<List<MealSettingsDetail>> GetAllMealAsync()
        {
            return await _context.MealSettingsDetails.ToListAsync();
        }

        ///// Delete
        public async Task DeleteMealSettingsDetailAsync(int id)
        {
            var mealSettingsDetail = await _context.MealSettingsDetails.FindAsync(id);
            if (mealSettingsDetail != null)
            {
                _context.MealSettingsDetails.Remove(mealSettingsDetail);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<MealSettingsDetail> RemoveMealToListAsync(int mealId)
        {
            var mealSettingsDetail = await FindMealSettingsDetailByIdAsync(mealId);
            if (mealSettingsDetail == null)
            {
                return null;
            }

            var removedOrderNumber = mealSettingsDetail.OrderNumber;
            mealSettingsDetail.IsActive = false;
            mealSettingsDetail.OrderNumber = null;

            _context.MealSettingsDetails.Update(mealSettingsDetail);
            await _context.SaveChangesAsync();

            var existingMeals = await GetAllMealSettingBySelectedAsync(mealSettingsDetail.MealSettingsId);
            var mealsToUpdate = existingMeals
                .Where(m => m.SlotOfTheDayId == mealSettingsDetail.SlotOfTheDayId
                            && (m.IsActive ?? false)
                            && m.DayOfTheWeekId == mealSettingsDetail.DayOfTheWeekId
                            && m.OrderNumber > removedOrderNumber)
                .OrderBy(m => m.OrderNumber)
                .ToList();

            foreach (var meal in mealsToUpdate)
            {
                meal.OrderNumber = (short?)(meal.OrderNumber - 1);
                _context.MealSettingsDetails.Update(meal);
            }

            await _context.SaveChangesAsync();

            return mealSettingsDetail;
        }

        //// Change
        public async Task<bool> ChangeOrderNumberAsync(int mealId, string direction)
        {

            var mealSettingsDetail = await FindMealSettingsDetailByIdAsync(mealId);
            if (mealSettingsDetail == null) return false;


            var existingMeals = await GetAllMealSettingBySelectedAsync(mealSettingsDetail.MealSettingsId);
            var orderedMeals = existingMeals
                .Where(m => m.SlotOfTheDayId == mealSettingsDetail.SlotOfTheDayId
                            && m.IsActive.GetValueOrDefault()
                            && m.DayOfTheWeekId == mealSettingsDetail.DayOfTheWeekId)
                .OrderBy(m => m.OrderNumber)
                .ToList();

            int currentIndex = orderedMeals.FindIndex(m => m.Id == mealId);
            if (currentIndex == -1) return false;

            int targetIndex = direction == "up" ? currentIndex - 1 : currentIndex + 1;

            if (targetIndex >= 0 && targetIndex < orderedMeals.Count)
            {

                var targetMeal = orderedMeals[targetIndex];
                short? tempOrder = mealSettingsDetail.OrderNumber;
                mealSettingsDetail.OrderNumber = targetMeal.OrderNumber;
                targetMeal.OrderNumber = tempOrder;

                _context.MealSettingsDetails.Update(mealSettingsDetail);
                _context.MealSettingsDetails.Update(targetMeal);
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }


    }
}
