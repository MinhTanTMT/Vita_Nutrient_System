﻿namespace SEP490_G87_Vita_Nutrient_System_Client.Models
{
    public class CreateMealSettingsDetail
    {
        public int Id { get; set; }
        public int MealSettingsId { get; set; }
        public short SlotOfTheDayId { get; set; }
        public int? NutritionTargetsDailyId { get; set; }
        public short? DayOfTheWeekId { get; set; }
        public bool SkipCreationProcess { get; set; } = false;
        public string? Size { get; set; }
        public bool NutritionFocus { get; set; } = false;
        public short? NumberOfDishes { get; set; }
        public string? TypeFavoriteFood { get; set; }
        public short WantCookingId { get; set; }
        public short? TimeAvailable { get; set; }
        public short CookingDifficultyId { get; set; }
        public bool? IsActive { get; set; }
        public short? OrderNumber { get; set; }
        public string? Name { get; set; }

        public string? CookingDifficulty { get; set; }
        public string? SlotOfTheDay { get; set; }
        public string? WantCooking { get; set; }
    }
}