using SEP490_G87_Vita_Nutrient_System_API.Models;
using System;
using System.Collections.Generic;

namespace SEP490_G87_Vita_Nutrient_System_API.Dtos;

public partial class FoodListDTO
{
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

    public virtual IngredientDetails100gReduceDTO ingredientDetails100gReduceDTO { get; set; } = null!;
    public virtual ScaleAmountDTO ScaleAmounts { get; set; } = null!;

    //public virtual CookingDifficultyDTO? CookingDifficulty { get; set; }

    public virtual ICollection<FoodAndDiseaseDTO> FoodAndDiseases { get; set; } = new List<FoodAndDiseaseDTO>();

    public virtual ICollection<FoodSelectionDTO> FoodSelections { get; set; } = new List<FoodSelectionDTO>();

    //public virtual FoodType FoodType { get; set; } = null!;

    public virtual KeyNoteDTO? KeyNote { get; set; }

    //public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();

    //public virtual ICollection<ScaleAmountDTO> ScaleAmounts { get; set; } = new List<ScaleAmountDTO>();


}
