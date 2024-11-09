namespace SEP490_G87_Vita_Nutrient_System_Client.Models
{
    public class ApiResponse
    {
        public List<UserItem> Items { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
    }
}
