namespace SEP490_G87_Vita_Nutrient_System_API.Domain.ResponseModels
{
    public class UserDetailResponse
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
        public UserDetail DetailsInformation { get; set; }
        public class UserDetail
        {
            public string? Description { get; set; }

            public short? Height { get; set; }

            public short? Weight { get; set; }

            public short? Age { get; set; }

            public string? WantImprove { get; set; }

            public string? UnderlyingDisease { get; set; }

            public string? InforConfirmGood { get; set; }

            public string? InforConfirmBad { get; set; }

            public bool? IsPremium { get; set; }
        }
    }
}
