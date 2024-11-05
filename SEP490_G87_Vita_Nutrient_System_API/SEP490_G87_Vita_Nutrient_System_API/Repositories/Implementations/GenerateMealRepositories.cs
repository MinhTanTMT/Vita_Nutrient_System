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

            if(combinedAndFilteredList != null)
            {
                foreach (int idFood in combinedAndFilteredList)
                {
                    FoodListDTO foodObtained = await TotalAllTheIngredientsOfTheDish(await TakeAllTheIngredientsOfTheDish(idFood));
                    collectionOfDishes.Add(foodObtained);
                }
            }

            bool foragingLoop = true;
            int loopCount = 0;

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
                    else if(collectionOfDishes.Count() < (mealSettingsDetail.NumberOfDishes ?? 1))
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
            MealSetting MealSettingDataOfUser = await _context.MealSettings.FirstOrDefaultAsync(x => x.UserId == idUser);
            int getNutritionRouteId;
            NutritionRoute activeNutritionRoute = await _context.NutritionRoutes.FirstOrDefaultAsync(nr => nr.StartDate <= MyDay && nr.EndDate >= MyDay && nr.UserId == idUser && nr.IsDone == false);
            if (activeNutritionRoute == null)
            {
                NutritionRoute CreaterNutritionRoute = new NutritionRoute()
                {
                    UserId = idUser,
                    CreateById = 1,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.ParseExact("11/11/9999 00:00:00", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture),
                    IsDone = false,
                };
                _context.NutritionRoutes.Add(CreaterNutritionRoute);
                _context.SaveChanges();
                getNutritionRouteId = CreaterNutritionRoute.Id;
            }
            else
            {
                getNutritionRouteId = activeNutritionRoute.Id;
            }

            if (MealSettingDataOfUser != null)
            {
                if (MealSettingDataOfUser.SameScheduleEveryDay ?? false)
                {
                    IEnumerable<MealSettingsDetail> MealSettingsDetailDataOfUser = await _context.MealSettingsDetails.Where(x => x.MealSettingsId == MealSettingDataOfUser.Id && x.DayOfTheWeekId == 8 & x.IsActive == true).ToListAsync();

                    if (MealSettingsDetailDataOfUser.Count() > 0)
                    {
                        StringBuilder stringListId = new StringBuilder();
                        DateTime TheDayBefore = new DateTime(DateTime.Now.Year, 1, 1).AddDays((MyDay.DayOfYear - 1) - 1);
                        MealOfTheDay mealSettingsDetailDayBefore = await _context.MealOfTheDays.FirstOrDefaultAsync(x => x.NutritionRouteId == getNutritionRouteId && x.DateExecute == DateOnly.FromDateTime(TheDayBefore));  
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
                                IEnumerable<FoodListDTO> dataFoodOfSlot = await GetTheListOfDishesByMealSettingsDetails(null, itemMealSettingsDetai.Id);
                                stringListId.AppendLine($"SlotOfTheDay={itemMealSettingsDetai.SlotOfTheDayId};SettingDetail={itemMealSettingsDetai.Id};OrderNumber={itemMealSettingsDetai.OrderNumber}:");
                                foreach (var foodOfSlot in dataFoodOfSlot)
                                {
                                    stringListId.Append(foodOfSlot.FoodListId + "-;");
                                }
                                stringListId.AppendLine("#");
                            }
                        }

                        MealOfTheDay mealSettingsDetail = await _context.MealOfTheDays.FirstOrDefaultAsync(x => x.NutritionRouteId == getNutritionRouteId && x.DateExecute == DateOnly.FromDateTime(MyDay));
                        if (mealSettingsDetail != null)
                        {
                            mealSettingsDetail.DataFoodListId = stringListId.ToString();
                            mealSettingsDetail.IsEditByUser = true;
                            _context.SaveChanges();
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
                            _context.SaveChanges();
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else // loai co petium nhieu nhat 7 bua moi ngay
                {

                    DayOfWeek[] DOW = [DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Saturday];
                    List<short> weekNumber = new List<short> { 1, 2, 3, 4, 5, 6, 7 };

                    foreach (short nbWk in weekNumber)
                    {
                        IEnumerable<MealSettingsDetail> MealSettingsDetailDataOfUser = await _context.MealSettingsDetails.Where(x => x.MealSettingsId == MealSettingDataOfUser.Id && x.DayOfTheWeekId == nbWk & x.IsActive == true).ToListAsync();

                        if (MealSettingsDetailDataOfUser.Count() > 0)
                        {
                            StringBuilder stringListId = new StringBuilder();
                            DateTime today = MyDay;
                            DayOfWeek targetDay = DOW[nbWk-1];

                            int daysDifference = (7 + (targetDay - MyDay.DayOfWeek)) % 7;
                            DateTime targetDate = today.AddDays(daysDifference);
                            DateTime TheDayBefore = new DateTime(DateTime.Now.Year, 1, 1).AddDays((targetDate.DayOfYear - 7) - 1);
                            MealOfTheDay mealSettingsDetailDayBefore = await _context.MealOfTheDays.FirstOrDefaultAsync(x => x.NutritionRouteId == getNutritionRouteId && x.DateExecute == DateOnly.FromDateTime(TheDayBefore));
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
                                    IEnumerable<FoodListDTO> dataFoodOfSlot = await GetTheListOfDishesByMealSettingsDetails(null, itemMealSettingsDetai.Id);
                                    stringListId.AppendLine($"SlotOfTheDay={itemMealSettingsDetai.SlotOfTheDayId};SettingDetail={itemMealSettingsDetai.Id};OrderNumber={itemMealSettingsDetai.OrderNumber}:");
                                    foreach (var foodOfSlot in dataFoodOfSlot)
                                    {
                                        stringListId.Append(foodOfSlot.FoodListId + "-;");
                                    }
                                    stringListId.AppendLine("#");
                                }
                            }

                            MealOfTheDay mealSettingsDetail = await _context.MealOfTheDays.FirstOrDefaultAsync(x => x.NutritionRouteId == getNutritionRouteId && x.DateExecute == DateOnly.FromDateTime(targetDate));
                            if (mealSettingsDetail != null)
                            {
                                mealSettingsDetail.DataFoodListId = stringListId.ToString();
                                mealSettingsDetail.IsEditByUser = true;
                                _context.SaveChanges();
                            }
                            else
                            {
                                MealOfTheDay NewSettingsDetail = new MealOfTheDay()
                                {
                                    NutritionRouteId = getNutritionRouteId,
                                    DataFoodListId = stringListId.ToString(),
                                    DateExecute = DateOnly.FromDateTime(targetDate),
                                    IsEditByUser = true
                                };
                                _context.MealOfTheDays.Add(NewSettingsDetail);
                                _context.SaveChanges();
                            }
                        }
                    }
                    return true;
                }
            }
            return false;
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

            FoodStatusUpdateModel unitSlotFoodChange = new FoodStatusUpdateModel() { UserId = userId, MyDay = myDay, SlotOfTheDay = dataListChange.SlotOfTheDay, SettingDetail = dataListChange.SettingDetail };
            
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
                        for (int i = 0; i < arrayData.Length;  i++)
                        { 
                            if (arrayData[i].Contains($"SlotOfTheDay={dataprocess.SlotOfTheDay};SettingDetail={dataprocess.SettingDetail};OrderNumber={dataprocess.OrderNumber}:"))
                            {

                                MealSettingsDetail mealSettingsDetail = await _context.MealSettingsDetails.FirstOrDefaultAsync(x => x.Id == dataprocess.SettingDetail && x.IsActive == true);

                                if (mealSettingsDetail != null) {

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
                    _context.SaveChanges();
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

    }
}



//DateOnly dateOnly = DateTime.Now.DayOfWeek;
//DateTime date = DateTime.ParseExact("31/12/2024 00:00:00", "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
//int dayOfYear = 298; // Ví dụ đây là số ngày trong năm cần chuyển đổi
//int year = 2024; // Năm bạn muốn tính
// Tạo một DateTime bắt đầu từ ngày đầu tiên của năm, sau đó thêm (dayOfYear - 1) ngày
//DateTime date2 = new DateTime(year, 1, 1).AddDays(dayOfYear - 1);
//Console.WriteLine(date.ToString("dd/MM/yyyy"));
//File.WriteAllText(@"C:\Users\msi\Desktop\SEP490_G87\Referent\DaChayDenDay.txt", date.DayOfYear + "");  số ngày trong năm
//File.WriteAllText(@"C:\Users\msi\Desktop\SEP490_G87\Referent\DaChayDenDay.txt", date.DayOfWeek + "");  thứ trong tuần