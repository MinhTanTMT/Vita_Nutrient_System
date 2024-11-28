using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SEP490_G87_Vita_Nutrient_System_API.Dtos;
using SEP490_G87_Vita_Nutrient_System_API.Models;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces;
using System.Reflection.Metadata.Ecma335;

namespace SEP490_G87_Vita_Nutrient_System_API.Repositories.Implementations
{
    public class FoodRepositories : IFoodRepositories
    {
        private readonly Sep490G87VitaNutrientSystemContext _context = new Sep490G87VitaNutrientSystemContext();
        public FoodRepositories() { }

        public bool IsFoodExisted(int foodId)
        {
            return _context.FoodLists.Find(foodId) is not null;
        }
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
        public List<DietType> GetDietTypes()
        {
            return _context.DietTypes.ToList();
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

        public List<KeyNote> GetKeynotes()
        {
            return _context.KeyNotes.ToList();
        }

        public KeyNote GetKeynote(int id)
        {
            return _context.KeyNotes.Find(id);
        }

        public List<TypeOfCalculation> GetTypesOfCalculation()
        {
            return _context.TypeOfCalculations.ToList();
        }

        public TypeOfCalculation GetTypeOfCalculation(short id)
        {
            return _context.TypeOfCalculations.Find(id);
        }

        public List<FoodSelection> GetFoodSelectionsByUserId(int userId)
        {
            return _context.FoodSelections.Where(fs => fs.UserId == userId).ToList();
        }

        public List<ScaleAmount> GetIngredientByFoodId(int foodId)
        {
            return _context.ScaleAmounts
                .Include(s => s.IngredientDetails)
                .Where(s => s.FoodListId == foodId).ToList();
        }



        public async Task<string> SaveFoodRecipe(SaveFoodRecipeDTO model)
        {
            var food = await _context.FoodLists.FirstOrDefaultAsync(t => t.FoodListId == model.FoodId);
            if (food == null)
            {
                return "Food not found!";
            }
            var recipe = GetFoodRecipe(model.FoodId);

            foreach (var item in model.Recipes)
            {
                var entity = recipe.FirstOrDefault(t => t.RecipeId == item.RecipeId);
                if (entity != null)
                {
                    entity.NumericalOrder = item.NumericalOrder;
                    entity.Describe = item.Describe;
                    entity.Urlimage = item.Urlimage;

                    _context.Update(entity);
                }
                else
                {
                    var add = new Recipe
                    {
                        FoodListId = item.FoodListId,
                        NumericalOrder = (short?)((recipe.Any()
                                        ? recipe.Max(t => t.NumericalOrder) ?? 0
                                        : 0) + 1),
                        Describe = item.Describe,
                        Urlimage = item.Urlimage
                    };

                    await _context.AddAsync(add);
                }
            }

            await _context.SaveChangesAsync();

            return "Save Recipe successfully!";
        }

        public void AddIngredientToFood(int foodId, int ingredientId, double amount)
        {
            ScaleAmount s = new ScaleAmount
            {
                FoodListId = foodId,
                IngredientDetailsId = ingredientId,
                ScaleAmount1 = amount
            };

            _context.ScaleAmounts.Add(s);
            _context.SaveChanges();
        }
        public void RemoveIngredientFromFood(int foodId, int ingredientId)
        {
            ScaleAmount s = _context.ScaleAmounts
                .SingleOrDefault(sa => sa.FoodListId == foodId && sa.IngredientDetailsId == ingredientId);

            if (s is not null)
            {
                _context.ScaleAmounts.Remove(s);
                _context.SaveChanges();
            }
        }
    }
}
