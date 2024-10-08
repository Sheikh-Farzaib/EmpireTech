USE [master]
GO
/****** Object:  Database [EmpireTech]    Script Date: 2024-10-03 8:02:10 PM ******/
CREATE DATABASE [EmpireTech]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'EmpireTech', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\EmpireTech.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'EmpireTech_log', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\EmpireTech_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [EmpireTech] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [EmpireTech].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [EmpireTech] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [EmpireTech] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [EmpireTech] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [EmpireTech] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [EmpireTech] SET ARITHABORT OFF 
GO
ALTER DATABASE [EmpireTech] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [EmpireTech] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [EmpireTech] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [EmpireTech] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [EmpireTech] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [EmpireTech] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [EmpireTech] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [EmpireTech] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [EmpireTech] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [EmpireTech] SET  ENABLE_BROKER 
GO
ALTER DATABASE [EmpireTech] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [EmpireTech] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [EmpireTech] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [EmpireTech] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [EmpireTech] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [EmpireTech] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [EmpireTech] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [EmpireTech] SET RECOVERY FULL 
GO
ALTER DATABASE [EmpireTech] SET  MULTI_USER 
GO
ALTER DATABASE [EmpireTech] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [EmpireTech] SET DB_CHAINING OFF 
GO
ALTER DATABASE [EmpireTech] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [EmpireTech] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [EmpireTech] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [EmpireTech] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'EmpireTech', N'ON'
GO
ALTER DATABASE [EmpireTech] SET QUERY_STORE = ON
GO
ALTER DATABASE [EmpireTech] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [EmpireTech]
GO
/****** Object:  Table [dbo].[UsersProfile]    Script Date: 2024-10-03 8:02:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsersProfile](
	[Id] [uniqueidentifier] NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[Email] [nvarchar](256) NOT NULL,
	[PasswordHash] [nvarchar](max) NOT NULL,
	[VerificationToken] [nvarchar](128) NULL,
	[IsVerified] [bit] NULL,
	[IsDeleted] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[UsersProfile] ([Id], [UserName], [Email], [PasswordHash], [VerificationToken], [IsVerified], [IsDeleted]) VALUES (N'c2d39a2b-3e93-4b12-a5b0-05aa64756b79', N'JaneDoe', N'jane.doe@example.com', N'hashed_password_2', NULL, 1, 0)
INSERT [dbo].[UsersProfile] ([Id], [UserName], [Email], [PasswordHash], [VerificationToken], [IsVerified], [IsDeleted]) VALUES (N'749ed216-6308-4529-a782-0c5777bc8862', N'Admin', N'sfarzaib@gmail.com', N'dsadasda@', N'f5fa48a7-1b80-46e9-853e-75129a7c05be', 0, 0)
INSERT [dbo].[UsersProfile] ([Id], [UserName], [Email], [PasswordHash], [VerificationToken], [IsVerified], [IsDeleted]) VALUES (N'1dd3ca6d-a603-4594-a147-17f57f30afb8', N'Saad Ullah Khan', N'saadullah.khan@salaamlifetakaful.com', N'Admin@1234', N'c0771c79-62ba-4092-9389-c7a28204d205', 0, 1)
INSERT [dbo].[UsersProfile] ([Id], [UserName], [Email], [PasswordHash], [VerificationToken], [IsVerified], [IsDeleted]) VALUES (N'c476794d-6b75-4987-b9e5-18dc2021ee6c', N'Saad Ullah', N'alice.smith@example.com', N'hashed_password_3', NULL, 1, 0)
INSERT [dbo].[UsersProfile] ([Id], [UserName], [Email], [PasswordHash], [VerificationToken], [IsVerified], [IsDeleted]) VALUES (N'4bc9687d-dbc0-466a-8a17-1f6622aefde2', N'osama', N'osamaghghg@gmail.com', N'Admin@123', N'b94aa0bb-819c-4e95-8772-8ea26825265f', 0, 1)
INSERT [dbo].[UsersProfile] ([Id], [UserName], [Email], [PasswordHash], [VerificationToken], [IsVerified], [IsDeleted]) VALUES (N'8a3f70aa-0337-4c67-b324-25b4c250ca74', N'Admin', N'sfarzaib@gmail.com', N'Admin@123', N'adf353a9-b725-4586-b0bb-938f015784b9', 1, 1)
INSERT [dbo].[UsersProfile] ([Id], [UserName], [Email], [PasswordHash], [VerificationToken], [IsVerified], [IsDeleted]) VALUES (N'24c59b80-45fe-4af6-9d7e-37c72ac266fa', N'JohnDoe', N'john.doe@example.com', N'hashed_password_1', NULL, 0, 1)
INSERT [dbo].[UsersProfile] ([Id], [UserName], [Email], [PasswordHash], [VerificationToken], [IsVerified], [IsDeleted]) VALUES (N'7fde6255-2e44-4e2a-9552-45151bdef8cc', N'Admin1', N'sfarzdaib@gmail.com', N'123456@', NULL, 0, 1)
INSERT [dbo].[UsersProfile] ([Id], [UserName], [Email], [PasswordHash], [VerificationToken], [IsVerified], [IsDeleted]) VALUES (N'ca8208a4-2869-447c-8299-4d1abb9be269', N'BobJones', N'bob.jones@example.com', N'hashed_password_4', NULL, 0, 1)
INSERT [dbo].[UsersProfile] ([Id], [UserName], [Email], [PasswordHash], [VerificationToken], [IsVerified], [IsDeleted]) VALUES (N'be915280-874e-43c6-b66b-60cfc031c8ea', N'saad', N'saad@gmail.com', N'Admin@123', N'20226dc1-6ee3-433e-8cf7-c0cdec4e62e1', 1, 0)
INSERT [dbo].[UsersProfile] ([Id], [UserName], [Email], [PasswordHash], [VerificationToken], [IsVerified], [IsDeleted]) VALUES (N'1fa4d094-7cfe-420e-885d-6fa5b87a83f7', N'Admin', N'sfarzaib@gmail.com', N'1122@33', N'edc3c459-95ff-4db2-9074-c270a5f7af5c', 0, 0)
INSERT [dbo].[UsersProfile] ([Id], [UserName], [Email], [PasswordHash], [VerificationToken], [IsVerified], [IsDeleted]) VALUES (N'8fac2d42-8fee-4755-b4b4-8dab38061609', N'Saad Ullah Khan', N'saadullah.khan@salaamlifetakaful.com', N'Admin@333', N'34c65c66-c775-43f9-994c-f199d9595550', 0, 1)
INSERT [dbo].[UsersProfile] ([Id], [UserName], [Email], [PasswordHash], [VerificationToken], [IsVerified], [IsDeleted]) VALUES (N'f5caefe9-3cad-4764-85b1-9d4b5c01b9a0', N'Admin', N'huzaifaranta@gmail.com', N'Admin@123', N'0f483992-d837-4690-8a76-23effad2086c', 0, 1)
INSERT [dbo].[UsersProfile] ([Id], [UserName], [Email], [PasswordHash], [VerificationToken], [IsVerified], [IsDeleted]) VALUES (N'119e210e-31a3-4d43-acae-ac7af25dfbea', N'Saad Ullah Khan', N'saadullah.khan@salaamlifetakaful.com', N'Admin@333', N'03229fb3-662e-4d46-96cd-7ea2395002e0', 0, 1)
INSERT [dbo].[UsersProfile] ([Id], [UserName], [Email], [PasswordHash], [VerificationToken], [IsVerified], [IsDeleted]) VALUES (N'bdd4958c-5012-410b-b702-c1b43d02b2c1', N'Saad Ullah Khan', N'sfarzaib@gmail.com', N'@123456', N'07dd7b1a-8c93-4c18-8716-04c7ab41f2ae', 0, 1)
INSERT [dbo].[UsersProfile] ([Id], [UserName], [Email], [PasswordHash], [VerificationToken], [IsVerified], [IsDeleted]) VALUES (N'2b983be6-1e5d-42ae-933f-c66d23a29234', N'Farzaib', N'sfarzaib@gmail.com', N'Admin@1234', N'3af60e1c-e746-4e32-95ef-b1ca7c391aef', 0, 0)
INSERT [dbo].[UsersProfile] ([Id], [UserName], [Email], [PasswordHash], [VerificationToken], [IsVerified], [IsDeleted]) VALUES (N'f91bd05f-0890-4c24-9b02-cab4e98b1e19', N'Osama Shaikh', N'osama1343@gmail.com', N'Admin@123', N'b2152f82-48ac-4c84-bc82-07436872b40c', 0, 0)
INSERT [dbo].[UsersProfile] ([Id], [UserName], [Email], [PasswordHash], [VerificationToken], [IsVerified], [IsDeleted]) VALUES (N'fa17bb91-5296-4694-ba6a-cca11b8e9aef', N'Osama', N'osama@gmail.com', N'osama@123', N'9f8fcc32-8ad2-4b8a-b0d5-2680e1abf59c', 0, 1)
INSERT [dbo].[UsersProfile] ([Id], [UserName], [Email], [PasswordHash], [VerificationToken], [IsVerified], [IsDeleted]) VALUES (N'a42024cc-d55b-4183-9e11-e2a1960f641e', N'Saad Ullah Khan', N'saadullah.khan@salaamlifetakaful.com', N'Admin@333', N'0ce5e230-2528-432a-8e8e-4bbb781b8072', 0, 1)
INSERT [dbo].[UsersProfile] ([Id], [UserName], [Email], [PasswordHash], [VerificationToken], [IsVerified], [IsDeleted]) VALUES (N'71369ba4-8f76-4994-830f-ea17d639343f', N'Admin', N'sfarzaib@gmail.com', N'!234566', N'2547b090-d67d-4722-993e-1be360edb99c', 0, 1)
INSERT [dbo].[UsersProfile] ([Id], [UserName], [Email], [PasswordHash], [VerificationToken], [IsVerified], [IsDeleted]) VALUES (N'c6e80b05-88f8-4ad8-a6ac-f068493f7704', N'Admin', N'huzaifaranta@gmail.com', N'Admin@1234', N'2ff7d429-e451-40bc-ba13-ca31cdcb8871', 0, 1)
GO
ALTER TABLE [dbo].[UsersProfile] ADD  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[UsersProfile] ADD  DEFAULT ((0)) FOR [IsVerified]
GO
ALTER TABLE [dbo].[UsersProfile] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
USE [master]
GO
ALTER DATABASE [EmpireTech] SET  READ_WRITE 
GO
