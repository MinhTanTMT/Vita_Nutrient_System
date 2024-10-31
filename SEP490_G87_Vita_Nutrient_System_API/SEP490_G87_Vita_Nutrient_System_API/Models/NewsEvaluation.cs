using System;
using System.Collections.Generic;

namespace SEP490_G87_Vita_Nutrient_System_API.Models;

public partial class NewsEvaluation
{
    public int Id { get; set; }

    public int ArticlesNewsId { get; set; }

    public int UserId { get; set; }

    public short? Ratting { get; set; }

    public virtual ArticlesNews ArticlesNews { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
