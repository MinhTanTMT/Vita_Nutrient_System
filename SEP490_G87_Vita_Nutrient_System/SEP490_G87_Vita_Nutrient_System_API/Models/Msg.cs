using System;
using System.Collections.Generic;

namespace SEP490_G87_Vita_Nutrient_System_API.Models;

public partial class Msg
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string? MessageType { get; set; }

    public string? MessageContent { get; set; }

    public DateTime? SendDateTime { get; set; }

    public bool? IsSent { get; set; }

    public bool? SendResult { get; set; }

    public string? ErrorMessage { get; set; }

    public virtual User User { get; set; } = null!;
}
