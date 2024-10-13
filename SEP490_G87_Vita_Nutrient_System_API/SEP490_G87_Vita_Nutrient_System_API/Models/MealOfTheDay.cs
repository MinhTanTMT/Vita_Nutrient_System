using System;
using System.Collections.Generic;

namespace SEP490_G87_Vita_Nutrient_System_API.Models;

public partial class MealOfTheDay
{
    public int Id { get; set; }

    public int NutritionRouteId { get; set; }

    public short? Slot1OfTheDayId { get; set; }

    public string? Slot1FoodListId { get; set; }

    public short? Slot2OfTheDayId { get; set; }

    public string? Slot2FoodListId { get; set; }

    public short? Slot3OfTheDayId { get; set; }

    public string? Slot3FoodListId { get; set; }

    public short? Slot4OfTheDayId { get; set; }

    public string? Slot4FoodListId { get; set; }

    public short? Slot5OfTheDayId { get; set; }

    public string? Slot5FoodListId { get; set; }

    public DateOnly? DateExecute { get; set; }

    public bool? IsDone { get; set; }

    public bool? IsEditByUser { get; set; }

    public virtual NutritionRoute NutritionRoute { get; set; } = null!;

    public virtual SlotOfTheDay? Slot1OfTheDay { get; set; }

    public virtual SlotOfTheDay? Slot2OfTheDay { get; set; }

    public virtual SlotOfTheDay? Slot3OfTheDay { get; set; }

    public virtual SlotOfTheDay? Slot4OfTheDay { get; set; }

    public virtual SlotOfTheDay? Slot5OfTheDay { get; set; }
}
