namespace SEP490_G87_Vita_Nutrient_System_Client.Models
{
    public class UserListManagement
    {
        public int Id { get; set; }

        public int NutritionistId { get; set; }

        public int UserId { get; set; }

        public string? Describe { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public short? Rate { get; set; }

        public bool? IsDone { get; set; }

    }
}
