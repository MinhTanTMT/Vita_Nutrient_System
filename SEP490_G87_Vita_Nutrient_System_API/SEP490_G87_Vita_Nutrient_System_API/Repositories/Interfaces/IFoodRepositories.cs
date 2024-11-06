using Microsoft.AspNetCore.Mvc;
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
        void AddIngredient(IngredientDetails100g ingredient);
        void UpdateIngredient(IngredientDetails100g ingredient);
        bool IsIngredientExisted(int id);
        List<KeyNote> GetKeynotes();
        KeyNote GetKeynote(int id);
        List<TypeOfCalculation> GetTypesOfCalculation();
        TypeOfCalculation GetTypeOfCalculation(short id);
        List<FoodSelection> GetFoodSelectionsByUserId(int userId);
    }
}
