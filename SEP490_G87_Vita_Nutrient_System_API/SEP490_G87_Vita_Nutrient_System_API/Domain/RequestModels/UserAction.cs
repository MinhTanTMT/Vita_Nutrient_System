namespace SEP490_G87_Vita_Nutrient_System_API.Domain.RequestModels
{
    public class UserAction
    {
        public int UserId { get; set; }
        public int FoodId { get; set; }
    }

    public class UserActionRecur
    {
        public int UserId { get; set; }
        public int FoodId { get; set; }
        public short RecurId { get; set; }
    }

    public class UserActionRate
    {
        public int UserId { get; set; }
        public int FoodId { get; set; }
        public short Rate { get; set; }
    }
}
