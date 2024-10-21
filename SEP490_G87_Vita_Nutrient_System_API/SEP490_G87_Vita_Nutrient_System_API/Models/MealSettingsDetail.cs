using System;
using System.Collections.Generic;

namespace SEP490_G87_Vita_Nutrient_System_API.Models;

public partial class MealSettingsDetail
{
    public int Id { get; set; }

    public int MealSettingsId { get; set; }

    public short? SlotOfTheDayId { get; set; }

    public int? NutritionTargetsDailyId { get; set; }

    public short? DayOfTheWeekId { get; set; }

    public bool? SkipCreationProcess { get; set; }

    public string? Size { get; set; }

    public bool? NutritionFocus { get; set; }

    public short? NumberOfDishes { get; set; }

    public string? TypeFavoriteFood { get; set; }

    public short? WantCookingId { get; set; }

    public short? TimeAvailable { get; set; }

    public short? CookingDifficultyId { get; set; }

    public virtual CookingDifficulty? CookingDifficulty { get; set; }

    public virtual DayOfTheWeek? DayOfTheWeek { get; set; }

    public virtual MealSetting MealSettings { get; set; } = null!;

    public virtual NutritionTargetsDaily? NutritionTargetsDaily { get; set; }

    public virtual SlotOfTheDay? SlotOfTheDay { get; set; }

    public virtual WantCooking? WantCooking { get; set; }
}
