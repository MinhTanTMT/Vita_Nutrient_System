using System;
using System.Collections.Generic;

namespace SEP490_G87_Vita_Nutrient_System_API.Models
{
    public partial class ExerciseIntensity
    {
        public ExerciseIntensity()
        {
            NutritionTargetsDailies = new HashSet<NutritionTargetsDaily>();
        }

        public short Id { get; set; }
        public string? Name { get; set; }
        public string? Describe { get; set; }
        public string? ListKey { get; set; }

        public virtual ICollection<NutritionTargetsDaily> NutritionTargetsDailies { get; set; }
    }
}
