namespace SEP490_G87_Vita_Nutrient_System_API.Domain.ResponseModels
{
    public class FoodResponse
    {
        public int FoodId { get; set; }

        public string? Name { get; set; }

        public string? Describe { get; set; }

        public double? Rate { get; set; }

        public int? NumberRate { get; set; }

        public string? Urlimage { get; set; }

        public FoodTypeResponse FoodType { get; set; }

        public int? KeyNoteId { get; set; }

        public bool? IsActive { get; set; }

        public short? PreparationTime { get; set; }

        public short? CookingTime { get; set; }

        public short? CookingDifficultyId { get; set; }
    }

    public class FoodTypeResponse
    {
        public short FoodTypeId { get; set; }

        public string? Name { get; set; }

        public string? Describe { get; set; }
    }
}
