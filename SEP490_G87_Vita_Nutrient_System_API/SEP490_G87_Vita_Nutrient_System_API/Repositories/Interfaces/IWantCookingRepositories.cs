using SEP490_G87_Vita_Nutrient_System_API.Models;

namespace SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces
{
    public interface IWantCookingRepositories
    {
        Task<List<WantCooking>> GetAllWantCookingAsync();
    }
}
