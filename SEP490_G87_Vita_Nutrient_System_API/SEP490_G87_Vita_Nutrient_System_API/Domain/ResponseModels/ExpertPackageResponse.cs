namespace SEP490_G87_Vita_Nutrient_System_API.Domain.ResponseModels
{
    public class ExpertPackageResponse
    {
        public short Id { get; set; }

        public string? Name { get; set; }

        public string? Describe { get; set; }

        public decimal? Price { get; set; }

        public short? Duration { get; set; }

    }

    public class ExpertPackageResponse1
    {
        public ExpertPackageResponse Package { get; set; }
        public List<User>? Nutrititonists {  get; set; }

        public class User
        {
            public int Id { get; set; }
            public string? Name { get; set; }
            public string? Account { get; set; }

        }

    }
}
