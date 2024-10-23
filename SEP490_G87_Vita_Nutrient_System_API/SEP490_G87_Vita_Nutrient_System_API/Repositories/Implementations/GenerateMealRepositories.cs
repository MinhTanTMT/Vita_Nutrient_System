using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using NuGet.Configuration;
using SEP490_G87_Vita_Nutrient_System_API.Dtos;
using SEP490_G87_Vita_Nutrient_System_API.Models;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SEP490_G87_Vita_Nutrient_System_API.Repositories.Implementations
{
    public class GenerateMealRepositories
    {

        public GenerateMealRepositories() { }

        Sep490G87VitaNutrientSystemContext _context = new Sep490G87VitaNutrientSystemContext();

        private static Random random = new Random();
        public async Task<int> GetRandomFoodId(List<int> idFoodListSystem, List<int> selectedIds)
        {
            // Tạo một danh sách các phần tử chưa được chọn
            List<int> availableIds = new List<int>(idFoodListSystem);
            availableIds.RemoveAll(id => selectedIds.Contains(id)); // Xóa các id đã chọn

            if (availableIds.Count == 0)
            {
                throw new InvalidOperationException("Không còn id nào để chọn.");
            }

            int randomIndex = random.Next(availableIds.Count);
            return availableIds[randomIndex];
        }

        public async Task<dynamic> GetTheListOfDishesByMealSettingsDetails(int MealSettingsDetailsId)
        {

            //loc luon kieu thuc an nay 
            //loc luon check benh
            // loc luon block mon an
            List<int> idFoodListSystem = await _context.FoodLists.Select(x => x.FoodListId).ToListAsync();



            MealSettingsDetail mealSettingsDetail = _context.MealSettingsDetails.Find(MealSettingsDetailsId);
            List<FoodListDTO> collectionOfDishes = new List<FoodListDTO>();

            bool foragingLoop = true;
            int loopCount = 0;
            while (foragingLoop)
            {
                List<int> selectedIds = new List<int>();
                loopCount++;
                foreach (int idGet in idFoodListSystem)
                {
                    int randomId = await GetRandomFoodId(idFoodListSystem, selectedIds);
                    selectedIds.Add(randomId);
                    if (collectionOfDishes.Count() == 0)
                    {
                        FoodListDTO foodObtained = await TotalAllTheIngredientsOfTheDish(await TakeAllTheIngredientsOfTheDish(randomId));
                        if (await CheckForUserMealSettingsDetails(foodObtained, MealSettingsDetailsId))
                        {
                            File.WriteAllText(@"C:\Users\msi\Desktop\SEP490_G87\Referent\DaChayDenDay.txt", DateTime.Now + "");
                            collectionOfDishes.Add(foodObtained);
                            break;
                        }
                    }
                    else
                    {
                        if (await CheckForUserMealSettingsDetails(await TotalAllTheIngredientsOfTheDish(collectionOfDishes), MealSettingsDetailsId))
                        {
                            FoodListDTO foodObtained = await TotalAllTheIngredientsOfTheDish(await TakeAllTheIngredientsOfTheDish(randomId));
                            if (await CheckForUserMealSettingsDetails(foodObtained, MealSettingsDetailsId))
                            {
                                collectionOfDishes.Add(foodObtained);
                                break;
                            }
                        }
                    }
                }
                if(collectionOfDishes.Count() == (mealSettingsDetail.NumberOfDishes ?? 1))
                {
                    foragingLoop = false;
                }
                if (loopCount == (mealSettingsDetail.NumberOfDishes ?? 1))
                {
                    foragingLoop = false;
                }
            }


            




            return collectionOfDishes;
        }

        public async Task<bool> CheckForUserMealSettingsDetails(FoodListDTO dataFood, int MealSettingsDetailsId)
        {
            MealSettingsDetail mealSettingsDetail = _context.MealSettingsDetails.Find(MealSettingsDetailsId);

            string[] result = await SplitAndProcessFirst(dataFood.KeyNote.KeyList);

            
            foreach (var item in result)
            {

                if (item.Contains("="))
                {
                    Dictionary<string, string> dataProcess1 = await SplitAndProcess1(item);

                    if (IsNumeric(dataProcess1.Values.FirstOrDefault().ToString()))
                    {
                        if (dataProcess1.Keys.FirstOrDefault().Equals("WantCooking"))
                        {
                            if (!dataProcess1.Values.FirstOrDefault().Equals(mealSettingsDetail.WantCookingId.ToString())) return false;
                        }
                    }
                    else
                    {
                        if (dataProcess1.Keys.FirstOrDefault().Equals("Size"))
                        {
                            if (!dataProcess1.Values.FirstOrDefault().Equals(mealSettingsDetail.Size)) return false;
                        }
                    }
                }
                if (item.Contains(":"))
                {
                    Dictionary<string, int[]> dataProcess2 = await SplitAndProcess2(item);
                    if (dataProcess2.Keys.FirstOrDefault().Equals("SlotOfTheDay"))
                    {
                        if (!dataProcess2.Values.FirstOrDefault().Contains(Int32.Parse(mealSettingsDetail.TypeFavoriteFood))) return false;
                    }
                }
            }

            if (mealSettingsDetail.CookingDifficultyId != dataFood.CookingDifficultyId) return false;
            if (mealSettingsDetail.TimeAvailable < (dataFood.PreparationTime + dataFood.CookingTime)) return false;

            double calorieTolerance;
            double carbTolerance;
            double fatTolerance;
            double proteinTolerance;
            double fiberTolerance;
            double sodiumTolerance;
            double cholesterolTolerance;

            if (mealSettingsDetail.NutritionFocus ?? true)
            {
                calorieTolerance = 0.1;
                carbTolerance = 0.0;
                fatTolerance = 0.0;
                proteinTolerance = 0.0;
                fiberTolerance = 0.0;
                sodiumTolerance = 0.0;
                cholesterolTolerance = 0.0;
            }
            else
            {
                calorieTolerance = 0.1; // 10%
                carbTolerance = 0.15; // 15%
                fatTolerance = 0.1; // 10%
                proteinTolerance = 0.1; // 10%
                fiberTolerance = 0.15; // 15%
                sodiumTolerance = 0.1; // 10%
                cholesterolTolerance = 0.1; // 10%
            }

            NutritionTargetsDaily nutritionTargetsDaily = await _context.NutritionTargetsDailies.FindAsync(mealSettingsDetail.NutritionTargetsDailyId);
            if (dataFood.ingredientDetails100gReduceDTO.Energy < nutritionTargetsDaily.Calories * (1 - calorieTolerance) || dataFood.ingredientDetails100gReduceDTO.Energy > nutritionTargetsDaily.Calories * (1 + calorieTolerance)) return false;
            if (dataFood.ingredientDetails100gReduceDTO.Carbohydrate < nutritionTargetsDaily.CarbsMin * (1 - carbTolerance) || dataFood.ingredientDetails100gReduceDTO.Carbohydrate > nutritionTargetsDaily.CarbsMax * (1 + carbTolerance)) return false;
            if (dataFood.ingredientDetails100gReduceDTO.Fat < nutritionTargetsDaily.FatsMin * (1 - fatTolerance) || dataFood.ingredientDetails100gReduceDTO.Fat > nutritionTargetsDaily.FatsMax * (1 + fatTolerance)) return false;
            if (dataFood.ingredientDetails100gReduceDTO.Protein < nutritionTargetsDaily.ProteinMin * (1 - proteinTolerance) || dataFood.ingredientDetails100gReduceDTO.Protein > nutritionTargetsDaily.ProteinMax * (1 + proteinTolerance)) return false;
            if (dataFood.ingredientDetails100gReduceDTO.Fiber > nutritionTargetsDaily.MinimumFiber * (1 + fiberTolerance)) return false;

            double targetSodium = 2300;
            double targetCholesterol = 300;
            if (nutritionTargetsDaily.LimitDailySodium ?? false) { }
            else
            {
                if (dataFood.ingredientDetails100gReduceDTO.Sodium > targetSodium * (1 + sodiumTolerance)) return false;
            }
            if (nutritionTargetsDaily.LimitDailyCholesterol ?? false) { }
            else
            {  
                if (dataFood.ingredientDetails100gReduceDTO.Cholesterol > targetCholesterol * (1 + cholesterolTolerance)) return false;
            }

            
            return true;
        }

        public async Task<string[]> SplitAndProcessFirst(string input)
        {
            string[] splitByHash = input.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries);

            return splitByHash;
        }

        public async Task<Dictionary<string, string>> SplitAndProcess1(string part)
        {
            var result = new Dictionary<string, string>();
            string[] splitByEqual = part.Split('=');
            if (splitByEqual.Length == 2)
            {
                result[splitByEqual[0]] = splitByEqual[1];
            }
            return result;
        }

        public async Task<Dictionary<string, int[]>> SplitAndProcess2(string part)
        {
            var result = new Dictionary<string, int[]>();
            string[] splitByEqual = part.Split(':');
            if (splitByEqual.Length == 2)
            {
                int[] ints = splitByEqual[1].Split(';').Select(s => { int number; return int.TryParse(s, out number) ? number : 0; }).ToArray();
                result[splitByEqual[0]] = ints;
            }
            return result;
        }

        private bool IsValidSize(string value, string size)
        {
            if (int.TryParse(value, out int numericValue))
            {
                return numericValue.ToString() == size;
            }
            return value.Equals(size, StringComparison.OrdinalIgnoreCase);
        }


        public bool IsNumeric(string str)
        {
            return int.TryParse(str, out _);
        }


        public async Task<FoodListDTO> TotalAllTheIngredientsOfTheDish(IEnumerable<FoodListDTO> dataFood)
        {
            FoodListDTO totalfoodListDTO = new FoodListDTO()
            {
                FoodListId = dataFood.First().FoodListId,
                Name = dataFood.First().Name,
                Describe = dataFood.First().Describe,
                Rate = dataFood.First().Rate,
                NumberRate = dataFood.First().NumberRate,
                Urlimage = dataFood.First().Urlimage,
                FoodTypeId = dataFood.First().FoodTypeId,
                KeyNoteId = dataFood.First().KeyNoteId,
                IsActive = dataFood.First().IsActive,
                PreparationTime = dataFood.First().PreparationTime,
                CookingTime = dataFood.First().CookingTime,
                CookingDifficultyId = dataFood.First().CookingDifficultyId,
                ingredientDetails100gReduceDTO = new IngredientDetails100gReduceDTO()
                {
                    Id = -1,
                    KeyNoteId = -1,
                    Name = "SummaryOfTheEntireList",
                    Describe = "SummaryOfTheEntireList",
                    Urlimage = "SummaryOfTheEntireList",
                    TypeOfCalculationId = -1,
                    Energy = dataFood.Sum(x => x.ingredientDetails100gReduceDTO.Energy),
                    Protein = dataFood.Sum(x => x.ingredientDetails100gReduceDTO.Protein),
                    Fat = dataFood.Sum(x => x.ingredientDetails100gReduceDTO.Fat),
                    Carbohydrate = dataFood.Sum(x => x.ingredientDetails100gReduceDTO.Carbohydrate),
                    Fiber = dataFood.Sum(x => x.ingredientDetails100gReduceDTO.Fiber),
                    Sodium = dataFood.Sum(x => x.ingredientDetails100gReduceDTO.Sodium),
                    Cholesterol = dataFood.Sum(x => x.ingredientDetails100gReduceDTO.Cholesterol)
                },
                KeyNote = new KeyNoteDTO
                {
                    Id = dataFood.First().KeyNote.Id,
                    KeyList = dataFood.First().KeyNote.KeyList
                },
                ScaleAmounts = new ScaleAmountDTO
                {
                    FoodListId = dataFood.First().FoodListId,
                    IngredientDetailsId = -1,
                    ScaleAmount1 = -1
                }
            };
            return totalfoodListDTO;
        }

        public async Task<IEnumerable<FoodListDTO>> TakeAllTheIngredientsOfTheDish(int idFoodListId)
        {
            IEnumerable<FoodListDTO> dataFood = (from scaleAmounts in _context.ScaleAmounts
                                                 join foodLists in _context.FoodLists
            on scaleAmounts.FoodListId equals foodLists.FoodListId
                                                 join ingredientDetails100gs in _context.IngredientDetails100gs
                                                 on scaleAmounts.IngredientDetailsId equals ingredientDetails100gs.Id
                                                 join keyNotes in _context.KeyNotes
                                                 on foodLists.KeyNoteId equals keyNotes.Id
                                                 where scaleAmounts.FoodListId == idFoodListId
                                                 select new FoodListDTO
                                                 {
                                                     FoodListId = foodLists.FoodListId,
                                                     Name = foodLists.Name,
                                                     Describe = foodLists.Describe,
                                                     Rate = foodLists.Rate,
                                                     NumberRate = foodLists.NumberRate,
                                                     Urlimage = foodLists.Urlimage,
                                                     FoodTypeId = foodLists.FoodTypeId,
                                                     KeyNoteId = foodLists.KeyNoteId,
                                                     KeyNote = new KeyNoteDTO
                                                     {
                                                         Id = keyNotes.Id,
                                                         KeyList = keyNotes.KeyList
                                                     },
                                                     IsActive = foodLists.IsActive,
                                                     PreparationTime = foodLists.PreparationTime,
                                                     CookingTime = foodLists.CookingTime,
                                                     CookingDifficultyId = foodLists.CookingDifficultyId,
                                                     ingredientDetails100gReduceDTO = new IngredientDetails100gReduceDTO
                                                     {
                                                         Id = ingredientDetails100gs.Id,
                                                         KeyNoteId = ingredientDetails100gs.KeyNoteId,
                                                         Name = ingredientDetails100gs.Name,
                                                         Describe = ingredientDetails100gs.Describe,
                                                         Urlimage = ingredientDetails100gs.Urlimage,
                                                         TypeOfCalculationId = ingredientDetails100gs.TypeOfCalculationId,
                                                         Energy = ingredientDetails100gs.Energy / 100.0 * scaleAmounts.ScaleAmount1,
                                                         Protein = ingredientDetails100gs.Protein / 100.0 * scaleAmounts.ScaleAmount1,
                                                         Fat = ingredientDetails100gs.Fat / 100.0 * scaleAmounts.ScaleAmount1,
                                                         Carbohydrate = ingredientDetails100gs.Carbohydrate / 100.0 * scaleAmounts.ScaleAmount1,
                                                         Fiber = ingredientDetails100gs.Fiber / 100.0 * scaleAmounts.ScaleAmount1,
                                                         Sodium = ingredientDetails100gs.Sodium / 100.0 * scaleAmounts.ScaleAmount1,
                                                         Cholesterol = ingredientDetails100gs.Cholesterol / 100.0 * scaleAmounts.ScaleAmount1
                                                     },
                                                     ScaleAmounts = new ScaleAmountDTO
                                                     {
                                                         FoodListId = scaleAmounts.FoodListId,
                                                         IngredientDetailsId = scaleAmounts.IngredientDetailsId,
                                                         ScaleAmount1 = scaleAmounts.ScaleAmount1
                                                     }
                                                 }).ToList();
            return dataFood;

        }
    }
}
