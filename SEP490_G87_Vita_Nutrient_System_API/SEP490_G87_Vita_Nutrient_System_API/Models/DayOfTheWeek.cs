using System;
using System.Collections.Generic;

namespace SEP490_G87_Vita_Nutrient_System_API.Models;

public partial class DayOfTheWeek
{
    public short Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<MealSetting> MealSettings { get; set; } = new List<MealSetting>();

    public virtual ICollection<MealSettingsDetail> MealSettingsDetails { get; set; } = new List<MealSettingsDetail>();
}
