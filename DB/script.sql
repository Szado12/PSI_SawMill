USE [master]
GO
/****** Object:  Database [PSI_SawMill]    Script Date: 24.01.2023 20:54:36 ******/
CREATE DATABASE [PSI_SawMill]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PSI_SawMill', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\PSI_SawMill.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PSI_SawMill_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\PSI_SawMill_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [PSI_SawMill] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PSI_SawMill].[dbo].[sp_fulltext_database] @action = 'enable'
end
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
/****** Object:  Table [dbo].[Addresses]    Script Date: 24.01.2023 20:54:36 ******/
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
/****** Object:  Table [dbo].[Clients]    Script Date: 24.01.2023 20:54:36 ******/
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
/****** Object:  Table [dbo].[Deliveries]    Script Date: 24.01.2023 20:54:36 ******/
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
/****** Object:  Table [dbo].[DeliveryStates]    Script Date: 24.01.2023 20:54:36 ******/
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
/****** Object:  Table [dbo].[Employees]    Script Date: 24.01.2023 20:54:36 ******/
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
/****** Object:  Table [dbo].[EmployeeTypes]    Script Date: 24.01.2023 20:54:36 ******/
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
/****** Object:  Table [dbo].[LoginData]    Script Date: 24.01.2023 20:54:36 ******/
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
/****** Object:  Table [dbo].[Machine]    Script Date: 24.01.2023 20:54:36 ******/
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
/****** Object:  Table [dbo].[Operations]    Script Date: 24.01.2023 20:54:36 ******/
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
/****** Object:  Table [dbo].[OperationsToMachines]    Script Date: 24.01.2023 20:54:36 ******/
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
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 24.01.2023 20:54:36 ******/
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
/****** Object:  Table [dbo].[Orders]    Script Date: 24.01.2023 20:54:36 ******/
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
/****** Object:  Table [dbo].[OrderStates]    Script Date: 24.01.2023 20:54:36 ******/
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
/****** Object:  Table [dbo].[ProductionDetails]    Script Date: 24.01.2023 20:54:36 ******/
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
/****** Object:  Table [dbo].[Products]    Script Date: 24.01.2023 20:54:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[Price] [float] NOT NULL,
	[ProductTypeId] [int] NOT NULL,
	[WoodTypeId] [int] NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductTypes]    Script Date: 24.01.2023 20:54:36 ******/
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
/****** Object:  Table [dbo].[Warehouses]    Script Date: 24.01.2023 20:54:36 ******/
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
/****** Object:  Table [dbo].[WarehousesToProducts]    Script Date: 24.01.2023 20:54:36 ******/
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
/****** Object:  Table [dbo].[WoodTypes]    Script Date: 24.01.2023 20:54:37 ******/
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
SET IDENTITY_INSERT [dbo].[DeliveryStates] ON 

INSERT [dbo].[DeliveryStates] ([DeliveryStateId], [Name]) VALUES (1, N'Utworzone')
INSERT [dbo].[DeliveryStates] ([DeliveryStateId], [Name]) VALUES (2, N'Anulowane')
INSERT [dbo].[DeliveryStates] ([DeliveryStateId], [Name]) VALUES (3, N'Zaakceptowane')
INSERT [dbo].[DeliveryStates] ([DeliveryStateId], [Name]) VALUES (4, N'Gotowe do wysyłki')
INSERT [dbo].[DeliveryStates] ([DeliveryStateId], [Name]) VALUES (5, N'Wysłane')
INSERT [dbo].[DeliveryStates] ([DeliveryStateId], [Name]) VALUES (6, N'Dostarczone')
SET IDENTITY_INSERT [dbo].[DeliveryStates] OFF
GO
SET IDENTITY_INSERT [dbo].[EmployeeTypes] ON 

INSERT [dbo].[EmployeeTypes] ([EmployeeTypeId], [Name]) VALUES (1, N'Administrator')
INSERT [dbo].[EmployeeTypes] ([EmployeeTypeId], [Name]) VALUES (2, N'Sprzedawca')
INSERT [dbo].[EmployeeTypes] ([EmployeeTypeId], [Name]) VALUES (3, N'Kurier')
INSERT [dbo].[EmployeeTypes] ([EmployeeTypeId], [Name]) VALUES (4, N'Operator maszyn')
SET IDENTITY_INSERT [dbo].[EmployeeTypes] OFF
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
/****** Object:  Index [UQ__Orders__626D8FCFB687F09B]    Script Date: 24.01.2023 20:54:37 ******/
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
