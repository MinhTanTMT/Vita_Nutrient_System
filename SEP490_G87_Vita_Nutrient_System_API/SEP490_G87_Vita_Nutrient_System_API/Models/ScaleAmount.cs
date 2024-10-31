using System;
using System.Collections.Generic;

namespace SEP490_G87_Vita_Nutrient_System_API.Models
{
    public partial class ScaleAmount
    {
        public int FoodListId { get; set; }
        public int IngredientDetailsId { get; set; }
        public double? ScaleAmount1 { get; set; }

        public virtual FoodList FoodList { get; set; } = null!;
        public virtual IngredientDetails100g IngredientDetails { get; set; } = null!;
    }
}
