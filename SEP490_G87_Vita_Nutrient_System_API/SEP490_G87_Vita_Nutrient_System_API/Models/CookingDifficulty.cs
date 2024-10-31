using System;
using System.Collections.Generic;

namespace SEP490_G87_Vita_Nutrient_System_API.Models;

public partial class CookingDifficulty
{
    public short Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<FoodList> FoodLists { get; set; } = new List<FoodList>();

    public virtual ICollection<MealSettingsDetail> MealSettingsDetails { get; set; } = new List<MealSettingsDetail>();
}
