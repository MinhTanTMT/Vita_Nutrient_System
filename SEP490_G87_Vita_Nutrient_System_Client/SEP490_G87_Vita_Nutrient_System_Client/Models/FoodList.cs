using System;
using System.Collections.Generic;

namespace SEP490_G87_Vita_Nutrient_System_Client.Models;

public class RootObjectFoodList
{
    public FoodList[] Property1 { get; set; }
}

public class FoodList
{
    public int foodListId { get; set; }
    public string name { get; set; }
    public string describe { get; set; }
    public float rate { get; set; }
    public int numberRate { get; set; }
    public object urlimage { get; set; }
    public int foodTypeId { get; set; }
    public int keyNoteId { get; set; }
    public bool isActive { get; set; }
    public int preparationTime { get; set; }
    public int cookingTime { get; set; }
    public int cookingDifficultyId { get; set; }
    public Ingredientdetails100greducedto ingredientDetails100gReduceDTO { get; set; }
    public Scaleamounts scaleAmounts { get; set; }
    public object[] foodAndDiseases { get; set; }
    public object[] foodSelections { get; set; }
    public Keynote keyNote { get; set; }
}

public class Ingredientdetails100greducedto
{
    public int id { get; set; }
    public int keyNoteId { get; set; }
    public string name { get; set; }
    public string describe { get; set; }
    public string urlimage { get; set; }
    public int typeOfCalculationId { get; set; }
    public float energy { get; set; }
    public float protein { get; set; }
    public float fat { get; set; }
    public float carbohydrate { get; set; }
    public float fiber { get; set; }
    public float sodium { get; set; }
    public float cholesterol { get; set; }
}

public class Scaleamounts
{
    public int foodListId { get; set; }
    public int ingredientDetailsId { get; set; }
    public int scaleAmount1 { get; set; }
}

public class Keynote
{
    public int id { get; set; }
    public string keyList { get; set; }
}

