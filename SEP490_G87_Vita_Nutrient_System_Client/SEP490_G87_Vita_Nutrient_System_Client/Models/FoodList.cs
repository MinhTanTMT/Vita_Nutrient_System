
﻿using System;
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
        public Ingredientdetails100greducedto IngredientDetails100gReduceDTO { get; set; }
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

    public class ScaleAmounts
    {
        public int FoodListId { get; set; }
        public int IngredientDetailsId { get; set; }
        public int ScaleAmount { get; set; }
    }

}
