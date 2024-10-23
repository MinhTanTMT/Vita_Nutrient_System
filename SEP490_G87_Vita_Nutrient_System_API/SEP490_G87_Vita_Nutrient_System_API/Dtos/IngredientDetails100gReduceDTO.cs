using System;
using System.Collections.Generic;

namespace SEP490_G87_Vita_Nutrient_System_API.Dtos;

public partial class IngredientDetails100gReduceDTO
{
    public int Id { get; set; }

    public int? KeyNoteId { get; set; }

    public string? Name { get; set; }

    public string? Describe { get; set; }

    public string? Urlimage { get; set; }

    public short TypeOfCalculationId { get; set; }

    public double? Energy { get; set; }

    public double? Protein { get; set; }

    public double? Fat { get; set; }

    public double? Carbohydrate { get; set; }

    public double? Fiber { get; set; }

    public double? Sodium { get; set; }

    public double? Cholesterol { get; set; }


}
