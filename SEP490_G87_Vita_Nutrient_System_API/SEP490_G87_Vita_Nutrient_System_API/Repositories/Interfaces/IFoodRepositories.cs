﻿using Microsoft.AspNetCore.Mvc;
using SEP490_G87_Vita_Nutrient_System_API.Dtos;
using SEP490_G87_Vita_Nutrient_System_API.Models;

namespace SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces
{
    public interface IFoodRepositories
    {
        bool IsFoodExisted(int foodId);
        List<FoodList> GetFoods();
        List<FoodList> GetFoodsByType(int typeId);
        FoodList? GetFood(int foodId);
        List<Recipe> GetFoodRecipe(int foodId);
        List<FoodType> GetFoodTypes();
        List<DietType> GetDietTypes();
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
        List<ScaleAmount> GetIngredientByFoodId(int foodId);
        Task<string> SaveFoodRecipe(SaveFoodRecipeDTO model);
        void AddIngredientToFood(int foodId, int ingredientId, double amount);
        void RemoveIngredientFromFood(int foodId, int ingredientId);

    }
}
