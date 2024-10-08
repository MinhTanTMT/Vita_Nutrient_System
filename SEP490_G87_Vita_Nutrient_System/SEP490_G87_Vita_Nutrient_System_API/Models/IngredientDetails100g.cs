using System;
using System.Collections.Generic;

namespace SEP490_G87_Vita_Nutrient_System_API.Models;

public partial class IngredientDetails100g
{
    public int Id { get; set; }

    public int? KeyNoteId { get; set; }

    public string? Name { get; set; }

    public string? Describe { get; set; }

    public string? Urlimage { get; set; }

    public short TypeOfCalculationId { get; set; }

    public short? Energy { get; set; }

    public double? Water { get; set; }

    public double? Protein { get; set; }

    public double? Fat { get; set; }

    public double? Carbohydrate { get; set; }

    public double? Fiber { get; set; }

    public double? Ash { get; set; }

    public double? Sugar { get; set; }

    public double? Galactose { get; set; }

    public double? Maltose { get; set; }

    public double? Lactose { get; set; }

    public double? Fructose { get; set; }

    public double? Glucose { get; set; }

    public double? Sucrose { get; set; }

    public double? Calcium { get; set; }

    public double? Iron { get; set; }

    public double? Magnesium { get; set; }

    public double? Manganese { get; set; }

    public double? Phosphorous { get; set; }

    public double? Potassium { get; set; }

    public double? Sodium { get; set; }

    public double? Zinc { get; set; }

    public double? Copper { get; set; }

    public double? Selenium { get; set; }

    public double? VitaminC { get; set; }

    public double? VitaminB1 { get; set; }

    public double? VitaminB2 { get; set; }

    public double? VitaminPp { get; set; }

    public double? VitaminB5 { get; set; }

    public double? VitaminB6 { get; set; }

    public double? Folat { get; set; }

    public double? VitaminB9 { get; set; }

    public double? VitaminH { get; set; }

    public double? VitaminB12 { get; set; }

    public double? VitaminA { get; set; }

    public double? VitaminD { get; set; }

    public double? VitaminE { get; set; }

    public double? VitaminK { get; set; }

    public double? BetaCaroten { get; set; }

    public double? AlphaCaroten { get; set; }

    public double? BetaCryptoxanthin { get; set; }

    public double? Lycopen { get; set; }

    public double? LuteinVsZeaxanthin { get; set; }

    public double? Purin { get; set; }

    public double? TotalIsoflavone { get; set; }

    public double? Daidzein { get; set; }

    public double? Genistein { get; set; }

    public double? Glycetin { get; set; }

    public double? TotalSaturatedFattyAcid { get; set; }

    public double? PalmiticC160 { get; set; }

    public double? MargaricC170 { get; set; }

    public double? StearicC180 { get; set; }

    public double? ArachidicC200 { get; set; }

    public double? BehenicC220 { get; set; }

    public double? LignocericC240 { get; set; }

    public double? TotalMonounsaturatedFattyAcid { get; set; }

    public double? MyristoleicC141 { get; set; }

    public double? PalmitoleicC161 { get; set; }

    public double? OleicC181 { get; set; }

    public double? TotalPolyunsaturatedFattyAcid { get; set; }

    public double? LinoleicC182N6 { get; set; }

    public double? LinolenicC182N3 { get; set; }

    public double? ArachidonicC204 { get; set; }

    public double? EicosapentaenoicC205N3 { get; set; }

    public double? DocosahexaenoicC226N3 { get; set; }

    public double? TotalTransFattyAcid { get; set; }

    public double? Cholesterol { get; set; }

    public double? Phytosterol { get; set; }

    public double? Lysin { get; set; }

    public double? Methionin { get; set; }

    public double? Tryptophan { get; set; }

    public double? Phenylalanin { get; set; }

    public double? Threonin { get; set; }

    public double? Valin { get; set; }

    public double? Leucin { get; set; }

    public double? Isoleucin { get; set; }

    public double? Arginin { get; set; }

    public double? Histidin { get; set; }

    public double? Cystin { get; set; }

    public double? Tyrosin { get; set; }

    public double? Alanin { get; set; }

    public double? AcidAspartic { get; set; }

    public double? AcidGlutamic { get; set; }

    public double? Glycin { get; set; }

    public double? Prolin { get; set; }

    public double? Serin { get; set; }

    public virtual ICollection<ScaleAmount> ScaleAmounts { get; set; } = new List<ScaleAmount>();

    public virtual TypeOfCalculation TypeOfCalculation { get; set; } = null!;
}
