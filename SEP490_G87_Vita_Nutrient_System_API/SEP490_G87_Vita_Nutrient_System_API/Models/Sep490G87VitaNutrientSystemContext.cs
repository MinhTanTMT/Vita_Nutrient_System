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

    public virtual DbSet<CookingDifficulty> CookingDifficulties { get; set; }

    public virtual DbSet<DayOfTheWeek> DayOfTheWeeks { get; set; }

    public virtual DbSet<DietType> DietTypes { get; set; }

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

    public virtual DbSet<NewsEvaluation> NewsEvaluations { get; set; }

    public virtual DbSet<NutritionRoute> NutritionRoutes { get; set; }

    public virtual DbSet<NutritionTargetsDaily> NutritionTargetsDailies { get; set; }

    public virtual DbSet<NutritionistDetail> NutritionistDetails { get; set; }

    public virtual DbSet<Recipe> Recipes { get; set; }

    public virtual DbSet<RecurringSetting> RecurringSettings { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

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
        var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ArticlesNews>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Articles__3214EC07B4ED5C8F");

            entity.ToTable("ArticlesNews", "Business");

            entity.Property(e => e.DateCreated).HasColumnType("datetime");
            entity.Property(e => e.HeaderImage).HasMaxLength(255);
            entity.Property(e => e.NameCreater).HasMaxLength(50);
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.ArticlesNews)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ArticlesN__UserI__68487DD7");
        });

        modelBuilder.Entity<BankInformation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BankInfo__3214EC075B35650A");

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
                .HasConstraintName("FK__BankInfor__UserI__14270015");
        });

        modelBuilder.Entity<CookingDifficulty>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CookingD__3214EC07A44543BB");

            entity.ToTable("CookingDifficulty", "FoodData");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<DayOfTheWeek>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DayOfThe__3214EC07EBC2094F");

            entity.ToTable("DayOfTheWeek", "Business");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<DietType>(entity =>
        {
            entity.HasKey(e => e.DietTypeId).HasName("PK__DietType__B578CC5FF534FABD");

            entity.ToTable("DietType", "FoodData");

            entity.Property(e => e.Describe).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<ExpertPackage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ExpertPa__3214EC074B2F8E11");

            entity.ToTable("ExpertPackages", "UserData");

            entity.Property(e => e.Describe).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Price).HasColumnType("money");
        });

        modelBuilder.Entity<FoodAndDisease>(entity =>
        {
            entity.HasKey(e => new { e.ListOfDiseasesId, e.FoodListId }).HasName("PK__FoodAndD__13B67669DFF6A667");

            entity.ToTable("FoodAndDiseases", "FoodData");

            entity.Property(e => e.Describe).HasMaxLength(500);

            entity.HasOne(d => d.FoodList).WithMany(p => p.FoodAndDiseases)
                .HasForeignKey(d => d.FoodListId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FoodAndDi__FoodL__778AC167");

            entity.HasOne(d => d.ListOfDiseases).WithMany(p => p.FoodAndDiseases)
                .HasForeignKey(d => d.ListOfDiseasesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FoodAndDi__ListO__787EE5A0");
        });

        modelBuilder.Entity<FoodList>(entity =>
        {
            entity.HasKey(e => e.FoodListId).HasName("PK__FoodList__DFC99F4D9CAB5EC8");

            entity.ToTable("FoodList", "FoodData");

            entity.Property(e => e.Describe).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Urlimage)
                .HasMaxLength(512)
                .IsUnicode(false)
                .HasColumnName("URLImage");

            entity.HasOne(d => d.CookingDifficulty).WithMany(p => p.FoodLists)
                .HasForeignKey(d => d.CookingDifficultyId)
                .HasConstraintName("FK__FoodList__Cookin__797309D9");

            entity.HasOne(d => d.FoodType).WithMany(p => p.FoodLists)
                .HasForeignKey(d => d.FoodTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FoodList__FoodTy__7A672E12");

            entity.HasOne(d => d.KeyNote).WithMany(p => p.FoodLists)
                .HasForeignKey(d => d.KeyNoteId)
                .HasConstraintName("FK__FoodList__KeyNot__7B5B524B");
        });

        modelBuilder.Entity<FoodSelection>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.FoodListId }).HasName("PK__FoodSele__DA7455B8E02F2BD1");

            entity.ToTable("FoodSelection", "UserData");

            entity.HasOne(d => d.FoodList).WithMany(p => p.FoodSelections)
                .HasForeignKey(d => d.FoodListId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FoodSelec__FoodL__01142BA1");

            entity.HasOne(d => d.Recurring).WithMany(p => p.FoodSelections)
                .HasForeignKey(d => d.RecurringId)
                .HasConstraintName("FK__FoodSelec__Recur__02084FDA");

            entity.HasOne(d => d.User).WithMany(p => p.FoodSelections)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FoodSelec__UserI__02FC7413");
        });

        modelBuilder.Entity<FoodType>(entity =>
        {
            entity.HasKey(e => e.FoodTypeId).HasName("PK__FoodType__D3D1548C2B052CD9");

            entity.ToTable("FoodType", "FoodData");

            entity.Property(e => e.Describe).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasMany(d => d.DietTypes).WithMany(p => p.FoodTypes)
                .UsingEntity<Dictionary<string, object>>(
                    "DietWithFoodType",
                    r => r.HasOne<DietType>().WithMany()
                        .HasForeignKey("DietTypeId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__DietWithF__DietT__75A278F5"),
                    l => l.HasOne<FoodType>().WithMany()
                        .HasForeignKey("FoodTypeId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__DietWithF__FoodT__76969D2E"),
                    j =>
                    {
                        j.HasKey("FoodTypeId", "DietTypeId").HasName("PK__DietWith__3886D84930801E5C");
                        j.ToTable("DietWithFoodType", "FoodData");
                    });
        });

        modelBuilder.Entity<IngredientDetails100g>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Ingredie__3214EC07816293CA");

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
                .HasConstraintName("FK__Ingredien__TypeO__7C4F7684");
        });

        modelBuilder.Entity<KeyNote>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__KeyNote__3214EC074F798524");

            entity.ToTable("KeyNote", "FoodData");

            entity.Property(e => e.KeyList).HasMaxLength(500);
        });

        modelBuilder.Entity<ListOfDisease>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ListOfDi__3214EC0711AF8126");

            entity.ToTable("ListOfDiseases", "UserData");

            entity.Property(e => e.Describe).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<MealOfTheDay>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MealOfTh__3214EC078BF80B4F");

            entity.ToTable("MealOfTheDay", "Business");

            entity.HasIndex(e => new { e.NutritionRouteId, e.DateExecute }, "UQ__MealOfTh__641B4C6D6CA697B4").IsUnique();

            entity.Property(e => e.DataFoodListId)
                .HasMaxLength(512)
                .IsUnicode(false);

            entity.HasOne(d => d.NutritionRoute).WithMany(p => p.MealOfTheDays)
                .HasForeignKey(d => d.NutritionRouteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MealOfThe__Nutri__693CA210");
        });

        modelBuilder.Entity<MealSetting>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MealSett__3214EC07FF247151");

            entity.ToTable("MealSettings", "UserData");

            entity.HasIndex(e => e.UserId, "UQ__MealSett__1788CC4D340E5288").IsUnique();

            entity.HasOne(d => d.DayOfTheWeekStart).WithMany(p => p.MealSettings)
                .HasForeignKey(d => d.DayOfTheWeekStartId)
                .HasConstraintName("FK__MealSetti__DayOf__03F0984C");

            entity.HasOne(d => d.FoodTypeIdWantNavigation).WithMany(p => p.MealSettings)
                .HasForeignKey(d => d.FoodTypeIdWant)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MealSetti__FoodT__04E4BC85");

            entity.HasOne(d => d.User).WithOne(p => p.MealSetting)
                .HasForeignKey<MealSetting>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MealSetti__UserI__05D8E0BE");
        });

        modelBuilder.Entity<MealSettingsDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MealSett__3214EC07B5D4370C");

            entity.ToTable("MealSettingsDetails", "UserData");

            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Size).HasMaxLength(50);
            entity.Property(e => e.TypeFavoriteFood).HasMaxLength(50);

            entity.HasOne(d => d.CookingDifficulty).WithMany(p => p.MealSettingsDetails)
                .HasForeignKey(d => d.CookingDifficultyId)
                .HasConstraintName("FK__MealSetti__Cooki__06CD04F7");

            entity.HasOne(d => d.DayOfTheWeek).WithMany(p => p.MealSettingsDetails)
                .HasForeignKey(d => d.DayOfTheWeekId)
                .HasConstraintName("FK__MealSetti__DayOf__07C12930");

            entity.HasOne(d => d.MealSettings).WithMany(p => p.MealSettingsDetails)
                .HasForeignKey(d => d.MealSettingsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MealSetti__MealS__08B54D69");

            entity.HasOne(d => d.NutritionTargetsDaily).WithMany(p => p.MealSettingsDetails)
                .HasForeignKey(d => d.NutritionTargetsDailyId)
                .HasConstraintName("FK__MealSetti__Nutri__09A971A2");

            entity.HasOne(d => d.SlotOfTheDay).WithMany(p => p.MealSettingsDetails)
                .HasForeignKey(d => d.SlotOfTheDayId)
                .HasConstraintName("FK__MealSetti__SlotO__0A9D95DB");

            entity.HasOne(d => d.WantCooking).WithMany(p => p.MealSettingsDetails)
                .HasForeignKey(d => d.WantCookingId)
                .HasConstraintName("FK__MealSetti__WantC__0B91BA14");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Messages__3214EC07F5CE78FF");

            entity.ToTable("Messages", "Business");

            entity.Property(e => e.Content).HasMaxLength(500);
            entity.Property(e => e.Timestamp).HasColumnType("datetime");

            entity.HasOne(d => d.FromUser).WithMany(p => p.Messages)
                .HasForeignKey(d => d.FromUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Messages__FromUs__6A30C649");

            entity.HasOne(d => d.ToRoom).WithMany(p => p.Messages)
                .HasForeignKey(d => d.ToRoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Messages__ToRoom__6B24EA82");
        });

        modelBuilder.Entity<NewsEvaluation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__NewsEval__3214EC07FC2638DD");

            entity.ToTable("NewsEvaluation", "Business");

            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.ArticlesNews).WithMany(p => p.NewsEvaluations)
                .HasForeignKey(d => d.ArticlesNewsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NewsEvalu__Artic__6C190EBB");

            entity.HasOne(d => d.User).WithMany(p => p.NewsEvaluations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NewsEvalu__UserI__6D0D32F4");
        });

        modelBuilder.Entity<NutritionRoute>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Nutritio__3214EC0796EB94A6");

            entity.ToTable("NutritionRoute", "Business");

            entity.Property(e => e.Describe).HasMaxLength(500);
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.StartDate).HasColumnType("datetime");

            entity.HasOne(d => d.CreateBy).WithMany(p => p.NutritionRouteCreateBies)
                .HasForeignKey(d => d.CreateById)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Nutrition__Creat__6E01572D");

            entity.HasOne(d => d.User).WithMany(p => p.NutritionRouteUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Nutrition__UserI__6EF57B66");
        });

        modelBuilder.Entity<NutritionTargetsDaily>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Nutritio__3214EC07F1454B74");

            entity.ToTable("NutritionTargetsDaily", "UserData");

            entity.Property(e => e.AvoidIngredient).HasMaxLength(500);
            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasOne(d => d.FoodTypeIdWantNavigation).WithMany(p => p.NutritionTargetsDailies)
                .HasForeignKey(d => d.FoodTypeIdWant)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Nutrition__FoodT__0E6E26BF");

            entity.HasOne(d => d.User).WithMany(p => p.NutritionTargetsDailies)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Nutrition__UserI__0F624AF8");
        });

        modelBuilder.Entity<NutritionistDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Nutritio__3214EC07CE15EEBD");

            entity.ToTable("NutritionistDetails", "UserData");

            entity.HasIndex(e => new { e.NutritionistId, e.ExpertPackagesId }, "UQ__Nutritio__9C31FC2DD5638127").IsUnique();

            entity.HasIndex(e => e.NutritionistId, "UQ__Nutritio__F4399C8D1ADD8E57").IsUnique();

            entity.Property(e => e.DescribeYourself).HasMaxLength(500);

            entity.HasOne(d => d.ExpertPackages).WithMany(p => p.NutritionistDetails)
                .HasForeignKey(d => d.ExpertPackagesId)
                .HasConstraintName("FK__Nutrition__Exper__0C85DE4D");

            entity.HasOne(d => d.Nutritionist).WithOne(p => p.NutritionistDetail)
                .HasForeignKey<NutritionistDetail>(d => d.NutritionistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Nutrition__Nutri__0D7A0286");
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasKey(e => e.RecipeId).HasName("PK__Recipe__FDD988B0F469B8F8");

            entity.ToTable("Recipe", "FoodData");

            entity.Property(e => e.Describe).HasMaxLength(500);
            entity.Property(e => e.Urlimage)
                .HasMaxLength(512)
                .IsUnicode(false)
                .HasColumnName("URLImage");

            entity.HasOne(d => d.FoodList).WithMany(p => p.Recipes)
                .HasForeignKey(d => d.FoodListId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Recipe__FoodList__7E37BEF6");
        });

        modelBuilder.Entity<RecurringSetting>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Recurrin__3214EC0776F2C7C7");

            entity.ToTable("RecurringSettings", "UserData");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__8AFACE3ADC18F501");

            entity.ToTable("Role", "UserData");

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Rooms__3214EC07E83D2917");

            entity.ToTable("Rooms", "Business");

            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.Nutrition).WithMany(p => p.RoomNutritions)
                .HasForeignKey(d => d.NutritionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Rooms__Nutrition__6FE99F9F");

            entity.HasOne(d => d.User).WithMany(p => p.RoomUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Rooms__UserId__70DDC3D8");
        });

        modelBuilder.Entity<ScaleAmount>(entity =>
        {
            entity.HasKey(e => new { e.FoodListId, e.IngredientDetailsId }).HasName("PK__ScaleAmo__8F2BA49AD6BD2B9D");

            entity.ToTable("ScaleAmount", "FoodData");

            entity.Property(e => e.ScaleAmount1).HasColumnName("ScaleAmount");

            entity.HasOne(d => d.FoodList).WithMany(p => p.ScaleAmounts)
                .HasForeignKey(d => d.FoodListId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ScaleAmou__FoodL__7F2BE32F");

            entity.HasOne(d => d.IngredientDetails).WithMany(p => p.ScaleAmounts)
                .HasForeignKey(d => d.IngredientDetailsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ScaleAmou__Ingre__00200768");
        });

        modelBuilder.Entity<SlotOfTheDay>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SlotOfTh__3214EC0796594A00");

            entity.ToTable("SlotOfTheDay", "Business");

            entity.Property(e => e.Slot).HasMaxLength(50);
        });

        modelBuilder.Entity<TransactionsSystem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Transact__3214EC079824095D");

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
                .HasConstraintName("FK__Transacti__Payee__71D1E811");

            entity.HasOne(d => d.UserPay).WithMany(p => p.TransactionsSystemUserPays)
                .HasForeignKey(d => d.UserPayId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Transacti__UserP__72C60C4A");
        });

        modelBuilder.Entity<TypeOfCalculation>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("PK__TypeOfCa__516F03B5B2D95D7C");

            entity.ToTable("TypeOfCalculation", "FoodData");

            entity.Property(e => e.CalculationForm).HasMaxLength(500);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CC4C3BABB7B0");

            entity.ToTable("User", "UserData");

            entity.HasIndex(e => e.Account, "UQ_Account").IsUnique();

            entity.Property(e => e.Account)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValue("example@example.com");
            entity.Property(e => e.AccountGoogle).HasMaxLength(255);
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
                .HasConstraintName("FK__User__Role__10566F31");
        });

        modelBuilder.Entity<UserDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserDeta__3214EC071BA9F240");

            entity.ToTable("UserDetails", "UserData");

            entity.HasIndex(e => e.UserId, "UQ__UserDeta__1788CC4D9F9D47C2").IsUnique();

            entity.Property(e => e.DescribeYourself).HasMaxLength(500);
            entity.Property(e => e.InforConfirmBad).HasMaxLength(500);
            entity.Property(e => e.InforConfirmGood).HasMaxLength(500);
            entity.Property(e => e.TimeUpdate).HasColumnType("datetime");
            entity.Property(e => e.UnderlyingDisease).HasMaxLength(500);
            entity.Property(e => e.WantImprove).HasMaxLength(500);

            entity.HasOne(d => d.User).WithOne(p => p.UserDetail)
                .HasForeignKey<UserDetail>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserDetai__UserI__114A936A");
        });

        modelBuilder.Entity<UserListManagement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserList__3214EC07A9FA1F1B");

            entity.ToTable("UserListManagement", "Business");

            entity.Property(e => e.Describe).HasMaxLength(500);
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.StartDate).HasColumnType("datetime");

            entity.HasOne(d => d.Nutritionist).WithMany(p => p.UserListManagementNutritionists)
                .HasForeignKey(d => d.NutritionistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserListM__Nutri__73BA3083");

            entity.HasOne(d => d.User).WithMany(p => p.UserListManagementUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserListM__UserI__74AE54BC");
        });

        modelBuilder.Entity<WantCooking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__WantCook__3214EC079827138F");

            entity.ToTable("WantCooking", "UserData");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
