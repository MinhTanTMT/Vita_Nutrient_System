using SEP490_G87_Vita_Nutrient_System_API.Dtos;

namespace SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces
{
    public interface IUserDetailsRepository
    {
        Task SaveUserDetails(UserPhysicalStatisticsDTO userDetails);

    }
}
