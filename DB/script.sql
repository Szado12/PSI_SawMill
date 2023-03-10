USE [master]
GO
/****** Object:  Database [PSI_SawMill]    Script Date: 28.01.2023 21:34:08 ******/
CREATE DATABASE [PSI_SawMill]
GO
ALTER DATABASE [PSI_SawMill] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PSI_SawMill] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PSI_SawMill] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PSI_SawMill] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PSI_SawMill] SET ARITHABORT OFF 
GO
ALTER DATABASE [PSI_SawMill] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PSI_SawMill] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PSI_SawMill] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PSI_SawMill] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PSI_SawMill] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PSI_SawMill] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PSI_SawMill] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PSI_SawMill] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PSI_SawMill] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PSI_SawMill] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PSI_SawMill] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PSI_SawMill] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PSI_SawMill] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PSI_SawMill] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PSI_SawMill] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PSI_SawMill] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PSI_SawMill] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PSI_SawMill] SET RECOVERY FULL 
GO
ALTER DATABASE [PSI_SawMill] SET  MULTI_USER 
GO
ALTER DATABASE [PSI_SawMill] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PSI_SawMill] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PSI_SawMill] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PSI_SawMill] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PSI_SawMill] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PSI_SawMill] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'PSI_SawMill', N'ON'
GO
ALTER DATABASE [PSI_SawMill] SET QUERY_STORE = OFF
GO
USE [PSI_SawMill]
GO
/****** Object:  Table [dbo].[Addresses]    Script Date: 28.01.2023 21:34:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Addresses](
	[AddressId] [int] IDENTITY(1,1) NOT NULL,
	[City] [nvarchar](50) NOT NULL,
	[Street] [nvarchar](100) NOT NULL,
	[PostalCode] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_Addresses] PRIMARY KEY CLUSTERED 
(
	[AddressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Clients]    Script Date: 28.01.2023 21:34:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clients](
	[ClientId] [int] IDENTITY(1,1) NOT NULL,
	[CompanyName] [varchar](150) NOT NULL,
	[NIP] [varchar](50) NOT NULL,
	[AddressId] [int] NOT NULL,
	[FirstName] [binary](48) NOT NULL,
	[LastName] [binary](48) NOT NULL,
	[IsArchived] [bit] NOT NULL,
 CONSTRAINT [PK_Clients] PRIMARY KEY CLUSTERED 
(
	[ClientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Deliveries]    Script Date: 28.01.2023 21:34:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Deliveries](
	[DeliveryId] [int] IDENTITY(1,1) NOT NULL,
	[SendDate] [date] NULL,
	[DeliveryStateId] [int] NOT NULL,
	[DelivererId] [int] NULL,
 CONSTRAINT [PK_Deliveries] PRIMARY KEY CLUSTERED 
(
	[DeliveryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DeliveryStates]    Script Date: 28.01.2023 21:34:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DeliveryStates](
	[DeliveryStateId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_DeliveryStates] PRIMARY KEY CLUSTERED 
(
	[DeliveryStateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 28.01.2023 21:34:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[EmployeeId] [int] IDENTITY(1,1) NOT NULL,
	[IsBlocked] [bit] NOT NULL,
	[EmployeeTypeId] [int] NOT NULL,
	[LastName] [binary](48) NOT NULL,
	[FirstName] [binary](48) NOT NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeeTypes]    Script Date: 28.01.2023 21:34:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeTypes](
	[EmployeeTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_EmployeeTypes] PRIMARY KEY CLUSTERED 
(
	[EmployeeTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoginData]    Script Date: 28.01.2023 21:34:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoginData](
	[LoginId] [int] IDENTITY(1,1) NOT NULL,
	[RefreshToken] [nvarchar](100) NULL,
	[RefreshTokenExpireDate] [datetime] NULL,
	[EmployeeId] [int] NOT NULL,
	[Login] [binary](128) NOT NULL,
	[Password] [binary](128) NOT NULL,
 CONSTRAINT [PK_LoginData] PRIMARY KEY CLUSTERED 
(
	[LoginId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Machine]    Script Date: 28.01.2023 21:34:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Machine](
	[MachineId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[IsBroken] [bit] NOT NULL,
	[IsArchived] [bit] NOT NULL,
 CONSTRAINT [PK_Machine] PRIMARY KEY CLUSTERED 
(
	[MachineId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Operations]    Script Date: 28.01.2023 21:34:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Operations](
	[OperationId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[SourceProductTypeId] [int] NOT NULL,
	[OutputProductTypeId] [int] NOT NULL,
	[SourceOutputRatio] [float] NOT NULL,
	[Duration] [float] NOT NULL,
	[IsArchived] [bit] NOT NULL,
 CONSTRAINT [PK_Operations] PRIMARY KEY CLUSTERED 
(
	[OperationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OperationsToMachines]    Script Date: 28.01.2023 21:34:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OperationsToMachines](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MachineId] [int] NOT NULL,
	[OperationId] [int] NOT NULL,
 CONSTRAINT [PK_OperationsToMachines] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 28.01.2023 21:34:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetails](
	[OrderDetailId] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NOT NULL,
	[Amount] [nchar](10) NOT NULL,
	[OrderId] [int] NOT NULL,
 CONSTRAINT [PK_OrderDetails] PRIMARY KEY CLUSTERED 
(
	[OrderDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 28.01.2023 21:34:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[ClientId] [int] NULL,
	[OrderStateId] [int] NULL,
	[CreationDate] [date] NULL,
	[AcceptanceDate] [date] NULL,
	[DeliveryId] [int] NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderStates]    Script Date: 28.01.2023 21:34:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderStates](
	[OrderStateId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_OrderStates] PRIMARY KEY CLUSTERED 
(
	[OrderStateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductionDetails]    Script Date: 28.01.2023 21:34:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductionDetails](
	[ProductionDetailId] [int] IDENTITY(1,1) NOT NULL,
	[MaterialAmount] [float] NOT NULL,
	[StartDate] [date] NOT NULL,
	[IsArchived] [bit] NOT NULL,
	[MachineId] [int] NOT NULL,
	[OperationId] [int] NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_ProductionDetails] PRIMARY KEY CLUSTERED 
(
	[ProductionDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 28.01.2023 21:34:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[Price] [float] NOT NULL,
	[ProductTypeId] [int] NOT NULL,
	[WoodTypeId] [int] NOT NULL,
	[IsArchived] [bit] NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductTypes]    Script Date: 28.01.2023 21:34:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductTypes](
	[ProductTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ProductTypes] PRIMARY KEY CLUSTERED 
(
	[ProductTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Warehouses]    Script Date: 28.01.2023 21:34:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Warehouses](
	[WarehouseId] [int] IDENTITY(1,1) NOT NULL,
	[Capacity] [float] NOT NULL,
	[AddressId] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Warehouses] PRIMARY KEY CLUSTERED 
(
	[WarehouseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WarehousesToProducts]    Script Date: 28.01.2023 21:34:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WarehousesToProducts](
	[WarehouseId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Amount] [float] NOT NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_WarehousesToProducts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WoodTypes]    Script Date: 28.01.2023 21:34:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WoodTypes](
	[WoodTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_WoodTypes] PRIMARY KEY CLUSTERED 
(
	[WoodTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Addresses] ON 

INSERT [dbo].[Addresses] ([AddressId], [City], [Street], [PostalCode]) VALUES (1, N'Wrocław', N'Wojszycka 15', N'52-123')
INSERT [dbo].[Addresses] ([AddressId], [City], [Street], [PostalCode]) VALUES (2, N'Warszawa', N'Krakowska 13', N'42-423')
INSERT [dbo].[Addresses] ([AddressId], [City], [Street], [PostalCode]) VALUES (3, N'Poznań', N'Bajana Jerzego 148', N'60-408')
INSERT [dbo].[Addresses] ([AddressId], [City], [Street], [PostalCode]) VALUES (4, N'Ruda Śląska', N'Księdza Ściegiennego Piotra 59', N'41-710')
INSERT [dbo].[Addresses] ([AddressId], [City], [Street], [PostalCode]) VALUES (5, N'Bydgoszcz', N'Celichowskich 17', N'61-680')
INSERT [dbo].[Addresses] ([AddressId], [City], [Street], [PostalCode]) VALUES (6, N'Bydgoszcz', N'Celichowskich 17', N'61-680')
INSERT [dbo].[Addresses] ([AddressId], [City], [Street], [PostalCode]) VALUES (7, N'Bydgoszcz', N'Zamłynie 13', N'61-650')
INSERT [dbo].[Addresses] ([AddressId], [City], [Street], [PostalCode]) VALUES (8, N'Bydgoszcz', N'Zamłynie 13', N'61-650')
SET IDENTITY_INSERT [dbo].[Addresses] OFF
GO
SET IDENTITY_INSERT [dbo].[Clients] ON 

INSERT [dbo].[Clients] ([ClientId], [CompanyName], [NIP], [AddressId], [FirstName], [LastName], [IsArchived]) VALUES (1, N'Drewnostan', N'56433252612', 1, 0xF28ED6FF58532BAA7552DCBEBBCF0A5D0000000000000000000000000000000000000000000000000000000000000000, 0x0794F1CB844DA8D74F1C7EC23724FCD30000000000000000000000000000000000000000000000000000000000000000, 0)
INSERT [dbo].[Clients] ([ClientId], [CompanyName], [NIP], [AddressId], [FirstName], [LastName], [IsArchived]) VALUES (2, N'Artur Jankowski sp', N'93475028612', 2, 0xE85ED02D21DE5038DA75EBAA67AA5E220000000000000000000000000000000000000000000000000000000000000000, 0xB13E0AE40B6C1BD6CC21E899E2318B430000000000000000000000000000000000000000000000000000000000000000, 0)
INSERT [dbo].[Clients] ([ClientId], [CompanyName], [NIP], [AddressId], [FirstName], [LastName], [IsArchived]) VALUES (3, N'Stolarnia Poznań', N'78547012013', 3, 0x1335A67E2DC0C124CC8128D579A66FE70000000000000000000000000000000000000000000000000000000000000000, 0x69291D3F973F41308C4467C03592CFB00000000000000000000000000000000000000000000000000000000000000000, 0)
INSERT [dbo].[Clients] ([ClientId], [CompanyName], [NIP], [AddressId], [FirstName], [LastName], [IsArchived]) VALUES (4, N'Ruda Śląska Stolarz', N'67368963723', 4, 0x8C5C287ABA29FEE41C8448AAFDC7ACAB0000000000000000000000000000000000000000000000000000000000000000, 0x7C2E49DCA3FBA2A5BBCD54D4084705C10000000000000000000000000000000000000000000000000000000000000000, 0)
SET IDENTITY_INSERT [dbo].[Clients] OFF
GO
SET IDENTITY_INSERT [dbo].[Deliveries] ON 

INSERT [dbo].[Deliveries] ([DeliveryId], [SendDate], [DeliveryStateId], [DelivererId]) VALUES (1, NULL, 1, NULL)
SET IDENTITY_INSERT [dbo].[Deliveries] OFF
GO
SET IDENTITY_INSERT [dbo].[DeliveryStates] ON 

INSERT [dbo].[DeliveryStates] ([DeliveryStateId], [Name]) VALUES (1, N'Utworzone')
INSERT [dbo].[DeliveryStates] ([DeliveryStateId], [Name]) VALUES (2, N'Anulowane')
INSERT [dbo].[DeliveryStates] ([DeliveryStateId], [Name]) VALUES (3, N'Zaakceptowane')
INSERT [dbo].[DeliveryStates] ([DeliveryStateId], [Name]) VALUES (4, N'Gotowe do wysyłki')
INSERT [dbo].[DeliveryStates] ([DeliveryStateId], [Name]) VALUES (5, N'Wysłane')
INSERT [dbo].[DeliveryStates] ([DeliveryStateId], [Name]) VALUES (6, N'Dostarczone')
SET IDENTITY_INSERT [dbo].[DeliveryStates] OFF
GO
SET IDENTITY_INSERT [dbo].[Employees] ON 

INSERT [dbo].[Employees] ([EmployeeId], [IsBlocked], [EmployeeTypeId], [LastName], [FirstName]) VALUES (1, 0, 1, 0x6991FD8C7E90571A456D21F089DEA6E40000000000000000000000000000000000000000000000000000000000000000, 0x0BF7347535E8DF3750B2F51B04CC040F0000000000000000000000000000000000000000000000000000000000000000)
INSERT [dbo].[Employees] ([EmployeeId], [IsBlocked], [EmployeeTypeId], [LastName], [FirstName]) VALUES (2, 0, 2, 0x6991FD8C7E90571A456D21F089DEA6E40000000000000000000000000000000000000000000000000000000000000000, 0x58B7694FAAF489BE9256C694EF1F19210000000000000000000000000000000000000000000000000000000000000000)
INSERT [dbo].[Employees] ([EmployeeId], [IsBlocked], [EmployeeTypeId], [LastName], [FirstName]) VALUES (3, 0, 3, 0xCB18270D1E6FDDF5C9AF8ECF8B65B9020000000000000000000000000000000000000000000000000000000000000000, 0xEE1078BB7AB0BC452605815B23D9ADC90000000000000000000000000000000000000000000000000000000000000000)
INSERT [dbo].[Employees] ([EmployeeId], [IsBlocked], [EmployeeTypeId], [LastName], [FirstName]) VALUES (4, 0, 4, 0xDB9F4A6B04BC4469FC3BCC8B7481E5850000000000000000000000000000000000000000000000000000000000000000, 0x51FEFD2B2585D863C2436EE2624F81D80000000000000000000000000000000000000000000000000000000000000000)
INSERT [dbo].[Employees] ([EmployeeId], [IsBlocked], [EmployeeTypeId], [LastName], [FirstName]) VALUES (5, 0, 4, 0xF912444F2F091CC3075A6E0E39289CBD0000000000000000000000000000000000000000000000000000000000000000, 0x58B7694FAAF489BE9256C694EF1F19210000000000000000000000000000000000000000000000000000000000000000)
INSERT [dbo].[Employees] ([EmployeeId], [IsBlocked], [EmployeeTypeId], [LastName], [FirstName]) VALUES (6, 0, 4, 0xA118B370F32771AFCDEE59C03C49F9D30000000000000000000000000000000000000000000000000000000000000000, 0x9C4D243FBEC48594EEC17EF3AA4EBE3C0000000000000000000000000000000000000000000000000000000000000000)
SET IDENTITY_INSERT [dbo].[Employees] OFF
GO
SET IDENTITY_INSERT [dbo].[EmployeeTypes] ON 

INSERT [dbo].[EmployeeTypes] ([EmployeeTypeId], [Name]) VALUES (1, N'Administrator')
INSERT [dbo].[EmployeeTypes] ([EmployeeTypeId], [Name]) VALUES (2, N'Sprzedawca')
INSERT [dbo].[EmployeeTypes] ([EmployeeTypeId], [Name]) VALUES (3, N'Kurier')
INSERT [dbo].[EmployeeTypes] ([EmployeeTypeId], [Name]) VALUES (4, N'Operator maszyn')
SET IDENTITY_INSERT [dbo].[EmployeeTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[LoginData] ON 

INSERT [dbo].[LoginData] ([LoginId], [RefreshToken], [RefreshTokenExpireDate], [EmployeeId], [Login], [Password]) VALUES (1, N'sZa3wfh5gmWghhHzTgSHURUepG3ilrdweX9p3qslSk8bc/3o58oRueW4VH+ddZUNc2H9ZoyO+jMYCieS+OFluw==', CAST(N'2023-01-29T21:18:56.190' AS DateTime), 1, 0xEE26B0DD4AF7E749AA1A8EE3C10AE9923F618980772E473F8819A5D4940E0DB27AC185F8A0E1D5F84F88BC887FD67B143732C304CC5FA9AD8E6F57F50028A8FF00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000, 0xEE26B0DD4AF7E749AA1A8EE3C10AE9923F618980772E473F8819A5D4940E0DB27AC185F8A0E1D5F84F88BC887FD67B143732C304CC5FA9AD8E6F57F50028A8FF00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000)
INSERT [dbo].[LoginData] ([LoginId], [RefreshToken], [RefreshTokenExpireDate], [EmployeeId], [Login], [Password]) VALUES (2, NULL, NULL, 2, 0x6D201BEEEFB589B08EF0672DAC82353D0CBD9AD99E1642C83A1601F3D647BCCA003257B5E8F31BDC1D73FBEC84FB085C79D6E2677B7FF927E823A54E789140D900000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000, 0x6D201BEEEFB589B08EF0672DAC82353D0CBD9AD99E1642C83A1601F3D647BCCA003257B5E8F31BDC1D73FBEC84FB085C79D6E2677B7FF927E823A54E789140D900000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000)
INSERT [dbo].[LoginData] ([LoginId], [RefreshToken], [RefreshTokenExpireDate], [EmployeeId], [Login], [Password]) VALUES (3, NULL, NULL, 3, 0xCB872DE2B8D2509C54344435CE9CB43B4FAA27F97D486FF4DE35AF03E4919FB4EC53267CAF8DEF06EF177D69FE0ABAB3C12FBDC2F267D895FD07C36A62BFF4BF00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000, 0xCB872DE2B8D2509C54344435CE9CB43B4FAA27F97D486FF4DE35AF03E4919FB4EC53267CAF8DEF06EF177D69FE0ABAB3C12FBDC2F267D895FD07C36A62BFF4BF00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000)
INSERT [dbo].[LoginData] ([LoginId], [RefreshToken], [RefreshTokenExpireDate], [EmployeeId], [Login], [Password]) VALUES (4, NULL, NULL, 4, 0x2257AAB44B42813142AA8AC4767116AD5BD41E94A79AA0672CC962128ED4809F50ED38D35BA945A80799976C9EFA9B686F28D18036134BC2BB0AC2DE96EC628000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000, 0x2257AAB44B42813142AA8AC4767116AD5BD41E94A79AA0672CC962128ED4809F50ED38D35BA945A80799976C9EFA9B686F28D18036134BC2BB0AC2DE96EC628000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000)
INSERT [dbo].[LoginData] ([LoginId], [RefreshToken], [RefreshTokenExpireDate], [EmployeeId], [Login], [Password]) VALUES (5, NULL, NULL, 5, 0x64C26FFE3B35C65DFB93A8FD9A91828C57ED76D3809D06B03E17128125B48E5D01B37BB605A0A0305EFF8341FBD56096664597F5CD091BF036E4CA31B304A9CC00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000, 0x64C26FFE3B35C65DFB93A8FD9A91828C57ED76D3809D06B03E17128125B48E5D01B37BB605A0A0305EFF8341FBD56096664597F5CD091BF036E4CA31B304A9CC00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000)
INSERT [dbo].[LoginData] ([LoginId], [RefreshToken], [RefreshTokenExpireDate], [EmployeeId], [Login], [Password]) VALUES (6, NULL, NULL, 6, 0x30634FC2DD28E412A684771875FD805747FB3BD9760A085EFB554A0A4233223A4D98F6DA2336179CA33D1170E6E27B18BE4588978C35115A43AEF6DDC226C80800000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000, 0x30634FC2DD28E412A684771875FD805747FB3BD9760A085EFB554A0A4233223A4D98F6DA2336179CA33D1170E6E27B18BE4588978C35115A43AEF6DDC226C80800000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000)
SET IDENTITY_INSERT [dbo].[LoginData] OFF
GO
SET IDENTITY_INSERT [dbo].[Machine] ON 

INSERT [dbo].[Machine] ([MachineId], [Name], [IsBroken], [IsArchived]) VALUES (1, N'Piła', 0, 0)
INSERT [dbo].[Machine] ([MachineId], [Name], [IsBroken], [IsArchived]) VALUES (2, N'Piła2', 0, 0)
SET IDENTITY_INSERT [dbo].[Machine] OFF
GO
SET IDENTITY_INSERT [dbo].[Operations] ON 

INSERT [dbo].[Operations] ([OperationId], [Name], [Description], [SourceProductTypeId], [OutputProductTypeId], [SourceOutputRatio], [Duration], [IsArchived]) VALUES (1, N'Korowanie', N'Korowanie', 1, 2, 1, 3, 0)
INSERT [dbo].[Operations] ([OperationId], [Name], [Description], [SourceProductTypeId], [OutputProductTypeId], [SourceOutputRatio], [Duration], [IsArchived]) VALUES (2, N'Cięcie', N'Cięcie', 2, 3, 0.8, 4, 0)
INSERT [dbo].[Operations] ([OperationId], [Name], [Description], [SourceProductTypeId], [OutputProductTypeId], [SourceOutputRatio], [Duration], [IsArchived]) VALUES (3, N'Szlifowanie', N'Szlifowanie', 3, 4, 1, 6, 0)
SET IDENTITY_INSERT [dbo].[Operations] OFF
GO
SET IDENTITY_INSERT [dbo].[OperationsToMachines] ON 

INSERT [dbo].[OperationsToMachines] ([Id], [MachineId], [OperationId]) VALUES (1, 1, 1)
INSERT [dbo].[OperationsToMachines] ([Id], [MachineId], [OperationId]) VALUES (2, 1, 2)
INSERT [dbo].[OperationsToMachines] ([Id], [MachineId], [OperationId]) VALUES (3, 2, 1)
INSERT [dbo].[OperationsToMachines] ([Id], [MachineId], [OperationId]) VALUES (4, 2, 2)
SET IDENTITY_INSERT [dbo].[OperationsToMachines] OFF
GO
SET IDENTITY_INSERT [dbo].[OrderDetails] ON 

INSERT [dbo].[OrderDetails] ([OrderDetailId], [ProductId], [Amount], [OrderId]) VALUES (1, 1, N'20        ', 1)
INSERT [dbo].[OrderDetails] ([OrderDetailId], [ProductId], [Amount], [OrderId]) VALUES (2, 2, N'20        ', 1)
INSERT [dbo].[OrderDetails] ([OrderDetailId], [ProductId], [Amount], [OrderId]) VALUES (3, 3, N'20        ', 1)
SET IDENTITY_INSERT [dbo].[OrderDetails] OFF
GO
SET IDENTITY_INSERT [dbo].[Orders] ON 

INSERT [dbo].[Orders] ([OrderId], [ClientId], [OrderStateId], [CreationDate], [AcceptanceDate], [DeliveryId]) VALUES (1, 1, 1, CAST(N'2023-01-28' AS Date), NULL, 1)
SET IDENTITY_INSERT [dbo].[Orders] OFF
GO
SET IDENTITY_INSERT [dbo].[OrderStates] ON 

INSERT [dbo].[OrderStates] ([OrderStateId], [Name]) VALUES (1, N'Utworzone')
INSERT [dbo].[OrderStates] ([OrderStateId], [Name]) VALUES (2, N'Anulowane')
INSERT [dbo].[OrderStates] ([OrderStateId], [Name]) VALUES (3, N'Zaakceptowane')
INSERT [dbo].[OrderStates] ([OrderStateId], [Name]) VALUES (4, N'Gotowe do wysyłki')
INSERT [dbo].[OrderStates] ([OrderStateId], [Name]) VALUES (5, N'Wysłane')
INSERT [dbo].[OrderStates] ([OrderStateId], [Name]) VALUES (6, N'Dostarczone')
SET IDENTITY_INSERT [dbo].[OrderStates] OFF
GO
SET IDENTITY_INSERT [dbo].[ProductionDetails] ON 

INSERT [dbo].[ProductionDetails] ([ProductionDetailId], [MaterialAmount], [StartDate], [IsArchived], [MachineId], [OperationId], [EmployeeId], [ProductId], [Status]) VALUES (1, 20, CAST(N'2023-01-28' AS Date), 0, 1, 1, 4, 1, 1)
SET IDENTITY_INSERT [dbo].[ProductionDetails] OFF
GO
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([ProductId], [Price], [ProductTypeId], [WoodTypeId], [IsArchived]) VALUES (1, 20, 1, 1, 0)
INSERT [dbo].[Products] ([ProductId], [Price], [ProductTypeId], [WoodTypeId], [IsArchived]) VALUES (2, 20, 1, 2, 0)
INSERT [dbo].[Products] ([ProductId], [Price], [ProductTypeId], [WoodTypeId], [IsArchived]) VALUES (3, 20, 1, 3, 0)
INSERT [dbo].[Products] ([ProductId], [Price], [ProductTypeId], [WoodTypeId], [IsArchived]) VALUES (4, 20, 1, 4, 0)
INSERT [dbo].[Products] ([ProductId], [Price], [ProductTypeId], [WoodTypeId], [IsArchived]) VALUES (5, 50, 1, 5, 0)
INSERT [dbo].[Products] ([ProductId], [Price], [ProductTypeId], [WoodTypeId], [IsArchived]) VALUES (6, 70, 2, 1, 0)
INSERT [dbo].[Products] ([ProductId], [Price], [ProductTypeId], [WoodTypeId], [IsArchived]) VALUES (7, 70, 2, 2, 0)
INSERT [dbo].[Products] ([ProductId], [Price], [ProductTypeId], [WoodTypeId], [IsArchived]) VALUES (8, 70, 2, 3, 0)
INSERT [dbo].[Products] ([ProductId], [Price], [ProductTypeId], [WoodTypeId], [IsArchived]) VALUES (9, 70, 2, 4, 0)
INSERT [dbo].[Products] ([ProductId], [Price], [ProductTypeId], [WoodTypeId], [IsArchived]) VALUES (10, 120, 3, 1, 0)
INSERT [dbo].[Products] ([ProductId], [Price], [ProductTypeId], [WoodTypeId], [IsArchived]) VALUES (11, 120, 3, 2, 0)
INSERT [dbo].[Products] ([ProductId], [Price], [ProductTypeId], [WoodTypeId], [IsArchived]) VALUES (12, 120, 3, 3, 0)
INSERT [dbo].[Products] ([ProductId], [Price], [ProductTypeId], [WoodTypeId], [IsArchived]) VALUES (13, 120, 3, 4, 0)
INSERT [dbo].[Products] ([ProductId], [Price], [ProductTypeId], [WoodTypeId], [IsArchived]) VALUES (14, 160, 4, 1, 0)
INSERT [dbo].[Products] ([ProductId], [Price], [ProductTypeId], [WoodTypeId], [IsArchived]) VALUES (15, 160, 4, 2, 0)
INSERT [dbo].[Products] ([ProductId], [Price], [ProductTypeId], [WoodTypeId], [IsArchived]) VALUES (16, 160, 4, 3, 0)
INSERT [dbo].[Products] ([ProductId], [Price], [ProductTypeId], [WoodTypeId], [IsArchived]) VALUES (17, 160, 4, 4, 0)
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
SET IDENTITY_INSERT [dbo].[ProductTypes] ON 

INSERT [dbo].[ProductTypes] ([ProductTypeId], [Name]) VALUES (1, N'Surowe')
INSERT [dbo].[ProductTypes] ([ProductTypeId], [Name]) VALUES (2, N'Okorowane')
INSERT [dbo].[ProductTypes] ([ProductTypeId], [Name]) VALUES (3, N'Deska')
INSERT [dbo].[ProductTypes] ([ProductTypeId], [Name]) VALUES (4, N'Wyszlifowana deska')
SET IDENTITY_INSERT [dbo].[ProductTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[Warehouses] ON 

INSERT [dbo].[Warehouses] ([WarehouseId], [Capacity], [AddressId], [Name]) VALUES (1, 30000, 6, N'Magazyn B')
INSERT [dbo].[Warehouses] ([WarehouseId], [Capacity], [AddressId], [Name]) VALUES (2, 20000, 8, N'Magazyn B')
SET IDENTITY_INSERT [dbo].[Warehouses] OFF
GO
SET IDENTITY_INSERT [dbo].[WarehousesToProducts] ON 

INSERT [dbo].[WarehousesToProducts] ([WarehouseId], [ProductId], [Amount], [Id]) VALUES (1, 1, 200, 1)
INSERT [dbo].[WarehousesToProducts] ([WarehouseId], [ProductId], [Amount], [Id]) VALUES (1, 2, 200, 2)
INSERT [dbo].[WarehousesToProducts] ([WarehouseId], [ProductId], [Amount], [Id]) VALUES (1, 3, 200, 3)
INSERT [dbo].[WarehousesToProducts] ([WarehouseId], [ProductId], [Amount], [Id]) VALUES (1, 4, 200, 4)
INSERT [dbo].[WarehousesToProducts] ([WarehouseId], [ProductId], [Amount], [Id]) VALUES (2, 1, 200, 5)
INSERT [dbo].[WarehousesToProducts] ([WarehouseId], [ProductId], [Amount], [Id]) VALUES (2, 2, 200, 6)
INSERT [dbo].[WarehousesToProducts] ([WarehouseId], [ProductId], [Amount], [Id]) VALUES (2, 3, 200, 7)
INSERT [dbo].[WarehousesToProducts] ([WarehouseId], [ProductId], [Amount], [Id]) VALUES (2, 4, 200, 8)
SET IDENTITY_INSERT [dbo].[WarehousesToProducts] OFF
GO
SET IDENTITY_INSERT [dbo].[WoodTypes] ON 

INSERT [dbo].[WoodTypes] ([WoodTypeId], [Name]) VALUES (1, N'Sosna')
INSERT [dbo].[WoodTypes] ([WoodTypeId], [Name]) VALUES (2, N'Buk')
INSERT [dbo].[WoodTypes] ([WoodTypeId], [Name]) VALUES (3, N'Brzoza')
INSERT [dbo].[WoodTypes] ([WoodTypeId], [Name]) VALUES (4, N'Wiąz')
INSERT [dbo].[WoodTypes] ([WoodTypeId], [Name]) VALUES (5, N'Dąb')
INSERT [dbo].[WoodTypes] ([WoodTypeId], [Name]) VALUES (6, N'Klon')
SET IDENTITY_INSERT [dbo].[WoodTypes] OFF
GO
/****** Object:  Index [UQ__Orders__626D8FCFF38926F7]    Script Date: 28.01.2023 21:34:08 ******/
ALTER TABLE [dbo].[Orders] ADD UNIQUE NONCLUSTERED 
(
	[DeliveryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Clients]  WITH CHECK ADD  CONSTRAINT [FK_Clients_Addresses] FOREIGN KEY([AddressId])
REFERENCES [dbo].[Addresses] ([AddressId])
GO
ALTER TABLE [dbo].[Clients] CHECK CONSTRAINT [FK_Clients_Addresses]
GO
ALTER TABLE [dbo].[Deliveries]  WITH CHECK ADD  CONSTRAINT [FK_Deliveries_DeliveryStates1] FOREIGN KEY([DeliveryStateId])
REFERENCES [dbo].[DeliveryStates] ([DeliveryStateId])
GO
ALTER TABLE [dbo].[Deliveries] CHECK CONSTRAINT [FK_Deliveries_DeliveryStates1]
GO
ALTER TABLE [dbo].[Deliveries]  WITH CHECK ADD  CONSTRAINT [FK_Deliveries_Employees1] FOREIGN KEY([DelivererId])
REFERENCES [dbo].[Employees] ([EmployeeId])
GO
ALTER TABLE [dbo].[Deliveries] CHECK CONSTRAINT [FK_Deliveries_Employees1]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK__Employees__Emplo__7F2BE32F] FOREIGN KEY([EmployeeTypeId])
REFERENCES [dbo].[EmployeeTypes] ([EmployeeTypeId])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK__Employees__Emplo__7F2BE32F]
GO
ALTER TABLE [dbo].[LoginData]  WITH CHECK ADD  CONSTRAINT [FK_LoginData_Employees] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employees] ([EmployeeId])
GO
ALTER TABLE [dbo].[LoginData] CHECK CONSTRAINT [FK_LoginData_Employees]
GO
ALTER TABLE [dbo].[Operations]  WITH CHECK ADD  CONSTRAINT [FK_Operations_ProductTypes] FOREIGN KEY([OutputProductTypeId])
REFERENCES [dbo].[ProductTypes] ([ProductTypeId])
GO
ALTER TABLE [dbo].[Operations] CHECK CONSTRAINT [FK_Operations_ProductTypes]
GO
ALTER TABLE [dbo].[Operations]  WITH CHECK ADD  CONSTRAINT [FK_Operations_ProductTypes1] FOREIGN KEY([SourceProductTypeId])
REFERENCES [dbo].[ProductTypes] ([ProductTypeId])
GO
ALTER TABLE [dbo].[Operations] CHECK CONSTRAINT [FK_Operations_ProductTypes1]
GO
ALTER TABLE [dbo].[OperationsToMachines]  WITH CHECK ADD  CONSTRAINT [FK_OperationsToMachines_Machine] FOREIGN KEY([MachineId])
REFERENCES [dbo].[Machine] ([MachineId])
GO
ALTER TABLE [dbo].[OperationsToMachines] CHECK CONSTRAINT [FK_OperationsToMachines_Machine]
GO
ALTER TABLE [dbo].[OperationsToMachines]  WITH CHECK ADD  CONSTRAINT [FK_OperationsToMachines_Operations] FOREIGN KEY([OperationId])
REFERENCES [dbo].[Operations] ([OperationId])
GO
ALTER TABLE [dbo].[OperationsToMachines] CHECK CONSTRAINT [FK_OperationsToMachines_Operations]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([OrderId])
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD FOREIGN KEY([ClientId])
REFERENCES [dbo].[Clients] ([ClientId])
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD FOREIGN KEY([DeliveryId])
REFERENCES [dbo].[Deliveries] ([DeliveryId])
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD FOREIGN KEY([OrderStateId])
REFERENCES [dbo].[OrderStates] ([OrderStateId])
GO
ALTER TABLE [dbo].[ProductionDetails]  WITH CHECK ADD  CONSTRAINT [FK_ProductionDetails_Employees] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employees] ([EmployeeId])
GO
ALTER TABLE [dbo].[ProductionDetails] CHECK CONSTRAINT [FK_ProductionDetails_Employees]
GO
ALTER TABLE [dbo].[ProductionDetails]  WITH CHECK ADD  CONSTRAINT [FK_ProductionDetails_Machine] FOREIGN KEY([MachineId])
REFERENCES [dbo].[Machine] ([MachineId])
GO
ALTER TABLE [dbo].[ProductionDetails] CHECK CONSTRAINT [FK_ProductionDetails_Machine]
GO
ALTER TABLE [dbo].[ProductionDetails]  WITH CHECK ADD  CONSTRAINT [FK_ProductionDetails_Operations] FOREIGN KEY([OperationId])
REFERENCES [dbo].[Operations] ([OperationId])
GO
ALTER TABLE [dbo].[ProductionDetails] CHECK CONSTRAINT [FK_ProductionDetails_Operations]
GO
ALTER TABLE [dbo].[ProductionDetails]  WITH CHECK ADD  CONSTRAINT [FK_ProductionDetails_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
GO
ALTER TABLE [dbo].[ProductionDetails] CHECK CONSTRAINT [FK_ProductionDetails_Products]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_ProductTypes] FOREIGN KEY([ProductTypeId])
REFERENCES [dbo].[ProductTypes] ([ProductTypeId])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_ProductTypes]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_WoodTypes] FOREIGN KEY([WoodTypeId])
REFERENCES [dbo].[WoodTypes] ([WoodTypeId])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_WoodTypes]
GO
ALTER TABLE [dbo].[Warehouses]  WITH CHECK ADD  CONSTRAINT [FK_Warehouses_Addresses] FOREIGN KEY([AddressId])
REFERENCES [dbo].[Addresses] ([AddressId])
GO
ALTER TABLE [dbo].[Warehouses] CHECK CONSTRAINT [FK_Warehouses_Addresses]
GO
ALTER TABLE [dbo].[WarehousesToProducts]  WITH CHECK ADD  CONSTRAINT [FK_WarehousesToProducts_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
GO
ALTER TABLE [dbo].[WarehousesToProducts] CHECK CONSTRAINT [FK_WarehousesToProducts_Products]
GO
ALTER TABLE [dbo].[WarehousesToProducts]  WITH CHECK ADD  CONSTRAINT [FK_WarehousesToProducts_Warehouses] FOREIGN KEY([WarehouseId])
REFERENCES [dbo].[Warehouses] ([WarehouseId])
GO
ALTER TABLE [dbo].[WarehousesToProducts] CHECK CONSTRAINT [FK_WarehousesToProducts_Warehouses]
GO
USE [master]
GO
ALTER DATABASE [PSI_SawMill] SET  READ_WRITE 
GO
