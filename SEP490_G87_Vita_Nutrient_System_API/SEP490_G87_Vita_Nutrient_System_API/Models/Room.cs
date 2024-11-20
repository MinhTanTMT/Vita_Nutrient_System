using System;
using System.Collections.Generic;

namespace SEP490_G87_Vita_Nutrient_System_API.Models;

public partial class Room
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int NutritionId { get; set; }

    public int UserId { get; set; }

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual User Nutrition { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
