using System;
using System.Collections.Generic;

namespace SEP490_G87_Vita_Nutrient_System_API.Models;

public partial class KeyNote
{
    public int Id { get; set; }

    public string? KeyList { get; set; }

    public virtual ICollection<FoodList> FoodLists { get; set; } = new List<FoodList>();
}
