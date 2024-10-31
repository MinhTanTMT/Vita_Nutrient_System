using System;
using System.Collections.Generic;

namespace SEP490_G87_Vita_Nutrient_System_API.Models
{
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public short RoleId { get; set; }
        public string? RoleName { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
