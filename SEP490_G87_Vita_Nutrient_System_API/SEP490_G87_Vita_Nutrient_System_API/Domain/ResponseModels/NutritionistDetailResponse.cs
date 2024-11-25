namespace SEP490_G87_Vita_Nutrient_System_API.Domain.ResponseModels
{
    public class NutritionistDetailResponse
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
        public NutritionistDetail DetailsInformation { get; set; }

        public class NutritionistDetail
        {
            public int Id { get; set; }

            public string? Description { get; set; }

            public short? Height { get; set; }

            public short? Weight { get; set; }

            public short? Age { get; set; }

            public double? Rate { get; set; }

            public int? NumberRate { get; set; }
            public short? ExpertPackagesId { get; set; }
        }

    }
}
