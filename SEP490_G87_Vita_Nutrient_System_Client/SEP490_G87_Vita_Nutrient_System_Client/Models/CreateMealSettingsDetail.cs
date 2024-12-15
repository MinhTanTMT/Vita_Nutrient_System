using System.ComponentModel.DataAnnotations;

namespace SEP490_G87_Vita_Nutrient_System_Client.Models
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
        [Required(ErrorMessage = "Tên bữa ăn không được để trống.")]
        [MaxLength(50, ErrorMessage = "Tên bữa ăn không được vượt quá 50 ký tự.")]
        public string? Name { get; set; }
        public short? Calories { get; set; }

        public short? CarbsMin { get; set; }

        public short? CarbsMax { get; set; }

        public short? FatsMin { get; set; }

        public short? FatsMax { get; set; }

        public short? ProteinMin { get; set; }

        public short? ProteinMax { get; set; }

        public short? MinimumFiber { get; set; }

        public string? CookingDifficulty { get; set; }
        public string? SlotOfTheDay { get; set; }
        public string? WantCooking { get; set; }
    }
}
