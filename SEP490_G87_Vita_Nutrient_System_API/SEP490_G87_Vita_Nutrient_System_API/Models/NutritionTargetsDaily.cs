using System;
using System.Collections.Generic;

namespace SEP490_G87_Vita_Nutrient_System_API.Models
{
    public partial class NutritionTargetsDaily
    {
        public NutritionTargetsDaily()
        {
            MealSettingsDetails = new HashSet<MealSettingsDetail>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public string? Title { get; set; }
        public short? Calories { get; set; }
        public short? CarbsMin { get; set; }
        public short? CarbsMax { get; set; }
        public short? FatsMin { get; set; }
        public short? FatsMax { get; set; }
        public short? ProteinMin { get; set; }
        public short? ProteinMax { get; set; }
        public short? MinimumFiber { get; set; }
        public bool? LimitDailySodium { get; set; }
        public bool? LimitDailyCholesterol { get; set; }
        public short ExerciseIntensityId { get; set; }
        public short FoodTypeIdWant { get; set; }
        public string? AvoidIngredient { get; set; }
        public bool? IsActive { get; set; }

        public virtual ExerciseIntensity ExerciseIntensity { get; set; } = null!;
        public virtual FoodType FoodTypeIdWantNavigation { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<MealSettingsDetail> MealSettingsDetails { get; set; }
    }
}
