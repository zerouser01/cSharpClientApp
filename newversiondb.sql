USE [master]
GO
/****** Object:  Database [Organization]    Script Date: 01.05.2022 15:13:19 ******/
CREATE DATABASE [Organization]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Organization', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Organization.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Organization_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Organization_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Organization] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Organization].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Organization] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Organization] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Organization] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Organization] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Organization] SET ARITHABORT OFF 
GO
ALTER DATABASE [Organization] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Organization] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Organization] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Organization] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Organization] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Organization] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Organization] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Organization] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Organization] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Organization] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Organization] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Organization] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Organization] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Organization] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Organization] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Organization] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Organization] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Organization] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Organization] SET  MULTI_USER 
GO
ALTER DATABASE [Organization] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Organization] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Organization] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Organization] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Organization] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Organization] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Organization] SET QUERY_STORE = OFF
GO
USE [Organization]
GO
/****** Object:  Table [dbo].[Clients]    Script Date: 01.05.2022 15:13:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clients](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[FieldOfActivity] [nvarchar](100) NULL,
	[INN] [nvarchar](100) NULL,
	[NumberOfTickets] [int] NULL,
	[DateLastTicket] [datetime] NOT NULL,
	[About] [nvarchar](100) NULL,
 CONSTRAINT [PK_Clients] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Status]    Script Date: 01.05.2022 15:13:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Status](
	[CurrentStatus] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED 
(
	[CurrentStatus] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tickets]    Script Date: 01.05.2022 15:13:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tickets](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DateOfTickets] [datetime] NULL,
	[Naimenovanie] [nvarchar](100) NULL,
	[Deskription] [nvarchar](100) NULL,
	[Status] [nvarchar](100) NULL,
	[ClientID] [int] NOT NULL,
 CONSTRAINT [PK_Tickets] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Clients] ON 

INSERT [dbo].[Clients] ([ID], [Name], [FieldOfActivity], [INN], [NumberOfTickets], [DateLastTicket], [About]) VALUES (1, N'НЛМК', N'Металлургия', N'345789621', 5, CAST(N'2014-05-14T00:00:00.000' AS DateTime), N'завод металлургически')
INSERT [dbo].[Clients] ([ID], [Name], [FieldOfActivity], [INN], [NumberOfTickets], [DateLastTicket], [About]) VALUES (2, N'Промзона', N'Производство', N'480004789', 2, CAST(N'2019-12-12T00:00:00.000' AS DateTime), N'зона на окраине Липецка')
INSERT [dbo].[Clients] ([ID], [Name], [FieldOfActivity], [INN], [NumberOfTickets], [DateLastTicket], [About]) VALUES (3, N'Птицефабрика', N'Мясокомбинат', N'2448786345', 1, CAST(N'2022-04-24T14:40:34.050' AS DateTime), N'фабрика птицеферма')
INSERT [dbo].[Clients] ([ID], [Name], [FieldOfActivity], [INN], [NumberOfTickets], [DateLastTicket], [About]) VALUES (5, N'Макдональдс', N'Общепит', N'478866978', 2, CAST(N'2022-04-24T15:12:39.887' AS DateTime), N'зарубежный общепит')
INSERT [dbo].[Clients] ([ID], [Name], [FieldOfActivity], [INN], [NumberOfTickets], [DateLastTicket], [About]) VALUES (6, N'KFC', N'Ресторан', N'4785123655', 1, CAST(N'2022-04-29T22:34:19.967' AS DateTime), N'зарубежный общественный общепит')
INSERT [dbo].[Clients] ([ID], [Name], [FieldOfActivity], [INN], [NumberOfTickets], [DateLastTicket], [About]) VALUES (7, N'АгроПромБанк', N'Финансы', N'423654783', 1, CAST(N'2022-04-30T21:18:36.027' AS DateTime), N'малый банк')
SET IDENTITY_INSERT [dbo].[Clients] OFF
GO
INSERT [dbo].[Status] ([CurrentStatus]) VALUES (N'В работе')
INSERT [dbo].[Status] ([CurrentStatus]) VALUES (N'Доработать')
INSERT [dbo].[Status] ([CurrentStatus]) VALUES (N'Закрыта')
INSERT [dbo].[Status] ([CurrentStatus]) VALUES (N'Новая')
GO
SET IDENTITY_INSERT [dbo].[Tickets] ON 

INSERT [dbo].[Tickets] ([ID], [DateOfTickets], [Naimenovanie], [Deskription], [Status], [ClientID]) VALUES (1, CAST(N'2022-04-29T22:13:34.727' AS DateTime), N'Поставка', N'2 тонны бетона', N'Доработать', 1)
INSERT [dbo].[Tickets] ([ID], [DateOfTickets], [Naimenovanie], [Deskription], [Status], [ClientID]) VALUES (2, CAST(N'2021-06-15T00:00:00.000' AS DateTime), N'Работа', N'Какая-то', N'В работе', 2)
INSERT [dbo].[Tickets] ([ID], [DateOfTickets], [Naimenovanie], [Deskription], [Status], [ClientID]) VALUES (4, CAST(N'2022-05-01T14:01:49.440' AS DateTime), N'Выплавка чугуна', N'Покраска газона', N'Закрыта', 1)
INSERT [dbo].[Tickets] ([ID], [DateOfTickets], [Naimenovanie], [Deskription], [Status], [ClientID]) VALUES (5, CAST(N'2022-04-24T13:09:30.247' AS DateTime), N'Перенести шкаф', N'В подвал', N'В работе', 1)
INSERT [dbo].[Tickets] ([ID], [DateOfTickets], [Naimenovanie], [Deskription], [Status], [ClientID]) VALUES (7, CAST(N'2022-04-29T22:30:57.703' AS DateTime), N'Имя', N'Фамилия', N'Доработать', 2)
INSERT [dbo].[Tickets] ([ID], [DateOfTickets], [Naimenovanie], [Deskription], [Status], [ClientID]) VALUES (8, CAST(N'2022-04-24T13:17:58.373' AS DateTime), N'Срочно сделать макет', N'Ещё вчера', N'Новая', 1)
INSERT [dbo].[Tickets] ([ID], [DateOfTickets], [Naimenovanie], [Deskription], [Status], [ClientID]) VALUES (9, CAST(N'2022-04-29T22:14:20.693' AS DateTime), N'Доработать форму', N'выплавки стали', N'Новая', 1)
INSERT [dbo].[Tickets] ([ID], [DateOfTickets], [Naimenovanie], [Deskription], [Status], [ClientID]) VALUES (10, CAST(N'2022-04-29T22:15:24.250' AS DateTime), N'Сделать срочно', N'Весь асортимент', N'В работе', 5)
INSERT [dbo].[Tickets] ([ID], [DateOfTickets], [Naimenovanie], [Deskription], [Status], [ClientID]) VALUES (11, CAST(N'2022-04-26T19:19:16.630' AS DateTime), N'Бифштекс', N'Средней прожарки', N'Новая', 5)
INSERT [dbo].[Tickets] ([ID], [DateOfTickets], [Naimenovanie], [Deskription], [Status], [ClientID]) VALUES (12, CAST(N'2022-04-29T22:15:03.313' AS DateTime), N'Сверстать сайт', N'Онлайн-магазин', N'В работе', 3)
INSERT [dbo].[Tickets] ([ID], [DateOfTickets], [Naimenovanie], [Deskription], [Status], [ClientID]) VALUES (13, CAST(N'2022-04-29T22:34:19.967' AS DateTime), N'Поиск персонала', N'Найти уборщицу', N'В работе', 6)
INSERT [dbo].[Tickets] ([ID], [DateOfTickets], [Naimenovanie], [Deskription], [Status], [ClientID]) VALUES (14, CAST(N'2022-04-30T21:18:36.027' AS DateTime), N'Уборка', N'Убрать зал ожидания', N'Новая', 7)
SET IDENTITY_INSERT [dbo].[Tickets] OFF
GO
ALTER TABLE [dbo].[Tickets]  WITH CHECK ADD  CONSTRAINT [FK_Tickets_Clients] FOREIGN KEY([ClientID])
REFERENCES [dbo].[Clients] ([ID])
GO
ALTER TABLE [dbo].[Tickets] CHECK CONSTRAINT [FK_Tickets_Clients]
GO
USE [master]
GO
ALTER DATABASE [Organization] SET  READ_WRITE 
GO
