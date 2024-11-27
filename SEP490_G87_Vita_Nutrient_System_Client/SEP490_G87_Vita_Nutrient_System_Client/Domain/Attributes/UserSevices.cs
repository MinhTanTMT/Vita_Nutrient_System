using SEP490_G87_Vita_Nutrient_System_Client.Models;

namespace SEP490_G87_Vita_Nutrient_System_Client.Domain.Attributes
{


    public class UserSevices
    {
        public FoodList TotalAllTheIngredientsOfTheDish(IEnumerable<FoodList> dataFood)
        {
            FoodList totalfoodListDTO = new FoodList()
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
                IngredientDetails100gReduceDTO = new Ingredientdetails100greducedto()
                {
                    Id = -1,
                    KeyNoteId = -1,
                    Name = "SummaryOfTheEntireList",
                    Describe = "SummaryOfTheEntireList",
                    Urlimage = "SummaryOfTheEntireList",
                    TypeOfCalculationId = -1,
                    Energy = dataFood.Sum(x => x.IngredientDetails100gReduceDTO.Energy),
                    Protein = dataFood.Sum(x => x.IngredientDetails100gReduceDTO.Protein),
                    Fat = dataFood.Sum(x => x.IngredientDetails100gReduceDTO.Fat),
                    Carbohydrate = dataFood.Sum(x => x.IngredientDetails100gReduceDTO.Carbohydrate),
                    Fiber = dataFood.Sum(x => x.IngredientDetails100gReduceDTO.Fiber),
                    Sodium = dataFood.Sum(x => x.IngredientDetails100gReduceDTO.Sodium),
                    Cholesterol = dataFood.Sum(x => x.IngredientDetails100gReduceDTO.Cholesterol)
                },
                KeyNote = new KeyNote
                {
                    Id = dataFood.First().KeyNote.Id,
                    KeyList = dataFood.First().KeyNote.KeyList
                },
                ScaleAmounts = new ScaleAmounts
                {
                    FoodListId = dataFood.First().FoodListId,
                    IngredientDetailsId = -1,
                    ScaleAmount = -1
                }
            };
            return totalfoodListDTO;
        }


        public List<SlotBranch> GetListCollection(List<DataFoodListMealOfTheDay> rootObjectFoodList)
        {
            List<SlotBranch> slotBranchesData = new List<SlotBranch>();
            var numberSlot = rootObjectFoodList.Select(x => new
            {
                x.SlotOfTheDay,
                x.NameSlotOfTheDay
            }).Distinct().ToList();

            foreach (var item in numberSlot)
            {
                SlotBranch slotBranch = new SlotBranch()
                {
                    SlotOfTheDay = item.SlotOfTheDay,
                    NameSlotOfTheDay = item.NameSlotOfTheDay,
                    TotalCaloriesPerMeal = (float)Math.Round(rootObjectFoodList.Where(x => x.SlotOfTheDay == item.SlotOfTheDay).OrderBy(x => x.SettingDetail).ToArray().Sum(x => x.foodIdData.Sum(x => x.foodData.IngredientDetails100gReduceDTO.Energy)), 2),
                    foodDataOfSlot = rootObjectFoodList.Where(x => x.SlotOfTheDay == item.SlotOfTheDay).OrderBy(x => x.OrderSettingDetail).ToArray()
                };
                slotBranchesData.Add(slotBranch);
            }
            return slotBranchesData;
        }


    }
}
