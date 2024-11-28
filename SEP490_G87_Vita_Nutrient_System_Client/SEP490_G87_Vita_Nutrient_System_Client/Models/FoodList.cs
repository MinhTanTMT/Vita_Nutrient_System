
using System;
using System.Collections.Generic;

namespace SEP490_G87_Vita_Nutrient_System_Client.Models
{
    public class FoodList
    {
        public int FoodListId { get; set; }
        public string Name { get; set; }
        public string Describe { get; set; }
        public float Rate { get; set; }
        public int NumberRate { get; set; }
        public string Urlimage { get; set; }
        public int FoodTypeId { get; set; }
        public int KeyNoteId { get; set; }
        public bool IsActive { get; set; }
        public int PreparationTime { get; set; }
        public int CookingTime { get; set; }
        public int CookingDifficultyId { get; set; }
        public IngredientDetails100gDTO IngredientDetails100gDTO { get; set; }
        public ScaleAmounts ScaleAmounts { get; set; }
        public object[] FoodAndDiseases { get; set; }
        public object[] FoodSelections { get; set; }
        public KeyNote? KeyNote { get; set; }
        public FoodType FoodType { get; set; }
    }

    public class Ingredientdetails100greducedto
    {
        public int Id { get; set; }
        public int KeyNoteId { get; set; }
        public string Name { get; set; }
        public string Describe { get; set; }
        public string Urlimage { get; set; }
        public int TypeOfCalculationId { get; set; }
        public float Energy { get; set; }
        public float Protein { get; set; }
        public float Fat { get; set; }
        public float Carbohydrate { get; set; }
        public float Fiber { get; set; }
        public float Sodium { get; set; }
        public float Cholesterol { get; set; }
    }



    public class IngredientDetails100gDTO
    {
        public int Id { get; set; }

        public int KeyNoteId { get; set; }

        public string Name { get; set; }

        public string Describe { get; set; }

        public string Urlimage { get; set; }

        public short TypeOfCalculationId { get; set; }

        //public short Energy { get; set; }

        public float Energy { get; set; }

        public float Water { get; set; }

        public float Protein { get; set; }

        public float Fat { get; set; }

        public float Carbohydrate { get; set; }

        public float Fiber { get; set; }

        public float Ash { get; set; }

        public float Sugar { get; set; }

        public float Galactose { get; set; }

        public float Maltose { get; set; }

        public float Lactose { get; set; }

        public float Fructose { get; set; }

        public float Glucose { get; set; }

        public float Sucrose { get; set; }

        public float Calcium { get; set; }

        public float Iron { get; set; }

        public float Magnesium { get; set; }

        public float Manganese { get; set; }

        public float Phosphorous { get; set; }

        public float Potassium { get; set; }

        public float Sodium { get; set; }

        public float Zinc { get; set; }

        public float Copper { get; set; }

        public float Selenium { get; set; }

        public float VitaminC { get; set; }

        public float VitaminB1 { get; set; }

        public float VitaminB2 { get; set; }

        public float VitaminPp { get; set; }

        public float VitaminB5 { get; set; }

        public float VitaminB6 { get; set; }

        public float Folat { get; set; }

        public float VitaminB9 { get; set; }

        public float VitaminH { get; set; }

        public float VitaminB12 { get; set; }

        public float VitaminA { get; set; }

        public float VitaminD { get; set; }

        public float VitaminE { get; set; }

        public float VitaminK { get; set; }

        public float BetaCaroten { get; set; }

        public float AlphaCaroten { get; set; }

        public float BetaCryptoxanthin { get; set; }

        public float Lycopen { get; set; }

        public float LuteinVsZeaxanthin { get; set; }

        public float Purin { get; set; }

        public float TotalIsoflavone { get; set; }

        public float Daidzein { get; set; }

        public float Genistein { get; set; }

        public float Glycetin { get; set; }

        public float TotalSaturatedFattyAcid { get; set; }

        public float PalmiticC160 { get; set; }

        public float MargaricC170 { get; set; }

        public float StearicC180 { get; set; }

        public float ArachidicC200 { get; set; }

        public float BehenicC220 { get; set; }

        public float LignocericC240 { get; set; }

        public float TotalMonounsaturatedFattyAcid { get; set; }

        public float MyristoleicC141 { get; set; }

        public float PalmitoleicC161 { get; set; }

        public float OleicC181 { get; set; }

        public float TotalPolyunsaturatedFattyAcid { get; set; }

        public float LinoleicC182N6 { get; set; }

        public float LinolenicC182N3 { get; set; }

        public float ArachidonicC204 { get; set; }

        public float EicosapentaenoicC205N3 { get; set; }

        public float DocosahexaenoicC226N3 { get; set; }

        public float TotalTransFattyAcid { get; set; }

        public float Cholesterol { get; set; }

        public float Phytosterol { get; set; }

        public float Lysin { get; set; }

        public float Methionin { get; set; }

        public float Tryptophan { get; set; }

        public float Phenylalanin { get; set; }

        public float Threonin { get; set; }

        public float Valin { get; set; }

        public float Leucin { get; set; }

        public float Isoleucin { get; set; }

        public float Arginin { get; set; }

        public float Histidin { get; set; }

        public float Cystin { get; set; }

        public float Tyrosin { get; set; }

        public float Alanin { get; set; }

        public float AcidAspartic { get; set; }

        public float AcidGlutamic { get; set; }

        public float Glycin { get; set; }

        public float Prolin { get; set; }

        public float Serin { get; set; }
    }





    public class ScaleAmounts
    {
        public int FoodListId { get; set; }
        public int IngredientDetailsId { get; set; }
        public int ScaleAmount { get; set; }
    }








}

