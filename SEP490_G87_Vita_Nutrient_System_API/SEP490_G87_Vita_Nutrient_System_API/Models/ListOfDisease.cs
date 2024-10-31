using System;
using System.Collections.Generic;

namespace SEP490_G87_Vita_Nutrient_System_API.Models;

public partial class ListOfDisease
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Describe { get; set; }

    public virtual ICollection<FoodAndDisease> FoodAndDiseases { get; set; } = new List<FoodAndDisease>();
}
