using System;
using System.Collections.Generic;

namespace SEP490_G87_Vita_Nutrient_System_API.Models;

public partial class BankInformation
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string? BankAccount { get; set; }

    public string? TypeBank { get; set; }

    public bool? IsUsed { get; set; }

    public virtual User User { get; set; } = null!;
}
