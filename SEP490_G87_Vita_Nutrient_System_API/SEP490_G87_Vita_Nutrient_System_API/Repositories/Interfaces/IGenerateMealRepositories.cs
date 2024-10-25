using SEP490_G87_Vita_Nutrient_System_API.Dtos;
using SEP490_G87_Vita_Nutrient_System_API.Models;

namespace SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces
{
    public interface IGenerateMealRepositories
    {
        Task<bool> CheckOfFilterTheTypeDiseaseBlockListAvoidIngredient(FoodList FoodSystemInput, NutritionTargetsDaily nutritionTargetsDaily, int idUser);

        Task<IEnumerable<int>> FilterTheTypeDiseaseBlockListAvoidIngredient(NutritionTargetsDaily nutritionTargetsDaily, int idUser);

        Task<IEnumerable<FoodListDTO>> GetTheListOfDishesByMealSettingsDetails(int MealSettingsDetailsId);

        Task<int> GetRandomFoodId(IEnumerable<int> idFoodListSystem, List<int> selectedIds);

        Task<bool> CheckForUserMealSettingsDetailsIsSmallerThanNeeded(FoodListDTO dataFood, int MealSettingsDetailsId);

        Task<bool> CheckForUserMealSettingsDetails(FoodListDTO dataFood, int MealSettingsDetailsId);

        Task<string[]> SplitAndProcessFirst(string input);

        Task<Dictionary<string, string>> SplitAndProcess1(string part);

        Task<Dictionary<string, int[]>> SplitAndProcess2(string part);

        Task<int[]> SplitAndProcess3(string part);

        bool IsNumeric(string str);

        Task<FoodListDTO> TotalAllTheIngredientsOfTheDish(IEnumerable<FoodListDTO> dataFood);

        Task<IEnumerable<FoodListDTO>> TakeAllTheIngredientsOfTheDish(int idFoodListId);

    }
}
