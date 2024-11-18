using Microsoft.EntityFrameworkCore;
using SEP490_G87_Vita_Nutrient_System_API.Dtos;
using SEP490_G87_Vita_Nutrient_System_API.Models;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces;

namespace SEP490_G87_Vita_Nutrient_System_API.Repositories.Implementations
{
    public class NutritionTargetsDailyRepositories : INutritionTargetsDailyRepositories
    {
        private readonly Sep490G87VitaNutrientSystemContext _context = new Sep490G87VitaNutrientSystemContext();

        public NutritionTargetsDailyRepositories()
        {

        }

        ////////////////////////////////////////////////////////////
        /// Chiến
        ////////////////////////////////////////////////////////////
        ///
        public async Task<List<NutritionTargetsDaily>> GetAllNutritionTargetsDailyAsync()
        {
            return await _context.NutritionTargetsDailies.ToListAsync();
        }

        public async Task<NutritionTargetsDailyDTO> CreateNutritionTargetsDailyAsync(int userId, short calories)
        {
            var newNutritionTarget = new NutritionTargetsDaily
            {
                UserId = userId,
                Calories = calories
            };

            _context.NutritionTargetsDailies.Add(newNutritionTarget);
            await _context.SaveChangesAsync();

            var nutritionTargetsDailyDTO = new NutritionTargetsDailyDTO
            {
                Id = newNutritionTarget.Id,
                UserId = newNutritionTarget.UserId,
                Calories = newNutritionTarget.Calories,
                FoodTypeIdWant = 1,
                ExerciseIntensityId = 1,
            };

            return nutritionTargetsDailyDTO;
        }
        public async Task<NutritionTargetOfMealDTO> GetNutritionTargetOfMealByIdAsync(int id)
        {
            var nutritionTarget = await _context.NutritionTargetsDailies.FirstOrDefaultAsync(nt => nt.Id == id);
            var mealSettingDetail =  await _context.MealSettingsDetails.FirstOrDefaultAsync(nt => nt.NutritionTargetsDailyId == id);
            if (nutritionTarget == null)
            {
                return null;
            }
            return new NutritionTargetOfMealDTO
            {
                NutritionTargetsDailyId = mealSettingDetail.NutritionTargetsDailyId,
                Title = nutritionTarget.Title,
                Calories = nutritionTarget.Calories,
                CarbsMin = nutritionTarget.CarbsMin,
                CarbsMax = nutritionTarget.CarbsMax,
                FatsMin = nutritionTarget.FatsMin,
                FatsMax = nutritionTarget.FatsMax,
                ProteinMin = nutritionTarget.ProteinMin,
                ProteinMax = nutritionTarget.ProteinMax,
                MinimumFiber = nutritionTarget.MinimumFiber,
                LimitDailySodium = nutritionTarget.LimitDailySodium,
                LimitDailyCholesterol = nutritionTarget.LimitDailyCholesterol,
                ExerciseIntensityId = nutritionTarget.ExerciseIntensityId,
                FoodTypeIdWant = nutritionTarget.FoodTypeIdWant,
                AvoidIngredient = nutritionTarget.AvoidIngredient,
            };
        }
        public async Task<NutritionTargetsDailyDTO> GetNutritionTargetByIdAsync(int id)
        {
            var nutritionTarget = await _context.NutritionTargetsDailies.FirstOrDefaultAsync(nt => nt.Id == id);           
            if (nutritionTarget == null)
            {
                return null;
            }
            return new NutritionTargetsDailyDTO
            {
                Title = nutritionTarget.Title,
                Calories = nutritionTarget.Calories,
                CarbsMin = nutritionTarget.CarbsMin,
                CarbsMax = nutritionTarget.CarbsMax,
                FatsMin = nutritionTarget.FatsMin,
                FatsMax = nutritionTarget.FatsMax,
                ProteinMin = nutritionTarget.ProteinMin,
                ProteinMax = nutritionTarget.ProteinMax,
                MinimumFiber = nutritionTarget.MinimumFiber,
                LimitDailySodium = nutritionTarget.LimitDailySodium,
                LimitDailyCholesterol = nutritionTarget.LimitDailyCholesterol,
                ExerciseIntensityId = nutritionTarget.ExerciseIntensityId,
                FoodTypeIdWant = nutritionTarget.FoodTypeIdWant,
                AvoidIngredient = nutritionTarget.AvoidIngredient,
            };
        }

        public async Task<NutritionTargetOfMealDTO> UpdateNutritionTargetAsync(NutritionTargetOfMealDTO nutritionTargetOfMealDTO)
        {
            var existingTarget = await _context.NutritionTargetsDailies.FirstOrDefaultAsync(nt => nt.Id == nutritionTargetOfMealDTO.NutritionTargetsDailyId);
            var mealSetting = await _context.MealSettingsDetails.FirstOrDefaultAsync(x => x.NutritionTargetsDailyId == nutritionTargetOfMealDTO.NutritionTargetsDailyId);
            if (existingTarget == null)
            {
                return null;
            }
            existingTarget.Title = nutritionTargetOfMealDTO.Title;
            existingTarget.Calories = nutritionTargetOfMealDTO.Calories;
            existingTarget.CarbsMin = nutritionTargetOfMealDTO.CarbsMin;
            existingTarget.CarbsMax = nutritionTargetOfMealDTO.CarbsMax;
            existingTarget.FatsMin = nutritionTargetOfMealDTO.FatsMin;
            existingTarget.FatsMax = nutritionTargetOfMealDTO.FatsMax;
            existingTarget.ProteinMin = nutritionTargetOfMealDTO.ProteinMin;
            existingTarget.ProteinMax = nutritionTargetOfMealDTO.ProteinMax;
            existingTarget.MinimumFiber = nutritionTargetOfMealDTO.MinimumFiber;
            existingTarget.LimitDailySodium = nutritionTargetOfMealDTO.LimitDailySodium;
            existingTarget.LimitDailyCholesterol = nutritionTargetOfMealDTO.LimitDailyCholesterol;
            existingTarget.ExerciseIntensityId = nutritionTargetOfMealDTO.ExerciseIntensityId;
            existingTarget.FoodTypeIdWant = nutritionTargetOfMealDTO.FoodTypeIdWant;
            existingTarget.AvoidIngredient = nutritionTargetOfMealDTO.AvoidIngredient;
            mealSetting.CarbsMax = nutritionTargetOfMealDTO.ViewBagCarbsMax;
            mealSetting.FatsMax = nutritionTargetOfMealDTO.ViewBagFatsMax;
            mealSetting.ProteinMax = nutritionTargetOfMealDTO.ViewBagProteinMax;
            mealSetting.MinimumFiber = nutritionTargetOfMealDTO.MinimumFiber;
            _context.NutritionTargetsDailies.Update(existingTarget);
            _context.MealSettingsDetails.Update(mealSetting);
            await _context.SaveChangesAsync();

            return nutritionTargetOfMealDTO;
        }




    }
}
