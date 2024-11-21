using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;
using System;

namespace SEP490_G87_Vita_Nutrient_System_Client.Models
{
    public class SystemPremiumPackage
    {
        public int IdAdminSystem { get; set; }
        public string? Describe { get; set; }
        public decimal? Price { get; set; }
        public short? Duration { get; set; }

    }
}