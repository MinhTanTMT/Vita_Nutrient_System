namespace SEP490_G87_Vita_Nutrient_System_API.Dtos
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string UrlImage { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserDetailsDTO UserDetail { get; set; }
    }
}
