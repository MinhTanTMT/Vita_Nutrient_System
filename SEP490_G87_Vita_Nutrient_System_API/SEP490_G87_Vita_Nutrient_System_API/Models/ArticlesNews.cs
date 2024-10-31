using System;
using System.Collections.Generic;

namespace SEP490_G87_Vita_Nutrient_System_API.Models;

public partial class ArticlesNews
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string? NameCreater { get; set; }

    public string? Title { get; set; }

    public string? Content { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? DateCreated { get; set; }

    public string? HeaderImage { get; set; }

    public double? Rate { get; set; }

    public int? NumberRate { get; set; }

    public virtual ICollection<NewsEvaluation> NewsEvaluations { get; set; } = new List<NewsEvaluation>();

    public virtual User User { get; set; } = null!;
}
