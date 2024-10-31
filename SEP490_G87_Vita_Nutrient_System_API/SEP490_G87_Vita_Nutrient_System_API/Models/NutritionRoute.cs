using System;
using System.Collections.Generic;

namespace SEP490_G87_Vita_Nutrient_System_API.Models;

public partial class NutritionRoute
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int CreateById { get; set; }

    public string? Name { get; set; }

    public string? Describe { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public bool? IsDone { get; set; }

    public virtual User CreateBy { get; set; } = null!;

    public virtual ICollection<MealOfTheDay> MealOfTheDays { get; set; } = new List<MealOfTheDay>();

    public virtual User User { get; set; } = null!;
}
