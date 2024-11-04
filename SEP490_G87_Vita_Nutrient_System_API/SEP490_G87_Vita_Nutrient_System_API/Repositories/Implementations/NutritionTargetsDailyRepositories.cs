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
        public async Task<NutritionTargetsDailyDTO> GetNutritionTargetByIdAsync(int id)
        {
            var nutritionTarget = await _context.NutritionTargetsDailies.FirstOrDefaultAsync(nt => nt.Id == id);
            if (nutritionTarget == null)
            {
                return null;
            }

            return new NutritionTargetsDailyDTO
            {
                Id = nutritionTarget.Id,
                UserId = nutritionTarget.UserId,
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
                IsActive = nutritionTarget.IsActive
            };
        }


        public async Task<NutritionTargetsDailyDTO> UpdateNutritionTargetAsync(NutritionTargetsDailyDTO nutritionTargetDTO)
        {
            var existingTarget = await _context.NutritionTargetsDailies.FirstOrDefaultAsync(nt => nt.Id == nutritionTargetDTO.Id);
            if (existingTarget == null)
            {
                return null;
            }

            existingTarget.Calories = nutritionTargetDTO.Calories;
            existingTarget.CarbsMin = nutritionTargetDTO.CarbsMin;
            existingTarget.CarbsMax = nutritionTargetDTO.CarbsMax;
            existingTarget.FatsMin = nutritionTargetDTO.FatsMin;
            existingTarget.FatsMax = nutritionTargetDTO.FatsMax;
            existingTarget.ProteinMin = nutritionTargetDTO.ProteinMin;
            existingTarget.ProteinMax = nutritionTargetDTO.ProteinMax;
            existingTarget.MinimumFiber = nutritionTargetDTO.MinimumFiber;
            existingTarget.LimitDailySodium = nutritionTargetDTO.LimitDailySodium;
            existingTarget.LimitDailyCholesterol = nutritionTargetDTO.LimitDailyCholesterol;
            existingTarget.ExerciseIntensityId = nutritionTargetDTO.ExerciseIntensityId;
            existingTarget.FoodTypeIdWant = nutritionTargetDTO.FoodTypeIdWant;
            existingTarget.AvoidIngredient = nutritionTargetDTO.AvoidIngredient;
            existingTarget.IsActive = nutritionTargetDTO.IsActive;

            _context.NutritionTargetsDailies.Update(existingTarget);
            await _context.SaveChangesAsync();

            return nutritionTargetDTO;
        }




    }
}
