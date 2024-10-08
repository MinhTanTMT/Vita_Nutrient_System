﻿using System;
using System.Collections.Generic;

namespace SEP490_G87_Vita_Nutrient_System_API.Models;

public partial class FoodAndDisease
{
    public int ListOfDiseasesId { get; set; }

    public int FoodListId { get; set; }

    public string? Describe { get; set; }

    public bool? IsGoodOrBad { get; set; }

    public virtual FoodList FoodList { get; set; } = null!;

    public virtual ListOfDisease ListOfDiseases { get; set; } = null!;
}
