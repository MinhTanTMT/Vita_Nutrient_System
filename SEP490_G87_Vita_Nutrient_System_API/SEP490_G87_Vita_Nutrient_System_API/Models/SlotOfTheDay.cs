using System;
using System.Collections.Generic;

namespace SEP490_G87_Vita_Nutrient_System_API.Models
{
    public partial class SlotOfTheDay
    {
        public SlotOfTheDay()
        {
            MealSettingsDetails = new HashSet<MealSettingsDetail>();
        }

        public short Id { get; set; }
        public string? Slot { get; set; }
        public TimeSpan? StartAt { get; set; }
        public TimeSpan? EndAt { get; set; }

        public virtual ICollection<MealSettingsDetail> MealSettingsDetails { get; set; }
    }
}
