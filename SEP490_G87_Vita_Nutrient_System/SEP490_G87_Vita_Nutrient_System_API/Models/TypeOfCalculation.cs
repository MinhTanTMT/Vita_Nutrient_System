using System;
using System.Collections.Generic;

namespace SEP490_G87_Vita_Nutrient_System_API.Models;

public partial class TypeOfCalculation
{
    public short TypeId { get; set; }

    public string? CalculationForm { get; set; }

    public virtual ICollection<IngredientDetails100g> IngredientDetails100gs { get; set; } = new List<IngredientDetails100g>();
}
