using Microsoft.EntityFrameworkCore;
using SEP490_G87_Vita_Nutrient_System_API.Dtos;
using SEP490_G87_Vita_Nutrient_System_API.Models;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces;

namespace SEP490_G87_Vita_Nutrient_System_API.Repositories.Implementations
{
    public class UserDetailsRepository : IUserDetailsRepository
    {
        private readonly Sep490G87VitaNutrientSystemContext _context = new Sep490G87VitaNutrientSystemContext();

        public UserDetailsRepository()
        {

        }
        public async Task SaveUserDetails(UserPhysicalStatisticsDTO userDetails)
        {
            // Tìm User dựa vào UserId
            var userEntity = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userDetails.UserId);
            if (userEntity != null)
            {
                userEntity.Gender = userDetails.Gender;
                _context.Users.Update(userEntity);
            }

            var userDetailEntity = await _context.UserDetails.FirstOrDefaultAsync(ud => ud.UserId == userDetails.UserId);
            if (userDetailEntity != null)
            {
                userDetailEntity.Height = userDetails.Height;
                userDetailEntity.Weight = userDetails.Weight;
                userDetailEntity.Age = userDetails.Age;
                userDetailEntity.ActivityLevel = userDetails.ActivityLevel;

                double bmr;
                double weight = userDetails.Weight ?? 0;
                double height = userDetails.Height ?? 0;
                double age = userDetails.Age ?? 0;

                if (userDetails.Gender == true)
                {
                    bmr = (10 * weight) + (6.25 * height) - (5 * age) + 5;
                }
                else
                {
                    bmr = (10 * weight) + (6.25 * height) - (5 * age) - 161;
                }
                double tdee;
                switch (userDetails.ActivityLevel)
                {
                    case 1.2:
                        tdee = bmr * 1.2;
                        break;
                    case 1.375:
                        tdee = bmr * 1.375;
                        break;
                    case 1.55:
                        tdee = bmr * 1.55;
                        break;
                    case 1.725:
                        tdee = bmr * 1.725;
                        break;
                    case 1.9:
                        tdee = bmr * 1.9;
                        break;
                    default:
                        tdee = bmr * 1.2;
                        break;
                }
                userDetailEntity.Calo = (int)tdee;

                

                _context.UserDetails.Update(userDetailEntity);
            }

            await _context.SaveChangesAsync();
        }


        public UserDetail GetUserDetail(int userId)
        {
            return _context.UserDetails.SingleOrDefault(u => u.UserId == userId);
        }

        public void UpdateUserDetails(UserDetail userDetail)
        {
            _context.Entry<UserDetail>(userDetail).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public NutritionistDetail GetNutritionistDetail(int nutritionistId)
        {
            return _context.NutritionistDetails.SingleOrDefault(u => u.NutritionistId == nutritionistId);
        }

        public void UpdateNutritionistDetails(NutritionistDetail nutritionistDetail)
        {
            _context.Entry<NutritionistDetail>(nutritionistDetail).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
