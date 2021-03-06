USE [master]
GO
/****** Object:  Database [PruebaBD]    Script Date: 09/06/2021 02:10:01 a. m. ******/
CREATE DATABASE [PruebaBD]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PruebaTVAzteca', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\PruebaTVAzteca.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PruebaTVAzteca_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\PruebaTVAzteca_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [PruebaBD] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PruebaBD].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PruebaBD] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PruebaBD] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PruebaBD] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PruebaBD] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PruebaBD] SET ARITHABORT OFF 
GO
ALTER DATABASE [PruebaBD] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PruebaBD] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PruebaBD] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PruebaBD] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PruebaBD] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PruebaBD] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PruebaBD] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PruebaBD] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PruebaBD] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PruebaBD] SET  ENABLE_BROKER 
GO
ALTER DATABASE [PruebaBD] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PruebaBD] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PruebaBD] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PruebaBD] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PruebaBD] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PruebaBD] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PruebaBD] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PruebaBD] SET RECOVERY FULL 
GO
ALTER DATABASE [PruebaBD] SET  MULTI_USER 
GO
ALTER DATABASE [PruebaBD] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PruebaBD] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PruebaBD] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PruebaBD] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PruebaBD] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'PruebaBD', N'ON'
GO
ALTER DATABASE [PruebaBD] SET QUERY_STORE = OFF
GO
USE [PruebaBD]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 09/06/2021 02:10:02 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Edad] [int] NOT NULL,
	[Email] [varchar](50) NOT NULL,
 CONSTRAINT [PK__Usuario__3213E83F850EAA6F] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[nuevoUsuario]    Script Date: 09/06/2021 02:10:02 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[nuevoUsuario](
@Nombre as varchar(50),
@Edad as int,
@Email as varchar(50)
)
as
BEGIN
	INSERT INTO Usuario VALUES(@Nombre, @Edad, @Email)
END
GO
USE [master]
GO
ALTER DATABASE [PruebaBD] SET  READ_WRITE 
GO
