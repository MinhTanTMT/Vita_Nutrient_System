using System;
using System.Collections.Generic;

namespace SEP490_G87_Vita_Nutrient_System_API.Models;

public partial class ArticleImage
{
    public int Id { get; set; }

    public int ArticleId { get; set; }

    public string ImagePath { get; set; } = null!;

    public virtual ArticlesNews Article { get; set; } = null!;
}
