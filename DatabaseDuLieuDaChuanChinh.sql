USE [SEP490_G87_VitaNutrientSystem]
GO
/****** Object:  Schema [Business]    Script Date: 10/24/2024 2:04:10 PM ******/
CREATE SCHEMA [Business]
GO
/****** Object:  Schema [FoodData]    Script Date: 10/24/2024 2:04:10 PM ******/
CREATE SCHEMA [FoodData]
GO
/****** Object:  Schema [UserData]    Script Date: 10/24/2024 2:04:10 PM ******/
CREATE SCHEMA [UserData]
GO
/****** Object:  Table [Business].[ArticlesNews]    Script Date: 10/24/2024 2:04:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Business].[ArticlesNews](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[NameCreater] [nvarchar](50) NULL,
	[Title] [nvarchar](255) NULL,
	[Content] [nvarchar](500) NULL,
	[IsActive] [bit] NULL,
	[DateCreated] [datetime] NULL,
	[HeaderImage] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Business].[ConversationParticipants]    Script Date: 10/24/2024 2:04:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Business].[ConversationParticipants](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ConversationsId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[AddedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Business].[Conversations]    Script Date: 10/24/2024 2:04:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Business].[Conversations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[IsGroup] [bit] NULL,
	[CreatedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Business].[DayOfTheWeek]    Script Date: 10/24/2024 2:04:10 PM ******/
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
/****** Object:  Table [Business].[MealOfTheDay]    Script Date: 10/24/2024 2:04:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Business].[MealOfTheDay](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NutritionRouteId] [int] NOT NULL,
	[Slot1OfTheDayId] [smallint] NULL,
	[Slot1FoodListId] [varchar](512) NULL,
	[Slot2OfTheDayId] [smallint] NULL,
	[Slot2FoodListId] [varchar](512) NULL,
	[Slot3OfTheDayId] [smallint] NULL,
	[Slot3FoodListId] [varchar](512) NULL,
	[Slot4OfTheDayId] [smallint] NULL,
	[Slot4FoodListId] [varchar](512) NULL,
	[Slot5OfTheDayId] [smallint] NULL,
	[Slot5FoodListId] [varchar](512) NULL,
	[DateExecute] [date] NULL,
	[IsDone] [bit] NULL,
	[IsEditByUser] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Business].[Messages]    Script Date: 10/24/2024 2:04:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Business].[Messages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ConversationsId] [int] NOT NULL,
	[SenderId] [int] NOT NULL,
	[Content] [nvarchar](500) NULL,
	[SentAt] [datetime] NULL,
	[IsRead] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Business].[MSG]    Script Date: 10/24/2024 2:04:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Business].[MSG](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[MessageType] [nvarchar](50) NULL,
	[MessageContent] [nvarchar](500) NULL,
	[SendDateTime] [datetime] NULL,
	[IsSent] [bit] NULL,
	[SendResult] [bit] NULL,
	[ErrorMessage] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Business].[NutritionRoute]    Script Date: 10/24/2024 2:04:10 PM ******/
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
/****** Object:  Table [Business].[SlotOfTheDay]    Script Date: 10/24/2024 2:04:10 PM ******/
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
/****** Object:  Table [Business].[TransactionsSystem]    Script Date: 10/24/2024 2:04:10 PM ******/
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
/****** Object:  Table [Business].[UserListManagement]    Script Date: 10/24/2024 2:04:10 PM ******/
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
/****** Object:  Table [FoodData].[CookingDifficulty]    Script Date: 10/24/2024 2:04:10 PM ******/
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
/****** Object:  Table [FoodData].[FoodAndDiseases]    Script Date: 10/24/2024 2:04:10 PM ******/
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
/****** Object:  Table [FoodData].[FoodList]    Script Date: 10/24/2024 2:04:10 PM ******/
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
/****** Object:  Table [FoodData].[FoodType]    Script Date: 10/24/2024 2:04:10 PM ******/
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
/****** Object:  Table [FoodData].[IngredientDetails100g]    Script Date: 10/24/2024 2:04:10 PM ******/
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
/****** Object:  Table [FoodData].[KeyNote]    Script Date: 10/24/2024 2:04:10 PM ******/
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
/****** Object:  Table [FoodData].[Recipe]    Script Date: 10/24/2024 2:04:10 PM ******/
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
/****** Object:  Table [FoodData].[ScaleAmount]    Script Date: 10/24/2024 2:04:10 PM ******/
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
/****** Object:  Table [FoodData].[TypeOfCalculation]    Script Date: 10/24/2024 2:04:10 PM ******/
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
/****** Object:  Table [UserData].[BankInformation]    Script Date: 10/24/2024 2:04:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [UserData].[BankInformation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[BankAccount] [varchar](50) NULL,
	[TypeBank] [varchar](50) NULL,
	[IsUsed] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [UserData].[ExerciseIntensity]    Script Date: 10/24/2024 2:04:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [UserData].[ExerciseIntensity](
	[Id] [smallint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Describe] [nvarchar](500) NULL,
	[ListKey] [nvarchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [UserData].[ExpertPackages]    Script Date: 10/24/2024 2:04:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [UserData].[ExpertPackages](
	[Id] [smallint] IDENTITY(1,1) NOT NULL,
	[NutritionistDetailsId] [int] NOT NULL,
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
/****** Object:  Table [UserData].[FoodSelection]    Script Date: 10/24/2024 2:04:10 PM ******/
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
/****** Object:  Table [UserData].[ListOfDiseases]    Script Date: 10/24/2024 2:04:10 PM ******/
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
/****** Object:  Table [UserData].[MealSettings]    Script Date: 10/24/2024 2:04:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [UserData].[MealSettings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[DayOfTheWeekStartId] [smallint] NULL,
	[SameScheduleEveryDay] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [UserData].[MealSettingsDetails]    Script Date: 10/24/2024 2:04:10 PM ******/
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
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [UserData].[NutritionistDetails]    Script Date: 10/24/2024 2:04:10 PM ******/
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
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [UserData].[NutritionTargetsDaily]    Script Date: 10/24/2024 2:04:10 PM ******/
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
	[FoodTypeIdWant] [smallint] NOT NULL,
	[AvoidIngredient] [nvarchar](500) NULL,
	[IsActive] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [UserData].[RecurringSettings]    Script Date: 10/24/2024 2:04:10 PM ******/
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
/****** Object:  Table [UserData].[Role]    Script Date: 10/24/2024 2:04:10 PM ******/
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
/****** Object:  Table [UserData].[User]    Script Date: 10/24/2024 2:04:10 PM ******/
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
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [UserData].[UserDetails]    Script Date: 10/24/2024 2:04:10 PM ******/
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
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [UserData].[WantCooking]    Script Date: 10/24/2024 2:04:10 PM ******/
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

INSERT [Business].[ArticlesNews] ([Id], [UserID], [NameCreater], [Title], [Content], [IsActive], [DateCreated], [HeaderImage]) VALUES (1, 1, N'John Doe', N'Health Tips', N'Here are some tips...', 1, NULL, NULL)
INSERT [Business].[ArticlesNews] ([Id], [UserID], [NameCreater], [Title], [Content], [IsActive], [DateCreated], [HeaderImage]) VALUES (2, 2, N'Jane Smith', N'Diet Plans', N'Learn about diet plans...', 1, NULL, NULL)
INSERT [Business].[ArticlesNews] ([Id], [UserID], [NameCreater], [Title], [Content], [IsActive], [DateCreated], [HeaderImage]) VALUES (3, 3, N'Emily Brown', N'Exercise Routines', N'Best exercises for you...', 1, NULL, NULL)
INSERT [Business].[ArticlesNews] ([Id], [UserID], [NameCreater], [Title], [Content], [IsActive], [DateCreated], [HeaderImage]) VALUES (4, 4, N'Michael Johnson', N'Nutrition Facts', N'Important nutrition info...', 1, NULL, NULL)
SET IDENTITY_INSERT [Business].[ArticlesNews] OFF
GO
SET IDENTITY_INSERT [Business].[ConversationParticipants] ON 

INSERT [Business].[ConversationParticipants] ([Id], [ConversationsId], [UserId], [AddedAt]) VALUES (1, 1, 1, CAST(N'2024-10-03T20:46:55.223' AS DateTime))
INSERT [Business].[ConversationParticipants] ([Id], [ConversationsId], [UserId], [AddedAt]) VALUES (2, 2, 2, CAST(N'2024-10-03T20:46:55.223' AS DateTime))
INSERT [Business].[ConversationParticipants] ([Id], [ConversationsId], [UserId], [AddedAt]) VALUES (3, 3, 3, CAST(N'2024-10-03T20:46:55.223' AS DateTime))
INSERT [Business].[ConversationParticipants] ([Id], [ConversationsId], [UserId], [AddedAt]) VALUES (4, 4, 4, CAST(N'2024-10-03T20:46:55.223' AS DateTime))
SET IDENTITY_INSERT [Business].[ConversationParticipants] OFF
GO
SET IDENTITY_INSERT [Business].[Conversations] ON 

INSERT [Business].[Conversations] ([Id], [Name], [IsGroup], [CreatedAt]) VALUES (1, N'Group A', 1, CAST(N'2024-10-03T20:46:55.217' AS DateTime))
INSERT [Business].[Conversations] ([Id], [Name], [IsGroup], [CreatedAt]) VALUES (2, N'Group B', 1, CAST(N'2024-10-03T20:46:55.217' AS DateTime))
INSERT [Business].[Conversations] ([Id], [Name], [IsGroup], [CreatedAt]) VALUES (3, N'Private Chat', 0, CAST(N'2024-10-03T20:46:55.217' AS DateTime))
INSERT [Business].[Conversations] ([Id], [Name], [IsGroup], [CreatedAt]) VALUES (4, N'Support Chat', 0, CAST(N'2024-10-03T20:46:55.217' AS DateTime))
SET IDENTITY_INSERT [Business].[Conversations] OFF
GO
SET IDENTITY_INSERT [Business].[DayOfTheWeek] ON 

INSERT [Business].[DayOfTheWeek] ([Id], [Name]) VALUES (1, N'Monday')
INSERT [Business].[DayOfTheWeek] ([Id], [Name]) VALUES (2, N'Tuesday')
INSERT [Business].[DayOfTheWeek] ([Id], [Name]) VALUES (3, N'Wednesday')
INSERT [Business].[DayOfTheWeek] ([Id], [Name]) VALUES (4, N'Thursday')
INSERT [Business].[DayOfTheWeek] ([Id], [Name]) VALUES (5, N'Friday')
INSERT [Business].[DayOfTheWeek] ([Id], [Name]) VALUES (6, N'Saturday')
INSERT [Business].[DayOfTheWeek] ([Id], [Name]) VALUES (7, N'Sunday')
INSERT [Business].[DayOfTheWeek] ([Id], [Name]) VALUES (8, N'AllWeek')
SET IDENTITY_INSERT [Business].[DayOfTheWeek] OFF
GO
SET IDENTITY_INSERT [Business].[MealOfTheDay] ON 

INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [Slot1OfTheDayId], [Slot1FoodListId], [Slot2OfTheDayId], [Slot2FoodListId], [Slot3OfTheDayId], [Slot3FoodListId], [Slot4OfTheDayId], [Slot4FoodListId], [Slot5OfTheDayId], [Slot5FoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (1, 1, 1, N'#1#2#3#4#5', 2, N'#1#2#3#4#5', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [Slot1OfTheDayId], [Slot1FoodListId], [Slot2OfTheDayId], [Slot2FoodListId], [Slot3OfTheDayId], [Slot3FoodListId], [Slot4OfTheDayId], [Slot4FoodListId], [Slot5OfTheDayId], [Slot5FoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (2, 2, 3, N'#1#2#3#4#5', 4, N'#1#2#3#4#5', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [Slot1OfTheDayId], [Slot1FoodListId], [Slot2OfTheDayId], [Slot2FoodListId], [Slot3OfTheDayId], [Slot3FoodListId], [Slot4OfTheDayId], [Slot4FoodListId], [Slot5OfTheDayId], [Slot5FoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (3, 3, 1, N'#1#2#3#4#5', 2, N'#1#2#3#4#5', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [Business].[MealOfTheDay] ([Id], [NutritionRouteId], [Slot1OfTheDayId], [Slot1FoodListId], [Slot2OfTheDayId], [Slot2FoodListId], [Slot3OfTheDayId], [Slot3FoodListId], [Slot4OfTheDayId], [Slot4FoodListId], [Slot5OfTheDayId], [Slot5FoodListId], [DateExecute], [IsDone], [IsEditByUser]) VALUES (4, 4, 3, N'#1#2#3#4#5', 4, N'#1#2#3#4#5', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [Business].[MealOfTheDay] OFF
GO
SET IDENTITY_INSERT [Business].[Messages] ON 

INSERT [Business].[Messages] ([Id], [ConversationsId], [SenderId], [Content], [SentAt], [IsRead]) VALUES (1, 1, 1, N'Hello!', CAST(N'2024-10-03T20:46:55.223' AS DateTime), 0)
INSERT [Business].[Messages] ([Id], [ConversationsId], [SenderId], [Content], [SentAt], [IsRead]) VALUES (2, 2, 2, N'How are you?', CAST(N'2024-10-03T20:46:55.223' AS DateTime), 0)
INSERT [Business].[Messages] ([Id], [ConversationsId], [SenderId], [Content], [SentAt], [IsRead]) VALUES (3, 3, 3, N'What up?', CAST(N'2024-10-03T20:46:55.223' AS DateTime), 0)
INSERT [Business].[Messages] ([Id], [ConversationsId], [SenderId], [Content], [SentAt], [IsRead]) VALUES (4, 4, 4, N'Good day!', CAST(N'2024-10-03T20:46:55.223' AS DateTime), 1)
SET IDENTITY_INSERT [Business].[Messages] OFF
GO
SET IDENTITY_INSERT [Business].[MSG] ON 

INSERT [Business].[MSG] ([Id], [UserID], [MessageType], [MessageContent], [SendDateTime], [IsSent], [SendResult], [ErrorMessage]) VALUES (1, 1, N'SMS', N'Your order is ready', CAST(N'2024-10-03T20:46:55.223' AS DateTime), 1, NULL, NULL)
INSERT [Business].[MSG] ([Id], [UserID], [MessageType], [MessageContent], [SendDateTime], [IsSent], [SendResult], [ErrorMessage]) VALUES (2, 2, N'Email', N'Meeting reminder', CAST(N'2024-10-03T20:46:55.223' AS DateTime), 1, NULL, NULL)
INSERT [Business].[MSG] ([Id], [UserID], [MessageType], [MessageContent], [SendDateTime], [IsSent], [SendResult], [ErrorMessage]) VALUES (3, 3, N'Push Notification', N'New update available', CAST(N'2024-10-03T20:46:55.223' AS DateTime), 1, NULL, NULL)
INSERT [Business].[MSG] ([Id], [UserID], [MessageType], [MessageContent], [SendDateTime], [IsSent], [SendResult], [ErrorMessage]) VALUES (4, 4, N'SMS', N'Subscription renewal', CAST(N'2024-10-03T20:46:55.223' AS DateTime), 1, NULL, NULL)
SET IDENTITY_INSERT [Business].[MSG] OFF
GO
SET IDENTITY_INSERT [Business].[NutritionRoute] ON 

INSERT [Business].[NutritionRoute] ([Id], [UserId], [CreateById], [Name], [Describe], [StartDate], [EndDate], [IsDone]) VALUES (1, 1, 2, N'Weight Loss Plan', N'A customized plan for weight loss', NULL, NULL, NULL)
INSERT [Business].[NutritionRoute] ([Id], [UserId], [CreateById], [Name], [Describe], [StartDate], [EndDate], [IsDone]) VALUES (2, 2, 3, N'Muscle Gain Plan', N'A customized plan for muscle gain', NULL, NULL, NULL)
INSERT [Business].[NutritionRoute] ([Id], [UserId], [CreateById], [Name], [Describe], [StartDate], [EndDate], [IsDone]) VALUES (3, 3, 4, N'Balanced Diet Plan', N'A customized plan for balanced nutrition', NULL, NULL, NULL)
INSERT [Business].[NutritionRoute] ([Id], [UserId], [CreateById], [Name], [Describe], [StartDate], [EndDate], [IsDone]) VALUES (4, 4, 1, N'Health Improvement Plan', N'A plan focused on overall health', NULL, NULL, NULL)
SET IDENTITY_INSERT [Business].[NutritionRoute] OFF
GO
SET IDENTITY_INSERT [Business].[SlotOfTheDay] ON 

INSERT [Business].[SlotOfTheDay] ([Id], [Slot], [StartAt], [EndAt]) VALUES (1, N'Bữa sáng', CAST(N'08:00:00' AS Time), CAST(N'12:00:00' AS Time))
INSERT [Business].[SlotOfTheDay] ([Id], [Slot], [StartAt], [EndAt]) VALUES (2, N'Bữa trưa', CAST(N'12:00:00' AS Time), CAST(N'17:00:00' AS Time))
INSERT [Business].[SlotOfTheDay] ([Id], [Slot], [StartAt], [EndAt]) VALUES (3, N'Bữa chiều', CAST(N'17:00:00' AS Time), CAST(N'21:00:00' AS Time))
INSERT [Business].[SlotOfTheDay] ([Id], [Slot], [StartAt], [EndAt]) VALUES (4, N'Bữa tối', CAST(N'21:00:00' AS Time), CAST(N'00:00:00' AS Time))
SET IDENTITY_INSERT [Business].[SlotOfTheDay] OFF
GO
SET IDENTITY_INSERT [Business].[TransactionsSystem] ON 

INSERT [Business].[TransactionsSystem] ([Id], [UserPayID], [PayeeID], [APITransactions], [BankBrandName], [AccountNumber], [TransactionDate], [AmountOut], [AmountIn], [Accumulated], [TransactionContent], [ReferenceNumber], [Code], [SubAccount], [BankAccountId], [Status]) VALUES (3, 3, 1, NULL, N'MBBank', N'0569000899', CAST(N'2024-10-16T17:01:19.000' AS DateTime), NULL, 4000.0000, NULL, N'thank toan test 1', NULL, NULL, NULL, NULL, 0)
INSERT [Business].[TransactionsSystem] ([Id], [UserPayID], [PayeeID], [APITransactions], [BankBrandName], [AccountNumber], [TransactionDate], [AmountOut], [AmountIn], [Accumulated], [TransactionContent], [ReferenceNumber], [Code], [SubAccount], [BankAccountId], [Status]) VALUES (4, 3, 1, 3569566, N'MBBank', N'0569000899', CAST(N'2024-10-16T17:01:19.000' AS DateTime), 0.0000, 2000.0000, 0.0000, N'Hello', N'FT24290600691633', N'', N'', 3342, 1)
INSERT [Business].[TransactionsSystem] ([Id], [UserPayID], [PayeeID], [APITransactions], [BankBrandName], [AccountNumber], [TransactionDate], [AmountOut], [AmountIn], [Accumulated], [TransactionContent], [ReferenceNumber], [Code], [SubAccount], [BankAccountId], [Status]) VALUES (5, 3, 1, 3679627, N'MBBank', N'0569000899', CAST(N'2024-10-19T00:39:58.000' AS DateTime), 0.0000, 5000.0000, 0.0000, N'thank toan test mon 9', N'FT24293547418805', N'', N'', 3342, 1)
INSERT [Business].[TransactionsSystem] ([Id], [UserPayID], [PayeeID], [APITransactions], [BankBrandName], [AccountNumber], [TransactionDate], [AmountOut], [AmountIn], [Accumulated], [TransactionContent], [ReferenceNumber], [Code], [SubAccount], [BankAccountId], [Status]) VALUES (6, 3, 1, 3679831, N'MBBank', N'0569000899', CAST(N'2024-10-19T00:56:39.000' AS DateTime), 0.0000, 3000.0000, 0.0000, N'lo trinh ti do', N'FT24293334700168', N'', N'', 3342, 1)
INSERT [Business].[TransactionsSystem] ([Id], [UserPayID], [PayeeID], [APITransactions], [BankBrandName], [AccountNumber], [TransactionDate], [AmountOut], [AmountIn], [Accumulated], [TransactionContent], [ReferenceNumber], [Code], [SubAccount], [BankAccountId], [Status]) VALUES (7, 1, 3, 3679831, N'MBBank', N'0569000899', CAST(N'2024-10-19T00:56:39.000' AS DateTime), 6000.0000, 0.0000, 0.0000, N'lo trinh ti do 434', N'FT24293334700168', N'', N'', 3342, 1)
INSERT [Business].[TransactionsSystem] ([Id], [UserPayID], [PayeeID], [APITransactions], [BankBrandName], [AccountNumber], [TransactionDate], [AmountOut], [AmountIn], [Accumulated], [TransactionContent], [ReferenceNumber], [Code], [SubAccount], [BankAccountId], [Status]) VALUES (10, 3, 1, 3798396, N'MBBank', N'0569000899', CAST(N'2024-10-21T21:09:49.000' AS DateTime), 0.0000, 20000.0000, 0.0000, N'User chuyen tien QHARTM', N'FT24295555835584', N'', N'', 3342, 1)
INSERT [Business].[TransactionsSystem] ([Id], [UserPayID], [PayeeID], [APITransactions], [BankBrandName], [AccountNumber], [TransactionDate], [AmountOut], [AmountIn], [Accumulated], [TransactionContent], [ReferenceNumber], [Code], [SubAccount], [BankAccountId], [Status]) VALUES (12, 3, 1, NULL, NULL, NULL, NULL, NULL, 20000.0000, NULL, N'Co Xuan chuyen chuyen tien QHARTM', NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [Business].[TransactionsSystem] OFF
GO
SET IDENTITY_INSERT [FoodData].[CookingDifficulty] ON 

INSERT [FoodData].[CookingDifficulty] ([Id], [Name]) VALUES (1, N'Super hard')
INSERT [FoodData].[CookingDifficulty] ([Id], [Name]) VALUES (2, N'Hard')
INSERT [FoodData].[CookingDifficulty] ([Id], [Name]) VALUES (3, N'Medium')
INSERT [FoodData].[CookingDifficulty] ([Id], [Name]) VALUES (4, N'Easy')
SET IDENTITY_INSERT [FoodData].[CookingDifficulty] OFF
GO
INSERT [FoodData].[FoodAndDiseases] ([ListOfDiseasesId], [FoodListId], [Describe], [IsGoodOrBad]) VALUES (1, 1, N'Tomatoes are good for diabetes', NULL)
INSERT [FoodData].[FoodAndDiseases] ([ListOfDiseasesId], [FoodListId], [Describe], [IsGoodOrBad]) VALUES (2, 2, N'Beef should be avoided for heart disease', 0)
INSERT [FoodData].[FoodAndDiseases] ([ListOfDiseasesId], [FoodListId], [Describe], [IsGoodOrBad]) VALUES (3, 3, N'Yogurt helps digestion', 1)
INSERT [FoodData].[FoodAndDiseases] ([ListOfDiseasesId], [FoodListId], [Describe], [IsGoodOrBad]) VALUES (4, 4, N'Apples are good for weight loss', 1)
GO
SET IDENTITY_INSERT [FoodData].[FoodList] ON 

INSERT [FoodData].[FoodList] ([FoodListId], [Name], [Describe], [Rate], [NumberRate], [URLImage], [FoodTypeId], [KeyNoteId], [IsActive], [PreparationTime], [CookingTime], [CookingDifficultyId]) VALUES (1, N'Trứng chiên xúc xích', N'Fresh vegetable', 89.6, 78, NULL, 2, 1, 1, 20, 24, 1)
INSERT [FoodData].[FoodList] ([FoodListId], [Name], [Describe], [Rate], [NumberRate], [URLImage], [FoodTypeId], [KeyNoteId], [IsActive], [PreparationTime], [CookingTime], [CookingDifficultyId]) VALUES (2, N'Bánh mì ốp la thịt bò', N'Rich in protein', 86.9, 56, NULL, 2, 1, 1, 30, 35, 3)
INSERT [FoodData].[FoodList] ([FoodListId], [Name], [Describe], [Rate], [NumberRate], [URLImage], [FoodTypeId], [KeyNoteId], [IsActive], [PreparationTime], [CookingTime], [CookingDifficultyId]) VALUES (3, N'Salad gà cần tây', N'Dairy product', 90.6, 34, NULL, 1, 1, 1, 40, 32, 2)
INSERT [FoodData].[FoodList] ([FoodListId], [Name], [Describe], [Rate], [NumberRate], [URLImage], [FoodTypeId], [KeyNoteId], [IsActive], [PreparationTime], [CookingTime], [CookingDifficultyId]) VALUES (4, N'Đậu phụ xào rau muống', N'Sweet fruit', 75.8, 46, NULL, 1, 1, 1, 40, 27, 4)
INSERT [FoodData].[FoodList] ([FoodListId], [Name], [Describe], [Rate], [NumberRate], [URLImage], [FoodTypeId], [KeyNoteId], [IsActive], [PreparationTime], [CookingTime], [CookingDifficultyId]) VALUES (5, N'Thịt bò xào cần tây', N'Sweet fruit', 67.9, 73, NULL, 2, 1, 1, 30, 45, 1)
INSERT [FoodData].[FoodList] ([FoodListId], [Name], [Describe], [Rate], [NumberRate], [URLImage], [FoodTypeId], [KeyNoteId], [IsActive], [PreparationTime], [CookingTime], [CookingDifficultyId]) VALUES (6, N'Đậu phụ', N'Sweet fruit', 67.9, 73, NULL, 2, 2, 1, 20, 0, 4)
SET IDENTITY_INSERT [FoodData].[FoodList] OFF
GO
SET IDENTITY_INSERT [FoodData].[FoodType] ON 

INSERT [FoodData].[FoodType] ([FoodTypeId], [Name], [Describe]) VALUES (1, N'Vegetable', N'Plant-based food')
INSERT [FoodData].[FoodType] ([FoodTypeId], [Name], [Describe]) VALUES (2, N'Meat', N'Animal-based food')
INSERT [FoodData].[FoodType] ([FoodTypeId], [Name], [Describe]) VALUES (3, N'Dairy', N'Milk-based products')
INSERT [FoodData].[FoodType] ([FoodTypeId], [Name], [Describe]) VALUES (4, N'Fruit', N'Edible fruit products')
SET IDENTITY_INSERT [FoodData].[FoodType] OFF
GO
SET IDENTITY_INSERT [FoodData].[IngredientDetails100g] ON 

INSERT [FoodData].[IngredientDetails100g] ([Id], [KeyNoteId], [Name], [Describe], [URLImage], [TypeOfCalculationId], [Energy], [Water], [Protein], [Fat], [Carbohydrate], [Fiber], [Ash], [Sugar], [Galactose], [Maltose], [Lactose], [Fructose], [Glucose], [Sucrose], [Calcium], [Iron], [Magnesium], [Manganese], [Phosphorous], [Potassium], [Sodium], [Zinc], [Copper], [Selenium], [Vitamin_C], [Vitamin_B1], [Vitamin_B2], [Vitamin_PP], [Vitamin_B5], [Vitamin_B6], [Folat], [Vitamin_B9], [Vitamin_H], [Vitamin_B12], [Vitamin_A], [Vitamin_D], [Vitamin_E], [Vitamin_K], [Beta_caroten], [Alpha_caroten], [Beta_cryptoxanthin], [Lycopen], [LuteinVsZeaxanthin], [Purin], [Total_isoflavone], [Daidzein], [Genistein], [Glycetin], [Total_saturated_fatty_acid], [Palmitic_C16_0], [Margaric_C17_0], [Stearic_C18_0], [Arachidic_C20_0], [Behenic_C22_0], [Lignoceric_C24_0], [TotalMonounsaturatedFattyAcid], [Myristoleic_C14_1], [Palmitoleic_C16_1], [Oleic_C18_1], [TotalPolyunsaturatedFattyAcid], [Linoleic_C18_2_n6], [Linolenic_C18_2_n3], [Arachidonic_C20_4], [Eicosapentaenoic_C20_5_n3], [Docosahexaenoic_C22_6_n3], [TotalTransFattyAcid], [Cholesterol], [Phytosterol], [Lysin], [Methionin], [Tryptophan], [Phenylalanin], [Threonin], [Valin], [Leucin], [Isoleucin], [Arginin], [Histidin], [Cystin], [Tyrosin], [Alanin], [Acid_aspartic], [Acid_glutamic], [Glycin], [Prolin], [Serin]) VALUES (1, NULL, N'Trứng', N'Nguyên liệu phổ biến trong nấu ăn, giàu protein và chất béo.', N'https://example.com/trung.jpg', 1, 155, 74, 12.6, 10.6, 1.1, 0, 0.5, 0, 0, 0, 0, 0, 0, 0, 50, 1.2, 10, 0.03, 172, 126, 140, 0.7, 0.02, 0.2, 0, 0.05, 0.03, 0.09, 0.05, 0.04, 0, 0.03, 640, 0, 1.05, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3.3, 1.5, 0, 1.3, 0.03, 0, 5.7, 0, 0.03, 4.6, 0.8, 0, 0, 0, 0, 0, 0.4, 0, 0.6, 0.7, 0, 0.2, 0.1, 0.7, 1.2, 0.9, 0.8, 0.6, 0.5, 1.1, 0.4, 0.5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
INSERT [FoodData].[IngredientDetails100g] ([Id], [KeyNoteId], [Name], [Describe], [URLImage], [TypeOfCalculationId], [Energy], [Water], [Protein], [Fat], [Carbohydrate], [Fiber], [Ash], [Sugar], [Galactose], [Maltose], [Lactose], [Fructose], [Glucose], [Sucrose], [Calcium], [Iron], [Magnesium], [Manganese], [Phosphorous], [Potassium], [Sodium], [Zinc], [Copper], [Selenium], [Vitamin_C], [Vitamin_B1], [Vitamin_B2], [Vitamin_PP], [Vitamin_B5], [Vitamin_B6], [Folat], [Vitamin_B9], [Vitamin_H], [Vitamin_B12], [Vitamin_A], [Vitamin_D], [Vitamin_E], [Vitamin_K], [Beta_caroten], [Alpha_caroten], [Beta_cryptoxanthin], [Lycopen], [LuteinVsZeaxanthin], [Purin], [Total_isoflavone], [Daidzein], [Genistein], [Glycetin], [Total_saturated_fatty_acid], [Palmitic_C16_0], [Margaric_C17_0], [Stearic_C18_0], [Arachidic_C20_0], [Behenic_C22_0], [Lignoceric_C24_0], [TotalMonounsaturatedFattyAcid], [Myristoleic_C14_1], [Palmitoleic_C16_1], [Oleic_C18_1], [TotalPolyunsaturatedFattyAcid], [Linoleic_C18_2_n6], [Linolenic_C18_2_n3], [Arachidonic_C20_4], [Eicosapentaenoic_C20_5_n3], [Docosahexaenoic_C22_6_n3], [TotalTransFattyAcid], [Cholesterol], [Phytosterol], [Lysin], [Methionin], [Tryptophan], [Phenylalanin], [Threonin], [Valin], [Leucin], [Isoleucin], [Arginin], [Histidin], [Cystin], [Tyrosin], [Alanin], [Acid_aspartic], [Acid_glutamic], [Glycin], [Prolin], [Serin]) VALUES (2, NULL, N'Thịt bò', N'Thịt đỏ, giàu protein và sắt.', N'https://example.com/thit-bo.jpg', 2, 250, 55, 26, 17, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 300, 2.7, 24, 0.02, 180, 352, 72, 8, 0.12, 0.1, 0, 0.06, 0.04, 0.1, 0.07, 0.06, 0, 0.03, 50, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6, 2.5, 0, 1.6, 0.02, 0, 8, 0, 0.1, 7.8, 0.6, 0, 0, 0, 0, 0, 0.7, 0, 1.2, 0.5, 0, 0.3, 0.2, 0.8, 1.3, 1.1, 1, 0.8, 0.7, 1.2, 0.5, 0.7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
INSERT [FoodData].[IngredientDetails100g] ([Id], [KeyNoteId], [Name], [Describe], [URLImage], [TypeOfCalculationId], [Energy], [Water], [Protein], [Fat], [Carbohydrate], [Fiber], [Ash], [Sugar], [Galactose], [Maltose], [Lactose], [Fructose], [Glucose], [Sucrose], [Calcium], [Iron], [Magnesium], [Manganese], [Phosphorous], [Potassium], [Sodium], [Zinc], [Copper], [Selenium], [Vitamin_C], [Vitamin_B1], [Vitamin_B2], [Vitamin_PP], [Vitamin_B5], [Vitamin_B6], [Folat], [Vitamin_B9], [Vitamin_H], [Vitamin_B12], [Vitamin_A], [Vitamin_D], [Vitamin_E], [Vitamin_K], [Beta_caroten], [Alpha_caroten], [Beta_cryptoxanthin], [Lycopen], [LuteinVsZeaxanthin], [Purin], [Total_isoflavone], [Daidzein], [Genistein], [Glycetin], [Total_saturated_fatty_acid], [Palmitic_C16_0], [Margaric_C17_0], [Stearic_C18_0], [Arachidic_C20_0], [Behenic_C22_0], [Lignoceric_C24_0], [TotalMonounsaturatedFattyAcid], [Myristoleic_C14_1], [Palmitoleic_C16_1], [Oleic_C18_1], [TotalPolyunsaturatedFattyAcid], [Linoleic_C18_2_n6], [Linolenic_C18_2_n3], [Arachidonic_C20_4], [Eicosapentaenoic_C20_5_n3], [Docosahexaenoic_C22_6_n3], [TotalTransFattyAcid], [Cholesterol], [Phytosterol], [Lysin], [Methionin], [Tryptophan], [Phenylalanin], [Threonin], [Valin], [Leucin], [Isoleucin], [Arginin], [Histidin], [Cystin], [Tyrosin], [Alanin], [Acid_aspartic], [Acid_glutamic], [Glycin], [Prolin], [Serin]) VALUES (3, NULL, N'Thịt gà', N'Thịt trắng, dễ tiêu hoá, giàu protein.', N'https://example.com/thit-ga.jpg', 2, 165, 70, 31, 3.6, 0, 0, 0, 0, 0, 0, 0, 0, 0, 12, 12, 1, 20, 0.01, 150, 225, 50, 6, 0.09, 0.09, 0, 0.02, 0.02, 0.08, 0.04, 0.05, 0, 0.01, 0, 0, 0.8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 1.7, 0, 1.2, 0.02, 0, 5, 0, 0.07, 4.8, 0.5, 0, 0, 0, 0, 0, 0.4, 0, 0.7, 0.4, 0, 0.2, 0.1, 0.6, 1, 0.8, 0.7, 0.6, 0.4, 1, 0.3, 0.4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
INSERT [FoodData].[IngredientDetails100g] ([Id], [KeyNoteId], [Name], [Describe], [URLImage], [TypeOfCalculationId], [Energy], [Water], [Protein], [Fat], [Carbohydrate], [Fiber], [Ash], [Sugar], [Galactose], [Maltose], [Lactose], [Fructose], [Glucose], [Sucrose], [Calcium], [Iron], [Magnesium], [Manganese], [Phosphorous], [Potassium], [Sodium], [Zinc], [Copper], [Selenium], [Vitamin_C], [Vitamin_B1], [Vitamin_B2], [Vitamin_PP], [Vitamin_B5], [Vitamin_B6], [Folat], [Vitamin_B9], [Vitamin_H], [Vitamin_B12], [Vitamin_A], [Vitamin_D], [Vitamin_E], [Vitamin_K], [Beta_caroten], [Alpha_caroten], [Beta_cryptoxanthin], [Lycopen], [LuteinVsZeaxanthin], [Purin], [Total_isoflavone], [Daidzein], [Genistein], [Glycetin], [Total_saturated_fatty_acid], [Palmitic_C16_0], [Margaric_C17_0], [Stearic_C18_0], [Arachidic_C20_0], [Behenic_C22_0], [Lignoceric_C24_0], [TotalMonounsaturatedFattyAcid], [Myristoleic_C14_1], [Palmitoleic_C16_1], [Oleic_C18_1], [TotalPolyunsaturatedFattyAcid], [Linoleic_C18_2_n6], [Linolenic_C18_2_n3], [Arachidonic_C20_4], [Eicosapentaenoic_C20_5_n3], [Docosahexaenoic_C22_6_n3], [TotalTransFattyAcid], [Cholesterol], [Phytosterol], [Lysin], [Methionin], [Tryptophan], [Phenylalanin], [Threonin], [Valin], [Leucin], [Isoleucin], [Arginin], [Histidin], [Cystin], [Tyrosin], [Alanin], [Acid_aspartic], [Acid_glutamic], [Glycin], [Prolin], [Serin]) VALUES (4, NULL, N'Rau muống', N'Rau xanh giàu chất xơ và vitamin.', N'https://example.com/rau-muong.jpg', 3, 19, 91.2, 2.6, 0.3, 3.5, 2.1, 0, 0, 0, 0, 0, 0, 0, 0, 60, 0.5, 30, 0.05, 40, 260, 70, 0.4, 0.03, 0.01, 0, 0.02, 0.03, 0.09, 0.04, 0.02, 0, 0.01, 500, 0, 0.4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0.3, 0.1, 0, 0.2, 0.01, 0, 0.4, 0, 0.02, 0.4, 0.05, 0, 0, 0, 0, 0, 0.05, 0, 0.09, 0.05, 0, 0.03, 0.02, 0.1, 0.2, 0.2, 0.2, 0.1, 0.1, 0.3, 0.1, 0.1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
INSERT [FoodData].[IngredientDetails100g] ([Id], [KeyNoteId], [Name], [Describe], [URLImage], [TypeOfCalculationId], [Energy], [Water], [Protein], [Fat], [Carbohydrate], [Fiber], [Ash], [Sugar], [Galactose], [Maltose], [Lactose], [Fructose], [Glucose], [Sucrose], [Calcium], [Iron], [Magnesium], [Manganese], [Phosphorous], [Potassium], [Sodium], [Zinc], [Copper], [Selenium], [Vitamin_C], [Vitamin_B1], [Vitamin_B2], [Vitamin_PP], [Vitamin_B5], [Vitamin_B6], [Folat], [Vitamin_B9], [Vitamin_H], [Vitamin_B12], [Vitamin_A], [Vitamin_D], [Vitamin_E], [Vitamin_K], [Beta_caroten], [Alpha_caroten], [Beta_cryptoxanthin], [Lycopen], [LuteinVsZeaxanthin], [Purin], [Total_isoflavone], [Daidzein], [Genistein], [Glycetin], [Total_saturated_fatty_acid], [Palmitic_C16_0], [Margaric_C17_0], [Stearic_C18_0], [Arachidic_C20_0], [Behenic_C22_0], [Lignoceric_C24_0], [TotalMonounsaturatedFattyAcid], [Myristoleic_C14_1], [Palmitoleic_C16_1], [Oleic_C18_1], [TotalPolyunsaturatedFattyAcid], [Linoleic_C18_2_n6], [Linolenic_C18_2_n3], [Arachidonic_C20_4], [Eicosapentaenoic_C20_5_n3], [Docosahexaenoic_C22_6_n3], [TotalTransFattyAcid], [Cholesterol], [Phytosterol], [Lysin], [Methionin], [Tryptophan], [Phenylalanin], [Threonin], [Valin], [Leucin], [Isoleucin], [Arginin], [Histidin], [Cystin], [Tyrosin], [Alanin], [Acid_aspartic], [Acid_glutamic], [Glycin], [Prolin], [Serin]) VALUES (6, NULL, N'Xúc xích', N'Nguyên liệu chế biến từ thịt, giàu năng lượng.', N'https://example.com/xuc-xich.jpg', 4, 320, 52, 10, 27, 1.5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 200, 1, 30, 0.04, 120, 250, 900, 0.3, 0.05, 0.02, 0, 0.03, 0.02, 0.06, 0.03, 0.02, 0, 0.03, 1200, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 6.7, 3.2, 0, 1.5, 0.02, 0, 10, 0, 0.05, 8, 0.6, 0, 0, 0, 0, 0, 0.6, 0, 1, 0.9, 0, 0.3, 0.2, 0.7, 1.3, 1, 0.9, 0.8, 0.6, 1.3, 0.5, 0.7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
INSERT [FoodData].[IngredientDetails100g] ([Id], [KeyNoteId], [Name], [Describe], [URLImage], [TypeOfCalculationId], [Energy], [Water], [Protein], [Fat], [Carbohydrate], [Fiber], [Ash], [Sugar], [Galactose], [Maltose], [Lactose], [Fructose], [Glucose], [Sucrose], [Calcium], [Iron], [Magnesium], [Manganese], [Phosphorous], [Potassium], [Sodium], [Zinc], [Copper], [Selenium], [Vitamin_C], [Vitamin_B1], [Vitamin_B2], [Vitamin_PP], [Vitamin_B5], [Vitamin_B6], [Folat], [Vitamin_B9], [Vitamin_H], [Vitamin_B12], [Vitamin_A], [Vitamin_D], [Vitamin_E], [Vitamin_K], [Beta_caroten], [Alpha_caroten], [Beta_cryptoxanthin], [Lycopen], [LuteinVsZeaxanthin], [Purin], [Total_isoflavone], [Daidzein], [Genistein], [Glycetin], [Total_saturated_fatty_acid], [Palmitic_C16_0], [Margaric_C17_0], [Stearic_C18_0], [Arachidic_C20_0], [Behenic_C22_0], [Lignoceric_C24_0], [TotalMonounsaturatedFattyAcid], [Myristoleic_C14_1], [Palmitoleic_C16_1], [Oleic_C18_1], [TotalPolyunsaturatedFattyAcid], [Linoleic_C18_2_n6], [Linolenic_C18_2_n3], [Arachidonic_C20_4], [Eicosapentaenoic_C20_5_n3], [Docosahexaenoic_C22_6_n3], [TotalTransFattyAcid], [Cholesterol], [Phytosterol], [Lysin], [Methionin], [Tryptophan], [Phenylalanin], [Threonin], [Valin], [Leucin], [Isoleucin], [Arginin], [Histidin], [Cystin], [Tyrosin], [Alanin], [Acid_aspartic], [Acid_glutamic], [Glycin], [Prolin], [Serin]) VALUES (11, NULL, N'Cần tây', N'Rau xanh, giàu chất xơ và vitamin.', N'https://example.com/can-tay.jpg', 5, 16, 94, 0.7, 0.2, 3, 1.8, 0.1, 0, 0, 0, 0, 0, 0, 0, 40, 0.5, 11, 0.03, 26, 260, 80, 0.2, 0.03, 0.01, 0, 0.02, 0.02, 0.07, 0.03, 0.02, 0, 0.01, 450, 0, 0.3, 0, 0, 0, 0, 0, 0, 0, 0, 0.2, 0.1, 0, 0.15, 0.01, 0, 0.3, 0, 0.02, 0.3, 0.04, 0, 0, 0, 0, 0, 0.05, 0, 0.08, 0.05, 0, 0.03, 0.02, 0.1, 0.2, 0.1, 0.1, 0.1, 0.1, 0.2, 0.1, 0.1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
INSERT [FoodData].[IngredientDetails100g] ([Id], [KeyNoteId], [Name], [Describe], [URLImage], [TypeOfCalculationId], [Energy], [Water], [Protein], [Fat], [Carbohydrate], [Fiber], [Ash], [Sugar], [Galactose], [Maltose], [Lactose], [Fructose], [Glucose], [Sucrose], [Calcium], [Iron], [Magnesium], [Manganese], [Phosphorous], [Potassium], [Sodium], [Zinc], [Copper], [Selenium], [Vitamin_C], [Vitamin_B1], [Vitamin_B2], [Vitamin_PP], [Vitamin_B5], [Vitamin_B6], [Folat], [Vitamin_B9], [Vitamin_H], [Vitamin_B12], [Vitamin_A], [Vitamin_D], [Vitamin_E], [Vitamin_K], [Beta_caroten], [Alpha_caroten], [Beta_cryptoxanthin], [Lycopen], [LuteinVsZeaxanthin], [Purin], [Total_isoflavone], [Daidzein], [Genistein], [Glycetin], [Total_saturated_fatty_acid], [Palmitic_C16_0], [Margaric_C17_0], [Stearic_C18_0], [Arachidic_C20_0], [Behenic_C22_0], [Lignoceric_C24_0], [TotalMonounsaturatedFattyAcid], [Myristoleic_C14_1], [Palmitoleic_C16_1], [Oleic_C18_1], [TotalPolyunsaturatedFattyAcid], [Linoleic_C18_2_n6], [Linolenic_C18_2_n3], [Arachidonic_C20_4], [Eicosapentaenoic_C20_5_n3], [Docosahexaenoic_C22_6_n3], [TotalTransFattyAcid], [Cholesterol], [Phytosterol], [Lysin], [Methionin], [Tryptophan], [Phenylalanin], [Threonin], [Valin], [Leucin], [Isoleucin], [Arginin], [Histidin], [Cystin], [Tyrosin], [Alanin], [Acid_aspartic], [Acid_glutamic], [Glycin], [Prolin], [Serin]) VALUES (12, NULL, N'Tỏi', N'Nguyên liệu thường dùng làm gia vị, giàu allicin.', N'https://example.com/toi.jpg', 5, 149, 58, 6.4, 0.5, 33, 2.1, 0, 0, 0, 0, 0, 0, 0, 0, 181, 1.7, 25, 0.2, 153, 401, 17, 0.6, 0.15, 0.02, 0, 0.02, 0.02, 0.08, 0.03, 0.04, 0, 0.03, 0, 0, 0.7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1.4, 0.7, 0, 0.5, 0.03, 0, 2.3, 0, 0.04, 1.8, 0.6, 0, 0, 0, 0, 0, 0.3, 0, 0.4, 0.2, 0, 0.2, 0.1, 0.5, 0.8, 0.6, 0.5, 0.4, 0.3, 0.8, 0.3, 0.4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
INSERT [FoodData].[IngredientDetails100g] ([Id], [KeyNoteId], [Name], [Describe], [URLImage], [TypeOfCalculationId], [Energy], [Water], [Protein], [Fat], [Carbohydrate], [Fiber], [Ash], [Sugar], [Galactose], [Maltose], [Lactose], [Fructose], [Glucose], [Sucrose], [Calcium], [Iron], [Magnesium], [Manganese], [Phosphorous], [Potassium], [Sodium], [Zinc], [Copper], [Selenium], [Vitamin_C], [Vitamin_B1], [Vitamin_B2], [Vitamin_PP], [Vitamin_B5], [Vitamin_B6], [Folat], [Vitamin_B9], [Vitamin_H], [Vitamin_B12], [Vitamin_A], [Vitamin_D], [Vitamin_E], [Vitamin_K], [Beta_caroten], [Alpha_caroten], [Beta_cryptoxanthin], [Lycopen], [LuteinVsZeaxanthin], [Purin], [Total_isoflavone], [Daidzein], [Genistein], [Glycetin], [Total_saturated_fatty_acid], [Palmitic_C16_0], [Margaric_C17_0], [Stearic_C18_0], [Arachidic_C20_0], [Behenic_C22_0], [Lignoceric_C24_0], [TotalMonounsaturatedFattyAcid], [Myristoleic_C14_1], [Palmitoleic_C16_1], [Oleic_C18_1], [TotalPolyunsaturatedFattyAcid], [Linoleic_C18_2_n6], [Linolenic_C18_2_n3], [Arachidonic_C20_4], [Eicosapentaenoic_C20_5_n3], [Docosahexaenoic_C22_6_n3], [TotalTransFattyAcid], [Cholesterol], [Phytosterol], [Lysin], [Methionin], [Tryptophan], [Phenylalanin], [Threonin], [Valin], [Leucin], [Isoleucin], [Arginin], [Histidin], [Cystin], [Tyrosin], [Alanin], [Acid_aspartic], [Acid_glutamic], [Glycin], [Prolin], [Serin]) VALUES (13, NULL, N'Hành lá', N'Nguyên liệu làm gia vị, giàu vitamin C và chất xơ.', N'https://example.com/hanh-la.jpg', 5, 32, 92.5, 1.3, 0.2, 7.3, 2.6, 0, 0, 0, 0, 0, 0, 0, 0, 72, 0.3, 20, 0.03, 30, 270, 50, 0.2, 0.03, 0.02, 0, 0.02, 0.02, 0.06, 0.02, 0.02, 0, 0.01, 380, 0, 0.3, 0, 0, 0, 0, 0, 0, 0, 0, 0.4, 0.2, 0, 0.1, 0.01, 0, 0.5, 0, 0.02, 0.5, 0.04, 0, 0, 0, 0, 0, 0.05, 0, 0.08, 0.05, 0, 0.03, 0.02, 0.1, 0.2, 0.2, 0.1, 0.1, 0.1, 0.2, 0.1, 0.1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
INSERT [FoodData].[IngredientDetails100g] ([Id], [KeyNoteId], [Name], [Describe], [URLImage], [TypeOfCalculationId], [Energy], [Water], [Protein], [Fat], [Carbohydrate], [Fiber], [Ash], [Sugar], [Galactose], [Maltose], [Lactose], [Fructose], [Glucose], [Sucrose], [Calcium], [Iron], [Magnesium], [Manganese], [Phosphorous], [Potassium], [Sodium], [Zinc], [Copper], [Selenium], [Vitamin_C], [Vitamin_B1], [Vitamin_B2], [Vitamin_PP], [Vitamin_B5], [Vitamin_B6], [Folat], [Vitamin_B9], [Vitamin_H], [Vitamin_B12], [Vitamin_A], [Vitamin_D], [Vitamin_E], [Vitamin_K], [Beta_caroten], [Alpha_caroten], [Beta_cryptoxanthin], [Lycopen], [LuteinVsZeaxanthin], [Purin], [Total_isoflavone], [Daidzein], [Genistein], [Glycetin], [Total_saturated_fatty_acid], [Palmitic_C16_0], [Margaric_C17_0], [Stearic_C18_0], [Arachidic_C20_0], [Behenic_C22_0], [Lignoceric_C24_0], [TotalMonounsaturatedFattyAcid], [Myristoleic_C14_1], [Palmitoleic_C16_1], [Oleic_C18_1], [TotalPolyunsaturatedFattyAcid], [Linoleic_C18_2_n6], [Linolenic_C18_2_n3], [Arachidonic_C20_4], [Eicosapentaenoic_C20_5_n3], [Docosahexaenoic_C22_6_n3], [TotalTransFattyAcid], [Cholesterol], [Phytosterol], [Lysin], [Methionin], [Tryptophan], [Phenylalanin], [Threonin], [Valin], [Leucin], [Isoleucin], [Arginin], [Histidin], [Cystin], [Tyrosin], [Alanin], [Acid_aspartic], [Acid_glutamic], [Glycin], [Prolin], [Serin]) VALUES (14, NULL, N'Đậu phụ', N'Nguyên liệu làm từ đậu nành, giàu protein.', N'https://example.com/dau-phu.jpg', 6, 76, 84, 8, 4.8, 1.6, 0, 0, 0, 0, 0, 0, 0, 0, 0, 350, 1.5, 60, 0.02, 200, 150, 7, 1, 0.05, 0.02, 0, 0.02, 0.02, 0.08, 0.02, 0.03, 0, 0.02, 0, 0, 0.8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5.4, 2.5, 0, 1.5, 0.03, 0, 7, 0, 0.06, 5.8, 0.4, 0, 0, 0, 0, 0, 0.5, 0, 1, 0.6, 0, 0.2, 0.1, 0.7, 1.2, 1, 0.8, 0.6, 0.4, 1, 0.5, 0.6, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
SET IDENTITY_INSERT [FoodData].[IngredientDetails100g] OFF
GO
SET IDENTITY_INSERT [FoodData].[KeyNote] ON 

INSERT [FoodData].[KeyNote] ([Id], [KeyList]) VALUES (1, N'#WantCooking=1#SlotOfTheDay:1;2;#Size=Bữa lớn')
INSERT [FoodData].[KeyNote] ([Id], [KeyList]) VALUES (2, N'#WantCooking=2#SlotOfTheDay:3;4;#Size=Bữa lớn')
INSERT [FoodData].[KeyNote] ([Id], [KeyList]) VALUES (3, N'#WantCooking=1#SlotOfTheDay:4;#Size=Bữa lớn')
INSERT [FoodData].[KeyNote] ([Id], [KeyList]) VALUES (4, N'#WantCooking=2#SlotOfTheDay:4;#Size=Bữa lớn')
INSERT [FoodData].[KeyNote] ([Id], [KeyList]) VALUES (5, N'#WantCooking=2#SlotOfTheDay:3;#Size=Bữa nhỏ')
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

INSERT [FoodData].[TypeOfCalculation] ([TypeId], [CalculationForm]) VALUES (1, N'#Medium=50#TypeOfCalculation:Quả-50;2 Quả-100;3 Quả-150')
INSERT [FoodData].[TypeOfCalculation] ([TypeId], [CalculationForm]) VALUES (2, N'#Medium=100#TypeOfCalculation:Lạng-100;Cân-1000;Miếng-150')
INSERT [FoodData].[TypeOfCalculation] ([TypeId], [CalculationForm]) VALUES (3, N'#Medium=300#TypeOfCalculation:Bó-300;Mớ-200;Cân-1000')
INSERT [FoodData].[TypeOfCalculation] ([TypeId], [CalculationForm]) VALUES (4, N'#Medium=30#TypeOfCalculation:Cái-30;Túi-300;Vỉ-300')
INSERT [FoodData].[TypeOfCalculation] ([TypeId], [CalculationForm]) VALUES (5, N'#Medium=100#TypeOfCalculation:Củ-100;Nhánh-30')
INSERT [FoodData].[TypeOfCalculation] ([TypeId], [CalculationForm]) VALUES (6, N'#Medium=200#TypeOfCalculation:Cái-200;Chiếc-200')
INSERT [FoodData].[TypeOfCalculation] ([TypeId], [CalculationForm]) VALUES (7, N'Unknow')
INSERT [FoodData].[TypeOfCalculation] ([TypeId], [CalculationForm]) VALUES (8, N'Unknow')
INSERT [FoodData].[TypeOfCalculation] ([TypeId], [CalculationForm]) VALUES (9, N'Unknow')
SET IDENTITY_INSERT [FoodData].[TypeOfCalculation] OFF
GO
SET IDENTITY_INSERT [UserData].[BankInformation] ON 

INSERT [UserData].[BankInformation] ([Id], [UserID], [BankAccount], [TypeBank], [IsUsed]) VALUES (1, 1, N'0569000899', N'MBBank', 1)
SET IDENTITY_INSERT [UserData].[BankInformation] OFF
GO
SET IDENTITY_INSERT [UserData].[ExerciseIntensity] ON 

INSERT [UserData].[ExerciseIntensity] ([Id], [Name], [Describe], [ListKey]) VALUES (1, N'Low', N'Suitable for light activities', N'Low001')
INSERT [UserData].[ExerciseIntensity] ([Id], [Name], [Describe], [ListKey]) VALUES (2, N'Medium', N'Suitable for moderate activities', N'Med002')
INSERT [UserData].[ExerciseIntensity] ([Id], [Name], [Describe], [ListKey]) VALUES (3, N'High', N'Suitable for intense activities', N'High003')
INSERT [UserData].[ExerciseIntensity] ([Id], [Name], [Describe], [ListKey]) VALUES (4, N'Very High', N'Suitable for extreme activities', N'VHigh004')
SET IDENTITY_INSERT [UserData].[ExerciseIntensity] OFF
GO
SET IDENTITY_INSERT [UserData].[ExpertPackages] ON 

INSERT [UserData].[ExpertPackages] ([Id], [NutritionistDetailsId], [Name], [Describe], [Price], [Duration]) VALUES (1, 1, N'Basic Package', N'A starter package', 100.0000, 30)
INSERT [UserData].[ExpertPackages] ([Id], [NutritionistDetailsId], [Name], [Describe], [Price], [Duration]) VALUES (2, 2, N'Advanced Package', N'A comprehensive package', 200.0000, 60)
INSERT [UserData].[ExpertPackages] ([Id], [NutritionistDetailsId], [Name], [Describe], [Price], [Duration]) VALUES (3, 3, N'Premium Package', N'All-in-one package', 300.0000, 90)
INSERT [UserData].[ExpertPackages] ([Id], [NutritionistDetailsId], [Name], [Describe], [Price], [Duration]) VALUES (4, 4, N'Customized Package', N'Tailored to individual needs', 400.0000, 120)
SET IDENTITY_INSERT [UserData].[ExpertPackages] OFF
GO
INSERT [UserData].[FoodSelection] ([UserId], [FoodListId], [Rate], [RecurringId], [IsBlock], [IsCollection], [IsLike]) VALUES (1, 1, 5, 1, 0, NULL, NULL)
INSERT [UserData].[FoodSelection] ([UserId], [FoodListId], [Rate], [RecurringId], [IsBlock], [IsCollection], [IsLike]) VALUES (2, 2, 4, 2, NULL, NULL, NULL)
INSERT [UserData].[FoodSelection] ([UserId], [FoodListId], [Rate], [RecurringId], [IsBlock], [IsCollection], [IsLike]) VALUES (3, 3, 5, 3, NULL, NULL, NULL)
INSERT [UserData].[FoodSelection] ([UserId], [FoodListId], [Rate], [RecurringId], [IsBlock], [IsCollection], [IsLike]) VALUES (4, 4, 5, 4, NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [UserData].[ListOfDiseases] ON 

INSERT [UserData].[ListOfDiseases] ([Id], [Name], [Describe]) VALUES (1, N'Béo phì', N'A disease that causes high blood sugar')
INSERT [UserData].[ListOfDiseases] ([Id], [Name], [Describe]) VALUES (2, N'Gan Nhiễm Mỡ', N'High blood pressure condition')
INSERT [UserData].[ListOfDiseases] ([Id], [Name], [Describe]) VALUES (3, N'Lậu', N'Chronic respiratory condition')
INSERT [UserData].[ListOfDiseases] ([Id], [Name], [Describe]) VALUES (4, N'Dạ dày', N'A condition affecting heart health')
SET IDENTITY_INSERT [UserData].[ListOfDiseases] OFF
GO
SET IDENTITY_INSERT [UserData].[MealSettings] ON 

INSERT [UserData].[MealSettings] ([Id], [UserId], [DayOfTheWeekStartId], [SameScheduleEveryDay]) VALUES (1, 1, 8, 1)
SET IDENTITY_INSERT [UserData].[MealSettings] OFF
GO
SET IDENTITY_INSERT [UserData].[MealSettingsDetails] ON 

INSERT [UserData].[MealSettingsDetails] ([Id], [MealSettingsId], [SlotOfTheDayId], [NutritionTargetsDailyId], [DayOfTheWeekId], [SkipCreationProcess], [Size], [NutritionFocus], [NumberOfDishes], [TypeFavoriteFood], [WantCookingId], [TimeAvailable], [CookingDifficultyId], [Name]) VALUES (1, 1, 1, 2, 1, 1, N'Bữa lớn', 0, 2, N'1', 1, 50, 1, NULL)
SET IDENTITY_INSERT [UserData].[MealSettingsDetails] OFF
GO
SET IDENTITY_INSERT [UserData].[NutritionistDetails] ON 

INSERT [UserData].[NutritionistDetails] ([Id], [NutritionistId], [DescribeYourself], [Height], [Weight], [Age], [Rate], [NumberRate]) VALUES (1, 1, N'Experienced nutritionist', 180, 75, NULL, NULL, NULL)
INSERT [UserData].[NutritionistDetails] ([Id], [NutritionistId], [DescribeYourself], [Height], [Weight], [Age], [Rate], [NumberRate]) VALUES (2, 2, N'Fitness expert with focus on healthy eating', 165, 60, NULL, NULL, NULL)
INSERT [UserData].[NutritionistDetails] ([Id], [NutritionistId], [DescribeYourself], [Height], [Weight], [Age], [Rate], [NumberRate]) VALUES (3, 3, N'Specializes in plant-based diets', 170, 65, NULL, NULL, NULL)
INSERT [UserData].[NutritionistDetails] ([Id], [NutritionistId], [DescribeYourself], [Height], [Weight], [Age], [Rate], [NumberRate]) VALUES (4, 4, N'Dietitian with over 10 years of experience', 175, 80, NULL, NULL, NULL)
SET IDENTITY_INSERT [UserData].[NutritionistDetails] OFF
GO
SET IDENTITY_INSERT [UserData].[NutritionTargetsDaily] ON 

INSERT [UserData].[NutritionTargetsDaily] ([Id], [UserId], [Title], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber], [LimitDailySodium], [LimitDailyCholesterol], [ExerciseIntensityId], [FoodTypeIdWant], [AvoidIngredient], [IsActive]) VALUES (1, 1, N'Daily Nutrition Target', 325, 0, 70, 0, 500, 0, 500, 40, 0, 0, 1, 2, N'1;2;3;', NULL)
INSERT [UserData].[NutritionTargetsDaily] ([Id], [UserId], [Title], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber], [LimitDailySodium], [LimitDailyCholesterol], [ExerciseIntensityId], [FoodTypeIdWant], [AvoidIngredient], [IsActive]) VALUES (2, 2, N'Muscle Building Target', 2500, 0, 500, 0, 5000, 0, 500, 40, 1, 1, 2, 2, N'2;3;', NULL)
INSERT [UserData].[NutritionTargetsDaily] ([Id], [UserId], [Title], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber], [LimitDailySodium], [LimitDailyCholesterol], [ExerciseIntensityId], [FoodTypeIdWant], [AvoidIngredient], [IsActive]) VALUES (3, 3, N'Weight Loss Target', 1500, 40, 70, 40, 70, 40, 70, 40, 1, 1, 3, 3, N'4;', NULL)
INSERT [UserData].[NutritionTargetsDaily] ([Id], [UserId], [Title], [Calories], [CarbsMin], [CarbsMax], [FatsMin], [FatsMax], [ProteinMin], [ProteinMax], [MinimumFiber], [LimitDailySodium], [LimitDailyCholesterol], [ExerciseIntensityId], [FoodTypeIdWant], [AvoidIngredient], [IsActive]) VALUES (4, 4, N'Balanced Diet Target', 1800, 40, 70, 40, 70, 40, 70, 40, 1, 1, 4, 4, N'14;', NULL)
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

INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account]) VALUES (1, N'~/images/faces/face4.jpg', N'John', N'Doe', NULL, NULL, NULL, N'', N'1', 1, 1, N'a')
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account]) VALUES (2, N'/images/user2.jpg', N'Jane', N'Smith', NULL, NULL, NULL, NULL, N'2', 2, 1, N'b')
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account]) VALUES (3, N'/images/user3.jpg', N'Emily', N'Brown', NULL, NULL, NULL, NULL, N'123456789', 3, 1, N'example@example.com')
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account]) VALUES (4, N'/images/user4.jpg', N'Michael', N'Johnson', NULL, NULL, NULL, NULL, N'123456789', 4, 0, N'example@example.com')
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account]) VALUES (10, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'123', 4, 1, N'bb')
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account]) VALUES (13, N'fghgfh', NULL, NULL, NULL, NULL, NULL, NULL, N'acb', 1, NULL, N'acb')
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account]) VALUES (14, N'hjhgkgk', N'string', N'string', CAST(N'2024-10-08T11:32:24.317' AS DateTime), 1, N'string', N'string', N'string', 4, 1, N'string')
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account]) VALUES (15, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'llll', 4, 0, N'lll')
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account]) VALUES (16, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'mmm', 4, 1, N'mmm')
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account]) VALUES (17, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'yyy', 4, 1, N'yyy')
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account]) VALUES (18, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'qqq', 4, 1, N'qqq')
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account]) VALUES (19, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'eee', 4, 1, N'eee')
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account]) VALUES (22, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'12345', 4, 1, N'mmm')
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account]) VALUES (23, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'12345', 4, 1, N'mmm')
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account]) VALUES (24, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'123', 4, 1, N'asfd')
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account]) VALUES (25, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'dddd', 4, 1, N'dddd')
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account]) VALUES (26, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'sssss', 4, 1, N'ssssss')
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account]) VALUES (27, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'asdf', 4, 1, N'asdf')
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account]) VALUES (30, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'admin', 1, 1, N'admin')
INSERT [UserData].[User] ([UserId], [URLImage], [FirstName], [LastName], [DOB], [Gender], [Address], [Phone], [Password], [Role], [IsActive], [Account]) VALUES (31, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'tantantan', 4, 1, N'tantantan')
SET IDENTITY_INSERT [UserData].[User] OFF
GO
SET IDENTITY_INSERT [UserData].[UserDetails] ON 

INSERT [UserData].[UserDetails] ([Id], [UserId], [DescribeYourself], [Height], [Weight], [Age], [WantImprove], [UnderlyingDisease], [InforConfirmGood], [InforConfirmBad], [IsPremium]) VALUES (1, 1, N'Interested in weight loss', 180, 75, NULL, NULL, N'1;2;3;4;', NULL, NULL, NULL)
INSERT [UserData].[UserDetails] ([Id], [UserId], [DescribeYourself], [Height], [Weight], [Age], [WantImprove], [UnderlyingDisease], [InforConfirmGood], [InforConfirmBad], [IsPremium]) VALUES (2, 2, N'Focused on building muscle', 165, 60, NULL, NULL, N'1;2;3;4;5;', NULL, NULL, NULL)
INSERT [UserData].[UserDetails] ([Id], [UserId], [DescribeYourself], [Height], [Weight], [Age], [WantImprove], [UnderlyingDisease], [InforConfirmGood], [InforConfirmBad], [IsPremium]) VALUES (3, 3, N'Looking for balanced diet', 170, 65, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [UserData].[UserDetails] ([Id], [UserId], [DescribeYourself], [Height], [Weight], [Age], [WantImprove], [UnderlyingDisease], [InforConfirmGood], [InforConfirmBad], [IsPremium]) VALUES (4, 4, N'Aiming to improve overall health', 175, 80, NULL, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [UserData].[UserDetails] OFF
GO
SET IDENTITY_INSERT [UserData].[WantCooking] ON 

INSERT [UserData].[WantCooking] ([Id], [Name]) VALUES (1, N'Phải nấu')
INSERT [UserData].[WantCooking] ([Id], [Name]) VALUES (2, N'Không nấu ăn')
SET IDENTITY_INSERT [UserData].[WantCooking] OFF
GO
/****** Object:  Index [UQ__Conversa__135EC2124A57C55D]    Script Date: 10/24/2024 2:04:10 PM ******/
ALTER TABLE [Business].[ConversationParticipants] ADD UNIQUE NONCLUSTERED 
(
	[ConversationsId] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [UQ__MealSett__1788CC4D843F35AF]    Script Date: 10/24/2024 2:04:10 PM ******/
ALTER TABLE [UserData].[MealSettings] ADD UNIQUE NONCLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [UQ__Nutritio__F4399C8D333D8E36]    Script Date: 10/24/2024 2:04:10 PM ******/
ALTER TABLE [UserData].[NutritionistDetails] ADD UNIQUE NONCLUSTERED 
(
	[NutritionistId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [UQ__UserDeta__1788CC4DE9C17C83]    Script Date: 10/24/2024 2:04:10 PM ******/
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
ALTER TABLE [Business].[ConversationParticipants]  WITH CHECK ADD FOREIGN KEY([ConversationsId])
REFERENCES [Business].[Conversations] ([Id])
GO
ALTER TABLE [Business].[ConversationParticipants]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [UserData].[User] ([UserId])
GO
ALTER TABLE [Business].[MealOfTheDay]  WITH CHECK ADD FOREIGN KEY([NutritionRouteId])
REFERENCES [Business].[NutritionRoute] ([Id])
GO
ALTER TABLE [Business].[MealOfTheDay]  WITH CHECK ADD FOREIGN KEY([Slot1OfTheDayId])
REFERENCES [Business].[SlotOfTheDay] ([Id])
GO
ALTER TABLE [Business].[MealOfTheDay]  WITH CHECK ADD FOREIGN KEY([Slot2OfTheDayId])
REFERENCES [Business].[SlotOfTheDay] ([Id])
GO
ALTER TABLE [Business].[MealOfTheDay]  WITH CHECK ADD FOREIGN KEY([Slot3OfTheDayId])
REFERENCES [Business].[SlotOfTheDay] ([Id])
GO
ALTER TABLE [Business].[MealOfTheDay]  WITH CHECK ADD FOREIGN KEY([Slot4OfTheDayId])
REFERENCES [Business].[SlotOfTheDay] ([Id])
GO
ALTER TABLE [Business].[MealOfTheDay]  WITH CHECK ADD FOREIGN KEY([Slot5OfTheDayId])
REFERENCES [Business].[SlotOfTheDay] ([Id])
GO
ALTER TABLE [Business].[Messages]  WITH CHECK ADD FOREIGN KEY([ConversationsId])
REFERENCES [Business].[Conversations] ([Id])
GO
ALTER TABLE [Business].[Messages]  WITH CHECK ADD FOREIGN KEY([SenderId])
REFERENCES [UserData].[User] ([UserId])
GO
ALTER TABLE [Business].[MSG]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [UserData].[User] ([UserId])
GO
ALTER TABLE [Business].[NutritionRoute]  WITH CHECK ADD FOREIGN KEY([CreateById])
REFERENCES [UserData].[User] ([UserId])
GO
ALTER TABLE [Business].[NutritionRoute]  WITH CHECK ADD FOREIGN KEY([UserId])
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
ALTER TABLE [UserData].[BankInformation]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [UserData].[User] ([UserId])
GO
ALTER TABLE [UserData].[ExpertPackages]  WITH CHECK ADD FOREIGN KEY([NutritionistDetailsId])
REFERENCES [UserData].[NutritionistDetails] ([Id])
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
ALTER TABLE [UserData].[NutritionistDetails]  WITH CHECK ADD FOREIGN KEY([NutritionistId])
REFERENCES [UserData].[User] ([UserId])
GO
ALTER TABLE [UserData].[NutritionTargetsDaily]  WITH CHECK ADD FOREIGN KEY([ExerciseIntensityId])
REFERENCES [UserData].[ExerciseIntensity] ([Id])
GO
ALTER TABLE [UserData].[NutritionTargetsDaily]  WITH CHECK ADD FOREIGN KEY([FoodTypeIdWant])
REFERENCES [FoodData].[FoodType] ([FoodTypeId])
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
