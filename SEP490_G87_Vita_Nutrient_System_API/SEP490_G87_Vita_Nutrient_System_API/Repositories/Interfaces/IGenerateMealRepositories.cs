using SEP490_G87_Vita_Nutrient_System_API.Domain.DataFoodList;
using SEP490_G87_Vita_Nutrient_System_API.Dtos;
using SEP490_G87_Vita_Nutrient_System_API.Models;

namespace SEP490_G87_Vita_Nutrient_System_API.Repositories.Interfaces
{
    public interface IGenerateMealRepositories
    {
        Task<bool> CheckOfFilterTheTypeDiseaseBlockListAvoidIngredient(FoodList FoodSystemInput, NutritionTargetsDaily nutritionTargetsDaily, int idUser);

        Task<IEnumerable<int>> FilterTheTypeDiseaseBlockListAvoidIngredient(NutritionTargetsDaily nutritionTargetsDaily, int idUser, int foodSelectionType);

        Task<IEnumerable<FoodListDTO>> GetTheListOfDishesByMealSettingsDetails(IEnumerable<int>? listItemIdAlreadyExistsOfMealSettingsDetails, int MealSettingsDetailsId, int foodSelectionType);

        Task<IEnumerable<FoodListDTO>> CreateAGettableFoodList(MealSettingsDetail mealSettingsDetail, int MealSettingsDetailsId, IEnumerable<int> idFoodListSystemCanBeObtained, IEnumerable<int>? combinedAndFilteredList);

        Task<int> GetRandomFoodId(IEnumerable<int> idFoodListSystem, List<int> selectedIds);

        Task<bool> CheckForUserMealSettingsDetailsIsSmallerThanNeeded(FoodListDTO dataFood, int MealSettingsDetailsId);

        Task<bool> CheckKeyListAndData(FoodListDTO dataFood, MealSettingsDetail mealSettingsDetail);

        Task<bool> CheckForUserMealSettingsDetails(FoodListDTO dataFood, int MealSettingsDetailsId);

        Task<FoodListDTO> TotalAllTheIngredientsOfTheDish(IEnumerable<FoodListDTO> dataFood);

        Task<IEnumerable<FoodListDTO>> TakeAllTheIngredientsOfTheDish(int idFoodListId);

        Task<bool> FillInDishIdInDailyDish(int idUser, DateTime MyDay);

        Task<bool> FillInDishIdInDailyDishWithCondition(int idUser, DateTime MyDay);

        Task<bool> insertStringDataIntoMealOfTheDay(int getNutritionRouteId, DateTime MyDay, string stringListId);

        Task<string> TakeDataIntoMealOfTheDayBefore(DataFoodListMealOfTheDay dataFoodListMealOfTheDay);

        Task<bool> FillInDishIdInWeekDish(int idUser, DateTime myDay);

        Task<IEnumerable<DataFoodAllDayOfWeek>> ListMealOfTheWeek(DateTime myDay, int idUser);

        Task<bool> RegenerateListMealOfTheWeek(DateTime myDay, int idUser);

        Task<DayOfWeek> ConvertToDayOfWeek(string dayString);

        Task<DataFoodListMealOfTheDay> SplitAndProcessDataMealOfTheDay(string part);

        Task<int?> ParseNumeric(string str);

        Task<string[]> SplitAndProcessFirst(string input);

        Task<Dictionary<string, string>> SplitAndProcess1(string part);

        Task<Dictionary<string, int[]>> SplitAndProcess2(string part);

        Task<int[]> SplitAndProcess3(string part);

        Task<bool> GetThisListOfDishesInputMealDay(DataFoodListMealOfTheDay dataListChange, int userId, DateTime myDay);

        Task<IEnumerable<DataFoodListMealOfTheDay>> ListMealOfTheDay(DateTime myDay, int idUser);

        Task<bool> ModifiedCompleteTheDish(FoodStatusUpdateModel dataprocess, string? statusSymbolReplace, int? idFoodReplace, List<KeyValuePair<int, string?>>? listIdFoodChange);

        Task<IEnumerable<DataFoodListMealOfTheDay>> CreateListOfAlternativeDishes(List<int>? listIdFood, int MealSettingsDetailsId, int numberOfCreation, int foodSelectionType);
        
        Task<bool> IsNumeric(string str);

        Task<bool> SystemUserConfiguration(int userId);


    }

}
