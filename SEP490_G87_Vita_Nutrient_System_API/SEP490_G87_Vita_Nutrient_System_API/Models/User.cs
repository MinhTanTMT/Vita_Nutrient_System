using System;
using System.Collections.Generic;

namespace SEP490_G87_Vita_Nutrient_System_API.Models;

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

    public string Password { get; set; } = null!;

    public short Role { get; set; }

    public bool? IsActive { get; set; }

    public string Account { get; set; } = null!;

    public virtual ICollection<ArticlesNews> ArticlesNews { get; set; } = new List<ArticlesNews>();

    public virtual ICollection<BankInformation> BankInformations { get; set; } = new List<BankInformation>();

    public virtual ICollection<ConversationParticipant> ConversationParticipants { get; set; } = new List<ConversationParticipant>();

    public virtual ICollection<FoodSelection> FoodSelections { get; set; } = new List<FoodSelection>();

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual ICollection<Msg> Msgs { get; set; } = new List<Msg>();

    public virtual ICollection<NutritionRoute> NutritionRouteCreateBies { get; set; } = new List<NutritionRoute>();

    public virtual ICollection<NutritionRoute> NutritionRouteUsers { get; set; } = new List<NutritionRoute>();

    public virtual ICollection<NutritionTargetsDaily> NutritionTargetsDailies { get; set; } = new List<NutritionTargetsDaily>();

    public virtual NutritionistDetail? NutritionistDetail { get; set; }

    public virtual Role? RoleNavigation { get; set; } = null!;

    public virtual ICollection<TransactionsSystem> TransactionsSystemPayees { get; set; } = new List<TransactionsSystem>();

    public virtual ICollection<TransactionsSystem> TransactionsSystemUserPays { get; set; } = new List<TransactionsSystem>();

    public virtual UserDetail? UserDetail { get; set; }

    public virtual ICollection<UserListManagement> UserListManagementNutritionists { get; set; } = new List<UserListManagement>();

    public virtual ICollection<UserListManagement> UserListManagementUsers { get; set; } = new List<UserListManagement>();
}
