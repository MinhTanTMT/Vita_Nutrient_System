using System;
using System.Collections.Generic;

namespace SEP490_G87_Vita_Nutrient_System_Client.Models;

public partial class User
{
    public int UserId { get; set; }

    public string? Urlimage { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateTime? Dob { get; set; }

    public bool? Gender { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public string? Password { get; set; }

    public short Role { get; set; }

    public bool? IsActive { get; set; }

	public string Account { get; set; } = null!;
    public virtual UserDetail? UserDetail { get; set; }
    public virtual NutritionistDetail? NutritionistDetail { get; set; }
    public virtual UserRole? UserRole { get; set; }

    public string? FullName { get; set; }
    public short? Height { get; set; }
    public short? Weight { get; set; }
    public short? Age { get; set; }

    public List<string>? UnderlyingDiseaseNames { get; set; }
}
