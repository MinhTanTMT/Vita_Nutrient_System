﻿using SEP490_G87_Vita_Nutrient_System_API.Dtos;
using SEP490_G87_Vita_Nutrient_System_API.Models;

namespace SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces
{
    public interface IUserDetailsRepository
    {
        Task SaveUserWeightGoal(UserPhysicalStatisticsDTO userDetails);
        Task SaveUserDetails(UserPhysicalStatisticsDTO userDetails);
        void UpdateUserDetails(UserDetail userDetail);
        UserDetail GetUserDetail(int id);
        NutritionistDetail GetNutritionistDetail(int nutritionistId);
        void UpdateNutritionistDetails(NutritionistDetail nutritionistDetail);
    }
}
