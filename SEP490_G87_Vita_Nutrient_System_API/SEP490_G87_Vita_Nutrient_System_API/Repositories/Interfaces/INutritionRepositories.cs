﻿using SEP490_G87_Vita_Nutrient_System_API.Dtos;
using SEP490_G87_Vita_Nutrient_System_API.Models;

namespace SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces
{
    public interface INutritionRepositories
    {
        Task<NutritionTargetsDaily> CalculateNutritionNeeds(UserDetailsDTO userDetails);
        Task SaveNutritionTargetsAsync(NutritionTargetsDaily nutritionTargets);
        Task<bool> SaveOrUpdateUserDetailsAsync(UserDetailsDTO userDetails);
        Task<UserDetail?> GetUserDetailsAsync(int userId);
        Task<List<User>> GetUsers(string? search, int page = 1, int pageSize = 10);
        Task<bool> UpdateUser(int userId, User updateUser);
        Task<bool> CheckFoodDiseaseRelation(int diseaseId, int foodId);
    }
}