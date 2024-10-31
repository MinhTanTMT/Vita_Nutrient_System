using System;
using System.Collections.Generic;

namespace SEP490_G87_Vita_Nutrient_System_API.Models
{
    public partial class MealOfTheDay
    {
        public int Id { get; set; }
        public int NutritionRouteId { get; set; }
        public string? DataFoodListId { get; set; }
        public DateTime? DateExecute { get; set; }
        public bool? IsDone { get; set; }
        public bool? IsEditByUser { get; set; }

        public virtual NutritionRoute NutritionRoute { get; set; } = null!;
    }
}
