﻿using System;
using System.Collections.Generic;

namespace SEP490_G87_Vita_Nutrient_System_API.Dtos;

public partial class FoodSelectionDTO
{
    public int UserId { get; set; }

    public int FoodListId { get; set; }

    public short? Rate { get; set; }

    public short? RecurringId { get; set; }

    public bool? IsBlock { get; set; }

    public bool? IsCollection { get; set; }

    public bool? IsLike { get; set; }

    //public virtual FoodListDTO FoodList { get; set; } = null!;

    //public virtual RecurringSetting? Recurring { get; set; }

    //public virtual User User { get; set; } = null!;
}