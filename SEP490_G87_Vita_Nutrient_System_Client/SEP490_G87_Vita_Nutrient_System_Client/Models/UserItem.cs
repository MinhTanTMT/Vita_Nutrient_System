namespace SEP490_G87_Vita_Nutrient_System_Client.Models
{
    public class UserItem
    {
        public int UserId { get; set; }
        public string UrlImage { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ListUserDetail UserDetail { get; set; }
    }
}
