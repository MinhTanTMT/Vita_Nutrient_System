using System;
using System.Collections.Generic;

namespace SEP490_G87_Vita_Nutrient_System_API.Models
{
    public partial class FoodList
    {
        public FoodList()
        {
            FoodAndDiseases = new HashSet<FoodAndDisease>();
            FoodSelections = new HashSet<FoodSelection>();
            Recipes = new HashSet<Recipe>();
            ScaleAmounts = new HashSet<ScaleAmount>();
        }

        public int FoodListId { get; set; }
        public string? Name { get; set; }
        public string? Describe { get; set; }
        public double? Rate { get; set; }
        public int? NumberRate { get; set; }
        public string? Urlimage { get; set; }
        public short FoodTypeId { get; set; }
        public int? KeyNoteId { get; set; }
        public bool? IsActive { get; set; }
        public short? PreparationTime { get; set; }
        public short? CookingTime { get; set; }
        public short? CookingDifficultyId { get; set; }

        public virtual CookingDifficulty? CookingDifficulty { get; set; }
        public virtual FoodType FoodType { get; set; } = null!;
        public virtual KeyNote? KeyNote { get; set; }
        public virtual ICollection<FoodAndDisease> FoodAndDiseases { get; set; }
        public virtual ICollection<FoodSelection> FoodSelections { get; set; }
        public virtual ICollection<Recipe> Recipes { get; set; }
        public virtual ICollection<ScaleAmount> ScaleAmounts { get; set; }
    }
}
