using System;
using System.Collections.Generic;

namespace SEP490_G87_Vita_Nutrient_System_API.Models;

public partial class MealSetting
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public short? DayOfTheWeekStartId { get; set; }

    public bool? SameScheduleEveryDay { get; set; }

    public virtual DayOfTheWeek? DayOfTheWeekStart { get; set; }

    public virtual User User { get; set; } = null!;
}
