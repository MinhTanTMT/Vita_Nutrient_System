using System;
using System.Collections.Generic;

namespace SEP490_G87_Vita_Nutrient_System_API.Models;

public partial class FoodType
{
    public short FoodTypeId { get; set; }

    public string? Name { get; set; }

    public string? Describe { get; set; }

    public virtual ICollection<FoodList> FoodLists { get; set; } = new List<FoodList>();

    public virtual ICollection<MealSetting> MealSettings { get; set; } = new List<MealSetting>();

    public virtual ICollection<NutritionTargetsDaily> NutritionTargetsDailies { get; set; } = new List<NutritionTargetsDaily>();
}
