﻿namespace SEP490_G87_Vita_Nutrient_System_API.Domain.RequestModels
{
    public class AddUpdatePackageRequest
    {
        public short Id { get; set; }

        public string? Name { get; set; }

        public string? Describe { get; set; }

        public decimal? Price { get; set; }

        public short? Duration { get; set; }
    }
}
