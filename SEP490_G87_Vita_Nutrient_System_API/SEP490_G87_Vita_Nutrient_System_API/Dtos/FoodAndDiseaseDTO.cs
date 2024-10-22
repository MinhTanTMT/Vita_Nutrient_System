using System;
using System.Collections.Generic;

namespace SEP490_G87_Vita_Nutrient_System_API.Dtos;

public partial class FoodAndDiseaseDTO {

    public int ListOfDiseasesId { get; set; }

    public int FoodListId { get; set; }

    public string? Describe { get; set; }

    public bool? IsGoodOrBad { get; set; }

    public virtual FoodListDTO FoodList { get; set; } = null!;
}
