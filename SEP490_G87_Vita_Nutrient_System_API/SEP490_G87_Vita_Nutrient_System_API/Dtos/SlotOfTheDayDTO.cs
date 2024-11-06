namespace SEP490_G87_Vita_Nutrient_System_API.Dtos
{
    public class SlotOfTheDayDTO
    {
        public short Id { get; set; }

        public string? Slot { get; set; }

        public TimeOnly? StartAt { get; set; }

        public TimeOnly? EndAt { get; set; }
    }
}
