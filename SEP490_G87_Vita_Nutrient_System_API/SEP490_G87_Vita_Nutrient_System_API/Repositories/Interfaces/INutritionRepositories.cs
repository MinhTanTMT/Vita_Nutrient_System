using SEP490_G87_Vita_Nutrient_System_API.Dtos;
using SEP490_G87_Vita_Nutrient_System_API.Dtos.Disease;
using SEP490_G87_Vita_Nutrient_System_API.Dtos.Food;
using SEP490_G87_Vita_Nutrient_System_API.Dtos.FoodAndDisease;
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
        Task<List<GetFoodListDTO>> GetFoodLists(string search);
        Task<GetFoodListDTO> GetFoodList(int id);
        Task<FoodList> SaveFoodList(SaveFoodDTO model);
        Task<int> DeleteFoodList(int id);
        Task<List<ListOfDisease>> GetDiseases(string search);
        Task<ListOfDisease> GetDiseases(int id);
        Task<ListOfDisease> SaveDisease(SaveDiseaseDTO model);
        Task<ListFoodAndDiseaseDTO> GetFoodAndDiseases(int foodId, int diseaseId);
        Task<List<ListFoodAndDiseaseDTO>> GetFoodAndDiseases();
        Task<FoodAndDisease> SaveFoodAndDiseases(SaveFoodAndDiseaseDTO model);
        Task<int> DeleteFoodAndDisease(int foodId, int diseaseId);
    }
}
