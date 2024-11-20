namespace SEP490_G87_Vita_Nutrient_System_Client.Models
{
    public class FoodSelection
    {
        public int UserId { get; set; }

        public int FoodListId { get; set; }

        public short? Rate { get; set; }

        public short? RecurringId { get; set; }

        public bool? IsBlock { get; set; }

        public bool? IsCollection { get; set; }

        public bool? IsLike { get; set; }
    }
}
