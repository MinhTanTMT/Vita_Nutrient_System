using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SEP490_G87_Vita_Nutrient_System_API.Dtos;
using SEP490_G87_Vita_Nutrient_System_API.Models;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces;
using System;

namespace SEP490_G87_Vita_Nutrient_System_API.Repositories.Implementations
{
    public class NutritionRepositories : INutritionRepositories
    {
        private readonly Sep490G87VitaNutrientSystemContext _context = new Sep490G87VitaNutrientSystemContext();

        public NutritionRepositories()
        {
        }

        public async Task<UserDetail?> GetUserDetailsAsync(int userId)
        {
            return await _context.UserDetails.FirstOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task<bool> SaveOrUpdateUserDetailsAsync(UserDetailsDTO userDetails)
        {
            var existingRecord = await _context.UserDetails
                .FirstOrDefaultAsync(u => u.UserId == userDetails.UserId);

            if (existingRecord != null)
            {
                existingRecord.DescribeYourself = userDetails.DescribeYourself;
                existingRecord.Height = userDetails.Height;
                existingRecord.Weight = userDetails.Weight;
                existingRecord.Age = userDetails.Age;
                existingRecord.WantImprove = userDetails.WantImprove;
                existingRecord.UnderlyingDisease = userDetails.UnderlyingDisease;
                existingRecord.InforConfirmGood = userDetails.InforConfirmGood;
                existingRecord.InforConfirmBad = userDetails.InforConfirmBad;
                existingRecord.IsPremium = userDetails.IsPremium;

                _context.UserDetails.Update(existingRecord);
            }
            else
            {
                var userEntity = new UserDetail
                {
                    UserId = userDetails.UserId,
                    DescribeYourself = userDetails.DescribeYourself,
                    Height = userDetails.Height,
                    Weight = userDetails.Weight,
                    Age = userDetails.Age,
                    WantImprove = userDetails.WantImprove,
                    UnderlyingDisease = userDetails.UnderlyingDisease,
                    InforConfirmGood = userDetails.InforConfirmGood,
                    InforConfirmBad = userDetails.InforConfirmBad,
                    IsPremium = userDetails.IsPremium
                };
                await _context.UserDetails.AddAsync(userEntity);
            }

            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<NutritionTargetsDaily> CalculateNutritionNeeds(UserDetailsDTO userDetails)
        {
            double bmr;
            var user = await _context.Users.FirstOrDefaultAsync(t => t.UserId == userDetails.UserId);
            if (user.Gender == true)
            {
                bmr = (double)(10 * userDetails.Weight + 6.25 * userDetails.Height - 5 * userDetails.Age + 5);
            }
            else 
            {
                bmr = (double)(10 * userDetails.Weight + 6.25 * userDetails.Height - 5 * userDetails.Age - 161);
            }


            double activityFactor;
            switch (userDetails.DescribeYourself)
            {
                case "sedentary": activityFactor = 1.2; break;
                case "light": activityFactor = 1.375; break;
                case "moderate": activityFactor = 1.55; break;
                case "active": activityFactor = 1.725; break;
                case "very active": activityFactor = 1.9; break;
                default: activityFactor = 1.2; break; 
            }

            short calories = (short)(bmr * activityFactor);

            short proteinMin = (short)(1.2 * userDetails.Weight); 
            short proteinMax = (short)(1.8 * userDetails.Weight); 

            short carbsMin = (short)((calories * 0.45) / 4); 
            short carbsMax = (short)((calories * 0.65) / 4);

            short fatsMin = (short)((calories * 0.20) / 9); 
            short fatsMax = (short)((calories * 0.35) / 9); 

            return new NutritionTargetsDaily
            {
                UserId = userDetails.UserId,
                Calories = calories,
                ProteinMin = proteinMin,
                ProteinMax = proteinMax,
                CarbsMin = carbsMin,
                CarbsMax = carbsMax,
                FatsMin = fatsMin,
                FatsMax = fatsMax,
                IsActive = true
            };
        }

        public async Task SaveNutritionTargetsAsync(NutritionTargetsDaily nutritionTargets)
        {
            bool exerciseIntensityExists = await _context.ExerciseIntensities
        .AnyAsync(e => e.Id == nutritionTargets.ExerciseIntensityId);
            if (!exerciseIntensityExists)
            {
                throw new Exception("Invalid ExerciseIntensityId. Please ensure that the Exercise Intensity exists.");
            }
            _context.Add(nutritionTargets);
            await _context.SaveChangesAsync();
        }

        public async Task<List<User>> GetUsers(string? search, int page = 1, int pageSize = 10)
        {
            var query = _context.Users.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(u => u.FirstName.Contains(search) || u.LastName.Contains(search));
            }

            var paginatedUsers = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return paginatedUsers;
        }

        public async Task<bool> UpdateUser(int userId, User updateUser)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null) return false;

            user.FirstName = updateUser.FirstName;
            user.LastName = updateUser.LastName;

            _context.SaveChanges();

            return true;
        }

        public async Task<bool> CheckFoodDiseaseRelation(int diseaseId, int foodId)
        {
            var relation = _context.FoodAndDiseases
                .FirstOrDefault(fd => fd.ListOfDiseasesId == diseaseId && fd.FoodListId == foodId);

            if (relation == null) return false;

            if(relation.IsGoodOrBad == null)
            {
                relation.IsGoodOrBad = false;
            }

            return (bool)relation.IsGoodOrBad;
        }
    }

}
