using System;
using System.Collections.Generic;

namespace SEP490_G87_Vita_Nutrient_System_API.Models;

public partial class DietType
{
    public short DietTypeId { get; set; }

    public string? Name { get; set; }

    public string? Describe { get; set; }

    public virtual ICollection<MealSetting> MealSettings { get; set; } = new List<MealSetting>();

    public virtual ICollection<NutritionTargetsDaily> NutritionTargetsDailies { get; set; } = new List<NutritionTargetsDaily>();

    public virtual ICollection<FoodType> FoodTypes { get; set; } = new List<FoodType>();
}
