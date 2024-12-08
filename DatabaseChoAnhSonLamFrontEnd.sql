USE [SEP490_G87_VitaNutrientSystem]
GO
/****** Object:  Schema [Business]    Script Date: 12/6/2024 3:55:06 AM ******/
CREATE SCHEMA [Business]
GO
/****** Object:  Schema [FoodData]    Script Date: 12/6/2024 3:55:06 AM ******/
CREATE SCHEMA [FoodData]
GO
/****** Object:  Schema [UserData]    Script Date: 12/6/2024 3:55:06 AM ******/
CREATE SCHEMA [UserData]
GO
/****** Object:  Table [Business].[ArticlesNews]    Script Date: 12/6/2024 3:55:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Business].[ArticlesNews](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[NameCreater] [nvarchar](50) NULL,
	[Title] [nvarchar](255) NULL,
	[Content] [nvarchar](max) NULL,
	[IsActive] [bit] NULL,
	[DateCreated] [datetime] NULL,
	[HeaderImage] [nvarchar](255) NULL,
	[Rate] [float] NULL,
	[NumberRate] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [Business].[DayOfTheWeek]    Script Date: 12/6/2024 3:55:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Business].[DayOfTheWeek](
	[Id] [smallint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Business].[MealOfTheDay]    Script Date: 12/6/2024 3:55:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Business].[MealOfTheDay](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NutritionRouteId] [int] NOT NULL,
	[DataFoodListId] [varchar](512) NULL,
	[DateExecute] [date] NULL,
	[IsDone] [bit] NULL,
	[IsEditByUser] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Business].[Messages]    Script Date: 12/6/2024 3:55:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Business].[Messages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Content] [nvarchar](500) NULL,
	[Timestamp] [datetime] NULL,
	[FromUserId] [int] NOT NULL,
	[ToRoomId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Business].[NewsEvaluation]    Script Date: 12/6/2024 3:55:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Business].[NewsEvaluation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ArticlesNewsId] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[Ratting] [smallint] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Business].[NutritionRoute]    Script Date: 12/6/2024 3:55:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Business].[NutritionRoute](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[CreateById] [int] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Describe] [nvarchar](500) NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[IsDone] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Business].[Rooms]    Script Date: 12/6/2024 3:55:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Business].[Rooms](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[NutritionId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Business].[SlotOfTheDay]    Script Date: 12/6/2024 3:55:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Business].[SlotOfTheDay](
	[Id] [smallint] IDENTITY(1,1) NOT NULL,
	[Slot] [nvarchar](50) NULL,
	[StartAt] [time](7) NULL,
	[EndAt] [time](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Business].[TransactionsSystem]    Script Date: 12/6/2024 3:55:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Business].[TransactionsSystem](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserPayID] [int] NOT NULL,
	[PayeeID] [int] NOT NULL,
	[APITransactions] [int] NULL,
	[BankBrandName] [varchar](50) NULL,
	[AccountNumber] [varchar](50) NULL,
	[TransactionDate] [datetime] NULL,
	[AmountOut] [money] NULL,
	[AmountIn] [money] NULL,
	[Accumulated] [money] NULL,
	[TransactionContent] [varchar](500) NULL,
	[ReferenceNumber] [varchar](100) NULL,
	[Code] [varchar](100) NULL,
	[SubAccount] [varchar](50) NULL,
	[BankAccountId] [int] NULL,
	[Status] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Business].[UserListManagement]    Script Date: 12/6/2024 3:55:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Business].[UserListManagement](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NutritionistId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[Describe] [nvarchar](500) NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[Rate] [smallint] NULL,
	[IsDone] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [FoodData].[CookingDifficulty]    Script Date: 12/6/2024 3:55:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [FoodData].[CookingDifficulty](
	[Id] [smallint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [FoodData].[DietType]    Script Date: 12/6/2024 3:55:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [FoodData].[DietType](
	[DietTypeId] [smallint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Describe] [nvarchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[DietTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [FoodData].[DietWithFoodType]    Script Date: 12/6/2024 3:55:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [FoodData].[DietWithFoodType](
	[FoodTypeId] [smallint] NOT NULL,
	[DietTypeId] [smallint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[FoodTypeId] ASC,
	[DietTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [FoodData].[FoodAndDiseases]    Script Date: 12/6/2024 3:55:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [FoodData].[FoodAndDiseases](
	[ListOfDiseasesId] [int] NOT NULL,
	[FoodListId] [int] NOT NULL,
	[Describe] [nvarchar](500) NULL,
	[IsGoodOrBad] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[ListOfDiseasesId] ASC,
	[FoodListId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [FoodData].[FoodList]    Script Date: 12/6/2024 3:55:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [FoodData].[FoodList](
	[FoodListId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Describe] [nvarchar](500) NULL,
	[Rate] [float] NULL,
	[NumberRate] [int] NULL,
	[URLImage] [varchar](512) NULL,
	[FoodTypeId] [smallint] NOT NULL,
	[KeyNoteId] [int] NULL,
	[IsActive] [bit] NULL,
	[PreparationTime] [smallint] NULL,
	[CookingTime] [smallint] NULL,
	[CookingDifficultyId] [smallint] NULL,
PRIMARY KEY CLUSTERED 
(
	[FoodListId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [FoodData].[FoodType]    Script Date: 12/6/2024 3:55:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [FoodData].[FoodType](
	[FoodTypeId] [smallint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Describe] [nvarchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[FoodTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [FoodData].[IngredientDetails100g]    Script Date: 12/6/2024 3:55:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [FoodData].[IngredientDetails100g](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[KeyNoteId] [int] NULL,
	[Name] [nvarchar](50) NULL,
	[Describe] [nvarchar](500) NULL,
	[URLImage] [varchar](512) NULL,
	[TypeOfCalculationId] [smallint] NOT NULL,
	[Energy] [smallint] NULL,
	[Water] [float] NULL,
	[Protein] [float] NULL,
	[Fat] [float] NULL,
	[Carbohydrate] [float] NULL,
	[Fiber] [float] NULL,
	[Ash] [float] NULL,
	[Sugar] [float] NULL,
	[Galactose] [float] NULL,
	[Maltose] [float] NULL,
	[Lactose] [float] NULL,
	[Fructose] [float] NULL,
	[Glucose] [float] NULL,
	[Sucrose] [float] NULL,
	[Calcium] [float] NULL,
	[Iron] [float] NULL,
	[Magnesium] [float] NULL,
	[Manganese] [float] NULL,
	[Phosphorous] [float] NULL,
	[Potassium] [float] NULL,
	[Sodium] [float] NULL,
	[Zinc] [float] NULL,
	[Copper] [float] NULL,
	[Selenium] [float] NULL,
	[Vitamin_C] [float] NULL,
	[Vitamin_B1] [float] NULL,
	[Vitamin_B2] [float] NULL,
	[Vitamin_PP] [float] NULL,
	[Vitamin_B5] [float] NULL,
	[Vitamin_B6] [float] NULL,
	[Folat] [float] NULL,
	[Vitamin_B9] [float] NULL,
	[Vitamin_H] [float] NULL,
	[Vitamin_B12] [float] NULL,
	[Vitamin_A] [float] NULL,
	[Vitamin_D] [float] NULL,
	[Vitamin_E] [float] NULL,
	[Vitamin_K] [float] NULL,
	[Beta_caroten] [float] NULL,
	[Alpha_caroten] [float] NULL,
	[Beta_cryptoxanthin] [float] NULL,
	[Lycopen] [float] NULL,
	[LuteinVsZeaxanthin] [float] NULL,
	[Purin] [float] NULL,
	[Total_isoflavone] [float] NULL,
	[Daidzein] [float] NULL,
	[Genistein] [float] NULL,
	[Glycetin] [float] NULL,
	[Total_saturated_fatty_acid] [float] NULL,
	[Palmitic_C16_0] [float] NULL,
	[Margaric_C17_0] [float] NULL,
	[Stearic_C18_0] [float] NULL,
	[Arachidic_C20_0] [float] NULL,
	[Behenic_C22_0] [float] NULL,
	[Lignoceric_C24_0] [float] NULL,
	[TotalMonounsaturatedFattyAcid] [float] NULL,
	[Myristoleic_C14_1] [float] NULL,
	[Palmitoleic_C16_1] [float] NULL,
	[Oleic_C18_1] [float] NULL,
	[TotalPolyunsaturatedFattyAcid] [float] NULL,
	[Linoleic_C18_2_n6] [float] NULL,
	[Linolenic_C18_2_n3] [float] NULL,
	[Arachidonic_C20_4] [float] NULL,
	[Eicosapentaenoic_C20_5_n3] [float] NULL,
	[Docosahexaenoic_C22_6_n3] [float] NULL,
	[TotalTransFattyAcid] [float] NULL,
	[Cholesterol] [float] NULL,
	[Phytosterol] [float] NULL,
	[Lysin] [float] NULL,
	[Methionin] [float] NULL,
	[Tryptophan] [float] NULL,
	[Phenylalanin] [float] NULL,
	[Threonin] [float] NULL,
	[Valin] [float] NULL,
	[Leucin] [float] NULL,
	[Isoleucin] [float] NULL,
	[Arginin] [float] NULL,
	[Histidin] [float] NULL,
	[Cystin] [float] NULL,
	[Tyrosin] [float] NULL,
	[Alanin] [float] NULL,
	[Acid_aspartic] [float] NULL,
	[Acid_glutamic] [float] NULL,
	[Glycin] [float] NULL,
	[Prolin] [float] NULL,
	[Serin] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [FoodData].[KeyNote]    Script Date: 12/6/2024 3:55:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [FoodData].[KeyNote](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[KeyList] [nvarchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [FoodData].[Recipe]    Script Date: 12/6/2024 3:55:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [FoodData].[Recipe](
	[RecipeId] [int] IDENTITY(1,1) NOT NULL,
	[FoodListId] [int] NOT NULL,
	[NumericalOrder] [smallint] NULL,
	[Describe] [nvarchar](500) NULL,
	[URLImage] [varchar](512) NULL,
PRIMARY KEY CLUSTERED 
(
	[RecipeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [FoodData].[ScaleAmount]    Script Date: 12/6/2024 3:55:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [FoodData].[ScaleAmount](
	[FoodListId] [int] NOT NULL,
	[IngredientDetailsId] [int] NOT NULL,
	[ScaleAmount] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[FoodListId] ASC,
	[IngredientDetailsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [FoodData].[TypeOfCalculation]    Script Date: 12/6/2024 3:55:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [FoodData].[TypeOfCalculation](
	[TypeId] [smallint] IDENTITY(1,1) NOT NULL,
	[CalculationForm] [nvarchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[TypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [UserData].[ExpertPackages]    Script Date: 12/6/2024 3:55:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [UserData].[ExpertPackages](
	[Id] [smallint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Describe] [nvarchar](500) NULL,
	[Price] [money] NULL,
	[Duration] [smallint] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [UserData].[FoodSelection]    Script Date: 12/6/2024 3:55:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [UserData].[FoodSelection](
	[UserId] [int] NOT NULL,
	[FoodListId] [int] NOT NULL,
	[Rate] [smallint] NULL,
	[RecurringId] [smallint] NULL,
	[IsBlock] [bit] NULL,
	[IsCollection] [bit] NULL,
	[IsLike] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[FoodListId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [UserData].[ListOfDiseases]    Script Date: 12/6/2024 3:55:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [UserData].[ListOfDiseases](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Describe] [nvarchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [UserData].[MealSettings]    Script Date: 12/6/2024 3:55:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [UserData].[MealSettings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[DayOfTheWeekStartId] [smallint] NULL,
	[SameScheduleEveryDay] [bit] NULL,
	[FoodTypeIdWant] [smallint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [UserData].[MealSettingsDetails]    Script Date: 12/6/2024 3:55:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [UserData].[MealSettingsDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MealSettingsId] [int] NOT NULL,
	[SlotOfTheDayId] [smallint] NULL,
	[NutritionTargetsDailyId] [int] NULL,
	[DayOfTheWeekId] [smallint] NULL,
	[SkipCreationProcess] [bit] NULL,
	[Size] [nvarchar](50) NULL,
	[NutritionFocus] [bit] NULL,
	[NumberOfDishes] [smallint] NULL,
	[TypeFavoriteFood] [nvarchar](50) NULL,
	[WantCookingId] [smallint] NULL,
	[TimeAvailable] [smallint] NULL,
	[CookingDifficultyId] [smallint] NULL,
	[Name] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
	[OrderNumber] [smallint] NULL,
	[Calories] [smallint] NULL,
	[CarbsMin] [smallint] NULL,
	[CarbsMax] [smallint] NULL,
	[FatsMin] [smallint] NULL,
	[FatsMax] [smallint] NULL,
	[ProteinMin] [smallint] NULL,
	[ProteinMax] [smallint] NULL,
	[MinimumFiber] [smallint] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [UserData].[NutritionistDetails]    Script Date: 12/6/2024 3:55:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [UserData].[NutritionistDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NutritionistId] [int] NOT NULL,
	[DescribeYourself] [nvarchar](500) NULL,
	[Height] [smallint] NULL,
	[Weight] [smallint] NULL,
	[Age] [smallint] NULL,
	[Rate] [float] NULL,
	[NumberRate] [int] NULL,
	[ExpertPackagesId] [smallint] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [UserData].[NutritionTargetsDaily]    Script Date: 12/6/2024 3:55:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [UserData].[NutritionTargetsDaily](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Title] [nvarchar](255) NULL,
	[Calories] [smallint] NULL,
	[CarbsMin] [smallint] NULL,
	[CarbsMax] [smallint] NULL,
	[FatsMin] [smallint] NULL,
	[FatsMax] [smallint] NULL,
	[ProteinMin] [smallint] NULL,
	[ProteinMax] [smallint] NULL,
	[MinimumFiber] [smallint] NULL,
	[LimitDailySodium] [bit] NULL,
	[LimitDailyCholesterol] [bit] NULL,
	[ExerciseIntensityId] [smallint] NOT NULL,
	[AvoidIngredient] [nvarchar](500) NULL,
	[IsActive] [bit] NULL,
	[FoodTypeIdWant] [smallint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [UserData].[RecurringSettings]    Script Date: 12/6/2024 3:55:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [UserData].[RecurringSettings](
	[Id] [smallint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Frequency] [smallint] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [UserData].[Role]    Script Date: 12/6/2024 3:55:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [UserData].[Role](
	[RoleID] [smallint] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [UserData].[User]    Script Date: 12/6/2024 3:55:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [UserData].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[URLImage] [varchar](512) NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[DOB] [datetime] NULL,
	[Gender] [bit] NULL,
	[Address] [nvarchar](255) NULL,
	[Phone] [varchar](15) NULL,
	[Password] [varchar](100) NOT NULL,
	[Role] [smallint] NOT NULL,
	[IsActive] [bit] NULL,
	[Account] [varchar](255) NOT NULL,
	[AccountGoogle] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [UserData].[UserDetails]    Script Date: 12/6/2024 3:55:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [UserData].[UserDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[DescribeYourself] [nvarchar](500) NULL,
	[Height] [smallint] NULL,
	[Weight] [smallint] NULL,
	[Age] [smallint] NULL,
	[WantImprove] [nvarchar](500) NULL,
	[UnderlyingDisease] [nvarchar](500) NULL,
	[InforConfirmGood] [nvarchar](500) NULL,
	[InforConfirmBad] [nvarchar](500) NULL,
	[IsPremium] [bit] NULL,
	[ActivityLevel] [float] NULL,
	[Calo] [int] NULL,
	[TimeUpdate] [datetime] NULL,
	[WeightGoal] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [UserData].[WantCooking]    Script Date: 12/6/2024 3:55:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [UserData].[WantCooking](
	[Id] [smallint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [Business].[ArticlesNews] ON 

INSERT [Business].[ArticlesNews] ([Id], [UserID], [NameCreater], [Title], [Content], [IsActive], [DateCreated], [HeaderImage], [Rate], [NumberRate]) VALUES (1, 1, N'John Doe', N'Health Tips', N'Here are some tips...', 1, NULL, NULL, 2, 1)
INSERT [Business].[ArticlesNews] ([Id], [UserID], [NameCreater], [Title], [Content], [IsActive], [DateCreated], [HeaderImage], [Rate], [NumberRate]) VALUES (2, 2, N'Jane Smith', N'Diet Plans', N'Learn about diet plans...', 1, NULL, NULL, 3.3333333333333335, 3)
INSERT [Business].[ArticlesNews] ([Id], [UserID], [NameCreater], [Title], [Content], [IsActive], [DateCreated], [HeaderImage], [Rate], [NumberRate]) VALUES (3, 3, N'Emily Brown', N'Exercise Routines', N'Best exercises for you...', 1, NULL, NULL, NULL, NULL)
INSERT [Business].[ArticlesNews] ([Id], [UserID], [NameCreater], [Title], [Content], [IsActive], [DateCreated], [HeaderImage], [Rate], [NumberRate]) VALUES (4, 1, N'Michael Johnson', N'Nutrition Facts TMT', N'<p>Important nutrition info...</p>', 0, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [Business].[ArticlesNews] OFF
GO
SET IDENTITY_INSERT [Business].[DayOfTheWeek] ON 

INSERT [Business].[DayOfTheWeek] ([Id], [Name]) VALUES (1, N'Thứ Hai')
INSERT [Business].[DayOfTheWeek] ([Id], [Name]) VALUES (2, N'Thứ Ba')
INSERT [Business].[DayOfTheWeek] ([Id], [Name]) VALUES (3, N'Thứ Tư')
INSERT [Business].[DayOfTheWeek] ([Id], [Name]) VALUES (4, N'Thứ Năm')
INSERT [Business].[DayOfTheWeek] ([Id], [Name]) VALUES (5, N'Thứ Sáu')
INSERT [Business].[DayOfTheWeek] ([Id], [Name]) VALUES (6, N'Thứ Bảy')
INSERT [Business].[DayOfTheWeek] ([Id], [Name]) VALUES (7, N'Chủ nhật')
INSERT [Business].[DayOfTheWeek] ([Id], [Name]) VALUES (8, N'Cả Tuần')
SET IDENTITY_INSERT [Business].[DayOfTheWeek] OFF
GO
SET IDENTITY_INSERT [Business].[MealOfTheDay] ON 

INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (1, 1, N'SlotOfTheDay=1;SettingDetail=1;OrderNumber=1:
#
SlotOfTheDay=1;SettingDetail=4;OrderNumber=2:
4-;4-;6-;#
SlotOfTheDay=3;SettingDetail=5;OrderNumber=3:
6-;4-;6-;#
SlotOfTheDay=4;SettingDetail=6;OrderNumber=4:
4-;6-;6-;#
', CAST(N'2024-10-24' AS Date), NULL, NULL)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (2, 2, N'SlotOfTheDay=1;SettingDetail=1;OrderNumber=1:
#
SlotOfTheDay=1;SettingDetail=4;OrderNumber=2:
4-;4-;6-;#
SlotOfTheDay=3;SettingDetail=5;OrderNumber=3:
6-;4-;6-;#
SlotOfTheDay=4;SettingDetail=6;OrderNumber=4:
4-;6-;6-;#
', CAST(N'2024-10-24' AS Date), NULL, NULL)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (3, 5, N'SlotOfTheDay=1;SettingDetail=1;OrderNumber=1:
#
SlotOfTheDay=1;SettingDetail=4;OrderNumber=2:
4-;4-;6-;#
SlotOfTheDay=3;SettingDetail=5;OrderNumber=3:
6-;4-;6-;#
SlotOfTheDay=4;SettingDetail=6;OrderNumber=4:
4-;6-;6-;#
', CAST(N'2024-10-27' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (5, 5, N'SlotOfTheDay=1;SettingDetail=1;OrderNumber=1:
#
SlotOfTheDay=1;SettingDetail=4;OrderNumber=2:
4-;4-;6-;#
SlotOfTheDay=3;SettingDetail=5;OrderNumber=3:
6-;4-;6-;#
SlotOfTheDay=4;SettingDetail=6;OrderNumber=4:
4-;6-;6-;#
', CAST(N'2024-10-28' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (19, 5, N'SlotOfTheDay=1;SettingDetail=1;OrderNumber=1:
#SlotOfTheDay=1;SettingDetail=4;OrderNumber=2:
4+;4+;6-;#SlotOfTheDay=3;SettingDetail=5;OrderNumber=3:
6+;4+;6+;#SlotOfTheDay=4;SettingDetail=6;OrderNumber=4:
4+;6+;6+;#
', CAST(N'2024-11-04' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (20, 5, N'SlotOfTheDay=1;SettingDetail=1;OrderNumber=2:
3-;6-;6-;#
SlotOfTheDay=1;SettingDetail=4;OrderNumber=1:
4-;4-;4-;#
SlotOfTheDay=3;SettingDetail=5;OrderNumber=3:
6-;3-;4-;#
SlotOfTheDay=4;SettingDetail=6;OrderNumber=4:
4-;3-;4-;#
', CAST(N'2024-10-30' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (21, 5, N'SlotOfTheDay=1;SettingDetail=1;OrderNumber=1:
#
SlotOfTheDay=1;SettingDetail=4;OrderNumber=2:
4-;4-;6-;#
SlotOfTheDay=3;SettingDetail=5;OrderNumber=3:
6-;4-;6-;#
SlotOfTheDay=4;SettingDetail=6;OrderNumber=4:
4-;6-;6-;#
', CAST(N'2024-11-01' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (26, 5, N'SlotOfTheDay=1;SettingDetail=1;OrderNumber=1:
#
SlotOfTheDay=1;SettingDetail=4;OrderNumber=2:
4-;4-;6-;#
SlotOfTheDay=3;SettingDetail=5;OrderNumber=3:
6-;4-;6-;#
SlotOfTheDay=4;SettingDetail=6;OrderNumber=4:
4-;6-;6-;#
', CAST(N'2024-11-11' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (27, 5, N'SlotOfTheDay=1;SettingDetail=1;OrderNumber=1:
#
SlotOfTheDay=1;SettingDetail=4;OrderNumber=2:
4-;4-;6-;#
SlotOfTheDay=3;SettingDetail=5;OrderNumber=3:
6-;4-;6-;#
SlotOfTheDay=4;SettingDetail=6;OrderNumber=4:
4-;6-;6-;#
', CAST(N'2024-11-14' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (28, 5, N'SlotOfTheDay=1;SettingDetail=1;OrderNumber=1:
#
SlotOfTheDay=1;SettingDetail=4;OrderNumber=2:
4-;4-;6-;#
SlotOfTheDay=3;SettingDetail=5;OrderNumber=3:
6-;4-;6-;#
SlotOfTheDay=4;SettingDetail=6;OrderNumber=4:
4-;6-;6-;#
', CAST(N'2024-10-29' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (29, 6, N'SlotOfTheDay=1;SettingDetail=1;OrderNumber=1:
#
SlotOfTheDay=1;SettingDetail=4;OrderNumber=2:
4-;4-;6-;#
SlotOfTheDay=3;SettingDetail=5;OrderNumber=3:
6-;4-;6-;#
SlotOfTheDay=4;SettingDetail=6;OrderNumber=4:
4-;6-;6-;#
', CAST(N'2024-10-23' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (30, 7, N'SlotOfTheDay=1;SettingDetail=1;OrderNumber=1:
#
SlotOfTheDay=1;SettingDetail=4;OrderNumber=2:
4-;4-;6-;#
SlotOfTheDay=3;SettingDetail=5;OrderNumber=3:
6-;4-;6-;#
SlotOfTheDay=4;SettingDetail=6;OrderNumber=4:
4-;6-;6-;#
', CAST(N'2024-10-23' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (31, 8, N'SlotOfTheDay=1;SettingDetail=1;OrderNumber=1:
#
SlotOfTheDay=1;SettingDetail=4;OrderNumber=2:
4-;4-;6-;#
SlotOfTheDay=3;SettingDetail=5;OrderNumber=3:
6-;4-;6-;#
SlotOfTheDay=4;SettingDetail=6;OrderNumber=4:
4-;6-;6-;#
', CAST(N'2020-09-29' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (32, 9, N'SlotOfTheDay=1;SettingDetail=1;OrderNumber=1:
#
SlotOfTheDay=1;SettingDetail=4;OrderNumber=2:
4-;4-;6-;#
SlotOfTheDay=3;SettingDetail=5;OrderNumber=3:
6-;4-;6-;#
SlotOfTheDay=4;SettingDetail=6;OrderNumber=4:
4-;6-;6-;#
', CAST(N'2020-09-29' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (33, 10, N'SlotOfTheDay=1;SettingDetail=1;OrderNumber=1:
#
SlotOfTheDay=1;SettingDetail=4;OrderNumber=2:
4-;4-;6-;#
SlotOfTheDay=3;SettingDetail=5;OrderNumber=3:
6-;4-;6-;#
SlotOfTheDay=4;SettingDetail=6;OrderNumber=4:
4-;6-;6-;#
', CAST(N'2020-09-29' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (34, 11, N'SlotOfTheDay=1;SettingDetail=1;OrderNumber=1:
#
SlotOfTheDay=1;SettingDetail=4;OrderNumber=2:
4-;4-;6-;#
SlotOfTheDay=3;SettingDetail=5;OrderNumber=3:
6-;4-;6-;#
SlotOfTheDay=4;SettingDetail=6;OrderNumber=4:
4-;6-;6-;#
', CAST(N'2024-10-03' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (35, 12, N'SlotOfTheDay=1;SettingDetail=8;OrderNumber=1:
6+;4-;3-;#
SlotOfTheDay=2;SettingDetail=10;OrderNumber=1:
4-;3-;6-;#
SlotOfTheDay=3;SettingDetail=11;OrderNumber=2:
4-;3-;6-;#SlotOfTheDay=1;SettingDetail=13;OrderNumber=1:
6-;4-;4-;#
', CAST(N'2024-11-05' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (36, 13, NULL, CAST(N'2024-11-08' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (37, 12, N'SlotOfTheDay=1;SettingDetail=8;OrderNumber=1:
6-;4-;3-;#
SlotOfTheDay=1;SettingDetail=14;OrderNumber=2:
#
', CAST(N'2024-11-08' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (38, 12, N'SlotOfTheDay=1;SettingDetail=8;OrderNumber=1:
4-;6-;6-;#
SlotOfTheDay=1;SettingDetail=14;OrderNumber=2:
3-;4-;6-;#
', CAST(N'2024-11-09' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (39, 16, N'SlotOfTheDay=1;SettingDetail=18;OrderNumber=1:
4-;4+;3-;#
SlotOfTheDay=1;SettingDetail=19;OrderNumber=3:
#
SlotOfTheDay=1;SettingDetail=20;OrderNumber=2:
#
SlotOfTheDay=1;SettingDetail=21;OrderNumber=4:
#
', CAST(N'2024-11-09' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (40, 12, N'SlotOfTheDay=1;SettingDetail=8;OrderNumber=1:
4-;4-;6-;#
SlotOfTheDay=1;SettingDetail=14;OrderNumber=2:
6-;6-;3-;#
', CAST(N'2024-11-10' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (41, 12, N'SlotOfTheDay=1;SettingDetail=8;OrderNumber=1:
6-;3-;3-;#SlotOfTheDay=1;SettingDetail=14;OrderNumber=2:
6-;6-;3-;#
', CAST(N'2024-11-11' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (42, 12, N'SlotOfTheDay=1;SettingDetail=8;OrderNumber=1:
6-;6-;3-;#
SlotOfTheDay=1;SettingDetail=14;OrderNumber=2:
#
', CAST(N'2024-12-07' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (43, 12, N'SlotOfTheDay=1;SettingDetail=8;OrderNumber=1:
#
SlotOfTheDay=1;SettingDetail=14;OrderNumber=2:
6-;6-;4-;#
', CAST(N'2024-12-11' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (44, 12, N'SlotOfTheDay=1;SettingDetail=8;OrderNumber=1:
#
SlotOfTheDay=1;SettingDetail=14;OrderNumber=2:
3-;4-;6-;#
', CAST(N'2024-11-12' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (45, 12, N'SlotOfTheDay=1;SettingDetail=8;OrderNumber=1:
3-;6-;6-;#
SlotOfTheDay=1;SettingDetail=14;OrderNumber=2:
6-;6-;3-;#
', CAST(N'2024-11-13' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (46, 12, N'SlotOfTheDay=1;SettingDetail=8;OrderNumber=1:
4-;6-;6-;#
SlotOfTheDay=1;SettingDetail=14;OrderNumber=2:
#
', CAST(N'2024-11-14' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (47, 12, N'SlotOfTheDay=1;SettingDetail=8;OrderNumber=1:
3-;6-;6-;#
SlotOfTheDay=1;SettingDetail=14;OrderNumber=2:
6-;4-;4-;#
', CAST(N'2024-11-15' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (48, 12, N'SlotOfTheDay=1;SettingDetail=8;OrderNumber=1:
3-;3-;6-;#
SlotOfTheDay=1;SettingDetail=14;OrderNumber=2:
4-;6-;4-;#
', CAST(N'2024-11-30' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (49, 12, N'SlotOfTheDay=1;SettingDetail=8;OrderNumber=1:
3-;3-;6-;#
SlotOfTheDay=1;SettingDetail=14;OrderNumber=2:
4-;6-;4-;#
', CAST(N'2024-11-07' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (50, 19, N'SlotOfTheDay=1;SettingDetail=22;OrderNumber=1:
1-;6-;4-;#
', CAST(N'2024-11-12' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (51, 19, N'SlotOfTheDay=1;SettingDetail=23;OrderNumber=1:
#
SlotOfTheDay=1;SettingDetail=24;OrderNumber=1:
#
SlotOfTheDay=1;SettingDetail=27;OrderNumber=2:
#
', CAST(N'2024-11-14' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (52, 19, N'#
', CAST(N'2024-11-19' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (53, 19, N'SlotOfTheDay=1;SettingDetail=23;OrderNumber=1:
6-;4-;4-;#SlotOfTheDay=1;SettingDetail=24;OrderNumber=1:
6-;3-;6-;#SlotOfTheDay=1;SettingDetail=27;OrderNumber=2:
3-;6-;4-;#
', CAST(N'2024-11-21' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (54, 19, N'', CAST(N'2024-11-13' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (55, 19, N'#
', CAST(N'2024-11-20' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (56, 19, N'#
', CAST(N'2024-11-26' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (57, 19, N'#
', CAST(N'2024-11-15' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (58, 19, N'#
', CAST(N'2024-11-16' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (59, 19, N'#
', CAST(N'2024-11-22' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (60, 19, N'#
', CAST(N'2024-11-23' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (61, 19, N'', CAST(N'2024-11-17' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (62, 19, N'', CAST(N'2024-11-18' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (63, 21, N'SlotOfTheDay=1;SettingDetail=29;OrderNumber=1:
6-;3-;1-;#
', CAST(N'2024-11-12' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (64, 22, N'', CAST(N'2024-11-12' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (65, 21, N'SlotOfTheDay=1;SettingDetail=29;OrderNumber=1:
4-;6-;3-;#
', CAST(N'2024-11-13' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (66, 21, N'SlotOfTheDay=1;SettingDetail=29;OrderNumber=1:
6-;4-;4-;#
', CAST(N'2024-11-14' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (67, 21, N'SlotOfTheDay=1;SettingDetail=29;OrderNumber=1:
6-;1-;6-;#
', CAST(N'2024-11-15' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (68, 21, N'SlotOfTheDay=1;SettingDetail=29;OrderNumber=1:
#
', CAST(N'2024-11-16' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (69, 23, N'SlotOfTheDay=1;SettingDetail=30;OrderNumber=1:
4-;3-;4-;#
', CAST(N'2024-11-12' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (70, 24, N'SlotOfTheDay=1;SettingDetail=30;OrderNumber=1:
1+;6-;4-;#
', CAST(N'2024-11-12' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (71, 23, N'', CAST(N'2024-11-13' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (72, 23, N'SlotOfTheDay=1;SettingDetail=31;OrderNumber=1:
6-;3-;4-;#
', CAST(N'2024-11-14' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (73, 23, N'SlotOfTheDay=1;SettingDetail=32;OrderNumber=1:
#
SlotOfTheDay=1;SettingDetail=33;OrderNumber=2:
#
SlotOfTheDay=3;SettingDetail=34;OrderNumber=1:
#
', CAST(N'2024-11-15' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (74, 23, N'', CAST(N'2024-11-16' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (75, 23, N'', CAST(N'2024-11-17' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (76, 23, N'', CAST(N'2024-11-18' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (77, 23, N'', CAST(N'2024-11-19' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (78, 23, N'', CAST(N'2024-11-20' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (79, 23, N'SlotOfTheDay=1;SettingDetail=31;OrderNumber=1:
1-;6-;3-;#
', CAST(N'2024-11-21' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (80, 23, N'SlotOfTheDay=1;SettingDetail=32;OrderNumber=1:
3-;4-;4-;#
SlotOfTheDay=1;SettingDetail=33;OrderNumber=2:
4-;4-;4-;#
SlotOfTheDay=3;SettingDetail=34;OrderNumber=1:
6-;1-;3-;#
', CAST(N'2024-11-22' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (81, 23, N'', CAST(N'2024-11-23' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (82, 23, N'', CAST(N'2024-11-24' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (83, 23, N'', CAST(N'2024-11-25' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (84, 23, N'', CAST(N'2024-11-30' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (85, 23, N'', CAST(N'2024-11-26' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (86, 23, N'', CAST(N'2024-11-27' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (87, 23, N'SlotOfTheDay=1;SettingDetail=31;OrderNumber=1:
1-;4-;6-;#
', CAST(N'2024-11-28' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (88, 23, N'SlotOfTheDay=1;SettingDetail=32;OrderNumber=1:
#
SlotOfTheDay=1;SettingDetail=33;OrderNumber=2:
#
SlotOfTheDay=3;SettingDetail=34;OrderNumber=1:
#
', CAST(N'2024-11-29' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (89, 23, N'', CAST(N'2024-12-01' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (90, 23, N'', CAST(N'2024-12-02' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (91, 19, N'', CAST(N'2024-11-24' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (92, 19, N'', CAST(N'2024-11-25' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (93, 19, N'', CAST(N'2024-11-27' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (94, 19, N'#
', CAST(N'2024-11-28' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (95, 19, N'SlotOfTheDay=1;SettingDetail=3035;OrderNumber=1:
3-;6-;4-;#
', CAST(N'2024-11-29' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (96, 19, N'SlotOfTheDay=1;SettingDetail=3035;OrderNumber=1:
3-;6-;4-;#
', CAST(N'2024-11-30' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (97, 19, N'', CAST(N'2024-12-01' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (98, 19, N'SlotOfTheDay=1;SettingDetail=3035;OrderNumber=1:
#SlotOfTheDay=1;SettingDetail=4043;OrderNumber=2:
#SlotOfTheDay=3;SettingDetail=4044;OrderNumber=1:
#
', CAST(N'2024-12-02' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (99, 19, N'SlotOfTheDay=1;SettingDetail=3035;OrderNumber=1:
#
', CAST(N'2024-12-03' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (1091, 25, N'SlotOfTheDay=1;SettingDetail=35;OrderNumber=1:
#
', CAST(N'2024-11-21' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (1092, 26, N'SlotOfTheDay=1;SettingDetail=35;OrderNumber=1:
6-;4-;6-;#
', CAST(N'2024-11-21' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (1093, 25, N'SlotOfTheDay=1;SettingDetail=35;OrderNumber=1:
6-;6-;1-;#
', CAST(N'2024-11-22' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (1094, 25, N'SlotOfTheDay=1;SettingDetail=35;OrderNumber=1:
4-;6-;1-;#
', CAST(N'2024-11-23' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (1095, 28, N'SlotOfTheDay=1;SettingDetail=36;OrderNumber=1:
1-;6-;3-;#
', CAST(N'2024-11-21' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (1096, 28, N'SlotOfTheDay=1;SettingDetail=36;OrderNumber=1:
4-;4-;4-;#
', CAST(N'2024-11-22' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (1097, 28, N'SlotOfTheDay=1;SettingDetail=36;OrderNumber=1:
4-;3-;6-;#
', CAST(N'2024-11-23' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (1098, 28, N'SlotOfTheDay=1;SettingDetail=36;OrderNumber=1:
3-;4-;3-;#
', CAST(N'2024-11-24' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (1099, 28, N'SlotOfTheDay=1;SettingDetail=36;OrderNumber=1:
3-;1-;6-;#
', CAST(N'2024-11-29' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (1100, 29, N'SlotOfTheDay=1;SettingDetail=36;OrderNumber=1:
3-;4-;4-;#
', CAST(N'2024-11-21' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (1101, 30, N'', CAST(N'2024-11-22' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (1102, 30, N'', CAST(N'2024-11-23' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (1103, 31, N'SlotOfTheDay=1;SettingDetail=37;OrderNumber=1:
6-;4-;4-;#
', CAST(N'2024-11-23' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (1125, 32, N'SlotOfTheDay=1;SettingDetail=38;OrderNumber=1:
4-;6-;4-;#
', CAST(N'2024-11-23' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (1126, 32, N'', CAST(N'2024-11-24' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (1127, 32, N'', CAST(N'2024-11-25' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (1128, 32, N'', CAST(N'2024-11-26' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (1129, 32, N'', CAST(N'2024-11-27' AS Date), NULL, 1)
GO
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (1130, 32, N'', CAST(N'2024-11-28' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (1131, 32, N'', CAST(N'2024-11-29' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (1132, 32, N'SlotOfTheDay=1;SettingDetail=38;OrderNumber=1:
6-;6-;1-;#
', CAST(N'2024-11-30' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (1133, 32, N'', CAST(N'2024-12-01' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (1134, 32, N'', CAST(N'2024-12-23' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (1135, 32, N'', CAST(N'2024-12-16' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (1136, 32, N'', CAST(N'2024-12-17' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (1137, 32, N'', CAST(N'2024-12-18' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (1138, 32, N'', CAST(N'2024-12-19' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (1139, 32, N'', CAST(N'2024-12-20' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (1140, 32, N'SlotOfTheDay=1;SettingDetail=38;OrderNumber=1:
3-;4-;4-;#
', CAST(N'2024-12-21' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (1141, 32, N'', CAST(N'2024-12-22' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (1142, 33, N'SlotOfTheDay=1;SettingDetail=38;OrderNumber=1:
4-;6-;4-;#
', CAST(N'2024-11-23' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (1143, 32, N'', CAST(N'2024-12-02' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (1144, 32, N'', CAST(N'2024-12-03' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (1145, 32, N'', CAST(N'2024-12-04' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (1146, 32, N'', CAST(N'2024-12-05' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (1147, 32, N'', CAST(N'2024-12-06' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (1148, 32, N'SlotOfTheDay=1;SettingDetail=38;OrderNumber=1:
3-;4-;6-;#
', CAST(N'2024-12-07' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (1149, 32, N'', CAST(N'2024-12-08' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (2091, 1025, N'', CAST(N'2024-11-24' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (2092, 1025, N'', CAST(N'2024-11-25' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (2093, 1025, N'', CAST(N'2024-11-26' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (2094, 1025, N'', CAST(N'2024-11-27' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (2095, 1025, N'', CAST(N'2024-11-28' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (2096, 1025, N'', CAST(N'2024-11-29' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (2097, 1025, N'', CAST(N'2024-11-30' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (2098, 5, N'SlotOfTheDay=1;SettingDetail=1;OrderNumber=2:
4-;4-;3-;#SlotOfTheDay=1;SettingDetail=4;OrderNumber=1:
1-;4-;6-;#SlotOfTheDay=3;SettingDetail=5;OrderNumber=3:
6-;1-;4-;#SlotOfTheDay=4;SettingDetail=6;OrderNumber=4:
4-;6-;1-;#
', CAST(N'2024-11-24' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (2099, 19, N'', CAST(N'2024-12-23' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (2100, 19, N'', CAST(N'2024-12-16' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (2101, 19, N'SlotOfTheDay=1;SettingDetail=22;OrderNumber=1:
6-;6-;1-;#
', CAST(N'2024-12-17' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (2102, 19, N'SlotOfTheDay=1;SettingDetail=25;OrderNumber=1:
3-;3-;3-;#
', CAST(N'2024-12-18' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (2103, 19, N'SlotOfTheDay=1;SettingDetail=23;OrderNumber=1:
#SlotOfTheDay=1;SettingDetail=24;OrderNumber=2:
3-;6-;1-;#SlotOfTheDay=1;SettingDetail=27;OrderNumber=3:
#
', CAST(N'2024-12-19' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (2104, 19, N'SlotOfTheDay=1;SettingDetail=26;OrderNumber=1:
#
', CAST(N'2024-12-20' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (2105, 19, N'SlotOfTheDay=1;SettingDetail=28;OrderNumber=1:
1-;6-;6-;#
', CAST(N'2024-12-21' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (2106, 19, N'', CAST(N'2024-12-22' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (3091, 16, N'SlotOfTheDay=1;SettingDetail=18;OrderNumber=1:
3-;4-;4-;#SlotOfTheDay=1;SettingDetail=19;OrderNumber=3:
#SlotOfTheDay=1;SettingDetail=20;OrderNumber=2:
#SlotOfTheDay=1;SettingDetail=21;OrderNumber=4:
#
', CAST(N'2024-11-29' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (3092, 16, N'SlotOfTheDay=1;SettingDetail=18;OrderNumber=1:
4-;4-;3-;#SlotOfTheDay=1;SettingDetail=19;OrderNumber=3:
#SlotOfTheDay=1;SettingDetail=20;OrderNumber=2:
#SlotOfTheDay=1;SettingDetail=21;OrderNumber=4:
#
', CAST(N'2024-11-26' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (3093, 16, N'SlotOfTheDay=1;SettingDetail=18;OrderNumber=1:
3-;6-;4-;#SlotOfTheDay=1;SettingDetail=19;OrderNumber=3:
#SlotOfTheDay=1;SettingDetail=20;OrderNumber=2:
#SlotOfTheDay=1;SettingDetail=21;OrderNumber=4:
#
', CAST(N'2024-11-27' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (3094, 16, N'SlotOfTheDay=1;SettingDetail=18;OrderNumber=1:
3-;3-;3-;#SlotOfTheDay=1;SettingDetail=19;OrderNumber=3:
#SlotOfTheDay=1;SettingDetail=20;OrderNumber=2:
#SlotOfTheDay=1;SettingDetail=21;OrderNumber=4:
#
', CAST(N'2024-11-28' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (3095, 16, N'SlotOfTheDay=1;SettingDetail=18;OrderNumber=1:
1-;6-;4-;#SlotOfTheDay=1;SettingDetail=19;OrderNumber=3:
#SlotOfTheDay=1;SettingDetail=20;OrderNumber=2:
#SlotOfTheDay=1;SettingDetail=21;OrderNumber=4:
#
', CAST(N'2024-11-30' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (4091, 3025, N'SlotOfTheDay=1;SettingDetail=2035;OrderNumber=1:
4+;6-;1-;#
', CAST(N'2024-11-27' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (4092, 3026, N'SlotOfTheDay=1;SettingDetail=2035;OrderNumber=1:
1-;4-;6-;#
', CAST(N'2024-11-27' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (4093, 3027, N'', CAST(N'2024-11-27' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (4094, 3028, N'', CAST(N'2024-11-27' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (4095, 3029, N'', CAST(N'2024-11-27' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (4096, 3029, N'SlotOfTheDay=1;SettingDetail=2036;OrderNumber=1:
4-;3-;4-;#
', CAST(N'2024-11-28' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (4097, 3029, N'', CAST(N'2024-11-29' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (4098, 3029, N'', CAST(N'2024-11-30' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (4099, 3029, N'', CAST(N'2024-12-01' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (4100, 3031, N'', CAST(N'2024-11-27' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (4101, 3030, N'SlotOfTheDay=1;SettingDetail=2036;OrderNumber=1:
3-;6-;3-;#
', CAST(N'2024-11-28' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (4102, 3030, N'', CAST(N'2024-11-29' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (4103, 3030, N'', CAST(N'2024-11-30' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (4104, 3030, N'', CAST(N'2024-12-01' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (4105, 3032, N'', CAST(N'2024-11-27' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (4106, 3033, N'', CAST(N'2024-11-27' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (4107, 3034, N'', CAST(N'2024-11-27' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (4108, 3035, N'', CAST(N'2024-11-27' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (4109, 3037, N'', CAST(N'2024-11-27' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (4110, 3038, N'SlotOfTheDay=1;SettingDetail=2039;OrderNumber=1:
3-;6-;3-;#
', CAST(N'2024-11-27' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (4111, 3039, N'SlotOfTheDay=1;SettingDetail=2039;OrderNumber=1:
6-;1-;6-;#
', CAST(N'2024-11-27' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (4113, 3039, N'SlotOfTheDay=1;SettingDetail=2039;OrderNumber=1:
3-;6-;3-;#
', CAST(N'2024-11-28' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (4114, 3039, N'SlotOfTheDay=1;SettingDetail=2039;OrderNumber=1:
6-;6-;3-;#
', CAST(N'2024-11-29' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (4115, 3039, N'SlotOfTheDay=1;SettingDetail=2039;OrderNumber=1:
3-;3-;6-;#
', CAST(N'2024-11-30' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (4116, 3041, N'SlotOfTheDay=1;SettingDetail=2041;OrderNumber=1:
4-;3-;3-;#
', CAST(N'2024-11-27' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (4117, 3041, N'SlotOfTheDay=1;SettingDetail=2042;OrderNumber=1:
3-;4-;4-;#
', CAST(N'2024-11-28' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (4118, 3041, N'', CAST(N'2024-11-29' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (4119, 3041, N'', CAST(N'2024-11-30' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (4120, 3041, N'', CAST(N'2024-12-01' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (4121, 3041, N'SlotOfTheDay=1;SettingDetail=2040;OrderNumber=1:
4-;4-;3-;#
', CAST(N'2024-12-02' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (4122, 3041, N'SlotOfTheDay=1;SettingDetail=2040;OrderNumber=1:
3-;4-;3-;#
', CAST(N'2024-12-03' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (4123, 3041, N'SlotOfTheDay=1;SettingDetail=2040;OrderNumber=1:
3-;4-;4-;#
', CAST(N'2024-12-04' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (4124, 3041, N'SlotOfTheDay=1;SettingDetail=2040;OrderNumber=1:
4-;4-;3-;#
', CAST(N'2024-12-05' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (4125, 3041, N'SlotOfTheDay=1;SettingDetail=2040;OrderNumber=1:
3-;3-;4-;#
', CAST(N'2024-12-06' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (4126, 3041, N'SlotOfTheDay=1;SettingDetail=2040;OrderNumber=1:
4-;4-;4-;#
', CAST(N'2024-12-07' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (4127, 3042, N'SlotOfTheDay=1;SettingDetail=2041;OrderNumber=1:
4+;4-;3-;#
', CAST(N'2024-11-27' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (4128, 3043, N'', CAST(N'2024-11-27' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (4129, 3042, N'SlotOfTheDay=1;SettingDetail=2042;OrderNumber=1:
4-;4-;3-;#SlotOfTheDay=1;SettingDetail=2043;OrderNumber=2:
4-;3-;4-;#
', CAST(N'2024-11-28' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (4130, 3042, N'', CAST(N'2024-11-29' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (4131, 3042, N'', CAST(N'2024-11-30' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (4132, 3042, N'', CAST(N'2024-12-01' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (5091, 2026, N'SlotOfTheDay=1;SettingDetail=18;OrderNumber=1:
4-;4-;3-;#SlotOfTheDay=1;SettingDetail=19;OrderNumber=3:
#SlotOfTheDay=1;SettingDetail=20;OrderNumber=2:
#SlotOfTheDay=1;SettingDetail=21;OrderNumber=4:
#
', CAST(N'2024-11-28' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (5092, 4025, N'SlotOfTheDay=1;SettingDetail=3036;OrderNumber=1:
#SlotOfTheDay=2;SettingDetail=3037;OrderNumber=1:
#SlotOfTheDay=4;SettingDetail=3038;OrderNumber=1:
#
', CAST(N'2024-11-28' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (5093, 4026, N'', CAST(N'2024-11-28' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (5094, 19, N'SlotOfTheDay=1;SettingDetail=3035;OrderNumber=1:
#
', CAST(N'2024-12-06' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (5095, 19, N'SlotOfTheDay=1;SettingDetail=3035;OrderNumber=1:
#
', CAST(N'2024-12-04' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (5096, 19, N'SlotOfTheDay=1;SettingDetail=3035;OrderNumber=1:
#
', CAST(N'2024-12-05' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (5097, 19, N'SlotOfTheDay=1;SettingDetail=3035;OrderNumber=1:
#
', CAST(N'2024-12-07' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (5098, 19, N'', CAST(N'2024-12-08' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (6091, 5025, N'SlotOfTheDay=1;SettingDetail=4035;OrderNumber=1:
#SlotOfTheDay=2;SettingDetail=4036;OrderNumber=1:
#SlotOfTheDay=4;SettingDetail=4037;OrderNumber=1:
#
', CAST(N'2024-11-30' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (6092, 5026, N'SlotOfTheDay=1;SettingDetail=4035;OrderNumber=1:
#SlotOfTheDay=2;SettingDetail=4036;OrderNumber=1:
#SlotOfTheDay=4;SettingDetail=4037;OrderNumber=1:
#
', CAST(N'2024-11-30' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (6093, 5027, N'SlotOfTheDay=1;SettingDetail=4039;OrderNumber=1:
4-;4-;3-;#
', CAST(N'2024-11-30' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (6094, 5028, N'', CAST(N'2024-12-01' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (6095, 5029, N'SlotOfTheDay=1;SettingDetail=4040;OrderNumber=1:
#SlotOfTheDay=1;SettingDetail=4041;OrderNumber=2:
#SlotOfTheDay=4;SettingDetail=4042;OrderNumber=1:
#
', CAST(N'2024-12-02' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (6096, 5030, N'SlotOfTheDay=1;SettingDetail=4040;OrderNumber=1:
#SlotOfTheDay=1;SettingDetail=4041;OrderNumber=2:
#SlotOfTheDay=4;SettingDetail=4042;OrderNumber=1:
#
', CAST(N'2024-12-02' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (6097, 5031, N'SlotOfTheDay=1;SettingDetail=4045;OrderNumber=1:
1-;6-;6-;#SlotOfTheDay=2;SettingDetail=4046;OrderNumber=1:
#SlotOfTheDay=4;SettingDetail=4047;OrderNumber=1:
#
', CAST(N'2024-12-03' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (7091, 31, N'', CAST(N'2024-12-05' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (7092, 31, N'', CAST(N'2024-12-06' AS Date), NULL, 1)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (7093, 31, N'SlotOfTheDay=1;SettingDetail=37;OrderNumber=1:
6-;1-;4-;#
', CAST(N'2024-12-07' AS Date), NULL, 1)
GO
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [DataFoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (7094, 31, N'', CAST(N'2024-12-08' AS Date), NULL, 1)
SET IDENTITY_INSERT [Business].[MealOfTheDay] OFF
GO
SET IDENTITY_INSERT [Business].[Messages] ON 

INSERT [Business].[Messages] ([Id], [Content], [Timestamp], [FromUserId], [ToRoomId]) VALUES (1, N'hello slyy', CAST(N'2024-11-24T21:20:44.127' AS DateTime), 2, 3)
INSERT [Business].[Messages] ([Id], [Content], [Timestamp], [FromUserId], [ToRoomId]) VALUES (2, N'hom nay troi depj', CAST(N'2024-11-24T21:22:02.723' AS DateTime), 45, 3)
INSERT [Business].[Messages] ([Id], [Content], [Timestamp], [FromUserId], [ToRoomId]) VALUES (3, N'depj gi ma dep', CAST(N'2024-11-24T21:22:15.643' AS DateTime), 2, 3)
INSERT [Business].[Messages] ([Id], [Content], [Timestamp], [FromUserId], [ToRoomId]) VALUES (1002, N'Home', CAST(N'2024-11-25T19:22:38.560' AS DateTime), 2, 3)
INSERT [Business].[Messages] ([Id], [Content], [Timestamp], [FromUserId], [ToRoomId]) VALUES (1003, N'gdfh', CAST(N'2024-11-25T19:22:41.320' AS DateTime), 45, 3)
INSERT [Business].[Messages] ([Id], [Content], [Timestamp], [FromUserId], [ToRoomId]) VALUES (1004, N'ghjghj', CAST(N'2024-11-25T19:22:43.407' AS DateTime), 2, 3)
INSERT [Business].[Messages] ([Id], [Content], [Timestamp], [FromUserId], [ToRoomId]) VALUES (1005, N'hgjhgk', CAST(N'2024-11-25T19:22:45.280' AS DateTime), 45, 3)
INSERT [Business].[Messages] ([Id], [Content], [Timestamp], [FromUserId], [ToRoomId]) VALUES (1006, N'hgjhjk', CAST(N'2024-11-25T19:22:48.990' AS DateTime), 45, 3)
INSERT [Business].[Messages] ([Id], [Content], [Timestamp], [FromUserId], [ToRoomId]) VALUES (1007, N'jkljkl', CAST(N'2024-11-25T19:22:51.270' AS DateTime), 2, 3)
INSERT [Business].[Messages] ([Id], [Content], [Timestamp], [FromUserId], [ToRoomId]) VALUES (1008, N'Hello benh nhan', CAST(N'2024-11-25T21:17:09.630' AS DateTime), 2, 2)
INSERT [Business].[Messages] ([Id], [Content], [Timestamp], [FromUserId], [ToRoomId]) VALUES (1009, N'chao bac si', CAST(N'2024-11-25T21:17:22.420' AS DateTime), 45, 2)
INSERT [Business].[Messages] ([Id], [Content], [Timestamp], [FromUserId], [ToRoomId]) VALUES (1010, N'Tôi bị bệnh thận thì càn những món j', CAST(N'2024-11-25T21:18:30.997' AS DateTime), 45, 2)
INSERT [Business].[Messages] ([Id], [Content], [Timestamp], [FromUserId], [ToRoomId]) VALUES (1011, N'tôi sẽ cài đặt bữa ăn cho bạn để bạn tránh bị nặng bệnh', CAST(N'2024-11-25T21:18:52.260' AS DateTime), 2, 2)
INSERT [Business].[Messages] ([Id], [Content], [Timestamp], [FromUserId], [ToRoomId]) VALUES (1012, N'lo', CAST(N'2024-11-26T07:54:06.510' AS DateTime), 45, 2)
INSERT [Business].[Messages] ([Id], [Content], [Timestamp], [FromUserId], [ToRoomId]) VALUES (1013, N'OKe', CAST(N'2024-11-26T07:57:50.760' AS DateTime), 2, 2)
INSERT [Business].[Messages] ([Id], [Content], [Timestamp], [FromUserId], [ToRoomId]) VALUES (1014, N'Abc', CAST(N'2024-11-26T07:57:54.317' AS DateTime), 45, 2)
INSERT [Business].[Messages] ([Id], [Content], [Timestamp], [FromUserId], [ToRoomId]) VALUES (2002, N'tôi bị bệnh hen suyễn mà trong danh sách tránh kia lại ko có', CAST(N'2024-11-27T21:08:19.240' AS DateTime), 3084, 1002)
INSERT [Business].[Messages] ([Id], [Content], [Timestamp], [FromUserId], [ToRoomId]) VALUES (2003, N'tôi rõ rồi, tôi sẽ tự động tạo món phòng tránh cho bạn', CAST(N'2024-11-27T21:08:53.597' AS DateTime), 2, 1002)
INSERT [Business].[Messages] ([Id], [Content], [Timestamp], [FromUserId], [ToRoomId]) VALUES (2004, N'cám ơn', CAST(N'2024-11-27T21:08:58.447' AS DateTime), 3084, 1002)
INSERT [Business].[Messages] ([Id], [Content], [Timestamp], [FromUserId], [ToRoomId]) VALUES (3002, N'lô chao ngay moi', CAST(N'2024-11-28T18:31:49.087' AS DateTime), 2, 3)
INSERT [Business].[Messages] ([Id], [Content], [Timestamp], [FromUserId], [ToRoomId]) VALUES (3003, N'gjkhfghj', CAST(N'2024-11-28T18:32:34.867' AS DateTime), 45, 2)
INSERT [Business].[Messages] ([Id], [Content], [Timestamp], [FromUserId], [ToRoomId]) VALUES (3004, N'jljkl', CAST(N'2024-11-28T18:32:36.933' AS DateTime), 2, 2)
SET IDENTITY_INSERT [Business].[Messages] OFF
GO
SET IDENTITY_INSERT [Business].[NewsEvaluation] ON 

INSERT [Business].[NewsEvaluation] ([Id], [ArticlesNewsId], [UserID], [Ratting]) VALUES (1, 1, 32, 2)
INSERT [Business].[NewsEvaluation] ([Id], [ArticlesNewsId], [UserID], [Ratting]) VALUES (2, 2, 32, 3)
INSERT [Business].[NewsEvaluation] ([Id], [ArticlesNewsId], [UserID], [Ratting]) VALUES (3, 2, 45, 3)
INSERT [Business].[NewsEvaluation] ([Id], [ArticlesNewsId], [UserID], [Ratting]) VALUES (4, 2, 47, 4)
SET IDENTITY_INSERT [Business].[NewsEvaluation] OFF
GO
SET IDENTITY_INSERT [Business].[NutritionRoute] ON 

INSERT [Business].[NutritionRoute] ([Id], [UserId], [CreateById], [Name], [Describe], [StartDate], [EndDate], [IsDone]) VALUES (1, 1, 2, N'Weight Loss Plan', N'A customized plan for weight loss', CAST(N'2024-11-01T00:00:00.000' AS DateTime), CAST(N'9999-01-31T00:00:00.000' AS DateTime), 1)
INSERT [Business].[NutritionRoute] ([Id], [UserId], [CreateById], [Name], [Describe], [StartDate], [EndDate], [IsDone]) VALUES (2, 2, 3, N'Muscle Gain Plan', N'A customized plan for muscle gain', NULL, NULL, NULL)
INSERT [Business].[NutritionRoute] ([Id], [UserId], [CreateById], [Name], [Describe], [StartDate], [EndDate], [IsDone]) VALUES (3, 3, 4, N'Balanced Diet Plan', N'A customized plan for balanced nutrition', NULL, NULL, NULL)
INSERT [Business].[NutritionRoute] ([Id], [UserId], [CreateById], [Name], [Describe], [StartDate], [EndDate], [IsDone]) VALUES (4, 4, 1, N'Health Improvement Plan', N'A plan focused on overall health', NULL, NULL, NULL)
INSERT [Business].[NutritionRoute] ([Id], [UserId], [CreateById], [Name], [Describe], [StartDate], [EndDate], [IsDone]) VALUES (5, 1, 1, NULL, NULL, CAST(N'2024-10-27T11:41:28.377' AS DateTime), CAST(N'9999-11-11T00:00:00.000' AS DateTime), 0)
INSERT [Business].[NutritionRoute] ([Id], [UserId], [CreateById], [Name], [Describe], [StartDate], [EndDate], [IsDone]) VALUES (6, 1, 1, NULL, NULL, CAST(N'2024-10-28T21:21:42.237' AS DateTime), CAST(N'9999-11-11T00:00:00.000' AS DateTime), 0)
INSERT [Business].[NutritionRoute] ([Id], [UserId], [CreateById], [Name], [Describe], [StartDate], [EndDate], [IsDone]) VALUES (7, 1, 1, NULL, NULL, CAST(N'2024-10-28T21:22:02.830' AS DateTime), CAST(N'9999-11-11T00:00:00.000' AS DateTime), 0)
INSERT [Business].[NutritionRoute] ([Id], [UserId], [CreateById], [Name], [Describe], [StartDate], [EndDate], [IsDone]) VALUES (8, 1, 1, NULL, NULL, CAST(N'2024-10-30T10:25:51.240' AS DateTime), CAST(N'9999-11-11T00:00:00.000' AS DateTime), 0)
INSERT [Business].[NutritionRoute] ([Id], [UserId], [CreateById], [Name], [Describe], [StartDate], [EndDate], [IsDone]) VALUES (9, 1, 1, NULL, NULL, CAST(N'2024-10-30T11:19:17.203' AS DateTime), CAST(N'9999-11-11T00:00:00.000' AS DateTime), 0)
INSERT [Business].[NutritionRoute] ([Id], [UserId], [CreateById], [Name], [Describe], [StartDate], [EndDate], [IsDone]) VALUES (10, 1, 1, NULL, NULL, CAST(N'2024-10-30T11:21:01.787' AS DateTime), CAST(N'9999-11-11T00:00:00.000' AS DateTime), 0)
INSERT [Business].[NutritionRoute] ([Id], [UserId], [CreateById], [Name], [Describe], [StartDate], [EndDate], [IsDone]) VALUES (11, 1, 1, NULL, NULL, CAST(N'2024-10-30T11:40:47.827' AS DateTime), CAST(N'9999-11-11T00:00:00.000' AS DateTime), 0)
INSERT [Business].[NutritionRoute] ([Id], [UserId], [CreateById], [Name], [Describe], [StartDate], [EndDate], [IsDone]) VALUES (12, 32, 1, NULL, NULL, CAST(N'2024-10-30T11:40:47.827' AS DateTime), CAST(N'9999-11-11T00:00:00.000' AS DateTime), 0)
INSERT [Business].[NutritionRoute] ([Id], [UserId], [CreateById], [Name], [Describe], [StartDate], [EndDate], [IsDone]) VALUES (13, 40, 1, NULL, NULL, CAST(N'2024-11-08T14:21:10.553' AS DateTime), CAST(N'9999-11-11T00:00:00.000' AS DateTime), 0)
INSERT [Business].[NutritionRoute] ([Id], [UserId], [CreateById], [Name], [Describe], [StartDate], [EndDate], [IsDone]) VALUES (14, 42, 1, NULL, NULL, CAST(N'2024-11-09T17:42:09.900' AS DateTime), CAST(N'9999-11-11T00:00:00.000' AS DateTime), 0)
INSERT [Business].[NutritionRoute] ([Id], [UserId], [CreateById], [Name], [Describe], [StartDate], [EndDate], [IsDone]) VALUES (15, 43, 1, NULL, NULL, CAST(N'2024-11-09T17:49:00.417' AS DateTime), CAST(N'9999-11-11T00:00:00.000' AS DateTime), 0)
INSERT [Business].[NutritionRoute] ([Id], [UserId], [CreateById], [Name], [Describe], [StartDate], [EndDate], [IsDone]) VALUES (16, 44, 1, NULL, NULL, CAST(N'2024-11-09T20:35:12.773' AS DateTime), CAST(N'9999-11-11T00:00:00.000' AS DateTime), 1)
INSERT [Business].[NutritionRoute] ([Id], [UserId], [CreateById], [Name], [Describe], [StartDate], [EndDate], [IsDone]) VALUES (19, 45, 1, NULL, NULL, CAST(N'2024-11-10T11:11:11.000' AS DateTime), CAST(N'9999-11-11T00:00:00.000' AS DateTime), 0)
INSERT [Business].[NutritionRoute] ([Id], [UserId], [CreateById], [Name], [Describe], [StartDate], [EndDate], [IsDone]) VALUES (20, 45, 1, NULL, NULL, CAST(N'2024-11-10T00:00:00.000' AS DateTime), CAST(N'9999-11-11T00:00:00.000' AS DateTime), 0)
INSERT [Business].[NutritionRoute] ([Id], [UserId], [CreateById], [Name], [Describe], [StartDate], [EndDate], [IsDone]) VALUES (21, 46, 1, NULL, NULL, CAST(N'2024-11-12T20:23:58.000' AS DateTime), CAST(N'9999-11-11T00:00:00.000' AS DateTime), 0)
INSERT [Business].[NutritionRoute] ([Id], [UserId], [CreateById], [Name], [Describe], [StartDate], [EndDate], [IsDone]) VALUES (22, 46, 1, NULL, NULL, CAST(N'2024-11-12T00:00:00.000' AS DateTime), CAST(N'9999-11-11T00:00:00.000' AS DateTime), 0)
INSERT [Business].[NutritionRoute] ([Id], [UserId], [CreateById], [Name], [Describe], [StartDate], [EndDate], [IsDone]) VALUES (23, 47, 1, NULL, NULL, CAST(N'2024-11-12T21:00:13.000' AS DateTime), CAST(N'9999-11-11T00:00:00.000' AS DateTime), 0)
INSERT [Business].[NutritionRoute] ([Id], [UserId], [CreateById], [Name], [Describe], [StartDate], [EndDate], [IsDone]) VALUES (24, 47, 1, NULL, NULL, CAST(N'2024-11-12T00:00:00.000' AS DateTime), CAST(N'9999-11-11T00:00:00.000' AS DateTime), 0)
INSERT [Business].[NutritionRoute] ([Id], [UserId], [CreateById], [Name], [Describe], [StartDate], [EndDate], [IsDone]) VALUES (25, 48, 1, NULL, NULL, CAST(N'2024-11-21T10:25:30.000' AS DateTime), CAST(N'9999-11-11T00:00:00.000' AS DateTime), 0)
INSERT [Business].[NutritionRoute] ([Id], [UserId], [CreateById], [Name], [Describe], [StartDate], [EndDate], [IsDone]) VALUES (26, 48, 1, NULL, NULL, CAST(N'2024-11-21T00:00:00.000' AS DateTime), CAST(N'9999-11-11T00:00:00.000' AS DateTime), 0)
INSERT [Business].[NutritionRoute] ([Id], [UserId], [CreateById], [Name], [Describe], [StartDate], [EndDate], [IsDone]) VALUES (27, 1, 2, N'Món nhẹ buổi sáng thứ 2', N'fdgdfg', CAST(N'2024-12-01T00:00:00.000' AS DateTime), CAST(N'2024-12-30T00:00:00.000' AS DateTime), 1)
INSERT [Business].[NutritionRoute] ([Id], [UserId], [CreateById], [Name], [Describe], [StartDate], [EndDate], [IsDone]) VALUES (28, 49, 1, NULL, NULL, CAST(N'2024-11-21T21:47:46.000' AS DateTime), CAST(N'9999-11-11T00:00:00.000' AS DateTime), 0)
INSERT [Business].[NutritionRoute] ([Id], [UserId], [CreateById], [Name], [Describe], [StartDate], [EndDate], [IsDone]) VALUES (29, 49, 1, NULL, NULL, CAST(N'2024-11-21T00:00:00.000' AS DateTime), CAST(N'9999-11-11T00:00:00.000' AS DateTime), 0)
INSERT [Business].[NutritionRoute] ([Id], [UserId], [CreateById], [Name], [Describe], [StartDate], [EndDate], [IsDone]) VALUES (30, 55, 1, NULL, NULL, CAST(N'2024-11-22T16:44:26.000' AS DateTime), CAST(N'9999-11-11T00:00:00.000' AS DateTime), 0)
INSERT [Business].[NutritionRoute] ([Id], [UserId], [CreateById], [Name], [Describe], [StartDate], [EndDate], [IsDone]) VALUES (31, 56, 1, NULL, NULL, CAST(N'2024-11-23T09:39:55.000' AS DateTime), CAST(N'9999-11-11T00:00:00.000' AS DateTime), 0)
INSERT [Business].[NutritionRoute] ([Id], [UserId], [CreateById], [Name], [Describe], [StartDate], [EndDate], [IsDone]) VALUES (32, 57, 1, NULL, NULL, CAST(N'2024-11-23T17:00:32.000' AS DateTime), CAST(N'9999-11-11T00:00:00.000' AS DateTime), 0)
INSERT [Business].[NutritionRoute] ([Id], [UserId], [CreateById], [Name], [Describe], [StartDate], [EndDate], [IsDone]) VALUES (33, 57, 1, NULL, NULL, CAST(N'2024-11-23T00:00:00.000' AS DateTime), CAST(N'9999-11-11T00:00:00.000' AS DateTime), 0)
INSERT [Business].[NutritionRoute] ([Id], [UserId], [CreateById], [Name], [Describe], [StartDate], [EndDate], [IsDone]) VALUES (1025, 1056, 1, NULL, NULL, CAST(N'2024-11-24T13:40:48.000' AS DateTime), CAST(N'9999-11-11T00:00:00.000' AS DateTime), 0)
INSERT [Business].[NutritionRoute] ([Id], [UserId], [CreateById], [Name], [Describe], [StartDate], [EndDate], [IsDone]) VALUES (2025, 44, 2, N'Món nhẹ buổi sáng', N'testLoTrinh', CAST(N'2024-11-20T00:00:00.000' AS DateTime), CAST(N'2024-11-30T00:00:00.000' AS DateTime), 1)
INSERT [Business].[NutritionRoute] ([Id], [UserId], [CreateById], [Name], [Describe], [StartDate], [EndDate], [IsDone]) VALUES (2026, 44, 2, N'Món nhẹ buổi sáng thứ 2', N'abc', CAST(N'2024-11-26T00:00:00.000' AS DateTime), CAST(N'2024-12-07T00:00:00.000' AS DateTime), 0)
INSERT [Business].[NutritionRoute] ([Id], [UserId], [CreateById], [Name], [Describe], [StartDate], [EndDate], [IsDone]) VALUES (3025, 3056, 1, NULL, NULL, CAST(N'2024-11-27T07:53:26.000' AS DateTime), CAST(N'9999-11-11T00:00:00.000' AS DateTime), 0)
INSERT [Business].[NutritionRoute] ([Id], [UserId], [CreateById], [Name], [Describe], [StartDate], [EndDate], [IsDone]) VALUES (3026, 3056, 1, NULL, NULL, CAST(N'2024-11-27T00:00:00.000' AS DateTime), CAST(N'9999-11-11T00:00:00.000' AS DateTime), 0)
INSERT [Business].[NutritionRoute] ([Id], [UserId], [CreateById], [Name], [Describe], [StartDate], [EndDate], [IsDone]) VALUES (3027, 3068, 1, NULL, NULL, CAST(N'2024-11-27T13:01:13.000' AS DateTime), CAST(N'9999-11-11T00:00:00.000' AS DateTime), 0)
INSERT [Business].[NutritionRoute] ([Id], [UserId], [CreateById], [Name], [Describe], [StartDate], [EndDate], [IsDone]) VALUES (3028, 3077, 1, NULL, NULL, CAST(N'2024-11-27T14:04:04.000' AS DateTime), CAST(N'9999-11-11T00:00:00.000' AS DateTime), 0)
INSERT [Business].[NutritionRoute] ([Id], [UserId], [CreateById], [Name], [Describe], [StartDate], [EndDate], [IsDone]) VALUES (3029, 3078, 1, NULL, NULL, CAST(N'2024-11-27T14:27:20.000' AS DateTime), CAST(N'9999-11-11T00:00:00.000' AS DateTime), 1)
INSERT [Business].[NutritionRoute] ([Id], [UserId], [CreateById], [Name], [Describe], [StartDate], [EndDate], [IsDone]) VALUES (3030, 3078, 2, N'test2lotrinh', N'test2lotrinh', CAST(N'2024-11-28T00:00:00.000' AS DateTime), CAST(N'2024-12-08T00:00:00.000' AS DateTime), 0)
INSERT [Business].[NutritionRoute] ([Id], [UserId], [CreateById], [Name], [Describe], [StartDate], [EndDate], [IsDone]) VALUES (3031, 3078, 1, NULL, NULL, CAST(N'2024-11-27T14:33:05.000' AS DateTime), CAST(N'9999-11-11T00:00:00.000' AS DateTime), 0)
INSERT [Business].[NutritionRoute] ([Id], [UserId], [CreateById], [Name], [Describe], [StartDate], [EndDate], [IsDone]) VALUES (3032, 3079, 1, NULL, NULL, CAST(N'2024-11-27T16:18:46.000' AS DateTime), CAST(N'9999-11-11T00:00:00.000' AS DateTime), 0)
INSERT [Business].[NutritionRoute] ([Id], [UserId], [CreateById], [Name], [Describe], [StartDate], [EndDate], [IsDone]) VALUES (3033, 3080, 1, NULL, NULL, CAST(N'2024-11-27T16:19:35.000' AS DateTime), CAST(N'9999-11-11T00:00:00.000' AS DateTime), 0)
INSERT [Business].[NutritionRoute] ([Id], [UserId], [CreateById], [Name], [Describe], [StartDate], [EndDate], [IsDone]) VALUES (3034, 3081, 1, NULL, NULL, CAST(N'2024-11-27T17:00:54.000' AS DateTime), CAST(N'9999-11-11T00:00:00.000' AS DateTime), 0)
INSERT [Business].[NutritionRoute] ([Id], [UserId], [CreateById], [Name], [Describe], [StartDate], [EndDate], [IsDone]) VALUES (3035, 3082, 1, NULL, NULL, CAST(N'2024-11-27T18:39:07.000' AS DateTime), CAST(N'9999-11-11T00:00:00.000' AS DateTime), 1)
INSERT [Business].[NutritionRoute] ([Id], [UserId], [CreateById], [Name], [Describe], [StartDate], [EndDate], [IsDone]) VALUES (3036, 3082, 2, N'lotrinhtestthutmt', N'lotrinhtestthutmt', CAST(N'2024-11-27T18:49:20.000' AS DateTime), CAST(N'2024-12-08T00:00:00.000' AS DateTime), 1)
INSERT [Business].[NutritionRoute] ([Id], [UserId], [CreateById], [Name], [Describe], [StartDate], [EndDate], [IsDone]) VALUES (3037, 3082, 1, NULL, NULL, CAST(N'2024-11-27T18:49:20.000' AS DateTime), CAST(N'9999-11-11T00:00:00.000' AS DateTime), 0)
INSERT [Business].[NutritionRoute] ([Id], [UserId], [CreateById], [Name], [Describe], [StartDate], [EndDate], [IsDone]) VALUES (3038, 3083, 1, NULL, NULL, CAST(N'2024-11-27T20:17:55.830' AS DateTime), CAST(N'9999-11-11T00:00:00.000' AS DateTime), 1)
INSERT [Business].[NutritionRoute] ([Id], [UserId], [CreateById], [Name], [Describe], [StartDate], [EndDate], [IsDone]) VALUES (3039, 3083, 2, N'Trịnh Minh Tân', N'Trịnh Minh Tân', CAST(N'2024-11-27T20:25:42.080' AS DateTime), CAST(N'2024-12-07T00:00:00.000' AS DateTime), 0)
INSERT [Business].[NutritionRoute] ([Id], [UserId], [CreateById], [Name], [Describe], [StartDate], [EndDate], [IsDone]) VALUES (3040, 3083, 1, NULL, NULL, CAST(N'2024-11-27T00:00:00.000' AS DateTime), CAST(N'9999-11-11T00:00:00.000' AS DateTime), 0)
INSERT [Business].[NutritionRoute] ([Id], [UserId], [CreateById], [Name], [Describe], [StartDate], [EndDate], [IsDone]) VALUES (3041, 3084, 1, NULL, NULL, CAST(N'2024-11-27T20:41:15.000' AS DateTime), CAST(N'9999-11-11T00:00:00.000' AS DateTime), 1)
INSERT [Business].[NutritionRoute] ([Id], [UserId], [CreateById], [Name], [Describe], [StartDate], [EndDate], [IsDone]) VALUES (3042, 3084, 1, NULL, NULL, CAST(N'2024-11-27T00:00:00.000' AS DateTime), CAST(N'9999-11-11T00:00:00.000' AS DateTime), 0)
INSERT [Business].[NutritionRoute] ([Id], [UserId], [CreateById], [Name], [Describe], [StartDate], [EndDate], [IsDone]) VALUES (3043, 2, 1, NULL, NULL, CAST(N'2024-11-27T20:53:03.000' AS DateTime), CAST(N'9999-11-11T00:00:00.000' AS DateTime), 0)
INSERT [Business].[NutritionRoute] ([Id], [UserId], [CreateById], [Name], [Describe], [StartDate], [EndDate], [IsDone]) VALUES (3044, 3084, 2, N'tandungdedangnhap', N'tandungdedangnhap', CAST(N'2024-11-27T20:55:37.027' AS DateTime), CAST(N'2024-12-12T00:00:00.000' AS DateTime), 0)
INSERT [Business].[NutritionRoute] ([Id], [UserId], [CreateById], [Name], [Describe], [StartDate], [EndDate], [IsDone]) VALUES (4025, 4056, 1, NULL, NULL, CAST(N'2024-11-28T17:13:36.000' AS DateTime), CAST(N'9999-11-11T00:00:00.000' AS DateTime), 0)
INSERT [Business].[NutritionRoute] ([Id], [UserId], [CreateById], [Name], [Describe], [StartDate], [EndDate], [IsDone]) VALUES (4026, 4057, 1, NULL, NULL, CAST(N'2024-11-28T18:43:15.000' AS DateTime), CAST(N'9999-11-11T00:00:00.000' AS DateTime), 0)
INSERT [Business].[NutritionRoute] ([Id], [UserId], [CreateById], [Name], [Describe], [StartDate], [EndDate], [IsDone]) VALUES (5025, 5056, 1, NULL, NULL, CAST(N'2024-11-30T16:51:37.000' AS DateTime), CAST(N'9999-11-11T00:00:00.000' AS DateTime), 0)
INSERT [Business].[NutritionRoute] ([Id], [UserId], [CreateById], [Name], [Describe], [StartDate], [EndDate], [IsDone]) VALUES (5026, 5056, 1, NULL, NULL, CAST(N'2024-11-30T00:00:00.000' AS DateTime), CAST(N'9999-11-11T00:00:00.000' AS DateTime), 0)
INSERT [Business].[NutritionRoute] ([Id], [UserId], [CreateById], [Name], [Describe], [StartDate], [EndDate], [IsDone]) VALUES (5027, 5057, 1, NULL, NULL, CAST(N'2024-11-30T17:11:03.000' AS DateTime), CAST(N'9999-11-11T00:00:00.000' AS DateTime), 0)
INSERT [Business].[NutritionRoute] ([Id], [UserId], [CreateById], [Name], [Describe], [StartDate], [EndDate], [IsDone]) VALUES (5028, 5058, 1, NULL, NULL, CAST(N'2024-12-01T21:07:27.000' AS DateTime), CAST(N'9999-11-11T00:00:00.000' AS DateTime), 0)
INSERT [Business].[NutritionRoute] ([Id], [UserId], [CreateById], [Name], [Describe], [StartDate], [EndDate], [IsDone]) VALUES (5029, 5059, 1, NULL, NULL, CAST(N'2024-12-02T13:48:25.000' AS DateTime), CAST(N'9999-11-11T00:00:00.000' AS DateTime), 0)
INSERT [Business].[NutritionRoute] ([Id], [UserId], [CreateById], [Name], [Describe], [StartDate], [EndDate], [IsDone]) VALUES (5030, 5059, 1, NULL, NULL, CAST(N'2024-12-02T00:00:00.000' AS DateTime), CAST(N'9999-11-11T00:00:00.000' AS DateTime), 0)
INSERT [Business].[NutritionRoute] ([Id], [UserId], [CreateById], [Name], [Describe], [StartDate], [EndDate], [IsDone]) VALUES (5031, 5060, 1, NULL, NULL, CAST(N'2024-12-03T06:39:25.000' AS DateTime), CAST(N'9999-11-11T00:00:00.000' AS DateTime), 0)
SET IDENTITY_INSERT [Business].[NutritionRoute] OFF
GO
SET IDENTITY_INSERT [Business].[Rooms] ON 

INSERT [Business].[Rooms] ([Id], [Name], [NutritionId], [UserId]) VALUES (2, N'ChatUser', 2, 45)
INSERT [Business].[Rooms] ([Id], [Name], [NutritionId], [UserId]) VALUES (3, N'ChatUser2', 2, 56)
INSERT [Business].[Rooms] ([Id], [Name], [NutritionId], [UserId]) VALUES (1002, NULL, 2, 46)
SET IDENTITY_INSERT [Business].[Rooms] OFF
GO
SET IDENTITY_INSERT [Business].[SlotOfTheDay] ON 

INSERT [Business].[SlotOfTheDay] ([Id], [Slot], [StartAt], [EndAt]) VALUES (1, N'Bữa sáng', CAST(N'08:00:00' AS Time), CAST(N'12:00:00' AS Time))
INSERT [Business].[SlotOfTheDay] ([Id], [Slot], [StartAt], [EndAt]) VALUES (2, N'Bữa trưa', CAST(N'12:00:00' AS Time), CAST(N'17:00:00' AS Time))
INSERT [Business].[SlotOfTheDay] ([Id], [Slot], [StartAt], [EndAt]) VALUES (3, N'Bữa chiều', CAST(N'17:00:00' AS Time), CAST(N'21:00:00' AS Time))
INSERT [Business].[SlotOfTheDay] ([Id], [Slot], [StartAt], [EndAt]) VALUES (4, N'Bữa tối', CAST(N'21:00:00' AS Time), CAST(N'00:00:00' AS Time))
INSERT [Business].[SlotOfTheDay] ([Id], [Slot], [StartAt], [EndAt]) VALUES (5, N'Cả ngày', CAST(N'00:00:00' AS Time), CAST(N'23:59:59' AS Time))
SET IDENTITY_INSERT [Business].[SlotOfTheDay] OFF
GO
SET IDENTITY_INSERT [Business].[TransactionsSystem] ON 

INSERT [Business].[TransactionsSystem] ([Id], [UserPayID], [PayeeID], [APITransactions], [BankBrandName], [AccountNumber], [TransactionDate], [AmountOut], [AmountIn], [Accumulated], [TransactionContent], [ReferenceNumber], [Code], [SubAccount], [BankAccountId], [Status]) VALUES (3, 3, 1, NULL, N'MBBank', N'0569000899', CAST(N'2024-10-16T17:01:19.000' AS DateTime), NULL, 4000.0000, NULL, N'thank toan test 1', NULL, NULL, NULL, NULL, 0)
INSERT [Business].[TransactionsSystem] ([Id], [UserPayID], [PayeeID], [APITransactions], [BankBrandName], [AccountNumber], [TransactionDate], [AmountOut], [AmountIn], [Accumulated], [TransactionContent], [ReferenceNumber], [Code], [SubAccount], [BankAccountId], [Status]) VALUES (4, 3, 1, 3569566, N'MBBank', N'0569000899', CAST(N'2024-10-16T17:01:19.000' AS DateTime), 0.0000, 2000.0000, 0.0000, N'Hello', N'FT24290600691633', N'', N'', 3342, 1)
INSERT [Business].[TransactionsSystem] ([Id], [UserPayID], [PayeeID], [APITransactions], [BankBrandName], [AccountNumber], [TransactionDate], [AmountOut], [AmountIn], [Accumulated], [TransactionContent], [ReferenceNumber], [Code], [SubAccount], [BankAccountId], [Status]) VALUES (5, 3, 1, 3679627, N'MBBank', N'0569000899', CAST(N'2024-10-19T00:39:58.000' AS DateTime), 0.0000, 5000.0000, 0.0000, N'thank toan test mon 9', N'FT24293547418805', N'', N'', 3342, 1)
INSERT [Business].[TransactionsSystem] ([Id], [UserPayID], [PayeeID], [APITransactions], [BankBrandName], [AccountNumber], [TransactionDate], [AmountOut], [AmountIn], [Accumulated], [TransactionContent], [ReferenceNumber], [Code], [SubAccount], [BankAccountId], [Status]) VALUES (6, 3, 1, 3679831, N'MBBank', N'0569000899', CAST(N'2024-10-19T00:56:39.000' AS DateTime), 0.0000, 3000.0000, 0.0000, N'lo trinh ti do', N'FT24293334700168', N'', N'', 3342, 1)
INSERT [Business].[TransactionsSystem] ([Id], [UserPayID], [PayeeID], [APITransactions], [BankBrandName], [AccountNumber], [TransactionDate], [AmountOut], [AmountIn], [Accumulated], [TransactionContent], [ReferenceNumber], [Code], [SubAccount], [BankAccountId], [Status]) VALUES (7, 1, 3, 3679831, N'MBBank', N'0569000899', CAST(N'2024-10-19T00:56:39.000' AS DateTime), 6000.0000, 0.0000, 0.0000, N'lo trinh ti do 434', N'FT24293334700168', N'', N'', 3342, 1)
INSERT [Business].[TransactionsSystem] ([Id], [UserPayID], [PayeeID], [APITransactions], [BankBrandName], [AccountNumber], [TransactionDate], [AmountOut], [AmountIn], [Accumulated], [TransactionContent], [ReferenceNumber], [Code], [SubAccount], [BankAccountId], [Status]) VALUES (10, 3, 1, 3798396, N'MBBank', N'0569000899', CAST(N'2024-10-21T21:09:49.000' AS DateTime), 0.0000, 20000.0000, 0.0000, N'User chuyen tien QHARTM', N'FT24295555835584', N'', N'', 3342, 1)
INSERT [Business].[TransactionsSystem] ([Id], [UserPayID], [PayeeID], [APITransactions], [BankBrandName], [AccountNumber], [TransactionDate], [AmountOut], [AmountIn], [Accumulated], [TransactionContent], [ReferenceNumber], [Code], [SubAccount], [BankAccountId], [Status]) VALUES (13, 3, 1, 4824236, N'MBBank', N'0569000899', CAST(N'2024-11-20T17:26:51.000' AS DateTime), 0.0000, 20000.0000, 0.0000, N'yfCIqnYh', N'FT24325259081440', N'', N'', 3342, 1)
INSERT [Business].[TransactionsSystem] ([Id], [UserPayID], [PayeeID], [APITransactions], [BankBrandName], [AccountNumber], [TransactionDate], [AmountOut], [AmountIn], [Accumulated], [TransactionContent], [ReferenceNumber], [Code], [SubAccount], [BankAccountId], [Status]) VALUES (34, 45, 1, 4835851, N'MBBank', N'0569000899', CAST(N'2024-11-21T00:54:25.000' AS DateTime), 0.0000, 10000.0000, 0.0000, N'xbQej0DU9pnl', N'FT24326374461767', N'', N'', 3342, 1)
INSERT [Business].[TransactionsSystem] ([Id], [UserPayID], [PayeeID], [APITransactions], [BankBrandName], [AccountNumber], [TransactionDate], [AmountOut], [AmountIn], [Accumulated], [TransactionContent], [ReferenceNumber], [Code], [SubAccount], [BankAccountId], [Status]) VALUES (35, 48, 1, 4841889, N'MBBank', N'0569000899', CAST(N'2024-11-21T10:29:13.000' AS DateTime), 0.0000, 89000.0000, 0.0000, N'qCvkREJsWMmE', N'FT24326385819019', N'', N'', 3342, 1)
INSERT [Business].[TransactionsSystem] ([Id], [UserPayID], [PayeeID], [APITransactions], [BankBrandName], [AccountNumber], [TransactionDate], [AmountOut], [AmountIn], [Accumulated], [TransactionContent], [ReferenceNumber], [Code], [SubAccount], [BankAccountId], [Status]) VALUES (36, 49, 1, NULL, NULL, N'0569000899', NULL, NULL, 89000.0000, NULL, N'dBzKZl1x0KRg', NULL, NULL, NULL, NULL, NULL)
INSERT [Business].[TransactionsSystem] ([Id], [UserPayID], [PayeeID], [APITransactions], [BankBrandName], [AccountNumber], [TransactionDate], [AmountOut], [AmountIn], [Accumulated], [TransactionContent], [ReferenceNumber], [Code], [SubAccount], [BankAccountId], [Status]) VALUES (37, 49, 1, 4857750, N'MBBank', N'0569000899', CAST(N'2024-11-21T22:20:25.000' AS DateTime), 0.0000, 10000.0000, 0.0000, N'eZQ8SqBeFcjc', N'FT24327483026847', N'', N'', 3342, 1)
INSERT [Business].[TransactionsSystem] ([Id], [UserPayID], [PayeeID], [APITransactions], [BankBrandName], [AccountNumber], [TransactionDate], [AmountOut], [AmountIn], [Accumulated], [TransactionContent], [ReferenceNumber], [Code], [SubAccount], [BankAccountId], [Status]) VALUES (38, 55, 1, NULL, NULL, N'0569000899', NULL, NULL, 10000.0000, NULL, N'C7kIerwrS2aM', NULL, NULL, NULL, NULL, NULL)
INSERT [Business].[TransactionsSystem] ([Id], [UserPayID], [PayeeID], [APITransactions], [BankBrandName], [AccountNumber], [TransactionDate], [AmountOut], [AmountIn], [Accumulated], [TransactionContent], [ReferenceNumber], [Code], [SubAccount], [BankAccountId], [Status]) VALUES (39, 55, 1, NULL, NULL, N'0569000899', NULL, NULL, 10000.0000, NULL, N'yeSoDEkIFuxA', NULL, NULL, NULL, NULL, NULL)
INSERT [Business].[TransactionsSystem] ([Id], [UserPayID], [PayeeID], [APITransactions], [BankBrandName], [AccountNumber], [TransactionDate], [AmountOut], [AmountIn], [Accumulated], [TransactionContent], [ReferenceNumber], [Code], [SubAccount], [BankAccountId], [Status]) VALUES (40, 55, 1, NULL, NULL, N'0569000899', NULL, NULL, 10000.0000, NULL, N'3zSycuybFuVH', NULL, NULL, NULL, NULL, NULL)
INSERT [Business].[TransactionsSystem] ([Id], [UserPayID], [PayeeID], [APITransactions], [BankBrandName], [AccountNumber], [TransactionDate], [AmountOut], [AmountIn], [Accumulated], [TransactionContent], [ReferenceNumber], [Code], [SubAccount], [BankAccountId], [Status]) VALUES (41, 55, 1, NULL, NULL, N'0569000899', NULL, NULL, 10000.0000, NULL, N'2DbbeNbbSwtu', NULL, NULL, NULL, NULL, NULL)
INSERT [Business].[TransactionsSystem] ([Id], [UserPayID], [PayeeID], [APITransactions], [BankBrandName], [AccountNumber], [TransactionDate], [AmountOut], [AmountIn], [Accumulated], [TransactionContent], [ReferenceNumber], [Code], [SubAccount], [BankAccountId], [Status]) VALUES (42, 55, 1, NULL, NULL, N'0569000899', NULL, NULL, 10000.0000, NULL, N'bUBRbtRBZiHp', NULL, NULL, NULL, NULL, NULL)
INSERT [Business].[TransactionsSystem] ([Id], [UserPayID], [PayeeID], [APITransactions], [BankBrandName], [AccountNumber], [TransactionDate], [AmountOut], [AmountIn], [Accumulated], [TransactionContent], [ReferenceNumber], [Code], [SubAccount], [BankAccountId], [Status]) VALUES (43, 55, 1, NULL, NULL, N'0569000899', NULL, NULL, 2000.0000, NULL, N'9WI3qASKdx4c', NULL, NULL, NULL, NULL, NULL)
INSERT [Business].[TransactionsSystem] ([Id], [UserPayID], [PayeeID], [APITransactions], [BankBrandName], [AccountNumber], [TransactionDate], [AmountOut], [AmountIn], [Accumulated], [TransactionContent], [ReferenceNumber], [Code], [SubAccount], [BankAccountId], [Status]) VALUES (44, 55, 1, NULL, NULL, N'0569000899', NULL, NULL, 10000.0000, NULL, N'usLPOboYpQUw', NULL, NULL, NULL, NULL, NULL)
INSERT [Business].[TransactionsSystem] ([Id], [UserPayID], [PayeeID], [APITransactions], [BankBrandName], [AccountNumber], [TransactionDate], [AmountOut], [AmountIn], [Accumulated], [TransactionContent], [ReferenceNumber], [Code], [SubAccount], [BankAccountId], [Status]) VALUES (45, 55, 1, NULL, NULL, N'0569000899', NULL, NULL, 10000.0000, NULL, N'3cSVusAbFLiG', NULL, NULL, NULL, NULL, NULL)
INSERT [Business].[TransactionsSystem] ([Id], [UserPayID], [PayeeID], [APITransactions], [BankBrandName], [AccountNumber], [TransactionDate], [AmountOut], [AmountIn], [Accumulated], [TransactionContent], [ReferenceNumber], [Code], [SubAccount], [BankAccountId], [Status]) VALUES (46, 55, 1, NULL, NULL, N'0569000899', NULL, NULL, 10000.0000, NULL, N'hlvEy3eid4N2', NULL, NULL, NULL, NULL, NULL)
INSERT [Business].[TransactionsSystem] ([Id], [UserPayID], [PayeeID], [APITransactions], [BankBrandName], [AccountNumber], [TransactionDate], [AmountOut], [AmountIn], [Accumulated], [TransactionContent], [ReferenceNumber], [Code], [SubAccount], [BankAccountId], [Status]) VALUES (47, 55, 1, NULL, NULL, N'0569000899', NULL, NULL, 10000.0000, NULL, N'2sHJWndyBkRE', NULL, NULL, NULL, NULL, NULL)
INSERT [Business].[TransactionsSystem] ([Id], [UserPayID], [PayeeID], [APITransactions], [BankBrandName], [AccountNumber], [TransactionDate], [AmountOut], [AmountIn], [Accumulated], [TransactionContent], [ReferenceNumber], [Code], [SubAccount], [BankAccountId], [Status]) VALUES (48, 55, 1, NULL, NULL, N'0569000899', NULL, NULL, 10000.0000, NULL, N'D3JJNMAe2Gto', NULL, NULL, NULL, NULL, NULL)
INSERT [Business].[TransactionsSystem] ([Id], [UserPayID], [PayeeID], [APITransactions], [BankBrandName], [AccountNumber], [TransactionDate], [AmountOut], [AmountIn], [Accumulated], [TransactionContent], [ReferenceNumber], [Code], [SubAccount], [BankAccountId], [Status]) VALUES (49, 55, 1, NULL, NULL, N'0569000899', NULL, NULL, 10000.0000, NULL, N'4GuKzCmbtGPj', NULL, NULL, NULL, NULL, NULL)
INSERT [Business].[TransactionsSystem] ([Id], [UserPayID], [PayeeID], [APITransactions], [BankBrandName], [AccountNumber], [TransactionDate], [AmountOut], [AmountIn], [Accumulated], [TransactionContent], [ReferenceNumber], [Code], [SubAccount], [BankAccountId], [Status]) VALUES (50, 55, 1, NULL, NULL, N'0569000899', NULL, NULL, 10000.0000, NULL, N'EXlE1fSqGJo1', NULL, NULL, NULL, NULL, NULL)
INSERT [Business].[TransactionsSystem] ([Id], [UserPayID], [PayeeID], [APITransactions], [BankBrandName], [AccountNumber], [TransactionDate], [AmountOut], [AmountIn], [Accumulated], [TransactionContent], [ReferenceNumber], [Code], [SubAccount], [BankAccountId], [Status]) VALUES (51, 55, 1, NULL, NULL, N'0569000899', NULL, NULL, 10000.0000, NULL, N'rmC5coadxvb7', NULL, NULL, NULL, NULL, NULL)
INSERT [Business].[TransactionsSystem] ([Id], [UserPayID], [PayeeID], [APITransactions], [BankBrandName], [AccountNumber], [TransactionDate], [AmountOut], [AmountIn], [Accumulated], [TransactionContent], [ReferenceNumber], [Code], [SubAccount], [BankAccountId], [Status]) VALUES (52, 56, 1, NULL, NULL, N'0569000899', NULL, NULL, 10000.0000, NULL, N'FcUFRA9wII78', NULL, NULL, NULL, NULL, NULL)
INSERT [Business].[TransactionsSystem] ([Id], [UserPayID], [PayeeID], [APITransactions], [BankBrandName], [AccountNumber], [TransactionDate], [AmountOut], [AmountIn], [Accumulated], [TransactionContent], [ReferenceNumber], [Code], [SubAccount], [BankAccountId], [Status]) VALUES (53, 45, 1, NULL, NULL, N'0569000899', NULL, NULL, 10000.0000, NULL, N'1qcn9Kl5P73d', NULL, NULL, NULL, NULL, NULL)
INSERT [Business].[TransactionsSystem] ([Id], [UserPayID], [PayeeID], [APITransactions], [BankBrandName], [AccountNumber], [TransactionDate], [AmountOut], [AmountIn], [Accumulated], [TransactionContent], [ReferenceNumber], [Code], [SubAccount], [BankAccountId], [Status]) VALUES (54, 56, 1, NULL, NULL, N'0569000899', NULL, NULL, 10000.0000, NULL, N'QdL3rw55Fgxc', NULL, NULL, NULL, NULL, NULL)
INSERT [Business].[TransactionsSystem] ([Id], [UserPayID], [PayeeID], [APITransactions], [BankBrandName], [AccountNumber], [TransactionDate], [AmountOut], [AmountIn], [Accumulated], [TransactionContent], [ReferenceNumber], [Code], [SubAccount], [BankAccountId], [Status]) VALUES (55, 57, 1, NULL, NULL, N'0569000899', NULL, NULL, 10000.0000, NULL, N's0TnZtqzESAF', NULL, NULL, NULL, NULL, NULL)
INSERT [Business].[TransactionsSystem] ([Id], [UserPayID], [PayeeID], [APITransactions], [BankBrandName], [AccountNumber], [TransactionDate], [AmountOut], [AmountIn], [Accumulated], [TransactionContent], [ReferenceNumber], [Code], [SubAccount], [BankAccountId], [Status]) VALUES (1013, 1056, 1, NULL, NULL, N'0569000899', NULL, NULL, 10000.0000, NULL, N'qPuO5OJJrXXy', NULL, NULL, NULL, NULL, NULL)
INSERT [Business].[TransactionsSystem] ([Id], [UserPayID], [PayeeID], [APITransactions], [BankBrandName], [AccountNumber], [TransactionDate], [AmountOut], [AmountIn], [Accumulated], [TransactionContent], [ReferenceNumber], [Code], [SubAccount], [BankAccountId], [Status]) VALUES (2013, 3078, 1, NULL, NULL, N'0569000899', NULL, NULL, 10000.0000, NULL, N'3QtOCCNk3174', NULL, NULL, NULL, NULL, NULL)
INSERT [Business].[TransactionsSystem] ([Id], [UserPayID], [PayeeID], [APITransactions], [BankBrandName], [AccountNumber], [TransactionDate], [AmountOut], [AmountIn], [Accumulated], [TransactionContent], [ReferenceNumber], [Code], [SubAccount], [BankAccountId], [Status]) VALUES (2014, 3081, 1, NULL, NULL, N'0569000899', NULL, NULL, 10000.0000, NULL, N'QgG8ZcKW1ntV', NULL, NULL, NULL, NULL, NULL)
INSERT [Business].[TransactionsSystem] ([Id], [UserPayID], [PayeeID], [APITransactions], [BankBrandName], [AccountNumber], [TransactionDate], [AmountOut], [AmountIn], [Accumulated], [TransactionContent], [ReferenceNumber], [Code], [SubAccount], [BankAccountId], [Status]) VALUES (2015, 3082, 1, NULL, NULL, N'0569000899', NULL, NULL, 10000.0000, NULL, N'KT0WO5UC11Tj', NULL, NULL, NULL, NULL, NULL)
INSERT [Business].[TransactionsSystem] ([Id], [UserPayID], [PayeeID], [APITransactions], [BankBrandName], [AccountNumber], [TransactionDate], [AmountOut], [AmountIn], [Accumulated], [TransactionContent], [ReferenceNumber], [Code], [SubAccount], [BankAccountId], [Status]) VALUES (2016, 3083, 1, NULL, NULL, N'0569000899', NULL, NULL, 10000.0000, NULL, N'GkNM0KcMbrxd', NULL, NULL, NULL, NULL, NULL)
INSERT [Business].[TransactionsSystem] ([Id], [UserPayID], [PayeeID], [APITransactions], [BankBrandName], [AccountNumber], [TransactionDate], [AmountOut], [AmountIn], [Accumulated], [TransactionContent], [ReferenceNumber], [Code], [SubAccount], [BankAccountId], [Status]) VALUES (2017, 3084, 1, NULL, NULL, N'0569000899', NULL, NULL, 10000.0000, NULL, N'YPgN1JstbGrA', NULL, NULL, NULL, NULL, NULL)
INSERT [Business].[TransactionsSystem] ([Id], [UserPayID], [PayeeID], [APITransactions], [BankBrandName], [AccountNumber], [TransactionDate], [AmountOut], [AmountIn], [Accumulated], [TransactionContent], [ReferenceNumber], [Code], [SubAccount], [BankAccountId], [Status]) VALUES (3013, 4057, 1, NULL, NULL, N'0569000899', NULL, NULL, 10000.0000, NULL, N'XU5OyK0DqJMW', NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [Business].[TransactionsSystem] OFF
GO
SET IDENTITY_INSERT [Business].[UserListManagement] ON 

INSERT [Business].[UserListManagement] ([Id], [NutritionistId], [UserId], [Describe], [StartDate], [EndDate], [Rate], [IsDone]) VALUES (1, 2, 45, N'Pagkit1', NULL, NULL, NULL, NULL)
INSERT [Business].[UserListManagement] ([Id], [NutritionistId], [UserId], [Describe], [StartDate], [EndDate], [Rate], [IsDone]) VALUES (4, 2, 44, N'This is a starter package.', CAST(N'2024-11-20T17:53:54.197' AS DateTime), CAST(N'2024-12-20T17:53:54.197' AS DateTime), NULL, 0)
INSERT [Business].[UserListManagement] ([Id], [NutritionistId], [UserId], [Describe], [StartDate], [EndDate], [Rate], [IsDone]) VALUES (5, 1, 48, N'Gói 1 tháng nâng cao trải nghiệm Premium.', CAST(N'2024-11-21T03:28:39.433' AS DateTime), CAST(N'2024-12-21T03:28:39.433' AS DateTime), NULL, 0)
INSERT [Business].[UserListManagement] ([Id], [NutritionistId], [UserId], [Describe], [StartDate], [EndDate], [Rate], [IsDone]) VALUES (6, 2, 49, N'This is a starter package.', CAST(N'2024-11-21T15:18:29.390' AS DateTime), CAST(N'2024-12-21T15:18:29.390' AS DateTime), NULL, 0)
INSERT [Business].[UserListManagement] ([Id], [NutritionistId], [UserId], [Describe], [StartDate], [EndDate], [Rate], [IsDone]) VALUES (7, 2, 50, N'This is a starter package.', CAST(N'2024-11-22T10:11:59.803' AS DateTime), CAST(N'2025-11-17T10:11:59.803' AS DateTime), NULL, 0)
INSERT [Business].[UserListManagement] ([Id], [NutritionistId], [UserId], [Describe], [StartDate], [EndDate], [Rate], [IsDone]) VALUES (8, 3, 55, N'This package offers moderate features.', CAST(N'2024-11-22T10:21:55.937' AS DateTime), CAST(N'2025-01-21T10:21:55.937' AS DateTime), NULL, 0)
INSERT [Business].[UserListManagement] ([Id], [NutritionistId], [UserId], [Describe], [StartDate], [EndDate], [Rate], [IsDone]) VALUES (9, 2, 56, N'This is a starter package.', CAST(N'2024-10-23T02:41:15.790' AS DateTime), CAST(N'2024-11-23T02:41:15.790' AS DateTime), NULL, 0)
INSERT [Business].[UserListManagement] ([Id], [NutritionistId], [UserId], [Describe], [StartDate], [EndDate], [Rate], [IsDone]) VALUES (10, 2, 45, N'This is a starter package.', CAST(N'2024-11-23T07:34:39.877' AS DateTime), CAST(N'2024-12-23T07:34:39.877' AS DateTime), NULL, 0)
INSERT [Business].[UserListManagement] ([Id], [NutritionistId], [UserId], [Describe], [StartDate], [EndDate], [Rate], [IsDone]) VALUES (11, 2, 56, N'This is a starter package.', CAST(N'2024-11-23T09:57:37.533' AS DateTime), CAST(N'2024-12-23T09:57:37.533' AS DateTime), NULL, 0)
INSERT [Business].[UserListManagement] ([Id], [NutritionistId], [UserId], [Describe], [StartDate], [EndDate], [Rate], [IsDone]) VALUES (12, 2, 57, N'This is a starter package.', CAST(N'2024-11-23T10:01:09.643' AS DateTime), CAST(N'2024-12-23T10:01:09.643' AS DateTime), NULL, 0)
INSERT [Business].[UserListManagement] ([Id], [NutritionistId], [UserId], [Describe], [StartDate], [EndDate], [Rate], [IsDone]) VALUES (1002, 2, 1056, N'This is a starter package.', CAST(N'2024-11-24T06:40:55.753' AS DateTime), CAST(N'2024-12-24T06:40:55.753' AS DateTime), NULL, 0)
INSERT [Business].[UserListManagement] ([Id], [NutritionistId], [UserId], [Describe], [StartDate], [EndDate], [Rate], [IsDone]) VALUES (2002, 2, 3078, N'This is a starter package.', CAST(N'2024-11-27T07:28:07.300' AS DateTime), CAST(N'2024-12-27T07:28:07.300' AS DateTime), NULL, 0)
INSERT [Business].[UserListManagement] ([Id], [NutritionistId], [UserId], [Describe], [StartDate], [EndDate], [Rate], [IsDone]) VALUES (2003, 2, 3081, N'This is a starter package.', CAST(N'2024-11-27T10:01:21.330' AS DateTime), CAST(N'2024-12-27T10:01:21.330' AS DateTime), NULL, 0)
INSERT [Business].[UserListManagement] ([Id], [NutritionistId], [UserId], [Describe], [StartDate], [EndDate], [Rate], [IsDone]) VALUES (2004, 2, 3082, N'This is a starter package.', CAST(N'2024-11-27T11:39:35.263' AS DateTime), CAST(N'2024-12-27T11:39:35.263' AS DateTime), NULL, 0)
INSERT [Business].[UserListManagement] ([Id], [NutritionistId], [UserId], [Describe], [StartDate], [EndDate], [Rate], [IsDone]) VALUES (2005, 2, 3083, N'This is a starter package.', CAST(N'2024-11-27T13:24:23.683' AS DateTime), CAST(N'2024-12-27T13:24:23.683' AS DateTime), NULL, 0)
INSERT [Business].[UserListManagement] ([Id], [NutritionistId], [UserId], [Describe], [StartDate], [EndDate], [Rate], [IsDone]) VALUES (2006, 2, 3084, N'This is a starter package.', CAST(N'2024-11-27T13:47:07.500' AS DateTime), CAST(N'2024-12-27T13:47:07.500' AS DateTime), NULL, 0)
INSERT [Business].[UserListManagement] ([Id], [NutritionistId], [UserId], [Describe], [StartDate], [EndDate], [Rate], [IsDone]) VALUES (3002, 2, 4057, N'This is a starter package.', CAST(N'2024-11-28T11:45:28.863' AS DateTime), CAST(N'2024-12-28T11:45:28.863' AS DateTime), NULL, 0)
SET IDENTITY_INSERT [Business].[UserListManagement] OFF
GO
SET IDENTITY_INSERT [FoodData].[CookingDifficulty] ON 

INSERT [FoodData].[CookingDifficulty] ([Id], [Name]) VALUES (1, N'Super hard')
INSERT [FoodData].[CookingDifficulty] ([Id], [Name]) VALUES (2, N'Hard')
INSERT [FoodData].[CookingDifficulty] ([Id], [Name]) VALUES (3, N'Medium')
INSERT [FoodData].[CookingDifficulty] ([Id], [Name]) VALUES (4, N'Easy')
SET IDENTITY_INSERT [FoodData].[CookingDifficulty] OFF
GO
SET IDENTITY_INSERT [FoodData].[DietType] ON 

INSERT [FoodData].[DietType] ([DietTypeId], [Name], [Describe]) VALUES (1, N'Tất cả mọi thứ', N'Ăn tạp')
INSERT [FoodData].[DietType] ([DietTypeId], [Name], [Describe]) VALUES (2, N'Ăn chay', N'Món toàn rau')
SET IDENTITY_INSERT [FoodData].[DietType] OFF
GO
INSERT [FoodData].[DietWithFoodType] ([FoodTypeId], [DietTypeId]) VALUES (1, 1)
INSERT [FoodData].[DietWithFoodType] ([FoodTypeId], [DietTypeId]) VALUES (1, 2)
INSERT [FoodData].[DietWithFoodType] ([FoodTypeId], [DietTypeId]) VALUES (2, 1)
INSERT [FoodData].[DietWithFoodType] ([FoodTypeId], [DietTypeId]) VALUES (3, 1)
INSERT [FoodData].[DietWithFoodType] ([FoodTypeId], [DietTypeId]) VALUES (3, 2)
INSERT [FoodData].[DietWithFoodType] ([FoodTypeId], [DietTypeId]) VALUES (4, 1)
INSERT [FoodData].[DietWithFoodType] ([FoodTypeId], [DietTypeId]) VALUES (4, 2)
INSERT [FoodData].[DietWithFoodType] ([FoodTypeId], [DietTypeId]) VALUES (5, 1)
INSERT [FoodData].[DietWithFoodType] ([FoodTypeId], [DietTypeId]) VALUES (5, 2)
GO
INSERT [FoodData].[FoodAndDiseases] ([ListOfDiseasesId], [FoodListId], [Describe], [IsGoodOrBad]) VALUES (1, 1, N'Tomatoes are good for diabetes', 1)
INSERT [FoodData].[FoodAndDiseases] ([ListOfDiseasesId], [FoodListId], [Describe], [IsGoodOrBad]) VALUES (2, 2, N'Beef should be avoided for heart disease', 0)
INSERT [FoodData].[FoodAndDiseases] ([ListOfDiseasesId], [FoodListId], [Describe], [IsGoodOrBad]) VALUES (3, 3, N'Yogurt helps digestion', 0)
INSERT [FoodData].[FoodAndDiseases] ([ListOfDiseasesId], [FoodListId], [Describe], [IsGoodOrBad]) VALUES (4, 4, N'Apples are good for weight loss', 1)
INSERT [FoodData].[FoodAndDiseases] ([ListOfDiseasesId], [FoodListId], [Describe], [IsGoodOrBad]) VALUES (5, 1, N'55', 0)
INSERT [FoodData].[FoodAndDiseases] ([ListOfDiseasesId], [FoodListId], [Describe], [IsGoodOrBad]) VALUES (1005, 4, N'ko tốt', 0)
GO
SET IDENTITY_INSERT [FoodData].[FoodList] ON 

INSERT [FoodData].[FoodList] ([FoodListId], [Name], [Describe], [Rate], [NumberRate], [URLImage], [FoodTypeId], [KeyNoteId], [IsActive], [PreparationTime], [CookingTime], [CookingDifficultyId]) VALUES (1, N'Trứng chiên xúc xích', N'Fresh vegetable', 89.6, 78, N'food_1.png', 2, 1, 1, 20, 24, 1)
INSERT [FoodData].[FoodList] ([FoodListId], [Name], [Describe], [Rate], [NumberRate], [URLImage], [FoodTypeId], [KeyNoteId], [IsActive], [PreparationTime], [CookingTime], [CookingDifficultyId]) VALUES (2, N'Bánh mì ốp la thịt bò', N'Rich in protein', 86.9, 56, N'food_2.png', 2, 1, 1, 30, 35, 3)
INSERT [FoodData].[FoodList] ([FoodListId], [Name], [Describe], [Rate], [NumberRate], [URLImage], [FoodTypeId], [KeyNoteId], [IsActive], [PreparationTime], [CookingTime], [CookingDifficultyId]) VALUES (3, N'Salad gà cần tây', N'Dairy product', 90.6, 34, N'food_3.png', 1, 1, 1, 40, 32, 1)
INSERT [FoodData].[FoodList] ([FoodListId], [Name], [Describe], [Rate], [NumberRate], [URLImage], [FoodTypeId], [KeyNoteId], [IsActive], [PreparationTime], [CookingTime], [CookingDifficultyId]) VALUES (4, N'Đậu phụ xào rau muống', N'Sweet fruit', 75.8, 46, N'food_4.png', 1, 1, 1, 40, 27, 1)
INSERT [FoodData].[FoodList] ([FoodListId], [Name], [Describe], [Rate], [NumberRate], [URLImage], [FoodTypeId], [KeyNoteId], [IsActive], [PreparationTime], [CookingTime], [CookingDifficultyId]) VALUES (5, N'Thịt bò xào cần tây', N'Sweet fruit', 67.9, 73, N'food_5.png', 2, 1, 1, 30, 45, 4)
INSERT [FoodData].[FoodList] ([FoodListId], [Name], [Describe], [Rate], [NumberRate], [URLImage], [FoodTypeId], [KeyNoteId], [IsActive], [PreparationTime], [CookingTime], [CookingDifficultyId]) VALUES (6, N'Đậu phụ', N'Sweet fruit', 67.9, 73, N'food_6.png', 1, 2, 1, 20, 0, 1)
SET IDENTITY_INSERT [FoodData].[FoodList] OFF
GO
SET IDENTITY_INSERT [FoodData].[FoodType] ON 

INSERT [FoodData].[FoodType] ([FoodTypeId], [Name], [Describe]) VALUES (1, N'Rau', N'Plant-based food')
INSERT [FoodData].[FoodType] ([FoodTypeId], [Name], [Describe]) VALUES (2, N'Thịt', N'Animal-based food')
INSERT [FoodData].[FoodType] ([FoodTypeId], [Name], [Describe]) VALUES (3, N'Sữa', N'Milk-based products')
INSERT [FoodData].[FoodType] ([FoodTypeId], [Name], [Describe]) VALUES (4, N'Hoa Quả', N'Edible fruit products')
INSERT [FoodData].[FoodType] ([FoodTypeId], [Name], [Describe]) VALUES (5, N'Hạt', NULL)
SET IDENTITY_INSERT [FoodData].[FoodType] OFF
GO
SET IDENTITY_INSERT [FoodData].[IngredientDetails100g] ON 

INSERT [FoodData].[IngredientDetails100g] ([Id], [KeyNoteId], [Name], [Describe], [URLImage], [TypeOfCalculationId], [Energy], [Water], [Protein], [Fat], [Carbohydrate], [Fiber], [Ash], [Sugar], [Galactose], [Maltose], [Lactose], [Fructose], [Glucose], [Sucrose], [Calcium], [Iron], [Magnesium], [Manganese], [Phosphorous], [Potassium], [Sodium], [Zinc], [Copper], [Selenium], [Vitamin_C], [Vitamin_B1], [Vitamin_B2], [Vitamin_PP], [Vitamin_B5], [Vitamin_B6], [Folat], [Vitamin_B9], [Vitamin_H], [Vitamin_B12], [Vitamin_A], [Vitamin_D], [Vitamin_E], [Vitamin_K], [Beta_caroten], [Alpha_caroten], [Beta_cryptoxanthin], [Lycopen], [LuteinVsZeaxanthin], [Purin], [Total_isoflavone], [Daidzein], [Genistein], [Glycetin], [Total_saturated_fatty_acid], [Palmitic_C16_0], [Margaric_C17_0], [Stearic_C18_0], [Arachidic_C20_0], [Behenic_C22_0], [Lignoceric_C24_0], [TotalMonounsaturatedFattyAcid], [Myristoleic_C14_1], [Palmitoleic_C16_1], [Oleic_C18_1], [TotalPolyunsaturatedFattyAcid], [Linoleic_C18_2_n6], [Linolenic_C18_2_n3], [Arachidonic_C20_4], [Eicosapentaenoic_C20_5_n3], [Docosahexaenoic_C22_6_n3], [TotalTransFattyAcid], [Cholesterol], [Phytosterol], [Lysin], [Methionin], [Tryptophan], [Phenylalanin], [Threonin], [Valin], [Leucin], [Isoleucin], [Arginin], [Histidin], [Cystin], [Tyrosin], [Alanin], [Acid_aspartic], [Acid_glutamic], [Glycin], [Prolin], [Serin]) VALUES (1, NULL, N'Trứng', N'Nguyên liệu phổ biến trong nấu ăn, giàu protein và chất béo.', N'https://example.com/trung.jpg', 1, 155, 74, 12.6, 10.6, 1.1, 0, 0.5, 0, 0, 0, 0, 0, 0, 0, 50, 1.2, 10, 0.03, 172, 126, 140, 0.7, 0.02, 0.2, 0, 0.05, 0.03, 0.09, 0.05, 0.04, 0, 0.03, 640, 0, 1.05, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3.3, 1.5, 0, 1.3, 0.03, 0, 5.7, 0, 0.03, 4.6, 0.8, 0, 0, 0, 0, 0, 0.4, 0, 0.6, 0.7, 0, 0.2, 0.1, 0.7, 1.2, 0.9, 0.8, 0.6, 0.5, 1.1, 0.4, 0.5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
INSERT [FoodData].[IngredientDetails100g] ([Id], [KeyNoteId], [Name], [Describe], [URLImage], [TypeOfCalculationId], [Energy], [Water], [Protein], [Fat], [Carbohydrate], [Fiber], [Ash], [Sugar], [Galactose], [Maltose], [Lactose], [Fructose], [Glucose], [Sucrose], [Calcium], [Iron], [Magnesium], [Manganese], [Phosphorous], [Potassium], [Sodium], [Zinc], [Copper], [Selenium], [Vitamin_C], [Vitamin_B1], [Vitamin_B2], [Vitamin_PP], [Vitamin_B5], [Vitamin_B6], [Folat], [Vitamin_B9], [Vitamin_H], [Vitamin_B12], [Vitamin_A], [Vitamin_D], [Vitamin_E], [Vitamin_K], [Beta_caroten], [Alpha_caroten], [Beta_cryptoxanthin], [Lycopen], [LuteinVsZeaxanthin], [Purin], [Total_isoflavone], [Daidzein], [Genistein], [Glycetin], [Total_saturated_fatty_acid], [Palmitic_C16_0], [Margaric_C17_0], [Stearic_C18_0], [Arachidic_C20_0], [Behenic_C22_0], [Lignoceric_C24_0], [TotalMonounsaturatedFattyAcid], [Myristoleic_C14_1], [Palmitoleic_C16_1], [Oleic_C18_1], [TotalPolyunsaturatedFattyAcid], [Linoleic_C18_2_n6], [Linolenic_C18_2_n3], [Arachidonic_C20_4], [Eicosapentaenoic_C20_5_n3], [Docosahexaenoic_C22_6_n3], [TotalTransFattyAcid], [Cholesterol], [Phytosterol], [Lysin], [Methionin], [Tryptophan], [Phenylalanin], [Threonin], [Valin], [Leucin], [Isoleucin], [Arginin], [Histidin], [Cystin], [Tyrosin], [Alanin], [Acid_aspartic], [Acid_glutamic], [Glycin], [Prolin], [Serin]) VALUES (2, NULL, N'Thịt bò', N'Thịt đỏ, giàu protein và sắt.', N'https://example.com/thit-bo.jpg', 2, 250, 55, 26, 17, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 300, 2.7, 24, 0.02, 180, 352, 72, 8, 0.12, 0.1, 0, 0.06, 0.04, 0.1, 0.07, 0.06, 0, 0.03, 50, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6, 2.5, 0, 1.6, 0.02, 0, 8, 0, 0.1, 7.8, 0.6, 0, 0, 0, 0, 0, 0.7, 0, 1.2, 0.5, 0, 0.3, 0.2, 0.8, 1.3, 1.1, 1, 0.8, 0.7, 1.2, 0.5, 0.7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
INSERT [FoodData].[IngredientDetails100g] ([Id], [KeyNoteId], [Name], [Describe], [URLImage], [TypeOfCalculationId], [Energy], [Water], [Protein], [Fat], [Carbohydrate], [Fiber], [Ash], [Sugar], [Galactose], [Maltose], [Lactose], [Fructose], [Glucose], [Sucrose], [Calcium], [Iron], [Magnesium], [Manganese], [Phosphorous], [Potassium], [Sodium], [Zinc], [Copper], [Selenium], [Vitamin_C], [Vitamin_B1], [Vitamin_B2], [Vitamin_PP], [Vitamin_B5], [Vitamin_B6], [Folat], [Vitamin_B9], [Vitamin_H], [Vitamin_B12], [Vitamin_A], [Vitamin_D], [Vitamin_E], [Vitamin_K], [Beta_caroten], [Alpha_caroten], [Beta_cryptoxanthin], [Lycopen], [LuteinVsZeaxanthin], [Purin], [Total_isoflavone], [Daidzein], [Genistein], [Glycetin], [Total_saturated_fatty_acid], [Palmitic_C16_0], [Margaric_C17_0], [Stearic_C18_0], [Arachidic_C20_0], [Behenic_C22_0], [Lignoceric_C24_0], [TotalMonounsaturatedFattyAcid], [Myristoleic_C14_1], [Palmitoleic_C16_1], [Oleic_C18_1], [TotalPolyunsaturatedFattyAcid], [Linoleic_C18_2_n6], [Linolenic_C18_2_n3], [Arachidonic_C20_4], [Eicosapentaenoic_C20_5_n3], [Docosahexaenoic_C22_6_n3], [TotalTransFattyAcid], [Cholesterol], [Phytosterol], [Lysin], [Methionin], [Tryptophan], [Phenylalanin], [Threonin], [Valin], [Leucin], [Isoleucin], [Arginin], [Histidin], [Cystin], [Tyrosin], [Alanin], [Acid_aspartic], [Acid_glutamic], [Glycin], [Prolin], [Serin]) VALUES (3, NULL, N'Thịt gà', N'Thịt trắng, dễ tiêu hoá, giàu protein.', N'https://example.com/thit-ga.jpg', 2, 165, 70, 31, 3.6, 0, 0, 0, 0, 0, 0, 0, 0, 0, 12, 12, 1, 20, 0.01, 150, 225, 50, 6, 0.09, 0.09, 0, 0.02, 0.02, 0.08, 0.04, 0.05, 0, 0.01, 0, 0, 0.8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 1.7, 0, 1.2, 0.02, 0, 5, 0, 0.07, 4.8, 0.5, 0, 0, 0, 0, 0, 0.4, 0, 0.7, 0.4, 0, 0.2, 0.1, 0.6, 1, 0.8, 0.7, 0.6, 0.4, 1, 0.3, 0.4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
INSERT [FoodData].[IngredientDetails100g] ([Id], [KeyNoteId], [Name], [Describe], [URLImage], [TypeOfCalculationId], [Energy], [Water], [Protein], [Fat], [Carbohydrate], [Fiber], [Ash], [Sugar], [Galactose], [Maltose], [Lactose], [Fructose], [Glucose], [Sucrose], [Calcium], [Iron], [Magnesium], [Manganese], [Phosphorous], [Potassium], [Sodium], [Zinc], [Copper], [Selenium], [Vitamin_C], [Vitamin_B1], [Vitamin_B2], [Vitamin_PP], [Vitamin_B5], [Vitamin_B6], [Folat], [Vitamin_B9], [Vitamin_H], [Vitamin_B12], [Vitamin_A], [Vitamin_D], [Vitamin_E], [Vitamin_K], [Beta_caroten], [Alpha_caroten], [Beta_cryptoxanthin], [Lycopen], [LuteinVsZeaxanthin], [Purin], [Total_isoflavone], [Daidzein], [Genistein], [Glycetin], [Total_saturated_fatty_acid], [Palmitic_C16_0], [Margaric_C17_0], [Stearic_C18_0], [Arachidic_C20_0], [Behenic_C22_0], [Lignoceric_C24_0], [TotalMonounsaturatedFattyAcid], [Myristoleic_C14_1], [Palmitoleic_C16_1], [Oleic_C18_1], [TotalPolyunsaturatedFattyAcid], [Linoleic_C18_2_n6], [Linolenic_C18_2_n3], [Arachidonic_C20_4], [Eicosapentaenoic_C20_5_n3], [Docosahexaenoic_C22_6_n3], [TotalTransFattyAcid], [Cholesterol], [Phytosterol], [Lysin], [Methionin], [Tryptophan], [Phenylalanin], [Threonin], [Valin], [Leucin], [Isoleucin], [Arginin], [Histidin], [Cystin], [Tyrosin], [Alanin], [Acid_aspartic], [Acid_glutamic], [Glycin], [Prolin], [Serin]) VALUES (4, NULL, N'Rau muống', N'Rau xanh giàu chất xơ và vitamin.', N'https://example.com/rau-muong.jpg', 3, 19, 91.2, 2.6, 0.3, 3.5, 2.1, 0, 0, 0, 0, 0, 0, 0, 0, 60, 0.5, 30, 0.05, 40, 260, 70, 0.4, 0.03, 0.01, 0, 0.02, 0.03, 0.09, 0.04, 0.02, 0, 0.01, 500, 0, 0.4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0.3, 0.1, 0, 0.2, 0.01, 0, 0.4, 0, 0.02, 0.4, 0.05, 0, 0, 0, 0, 0, 0.05, 0, 0.09, 0.05, 0, 0.03, 0.02, 0.1, 0.2, 0.2, 0.2, 0.1, 0.1, 0.3, 0.1, 0.1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
INSERT [FoodData].[IngredientDetails100g] ([Id], [KeyNoteId], [Name], [Describe], [URLImage], [TypeOfCalculationId], [Energy], [Water], [Protein], [Fat], [Carbohydrate], [Fiber], [Ash], [Sugar], [Galactose], [Maltose], [Lactose], [Fructose], [Glucose], [Sucrose], [Calcium], [Iron], [Magnesium], [Manganese], [Phosphorous], [Potassium], [Sodium], [Zinc], [Copper], [Selenium], [Vitamin_C], [Vitamin_B1], [Vitamin_B2], [Vitamin_PP], [Vitamin_B5], [Vitamin_B6], [Folat], [Vitamin_B9], [Vitamin_H], [Vitamin_B12], [Vitamin_A], [Vitamin_D], [Vitamin_E], [Vitamin_K], [Beta_caroten], [Alpha_caroten], [Beta_cryptoxanthin], [Lycopen], [LuteinVsZeaxanthin], [Purin], [Total_isoflavone], [Daidzein], [Genistein], [Glycetin], [Total_saturated_fatty_acid], [Palmitic_C16_0], [Margaric_C17_0], [Stearic_C18_0], [Arachidic_C20_0], [Behenic_C22_0], [Lignoceric_C24_0], [TotalMonounsaturatedFattyAcid], [Myristoleic_C14_1], [Palmitoleic_C16_1], [Oleic_C18_1], [TotalPolyunsaturatedFattyAcid], [Linoleic_C18_2_n6], [Linolenic_C18_2_n3], [Arachidonic_C20_4], [Eicosapentaenoic_C20_5_n3], [Docosahexaenoic_C22_6_n3], [TotalTransFattyAcid], [Cholesterol], [Phytosterol], [Lysin], [Methionin], [Tryptophan], [Phenylalanin], [Threonin], [Valin], [Leucin], [Isoleucin], [Arginin], [Histidin], [Cystin], [Tyrosin], [Alanin], [Acid_aspartic], [Acid_glutamic], [Glycin], [Prolin], [Serin]) VALUES (6, 1, N'Xúc xích Rans', N'Nguyên liệu chế biến từ thịt, giàu năng lượng.', N'https://example.com/xuc-xich.jpg', 4, 150, 52, 10, 27, 1.5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 200, 1, 30, 0.04, 120, 250, 900, 0.3, 0.05, 0.02, 0, 0.03, 0.02, 0.06, 0.03, 0.02, 0, 0.03, 1200, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 6.7, 3.2, 0, 1.5, 0.02, 0, 10, 0, 0.05, 8, 0.6, 0, 0, 0, 0, 0, 0.6, 0, 1, 0.9, 0, 0.3, 0.2, 0.7, 1.3, 1, 0.9, 0.8, 0.6, 1.3, 0.5, 0.7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
INSERT [FoodData].[IngredientDetails100g] ([Id], [KeyNoteId], [Name], [Describe], [URLImage], [TypeOfCalculationId], [Energy], [Water], [Protein], [Fat], [Carbohydrate], [Fiber], [Ash], [Sugar], [Galactose], [Maltose], [Lactose], [Fructose], [Glucose], [Sucrose], [Calcium], [Iron], [Magnesium], [Manganese], [Phosphorous], [Potassium], [Sodium], [Zinc], [Copper], [Selenium], [Vitamin_C], [Vitamin_B1], [Vitamin_B2], [Vitamin_PP], [Vitamin_B5], [Vitamin_B6], [Folat], [Vitamin_B9], [Vitamin_H], [Vitamin_B12], [Vitamin_A], [Vitamin_D], [Vitamin_E], [Vitamin_K], [Beta_caroten], [Alpha_caroten], [Beta_cryptoxanthin], [Lycopen], [LuteinVsZeaxanthin], [Purin], [Total_isoflavone], [Daidzein], [Genistein], [Glycetin], [Total_saturated_fatty_acid], [Palmitic_C16_0], [Margaric_C17_0], [Stearic_C18_0], [Arachidic_C20_0], [Behenic_C22_0], [Lignoceric_C24_0], [TotalMonounsaturatedFattyAcid], [Myristoleic_C14_1], [Palmitoleic_C16_1], [Oleic_C18_1], [TotalPolyunsaturatedFattyAcid], [Linoleic_C18_2_n6], [Linolenic_C18_2_n3], [Arachidonic_C20_4], [Eicosapentaenoic_C20_5_n3], [Docosahexaenoic_C22_6_n3], [TotalTransFattyAcid], [Cholesterol], [Phytosterol], [Lysin], [Methionin], [Tryptophan], [Phenylalanin], [Threonin], [Valin], [Leucin], [Isoleucin], [Arginin], [Histidin], [Cystin], [Tyrosin], [Alanin], [Acid_aspartic], [Acid_glutamic], [Glycin], [Prolin], [Serin]) VALUES (11, NULL, N'Cần tây', N'Rau xanh, giàu chất xơ và vitamin.', N'https://example.com/can-tay.jpg', 5, 16, 94, 0.7, 0.2, 3, 1.8, 0.1, 0, 0, 0, 0, 0, 0, 0, 40, 0.5, 11, 0.03, 26, 260, 80, 0.2, 0.03, 0.01, 0, 0.02, 0.02, 0.07, 0.03, 0.02, 0, 0.01, 450, 0, 0.3, 0, 0, 0, 0, 0, 0, 0, 0, 0.2, 0.1, 0, 0.15, 0.01, 0, 0.3, 0, 0.02, 0.3, 0.04, 0, 0, 0, 0, 0, 0.05, 0, 0.08, 0.05, 0, 0.03, 0.02, 0.1, 0.2, 0.1, 0.1, 0.1, 0.1, 0.2, 0.1, 0.1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
INSERT [FoodData].[IngredientDetails100g] ([Id], [KeyNoteId], [Name], [Describe], [URLImage], [TypeOfCalculationId], [Energy], [Water], [Protein], [Fat], [Carbohydrate], [Fiber], [Ash], [Sugar], [Galactose], [Maltose], [Lactose], [Fructose], [Glucose], [Sucrose], [Calcium], [Iron], [Magnesium], [Manganese], [Phosphorous], [Potassium], [Sodium], [Zinc], [Copper], [Selenium], [Vitamin_C], [Vitamin_B1], [Vitamin_B2], [Vitamin_PP], [Vitamin_B5], [Vitamin_B6], [Folat], [Vitamin_B9], [Vitamin_H], [Vitamin_B12], [Vitamin_A], [Vitamin_D], [Vitamin_E], [Vitamin_K], [Beta_caroten], [Alpha_caroten], [Beta_cryptoxanthin], [Lycopen], [LuteinVsZeaxanthin], [Purin], [Total_isoflavone], [Daidzein], [Genistein], [Glycetin], [Total_saturated_fatty_acid], [Palmitic_C16_0], [Margaric_C17_0], [Stearic_C18_0], [Arachidic_C20_0], [Behenic_C22_0], [Lignoceric_C24_0], [TotalMonounsaturatedFattyAcid], [Myristoleic_C14_1], [Palmitoleic_C16_1], [Oleic_C18_1], [TotalPolyunsaturatedFattyAcid], [Linoleic_C18_2_n6], [Linolenic_C18_2_n3], [Arachidonic_C20_4], [Eicosapentaenoic_C20_5_n3], [Docosahexaenoic_C22_6_n3], [TotalTransFattyAcid], [Cholesterol], [Phytosterol], [Lysin], [Methionin], [Tryptophan], [Phenylalanin], [Threonin], [Valin], [Leucin], [Isoleucin], [Arginin], [Histidin], [Cystin], [Tyrosin], [Alanin], [Acid_aspartic], [Acid_glutamic], [Glycin], [Prolin], [Serin]) VALUES (12, NULL, N'Tỏi', N'Nguyên liệu thường dùng làm gia vị, giàu allicin.', N'https://example.com/toi.jpg', 5, 149, 58, 6.4, 0.5, 33, 2.1, 0, 0, 0, 0, 0, 0, 0, 0, 181, 1.7, 25, 0.2, 153, 401, 17, 0.6, 0.15, 0.02, 0, 0.02, 0.02, 0.08, 0.03, 0.04, 0, 0.03, 0, 0, 0.7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1.4, 0.7, 0, 0.5, 0.03, 0, 2.3, 0, 0.04, 1.8, 0.6, 0, 0, 0, 0, 0, 0.3, 0, 0.4, 0.2, 0, 0.2, 0.1, 0.5, 0.8, 0.6, 0.5, 0.4, 0.3, 0.8, 0.3, 0.4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
INSERT [FoodData].[IngredientDetails100g] ([Id], [KeyNoteId], [Name], [Describe], [URLImage], [TypeOfCalculationId], [Energy], [Water], [Protein], [Fat], [Carbohydrate], [Fiber], [Ash], [Sugar], [Galactose], [Maltose], [Lactose], [Fructose], [Glucose], [Sucrose], [Calcium], [Iron], [Magnesium], [Manganese], [Phosphorous], [Potassium], [Sodium], [Zinc], [Copper], [Selenium], [Vitamin_C], [Vitamin_B1], [Vitamin_B2], [Vitamin_PP], [Vitamin_B5], [Vitamin_B6], [Folat], [Vitamin_B9], [Vitamin_H], [Vitamin_B12], [Vitamin_A], [Vitamin_D], [Vitamin_E], [Vitamin_K], [Beta_caroten], [Alpha_caroten], [Beta_cryptoxanthin], [Lycopen], [LuteinVsZeaxanthin], [Purin], [Total_isoflavone], [Daidzein], [Genistein], [Glycetin], [Total_saturated_fatty_acid], [Palmitic_C16_0], [Margaric_C17_0], [Stearic_C18_0], [Arachidic_C20_0], [Behenic_C22_0], [Lignoceric_C24_0], [TotalMonounsaturatedFattyAcid], [Myristoleic_C14_1], [Palmitoleic_C16_1], [Oleic_C18_1], [TotalPolyunsaturatedFattyAcid], [Linoleic_C18_2_n6], [Linolenic_C18_2_n3], [Arachidonic_C20_4], [Eicosapentaenoic_C20_5_n3], [Docosahexaenoic_C22_6_n3], [TotalTransFattyAcid], [Cholesterol], [Phytosterol], [Lysin], [Methionin], [Tryptophan], [Phenylalanin], [Threonin], [Valin], [Leucin], [Isoleucin], [Arginin], [Histidin], [Cystin], [Tyrosin], [Alanin], [Acid_aspartic], [Acid_glutamic], [Glycin], [Prolin], [Serin]) VALUES (13, NULL, N'Hành lá', N'Nguyên liệu làm gia vị, giàu vitamin C và chất xơ.', N'https://example.com/hanh-la.jpg', 5, 32, 92.5, 1.3, 0.2, 7.3, 2.6, 0, 0, 0, 0, 0, 0, 0, 0, 72, 0.3, 20, 0.03, 30, 270, 50, 0.2, 0.03, 0.02, 0, 0.02, 0.02, 0.06, 0.02, 0.02, 0, 0.01, 380, 0, 0.3, 0, 0, 0, 0, 0, 0, 0, 0, 0.4, 0.2, 0, 0.1, 0.01, 0, 0.5, 0, 0.02, 0.5, 0.04, 0, 0, 0, 0, 0, 0.05, 0, 0.08, 0.05, 0, 0.03, 0.02, 0.1, 0.2, 0.2, 0.1, 0.1, 0.1, 0.2, 0.1, 0.1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
INSERT [FoodData].[IngredientDetails100g] ([Id], [KeyNoteId], [Name], [Describe], [URLImage], [TypeOfCalculationId], [Energy], [Water], [Protein], [Fat], [Carbohydrate], [Fiber], [Ash], [Sugar], [Galactose], [Maltose], [Lactose], [Fructose], [Glucose], [Sucrose], [Calcium], [Iron], [Magnesium], [Manganese], [Phosphorous], [Potassium], [Sodium], [Zinc], [Copper], [Selenium], [Vitamin_C], [Vitamin_B1], [Vitamin_B2], [Vitamin_PP], [Vitamin_B5], [Vitamin_B6], [Folat], [Vitamin_B9], [Vitamin_H], [Vitamin_B12], [Vitamin_A], [Vitamin_D], [Vitamin_E], [Vitamin_K], [Beta_caroten], [Alpha_caroten], [Beta_cryptoxanthin], [Lycopen], [LuteinVsZeaxanthin], [Purin], [Total_isoflavone], [Daidzein], [Genistein], [Glycetin], [Total_saturated_fatty_acid], [Palmitic_C16_0], [Margaric_C17_0], [Stearic_C18_0], [Arachidic_C20_0], [Behenic_C22_0], [Lignoceric_C24_0], [TotalMonounsaturatedFattyAcid], [Myristoleic_C14_1], [Palmitoleic_C16_1], [Oleic_C18_1], [TotalPolyunsaturatedFattyAcid], [Linoleic_C18_2_n6], [Linolenic_C18_2_n3], [Arachidonic_C20_4], [Eicosapentaenoic_C20_5_n3], [Docosahexaenoic_C22_6_n3], [TotalTransFattyAcid], [Cholesterol], [Phytosterol], [Lysin], [Methionin], [Tryptophan], [Phenylalanin], [Threonin], [Valin], [Leucin], [Isoleucin], [Arginin], [Histidin], [Cystin], [Tyrosin], [Alanin], [Acid_aspartic], [Acid_glutamic], [Glycin], [Prolin], [Serin]) VALUES (14, NULL, N'Đậu phụ', N'Nguyên liệu làm từ đậu nành, giàu protein.', N'https://example.com/dau-phu.jpg', 6, 76, 84, 8, 4.8, 1.6, 0, 0, 0, 0, 0, 0, 0, 0, 0, 350, 1.5, 60, 0.02, 200, 150, 7, 1, 0.05, 0.02, 0, 0.02, 0.02, 0.08, 0.02, 0.03, 0, 0.02, 0, 0, 0.8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5.4, 2.5, 0, 1.5, 0.03, 0, 7, 0, 0.06, 5.8, 0.4, 0, 0, 0, 0, 0, 0.5, 0, 1, 0.6, 0, 0.2, 0.1, 0.7, 1.2, 1, 0.8, 0.6, 0.4, 1, 0.5, 0.6, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
INSERT [FoodData].[IngredientDetails100g] ([Id], [KeyNoteId], [Name], [Describe], [URLImage], [TypeOfCalculationId], [Energy], [Water], [Protein], [Fat], [Carbohydrate], [Fiber], [Ash], [Sugar], [Galactose], [Maltose], [Lactose], [Fructose], [Glucose], [Sucrose], [Calcium], [Iron], [Magnesium], [Manganese], [Phosphorous], [Potassium], [Sodium], [Zinc], [Copper], [Selenium], [Vitamin_C], [Vitamin_B1], [Vitamin_B2], [Vitamin_PP], [Vitamin_B5], [Vitamin_B6], [Folat], [Vitamin_B9], [Vitamin_H], [Vitamin_B12], [Vitamin_A], [Vitamin_D], [Vitamin_E], [Vitamin_K], [Beta_caroten], [Alpha_caroten], [Beta_cryptoxanthin], [Lycopen], [LuteinVsZeaxanthin], [Purin], [Total_isoflavone], [Daidzein], [Genistein], [Glycetin], [Total_saturated_fatty_acid], [Palmitic_C16_0], [Margaric_C17_0], [Stearic_C18_0], [Arachidic_C20_0], [Behenic_C22_0], [Lignoceric_C24_0], [TotalMonounsaturatedFattyAcid], [Myristoleic_C14_1], [Palmitoleic_C16_1], [Oleic_C18_1], [TotalPolyunsaturatedFattyAcid], [Linoleic_C18_2_n6], [Linolenic_C18_2_n3], [Arachidonic_C20_4], [Eicosapentaenoic_C20_5_n3], [Docosahexaenoic_C22_6_n3], [TotalTransFattyAcid], [Cholesterol], [Phytosterol], [Lysin], [Methionin], [Tryptophan], [Phenylalanin], [Threonin], [Valin], [Leucin], [Isoleucin], [Arginin], [Histidin], [Cystin], [Tyrosin], [Alanin], [Acid_aspartic], [Acid_glutamic], [Glycin], [Prolin], [Serin]) VALUES (15, 1, N'Trứng2', N'Nguyên liệu phổ biến trong nấu ăn, giàu protein và chất béo.', N'https://example.com/trung.jpg', 1, 155, 74, 12.6, 10.6, 1.1, 0, 0.5, 0, 0, 0, 0, 0, 0, 0, 50, 1.2, 10, 0.03, 172, 126, 140, 0.7, 0.02, 0.2, 0, 0.05, 0.03, 0.09, 0.05, 0.04, 0, 0.03, 640, 0, 1.05, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3.3, 1.5, 0, 1.3, 0.03, 0, 5.7, 0, 0.03, 4.6, 0.8, 0, 0, 0, 0, 0, 0.4, 0, 0.6, 0.7, 0, 0.2, 0.1, 0.7, 1.2, 0.9, 0.8, 0.6, 0.5, 1.1, 0.4, 0.5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
INSERT [FoodData].[IngredientDetails100g] ([Id], [KeyNoteId], [Name], [Describe], [URLImage], [TypeOfCalculationId], [Energy], [Water], [Protein], [Fat], [Carbohydrate], [Fiber], [Ash], [Sugar], [Galactose], [Maltose], [Lactose], [Fructose], [Glucose], [Sucrose], [Calcium], [Iron], [Magnesium], [Manganese], [Phosphorous], [Potassium], [Sodium], [Zinc], [Copper], [Selenium], [Vitamin_C], [Vitamin_B1], [Vitamin_B2], [Vitamin_PP], [Vitamin_B5], [Vitamin_B6], [Folat], [Vitamin_B9], [Vitamin_H], [Vitamin_B12], [Vitamin_A], [Vitamin_D], [Vitamin_E], [Vitamin_K], [Beta_caroten], [Alpha_caroten], [Beta_cryptoxanthin], [Lycopen], [LuteinVsZeaxanthin], [Purin], [Total_isoflavone], [Daidzein], [Genistein], [Glycetin], [Total_saturated_fatty_acid], [Palmitic_C16_0], [Margaric_C17_0], [Stearic_C18_0], [Arachidic_C20_0], [Behenic_C22_0], [Lignoceric_C24_0], [TotalMonounsaturatedFattyAcid], [Myristoleic_C14_1], [Palmitoleic_C16_1], [Oleic_C18_1], [TotalPolyunsaturatedFattyAcid], [Linoleic_C18_2_n6], [Linolenic_C18_2_n3], [Arachidonic_C20_4], [Eicosapentaenoic_C20_5_n3], [Docosahexaenoic_C22_6_n3], [TotalTransFattyAcid], [Cholesterol], [Phytosterol], [Lysin], [Methionin], [Tryptophan], [Phenylalanin], [Threonin], [Valin], [Leucin], [Isoleucin], [Arginin], [Histidin], [Cystin], [Tyrosin], [Alanin], [Acid_aspartic], [Acid_glutamic], [Glycin], [Prolin], [Serin]) VALUES (16, 1, N'Trứng', N'Nguyên liệu phổ biến trong nấu ăn, giàu protein và chất béo.', N'https://example.com/trung.jpg', 1, 155, 74, 12.6, 10.6, 1.1, 0, 0.5, 0, 0, 0, 0, 0, 0, 0, 50, 1.2, 10, 0.03, 172, 126, 140, 0.7, 0.02, 0.2, 0, 0.05, 0.03, 0.09, 0.05, 0.04, 0, 0.03, 640, 0, 1.05, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3.3, 1.5, 0, 1.3, 0.03, 0, 5.7, 0, 0.03, 4.6, 0.8, 0, 0, 0, 0, 0, 0.4, 0, 0.6, 0.7, 0, 0.2, 0.1, 0.7, 1.2, 0.9, 0.8, 0.6, 0.5, 1.1, 0.4, 0.5, 0, 0, 0, 0, 0, 0, 0, 0, 0)
INSERT [FoodData].[IngredientDetails100g] ([Id], [KeyNoteId], [Name], [Describe], [URLImage], [TypeOfCalculationId], [Energy], [Water], [Protein], [Fat], [Carbohydrate], [Fiber], [Ash], [Sugar], [Galactose], [Maltose], [Lactose], [Fructose], [Glucose], [Sucrose], [Calcium], [Iron], [Magnesium], [Manganese], [Phosphorous], [Potassium], [Sodium], [Zinc], [Copper], [Selenium], [Vitamin_C], [Vitamin_B1], [Vitamin_B2], [Vitamin_PP], [Vitamin_B5], [Vitamin_B6], [Folat], [Vitamin_B9], [Vitamin_H], [Vitamin_B12], [Vitamin_A], [Vitamin_D], [Vitamin_E], [Vitamin_K], [Beta_caroten], [Alpha_caroten], [Beta_cryptoxanthin], [Lycopen], [LuteinVsZeaxanthin], [Purin], [Total_isoflavone], [Daidzein], [Genistein], [Glycetin], [Total_saturated_fatty_acid], [Palmitic_C16_0], [Margaric_C17_0], [Stearic_C18_0], [Arachidic_C20_0], [Behenic_C22_0], [Lignoceric_C24_0], [TotalMonounsaturatedFattyAcid], [Myristoleic_C14_1], [Palmitoleic_C16_1], [Oleic_C18_1], [TotalPolyunsaturatedFattyAcid], [Linoleic_C18_2_n6], [Linolenic_C18_2_n3], [Arachidonic_C20_4], [Eicosapentaenoic_C20_5_n3], [Docosahexaenoic_C22_6_n3], [TotalTransFattyAcid], [Cholesterol], [Phytosterol], [Lysin], [Methionin], [Tryptophan], [Phenylalanin], [Threonin], [Valin], [Leucin], [Isoleucin], [Arginin], [Histidin], [Cystin], [Tyrosin], [Alanin], [Acid_aspartic], [Acid_glutamic], [Glycin], [Prolin], [Serin]) VALUES (1015, 3, N'Đậu phụ', N'Nguyên liệu làm từ đậu nành, giàu protein.', N'https://example.com/dau-phu.jpg', 1, 76, 84, 8, 4.8, 1.6, 0, 0, 0, 0, 0, 0, 0, 0, 0, 350, 1.5, 60, 0.02, 200, 150, 7, 1, 0.05, 0.02, 0, 0.02, 0.02, 0.08, 0.02, 0.03, 0, 0.02, 0, 0, 0.8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5.4, 2.5, 0, 1.5, 0.03, 0, 7, 0, 0.06, 5.8, 0.4, 0, 0, 0, 0, 0, 0.5, 0, 1, 0.6, 0, 0.2, 0.1, 0.7, 1.2, 1, 0.8, 0.6, 0.4, 1, 0.5, 0.6, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
INSERT [FoodData].[IngredientDetails100g] ([Id], [KeyNoteId], [Name], [Describe], [URLImage], [TypeOfCalculationId], [Energy], [Water], [Protein], [Fat], [Carbohydrate], [Fiber], [Ash], [Sugar], [Galactose], [Maltose], [Lactose], [Fructose], [Glucose], [Sucrose], [Calcium], [Iron], [Magnesium], [Manganese], [Phosphorous], [Potassium], [Sodium], [Zinc], [Copper], [Selenium], [Vitamin_C], [Vitamin_B1], [Vitamin_B2], [Vitamin_PP], [Vitamin_B5], [Vitamin_B6], [Folat], [Vitamin_B9], [Vitamin_H], [Vitamin_B12], [Vitamin_A], [Vitamin_D], [Vitamin_E], [Vitamin_K], [Beta_caroten], [Alpha_caroten], [Beta_cryptoxanthin], [Lycopen], [LuteinVsZeaxanthin], [Purin], [Total_isoflavone], [Daidzein], [Genistein], [Glycetin], [Total_saturated_fatty_acid], [Palmitic_C16_0], [Margaric_C17_0], [Stearic_C18_0], [Arachidic_C20_0], [Behenic_C22_0], [Lignoceric_C24_0], [TotalMonounsaturatedFattyAcid], [Myristoleic_C14_1], [Palmitoleic_C16_1], [Oleic_C18_1], [TotalPolyunsaturatedFattyAcid], [Linoleic_C18_2_n6], [Linolenic_C18_2_n3], [Arachidonic_C20_4], [Eicosapentaenoic_C20_5_n3], [Docosahexaenoic_C22_6_n3], [TotalTransFattyAcid], [Cholesterol], [Phytosterol], [Lysin], [Methionin], [Tryptophan], [Phenylalanin], [Threonin], [Valin], [Leucin], [Isoleucin], [Arginin], [Histidin], [Cystin], [Tyrosin], [Alanin], [Acid_aspartic], [Acid_glutamic], [Glycin], [Prolin], [Serin]) VALUES (1017, 1, N'Gạo nếp cái', N'Gạo nếp cái hoa vàng là loại gạo nếp truyền thống của Việt Nam, nổi bật với hạt tròn, dẻo, thơm, thường dùng để nấu xôi, làm bánh và ủ rượu.', N'https://example.com/Gao-nep-cai.jpg', 9, 0, 0, 0, 0, 0, 0.032, 0.001, 0.017, 0.001, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
INSERT [FoodData].[IngredientDetails100g] ([Id], [KeyNoteId], [Name], [Describe], [URLImage], [TypeOfCalculationId], [Energy], [Water], [Protein], [Fat], [Carbohydrate], [Fiber], [Ash], [Sugar], [Galactose], [Maltose], [Lactose], [Fructose], [Glucose], [Sucrose], [Calcium], [Iron], [Magnesium], [Manganese], [Phosphorous], [Potassium], [Sodium], [Zinc], [Copper], [Selenium], [Vitamin_C], [Vitamin_B1], [Vitamin_B2], [Vitamin_PP], [Vitamin_B5], [Vitamin_B6], [Folat], [Vitamin_B9], [Vitamin_H], [Vitamin_B12], [Vitamin_A], [Vitamin_D], [Vitamin_E], [Vitamin_K], [Beta_caroten], [Alpha_caroten], [Beta_cryptoxanthin], [Lycopen], [LuteinVsZeaxanthin], [Purin], [Total_isoflavone], [Daidzein], [Genistein], [Glycetin], [Total_saturated_fatty_acid], [Palmitic_C16_0], [Margaric_C17_0], [Stearic_C18_0], [Arachidic_C20_0], [Behenic_C22_0], [Lignoceric_C24_0], [TotalMonounsaturatedFattyAcid], [Myristoleic_C14_1], [Palmitoleic_C16_1], [Oleic_C18_1], [TotalPolyunsaturatedFattyAcid], [Linoleic_C18_2_n6], [Linolenic_C18_2_n3], [Arachidonic_C20_4], [Eicosapentaenoic_C20_5_n3], [Docosahexaenoic_C22_6_n3], [TotalTransFattyAcid], [Cholesterol], [Phytosterol], [Lysin], [Methionin], [Tryptophan], [Phenylalanin], [Threonin], [Valin], [Leucin], [Isoleucin], [Arginin], [Histidin], [Cystin], [Tyrosin], [Alanin], [Acid_aspartic], [Acid_glutamic], [Glycin], [Prolin], [Serin]) VALUES (1018, 1, N'Gạo nếp cái', N'Gạo nếp cái hoa vàng là loại gạo nếp truyền thống của Việt Nam, nổi bật với hạt tròn, dẻo, thơm, thường dùng để nấu xôi, làm bánh và ủ rượu.', N'https://example.com/Gao-nep-cai.jpg', 9, 0, 0, 0, 0, 0, 0.032, 0.001, 0.017, 0.001, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
INSERT [FoodData].[IngredientDetails100g] ([Id], [KeyNoteId], [Name], [Describe], [URLImage], [TypeOfCalculationId], [Energy], [Water], [Protein], [Fat], [Carbohydrate], [Fiber], [Ash], [Sugar], [Galactose], [Maltose], [Lactose], [Fructose], [Glucose], [Sucrose], [Calcium], [Iron], [Magnesium], [Manganese], [Phosphorous], [Potassium], [Sodium], [Zinc], [Copper], [Selenium], [Vitamin_C], [Vitamin_B1], [Vitamin_B2], [Vitamin_PP], [Vitamin_B5], [Vitamin_B6], [Folat], [Vitamin_B9], [Vitamin_H], [Vitamin_B12], [Vitamin_A], [Vitamin_D], [Vitamin_E], [Vitamin_K], [Beta_caroten], [Alpha_caroten], [Beta_cryptoxanthin], [Lycopen], [LuteinVsZeaxanthin], [Purin], [Total_isoflavone], [Daidzein], [Genistein], [Glycetin], [Total_saturated_fatty_acid], [Palmitic_C16_0], [Margaric_C17_0], [Stearic_C18_0], [Arachidic_C20_0], [Behenic_C22_0], [Lignoceric_C24_0], [TotalMonounsaturatedFattyAcid], [Myristoleic_C14_1], [Palmitoleic_C16_1], [Oleic_C18_1], [TotalPolyunsaturatedFattyAcid], [Linoleic_C18_2_n6], [Linolenic_C18_2_n3], [Arachidonic_C20_4], [Eicosapentaenoic_C20_5_n3], [Docosahexaenoic_C22_6_n3], [TotalTransFattyAcid], [Cholesterol], [Phytosterol], [Lysin], [Methionin], [Tryptophan], [Phenylalanin], [Threonin], [Valin], [Leucin], [Isoleucin], [Arginin], [Histidin], [Cystin], [Tyrosin], [Alanin], [Acid_aspartic], [Acid_glutamic], [Glycin], [Prolin], [Serin]) VALUES (1019, 1, N'Gạo nếp cái', N'Gạo nếp cái hoa vàng là loại gạo nếp truyền thống của Việt Nam, nổi bật với hạt tròn, dẻo, thơm, thường dùng để nấu xôi, làm bánh và ủ rượu.', N'https://example.com/Gao-nep-cai.jpg', 9, 0, 0, 0, 0, 0, 0.032, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
INSERT [FoodData].[IngredientDetails100g] ([Id], [KeyNoteId], [Name], [Describe], [URLImage], [TypeOfCalculationId], [Energy], [Water], [Protein], [Fat], [Carbohydrate], [Fiber], [Ash], [Sugar], [Galactose], [Maltose], [Lactose], [Fructose], [Glucose], [Sucrose], [Calcium], [Iron], [Magnesium], [Manganese], [Phosphorous], [Potassium], [Sodium], [Zinc], [Copper], [Selenium], [Vitamin_C], [Vitamin_B1], [Vitamin_B2], [Vitamin_PP], [Vitamin_B5], [Vitamin_B6], [Folat], [Vitamin_B9], [Vitamin_H], [Vitamin_B12], [Vitamin_A], [Vitamin_D], [Vitamin_E], [Vitamin_K], [Beta_caroten], [Alpha_caroten], [Beta_cryptoxanthin], [Lycopen], [LuteinVsZeaxanthin], [Purin], [Total_isoflavone], [Daidzein], [Genistein], [Glycetin], [Total_saturated_fatty_acid], [Palmitic_C16_0], [Margaric_C17_0], [Stearic_C18_0], [Arachidic_C20_0], [Behenic_C22_0], [Lignoceric_C24_0], [TotalMonounsaturatedFattyAcid], [Myristoleic_C14_1], [Palmitoleic_C16_1], [Oleic_C18_1], [TotalPolyunsaturatedFattyAcid], [Linoleic_C18_2_n6], [Linolenic_C18_2_n3], [Arachidonic_C20_4], [Eicosapentaenoic_C20_5_n3], [Docosahexaenoic_C22_6_n3], [TotalTransFattyAcid], [Cholesterol], [Phytosterol], [Lysin], [Methionin], [Tryptophan], [Phenylalanin], [Threonin], [Valin], [Leucin], [Isoleucin], [Arginin], [Histidin], [Cystin], [Tyrosin], [Alanin], [Acid_aspartic], [Acid_glutamic], [Glycin], [Prolin], [Serin]) VALUES (1020, 1, N'GẠO NẾP MÁY', N'Gạo nếp máy là loại gạo được xay xát từ hạt lúa nếp dùng để nấu xôi làm bánh và nhiều món ăn khác.', N'https://example.com/gao-nep-may.jpg', 9, 13, 346, 8.4, 1.6, 74.9, 0.5, 0.7, 0, 0, 0, 0, 0, 0, 0, 0.016, 0.0012, 0.017, 0.0011, 0.13, 0.282, 0.003, 0.0022, 0.00028, 1.51E-05, 0, 0.00016, 5.9999999999999995E-05, 0.0024, 0.00028399999999999996, 0.000107, 7E-06, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0.11, 0.09, 0, 0.01, 0, 0, 0, 0.2, 0, 0, 0.2, 0.2, 0.19, 0.01, 0, 0, 0, 0, 0, 0, 0.246, 0.16, 0.079, 0.364, 0.244, 0.416, 0.563, 0.294, 0.568, 0.16, 0.14, 0.228, 0.395, 0.64, 1.328, 0.31, 0.321, 0.358)
INSERT [FoodData].[IngredientDetails100g] ([Id], [KeyNoteId], [Name], [Describe], [URLImage], [TypeOfCalculationId], [Energy], [Water], [Protein], [Fat], [Carbohydrate], [Fiber], [Ash], [Sugar], [Galactose], [Maltose], [Lactose], [Fructose], [Glucose], [Sucrose], [Calcium], [Iron], [Magnesium], [Manganese], [Phosphorous], [Potassium], [Sodium], [Zinc], [Copper], [Selenium], [Vitamin_C], [Vitamin_B1], [Vitamin_B2], [Vitamin_PP], [Vitamin_B5], [Vitamin_B6], [Folat], [Vitamin_B9], [Vitamin_H], [Vitamin_B12], [Vitamin_A], [Vitamin_D], [Vitamin_E], [Vitamin_K], [Beta_caroten], [Alpha_caroten], [Beta_cryptoxanthin], [Lycopen], [LuteinVsZeaxanthin], [Purin], [Total_isoflavone], [Daidzein], [Genistein], [Glycetin], [Total_saturated_fatty_acid], [Palmitic_C16_0], [Margaric_C17_0], [Stearic_C18_0], [Arachidic_C20_0], [Behenic_C22_0], [Lignoceric_C24_0], [TotalMonounsaturatedFattyAcid], [Myristoleic_C14_1], [Palmitoleic_C16_1], [Oleic_C18_1], [TotalPolyunsaturatedFattyAcid], [Linoleic_C18_2_n6], [Linolenic_C18_2_n3], [Arachidonic_C20_4], [Eicosapentaenoic_C20_5_n3], [Docosahexaenoic_C22_6_n3], [TotalTransFattyAcid], [Cholesterol], [Phytosterol], [Lysin], [Methionin], [Tryptophan], [Phenylalanin], [Threonin], [Valin], [Leucin], [Isoleucin], [Arginin], [Histidin], [Cystin], [Tyrosin], [Alanin], [Acid_aspartic], [Acid_glutamic], [Glycin], [Prolin], [Serin]) VALUES (2015, 1, N'CẢI CÚC', N'Cải cúc là một loại rau thường dùng trong các món canh, xào, hoặc ăn sống.', N'https://example.com/cai-cuc.jpg', 3, 93, 14, 1.6, 0, 1.9, 2, 7, 0, 0, 0, 0, 0, 0, 0, 0.063, 0.0008, 0.032, 0.0007, 0.038, 0.219, 0.033, 0.00067, 0.00018, 0.0003, 0.027, 1E-05, 3E-05, 0.0002, 0.000221, 0.000176, 0.000177, 0, 0, 0, 0, 0, 0, 0, 0.001115, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
INSERT [FoodData].[IngredientDetails100g] ([Id], [KeyNoteId], [Name], [Describe], [URLImage], [TypeOfCalculationId], [Energy], [Water], [Protein], [Fat], [Carbohydrate], [Fiber], [Ash], [Sugar], [Galactose], [Maltose], [Lactose], [Fructose], [Glucose], [Sucrose], [Calcium], [Iron], [Magnesium], [Manganese], [Phosphorous], [Potassium], [Sodium], [Zinc], [Copper], [Selenium], [Vitamin_C], [Vitamin_B1], [Vitamin_B2], [Vitamin_PP], [Vitamin_B5], [Vitamin_B6], [Folat], [Vitamin_B9], [Vitamin_H], [Vitamin_B12], [Vitamin_A], [Vitamin_D], [Vitamin_E], [Vitamin_K], [Beta_caroten], [Alpha_caroten], [Beta_cryptoxanthin], [Lycopen], [LuteinVsZeaxanthin], [Purin], [Total_isoflavone], [Daidzein], [Genistein], [Glycetin], [Total_saturated_fatty_acid], [Palmitic_C16_0], [Margaric_C17_0], [Stearic_C18_0], [Arachidic_C20_0], [Behenic_C22_0], [Lignoceric_C24_0], [TotalMonounsaturatedFattyAcid], [Myristoleic_C14_1], [Palmitoleic_C16_1], [Oleic_C18_1], [TotalPolyunsaturatedFattyAcid], [Linoleic_C18_2_n6], [Linolenic_C18_2_n3], [Arachidonic_C20_4], [Eicosapentaenoic_C20_5_n3], [Docosahexaenoic_C22_6_n3], [TotalTransFattyAcid], [Cholesterol], [Phytosterol], [Lysin], [Methionin], [Tryptophan], [Phenylalanin], [Threonin], [Valin], [Leucin], [Isoleucin], [Arginin], [Histidin], [Cystin], [Tyrosin], [Alanin], [Acid_aspartic], [Acid_glutamic], [Glycin], [Prolin], [Serin]) VALUES (2016, 1, N'CẢI XOONG', N'Cải xoong là loại rau thường dùng trong các món canh, salad hoặc ăn sống, giàu dinh dưỡng và rất phổ biến.', N'https://example.com/cai-xoong.jpg', 3, 93, 15, 2.1, 0.1, 1.3, 2, 0.8, 0.2, 0, 0, 0, 0, 0, 0, 0.069, 0.0016, 0.021, 0.00038, 0.028, 0.211, 0.085, 0.0008, 0.0002, 9E-07, 0.025, 8E-05, 0.00026, 0.001, 0.00031, 0.000129, 9E-06, 0, 0, 0, 0, 0, 0.001, 0.00025, 2.82, 0, 0, 0, 0.005767, 0.028, 0, 0, 0, 0, 0.03, 0.02, 0, 0, 0, 0, 0, 0.01, 0, 0, 0.01, 0.04, 0.01, 0.02, 0, 0, 0, 0, 0, 0, 0.134, 0.02, 0.03, 0.114, 0.133, 0.137, 0.166, 0.093, 0.15, 0.04, 0.007, 0.063, 0.137, 0.187, 0.19, 0.112, 0.096, 0.06)
INSERT [FoodData].[IngredientDetails100g] ([Id], [KeyNoteId], [Name], [Describe], [URLImage], [TypeOfCalculationId], [Energy], [Water], [Protein], [Fat], [Carbohydrate], [Fiber], [Ash], [Sugar], [Galactose], [Maltose], [Lactose], [Fructose], [Glucose], [Sucrose], [Calcium], [Iron], [Magnesium], [Manganese], [Phosphorous], [Potassium], [Sodium], [Zinc], [Copper], [Selenium], [Vitamin_C], [Vitamin_B1], [Vitamin_B2], [Vitamin_PP], [Vitamin_B5], [Vitamin_B6], [Folat], [Vitamin_B9], [Vitamin_H], [Vitamin_B12], [Vitamin_A], [Vitamin_D], [Vitamin_E], [Vitamin_K], [Beta_caroten], [Alpha_caroten], [Beta_cryptoxanthin], [Lycopen], [LuteinVsZeaxanthin], [Purin], [Total_isoflavone], [Daidzein], [Genistein], [Glycetin], [Total_saturated_fatty_acid], [Palmitic_C16_0], [Margaric_C17_0], [Stearic_C18_0], [Arachidic_C20_0], [Behenic_C22_0], [Lignoceric_C24_0], [TotalMonounsaturatedFattyAcid], [Myristoleic_C14_1], [Palmitoleic_C16_1], [Oleic_C18_1], [TotalPolyunsaturatedFattyAcid], [Linoleic_C18_2_n6], [Linolenic_C18_2_n3], [Arachidonic_C20_4], [Eicosapentaenoic_C20_5_n3], [Docosahexaenoic_C22_6_n3], [TotalTransFattyAcid], [Cholesterol], [Phytosterol], [Lysin], [Methionin], [Tryptophan], [Phenylalanin], [Threonin], [Valin], [Leucin], [Isoleucin], [Arginin], [Histidin], [Cystin], [Tyrosin], [Alanin], [Acid_aspartic], [Acid_glutamic], [Glycin], [Prolin], [Serin]) VALUES (2017, 1, N'CẢI XANH', N'Cải xanh là loại rau thường dùng trong các món xào, luộc hoặc nấu canh, rất phổ biến trong bữa ăn hàng ngày.', N'https://example.com/cai-xanh.jpg', 3, 93, 16, 1.7, 0.2, 1.9, 1.8, 0.6, 1.6, 0, 0, 0, 0, 0, 0, 0.089, 0.0019, 0.023, 0.00032, 0.014, 0.221, 0.029, 0.0009, 0.00012, 9E-07, 0.051, 7E-05, 0.0001, 0.0008, 0.00021, 0.00018, 0.000187, 0, 0, 0, 0, 0, 0.00201, 0.0004973, 0.0063, 0, 0, 0, 0.0099, 0, 0, 0, 0, 0, 0.01, 0.01, 0, 0, 0, 0, 0, 0.09, 0, 0, 0.02, 0.04, 0.02, 0.02, 0, 0, 0, 0, 0, 0, 0.123, 0.025, 0.03, 0.072, 0.072, 0.105, 0.083, 0.098, 0.197, 0.048, 0.04, 0.143, 0, 0, 0, 0, 0, 0)
INSERT [FoodData].[IngredientDetails100g] ([Id], [KeyNoteId], [Name], [Describe], [URLImage], [TypeOfCalculationId], [Energy], [Water], [Protein], [Fat], [Carbohydrate], [Fiber], [Ash], [Sugar], [Galactose], [Maltose], [Lactose], [Fructose], [Glucose], [Sucrose], [Calcium], [Iron], [Magnesium], [Manganese], [Phosphorous], [Potassium], [Sodium], [Zinc], [Copper], [Selenium], [Vitamin_C], [Vitamin_B1], [Vitamin_B2], [Vitamin_PP], [Vitamin_B5], [Vitamin_B6], [Folat], [Vitamin_B9], [Vitamin_H], [Vitamin_B12], [Vitamin_A], [Vitamin_D], [Vitamin_E], [Vitamin_K], [Beta_caroten], [Alpha_caroten], [Beta_cryptoxanthin], [Lycopen], [LuteinVsZeaxanthin], [Purin], [Total_isoflavone], [Daidzein], [Genistein], [Glycetin], [Total_saturated_fatty_acid], [Palmitic_C16_0], [Margaric_C17_0], [Stearic_C18_0], [Arachidic_C20_0], [Behenic_C22_0], [Lignoceric_C24_0], [TotalMonounsaturatedFattyAcid], [Myristoleic_C14_1], [Palmitoleic_C16_1], [Oleic_C18_1], [TotalPolyunsaturatedFattyAcid], [Linoleic_C18_2_n6], [Linolenic_C18_2_n3], [Arachidonic_C20_4], [Eicosapentaenoic_C20_5_n3], [Docosahexaenoic_C22_6_n3], [TotalTransFattyAcid], [Cholesterol], [Phytosterol], [Lysin], [Methionin], [Tryptophan], [Phenylalanin], [Threonin], [Valin], [Leucin], [Isoleucin], [Arginin], [Histidin], [Cystin], [Tyrosin], [Alanin], [Acid_aspartic], [Acid_glutamic], [Glycin], [Prolin], [Serin]) VALUES (2018, 1, N'BẦU', N'Bầu là loại quả thường dùng trong các món canh, luộc hoặc xào, có vị ngọt thanh và nhiều nước.', N'https://example.com/bau.jpg', 2, 95, 14, 0.6, 0.02, 2.9, 1, 0.4, 0, 0, 0, 0, 0, 0, 0, 0.021, 0.0002, 0.011, 7E-05, 0.025, 0.15, 0.002, 0.0007, 2.6E-05, 2E-07, 0.012, 2E-05, 3E-05, 0.0004, 0.000152, 4E-05, 6E-06, 0, 0, 0, 0, 0, 0, 0, 1E-05, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0.01, 0.01, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
INSERT [FoodData].[IngredientDetails100g] ([Id], [KeyNoteId], [Name], [Describe], [URLImage], [TypeOfCalculationId], [Energy], [Water], [Protein], [Fat], [Carbohydrate], [Fiber], [Ash], [Sugar], [Galactose], [Maltose], [Lactose], [Fructose], [Glucose], [Sucrose], [Calcium], [Iron], [Magnesium], [Manganese], [Phosphorous], [Potassium], [Sodium], [Zinc], [Copper], [Selenium], [Vitamin_C], [Vitamin_B1], [Vitamin_B2], [Vitamin_PP], [Vitamin_B5], [Vitamin_B6], [Folat], [Vitamin_B9], [Vitamin_H], [Vitamin_B12], [Vitamin_A], [Vitamin_D], [Vitamin_E], [Vitamin_K], [Beta_caroten], [Alpha_caroten], [Beta_cryptoxanthin], [Lycopen], [LuteinVsZeaxanthin], [Purin], [Total_isoflavone], [Daidzein], [Genistein], [Glycetin], [Total_saturated_fatty_acid], [Palmitic_C16_0], [Margaric_C17_0], [Stearic_C18_0], [Arachidic_C20_0], [Behenic_C22_0], [Lignoceric_C24_0], [TotalMonounsaturatedFattyAcid], [Myristoleic_C14_1], [Palmitoleic_C16_1], [Oleic_C18_1], [TotalPolyunsaturatedFattyAcid], [Linoleic_C18_2_n6], [Linolenic_C18_2_n3], [Arachidonic_C20_4], [Eicosapentaenoic_C20_5_n3], [Docosahexaenoic_C22_6_n3], [TotalTransFattyAcid], [Cholesterol], [Phytosterol], [Lysin], [Methionin], [Tryptophan], [Phenylalanin], [Threonin], [Valin], [Leucin], [Isoleucin], [Arginin], [Histidin], [Cystin], [Tyrosin], [Alanin], [Acid_aspartic], [Acid_glutamic], [Glycin], [Prolin], [Serin]) VALUES (2019, 1, N'Mỡ lợn nước', N'Mỡ lợn nước là mỡ lợn đã được nấu chảy thành dầu, thường dùng để chiên, xào hoặc làm gia vị trong các món ăn.', N'https://nld.mediacdn.vn/2016/mo-lon-sach-1475028558070.jpg', 7, 0, 896, 0, 99.6, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0.002, 0.0003, 0, 0, 0.012, 0, 0, 0, 0, 0, 0, 2E-05, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 39.1, 23.8, 0, 13.5, 0, 0, 0, 45.1, 0, 2.7, 41.2, 11.2, 10.2, 1, 0, 0, 0, 0, 0.095, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
INSERT [FoodData].[IngredientDetails100g] ([Id], [KeyNoteId], [Name], [Describe], [URLImage], [TypeOfCalculationId], [Energy], [Water], [Protein], [Fat], [Carbohydrate], [Fiber], [Ash], [Sugar], [Galactose], [Maltose], [Lactose], [Fructose], [Glucose], [Sucrose], [Calcium], [Iron], [Magnesium], [Manganese], [Phosphorous], [Potassium], [Sodium], [Zinc], [Copper], [Selenium], [Vitamin_C], [Vitamin_B1], [Vitamin_B2], [Vitamin_PP], [Vitamin_B5], [Vitamin_B6], [Folat], [Vitamin_B9], [Vitamin_H], [Vitamin_B12], [Vitamin_A], [Vitamin_D], [Vitamin_E], [Vitamin_K], [Beta_caroten], [Alpha_caroten], [Beta_cryptoxanthin], [Lycopen], [LuteinVsZeaxanthin], [Purin], [Total_isoflavone], [Daidzein], [Genistein], [Glycetin], [Total_saturated_fatty_acid], [Palmitic_C16_0], [Margaric_C17_0], [Stearic_C18_0], [Arachidic_C20_0], [Behenic_C22_0], [Lignoceric_C24_0], [TotalMonounsaturatedFattyAcid], [Myristoleic_C14_1], [Palmitoleic_C16_1], [Oleic_C18_1], [TotalPolyunsaturatedFattyAcid], [Linoleic_C18_2_n6], [Linolenic_C18_2_n3], [Arachidonic_C20_4], [Eicosapentaenoic_C20_5_n3], [Docosahexaenoic_C22_6_n3], [TotalTransFattyAcid], [Cholesterol], [Phytosterol], [Lysin], [Methionin], [Tryptophan], [Phenylalanin], [Threonin], [Valin], [Leucin], [Isoleucin], [Arginin], [Histidin], [Cystin], [Tyrosin], [Alanin], [Acid_aspartic], [Acid_glutamic], [Glycin], [Prolin], [Serin]) VALUES (2020, 1, N'Thịt lợn nạc', N'Thịt lợn nạc là phần thịt lợn không có mỡ, thường được dùng trong các món kho, xào, nấu hoặc chế biến món ăn có yêu cầu thịt ít mỡ.', N'https://s1.media.ngoisao.vn/news/2023/05/30/thit-ngoisaovn-w1200-h720.jpg', 9, 73, 139, 19, 7, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0.007, 0.00096, 0.032, 1E-05, 0.19, 341, 0.076, 0.0025, 0.00019, 2.39E-05, 0.001, 9E-05, 1.8E-05, 0.00044, 0.000822, 0.000415, 5E-06, 0, 2.6E-06, 8.4E-08, 2E-06, 0, 0, 0, 0, 0, 0, 0, 0, 0.166, 0, 0, 0, 0, 2.47, 1.57, 0, 0.77, 0, 0, 0, 3.23, 0, 0.23, 2.93, 0.77, 0.62, 0.03, 0.09, 0, 0, 0, 0.067, 0, 1.44, 0.4, 0.23, 0.69, 0.74, 0.91, 1.19, 0.94, 1.01, 0.51, 0.202, 0.644, 0.99, 1.607, 2.603, 1.023, 0.821, 0.752)
INSERT [FoodData].[IngredientDetails100g] ([Id], [KeyNoteId], [Name], [Describe], [URLImage], [TypeOfCalculationId], [Energy], [Water], [Protein], [Fat], [Carbohydrate], [Fiber], [Ash], [Sugar], [Galactose], [Maltose], [Lactose], [Fructose], [Glucose], [Sucrose], [Calcium], [Iron], [Magnesium], [Manganese], [Phosphorous], [Potassium], [Sodium], [Zinc], [Copper], [Selenium], [Vitamin_C], [Vitamin_B1], [Vitamin_B2], [Vitamin_PP], [Vitamin_B5], [Vitamin_B6], [Folat], [Vitamin_B9], [Vitamin_H], [Vitamin_B12], [Vitamin_A], [Vitamin_D], [Vitamin_E], [Vitamin_K], [Beta_caroten], [Alpha_caroten], [Beta_cryptoxanthin], [Lycopen], [LuteinVsZeaxanthin], [Purin], [Total_isoflavone], [Daidzein], [Genistein], [Glycetin], [Total_saturated_fatty_acid], [Palmitic_C16_0], [Margaric_C17_0], [Stearic_C18_0], [Arachidic_C20_0], [Behenic_C22_0], [Lignoceric_C24_0], [TotalMonounsaturatedFattyAcid], [Myristoleic_C14_1], [Palmitoleic_C16_1], [Oleic_C18_1], [TotalPolyunsaturatedFattyAcid], [Linoleic_C18_2_n6], [Linolenic_C18_2_n3], [Arachidonic_C20_4], [Eicosapentaenoic_C20_5_n3], [Docosahexaenoic_C22_6_n3], [TotalTransFattyAcid], [Cholesterol], [Phytosterol], [Lysin], [Methionin], [Tryptophan], [Phenylalanin], [Threonin], [Valin], [Leucin], [Isoleucin], [Arginin], [Histidin], [Cystin], [Tyrosin], [Alanin], [Acid_aspartic], [Acid_glutamic], [Glycin], [Prolin], [Serin]) VALUES (2021, 1, N'Thịt lợn nửa nạc, nửa mỡ', N'Thịt lợn nạc là phần thịt lợn có cả thịt nạc và mỡ, thường được dùng trong các món ăn cần độ béo và ngọt từ mỡ lợn.', N'https://sieuthitrongnha.com/wp-content/uploads/2021/11/thit-heo-ba-roi-sieu-thi-trong-nha.png', 9, 60, 260, 16.5, 21.5, 0, 0, 1.1, 0, 0, 0, 0, 0, 0, 0, 0.009, 0.0015, 0.019, 1E-05, 0.178, 0.285, 0.055, 0.00191, 6.3E-05, 2.87E-05, 0.002, 0.00053, 0.00016, 0.0027, 0.000672, 0.000393, 4E-06, 0, 2.6E-06, 6E-08, 1E-05, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7.29, 4.52, 0, 2.41, 0, 0, 0, 9.34, 0, 0.58, 8.58, 2.24, 1.88, 0.16, 0.09, 0, 0, 0, 0.068, 0, 1.517, 0.432, 0.202, 0.674, 0.757, 0.911, 1.344, 0.767, 1.104, 0.64, 0.21, 0.567, 1.014, 1.547, 2.568, 0.999, 0.789, 0.705)
INSERT [FoodData].[IngredientDetails100g] ([Id], [KeyNoteId], [Name], [Describe], [URLImage], [TypeOfCalculationId], [Energy], [Water], [Protein], [Fat], [Carbohydrate], [Fiber], [Ash], [Sugar], [Galactose], [Maltose], [Lactose], [Fructose], [Glucose], [Sucrose], [Calcium], [Iron], [Magnesium], [Manganese], [Phosphorous], [Potassium], [Sodium], [Zinc], [Copper], [Selenium], [Vitamin_C], [Vitamin_B1], [Vitamin_B2], [Vitamin_PP], [Vitamin_B5], [Vitamin_B6], [Folat], [Vitamin_B9], [Vitamin_H], [Vitamin_B12], [Vitamin_A], [Vitamin_D], [Vitamin_E], [Vitamin_K], [Beta_caroten], [Alpha_caroten], [Beta_cryptoxanthin], [Lycopen], [LuteinVsZeaxanthin], [Purin], [Total_isoflavone], [Daidzein], [Genistein], [Glycetin], [Total_saturated_fatty_acid], [Palmitic_C16_0], [Margaric_C17_0], [Stearic_C18_0], [Arachidic_C20_0], [Behenic_C22_0], [Lignoceric_C24_0], [TotalMonounsaturatedFattyAcid], [Myristoleic_C14_1], [Palmitoleic_C16_1], [Oleic_C18_1], [TotalPolyunsaturatedFattyAcid], [Linoleic_C18_2_n6], [Linolenic_C18_2_n3], [Arachidonic_C20_4], [Eicosapentaenoic_C20_5_n3], [Docosahexaenoic_C22_6_n3], [TotalTransFattyAcid], [Cholesterol], [Phytosterol], [Lysin], [Methionin], [Tryptophan], [Phenylalanin], [Threonin], [Valin], [Leucin], [Isoleucin], [Arginin], [Histidin], [Cystin], [Tyrosin], [Alanin], [Acid_aspartic], [Acid_glutamic], [Glycin], [Prolin], [Serin]) VALUES (2022, 1, N'Thịt gà ta', N'Thịt gà ta là thực phẩm phổ biến, có thể là thịt ức gà, đùi gà, hoặc cánh gà, dùng trong nhiều món ăn như xào, luộc, nướng.', N'https://media.loveitopcdn.com/30784/thumb/7.png', 2, 65, 199, 20.3, 13.1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0.012, 0.0015, 0.029, 2E-05, 0.2, 0.189, 0.07, 0.0015, 4.8E-05, 1.41E-05, 0.004, 0.00015, 0.00016, 0.0081, 0.00091, 0.00035, 6E-06, 0, 0, 3.1E-08, 0.00012, 0, 0.0003, 1.5E-06, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4.31, 3.15, 0, 0.87, 0, 0, 0, 6.24, 0, 0.83, 5.17, 3.23, 2.88, 0.14, 0.08, 0.01, 0.03, 0, 0.075, 0, 1.859, 0.653, 0.246, 0.717, 0.787, 0.972, 1.629, 1.293, 1.19, 0.432, 0.288, 0.595, 1.088, 1.965, 2.848, 0.797, 0.925, 0.938)
INSERT [FoodData].[IngredientDetails100g] ([Id], [KeyNoteId], [Name], [Describe], [URLImage], [TypeOfCalculationId], [Energy], [Water], [Protein], [Fat], [Carbohydrate], [Fiber], [Ash], [Sugar], [Galactose], [Maltose], [Lactose], [Fructose], [Glucose], [Sucrose], [Calcium], [Iron], [Magnesium], [Manganese], [Phosphorous], [Potassium], [Sodium], [Zinc], [Copper], [Selenium], [Vitamin_C], [Vitamin_B1], [Vitamin_B2], [Vitamin_PP], [Vitamin_B5], [Vitamin_B6], [Folat], [Vitamin_B9], [Vitamin_H], [Vitamin_B12], [Vitamin_A], [Vitamin_D], [Vitamin_E], [Vitamin_K], [Beta_caroten], [Alpha_caroten], [Beta_cryptoxanthin], [Lycopen], [LuteinVsZeaxanthin], [Purin], [Total_isoflavone], [Daidzein], [Genistein], [Glycetin], [Total_saturated_fatty_acid], [Palmitic_C16_0], [Margaric_C17_0], [Stearic_C18_0], [Arachidic_C20_0], [Behenic_C22_0], [Lignoceric_C24_0], [TotalMonounsaturatedFattyAcid], [Myristoleic_C14_1], [Palmitoleic_C16_1], [Oleic_C18_1], [TotalPolyunsaturatedFattyAcid], [Linoleic_C18_2_n6], [Linolenic_C18_2_n3], [Arachidonic_C20_4], [Eicosapentaenoic_C20_5_n3], [Docosahexaenoic_C22_6_n3], [TotalTransFattyAcid], [Cholesterol], [Phytosterol], [Lysin], [Methionin], [Tryptophan], [Phenylalanin], [Threonin], [Valin], [Leucin], [Isoleucin], [Arginin], [Histidin], [Cystin], [Tyrosin], [Alanin], [Acid_aspartic], [Acid_glutamic], [Glycin], [Prolin], [Serin]) VALUES (2023, 1, N'Cá rô đồng', N'Cá rô đồng là một đặc sản của đồng ruộng Việt Nam, thịt thơm ngọt, thường được chế biến thành các món như cá rô kho tộ, cá rô chiên giòn hoặc nấu canh rau.', N'https://example.com/Ca-ro-dong.jpg', 9, 74, 126, 19.1, 5.5, 0, 0, 1.2, 0, 0, 0, 0, 0, 0, 0.026, 0.00025, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
INSERT [FoodData].[IngredientDetails100g] ([Id], [KeyNoteId], [Name], [Describe], [URLImage], [TypeOfCalculationId], [Energy], [Water], [Protein], [Fat], [Carbohydrate], [Fiber], [Ash], [Sugar], [Galactose], [Maltose], [Lactose], [Fructose], [Glucose], [Sucrose], [Calcium], [Iron], [Magnesium], [Manganese], [Phosphorous], [Potassium], [Sodium], [Zinc], [Copper], [Selenium], [Vitamin_C], [Vitamin_B1], [Vitamin_B2], [Vitamin_PP], [Vitamin_B5], [Vitamin_B6], [Folat], [Vitamin_B9], [Vitamin_H], [Vitamin_B12], [Vitamin_A], [Vitamin_D], [Vitamin_E], [Vitamin_K], [Beta_caroten], [Alpha_caroten], [Beta_cryptoxanthin], [Lycopen], [LuteinVsZeaxanthin], [Purin], [Total_isoflavone], [Daidzein], [Genistein], [Glycetin], [Total_saturated_fatty_acid], [Palmitic_C16_0], [Margaric_C17_0], [Stearic_C18_0], [Arachidic_C20_0], [Behenic_C22_0], [Lignoceric_C24_0], [TotalMonounsaturatedFattyAcid], [Myristoleic_C14_1], [Palmitoleic_C16_1], [Oleic_C18_1], [TotalPolyunsaturatedFattyAcid], [Linoleic_C18_2_n6], [Linolenic_C18_2_n3], [Arachidonic_C20_4], [Eicosapentaenoic_C20_5_n3], [Docosahexaenoic_C22_6_n3], [TotalTransFattyAcid], [Cholesterol], [Phytosterol], [Lysin], [Methionin], [Tryptophan], [Phenylalanin], [Threonin], [Valin], [Leucin], [Isoleucin], [Arginin], [Histidin], [Cystin], [Tyrosin], [Alanin], [Acid_aspartic], [Acid_glutamic], [Glycin], [Prolin], [Serin]) VALUES (2024, 1, N'Sườn lợn', N'Sườn lợn là phần thịt được lấy từ xương sườn của lợn, thường được sử dụng để nấu canh, nướng, hoặc chiên.', N'https://example.com/suon-lon.jpg', 9, 68, 187, 17.9, 12.8, 0, 1.1, 0, 0, 0, 0, 0, 0.007, 0.00061, 0.014, 1E-05, 0.16, 0.2, 0.0038, 0.1, 0.0001, 6.9E-06, 0, 0.00096, 0.00023, 0.0052, 0.0006, 0.00026, 2E-06, 0, 2.6E-06, 7E-07, 0, 6.9E-07, 0.0001, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6.45, 3.97, 0, 2.12, 0.04, 0, 0, 7.45, 0, 0.43, 6.24, 1.97, 1.81, 0.16, 0, 0, 0, 0.08, 6.5E-05, 0, 0, 0, 65, 0, 1.56, 0.42, 0.19, 0.65, 0.74, 0.93, 1.27, 0.85, 1.05, 0.65, 0.12, 0.59, 1.05, 1.56, 2.29, 0.91, 0.25, 0.68)
INSERT [FoodData].[IngredientDetails100g] ([Id], [KeyNoteId], [Name], [Describe], [URLImage], [TypeOfCalculationId], [Energy], [Water], [Protein], [Fat], [Carbohydrate], [Fiber], [Ash], [Sugar], [Galactose], [Maltose], [Lactose], [Fructose], [Glucose], [Sucrose], [Calcium], [Iron], [Magnesium], [Manganese], [Phosphorous], [Potassium], [Sodium], [Zinc], [Copper], [Selenium], [Vitamin_C], [Vitamin_B1], [Vitamin_B2], [Vitamin_PP], [Vitamin_B5], [Vitamin_B6], [Folat], [Vitamin_B9], [Vitamin_H], [Vitamin_B12], [Vitamin_A], [Vitamin_D], [Vitamin_E], [Vitamin_K], [Beta_caroten], [Alpha_caroten], [Beta_cryptoxanthin], [Lycopen], [LuteinVsZeaxanthin], [Purin], [Total_isoflavone], [Daidzein], [Genistein], [Glycetin], [Total_saturated_fatty_acid], [Palmitic_C16_0], [Margaric_C17_0], [Stearic_C18_0], [Arachidic_C20_0], [Behenic_C22_0], [Lignoceric_C24_0], [TotalMonounsaturatedFattyAcid], [Myristoleic_C14_1], [Palmitoleic_C16_1], [Oleic_C18_1], [TotalPolyunsaturatedFattyAcid], [Linoleic_C18_2_n6], [Linolenic_C18_2_n3], [Arachidonic_C20_4], [Eicosapentaenoic_C20_5_n3], [Docosahexaenoic_C22_6_n3], [TotalTransFattyAcid], [Cholesterol], [Phytosterol], [Lysin], [Methionin], [Tryptophan], [Phenylalanin], [Threonin], [Valin], [Leucin], [Isoleucin], [Arginin], [Histidin], [Cystin], [Tyrosin], [Alanin], [Acid_aspartic], [Acid_glutamic], [Glycin], [Prolin], [Serin]) VALUES (2025, 1, N'Cua Đồng', N'Cua đồng là loại hải sản nước ngọt phổ biến, thường được dùng để chế biến các món ăn như canh cua, bún riêu, hoặc rang me.', N'https://example.com/cua-dong.jpg', 9, 74, 87, 12.3, 3.3, 2, 0, 8, 0, 0, 0, 0, 0, 0, 0, 0.12, 0.0014, 0, 0.171, 0.266, 0.453, 0, 0, 0, 0, 1E-05, 0.00051, 0.0021, 0, 0, 0, 0, 0, 0.00021, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
INSERT [FoodData].[IngredientDetails100g] ([Id], [KeyNoteId], [Name], [Describe], [URLImage], [TypeOfCalculationId], [Energy], [Water], [Protein], [Fat], [Carbohydrate], [Fiber], [Ash], [Sugar], [Galactose], [Maltose], [Lactose], [Fructose], [Glucose], [Sucrose], [Calcium], [Iron], [Magnesium], [Manganese], [Phosphorous], [Potassium], [Sodium], [Zinc], [Copper], [Selenium], [Vitamin_C], [Vitamin_B1], [Vitamin_B2], [Vitamin_PP], [Vitamin_B5], [Vitamin_B6], [Folat], [Vitamin_B9], [Vitamin_H], [Vitamin_B12], [Vitamin_A], [Vitamin_D], [Vitamin_E], [Vitamin_K], [Beta_caroten], [Alpha_caroten], [Beta_cryptoxanthin], [Lycopen], [LuteinVsZeaxanthin], [Purin], [Total_isoflavone], [Daidzein], [Genistein], [Glycetin], [Total_saturated_fatty_acid], [Palmitic_C16_0], [Margaric_C17_0], [Stearic_C18_0], [Arachidic_C20_0], [Behenic_C22_0], [Lignoceric_C24_0], [TotalMonounsaturatedFattyAcid], [Myristoleic_C14_1], [Palmitoleic_C16_1], [Oleic_C18_1], [TotalPolyunsaturatedFattyAcid], [Linoleic_C18_2_n6], [Linolenic_C18_2_n3], [Arachidonic_C20_4], [Eicosapentaenoic_C20_5_n3], [Docosahexaenoic_C22_6_n3], [TotalTransFattyAcid], [Cholesterol], [Phytosterol], [Lysin], [Methionin], [Tryptophan], [Phenylalanin], [Threonin], [Valin], [Leucin], [Isoleucin], [Arginin], [Histidin], [Cystin], [Tyrosin], [Alanin], [Acid_aspartic], [Acid_glutamic], [Glycin], [Prolin], [Serin]) VALUES (2026, 1, N'Tôm biển', N'Tôm biển là hải sản giàu protein, ít chất béo và cung cấp nhiều vitamin, khoáng chất như iodine và vitamin B12. Chúng có thể được chế biến bằng cách luộc, nướng, xào hoặc hấp, với hương vị ngọt, chắc và dễ dàng kết hợp trong nhiều món ăn. Các loại tôm biển phổ biến bao gồm tôm sú, tôm hùm và tôm thẻ chân trắng.', N'https://cdn.tgdd.vn/2021/09/CookProduct/61-1200x676.jpg', 2, 79, 82, 17.6, 0.9, 0.9, 0, 1.4, 0, 0, 0, 0, 0, 0, 0, 0.079, 0.0016, 0.037, 5E-05, 0.184, 0.185, 0.148, 0.00111, 0.000264, 3.8E-05, 0, 4E-05, 8E-05, 0.0023, 0.000276, 0.000104, 3E-06, 0, 5E-06, 1.16E-06, 2E-05, 0, 0.0011, 0, 5E-06, 0, 0, 0, 0, 0.147, 0, 0, 0, 0, 0.33, 0.18, 0, 0.1, 0, 0, 0, 0.25, 0, 0.08, 0.15, 0.67, 0.03, 0.01, 0.09, 0.26, 0.22, 0, 0.152, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
INSERT [FoodData].[IngredientDetails100g] ([Id], [KeyNoteId], [Name], [Describe], [URLImage], [TypeOfCalculationId], [Energy], [Water], [Protein], [Fat], [Carbohydrate], [Fiber], [Ash], [Sugar], [Galactose], [Maltose], [Lactose], [Fructose], [Glucose], [Sucrose], [Calcium], [Iron], [Magnesium], [Manganese], [Phosphorous], [Potassium], [Sodium], [Zinc], [Copper], [Selenium], [Vitamin_C], [Vitamin_B1], [Vitamin_B2], [Vitamin_PP], [Vitamin_B5], [Vitamin_B6], [Folat], [Vitamin_B9], [Vitamin_H], [Vitamin_B12], [Vitamin_A], [Vitamin_D], [Vitamin_E], [Vitamin_K], [Beta_caroten], [Alpha_caroten], [Beta_cryptoxanthin], [Lycopen], [LuteinVsZeaxanthin], [Purin], [Total_isoflavone], [Daidzein], [Genistein], [Glycetin], [Total_saturated_fatty_acid], [Palmitic_C16_0], [Margaric_C17_0], [Stearic_C18_0], [Arachidic_C20_0], [Behenic_C22_0], [Lignoceric_C24_0], [TotalMonounsaturatedFattyAcid], [Myristoleic_C14_1], [Palmitoleic_C16_1], [Oleic_C18_1], [TotalPolyunsaturatedFattyAcid], [Linoleic_C18_2_n6], [Linolenic_C18_2_n3], [Arachidonic_C20_4], [Eicosapentaenoic_C20_5_n3], [Docosahexaenoic_C22_6_n3], [TotalTransFattyAcid], [Cholesterol], [Phytosterol], [Lysin], [Methionin], [Tryptophan], [Phenylalanin], [Threonin], [Valin], [Leucin], [Isoleucin], [Arginin], [Histidin], [Cystin], [Tyrosin], [Alanin], [Acid_aspartic], [Acid_glutamic], [Glycin], [Prolin], [Serin]) VALUES (2027, 1, N'Đường Kính', N'Đường là carbohydrate đơn giản, cung cấp năng lượng nhanh cho cơ thể. Nó có thể được chiết xuất từ mía, củ cải đường hoặc trái cây. Đường có dạng tinh luyện (sucrose) dùng trong nấu ăn và đồ uống, hoặc dạng tự nhiên có trong trái cây và sữa. Tuy nhiên, tiêu thụ quá nhiều đường có thể gây ảnh hưởng tiêu cực đến sức khỏe.', N'https://media.cooky.vn/images/blog-2016/cac-loai-duong.jpeg', 7, 0, 397, 0, 0, 99.3, 0, 0.2, 0, 0, 0, 0, 0, 0, 0, 0, 6E-05, 0, 0, 0, 0.002, 0, 0, 0, 0, 0, 0, 3E-05, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
INSERT [FoodData].[IngredientDetails100g] ([Id], [KeyNoteId], [Name], [Describe], [URLImage], [TypeOfCalculationId], [Energy], [Water], [Protein], [Fat], [Carbohydrate], [Fiber], [Ash], [Sugar], [Galactose], [Maltose], [Lactose], [Fructose], [Glucose], [Sucrose], [Calcium], [Iron], [Magnesium], [Manganese], [Phosphorous], [Potassium], [Sodium], [Zinc], [Copper], [Selenium], [Vitamin_C], [Vitamin_B1], [Vitamin_B2], [Vitamin_PP], [Vitamin_B5], [Vitamin_B6], [Folat], [Vitamin_B9], [Vitamin_H], [Vitamin_B12], [Vitamin_A], [Vitamin_D], [Vitamin_E], [Vitamin_K], [Beta_caroten], [Alpha_caroten], [Beta_cryptoxanthin], [Lycopen], [LuteinVsZeaxanthin], [Purin], [Total_isoflavone], [Daidzein], [Genistein], [Glycetin], [Total_saturated_fatty_acid], [Palmitic_C16_0], [Margaric_C17_0], [Stearic_C18_0], [Arachidic_C20_0], [Behenic_C22_0], [Lignoceric_C24_0], [TotalMonounsaturatedFattyAcid], [Myristoleic_C14_1], [Palmitoleic_C16_1], [Oleic_C18_1], [TotalPolyunsaturatedFattyAcid], [Linoleic_C18_2_n6], [Linolenic_C18_2_n3], [Arachidonic_C20_4], [Eicosapentaenoic_C20_5_n3], [Docosahexaenoic_C22_6_n3], [TotalTransFattyAcid], [Cholesterol], [Phytosterol], [Lysin], [Methionin], [Tryptophan], [Phenylalanin], [Threonin], [Valin], [Leucin], [Isoleucin], [Arginin], [Histidin], [Cystin], [Tyrosin], [Alanin], [Acid_aspartic], [Acid_glutamic], [Glycin], [Prolin], [Serin]) VALUES (2028, 1, N'Trứng Gà', N'Trứng gà là một loại thực phẩm phổ biến, gồm vỏ cứng bên ngoài, lòng trắng và lòng đỏ giàu dinh dưỡng bên trong, chứa protein, vitamin và khoáng chất.', N'https://watermark.lovepik.com/photo/20211120/large/lovepik-red-egg-picture_500499123.jpg', 1, 72, 166, 14.8, 11.6, 0.5, 0, 1.1, 0.77, 0.11, 0.11, 0.11, 0.11, 0.21, 0.11, 0.055, 0.0027, 0.011, 4E-05, 0.21, 0.176, 0.158, 0.0009, 5.5E-05, 3.17E-05, 0, 0.00016, 0.00031, 0.0002, 0.001438, 0.000143, 4.7E-05, 0, 2.5E-05, 1.29E-06, 0.0007, 8.8E-07, 0.00097, 3E-07, 0, 0, 0, 0, 5.5E-05, 0, 0, 0, 0, 0, 3.1, 2.23, 0, 0.78, 0.01, 0.01, 0, 3.81, 0.01, 0.3, 3.47, 1.36, 1.15, 0.03, 0.14, 0, 0.04, 0, 0.00047, 0, 0.000796, 0.000428, 0.000188, 0.000703, 0.000598, 0.000876, 0.00108, 0.000746, 0.000778, 0.00029, 0.00028, 0.000514, 0.000744, 0.001084, 0.001068, 0.000436, 0.000488, 0.000963)
INSERT [FoodData].[IngredientDetails100g] ([Id], [KeyNoteId], [Name], [Describe], [URLImage], [TypeOfCalculationId], [Energy], [Water], [Protein], [Fat], [Carbohydrate], [Fiber], [Ash], [Sugar], [Galactose], [Maltose], [Lactose], [Fructose], [Glucose], [Sucrose], [Calcium], [Iron], [Magnesium], [Manganese], [Phosphorous], [Potassium], [Sodium], [Zinc], [Copper], [Selenium], [Vitamin_C], [Vitamin_B1], [Vitamin_B2], [Vitamin_PP], [Vitamin_B5], [Vitamin_B6], [Folat], [Vitamin_B9], [Vitamin_H], [Vitamin_B12], [Vitamin_A], [Vitamin_D], [Vitamin_E], [Vitamin_K], [Beta_caroten], [Alpha_caroten], [Beta_cryptoxanthin], [Lycopen], [LuteinVsZeaxanthin], [Purin], [Total_isoflavone], [Daidzein], [Genistein], [Glycetin], [Total_saturated_fatty_acid], [Palmitic_C16_0], [Margaric_C17_0], [Stearic_C18_0], [Arachidic_C20_0], [Behenic_C22_0], [Lignoceric_C24_0], [TotalMonounsaturatedFattyAcid], [Myristoleic_C14_1], [Palmitoleic_C16_1], [Oleic_C18_1], [TotalPolyunsaturatedFattyAcid], [Linoleic_C18_2_n6], [Linolenic_C18_2_n3], [Arachidonic_C20_4], [Eicosapentaenoic_C20_5_n3], [Docosahexaenoic_C22_6_n3], [TotalTransFattyAcid], [Cholesterol], [Phytosterol], [Lysin], [Methionin], [Tryptophan], [Phenylalanin], [Threonin], [Valin], [Leucin], [Isoleucin], [Arginin], [Histidin], [Cystin], [Tyrosin], [Alanin], [Acid_aspartic], [Acid_glutamic], [Glycin], [Prolin], [Serin]) VALUES (2029, 1, N'Riềng', N'Riềng là một loại củ thuộc họ gừng, có vỏ màu nâu nhạt, thịt củ màu trắng hơi ngả vàng, mùi thơm đặc trưng, vị cay nhẹ, thường dùng làm gia vị trong các món ăn.', N'https://suckhoehangngay.mediacdn.vn/154880486097817600/2021/8/17/cu-rieng-la-gi-tac-dung-cua-cu-rieng-doi-voi-suc-khoe-con-nguoi-2-800x448-16292006728911736265135.jpg', 5, 12, 227, 15.6, 4.2, 31.8, 23.6, 12.8, 0, 0, 0, 0, 0, 0, 0, 8.5E-05, 1.7E-05, 0, 0, 0.00038, 0, 0, 0, 0, 0, 0, 0.00061, 0.0009, 0.0081, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0.008442, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
SET IDENTITY_INSERT [FoodData].[IngredientDetails100g] OFF
GO
SET IDENTITY_INSERT [FoodData].[KeyNote] ON 

INSERT [FoodData].[KeyNote] ([Id], [KeyList]) VALUES (1, N'#WantCooking=1#SlotOfTheDay:1;2;#Size=Bữa lớn')
INSERT [FoodData].[KeyNote] ([Id], [KeyList]) VALUES (2, N'#WantCooking=1#SlotOfTheDay:1;2;3;4;#Size=Bữa lớn')
INSERT [FoodData].[KeyNote] ([Id], [KeyList]) VALUES (3, N'#WantCooking=1#SlotOfTheDay:4;#Size=Bữa lớn')
INSERT [FoodData].[KeyNote] ([Id], [KeyList]) VALUES (4, N'#WantCooking=2#SlotOfTheDay:4;#Size=Bữa lớn')
INSERT [FoodData].[KeyNote] ([Id], [KeyList]) VALUES (5, N'#WantCooking=1#SlotOfTheDay:3;#Size=Bữa lớn')
INSERT [FoodData].[KeyNote] ([Id], [KeyList]) VALUES (6, N'Unknow')
SET IDENTITY_INSERT [FoodData].[KeyNote] OFF
GO
SET IDENTITY_INSERT [FoodData].[Recipe] ON 

INSERT [FoodData].[Recipe] ([RecipeId], [FoodListId], [NumericalOrder], [Describe], [URLImage]) VALUES (1, 1, 1, N'Cut tomatoes into small pieces', N'/images/recipe1.jpg')
INSERT [FoodData].[Recipe] ([RecipeId], [FoodListId], [NumericalOrder], [Describe], [URLImage]) VALUES (2, 2, 1, N'Grill the beef', N'/images/recipe2.jpg')
INSERT [FoodData].[Recipe] ([RecipeId], [FoodListId], [NumericalOrder], [Describe], [URLImage]) VALUES (3, 3, 1, N'Mix yogurt with fruits', N'/images/recipe3.jpg')
INSERT [FoodData].[Recipe] ([RecipeId], [FoodListId], [NumericalOrder], [Describe], [URLImage]) VALUES (4, 4, 1, N'Slice apples', N'/images/recipe4.jpg')
SET IDENTITY_INSERT [FoodData].[Recipe] OFF
GO
INSERT [FoodData].[ScaleAmount] ([FoodListId], [IngredientDetailsId], [ScaleAmount]) VALUES (1, 1, 100)
INSERT [FoodData].[ScaleAmount] ([FoodListId], [IngredientDetailsId], [ScaleAmount]) VALUES (1, 6, 50)
INSERT [FoodData].[ScaleAmount] ([FoodListId], [IngredientDetailsId], [ScaleAmount]) VALUES (1, 12, 5)
INSERT [FoodData].[ScaleAmount] ([FoodListId], [IngredientDetailsId], [ScaleAmount]) VALUES (1, 13, 10)
INSERT [FoodData].[ScaleAmount] ([FoodListId], [IngredientDetailsId], [ScaleAmount]) VALUES (2, 1, 50)
INSERT [FoodData].[ScaleAmount] ([FoodListId], [IngredientDetailsId], [ScaleAmount]) VALUES (2, 2, 100)
INSERT [FoodData].[ScaleAmount] ([FoodListId], [IngredientDetailsId], [ScaleAmount]) VALUES (2, 12, 5)
INSERT [FoodData].[ScaleAmount] ([FoodListId], [IngredientDetailsId], [ScaleAmount]) VALUES (2, 13, 10)
INSERT [FoodData].[ScaleAmount] ([FoodListId], [IngredientDetailsId], [ScaleAmount]) VALUES (3, 3, 100)
INSERT [FoodData].[ScaleAmount] ([FoodListId], [IngredientDetailsId], [ScaleAmount]) VALUES (3, 4, 50)
INSERT [FoodData].[ScaleAmount] ([FoodListId], [IngredientDetailsId], [ScaleAmount]) VALUES (3, 11, 50)
INSERT [FoodData].[ScaleAmount] ([FoodListId], [IngredientDetailsId], [ScaleAmount]) VALUES (3, 12, 5)
INSERT [FoodData].[ScaleAmount] ([FoodListId], [IngredientDetailsId], [ScaleAmount]) VALUES (4, 4, 100)
INSERT [FoodData].[ScaleAmount] ([FoodListId], [IngredientDetailsId], [ScaleAmount]) VALUES (4, 12, 10)
INSERT [FoodData].[ScaleAmount] ([FoodListId], [IngredientDetailsId], [ScaleAmount]) VALUES (4, 13, 10)
INSERT [FoodData].[ScaleAmount] ([FoodListId], [IngredientDetailsId], [ScaleAmount]) VALUES (4, 14, 200)
INSERT [FoodData].[ScaleAmount] ([FoodListId], [IngredientDetailsId], [ScaleAmount]) VALUES (5, 2, 150)
INSERT [FoodData].[ScaleAmount] ([FoodListId], [IngredientDetailsId], [ScaleAmount]) VALUES (5, 11, 50)
INSERT [FoodData].[ScaleAmount] ([FoodListId], [IngredientDetailsId], [ScaleAmount]) VALUES (5, 12, 10)
INSERT [FoodData].[ScaleAmount] ([FoodListId], [IngredientDetailsId], [ScaleAmount]) VALUES (5, 13, 10)
INSERT [FoodData].[ScaleAmount] ([FoodListId], [IngredientDetailsId], [ScaleAmount]) VALUES (6, 14, 200)
GO
SET IDENTITY_INSERT [FoodData].[TypeOfCalculation] ON 

INSERT [FoodData].[TypeOfCalculation] ([TypeId], [CalculationForm]) VALUES (1, N'Medium=50#TypeOfCalculation:Quả-50;2 Quả-100;3 Quả-150')
INSERT [FoodData].[TypeOfCalculation] ([TypeId], [CalculationForm]) VALUES (2, N'Medium=100#TypeOfCalculation:Lạng-100;Cân-1000;Miếng-150')
INSERT [FoodData].[TypeOfCalculation] ([TypeId], [CalculationForm]) VALUES (3, N'Medium=300#TypeOfCalculation:Bó-300;Mớ-200;Cân-1000')
INSERT [FoodData].[TypeOfCalculation] ([TypeId], [CalculationForm]) VALUES (4, N'Medium=30#TypeOfCalculation:Cái-30;Túi-300;Vỉ-300')
INSERT [FoodData].[TypeOfCalculation] ([TypeId], [CalculationForm]) VALUES (5, N'Medium=100#TypeOfCalculation:Củ-100;Nhánh-30')
INSERT [FoodData].[TypeOfCalculation] ([TypeId], [CalculationForm]) VALUES (6, N'Medium=200#TypeOfCalculation:Cái-200;Chiếc-200')
INSERT [FoodData].[TypeOfCalculation] ([TypeId], [CalculationForm]) VALUES (7, N'Medium=200#TypeOfCalculation:Muỗn-200;Thìa-200')
INSERT [FoodData].[TypeOfCalculation] ([TypeId], [CalculationForm]) VALUES (8, N'Unknow')
INSERT [FoodData].[TypeOfCalculation] ([TypeId], [CalculationForm]) VALUES (9, N'Unknow')
SET IDENTITY_INSERT [FoodData].[TypeOfCalculation] OFF
GO
SET IDENTITY_INSERT [UserData].[ExpertPackages] ON 

INSERT [UserData].[ExpertPackages] ([Id], [Name], [Describe], [Price], [Duration]) VALUES (1, N'Basic Package', N'This is a starter package.', 10000.0000, 30)
INSERT [UserData].[ExpertPackages] ([Id], [Name], [Describe], [Price], [Duration]) VALUES (2, N'Standard Package', N'This package offers moderate features.', 2000.0000, 60)
INSERT [UserData].[ExpertPackages] ([Id], [Name], [Describe], [Price], [Duration]) VALUES (3, N'Premium Package', N'This is the best package with advanced features.', 3000.0000, 90)
SET IDENTITY_INSERT [UserData].[ExpertPackages] OFF
GO
INSERT [UserData].[FoodSelection] ([UserId], [FoodListId], [Rate], [RecurringId], [IsBlock], [IsCollection], [IsLike]) VALUES (1, 1, 5, 1, 0, NULL, NULL)
INSERT [UserData].[FoodSelection] ([UserId], [FoodListId], [Rate], [RecurringId], [IsBlock], [IsCollection], [IsLike]) VALUES (2, 2, 4, 2, 0, NULL, NULL)
INSERT [UserData].[FoodSelection] ([UserId], [FoodListId], [Rate], [RecurringId], [IsBlock], [IsCollection], [IsLike]) VALUES (3, 3, 5, 3, 0, NULL, NULL)
INSERT [UserData].[FoodSelection] ([UserId], [FoodListId], [Rate], [RecurringId], [IsBlock], [IsCollection], [IsLike]) VALUES (45, 1, NULL, 2, 0, 1, 1)
INSERT [UserData].[FoodSelection] ([UserId], [FoodListId], [Rate], [RecurringId], [IsBlock], [IsCollection], [IsLike]) VALUES (45, 2, NULL, NULL, 0, 0, 0)
INSERT [UserData].[FoodSelection] ([UserId], [FoodListId], [Rate], [RecurringId], [IsBlock], [IsCollection], [IsLike]) VALUES (45, 3, NULL, NULL, 0, 0, 0)
INSERT [UserData].[FoodSelection] ([UserId], [FoodListId], [Rate], [RecurringId], [IsBlock], [IsCollection], [IsLike]) VALUES (45, 4, 5, 4, 0, NULL, 0)
INSERT [UserData].[FoodSelection] ([UserId], [FoodListId], [Rate], [RecurringId], [IsBlock], [IsCollection], [IsLike]) VALUES (45, 5, 5, 4, 0, NULL, 0)
INSERT [UserData].[FoodSelection] ([UserId], [FoodListId], [Rate], [RecurringId], [IsBlock], [IsCollection], [IsLike]) VALUES (45, 6, NULL, NULL, 0, 0, 0)
INSERT [UserData].[FoodSelection] ([UserId], [FoodListId], [Rate], [RecurringId], [IsBlock], [IsCollection], [IsLike]) VALUES (3083, 4, NULL, NULL, 1, 0, 0)
INSERT [UserData].[FoodSelection] ([UserId], [FoodListId], [Rate], [RecurringId], [IsBlock], [IsCollection], [IsLike]) VALUES (3083, 6, NULL, NULL, 0, 0, 0)
INSERT [UserData].[FoodSelection] ([UserId], [FoodListId], [Rate], [RecurringId], [IsBlock], [IsCollection], [IsLike]) VALUES (3084, 1, NULL, NULL, 0, 0, 0)
INSERT [UserData].[FoodSelection] ([UserId], [FoodListId], [Rate], [RecurringId], [IsBlock], [IsCollection], [IsLike]) VALUES (3084, 6, NULL, NULL, 1, 0, 0)
GO
SET IDENTITY_INSERT [UserData].[ListOfDiseases] ON 

INSERT [UserData].[ListOfDiseases] ([Id], [Name], [Describe]) VALUES (1, N'Béo phì', N'A disease that causes high blood sugar')
INSERT [UserData].[ListOfDiseases] ([Id], [Name], [Describe]) VALUES (2, N'Gan Nhiễm Mỡ', N'High blood pressure condition')
INSERT [UserData].[ListOfDiseases] ([Id], [Name], [Describe]) VALUES (3, N'Lậu', N'Chronic respiratory condition')
INSERT [UserData].[ListOfDiseases] ([Id], [Name], [Describe]) VALUES (4, N'Dạ dày', N'A condition affecting heart health')
INSERT [UserData].[ListOfDiseases] ([Id], [Name], [Describe]) VALUES (5, N'Máu nhiễm mỡ', N'Máu nhiễm mỡ')
INSERT [UserData].[ListOfDiseases] ([Id], [Name], [Describe]) VALUES (1005, N'Bệnh Hen Suyễn', N'bệnh khó thở')
SET IDENTITY_INSERT [UserData].[ListOfDiseases] OFF
GO
SET IDENTITY_INSERT [UserData].[MealSettings] ON 

INSERT [UserData].[MealSettings] ([Id], [UserId], [DayOfTheWeekStartId], [SameScheduleEveryDay], [FoodTypeIdWant]) VALUES (1, 1, 8, 1, 1)
INSERT [UserData].[MealSettings] ([Id], [UserId], [DayOfTheWeekStartId], [SameScheduleEveryDay], [FoodTypeIdWant]) VALUES (2, 32, 8, 1, 1)
INSERT [UserData].[MealSettings] ([Id], [UserId], [DayOfTheWeekStartId], [SameScheduleEveryDay], [FoodTypeIdWant]) VALUES (3, 39, 8, 1, 1)
INSERT [UserData].[MealSettings] ([Id], [UserId], [DayOfTheWeekStartId], [SameScheduleEveryDay], [FoodTypeIdWant]) VALUES (4, 40, 8, 1, 1)
INSERT [UserData].[MealSettings] ([Id], [UserId], [DayOfTheWeekStartId], [SameScheduleEveryDay], [FoodTypeIdWant]) VALUES (8, 43, 8, 1, 1)
INSERT [UserData].[MealSettings] ([Id], [UserId], [DayOfTheWeekStartId], [SameScheduleEveryDay], [FoodTypeIdWant]) VALUES (9, 44, 8, 1, 1)
INSERT [UserData].[MealSettings] ([Id], [UserId], [DayOfTheWeekStartId], [SameScheduleEveryDay], [FoodTypeIdWant]) VALUES (10, 45, 8, 1, 1)
INSERT [UserData].[MealSettings] ([Id], [UserId], [DayOfTheWeekStartId], [SameScheduleEveryDay], [FoodTypeIdWant]) VALUES (11, 46, 8, 1, 1)
INSERT [UserData].[MealSettings] ([Id], [UserId], [DayOfTheWeekStartId], [SameScheduleEveryDay], [FoodTypeIdWant]) VALUES (12, 47, 2, 0, 1)
INSERT [UserData].[MealSettings] ([Id], [UserId], [DayOfTheWeekStartId], [SameScheduleEveryDay], [FoodTypeIdWant]) VALUES (13, 48, 8, 1, 1)
INSERT [UserData].[MealSettings] ([Id], [UserId], [DayOfTheWeekStartId], [SameScheduleEveryDay], [FoodTypeIdWant]) VALUES (14, 49, 8, 1, 1)
INSERT [UserData].[MealSettings] ([Id], [UserId], [DayOfTheWeekStartId], [SameScheduleEveryDay], [FoodTypeIdWant]) VALUES (16, 55, 8, 1, 1)
INSERT [UserData].[MealSettings] ([Id], [UserId], [DayOfTheWeekStartId], [SameScheduleEveryDay], [FoodTypeIdWant]) VALUES (17, 56, 8, 1, 1)
INSERT [UserData].[MealSettings] ([Id], [UserId], [DayOfTheWeekStartId], [SameScheduleEveryDay], [FoodTypeIdWant]) VALUES (18, 57, 1, 0, 1)
INSERT [UserData].[MealSettings] ([Id], [UserId], [DayOfTheWeekStartId], [SameScheduleEveryDay], [FoodTypeIdWant]) VALUES (1013, 1056, 8, 1, 1)
INSERT [UserData].[MealSettings] ([Id], [UserId], [DayOfTheWeekStartId], [SameScheduleEveryDay], [FoodTypeIdWant]) VALUES (1014, 2, 8, 1, 1)
INSERT [UserData].[MealSettings] ([Id], [UserId], [DayOfTheWeekStartId], [SameScheduleEveryDay], [FoodTypeIdWant]) VALUES (1015, 1057, 8, 1, 1)
INSERT [UserData].[MealSettings] ([Id], [UserId], [DayOfTheWeekStartId], [SameScheduleEveryDay], [FoodTypeIdWant]) VALUES (2013, 3056, 8, 1, 1)
INSERT [UserData].[MealSettings] ([Id], [UserId], [DayOfTheWeekStartId], [SameScheduleEveryDay], [FoodTypeIdWant]) VALUES (2014, 3068, 8, 1, 1)
INSERT [UserData].[MealSettings] ([Id], [UserId], [DayOfTheWeekStartId], [SameScheduleEveryDay], [FoodTypeIdWant]) VALUES (2015, 3069, 8, 1, 1)
INSERT [UserData].[MealSettings] ([Id], [UserId], [DayOfTheWeekStartId], [SameScheduleEveryDay], [FoodTypeIdWant]) VALUES (2018, 3073, 8, 1, 1)
INSERT [UserData].[MealSettings] ([Id], [UserId], [DayOfTheWeekStartId], [SameScheduleEveryDay], [FoodTypeIdWant]) VALUES (2021, 3075, 8, 1, 1)
INSERT [UserData].[MealSettings] ([Id], [UserId], [DayOfTheWeekStartId], [SameScheduleEveryDay], [FoodTypeIdWant]) VALUES (2022, 3076, 8, 1, 1)
INSERT [UserData].[MealSettings] ([Id], [UserId], [DayOfTheWeekStartId], [SameScheduleEveryDay], [FoodTypeIdWant]) VALUES (2023, 3077, 8, 1, 1)
INSERT [UserData].[MealSettings] ([Id], [UserId], [DayOfTheWeekStartId], [SameScheduleEveryDay], [FoodTypeIdWant]) VALUES (2024, 3078, 1, 0, 1)
INSERT [UserData].[MealSettings] ([Id], [UserId], [DayOfTheWeekStartId], [SameScheduleEveryDay], [FoodTypeIdWant]) VALUES (2025, 3079, 8, 1, 1)
INSERT [UserData].[MealSettings] ([Id], [UserId], [DayOfTheWeekStartId], [SameScheduleEveryDay], [FoodTypeIdWant]) VALUES (2026, 3080, 8, 1, 1)
INSERT [UserData].[MealSettings] ([Id], [UserId], [DayOfTheWeekStartId], [SameScheduleEveryDay], [FoodTypeIdWant]) VALUES (2027, 3081, 1, 0, 1)
INSERT [UserData].[MealSettings] ([Id], [UserId], [DayOfTheWeekStartId], [SameScheduleEveryDay], [FoodTypeIdWant]) VALUES (2028, 3082, 8, 1, 1)
INSERT [UserData].[MealSettings] ([Id], [UserId], [DayOfTheWeekStartId], [SameScheduleEveryDay], [FoodTypeIdWant]) VALUES (2029, 3083, 8, 1, 1)
INSERT [UserData].[MealSettings] ([Id], [UserId], [DayOfTheWeekStartId], [SameScheduleEveryDay], [FoodTypeIdWant]) VALUES (2030, 3084, 1, 0, 1)
INSERT [UserData].[MealSettings] ([Id], [UserId], [DayOfTheWeekStartId], [SameScheduleEveryDay], [FoodTypeIdWant]) VALUES (3013, 4056, 8, 1, 1)
INSERT [UserData].[MealSettings] ([Id], [UserId], [DayOfTheWeekStartId], [SameScheduleEveryDay], [FoodTypeIdWant]) VALUES (3014, 4057, 8, 1, 1)
INSERT [UserData].[MealSettings] ([Id], [UserId], [DayOfTheWeekStartId], [SameScheduleEveryDay], [FoodTypeIdWant]) VALUES (3015, 4058, 8, 1, 1)
INSERT [UserData].[MealSettings] ([Id], [UserId], [DayOfTheWeekStartId], [SameScheduleEveryDay], [FoodTypeIdWant]) VALUES (3016, 4061, 8, 1, 1)
INSERT [UserData].[MealSettings] ([Id], [UserId], [DayOfTheWeekStartId], [SameScheduleEveryDay], [FoodTypeIdWant]) VALUES (3017, 4062, 8, 1, 1)
INSERT [UserData].[MealSettings] ([Id], [UserId], [DayOfTheWeekStartId], [SameScheduleEveryDay], [FoodTypeIdWant]) VALUES (4013, 5056, 8, 1, 1)
INSERT [UserData].[MealSettings] ([Id], [UserId], [DayOfTheWeekStartId], [SameScheduleEveryDay], [FoodTypeIdWant]) VALUES (4014, 5057, 8, 1, 1)
INSERT [UserData].[MealSettings] ([Id], [UserId], [DayOfTheWeekStartId], [SameScheduleEveryDay], [FoodTypeIdWant]) VALUES (4015, 5058, 8, 1, 1)
INSERT [UserData].[MealSettings] ([Id], [UserId], [DayOfTheWeekStartId], [SameScheduleEveryDay], [FoodTypeIdWant]) VALUES (4016, 5059, 8, 1, 1)
INSERT [UserData].[MealSettings] ([Id], [UserId], [DayOfTheWeekStartId], [SameScheduleEveryDay], [FoodTypeIdWant]) VALUES (4017, 5060, 8, 1, 1)
INSERT [UserData].[MealSettings] ([Id], [UserId], [DayOfTheWeekStartId], [SameScheduleEveryDay], [FoodTypeIdWant]) VALUES (4018, 5061, 8, 1, 1)
SET IDENTITY_INSERT [UserData].[MealSettings] OFF
GO
SET IDENTITY_INSERT [UserData].[MealSettingsDetails] ON 

INSERT [UserData].[MealSettingsDetails] ([Id], [MealSettingsId], [SlotOfTheDayId], [NutritionTargetsDailyId], [DayOfTheWeekId], [SkipCreationProcess], [Size], [NutritionFocus], [NumberOfDishes], [TypeFavoriteFood], [WantCookingId], [TimeAvailable], [CookingDifficultyId], [Name], [IsActive], [OrderNumber], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber]) VALUES (1, 1, 1, 4, 8, 0, N'Bữa lớn', 0, 3, N'0', 1, 300, 1, NULL, 1, 2, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [UserData].[MealSettingsDetails] ([Id], [MealSettingsId], [SlotOfTheDayId], [NutritionTargetsDailyId], [DayOfTheWeekId], [SkipCreationProcess], [Size], [NutritionFocus], [NumberOfDishes], [TypeFavoriteFood], [WantCookingId], [TimeAvailable], [CookingDifficultyId], [Name], [IsActive], [OrderNumber], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber]) VALUES (4, 1, 1, 4, 8, 0, N'Bữa lớn', 0, 3, N'0', 1, 300, 1, NULL, 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [UserData].[MealSettingsDetails] ([Id], [MealSettingsId], [SlotOfTheDayId], [NutritionTargetsDailyId], [DayOfTheWeekId], [SkipCreationProcess], [Size], [NutritionFocus], [NumberOfDishes], [TypeFavoriteFood], [WantCookingId], [TimeAvailable], [CookingDifficultyId], [Name], [IsActive], [OrderNumber], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber]) VALUES (5, 1, 3, 4, 8, 0, N'Bữa lớn', 0, 3, N'0', 1, 300, 1, NULL, 1, 3, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [UserData].[MealSettingsDetails] ([Id], [MealSettingsId], [SlotOfTheDayId], [NutritionTargetsDailyId], [DayOfTheWeekId], [SkipCreationProcess], [Size], [NutritionFocus], [NumberOfDishes], [TypeFavoriteFood], [WantCookingId], [TimeAvailable], [CookingDifficultyId], [Name], [IsActive], [OrderNumber], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber]) VALUES (6, 1, 4, 4, 8, 0, N'Bữa lớn', 0, 3, N'0', 1, 300, 1, NULL, 1, 4, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [UserData].[MealSettingsDetails] ([Id], [MealSettingsId], [SlotOfTheDayId], [NutritionTargetsDailyId], [DayOfTheWeekId], [SkipCreationProcess], [Size], [NutritionFocus], [NumberOfDishes], [TypeFavoriteFood], [WantCookingId], [TimeAvailable], [CookingDifficultyId], [Name], [IsActive], [OrderNumber], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber]) VALUES (8, 2, 1, 5, 8, 0, N'Bữa lớn', 0, 3, N'1', 1, 300, 1, N'Món nhẹ buổi sáng', 1, 1, 0, 0, 92, 0, 41, 0, 92, 5)
INSERT [UserData].[MealSettingsDetails] ([Id], [MealSettingsId], [SlotOfTheDayId], [NutritionTargetsDailyId], [DayOfTheWeekId], [SkipCreationProcess], [Size], [NutritionFocus], [NumberOfDishes], [TypeFavoriteFood], [WantCookingId], [TimeAvailable], [CookingDifficultyId], [Name], [IsActive], [OrderNumber], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber]) VALUES (10, 2, 2, 5, 8, 0, N'Bữa lớn', 0, 3, N'1', 1, 300, 1, N'Món nhẹ buổi sáng thứ 2', 0, 1, 318, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [UserData].[MealSettingsDetails] ([Id], [MealSettingsId], [SlotOfTheDayId], [NutritionTargetsDailyId], [DayOfTheWeekId], [SkipCreationProcess], [Size], [NutritionFocus], [NumberOfDishes], [TypeFavoriteFood], [WantCookingId], [TimeAvailable], [CookingDifficultyId], [Name], [IsActive], [OrderNumber], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber]) VALUES (11, 2, 3, 5, 8, 0, N'Bữa lớn', 0, 3, N'1', 1, 300, 1, N'Món nhẹ buổi sáng thứ 2', 0, 2, 212, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [UserData].[MealSettingsDetails] ([Id], [MealSettingsId], [SlotOfTheDayId], [NutritionTargetsDailyId], [DayOfTheWeekId], [SkipCreationProcess], [Size], [NutritionFocus], [NumberOfDishes], [TypeFavoriteFood], [WantCookingId], [TimeAvailable], [CookingDifficultyId], [Name], [IsActive], [OrderNumber], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber]) VALUES (12, 2, 4, 5, 8, 0, N'Bữa lớn', 0, 3, N'1', 1, 300, 1, N'Món nhẹ buổi sáng thứ 2', 0, 1, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [UserData].[MealSettingsDetails] ([Id], [MealSettingsId], [SlotOfTheDayId], [NutritionTargetsDailyId], [DayOfTheWeekId], [SkipCreationProcess], [Size], [NutritionFocus], [NumberOfDishes], [TypeFavoriteFood], [WantCookingId], [TimeAvailable], [CookingDifficultyId], [Name], [IsActive], [OrderNumber], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber]) VALUES (13, 2, 1, 5, 8, 0, N'Bữa lớn', 0, 3, N'1', 1, 300, 1, N'dfgdfg', 0, 2, 531, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [UserData].[MealSettingsDetails] ([Id], [MealSettingsId], [SlotOfTheDayId], [NutritionTargetsDailyId], [DayOfTheWeekId], [SkipCreationProcess], [Size], [NutritionFocus], [NumberOfDishes], [TypeFavoriteFood], [WantCookingId], [TimeAvailable], [CookingDifficultyId], [Name], [IsActive], [OrderNumber], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber]) VALUES (14, 2, 1, 5, 8, 0, N'Bữa lớn', 0, 3, N'1', 1, 300, 1, N'Test Món hôm nay', 1, 2, 159, NULL, 39, NULL, 17, NULL, 39, 2)
INSERT [UserData].[MealSettingsDetails] ([Id], [MealSettingsId], [SlotOfTheDayId], [NutritionTargetsDailyId], [DayOfTheWeekId], [SkipCreationProcess], [Size], [NutritionFocus], [NumberOfDishes], [TypeFavoriteFood], [WantCookingId], [TimeAvailable], [CookingDifficultyId], [Name], [IsActive], [OrderNumber], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber]) VALUES (15, 4, 2, 11, 8, 0, N'Bữa nhỏ', 0, 3, N'1', 1, 5, 1, N'Món nhẹ buổi sáng', 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0)
INSERT [UserData].[MealSettingsDetails] ([Id], [MealSettingsId], [SlotOfTheDayId], [NutritionTargetsDailyId], [DayOfTheWeekId], [SkipCreationProcess], [Size], [NutritionFocus], [NumberOfDishes], [TypeFavoriteFood], [WantCookingId], [TimeAvailable], [CookingDifficultyId], [Name], [IsActive], [OrderNumber], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber]) VALUES (17, 8, 1, NULL, 8, 0, N'Bữa lớn', 0, 3, N'1', 1, 5, 1, N'Món nhẹ buổi sáng', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [UserData].[MealSettingsDetails] ([Id], [MealSettingsId], [SlotOfTheDayId], [NutritionTargetsDailyId], [DayOfTheWeekId], [SkipCreationProcess], [Size], [NutritionFocus], [NumberOfDishes], [TypeFavoriteFood], [WantCookingId], [TimeAvailable], [CookingDifficultyId], [Name], [IsActive], [OrderNumber], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber]) VALUES (18, 9, 1, 12, 8, 0, N'Bữa lớn', 0, 3, N'1', 1, 300, 1, N'Món nhẹ buổi sáng', 1, 1, 491, 0, 122, 0, 54, 0, 122, 7)
INSERT [UserData].[MealSettingsDetails] ([Id], [MealSettingsId], [SlotOfTheDayId], [NutritionTargetsDailyId], [DayOfTheWeekId], [SkipCreationProcess], [Size], [NutritionFocus], [NumberOfDishes], [TypeFavoriteFood], [WantCookingId], [TimeAvailable], [CookingDifficultyId], [Name], [IsActive], [OrderNumber], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber]) VALUES (19, 9, 1, 14, 8, 0, N'Bữa lớn', 0, 3, N'1', 1, 5, 1, N'Món nhẹ buổi sáng thứ 2', 1, 3, 491, NULL, 122, NULL, 54, NULL, 122, 7)
INSERT [UserData].[MealSettingsDetails] ([Id], [MealSettingsId], [SlotOfTheDayId], [NutritionTargetsDailyId], [DayOfTheWeekId], [SkipCreationProcess], [Size], [NutritionFocus], [NumberOfDishes], [TypeFavoriteFood], [WantCookingId], [TimeAvailable], [CookingDifficultyId], [Name], [IsActive], [OrderNumber], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber]) VALUES (20, 9, 1, 13, 8, 0, N'Bữa nhỏ', 0, 3, N'1', 1, 0, 1, N'dfgdfg', 1, 2, 632, NULL, 157, NULL, 70, NULL, 157, 9)
INSERT [UserData].[MealSettingsDetails] ([Id], [MealSettingsId], [SlotOfTheDayId], [NutritionTargetsDailyId], [DayOfTheWeekId], [SkipCreationProcess], [Size], [NutritionFocus], [NumberOfDishes], [TypeFavoriteFood], [WantCookingId], [TimeAvailable], [CookingDifficultyId], [Name], [IsActive], [OrderNumber], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber]) VALUES (21, 9, 1, 15, 8, 0, N'Bữa lớn', 0, 3, N'1', 1, 0, 1, NULL, 1, 4, 491, NULL, 122, NULL, 54, NULL, 122, 7)
INSERT [UserData].[MealSettingsDetails] ([Id], [MealSettingsId], [SlotOfTheDayId], [NutritionTargetsDailyId], [DayOfTheWeekId], [SkipCreationProcess], [Size], [NutritionFocus], [NumberOfDishes], [TypeFavoriteFood], [WantCookingId], [TimeAvailable], [CookingDifficultyId], [Name], [IsActive], [OrderNumber], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber]) VALUES (23, 10, 1, 17, 4, 0, N'Bữa lớn', 0, 3, N'1', 1, 300, 1, N'Món nhẹ buổi sáng', 1, 1, 266, 0, 66, 0, 29, 0, 66, 4)
INSERT [UserData].[MealSettingsDetails] ([Id], [MealSettingsId], [SlotOfTheDayId], [NutritionTargetsDailyId], [DayOfTheWeekId], [SkipCreationProcess], [Size], [NutritionFocus], [NumberOfDishes], [TypeFavoriteFood], [WantCookingId], [TimeAvailable], [CookingDifficultyId], [Name], [IsActive], [OrderNumber], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber]) VALUES (24, 10, 1, 18, 4, 0, N'Bữa lớn', 0, 3, N'1', 1, 9999, 1, N'Món nhẹ buổi sáng', 1, 2, 177, NULL, 133, NULL, 59, NULL, 133, 7)
INSERT [UserData].[MealSettingsDetails] ([Id], [MealSettingsId], [SlotOfTheDayId], [NutritionTargetsDailyId], [DayOfTheWeekId], [SkipCreationProcess], [Size], [NutritionFocus], [NumberOfDishes], [TypeFavoriteFood], [WantCookingId], [TimeAvailable], [CookingDifficultyId], [Name], [IsActive], [OrderNumber], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber]) VALUES (25, 10, 1, 18, 3, 0, N'Bữa lớn', 0, 3, N'1', 1, 300, 1, N'Món nhẹ buổi sáng', 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0)
INSERT [UserData].[MealSettingsDetails] ([Id], [MealSettingsId], [SlotOfTheDayId], [NutritionTargetsDailyId], [DayOfTheWeekId], [SkipCreationProcess], [Size], [NutritionFocus], [NumberOfDishes], [TypeFavoriteFood], [WantCookingId], [TimeAvailable], [CookingDifficultyId], [Name], [IsActive], [OrderNumber], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber]) VALUES (26, 10, 1, 19, 5, 0, N'Bữa lớn', 0, 3, N'1', 1, 9999, 1, N'Món nhẹ buổi sáng', 1, 1, 531, NULL, 132, NULL, 59, NULL, 132, 7)
INSERT [UserData].[MealSettingsDetails] ([Id], [MealSettingsId], [SlotOfTheDayId], [NutritionTargetsDailyId], [DayOfTheWeekId], [SkipCreationProcess], [Size], [NutritionFocus], [NumberOfDishes], [TypeFavoriteFood], [WantCookingId], [TimeAvailable], [CookingDifficultyId], [Name], [IsActive], [OrderNumber], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber]) VALUES (27, 10, 1, 20, 4, 0, N'Bữa lớn', 0, 3, N'1', 1, 9999, 1, N'Món nhẹ buổi sáng thứ 2', 1, 3, 266, 0, 66, 0, 29, 0, 66, 4)
INSERT [UserData].[MealSettingsDetails] ([Id], [MealSettingsId], [SlotOfTheDayId], [NutritionTargetsDailyId], [DayOfTheWeekId], [SkipCreationProcess], [Size], [NutritionFocus], [NumberOfDishes], [TypeFavoriteFood], [WantCookingId], [TimeAvailable], [CookingDifficultyId], [Name], [IsActive], [OrderNumber], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber]) VALUES (28, 10, 1, 21, 6, 0, N'Bữa lớn', 0, 3, N'1', 1, 9999, 1, N'Món nhẹ buổi sáng', 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0)
INSERT [UserData].[MealSettingsDetails] ([Id], [MealSettingsId], [SlotOfTheDayId], [NutritionTargetsDailyId], [DayOfTheWeekId], [SkipCreationProcess], [Size], [NutritionFocus], [NumberOfDishes], [TypeFavoriteFood], [WantCookingId], [TimeAvailable], [CookingDifficultyId], [Name], [IsActive], [OrderNumber], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber]) VALUES (29, 11, 1, 22, 8, 0, N'Bữa lớn', 0, 3, N'1', 1, 9999, 1, NULL, 1, 1, 531, NULL, 132, NULL, 59, NULL, 132, 7)
INSERT [UserData].[MealSettingsDetails] ([Id], [MealSettingsId], [SlotOfTheDayId], [NutritionTargetsDailyId], [DayOfTheWeekId], [SkipCreationProcess], [Size], [NutritionFocus], [NumberOfDishes], [TypeFavoriteFood], [WantCookingId], [TimeAvailable], [CookingDifficultyId], [Name], [IsActive], [OrderNumber], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber]) VALUES (30, 12, 1, 23, 8, 0, N'Bữa lớn', 0, 3, N'1', 1, 9999, 1, N'Món nhẹ buổi sáng', 1, 1, 531, NULL, 132, NULL, 59, NULL, 132, 7)
INSERT [UserData].[MealSettingsDetails] ([Id], [MealSettingsId], [SlotOfTheDayId], [NutritionTargetsDailyId], [DayOfTheWeekId], [SkipCreationProcess], [Size], [NutritionFocus], [NumberOfDishes], [TypeFavoriteFood], [WantCookingId], [TimeAvailable], [CookingDifficultyId], [Name], [IsActive], [OrderNumber], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber]) VALUES (31, 12, 1, 24, 4, 0, N'Bữa lớn', 0, 3, N'1', 1, 9999, 1, N'Món nhẹ buổi sáng', 1, 1, 531, NULL, 132, NULL, 59, NULL, 132, 7)
INSERT [UserData].[MealSettingsDetails] ([Id], [MealSettingsId], [SlotOfTheDayId], [NutritionTargetsDailyId], [DayOfTheWeekId], [SkipCreationProcess], [Size], [NutritionFocus], [NumberOfDishes], [TypeFavoriteFood], [WantCookingId], [TimeAvailable], [CookingDifficultyId], [Name], [IsActive], [OrderNumber], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber]) VALUES (32, 12, 1, 25, 5, 0, N'Bữa lớn', 0, 3, N'1', 1, 9999, 1, N'Món nhẹ buổi sáng', 1, 1, 159, NULL, 39, NULL, 17, NULL, 39, 2)
INSERT [UserData].[MealSettingsDetails] ([Id], [MealSettingsId], [SlotOfTheDayId], [NutritionTargetsDailyId], [DayOfTheWeekId], [SkipCreationProcess], [Size], [NutritionFocus], [NumberOfDishes], [TypeFavoriteFood], [WantCookingId], [TimeAvailable], [CookingDifficultyId], [Name], [IsActive], [OrderNumber], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber]) VALUES (33, 12, 1, 26, 5, 0, N'Bữa lớn', 0, 3, N'1', 1, 9999, 1, NULL, 1, 2, 159, NULL, 39, NULL, 17, NULL, 39, 2)
INSERT [UserData].[MealSettingsDetails] ([Id], [MealSettingsId], [SlotOfTheDayId], [NutritionTargetsDailyId], [DayOfTheWeekId], [SkipCreationProcess], [Size], [NutritionFocus], [NumberOfDishes], [TypeFavoriteFood], [WantCookingId], [TimeAvailable], [CookingDifficultyId], [Name], [IsActive], [OrderNumber], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber]) VALUES (34, 12, 3, 27, 5, 0, N'Bữa lớn', 0, 2, N'1', 1, 9999, 1, NULL, 1, 1, 212, NULL, 53, NULL, 23, NULL, 53, 3)
INSERT [UserData].[MealSettingsDetails] ([Id], [MealSettingsId], [SlotOfTheDayId], [NutritionTargetsDailyId], [DayOfTheWeekId], [SkipCreationProcess], [Size], [NutritionFocus], [NumberOfDishes], [TypeFavoriteFood], [WantCookingId], [TimeAvailable], [CookingDifficultyId], [Name], [IsActive], [OrderNumber], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber]) VALUES (35, 13, 1, 28, 8, 0, N'Bữa lớn', 0, 3, N'1', 1, 9999, 1, N'Món nhẹ buổi sáng', 1, 1, 1731, 0, 133, 0, 59, 0, 133, 7)
INSERT [UserData].[MealSettingsDetails] ([Id], [MealSettingsId], [SlotOfTheDayId], [NutritionTargetsDailyId], [DayOfTheWeekId], [SkipCreationProcess], [Size], [NutritionFocus], [NumberOfDishes], [TypeFavoriteFood], [WantCookingId], [TimeAvailable], [CookingDifficultyId], [Name], [IsActive], [OrderNumber], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber]) VALUES (36, 14, 1, 29, 8, 0, N'Bữa lớn', 0, 3, N'1', 1, 9999, 1, N'Món nhẹ buổi sáng', 1, 1, 1731, 0, 133, 0, 59, 0, 133, 7)
INSERT [UserData].[MealSettingsDetails] ([Id], [MealSettingsId], [SlotOfTheDayId], [NutritionTargetsDailyId], [DayOfTheWeekId], [SkipCreationProcess], [Size], [NutritionFocus], [NumberOfDishes], [TypeFavoriteFood], [WantCookingId], [TimeAvailable], [CookingDifficultyId], [Name], [IsActive], [OrderNumber], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber]) VALUES (37, 17, 1, 30, 6, 0, N'Bữa lớn', 0, 3, N'1', 1, 9999, 1, N'Món nhẹ buổi sáng', 1, 1, 1731, 0, 133, 0, 59, 0, 133, 18)
INSERT [UserData].[MealSettingsDetails] ([Id], [MealSettingsId], [SlotOfTheDayId], [NutritionTargetsDailyId], [DayOfTheWeekId], [SkipCreationProcess], [Size], [NutritionFocus], [NumberOfDishes], [TypeFavoriteFood], [WantCookingId], [TimeAvailable], [CookingDifficultyId], [Name], [IsActive], [OrderNumber], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber]) VALUES (38, 18, 1, 31, 6, 0, N'Bữa lớn', 0, 3, N'1', 1, 9999, 1, N'Món nhẹ buổi sáng', 1, 1, 1731, 0, 133, 0, 59, 0, 133, 27)
INSERT [UserData].[MealSettingsDetails] ([Id], [MealSettingsId], [SlotOfTheDayId], [NutritionTargetsDailyId], [DayOfTheWeekId], [SkipCreationProcess], [Size], [NutritionFocus], [NumberOfDishes], [TypeFavoriteFood], [WantCookingId], [TimeAvailable], [CookingDifficultyId], [Name], [IsActive], [OrderNumber], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber]) VALUES (1035, 10, 1, 1028, 2, 0, N'Bữa lớn', 0, 3, N'1', 1, 9999, 1, NULL, 1, 1, 531, 0, 132, 0, 59, 0, 132, 7)
INSERT [UserData].[MealSettingsDetails] ([Id], [MealSettingsId], [SlotOfTheDayId], [NutritionTargetsDailyId], [DayOfTheWeekId], [SkipCreationProcess], [Size], [NutritionFocus], [NumberOfDishes], [TypeFavoriteFood], [WantCookingId], [TimeAvailable], [CookingDifficultyId], [Name], [IsActive], [OrderNumber], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber]) VALUES (2035, 2013, 1, 2028, 8, 0, N'Bữa lớn', 0, 3, N'1', 1, 9999, 1, N'Món nhẹ buổi sáng', 1, 1, 531, 0, 132, 0, 59, 0, 132, 99)
INSERT [UserData].[MealSettingsDetails] ([Id], [MealSettingsId], [SlotOfTheDayId], [NutritionTargetsDailyId], [DayOfTheWeekId], [SkipCreationProcess], [Size], [NutritionFocus], [NumberOfDishes], [TypeFavoriteFood], [WantCookingId], [TimeAvailable], [CookingDifficultyId], [Name], [IsActive], [OrderNumber], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber]) VALUES (2036, 2024, 1, 2029, 4, 0, N'Bữa lớn', 0, 3, N'1', 1, 9999, 1, N'Món nhẹ buổi sáng', 1, 1, 531, 0, 132, 0, 59, 0, 132, 99)
INSERT [UserData].[MealSettingsDetails] ([Id], [MealSettingsId], [SlotOfTheDayId], [NutritionTargetsDailyId], [DayOfTheWeekId], [SkipCreationProcess], [Size], [NutritionFocus], [NumberOfDishes], [TypeFavoriteFood], [WantCookingId], [TimeAvailable], [CookingDifficultyId], [Name], [IsActive], [OrderNumber], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber]) VALUES (2037, 2027, 1, NULL, 8, 0, N'Bữa lớn', 0, 3, N'1', 1, 9999, 1, N'fgjfg', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [UserData].[MealSettingsDetails] ([Id], [MealSettingsId], [SlotOfTheDayId], [NutritionTargetsDailyId], [DayOfTheWeekId], [SkipCreationProcess], [Size], [NutritionFocus], [NumberOfDishes], [TypeFavoriteFood], [WantCookingId], [TimeAvailable], [CookingDifficultyId], [Name], [IsActive], [OrderNumber], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber]) VALUES (2038, 2027, 1, 2030, 3, 0, N'Bữa lớn', 0, 3, N'1', 1, 9999, 1, NULL, 1, 1, 531, 0, 132, 0, 59, 0, 132, 7)
INSERT [UserData].[MealSettingsDetails] ([Id], [MealSettingsId], [SlotOfTheDayId], [NutritionTargetsDailyId], [DayOfTheWeekId], [SkipCreationProcess], [Size], [NutritionFocus], [NumberOfDishes], [TypeFavoriteFood], [WantCookingId], [TimeAvailable], [CookingDifficultyId], [Name], [IsActive], [OrderNumber], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber]) VALUES (2039, 2029, 1, 2031, 8, 0, N'Bữa lớn', 0, 3, N'1', 1, 9999, 1, NULL, 1, 1, 531, 0, 132, 0, 59, 0, 132, 7)
INSERT [UserData].[MealSettingsDetails] ([Id], [MealSettingsId], [SlotOfTheDayId], [NutritionTargetsDailyId], [DayOfTheWeekId], [SkipCreationProcess], [Size], [NutritionFocus], [NumberOfDishes], [TypeFavoriteFood], [WantCookingId], [TimeAvailable], [CookingDifficultyId], [Name], [IsActive], [OrderNumber], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber]) VALUES (2040, 2030, 1, 2032, 8, 0, N'Bữa lớn', 0, 3, N'1', 1, 9999, 1, N'Món nhẹ buổi sáng', 1, 1, 531, 0, 132, 0, 59, 0, 132, 7)
INSERT [UserData].[MealSettingsDetails] ([Id], [MealSettingsId], [SlotOfTheDayId], [NutritionTargetsDailyId], [DayOfTheWeekId], [SkipCreationProcess], [Size], [NutritionFocus], [NumberOfDishes], [TypeFavoriteFood], [WantCookingId], [TimeAvailable], [CookingDifficultyId], [Name], [IsActive], [OrderNumber], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber]) VALUES (2041, 2030, 1, 2033, 3, 0, N'Bữa lớn', 0, 3, N'1', 1, 9999, 1, N'Món nhẹ buổi sáng', 1, 1, 531, 0, 132, 0, 59, 0, 132, 7)
INSERT [UserData].[MealSettingsDetails] ([Id], [MealSettingsId], [SlotOfTheDayId], [NutritionTargetsDailyId], [DayOfTheWeekId], [SkipCreationProcess], [Size], [NutritionFocus], [NumberOfDishes], [TypeFavoriteFood], [WantCookingId], [TimeAvailable], [CookingDifficultyId], [Name], [IsActive], [OrderNumber], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber]) VALUES (2042, 2030, 1, 2034, 4, 0, N'Bữa lớn', 0, 3, N'1', 1, 9999, 1, NULL, 1, 1, 266, 0, 133, 0, 59, 0, 133, 7)
INSERT [UserData].[MealSettingsDetails] ([Id], [MealSettingsId], [SlotOfTheDayId], [NutritionTargetsDailyId], [DayOfTheWeekId], [SkipCreationProcess], [Size], [NutritionFocus], [NumberOfDishes], [TypeFavoriteFood], [WantCookingId], [TimeAvailable], [CookingDifficultyId], [Name], [IsActive], [OrderNumber], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber]) VALUES (2043, 2030, 1, 2035, 4, 0, N'Bữa lớn', 0, 3, N'1', 1, 9999, 1, NULL, 1, 2, 266, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [UserData].[MealSettingsDetails] ([Id], [MealSettingsId], [SlotOfTheDayId], [NutritionTargetsDailyId], [DayOfTheWeekId], [SkipCreationProcess], [Size], [NutritionFocus], [NumberOfDishes], [TypeFavoriteFood], [WantCookingId], [TimeAvailable], [CookingDifficultyId], [Name], [IsActive], [OrderNumber], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber]) VALUES (3035, 10, 1, 3028, 8, 1, N'Bữa lớn', 0, 3, N'1', 1, 9999, 1, NULL, 1, 1, 159, NULL, 39, NULL, 17, NULL, 39, 2)
INSERT [UserData].[MealSettingsDetails] ([Id], [MealSettingsId], [SlotOfTheDayId], [NutritionTargetsDailyId], [DayOfTheWeekId], [SkipCreationProcess], [Size], [NutritionFocus], [NumberOfDishes], [TypeFavoriteFood], [WantCookingId], [TimeAvailable], [CookingDifficultyId], [Name], [IsActive], [OrderNumber], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber]) VALUES (3036, 3013, 1, 3029, 8, 0, N'Bữa lớn', 0, 3, N'1', 1, 30, 2, N'Bữa Sáng', 1, 1, 539, 0, 134, 0, 59, 0, 134, 8)
INSERT [UserData].[MealSettingsDetails] ([Id], [MealSettingsId], [SlotOfTheDayId], [NutritionTargetsDailyId], [DayOfTheWeekId], [SkipCreationProcess], [Size], [NutritionFocus], [NumberOfDishes], [TypeFavoriteFood], [WantCookingId], [TimeAvailable], [CookingDifficultyId], [Name], [IsActive], [OrderNumber], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber]) VALUES (3037, 3013, 2, 3030, 8, 0, N'Bữa lớn', 0, 3, N'1', 1, 30, 2, N'Bữa Trưa', 1, 1, 629, 0, 157, 0, 69, 0, 157, 9)
INSERT [UserData].[MealSettingsDetails] ([Id], [MealSettingsId], [SlotOfTheDayId], [NutritionTargetsDailyId], [DayOfTheWeekId], [SkipCreationProcess], [Size], [NutritionFocus], [NumberOfDishes], [TypeFavoriteFood], [WantCookingId], [TimeAvailable], [CookingDifficultyId], [Name], [IsActive], [OrderNumber], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber]) VALUES (3038, 3013, 4, 3031, 8, 0, N'Bữa lớn', 0, 3, N'1', 1, 30, 2, N'Bữa Tối', 1, 1, 628, 0, 157, 0, 69, 0, 157, 9)
INSERT [UserData].[MealSettingsDetails] ([Id], [MealSettingsId], [SlotOfTheDayId], [NutritionTargetsDailyId], [DayOfTheWeekId], [SkipCreationProcess], [Size], [NutritionFocus], [NumberOfDishes], [TypeFavoriteFood], [WantCookingId], [TimeAvailable], [CookingDifficultyId], [Name], [IsActive], [OrderNumber], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber]) VALUES (4035, 4013, 1, 4028, 8, 0, N'Bữa lớn', 0, 3, N'1', 1, 30, 2, N'Bữa Sáng', 1, 1, 338, 0, 84, 0, 37, 0, 84, 5)
INSERT [UserData].[MealSettingsDetails] ([Id], [MealSettingsId], [SlotOfTheDayId], [NutritionTargetsDailyId], [DayOfTheWeekId], [SkipCreationProcess], [Size], [NutritionFocus], [NumberOfDishes], [TypeFavoriteFood], [WantCookingId], [TimeAvailable], [CookingDifficultyId], [Name], [IsActive], [OrderNumber], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber]) VALUES (4036, 4013, 2, 4029, 8, 0, N'Bữa lớn', 0, 3, N'1', 1, 9999, 2, N'Bữa Trưa', 1, 1, 788, NULL, 196, NULL, 87, NULL, 196, 11)
INSERT [UserData].[MealSettingsDetails] ([Id], [MealSettingsId], [SlotOfTheDayId], [NutritionTargetsDailyId], [DayOfTheWeekId], [SkipCreationProcess], [Size], [NutritionFocus], [NumberOfDishes], [TypeFavoriteFood], [WantCookingId], [TimeAvailable], [CookingDifficultyId], [Name], [IsActive], [OrderNumber], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber]) VALUES (4037, 4013, 4, 4030, 8, 0, N'Bữa lớn', 0, 3, N'1', 1, 9999, 2, N'Bữa Tối', 1, 1, 788, NULL, 196, NULL, 87, NULL, 196, 11)
INSERT [UserData].[MealSettingsDetails] ([Id], [MealSettingsId], [SlotOfTheDayId], [NutritionTargetsDailyId], [DayOfTheWeekId], [SkipCreationProcess], [Size], [NutritionFocus], [NumberOfDishes], [TypeFavoriteFood], [WantCookingId], [TimeAvailable], [CookingDifficultyId], [Name], [IsActive], [OrderNumber], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber]) VALUES (4038, 4013, 1, 4031, 8, 0, N'Bữa lớn', 0, 3, N'1', 1, 9999, 1, N'Món nhẹ buổi sáng', 1, 2, 338, NULL, 133, NULL, 59, NULL, 133, 56)
INSERT [UserData].[MealSettingsDetails] ([Id], [MealSettingsId], [SlotOfTheDayId], [NutritionTargetsDailyId], [DayOfTheWeekId], [SkipCreationProcess], [Size], [NutritionFocus], [NumberOfDishes], [TypeFavoriteFood], [WantCookingId], [TimeAvailable], [CookingDifficultyId], [Name], [IsActive], [OrderNumber], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber]) VALUES (4039, 4014, 1, 4032, 8, 0, N'Bữa lớn', 0, 3, N'1', 1, 9999, 1, NULL, 1, 1, 331, 0, 133, 0, 59, 0, 133, 7)
INSERT [UserData].[MealSettingsDetails] ([Id], [MealSettingsId], [SlotOfTheDayId], [NutritionTargetsDailyId], [DayOfTheWeekId], [SkipCreationProcess], [Size], [NutritionFocus], [NumberOfDishes], [TypeFavoriteFood], [WantCookingId], [TimeAvailable], [CookingDifficultyId], [Name], [IsActive], [OrderNumber], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber]) VALUES (4040, 4016, 1, 4033, 8, 0, N'Bữa lớn', 0, 3, N'1', 1, 9999, 2, N'Bữa Sáng', 1, 1, 474, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [UserData].[MealSettingsDetails] ([Id], [MealSettingsId], [SlotOfTheDayId], [NutritionTargetsDailyId], [DayOfTheWeekId], [SkipCreationProcess], [Size], [NutritionFocus], [NumberOfDishes], [TypeFavoriteFood], [WantCookingId], [TimeAvailable], [CookingDifficultyId], [Name], [IsActive], [OrderNumber], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber]) VALUES (4041, 4016, 1, 4034, 8, 0, N'Bữa lớn', 0, 3, N'1', 1, 9999, 2, N'Bữa Trưa', 1, 2, 553, NULL, 133, NULL, 59, NULL, 133, 7)
INSERT [UserData].[MealSettingsDetails] ([Id], [MealSettingsId], [SlotOfTheDayId], [NutritionTargetsDailyId], [DayOfTheWeekId], [SkipCreationProcess], [Size], [NutritionFocus], [NumberOfDishes], [TypeFavoriteFood], [WantCookingId], [TimeAvailable], [CookingDifficultyId], [Name], [IsActive], [OrderNumber], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber]) VALUES (4042, 4016, 4, 4035, 8, 0, N'Bữa lớn', 0, 3, N'1', 1, 9999, 2, N'Bữa Tối', 1, 1, 553, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [UserData].[MealSettingsDetails] ([Id], [MealSettingsId], [SlotOfTheDayId], [NutritionTargetsDailyId], [DayOfTheWeekId], [SkipCreationProcess], [Size], [NutritionFocus], [NumberOfDishes], [TypeFavoriteFood], [WantCookingId], [TimeAvailable], [CookingDifficultyId], [Name], [IsActive], [OrderNumber], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber]) VALUES (4043, 10, 1, 4036, 8, 0, N'Bữa lớn', 0, 3, N'1', 1, 9999, 1, NULL, 1, 2, 159, 0, 39, 0, 17, 0, 39, 2)
INSERT [UserData].[MealSettingsDetails] ([Id], [MealSettingsId], [SlotOfTheDayId], [NutritionTargetsDailyId], [DayOfTheWeekId], [SkipCreationProcess], [Size], [NutritionFocus], [NumberOfDishes], [TypeFavoriteFood], [WantCookingId], [TimeAvailable], [CookingDifficultyId], [Name], [IsActive], [OrderNumber], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber]) VALUES (4044, 10, 3, 4037, 8, 0, N'Bữa lớn', 0, 3, N'1', 1, 9999, 1, NULL, 1, 1, 212, 0, 53, 0, 23, 0, 53, 3)
INSERT [UserData].[MealSettingsDetails] ([Id], [MealSettingsId], [SlotOfTheDayId], [NutritionTargetsDailyId], [DayOfTheWeekId], [SkipCreationProcess], [Size], [NutritionFocus], [NumberOfDishes], [TypeFavoriteFood], [WantCookingId], [TimeAvailable], [CookingDifficultyId], [Name], [IsActive], [OrderNumber], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber]) VALUES (4045, 4017, 1, 4038, 8, 0, N'Bữa lớn', 0, 3, N'1', 1, 9999, 1, N'Bữa Sáng', 1, 1, 564, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [UserData].[MealSettingsDetails] ([Id], [MealSettingsId], [SlotOfTheDayId], [NutritionTargetsDailyId], [DayOfTheWeekId], [SkipCreationProcess], [Size], [NutritionFocus], [NumberOfDishes], [TypeFavoriteFood], [WantCookingId], [TimeAvailable], [CookingDifficultyId], [Name], [IsActive], [OrderNumber], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber]) VALUES (4046, 4017, 2, 4039, 8, 0, N'Bữa lớn', 0, 3, N'1', 1, 30, 2, N'Bữa Trưa', 1, 1, 658, 0, 164, 0, 73, 0, 164, 9)
INSERT [UserData].[MealSettingsDetails] ([Id], [MealSettingsId], [SlotOfTheDayId], [NutritionTargetsDailyId], [DayOfTheWeekId], [SkipCreationProcess], [Size], [NutritionFocus], [NumberOfDishes], [TypeFavoriteFood], [WantCookingId], [TimeAvailable], [CookingDifficultyId], [Name], [IsActive], [OrderNumber], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber]) VALUES (4047, 4017, 4, 4040, 8, 0, N'Bữa lớn', 0, 3, N'1', 1, 30, 2, N'Bữa Tối', 1, 1, 658, 0, 164, 0, 73, 0, 164, 9)
SET IDENTITY_INSERT [UserData].[MealSettingsDetails] OFF
GO
SET IDENTITY_INSERT [UserData].[NutritionistDetails] ON 

INSERT [UserData].[NutritionistDetails] ([Id], [NutritionistId], [DescribeYourself], [Height], [Weight], [Age], [Rate], [NumberRate], [ExpertPackagesId]) VALUES (1, 2, N'Siêu Tốt', 43, 43, 43, 89.8, 54, 1)
INSERT [UserData].[NutritionistDetails] ([Id], [NutritionistId], [DescribeYourself], [Height], [Weight], [Age], [Rate], [NumberRate], [ExpertPackagesId]) VALUES (3, 3, N'Siêu Tốt', 43, 43, 43, 89.8, 54, 1)
INSERT [UserData].[NutritionistDetails] ([Id], [NutritionistId], [DescribeYourself], [Height], [Weight], [Age], [Rate], [NumberRate], [ExpertPackagesId]) VALUES (15, 4, N'Siêu Tốt', 43, 43, 43, 89.8, 54, 3)
SET IDENTITY_INSERT [UserData].[NutritionistDetails] OFF
GO
SET IDENTITY_INSERT [UserData].[NutritionTargetsDaily] ON 

INSERT [UserData].[NutritionTargetsDaily] ([Id], [UserId], [Title], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber], [LimitDailySodium], [LimitDailyCholesterol], [ExerciseIntensityId], [AvoidIngredient], [IsActive], [FoodTypeIdWant]) VALUES (1, 1, N'Daily Nutrition Target', 325, 0, 70, 0, 500, 0, 500, 40, 0, 0, 1, N'5;2', NULL, 1)
INSERT [UserData].[NutritionTargetsDaily] ([Id], [UserId], [Title], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber], [LimitDailySodium], [LimitDailyCholesterol], [ExerciseIntensityId], [AvoidIngredient], [IsActive], [FoodTypeIdWant]) VALUES (2, 2, N'Muscle Building Target', 2500, 0, 500, 0, 5000, 0, 500, 40, 1, 1, 2, N'2;3;', NULL, 1)
INSERT [UserData].[NutritionTargetsDaily] ([Id], [UserId], [Title], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber], [LimitDailySodium], [LimitDailyCholesterol], [ExerciseIntensityId], [AvoidIngredient], [IsActive], [FoodTypeIdWant]) VALUES (3, 3, N'Weight Loss Target', 1500, 40, 70, 40, 70, 40, 70, 40, 1, 1, 3, N'4;', NULL, 1)
INSERT [UserData].[NutritionTargetsDaily] ([Id], [UserId], [Title], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber], [LimitDailySodium], [LimitDailyCholesterol], [ExerciseIntensityId], [AvoidIngredient], [IsActive], [FoodTypeIdWant]) VALUES (4, 1, N'Balanced Diet Target', 531, 9, 70, 9, 70, 9, 70, 40, 1, 1, 4, N'5;2', NULL, 1)
INSERT [UserData].[NutritionTargetsDaily] ([Id], [UserId], [Title], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber], [LimitDailySodium], [LimitDailyCholesterol], [ExerciseIntensityId], [AvoidIngredient], [IsActive], [FoodTypeIdWant]) VALUES (5, 32, N'Balanced Diet Target', 531, 9, 92, 9, 41, 9, 92, 5, 1, 1, 4, N'5;2', 1, 1)
INSERT [UserData].[NutritionTargetsDaily] ([Id], [UserId], [Title], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber], [LimitDailySodium], [LimitDailyCholesterol], [ExerciseIntensityId], [AvoidIngredient], [IsActive], [FoodTypeIdWant]) VALUES (6, 32, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, NULL, 0, 1)
INSERT [UserData].[NutritionTargetsDaily] ([Id], [UserId], [Title], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber], [LimitDailySodium], [LimitDailyCholesterol], [ExerciseIntensityId], [AvoidIngredient], [IsActive], [FoodTypeIdWant]) VALUES (7, 32, NULL, 212, 0, 53, 0, 23, 0, 53, 3, 0, 0, 1, NULL, 1, 1)
INSERT [UserData].[NutritionTargetsDaily] ([Id], [UserId], [Title], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber], [LimitDailySodium], [LimitDailyCholesterol], [ExerciseIntensityId], [AvoidIngredient], [IsActive], [FoodTypeIdWant]) VALUES (8, 32, NULL, 266, 0, 66, 0, 29, 0, 66, 4, 0, 0, 1, NULL, 1, 1)
INSERT [UserData].[NutritionTargetsDaily] ([Id], [UserId], [Title], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber], [LimitDailySodium], [LimitDailyCholesterol], [ExerciseIntensityId], [AvoidIngredient], [IsActive], [FoodTypeIdWant]) VALUES (9, 32, NULL, 266, 0, 66, 0, 29, 0, 66, 4, NULL, NULL, 1, NULL, 1, 1)
INSERT [UserData].[NutritionTargetsDaily] ([Id], [UserId], [Title], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber], [LimitDailySodium], [LimitDailyCholesterol], [ExerciseIntensityId], [AvoidIngredient], [IsActive], [FoodTypeIdWant]) VALUES (10, 32, NULL, 159, 0, 39, 0, 17, 0, 39, 2, NULL, NULL, 1, NULL, 1, 1)
INSERT [UserData].[NutritionTargetsDaily] ([Id], [UserId], [Title], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber], [LimitDailySodium], [LimitDailyCholesterol], [ExerciseIntensityId], [AvoidIngredient], [IsActive], [FoodTypeIdWant]) VALUES (11, 40, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, NULL, 0, 1)
INSERT [UserData].[NutritionTargetsDaily] ([Id], [UserId], [Title], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber], [LimitDailySodium], [LimitDailyCholesterol], [ExerciseIntensityId], [AvoidIngredient], [IsActive], [FoodTypeIdWant]) VALUES (12, 44, NULL, 531, 0, 122, 0, 54, 0, 122, 7, 0, 0, 1, NULL, 1, 1)
INSERT [UserData].[NutritionTargetsDaily] ([Id], [UserId], [Title], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber], [LimitDailySodium], [LimitDailyCholesterol], [ExerciseIntensityId], [AvoidIngredient], [IsActive], [FoodTypeIdWant]) VALUES (13, 44, NULL, 632, 0, 157, 0, 70, 0, 157, 9, 0, 0, 1, NULL, 1, 1)
INSERT [UserData].[NutritionTargetsDaily] ([Id], [UserId], [Title], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber], [LimitDailySodium], [LimitDailyCholesterol], [ExerciseIntensityId], [AvoidIngredient], [IsActive], [FoodTypeIdWant]) VALUES (14, 44, NULL, 491, 0, 122, 0, 54, 0, 122, 7, 0, 0, 1, NULL, 1, 1)
INSERT [UserData].[NutritionTargetsDaily] ([Id], [UserId], [Title], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber], [LimitDailySodium], [LimitDailyCholesterol], [ExerciseIntensityId], [AvoidIngredient], [IsActive], [FoodTypeIdWant]) VALUES (15, 44, NULL, 491, 0, 122, 0, 54, 0, 122, 7, 0, 0, 1, NULL, 1, 1)
INSERT [UserData].[NutritionTargetsDaily] ([Id], [UserId], [Title], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber], [LimitDailySodium], [LimitDailyCholesterol], [ExerciseIntensityId], [AvoidIngredient], [IsActive], [FoodTypeIdWant]) VALUES (17, 45, NULL, 266, 0, 66, 0, 29, 0, 66, 4, 0, 0, 1, NULL, 1, 1)
INSERT [UserData].[NutritionTargetsDaily] ([Id], [UserId], [Title], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber], [LimitDailySodium], [LimitDailyCholesterol], [ExerciseIntensityId], [AvoidIngredient], [IsActive], [FoodTypeIdWant]) VALUES (18, 45, NULL, 531, 0, 133, 0, 59, 0, 133, 7, 0, 0, 1, NULL, 0, 1)
INSERT [UserData].[NutritionTargetsDaily] ([Id], [UserId], [Title], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber], [LimitDailySodium], [LimitDailyCholesterol], [ExerciseIntensityId], [AvoidIngredient], [IsActive], [FoodTypeIdWant]) VALUES (19, 45, NULL, 531, 0, 132, 0, 59, 0, 132, 7, 0, 0, 0, NULL, 1, 1)
INSERT [UserData].[NutritionTargetsDaily] ([Id], [UserId], [Title], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber], [LimitDailySodium], [LimitDailyCholesterol], [ExerciseIntensityId], [AvoidIngredient], [IsActive], [FoodTypeIdWant]) VALUES (20, 45, NULL, 266, 0, 66, 0, 29, 0, 66, 4, 0, 0, 1, NULL, 1, 1)
INSERT [UserData].[NutritionTargetsDaily] ([Id], [UserId], [Title], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber], [LimitDailySodium], [LimitDailyCholesterol], [ExerciseIntensityId], [AvoidIngredient], [IsActive], [FoodTypeIdWant]) VALUES (21, 45, NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, NULL, 0, 1)
INSERT [UserData].[NutritionTargetsDaily] ([Id], [UserId], [Title], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber], [LimitDailySodium], [LimitDailyCholesterol], [ExerciseIntensityId], [AvoidIngredient], [IsActive], [FoodTypeIdWant]) VALUES (22, 46, NULL, 531, 0, 132, 0, 59, 0, 132, 7, 0, 0, 0, NULL, 1, 1)
INSERT [UserData].[NutritionTargetsDaily] ([Id], [UserId], [Title], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber], [LimitDailySodium], [LimitDailyCholesterol], [ExerciseIntensityId], [AvoidIngredient], [IsActive], [FoodTypeIdWant]) VALUES (23, 47, NULL, 531, 0, 132, 0, 59, 0, 132, 7, 0, 0, 0, NULL, 1, 1)
INSERT [UserData].[NutritionTargetsDaily] ([Id], [UserId], [Title], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber], [LimitDailySodium], [LimitDailyCholesterol], [ExerciseIntensityId], [AvoidIngredient], [IsActive], [FoodTypeIdWant]) VALUES (24, 47, NULL, 531, 0, 132, 0, 59, 0, 132, 7, 0, 0, 0, NULL, 1, 1)
INSERT [UserData].[NutritionTargetsDaily] ([Id], [UserId], [Title], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber], [LimitDailySodium], [LimitDailyCholesterol], [ExerciseIntensityId], [AvoidIngredient], [IsActive], [FoodTypeIdWant]) VALUES (25, 47, NULL, 159, 0, 39, 0, 17, 0, 39, 2, 0, 0, 0, NULL, 1, 1)
INSERT [UserData].[NutritionTargetsDaily] ([Id], [UserId], [Title], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber], [LimitDailySodium], [LimitDailyCholesterol], [ExerciseIntensityId], [AvoidIngredient], [IsActive], [FoodTypeIdWant]) VALUES (26, 47, NULL, 159, 0, 39, 0, 17, 0, 39, 2, 0, 0, 1, NULL, 1, 1)
INSERT [UserData].[NutritionTargetsDaily] ([Id], [UserId], [Title], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber], [LimitDailySodium], [LimitDailyCholesterol], [ExerciseIntensityId], [AvoidIngredient], [IsActive], [FoodTypeIdWant]) VALUES (27, 47, NULL, 531, 0, 53, 0, 23, 0, 53, 3, 0, 0, 1, NULL, 1, 1)
INSERT [UserData].[NutritionTargetsDaily] ([Id], [UserId], [Title], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber], [LimitDailySodium], [LimitDailyCholesterol], [ExerciseIntensityId], [AvoidIngredient], [IsActive], [FoodTypeIdWant]) VALUES (28, 48, NULL, 531, 0, 133, 0, 59, 0, 133, 7, 0, 0, 0, NULL, 1, 1)
INSERT [UserData].[NutritionTargetsDaily] ([Id], [UserId], [Title], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber], [LimitDailySodium], [LimitDailyCholesterol], [ExerciseIntensityId], [AvoidIngredient], [IsActive], [FoodTypeIdWant]) VALUES (29, 49, NULL, 531, 0, 133, 0, 59, 0, 133, 7, 0, 0, 0, NULL, 1, 1)
INSERT [UserData].[NutritionTargetsDaily] ([Id], [UserId], [Title], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber], [LimitDailySodium], [LimitDailyCholesterol], [ExerciseIntensityId], [AvoidIngredient], [IsActive], [FoodTypeIdWant]) VALUES (30, 56, NULL, 531, 0, 133, 0, 59, 0, 133, 18, 0, 0, 0, NULL, 1, 1)
INSERT [UserData].[NutritionTargetsDaily] ([Id], [UserId], [Title], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber], [LimitDailySodium], [LimitDailyCholesterol], [ExerciseIntensityId], [AvoidIngredient], [IsActive], [FoodTypeIdWant]) VALUES (31, 57, NULL, 531, 0, 133, 0, 59, 0, 133, 27, 0, 0, 0, NULL, 1, 1)
INSERT [UserData].[NutritionTargetsDaily] ([Id], [UserId], [Title], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber], [LimitDailySodium], [LimitDailyCholesterol], [ExerciseIntensityId], [AvoidIngredient], [IsActive], [FoodTypeIdWant]) VALUES (1028, 45, NULL, 531, 0, 132, 0, 59, 0, 132, 7, 0, 0, 0, NULL, 1, 1)
INSERT [UserData].[NutritionTargetsDaily] ([Id], [UserId], [Title], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber], [LimitDailySodium], [LimitDailyCholesterol], [ExerciseIntensityId], [AvoidIngredient], [IsActive], [FoodTypeIdWant]) VALUES (2028, 3056, NULL, 531, 0, 132, 0, 59, 0, 132, 99, 0, 0, 0, NULL, 1, 1)
INSERT [UserData].[NutritionTargetsDaily] ([Id], [UserId], [Title], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber], [LimitDailySodium], [LimitDailyCholesterol], [ExerciseIntensityId], [AvoidIngredient], [IsActive], [FoodTypeIdWant]) VALUES (2029, 3078, NULL, 531, 0, 132, 0, 59, 0, 132, 99, 0, 0, 0, NULL, 1, 1)
INSERT [UserData].[NutritionTargetsDaily] ([Id], [UserId], [Title], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber], [LimitDailySodium], [LimitDailyCholesterol], [ExerciseIntensityId], [AvoidIngredient], [IsActive], [FoodTypeIdWant]) VALUES (2030, 3081, NULL, 531, 0, 132, 0, 59, 0, 132, 7, 0, 0, 0, NULL, 1, 1)
INSERT [UserData].[NutritionTargetsDaily] ([Id], [UserId], [Title], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber], [LimitDailySodium], [LimitDailyCholesterol], [ExerciseIntensityId], [AvoidIngredient], [IsActive], [FoodTypeIdWant]) VALUES (2031, 3083, NULL, 531, 0, 132, 0, 59, 0, 132, 7, 0, 0, 0, NULL, 1, 1)
INSERT [UserData].[NutritionTargetsDaily] ([Id], [UserId], [Title], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber], [LimitDailySodium], [LimitDailyCholesterol], [ExerciseIntensityId], [AvoidIngredient], [IsActive], [FoodTypeIdWant]) VALUES (2032, 3084, NULL, 531, 0, 132, 0, 59, 0, 132, 7, 0, 0, 0, NULL, 1, 1)
INSERT [UserData].[NutritionTargetsDaily] ([Id], [UserId], [Title], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber], [LimitDailySodium], [LimitDailyCholesterol], [ExerciseIntensityId], [AvoidIngredient], [IsActive], [FoodTypeIdWant]) VALUES (2033, 3084, NULL, 531, 0, 132, 0, 59, 0, 132, 7, 0, 0, 0, NULL, 1, 1)
INSERT [UserData].[NutritionTargetsDaily] ([Id], [UserId], [Title], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber], [LimitDailySodium], [LimitDailyCholesterol], [ExerciseIntensityId], [AvoidIngredient], [IsActive], [FoodTypeIdWant]) VALUES (2034, 3084, NULL, 531, 0, 66, 0, 29, 0, 66, 7, 0, 0, 0, NULL, 1, 1)
INSERT [UserData].[NutritionTargetsDaily] ([Id], [UserId], [Title], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber], [LimitDailySodium], [LimitDailyCholesterol], [ExerciseIntensityId], [AvoidIngredient], [IsActive], [FoodTypeIdWant]) VALUES (2035, 3084, NULL, 531, 0, 66, 0, 29, 0, 66, 7, 0, 0, 1, NULL, 1, 1)
INSERT [UserData].[NutritionTargetsDaily] ([Id], [UserId], [Title], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber], [LimitDailySodium], [LimitDailyCholesterol], [ExerciseIntensityId], [AvoidIngredient], [IsActive], [FoodTypeIdWant]) VALUES (3028, 45, NULL, 159, 0, 39, 0, 17, 0, 39, 2, 0, 0, 0, NULL, 1, 1)
INSERT [UserData].[NutritionTargetsDaily] ([Id], [UserId], [Title], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber], [LimitDailySodium], [LimitDailyCholesterol], [ExerciseIntensityId], [AvoidIngredient], [IsActive], [FoodTypeIdWant]) VALUES (3029, 4056, NULL, 539, 0, 134, 0, 59, 0, 134, 8, 0, 0, 0, NULL, 1, 1)
INSERT [UserData].[NutritionTargetsDaily] ([Id], [UserId], [Title], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber], [LimitDailySodium], [LimitDailyCholesterol], [ExerciseIntensityId], [AvoidIngredient], [IsActive], [FoodTypeIdWant]) VALUES (3030, 4056, NULL, 629, 0, 157, 0, 69, 0, 157, 9, 0, 0, 0, NULL, 1, 1)
INSERT [UserData].[NutritionTargetsDaily] ([Id], [UserId], [Title], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber], [LimitDailySodium], [LimitDailyCholesterol], [ExerciseIntensityId], [AvoidIngredient], [IsActive], [FoodTypeIdWant]) VALUES (3031, 4056, NULL, 628, 0, 157, 0, 69, 0, 157, 9, 0, 0, 0, NULL, 1, 1)
INSERT [UserData].[NutritionTargetsDaily] ([Id], [UserId], [Title], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber], [LimitDailySodium], [LimitDailyCholesterol], [ExerciseIntensityId], [AvoidIngredient], [IsActive], [FoodTypeIdWant]) VALUES (4028, 5056, NULL, 338, 0, 84, 0, 37, 0, 84, 5, 0, 0, 0, NULL, 1, 2)
INSERT [UserData].[NutritionTargetsDaily] ([Id], [UserId], [Title], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber], [LimitDailySodium], [LimitDailyCholesterol], [ExerciseIntensityId], [AvoidIngredient], [IsActive], [FoodTypeIdWant]) VALUES (4029, 5056, NULL, 788, 0, 196, 0, 87, 0, 196, 11, 0, 0, 0, NULL, 1, 1)
INSERT [UserData].[NutritionTargetsDaily] ([Id], [UserId], [Title], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber], [LimitDailySodium], [LimitDailyCholesterol], [ExerciseIntensityId], [AvoidIngredient], [IsActive], [FoodTypeIdWant]) VALUES (4030, 5056, NULL, 788, 0, 196, 0, 87, 0, 196, 11, 0, 0, 0, NULL, 1, 1)
INSERT [UserData].[NutritionTargetsDaily] ([Id], [UserId], [Title], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber], [LimitDailySodium], [LimitDailyCholesterol], [ExerciseIntensityId], [AvoidIngredient], [IsActive], [FoodTypeIdWant]) VALUES (4031, 5056, NULL, 531, 0, 133, 0, 59, 0, 133, 56, 0, 0, 1, NULL, 1, 1)
INSERT [UserData].[NutritionTargetsDaily] ([Id], [UserId], [Title], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber], [LimitDailySodium], [LimitDailyCholesterol], [ExerciseIntensityId], [AvoidIngredient], [IsActive], [FoodTypeIdWant]) VALUES (4032, 5057, NULL, 531, 0, 82, 0, 36, 0, 82, 7, 0, 0, 0, NULL, 1, 1)
INSERT [UserData].[NutritionTargetsDaily] ([Id], [UserId], [Title], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber], [LimitDailySodium], [LimitDailyCholesterol], [ExerciseIntensityId], [AvoidIngredient], [IsActive], [FoodTypeIdWant]) VALUES (4033, 5059, NULL, 531, 0, 118, 0, 52, 0, 118, 99, 0, 0, 0, NULL, 1, 1)
INSERT [UserData].[NutritionTargetsDaily] ([Id], [UserId], [Title], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber], [LimitDailySodium], [LimitDailyCholesterol], [ExerciseIntensityId], [AvoidIngredient], [IsActive], [FoodTypeIdWant]) VALUES (4034, 5059, NULL, 531, 0, 133, 0, 59, 0, 133, 7, 0, 0, 0, NULL, 1, 1)
INSERT [UserData].[NutritionTargetsDaily] ([Id], [UserId], [Title], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber], [LimitDailySodium], [LimitDailyCholesterol], [ExerciseIntensityId], [AvoidIngredient], [IsActive], [FoodTypeIdWant]) VALUES (4035, 5059, NULL, 531, 0, 133, 0, 59, 0, 133, 7, 0, 0, 0, NULL, 1, 1)
INSERT [UserData].[NutritionTargetsDaily] ([Id], [UserId], [Title], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber], [LimitDailySodium], [LimitDailyCholesterol], [ExerciseIntensityId], [AvoidIngredient], [IsActive], [FoodTypeIdWant]) VALUES (4036, 45, NULL, 159, 0, 39, 0, 17, 0, 39, 2, 0, 0, 1, NULL, 1, 1)
INSERT [UserData].[NutritionTargetsDaily] ([Id], [UserId], [Title], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber], [LimitDailySodium], [LimitDailyCholesterol], [ExerciseIntensityId], [AvoidIngredient], [IsActive], [FoodTypeIdWant]) VALUES (4037, 45, NULL, 212, 0, 53, 0, 23, 0, 53, 3, 0, 0, 0, NULL, 1, 1)
INSERT [UserData].[NutritionTargetsDaily] ([Id], [UserId], [Title], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber], [LimitDailySodium], [LimitDailyCholesterol], [ExerciseIntensityId], [AvoidIngredient], [IsActive], [FoodTypeIdWant]) VALUES (4038, 5060, NULL, 531, 3, 133, 3, 59, 3, 133, 99, 0, 0, 0, NULL, 1, 1)
INSERT [UserData].[NutritionTargetsDaily] ([Id], [UserId], [Title], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber], [LimitDailySodium], [LimitDailyCholesterol], [ExerciseIntensityId], [AvoidIngredient], [IsActive], [FoodTypeIdWant]) VALUES (4039, 5060, NULL, 658, 0, 164, 0, 73, 0, 164, 9, 0, 0, 0, NULL, 1, 1)
INSERT [UserData].[NutritionTargetsDaily] ([Id], [UserId], [Title], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber], [LimitDailySodium], [LimitDailyCholesterol], [ExerciseIntensityId], [AvoidIngredient], [IsActive], [FoodTypeIdWant]) VALUES (4040, 5060, NULL, 658, 0, 164, 0, 73, 0, 164, 9, 0, 0, 0, NULL, 1, 1)
SET IDENTITY_INSERT [UserData].[NutritionTargetsDaily] OFF
GO
SET IDENTITY_INSERT [UserData].[RecurringSettings] ON 

INSERT [UserData].[RecurringSettings] ([Id], [Name], [Frequency]) VALUES (1, N'Daily', 1)
INSERT [UserData].[RecurringSettings] ([Id], [Name], [Frequency]) VALUES (2, N'Weekly', 7)
INSERT [UserData].[RecurringSettings] ([Id], [Name], [Frequency]) VALUES (3, N'Monthly', 30)
INSERT [UserData].[RecurringSettings] ([Id], [Name], [Frequency]) VALUES (4, N'Yearly', 365)
SET IDENTITY_INSERT [UserData].[RecurringSettings] OFF
GO
SET IDENTITY_INSERT [UserData].[Role] ON 

INSERT [UserData].[Role] ([RoleID], [RoleName]) VALUES (1, N'Admin')
INSERT [UserData].[Role] ([RoleID], [RoleName]) VALUES (2, N'Nutritionist')
INSERT [UserData].[Role] ([RoleID], [RoleName]) VALUES (3, N'UserPremium')
INSERT [UserData].[Role] ([RoleID], [RoleName]) VALUES (4, N'User')
INSERT [UserData].[Role] ([RoleID], [RoleName]) VALUES (5, N'Guest')
SET IDENTITY_INSERT [UserData].[Role] OFF
GO
SET IDENTITY_INSERT [UserData].[User] ON 

INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account], [AccountGoogle]) VALUES (1, N'~/images/faces/face4.jpg', N'John', N'Doe', NULL, NULL, NULL, N'', N'$2a$11$TrQAd4xIgn8fbMuPSBS0eO9lkUWhYHsb263LevWmxtOvS1YUOixZC', 1, 1, N'Admin', NULL)
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account], [AccountGoogle]) VALUES (2, N'/images/user2.jpg', N'Jane', N'Smith', NULL, NULL, NULL, NULL, N'$2a$11$TrQAd4xIgn8fbMuPSBS0eO9lkUWhYHsb263LevWmxtOvS1YUOixZC', 2, 1, N'bbbbbb', NULL)
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account], [AccountGoogle]) VALUES (3, N'/images/user3.jpg', N'Emily', N'Brown', NULL, NULL, NULL, NULL, N'$2a$11$TrQAd4xIgn8fbMuPSBS0eO9lkUWhYHsb263LevWmxtOvS1YUOixZC', 2, 1, N'example@example.com', NULL)
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account], [AccountGoogle]) VALUES (4, N'/images/user4.jpg', N'Michael', N'Johnson', NULL, NULL, NULL, NULL, N'$2a$11$TrQAd4xIgn8fbMuPSBS0eO9lkUWhYHsb263LevWmxtOvS1YUOixZC', 2, 0, N'example2@example.com', NULL)
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account], [AccountGoogle]) VALUES (32, NULL, N'Tan', N'a', NULL, NULL, NULL, NULL, N'tan', 4, 1, N'tan', NULL)
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account], [AccountGoogle]) VALUES (39, NULL, N'Tan', N'b', NULL, NULL, NULL, NULL, N'tanTMT', 4, 1, N'tanTMT', NULL)
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account], [AccountGoogle]) VALUES (40, NULL, N'Tan', N'NULLd', NULL, 1, NULL, NULL, N'tantest4', 4, 1, N'tantest4', NULL)
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account], [AccountGoogle]) VALUES (42, NULL, N'Tan', N'3', NULL, NULL, NULL, NULL, N'banko loi', 4, 1, N'banko loi', NULL)
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account], [AccountGoogle]) VALUES (43, NULL, N'Tan', N'gf', NULL, NULL, NULL, NULL, N'dangdungdeTest', 4, 1, N'dangdungdeTest', NULL)
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account], [AccountGoogle]) VALUES (44, NULL, N'Tan', N'TanTestAllWeek Main', CAST(N'2002-11-12T00:00:00.000' AS DateTime), 1, NULL, NULL, N'amamamamam', 4, 1, N'amamamamam', NULL)
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account], [AccountGoogle]) VALUES (45, NULL, N'Tan', N'TanTestAllWeek', NULL, 1, NULL, NULL, N'tanTestAllWeek', 3, 1, N'tanTestAllWeek', NULL)
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account], [AccountGoogle]) VALUES (46, NULL, N'Tan', N'hfg', NULL, 1, NULL, NULL, N'Test12/11', 4, 1, N'Test12/11', NULL)
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account], [AccountGoogle]) VALUES (47, NULL, N'Tan', N'h', NULL, 1, NULL, NULL, N'testTack12/11', 3, 1, N'testTack12/11', NULL)
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account], [AccountGoogle]) VALUES (48, NULL, N'Tan', N'hjk', NULL, 1, NULL, NULL, N'TanTestPestium', 3, 1, N'TanTestPestium', NULL)
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account], [AccountGoogle]) VALUES (49, NULL, N'Tan', N'bnn', NULL, 1, NULL, NULL, N'TanTest21_11', 3, 1, N'TanTest21_11', NULL)
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account], [AccountGoogle]) VALUES (50, NULL, N'Tan', N'jh', NULL, NULL, NULL, NULL, N'TanTestRegisterNoCallLogin', 4, 1, N'TanTestRegisterNoCallLogin', NULL)
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account], [AccountGoogle]) VALUES (54, NULL, N'Tan', N'jk', NULL, NULL, NULL, NULL, N'MMMMMMMMMMMMM', 4, NULL, N'@TestGmail', N'@TestGmail')
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account], [AccountGoogle]) VALUES (55, NULL, N'Tan', N'kj', NULL, 1, NULL, NULL, N'Ebx3DKkDJcPwoaWuGuUN', 3, 1, N'minhtanbeater@gmail.com', N'minhtanbeater@gmail.com')
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account], [AccountGoogle]) VALUES (56, NULL, NULL, NULL, NULL, 1, NULL, NULL, N'Gfro8pXmUKePYRSI13pN', 3, 1, N'minhtantmt2k2@gmail.com', N'minhtantmt2k2@gmail.com')
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account], [AccountGoogle]) VALUES (57, NULL, NULL, NULL, NULL, 1, NULL, NULL, N'1SBKGZgAD0nRS02xnv2e', 3, 1, N'deluudulieu1@gmail.com', N'deluudulieu1@gmail.com')
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account], [AccountGoogle]) VALUES (1056, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'qnBQzHjXHKO6nPG4/q0CSPU1nhECmfdK8/ZZr2AdFGk=', 3, 1, N'tantantan2', NULL)
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account], [AccountGoogle]) VALUES (1057, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'PkA1IcssEdgamPuGKoqZ', 4, 1, N'deluudulieu7@gmail.com', N'deluudulieu7@gmail.com')
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account], [AccountGoogle]) VALUES (2061, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'MQ8v/dtxQp9nz0ytumO3j4DIT5CEN8aS1iFP/fisbsM=', 4, NULL, N'tantestmahoa', NULL)
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account], [AccountGoogle]) VALUES (3056, NULL, NULL, NULL, NULL, 1, NULL, NULL, N'testphancuadung', 4, 1, N'testphancuadung', NULL)
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account], [AccountGoogle]) VALUES (3067, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'2BBJRU2OIRZvzKOWTWJm/kNMyYq6/ELuPRHrQGiRwmE=', 4, 1, N'tantestmaha3', NULL)
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account], [AccountGoogle]) VALUES (3068, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Kfh4Y+f4hU7aJsypTTy4DldKqo/e9g5FxIJxOea+qPc=', 4, 1, N'tantestmaha4', NULL)
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account], [AccountGoogle]) VALUES (3069, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'h8MrrDakBPIhy61kHgOADFYCTXo7hmfw4B5VTBz9ZbQ=', 4, 1, N'tantestmaha5', NULL)
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account], [AccountGoogle]) VALUES (3073, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'gq04Y5zSk2sUQ9MaOLbLW7Kdhxeq7xHFSH1f6WoA784VMzxj4qGLQswjeARoRaKD', 4, 1, N'deluudulieu6@gmail.com', N'deluudulieu6@gmail.com')
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account], [AccountGoogle]) VALUES (3075, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'4rlSEX3DAlhIuGnMoYUKovOnleoD09WfC1UwDCc+zHZyDwhITJTMtZ56CppDU4Am', 4, 1, N'kjnhkhjkl', N'ghjgj')
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account], [AccountGoogle]) VALUES (3076, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'80+OqXIvTPibkRqMl+rB6+NS9TAVP3PYqk22AK1NcIdT7N4lyodFJuAuT6wj/+Nr', 4, 1, N'deluudulieu8@gmail.com', N'deluudulieu8@gmail.com')
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account], [AccountGoogle]) VALUES (3077, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'biloi2lotrinh', 4, 1, N'biloi2lotrinh', NULL)
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account], [AccountGoogle]) VALUES (3078, NULL, N'test2lotrinh', N'test2lotrinh', CAST(N'2024-10-03T00:00:00.000' AS DateTime), 1, N'gfhfg', N'fgjgfj', N'testbitao2lotrinh', 3, 1, N'testbitao2lotrinh', NULL)
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account], [AccountGoogle]) VALUES (3079, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'tantestbiloifoodgen', 4, 1, N'tantestbiloifoodgen', NULL)
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account], [AccountGoogle]) VALUES (3080, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'tantestmahoamdfmdf', 4, 1, N'tantestmahoamdfmdf', NULL)
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account], [AccountGoogle]) VALUES (3081, NULL, NULL, NULL, NULL, 1, NULL, NULL, N'jkfgjkldfngkjldfnkgdnkfhg', 3, 1, N'jkfgjkldfngkjldfnkgdnkfhg', NULL)
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account], [AccountGoogle]) VALUES (3082, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'testlotrinhkobitaocaimoi', 3, 1, N'testlotrinhkobitaocaimoi', NULL)
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account], [AccountGoogle]) VALUES (3083, NULL, NULL, NULL, NULL, 1, NULL, NULL, N'testlaicaitaolotrinhcuathangdungconloiko', 3, 1, N'testlaicaitaolotrinhcuathangdungconloiko', NULL)
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account], [AccountGoogle]) VALUES (3084, NULL, NULL, NULL, NULL, 1, NULL, NULL, N'testAllFunction', 3, 1, N'testAllFunction', NULL)
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account], [AccountGoogle]) VALUES (4056, NULL, NULL, NULL, NULL, 1, NULL, NULL, N'ldmgkldnklvdmlk', 4, 1, N'ldmgkldnklvdmlk', NULL)
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account], [AccountGoogle]) VALUES (4057, NULL, NULL, NULL, NULL, 1, NULL, NULL, N'fghfgjngcmghc', 3, 1, N'fghfgjngcmghc', NULL)
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account], [AccountGoogle]) VALUES (4058, NULL, N'testconame', N'testconame2', NULL, NULL, NULL, NULL, N'testconame2', 4, 1, N'testconame2', NULL)
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account], [AccountGoogle]) VALUES (4061, NULL, N'dulieu3 deluu', N'', NULL, NULL, NULL, NULL, N'zpVBewQnu1u9kvslBQKs', 4, 1, N'deluudulieu3@gmail.com', N'deluudulieu3@gmail.com')
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account], [AccountGoogle]) VALUES (4062, NULL, N'jklbdfkmbldfmb;l', N'dfgdfghdfhdfhh', NULL, NULL, NULL, NULL, N'dfgdfghdfhdfhh', 4, 1, N'dfgdfghdfhdfhh', NULL)
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account], [AccountGoogle]) VALUES (5056, NULL, N'tan', N'minh', NULL, 1, NULL, NULL, N'NyUflG69J1+3voS2HQjFJEyD9nJdJ5UjopGIyc+jvrUZH4bEhS2IiArpeW4F+mTj', 4, 1, N'hfsdjgnkdsjhgkdfhg', NULL)
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account], [AccountGoogle]) VALUES (5057, NULL, N'tantest2', N'tantest2', NULL, 0, NULL, NULL, N'NAJ25GRQ2sdWVnGiRnBkHKY2GccrFabgz4YABSvkF9M=', 4, 1, N'fbkdsjngksdng', NULL)
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account], [AccountGoogle]) VALUES (5058, NULL, N'fgdfgdfhdfh', N'fgdfgdfhdfh', NULL, NULL, NULL, NULL, N'bLgFDhDYQ4Fk6OqFS+1DNwkX6Q0FUmdyTZ/Hz8n8Le0=', 4, 1, N'fgdfgdfhdfh', NULL)
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account], [AccountGoogle]) VALUES (5059, NULL, N'bfgbfxgngfxmgxhmxm', N'bfgbfxgngfxmgxhmxm', NULL, 1, NULL, NULL, N'bfgbfxgngfxmgxhmxm', 4, 1, N'bfgbfxgngfxmgxhmxm', NULL)
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account], [AccountGoogle]) VALUES (5060, NULL, N'dulieu4 deluu', N'', NULL, 1, NULL, NULL, N'KIILR11OCgqKnHhhBwO2', 4, 1, N'deluudulieu4@gmail.com', N'deluudulieu4@gmail.com')
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account], [AccountGoogle]) VALUES (5061, NULL, N'testconame', N'fbfgdhgf', NULL, NULL, NULL, NULL, N'$2a$11$QZVTAq.6vmC.XqZ1FnAyOuyt.y50cdlTYvt16V8RsBTfGYMFv.0Pa', 4, 1, N'gfjngfjfg', NULL)
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account], [AccountGoogle]) VALUES (5063, N'~/images/faces/face4.jpg', N'Nutri_1', N'Nutri_1', NULL, NULL, NULL, NULL, N'$2a$11$DM2O/YSpVwVuOCt9ZraD/u/Po4T0JYi/zj/SlGcyUakvGxSH75emG', 2, 1, N'Nutri_1', NULL)
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account], [AccountGoogle]) VALUES (5064, N'~/images/faces/face4.jpg', N'Nutri_2', N'Nutri_2', NULL, NULL, NULL, NULL, N'$2a$11$hRq/QjhRz/HH40zGimXY1OGLeLiyMnS3yCXrwi6oVKazdV2CUdnfS', 2, 1, N'Nutri_2', NULL)
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account], [AccountGoogle]) VALUES (6060, N'~/images/faces/face4.jpg', N'Nutri_3', N'Nutri_3', NULL, NULL, NULL, NULL, N'$2a$11$wRlAyaNsPu6GPLHrsibhOuiLHOTkmZ7Hg6s0jP6Z6QBECb2J2S57e', 2, 1, N'Nutri_3', NULL)
SET IDENTITY_INSERT [UserData].[User] OFF
GO
SET IDENTITY_INSERT [UserData].[UserDetails] ON 

INSERT [UserData].[UserDetails] ([Id], [UserId], [DescribeYourself], [Height], [Weight], [Age], [WantImprove], [UnderlyingDisease], [InforConfirmGood], [InforConfirmBad], [IsPremium], [ActivityLevel], [Calo], [TimeUpdate], [WeightGoal]) VALUES (1, 1, N'Interested in weight loss', 180, 75, NULL, NULL, N'1;2;3;4;', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [UserData].[UserDetails] ([Id], [UserId], [DescribeYourself], [Height], [Weight], [Age], [WantImprove], [UnderlyingDisease], [InforConfirmGood], [InforConfirmBad], [IsPremium], [ActivityLevel], [Calo], [TimeUpdate], [WeightGoal]) VALUES (2, 2, N'Focused on building muscle', 165, 60, NULL, NULL, N'1;2;3;4;5;', NULL, N'Bị Bệnh Thần kinh', NULL, NULL, NULL, NULL, NULL)
INSERT [UserData].[UserDetails] ([Id], [UserId], [DescribeYourself], [Height], [Weight], [Age], [WantImprove], [UnderlyingDisease], [InforConfirmGood], [InforConfirmBad], [IsPremium], [ActivityLevel], [Calo], [TimeUpdate], [WeightGoal]) VALUES (3, 3, N'Looking for balanced diet', 170, 65, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [UserData].[UserDetails] ([Id], [UserId], [DescribeYourself], [Height], [Weight], [Age], [WantImprove], [UnderlyingDisease], [InforConfirmGood], [InforConfirmBad], [IsPremium], [ActivityLevel], [Calo], [TimeUpdate], [WeightGoal]) VALUES (4, 4, N'Aiming to improve overall health', 175, 80, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [UserData].[UserDetails] ([Id], [UserId], [DescribeYourself], [Height], [Weight], [Age], [WantImprove], [UnderlyingDisease], [InforConfirmGood], [InforConfirmBad], [IsPremium], [ActivityLevel], [Calo], [TimeUpdate], [WeightGoal]) VALUES (5, 32, N'Aiming to improve overall health', 175, 80, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 531, NULL, NULL)
INSERT [UserData].[UserDetails] ([Id], [UserId], [DescribeYourself], [Height], [Weight], [Age], [WantImprove], [UnderlyingDisease], [InforConfirmGood], [InforConfirmBad], [IsPremium], [ActivityLevel], [Calo], [TimeUpdate], [WeightGoal]) VALUES (6, 40, N'Aiming to improve overall health', 200, 100, 25, NULL, NULL, NULL, NULL, NULL, 1.9, 4047, NULL, NULL)
INSERT [UserData].[UserDetails] ([Id], [UserId], [DescribeYourself], [Height], [Weight], [Age], [WantImprove], [UnderlyingDisease], [InforConfirmGood], [InforConfirmBad], [IsPremium], [ActivityLevel], [Calo], [TimeUpdate], [WeightGoal]) VALUES (8, 43, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [UserData].[UserDetails] ([Id], [UserId], [DescribeYourself], [Height], [Weight], [Age], [WantImprove], [UnderlyingDisease], [InforConfirmGood], [InforConfirmBad], [IsPremium], [ActivityLevel], [Calo], [TimeUpdate], [WeightGoal]) VALUES (9, 44, NULL, 170, 59, 25, NULL, N'4', NULL, NULL, NULL, 1.375, 2107, NULL, NULL)
INSERT [UserData].[UserDetails] ([Id], [UserId], [DescribeYourself], [Height], [Weight], [Age], [WantImprove], [UnderlyingDisease], [InforConfirmGood], [InforConfirmBad], [IsPremium], [ActivityLevel], [Calo], [TimeUpdate], [WeightGoal]) VALUES (10, 45, NULL, 50, 25, 25, NULL, NULL, NULL, NULL, NULL, 1.2, 531, NULL, NULL)
INSERT [UserData].[UserDetails] ([Id], [UserId], [DescribeYourself], [Height], [Weight], [Age], [WantImprove], [UnderlyingDisease], [InforConfirmGood], [InforConfirmBad], [IsPremium], [ActivityLevel], [Calo], [TimeUpdate], [WeightGoal]) VALUES (11, 46, NULL, 50, 25, 25, NULL, NULL, NULL, NULL, NULL, 1.2, 531, NULL, NULL)
INSERT [UserData].[UserDetails] ([Id], [UserId], [DescribeYourself], [Height], [Weight], [Age], [WantImprove], [UnderlyingDisease], [InforConfirmGood], [InforConfirmBad], [IsPremium], [ActivityLevel], [Calo], [TimeUpdate], [WeightGoal]) VALUES (12, 47, NULL, 50, 25, 25, NULL, NULL, NULL, NULL, NULL, 1.2, 531, NULL, NULL)
INSERT [UserData].[UserDetails] ([Id], [UserId], [DescribeYourself], [Height], [Weight], [Age], [WantImprove], [UnderlyingDisease], [InforConfirmGood], [InforConfirmBad], [IsPremium], [ActivityLevel], [Calo], [TimeUpdate], [WeightGoal]) VALUES (13, 48, NULL, 170, 50, 25, NULL, NULL, NULL, NULL, NULL, 1.2, 1731, NULL, NULL)
INSERT [UserData].[UserDetails] ([Id], [UserId], [DescribeYourself], [Height], [Weight], [Age], [WantImprove], [UnderlyingDisease], [InforConfirmGood], [InforConfirmBad], [IsPremium], [ActivityLevel], [Calo], [TimeUpdate], [WeightGoal]) VALUES (14, 49, NULL, 170, 50, 25, NULL, NULL, NULL, NULL, NULL, 1.2, 1731, NULL, NULL)
INSERT [UserData].[UserDetails] ([Id], [UserId], [DescribeYourself], [Height], [Weight], [Age], [WantImprove], [UnderlyingDisease], [InforConfirmGood], [InforConfirmBad], [IsPremium], [ActivityLevel], [Calo], [TimeUpdate], [WeightGoal]) VALUES (15, 55, NULL, 170, 50, 25, NULL, NULL, NULL, NULL, NULL, 1.2, 1731, NULL, NULL)
INSERT [UserData].[UserDetails] ([Id], [UserId], [DescribeYourself], [Height], [Weight], [Age], [WantImprove], [UnderlyingDisease], [InforConfirmGood], [InforConfirmBad], [IsPremium], [ActivityLevel], [Calo], [TimeUpdate], [WeightGoal]) VALUES (16, 56, NULL, 170, 50, 25, NULL, NULL, NULL, NULL, NULL, 1.2, 1731, NULL, NULL)
INSERT [UserData].[UserDetails] ([Id], [UserId], [DescribeYourself], [Height], [Weight], [Age], [WantImprove], [UnderlyingDisease], [InforConfirmGood], [InforConfirmBad], [IsPremium], [ActivityLevel], [Calo], [TimeUpdate], [WeightGoal]) VALUES (17, 57, NULL, 170, 50, 25, NULL, NULL, NULL, NULL, NULL, 1.2, 1731, NULL, NULL)
INSERT [UserData].[UserDetails] ([Id], [UserId], [DescribeYourself], [Height], [Weight], [Age], [WantImprove], [UnderlyingDisease], [InforConfirmGood], [InforConfirmBad], [IsPremium], [ActivityLevel], [Calo], [TimeUpdate], [WeightGoal]) VALUES (1013, 1056, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [UserData].[UserDetails] ([Id], [UserId], [DescribeYourself], [Height], [Weight], [Age], [WantImprove], [UnderlyingDisease], [InforConfirmGood], [InforConfirmBad], [IsPremium], [ActivityLevel], [Calo], [TimeUpdate], [WeightGoal]) VALUES (1014, 1057, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [UserData].[UserDetails] ([Id], [UserId], [DescribeYourself], [Height], [Weight], [Age], [WantImprove], [UnderlyingDisease], [InforConfirmGood], [InforConfirmBad], [IsPremium], [ActivityLevel], [Calo], [TimeUpdate], [WeightGoal]) VALUES (2013, 3056, NULL, 50, 25, 25, NULL, NULL, NULL, NULL, NULL, 1.2, 531, NULL, NULL)
INSERT [UserData].[UserDetails] ([Id], [UserId], [DescribeYourself], [Height], [Weight], [Age], [WantImprove], [UnderlyingDisease], [InforConfirmGood], [InforConfirmBad], [IsPremium], [ActivityLevel], [Calo], [TimeUpdate], [WeightGoal]) VALUES (2014, 3068, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [UserData].[UserDetails] ([Id], [UserId], [DescribeYourself], [Height], [Weight], [Age], [WantImprove], [UnderlyingDisease], [InforConfirmGood], [InforConfirmBad], [IsPremium], [ActivityLevel], [Calo], [TimeUpdate], [WeightGoal]) VALUES (2016, 3073, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [UserData].[UserDetails] ([Id], [UserId], [DescribeYourself], [Height], [Weight], [Age], [WantImprove], [UnderlyingDisease], [InforConfirmGood], [InforConfirmBad], [IsPremium], [ActivityLevel], [Calo], [TimeUpdate], [WeightGoal]) VALUES (2017, 3075, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [UserData].[UserDetails] ([Id], [UserId], [DescribeYourself], [Height], [Weight], [Age], [WantImprove], [UnderlyingDisease], [InforConfirmGood], [InforConfirmBad], [IsPremium], [ActivityLevel], [Calo], [TimeUpdate], [WeightGoal]) VALUES (2018, 3076, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [UserData].[UserDetails] ([Id], [UserId], [DescribeYourself], [Height], [Weight], [Age], [WantImprove], [UnderlyingDisease], [InforConfirmGood], [InforConfirmBad], [IsPremium], [ActivityLevel], [Calo], [TimeUpdate], [WeightGoal]) VALUES (2019, 3077, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [UserData].[UserDetails] ([Id], [UserId], [DescribeYourself], [Height], [Weight], [Age], [WantImprove], [UnderlyingDisease], [InforConfirmGood], [InforConfirmBad], [IsPremium], [ActivityLevel], [Calo], [TimeUpdate], [WeightGoal]) VALUES (2020, 3078, NULL, 50, 25, 25, NULL, NULL, NULL, NULL, NULL, 1.2, 531, NULL, NULL)
INSERT [UserData].[UserDetails] ([Id], [UserId], [DescribeYourself], [Height], [Weight], [Age], [WantImprove], [UnderlyingDisease], [InforConfirmGood], [InforConfirmBad], [IsPremium], [ActivityLevel], [Calo], [TimeUpdate], [WeightGoal]) VALUES (2021, 3079, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [UserData].[UserDetails] ([Id], [UserId], [DescribeYourself], [Height], [Weight], [Age], [WantImprove], [UnderlyingDisease], [InforConfirmGood], [InforConfirmBad], [IsPremium], [ActivityLevel], [Calo], [TimeUpdate], [WeightGoal]) VALUES (2022, 3080, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [UserData].[UserDetails] ([Id], [UserId], [DescribeYourself], [Height], [Weight], [Age], [WantImprove], [UnderlyingDisease], [InforConfirmGood], [InforConfirmBad], [IsPremium], [ActivityLevel], [Calo], [TimeUpdate], [WeightGoal]) VALUES (2023, 3081, NULL, 50, 25, 25, NULL, NULL, NULL, NULL, NULL, 1.2, 531, NULL, NULL)
INSERT [UserData].[UserDetails] ([Id], [UserId], [DescribeYourself], [Height], [Weight], [Age], [WantImprove], [UnderlyingDisease], [InforConfirmGood], [InforConfirmBad], [IsPremium], [ActivityLevel], [Calo], [TimeUpdate], [WeightGoal]) VALUES (2024, 3082, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [UserData].[UserDetails] ([Id], [UserId], [DescribeYourself], [Height], [Weight], [Age], [WantImprove], [UnderlyingDisease], [InforConfirmGood], [InforConfirmBad], [IsPremium], [ActivityLevel], [Calo], [TimeUpdate], [WeightGoal]) VALUES (2025, 3083, NULL, 50, 25, 25, NULL, NULL, NULL, NULL, NULL, 1.2, 531, NULL, NULL)
INSERT [UserData].[UserDetails] ([Id], [UserId], [DescribeYourself], [Height], [Weight], [Age], [WantImprove], [UnderlyingDisease], [InforConfirmGood], [InforConfirmBad], [IsPremium], [ActivityLevel], [Calo], [TimeUpdate], [WeightGoal]) VALUES (2026, 3084, NULL, 50, 25, 25, NULL, NULL, NULL, NULL, NULL, 1.2, 531, NULL, NULL)
INSERT [UserData].[UserDetails] ([Id], [UserId], [DescribeYourself], [Height], [Weight], [Age], [WantImprove], [UnderlyingDisease], [InforConfirmGood], [InforConfirmBad], [IsPremium], [ActivityLevel], [Calo], [TimeUpdate], [WeightGoal]) VALUES (3013, 4056, NULL, 150, 68, 25, NULL, NULL, NULL, NULL, NULL, 1.2, 1797, NULL, NULL)
INSERT [UserData].[UserDetails] ([Id], [UserId], [DescribeYourself], [Height], [Weight], [Age], [WantImprove], [UnderlyingDisease], [InforConfirmGood], [InforConfirmBad], [IsPremium], [ActivityLevel], [Calo], [TimeUpdate], [WeightGoal]) VALUES (3014, 4057, NULL, 50, 25, 25, NULL, NULL, NULL, NULL, NULL, 1.2, 531, NULL, NULL)
INSERT [UserData].[UserDetails] ([Id], [UserId], [DescribeYourself], [Height], [Weight], [Age], [WantImprove], [UnderlyingDisease], [InforConfirmGood], [InforConfirmBad], [IsPremium], [ActivityLevel], [Calo], [TimeUpdate], [WeightGoal]) VALUES (3015, 4058, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [UserData].[UserDetails] ([Id], [UserId], [DescribeYourself], [Height], [Weight], [Age], [WantImprove], [UnderlyingDisease], [InforConfirmGood], [InforConfirmBad], [IsPremium], [ActivityLevel], [Calo], [TimeUpdate], [WeightGoal]) VALUES (3016, 4061, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [UserData].[UserDetails] ([Id], [UserId], [DescribeYourself], [Height], [Weight], [Age], [WantImprove], [UnderlyingDisease], [InforConfirmGood], [InforConfirmBad], [IsPremium], [ActivityLevel], [Calo], [TimeUpdate], [WeightGoal]) VALUES (3017, 4062, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [UserData].[UserDetails] ([Id], [UserId], [DescribeYourself], [Height], [Weight], [Age], [WantImprove], [UnderlyingDisease], [InforConfirmGood], [InforConfirmBad], [IsPremium], [ActivityLevel], [Calo], [TimeUpdate], [WeightGoal]) VALUES (4017, 5056, NULL, 170, 50, 23, NULL, NULL, NULL, NULL, NULL, 1.55, 2251, NULL, NULL)
INSERT [UserData].[UserDetails] ([Id], [UserId], [DescribeYourself], [Height], [Weight], [Age], [WantImprove], [UnderlyingDisease], [InforConfirmGood], [InforConfirmBad], [IsPremium], [ActivityLevel], [Calo], [TimeUpdate], [WeightGoal]) VALUES (4018, 5057, NULL, 50, 25, 25, NULL, NULL, NULL, NULL, NULL, 1.2, 331, NULL, NULL)
INSERT [UserData].[UserDetails] ([Id], [UserId], [DescribeYourself], [Height], [Weight], [Age], [WantImprove], [UnderlyingDisease], [InforConfirmGood], [InforConfirmBad], [IsPremium], [ActivityLevel], [Calo], [TimeUpdate], [WeightGoal]) VALUES (4019, 5058, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [UserData].[UserDetails] ([Id], [UserId], [DescribeYourself], [Height], [Weight], [Age], [WantImprove], [UnderlyingDisease], [InforConfirmGood], [InforConfirmBad], [IsPremium], [ActivityLevel], [Calo], [TimeUpdate], [WeightGoal]) VALUES (4020, 5059, NULL, 150, 50, 25, NULL, NULL, NULL, NULL, NULL, 1.2, 1581, NULL, NULL)
INSERT [UserData].[UserDetails] ([Id], [UserId], [DescribeYourself], [Height], [Weight], [Age], [WantImprove], [UnderlyingDisease], [InforConfirmGood], [InforConfirmBad], [IsPremium], [ActivityLevel], [Calo], [TimeUpdate], [WeightGoal]) VALUES (4021, 5060, NULL, 150, 75, 25, NULL, NULL, NULL, NULL, NULL, 1.2, 1881, NULL, NULL)
INSERT [UserData].[UserDetails] ([Id], [UserId], [DescribeYourself], [Height], [Weight], [Age], [WantImprove], [UnderlyingDisease], [InforConfirmGood], [InforConfirmBad], [IsPremium], [ActivityLevel], [Calo], [TimeUpdate], [WeightGoal]) VALUES (4022, 5061, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [UserData].[UserDetails] OFF
GO
SET IDENTITY_INSERT [UserData].[WantCooking] ON 

INSERT [UserData].[WantCooking] ([Id], [Name]) VALUES (1, N'Phải nấu')
INSERT [UserData].[WantCooking] ([Id], [Name]) VALUES (2, N'Không nấu ăn')
SET IDENTITY_INSERT [UserData].[WantCooking] OFF
GO
/****** Object:  Index [UQ__MealOfTh__641B4C6D9F1431EA]    Script Date: 12/6/2024 3:55:06 AM ******/
ALTER TABLE [Business].[MealOfTheDay] ADD UNIQUE NONCLUSTERED 
(
	[NutritionRouteId] ASC,
	[DateExecute] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [UQ__MealSett__1788CC4D843F35AF]    Script Date: 12/6/2024 3:55:06 AM ******/
ALTER TABLE [UserData].[MealSettings] ADD UNIQUE NONCLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [UQ__Nutritio__9C31FC2DB4456619]    Script Date: 12/6/2024 3:55:06 AM ******/
ALTER TABLE [UserData].[NutritionistDetails] ADD UNIQUE NONCLUSTERED 
(
	[NutritionistId] ASC,
	[ExpertPackagesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [UQ__Nutritio__F4399C8D11E0A2FB]    Script Date: 12/6/2024 3:55:06 AM ******/
ALTER TABLE [UserData].[NutritionistDetails] ADD UNIQUE NONCLUSTERED 
(
	[NutritionistId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Account]    Script Date: 12/6/2024 3:55:06 AM ******/
ALTER TABLE [UserData].[User] ADD  CONSTRAINT [UQ_Account] UNIQUE NONCLUSTERED 
(
	[Account] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [UQ__UserDeta__1788CC4DE9C17C83]    Script Date: 12/6/2024 3:55:06 AM ******/
ALTER TABLE [UserData].[UserDetails] ADD UNIQUE NONCLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [UserData].[User] ADD  DEFAULT ('example@example.com') FOR [Account]
GO
ALTER TABLE [Business].[ArticlesNews]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [UserData].[User] ([UserId])
GO
ALTER TABLE [Business].[MealOfTheDay]  WITH CHECK ADD FOREIGN KEY([NutritionRouteId])
REFERENCES [Business].[NutritionRoute] ([Id])
GO
ALTER TABLE [Business].[Messages]  WITH CHECK ADD FOREIGN KEY([FromUserId])
REFERENCES [UserData].[User] ([UserId])
GO
ALTER TABLE [Business].[Messages]  WITH CHECK ADD FOREIGN KEY([ToRoomId])
REFERENCES [Business].[Rooms] ([Id])
GO
ALTER TABLE [Business].[NewsEvaluation]  WITH CHECK ADD FOREIGN KEY([ArticlesNewsId])
REFERENCES [Business].[ArticlesNews] ([Id])
GO
ALTER TABLE [Business].[NewsEvaluation]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [UserData].[User] ([UserId])
GO
ALTER TABLE [Business].[NutritionRoute]  WITH CHECK ADD FOREIGN KEY([CreateById])
REFERENCES [UserData].[User] ([UserId])
GO
ALTER TABLE [Business].[NutritionRoute]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [UserData].[User] ([UserId])
GO
ALTER TABLE [Business].[Rooms]  WITH CHECK ADD FOREIGN KEY([NutritionId])
REFERENCES [UserData].[User] ([UserId])
GO
ALTER TABLE [Business].[Rooms]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [UserData].[User] ([UserId])
GO
ALTER TABLE [Business].[TransactionsSystem]  WITH CHECK ADD FOREIGN KEY([PayeeID])
REFERENCES [UserData].[User] ([UserId])
GO
ALTER TABLE [Business].[TransactionsSystem]  WITH CHECK ADD FOREIGN KEY([UserPayID])
REFERENCES [UserData].[User] ([UserId])
GO
ALTER TABLE [Business].[UserListManagement]  WITH CHECK ADD FOREIGN KEY([NutritionistId])
REFERENCES [UserData].[User] ([UserId])
GO
ALTER TABLE [Business].[UserListManagement]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [UserData].[User] ([UserId])
GO
ALTER TABLE [FoodData].[DietWithFoodType]  WITH CHECK ADD FOREIGN KEY([DietTypeId])
REFERENCES [FoodData].[DietType] ([DietTypeId])
GO
ALTER TABLE [FoodData].[DietWithFoodType]  WITH CHECK ADD FOREIGN KEY([FoodTypeId])
REFERENCES [FoodData].[FoodType] ([FoodTypeId])
GO
ALTER TABLE [FoodData].[FoodAndDiseases]  WITH CHECK ADD FOREIGN KEY([FoodListId])
REFERENCES [FoodData].[FoodList] ([FoodListId])
GO
ALTER TABLE [FoodData].[FoodAndDiseases]  WITH CHECK ADD FOREIGN KEY([ListOfDiseasesId])
REFERENCES [UserData].[ListOfDiseases] ([Id])
GO
ALTER TABLE [FoodData].[FoodList]  WITH CHECK ADD FOREIGN KEY([CookingDifficultyId])
REFERENCES [FoodData].[CookingDifficulty] ([Id])
GO
ALTER TABLE [FoodData].[FoodList]  WITH CHECK ADD FOREIGN KEY([FoodTypeId])
REFERENCES [FoodData].[FoodType] ([FoodTypeId])
GO
ALTER TABLE [FoodData].[FoodList]  WITH CHECK ADD FOREIGN KEY([KeyNoteId])
REFERENCES [FoodData].[KeyNote] ([Id])
GO
ALTER TABLE [FoodData].[IngredientDetails100g]  WITH CHECK ADD FOREIGN KEY([TypeOfCalculationId])
REFERENCES [FoodData].[TypeOfCalculation] ([TypeId])
GO
ALTER TABLE [FoodData].[IngredientDetails100g]  WITH CHECK ADD  CONSTRAINT [FK_IngredientDetails100g_KeyNote] FOREIGN KEY([KeyNoteId])
REFERENCES [FoodData].[KeyNote] ([Id])
GO
ALTER TABLE [FoodData].[IngredientDetails100g] CHECK CONSTRAINT [FK_IngredientDetails100g_KeyNote]
GO
ALTER TABLE [FoodData].[Recipe]  WITH CHECK ADD FOREIGN KEY([FoodListId])
REFERENCES [FoodData].[FoodList] ([FoodListId])
GO
ALTER TABLE [FoodData].[ScaleAmount]  WITH CHECK ADD FOREIGN KEY([FoodListId])
REFERENCES [FoodData].[FoodList] ([FoodListId])
GO
ALTER TABLE [FoodData].[ScaleAmount]  WITH CHECK ADD FOREIGN KEY([IngredientDetailsId])
REFERENCES [FoodData].[IngredientDetails100g] ([Id])
GO
ALTER TABLE [UserData].[FoodSelection]  WITH CHECK ADD FOREIGN KEY([FoodListId])
REFERENCES [FoodData].[FoodList] ([FoodListId])
GO
ALTER TABLE [UserData].[FoodSelection]  WITH CHECK ADD FOREIGN KEY([RecurringId])
REFERENCES [UserData].[RecurringSettings] ([Id])
GO
ALTER TABLE [UserData].[FoodSelection]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [UserData].[User] ([UserId])
GO
ALTER TABLE [UserData].[MealSettings]  WITH CHECK ADD FOREIGN KEY([DayOfTheWeekStartId])
REFERENCES [Business].[DayOfTheWeek] ([Id])
GO
ALTER TABLE [UserData].[MealSettings]  WITH CHECK ADD FOREIGN KEY([FoodTypeIdWant])
REFERENCES [FoodData].[DietType] ([DietTypeId])
GO
ALTER TABLE [UserData].[MealSettings]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [UserData].[User] ([UserId])
GO
ALTER TABLE [UserData].[MealSettingsDetails]  WITH CHECK ADD FOREIGN KEY([CookingDifficultyId])
REFERENCES [FoodData].[CookingDifficulty] ([Id])
GO
ALTER TABLE [UserData].[MealSettingsDetails]  WITH CHECK ADD FOREIGN KEY([DayOfTheWeekId])
REFERENCES [Business].[DayOfTheWeek] ([Id])
GO
ALTER TABLE [UserData].[MealSettingsDetails]  WITH CHECK ADD FOREIGN KEY([MealSettingsId])
REFERENCES [UserData].[MealSettings] ([Id])
GO
ALTER TABLE [UserData].[MealSettingsDetails]  WITH CHECK ADD FOREIGN KEY([NutritionTargetsDailyId])
REFERENCES [UserData].[NutritionTargetsDaily] ([Id])
GO
ALTER TABLE [UserData].[MealSettingsDetails]  WITH CHECK ADD FOREIGN KEY([SlotOfTheDayId])
REFERENCES [Business].[SlotOfTheDay] ([Id])
GO
ALTER TABLE [UserData].[MealSettingsDetails]  WITH CHECK ADD FOREIGN KEY([WantCookingId])
REFERENCES [UserData].[WantCooking] ([Id])
GO
ALTER TABLE [UserData].[NutritionistDetails]  WITH CHECK ADD FOREIGN KEY([ExpertPackagesId])
REFERENCES [UserData].[ExpertPackages] ([Id])
GO
ALTER TABLE [UserData].[NutritionistDetails]  WITH CHECK ADD FOREIGN KEY([NutritionistId])
REFERENCES [UserData].[User] ([UserId])
GO
ALTER TABLE [UserData].[NutritionTargetsDaily]  WITH CHECK ADD FOREIGN KEY([FoodTypeIdWant])
REFERENCES [FoodData].[DietType] ([DietTypeId])
GO
ALTER TABLE [UserData].[NutritionTargetsDaily]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [UserData].[User] ([UserId])
GO
ALTER TABLE [UserData].[User]  WITH CHECK ADD FOREIGN KEY([Role])
REFERENCES [UserData].[Role] ([RoleID])
GO
ALTER TABLE [UserData].[UserDetails]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [UserData].[User] ([UserId])
GO
