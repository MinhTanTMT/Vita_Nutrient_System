using System;
using System.Collections.Generic;

namespace SEP490_G87_Vita_Nutrient_System_API.Models
{
    public partial class RecurringSetting
    {
        public RecurringSetting()
        {
            FoodSelections = new HashSet<FoodSelection>();
        }

        public short Id { get; set; }
        public string? Name { get; set; }
        public short? Frequency { get; set; }

        public virtual ICollection<FoodSelection> FoodSelections { get; set; }
    }
}
