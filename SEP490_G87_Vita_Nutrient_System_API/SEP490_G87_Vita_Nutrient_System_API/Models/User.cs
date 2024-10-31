using System;
using System.Collections.Generic;

namespace SEP490_G87_Vita_Nutrient_System_API.Models
{
    public partial class User
    {
        public User()
        {
            ArticlesNews = new HashSet<ArticlesNews>();
            BankInformations = new HashSet<BankInformation>();
            ConversationParticipants = new HashSet<ConversationParticipant>();
            FoodSelections = new HashSet<FoodSelection>();
            Messages = new HashSet<Message>();
            Msgs = new HashSet<Msg>();
            NutritionRouteCreateBies = new HashSet<NutritionRoute>();
            NutritionRouteUsers = new HashSet<NutritionRoute>();
            NutritionTargetsDailies = new HashSet<NutritionTargetsDaily>();
            TransactionsSystemPayees = new HashSet<TransactionsSystem>();
            TransactionsSystemUserPays = new HashSet<TransactionsSystem>();
            UserListManagementNutritionists = new HashSet<UserListManagement>();
            UserListManagementUsers = new HashSet<UserListManagement>();
        }

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

        public virtual Role RoleNavigation { get; set; } = null!;
        public virtual MealSetting? MealSetting { get; set; }
        public virtual NutritionistDetail? NutritionistDetail { get; set; }
        public virtual UserDetail? UserDetail { get; set; }
        public virtual ICollection<ArticlesNews> ArticlesNews { get; set; }
        public virtual ICollection<BankInformation> BankInformations { get; set; }
        public virtual ICollection<ConversationParticipant> ConversationParticipants { get; set; }
        public virtual ICollection<FoodSelection> FoodSelections { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Msg> Msgs { get; set; }
        public virtual ICollection<NutritionRoute> NutritionRouteCreateBies { get; set; }
        public virtual ICollection<NutritionRoute> NutritionRouteUsers { get; set; }
        public virtual ICollection<NutritionTargetsDaily> NutritionTargetsDailies { get; set; }
        public virtual ICollection<TransactionsSystem> TransactionsSystemPayees { get; set; }
        public virtual ICollection<TransactionsSystem> TransactionsSystemUserPays { get; set; }
        public virtual ICollection<UserListManagement> UserListManagementNutritionists { get; set; }
        public virtual ICollection<UserListManagement> UserListManagementUsers { get; set; }
    }
}
