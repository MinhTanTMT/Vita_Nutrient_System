using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using NuGet.Configuration;
using SEP490_G87_Vita_Nutrient_System_API.Dtos;
using SEP490_G87_Vita_Nutrient_System_API.Models;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SEP490_G87_Vita_Nutrient_System_API.Repositories.Implementations
{
    public class GenerateMealRepositories : IGenerateMealRepositories
    {
        public GenerateMealRepositories() { }

        Sep490G87VitaNutrientSystemContext _context = new Sep490G87VitaNutrientSystemContext();

        private static Random random = new Random();

        public async Task<bool> CheckOfFilterTheTypeDiseaseBlockListAvoidIngredient(FoodList FoodSystemInput, NutritionTargetsDaily nutritionTargetsDaily ,int idUser)
        {

            if(FoodSystemInput.FoodTypeId != nutritionTargetsDaily.FoodTypeIdWant) return false;

            FoodSelection foodSelection = await _context.FoodSelections.FirstOrDefaultAsync(x => x.FoodListId == FoodSystemInput.FoodListId && x.UserId == idUser);
            if (foodSelection != null)
            {
                if (foodSelection.IsBlock ?? false) return false;
            }

            UserDetail userDetail = await _context.UserDetails.FirstOrDefaultAsync(x => x.UserId == idUser);
            if (userDetail == null) return false;

            if(userDetail.UnderlyingDisease != null)
            {
                int[] allDiseaseOfUser = await SplitAndProcess3(userDetail.UnderlyingDisease);
                foreach (var itemIdDisease in allDiseaseOfUser)
                {
                    FoodAndDisease foodAndDisease = await _context.FoodAndDiseases.FirstOrDefaultAsync(x => x.FoodListId == FoodSystemInput.FoodListId && x.ListOfDiseasesId == itemIdDisease);
                    if(foodAndDisease != null)
                    {
                        if(!(foodAndDisease.IsGoodOrBad ?? true)) return false;
                    }
                }
            }

            int[] allAvoidIngredient = await SplitAndProcess3(nutritionTargetsDaily.AvoidIngredient ?? "-1");
            foreach (var item in allAvoidIngredient)
            {
                ScaleAmount scaleAmount = await _context.ScaleAmounts.FirstOrDefaultAsync(x => x.IngredientDetailsId == item && x.FoodListId == FoodSystemInput.FoodListId);
                if (scaleAmount != null) return false;
            }
            return true;
        }



        public async Task<IEnumerable<int>> FilterTheTypeDiseaseBlockListAvoidIngredient(NutritionTargetsDaily nutritionTargetsDaily, int idUser)
        {
            IEnumerable<FoodList> idFoodListSystemFilterDishType = await _context.FoodLists.Where(x => x.FoodTypeId == nutritionTargetsDaily.FoodTypeIdWant).ToListAsync();

            List<int> idFoodListSystemCollection = new List<int>();

            foreach (var item in idFoodListSystemFilterDishType)
            {
                if (await CheckOfFilterTheTypeDiseaseBlockListAvoidIngredient(item, nutritionTargetsDaily, idUser))
                {
                    idFoodListSystemCollection.Add(item.FoodListId);
                }
            }
            return idFoodListSystemCollection;
        }

        public async Task<IEnumerable<FoodListDTO>> GetTheListOfDishesByMealSettingsDetails(int MealSettingsDetailsId)
        {
            MealSettingsDetail mealSettingsDetail = _context.MealSettingsDetails.Find(MealSettingsDetailsId);
            if(mealSettingsDetail != null)
            {
                NutritionTargetsDaily nutritionTargetsDaily = await _context.NutritionTargetsDailies.FindAsync(mealSettingsDetail.NutritionTargetsDailyId);
                int idUser;

                if (mealSettingsDetail != null)
                {
                    nutritionTargetsDaily = await _context.NutritionTargetsDailies.FindAsync(mealSettingsDetail.NutritionTargetsDailyId);
                    MealSetting mealSettings = await _context.MealSettings.FindAsync(mealSettingsDetail.MealSettingsId);
                    if (mealSettings != null)
                    {
                        idUser = mealSettings.UserId;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }

                IEnumerable<int> idFoodListSystem = await FilterTheTypeDiseaseBlockListAvoidIngredient(nutritionTargetsDaily, idUser);
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

                            if (await CheckForUserMealSettingsDetailsIsSmallerThanNeeded(foodObtained, MealSettingsDetailsId))
                            {
                                collectionOfDishes.Add(foodObtained);
                                break;
                            }
                            else if (await CheckForUserMealSettingsDetails(foodObtained, MealSettingsDetailsId))
                            {
                                collectionOfDishes.Add(foodObtained);
                                break;
                            }
                        }
                        else
                        {
                            if (await CheckForUserMealSettingsDetailsIsSmallerThanNeeded(await TotalAllTheIngredientsOfTheDish(collectionOfDishes), MealSettingsDetailsId))
                            {
                                FoodListDTO foodObtained = await TotalAllTheIngredientsOfTheDish(await TakeAllTheIngredientsOfTheDish(randomId));
                                if (await CheckForUserMealSettingsDetailsIsSmallerThanNeeded(foodObtained, MealSettingsDetailsId))
                                {
                                    collectionOfDishes.Add(foodObtained);
                                    break;
                                }
                                else if (await CheckForUserMealSettingsDetails(foodObtained, MealSettingsDetailsId))
                                {
                                    collectionOfDishes.Add(foodObtained);
                                    break;
                                }

                            }else if (await CheckForUserMealSettingsDetails(await TotalAllTheIngredientsOfTheDish(collectionOfDishes), MealSettingsDetailsId))
                            {
                                FoodListDTO foodObtained = await TotalAllTheIngredientsOfTheDish(await TakeAllTheIngredientsOfTheDish(randomId));
                                if (await CheckForUserMealSettingsDetailsIsSmallerThanNeeded(foodObtained, MealSettingsDetailsId))
                                {
                                    collectionOfDishes.Add(foodObtained);
                                    break;
                                } 
                                else if (await CheckForUserMealSettingsDetails(foodObtained, MealSettingsDetailsId))
                                {
                                    collectionOfDishes.Add(foodObtained);
                                    break;
                                }
                            }
                        }
                    }
                    if (collectionOfDishes.Count() == (mealSettingsDetail.NumberOfDishes ?? 1))
                    {
                        FoodListDTO foodObtained = await TotalAllTheIngredientsOfTheDish(collectionOfDishes);
                        if (await CheckForUserMealSettingsDetails(foodObtained, MealSettingsDetailsId))
                        {
                            foragingLoop = false;
                        }
                        else
                        {
                            collectionOfDishes.Clear();
                        }
                    }
                    if (loopCount == (mealSettingsDetail.NumberOfDishes ?? 1)*5)
                    {
                        foragingLoop = false;
                    }
                }
                return collectionOfDishes;
            }
            else
            {
                return null;
            }
        }





        public async Task<int> GetRandomFoodId(IEnumerable<int> idFoodListSystem, List<int> selectedIds)
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



        public async Task<bool> CheckForUserMealSettingsDetailsIsSmallerThanNeeded(FoodListDTO dataFood, int MealSettingsDetailsId)
        {
            MealSettingsDetail mealSettingsDetail = _context.MealSettingsDetails.Find(MealSettingsDetailsId);

            if (!await CheckKeyListAndData(dataFood, mealSettingsDetail)) return false;

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
            if (dataFood.ingredientDetails100gReduceDTO.Energy > nutritionTargetsDaily.Calories * (1 + calorieTolerance)) return false;
            if (dataFood.ingredientDetails100gReduceDTO.Carbohydrate > nutritionTargetsDaily.CarbsMax * (1 + carbTolerance)) return false;
            if (dataFood.ingredientDetails100gReduceDTO.Fat > nutritionTargetsDaily.FatsMax * (1 + fatTolerance)) return false;
            if (dataFood.ingredientDetails100gReduceDTO.Protein > nutritionTargetsDaily.ProteinMax * (1 + proteinTolerance)) return false;
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


        public async Task<bool> CheckKeyListAndData(FoodListDTO dataFood, MealSettingsDetail mealSettingsDetail)
        {
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
            return true;
        }


        public async Task<bool> CheckForUserMealSettingsDetails(FoodListDTO dataFood, int MealSettingsDetailsId)
        {
            MealSettingsDetail mealSettingsDetail = _context.MealSettingsDetails.Find(MealSettingsDetailsId);

            if (!await CheckKeyListAndData(dataFood, mealSettingsDetail)) return false;

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

        public async Task<int[]> SplitAndProcess3(string part)
        {
            int[] ints = part.Split(';').Select(s => { int number; return int.TryParse(s, out number) ? number : 0; }).ToArray();
            return ints;
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

            double averageCramCount = 100.0;


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
                                                         Energy = ingredientDetails100gs.Energy / averageCramCount * scaleAmounts.ScaleAmount1,
                                                         Protein = ingredientDetails100gs.Protein / averageCramCount * scaleAmounts.ScaleAmount1,
                                                         Fat = ingredientDetails100gs.Fat / averageCramCount * scaleAmounts.ScaleAmount1,
                                                         Carbohydrate = ingredientDetails100gs.Carbohydrate / averageCramCount * scaleAmounts.ScaleAmount1,
                                                         Fiber = ingredientDetails100gs.Fiber / averageCramCount * scaleAmounts.ScaleAmount1,
                                                         Sodium = ingredientDetails100gs.Sodium / averageCramCount * scaleAmounts.ScaleAmount1,
                                                         Cholesterol = ingredientDetails100gs.Cholesterol / averageCramCount * scaleAmounts.ScaleAmount1
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



        public async Task<bool> FillInDishIdInDailyDish(int idUser)
        {

            //GetTheListOfDishesByMealSettingsDetails(null, idUser);





            return true;
        }

    }
}
