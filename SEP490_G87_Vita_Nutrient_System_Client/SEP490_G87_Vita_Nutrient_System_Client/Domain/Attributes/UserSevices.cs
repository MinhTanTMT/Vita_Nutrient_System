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
                IngredientDetails100gDTO = new IngredientDetails100gDTO()
                {
                    Id = -1,
                    KeyNoteId = -1,
                    Name = "SummaryOfTheEntireList",
                    Describe = "SummaryOfTheEntireList",
                    Urlimage = "SummaryOfTheEntireList",
                    TypeOfCalculationId = -1,


                    // Tổng hợp dữ liệu từ danh sách với ingredientDetails100gDTO
                    Energy = dataFood.Sum(x => x.IngredientDetails100gDTO.Energy),
                    Water = dataFood.Sum(x => x.IngredientDetails100gDTO.Water),
                    Protein = dataFood.Sum(x => x.IngredientDetails100gDTO.Protein),
                    Fat = dataFood.Sum(x => x.IngredientDetails100gDTO.Fat),
                    Carbohydrate = dataFood.Sum(x => x.IngredientDetails100gDTO.Carbohydrate),
                    Fiber = dataFood.Sum(x => x.IngredientDetails100gDTO.Fiber),
                    Ash = dataFood.Sum(x => x.IngredientDetails100gDTO.Ash),
                    Sugar = dataFood.Sum(x => x.IngredientDetails100gDTO.Sugar),
                    Galactose = dataFood.Sum(x => x.IngredientDetails100gDTO.Galactose),
                    Maltose = dataFood.Sum(x => x.IngredientDetails100gDTO.Maltose),
                    Lactose = dataFood.Sum(x => x.IngredientDetails100gDTO.Lactose),
                    Fructose = dataFood.Sum(x => x.IngredientDetails100gDTO.Fructose),
                    Glucose = dataFood.Sum(x => x.IngredientDetails100gDTO.Glucose),
                    Sucrose = dataFood.Sum(x => x.IngredientDetails100gDTO.Sucrose),
                    Calcium = dataFood.Sum(x => x.IngredientDetails100gDTO.Calcium),
                    Iron = dataFood.Sum(x => x.IngredientDetails100gDTO.Iron),



                    // Tổng hợp dữ liệu từ ingredientDetails100gDTO
                    Magnesium = dataFood.Sum(x => x.IngredientDetails100gDTO.Magnesium),
                    Manganese = dataFood.Sum(x => x.IngredientDetails100gDTO.Manganese),
                    Phosphorous = dataFood.Sum(x => x.IngredientDetails100gDTO.Phosphorous),
                    Potassium = dataFood.Sum(x => x.IngredientDetails100gDTO.Potassium),
                    Sodium = dataFood.Sum(x => x.IngredientDetails100gDTO.Sodium),
                    Zinc = dataFood.Sum(x => x.IngredientDetails100gDTO.Zinc),
                    Copper = dataFood.Sum(x => x.IngredientDetails100gDTO.Copper),
                    Selenium = dataFood.Sum(x => x.IngredientDetails100gDTO.Selenium),
                    VitaminC = dataFood.Sum(x => x.IngredientDetails100gDTO.VitaminC),
                    VitaminB1 = dataFood.Sum(x => x.IngredientDetails100gDTO.VitaminB1),
                    VitaminB2 = dataFood.Sum(x => x.IngredientDetails100gDTO.VitaminB2),
                    VitaminPp = dataFood.Sum(x => x.IngredientDetails100gDTO.VitaminPp),
                    VitaminB5 = dataFood.Sum(x => x.IngredientDetails100gDTO.VitaminB5),
                    VitaminB6 = dataFood.Sum(x => x.IngredientDetails100gDTO.VitaminB6),
                    Folat = dataFood.Sum(x => x.IngredientDetails100gDTO.Folat),
                    VitaminB9 = dataFood.Sum(x => x.IngredientDetails100gDTO.VitaminB9),
                    VitaminH = dataFood.Sum(x => x.IngredientDetails100gDTO.VitaminH),
                    VitaminB12 = dataFood.Sum(x => x.IngredientDetails100gDTO.VitaminB12),




                    // Tổng hợp dữ liệu từ ingredientDetails100gDTO
                    VitaminA = dataFood.Sum(x => x.IngredientDetails100gDTO.VitaminA),
                    VitaminD = dataFood.Sum(x => x.IngredientDetails100gDTO.VitaminD),
                    VitaminE = dataFood.Sum(x => x.IngredientDetails100gDTO.VitaminE),
                    VitaminK = dataFood.Sum(x => x.IngredientDetails100gDTO.VitaminK),
                    BetaCaroten = dataFood.Sum(x => x.IngredientDetails100gDTO.BetaCaroten),
                    AlphaCaroten = dataFood.Sum(x => x.IngredientDetails100gDTO.AlphaCaroten),
                    BetaCryptoxanthin = dataFood.Sum(x => x.IngredientDetails100gDTO.BetaCryptoxanthin),
                    Lycopen = dataFood.Sum(x => x.IngredientDetails100gDTO.Lycopen),
                    LuteinVsZeaxanthin = dataFood.Sum(x => x.IngredientDetails100gDTO.LuteinVsZeaxanthin),
                    Purin = dataFood.Sum(x => x.IngredientDetails100gDTO.Purin),
                    TotalIsoflavone = dataFood.Sum(x => x.IngredientDetails100gDTO.TotalIsoflavone),
                    Daidzein = dataFood.Sum(x => x.IngredientDetails100gDTO.Daidzein),
                    Genistein = dataFood.Sum(x => x.IngredientDetails100gDTO.Genistein),
                    Glycetin = dataFood.Sum(x => x.IngredientDetails100gDTO.Glycetin),
                    TotalSaturatedFattyAcid = dataFood.Sum(x => x.IngredientDetails100gDTO.TotalSaturatedFattyAcid),
                    PalmiticC160 = dataFood.Sum(x => x.IngredientDetails100gDTO.PalmiticC160),
                    MargaricC170 = dataFood.Sum(x => x.IngredientDetails100gDTO.MargaricC170),
                    StearicC180 = dataFood.Sum(x => x.IngredientDetails100gDTO.StearicC180),



                    // Tổng hợp dữ liệu từ ingredientDetails100gDTO
                    ArachidicC200 = dataFood.Sum(x => x.IngredientDetails100gDTO.ArachidicC200),
                    BehenicC220 = dataFood.Sum(x => x.IngredientDetails100gDTO.BehenicC220),
                    LignocericC240 = dataFood.Sum(x => x.IngredientDetails100gDTO.LignocericC240),
                    TotalMonounsaturatedFattyAcid = dataFood.Sum(x => x.IngredientDetails100gDTO.TotalMonounsaturatedFattyAcid),
                    MyristoleicC141 = dataFood.Sum(x => x.IngredientDetails100gDTO.MyristoleicC141),
                    PalmitoleicC161 = dataFood.Sum(x => x.IngredientDetails100gDTO.PalmitoleicC161),
                    OleicC181 = dataFood.Sum(x => x.IngredientDetails100gDTO.OleicC181),
                    TotalPolyunsaturatedFattyAcid = dataFood.Sum(x => x.IngredientDetails100gDTO.TotalPolyunsaturatedFattyAcid),
                    LinoleicC182N6 = dataFood.Sum(x => x.IngredientDetails100gDTO.LinoleicC182N6),
                    LinolenicC182N3 = dataFood.Sum(x => x.IngredientDetails100gDTO.LinolenicC182N3),
                    ArachidonicC204 = dataFood.Sum(x => x.IngredientDetails100gDTO.ArachidonicC204),
                    EicosapentaenoicC205N3 = dataFood.Sum(x => x.IngredientDetails100gDTO.EicosapentaenoicC205N3),
                    DocosahexaenoicC226N3 = dataFood.Sum(x => x.IngredientDetails100gDTO.DocosahexaenoicC226N3),
                    TotalTransFattyAcid = dataFood.Sum(x => x.IngredientDetails100gDTO.TotalTransFattyAcid),
                    Cholesterol = dataFood.Sum(x => x.IngredientDetails100gDTO.Cholesterol),
                    Phytosterol = dataFood.Sum(x => x.IngredientDetails100gDTO.Phytosterol),
                    Lysin = dataFood.Sum(x => x.IngredientDetails100gDTO.Lysin),
                    Methionin = dataFood.Sum(x => x.IngredientDetails100gDTO.Methionin),
                    Tryptophan = dataFood.Sum(x => x.IngredientDetails100gDTO.Tryptophan),
                    Phenylalanin = dataFood.Sum(x => x.IngredientDetails100gDTO.Phenylalanin),

                    // Tổng hợp dữ liệu từ ingredientDetails100gDTO
                    Threonin = dataFood.Sum(x => x.IngredientDetails100gDTO.Threonin),
                    Valin = dataFood.Sum(x => x.IngredientDetails100gDTO.Valin),
                    Leucin = dataFood.Sum(x => x.IngredientDetails100gDTO.Leucin),
                    Isoleucin = dataFood.Sum(x => x.IngredientDetails100gDTO.Isoleucin),
                    Arginin = dataFood.Sum(x => x.IngredientDetails100gDTO.Arginin),
                    Histidin = dataFood.Sum(x => x.IngredientDetails100gDTO.Histidin),
                    Cystin = dataFood.Sum(x => x.IngredientDetails100gDTO.Cystin),
                    Tyrosin = dataFood.Sum(x => x.IngredientDetails100gDTO.Tyrosin),
                    Alanin = dataFood.Sum(x => x.IngredientDetails100gDTO.Alanin),
                    AcidAspartic = dataFood.Sum(x => x.IngredientDetails100gDTO.AcidAspartic),
                    AcidGlutamic = dataFood.Sum(x => x.IngredientDetails100gDTO.AcidGlutamic),
                    Glycin = dataFood.Sum(x => x.IngredientDetails100gDTO.Glycin),
                    Prolin = dataFood.Sum(x => x.IngredientDetails100gDTO.Prolin),
                    Serin = dataFood.Sum(x => x.IngredientDetails100gDTO.Serin)
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

        
    //public class UserSevices
    //{
    //    public FoodList TotalAllTheIngredientsOfTheDish(IEnumerable<FoodList> dataFood)
    //    {
    //        FoodList totalfoodListDTO = new FoodList()
    //        {
    //            FoodListId = dataFood.First().FoodListId,
    //            Name = dataFood.First().Name,
    //            Describe = dataFood.First().Describe,
    //            Rate = dataFood.First().Rate,
    //            NumberRate = dataFood.First().NumberRate,
    //            Urlimage = dataFood.First().Urlimage,
    //            FoodTypeId = dataFood.First().FoodTypeId,
    //            KeyNoteId = dataFood.First().KeyNoteId,
    //            IsActive = dataFood.First().IsActive,
    //            PreparationTime = dataFood.First().PreparationTime,
    //            CookingTime = dataFood.First().CookingTime,
    //            CookingDifficultyId = dataFood.First().CookingDifficultyId,
    //            IngredientDetails100gReduceDTO = new Ingredientdetails100greducedto()
    //            {
    //                Id = -1,
    //                KeyNoteId = -1,
    //                Name = "SummaryOfTheEntireList",
    //                Describe = "SummaryOfTheEntireList",
    //                Urlimage = "SummaryOfTheEntireList",
    //                TypeOfCalculationId = -1,
    //                Energy = dataFood.Sum(x => x.IngredientDetails100gReduceDTO.Energy),
    //                Protein = dataFood.Sum(x => x.IngredientDetails100gReduceDTO.Protein),
    //                Fat = dataFood.Sum(x => x.IngredientDetails100gReduceDTO.Fat),
    //                Carbohydrate = dataFood.Sum(x => x.IngredientDetails100gReduceDTO.Carbohydrate),
    //                Fiber = dataFood.Sum(x => x.IngredientDetails100gReduceDTO.Fiber),
    //                Sodium = dataFood.Sum(x => x.IngredientDetails100gReduceDTO.Sodium),
    //                Cholesterol = dataFood.Sum(x => x.IngredientDetails100gReduceDTO.Cholesterol)
    //            },
    //            KeyNote = new KeyNote
    //            {
    //                Id = dataFood.First().KeyNote.Id,
    //                KeyList = dataFood.First().KeyNote.KeyList
    //            },
    //            ScaleAmounts = new ScaleAmounts
    //            {
    //                FoodListId = dataFood.First().FoodListId,
    //                IngredientDetailsId = -1,
    //                ScaleAmount = -1
    //            }
    //        };
    //        return totalfoodListDTO;
    //    }


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
                    TotalCaloriesPerMeal = (float)Math.Round(rootObjectFoodList.Where(x => x.SlotOfTheDay == item.SlotOfTheDay).OrderBy(x => x.SettingDetail).ToArray().Sum(x => x.foodIdData.Sum(x => x.foodData.IngredientDetails100gDTO.Energy)), 2),
                    foodDataOfSlot = rootObjectFoodList.Where(x => x.SlotOfTheDay == item.SlotOfTheDay).OrderBy(x => x.OrderSettingDetail).ToArray()
                };
                slotBranchesData.Add(slotBranch);
            }
            return slotBranchesData;
        }


    }
}
