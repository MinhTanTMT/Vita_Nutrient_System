namespace SEP490_G87_Vita_Nutrient_System_API.Dtos
{
    public class SaveFoodDTO
    {
        public int FoodListId { get; set; }
        public string? Name { get; set; }

        public string? Describe { get; set; }

        public double? Rate { get; set; }

        public int? NumberRate { get; set; }

        public string? Urlimage { get; set; }
        public short FoodTypeId { get; set; }

        public int? KeyNoteId { get; set; }

        public bool? IsActive { get; set; }

        public short? PreparationTime { get; set; }

        public short? CookingTime { get; set; }

        public short? CookingDifficultyId { get; set; }
    }
}
