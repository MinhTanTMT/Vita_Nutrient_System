namespace SEP490_G87_Vita_Nutrient_System_API.Domain.ResponseModels
{
    public class CommonUserResponse
    {
        public int Id { get; set; }

        public string? Urlimage { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public DateTime? Dob { get; set; }

        public bool? Gender { get; set; }

        public string? Address { get; set; }

        public string? Phone { get; set; }

        public bool? IsActive { get; set; }

        public string Account { get; set; } = null!;
        public UserRole Role { get; set; }
    }
    public class UserRole
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
