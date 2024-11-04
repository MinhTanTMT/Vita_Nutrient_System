using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SEP490_G87_Vita_Nutrient_System_API.Models;

public partial class Sep490G87VitaNutrientSystemContext : DbContext
{
    public Sep490G87VitaNutrientSystemContext()
    {
    }

    public Sep490G87VitaNutrientSystemContext(DbContextOptions<Sep490G87VitaNutrientSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ArticlesNews> ArticlesNews { get; set; }

    public virtual DbSet<BankInformation> BankInformations { get; set; }

    public virtual DbSet<Conversation> Conversations { get; set; }

    public virtual DbSet<ConversationParticipant> ConversationParticipants { get; set; }

    public virtual DbSet<CookingDifficulty> CookingDifficulties { get; set; }

    public virtual DbSet<DayOfTheWeek> DayOfTheWeeks { get; set; }

    public virtual DbSet<ExerciseIntensity> ExerciseIntensities { get; set; }

    public virtual DbSet<ExpertPackage> ExpertPackages { get; set; }

    public virtual DbSet<FoodAndDisease> FoodAndDiseases { get; set; }

    public virtual DbSet<FoodList> FoodLists { get; set; }

    public virtual DbSet<FoodSelection> FoodSelections { get; set; }

    public virtual DbSet<FoodType> FoodTypes { get; set; }

    public virtual DbSet<IngredientDetails100g> IngredientDetails100gs { get; set; }

    public virtual DbSet<KeyNote> KeyNotes { get; set; }

    public virtual DbSet<ListOfDisease> ListOfDiseases { get; set; }

    public virtual DbSet<MealOfTheDay> MealOfTheDays { get; set; }

    public virtual DbSet<MealSetting> MealSettings { get; set; }

    public virtual DbSet<MealSettingsDetail> MealSettingsDetails { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Msg> Msgs { get; set; }

    public virtual DbSet<NewsEvaluation> NewsEvaluations { get; set; }

    public virtual DbSet<NutritionRoute> NutritionRoutes { get; set; }

    public virtual DbSet<NutritionTargetsDaily> NutritionTargetsDailies { get; set; }

    public virtual DbSet<NutritionistDetail> NutritionistDetails { get; set; }

    public virtual DbSet<Recipe> Recipes { get; set; }

    public virtual DbSet<RecurringSetting> RecurringSettings { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<ScaleAmount> ScaleAmounts { get; set; }

    public virtual DbSet<SlotOfTheDay> SlotOfTheDays { get; set; }

    public virtual DbSet<TransactionsSystem> TransactionsSystems { get; set; }

    public virtual DbSet<TypeOfCalculation> TypeOfCalculations { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserDetail> UserDetails { get; set; }

    public virtual DbSet<UserListManagement> UserListManagements { get; set; }

    public virtual DbSet<WantCooking> WantCookings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var ConnectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("DefaultConnection");
        optionsBuilder.UseSqlServer(ConnectionString);

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ArticlesNews>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Articles__3214EC074497A950");

            entity.ToTable("ArticlesNews", "Business");

            entity.Property(e => e.DateCreated).HasColumnType("datetime");
            entity.Property(e => e.HeaderImage).HasMaxLength(255);
            entity.Property(e => e.NameCreater).HasMaxLength(50);
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.ArticlesNews)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ArticlesN__UserI__10566F31");
        });

        modelBuilder.Entity<BankInformation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BankInfo__3214EC0738E2432D");

            entity.ToTable("BankInformation", "UserData");

            entity.Property(e => e.BankAccount)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TypeBank)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.BankInformations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BankInfor__UserI__43D61337");
        });

        modelBuilder.Entity<Conversation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Conversa__3214EC072914FA57");

            entity.ToTable("Conversations", "Business");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<ConversationParticipant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Conversa__3214EC07BE4CCA8A");

            entity.ToTable("ConversationParticipants", "Business");

            entity.HasIndex(e => new { e.ConversationsId, e.UserId }, "UQ__Conversa__135EC2124A57C55D").IsUnique();

            entity.Property(e => e.AddedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Conversations).WithMany(p => p.ConversationParticipants)
                .HasForeignKey(d => d.ConversationsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Conversat__Conve__05D8E0BE");

            entity.HasOne(d => d.User).WithMany(p => p.ConversationParticipants)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Conversat__UserI__06CD04F7");
        });

        modelBuilder.Entity<CookingDifficulty>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CookingD__3214EC076AA13725");

            entity.ToTable("CookingDifficulty", "FoodData");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<DayOfTheWeek>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DayOfThe__3214EC07C347CEED");

            entity.ToTable("DayOfTheWeek", "Business");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<ExerciseIntensity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Exercise__3214EC07090D2F5D");

            entity.ToTable("ExerciseIntensity", "UserData");

            entity.Property(e => e.Describe).HasMaxLength(500);
            entity.Property(e => e.ListKey).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<ExpertPackage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ExpertPa__3214EC07F56CC7DA");

            entity.ToTable("ExpertPackages", "UserData");

            entity.Property(e => e.Describe).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Price).HasColumnType("money");

            entity.HasOne(d => d.NutritionistDetails).WithMany(p => p.ExpertPackages)
                .HasForeignKey(d => d.NutritionistDetailsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ExpertPac__Nutri__5EBF139D");
        });

        modelBuilder.Entity<FoodAndDisease>(entity =>
        {
            entity.HasKey(e => new { e.ListOfDiseasesId, e.FoodListId }).HasName("PK__FoodAndD__13B67669A1863E9F");

            entity.ToTable("FoodAndDiseases", "FoodData");

            entity.Property(e => e.Describe).HasMaxLength(500);

            entity.HasOne(d => d.FoodList).WithMany(p => p.FoodAndDiseases)
                .HasForeignKey(d => d.FoodListId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FoodAndDi__FoodL__787EE5A0");

            entity.HasOne(d => d.ListOfDiseases).WithMany(p => p.FoodAndDiseases)
                .HasForeignKey(d => d.ListOfDiseasesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FoodAndDi__ListO__778AC167");
        });

        modelBuilder.Entity<FoodList>(entity =>
        {
            entity.HasKey(e => e.FoodListId).HasName("PK__FoodList__DFC99F4D779CDB43");

            entity.ToTable("FoodList", "FoodData");

            entity.Property(e => e.Describe).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Urlimage)
                .HasMaxLength(512)
                .IsUnicode(false)
                .HasColumnName("URLImage");

            entity.HasOne(d => d.CookingDifficulty).WithMany(p => p.FoodLists)
                .HasForeignKey(d => d.CookingDifficultyId)
                .HasConstraintName("FK__FoodList__Cookin__59C55456");

            entity.HasOne(d => d.FoodType).WithMany(p => p.FoodLists)
                .HasForeignKey(d => d.FoodTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FoodList__FoodTy__70DDC3D8");

            entity.HasOne(d => d.KeyNote).WithMany(p => p.FoodLists)
                .HasForeignKey(d => d.KeyNoteId)
                .HasConstraintName("FK__FoodList__KeyNot__71D1E811");
        });

        modelBuilder.Entity<FoodSelection>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.FoodListId }).HasName("PK__FoodSele__DA7455B8E170105E");

            entity.ToTable("FoodSelection", "UserData");

            entity.HasOne(d => d.FoodList).WithMany(p => p.FoodSelections)
                .HasForeignKey(d => d.FoodListId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FoodSelec__FoodL__7C4F7684");

            entity.HasOne(d => d.Recurring).WithMany(p => p.FoodSelections)
                .HasForeignKey(d => d.RecurringId)
                .HasConstraintName("FK__FoodSelec__Recur__7D439ABD");

            entity.HasOne(d => d.User).WithMany(p => p.FoodSelections)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FoodSelec__UserI__7B5B524B");
        });

        modelBuilder.Entity<FoodType>(entity =>
        {
            entity.HasKey(e => e.FoodTypeId).HasName("PK__FoodType__D3D1548C9176353B");

            entity.ToTable("FoodType", "FoodData");

            entity.Property(e => e.Describe).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<IngredientDetails100g>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Ingredie__3214EC07555781DD");

            entity.ToTable("IngredientDetails100g", "FoodData");

            entity.Property(e => e.AcidAspartic).HasColumnName("Acid_aspartic");
            entity.Property(e => e.AcidGlutamic).HasColumnName("Acid_glutamic");
            entity.Property(e => e.AlphaCaroten).HasColumnName("Alpha_caroten");
            entity.Property(e => e.ArachidicC200).HasColumnName("Arachidic_C20_0");
            entity.Property(e => e.ArachidonicC204).HasColumnName("Arachidonic_C20_4");
            entity.Property(e => e.BehenicC220).HasColumnName("Behenic_C22_0");
            entity.Property(e => e.BetaCaroten).HasColumnName("Beta_caroten");
            entity.Property(e => e.BetaCryptoxanthin).HasColumnName("Beta_cryptoxanthin");
            entity.Property(e => e.Describe).HasMaxLength(500);
            entity.Property(e => e.DocosahexaenoicC226N3).HasColumnName("Docosahexaenoic_C22_6_n3");
            entity.Property(e => e.EicosapentaenoicC205N3).HasColumnName("Eicosapentaenoic_C20_5_n3");
            entity.Property(e => e.LignocericC240).HasColumnName("Lignoceric_C24_0");
            entity.Property(e => e.LinoleicC182N6).HasColumnName("Linoleic_C18_2_n6");
            entity.Property(e => e.LinolenicC182N3).HasColumnName("Linolenic_C18_2_n3");
            entity.Property(e => e.MargaricC170).HasColumnName("Margaric_C17_0");
            entity.Property(e => e.MyristoleicC141).HasColumnName("Myristoleic_C14_1");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.OleicC181).HasColumnName("Oleic_C18_1");
            entity.Property(e => e.PalmiticC160).HasColumnName("Palmitic_C16_0");
            entity.Property(e => e.PalmitoleicC161).HasColumnName("Palmitoleic_C16_1");
            entity.Property(e => e.StearicC180).HasColumnName("Stearic_C18_0");
            entity.Property(e => e.TotalIsoflavone).HasColumnName("Total_isoflavone");
            entity.Property(e => e.TotalSaturatedFattyAcid).HasColumnName("Total_saturated_fatty_acid");
            entity.Property(e => e.Urlimage)
                .HasMaxLength(512)
                .IsUnicode(false)
                .HasColumnName("URLImage");
            entity.Property(e => e.VitaminA).HasColumnName("Vitamin_A");
            entity.Property(e => e.VitaminB1).HasColumnName("Vitamin_B1");
            entity.Property(e => e.VitaminB12).HasColumnName("Vitamin_B12");
            entity.Property(e => e.VitaminB2).HasColumnName("Vitamin_B2");
            entity.Property(e => e.VitaminB5).HasColumnName("Vitamin_B5");
            entity.Property(e => e.VitaminB6).HasColumnName("Vitamin_B6");
            entity.Property(e => e.VitaminB9).HasColumnName("Vitamin_B9");
            entity.Property(e => e.VitaminC).HasColumnName("Vitamin_C");
            entity.Property(e => e.VitaminD).HasColumnName("Vitamin_D");
            entity.Property(e => e.VitaminE).HasColumnName("Vitamin_E");
            entity.Property(e => e.VitaminH).HasColumnName("Vitamin_H");
            entity.Property(e => e.VitaminK).HasColumnName("Vitamin_K");
            entity.Property(e => e.VitaminPp).HasColumnName("Vitamin_PP");

            entity.HasOne(d => d.KeyNote).WithMany(p => p.IngredientDetails100gs)
                .HasForeignKey(d => d.KeyNoteId)
                .HasConstraintName("FK_IngredientDetails100g_KeyNote");

            entity.HasOne(d => d.TypeOfCalculation).WithMany(p => p.IngredientDetails100gs)
                .HasForeignKey(d => d.TypeOfCalculationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ingredien__TypeO__17036CC0");
        });

        modelBuilder.Entity<KeyNote>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__KeyNote__3214EC077148E725");

            entity.ToTable("KeyNote", "FoodData");

            entity.Property(e => e.KeyList).HasMaxLength(500);
        });

        modelBuilder.Entity<ListOfDisease>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ListOfDi__3214EC078AC8210B");

            entity.ToTable("ListOfDiseases", "UserData");

            entity.Property(e => e.Describe).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<MealOfTheDay>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MealOfTh__3214EC07F5556AEE");

            entity.ToTable("MealOfTheDay", "Business");

            entity.HasIndex(e => new { e.NutritionRouteId, e.DateExecute }, "UQ__MealOfTh__641B4C6D9F1431EA").IsUnique();

            entity.Property(e => e.DataFoodListId)
                .HasMaxLength(512)
                .IsUnicode(false);

            entity.HasOne(d => d.NutritionRoute).WithMany(p => p.MealOfTheDays)
                .HasForeignKey(d => d.NutritionRouteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MealOfThe__Nutri__0D44F85C");
        });

        modelBuilder.Entity<MealSetting>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MealSett__3214EC070F8F0A38");

            entity.ToTable("MealSettings", "UserData");

            entity.HasIndex(e => e.UserId, "UQ__MealSett__1788CC4D843F35AF").IsUnique();

            entity.HasOne(d => d.DayOfTheWeekStart).WithMany(p => p.MealSettings)
                .HasForeignKey(d => d.DayOfTheWeekStartId)
                .HasConstraintName("FK__MealSetti__DayOf__5E8A0973");

            entity.HasOne(d => d.User).WithOne(p => p.MealSetting)
                .HasForeignKey<MealSetting>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MealSetti__UserI__5D95E53A");
        });

        modelBuilder.Entity<MealSettingsDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MealSett__3214EC07335095BE");

            entity.ToTable("MealSettingsDetails", "UserData");

            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Size).HasMaxLength(50);
            entity.Property(e => e.TypeFavoriteFood).HasMaxLength(50);

            entity.HasOne(d => d.CookingDifficulty).WithMany(p => p.MealSettingsDetails)
                .HasForeignKey(d => d.CookingDifficultyId)
                .HasConstraintName("FK__MealSetti__Cooki__6EC0713C");

            entity.HasOne(d => d.DayOfTheWeek).WithMany(p => p.MealSettingsDetails)
                .HasForeignKey(d => d.DayOfTheWeekId)
                .HasConstraintName("FK__MealSetti__DayOf__6CD828CA");

            entity.HasOne(d => d.MealSettings).WithMany(p => p.MealSettingsDetails)
                .HasForeignKey(d => d.MealSettingsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MealSetti__MealS__69FBBC1F");

            entity.HasOne(d => d.NutritionTargetsDaily).WithMany(p => p.MealSettingsDetails)
                .HasForeignKey(d => d.NutritionTargetsDailyId)
                .HasConstraintName("FK__MealSetti__Nutri__6BE40491");

            entity.HasOne(d => d.SlotOfTheDay).WithMany(p => p.MealSettingsDetails)
                .HasForeignKey(d => d.SlotOfTheDayId)
                .HasConstraintName("FK__MealSetti__SlotO__6AEFE058");

            entity.HasOne(d => d.WantCooking).WithMany(p => p.MealSettingsDetails)
                .HasForeignKey(d => d.WantCookingId)
                .HasConstraintName("FK__MealSetti__WantC__6DCC4D03");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Messages__3214EC07D9D4C8AF");

            entity.ToTable("Messages", "Business");

            entity.Property(e => e.Content).HasMaxLength(500);
            entity.Property(e => e.SentAt).HasColumnType("datetime");

            entity.HasOne(d => d.Conversations).WithMany(p => p.Messages)
                .HasForeignKey(d => d.ConversationsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Messages__Conver__09A971A2");

            entity.HasOne(d => d.Sender).WithMany(p => p.Messages)
                .HasForeignKey(d => d.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Messages__Sender__0A9D95DB");
        });

        modelBuilder.Entity<Msg>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MSG__3214EC07DA1FABA2");

            entity.ToTable("MSG", "Business");

            entity.Property(e => e.ErrorMessage).HasMaxLength(50);
            entity.Property(e => e.MessageContent).HasMaxLength(500);
            entity.Property(e => e.MessageType).HasMaxLength(50);
            entity.Property(e => e.SendDateTime).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Msgs)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MSG__UserID__0D7A0286");
        });

        modelBuilder.Entity<NewsEvaluation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__NewsEval__3214EC071137F020");

            entity.ToTable("NewsEvaluation", "Business");

            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.ArticlesNews).WithMany(p => p.NewsEvaluations)
                .HasForeignKey(d => d.ArticlesNewsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NewsEvalu__Artic__1B9317B3");

            entity.HasOne(d => d.User).WithMany(p => p.NewsEvaluations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NewsEvalu__UserI__1C873BEC");
        });

        modelBuilder.Entity<NutritionRoute>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Nutritio__3214EC07FEFFF158");

            entity.ToTable("NutritionRoute", "Business");

            entity.Property(e => e.Describe).HasMaxLength(500);
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.StartDate).HasColumnType("datetime");

            entity.HasOne(d => d.CreateBy).WithMany(p => p.NutritionRouteCreateBies)
                .HasForeignKey(d => d.CreateById)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Nutrition__Creat__66603565");

            entity.HasOne(d => d.User).WithMany(p => p.NutritionRouteUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Nutrition__UserI__656C112C");
        });

        modelBuilder.Entity<NutritionTargetsDaily>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Nutritio__3214EC07AE4EC2F3");

            entity.ToTable("NutritionTargetsDaily", "UserData");

            entity.Property(e => e.AvoidIngredient).HasMaxLength(500);
            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasOne(d => d.ExerciseIntensity).WithMany(p => p.NutritionTargetsDailies)
                .HasForeignKey(d => d.ExerciseIntensityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Nutrition__Exerc__01142BA1");

            entity.HasOne(d => d.FoodTypeIdWantNavigation).WithMany(p => p.NutritionTargetsDailies)
                .HasForeignKey(d => d.FoodTypeIdWant)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Nutrition__FoodT__02084FDA");

            entity.HasOne(d => d.User).WithMany(p => p.NutritionTargetsDailies)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Nutrition__UserI__00200768");
        });

        modelBuilder.Entity<NutritionistDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Nutritio__3214EC07F9BB174A");

            entity.ToTable("NutritionistDetails", "UserData");

            entity.HasIndex(e => e.NutritionistId, "UQ__Nutritio__F4399C8D333D8E36").IsUnique();

            entity.Property(e => e.DescribeYourself).HasMaxLength(500);

            entity.HasOne(d => d.Nutritionist).WithOne(p => p.NutritionistDetail)
                .HasForeignKey<NutritionistDetail>(d => d.NutritionistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Nutrition__Nutri__5BE2A6F2");
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasKey(e => e.RecipeId).HasName("PK__Recipe__FDD988B03D99941B");

            entity.ToTable("Recipe", "FoodData");

            entity.Property(e => e.Describe).HasMaxLength(500);
            entity.Property(e => e.Urlimage)
                .HasMaxLength(512)
                .IsUnicode(false)
                .HasColumnName("URLImage");

            entity.HasOne(d => d.FoodList).WithMany(p => p.Recipes)
                .HasForeignKey(d => d.FoodListId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Recipe__FoodList__74AE54BC");
        });

        modelBuilder.Entity<RecurringSetting>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Recurrin__3214EC07A71985C2");

            entity.ToTable("RecurringSettings", "UserData");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__8AFACE3AF5E7FB2B");

            entity.ToTable("Role", "UserData");

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<ScaleAmount>(entity =>
        {
            entity.HasKey(e => new { e.FoodListId, e.IngredientDetailsId }).HasName("PK__ScaleAmo__8F2BA49A1C1CDB9C");

            entity.ToTable("ScaleAmount", "FoodData");

            entity.Property(e => e.ScaleAmount1).HasColumnName("ScaleAmount");

            entity.HasOne(d => d.FoodList).WithMany(p => p.ScaleAmounts)
                .HasForeignKey(d => d.FoodListId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ScaleAmou__FoodL__756D6ECB");

            entity.HasOne(d => d.IngredientDetails).WithMany(p => p.ScaleAmounts)
                .HasForeignKey(d => d.IngredientDetailsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ScaleAmou__Ingre__76619304");
        });

        modelBuilder.Entity<SlotOfTheDay>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SlotOfTh__3214EC073B5D86FF");

            entity.ToTable("SlotOfTheDay", "Business");

            entity.Property(e => e.Slot).HasMaxLength(50);
        });

        modelBuilder.Entity<TransactionsSystem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Transact__3214EC072DB8F78F");

            entity.ToTable("TransactionsSystem", "Business");

            entity.Property(e => e.AccountNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Accumulated).HasColumnType("money");
            entity.Property(e => e.AmountIn).HasColumnType("money");
            entity.Property(e => e.AmountOut).HasColumnType("money");
            entity.Property(e => e.Apitransactions).HasColumnName("APITransactions");
            entity.Property(e => e.BankBrandName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Code)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PayeeId).HasColumnName("PayeeID");
            entity.Property(e => e.ReferenceNumber)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.SubAccount)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TransactionContent)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.TransactionDate).HasColumnType("datetime");
            entity.Property(e => e.UserPayId).HasColumnName("UserPayID");

            entity.HasOne(d => d.Payee).WithMany(p => p.TransactionsSystemPayees)
                .HasForeignKey(d => d.PayeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Transacti__Payee__40F9A68C");

            entity.HasOne(d => d.UserPay).WithMany(p => p.TransactionsSystemUserPays)
                .HasForeignKey(d => d.UserPayId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Transacti__UserP__40058253");
        });

        modelBuilder.Entity<TypeOfCalculation>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("PK__TypeOfCa__516F03B51B6A74C5");

            entity.ToTable("TypeOfCalculation", "FoodData");

            entity.Property(e => e.CalculationForm).HasMaxLength(500);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CC4CA04A9014");

            entity.ToTable("User", "UserData");

            entity.Property(e => e.Account)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValue("example@example.com");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Dob)
                .HasColumnType("datetime")
                .HasColumnName("DOB");
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Urlimage)
                .HasMaxLength(512)
                .IsUnicode(false)
                .HasColumnName("URLImage");

            entity.HasOne(d => d.RoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.Role)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__User__Role__5812160E");
        });

        modelBuilder.Entity<UserDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserDeta__3214EC0713A65B53");

            entity.ToTable("UserDetails", "UserData");

            entity.HasIndex(e => e.UserId, "UQ__UserDeta__1788CC4DE9C17C83").IsUnique();

            entity.Property(e => e.DescribeYourself).HasMaxLength(500);
            entity.Property(e => e.InforConfirmBad).HasMaxLength(500);
            entity.Property(e => e.InforConfirmGood).HasMaxLength(500);
            entity.Property(e => e.UnderlyingDisease).HasMaxLength(500);
            entity.Property(e => e.WantImprove).HasMaxLength(500);

            entity.HasOne(d => d.User).WithOne(p => p.UserDetail)
                .HasForeignKey<UserDetail>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserDetai__UserI__628FA481");
        });

        modelBuilder.Entity<UserListManagement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserList__3214EC07F5DFFA86");

            entity.ToTable("UserListManagement", "Business");

            entity.Property(e => e.Describe).HasMaxLength(500);
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.StartDate).HasColumnType("datetime");

            entity.HasOne(d => d.Nutritionist).WithMany(p => p.UserListManagementNutritionists)
                .HasForeignKey(d => d.NutritionistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserListM__Nutri__1332DBDC");

            entity.HasOne(d => d.User).WithMany(p => p.UserListManagementUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserListM__UserI__14270015");
        });

        modelBuilder.Entity<WantCooking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__WantCook__3214EC07081EF694");

            entity.ToTable("WantCooking", "UserData");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
