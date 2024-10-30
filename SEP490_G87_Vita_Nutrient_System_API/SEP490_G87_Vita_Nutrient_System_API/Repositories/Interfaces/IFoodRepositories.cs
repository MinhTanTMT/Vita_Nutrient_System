using SEP490_G87_Vita_Nutrient_System_API.Models;

namespace SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces
{
    public interface IFoodRepositories
    {
        List<FoodList> GetFoods();
        List<FoodList> GetFoodsByType(int typeId);
        FoodList? GetFood(int foodId);
        List<Recipe> GetFoodRecipe(int foodId);
        List<FoodType> GetFoodTypes();
        List<IngredientDetails100g> GetIngredientDetails();
        IngredientDetails100g? GetIngredientDetail(int id);
        void DeleteIngredientDetail(int id);
    }
}
