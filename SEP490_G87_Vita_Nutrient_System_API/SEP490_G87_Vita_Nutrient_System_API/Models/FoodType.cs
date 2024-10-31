using System;
using System.Collections.Generic;

namespace SEP490_G87_Vita_Nutrient_System_API.Models
{
    public partial class FoodType
    {
        public FoodType()
        {
            FoodLists = new HashSet<FoodList>();
            NutritionTargetsDailies = new HashSet<NutritionTargetsDaily>();
        }

        public short FoodTypeId { get; set; }
        public string? Name { get; set; }
        public string? Describe { get; set; }

        public virtual ICollection<FoodList> FoodLists { get; set; }
        public virtual ICollection<NutritionTargetsDaily> NutritionTargetsDailies { get; set; }
    }
}
