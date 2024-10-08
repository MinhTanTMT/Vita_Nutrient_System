﻿using System;
using System.Collections.Generic;

namespace SEP490_G87_Vita_Nutrient_System_API.Models;

public partial class ExpertPackage
{
    public short Id { get; set; }

    public int NutritionistDetailsId { get; set; }

    public string? Name { get; set; }

    public string? Describe { get; set; }

    public decimal? Price { get; set; }

    public short? Duration { get; set; }

    public virtual NutritionistDetail NutritionistDetails { get; set; } = null!;
}
