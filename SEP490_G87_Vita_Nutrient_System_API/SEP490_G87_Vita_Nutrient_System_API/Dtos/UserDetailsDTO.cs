namespace SEP490_G87_Vita_Nutrient_System_API.Dtos
{
    public class UserDetailsDTO
    {
        public int UserId { get; set; }
        public string DescribeYourself { get; set; }
        public short? Height { get; set; }
        public short? Weight { get; set; }
        public short? Age { get; set; }
        public string WantImprove { get; set; }
        public string UnderlyingDisease { get; set; }
        public string InforConfirmGood { get; set; }
        public string InforConfirmBad { get; set; }
        public bool? IsPremium { get; set; }
    }

}
