using System;
using System.Collections.Generic;

namespace SEP490_G87_Vita_Nutrient_System_API.Models
{
    public partial class UserDetail
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? DescribeYourself { get; set; }
        public short? Height { get; set; }
        public short? Weight { get; set; }
        public short? Age { get; set; }
        public string? WantImprove { get; set; }
        public string? UnderlyingDisease { get; set; }
        public string? InforConfirmGood { get; set; }
        public string? InforConfirmBad { get; set; }
        public bool? IsPremium { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
