namespace SEP490_G87_Vita_Nutrient_System_Client.Models
{
    public class ListUserDetail
    {
        public int UserId { get; set; }
        public string DescribeYourself { get; set; }
        public int? Height { get; set; }
        public int? Weight { get; set; }
        public int? Age { get; set; }
        public string WantImprove { get; set; }
        public string UnderlyingDisease { get; set; }
        public string InforConfirmGood { get; set; }
        public string InforConfirmBad { get; set; }
        public bool? IsPremium { get; set; }
    }

}
