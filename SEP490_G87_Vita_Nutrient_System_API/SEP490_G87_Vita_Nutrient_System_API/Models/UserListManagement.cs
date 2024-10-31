using System;
using System.Collections.Generic;

namespace SEP490_G87_Vita_Nutrient_System_API.Models
{
    public partial class UserListManagement
    {
        public int Id { get; set; }
        public int NutritionistId { get; set; }
        public int UserId { get; set; }
        public string? Describe { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public short? Rate { get; set; }
        public bool? IsDone { get; set; }

        public virtual User Nutritionist { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
