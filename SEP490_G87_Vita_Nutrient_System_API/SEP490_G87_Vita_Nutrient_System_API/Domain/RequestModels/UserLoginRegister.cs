namespace SEP490_G87_Vita_Nutrient_System_API.Domain.RequestModels
{
    public class UserLoginRegister
    {
        public int UserId { get; set; }

        public string? Urlimage { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public DateTime? Dob { get; set; }

        public bool? Gender { get; set; }

        public string? Address { get; set; }

        public string? Phone { get; set; }

        public string Password { get; set; } = null!;

        public short Role { get; set; }

        public bool? IsActive { get; set; }

        public string Account { get; set; } = null!;

        public string? AccountGoogle { get; set; }

        public string? FullName { get; set; }
        public string? RoleName { get; set; }

    }
}
