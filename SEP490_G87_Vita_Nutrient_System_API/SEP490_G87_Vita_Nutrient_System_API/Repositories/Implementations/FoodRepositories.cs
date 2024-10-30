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

        public IngredientDetails100g? GetIngredientDetail(int id)
        {
            return _context.IngredientDetails100gs.Find(id);
        }

        public List<IngredientDetails100g> GetIngredientDetails()
        {
            return _context.IngredientDetails100gs.ToList();
        }
        public void DeleteIngredientDetail(int id)
        {
            var scaleAmountRelated = _context.ScaleAmounts.Where(s => s.IngredientDetailsId == id).ToList();
            _context.ScaleAmounts.RemoveRange(scaleAmountRelated);
            IngredientDetails100g ingredient = GetIngredientDetail(id);
            _context.IngredientDetails100gs.Remove(ingredient);
            _context.SaveChanges();
        }

        public void AddIngredient(IngredientDetails100g ingredient)
        {
            _context.IngredientDetails100gs.Add(ingredient);
            _context.SaveChanges();
        }

        public void UpdateIngredient(IngredientDetails100g ingredient)
        {
            _context.Entry<IngredientDetails100g>(ingredient).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public bool IsIngredientExisted(int id)
        {
            var ingradient = _context.IngredientDetails100gs.Find(id);

            if(ingradient != null)
            {
                _context.Entry<IngredientDetails100g>(ingradient).State = EntityState.Detached;
                return true;
            }

            return false;
        }
    }
}
