USE [master]
GO
/****** Object:  Database [RestaurantMenu]    Script Date: 5/19/2020 1:41:41 PM ******/
CREATE DATABASE [RestaurantMenu]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'RestaurantMenu', FILENAME = N'D:\Database\RestaurantMenu.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'RestaurantMenu_log', FILENAME = N'D:\Database\RestaurantMenu_log.ldf' , SIZE = 2560KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [RestaurantMenu] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [RestaurantMenu].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [RestaurantMenu] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [RestaurantMenu] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [RestaurantMenu] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [RestaurantMenu] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [RestaurantMenu] SET ARITHABORT OFF 
GO
ALTER DATABASE [RestaurantMenu] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [RestaurantMenu] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [RestaurantMenu] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [RestaurantMenu] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [RestaurantMenu] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [RestaurantMenu] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [RestaurantMenu] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [RestaurantMenu] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [RestaurantMenu] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [RestaurantMenu] SET  DISABLE_BROKER 
GO
ALTER DATABASE [RestaurantMenu] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [RestaurantMenu] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [RestaurantMenu] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [RestaurantMenu] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [RestaurantMenu] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [RestaurantMenu] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [RestaurantMenu] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [RestaurantMenu] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [RestaurantMenu] SET  MULTI_USER 
GO
ALTER DATABASE [RestaurantMenu] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [RestaurantMenu] SET DB_CHAINING OFF 
GO
ALTER DATABASE [RestaurantMenu] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [RestaurantMenu] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [RestaurantMenu] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [RestaurantMenu] SET QUERY_STORE = OFF
GO
USE [RestaurantMenu]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 5/19/2020 1:41:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[OrderPlacedDTime] [datetime] NOT NULL,
	[RestaurantId] [int] NOT NULL,
	[TableId] [int] NOT NULL,
	[Paid] [bit] NOT NULL,
	[OrderBy] [int] NULL,
	[ProductId] [bigint] NOT NULL,
	[Qty] [int] NOT NULL,
	[StatusNameId] [int] NOT NULL,
	[Note] [nvarchar](200) NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderStatus]    Script Date: 5/19/2020 1:41:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderStatus](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[OrderId] [bigint] NOT NULL,
	[StatusId] [int] NOT NULL,
	[DateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_OrderStatus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderStatusNames]    Script Date: 5/19/2020 1:41:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderStatusNames](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Details] [nvarchar](200) NULL,
 CONSTRAINT [PK_OrderStatusNames] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductCategories]    Script Date: 5/19/2020 1:41:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductCategories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RestaurantId] [int] NOT NULL,
	[CategoryName] [nvarchar](50) NOT NULL,
	[Discription] [nvarchar](500) NULL,
	[DisplayOrder] [int] NULL,
 CONSTRAINT [PK_ProductCategories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 5/19/2020 1:41:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Discription] [nvarchar](500) NULL,
	[ImagePath] [nvarchar](500) NULL,
	[Price] [money] NOT NULL,
	[RestaurantId] [int] NOT NULL,
	[Enable] [bit] NOT NULL,
	[CategoryId] [int] NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Restaurants]    Script Date: 5/19/2020 1:41:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Restaurants](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[RegisterDate] [datetime] NULL,
	[Country] [nvarchar](50) NULL,
	[City] [nvarchar](50) NULL,
	[Address] [nvarchar](50) NULL,
	[Phone] [nvarchar](20) NULL,
	[NotificationEmail] [nvarchar](50) NULL,
	[ValidityDate] [date] NULL,
	[AllowedTables] [int] NULL,
	[SendEmailNotification] [bit] NULL,
	[CurrencyCode] [nvarchar](3) NULL,
	[LogoPath] [nvarchar](500) NULL,
	[LogoSmallPath] [nvarchar](500) NULL,
 CONSTRAINT [PK_Restaurant] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RestaurantTables]    Script Date: 5/19/2020 1:41:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RestaurantTables](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[RestaurantId] [int] NOT NULL,
	[Discription] [nvarchar](200) NULL,
	[Enable] [bit] NOT NULL,
	[QRImageLocation] [nvarchar](500) NOT NULL,
	[QRCodeStr] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_RestaurantTables] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RestaurantUsers]    Script Date: 5/19/2020 1:41:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RestaurantUsers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[RestaurantId] [int] NOT NULL,
 CONSTRAINT [PK_RestaurantUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RestaurantWaiters]    Script Date: 5/19/2020 1:41:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RestaurantWaiters](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[RestaurantId] [int] NOT NULL,
 CONSTRAINT [PK_RestaurantWaiters] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserLogs]    Script Date: 5/19/2020 1:41:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserLogs](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[LogDateTime] [datetime] NOT NULL,
	[UserID] [int] NOT NULL,
	[UserTypeID] [int] NOT NULL,
	[LogDetail] [nvarchar](500) NULL,
 CONSTRAINT [PK_UserLogs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 5/19/2020 1:41:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NULL,
	[Name] [nvarchar](50) NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Password] [nvarchar](500) NOT NULL,
	[Enable] [bit] NOT NULL,
	[LastLogin] [datetime] NULL,
	[LostPassKey] [varchar](10) NULL,
	[Type] [int] NOT NULL,
	[Details] [varchar](500) NULL,
	[EmailVerify] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserTypes]    Script Date: 5/19/2020 1:41:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserType] [varchar](50) NOT NULL,
	[Details] [varchar](200) NULL,
 CONSTRAINT [PK_UserTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Orders] ON 

INSERT [dbo].[Orders] ([Id], [OrderPlacedDTime], [RestaurantId], [TableId], [Paid], [OrderBy], [ProductId], [Qty], [StatusNameId], [Note]) VALUES (29, CAST(N'2020-05-18T10:17:36.037' AS DateTime), 11, 1021, 0, 1026, 18, 1, 1, N'some notes')
INSERT [dbo].[Orders] ([Id], [OrderPlacedDTime], [RestaurantId], [TableId], [Paid], [OrderBy], [ProductId], [Qty], [StatusNameId], [Note]) VALUES (30, CAST(N'2020-05-18T10:18:03.380' AS DateTime), 11, 1021, 0, 1026, 16, 1, 1, N'some note')
INSERT [dbo].[Orders] ([Id], [OrderPlacedDTime], [RestaurantId], [TableId], [Paid], [OrderBy], [ProductId], [Qty], [StatusNameId], [Note]) VALUES (31, CAST(N'2020-05-18T10:20:26.653' AS DateTime), 11, 1021, 0, 1027, 20, 1, 1, N'hot note')
INSERT [dbo].[Orders] ([Id], [OrderPlacedDTime], [RestaurantId], [TableId], [Paid], [OrderBy], [ProductId], [Qty], [StatusNameId], [Note]) VALUES (32, CAST(N'2020-05-18T10:23:24.673' AS DateTime), 11, 1022, 0, 1027, 18, 2, 3, N'some notes')
INSERT [dbo].[Orders] ([Id], [OrderPlacedDTime], [RestaurantId], [TableId], [Paid], [OrderBy], [ProductId], [Qty], [StatusNameId], [Note]) VALUES (33, CAST(N'2020-05-18T10:23:54.073' AS DateTime), 11, 1022, 0, 1027, 17, 2, 1, N'')
SET IDENTITY_INSERT [dbo].[Orders] OFF
SET IDENTITY_INSERT [dbo].[OrderStatus] ON 

INSERT [dbo].[OrderStatus] ([Id], [OrderId], [StatusId], [DateTime]) VALUES (146, 29, 1, CAST(N'2020-05-18T10:17:36.090' AS DateTime))
INSERT [dbo].[OrderStatus] ([Id], [OrderId], [StatusId], [DateTime]) VALUES (147, 30, 1, CAST(N'2020-05-18T10:18:03.380' AS DateTime))
INSERT [dbo].[OrderStatus] ([Id], [OrderId], [StatusId], [DateTime]) VALUES (148, 31, 1, CAST(N'2020-05-18T10:20:26.653' AS DateTime))
INSERT [dbo].[OrderStatus] ([Id], [OrderId], [StatusId], [DateTime]) VALUES (149, 32, 1, CAST(N'2020-05-18T10:23:24.673' AS DateTime))
INSERT [dbo].[OrderStatus] ([Id], [OrderId], [StatusId], [DateTime]) VALUES (150, 33, 1, CAST(N'2020-05-18T10:23:54.073' AS DateTime))
INSERT [dbo].[OrderStatus] ([Id], [OrderId], [StatusId], [DateTime]) VALUES (151, 32, 3, CAST(N'2020-05-18T10:48:03.533' AS DateTime))
SET IDENTITY_INSERT [dbo].[OrderStatus] OFF
SET IDENTITY_INSERT [dbo].[OrderStatusNames] ON 

INSERT [dbo].[OrderStatusNames] ([Id], [Name], [Details]) VALUES (1, N'Placed', N'Order Placed by customer')
INSERT [dbo].[OrderStatusNames] ([Id], [Name], [Details]) VALUES (2, N'Order Cancelled by Resaurant', N'Order Cancelled by Restaurant')
INSERT [dbo].[OrderStatusNames] ([Id], [Name], [Details]) VALUES (3, N'Order Processed', N'Order Processed by Restaurant')
INSERT [dbo].[OrderStatusNames] ([Id], [Name], [Details]) VALUES (4, N'Paid', N'Paid by Customer')
INSERT [dbo].[OrderStatusNames] ([Id], [Name], [Details]) VALUES (5, N'Order Cancelled by Customer', N'Order Cancelled by Customer')
INSERT [dbo].[OrderStatusNames] ([Id], [Name], [Details]) VALUES (6, N'Order Cancelled by Waiter', N'Order Cancelled by Waiter')
SET IDENTITY_INSERT [dbo].[OrderStatusNames] OFF
SET IDENTITY_INSERT [dbo].[ProductCategories] ON 

INSERT [dbo].[ProductCategories] ([Id], [RestaurantId], [CategoryName], [Discription], [DisplayOrder]) VALUES (17, 11, N'drink', N'', 3)
INSERT [dbo].[ProductCategories] ([Id], [RestaurantId], [CategoryName], [Discription], [DisplayOrder]) VALUES (18, 11, N'food', N'', 2)
INSERT [dbo].[ProductCategories] ([Id], [RestaurantId], [CategoryName], [Discription], [DisplayOrder]) VALUES (19, 11, N'starter', N'restaurant starter ', 1)
INSERT [dbo].[ProductCategories] ([Id], [RestaurantId], [CategoryName], [Discription], [DisplayOrder]) VALUES (1019, 11, N'Fast Food', N'', 0)
SET IDENTITY_INSERT [dbo].[ProductCategories] OFF
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([Id], [Name], [Discription], [ImagePath], [Price], [RestaurantId], [Enable], [CategoryId]) VALUES (16, N'single beer', N'', N'83b0fc04cbb3425c8f944ded2f25f04b', 200.0000, 11, 1, 17)
INSERT [dbo].[Products] ([Id], [Name], [Discription], [ImagePath], [Price], [RestaurantId], [Enable], [CategoryId]) VALUES (17, N'food 1', N'description', N'978b88e805164abbbeaa774f953a4014', 500.0000, 11, 1, 18)
INSERT [dbo].[Products] ([Id], [Name], [Discription], [ImagePath], [Price], [RestaurantId], [Enable], [CategoryId]) VALUES (18, N'starterA', N'food starter', N'935d26a4120d4964bf0633cbf6b71e26', 20.0000, 11, 1, 19)
INSERT [dbo].[Products] ([Id], [Name], [Discription], [ImagePath], [Price], [RestaurantId], [Enable], [CategoryId]) VALUES (19, N'double beer', N'two glass of 300ml', N'7f8b0de7d2fa4ccd99d0b12ca043a5ca', 200.0000, 11, 1, 17)
INSERT [dbo].[Products] ([Id], [Name], [Discription], [ImagePath], [Price], [RestaurantId], [Enable], [CategoryId]) VALUES (20, N'chiken', N'meat chiken', N'57f2ca92a3ad4346968da67199a67073', 500.0000, 11, 1, 18)
INSERT [dbo].[Products] ([Id], [Name], [Discription], [ImagePath], [Price], [RestaurantId], [Enable], [CategoryId]) VALUES (21, N'some starter', N'some starter', NULL, 10.0000, 11, 1, 19)
INSERT [dbo].[Products] ([Id], [Name], [Discription], [ImagePath], [Price], [RestaurantId], [Enable], [CategoryId]) VALUES (22, N'hidden food', N'this is hidden food', NULL, 1.0000, 11, 0, 18)
SET IDENTITY_INSERT [dbo].[Products] OFF
SET IDENTITY_INSERT [dbo].[Restaurants] ON 

INSERT [dbo].[Restaurants] ([Id], [Name], [RegisterDate], [Country], [City], [Address], [Phone], [NotificationEmail], [ValidityDate], [AllowedTables], [SendEmailNotification], [CurrencyCode], [LogoPath], [LogoSmallPath]) VALUES (11, N'restaurant 1', CAST(N'2020-05-16T18:37:48.290' AS DateTime), N'pk', N'mlx', N'address', N'0300', N'', CAST(N'2020-05-30' AS Date), NULL, 0, N'EUR', N'/Template/assets/img/logo-2.png', N'/Template/assets/img/smallLogo_tran.png')
SET IDENTITY_INSERT [dbo].[Restaurants] OFF
SET IDENTITY_INSERT [dbo].[RestaurantTables] ON 

INSERT [dbo].[RestaurantTables] ([Id], [Name], [RestaurantId], [Discription], [Enable], [QRImageLocation], [QRCodeStr]) VALUES (1021, N'table 1', 11, N'', 1, N'Reso/res/11/q/MqZFLaZd4F.bmp', N'MqZFLaZd4F')
INSERT [dbo].[RestaurantTables] ([Id], [Name], [RestaurantId], [Discription], [Enable], [QRImageLocation], [QRCodeStr]) VALUES (1022, N'table 2', 11, N'this is table 2', 1, N'Reso/res/11/q/SqA65cn44H.bmp', N'SqA65cn44H')
SET IDENTITY_INSERT [dbo].[RestaurantTables] OFF
SET IDENTITY_INSERT [dbo].[RestaurantUsers] ON 

INSERT [dbo].[RestaurantUsers] ([Id], [UserId], [RestaurantId]) VALUES (11, 1026, 11)
SET IDENTITY_INSERT [dbo].[RestaurantUsers] OFF
SET IDENTITY_INSERT [dbo].[RestaurantWaiters] ON 

INSERT [dbo].[RestaurantWaiters] ([Id], [UserId], [RestaurantId]) VALUES (11, 1027, 11)
SET IDENTITY_INSERT [dbo].[RestaurantWaiters] OFF
SET IDENTITY_INSERT [dbo].[UserLogs] ON 

INSERT [dbo].[UserLogs] ([Id], [LogDateTime], [UserID], [UserTypeID], [LogDetail]) VALUES (20497, CAST(N'2020-05-16T18:37:48.290' AS DateTime), 1026, 2, N'New Restaurant User Signup')
INSERT [dbo].[UserLogs] ([Id], [LogDateTime], [UserID], [UserTypeID], [LogDetail]) VALUES (20498, CAST(N'2020-05-16T18:38:24.060' AS DateTime), 1026, 2, N'Signin')
INSERT [dbo].[UserLogs] ([Id], [LogDateTime], [UserID], [UserTypeID], [LogDetail]) VALUES (20499, CAST(N'2020-05-16T18:39:16.263' AS DateTime), 1026, 2, N'Restaurant Profile Updated By Restaurant User')
INSERT [dbo].[UserLogs] ([Id], [LogDateTime], [UserID], [UserTypeID], [LogDetail]) VALUES (20500, CAST(N'2020-05-16T18:39:45.860' AS DateTime), 1, 1, N'Signin')
INSERT [dbo].[UserLogs] ([Id], [LogDateTime], [UserID], [UserTypeID], [LogDetail]) VALUES (20501, CAST(N'2020-05-16T18:40:33.763' AS DateTime), 1026, 2, N'Signin')
INSERT [dbo].[UserLogs] ([Id], [LogDateTime], [UserID], [UserTypeID], [LogDetail]) VALUES (20502, CAST(N'2020-05-16T18:41:17.677' AS DateTime), 1026, 2, N'New Restaurant Table table 1 Added')
INSERT [dbo].[UserLogs] ([Id], [LogDateTime], [UserID], [UserTypeID], [LogDetail]) VALUES (20503, CAST(N'2020-05-16T18:43:05.120' AS DateTime), 1026, 2, N'New Food Category drink Added')
INSERT [dbo].[UserLogs] ([Id], [LogDateTime], [UserID], [UserTypeID], [LogDetail]) VALUES (20504, CAST(N'2020-05-16T18:43:13.710' AS DateTime), 1026, 2, N'New Food Category food Added')
INSERT [dbo].[UserLogs] ([Id], [LogDateTime], [UserID], [UserTypeID], [LogDetail]) VALUES (20505, CAST(N'2020-05-16T18:43:44.530' AS DateTime), 1026, 2, N'New Food Product single beer Added')
INSERT [dbo].[UserLogs] ([Id], [LogDateTime], [UserID], [UserTypeID], [LogDetail]) VALUES (20506, CAST(N'2020-05-16T18:44:33.633' AS DateTime), 1026, 2, N'New Food Product food 1 Added')
INSERT [dbo].[UserLogs] ([Id], [LogDateTime], [UserID], [UserTypeID], [LogDetail]) VALUES (20507, CAST(N'2020-05-18T08:18:21.733' AS DateTime), 1026, 2, N'Signin')
INSERT [dbo].[UserLogs] ([Id], [LogDateTime], [UserID], [UserTypeID], [LogDetail]) VALUES (20508, CAST(N'2020-05-18T08:19:05.757' AS DateTime), 1026, 2, N'New Food Category starter Added')
INSERT [dbo].[UserLogs] ([Id], [LogDateTime], [UserID], [UserTypeID], [LogDetail]) VALUES (20509, CAST(N'2020-05-18T08:20:09.743' AS DateTime), 1026, 2, N'New Food Product starterA Added')
INSERT [dbo].[UserLogs] ([Id], [LogDateTime], [UserID], [UserTypeID], [LogDetail]) VALUES (20510, CAST(N'2020-05-18T08:20:34.277' AS DateTime), 1026, 2, N'New Food Product double beer Added')
INSERT [dbo].[UserLogs] ([Id], [LogDateTime], [UserID], [UserTypeID], [LogDetail]) VALUES (20511, CAST(N'2020-05-18T08:21:05.777' AS DateTime), 1026, 2, N'New Food Product chiken Added')
INSERT [dbo].[UserLogs] ([Id], [LogDateTime], [UserID], [UserTypeID], [LogDetail]) VALUES (20512, CAST(N'2020-05-18T08:21:24.017' AS DateTime), 1026, 2, N'New Food Product some starter Added')
INSERT [dbo].[UserLogs] ([Id], [LogDateTime], [UserID], [UserTypeID], [LogDetail]) VALUES (20513, CAST(N'2020-05-18T08:21:45.530' AS DateTime), 1026, 2, N'New Food Product hidden food Added')
INSERT [dbo].[UserLogs] ([Id], [LogDateTime], [UserID], [UserTypeID], [LogDetail]) VALUES (20514, CAST(N'2020-05-18T08:22:44.453' AS DateTime), 1026, 2, N'Food Product hidden food Updated')
INSERT [dbo].[UserLogs] ([Id], [LogDateTime], [UserID], [UserTypeID], [LogDetail]) VALUES (20515, CAST(N'2020-05-18T08:27:34.720' AS DateTime), 1026, 2, N'Signin')
INSERT [dbo].[UserLogs] ([Id], [LogDateTime], [UserID], [UserTypeID], [LogDetail]) VALUES (20516, CAST(N'2020-05-18T08:27:58.770' AS DateTime), 1026, 2, N'New Waiter Account Saved')
INSERT [dbo].[UserLogs] ([Id], [LogDateTime], [UserID], [UserTypeID], [LogDetail]) VALUES (30507, CAST(N'2020-05-18T10:16:52.317' AS DateTime), 1026, 2, N'New Restaurant Table table 2 Added')
INSERT [dbo].[UserLogs] ([Id], [LogDateTime], [UserID], [UserTypeID], [LogDetail]) VALUES (30508, CAST(N'2020-05-18T10:17:36.100' AS DateTime), 1026, 2, N'New Order Placed. Table:table 1. Food:starterA. Qty:1')
INSERT [dbo].[UserLogs] ([Id], [LogDateTime], [UserID], [UserTypeID], [LogDetail]) VALUES (30509, CAST(N'2020-05-18T10:18:03.380' AS DateTime), 1026, 2, N'New Order Placed. Table:table 1. Food:single beer. Qty:1')
INSERT [dbo].[UserLogs] ([Id], [LogDateTime], [UserID], [UserTypeID], [LogDetail]) VALUES (30510, CAST(N'2020-05-18T10:18:57.170' AS DateTime), 1027, 3, N'Signin')
INSERT [dbo].[UserLogs] ([Id], [LogDateTime], [UserID], [UserTypeID], [LogDetail]) VALUES (30511, CAST(N'2020-05-18T10:20:26.653' AS DateTime), 1026, 2, N'New Order Placed. Table:table 1. Food:chiken. Qty:1')
INSERT [dbo].[UserLogs] ([Id], [LogDateTime], [UserID], [UserTypeID], [LogDetail]) VALUES (30512, CAST(N'2020-05-18T10:23:24.673' AS DateTime), 1026, 2, N'New Order Placed. Table:table 2. Food:starterA. Qty:2')
INSERT [dbo].[UserLogs] ([Id], [LogDateTime], [UserID], [UserTypeID], [LogDetail]) VALUES (30513, CAST(N'2020-05-18T10:23:54.073' AS DateTime), 1026, 2, N'New Order Placed. Table:table 2. Food:food 1. Qty:2')
INSERT [dbo].[UserLogs] ([Id], [LogDateTime], [UserID], [UserTypeID], [LogDetail]) VALUES (30514, CAST(N'2020-05-18T10:47:22.190' AS DateTime), 1026, 2, N'Signin')
INSERT [dbo].[UserLogs] ([Id], [LogDateTime], [UserID], [UserTypeID], [LogDetail]) VALUES (30515, CAST(N'2020-05-18T10:48:03.550' AS DateTime), 1026, 2, N'Order Status Updated')
INSERT [dbo].[UserLogs] ([Id], [LogDateTime], [UserID], [UserTypeID], [LogDetail]) VALUES (30516, CAST(N'2020-05-18T15:06:46.673' AS DateTime), 1026, 2, N'Signin')
INSERT [dbo].[UserLogs] ([Id], [LogDateTime], [UserID], [UserTypeID], [LogDetail]) VALUES (30517, CAST(N'2020-05-18T15:08:03.063' AS DateTime), 1026, 2, N'New Food Category Fast Food Added')
INSERT [dbo].[UserLogs] ([Id], [LogDateTime], [UserID], [UserTypeID], [LogDetail]) VALUES (30518, CAST(N'2020-05-18T15:08:33.003' AS DateTime), 1026, 2, N'Food Category starter Updated')
INSERT [dbo].[UserLogs] ([Id], [LogDateTime], [UserID], [UserTypeID], [LogDetail]) VALUES (30519, CAST(N'2020-05-18T15:08:48.167' AS DateTime), 1026, 2, N'Food Category drink Updated')
INSERT [dbo].[UserLogs] ([Id], [LogDateTime], [UserID], [UserTypeID], [LogDetail]) VALUES (30520, CAST(N'2020-05-18T15:09:14.040' AS DateTime), 1026, 2, N'Food Category food Updated')
INSERT [dbo].[UserLogs] ([Id], [LogDateTime], [UserID], [UserTypeID], [LogDetail]) VALUES (30521, CAST(N'2020-05-18T15:09:28.237' AS DateTime), 1026, 2, N'Food Category starter Updated')
SET IDENTITY_INSERT [dbo].[UserLogs] OFF
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [UserName], [Name], [Email], [Password], [Enable], [LastLogin], [LostPassKey], [Type], [Details], [EmailVerify]) VALUES (1, NULL, N'Rana', N'ranamsikandar@gmail.com', N'hDCKYuciSjZFKSAkn49yMA==', 1, CAST(N'2020-05-16T18:39:45.860' AS DateTime), N'K7ZKRwDYza', 1, N'Rana admin of webapp', 1)
INSERT [dbo].[Users] ([Id], [UserName], [Name], [Email], [Password], [Enable], [LastLogin], [LostPassKey], [Type], [Details], [EmailVerify]) VALUES (1020, NULL, N'Alberto', N'info@mylebe.com', N'hDCKYuciSjZFKSAkn49yMA==', 1, CAST(N'2020-02-24T11:14:35.457' AS DateTime), NULL, 1, N'Alberto admin of web application', 1)
INSERT [dbo].[Users] ([Id], [UserName], [Name], [Email], [Password], [Enable], [LastLogin], [LostPassKey], [Type], [Details], [EmailVerify]) VALUES (1026, NULL, N'r1owner name1', N'importerexporter@asia.com', N'rz/48FLVs0BonS2F2BscnIqhR3GTdJzX4jgfbGMhdCs=', 1, CAST(N'2020-05-18T15:06:46.673' AS DateTime), N'K9Boq9kUTp', 2, N'', 0)
INSERT [dbo].[Users] ([Id], [UserName], [Name], [Email], [Password], [Enable], [LastLogin], [LostPassKey], [Type], [Details], [EmailVerify]) VALUES (1027, NULL, N'waiter 1', N'buyer007@asia.com', N'0Q0bWxMhQOxIYNTAJi5pLg==', 1, CAST(N'2020-05-18T10:18:57.170' AS DateTime), N'L4OCU2dHWO', 3, N'', 0)
SET IDENTITY_INSERT [dbo].[Users] OFF
SET IDENTITY_INSERT [dbo].[UserTypes] ON 

INSERT [dbo].[UserTypes] ([Id], [UserType], [Details]) VALUES (1, N'Admin', N'Admin of web app')
INSERT [dbo].[UserTypes] ([Id], [UserType], [Details]) VALUES (2, N'Restaurant', N'Restaurant Owners')
INSERT [dbo].[UserTypes] ([Id], [UserType], [Details]) VALUES (3, N'Waiter', N'Waiters of Restaurants')
SET IDENTITY_INSERT [dbo].[UserTypes] OFF
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_OrderStatusNames] FOREIGN KEY([StatusNameId])
REFERENCES [dbo].[OrderStatusNames] ([Id])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_OrderStatusNames]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Products]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Restaurants] FOREIGN KEY([RestaurantId])
REFERENCES [dbo].[Restaurants] ([Id])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Restaurants]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_RestaurantTables] FOREIGN KEY([TableId])
REFERENCES [dbo].[RestaurantTables] ([Id])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_RestaurantTables]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Users] FOREIGN KEY([OrderBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Users]
GO
ALTER TABLE [dbo].[OrderStatus]  WITH CHECK ADD  CONSTRAINT [FK_OrderStatus_Orders] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([Id])
GO
ALTER TABLE [dbo].[OrderStatus] CHECK CONSTRAINT [FK_OrderStatus_Orders]
GO
ALTER TABLE [dbo].[OrderStatus]  WITH CHECK ADD  CONSTRAINT [FK_OrderStatus_OrderStatusNames] FOREIGN KEY([StatusId])
REFERENCES [dbo].[OrderStatusNames] ([Id])
GO
ALTER TABLE [dbo].[OrderStatus] CHECK CONSTRAINT [FK_OrderStatus_OrderStatusNames]
GO
ALTER TABLE [dbo].[ProductCategories]  WITH CHECK ADD  CONSTRAINT [FK_ProductCategories_Restaurants] FOREIGN KEY([RestaurantId])
REFERENCES [dbo].[Restaurants] ([Id])
GO
ALTER TABLE [dbo].[ProductCategories] CHECK CONSTRAINT [FK_ProductCategories_Restaurants]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_ProductCategories] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[ProductCategories] ([Id])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_ProductCategories]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Restaurants] FOREIGN KEY([RestaurantId])
REFERENCES [dbo].[Restaurants] ([Id])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Restaurants]
GO
ALTER TABLE [dbo].[RestaurantTables]  WITH CHECK ADD  CONSTRAINT [FK_RestaurantTables_Restaurants] FOREIGN KEY([RestaurantId])
REFERENCES [dbo].[Restaurants] ([Id])
GO
ALTER TABLE [dbo].[RestaurantTables] CHECK CONSTRAINT [FK_RestaurantTables_Restaurants]
GO
ALTER TABLE [dbo].[RestaurantUsers]  WITH CHECK ADD  CONSTRAINT [FK_RestaurantUsers_Restaurant] FOREIGN KEY([RestaurantId])
REFERENCES [dbo].[Restaurants] ([Id])
GO
ALTER TABLE [dbo].[RestaurantUsers] CHECK CONSTRAINT [FK_RestaurantUsers_Restaurant]
GO
ALTER TABLE [dbo].[RestaurantUsers]  WITH CHECK ADD  CONSTRAINT [FK_RestaurantUsers_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[RestaurantUsers] CHECK CONSTRAINT [FK_RestaurantUsers_Users]
GO
ALTER TABLE [dbo].[RestaurantWaiters]  WITH CHECK ADD  CONSTRAINT [FK_RestaurantWaiters_Restaurants] FOREIGN KEY([RestaurantId])
REFERENCES [dbo].[Restaurants] ([Id])
GO
ALTER TABLE [dbo].[RestaurantWaiters] CHECK CONSTRAINT [FK_RestaurantWaiters_Restaurants]
GO
ALTER TABLE [dbo].[RestaurantWaiters]  WITH CHECK ADD  CONSTRAINT [FK_RestaurantWaiters_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[RestaurantWaiters] CHECK CONSTRAINT [FK_RestaurantWaiters_Users]
GO
ALTER TABLE [dbo].[UserLogs]  WITH CHECK ADD  CONSTRAINT [FK_UserLogs_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[UserLogs] CHECK CONSTRAINT [FK_UserLogs_Users]
GO
ALTER TABLE [dbo].[UserLogs]  WITH CHECK ADD  CONSTRAINT [FK_UserLogs_UserTypes] FOREIGN KEY([UserTypeID])
REFERENCES [dbo].[UserTypes] ([Id])
GO
ALTER TABLE [dbo].[UserLogs] CHECK CONSTRAINT [FK_UserLogs_UserTypes]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_UserTypes] FOREIGN KEY([Type])
REFERENCES [dbo].[UserTypes] ([Id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_UserTypes]
GO
/****** Object:  StoredProcedure [dbo].[Sps_AuthenticateActiveUser]    Script Date: 5/19/2020 1:41:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Sps_AuthenticateActiveUser] 
@p_Email nvarchar(100) = null,
@p_Password nvarchar(500) = null
AS
/*
Created By	: RanaSikandar@mail.com, Date : 11-Jan-2020
Use For		: Verifying user’s authentication, Prevent SQL Injection Attacks
*/
declare @var_SQL nvarchar(650)
if (@p_Email != '' AND @p_Password != '')
BEGIN
	set @var_SQL = 'SELECT * FROM dbo.Users
		WHERE Email = @p_Email AND Password = @p_Password AND Enable=1'
END

execute sp_executesql @var_SQL, N'@p_Email nvarchar(100), @p_Password nvarchar(500)', @p_Email, @p_Password


GO
/****** Object:  StoredProcedure [dbo].[Sps_ChangeClientStatus]    Script Date: 5/19/2020 1:41:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Sps_ChangeClientStatus] 
@p_ClientId INT,
@p_Status BIT,
@p_StatusChangedBy INT

AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	
	BEGIN TRAN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	--SET NOCOUNT ON;

	BEGIN TRY
		BEGIN TRAN
			-- update user 			
			UPDATE [Users] SET [Users].[Enable]=@p_Status WHERE [Users].[Id]=@p_ClientId AND [Users].[Type]=4

			-- Log Activity
			INSERT INTO [UserLogs]
			(
			[LogDateTime]
			,[UserID]
			,[UserTypeID]
			,[LogDetail]
			)
			VALUES
			(
			GETUTCDATE()
			,@p_StatusChangedBy
			,(SELECT [Type] FROM [Users] WHERE [Users].[Id]=@p_StatusChangedBy)
			,'Client Status Changed to '+CONVERT(VARCHAR(5),@p_Status)
			)

		COMMIT TRAN
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
	END CATCH    
	
	IF(@@ERROR <> 0)
		ROLLBACK TRAN
	ELSE
		COMMIT TRAN           
END

GO
/****** Object:  StoredProcedure [dbo].[Sps_CountQRTblId]    Script Date: 5/19/2020 1:41:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Sps_CountQRTblId] 
@p_RestaurantId INT,
@p_QRCodeStr nvarchar(10) 
AS
/*
Created By	: RanaSikandar@mail.com, Date : 19-Feb-2020
*/
declare @var_SQL nvarchar(650)
if (@p_RestaurantId != '' AND @p_QRCodeStr != '')
BEGIN
	set @var_SQL = 'SELECT COUNT(Id) FROM RestaurantTables WHERE RestaurantId = @p_RestaurantId AND QRCodeStr = @p_QRCodeStr'
END

execute sp_executesql @var_SQL, N'@p_RestaurantId INT, @p_QRCodeStr nvarchar(10)', @p_RestaurantId, @p_QRCodeStr


GO
/****** Object:  StoredProcedure [dbo].[Sps_DeleteProductCategory]    Script Date: 5/19/2020 1:41:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Sps_DeleteProductCategory]  
@p_UserId INT,
@p_Id INT

AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	
	BEGIN TRAN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	--SET NOCOUNT ON;

	BEGIN TRY
		BEGIN TRAN
			-- Insert statements for procedure here
			
			DELETE FROM [ProductCategories] WHERE [ProductCategories].[Id]=@p_Id
					
					-- Log Activity
					INSERT INTO [UserLogs]
					(
					 [LogDateTime]
					,[UserID]
					,[UserTypeID]
					,[LogDetail]
					)
					VALUES
					(
					 GETUTCDATE()
					,@p_UserId
					,2
					,'Food Category Deleted'
					)

		COMMIT TRAN
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
	END CATCH    
	
	IF(@@ERROR <> 0)
		ROLLBACK TRAN
	ELSE
		COMMIT TRAN           
END

GO
/****** Object:  StoredProcedure [dbo].[Sps_DeleteUserWaiter]    Script Date: 5/19/2020 1:41:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Sps_DeleteUserWaiter]  
@p_Id INT,
@p_ByUserId INT

AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	
	BEGIN TRAN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	--SET NOCOUNT ON;

	BEGIN TRY
		BEGIN TRAN
			-- Insert statements for procedure here
			
			DELETE FROM [RestaurantWaiters] WHERE [RestaurantWaiters].[UserId]=@p_Id

			DELETE FROM [Users] WHERE [Users].[Id]=@p_Id
					
					-- Log Activity
					INSERT INTO [UserLogs]
					(
					 [LogDateTime]
					,[UserID]
					,[UserTypeID]
					,[LogDetail]
					)
					VALUES
					(
					 GETUTCDATE()
					,@p_ByUserId
					,2
					,'Waiter Deleted'
					)

		COMMIT TRAN
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
	END CATCH    
	
	IF(@@ERROR <> 0)
		ROLLBACK TRAN
	ELSE
		COMMIT TRAN           
END

GO
/****** Object:  StoredProcedure [dbo].[Sps_Insert_LastLogin]    Script Date: 5/19/2020 1:41:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		<Author,,ranasikandar@mail.com>
-- Create date: <Create Date 11,Jan,2020>
-- =============================================
CREATE PROCEDURE [dbo].[Sps_Insert_LastLogin]
	-- Add the parameters for the stored procedure here
	@p_UserID int
AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	
	BEGIN TRAN

	BEGIN TRY
		BEGIN TRAN
			-- Insert statements for procedure here
			UPDATE [Users]
					SET [LastLogin]=GETUTCDATE()
					WHERE Id=@p_UserID

					-- Log Activity
					INSERT INTO [UserLogs]
					(
					[LogDateTime]
					,[UserID]
					,[UserTypeID]
					,[LogDetail]
					)
					VALUES
					(
					GETUTCDATE()
					,@p_UserID
					,(SELECT [Type] FROM Users WHERE Id=@p_UserID)
					,'Signin'
					)

		COMMIT TRAN
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
	END CATCH    
	
	IF(@@ERROR <> 0)
		ROLLBACK TRAN
	ELSE
		COMMIT TRAN           
END



GO
/****** Object:  StoredProcedure [dbo].[Sps_Insert_UserWaiter]    Script Date: 5/19/2020 1:41:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Sps_Insert_UserWaiter]  
@p_ByUserId INT,
@p_Name NVARCHAR(50)=NULL,
@p_Email NVARCHAR(100),
@p_Password NVARCHAR(500),
@p_Enable BIT,
@p_LostPassKey varchar(10)=NULL,
@p_Details NVARCHAR(500),
@p_RestaurantId INT

AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	
	BEGIN TRAN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	--SET NOCOUNT ON;

	BEGIN TRY
		BEGIN TRAN
			-- Insert statements for procedure here
			
			INSERT INTO [Users]
				   (
					 [Name]
					,[Email]
					,[Password] 
					,[Type] 
					,[Enable] 
					,[LostPassKey]
					,[EmailVerify]
					,[Details]
				   )
			 VALUES
				   (
					@p_Name
					,@p_Email
					,@p_Password 
					,3
					,@p_Enable
					,@p_LostPassKey
					,0
					,@p_Details
					)

			DECLARE @maxSiteUser INT;
			SET @maxSiteUser=SCOPE_IDENTITY();

			INSERT INTO [RestaurantWaiters]
					(
						 [UserId]
						 ,[RestaurantId]
					)
			VALUES
					(
						@maxSiteUser
						,@p_RestaurantId
					)

					-- Log Activity
					INSERT INTO [UserLogs]
					(
					[LogDateTime]
					,[UserID]
					,[UserTypeID]
					,[LogDetail]
					)
					VALUES
					(
					GETUTCDATE()
					,@p_ByUserId
					,2
					,'New Waiter Account Saved'
					)

		COMMIT TRAN
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
	END CATCH    
	
	IF(@@ERROR <> 0)
		ROLLBACK TRAN
	ELSE
		COMMIT TRAN           
END

GO
/****** Object:  StoredProcedure [dbo].[Sps_InsertProduct]    Script Date: 5/19/2020 1:41:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Sps_InsertProduct]  
@p_UserId INT,
@p_CategoryId INT,
@p_Name NVARCHAR(50),
@p_Discription NVARCHAR(500)=NULL,
@p_ImagePath NVARCHAR(500)=NULL,
@p_Price MONEY,
@p_RestaurantId INT,
@p_Enable BIT

AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	
	BEGIN TRAN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	--SET NOCOUNT ON;

	BEGIN TRY
		BEGIN TRAN
			-- Insert statements for procedure here
			
			IF(@p_ImagePath='' OR @p_ImagePath IS NULL)
			BEGIN
			INSERT INTO [Products]
				   (
					 [Name]
					,[Discription]
					,[Price] 
					,[RestaurantId] 
					,[Enable] 
					,[CategoryId]
				   )
			 VALUES
				   (
					@p_Name
					,@p_Discription
					,@p_Price 
					,@p_RestaurantId
					,@p_Enable 
					,@p_CategoryId
					)
					END
					ELSE
					BEGIN
					INSERT INTO [Products]
				   (
					 [Name]
					,[Discription]
					,[ImagePath]
					,[Price] 
					,[RestaurantId] 
					,[Enable] 
					,[CategoryId]
				   )
			 VALUES
				   (
					@p_Name
					,@p_Discription
					,@p_ImagePath
					,@p_Price 
					,@p_RestaurantId
					,@p_Enable 
					,@p_CategoryId
					)
					END

					-- Log Activity
					INSERT INTO [UserLogs]
					(
					[LogDateTime]
					,[UserID]
					,[UserTypeID]
					,[LogDetail]
					)
					VALUES
					(
					GETUTCDATE()
					,@p_UserId
					,2
					,'New Food Product '+@p_Name +' Added'
					)


		COMMIT TRAN
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
	END CATCH    
	
	IF(@@ERROR <> 0)
		ROLLBACK TRAN
	ELSE
		COMMIT TRAN           
END

GO
/****** Object:  StoredProcedure [dbo].[Sps_InsertProductCategory]    Script Date: 5/19/2020 1:41:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Sps_InsertProductCategory]  
@p_UserId INT,
@p_RestaurantId INT,
@p_Name NVARCHAR(50), 
@p_Discription NVARCHAR(500)=NULL, 
@p_DisplayOrder INT

AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	
	BEGIN TRAN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	--SET NOCOUNT ON;

	BEGIN TRY
		BEGIN TRAN
			-- Insert statements for procedure here
			
			INSERT INTO [ProductCategories]
				   (
					[RestaurantId] 
					,[CategoryName]
					,[Discription]
					,[DisplayOrder]
				   )
			 VALUES
				   (
					@p_RestaurantId
					,@p_Name
					,@p_Discription
					,@p_DisplayOrder
					)
					
					-- Log Activity
					INSERT INTO [UserLogs]
					(
					 [LogDateTime]
					,[UserID]
					,[UserTypeID]
					,[LogDetail]
					)
					VALUES
					(
					 GETUTCDATE()
					,@p_UserId
					,2
					,'New Food Category '+@p_Name +' Added'
					)

		COMMIT TRAN
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
	END CATCH    
	
	IF(@@ERROR <> 0)
		ROLLBACK TRAN
	ELSE
		COMMIT TRAN           
END

GO
/****** Object:  StoredProcedure [dbo].[Sps_InsertTable]    Script Date: 5/19/2020 1:41:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Sps_InsertTable]  
@p_UserId INT,
@p_Name NVARCHAR(50),
@p_RestaurantId INT,
@p_Discription NVARCHAR(200)=NULL,
@p_Enable BIT,
@p_QRImageLocation NVARCHAR(500),
@p_QRCodeStr NVARCHAR(100)

AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	
	BEGIN TRAN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	--SET NOCOUNT ON;

	BEGIN TRY
		BEGIN TRAN
			-- Insert statements for procedure here
			
			INSERT INTO [RestaurantTables]
				   (
					 [Name] 
					,[RestaurantId] 
					,[Discription]
					,[Enable] 
					,[QRImageLocation] 
					,[QRCodeStr] 
				   )
			 VALUES
				   (
					 @p_Name 
					,@p_RestaurantId 
					,@p_Discription 
					,@p_Enable 
					,@p_QRImageLocation 
					,@p_QRCodeStr 
					)
					
					-- Log Activity
					INSERT INTO [UserLogs]
					(
					 [LogDateTime]
					,[UserID]
					,[UserTypeID]
					,[LogDetail]
					)
					VALUES
					(
					 GETUTCDATE()
					,@p_UserId
					,2--(SELECT [Type] FROM Users WHERE Id=@p_UserId)
					,'New Restaurant Table '+@p_Name +' Added'
					)

		COMMIT TRAN
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
	END CATCH    
	
	IF(@@ERROR <> 0)
		ROLLBACK TRAN
	ELSE
		COMMIT TRAN           
END

GO
/****** Object:  StoredProcedure [dbo].[Sps_MarkOrderPaidUnPaid]    Script Date: 5/19/2020 1:41:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Sps_MarkOrderPaidUnPaid] 

@p_OrdId BIGINT,
@p_Paid BIT,
@p_UserId INT

AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	
	BEGIN TRAN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	--SET NOCOUNT ON;

	BEGIN TRY
		BEGIN TRAN
			-- Insert statements for procedure here
			
			DECLARE @PaidStatus NVARCHAR(100);

			UPDATE [Orders]
				   SET [Paid]=@p_Paid
					WHERE [Orders].[Id]=@p_OrdId 
					
					IF(@p_Paid=1)
					BEGIN
					UPDATE [Orders]
					SET [StatusNameId]=4
					WHERE [Orders].[Id]=@p_OrdId 
					-- insert status
					INSERT INTO [OrderStatus]
					(
					[OrderId]
					,[StatusId]
					,[DateTime]
					)
					VALUES
					(
					@p_OrdId,4,GETUTCDATE()
					)
					SET @PaidStatus='Paid';
					END
					ELSE
					BEGIN
					SET @PaidStatus='UnPaid';
					END

					-- Log Activity
					INSERT INTO [UserLogs]
					(
					[LogDateTime]
					,[UserID]
					,[UserTypeID]
					,[LogDetail]
					)
					VALUES
					(
					GETUTCDATE()
					,@p_UserId
					,2
					,'Order Status Updated As '+@PaidStatus
					)


		COMMIT TRAN
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
	END CATCH    
	
	IF(@@ERROR <> 0)
		ROLLBACK TRAN
	ELSE
		COMMIT TRAN           
END




GO
/****** Object:  StoredProcedure [dbo].[Sps_MarkOrdersCancel]    Script Date: 5/19/2020 1:41:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[Sps_MarkOrdersCancel] 

@p_TblId INT,
@p_ResId INT,
@p_UserId INT

AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	
	BEGIN TRAN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	--SET NOCOUNT ON;

	BEGIN TRY
		BEGIN TRAN
			-- Insert statements for procedure here
			
			UPDATE [Orders]
				   SET [StatusNameId]=2
					WHERE [Orders].[RestaurantId]=@p_ResId AND [Orders].[TableId]=@p_TblId AND ([Orders].[StatusNameId]=1 OR [Orders].[StatusNameId]=3)
					
					-- insert status --use foreach to put into table with orderId
					--INSERT INTO [OrderStatus]
					--(
					--[OrderId]
					--,[StatusId]
					--,[DateTime]
					--)
					--VALUES
					--(
					--@p_OrdId,4,GETUTCDATE()
					--)

					-- Log Activity
					INSERT INTO [UserLogs]
					(
					[LogDateTime]
					,[UserID]
					,[UserTypeID]
					,[LogDetail]
					)
					VALUES
					(
					GETUTCDATE()
					,@p_UserId
					,2
					,'Orders Status Updated As Cancel'
					)


		COMMIT TRAN
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
	END CATCH    
	
	IF(@@ERROR <> 0)
		ROLLBACK TRAN
	ELSE
		COMMIT TRAN           
END

GO
/****** Object:  StoredProcedure [dbo].[Sps_MarkOrdersPaid]    Script Date: 5/19/2020 1:41:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[Sps_MarkOrdersPaid] 

@p_TblId INT,
@p_ResId INT,
@p_UserId INT

AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	
	BEGIN TRAN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	--SET NOCOUNT ON;

	BEGIN TRY
		BEGIN TRAN
			-- Insert statements for procedure here
			
			DECLARE @PaidStatus NVARCHAR(100);

			UPDATE [Orders]
				   SET [Paid]=1, [StatusNameId]=4
					WHERE [Orders].[RestaurantId]=@p_ResId AND [Orders].[TableId]=@p_TblId AND ([Orders].[StatusNameId]=1 OR [Orders].[StatusNameId]=3)
					
					-- insert status --use foreach to put into table with orderId
					--INSERT INTO [OrderStatus]
					--(
					--[OrderId]
					--,[StatusId]
					--,[DateTime]
					--)
					--VALUES
					--(
					--@p_OrdId,4,GETUTCDATE()
					--)

					-- Log Activity
					INSERT INTO [UserLogs]
					(
					[LogDateTime]
					,[UserID]
					,[UserTypeID]
					,[LogDetail]
					)
					VALUES
					(
					GETUTCDATE()
					,@p_UserId
					,2
					,'Orders Status Updated As Paid'
					)


		COMMIT TRAN
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
	END CATCH    
	
	IF(@@ERROR <> 0)
		ROLLBACK TRAN
	ELSE
		COMMIT TRAN           
END

GO
/****** Object:  StoredProcedure [dbo].[Sps_MarkOrderStatus]    Script Date: 5/19/2020 1:41:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Sps_MarkOrderStatus] 

@p_OrdId BIGINT,
@p_Status INT,
@p_UserId INT

AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	
	BEGIN TRAN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	--SET NOCOUNT ON;

	BEGIN TRY
		BEGIN TRAN
			-- Insert statements for procedure here
			
			UPDATE [Orders]
				   SET [StatusNameId]=@p_Status
					WHERE [Orders].[Id]=@p_OrdId 
					
					-- insert status
					INSERT INTO [OrderStatus]
					(
					[OrderId]
					,[StatusId]
					,[DateTime]
					)
					VALUES
					(
					@p_OrdId,@p_Status,GETUTCDATE()
					)

					-- Log Activity
					INSERT INTO [UserLogs]
					(
					[LogDateTime]
					,[UserID]
					,[UserTypeID]
					,[LogDetail]
					)
					VALUES
					(
					GETUTCDATE()
					,@p_UserId
					,2
					,'Order Status Updated'
					)


		COMMIT TRAN
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
	END CATCH    
	
	IF(@@ERROR <> 0)
		ROLLBACK TRAN
	ELSE
		COMMIT TRAN           
END




GO
/****** Object:  StoredProcedure [dbo].[Sps_PlaceAnOrder]    Script Date: 5/19/2020 1:41:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Sps_PlaceAnOrder] 
@p_Rest INT,
@p_Tbl INT,
@p_Pro BIGINT, 
@p_Qty INT,
@p_OrderBy INT,
@p_Note NVARCHAR(200)=NULL

AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	
	BEGIN TRAN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	--SET NOCOUNT ON;

	BEGIN TRY
		BEGIN TRAN
			-- Insert order 			
			INSERT INTO [Orders]
				   (
					 [OrderPlacedDTime]
					,[RestaurantId]
					,[TableId] 
					,[Paid] 
					,[OrderBy] 
					,[ProductId]
					,[Qty]
					,[StatusNameId]
					,[Note]
				   )
			 VALUES
				   (
					GETUTCDATE()
					,@p_Rest
					,@p_Tbl 
					,0
					,@p_OrderBy
					,@p_Pro
					,@p_Qty
					,1
					,@p_Note
					)

			DECLARE @orderID BIGINT;
			SET @orderID=SCOPE_IDENTITY();
			
			--insert status 
			INSERT INTO [OrderStatus]
					(
						 [OrderId]
						 ,[StatusId]
						 ,[DateTime]
					)
			VALUES
					(
					@orderID 
					,1 
					,GETUTCDATE()
					)

					-- Log Activity
					INSERT INTO [UserLogs]
					(
					[LogDateTime]
					,[UserID]
					,[UserTypeID]
					,[LogDetail]
					)
					VALUES
					(
					GETUTCDATE()
					,(SELECT UserId FROM RestaurantUsers WHERE RestaurantId=@p_Rest)
					,2
					,'New Order Placed. Table:'+(SELECT Name FROM RestaurantTables WHERE RestaurantId=@p_Rest AND Id=@p_Tbl)+'. Food:'+(SELECT Name FROM Products WHERE RestaurantId=@p_Rest AND Id=@p_Pro)+'. Qty:'+CONVERT(VARCHAR(50),@p_Qty)
					)


		COMMIT TRAN
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
	END CATCH    
	
	IF(@@ERROR <> 0)
		ROLLBACK TRAN
	ELSE
		COMMIT TRAN           
END
GO
/****** Object:  StoredProcedure [dbo].[Sps_RestaurantTblIdValid]    Script Date: 5/19/2020 1:41:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[Sps_RestaurantTblIdValid] 
@p_RestaurantId INT,
@p_QRCodeStr nvarchar(10) 
AS
/*
Created By	: RanaSikandar@mail.com, Date : 23-Jan-2020
*/
declare @var_SQL nvarchar(650)
if (@p_RestaurantId != '' AND @p_QRCodeStr != '')
BEGIN
	set @var_SQL = 'SELECT Id FROM RestaurantTables WHERE RestaurantId = @p_RestaurantId AND QRCodeStr = @p_QRCodeStr AND Enable=1'
END

execute sp_executesql @var_SQL, N'@p_RestaurantId INT, @p_QRCodeStr nvarchar(10)', @p_RestaurantId, @p_QRCodeStr

GO
/****** Object:  StoredProcedure [dbo].[Sps_Update_UserWaiter]    Script Date: 5/19/2020 1:41:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Sps_Update_UserWaiter]  
@p_UserId INT,
@p_ByUserId INT,
@p_Name NVARCHAR(50)=NULL,
@p_Email NVARCHAR(100),
@p_Password NVARCHAR(500),
@p_Enable BIT,
@p_LostPassKey varchar(10)=NULL,
@p_Details NVARCHAR(500),
@p_RestaurantId INT,
@p_WaiterEmailUpdate BIT

AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	
	BEGIN TRAN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	--SET NOCOUNT ON;

	BEGIN TRY
		BEGIN TRAN
			-- Insert statements for procedure here
			
			IF(@p_WaiterEmailUpdate=1)
			BEGIN
			UPDATE [Users]
				   SET [Name]=@p_Name
					,[Email]=@p_Email
					,[Password] =@p_Password
					,[Enable] =@p_Enable
					,[LostPassKey]=@p_LostPassKey
					,[EmailVerify]=0
					,[Details]=@p_Details 
					WHERE [Id]=@p_UserId
				 END
				 ELSE
				 BEGIN
				 UPDATE [Users]
				   SET [Name]=@p_Name
					,[Password] =@p_Password
					,[Enable] =@p_Enable
					,[Details]=@p_Details 
					WHERE [Id]=@p_UserId
				 END  

					-- Log Activity
					INSERT INTO [UserLogs]
					(
					[LogDateTime]
					,[UserID]
					,[UserTypeID]
					,[LogDetail]
					)
					VALUES
					(
					GETUTCDATE()
					,@p_ByUserId
					,2
					,'Waiter Account Updated'
					)

		COMMIT TRAN
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
	END CATCH    
	
	IF(@@ERROR <> 0)
		ROLLBACK TRAN
	ELSE
		COMMIT TRAN           
END

GO
/****** Object:  StoredProcedure [dbo].[Sps_UpdateBrandingLogos]    Script Date: 5/19/2020 1:41:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Description:	<User Restaurant Logo Update>
-- =============================================
CREATE PROCEDURE [dbo].[Sps_UpdateBrandingLogos]  

	-- Add the parameters for the stored procedure here
@p_Id INT, 
@p_UserId INT,
@p_LogoPath NVARCHAR(500)=NULL, 
@p_LogoSmallPath NVARCHAR(500)=NULL

AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	
	BEGIN TRAN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	--SET NOCOUNT ON;

	BEGIN TRY
		BEGIN TRAN
		
		-- Insert statements for procedure here
			
			IF (@p_LogoPath!='0' AND @p_LogoSmallPath!='0')
			BEGIN
			UPDATE [Restaurants] 
					SET [Restaurants].[LogoPath]=@p_LogoPath 
					,[Restaurants].[LogoSmallPath]=@p_LogoSmallPath  
					WHERE [Restaurants].[Id]=@p_Id
			END
			ELSE
			BEGIN
				IF (@p_LogoPath='0')
				BEGIN
				UPDATE [Restaurants] 
				SET [Restaurants].[LogoSmallPath]=@p_LogoSmallPath 
				WHERE [Restaurants].[Id]=@p_Id
				END

				IF (@p_LogoSmallPath='0')
				BEGIN
				UPDATE [Restaurants] 
				SET [Restaurants].[LogoPath]=@p_LogoPath 
				WHERE [Restaurants].[Id]=@p_Id
				END
			END	

			-- Log Activity
			INSERT INTO [UserLogs]
			(
			[LogDateTime]
			,[UserID]
			,[UserTypeID]
			,[LogDetail]
			)
			VALUES
			(
			GETUTCDATE()
			,@p_UserId
			,2
			,'Restaurant Branding Logo Updated By Restaurant User'
			)
					
		COMMIT TRAN
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
	END CATCH    
	
	IF(@@ERROR <> 0)
		ROLLBACK TRAN
	ELSE
		COMMIT TRAN           
END

GO
/****** Object:  StoredProcedure [dbo].[Sps_UpdateProduct]    Script Date: 5/19/2020 1:41:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Sps_UpdateProduct]  

@p_Id BIGINT,
@p_UserId INT,
@p_CategoryId INT,
@p_Name NVARCHAR(50),
@p_Discription NVARCHAR(500)=NULL,
@p_ImagePath NVARCHAR(500)=NULL,
@p_Price MONEY,
@p_Enable BIT

AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	
	BEGIN TRAN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	--SET NOCOUNT ON;

	BEGIN TRY
		BEGIN TRAN
			-- Insert statements for procedure here
			
			IF(@p_ImagePath='' OR @p_ImagePath IS NULL)
			BEGIN
			UPDATE [Products]
				   SET [Name]=@p_Name
					,[Discription]=@p_Discription
					,[Price]=@p_Price 
					,[Enable]=@p_Enable 
					,[CategoryId]=@p_CategoryId
					WHERE [Products].[Id]=@p_Id 
					END
					ELSE
					BEGIN
					UPDATE [Products]
				   SET [Name]=@p_Name
					,[Discription]=@p_Discription
					,[ImagePath]=@p_ImagePath
					,[Price]=@p_Price 
					,[Enable]=@p_Enable 
					,[CategoryId]=@p_CategoryId
					WHERE [Products].[Id]=@p_Id 
					END

					-- Log Activity
					INSERT INTO [UserLogs]
					(
					[LogDateTime]
					,[UserID]
					,[UserTypeID]
					,[LogDetail]
					)
					VALUES
					(
					GETUTCDATE()
					,@p_UserId
					,2
					,'Food Product '+@p_Name +' Updated'
					)


		COMMIT TRAN
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
	END CATCH    
	
	IF(@@ERROR <> 0)
		ROLLBACK TRAN
	ELSE
		COMMIT TRAN           
END
GO
/****** Object:  StoredProcedure [dbo].[Sps_UpdateProductCategory]    Script Date: 5/19/2020 1:41:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Sps_UpdateProductCategory] 

@p_Id INT,
@p_UserId INT,
@p_RestaurantId INT,
@p_Name NVARCHAR(50),
@p_Discription NVARCHAR(500)=NULL,
@p_DisplayOrder INT

AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	
	BEGIN TRAN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	--SET NOCOUNT ON;

	BEGIN TRY
		BEGIN TRAN
			-- Insert statements for procedure here
			
			UPDATE [ProductCategories]
				   SET [CategoryName]=@p_Name ,[Discription] = @p_Discription, [DisplayOrder] = @p_DisplayOrder 
					WHERE [ProductCategories].[Id]=@p_Id AND [ProductCategories].[RestaurantId]=@p_RestaurantId
					
					-- Log Activity
					INSERT INTO [UserLogs]
					(
					[LogDateTime]
					,[UserID]
					,[UserTypeID]
					,[LogDetail]
					)
					VALUES
					(
					GETUTCDATE()
					,@p_UserId
					,2
					,'Food Category '+@p_Name +' Updated'
					)


		COMMIT TRAN
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
	END CATCH    
	
	IF(@@ERROR <> 0)
		ROLLBACK TRAN
	ELSE
		COMMIT TRAN           
END

GO
/****** Object:  StoredProcedure [dbo].[Sps_UpdateTable]    Script Date: 5/19/2020 1:41:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Sps_UpdateTable] 

@p_Id INT,
@p_UserId INT,
@p_Name NVARCHAR(50),
@p_RestaurantId INT,
@p_Discription NVARCHAR(200)=NULL,
@p_Enable BIT,
@p_QRImageLocation NVARCHAR(500),
@p_QRCodeStr NVARCHAR(100)

AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	
	BEGIN TRAN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	--SET NOCOUNT ON;

	BEGIN TRY
		BEGIN TRAN
			-- Insert statements for procedure here
			
			UPDATE [RestaurantTables]
				   SET [Name]=@p_Name
				   ,[RestaurantId]=@p_RestaurantId
					,[Discription]=@p_Discription
					,[Enable]=@p_Enable 
					,[QRImageLocation]=@p_QRImageLocation
					,[QRCodeStr]=@p_QRCodeStr
					WHERE [RestaurantTables].[Id]=@p_Id 
					
					-- Log Activity
					INSERT INTO [UserLogs]
					(
					[LogDateTime]
					,[UserID]
					,[UserTypeID]
					,[LogDetail]
					)
					VALUES
					(
					GETUTCDATE()
					,@p_UserId
					,2
					,'Restaurant Table '+@p_Name +' Updated'
					)


		COMMIT TRAN
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
	END CATCH    
	
	IF(@@ERROR <> 0)
		ROLLBACK TRAN
	ELSE
		COMMIT TRAN           
END



GO
/****** Object:  StoredProcedure [dbo].[Sps_UserClientEmailVerification]    Script Date: 5/19/2020 1:41:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Description:	<Insert Email Verification>
-- =============================================
CREATE PROCEDURE [dbo].[Sps_UserClientEmailVerification]  

	-- Add the parameters for the stored procedure here
@p_Email nvarchar(100), 
@p_LostPassKey varchar(10)

AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	
	BEGIN TRAN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	--SET NOCOUNT ON;

	BEGIN TRY
		BEGIN TRAN
		
		-- Insert statements for procedure here
			UPDATE [Users]
				   SET [LostPassKey]=null,
					[EmailVerify]=1
					WHERE [Users].[Email]=@p_Email AND [Users].[LostPassKey]=@p_LostPassKey
			
					-- Log Activity
					INSERT INTO [UserLogs]
					(
					[LogDateTime]
					,[UserID]
					,[UserTypeID]
					,[LogDetail]
					)
					VALUES
					(
					GETUTCDATE()
					,(SELECT [Id] FROM [Users] WHERE [Email]=@p_Email)
					,(SELECT [Type] FROM [Users] WHERE [Email]=@p_Email)
					,'Client Email varification'
					)

		COMMIT TRAN
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
	END CATCH    
	
	IF(@@ERROR <> 0)
		ROLLBACK TRAN
	ELSE
		COMMIT TRAN           
END

GO
/****** Object:  StoredProcedure [dbo].[Sps_UserRestaurant_ProfileUpdate]    Script Date: 5/19/2020 1:41:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Description:	<User Restaurant Profile Update>
-- =============================================
CREATE PROCEDURE [dbo].[Sps_UserRestaurant_ProfileUpdate]  

	-- Add the parameters for the stored procedure here
@p_UId INT, 
@p_UName NVARCHAR(50)=NULL, 
@p_RName NVARCHAR(100)=NULL,
@p_Country NVARCHAR(50)=NULL, 
@p_City NVARCHAR(50)=NULL, 
@p_Address NVARCHAR(50)=NULL, 
@p_Phone NVARCHAR(20)=NULL, 
@p_CurrencyCode NVARCHAR(3)=NULL, 
@p_NotiEmail NVARCHAR(50)=NULL, 
@p_SendNotiEmail BIT=NULL,
@p_RLogoSmallPath NVARCHAR(500)=NULL,
@p_RLogoPath NVARCHAR(500)=NULL

AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	
	BEGIN TRAN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	--SET NOCOUNT ON;

	BEGIN TRY
		BEGIN TRAN
		
		-- Insert statements for procedure here
			UPDATE [Users]
				   SET [Name]=@p_UName 
					WHERE [Users].[Type]=2 AND [Users].[Id]=@p_UId

					IF (@p_RLogoPath='0' OR @p_RLogoSmallPath='0')
					BEGIN
					UPDATE [Restaurants] 
					SET [Restaurants].[Name]=@p_RName 
					,[Restaurants].[Country]=@p_Country
					,[Restaurants].[City]=@p_City
					,[Restaurants].[Address]=@p_Address
					,[Restaurants].[Phone]=@p_Phone
					,[Restaurants].[CurrencyCode]=@p_CurrencyCode
					,[Restaurants].[NotificationEmail]=@p_NotiEmail
					,[Restaurants].[SendEmailNotification]=@p_SendNotiEmail
					WHERE [Restaurants].[Id]=(SELECT [RestaurantUsers].RestaurantId FROM [RestaurantUsers] WHERE [RestaurantUsers].UserId=@p_UId )
					END
					ELSE
					BEGIN
					UPDATE [Restaurants] 
					SET [Restaurants].[Name]=@p_RName 
					,[Restaurants].[Country]=@p_Country
					,[Restaurants].[City]=@p_City
					,[Restaurants].[Address]=@p_Address
					,[Restaurants].[Phone]=@p_Phone
					,[Restaurants].[CurrencyCode]=@p_CurrencyCode
					,[Restaurants].[NotificationEmail]=@p_NotiEmail
					,[Restaurants].[SendEmailNotification]=@p_SendNotiEmail
					,[Restaurants].[LogoPath]=@p_RLogoPath
					,[Restaurants].[LogoSmallPath]=@p_RLogoSmallPath
					WHERE [Restaurants].[Id]=(SELECT [RestaurantUsers].RestaurantId FROM [RestaurantUsers] WHERE [RestaurantUsers].UserId=@p_UId )
					END

					-- Log Activity
					INSERT INTO [UserLogs]
					(
					[LogDateTime]
					,[UserID]
					,[UserTypeID]
					,[LogDetail]
					)
					VALUES
					(
					GETUTCDATE()
					,@p_UId
					,2
					,'Restaurant Profile Updated By Restaurant User'
					)
					
		COMMIT TRAN
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
	END CATCH    
	
	IF(@@ERROR <> 0)
		ROLLBACK TRAN
	ELSE
		COMMIT TRAN           
END

GO
/****** Object:  StoredProcedure [dbo].[Sps_UserRestaurant_Signup]    Script Date: 5/19/2020 1:41:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Sps_UserRestaurant_Signup]  
@p_Name NVARCHAR(50)=NULL,
@p_Email NVARCHAR(100),
@p_Password NVARCHAR(500),
@p_LostPassKey varchar(10)=NULL

AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	
	BEGIN TRAN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	--SET NOCOUNT ON;

	BEGIN TRY
		BEGIN TRAN
			-- Insert statements for procedure here
			
			INSERT INTO [Users]
				   (
					 [Name]
					,[Email]
					,[Password] 
					,[Type] 
					,[Enable] 
					,[LostPassKey]
					,[EmailVerify]
				   )
			 VALUES
				   (
					@p_Name
					,@p_Email
					,@p_Password 
					,2
					,1
					,@p_LostPassKey
					,0
					)

			DECLARE @maxSiteUser INT;
			--SET @maxSiteUser=(SELECT COALESCE(MAX(Id), 0) FROM Users);
			SET @maxSiteUser=SCOPE_IDENTITY();

			DECLARE @maxResturantId INT;
			INSERT INTO [Restaurants]
					(
						 [RegisterDate]
					)
			VALUES
					(
						GETUTCDATE()
					)
					SET @maxResturantId=SCOPE_IDENTITY();

					INSERT INTO [RestaurantUsers]
					(
					[UserId]
					,[RestaurantId]
					)
					VALUES
					(
					@maxSiteUser
					,@maxResturantId
					)
					-- Log Activity
					INSERT INTO [UserLogs]
					(
					[LogDateTime]
					,[UserID]
					,[UserTypeID]
					,[LogDetail]
					)
					VALUES
					(
					GETUTCDATE()
					,@maxSiteUser
					,2
					,'New Restaurant User Signup'
					)


		COMMIT TRAN
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
	END CATCH    
	
	IF(@@ERROR <> 0)
		ROLLBACK TRAN
	ELSE
		COMMIT TRAN           
END



GO
/****** Object:  StoredProcedure [dbo].[Sps_UserRestaurantClient_Signup]    Script Date: 5/19/2020 1:41:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Sps_UserRestaurantClient_Signup]  

@p_Name NVARCHAR(50)=NULL,
@p_Email NVARCHAR(100),
@p_Password NVARCHAR(500),
@p_LostPassKey varchar(10)=NULL,

@p_CC NVARCHAR(50)=NULL,
@p_CCExpMonth INT=NULL,
@p_CCExpYear INT=NULL,
@p_CVV NVARCHAR(10)=NULL

AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	
	BEGIN TRAN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	--SET NOCOUNT ON;

	BEGIN TRY
		BEGIN TRAN
			-- Insert statements for procedure here
			
			INSERT INTO [Users]
				   (
					 [Name]
					,[Email]
					,[Password] 
					,[Type] 
					,[Enable] 
					,[LostPassKey]
					,[EmailVerify]
					,[Details]
				   )
			 VALUES
				   (
					@p_Name
					,@p_Email
					,@p_Password 
					,4
					,1
					,@p_LostPassKey
					,0
					,'New Restaurant Client Signup'
					)

			DECLARE @maxSiteUser INT;
			SET @maxSiteUser=SCOPE_IDENTITY();

			INSERT INTO [RestaurantClients]
					(
						 [UserId]
						 ,[CardNumber]
						 ,[CCExpMonth]
						 ,[CCExpYear]
						 ,[CVV]
					)
			VALUES
					(
						@maxSiteUser
						,@p_CC
						,@p_CCExpMonth
						,@p_CCExpYear
						,@p_CVV
					)

					-- Log Activity
					INSERT INTO [UserLogs]
					(
					[LogDateTime]
					,[UserID]
					,[UserTypeID]
					,[LogDetail]
					)
					VALUES
					(
					GETUTCDATE()
					,@maxSiteUser
					,4
					,'New Restaurant Client Signup'
					)

		COMMIT TRAN
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
	END CATCH    
	
	IF(@@ERROR <> 0)
		ROLLBACK TRAN
	ELSE
		COMMIT TRAN           
END


GO
/****** Object:  StoredProcedure [dbo].[Sps_UserRestaurantClient_Update]    Script Date: 5/19/2020 1:41:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Sps_UserRestaurantClient_Update]  
@p_UserId INT,
@p_Name NVARCHAR(50)=NULL,
@p_CC NVARCHAR(50)=NULL,
@p_CCExpMonth INT=NULL,
@p_CCExpYear INT=NULL,
@p_CVV NVARCHAR(10)=NULL

AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	
	BEGIN TRAN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	--SET NOCOUNT ON;

	BEGIN TRY
		BEGIN TRAN
			-- Insert statements for procedure here
			
				 UPDATE [Users]
				 SET [Name]=@p_Name
				 WHERE [Id]=@p_UserId 

				UPDATE [RestaurantClients] 
				SET [CardNumber]=@p_CC,[CCExpMonth]=@p_CCExpMonth,[CCExpYear]=@p_CCExpYear,[CVV]=@p_CVV 
				WHERE [UserId]=@p_UserId 

					-- Log Activity
					INSERT INTO [UserLogs]
					(
					[LogDateTime]
					,[UserID]
					,[UserTypeID]
					,[LogDetail]
					)
					VALUES
					(
					GETUTCDATE()
					,@p_UserId
					,4
					,'Client Profile Updated'
					)

		COMMIT TRAN
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
	END CATCH    
	
	IF(@@ERROR <> 0)
		ROLLBACK TRAN
	ELSE
		COMMIT TRAN           
END

GO
/****** Object:  StoredProcedure [dbo].[Sps_UserRestaurantEmailVerification]    Script Date: 5/19/2020 1:41:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Description:	<Insert Email Verification>
-- =============================================
CREATE PROCEDURE [dbo].[Sps_UserRestaurantEmailVerification]  

	-- Add the parameters for the stored procedure here
@p_Email nvarchar(100), 
@p_LostPassKey varchar(10)

AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	
	BEGIN TRAN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	--SET NOCOUNT ON;

	BEGIN TRY
		BEGIN TRAN
		
		-- Insert statements for procedure here
			UPDATE [Users]
				   SET [LostPassKey]=null,
					[EmailVerify]=1
					WHERE [Users].[Email]=@p_Email AND [Users].[LostPassKey]=@p_LostPassKey
			
					-- Log Activity
					INSERT INTO [UserLogs]
					(
					[LogDateTime]
					,[UserID]
					,[UserTypeID]
					,[LogDetail]
					)
					VALUES
					(
					GETUTCDATE()
					,(SELECT [Id] FROM [Users] WHERE [Email]=@p_Email)
					,(SELECT [Type] FROM [Users] WHERE [Email]=@p_Email)
					,'New Restaurant Email varification attempt'
					)

					--put join date
					UPDATE [Restaurants]
					SET [RegisterDate]=GETUTCDATE() 
					WHERE [Restaurants].[Id]=(
					SELECT RestaurantId FROM RestaurantUsers WHERE UserId=(SELECT [Id] FROM [Users] WHERE [Email]=@p_Email)
					)

		COMMIT TRAN
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
	END CATCH    
	
	IF(@@ERROR <> 0)
		ROLLBACK TRAN
	ELSE
		COMMIT TRAN           
END



GO
/****** Object:  StoredProcedure [dbo].[Sps_UserWaiterEmailVerification]    Script Date: 5/19/2020 1:41:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Description:	<Insert Email Verification>
-- =============================================
CREATE PROCEDURE [dbo].[Sps_UserWaiterEmailVerification]  

	-- Add the parameters for the stored procedure here
@p_Email nvarchar(100), 
@p_LostPassKey varchar(10)

AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	
	BEGIN TRAN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	--SET NOCOUNT ON;

	BEGIN TRY
		BEGIN TRAN
		
		-- Insert statements for procedure here
			UPDATE [Users]
				   SET [LostPassKey]=null,
					[EmailVerify]=1
					WHERE [Users].[Email]=@p_Email AND [Users].[LostPassKey]=@p_LostPassKey
			
					-- Log Activity
					INSERT INTO [UserLogs]
					(
					[LogDateTime]
					,[UserID]
					,[UserTypeID]
					,[LogDetail]
					)
					VALUES
					(
					GETUTCDATE()
					,(SELECT [Id] FROM [Users] WHERE [Email]=@p_Email)
					,(SELECT [Type] FROM [Users] WHERE [Email]=@p_Email)
					,'Waiter Email varification'
					)

		COMMIT TRAN
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
	END CATCH    
	
	IF(@@ERROR <> 0)
		ROLLBACK TRAN
	ELSE
		COMMIT TRAN           
END

GO
USE [master]
GO
ALTER DATABASE [RestaurantMenu] SET  READ_WRITE 
GO
