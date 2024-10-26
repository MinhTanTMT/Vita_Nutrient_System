using Microsoft.EntityFrameworkCore;
using SEP490_G87_Vita_Nutrient_System_API.Models;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces;

namespace SEP490_G87_Vita_Nutrient_System_API.Repositories.Implementations
{
    public class FoodRepositories : IFoodRepositories
    {
        private readonly Sep490G87VitaNutrientSystemContext _context = new Sep490G87VitaNutrientSystemContext();
        public FoodRepositories() { }
        public FoodList? GetFood(int foodId)
        {
            return _context.FoodLists.Include(food => food.FoodType)
                .FirstOrDefault(f=>f.FoodListId == foodId);
        }

        public List<Recipe> GetFoodRecipe(int foodId)
        {
            return _context.Recipes.Where(r => r.FoodListId == foodId)
                .OrderBy(r => r.NumericalOrder)
                .ToList();
        }

        public List<FoodList> GetFoods()
        {
            return _context.FoodLists.Include(food => food.FoodType).ToList();
        }

        public List<FoodList> GetFoodsByType(int typeId)
        {
            return _context.FoodLists.Include(food => food.FoodType)
                .Where(f => f.FoodTypeId == typeId)
                .ToList();
        }

        public List<FoodType> GetFoodTypes()
        {
            return _context.FoodTypes.ToList();
        }
    }
}
