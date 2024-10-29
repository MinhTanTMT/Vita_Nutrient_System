using Microsoft.EntityFrameworkCore;
using SEP490_G87_Vita_Nutrient_System_API.Dtos;
using SEP490_G87_Vita_Nutrient_System_API.Models;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces;

namespace SEP490_G87_Vita_Nutrient_System_API.Repositories.Implementations
{
    public class UserDetailsRepository : IUserDetailsRepository
    {
        private readonly Sep490G87VitaNutrientSystemContext _context;

        public UserDetailsRepository(Sep490G87VitaNutrientSystemContext context)
        {
            _context = context;
        }

        public async Task SaveUserDetails(UserDetailsDTO userDetails)
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

            _context.UserDetails.Add(userEntity);
            await _context.SaveChangesAsync();
        }
    }
}
