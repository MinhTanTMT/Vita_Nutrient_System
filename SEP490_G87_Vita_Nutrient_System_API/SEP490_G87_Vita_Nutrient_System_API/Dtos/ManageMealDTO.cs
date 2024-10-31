namespace SEP490_G87_Vita_Nutrient_System_API.Dtos
{
    // Represents a meal setting with associated details
    public class MealSettingsDto
    {
        public int? Id { get; set; }
        public short? DayOfTheWeekStartId { get; set; }
        public bool? SameScheduleEveryDay { get; set; }
        public List<MealSettingsDetailsDto>? MealDetails { get; set; }
    }

    // Represents detailed information of a specific meal setting
    public class MealSettingsDetailsDto
    {
        public int? Id { get; set; }
        public int? SlotOfTheDayId { get; set; }
        public int? DayOfTheWeekId { get; set; }
        public int? CookingDifficultyId { get; set; }
        public string? Size { get; set; }
        public bool? NutritionFocus { get; set; }
        public string? TypeFavoriteFood { get; set; }
    }

    // Used when creating a new meal setting, including details
    public class MealSettingsCreateDto
    {
        public int UserId { get; set; }
        public short? DayOfTheWeekStartId { get; set; }
        public bool? SameScheduleEveryDay { get; set; }
        public List<MealSettingsDetailsCreateDto>? MealDetails { get; set; }
    }

    // Represents details for creating a new meal setting
    public class MealSettingsDetailsCreateDto
    {
        public short? SlotOfTheDayId { get; set; }
        public short? DayOfTheWeekId { get; set; }
        public short? CookingDifficultyId { get; set; }
        public string? Size { get; set; }
        public bool? NutritionFocus { get; set; }
        public string? TypeFavoriteFood { get; set; }
    }

    // Used for updating an existing meal setting, including details
    public class MealSettingsUpdateDto
    {
        public short? DayOfTheWeekStartId { get; set; }
        public bool? SameScheduleEveryDay { get; set; }
        public List<MealSettingsDetailsUpdateDto>? MealDetails { get; set; }
    }

    // Represents details for updating an existing meal setting
    public class MealSettingsDetailsUpdateDto
    {
        public int? Id { get; set; }
        public short? SlotOfTheDayId { get; set; }
        public short? DayOfTheWeekId { get; set; }
        public short? CookingDifficultyId { get; set; }
        public string? Size { get; set; }
        public bool? NutritionFocus { get; set; }
        public string? TypeFavoriteFood { get; set; }
    }

}
