using System;
using System.Collections.Generic;

namespace SEP490_G87_Vita_Nutrient_System_API.Dtos;

public partial class TypeOfCalculationDTO
{
    public short TypeId { get; set; }

    public string? CalculationForm { get; set; }

    public virtual ICollection<IngredientDetails100gDTO> IngredientDetails100gs { get; set; } = new List<IngredientDetails100gDTO>();
}
