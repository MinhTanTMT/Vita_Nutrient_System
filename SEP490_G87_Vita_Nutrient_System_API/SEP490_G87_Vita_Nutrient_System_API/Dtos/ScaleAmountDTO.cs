using System;
using System.Collections.Generic;

namespace SEP490_G87_Vita_Nutrient_System_API.Dtos;

public partial class ScaleAmountDTO
{
    public int FoodListId { get; set; }

    public int IngredientDetailsId { get; set; }

    public double? ScaleAmount1 { get; set; }

    public virtual IngredientDetails100gDTO FoodList { get; set; } = null!;

    public virtual FoodListDTO IngredientDetails { get; set; } = null!;
}
