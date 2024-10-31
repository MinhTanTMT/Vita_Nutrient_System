using System;
using System.Collections.Generic;

namespace SEP490_G87_Vita_Nutrient_System_API.Models
{
    public partial class KeyNote
    {
        public KeyNote()
        {
            FoodLists = new HashSet<FoodList>();
            IngredientDetails100gs = new HashSet<IngredientDetails100g>();
        }

        public int Id { get; set; }
        public string? KeyList { get; set; }

        public virtual ICollection<FoodList> FoodLists { get; set; }
        public virtual ICollection<IngredientDetails100g> IngredientDetails100gs { get; set; }
    }
}
