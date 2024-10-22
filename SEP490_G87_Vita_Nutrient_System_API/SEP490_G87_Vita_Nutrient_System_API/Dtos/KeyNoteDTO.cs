using System;
using System.Collections.Generic;

namespace SEP490_G87_Vita_Nutrient_System_API.Dtos;

public partial class KeyNoteDTO
{
    public int Id { get; set; }

    public string? KeyList { get; set; }

    public virtual ICollection<FoodListDTO> FoodLists { get; set; } = new List<FoodListDTO>();

    public virtual ICollection<IngredientDetails100gDTO> IngredientDetails100gs { get; set; } = new List<IngredientDetails100gDTO>();
}
