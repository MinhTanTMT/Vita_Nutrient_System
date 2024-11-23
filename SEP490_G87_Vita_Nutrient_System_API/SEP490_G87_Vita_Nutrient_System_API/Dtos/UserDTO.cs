namespace SEP490_G87_Vita_Nutrient_System_API.Dtos
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string UrlImage { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public UserDetailsDTO UserDetail { get; set; }
        public short? Height { get; set; }

        public short? Weight { get; set; }

        public short? Age { get; set; }
        public string? Phone { get; set; }

        /*public string? UnderlyingDisease { get; set; }*/
        public List<string>? UnderlyingDiseaseNames { get; set; }
    }
}
