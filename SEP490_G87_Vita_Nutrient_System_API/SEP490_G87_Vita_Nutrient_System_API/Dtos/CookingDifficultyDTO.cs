using System;
using System.Collections.Generic;

namespace SEP490_G87_Vita_Nutrient_System_API.Dtos;

public partial class CookingDifficultyDTO
{
    public short Id { get; set; }

    public string? Name { get; set; }

    //public virtual ICollection<FoodListDTO> FoodLists { get; set; } = new List<FoodListDTO>();

}
