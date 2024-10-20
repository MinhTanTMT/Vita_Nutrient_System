using System;
using System.Collections.Generic;

namespace SEP490_G87_Vita_Nutrient_System_API.Models;

public partial class SlotOfTheDay
{
    public short Id { get; set; }

    public string? Slot { get; set; }

    public TimeOnly? StartAt { get; set; }

    public TimeOnly? EndAt { get; set; }

    public virtual ICollection<MealOfTheDay> MealOfTheDaySlot1OfTheDays { get; set; } = new List<MealOfTheDay>();

    public virtual ICollection<MealOfTheDay> MealOfTheDaySlot2OfTheDays { get; set; } = new List<MealOfTheDay>();

    public virtual ICollection<MealOfTheDay> MealOfTheDaySlot3OfTheDays { get; set; } = new List<MealOfTheDay>();

    public virtual ICollection<MealOfTheDay> MealOfTheDaySlot4OfTheDays { get; set; } = new List<MealOfTheDay>();

    public virtual ICollection<MealOfTheDay> MealOfTheDaySlot5OfTheDays { get; set; } = new List<MealOfTheDay>();

    public virtual ICollection<MealSettingsDetail> MealSettingsDetails { get; set; } = new List<MealSettingsDetail>();
}
