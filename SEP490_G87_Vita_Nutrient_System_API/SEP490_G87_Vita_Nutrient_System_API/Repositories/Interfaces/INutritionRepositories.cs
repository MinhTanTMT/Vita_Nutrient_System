using SEP490_G87_Vita_Nutrient_System_API.Dtos;
using SEP490_G87_Vita_Nutrient_System_API.Models;
using SEP490_G87_Vita_Nutrient_System_API.PageResult;

namespace SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces
{
    public interface INutritionRepositories
    {
        Task<NutritionTargetsDaily> CalculateNutritionNeeds(UserDetailsDTO userDetails);
        Task SaveNutritionTargetsAsync(NutritionTargetsDaily nutritionTargets);
        Task<bool> SaveOrUpdateUserDetailsAsync(UserDetailsDTO userDetails);
        Task<UserDetail?> GetUserDetailsAsync(int userId);
        Task<PagedResult<UserDTO>> GetUsers(int userId, string? search, int page = 1, int pageSize = 10);
        Task<bool> UpdateUser(int userId, string inforConfirmBad, string inforConfirmGood);
        Task<bool> CheckFoodDiseaseRelation(int diseaseId, int foodId);
        Task<int> DeleteUser(int userId);
        Task<UserDTO> GetUserDetail(int userId);
        Task<List<FoodList>> GetFoodLists(string search);
        Task<FoodList> GetFoodList(int id);
        Task<FoodList> CreateFoodList(FoodList foodList);
        Task<int> UpdateFoodList(FoodList foodList);
        Task<int> DeleteFoodList(int id);
        Task<List<ListOfDisease>> GetDiseases(string search);
        Task<ListOfDisease> GetDiseases(int id);
        Task<ListOfDisease> CreateDisease(ListOfDisease disease);
        Task<int> DeleteDisease(int id);
        Task<int> UpdateDisease(ListOfDisease disease);
        Task<FoodAndDisease> GetFoodAndDiseases(int foodId, int diseaseId);
        Task<List<FoodAndDisease>> GetFoodAndDiseases();
        Task<FoodAndDisease> CreateFoodAndDiseases(FoodAndDisease foodAndDisease);
        Task<bool?> UpdateFoodAndDisease(FoodAndDisease foodAndDisease);
        Task<int> DeleteFoodAndDisease(int foodId, int diseaseId);
    }
}
