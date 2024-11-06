using System;
using System.Collections.Generic;

namespace SEP490_G87_Vita_Nutrient_System_API.Models;

public partial class SlotOfTheDay
{
    public short Id { get; set; }

    public string? Slot { get; set; }

    public TimeOnly? StartAt { get; set; }

    public TimeOnly? EndAt { get; set; }

    public virtual ICollection<MealSettingsDetail> MealSettingsDetails { get; set; } = new List<MealSettingsDetail>();
}
