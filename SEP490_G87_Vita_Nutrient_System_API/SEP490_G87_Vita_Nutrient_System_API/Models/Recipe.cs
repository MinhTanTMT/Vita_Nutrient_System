using System;
using System.Collections.Generic;

namespace SEP490_G87_Vita_Nutrient_System_API.Models
{
    public partial class Recipe
    {
        public int RecipeId { get; set; }
        public int FoodListId { get; set; }
        public short? NumericalOrder { get; set; }
        public string? Describe { get; set; }
        public string? Urlimage { get; set; }

        public virtual FoodList FoodList { get; set; } = null!;
    }
}
