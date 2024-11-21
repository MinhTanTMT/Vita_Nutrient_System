using System;
using System.Collections.Generic;

namespace SEP490_G87_Vita_Nutrient_System_API.Models;

public partial class Message
{
    public int Id { get; set; }

    public string? Content { get; set; }

    public DateTime? Timestamp { get; set; }

    public int FromUserId { get; set; }

    public int ToRoomId { get; set; }

    public virtual User FromUser { get; set; } = null!;

    public virtual Room ToRoom { get; set; } = null!;
}
