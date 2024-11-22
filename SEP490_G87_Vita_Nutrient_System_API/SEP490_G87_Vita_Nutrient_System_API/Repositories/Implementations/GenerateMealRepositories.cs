using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using NuGet.Configuration;
using SEP490_G87_Vita_Nutrient_System_API.Domain.DataFoodList;
using SEP490_G87_Vita_Nutrient_System_API.Dtos;
using SEP490_G87_Vita_Nutrient_System_API.Models;
using SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SEP490_G87_Vita_Nutrient_System_API.Repositories.Implementations
{
    public class GenerateMealRepositories : IGenerateMealRepositories
    {
        public GenerateMealRepositories() { }

        Sep490G87VitaNutrientSystemContext _context = new Sep490G87VitaNutrientSystemContext();

        private static Random random = new Random();

        public async Task<bool> CheckOfFilterTheTypeDiseaseBlockListAvoidIngredient(FoodList FoodSystemInput, NutritionTargetsDaily nutritionTargetsDaily, int idUser)
        {
            IEnumerable<DietWithFoodType> dataDietWithFoodType = await _context.DietTypes
            .SelectMany(x => x.FoodTypes, (a, b) => new DietWithFoodType
            {
                DietTypeId = a.DietTypeId,
                FoodTypeId = b.FoodTypeId
            })
            .ToListAsync();

            List<short> allFoodTypeSelect = dataDietWithFoodType.Where(x => x.DietTypeId == nutritionTargetsDaily.FoodTypeIdWant).Select(x => x.FoodTypeId).ToList();

            if (!allFoodTypeSelect.Contains(FoodSystemInput.FoodTypeId)) return false; // test sau

            FoodSelection foodSelection = await _context.FoodSelections.FirstOrDefaultAsync(x => x.FoodListId == FoodSystemInput.FoodListId && x.UserId == idUser);
            if (foodSelection != null)
            {
                if (foodSelection.IsBlock ?? false) return false;
            }

            UserDetail userDetail = await _context.UserDetails.FirstOrDefaultAsync(x => x.UserId == idUser);
            if (userDetail == null) return false;

            if (userDetail.UnderlyingDisease != null)
            {
                int[] allDiseaseOfUser = await SplitAndProcess3(userDetail.UnderlyingDisease);
                foreach (var itemIdDisease in allDiseaseOfUser)
                {
                    FoodAndDisease foodAndDisease = await _context.FoodAndDiseases.FirstOrDefaultAsync(x => x.FoodListId == FoodSystemInput.FoodListId && x.ListOfDiseasesId == itemIdDisease);
                    if (foodAndDisease != null)
                    {
                        if (!(foodAndDisease.IsGoodOrBad ?? true)) return false;
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
            IEnumerable<DietWithFoodType> dataDietWithFoodType = await _context.DietTypes
            .SelectMany(x => x.FoodTypes, (a, b) => new DietWithFoodType
            {
                DietTypeId = a.DietTypeId,
                FoodTypeId = b.FoodTypeId
            })
            .ToListAsync();
  
            List<short> allFoodTypeSelect = dataDietWithFoodType.Where(x => x.DietTypeId == nutritionTargetsDaily.FoodTypeIdWant).Select(x => x.FoodTypeId).ToList();

            // Thêm cái các món ăn like, recurent , isCollection// mặc định thì ren all món 

            IEnumerable<FoodSelection> foodSelectionOfUser = await _context.FoodSelections.Where(x => x.UserId == idUser).ToListAsync();
            
            
            int foodSelectionType = 0;


            if (foodSelectionType == 0)
            {

            }
            else if (foodSelectionType == 1) 
            { 
            
            }
            else if (foodSelectionType == 2)
            {

            }
            else
            {

            }


            IEnumerable<FoodList> idFoodListSystemFilterDishType = await _context.FoodLists.Where(x => allFoodTypeSelect.Contains(x.FoodTypeId)).ToListAsync();

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

        public async Task<IEnumerable<FoodListDTO>> GetTheListOfDishesByMealSettingsDetails(IEnumerable<int>? listItemIdAlreadyExistsOfMealSettingsDetails, int MealSettingsDetailsId)
        {
            MealSettingsDetail mealSettingsDetail = _context.MealSettingsDetails.Find(MealSettingsDetailsId);

            IEnumerable<FoodListDTO> collectionOfDishes = new List<FoodListDTO>();
            if (mealSettingsDetail != null)
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

                if (listItemIdAlreadyExistsOfMealSettingsDetails != null)
                {
                    IEnumerable<int> combinedAndFilteredList = listItemIdAlreadyExistsOfMealSettingsDetails.Where(item => idFoodListSystem.Contains(item)).ToList();
                    collectionOfDishes = await CreateAGettableFoodList(mealSettingsDetail, MealSettingsDetailsId, idFoodListSystem, combinedAndFilteredList);
                }
                else
                {
                    collectionOfDishes = await CreateAGettableFoodList(mealSettingsDetail, MealSettingsDetailsId, idFoodListSystem, null);
                }

                return collectionOfDishes;
            }
            else
            {
                return null;
            }
        }


        public async Task<IEnumerable<FoodListDTO>> CreateAGettableFoodList(MealSettingsDetail mealSettingsDetail, int MealSettingsDetailsId, IEnumerable<int> idFoodListSystemCanBeObtained, IEnumerable<int>? combinedAndFilteredList)
        {
            List<FoodListDTO> collectionOfDishes = new List<FoodListDTO>();

            if (combinedAndFilteredList != null)
            {
                foreach (int idFood in combinedAndFilteredList)
                {
                    FoodListDTO foodObtained = await TotalAllTheIngredientsOfTheDish(await TakeAllTheIngredientsOfTheDish(idFood));
                    collectionOfDishes.Add(foodObtained);
                }
            }

            bool foragingLoop = true;
            int loopCount = 0;

            if(mealSettingsDetail.NumberOfDishes > 0)
            {
                while (foragingLoop)
                {
                    List<int> selectedIds = new List<int>();
                    loopCount++;
                    foreach (int idGet in idFoodListSystemCanBeObtained)
                    {
                        int randomId = await GetRandomFoodId(idFoodListSystemCanBeObtained, selectedIds);
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
                        else if (collectionOfDishes.Count() < (mealSettingsDetail.NumberOfDishes ?? 1))
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

                            }
                            else if (await CheckForUserMealSettingsDetails(await TotalAllTheIngredientsOfTheDish(collectionOfDishes), MealSettingsDetailsId))
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
                            if (combinedAndFilteredList != null)
                            {
                                foreach (int idFood in combinedAndFilteredList)
                                {
                                    FoodListDTO foodObtainedRetake = await TotalAllTheIngredientsOfTheDish(await TakeAllTheIngredientsOfTheDish(idFood));
                                    collectionOfDishes.Add(foodObtainedRetake);
                                }
                            }
                        }
                    }
                    if (loopCount == (mealSettingsDetail.NumberOfDishes ?? 1) * 5)
                    {
                        collectionOfDishes.Clear();
                        foragingLoop = false;
                    }
                }
            }

            return collectionOfDishes;
        }



        public async Task<int> GetRandomFoodId(IEnumerable<int> idFoodListSystem, List<int> selectedIds)
        {
            List<int> availableIds = new List<int>(idFoodListSystem);
            availableIds.RemoveAll(id => selectedIds.Contains(id));

            if (availableIds.Count == 0)
            {
                throw new InvalidOperationException("Nope");
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

            double targetSodium = 2300; // sửa lại phải nhân với số bữa melstting từ đó chia ra mỗi bữa nhiều hất bao nhiêu
            double targetCholesterol = 300; // sửa lại phải nhân với số bữa melstting từ đó chia ra mỗi bữa nhiều hất bao nhiêu

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

        public async Task<bool> FillInDishIdInDailyDish(int idUser, DateTime MyDay)
        {
            var data = _context.UserListManagements.FirstOrDefault(x =>
                x.UserId == idUser
                && x.StartDate <= MyDay
                && x.EndDate >= MyDay);

            MealSetting MealSettingDataOfUser = await _context.MealSettings.FirstOrDefaultAsync(x => x.UserId == idUser);
            int getNutritionRouteId;
            NutritionRoute activeNutritionRoute = await _context.NutritionRoutes.FirstOrDefaultAsync(nr => nr.StartDate <= MyDay && nr.EndDate >= MyDay && nr.UserId == idUser && nr.IsDone == false);
            if (activeNutritionRoute == null)
            {
                NutritionRoute CreaterNutritionRoute = new NutritionRoute()
                {
                    UserId = idUser,
                    CreateById = 1,
                    StartDate = MyDay,
                    EndDate = DateTime.ParseExact("11/11/9999 00:00:00", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture),
                    IsDone = false,
                };
                _context.NutritionRoutes.Add(CreaterNutritionRoute);
                await _context.SaveChangesAsync();
                getNutritionRouteId = CreaterNutritionRoute.Id;
            }
            else getNutritionRouteId = activeNutritionRoute.Id;

            if (MealSettingDataOfUser != null)
            {
                IEnumerable<MealSettingsDetail> MealSettingsDetailDataOfUser;
                DateTime TheDayBefore;
                MealOfTheDay mealSettingsDetailDayBefore;
                if (MealSettingDataOfUser.SameScheduleEveryDay ?? false)
                {
                    MealSettingsDetailDataOfUser = await _context.MealSettingsDetails.Where(x => x.MealSettingsId == MealSettingDataOfUser.Id && x.DayOfTheWeekId == 8 && x.IsActive == true).ToListAsync();

                    if (MealSettingsDetailDataOfUser != null)
                    {
                        if (MealSettingsDetailDataOfUser.Count() > 0)
                        {
                            TheDayBefore = new DateTime(DateTime.Now.Year, 1, 1).AddDays((MyDay.DayOfYear - 1) - 1);
                            mealSettingsDetailDayBefore = await _context.MealOfTheDays.FirstOrDefaultAsync(x => x.NutritionRouteId == getNutritionRouteId && x.DateExecute == DateOnly.FromDateTime(TheDayBefore));

                        }
                        else
                        {
                            await insertStringDataIntoMealOfTheDay(getNutritionRouteId, MyDay, "");
                            return false;
                        }   
                    } else return false;
                }
                else // loai co petium nhieu nhat 7 bua moi ngay
                {
                    int idDayOfTheWeek = await _context.DayOfTheWeeks
                    .Where(x => x.Name.ToLower() == MyDay.ToString("dddd", new System.Globalization.CultureInfo("vi-VN")).ToLower())
                    .Select(x => x.Id)
                    .FirstOrDefaultAsync();
                    MealSettingsDetailDataOfUser = await _context.MealSettingsDetails.Where(x => x.MealSettingsId == MealSettingDataOfUser.Id && x.DayOfTheWeekId == idDayOfTheWeek & x.IsActive == true).ToListAsync();

                    if (MealSettingsDetailDataOfUser != null)
                    {
                        if (MealSettingsDetailDataOfUser.Count() > 0)
                        {
                            TheDayBefore = new DateTime(DateTime.Now.Year, 1, 1).AddDays((MyDay.DayOfYear - 7) - 1);
                            mealSettingsDetailDayBefore = await _context.MealOfTheDays.FirstOrDefaultAsync(x => x.NutritionRouteId == getNutritionRouteId && x.DateExecute == DateOnly.FromDateTime(TheDayBefore));
                        }
                        else
                        {
                            await insertStringDataIntoMealOfTheDay(getNutritionRouteId, MyDay, "");
                            return false;
                        }
                    }
                    else return false;
                }

                StringBuilder stringListId = new StringBuilder(); 
                List<DataFoodListMealOfTheDay> dataFoodListMealOfTheDay = new List<DataFoodListMealOfTheDay>();
                string[] dataSplit;

                if (mealSettingsDetailDayBefore != null && mealSettingsDetailDayBefore.DataFoodListId != null)
                {
                    dataSplit = await SplitAndProcessFirst(mealSettingsDetailDayBefore.DataFoodListId);
                    foreach (var item in dataSplit)
                    {
                        if (item.Length > 0)
                        {
                            dataFoodListMealOfTheDay.Add(await SplitAndProcessDataMealOfTheDay(item));
                        }
                    }
                }

                IEnumerable<int> combinedAndFilteredList = MealSettingsDetailDataOfUser.Where(item => dataFoodListMealOfTheDay.Select(x => x.SettingDetail).ToList().Contains(item.Id)).Select(x => x.Id).ToList();

                foreach (var itemMealSettingsDetai in MealSettingsDetailDataOfUser)
                {
                    if (itemMealSettingsDetai.SkipCreationProcess ?? false)
                    {
                        if (mealSettingsDetailDayBefore != null && combinedAndFilteredList.Contains(itemMealSettingsDetai.Id))
                        {
                            stringListId.Append(await TakeDataIntoMealOfTheDayBefore(dataFoodListMealOfTheDay.FirstOrDefault(x => x.SettingDetail == itemMealSettingsDetai.Id)));
                        }
                        else
                        {
                            IEnumerable<FoodListDTO> dataFoodOfSlot = await GetTheListOfDishesByMealSettingsDetails(null, itemMealSettingsDetai.Id);
                            stringListId.AppendLine($"SlotOfTheDay={itemMealSettingsDetai.SlotOfTheDayId};SettingDetail={itemMealSettingsDetai.Id};OrderNumber={itemMealSettingsDetai.OrderNumber}:");
                            foreach (var foodOfSlot in dataFoodOfSlot)
                            {
                                stringListId.Append(foodOfSlot.FoodListId + "-;");
                            }
                            stringListId.AppendLine("#");
                        }
                    }
                    else
                    {
                        // bị lỗi nếu như số lượng món cài đặt = 0 // da fix
                        IEnumerable<FoodListDTO> dataFoodOfSlot = await GetTheListOfDishesByMealSettingsDetails(null, itemMealSettingsDetai.Id);
                        stringListId.AppendLine($"SlotOfTheDay={itemMealSettingsDetai.SlotOfTheDayId};SettingDetail={itemMealSettingsDetai.Id};OrderNumber={itemMealSettingsDetai.OrderNumber}:");
                        foreach (var foodOfSlot in dataFoodOfSlot)
                        {
                            stringListId.Append(foodOfSlot.FoodListId + "-;");
                        }
                        stringListId.AppendLine("#");
                    }
                }

                await insertStringDataIntoMealOfTheDay(getNutritionRouteId, MyDay, stringListId.ToString());
                return true;
            }
            return false;
        }


        public async Task<bool> insertStringDataIntoMealOfTheDay(int getNutritionRouteId ,DateTime MyDay, string stringListId)
        {
            MealOfTheDay mealSettingsDetail = await _context.MealOfTheDays.FirstOrDefaultAsync(x => x.NutritionRouteId == getNutritionRouteId && x.DateExecute == DateOnly.FromDateTime(MyDay));
            if (mealSettingsDetail != null)
            {
                mealSettingsDetail.DataFoodListId = stringListId.ToString();
                mealSettingsDetail.IsEditByUser = true;
                await _context.SaveChangesAsync();
            }
            else
            {
                MealOfTheDay NewSettingsDetail = new MealOfTheDay()
                {
                    NutritionRouteId = getNutritionRouteId,
                    DataFoodListId = stringListId.ToString(),
                    DateExecute = DateOnly.FromDateTime(MyDay),
                    IsEditByUser = true
                };
                _context.MealOfTheDays.Add(NewSettingsDetail);
                await _context.SaveChangesAsync();
            }
            return true;
        }


        public async Task<string> TakeDataIntoMealOfTheDayBefore(DataFoodListMealOfTheDay dataFoodListMealOfTheDay)
        {

            //MealSettingID se luon doc nhat trong mot danh sach string DataFoodListID

            List<int> listFoodCollection = new List<int>();
            StringBuilder stringListId = new StringBuilder();
            foreach (var getId in dataFoodListMealOfTheDay.foodIdData)
            {
                listFoodCollection.Add(getId.idFood);
            }
            IEnumerable<FoodListDTO> dataTake = await GetTheListOfDishesByMealSettingsDetails(listFoodCollection, dataFoodListMealOfTheDay.SettingDetail);

            MealSettingsDetail mealSetting = await _context.MealSettingsDetails.FindAsync(dataFoodListMealOfTheDay.SettingDetail);

            if (dataTake != null && mealSetting != null)
            {

                stringListId.AppendLine($"SlotOfTheDay={mealSetting.SlotOfTheDayId};SettingDetail={mealSetting.Id};OrderNumber={mealSetting.OrderNumber}:");
                foreach (var foodOfSlot in dataTake)
                {
                    stringListId.Append(foodOfSlot.FoodListId + "-;");
                }
                stringListId.AppendLine("#");
            }
            else
            {
                return stringListId.ToString();
            }

            return stringListId.ToString();
        }


        
        // dang lam viec
        public async Task<bool> FillInDishIdInWeekDish(int idUser, DateTime myDay)
        {
            var dataDayOfTheWeekStart = await _context.MealSettings.Include(x => x.DayOfTheWeekStart).FirstOrDefaultAsync(x => x.UserId == idUser);
            if (dataDayOfTheWeekStart != null)
            {
                DayOfWeek DayOfWeekStart = await ConvertToDayOfWeek(dataDayOfTheWeekStart.DayOfTheWeekStart.Name ?? "Thứ Hai");
                int daysDifference = (7 + (myDay.DayOfWeek - DayOfWeekStart)) % 7;
                DateTime startOfWeek = myDay.AddDays(-daysDifference);
                List<DateTime> weekAllDays = new List<DateTime>();
                for (int i = 0; i < 7; i++)
                {
                    weekAllDays.Add(startOfWeek.AddDays(i));
                }
                foreach (var day in weekAllDays)
                {
                    await FillInDishIdInDailyDish(idUser, day);
                }
                return true;
            }
            return false;
        }


        public async Task<IEnumerable<DataFoodAllDayOfWeek>> ListMealOfTheWeek(DateTime myDay, int idUser)
        {
            List<DataFoodAllDayOfWeek> dataFoodAllDayOfWeek = new List<DataFoodAllDayOfWeek>();

            var dataDayOfTheWeekStart = await _context.MealSettings.Include(x => x.DayOfTheWeekStart).FirstOrDefaultAsync(x => x.UserId == idUser);
            if (dataDayOfTheWeekStart != null)
            {
                DayOfWeek DayOfWeekStart = await ConvertToDayOfWeek(dataDayOfTheWeekStart.DayOfTheWeekStart.Name ?? "Thứ Hai");
                int daysDifference = (7 + (myDay.DayOfWeek - DayOfWeekStart)) % 7;
                DateTime startOfWeek = myDay.AddDays(-daysDifference);
                List<DateTime> weekAllDays = new List<DateTime>();
                for (int i = 0; i < 7; i++)
                {
                    weekAllDays.Add(startOfWeek.AddDays(i));
                }

                var dayOfWeekMap = (await _context.DayOfTheWeeks.ToListAsync()).ToDictionary(x => x.Name.ToLower(), x => x.Id);

                foreach (var day in weekAllDays)
                {
                    string dayName = day.ToString("dddd", new System.Globalization.CultureInfo("vi-VN")).ToLower();
                    int idDayOfTheWeek = dayOfWeekMap.ContainsKey(dayName) ? dayOfWeekMap[dayName] : -1;
                    IEnumerable<DataFoodListMealOfTheDay> dataFoodListMealOfTheDays = await ListMealOfTheDay(day, idUser);
                    if(dataFoodListMealOfTheDays.Count() == 0)
                    {
                        if (day.DayOfYear >= DateTime.Now.DayOfYear)
                        {
                            if (await FillInDishIdInDailyDish(idUser, day))
                            {
                                dataFoodListMealOfTheDays = await ListMealOfTheDay(day, idUser);
                            }
                        }
                    }
                    DataFoodAllDayOfWeek dataOneDayOfWeek = new DataFoodAllDayOfWeek() { DayOfTheWeekId = idDayOfTheWeek, DayOfTheWeekIdStart = dataDayOfTheWeekStart.DayOfTheWeekStartId, DayOfWeek = day, NameDayOfWeek = dayName , dataListFoodMealOfTheDay = dataFoodListMealOfTheDays.ToArray()};
                    dataFoodAllDayOfWeek.Add(dataOneDayOfWeek);
                }
            }
            return dataFoodAllDayOfWeek;
        }




        public async Task<bool> RegenerateListMealOfTheWeek(DateTime myDay, int idUser)
        {
            var dataDayOfTheWeekStart = await _context.MealSettings.Include(x => x.DayOfTheWeekStart).FirstOrDefaultAsync(x => x.UserId == idUser);
            if (dataDayOfTheWeekStart != null)
            {
                DayOfWeek DayOfWeekStart = await ConvertToDayOfWeek(dataDayOfTheWeekStart.DayOfTheWeekStart.Name ?? "Thứ Hai");
                int daysDifference = (7 + (myDay.DayOfWeek - DayOfWeekStart)) % 7;
                DateTime startOfWeek = myDay.AddDays(-daysDifference);
                List<DateTime> weekAllDays = new List<DateTime>();
                for (int i = 0; i < 7; i++)
                {
                    weekAllDays.Add(startOfWeek.AddDays(i));
                }

                foreach (var day in weekAllDays)
                {
                    if (day.DayOfYear >= DateTime.Now.DayOfYear) await FillInDishIdInDailyDish(idUser, day);
                }
            }
            return true;
        }

        public async Task<DayOfWeek> ConvertToDayOfWeek(string dayString)
        {
            // Từ điển ánh xạ chuỗi tiếng Việt sang DayOfWeek
            var dayMapping = new Dictionary<string, DayOfWeek>(StringComparer.OrdinalIgnoreCase)
        {
            { "Chủ nhật", DayOfWeek.Sunday },
            { "Thứ Hai", DayOfWeek.Monday },
            { "Thứ Ba", DayOfWeek.Tuesday },
            { "Thứ Tư", DayOfWeek.Wednesday },
            { "Thứ Năm", DayOfWeek.Thursday },
            { "Thứ Sáu", DayOfWeek.Friday },
            { "Thứ Bảy", DayOfWeek.Saturday }
        };

            // Trả về giá trị DayOfWeek nếu tìm thấy, nếu không sẽ mặc định là Chủ Nhật
            return dayMapping.TryGetValue(dayString, out DayOfWeek result) ? result : DayOfWeek.Sunday;
        }


        public async Task<DataFoodListMealOfTheDay> SplitAndProcessDataMealOfTheDay(string part)
        {
            DataFoodListMealOfTheDay dataFoodListMealOfTheDay = new DataFoodListMealOfTheDay();

            string[] splitByEqualHaiCham = part.Split(':');
            if (splitByEqualHaiCham.Length == 2)
            {
                string[] splitByEqualFirstChamPhay = splitByEqualHaiCham[0].Split(';');
                if (splitByEqualFirstChamPhay.Length == 3)
                {
                    foreach (var item in splitByEqualFirstChamPhay)
                    {
                        if (item.Contains("SlotOfTheDay"))
                        {
                            Dictionary<string, string> SlotOfTheDayData = await SplitAndProcess1(item);
                            dataFoodListMealOfTheDay.SlotOfTheDay = short.Parse(SlotOfTheDayData.Values.FirstOrDefault());
                            SlotOfTheDay slotOfTheDayInfo = await _context.SlotOfTheDays.FindAsync(dataFoodListMealOfTheDay.SlotOfTheDay);
                            dataFoodListMealOfTheDay.NameSlotOfTheDay = slotOfTheDayInfo.Slot;

                        }
                        else if (item.Contains("SettingDetail"))
                        {
                            Dictionary<string, string> SettingDetailData = await SplitAndProcess1(item);
                            dataFoodListMealOfTheDay.SettingDetail = Int32.Parse(SettingDetailData.Values.FirstOrDefault());
                        }
                        else if (item.Contains("OrderNumber"))
                        {
                            Dictionary<string, string> SettingDetailData = await SplitAndProcess1(item);
                            dataFoodListMealOfTheDay.OrderSettingDetail = Int32.Parse(SettingDetailData.Values.FirstOrDefault());
                        }
                    }
                }

                string[] splitByEqualSecondChamPhay = splitByEqualHaiCham[1].Split(';');
                List<FoodIdData> foodIdDataList = new List<FoodIdData>();

                for (int i = 0; i < splitByEqualSecondChamPhay.Length; i++)
                {
                    if (splitByEqualSecondChamPhay[i].Length > 0)
                    {
                        string symbolStatus = splitByEqualSecondChamPhay[i][^1].ToString();
                        int numberRemaining = await ParseNumeric(splitByEqualSecondChamPhay[i].Substring(0, splitByEqualSecondChamPhay[i].Length - 1)) ?? -1;
                        FoodList FoodInfo = await _context.FoodLists.FindAsync(numberRemaining);
                        if (FoodInfo != null)
                        {

                            FoodListDTO foodListDTO = await TotalAllTheIngredientsOfTheDish(await TakeAllTheIngredientsOfTheDish(numberRemaining));
                            foodIdDataList.Add(new FoodIdData { idFood = numberRemaining, statusSymbol = symbolStatus, positionFood = i, foodData = foodListDTO });

                        }
                    }
                    dataFoodListMealOfTheDay.foodIdData = foodIdDataList.ToArray();
                }

            }

            return dataFoodListMealOfTheDay;
        }



        public async Task<int?> ParseNumeric(string str)
        {
            if (int.TryParse(str, out int result))
            {
                return result;
            }
            return null;
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


        public async Task<bool> GetThisListOfDishesInputMealDay(DataFoodListMealOfTheDay dataListChange, int userId, DateTime myDay)
        {

            FoodStatusUpdateModel unitSlotFoodChange = new FoodStatusUpdateModel() { UserId = userId, MyDay = myDay, SlotOfTheDay = dataListChange.SlotOfTheDay, SettingDetail = dataListChange.SettingDetail, OrderNumber = dataListChange.OrderSettingDetail };

            List<int> listIdFoodChange = new List<int>();
            foreach (var item in dataListChange.foodIdData)
            {
                listIdFoodChange.Add(item.idFood);
            }

            if (await CompleteTheDish(unitSlotFoodChange, null, null, listIdFoodChange))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// /////////////////////////////////
        /// </summary>
        /// <returns></returns>

        public async Task<IEnumerable<DataFoodListMealOfTheDay>> ListMealOfTheDay(DateTime myDay, int idUser)
        {

            List<DataFoodListMealOfTheDay> dataFoodListMealOfTheDays = new List<DataFoodListMealOfTheDay>();

            NutritionRoute activeNutritionRoute = await _context.NutritionRoutes.FirstOrDefaultAsync(nr => nr.StartDate <= myDay && nr.EndDate >= myDay && nr.UserId == idUser && nr.IsDone == false);
            if (activeNutritionRoute != null)
            {
                MealOfTheDay mealOfTheDay = await _context.MealOfTheDays.FirstOrDefaultAsync(x => x.DateExecute == DateOnly.FromDateTime(myDay) && x.NutritionRouteId == activeNutritionRoute.Id);
                if (mealOfTheDay != null)
                {
                    string[] arrayData = await SplitAndProcessFirst(mealOfTheDay.DataFoodListId ?? "");
                    if (arrayData.Length > 0)
                    {
                        for (int i = 0; i < arrayData.Length - 1; i++)
                        {
                            dataFoodListMealOfTheDays.Add(await SplitAndProcessDataMealOfTheDay(arrayData[i]));
                        }
                    }
                }
            }
            return dataFoodListMealOfTheDays;
        }


        public async Task<bool> CheckDataMealOfTheToday(DataFoodListMealOfTheDay dataFoodListMealOfTheDay)
        {
            // can ham kiem tra

            // neu sai lay mon moi

            return true;
        }


        public async Task<bool> CompleteTheDish(FoodStatusUpdateModel dataprocess, string? statusSymbolReplace, int? idFoodReplace, List<int>? listIdFoodChange)
        {

            NutritionRoute activeNutritionRoute = await _context.NutritionRoutes.FirstOrDefaultAsync(nr => nr.StartDate <= dataprocess.MyDay && nr.EndDate >= dataprocess.MyDay && nr.UserId == dataprocess.UserId && nr.IsDone == false);
            if (activeNutritionRoute != null)
            {
                MealOfTheDay mealOfTheDay = await _context.MealOfTheDays.FirstOrDefaultAsync(x => x.DateExecute == DateOnly.FromDateTime(dataprocess.MyDay) && x.NutritionRouteId == activeNutritionRoute.Id);
                if (mealOfTheDay != null)
                {
                    string[] arrayData = await SplitAndProcessFirst(mealOfTheDay.DataFoodListId ?? "");
                    StringBuilder stringListId = new StringBuilder();
                    if (arrayData.Length > 0)
                    {
                        for (int i = 0; i < arrayData.Length; i++)
                        {
                            if (arrayData[i].Contains($"SlotOfTheDay={dataprocess.SlotOfTheDay};SettingDetail={dataprocess.SettingDetail};OrderNumber={dataprocess.OrderNumber}:"))
                            {

                                MealSettingsDetail mealSettingsDetail = await _context.MealSettingsDetails.FirstOrDefaultAsync(x => x.Id == dataprocess.SettingDetail && x.IsActive == true);

                                if (mealSettingsDetail != null)
                                {

                                    StringBuilder stringListIdOfSlot = new StringBuilder();
                                    DataFoodListMealOfTheDay dataFoodListMealOfTheDays = await SplitAndProcessDataMealOfTheDay(arrayData[i]);
                                    stringListIdOfSlot.AppendLine($"SlotOfTheDay={dataFoodListMealOfTheDays.SlotOfTheDay};SettingDetail={dataFoodListMealOfTheDays.SettingDetail};OrderNumber={dataFoodListMealOfTheDays.OrderSettingDetail}:");

                                    if (dataFoodListMealOfTheDays.foodIdData.Length == mealSettingsDetail.NumberOfDishes && listIdFoodChange == null)
                                    {
                                        foreach (var foodOfSlot in dataFoodListMealOfTheDays.foodIdData)
                                        {
                                            if (statusSymbolReplace != null && !foodOfSlot.statusSymbol.Equals("!"))
                                            {
                                                if (foodOfSlot.idFood == dataprocess.IdFood && foodOfSlot.statusSymbol.Equals(dataprocess.StatusSymbol) && foodOfSlot.positionFood == dataprocess.PositionFood)
                                                {
                                                    stringListIdOfSlot.Append($"{foodOfSlot.idFood}{statusSymbolReplace};");
                                                }
                                                else
                                                {
                                                    stringListIdOfSlot.Append($"{foodOfSlot.idFood}{foodOfSlot.statusSymbol};");
                                                }
                                            }
                                            else if (idFoodReplace != null && !foodOfSlot.statusSymbol.Equals("!"))
                                            {
                                                if (foodOfSlot.idFood == dataprocess.IdFood && foodOfSlot.positionFood == dataprocess.PositionFood)
                                                {
                                                    stringListIdOfSlot.Append($"{idFoodReplace}-;");
                                                }
                                                else
                                                {
                                                    stringListIdOfSlot.Append($"{foodOfSlot.idFood}{foodOfSlot.statusSymbol};");
                                                }
                                            }
                                            else
                                            {
                                                stringListIdOfSlot.Append($"{foodOfSlot.idFood}{foodOfSlot.statusSymbol};");
                                            }
                                        }
                                        arrayData[i] = stringListIdOfSlot.ToString();
                                    }
                                    else
                                    {
                                        IEnumerable<FoodListDTO> dataFoodOfSlot = await GetTheListOfDishesByMealSettingsDetails(listIdFoodChange, mealSettingsDetail.Id);
                                        foreach (var foodOfSlot in dataFoodOfSlot)
                                        {
                                            stringListIdOfSlot.Append(foodOfSlot.FoodListId + "-;");
                                        }
                                        arrayData[i] = stringListIdOfSlot.ToString();
                                    }
                                }
                                else
                                {
                                    arrayData[i] = "";
                                }
                            }
                            stringListId.Append(arrayData[i] + "#");
                        }
                    }

                    mealOfTheDay.DataFoodListId = stringListId.ToString().Remove(stringListId.ToString().Length - 1);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }



        public async Task<IEnumerable<DataFoodListMealOfTheDay>> CreateListOfAlternativeDishes(List<int>? listIdFood, int MealSettingsDetailsId, int numberOfCreation)
        {
            MealSettingsDetail mealSettingsDetail = _context.MealSettingsDetails.Find(MealSettingsDetailsId);

            if (mealSettingsDetail != null)
            {
                List<DataFoodListMealOfTheDay> dataFoodListMealOfTheDay = new List<DataFoodListMealOfTheDay>();
                for (int i = 0; i < numberOfCreation; i++)
                {
                    IEnumerable<FoodListDTO> foodListDTO = await GetTheListOfDishesByMealSettingsDetails(listIdFood, MealSettingsDetailsId);
                    FoodIdData[] dataCreateArray = new FoodIdData[foodListDTO.Count()];
                    int index = 0;
                    foreach (var item in foodListDTO)
                    {
                        dataCreateArray[index] = new FoodIdData
                        {
                            idFood = item.FoodListId,
                            statusSymbol = "-",
                            positionFood = index,
                            foodData = item
                        };
                        index++;
                    }
                    dataFoodListMealOfTheDay.Add(new DataFoodListMealOfTheDay
                    {
                        SlotOfTheDay = mealSettingsDetail.SlotOfTheDayId ?? 0,
                        SettingDetail = MealSettingsDetailsId,
                        OrderSettingDetail = mealSettingsDetail.OrderNumber ?? 0,
                        NameSlotOfTheDay = "Nope",
                        foodIdData = dataCreateArray
                    });
                }
                return dataFoodListMealOfTheDay.Distinct(new DataFoodListMealOfTheDayComparer()).ToList();
            }
            return null;
        }


        public async Task<bool> CreateMealSetting(int userId)
        {

            var myMealSetting = await _context.MealSettings.FirstOrDefaultAsync(x => x.UserId == userId);
            
            if (myMealSetting == null)
            {
                await _context.MealSettings.AddAsync(new MealSetting { UserId = userId, DayOfTheWeekStartId = 8, SameScheduleEveryDay = true , FoodTypeIdWant = 1});
                await _context.SaveChangesAsync();
            }

            var userDetail = await _context.UserDetails.FirstOrDefaultAsync(x => x.UserId == userId);

            if (userDetail == null)
            {
                await _context.UserDetails.AddAsync(new UserDetail { UserId = userId });
                await _context.SaveChangesAsync();
            }

            return true;
        }
    }
}

