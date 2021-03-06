USE [master]
GO
/****** Object:  Database [DASys]    Script Date: 01/10/2018 19:28:09 ******/
CREATE DATABASE [DASys]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DASys', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\DASys.mdf' , SIZE = 5184KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'DASys_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\DASys_log.ldf' , SIZE = 2368KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [DASys] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DASys].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DASys] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DASys] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DASys] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DASys] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DASys] SET ARITHABORT OFF 
GO
ALTER DATABASE [DASys] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DASys] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DASys] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DASys] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DASys] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DASys] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DASys] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DASys] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DASys] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DASys] SET  ENABLE_BROKER 
GO
ALTER DATABASE [DASys] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DASys] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DASys] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DASys] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DASys] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DASys] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DASys] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DASys] SET RECOVERY FULL 
GO
ALTER DATABASE [DASys] SET  MULTI_USER 
GO
ALTER DATABASE [DASys] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DASys] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DASys] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DASys] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'DASys', N'ON'
GO
USE [DASys]
GO
/****** Object:  UserDefinedFunction [dbo].[splitstring]    Script Date: 01/10/2018 19:28:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[splitstring] ( @stringToSplit VARCHAR(MAX) )
RETURNS
@returnList TABLE ([Name] [nvarchar] (500))
AS
BEGIN

 DECLARE @name NVARCHAR(255)
 DECLARE @pos INT

 WHILE CHARINDEX(',', @stringToSplit) > 0
 BEGIN
  SELECT @pos  = CHARINDEX(',', @stringToSplit)  
  SELECT @name = SUBSTRING(@stringToSplit, 1, @pos-1)

  INSERT INTO @returnList 
  SELECT @name

  SELECT @stringToSplit = SUBSTRING(@stringToSplit, @pos+1, LEN(@stringToSplit)-@pos)
 END

 INSERT INTO @returnList
 SELECT @stringToSplit

 RETURN
END
GO
/****** Object:  Table [dbo].[Utb_Area]    Script Date: 01/10/2018 19:28:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Utb_Area](
	[IdArea] [char](6) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[IdEstado] [char](6) NULL,
	[IdEmpresa] [char](6) NULL,
 CONSTRAINT [Pk_IdArea] PRIMARY KEY CLUSTERED 
(
	[IdArea] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Utb_Cargo]    Script Date: 01/10/2018 19:28:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Utb_Cargo](
	[IdCargo] [char](6) NOT NULL,
	[Descripcion] [varchar](50) NULL,
	[IdArea] [char](6) NULL,
	[IdEmpresa] [char](6) NULL,
 CONSTRAINT [Pk_IdCargo] PRIMARY KEY CLUSTERED 
(
	[IdCargo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Utb_Categoria]    Script Date: 01/10/2018 19:28:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Utb_Categoria](
	[IdCategoria] [char](6) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[IdEntidad] [char](6) NULL,
	[IdEstado] [char](6) NOT NULL,
	[IdEmpresa] [char](6) NULL,
 CONSTRAINT [Pk_IdCategoria] PRIMARY KEY CLUSTERED 
(
	[IdCategoria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Utb_CategoriaDocumento]    Script Date: 01/10/2018 19:28:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Utb_CategoriaDocumento](
	[IdCategoria] [char](6) NULL,
	[IdDocumento] [char](6) NULL,
	[IdEntidad] [char](6) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Utb_CategoriaEndidad]    Script Date: 01/10/2018 19:28:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Utb_CategoriaEndidad](
	[IdCategoria] [char](6) NULL,
	[IdEntidad] [char](6) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Utb_Cliente]    Script Date: 01/10/2018 19:28:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Utb_Cliente](
	[IdPersona] [char](15) NOT NULL,
	[Nombre] [varchar](50) NULL,
 CONSTRAINT [Pk_IdCliente] PRIMARY KEY CLUSTERED 
(
	[IdPersona] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Utb_Colaborador]    Script Date: 01/10/2018 19:28:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Utb_Colaborador](
	[IdColaborador] [char](15) NOT NULL,
	[NumeroDocumento] [varchar](15) NULL,
	[Nombres] [varchar](100) NULL,
	[ApellidoPaterno] [varchar](100) NULL,
	[ApellidoMaterno] [varchar](100) NULL,
	[Alias] [varchar](50) NULL,
	[Direccion] [varchar](50) NULL,
	[FechaNacimiento] [datetime] NULL,
	[NumeroContrato] [varchar](50) NULL,
	[IdPersona] [char](15) NOT NULL,
	[IdEmpresa] [char](6) NULL,
	[Genero] [char](1) NULL,
	[IdCategoria] [char](6) NULL,
	[IdCargo] [char](6) NULL,
	[IdRegimen] [char](6) NULL,
	[IdCondicion] [char](6) NULL,
	[IdEntidad] [char](6) NULL,
	[IdEstado] [char](6) NULL,
	[IdDepartamento] [char](6) NULL,
	[IdUbicacion] [char](6) NULL,
	[IdDitrito] [char](6) NULL,
	[IdProvincia] [char](2) NULL,
	[IdUDepartamento] [char](2) NULL,
	[IdDistrito] [char](2) NULL,
	[IdUsuario] [char](6) NULL,
	[Enviado] [int] NULL,
 CONSTRAINT [PK_Colaboradores] PRIMARY KEY CLUSTERED 
(
	[IdColaborador] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Utb_ColaboradorTipoLugar]    Script Date: 01/10/2018 19:28:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Utb_ColaboradorTipoLugar](
	[IdTipoLugar] [char](6) NOT NULL,
	[IdColaborador] [char](15) NULL,
	[descripcion] [varchar](300) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Utb_Condicion]    Script Date: 01/10/2018 19:28:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Utb_Condicion](
	[IdCondicion] [char](6) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[IdRegimen] [char](6) NULL,
	[IdEstado] [char](6) NULL,
	[IdEmpresa] [char](6) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdCondicion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Utb_Configuracion]    Script Date: 01/10/2018 19:28:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Utb_Configuracion](
	[IdConfiguracion] [char](6) NOT NULL,
	[Nombre] [char](50) NULL,
	[TiempoColor] [tinyint] NULL,
	[TiempoEntreMarcaciones] [char](6) NULL,
	[TiempoRELAY] [tinyint] NULL,
	[IdTipo] [char](6) NOT NULL,
	[IdEstado] [char](6) NOT NULL,
	[IdEmpresa] [char](6) NULL,
 CONSTRAINT [Pk_IdConfiguracion] PRIMARY KEY CLUSTERED 
(
	[IdConfiguracion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Utb_Contacto]    Script Date: 01/10/2018 19:28:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Utb_Contacto](
	[IdPersona] [char](15) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[Cargo] [varchar](50) NULL,
 CONSTRAINT [Pk_IdContacto] PRIMARY KEY CLUSTERED 
(
	[IdPersona] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Utb_Departamento]    Script Date: 01/10/2018 19:28:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Utb_Departamento](
	[IdDepartamento] [char](6) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[Nivel] [int] NULL,
	[IdPrincipal] [char](6) NULL,
	[IdEstado] [char](6) NULL,
	[IdEmpresa] [char](6) NULL,
 CONSTRAINT [Pk_IdDepartamento] PRIMARY KEY CLUSTERED 
(
	[IdDepartamento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Utb_Digitalizacion]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Utb_Digitalizacion](
	[IdDigitalizacion] [char](15) NOT NULL,
	[IdDocumento] [char](6) NULL,
	[Observacion] [varchar](255) NULL,
	[FechaVencimiento] [datetime] NULL,
	[IdEstado] [char](6) NULL,
	[IdPersona] [char](16) NULL,
	[Enviado] [int] NULL,
	[IdUsuario] [char](6) NULL,
 CONSTRAINT [PK__Digitalizacion] PRIMARY KEY CLUSTERED 
(
	[IdDigitalizacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Utb_Documento]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Utb_Documento](
	[IdDocumento] [char](6) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Descripcion] [varchar](250) NULL,
	[FechaVencimiento] [bit] NULL,
	[Obligatorio] [bit] NULL,
	[IdEstado] [char](6) NOT NULL,
	[IdEntidad] [char](6) NULL,
	[IdEmpresa] [char](6) NULL,
 CONSTRAINT [Pk_IdDocumento] PRIMARY KEY CLUSTERED 
(
	[IdDocumento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Utb_DocumentoAdjunto]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Utb_DocumentoAdjunto](
	[IdDocumentoAdjunto] [char](15) NOT NULL,
	[IdDigitalizacion] [char](15) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[Ruta] [nvarchar](max) NULL,
	[IdPersona] [char](15) NULL,
	[Fecha] [datetime] NULL,
	[IdEstado] [char](6) NULL,
	[IdDocumento] [char](6) NULL,
	[Observacion] [varchar](500) NULL,
 CONSTRAINT [Pk_IdDocumentoAdjunto] PRIMARY KEY CLUSTERED 
(
	[IdDocumentoAdjunto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Utb_Email]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Utb_Email](
	[IdEmail] [char](6) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Descripcion] [varchar](250) NULL,
 CONSTRAINT [Pk_IdEmail] PRIMARY KEY CLUSTERED 
(
	[IdEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Utb_Empresa]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Utb_Empresa](
	[IdEmpresa] [char](6) NOT NULL,
	[IdPersona] [char](15) NOT NULL,
	[RazonSocial] [varchar](100) NULL,
	[RUC] [char](11) NULL,
	[DireccionFiscal] [varchar](250) NULL,
	[IdPadre] [char](6) NULL,
	[EsPrincipal] [bit] NULL,
	[EsContratista] [bit] NULL,
	[EsSubContratista] [bit] NULL,
	[IdEntidad] [char](6) NULL,
	[IdEstado] [char](6) NULL,
	[IdCategoria] [char](6) NULL,
	[IdUsuario] [char](6) NULL,
	[Enviado] [int] NULL,
 CONSTRAINT [Pk_IdEmpresa] PRIMARY KEY CLUSTERED 
(
	[IdEmpresa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Utb_Entidad]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Utb_Entidad](
	[IdEntidad] [char](6) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[IdPrincipal] [char](6) NULL,
	[IdEstado] [char](6) NULL,
 CONSTRAINT [Pk_IdEntidad] PRIMARY KEY CLUSTERED 
(
	[IdEntidad] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Utb_Estado]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Utb_Estado](
	[IdEstado] [char](6) NOT NULL,
	[Nombre] [varchar](25) NOT NULL,
	[Descripcion] [varchar](50) NULL,
 CONSTRAINT [Pk_IdEstado] PRIMARY KEY CLUSTERED 
(
	[IdEstado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Utb_Marca]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Utb_Marca](
	[IdMarca] [char](6) NOT NULL,
	[Nombre] [char](50) NULL,
	[IdEstado] [char](6) NOT NULL,
	[IdEmpresa] [char](6) NULL,
 CONSTRAINT [Pk_IdMarca] PRIMARY KEY CLUSTERED 
(
	[IdMarca] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Utb_Marcacion]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Utb_Marcacion](
	[IdPersona] [char](15) NOT NULL,
	[FechaOriginal] [datetime] NOT NULL,
	[Fecha] [datetime] NULL,
	[IdTerminal] [char](6) NULL,
	[IdTipo] [char](6) NOT NULL,
	[IdEstado] [char](6) NOT NULL,
 CONSTRAINT [Pk_IdMarcacion] PRIMARY KEY CLUSTERED 
(
	[IdPersona] ASC,
	[FechaOriginal] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Utb_Modelo]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Utb_Modelo](
	[IdModelo] [char](6) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[SDK] [int] NULL,
	[IdMarca] [char](6) NOT NULL,
	[IdConfiguracion] [char](6) NULL,
	[IdEstado] [char](6) NOT NULL,
	[IdEmpresa] [char](6) NULL,
 CONSTRAINT [Pk_IdModelo] PRIMARY KEY CLUSTERED 
(
	[IdModelo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Utb_Modulo]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Utb_Modulo](
	[IdModulo] [char](6) NOT NULL,
	[Nombre] [varchar](700) NULL,
	[Descripcion] [varchar](200) NULL,
	[IdEstado] [char](6) NULL,
	[Posicion] [int] NULL,
	[Icono] [varchar](60) NULL,
 CONSTRAINT [PK_Modulo] PRIMARY KEY CLUSTERED 
(
	[IdModulo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Utb_Opcion]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Utb_Opcion](
	[IdOpcion] [char](6) NOT NULL,
	[IdModulo] [char](6) NOT NULL,
	[Nombre] [varchar](70) NULL,
	[Descripcion] [varchar](300) NULL,
	[Url] [varchar](100) NULL,
	[Controlador] [varchar](50) NULL,
	[Accion] [varchar](80) NULL,
	[Area] [varchar](50) NULL,
	[IdEstado] [char](6) NULL,
	[EsInicio] [bit] NULL,
	[Posicion] [int] NULL,
	[IdPadre] [char](6) NULL,
	[Icono] [varchar](60) NULL,
	[EsSubmenu] [bit] NULL,
 CONSTRAINT [PK_Opcion] PRIMARY KEY CLUSTERED 
(
	[IdOpcion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Utb_Operador]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Utb_Operador](
	[IdOperador] [char](6) NOT NULL,
	[Nombre] [varchar](25) NOT NULL,
	[Descripcion] [varchar](50) NULL,
 CONSTRAINT [Pk_IdOperador] PRIMARY KEY CLUSTERED 
(
	[IdOperador] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Utb_ParametrosEnvioCorreo]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Utb_ParametrosEnvioCorreo](
	[IdParametros] [char](6) NOT NULL,
	[Host] [varchar](200) NULL,
	[Port] [int] NULL,
	[Correo] [nvarchar](200) NULL,
	[Password] [nvarchar](200) NULL,
	[IdEmpresa] [char](6) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdParametros] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Utb_Perfil]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Utb_Perfil](
	[IdPerfil] [char](6) NOT NULL,
	[Nombre] [varchar](70) NULL,
	[Descripcion] [varchar](500) NULL,
	[IdEstado] [char](6) NULL,
 CONSTRAINT [PK_Perfil] PRIMARY KEY CLUSTERED 
(
	[IdPerfil] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Utb_PerfilModulo]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Utb_PerfilModulo](
	[IdModulo] [char](6) NOT NULL,
	[IdPerfil] [char](6) NOT NULL,
	[IdEstado] [char](6) NULL,
 CONSTRAINT [PK_UsuarioPerfil] PRIMARY KEY CLUSTERED 
(
	[IdModulo] ASC,
	[IdPerfil] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Utb_PerfilOpcion]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Utb_PerfilOpcion](
	[IdUsuario] [char](6) NOT NULL,
	[IdPerfil] [char](6) NOT NULL,
	[IdOpcion] [char](6) NOT NULL,
 CONSTRAINT [PK_PerfilOpcion] PRIMARY KEY CLUSTERED 
(
	[IdPerfil] ASC,
	[IdOpcion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Utb_Persona]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Utb_Persona](
	[IdPersona] [char](15) NOT NULL,
	[Observacion] [varchar](250) NULL,
	[IdPrincipal] [char](15) NULL,
	[IdTipo] [char](6) NULL,
	[IdEntidad] [char](6) NULL,
	[IdEstado] [char](6) NULL,
 CONSTRAINT [Pk_IdPersona] PRIMARY KEY CLUSTERED 
(
	[IdPersona] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Utb_PersonaAltaBaja]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Utb_PersonaAltaBaja](
	[IdPersonaAltaBaja] [char](14) NOT NULL,
	[IdPersona] [char](15) NOT NULL,
	[Fecha] [datetime] NULL,
 CONSTRAINT [Pk_IdPersonaAltaBaja] PRIMARY KEY CLUSTERED 
(
	[IdPersonaAltaBaja] ASC,
	[IdPersona] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Utb_PersonaArea]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Utb_PersonaArea](
	[IdPersona] [char](15) NOT NULL,
	[IdArea] [char](6) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Utb_PersonaDocumento]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Utb_PersonaDocumento](
	[IdPersona] [char](15) NOT NULL,
	[IdDocumento] [char](6) NOT NULL,
	[Documento] [varchar](15) NOT NULL,
	[IdEstado] [char](6) NULL,
 CONSTRAINT [Pk_IdPersonaDocumento] PRIMARY KEY CLUSTERED 
(
	[IdPersona] ASC,
	[IdDocumento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Utb_PersonaEmail]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Utb_PersonaEmail](
	[IdPersona] [char](15) NOT NULL,
	[IdEmail] [char](6) NOT NULL,
	[Cuenta] [varchar](50) NOT NULL,
	[IdEstado] [char](6) NULL,
 CONSTRAINT [Pk_IdPersonaEmail] PRIMARY KEY CLUSTERED 
(
	[IdPersona] ASC,
	[IdEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Utb_PersonaImagen]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Utb_PersonaImagen](
	[IdPersonaImagen] [char](14) NOT NULL,
	[IdPersona] [char](15) NOT NULL,
	[Imagen] [image] NULL,
	[Ruta] [varchar](250) NULL,
 CONSTRAINT [Pk_IdPersonaImagen] PRIMARY KEY CLUSTERED 
(
	[IdPersonaImagen] ASC,
	[IdPersona] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Utb_PersonaOperador]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Utb_PersonaOperador](
	[IdPersona] [char](15) NOT NULL,
	[IdOperador] [char](6) NOT NULL,
	[Numero] [varchar](15) NOT NULL,
	[IdEstado] [char](6) NULL,
 CONSTRAINT [Pk_IdPersonaOperador] PRIMARY KEY CLUSTERED 
(
	[IdPersona] ASC,
	[IdOperador] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Utb_PersonaPeriodo]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Utb_PersonaPeriodo](
	[IdPersonaPeriodo] [char](14) NOT NULL,
	[IdPersona] [char](15) NOT NULL,
	[FechaInicio] [datetime] NULL,
	[FechaFin] [datetime] NULL,
	[IdEstado] [char](6) NULL,
 CONSTRAINT [Pk_IdPersonaPeriodo] PRIMARY KEY CLUSTERED 
(
	[IdPersonaPeriodo] ASC,
	[IdPersona] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Utb_PersonaServicio]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Utb_PersonaServicio](
	[IdPersona] [char](15) NOT NULL,
	[IdServicio] [char](6) NOT NULL,
	[FechaInicial] [char](8) NULL,
	[FechaFinal] [char](8) NULL,
 CONSTRAINT [Pk_IdPersonaServicio] PRIMARY KEY CLUSTERED 
(
	[IdPersona] ASC,
	[IdServicio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Utb_PersonaTerminal]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Utb_PersonaTerminal](
	[IdPersonaTerminal] [char](14) NOT NULL,
	[IdPersona] [char](15) NOT NULL,
	[Alias] [varchar](24) NULL,
	[Password] [varchar](8) NULL,
	[Tarjeta] [varchar](10) NULL,
	[Privilegio] [tinyint] NULL,
 CONSTRAINT [Pk_IdPersonaTerminal] PRIMARY KEY CLUSTERED 
(
	[IdPersonaTerminal] ASC,
	[IdPersona] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Utb_Regimen]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Utb_Regimen](
	[IdRegimen] [char](6) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[IdEstado] [char](6) NULL,
	[IdEmpresa] [char](6) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdRegimen] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Utb_TarjetProximidad]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Utb_TarjetProximidad](
	[IdTarjetProximidad] [char](6) NOT NULL,
	[Descripcion] [varchar](200) NULL,
	[NumeroTrajeta] [nvarchar](15) NULL,
	[IdPersona] [char](15) NULL,
	[IdEstado] [char](6) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdTarjetProximidad] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Utb_Terminal]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Utb_Terminal](
	[IdTerminal] [char](6) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[IP] [varchar](15) NULL,
	[Puerto] [varchar](5) NULL,
	[Foto] [image] NULL,
	[IdModelo] [char](6) NOT NULL,
	[IdMarca] [char](6) NOT NULL,
	[IdConfiguracion] [char](6) NOT NULL,
	[IdArea] [char](6) NOT NULL,
	[IdEstado] [char](6) NOT NULL,
	[IdEmpresa] [char](6) NULL,
 CONSTRAINT [Pk_IdTerminal] PRIMARY KEY CLUSTERED 
(
	[IdTerminal] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Utb_Tipo]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Utb_Tipo](
	[IdTipo] [char](6) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[IdPrincipal] [char](6) NULL,
	[IdEstado] [char](6) NULL,
 CONSTRAINT [Pk_IdTipo] PRIMARY KEY CLUSTERED 
(
	[IdTipo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Utb_TipoLugar]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Utb_TipoLugar](
	[IdTipoLugar] [char](6) NOT NULL,
	[Nombre] [varchar](300) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdTipoLugar] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Utb_Ubicacion]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Utb_Ubicacion](
	[IdUbicacion] [char](6) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[Nivel] [int] NULL,
	[IdPrincipal] [char](6) NULL,
	[IdDepartamento] [char](6) NULL,
	[IdEmpresa] [char](6) NULL,
 CONSTRAINT [Pk_IdUbicacion] PRIMARY KEY CLUSTERED 
(
	[IdUbicacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Utb_Ubigeo]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Utb_Ubigeo](
	[IdDepartamento] [char](2) NOT NULL,
	[IdProvincia] [char](2) NOT NULL,
	[IdDistrito] [char](2) NOT NULL,
	[Nombre] [varchar](200) NULL,
	[IdUbigeo] [char](6) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Utb_Usuario]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Utb_Usuario](
	[IdUsuario] [char](6) NOT NULL,
	[IdPersona] [char](15) NOT NULL,
	[Usuario] [nvarchar](100) NOT NULL,
	[Correo] [nvarchar](100) NOT NULL,
	[Foto] [varbinary](max) NULL,
	[Password] [nvarchar](100) NOT NULL,
	[IdEstado] [char](6) NOT NULL,
	[IdPerfil] [char](6) NULL,
	[IdEmpresa] [char](6) NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Utb_UsuarioOpcion]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Utb_UsuarioOpcion](
	[IdUsuario] [char](6) NOT NULL,
	[IdOpcion] [char](6) NOT NULL,
	[IdEstado] [char](6) NULL,
	[Ejecutar] [bit] NULL,
	[Consultar] [bit] NULL,
	[Registrar] [bit] NULL,
	[Actualizar] [bit] NULL,
	[Eliminar] [bit] NULL,
	[Imprimir] [bit] NULL,
 CONSTRAINT [PK_UsuarioOpcion] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC,
	[IdOpcion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Utb_Configuracion] ADD  DEFAULT ((1)) FOR [TiempoColor]
GO
ALTER TABLE [dbo].[Utb_Configuracion] ADD  DEFAULT ('000100') FOR [TiempoEntreMarcaciones]
GO
ALTER TABLE [dbo].[Utb_Configuracion] ADD  DEFAULT ((1)) FOR [TiempoRELAY]
GO
ALTER TABLE [dbo].[Utb_PersonaTerminal] ADD  DEFAULT ((0)) FOR [Privilegio]
GO
ALTER TABLE [dbo].[Utb_Cargo]  WITH CHECK ADD  CONSTRAINT [Fk_IdArea_Cargo] FOREIGN KEY([IdArea])
REFERENCES [dbo].[Utb_Area] ([IdArea])
GO
ALTER TABLE [dbo].[Utb_Cargo] CHECK CONSTRAINT [Fk_IdArea_Cargo]
GO
ALTER TABLE [dbo].[Utb_CategoriaDocumento]  WITH CHECK ADD FOREIGN KEY([IdCategoria])
REFERENCES [dbo].[Utb_Categoria] ([IdCategoria])
GO
ALTER TABLE [dbo].[Utb_CategoriaDocumento]  WITH CHECK ADD FOREIGN KEY([IdDocumento])
REFERENCES [dbo].[Utb_Documento] ([IdDocumento])
GO
ALTER TABLE [dbo].[Utb_CategoriaEndidad]  WITH CHECK ADD FOREIGN KEY([IdCategoria])
REFERENCES [dbo].[Utb_Categoria] ([IdCategoria])
GO
ALTER TABLE [dbo].[Utb_CategoriaEndidad]  WITH CHECK ADD FOREIGN KEY([IdEntidad])
REFERENCES [dbo].[Utb_Entidad] ([IdEntidad])
GO
ALTER TABLE [dbo].[Utb_Cliente]  WITH CHECK ADD FOREIGN KEY([IdPersona])
REFERENCES [dbo].[Utb_Persona] ([IdPersona])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Utb_Colaborador]  WITH CHECK ADD FOREIGN KEY([IdCargo])
REFERENCES [dbo].[Utb_Cargo] ([IdCargo])
GO
ALTER TABLE [dbo].[Utb_Colaborador]  WITH CHECK ADD FOREIGN KEY([IdCargo])
REFERENCES [dbo].[Utb_Cargo] ([IdCargo])
GO
ALTER TABLE [dbo].[Utb_Colaborador]  WITH CHECK ADD FOREIGN KEY([IdCategoria])
REFERENCES [dbo].[Utb_Categoria] ([IdCategoria])
GO
ALTER TABLE [dbo].[Utb_Colaborador]  WITH CHECK ADD FOREIGN KEY([IdCategoria])
REFERENCES [dbo].[Utb_Categoria] ([IdCategoria])
GO
ALTER TABLE [dbo].[Utb_Colaborador]  WITH CHECK ADD FOREIGN KEY([IdEntidad])
REFERENCES [dbo].[Utb_Entidad] ([IdEntidad])
GO
ALTER TABLE [dbo].[Utb_Colaborador]  WITH CHECK ADD FOREIGN KEY([IdEntidad])
REFERENCES [dbo].[Utb_Entidad] ([IdEntidad])
GO
ALTER TABLE [dbo].[Utb_Colaborador]  WITH CHECK ADD FOREIGN KEY([IdEstado])
REFERENCES [dbo].[Utb_Estado] ([IdEstado])
GO
ALTER TABLE [dbo].[Utb_Colaborador]  WITH CHECK ADD FOREIGN KEY([IdEstado])
REFERENCES [dbo].[Utb_Estado] ([IdEstado])
GO
ALTER TABLE [dbo].[Utb_Colaborador]  WITH CHECK ADD FOREIGN KEY([IdPersona])
REFERENCES [dbo].[Utb_Persona] ([IdPersona])
GO
ALTER TABLE [dbo].[Utb_Colaborador]  WITH CHECK ADD FOREIGN KEY([IdPersona])
REFERENCES [dbo].[Utb_Persona] ([IdPersona])
GO
ALTER TABLE [dbo].[Utb_Condicion]  WITH CHECK ADD  CONSTRAINT [Fk_IdEstado_Condicion] FOREIGN KEY([IdEstado])
REFERENCES [dbo].[Utb_Estado] ([IdEstado])
GO
ALTER TABLE [dbo].[Utb_Condicion] CHECK CONSTRAINT [Fk_IdEstado_Condicion]
GO
ALTER TABLE [dbo].[Utb_Condicion]  WITH CHECK ADD  CONSTRAINT [Fk_IdRegimen_Terminal] FOREIGN KEY([IdRegimen])
REFERENCES [dbo].[Utb_Regimen] ([IdRegimen])
GO
ALTER TABLE [dbo].[Utb_Condicion] CHECK CONSTRAINT [Fk_IdRegimen_Terminal]
GO
ALTER TABLE [dbo].[Utb_Configuracion]  WITH CHECK ADD  CONSTRAINT [Fk_IdEstado_Configuracion] FOREIGN KEY([IdEstado])
REFERENCES [dbo].[Utb_Estado] ([IdEstado])
GO
ALTER TABLE [dbo].[Utb_Configuracion] CHECK CONSTRAINT [Fk_IdEstado_Configuracion]
GO
ALTER TABLE [dbo].[Utb_Configuracion]  WITH CHECK ADD  CONSTRAINT [Fk_IdTipo_Configuracion] FOREIGN KEY([IdTipo])
REFERENCES [dbo].[Utb_Tipo] ([IdTipo])
GO
ALTER TABLE [dbo].[Utb_Configuracion] CHECK CONSTRAINT [Fk_IdTipo_Configuracion]
GO
ALTER TABLE [dbo].[Utb_Contacto]  WITH CHECK ADD FOREIGN KEY([IdPersona])
REFERENCES [dbo].[Utb_Persona] ([IdPersona])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Utb_Digitalizacion]  WITH CHECK ADD  CONSTRAINT [FK_IdDocumento_Digitalizacion] FOREIGN KEY([IdDocumento])
REFERENCES [dbo].[Utb_Documento] ([IdDocumento])
GO
ALTER TABLE [dbo].[Utb_Digitalizacion] CHECK CONSTRAINT [FK_IdDocumento_Digitalizacion]
GO
ALTER TABLE [dbo].[Utb_Digitalizacion]  WITH CHECK ADD  CONSTRAINT [FK_IdDocumento_Estado] FOREIGN KEY([IdEstado])
REFERENCES [dbo].[Utb_Estado] ([IdEstado])
GO
ALTER TABLE [dbo].[Utb_Digitalizacion] CHECK CONSTRAINT [FK_IdDocumento_Estado]
GO
ALTER TABLE [dbo].[Utb_Entidad]  WITH CHECK ADD  CONSTRAINT [Fk_IdEstado_Entidad] FOREIGN KEY([IdEstado])
REFERENCES [dbo].[Utb_Estado] ([IdEstado])
GO
ALTER TABLE [dbo].[Utb_Entidad] CHECK CONSTRAINT [Fk_IdEstado_Entidad]
GO
ALTER TABLE [dbo].[Utb_Marca]  WITH CHECK ADD  CONSTRAINT [Fk_IdEstado_Marca] FOREIGN KEY([IdEstado])
REFERENCES [dbo].[Utb_Estado] ([IdEstado])
GO
ALTER TABLE [dbo].[Utb_Marca] CHECK CONSTRAINT [Fk_IdEstado_Marca]
GO
ALTER TABLE [dbo].[Utb_Marcacion]  WITH CHECK ADD  CONSTRAINT [Fk_IdEstado_Marcacion] FOREIGN KEY([IdEstado])
REFERENCES [dbo].[Utb_Estado] ([IdEstado])
GO
ALTER TABLE [dbo].[Utb_Marcacion] CHECK CONSTRAINT [Fk_IdEstado_Marcacion]
GO
ALTER TABLE [dbo].[Utb_Marcacion]  WITH CHECK ADD  CONSTRAINT [Fk_IdPersona_Marcacion] FOREIGN KEY([IdPersona])
REFERENCES [dbo].[Utb_Persona] ([IdPersona])
GO
ALTER TABLE [dbo].[Utb_Marcacion] CHECK CONSTRAINT [Fk_IdPersona_Marcacion]
GO
ALTER TABLE [dbo].[Utb_Marcacion]  WITH CHECK ADD  CONSTRAINT [Fk_IdTerminal_Marcacion] FOREIGN KEY([IdTerminal])
REFERENCES [dbo].[Utb_Terminal] ([IdTerminal])
GO
ALTER TABLE [dbo].[Utb_Marcacion] CHECK CONSTRAINT [Fk_IdTerminal_Marcacion]
GO
ALTER TABLE [dbo].[Utb_Marcacion]  WITH CHECK ADD  CONSTRAINT [Fk_IdTipo_Marcacion] FOREIGN KEY([IdTipo])
REFERENCES [dbo].[Utb_Tipo] ([IdTipo])
GO
ALTER TABLE [dbo].[Utb_Marcacion] CHECK CONSTRAINT [Fk_IdTipo_Marcacion]
GO
ALTER TABLE [dbo].[Utb_Modelo]  WITH CHECK ADD  CONSTRAINT [Fk_IdConfiguracion_Modelo] FOREIGN KEY([IdConfiguracion])
REFERENCES [dbo].[Utb_Configuracion] ([IdConfiguracion])
GO
ALTER TABLE [dbo].[Utb_Modelo] CHECK CONSTRAINT [Fk_IdConfiguracion_Modelo]
GO
ALTER TABLE [dbo].[Utb_Modelo]  WITH CHECK ADD  CONSTRAINT [Fk_IdEstado_Modelo] FOREIGN KEY([IdEstado])
REFERENCES [dbo].[Utb_Estado] ([IdEstado])
GO
ALTER TABLE [dbo].[Utb_Modelo] CHECK CONSTRAINT [Fk_IdEstado_Modelo]
GO
ALTER TABLE [dbo].[Utb_Modelo]  WITH CHECK ADD  CONSTRAINT [Fk_IdMarca_Modelo] FOREIGN KEY([IdMarca])
REFERENCES [dbo].[Utb_Marca] ([IdMarca])
GO
ALTER TABLE [dbo].[Utb_Modelo] CHECK CONSTRAINT [Fk_IdMarca_Modelo]
GO
ALTER TABLE [dbo].[Utb_Modulo]  WITH CHECK ADD  CONSTRAINT [Fk_IdEstado_Modulo] FOREIGN KEY([IdEstado])
REFERENCES [dbo].[Utb_Estado] ([IdEstado])
GO
ALTER TABLE [dbo].[Utb_Modulo] CHECK CONSTRAINT [Fk_IdEstado_Modulo]
GO
ALTER TABLE [dbo].[Utb_Opcion]  WITH CHECK ADD  CONSTRAINT [Fk_IdEstado_Opcion] FOREIGN KEY([IdEstado])
REFERENCES [dbo].[Utb_Estado] ([IdEstado])
GO
ALTER TABLE [dbo].[Utb_Opcion] CHECK CONSTRAINT [Fk_IdEstado_Opcion]
GO
ALTER TABLE [dbo].[Utb_Opcion]  WITH CHECK ADD  CONSTRAINT [FK_Opcion_Modulo] FOREIGN KEY([IdModulo])
REFERENCES [dbo].[Utb_Modulo] ([IdModulo])
GO
ALTER TABLE [dbo].[Utb_Opcion] CHECK CONSTRAINT [FK_Opcion_Modulo]
GO
ALTER TABLE [dbo].[Utb_Perfil]  WITH CHECK ADD  CONSTRAINT [Fk_IdEstado_Perfil] FOREIGN KEY([IdEstado])
REFERENCES [dbo].[Utb_Estado] ([IdEstado])
GO
ALTER TABLE [dbo].[Utb_Perfil] CHECK CONSTRAINT [Fk_IdEstado_Perfil]
GO
ALTER TABLE [dbo].[Utb_Persona]  WITH CHECK ADD  CONSTRAINT [Fk_IdEntidad_Persona] FOREIGN KEY([IdEntidad])
REFERENCES [dbo].[Utb_Entidad] ([IdEntidad])
GO
ALTER TABLE [dbo].[Utb_Persona] CHECK CONSTRAINT [Fk_IdEntidad_Persona]
GO
ALTER TABLE [dbo].[Utb_Persona]  WITH CHECK ADD  CONSTRAINT [Fk_IdEstado_Persona] FOREIGN KEY([IdEstado])
REFERENCES [dbo].[Utb_Estado] ([IdEstado])
GO
ALTER TABLE [dbo].[Utb_Persona] CHECK CONSTRAINT [Fk_IdEstado_Persona]
GO
ALTER TABLE [dbo].[Utb_Persona]  WITH CHECK ADD  CONSTRAINT [Fk_IdTipo_Persona] FOREIGN KEY([IdTipo])
REFERENCES [dbo].[Utb_Tipo] ([IdTipo])
GO
ALTER TABLE [dbo].[Utb_Persona] CHECK CONSTRAINT [Fk_IdTipo_Persona]
GO
ALTER TABLE [dbo].[Utb_PersonaAltaBaja]  WITH CHECK ADD  CONSTRAINT [Fk_IdPersona_FechaAltaBaja] FOREIGN KEY([IdPersona])
REFERENCES [dbo].[Utb_Persona] ([IdPersona])
GO
ALTER TABLE [dbo].[Utb_PersonaAltaBaja] CHECK CONSTRAINT [Fk_IdPersona_FechaAltaBaja]
GO
ALTER TABLE [dbo].[Utb_PersonaDocumento]  WITH CHECK ADD  CONSTRAINT [Fk_IdDocumento_PersonaDocumento] FOREIGN KEY([IdDocumento])
REFERENCES [dbo].[Utb_Documento] ([IdDocumento])
GO
ALTER TABLE [dbo].[Utb_PersonaDocumento] CHECK CONSTRAINT [Fk_IdDocumento_PersonaDocumento]
GO
ALTER TABLE [dbo].[Utb_PersonaDocumento]  WITH CHECK ADD  CONSTRAINT [Fk_IdEstado_PersonaDocumentoIdentidad] FOREIGN KEY([IdEstado])
REFERENCES [dbo].[Utb_Estado] ([IdEstado])
GO
ALTER TABLE [dbo].[Utb_PersonaDocumento] CHECK CONSTRAINT [Fk_IdEstado_PersonaDocumentoIdentidad]
GO
ALTER TABLE [dbo].[Utb_PersonaDocumento]  WITH CHECK ADD  CONSTRAINT [Fk_IdPersona_PersonaDocumentoIdentidad] FOREIGN KEY([IdPersona])
REFERENCES [dbo].[Utb_Persona] ([IdPersona])
GO
ALTER TABLE [dbo].[Utb_PersonaDocumento] CHECK CONSTRAINT [Fk_IdPersona_PersonaDocumentoIdentidad]
GO
ALTER TABLE [dbo].[Utb_PersonaImagen]  WITH CHECK ADD  CONSTRAINT [Fk_IdPersona_PersonaImagen] FOREIGN KEY([IdPersona])
REFERENCES [dbo].[Utb_Persona] ([IdPersona])
GO
ALTER TABLE [dbo].[Utb_PersonaImagen] CHECK CONSTRAINT [Fk_IdPersona_PersonaImagen]
GO
ALTER TABLE [dbo].[Utb_PersonaOperador]  WITH CHECK ADD  CONSTRAINT [Fk_IdEstado_PersonaOperadorTelefonico] FOREIGN KEY([IdEstado])
REFERENCES [dbo].[Utb_Estado] ([IdEstado])
GO
ALTER TABLE [dbo].[Utb_PersonaOperador] CHECK CONSTRAINT [Fk_IdEstado_PersonaOperadorTelefonico]
GO
ALTER TABLE [dbo].[Utb_PersonaOperador]  WITH CHECK ADD  CONSTRAINT [Fk_IdOperador_PersonaOperadorTelefonico] FOREIGN KEY([IdOperador])
REFERENCES [dbo].[Utb_Operador] ([IdOperador])
GO
ALTER TABLE [dbo].[Utb_PersonaOperador] CHECK CONSTRAINT [Fk_IdOperador_PersonaOperadorTelefonico]
GO
ALTER TABLE [dbo].[Utb_PersonaOperador]  WITH CHECK ADD  CONSTRAINT [Fk_IdPersona_PersonaOperadorTelefonico] FOREIGN KEY([IdPersona])
REFERENCES [dbo].[Utb_Persona] ([IdPersona])
GO
ALTER TABLE [dbo].[Utb_PersonaOperador] CHECK CONSTRAINT [Fk_IdPersona_PersonaOperadorTelefonico]
GO
ALTER TABLE [dbo].[Utb_PersonaTerminal]  WITH CHECK ADD  CONSTRAINT [Fk_IdPersona_PersonaTerminal] FOREIGN KEY([IdPersona])
REFERENCES [dbo].[Utb_Persona] ([IdPersona])
GO
ALTER TABLE [dbo].[Utb_PersonaTerminal] CHECK CONSTRAINT [Fk_IdPersona_PersonaTerminal]
GO
ALTER TABLE [dbo].[Utb_Regimen]  WITH CHECK ADD  CONSTRAINT [Fk_IdEstado_Regimen] FOREIGN KEY([IdEstado])
REFERENCES [dbo].[Utb_Estado] ([IdEstado])
GO
ALTER TABLE [dbo].[Utb_Regimen] CHECK CONSTRAINT [Fk_IdEstado_Regimen]
GO
ALTER TABLE [dbo].[Utb_TarjetProximidad]  WITH CHECK ADD FOREIGN KEY([IdEstado])
REFERENCES [dbo].[Utb_Estado] ([IdEstado])
GO
ALTER TABLE [dbo].[Utb_TarjetProximidad]  WITH CHECK ADD FOREIGN KEY([IdPersona])
REFERENCES [dbo].[Utb_Persona] ([IdPersona])
GO
ALTER TABLE [dbo].[Utb_Terminal]  WITH CHECK ADD  CONSTRAINT [Fk_IdArea_Terminal] FOREIGN KEY([IdArea])
REFERENCES [dbo].[Utb_Area] ([IdArea])
GO
ALTER TABLE [dbo].[Utb_Terminal] CHECK CONSTRAINT [Fk_IdArea_Terminal]
GO
ALTER TABLE [dbo].[Utb_Terminal]  WITH CHECK ADD  CONSTRAINT [Fk_IdConfiguracion_Terminal] FOREIGN KEY([IdConfiguracion])
REFERENCES [dbo].[Utb_Configuracion] ([IdConfiguracion])
GO
ALTER TABLE [dbo].[Utb_Terminal] CHECK CONSTRAINT [Fk_IdConfiguracion_Terminal]
GO
ALTER TABLE [dbo].[Utb_Terminal]  WITH CHECK ADD  CONSTRAINT [Fk_IdEstado_Terminal] FOREIGN KEY([IdEstado])
REFERENCES [dbo].[Utb_Estado] ([IdEstado])
GO
ALTER TABLE [dbo].[Utb_Terminal] CHECK CONSTRAINT [Fk_IdEstado_Terminal]
GO
ALTER TABLE [dbo].[Utb_Terminal]  WITH CHECK ADD  CONSTRAINT [Fk_IdMarca_Terminal] FOREIGN KEY([IdMarca])
REFERENCES [dbo].[Utb_Marca] ([IdMarca])
GO
ALTER TABLE [dbo].[Utb_Terminal] CHECK CONSTRAINT [Fk_IdMarca_Terminal]
GO
ALTER TABLE [dbo].[Utb_Terminal]  WITH CHECK ADD  CONSTRAINT [Fk_IdModelo_Terminal] FOREIGN KEY([IdModelo])
REFERENCES [dbo].[Utb_Modelo] ([IdModelo])
GO
ALTER TABLE [dbo].[Utb_Terminal] CHECK CONSTRAINT [Fk_IdModelo_Terminal]
GO
ALTER TABLE [dbo].[Utb_Tipo]  WITH CHECK ADD  CONSTRAINT [Fk_IdEstado_Tipo] FOREIGN KEY([IdEstado])
REFERENCES [dbo].[Utb_Estado] ([IdEstado])
GO
ALTER TABLE [dbo].[Utb_Tipo] CHECK CONSTRAINT [Fk_IdEstado_Tipo]
GO
ALTER TABLE [dbo].[Utb_Ubicacion]  WITH CHECK ADD  CONSTRAINT [Fk_IdDepartamento_Cargo] FOREIGN KEY([IdDepartamento])
REFERENCES [dbo].[Utb_Departamento] ([IdDepartamento])
GO
ALTER TABLE [dbo].[Utb_Ubicacion] CHECK CONSTRAINT [Fk_IdDepartamento_Cargo]
GO
ALTER TABLE [dbo].[Utb_Usuario]  WITH CHECK ADD  CONSTRAINT [Fk_IdEstado_Usuario] FOREIGN KEY([IdEstado])
REFERENCES [dbo].[Utb_Estado] ([IdEstado])
GO
ALTER TABLE [dbo].[Utb_Usuario] CHECK CONSTRAINT [Fk_IdEstado_Usuario]
GO
ALTER TABLE [dbo].[Utb_Usuario]  WITH CHECK ADD  CONSTRAINT [Fk_IdPersona_Usuario] FOREIGN KEY([IdPersona])
REFERENCES [dbo].[Utb_Persona] ([IdPersona])
GO
ALTER TABLE [dbo].[Utb_Usuario] CHECK CONSTRAINT [Fk_IdPersona_Usuario]
GO
ALTER TABLE [dbo].[Utb_UsuarioOpcion]  WITH CHECK ADD  CONSTRAINT [FK_UsuarioOpcion_IdEstado] FOREIGN KEY([IdEstado])
REFERENCES [dbo].[Utb_Estado] ([IdEstado])
GO
ALTER TABLE [dbo].[Utb_UsuarioOpcion] CHECK CONSTRAINT [FK_UsuarioOpcion_IdEstado]
GO
ALTER TABLE [dbo].[Utb_UsuarioOpcion]  WITH CHECK ADD  CONSTRAINT [FK_UsuarioOpcion_Opcion] FOREIGN KEY([IdOpcion])
REFERENCES [dbo].[Utb_Opcion] ([IdOpcion])
GO
ALTER TABLE [dbo].[Utb_UsuarioOpcion] CHECK CONSTRAINT [FK_UsuarioOpcion_Opcion]
GO
ALTER TABLE [dbo].[Utb_UsuarioOpcion]  WITH CHECK ADD  CONSTRAINT [FK_UsuarioOpcion_Usuario] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Utb_Usuario] ([IdUsuario])
GO
ALTER TABLE [dbo].[Utb_UsuarioOpcion] CHECK CONSTRAINT [FK_UsuarioOpcion_Usuario]
GO
/****** Object:  StoredProcedure [dbo].[Sp_AreaConsulta]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_AreaConsulta]
@IdArea CHAR(6),
@IdEmpresa varchar (6)
AS
BEGIN
SELECT [IdArea]
      ,[Nombre]
	  ,IdEmpresa
	  ,(CASE WHEN IdEstado='ES0201' THEN 'true' ELSE  'false'END)IdEstado
  FROM [dbo].[Utb_Area] WHERE IdArea =  @IdArea and (@IdEmpresa=''or IdEmpresa = @IdEmpresa)
END
GO
/****** Object:  StoredProcedure [dbo].[Sp_AreaDropDown]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROC [dbo].[Sp_AreaDropDown]
@IdConsulta varchar(15),
@IdEmpresa char(6)=''
AS
BEGIN
 IF(@IdConsulta='')
   BEGIN
    SELECT IdArea Value, Nombre, '' Valor1 FROM Utb_Area where  (@IdEmpresa='' or IdEmpresa = @IdEmpresa) 
   END
 ELSE
  BEGIN
   SELECT* FROM(
     SELECT AR.IdArea Value, AR.Nombre,'0' Valor1 FROM Utb_Area AR 
	 LEFT JOIN Utb_PersonaArea PAR ON AR.IdArea = PAR.IdArea WHERE  (@IdEmpresa='' or AR.IdEmpresa = @IdEmpresa) and AR.IdArea <> (SELECT TOP 1 ARE.IdArea FROM Utb_PersonaArea ARE WHERE ARE.IdPersona = @IdConsulta )
     union
	 SELECT AR.IdArea Value, AR.Nombre,'1' Valor1 FROM Utb_Area AR 
     JOIN Utb_PersonaArea PAR ON AR.IdArea = PAR.IdArea WHERE IdPersona = @IdConsulta and (@IdEmpresa='' or AR.IdEmpresa = @IdEmpresa) 
	 )P GROUP BY P.Nombre,P.Valor1,P.Value
	 order by p.Value
  END
END
GO
/****** Object:  StoredProcedure [dbo].[Sp_AreaEliminar]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_AreaEliminar]
@IdArea CHAR(6)=NULL
AS
BEGIN
 UPDATE Utb_Area SET IdEstado = 'ES0203' WHERE IdArea = @IdArea
end
GO
/****** Object:  StoredProcedure [dbo].[Sp_AreaMantenimiento]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_AreaMantenimiento]
@IdArea CHAR(6)=NULL,
@Nombre VARCHAR(50),
@IdEstado varchar (10),
@IdEmpresa varchar (6)
AS
SET @IdEstado =(CASE WHEN @IdEstado ='TRUE' THEN 'ES0201' ELSE 'ES0202' END)
BEGIN
BEGIN TRY
--Registrar
	IF(@IdArea = '')
	BEGIN
		--Existe 
		SET  @IdArea = ( CASE WHEN (SELECT COUNT(*) FROM Utb_Area )>0 THEN (SELECT 'AR' + RIGHT('000'+CONVERT(VARCHAR(4), SUBSTRING( MAX(IdArea),3,LEN(MAX(IdArea)))+1), 4) FROM Utb_Area) ELSE 'AR0001' END);
		IF NOT EXISTS (SELECT top 1 IdArea from Utb_Area where IdArea = @IdArea)
		BEGIN
			 INSERT INTO Utb_Area(IdArea,Nombre,IdEstado,IdEmpresa)
			        VALUES(@IdArea,@Nombre,@IdEstado,@IdEmpresa)
		END
		ELSE
		BEGIN
			SELECT -1 AS REGISTRO
		END
	END

--Actualizar
    ELSE
	BEGIN
		IF EXISTS (SELECT top 1 IdArea from Utb_Area where IdArea = @IdArea)
		BEGIN
		    UPDATE Utb_Area SET Nombre=@Nombre,IdEstado=@IdEstado,IdEmpresa=@IdEmpresa WHERE IdArea=@IdArea
		END
		ELSE
		BEGIN
			SELECT -1 AS ACTUALIZAR
		END

	END

END TRY
BEGIN CATCH 
	SELECT -2 AS IdEmpresa
END CATCH
end
GO
/****** Object:  StoredProcedure [dbo].[SP_AreaPaginado]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

-- =====================================================
-- Author:  LA
-- Create date: 15/04/2018
-- Description: Listar Area
-- Test:EXEC [dbo].[SP_AreaPaginado] @WhereFilter = "WHERE P.Estado != ''  AND P.IdEmprea ='EM0001'", @OrderBy = 'IdProspecto ASC', @Start = 0, @AmountRows = 10
-- =====================================================
CREATE PROCEDURE [dbo].[SP_AreaPaginado]
@WhereFilter VARCHAR(MAX) = '',
@OrderBy VARCHAR (100) = '', 
@Start INT = 0,
@AmountRows INT = 0
AS
BEGIN
SET NOCOUNT ON;
    DECLARE @SentenciaSQL nvarchar(3072)
	SET @SentenciaSQL =  "

	;WITH Consulta AS (
SELECT P.IdArea
 ,P.Nombre
 ,P.IdEmpresa
 ,p.Estado FROM(
   SELECT IdArea,Nombre,IdEmpresa,'Activo' Estado
FROM Utb_Area WHERE IdEstado <> 'ES0203')P " + 
	@WhereFilter + 
	") SELECT *
	FROM Consulta
	CROSS JOIN (SELECT Count(*) AS Cantidad FROM Consulta) AS CC
	ORDER BY " +
	CASE WHEN ISNULL(@OrderBy,'') != '' THEN (@OrderBy) 
	ELSE " IdArea DESC " END
	    + ' ' +
	" OFFSET " + CONVERT(VARCHAR, (@Start)) + 
	" ROWS FETCH NEXT " + CONVERT(VARCHAR, @AmountRows) + " ROWS ONLY"
	PRINT (@SentenciaSQL);
    EXECUTE sp_executesql  @stmt = @SentenciaSQL

END

GO
/****** Object:  StoredProcedure [dbo].[Sp_CategoriaConsulta]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_CategoriaConsulta]
@IdCategoria CHAR(6)
AS
BEGIN
SELECT [IdCategoria]
      ,[Nombre]
      ,[IdEntidad]
	  ,IdEmpresa
      ,(CASE WHEN IdEstado='ES0201' THEN 'true' ELSE  'false'END)IdEstado
  FROM [dbo].[Utb_Categoria] WHERE IdCategoria = @IdCategoria
END
GO
/****** Object:  StoredProcedure [dbo].[Sp_CategoriaDropDown]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
--Sp_CategoriaDropDown 'DO0001'
CREATE PROC [dbo].[Sp_CategoriaDropDown] 
@IdConsulta varchar(15),
@IdEmpresa varchar (6) = ''
AS
BEGIN
IF(@IdConsulta ='COLABORADOR')
 BEGIN
  SELECT  CA.IdCategoria Value, CA.Nombre,'0' Valor1 FROM Utb_Categoria CA JOIN Utb_CategoriaEndidad CAEN ON CA.IdCategoria=CAEN.IdCategoria WHERE CAEN.IdEntidad = 'EN0101' AND (@IdEmpresa='' OR CA.IdEmpresa = @IdEmpresa)
 END
IF(@IdConsulta='EMPRESA')
 BEGIN
    SELECT  CA.IdCategoria Value, CA.Nombre,'0' Valor1 FROM Utb_Categoria CA JOIN Utb_CategoriaEndidad CAEN ON CA.IdCategoria=CAEN.IdCategoria WHERE CAEN.IdEntidad = 'EN0105' AND (@IdEmpresa='' OR CA.IdEmpresa = @IdEmpresa)
 END
 ELSE
 BEGIN
  IF NOT EXISTS(SELECT TOP 1 CD.IdCategoria FROM  Utb_CategoriaDocumento CD WHERE CD.IdDocumento = @IdConsulta )
 BEGIN
  SELECT * FROM (
   SELECT CA.IdCategoria Value, CA.Nombre,'0' Valor1   FROM Utb_Categoria CA
   JOIN Utb_CategoriaEndidad CAE ON CA.IdCategoria = CAE.IdCategoria 
   WHERE CAE.IdEntidad  =@IdConsulta AND (@IdEmpresa='' OR CA.IdEmpresa = @IdEmpresa)
  )P GROUP BY P.Nombre,P.Valor1,P.Value
 END
ELSE
 BEGIN
 SELECT * FROM (
 SELECT CA.IdCategoria Value, CA.Nombre,'0' Valor1   FROM Utb_Categoria CA
   LEFT JOIN Utb_CategoriaEndidad CAE ON CA.IdCategoria = CAE.IdCategoria 
  LEFT JOIN Utb_CategoriaDocumento CAD ON CA.IdCategoria = CAD.IdCategoria 
  WHERE CAD.IdDocumento  IS NULL  AND CAE.IdEntidad =(SELECT top 1 CADD.IdEntidad FROM Utb_CategoriaDocumento CADD WHERE IdDocumento = @IdConsulta) AND (@IdEmpresa='' OR CA.IdEmpresa = @IdEmpresa)

union
 SELECT CA.IdCategoria Value, CA.Nombre,'1' Valor1   FROM Utb_Categoria CA
 JOIN Utb_CategoriaDocumento CAT ON CA.IdCategoria = CAT.IdCategoria WHERE CAT.IdDocumento = @IdConsulta AND (@IdEmpresa='' OR CA.IdEmpresa = @IdEmpresa)
 )P GROUP BY P.Nombre,P.Valor1,P.Value
 END
 END
END
GO
/****** Object:  StoredProcedure [dbo].[Sp_CategoriaEliminar]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_CategoriaEliminar]
@IdCategoria CHAR(6)
AS
BEGIN
BEGIN TRY
		--Existe 
     IF  EXISTS (SELECT top 1 IdCategoria from Utb_Categoria where IdCategoria = @IdCategoria)
	  BEGIN
	  	 UPDATE Utb_Categoria SET IdEstado = 'ES0203'  WHERE IdCategoria=@IdCategoria
	  END
	  ELSE
	  BEGIN
	  	SELECT -1 AS IdEmpresa
	  END

END TRY
BEGIN CATCH 
	SELECT -2 AS IdEmpresa
END CATCH
end
GO
/****** Object:  StoredProcedure [dbo].[Sp_CategoriaMantenimiento]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[Sp_CategoriaMantenimiento] 
@IdCategoria CHAR(6),
@Nombre VARCHAR(50),
@IdEntidad VARCHAR(MAX),
@IdEstado varchar(10),
@IdEmpresa CHAR(6)
AS
DELETE FROM Utb_CategoriaEndidad WHERE IdCategoria = @IdCategoria

DECLARE @list varchar(8000)
DECLARE @pos INT
DECLARE @len INT
DECLARE @value varchar(8000)

SET @list = @IdEntidad

set @pos = 0
set @len = 0
SET @IdEstado =(CASE WHEN upper(@IdEstado) ='TRUE' THEN 'ES0201' ELSE 'ES0202' END)
BEGIN
BEGIN TRY
--Registrar
	IF(@IdCategoria = '')
	BEGIN
		SET  @IdCategoria = ( CASE WHEN (SELECT COUNT(*) FROM Utb_Categoria )>0 THEN (SELECT 'CA' + RIGHT('000'+CONVERT(VARCHAR(4), SUBSTRING( MAX(IdCategoria),3,LEN(MAX(IdCategoria)))+1), 4) FROM Utb_Categoria) ELSE 'CA0001' END);
		--Existe 
		IF NOT EXISTS (SELECT top 1 IdCategoria from Utb_Categoria where IdCategoria = @IdCategoria)
		BEGIN
			 INSERT INTO Utb_Categoria(IdCategoria,Nombre,IdEstado,IdEmpresa)
			        VALUES(@IdCategoria,@Nombre,@IdEstado,@IdEmpresa)
		END
		ELSE
		BEGIN
			SELECT -1 AS IdEmpresa
		END
	END

--Actualizar
   ELSE
	BEGIN
		IF  EXISTS (SELECT top 1 IdCategoria from Utb_Categoria where IdCategoria = @IdCategoria)
		BEGIN
		    UPDATE Utb_Categoria SET Nombre=@Nombre,IdEstado=@IdEstado,IdEmpresa = @IdEmpresa WHERE IdCategoria=@IdCategoria
		END
		ELSE
		BEGIN
			SELECT -1 AS IdEmpresa
		END

	END


WHILE CHARINDEX(',', @list, @pos+1)>0
BEGIN
	set @len = CHARINDEX(',', @list, @pos+1) - @pos
	set @value = SUBSTRING(@list, @pos, @len)
            
	PRINT @value -- for debug porpose   
	IF(@value!='true' or @value!='false')
	 begin
	   if exists(select top 1 IdEntidad from Utb_Entidad where IdEntidad = @value)
	begin
	    INSERT INTO Utb_CategoriaEndidad(IdCategoria,IdEntidad)VALUES(@IdCategoria,@value)
	end
	 end

	set @pos = CHARINDEX(',', @list, @pos+@len) +1
END

END TRY
BEGIN CATCH 
	SELECT ISNULL(ERROR_NUMBER(),0) AS ErrorNumber
	,ISNULL(ERROR_SEVERITY(),0) AS ErrorSeverity
	,ISNULL(ERROR_STATE(),0) AS ErrorState
	,ISNULL(ERROR_PROCEDURE(),0) AS ErrorProcedure
	,GETDATE()		
	,ISNULL(ERROR_MESSAGE(),0) AS ErrorMessage
	,ISNULL(ERROR_LINE(),0) AS ErrorLine;
END CATCH
end
GO
/****** Object:  StoredProcedure [dbo].[Sp_CategoriaPaginado]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

-- =====================================================
-- Author:  LA
-- Create date: 15/04/2018
-- Description: Listar Categoria
-- Test:EXEC [dbo].[SP_CategoriaPaginado] @WhereFilter = "WHERE P.Estado <> '' ", @OrderBy = 'IdCategoria ASC', @Start = 0, @AmountRows = 10
-- =====================================================
CREATE PROCEDURE [dbo].[Sp_CategoriaPaginado]
@WhereFilter VARCHAR(MAX) = '',
@OrderBy VARCHAR (100) = '', 
@Start INT = 0,
@AmountRows INT = 0
AS
BEGIN
SET NOCOUNT ON;
    DECLARE @SentenciaSQL nvarchar(3072)
	SET @SentenciaSQL =  "
	;WITH Consulta AS (
	 SELECT 
	  p.IdCategoria
	 ,P.Nombre
	 ,p.Entidad
	 ,p.Estado 
	 ,p.IdEmpresa
	 FROM(
	   SELECT CA.IdCategoria,CA.Nombre,
	  ISNULL( (	 SELECT STUFF(
       (SELECT ', ' + EN.Nombre
		FROM Utb_CategoriaEndidad ENC
		JOIN Utb_Entidad EN ON ENC.IdEntidad = EN.IdEntidad 
		WHERE ENC.IdCategoria = CA.IdCategoria
		FOR XML PATH ('')),
		1,2, '')),'') Entidad ,ES.Nombre Estado,CA.IdEmpresa
	 FROM Utb_Categoria CA
	 JOIN Utb_Estado ES ON CA.IdEstado = ES.IdEstado WHERE ES.IdEstado <> 'ES0203')P
 " + 
	@WhereFilter + 
	") SELECT *
	FROM Consulta
	CROSS JOIN (SELECT Count(*) AS Cantidad FROM Consulta) AS CC
	ORDER BY " +
	CASE WHEN ISNULL(@OrderBy,'') != '' THEN (@OrderBy) 
	ELSE " IdCategoria DESC " END
	    + ' ' +
	" OFFSET " + CONVERT(VARCHAR, (@Start)) + 
	" ROWS FETCH NEXT " + CONVERT(VARCHAR, @AmountRows) + " ROWS ONLY"
	PRINT (@SentenciaSQL);
    EXECUTE sp_executesql  @stmt = @SentenciaSQL

END
GO
/****** Object:  StoredProcedure [dbo].[Sp_ColaboradorConsulta]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_ColaboradorConsulta]
@IdColaborador CHAR(15)
AS
BEGIN
SELECT COLA.IdColaborador
       ,IdPersona
      ,[NumeroDocumento]
      ,[Nombres]
      ,[ApellidoPaterno]
      ,[ApellidoMaterno]
      ,[Alias]
      ,[Direccion]
      ,(CONVERT (varchar(10),FechaNacimiento,103))FechaNacimiento
      ,[IdPersona]
      ,[IdEmpresa]
      ,[Genero]
      ,[IdCategoria]
      ,[IdCondicion]
      ,(CASE WHEN IdEstado='ES0201' THEN 'true' ELSE  'false'END)IdEstado
      ,[IdDepartamento]
      ,[IdUbicacion]
      ,[IdProvincia]
      ,[IdUDepartamento]
      ,[IdDistrito]
	  , ISNULL( (	 SELECT STUFF(
       (SELECT ', ' + OP.Numero+'-'+OP.IdOperador+'-'+OPER.Nombre
		FROM Utb_PersonaOperador OP
		JOIN Utb_Operador OPER ON OP.IdOperador=OPER.IdOperador
		WHERE OP.IdPersona = COLA.IdPersona
		FOR XML PATH ('')),
		1,2, '')),'') Telefono

	  ,ISNULL( (	 SELECT STUFF(
       (SELECT ', ' + OP.Cuenta+'-'+OP.IdEmail+'-'+EMA.Nombre
		FROM Utb_PersonaEmail OP
		JOIN Utb_Email EMA ON OP.IdEmail = EMA.IdEmail
		WHERE OP.IdPersona = COLA.IdPersona
		FOR XML PATH ('')),
		1,2, '')),'') Correo

	   ,ISNULL( (	 SELECT STUFF(
       (SELECT ', ' + OP.NumeroTrajeta
		FROM Utb_TarjetProximidad OP
		WHERE OP.IdPersona = COLA.IdPersona
		FOR XML PATH ('')),
		1,2, '')),'') Tarjeta,
		TI.descripcion AS Descripcion,
		TI.IdTipoLugar

  FROM [dbo].[Utb_Colaborador] COLA 
  LEFT JOIN Utb_ColaboradorTipoLugar TI ON COLA.IdColaborador = TI.IdColaborador
  WHERE COLA.IdColaborador = @IdColaborador
 
END
GO
/****** Object:  StoredProcedure [dbo].[Sp_ColaboradorCorreoEliminar]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_ColaboradorCorreoEliminar]
@IdPersona char(15),
@IdTipo CHAR(6),
@Nombre VARCHAR(20)
AS
BEGIN

	  delete from  Utb_PersonaEmail  WHERE  IdEmail=@IdTipo AND IdPersona=@IdPersona

END
GO
/****** Object:  StoredProcedure [dbo].[Sp_ColaboradorCorreoListado]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_ColaboradorCorreoListado]
@IdPersona char(15)
AS
BEGIN
 SELECT op.IdPersona, op.IdEmail IdTipo, op.Cuenta Nombre, o.Nombre Tipo from Utb_PersonaEmail op join Utb_Email o on op.IdEmail = o.IdEmail  WHERE IdPersona = @IdPersona
END
GO
/****** Object:  StoredProcedure [dbo].[Sp_ColaboradorCorreoMantenimiento]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_ColaboradorCorreoMantenimiento]
@IdPersona char(15),
@IdTipo CHAR(6),
@Nombre VARCHAR(20)
AS
BEGIN
  IF NOT EXISTS(SELECT top 1 IdEmail FROM Utb_PersonaEmail WHERE IdPersona = @IdPersona AND IdEmail = @IdTipo)
    BEGIN
	  INSERT INTO  Utb_PersonaEmail(IdEmail,IdPersona,Cuenta,IdEstado)VALUES(@IdTipo,@IdPersona,@Nombre,'ES0201')
	  select @IdPersona result
	END
  ELSE
    BEGIN
	  UPDATE  Utb_PersonaEmail SET Cuenta = @Nombre WHERE  IdEmail=@IdTipo AND IdPersona=@IdPersona
	   select @IdPersona result
	END
END
GO
/****** Object:  StoredProcedure [dbo].[Sp_ColaboradorDigitalizacionExcel]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[Sp_ColaboradorDigitalizacionExcel]
@IdPersona char(15)
AS
BEGIN
	SELECT P.Codigo,
	 p.Nombre
	,P.Numero,P.Empresa,P.Categoria,P.Documento,
	(CASE WHEN P.ObservacionDocumento IS NULL THEN P.ObservacionDigitalizacion ELSE P.ObservacionDocumento END)Observacion
	,P.Obligatorio,P.FechaVencimiento Vencimiento,P.Estado,P.EstadoDigitalizacion from(
	SELECT COLA.IdColaborador Codigo,COLA.Nombres+' '+COLA.ApellidoPaterno+' '+COLA.ApellidoMaterno Nombre,COLA.NumeroDocumento Numero,
   (SELECT top 1 Observacion FROM Utb_DocumentoAdjunto  WHERE IdDocumento = DOC.IdDocumento  AND IdPersona = COLA.IdPersona )ObservacionDocumento,
    ISNULL((SELECT top 1 Observacion FROM Utb_Digitalizacion  WHERE IdDocumento = DOC.IdDocumento AND  IdPersona = COLA.IdPersona ),'')ObservacionDigitalizacion,
	EM.RazonSocial Empresa,CA.Nombre Categoria,DOC.Nombre Documento,
    (CASE WHEN DOC.Obligatorio=1  THEN  'SI' ELSE 'NO' END) Obligatorio,
	(CASE WHEN DOC.FechaVencimiento=1  THEN  'SI' ELSE 'NO' END) FechaVencimiento,
	ISNULL((SELECT TOP 1 (CASE WHEN DOAC.IdEstado='ES0401'  THEN  'APROBADO' ELSE 'DESAPROBADO' END) FROM Utb_DocumentoAdjunto DOAC JOIN Utb_Estado ES ON DOAC.IdEstado = ES.IdEstado WHERE  DOAC.IdDocumento = DOC.IdDocumento  AND DOAC.IdPersona = COLA.IdPersona ),'')Estado
    ,ISNULL((SELECT top 1 ES.Nombre FROM Utb_Digitalizacion D JOIN Utb_Estado ES ON D.IdEstado = ES.IdEstado WHERE D.IdPersona = COLA.IdPersona),'SIN DIGITALIZAR')EstadoDigitalizacion
	,(SELECT COUNT(*) FROM Utb_DocumentoAdjunto DD WHERE DD.IdPersona = COLA.IdPersona AND IdEstado = 'ES0401'   )TotalAprobados
	,(SELECT COUNT(*) FROM Utb_DocumentoAdjunto DD WHERE DD.IdPersona = COLA.IdPersona AND IdEstado = 'ES0402'   )TotalDesaprobados
	,(SELECT COUNT(*) FROM Utb_DocumentoAdjunto DD WHERE DD.IdPersona = COLA.IdPersona )TotalDocumento
  FROM Utb_Colaborador COLA 
  JOIN Utb_Empresa EM ON COLA.IdEmpresa = EM.IdEmpresa
  JOIN Utb_CategoriaEndidad CADE ON COLA.IdEntidad = CADE.IdEntidad and CADE.IdCategoria = COLA.IdCategoria
  JOIN Utb_Categoria CA ON COLA.IdCategoria = CA.IdCategoria
  JOIN Utb_CategoriaDocumento CADO ON COLA.IdCategoria = CADO.IdCategoria AND CADO.IdEntidad = COLA.IdEntidad
  JOIN Utb_Documento DOC ON CADO.IdDocumento = DOC.IdDocumento
  WHERE COLA.Enviado = 1 and COLA.IdPersona = @IdPersona
 )P

END

--SELECT * FROM Utb_Colaborador WHERE IdColaborador = 'CO0000000000013'
GO
/****** Object:  StoredProcedure [dbo].[Sp_ColaboradorDigitalizacionListado]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO


CREATE PROCEDURE [dbo].[Sp_ColaboradorDigitalizacionListado]
@IdEmpresa VARCHAR(MAX) = '',
@IdPadre VARCHAR (100) = '', 
@Start INT = 0,
@AmountRows INT = 0
AS
BEGIN
		  SELECT *from(
	SELECT COLA.IdEmpresa, COLA.IdColaborador, COLA.IdPersona, COLA.Alias,COLA.Nombres,COLA.ApellidoPaterno,COLA.ApellidoMaterno,COLA.IdCategoria,CA.Nombre Categoria,DOC.IdDocumento,DOC.Nombre Documento,
    (Select COUNT(*) from Utb_DocumentoAdjunto where IdPersona = COLA.IdPersona and IdDocumento =DOC.IdDocumento)Adjuntado,(CASE WHEN DOC.Obligatorio=1  THEN  'SI' ELSE 'NO' END) Obligatorio, (CASE WHEN DOC.FechaVencimiento=1  THEN  'SI' ELSE 'NO' END) FechaVencimiento,
    ISNULL((SELECT top 1 (CASE WHEN  UPPER(ES.Nombre) IS NULL THEN 'REGISTRO' ELSE UPPER(ES.Nombre) END) FROM Utb_Digitalizacion DIG JOIN Utb_Estado ES ON DIG.IdEstado = ES.IdEstado WHERE IdDocumento = DOC.IdDocumento  AND IdPersona = COLA.IdPersona ),'REGISTRO')Estado,
	(SELECT top 1 Observacion FROM Utb_Digitalizacion WHERE IdDocumento = DOC.IdDocumento  AND IdPersona = COLA.IdPersona )Observacion,
	(SELECT top 1 IdDocumentoAdjunto FROM Utb_DocumentoAdjunto WHERE IdDocumento = DOC.IdDocumento  AND IdPersona = COLA.IdPersona )IdDocumentoAdjunto,
	(SELECT top 1 IdDigitalizacion FROM Utb_Digitalizacion WHERE IdDocumento = DOC.IdDocumento  AND IdPersona = COLA.IdPersona )IdDigitalizacion,
	(SELECT top 1 Enviado FROM Utb_Digitalizacion WHERE IdDocumento = DOC.IdDocumento  AND IdPersona = COLA.IdPersona )Enviado,
	ISNULL((SELECT TOP 1 (CASE WHEN ES.IdEstado='ES0401'  THEN  'APROBADO' ELSE 'DESAPROBADO' END) FROM Utb_DocumentoAdjunto DOAC JOIN Utb_Estado ES ON DOAC.IdEstado = ES.IdEstado WHERE ES.IdEstado BETWEEN 'ES0401' AND 'ES0499' AND DOAC.IdDocumento = DOC.IdDocumento  AND DOAC.IdPersona = COLA.IdPersona ),'')EstadoAdjunto,
	ISNULL((SELECT TOP 1 DOAC.Observacion FROM Utb_DocumentoAdjunto DOAC JOIN Utb_Estado ES ON DOAC.IdEstado = ES.IdEstado WHERE ES.IdEstado BETWEEN 'ES0401' AND 'ES0499' AND DOAC.IdDocumento = DOC.IdDocumento  AND DOAC.IdPersona = COLA.IdPersona ),'')ObservacionAdjunto,
	  (SELECT  E.IdPadre from Utb_Empresa E where  E.IdEmpresa = COLA.IdEmpresa   )IdPadre,
	 (SELECT  E.IdPadre from Utb_Empresa E where  E.IdEmpresa = (SELECT E.IdPadre FROM Utb_Empresa E WHERE E.IdEmpresa = COLA.IdEmpresa )  )IdPadreSubcontratista,
	 (CASE WHEN EM.EsPrincipal>0 THEN EM.RazonSocial 
	 ELSE 
	   (SELECT  (CASE WHEN E.IdPadre IS NULL 
	                  THEN (SELECT EM.RazonSocial FROM Utb_Empresa EM WHERE EM.IdEmpresa = E.IdEmpresa)
			   ELSE (SELECT EM.RazonSocial FROM Utb_Empresa EM WHERE EM.IdEmpresa = E.IdPadre) END ) from Utb_Empresa E where  E.IdEmpresa = EM.IdPadre   )
	  END)PadreSubcontratista
	 
  FROM Utb_Colaborador COLA 
  JOIN Utb_Empresa EM ON COLA.IdEmpresa = EM.IdEmpresa
  JOIN Utb_CategoriaEndidad CADE ON COLA.IdEntidad = CADE.IdEntidad and CADE.IdCategoria = COLA.IdCategoria
  JOIN Utb_Categoria CA ON COLA.IdCategoria = CA.IdCategoria
  JOIN Utb_CategoriaDocumento CADO ON COLA.IdCategoria = CADO.IdCategoria AND CADO.IdEntidad = COLA.IdEntidad
  JOIN Utb_Documento DOC ON CADO.IdDocumento = DOC.IdDocumento
  JOIN Utb_Persona PER ON COLA.IdPersona = PER.IdPersona )P
  WHERE P.Enviado = 1 AND (P.IdEmpresa = @IdEmpresa) OR (P.IdPadre = @IdEmpresa) OR(P.IdPadreSubcontratista = @IdEmpresa)
END





GO
/****** Object:  StoredProcedure [dbo].[Sp_ColaboradorDigitalizacionListar]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[Sp_ColaboradorDigitalizacionListar]
@IdPersona char(15)
AS
BEGIN
		  SELECT *from(
	SELECT COLA.IdEmpresa, COLA.IdColaborador, COLA.IdPersona, COLA.Alias,COLA.Nombres,COLA.ApellidoPaterno,COLA.ApellidoMaterno,COLA.IdCategoria,CA.Nombre Categoria,DOC.IdDocumento,DOC.Nombre Documento,
    (Select COUNT(*) from Utb_DocumentoAdjunto where IdPersona = COLA.IdPersona and IdDocumento =DOC.IdDocumento)Adjuntado,(CASE WHEN DOC.Obligatorio=1  THEN  'SI' ELSE 'NO' END) Obligatorio, (CASE WHEN DOC.FechaVencimiento=1  THEN  'SI' ELSE 'NO' END) FechaVencimiento,
    ISNULL((SELECT top 1 (CASE WHEN  UPPER(ES.Nombre) IS NULL THEN 'REGISTRO' ELSE UPPER(ES.Nombre) END) FROM Utb_Digitalizacion DIG JOIN Utb_Estado ES ON DIG.IdEstado = ES.IdEstado WHERE IdDocumento = DOC.IdDocumento  AND IdPersona = COLA.IdPersona ),'REGISTRO')Estado,
	(SELECT top 1 Observacion FROM Utb_Digitalizacion WHERE IdDocumento = DOC.IdDocumento  AND IdPersona = COLA.IdPersona )Observacion,
	(SELECT top 1 IdDocumentoAdjunto FROM Utb_DocumentoAdjunto WHERE IdDocumento = DOC.IdDocumento  AND IdPersona = COLA.IdPersona )IdDocumentoAdjunto,
	(SELECT top 1 IdDigitalizacion FROM Utb_Digitalizacion WHERE IdDocumento = DOC.IdDocumento  AND IdPersona = COLA.IdPersona )IdDigitalizacion,
	(SELECT top 1 Enviado FROM Utb_Digitalizacion WHERE IdDocumento = DOC.IdDocumento  AND IdPersona = COLA.IdPersona )Enviado,
	ISNULL((SELECT TOP 1 (CASE WHEN ES.IdEstado='ES0401'  THEN  'APROBADO' ELSE 'DESAPROBADO' END) FROM Utb_DocumentoAdjunto DOAC JOIN Utb_Estado ES ON DOAC.IdEstado = ES.IdEstado WHERE ES.IdEstado BETWEEN 'ES0401' AND 'ES0499' AND DOAC.IdDocumento = DOC.IdDocumento  AND DOAC.IdPersona = COLA.IdPersona ),'')EstadoAdjunto,
	ISNULL((SELECT TOP 1 DOAC.Observacion FROM Utb_DocumentoAdjunto DOAC JOIN Utb_Estado ES ON DOAC.IdEstado = ES.IdEstado WHERE ES.IdEstado BETWEEN 'ES0401' AND 'ES0499' AND DOAC.IdDocumento = DOC.IdDocumento  AND DOAC.IdPersona = COLA.IdPersona ),'')ObservacionAdjunto,
	  (SELECT  E.IdPadre from Utb_Empresa E where  E.IdEmpresa = COLA.IdEmpresa   )IdPadre,
	 (SELECT  E.IdPadre from Utb_Empresa E where  E.IdEmpresa = (SELECT E.IdPadre FROM Utb_Empresa E WHERE E.IdEmpresa = COLA.IdEmpresa )  )IdPadreSubcontratista,
	 (CASE WHEN EM.EsPrincipal>0 THEN EM.RazonSocial 
	 ELSE 
	   (SELECT  (CASE WHEN E.IdPadre IS NULL 
	                  THEN (SELECT EM.RazonSocial FROM Utb_Empresa EM WHERE EM.IdEmpresa = E.IdEmpresa)
			   ELSE (SELECT EM.RazonSocial FROM Utb_Empresa EM WHERE EM.IdEmpresa = E.IdPadre) END ) from Utb_Empresa E where  E.IdEmpresa = EM.IdPadre   )
	  END)PadreSubcontratista
	 
  FROM Utb_Colaborador COLA 
  JOIN Utb_Empresa EM ON COLA.IdEmpresa = EM.IdEmpresa
  JOIN Utb_CategoriaEndidad CADE ON COLA.IdEntidad = CADE.IdEntidad and CADE.IdCategoria = COLA.IdCategoria
  JOIN Utb_Categoria CA ON COLA.IdCategoria = CA.IdCategoria
  JOIN Utb_CategoriaDocumento CADO ON COLA.IdCategoria = CADO.IdCategoria AND CADO.IdEntidad = COLA.IdEntidad
  JOIN Utb_Documento DOC ON CADO.IdDocumento = DOC.IdDocumento
  JOIN Utb_Persona PER ON COLA.IdPersona = PER.IdPersona )P
  WHERE  P.IdPersona = @IdPersona
END





GO
/****** Object:  StoredProcedure [dbo].[Sp_ColaboradorDigitalizacionPaginado]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

-- =====================================================
-- Author:  LA
-- Create date: 15/04/2018
-- Description: Listar ColaboradorDigitalizacion
-- Test:EXEC [dbo].[SP_ColaboradorDigitalizacionPaginado] @WhereFilter = "WHERE P.Estado <> '' ", @OrderBy = 'IdColaborador ASC', @Start = 0, @AmountRows = 10
-- =====================================================
CREATE PROCEDURE [dbo].[Sp_ColaboradorDigitalizacionPaginado]
@WhereFilter VARCHAR(MAX) = '',
@OrderBy VARCHAR (100) = '', 
@Start INT = 0,
@AmountRows INT = 0
AS
BEGIN
SET NOCOUNT ON;
    DECLARE @SentenciaSQL nvarchar(3072)
	SET @SentenciaSQL =  "
	;WITH Consulta AS (

		  SELECT *from(
	SELECT COLA.IdColaborador, COLA.IdPersona, COLA.Alias,COLA.Nombres,COLA.ApellidoPaterno,COLA.ApellidoMaterno,COLA.IdCategoria,CA.Nombre Categoria,DOC.IdDocumento,DOC.Nombre Documento,
    (Select COUNT(*) from Utb_DocumentoAdjunto where IdPersona = COLA.IdPersona and IdDocumento =DOC.IdDocumento)Adjuntado,(CASE WHEN DOC.Obligatorio=1  THEN  'SI' ELSE 'NO' END) Obligatorio, (CASE WHEN DOC.FechaVencimiento=1  THEN  'SI' ELSE 'NO' END) FechaVencimiento,
    ISNULL((SELECT top 1 (CASE WHEN  UPPER(ES.Nombre) IS NULL THEN 'REGISTRO' ELSE UPPER(ES.Nombre) END) FROM Utb_Digitalizacion DIG JOIN Utb_Estado ES ON DIG.IdEstado = ES.IdEstado WHERE IdDocumento = DOC.IdDocumento  AND IdPersona = COLA.IdPersona ),'REGISTRO')Estado,
	(SELECT top 1 Observacion FROM Utb_Digitalizacion WHERE IdDocumento = DOC.IdDocumento  AND IdPersona = COLA.IdPersona )Observacion,
	(SELECT top 1 IdDocumentoAdjunto FROM Utb_DocumentoAdjunto WHERE IdDocumento = DOC.IdDocumento  AND IdPersona = COLA.IdPersona )IdDocumentoAdjunto,
	(SELECT top 1 IdDigitalizacion FROM Utb_Digitalizacion WHERE IdDocumento = DOC.IdDocumento  AND IdPersona = COLA.IdPersona )IdDigitalizacion,
	ISNULL((SELECT TOP 1 (CASE WHEN ES.IdEstado='ES0401'  THEN  'APROBADO' ELSE 'DESAPROBADO' END) FROM Utb_DocumentoAdjunto DOAC JOIN Utb_Estado ES ON DOAC.IdEstado = ES.IdEstado WHERE ES.IdEstado BETWEEN 'ES0401' AND 'ES0499' AND DOAC.IdDocumento = DOC.IdDocumento  AND DOAC.IdPersona = COLA.IdPersona ),'')EstadoAdjunto,
	ISNULL((SELECT TOP 1 DOAC.Observacion FROM Utb_DocumentoAdjunto DOAC JOIN Utb_Estado ES ON DOAC.IdEstado = ES.IdEstado WHERE ES.IdEstado BETWEEN 'ES0401' AND 'ES0499' AND DOAC.IdDocumento = DOC.IdDocumento  AND DOAC.IdPersona = COLA.IdPersona ),'')ObservacionAdjunto
  FROM Utb_Colaborador COLA 
  JOIN Utb_CategoriaEndidad CADE ON COLA.IdEntidad = CADE.IdEntidad and CADE.IdCategoria = COLA.IdCategoria
  JOIN Utb_Categoria CA ON COLA.IdCategoria = CA.IdCategoria
  JOIN Utb_CategoriaDocumento CADO ON COLA.IdCategoria = CADO.IdCategoria AND CADO.IdEntidad = COLA.IdEntidad
  JOIN Utb_Documento DOC ON CADO.IdDocumento = DOC.IdDocumento
  JOIN Utb_Persona PER ON COLA.IdPersona = PER.IdPersona )P
 
 " + 
	@WhereFilter + 
	") SELECT *
	FROM Consulta
	CROSS JOIN (SELECT Count(*) AS Cantidad FROM Consulta) AS CC
	ORDER BY " +
	CASE WHEN ISNULL(@OrderBy,'') != '' THEN (@OrderBy) 
	ELSE " IdColaborador DESC " END
	    + ' ' +
	" OFFSET " + CONVERT(VARCHAR, (@Start)) + 
	" ROWS FETCH NEXT " + CONVERT(VARCHAR, @AmountRows) + " ROWS ONLY"
	PRINT (@SentenciaSQL);
    EXECUTE sp_executesql  @stmt = @SentenciaSQL

END





GO
/****** Object:  StoredProcedure [dbo].[Sp_ColaboradorEliminar]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_ColaboradorEliminar]
@IdColaborador char(15)
AS
BEGIN
BEGIN TRY
		--Existe 
     IF  EXISTS (SELECT top 1 IdColaborador from Utb_Colaborador where IdColaborador = @IdColaborador)
	  BEGIN
	     DECLARE @IdPersona CHAR(15)=(SELECT IdPersona FROM Utb_Colaborador WHERE IdColaborador = @IdColaborador)
	  	 UPDATE Utb_Persona SET IdEstado = 'ES0203'  WHERE IdPersona = @IdPersona
	  END
	  ELSE
	  BEGIN
	  	SELECT -1 AS IdEmpresa
	  END

END TRY
BEGIN CATCH 
	SELECT -2 AS IdEmpresa
END CATCH
end
GO
/****** Object:  StoredProcedure [dbo].[Sp_ColaboradorListado]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_ColaboradorListado]
@IdEmpresa CHAR(15)
AS
BEGIN
SELECT 
P.IdColaborador,P.IdPersona,P.NumeroDocumento,P.Nombres,P.ApellidoPaterno,P.ApellidoMaterno,P.Alias,P.Digitalizado,P.FechaNacimiento,P.IdPersona,P.IdEmpresa,P.Genero,P.IdCategoria,P.IdCondicion,
P.IdEstado,P.IdDepartamento,P.IdUbicacion,P.IdProvincia,P.IdUDepartamento,P.IdDistrito,P.Telefono,P.Correo,P.Tarjeta,P.Descripcion,P.IdTipoLugar,P.TotalAprobados,P.TotalAprobados,P.TotalDocumento,P.Digitalizado,
('Colaborador: '+P.Nombres +' '+P.ApellidoPaterno + ' '+P.ApellidoMaterno+', Se aprobò '+CONVERT(varchar,P.TotalAprobados)+ ' de '+CONVERT(varchar,P.TotalDocumento) +' documentos adjuntados')Mensaje,P.Direccion,
P.IdUsuario,
P.Enviado
FROM
(
SELECT COLA.IdColaborador
       ,IdPersona
      ,[NumeroDocumento]
      ,[Nombres]
      ,[ApellidoPaterno]
      ,[ApellidoMaterno]
      ,[Alias]
      ,[Direccion]
      ,(CONVERT (varchar(10),FechaNacimiento,103))FechaNacimiento
      ,[IdEmpresa]
      ,[Genero]
      ,[IdCategoria]
      ,[IdCondicion]
      ,(CASE WHEN IdEstado='ES0201' THEN 'true' ELSE  'false'END)IdEstado
      ,[IdDepartamento]
      ,[IdUbicacion]
      ,[IdProvincia]
      ,[IdUDepartamento]
      ,[IdDistrito]
	  , ISNULL( (	 SELECT STUFF(
       (SELECT ', ' + OP.Numero+'-'+OP.IdOperador+'-'+OPER.Nombre
		FROM Utb_PersonaOperador OP
		JOIN Utb_Operador OPER ON OP.IdOperador=OPER.IdOperador
		WHERE OP.IdPersona = COLA.IdPersona
		FOR XML PATH ('')),
		1,2, '')),'') Telefono

	  ,ISNULL( (	 SELECT STUFF(
       (SELECT ', ' + OP.Cuenta+'-'+OP.IdEmail+'-'+EMA.Nombre
		FROM Utb_PersonaEmail OP
		JOIN Utb_Email EMA ON OP.IdEmail = EMA.IdEmail
		WHERE OP.IdPersona = COLA.IdPersona
		FOR XML PATH ('')),
		1,2, '')),'') Correo

	   ,ISNULL( (	 SELECT STUFF(
       (SELECT ', ' + OP.NumeroTrajeta
		FROM Utb_TarjetProximidad OP
		WHERE OP.IdPersona = COLA.IdPersona
		FOR XML PATH ('')),
		1,2, '')),'') Tarjeta,
		TI.descripcion AS Descripcion,
		TI.IdTipoLugar
	   ,(SELECT COUNT(*) FROM Utb_DocumentoAdjunto DD WHERE DD.IdPersona = COLA.IdPersona AND IdEstado = 'ES0401'   )TotalAprobados
	   ,(SELECT COUNT(*) FROM Utb_DocumentoAdjunto DD WHERE DD.IdPersona = COLA.IdPersona AND IdEstado = 'ES0402'   )TotalDesaprobados
	   ,(SELECT COUNT(*) FROM Utb_DocumentoAdjunto DD WHERE DD.IdPersona = COLA.IdPersona )TotalDocumento
	   ,(SELECT COUNT(*) FROM Utb_Digitalizacion DD WHERE DD.IdPersona = COLA.IdPersona )Digitalizado
	   ,COLA.Enviado
	   ,COLA.IdUsuario
  FROM [dbo].[Utb_Colaborador] COLA 
  LEFT JOIN Utb_ColaboradorTipoLugar TI ON COLA.IdColaborador = TI.IdColaborador
  WHERE COLA.IdEmpresa = @IdEmpresa
  )P
 
END
GO
/****** Object:  StoredProcedure [dbo].[Sp_ColaboradorMantenimiento]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_ColaboradorMantenimiento]
@IdColaborador  CHAR(15),
@IdPersona  CHAR(15) = NULL,
@Nombres VARCHAR(100),
@ApellidoPaterno VARCHAR(100),
@ApellidoMaterno VARCHAR(100),
@Genero  CHAR(1),
@FechaNacimiento DATETIME = NULL,
@IdUbicacion VARCHAR(100),
@IdDepartamento VARCHAR(100),
@IdDistrito CHAR(2),
@IdProvincia CHAR(2),
@IdUDepartamento CHAR(2),
@IdCondicion CHAR(6),
@Direccion VARCHAR(300),
@NumeroDocumento VARCHAR(20),
@IdCategoria CHAR(6),
@Alias varchar(50),
@IdArea varchar(500)=null,
@IdEstado varchar(100)=null,
@IdTipoLugar char(6),
@Descripcion varchar(200) = null,
@IdEmpresa char(6)
AS

DELETE FROM Utb_PersonaArea WHERE IdPersona = @IdPersona
DECLARE @list varchar(max)
DECLARE @pos INT
DECLARE @len INT
DECLARE @value varchar(max)
SET @IdEstado =(CASE WHEN @IdEstado ='TRUE' THEN 'ES0201' ELSE 'ES0202' END)
BEGIN
BEGIN TRY
--Registrar
	IF(@IdColaborador= '')
	BEGIN

	    SET  @IdPersona = ( CASE WHEN (SELECT COUNT(*) FROM Utb_Persona )>0 THEN (SELECT 'PE' + RIGHT('0000000000000'+CONVERT(VARCHAR(13), SUBSTRING( MAX(IdPersona),3,LEN(MAX(IdPersona)))+1), 13) FROM Utb_Persona) ELSE 'PE0000000000001' END);
		INSERT INTO Utb_Persona(IdPersona,IdEntidad,IdEstado)VALUES(@IdPersona,'EN0100','ES0101')
		SET  @IdColaborador = ( CASE WHEN (SELECT COUNT(*) FROM Utb_Colaborador )>0 THEN (SELECT 'CO' + RIGHT('0000000000000'+CONVERT(VARCHAR(13), SUBSTRING( MAX(IdColaborador),3,LEN(MAX(IdColaborador)))+1), 13) FROM Utb_Colaborador) ELSE 'CO0000000000001' END);
		--Existe 
		IF NOT EXISTS (SELECT top 1 IdColaborador from Utb_Colaborador where IdColaborador = @IdColaborador)
		BEGIN
			 INSERT INTO Utb_Colaborador(IdColaborador,IdPersona,Nombres,ApellidoPaterno,ApellidoMaterno,Genero,IdUbicacion,IdDepartamento,FechaNacimiento,IdDistrito,IdProvincia,IdUDepartamento,IdCondicion,IdEntidad,Direccion,NumeroDocumento,IdCategoria,Alias,IdEmpresa)
			        VALUES(@IdColaborador,@IdPersona,@Nombres,@ApellidoPaterno,@ApellidoMaterno,@Genero,@IdUbicacion,@IdDepartamento,@FechaNacimiento,@IdDistrito,@IdProvincia,@IdUDepartamento,@IdCondicion,'EN0101',@Direccion,@NumeroDocumento,@IdCategoria,@Alias,@IdEmpresa)
			 INSERT INTO Utb_ColaboradorTipoLugar(IdColaborador,IdTipoLugar,descripcion)VALUES(@IdColaborador,@IdTipoLugar,@Descripcion)
			 SELECT @IdPersona AS RESULT
		END
		ELSE
		BEGIN
			SELECT -1 AS IdEmpresa
		END
	END

--Actualizar
    ELSE
	BEGIN
		IF  EXISTS (SELECT top 1 IdColaborador from Utb_Colaborador where IdColaborador = @IdColaborador)
		BEGIN
		    UPDATE Utb_Colaborador SET Nombres=@Nombres,ApellidoPaterno=@ApellidoPaterno,
			ApellidoMaterno=@ApellidoMaterno,Genero=@Genero,IdUbicacion=@IdUbicacion,IdDepartamento=@IdDepartamento
			,FechaNacimiento=@FechaNacimiento ,IdDistrito=@IdDistrito,IdProvincia=@IdProvincia,IdUDepartamento=@IdUDepartamento
			,IdCondicion=@IdCondicion,Direccion=@Direccion,NumeroDocumento=@NumeroDocumento,IdCategoria=@IdCategoria,Alias=@Alias,IdEmpresa =@IdEmpresa
			where IdColaborador=@IdColaborador

			UPDATE Utb_ColaboradorTipoLugar SET Descripcion =@Descripcion WHERE IdColaborador = @IdColaborador AND IdTipoLugar = @IdTipoLugar
			SELECT @IdPersona AS RESULT
		END
		ELSE
		BEGIN
			SELECT -1 AS IdEmpresa
		END

	END



--PARA AREAS
SET @list = @IdArea

set @pos = 0
set @len = 0	
WHILE CHARINDEX(',', @list, @pos+1)>0
BEGIN
	set @len = CHARINDEX(',', @list, @pos+1) - @pos
	set @value = SUBSTRING(@list, @pos, @len)
	if exists(select top 1 @IdPersona from Utb_Persona where IdPersona = @IdPersona)
	begin
	    INSERT INTO Utb_PersonaArea(IdArea,IdPersona)VALUES(@value,@IdPersona)
	end
	--DO YOUR MAGIC HERE

	set @pos = CHARINDEX(',', @list, @pos+@len) +1
END

END TRY
BEGIN CATCH 
	SELECT ISNULL(ERROR_MESSAGE(),0) AS IdEmpresa
END CATCH
end

GO
/****** Object:  StoredProcedure [dbo].[Sp_ColaboradorPaginado]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

-- =====================================================
-- Author:  LA
-- Create date: 15/04/2018
-- Description: Listar Colaborador
-- Test:EXEC [dbo].[SP_ColaboradorPaginado] @WhereFilter = "WHERE P.Estado != ''  AND P.Digitalizacion ='REVISION'  AND P.IdEmpresa ='EM0001' OR P.IdPadre ='EM0001' OR P.IdPadreSubcontratista ='EM0001' ", @OrderBy = 'IdColaborador ASC', @Start = 0, @AmountRows = 10
-- =====================================================
CREATE PROCEDURE [dbo].[Sp_ColaboradorPaginado]
@WhereFilter VARCHAR(MAX) = '',
@OrderBy VARCHAR (100) = '', 
@Start INT = 0,
@AmountRows INT = 0
AS
BEGIN
SET NOCOUNT ON;
    DECLARE @SentenciaSQL nvarchar(max)
	SET @SentenciaSQL =  "
	;WITH Consulta AS (
	 SELECT
	  P.IdPadre
	 ,P.IdPadreSubcontratista 
	 ,P.PadreSubcontratista
	 ,P.IdEmpresa
	 ,p.IdColaborador
	 ,rtrim(ltrim(P.Nombres +' '+ P.ApellidoPaterno +' '+ ApellidoMaterno))Nombre
	 ,P.ApellidoPaterno
	 ,P.ApellidoMaterno
	 ,P.Genero
	 ,(convert(varchar(10),P.FechaNacimiento,103) )FechaNacimiento
	 ,P.IdUbicacion
	 ,P.Ubicacion
	 ,P.IdDepartamento
	 ,P.Departamento
	 ,p.Entidad
	 ,P.Empresa
	 ,p.Ubigeo
	 ,p.Categoria
	 ,p.Regimen
	 ,p.Condicion
	 ,p.Estado 
	 ,P.Digitalizacion
	 FROM(
	   SELECT CO.IdEmpresa,IdColaborador,CO.IdPersona,CO.Nombres,CO.ApellidoPaterno,CO.ApellidoMaterno,
	   (CASE WHEN CO.Genero ='M'THEN 'MASCULINO'ELSE 'FEMENINO' END)Genero,
	   CO.FechaNacimiento,UB.IdUbicacion,UB.Nombre Ubicacion,DE.IdDepartamento,DE.Nombre Departamento,EN.Nombre Entidad,
	    EM.RazonSocial Empresa,UBG.Nombre Ubigeo,CA.Nombre Categoria,REG.Nombre Regimen,COND.Nombre Condicion ,ES.Nombre Estado,
		ISNULL((SELECT top 1 (CASE WHEN   UPPER(ES.Nombre) IS NULL THEN 'REGISTRO' ELSE UPPER(ES.Nombre) END) FROM Utb_Digitalizacion DIG JOIN Utb_Estado ES ON DIG.IdEstado = ES.IdEstado WHERE  IdPersona = CO.IdPersona ),'REGISTRO')Digitalizacion,
	  
	  (SELECT  E.IdPadre from Utb_Empresa E where  E.IdEmpresa = CO.IdEmpresa   )IdPadre,
	 (SELECT  E.IdPadre from Utb_Empresa E where  E.IdEmpresa = (SELECT E.IdPadre FROM Utb_Empresa E WHERE E.IdEmpresa = CO.IdEmpresa )  )IdPadreSubcontratista,
	 (CASE WHEN EM.EsPrincipal>0 THEN EM.RazonSocial 
	 ELSE 
	   (SELECT  (CASE WHEN E.IdPadre IS NULL 
	                  THEN (SELECT EM.RazonSocial FROM Utb_Empresa EM WHERE EM.IdEmpresa = E.IdEmpresa)
			   ELSE (SELECT EM.RazonSocial FROM Utb_Empresa EM WHERE EM.IdEmpresa = E.IdPadre) END ) from Utb_Empresa E where  E.IdEmpresa = EM.IdPadre   )
	  END)PadreSubcontratista
	 FROM Utb_Colaborador CO
	 left JOIN Utb_Ubicacion UB ON CO.IdUbicacion = UB.IdUbicacion
	 left JOIN Utb_Departamento DE ON CO.IdDepartamento = DE.IdDepartamento
	 left JOIN Utb_Persona PER ON CO.IdPersona = PER.IdPersona
	 left JOIN Utb_Entidad EN ON CO.IdEntidad = EN.IdEntidad
	 left JOIN Utb_Estado ES ON PER.IdEstado = ES.IdEstado
	 left JOIN Utb_Empresa EM ON CO.IdEmpresa = EM.IdEmpresa
	 left JOIN Utb_Ubigeo UBG ON CO.IdDitrito = UBG.IdDepartamento+UBG.IdProvincia+UBG.IdDistrito 
	 left JOIN Utb_Categoria CA ON CO.IdCategoria = CA.IdCategoria
	 left JOIN Utb_Regimen REG ON CO.IdRegimen = REG.IdRegimen
	 left JOIN Utb_Condicion COND ON CO.IdCondicion = COND.IdCondicion
	 WHERE ES.IdEstado <> 'ES0203'
	 )P


 " + 
	@WhereFilter + 
	") SELECT *
	FROM Consulta
	CROSS JOIN (SELECT Count(*) AS Cantidad FROM Consulta) AS CC
	ORDER BY " +
	CASE WHEN ISNULL(@OrderBy,'') != '' THEN (@OrderBy) 
	ELSE " IdColaborador DESC " END
	    + ' ' +
	" OFFSET " + CONVERT(VARCHAR, (@Start)) + 
	" ROWS FETCH NEXT " + CONVERT(VARCHAR, @AmountRows) + " ROWS ONLY"
	PRINT (@SentenciaSQL);
    EXECUTE sp_executesql  @stmt = @SentenciaSQL

END
 
 	 
GO
/****** Object:  StoredProcedure [dbo].[Sp_ColaboradorTarjetaEliminar]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_ColaboradorTarjetaEliminar]
@IdPersona char(15),
@IdTipo CHAR(6),
@Nombre VARCHAR(20)
AS
BEGIN

	 delete from  Utb_TarjetProximidad WHERE IdTarjetProximidad=@IdTipo AND IdPersona=@IdPersona
END
GO
/****** Object:  StoredProcedure [dbo].[Sp_ColaboradorTarjetaListado]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_ColaboradorTarjetaListado]
@IdPersona char(15)
AS
BEGIN
 SELECT IdPersona, IdTarjetProximidad IdTipo, NumeroTrajeta Nombre, '' Tipo from Utb_TarjetProximidad  WHERE IdPersona = @IdPersona
END
GO
/****** Object:  StoredProcedure [dbo].[Sp_ColaboradorTarjetaMantenimiento]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_ColaboradorTarjetaMantenimiento]
@IdPersona char(15),
@IdTipo CHAR(6)= null,
@Nombre VARCHAR(20)
AS
BEGIN
  IF NOT EXISTS(SELECT TOP 1 IdTarjetProximidad FROM Utb_TarjetProximidad WHERE IdTarjetProximidad = @IdTipo AND IdPersona= @IdPersona)
    BEGIN
	  SET  @IdTipo = ( CASE WHEN (SELECT COUNT(*) FROM Utb_TarjetProximidad )>0 THEN (SELECT 'TA' + RIGHT('000'+CONVERT(VARCHAR(4), SUBSTRING( MAX(IdTarjetProximidad),3,LEN(MAX(IdTarjetProximidad)))+1), 4) FROM Utb_TarjetProximidad) ELSE 'TA0001' END);
      INSERT INTO  Utb_TarjetProximidad (IdTarjetProximidad,IdPersona,IdEstado,NumeroTrajeta)VALUES(@IdTipo,@IdPersona,'ES0201',@Nombre)
	   select @IdPersona result
	END
  ELSE
    BEGIN
	 UPDATE Utb_TarjetProximidad SET  NumeroTrajeta=@Nombre WHERE IdTarjetProximidad=@IdTipo AND IdPersona=@IdPersona
	  select @IdPersona result
	END
END
GO
/****** Object:  StoredProcedure [dbo].[Sp_ColaboradorTelefonoEliminar]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_ColaboradorTelefonoEliminar]
@IdPersona char(15),
@IdTipo CHAR(6),
@Nombre VARCHAR(20)
AS
BEGIN

	  delete from  Utb_PersonaOperador WHERE  IdOperador=@IdTipo AND IdPersona=@IdPersona
END
GO
/****** Object:  StoredProcedure [dbo].[Sp_ColaboradorTelefonoListado]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_ColaboradorTelefonoListado]
@IdPersona char(15)
AS
BEGIN
 SELECT op.IdPersona, op.IdOperador IdTipo, op.Numero Nombre, o.Nombre Tipo from Utb_PersonaOperador op join Utb_Operador o on op.IdOperador = o.IdOperador  WHERE IdPersona = @IdPersona
END
GO
/****** Object:  StoredProcedure [dbo].[Sp_ColaboradorTelefonoMantenimiento]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_ColaboradorTelefonoMantenimiento]
@IdPersona char(15),
@IdTipo CHAR(6),
@Nombre VARCHAR(20)
AS
BEGIN
 
    IF NOT EXISTS(SELECT * FROM Utb_PersonaOperador WHERE IdPersona = @IdPersona AND IdOperador = @IdTipo)
    BEGIN
	   INSERT INTO  Utb_PersonaOperador(IdOperador,IdPersona,Numero)VALUES(@IdTipo,@IdPersona,@Nombre)
	    select @IdPersona result
	END
  ELSE
    BEGIN
	  UPDATE  Utb_PersonaOperador SET Numero = @Nombre WHERE  IdOperador=@IdTipo AND IdPersona=@IdPersona
	   select @IdPersona result
	END
END
GO
/****** Object:  StoredProcedure [dbo].[Sp_CondicionConsulta]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_CondicionConsulta]
@IdCondicion char(6)
AS
BEGIN
SELECT 
P.IdCondicion,
P.Nombre,
P.IdRegimen,
P.Regimen,
P.IdEstado,
P.Estado FROM (
SELECT IdCondicion
      ,CON.Nombre
      ,CON.IdRegimen
      ,(CASE WHEN ES.IdEstado='ES0201' THEN 'true' ELSE  'false'END)IdEstado
	  ,ES.Nombre Estado
	  ,RE.Nombre Regimen
  FROM Utb_Condicion CON
  JOIN Utb_Estado ES ON CON.IdEstado = ES.IdEstado
  JOIN Utb_Regimen RE ON CON.IdRegimen = RE.IdRegimen
)P WHERE P.IdCondicion = @IdCondicion
END
GO
/****** Object:  StoredProcedure [dbo].[Sp_CondicionDropDown]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_CondicionDropDown]
@IdConsulta varchar(60) = ''
AS
BEGIN
SELECT IdCondicion Value
      ,Nombre,'' Valor1
  FROM Utb_Condicion  where (@IdConsulta='' or IdEmpresa = @IdConsulta)

END
GO
/****** Object:  StoredProcedure [dbo].[Sp_CondicionEliminar]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROC [dbo].[Sp_CondicionEliminar]
@IdCondicion char(6)
AS
BEGIN
BEGIN TRY
		--Existe 
     IF  EXISTS (SELECT top 1 IdCondicion from Utb_Condicion where IdCondicion = @IdCondicion)
	  BEGIN
	  	 UPDATE Utb_Condicion SET IdEstado = 'ES0203'  WHERE IdCondicion=@IdCondicion
	  END
	  ELSE
	  BEGIN
	  	SELECT -1 AS IdConfiguracion
	  END

END TRY
BEGIN CATCH 
	SELECT -2 AS IdConfiguracion
END CATCH
end
GO
/****** Object:  StoredProcedure [dbo].[Sp_CondicionListado]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_CondicionListado]
@Nombre varchar(60)
AS
BEGIN
SELECT 
P.IdCondicion,
P.Nombre,
P.IdRegimen,
P.Regimen,
P.IdEstado,
P.Estado FROM (
SELECT IdCondicion
      ,CON.Nombre
      ,CON.IdRegimen
      ,ES.IdEstado
	  ,ES.Nombre Estado
	  ,RE.Nombre Regimen
  FROM Utb_Condicion CON
  JOIN Utb_Estado ES ON CON.IdEstado = ES.IdEstado
  JOIN Utb_Regimen RE ON CON.IdRegimen = RE.IdRegimen
)P WHERE P.Nombre = @Nombre
END
GO
/****** Object:  StoredProcedure [dbo].[Sp_CondicionMantenimiento]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_CondicionMantenimiento]
@IdCondicion CHAR(6)=NULL,
@IdRegimen CHAR(6)=NULL,
@Nombre VARCHAR(50),
@IdEstado VARCHAR(10) = NULL,
@IdEmpresa char(6) = null
AS
SET @IdEstado =(CASE WHEN @IdEstado ='TRUE' THEN 'ES0201' ELSE 'ES0202' END)
BEGIN
BEGIN TRY
--Registrar
	IF(@IdCondicion = '')
	BEGIN
		--Existe 
		SET  @IdCondicion = ( CASE WHEN (SELECT COUNT(*) FROM Utb_Condicion )>0 THEN (SELECT 'CN' + RIGHT('000'+CONVERT(VARCHAR(4), SUBSTRING( MAX(IdCondicion),3,LEN(MAX(IdCondicion)))+1), 4) FROM Utb_Condicion) ELSE 'CN0001' END);
		IF NOT EXISTS (SELECT top 1 IdCondicion from Utb_Condicion where IdCondicion = @IdCondicion)
		BEGIN
			 INSERT INTO Utb_Condicion(IdCondicion,Nombre,IdRegimen,IdEstado,IdEmpresa)
			        VALUES(@IdCondicion,@Nombre,@IdRegimen,@IdEstado,@IdEmpresa)
		END
		ELSE
		BEGIN
			SELECT -1 AS REGISTRO
		END
	END

--Actualizar
    ELSE
	BEGIN
		IF EXISTS (SELECT top 1 IdCondicion from Utb_Condicion where IdCondicion = @IdCondicion)
		BEGIN
		    UPDATE Utb_Condicion SET Nombre=@Nombre,IdRegimen =@IdRegimen,IdEstado=IdEstado,@IdEmpresa = @IdEmpresa WHERE IdCondicion=@IdCondicion
		END
		ELSE
		BEGIN
			SELECT -1 AS ACTUALIZAR
		END

	END

END TRY
BEGIN CATCH 
	SELECT -2 AS ERROR
END CATCH
end
GO
/****** Object:  StoredProcedure [dbo].[SP_CondicionPaginado]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

-- =====================================================
-- Author:  LA
-- Create date: 15/04/2018
-- Description: Listar Area
-- Test:EXEC [dbo].[SP_CondicionPaginado] @WhereFilter = "WHERE P.Estado <> '' ", @OrderBy = 'IdCondicion ASC', @Start = 0, @AmountRows = 10
-- =====================================================
CREATE PROCEDURE [dbo].[SP_CondicionPaginado]
@WhereFilter VARCHAR(MAX) = '',
@OrderBy VARCHAR (100) = '', 
@Start INT = 0,
@AmountRows INT = 0
AS
BEGIN
SET NOCOUNT ON;
    DECLARE @SentenciaSQL nvarchar(3072)
	SET @SentenciaSQL =  "

	;WITH Consulta AS (
	SELECT 
	P.IdCondicion,
	P.Nombre,
	P.IdRegimen,
	P.Regimen,
	P.IdEstado,
	P.Estado,
	P.IdEmpresa FROM (
	SELECT IdCondicion
		  ,CON.Nombre
		  ,CON.IdRegimen
		  ,ES.IdEstado
		  ,ES.Nombre Estado
		  ,RE.Nombre Regimen
		  ,CON.IdEmpresa
	  FROM Utb_Condicion CON
	  JOIN Utb_Estado ES ON CON.IdEstado = ES.IdEstado
	  JOIN Utb_Regimen RE ON CON.IdRegimen = RE.IdRegimen
	  WHERE ES.IdEstado <> 'ES0203'
	)P
	" + 
	@WhereFilter + 
	") SELECT *
	FROM Consulta
	CROSS JOIN (SELECT Count(*) AS Cantidad FROM Consulta) AS CC
	ORDER BY " +
	CASE WHEN ISNULL(@OrderBy,'') != '' THEN (@OrderBy) 
	ELSE " IdCondicion DESC " END
	    + ' ' +
	" OFFSET " + CONVERT(VARCHAR, (@Start)) + 
	" ROWS FETCH NEXT " + CONVERT(VARCHAR, @AmountRows) + " ROWS ONLY"
	PRINT (@SentenciaSQL);
    EXECUTE sp_executesql  @stmt = @SentenciaSQL

END


GO
/****** Object:  StoredProcedure [dbo].[Sp_ConfiguracionConsulta]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_ConfiguracionConsulta]
@IdConfiguracion  CHAR(6)
AS
BEGIN
SELECT [IdConfiguracion]
      ,[Nombre]
      ,[TiempoColor]
      ,[TiempoEntreMarcaciones]
      ,[TiempoRELAY]
      ,[IdTipo]
	  ,IdEmpresa 
      ,(CASE WHEN IdEstado='ES0201' THEN 'true' ELSE  'false'END)IdEstado
  FROM [dbo].[Utb_Configuracion] WHERE IdConfiguracion = @IdConfiguracion
END
GO
/****** Object:  StoredProcedure [dbo].[Sp_ConfiguracionDropDown]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
--Sp_DepartamentoDropDown ''
CREATE PROC [dbo].[Sp_ConfiguracionDropDown]
@IdConsulta varchar(15)
AS
BEGIN
 SELECT IdConfiguracion Value, Nombre FROM Utb_Configuracion where IdEmpresa = @IdConsulta and IdEstado <> 'ES0203'
END
GO
/****** Object:  StoredProcedure [dbo].[Sp_ConfiguracionEliminar]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_ConfiguracionEliminar]
@IdConfiguracion char(6)
AS
BEGIN
BEGIN TRY
		--Existe 
     IF  EXISTS (SELECT top 1 IdConfiguracion from Utb_Configuracion where IdConfiguracion = @IdConfiguracion)
	  BEGIN
	  	 UPDATE Utb_Configuracion SET IdEstado = 'ES0203'  WHERE IdConfiguracion=@IdConfiguracion
	  END
	  ELSE
	  BEGIN
	  	SELECT -1 AS IdConfiguracion
	  END

END TRY
BEGIN CATCH 
	SELECT -2 AS IdConfiguracion
END CATCH
end
GO
/****** Object:  StoredProcedure [dbo].[Sp_ConfiguracionMantenimiento]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_ConfiguracionMantenimiento]
@IdConfiguracion char(6),
@Nombre char(50),
@TiempoColor varchar(100),
@TiempoEntreMarcaciones int,
@TiempoRELAY int,
@IdTipo char(6) = null,
@IdEstado CHAR(6) = NULL,
@IdEmpresa  CHAR(6)
AS
SET @IdEstado =(CASE WHEN @IdEstado ='TRUE' THEN 'ES0201' ELSE 'ES0202' END)
BEGIN
BEGIN TRY
--Registrar
	IF(@IdConfiguracion = '')
	BEGIN
		--Existe 
		IF NOT EXISTS (SELECT top 1 IdConfiguracion from Utb_Configuracion where IdConfiguracion = @IdConfiguracion)
		BEGIN
				SET  @IdConfiguracion = ( CASE WHEN (SELECT COUNT(*) FROM Utb_Categoria )>0 THEN (SELECT 'CO' + RIGHT('000'+CONVERT(VARCHAR(4), SUBSTRING( MAX(IdConfiguracion),3,LEN(MAX(IdConfiguracion)))+1), 4) FROM Utb_Configuracion) ELSE 'CO0001' END);
			 INSERT INTO Utb_Configuracion (IdConfiguracion,Nombre,TiempoColor,TiempoEntreMarcaciones,TiempoRELAY,IdTipo,IdEstado,IdEmpresa )
			        VALUES(@IdConfiguracion,@Nombre,@TiempoColor,@TiempoEntreMarcaciones,@TiempoRELAY,'TI0200',@IdEstado,@IdEmpresa)
		END
		ELSE
		BEGIN
			SELECT -1 AS IdConfiguracion
		END
	END

--Actualizar
    ELSE
	BEGIN
		IF  EXISTS (SELECT top 1 IdConfiguracion from Utb_Configuracion where IdConfiguracion = @IdConfiguracion)
		BEGIN
		    UPDATE Utb_Configuracion SET Nombre=@Nombre,TiempoColor=@TiempoColor,TiempoEntreMarcaciones=@TiempoEntreMarcaciones,TiempoRELAY=@TiempoRELAY,IdEstado=@IdEstado,IdEmpresa=@IdEmpresa WHERE IdConfiguracion = @IdConfiguracion
		END
		ELSE
		BEGIN
			SELECT -1 AS IdConfiguracion
		END

	END

END TRY
BEGIN CATCH 
	SELECT -2 AS IdConfiguracion
END CATCH
end
GO
/****** Object:  StoredProcedure [dbo].[Sp_ConfiguracionPaginado]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

-- =====================================================
-- Author:  LA
-- Create date: 15/04/2018
-- Description: Listar Configuracion
-- Test:EXEC [dbo].[SP_ConfiguracionPaginado] @WhereFilter = "WHERE P.Estado <> '' ", @OrderBy = 'IdConfiguracion ASC', @Start = 0, @AmountRows = 10
-- =====================================================
CREATE PROCEDURE [dbo].[Sp_ConfiguracionPaginado]
@WhereFilter VARCHAR(MAX) = '',
@OrderBy VARCHAR (100) = '', 
@Start INT = 0,
@AmountRows INT = 0
AS
BEGIN
SET NOCOUNT ON;
    DECLARE @SentenciaSQL nvarchar(3072)
	SET @SentenciaSQL =  "
	;WITH Consulta AS (
	 SELECT 
	  p.IdConfiguracion
	 ,P.Nombre
	 ,P.TiempoColor
	 ,P.TiempoEntreMarcaciones
	 ,P.TiempoRELAY
	 ,P.IdTipo
	 ,P.Tipo
	 ,p.Estado 
	 ,p.IdEmpresa 
	 FROM(
	   SELECT IdConfiguracion,CO.Nombre,CO.TiempoColor,CO.TiempoEntreMarcaciones,CO.TiempoRELAY,CO.IdTipo,TIP.Nombre Tipo,ES.Descripcion Estado,co.IdEmpresa 
	 FROM Utb_Configuracion CO
	 JOIN Utb_Tipo TIP ON CO.IdTipo = TIP.IdTipo
	 JOIN Utb_Estado ES ON CO.IdEstado = ES.IdEstado
	 WHERE ES.IdEstado <> 'ES0203'
	 )P
 " + 
	@WhereFilter + 
	") SELECT *
	FROM Consulta
	CROSS JOIN (SELECT Count(*) AS Cantidad FROM Consulta) AS CC
	ORDER BY " +
	CASE WHEN ISNULL(@OrderBy,'') != '' THEN (@OrderBy) 
	ELSE " IdConfiguracion DESC " END
	    + ' ' +
	" OFFSET " + CONVERT(VARCHAR, (@Start)) + 
	" ROWS FETCH NEXT " + CONVERT(VARCHAR, @AmountRows) + " ROWS ONLY"
	PRINT (@SentenciaSQL);
    EXECUTE sp_executesql  @stmt = @SentenciaSQL

END
GO
/****** Object:  StoredProcedure [dbo].[Sp_CorreDigitalizacionListado]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO


CREATE PROCEDURE [dbo].[Sp_CorreDigitalizacionListado]
@IdEmpresa VARCHAR(6) = '',
@Tipo VARCHAR(100)
AS
BEGIN
	 IF(@Tipo='COLABORADOR')
	 BEGIN
	  SELECT * FROM(
	    SELECT 
		  co.IdPersona, CO.Nombres+' '+ ISNULL(CO.ApellidoPaterno,'') +' '+ ISNULL(CO.ApellidoMaterno,'')Nombre,CO.NumeroDocumento Numero ,EM.RazonSocial Empresa,
		 (SELECT COUNT(*) FROM Utb_Digitalizacion DI WHERE CO.IdPersona = DI.IdPersona 	 )EnDigitalizacion,
	     (SELECT top 1 Enviado FROM Utb_Digitalizacion DI WHERE CO.IdPersona = DI.IdPersona 	 )Enviado,
		 UPPER((SELECT top 1 ES.Nombre FROM Utb_Digitalizacion DI JOIN Utb_Estado ES ON DI.IdEstado = ES.IdEstado   WHERE CO.IdPersona = DI.IdPersona)) EstadoDigitalizacion,
	     (SELECT Nombre FROM Utb_Categoria WHERE IdCategoria = CO.IdCategoria)Categoria,CO.IdEmpresa,
	     (CASE WHEN (SELECT EsPrincipal FROM Utb_Empresa WHERE IdEmpresa = CO.IdEmpresa)= 1 THEN CO.IdEmpresa
	         WHEN (SELECT EsContratista FROM Utb_Empresa WHERE IdEmpresa = CO.IdEmpresa)= 1   THEN (SELECT  E.IdPadre from Utb_Empresa E where  E.IdEmpresa = CO.IdEmpresa)
	      ELSE (SELECT  E.IdPadre from Utb_Empresa E where  E.IdEmpresa = (SELECT E.IdPadre FROM Utb_Empresa E WHERE E.IdEmpresa = CO.IdEmpresa )) END)IdEmpresaGeneral,
		 (CASE 
		     WHEN (SELECT EsContratista FROM Utb_Empresa WHERE IdEmpresa = CO.IdEmpresa)= 1   THEN (SELECT  E.IdPadre from Utb_Empresa E where  E.IdEmpresa = CO.IdEmpresa)
	         WHEN (SELECT EsSubContratista FROM Utb_Empresa WHERE IdEmpresa = CO.IdEmpresa)= 1   THEN (SELECT  E.IdPadre from Utb_Empresa E where  E.IdEmpresa = CO.IdEmpresa)
	         END)IdEmpresaPadre,
		 (CASE WHEN EM.EsPrincipal>0 THEN 'PRINCIPAL'
	         WHEN EM.EsContratista>0 THEN 'CONTRATISTA'
	      ELSE 'SUBCONTRATISTA' END )TipoEmpresa
	 from Utb_Colaborador CO
	 JOIN Utb_Empresa EM ON CO.IdEmpresa = EM.IdEmpresa
	 )P WHERE P.EnDigitalizacion >0 AND P.IdEmpresaGeneral = @IdEmpresa
	 END
   ELSE IF(@Tipo ='EMPRESA')
    BEGIN
	  SELECT * FROM(
	    SELECT 
		  em.IdPersona, EM.RazonSocial Nombre,EM.RUC Numero ,EM.RazonSocial Empresa,
		 (SELECT COUNT(*) FROM Utb_Digitalizacion DI WHERE EM.IdPersona = DI.IdPersona 	 )EnDigitalizacion,
	     (SELECT top 1 Enviado FROM Utb_Digitalizacion DI WHERE EM.IdPersona = DI.IdPersona 	 )Enviado,
		 UPPER((SELECT top 1 ES.Nombre FROM Utb_Digitalizacion DI JOIN Utb_Estado ES ON DI.IdEstado = ES.IdEstado   WHERE EM.IdPersona = DI.IdPersona)) EstadoDigitalizacion,
	     --(SELECT Nombre FROM Utb_Categoria WHERE IdCategoria = EM.IdCategoria)Categoria,
		 EM.IdEmpresa,
	     (CASE WHEN (SELECT EsPrincipal FROM Utb_Empresa WHERE IdEmpresa = EM.IdEmpresa)= 1 THEN EM.IdEmpresa
	         WHEN (SELECT EsContratista FROM Utb_Empresa WHERE IdEmpresa = EM.IdEmpresa)= 1   THEN (SELECT  E.IdPadre from Utb_Empresa E where  E.IdEmpresa = EM.IdEmpresa)
	      ELSE (SELECT  E.IdPadre from Utb_Empresa E where  E.IdEmpresa = (SELECT E.IdPadre FROM Utb_Empresa E WHERE E.IdEmpresa =	EM.IdEmpresa )) END)IdEmpresaGeneral,
		 (CASE 
		     WHEN (SELECT EsContratista FROM Utb_Empresa WHERE IdEmpresa = EM.IdEmpresa)= 1   THEN (SELECT  E.IdPadre from Utb_Empresa E where  E.IdEmpresa = EM.IdEmpresa)
	         WHEN (SELECT EsSubContratista FROM Utb_Empresa WHERE IdEmpresa = EM.IdEmpresa)= 1   THEN (SELECT  E.IdPadre from Utb_Empresa E where  E.IdEmpresa = EM.IdEmpresa)
	         END)IdEmpresaPadre,
		 (CASE WHEN EM.EsPrincipal>0 THEN 'PRINCIPAL'
	         WHEN EM.EsContratista>0 THEN 'CONTRATISTA'
	      ELSE 'SUBCONTRATISTA' END )TipoEmpresa
	 from Utb_Empresa EM
	 )P WHERE P.EnDigitalizacion >0 AND P.IdEmpresaGeneral = @IdEmpresa
	END
END


--SELECT * FROM Utb_Digitalizacion

--SELECT STUFF(
--    (SELECT ', ' + D.IdDigitalizacion
--FROM Utb_Digitalizacion D
--    FOR XML PATH ('')),
--1,2, '')

GO
/****** Object:  StoredProcedure [dbo].[Sp_DepartamentoConsulta]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_DepartamentoConsulta]
@IdDepartamento CHAR(6)
AS
BEGIN
SELECT [IdDepartamento]
      ,[Nombre]
      ,[Nivel]
      ,[IdPrincipal],
	  (CASE WHEN IdEstado='ES0201' THEN 'true' ELSE  'false'END)IdEstado
  FROM [dbo].[Utb_Departamento] WHERE IdDepartamento = @IdDepartamento 
END


GO
/****** Object:  StoredProcedure [dbo].[Sp_DepartamentoDropDown]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
--Sp_DepartamentoDropDown ''
CREATE PROC [dbo].[Sp_DepartamentoDropDown]
@IdConsulta varchar(15) = null
AS
BEGIN
 SELECT IdDepartamento Value, Nombre FROM Utb_Departamento where (@IdConsulta = null or IdEmpresa =@IdConsulta) and IdEstado <> 'ES0203'
END
GO
/****** Object:  StoredProcedure [dbo].[Sp_DepartamentoEliminar]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_DepartamentoEliminar]
@IdDepartamento char(6)
AS
BEGIN
BEGIN TRY
		--Existe 
     IF  EXISTS (SELECT top 1 IdDepartamento from Utb_Departamento where IdDepartamento = @IdDepartamento)
	  BEGIN
	     update Utb_Departamento set IdEstado = 'ES0203'  where IdDepartamento=@IdDepartamento
	  END
	  ELSE
	  BEGIN
	  	SELECT -1 AS IdEmpresa
	  END

END TRY
BEGIN CATCH 
	SELECT -2 AS IdEmpresa
END CATCH
end
GO
/****** Object:  StoredProcedure [dbo].[Sp_DepartamentoMantenimiento]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_DepartamentoMantenimiento]
@IdDepartamento CHAR(6),
@Nombre VARCHAR(50),
@IdPrincipal VARCHAR(6) = NULL,
@IdEstado varchar(6),
@IdEmpresa char(6)
AS
SET @IdEstado =(CASE WHEN @IdEstado ='TRUE' THEN 'ES0201' ELSE 'ES0202' END)
BEGIN
BEGIN TRY
--Registrar
	IF(@IdDepartamento= '')
	BEGIN
		--Existe 
		IF NOT EXISTS (SELECT top 1 IdDepartamento from Utb_Departamento where IdDepartamento = @IdDepartamento)
		BEGIN
			SET  @IdDepartamento = ( CASE WHEN (SELECT COUNT(*) FROM Utb_Departamento )>0 THEN (SELECT 'DE' + RIGHT('000'+CONVERT(VARCHAR(4), SUBSTRING( MAX(IdDepartamento),4,LEN(MAX(IdDepartamento)))+1), 4) FROM Utb_Departamento) ELSE 'DE0001' END);

			 INSERT INTO Utb_Departamento(IdDepartamento,Nombre,Nivel,IdEstado,IdEmpresa)
			        VALUES(@IdDepartamento,@Nombre,5,@IdEstado,@IdEmpresa)
		END
		ELSE
		BEGIN
			SELECT -1 AS IdEmpresa
		END
	END

--Actualizar
    ELSE
	BEGIN
		IF NOT EXISTS (SELECT top 1 IdDepartamento from Utb_Departamento where @IdDepartamento != @IdDepartamento)
		BEGIN
		    UPDATE Utb_Departamento SET Nombre =@Nombre,IdEstado=@IdEstado,IdEmpresa=@IdEmpresa where IdDepartamento=@IdDepartamento
		END
		ELSE
		BEGIN
			SELECT -1 AS IdEmpresa
		END

	END

END TRY
BEGIN CATCH 
	SELECT -2 AS IdEmpresa
END CATCH
end
GO
/****** Object:  StoredProcedure [dbo].[Sp_DepartamentoPaginado]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

-- =====================================================
-- Author:  LA
-- Create date: 15/04/2018
-- Description: Listar Departamento
-- Test:EXEC [dbo].[SP_DepartamentoPaginado] @WhereFilter = "WHERE P.Estado <> '' ", @OrderBy = 'IdDepartamento ASC', @Start = 0, @AmountRows = 10
-- =====================================================
CREATE PROCEDURE [dbo].[Sp_DepartamentoPaginado]
@WhereFilter VARCHAR(MAX) = '',
@OrderBy VARCHAR (100) = '', 
@Start INT = 0,
@AmountRows INT = 0
AS
BEGIN
SET NOCOUNT ON;
    DECLARE @SentenciaSQL nvarchar(3072)
	SET @SentenciaSQL =  "
	;WITH Consulta AS (
	 SELECT 
	  p.IdDepartamento
	 ,P.Nombre
	 ,P.Nivel
	 ,P.IdPrincipal
	 ,P.Estado
	 ,p.IdEmpresa
	 FROM(
	   SELECT IdDepartamento,Nombre,Nivel,IdPrincipal, 'ACTIVO' Estado,IdEmpresa
	 FROM Utb_Departamento 
	 WHERE IdEstado <> 'ES0203'
	 )P
 " + 
	@WhereFilter + 
	") SELECT *
	FROM Consulta
	CROSS JOIN (SELECT Count(*) AS Cantidad FROM Consulta) AS CC
	ORDER BY " +
	CASE WHEN ISNULL(@OrderBy,'') != '' THEN (@OrderBy) 
	ELSE " IdDepartamento DESC " END
	    + ' ' +
	" OFFSET " + CONVERT(VARCHAR, (@Start)) + 
	" ROWS FETCH NEXT " + CONVERT(VARCHAR, @AmountRows) + " ROWS ONLY"
	PRINT (@SentenciaSQL);
    EXECUTE sp_executesql  @stmt = @SentenciaSQL

END
GO
/****** Object:  StoredProcedure [dbo].[Sp_DigitalizacionEliminar]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROC [dbo].[Sp_DigitalizacionEliminar]
@IdDigitalizacion varchar(15)
AS
DECLARE @Observacion VARCHAR(MAX) = (select top 1 Observacion from Utb_DocumentoAdjunto where IdDigitalizacion = @IdDigitalizacion)
BEGIN
 update Utb_Digitalizacion set Observacion = @Observacion WHERE IdDigitalizacion =@IdDigitalizacion 
 DELETE FROM  Utb_DocumentoAdjunto WHERE IdDigitalizacion =@IdDigitalizacion
END
GO
/****** Object:  StoredProcedure [dbo].[Sp_DigitalizacionMantenimiento]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_DigitalizacionMantenimiento]
@IdDigitalizacion CHAR(15) ,
@IdDocumento CHAR(6)=NULL,
@IdPersona CHAR(15)=NULL ,
@Observacion VARCHAR(255) = NULL,
@FechaVencimiento DATETIME = NULL,
@IdEstado VARCHAR(200)=NULL,
@Opcion VARCHAR(200)=NULL,
@IdUsuario char(6)=NULL
AS
SET @IdEstado =(CASE WHEN @IdEstado ='REGISTRO' THEN 'ES0301' 
                     WHEN @IdEstado ='REVISION' THEN 'ES0302' 
				     ELSE 'ES0303' END)
IF(@IdPersona IS NULL)
 SET @IdPersona =(SELECT TOP 1 IdPersona FROM Utb_Digitalizacion WHERE IdDigitalizacion = @IdDigitalizacion)
BEGIN
BEGIN TRY
--Registrar
	IF(@IdDigitalizacion = '')
	BEGIN
		--Existe 
	    SET  @IdDigitalizacion = ( CASE WHEN (SELECT COUNT(*) FROM Utb_Digitalizacion )>0 THEN (SELECT 'DI' + RIGHT('0000000000000'+CONVERT(VARCHAR(13), SUBSTRING( MAX(IdDigitalizacion),3,LEN(MAX(IdDigitalizacion)))+1), 13) FROM Utb_Digitalizacion) ELSE 'DI0000000000001' END);
		IF NOT EXISTS (SELECT top 1 IdDigitalizacion from Utb_Digitalizacion where IdDigitalizacion = @IdDigitalizacion)
		BEGIN
		     SELECT @IdDigitalizacion AS REGISTRO
			 INSERT INTO Utb_Digitalizacion(IdDigitalizacion,IdDocumento,IdPersona,Observacion,FechaVencimiento,IdEstado,Enviado,IdUsuario)
			        VALUES(@IdDigitalizacion,@IdDocumento,@IdPersona,@Observacion,@FechaVencimiento,@IdEstado,0,@IdUsuario)

			 UPDATE Utb_Colaborador SET Enviado = 0, IdUsuario= @IdUsuario where IdPersona =@IdPersona
			 SELECT @IdDigitalizacion AS REGISTRO
		END
		ELSE
		BEGIN
			SELECT -1 AS REGISTRO
		END
	END

--Actualizar
    ELSE
	BEGIN
		IF EXISTS (SELECT top 1 IdDigitalizacion from Utb_Digitalizacion where IdDigitalizacion = @IdDigitalizacion)
		BEGIN
		     SELECT @IdDigitalizacion AS REGISTRO
		     IF(@Opcion!='' OR @Opcion IS NOT NULL)
			   BEGIN
			    IF(@Opcion='APROBAR')
			  	 BEGIN
				  UPDATE Utb_Digitalizacion SET Enviado = 0, IdUsuario =@IdUsuario WHERE IdDigitalizacion = @IdDigitalizacion
			      UPDATE Utb_DocumentoAdjunto SET  IdEstado ='ES0401' WHERE IdDigitalizacion = @IdDigitalizacion
				  UPDATE Utb_Colaborador SET Enviado = 1, IdUsuario= @IdUsuario where IdPersona =@IdPersona
			  	 END
			    IF(@Opcion='DESAPROBAR')
			  	 BEGIN
				  UPDATE Utb_Digitalizacion SET Enviado = 0, IdUsuario =@IdUsuario WHERE IdDigitalizacion = @IdDigitalizacion
			      UPDATE Utb_DocumentoAdjunto SET  IdEstado ='ES0402',Observacion=@Observacion WHERE IdDigitalizacion = @IdDigitalizacion
				  UPDATE Utb_Colaborador SET Enviado = 1, IdUsuario= @IdUsuario where IdPersona =@IdPersona
			  	 END
			   END
			 ELSE
			 BEGIN
			   UPDATE Utb_Digitalizacion SET IdEstado=@IdEstado WHERE IdPersona=@IdPersona 
			   UPDATE Utb_Digitalizacion SET Enviado = 0, IdUsuario =@IdUsuario WHERE IdDigitalizacion = @IdDigitalizacion
			 END
			 SELECT @IdDigitalizacion AS ACTUALIZAR
		END
		ELSE
		BEGIN
			SELECT -1 AS ACTUALIZAR
		END

	END

END TRY
BEGIN CATCH 
	SELECT -2 AS Error
END CATCH
end
GO
/****** Object:  StoredProcedure [dbo].[Sp_DistritoDropDown]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
--Sp_DistritoDropDown '01'
CREATE PROC [dbo].[Sp_DistritoDropDown]
@IdConsulta varchar(15),
@IdConsulta2 varchar(15)
AS
BEGIN
 select IdDistrito Value,Nombre,''Valor1 from Utb_Ubigeo where IdDistrito<>'00' and IdDistrito<>'01'  and  IdDepartamento = @IdConsulta2 AND IdProvincia = @IdConsulta
END
GO
/****** Object:  StoredProcedure [dbo].[Sp_DocumentoAdjuntoEliminar]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_DocumentoAdjuntoEliminar]
@IdDocumentoAdjunto CHAR(15)
AS
BEGIN
BEGIN TRY
		--Existe 
     IF  EXISTS (SELECT top 1 IdDocumentoAdjunto from Utb_DocumentoAdjunto where IdDocumentoAdjunto = @IdDocumentoAdjunto)
	  BEGIN
	  	 DELETE FROM  Utb_DocumentoAdjunto   WHERE IdDocumentoAdjunto=@IdDocumentoAdjunto
	  END
	  ELSE
	  BEGIN
	  	SELECT -1 AS IdEmpresa
	  END

END TRY
BEGIN CATCH 
	SELECT -2 AS IdEmpresa
END CATCH
end
GO
/****** Object:  StoredProcedure [dbo].[Sp_DocumentoAdjuntoListado]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

-- =====================================================
-- Author:  LA
-- Create date: 15/04/2018
-- Description: Listar DocumentoAdjunto
-- Test: Sp_DocumentoAdjuntoListado 'DI0000000000001'
-- =====================================================
CREATE PROCEDURE [dbo].[Sp_DocumentoAdjuntoListado]
@IdDigitalizacion CHAR(15)
AS
BEGIN

 SELECT 
	  p.IdDocumentoAdjunto
	 ,P.IdDigitalizacion
	 ,P.FechaVencimiento
	 ,P.Observacion
	 ,P.Documento
	 ,P.Vencimiento
	 ,P.Obligatorio
	 ,P.Ruta
	 ,P.Fecha
	 ,P.Nombre
	 ,P.Estado
	 FROM(
	   SELECT IdDocumentoAdjunto,DIG.IdDigitalizacion,CONVERT(varchar(10),DIG.FechaVencimiento,103) FechaVencimiento,DIG.Observacion,DOC.Nombre Documento,(CASE WHEN DOC.FechaVencimiento =1 THEN 'SI' ELSE 'NO' END) Vencimiento,DOA.Ruta,
	   CONVERT(varchar(10),DOA.Fecha,103) Fecha,DOA.Nombre, (CASE WHEN DOC.Obligatorio=1 THEN 'SI' ELSE 'NO' END) Obligatorio, ES.Nombre Estado
	 FROM Utb_DocumentoAdjunto DOA
	 JOIN Utb_Digitalizacion DIG ON doa.IdDigitalizacion = DIG.IdDigitalizacion
	 JOIN Utb_Documento DOC ON DIG.IdDocumento = DOC.IdDocumento
	 JOIN Utb_Estado ES ON  DOA.IdEstado = ES.IdEstado
	 )P WHERE P.IdDigitalizacion = @IdDigitalizacion
END
GO
/****** Object:  StoredProcedure [dbo].[Sp_DocumentoAdjuntoMantenimiento]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_DocumentoAdjuntoMantenimiento]
@IdDocumentoAdjunto CHAR(15),
@IdDigitalizacion CHAR(15),
@Nombre VARCHAR(50),
@Ruta VARCHAR(250),
@Fecha DATETIME,
@IdPersona CHAR(15),
@IdDocumento CHAR(6)
AS
BEGIN
BEGIN TRY
--Registrar
	IF(@IdDocumentoAdjunto = '')
	BEGIN
		--Existe 
		IF NOT EXISTS (SELECT top 1 IdDocumentoAdjunto from Utb_DocumentoAdjunto where IdDocumentoAdjunto = @IdDocumentoAdjunto)
		BEGIN
		   SET  @IdDocumentoAdjunto = ( CASE WHEN (SELECT COUNT(*) FROM Utb_DocumentoAdjunto )>0 THEN (SELECT 'DO' + RIGHT('0000000000000'+CONVERT(VARCHAR(13), SUBSTRING( MAX(IdDocumentoAdjunto),3,LEN(MAX(IdDocumentoAdjunto)))+1), 13) FROM Utb_DocumentoAdjunto) ELSE 'DO0000000000001' END);

			 INSERT INTO Utb_DocumentoAdjunto (IdDocumentoAdjunto,IdDigitalizacion,Nombre,Ruta,IdDocumento,Fecha,IdPersona,IdEstado)
			        VALUES(@IdDocumentoAdjunto,@IdDigitalizacion,@Nombre,@Ruta,@IdDocumento,@Fecha,@IdPersona,'ES0201')

		END
		ELSE
		BEGIN
			SELECT -1 AS IdDocumentoAdjunto
		END
	END

--Actualizar
    ELSE
	BEGIN
		IF  EXISTS (SELECT top 1 IdDocumentoAdjunto from Utb_DocumentoAdjunto where IdDocumentoAdjunto = @IdDocumentoAdjunto)
		BEGIN
		    UPDATE Utb_DocumentoAdjunto SET Nombre=@Nombre,Ruta=@Ruta,Fecha=@Fecha,IdPersona=@IdPersona,IdDocumento=@IdDocumento WHERE IdDocumentoAdjunto = @IdDocumentoAdjunto
		END
		ELSE
		BEGIN
			SELECT -1 AS IdDocumentoAdjunto
		END

	END

END TRY
BEGIN CATCH 
	SELECT -2 AS IdDocumentoAdjunto
END CATCH
end
GO
/****** Object:  StoredProcedure [dbo].[Sp_DocumentoAdjuntoPaginado]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

-- =====================================================
-- Author:  LA
-- Create date: 15/04/2018
-- Description: Listar DocumentoAdjunto
-- Test:EXEC [dbo].[SP_DocumentoAdjuntoPaginado] @WhereFilter = "WHERE P.Estado <> '' ", @OrderBy = 'IdDocumentoAdjunto ASC', @Start = 0, @AmountRows = 10
-- =====================================================
CREATE PROCEDURE [dbo].[Sp_DocumentoAdjuntoPaginado]
@WhereFilter VARCHAR(MAX) = '',
@OrderBy VARCHAR (100) = '', 
@Start INT = 0,
@AmountRows INT = 0
AS
BEGIN
SET NOCOUNT ON;
    DECLARE @SentenciaSQL nvarchar(3072)
	SET @SentenciaSQL =  "
	;WITH Consulta AS (
	 SELECT 
	  p.IdDocumentoAdjunto
	 ,P.IdDigitalizacion
	 ,P.IdPersona
	 ,P.FechaVencimiento
	 ,P.Observacion
	 ,P.Documento
	 ,P.Vencimiento
	 ,P.Obligatorio
	 ,P.IdEntidad
	 ,P.Entidad
	 ,P.Estado
	 FROM(
	   SELECT IdDocumentoAdjunto,DIG.IdDigitalizacion,PER.IdPersona,DIG.FechaVencimiento,DIG.Observacion,DOC.Nombre Documento,DOC.FechaVencimiento Vencimiento,DOC.Obligatorio,EN.IdEntidad,EN.Nombre Entidad, ES.Nombre Estado
	 FROM Utb_DocumentoAdjunto DOA
	 JOIN Utb_Persona PER ON DOA.IdPersona = PER.IdPersona
	 JOIN Utb_Entidad EN ON PER.IdEntidad = EN.IdEntidad
	 JOIN Utb_Digitalizacion DIG ON doa.IdDigitalizacion = DIG.IdDigitalizacion
	 JOIN Utb_Documento DOC ON DIG.IdDocumento = DOC.IdDocumento
	 JOIN Utb_Estado ES ON  DOA.IdEstado = ES.IdEstado
	 )P
 " + 
	@WhereFilter + 
	") SELECT *
	FROM Consulta
	CROSS JOIN (SELECT Count(*) AS Cantidad FROM Consulta) AS CC
	ORDER BY " +
	CASE WHEN ISNULL(@OrderBy,'') != '' THEN (@OrderBy) 
	ELSE " IdDocumentoAdjunto DESC " END
	    + ' ' +
	" OFFSET " + CONVERT(VARCHAR, (@Start)) + 
	" ROWS FETCH NEXT " + CONVERT(VARCHAR, @AmountRows) + " ROWS ONLY"
	PRINT (@SentenciaSQL);
    EXECUTE sp_executesql  @stmt = @SentenciaSQL

END
GO
/****** Object:  StoredProcedure [dbo].[Sp_DocumentoConsulta]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_DocumentoConsulta]
@IdDocumento CHAR(6)
AS
BEGIN
 SELECT [IdDocumento]
      ,[Nombre]
      ,[Descripcion]
      ,[FechaVencimiento]
      ,[Obligatorio]
      ,(CASE WHEN IdEstado='ES0201' THEN 'true' ELSE  'false'END)IdEstado
      ,[IdEntidad]
  FROM [dbo].[Utb_Documento] WHERE IdDocumento = @IdDocumento
END




GO
/****** Object:  StoredProcedure [dbo].[Sp_DocumentoEliminar]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_DocumentoEliminar]
@IdDocumento INT
AS
BEGIN
BEGIN TRY
		--Existe 
     IF  EXISTS (SELECT top 1 IdDocumento from Utb_Documento where IdDocumento = @IdDocumento)
	  BEGIN
	  	 UPDATE Utb_Documento SET IdEstado = 'ES0203'  WHERE IdDocumento=@IdDocumento
	  END
	  ELSE
	  BEGIN
	  	SELECT -1 AS IdEmpresa
	  END

END TRY
BEGIN CATCH 
	SELECT -2 AS IdEmpresa
END CATCH
end
GO
/****** Object:  StoredProcedure [dbo].[Sp_DocumentoMantenimiento]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_DocumentoMantenimiento]
@IdDocumento CHAR(6),
@Nombre VARCHAR(50),
@Descripcion VARCHAR(250),
@FechaVencimiento INT,
@Obligatorio INT,
@IdCategoria VARCHAR(MAX),
@IdEntidad char(6),
@IdEstado varchar(6),
@IdEmpresa char(6)
AS
DELETE FROM Utb_CategoriaDocumento WHERE IdDocumento = @IdDocumento
SET @IdEstado =(CASE WHEN @IdEstado ='TRUE' THEN 'ES0201' ELSE 'ES0202' END)
DECLARE @list varchar(8000)
DECLARE @pos INT
DECLARE @len INT
DECLARE @value varchar(8000)

SET @list = @IdCategoria

set @pos = 0
set @len = 0
BEGIN
BEGIN TRY
--Registrar
	IF(@IdDocumento = '')
	BEGIN
	    SET  @IdDocumento = ( CASE WHEN (SELECT COUNT(*) FROM Utb_Documento )>0 THEN (SELECT 'DO' + RIGHT('000'+CONVERT(VARCHAR(4), SUBSTRING( MAX(IdDocumento),3,LEN(MAX(IdDocumento)))+1), 4) FROM Utb_Documento) ELSE 'DO0001' END);
		--Existe 
		IF NOT EXISTS (SELECT top 1 IdDocumento from Utb_Documento where IdDocumento = @IdDocumento)
		BEGIN
			 INSERT INTO Utb_Documento (IdDocumento,Nombre,Descripcion,FechaVencimiento,Obligatorio,IdEntidad,IdEstado,IdEmpresa)
			        VALUES(@IdDocumento,@Nombre,@Descripcion,@FechaVencimiento,@Obligatorio,@IdEntidad,@IdEstado,@IdEmpresa)
		END
		ELSE
		BEGIN
			SELECT -1 AS IdDocumento
		END
	END

--Actualizar
    ELSE
	BEGIN
		IF  EXISTS (SELECT top 1 IdDocumento from Utb_Documento where IdDocumento = @IdDocumento)
		BEGIN
		    UPDATE Utb_Documento SET Nombre=@Nombre,Descripcion=@Descripcion,FechaVencimiento=@FechaVencimiento,Obligatorio=@Obligatorio,IdEntidad=@IdEntidad,IdEstado=@IdEstado,IdEmpresa=@IdEmpresa WHERE IdDocumento = @IdDocumento
		END
		ELSE
		BEGIN
			SELECT -1 AS IdDocumento
		END

	END
 WHILE CHARINDEX(',', @list, @pos+1)>0
	BEGIN
		set @len = CHARINDEX(',', @list, @pos+1) - @pos
		set @value = SUBSTRING(@list, @pos, @len)
            
		PRINT @value -- for debug porpose   
		INSERT INTO Utb_CategoriaDocumento(IdDocumento,IdCategoria,IdEntidad)VALUES(@IdDocumento,@value,@IdEntidad)
		--DO YOUR MAGIC HERE

		set @pos = CHARINDEX(',', @list, @pos+@len) +1
	END
END TRY
BEGIN CATCH 
	SELECT 100 AS IdDocumento
END CATCH
end

GO
/****** Object:  StoredProcedure [dbo].[Sp_DocumentoPaginado]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

-- =====================================================
-- Author:  LA
-- Create date: 15/04/2018
-- Description: Listar Documento
-- Test:EXEC [dbo].[SP_DocumentoPaginado] @WhereFilter = "WHERE P.Estado <> '' ", @OrderBy = 'IdDocumento ASC', @Start = 0, @AmountRows = 10
-- =====================================================
CREATE PROCEDURE [dbo].[Sp_DocumentoPaginado]
@WhereFilter VARCHAR(MAX) = '',
@OrderBy VARCHAR (100) = '', 
@Start INT = 0,
@AmountRows INT = 0
AS
BEGIN
SET NOCOUNT ON;
    DECLARE @SentenciaSQL nvarchar(3072)
	SET @SentenciaSQL =  "
	;WITH Consulta AS (
	SELECT 
	  p.IdDocumento
	 ,P.Nombre
	 ,P.Descripcion
	 ,P.FechaVencimiento
	 ,P.Obligatorio
	 ,P.Categoria
	 ,P.Estado
	 ,p.IdEmpresa
	 FROM(
	   SELECT IdDocumento,DO.Nombre,DO.Descripcion,DO.FechaVencimiento,DO.Obligatorio,
	   ISNULL( (	 SELECT STUFF(
       (SELECT ', ' + CA.Nombre
		FROM Utb_CategoriaDocumento ENC
		JOIN Utb_Categoria CA ON ENC.IdCategoria = CA.IdCategoria 
		WHERE ENC.IdDocumento = DO.IdDocumento
		FOR XML PATH ('')),
		1,2, '')),'') Categoria, ES.Nombre Estado,DO.IdEmpresa
	 FROM Utb_Documento DO
	 JOIN Utb_Estado ES ON DO.IdEstado = ES.IdEstado
	 )P
 " + 
	@WhereFilter + 
	") SELECT *
	FROM Consulta
	CROSS JOIN (SELECT Count(*) AS Cantidad FROM Consulta) AS CC
	ORDER BY " +
	CASE WHEN ISNULL(@OrderBy,'') != '' THEN (@OrderBy) 
	ELSE " IdDocumento DESC " END
	    + ' ' +
	" OFFSET " + CONVERT(VARCHAR, (@Start)) + 
	" ROWS FETCH NEXT " + CONVERT(VARCHAR, @AmountRows) + " ROWS ONLY"
	PRINT (@SentenciaSQL);
    EXECUTE sp_executesql  @stmt = @SentenciaSQL

END
GO
/****** Object:  StoredProcedure [dbo].[Sp_EmailDropDown]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROC [dbo].[Sp_EmailDropDown]
@IdConsulta char(6)
AS
BEGIN
SELECT IdEmail Value
      ,Nombre,'' Valor1
  FROM Utb_Email 
END
GO
/****** Object:  StoredProcedure [dbo].[Sp_EmpresaConsulta]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_EmpresaConsulta]
@IdEmpresa char(6)
as
begin
SELECT EM.IdEmpresa
      ,[IdPersona]
      ,[RazonSocial]
      ,[RUC]
      ,[DireccionFiscal]
      ,[IdPadre]
      ,EN.IdEntidad,
	  EN.Nombre Entidad
      ,(CASE WHEN ES.IdEstado='ES0201' THEN 'true' ELSE  'false'END)IdEstado,
	  ES.Nombre Estado
	  ,CA.IdCategoria
	  ,CA.Nombre Categoria
	  ,CONVERT(int,EM.EsContratista) EsContratista
	  ,CONVERT(int,EM.EsPrincipal) EsPrincipal 
	  ,CONVERT(int,EM.EsSubContratista) EsSubContratista
  FROM [DASys].[dbo].[Utb_Empresa] EM
  JOIN Utb_Estado ES ON EM.IdEstado = ES.IdEstado
  JOIN Utb_Entidad EN ON EM.IdEntidad = EN.IdEntidad
  LEFT JOIN Utb_Categoria CA ON EM.IdCategoria = CA.IdCategoria
   WHERE EM.IdEmpresa = @IdEmpresa
end
GO
/****** Object:  StoredProcedure [dbo].[Sp_EmpresaDigitalizacionExcel]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[Sp_EmpresaDigitalizacionExcel]
@IdPersona char(15)
AS
BEGIN
	SELECT P.Codigo,
	 p.Nombre
	,P.Numero,P.Empresa,P.Categoria,P.Documento,
	(CASE WHEN P.ObservacionDocumento IS NULL THEN P.ObservacionDigitalizacion ELSE P.ObservacionDocumento END)Observacion
	,P.Obligatorio,P.FechaVencimiento Vencimiento,P.Estado,P.EstadoDigitalizacion from(
	SELECT EM.IdEmpresa Codigo, EM.RazonSocial Nombre,EM.RUC Numero,
   (SELECT top 1 Observacion FROM Utb_DocumentoAdjunto  WHERE IdDocumento = DOC.IdDocumento  AND IdPersona = EM.IdPersona )ObservacionDocumento,
    ISNULL((SELECT top 1 Observacion FROM Utb_Digitalizacion  WHERE IdDocumento = DOC.IdDocumento AND  IdPersona = EM.IdPersona ),'')ObservacionDigitalizacion,
	EM.RazonSocial Empresa,CA.Nombre Categoria,DOC.Nombre Documento,
    (CASE WHEN DOC.Obligatorio=1  THEN  'SI' ELSE 'NO' END) Obligatorio,
	(CASE WHEN DOC.FechaVencimiento=1  THEN  'SI' ELSE 'NO' END) FechaVencimiento,
	ISNULL((SELECT TOP 1 (CASE WHEN DOAC.IdEstado='ES0401'  THEN  'APROBADO' ELSE 'DESAPROBADO' END) FROM Utb_DocumentoAdjunto DOAC JOIN Utb_Estado ES ON DOAC.IdEstado = ES.IdEstado WHERE  DOAC.IdDocumento = DOC.IdDocumento  AND DOAC.IdPersona = EM.IdPersona ),'')Estado
    ,ISNULL((SELECT top 1 ES.Nombre FROM Utb_Digitalizacion D JOIN Utb_Estado ES ON D.IdEstado = ES.IdEstado WHERE D.IdPersona = EM.IdPersona),'SIN DIGITALIZAR')EstadoDigitalizacion
	,(SELECT COUNT(*) FROM Utb_DocumentoAdjunto DD WHERE DD.IdPersona = EM.IdPersona AND IdEstado = 'ES0401'   )TotalAprobados
	,(SELECT COUNT(*) FROM Utb_DocumentoAdjunto DD WHERE DD.IdPersona = EM.IdPersona AND IdEstado = 'ES0402'   )TotalDesaprobados
	,(SELECT COUNT(*) FROM Utb_DocumentoAdjunto DD WHERE DD.IdPersona = EM.IdPersona )TotalDocumento
  FROM Utb_Empresa EM 
  JOIN Utb_CategoriaEndidad CADE ON EM.IdEntidad = CADE.IdEntidad and CADE.IdCategoria = EM.IdCategoria
  JOIN Utb_Categoria CA ON EM.IdCategoria = CA.IdCategoria
  JOIN Utb_CategoriaDocumento CADO ON EM.IdCategoria = CADO.IdCategoria AND CADO.IdEntidad = EM.IdEntidad
  JOIN Utb_Documento DOC ON CADO.IdDocumento = DOC.IdDocumento
  WHERE EM.Enviado = 1 and EM.IdPersona = @IdPersona
 )P

END

--SELECT * FROM Utb_Colaborador WHERE IdColaborador = 'CO0000000000013'
GO
/****** Object:  StoredProcedure [dbo].[Sp_EmpresaDigitalizacionPaginado]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

-- =====================================================
-- Author:  LA
-- Create date: 15/04/2018
-- Description: Listar EmpresaDigitalizacion
-- Test:EXEC [dbo].[SP_EmpresaDigitalizacionPaginado] @WhereFilter = "WHERE P.Estado <> '' ", @OrderBy = 'IdEmpresa ASC', @Start = 0, @AmountRows = 10
-- =====================================================
CREATE PROCEDURE [dbo].[Sp_EmpresaDigitalizacionPaginado]
@WhereFilter VARCHAR(MAX) = '',
@OrderBy VARCHAR (100) = '', 
@Start INT = 0,
@AmountRows INT = 0
AS
BEGIN
SET NOCOUNT ON;
    DECLARE @SentenciaSQL nvarchar(3072)
	SET @SentenciaSQL =  "
	;WITH Consulta AS (
			  SELECT *from(
	SELECT EMP.IdEmpresa, EMP.IdPersona, EMP.RazonSocial,EMP.IdCategoria,CA.Nombre Categoria,DOC.IdDocumento,DOC.Nombre Documento,
    (Select COUNT(*) from Utb_DocumentoAdjunto where IdPersona = EMP.IdPersona and IdDocumento =DOC.IdDocumento)Adjuntado,(CASE WHEN DOC.Obligatorio=1  THEN  'SI' ELSE 'NO' END) Obligatorio, (CASE WHEN DOC.FechaVencimiento=1  THEN  'SI' ELSE 'NO' END) FechaVencimiento,
    ISNULL((SELECT top 1 (CASE WHEN  UPPER(ES.Nombre) IS NULL THEN 'REGISTRO' ELSE UPPER(ES.Nombre) END) FROM Utb_Digitalizacion DIG JOIN Utb_Estado ES ON DIG.IdEstado = ES.IdEstado WHERE IdDocumento = DOC.IdDocumento  AND IdPersona = EMP.IdPersona ),'REGISTRO')Estado,
	(SELECT top 1 Observacion FROM Utb_Digitalizacion WHERE IdDocumento = DOC.IdDocumento  AND IdPersona = EMP.IdPersona )Observacion,
	(SELECT top 1 IdDocumentoAdjunto FROM Utb_DocumentoAdjunto WHERE IdDocumento = DOC.IdDocumento  AND IdPersona = EMP.IdPersona )IdDocumentoAdjunto,
	(SELECT top 1 IdDigitalizacion FROM Utb_Digitalizacion WHERE IdDocumento = DOC.IdDocumento  AND IdPersona = EMP.IdPersona )IdDigitalizacion,
	ISNULL((SELECT TOP 1 (CASE WHEN ES.IdEstado='ES0401'  THEN  'APROBADO' ELSE 'DESAPROBADO' END) FROM Utb_DocumentoAdjunto DOAC JOIN Utb_Estado ES ON DOAC.IdEstado = ES.IdEstado WHERE ES.IdEstado BETWEEN 'ES0401' AND 'ES0499' AND DOAC.IdDocumento = DOC.IdDocumento  AND DOAC.IdPersona = EMP.IdPersona ),'')EstadoAdjunto,
	ISNULL((SELECT TOP 1 DOAC.Observacion FROM Utb_DocumentoAdjunto DOAC JOIN Utb_Estado ES ON DOAC.IdEstado = ES.IdEstado WHERE ES.IdEstado BETWEEN 'ES0401' AND 'ES0499' AND DOAC.IdDocumento = DOC.IdDocumento  AND DOAC.IdPersona = EMP.IdPersona ),'')ObservacionAdjunto
  FROM Utb_Empresa EMP 
  LEFT JOIN Utb_CategoriaEndidad CADE ON EMP.IdEntidad = CADE.IdEntidad and CADE.IdCategoria = EMP.IdCategoria
  LEFT JOIN Utb_Categoria CA ON EMP.IdCategoria = CA.IdCategoria
  LEFT JOIN Utb_CategoriaDocumento CADO ON EMP.IdCategoria = CADO.IdCategoria AND CADO.IdEntidad = EMP.IdEntidad
  LEFT JOIN Utb_Documento DOC ON CADO.IdDocumento = DOC.IdDocumento
  LEFT JOIN Utb_Persona PER ON EMP.IdPersona = PER.IdPersona )P
 
 " + 
	@WhereFilter + 
	") SELECT *
	FROM Consulta
	CROSS JOIN (SELECT Count(*) AS Cantidad FROM Consulta) AS CC
	ORDER BY " +
	CASE WHEN ISNULL(@OrderBy,'') != '' THEN (@OrderBy) 
	ELSE " IdEmpresa DESC " END
	    + ' ' +
	" OFFSET " + CONVERT(VARCHAR, (@Start)) + 
	" ROWS FETCH NEXT " + CONVERT(VARCHAR, @AmountRows) + " ROWS ONLY"
	PRINT (@SentenciaSQL);
    EXECUTE sp_executesql  @stmt = @SentenciaSQL

END





GO
/****** Object:  StoredProcedure [dbo].[Sp_EmpresaDropDown]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
--Sp_EmpresaDropDown ''
CREATE PROC [dbo].[Sp_EmpresaDropDown]
@IdConsulta varchar(15)
AS
BEGIN

IF(@IdConsulta ='PRINCIPAL')
BEGIN
    SELECT IdEmpresa Value, RazonSocial+'  - '+(CASE WHEN EsPrincipal>0 THEN 'PRINCIPAL'
	         WHEN EsContratista>0 THEN 'CONTRATISTA'
	    ELSE 'SUBCONTRATISTA' END ) Nombre,IdPadre Valor1 FROM Utb_Empresa
END
ELSE IF(@IdConsulta ='CONTRATISTA')
BEGIN
    SELECT IdEmpresa Value, RazonSocial+'  - '+(CASE WHEN EsPrincipal>0 THEN 'PRINCIPAL'
	         WHEN EsContratista>0 THEN 'CONTRATISTA'
	    ELSE 'SUBCONTRATISTA' END ) Nombre,IdPadre Valor1 FROM Utb_Empresa WHERE EsContratista = 1	OR EsSubContratista = 1
END
ELSE IF(@IdConsulta ='SUBCONTRATISTA')
BEGIN
   SELECT IdEmpresa Value, RazonSocial +'  - '+(CASE WHEN EsPrincipal>0 THEN 'PRINCIPAL'
	         WHEN EsContratista>0 THEN 'CONTRATISTA'
	    ELSE 'SUBCONTRATISTA' END ) Nombre,IdPadre Valor1 FROM Utb_Empresa WHERE EsSubContratista = 1
END
ELSE
 IF(@IdConsulta='')
  BEGIN
  SELECT * FROM (
       SELECT EMP.IdEmpresa Value, EMP.RazonSocial Nombre,'' Valor1, (select COUNT(IdEmpresa) from Utb_Usuario where IdEmpresa = EMP.IdEmpresa )Total FROM Utb_Empresa EMP WHERE EsPrincipal = 1
  )P WHERE P.Total <2
  END
 ELSE
  BEGIN
    SELECT IdEmpresa Value, RazonSocial Nombre,IdPadre Valor1 FROM Utb_Empresa WHERE EsPrincipal = 1
  END
END
GO
/****** Object:  StoredProcedure [dbo].[Sp_EmpresaEliminar]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_EmpresaEliminar]
@IdEmpresa char(6)
AS
BEGIN
BEGIN TRY
		--SI EXISTE CONTRATA
		IF NOT EXISTS(SELECT TOP 1 IdPadre FROM  Utb_Empresa WHERE IdEmpresa = @IdEmpresa) 
		 BEGIN
	  	   UPDATE Utb_Empresa SET IdEstado = 'ES0203'  WHERE IdEmpresa=@IdEmpresa
		   select 'Se elimino correctamente' as Result
		 END
        ELSE 
		 BEGIN
		 IF ((SELECT TOP 1 IdPadre FROM Utb_Empresa WHERE IdEmpresa = 'EM0001') IS NULL)
		  BEGIN
		     IF((SELECT COUNT(*) FROM Utb_Empresa WHERE IdPadre = @IdEmpresa)=0)
			   BEGIN
			    UPDATE Utb_Empresa SET IdEstado = 'ES0203'  WHERE IdEmpresa=@IdEmpresa
			   END
			 ELSE
			  BEGIN
			       select 'No se puede eliminar, tiene empresas contratadas ' as Result
			  END
		  END
		 ELSE 
		   BEGIN
		     DECLARE @EMPRESA VARCHAR(200)= (SELECT TOP 1 RazonSocial FROM Utb_Empresa WHERE IdEmpresa = (SELECT TOP 1 IdPadre FROM Utb_Empresa WHERE IdEmpresa = 'EM0001') )
		     select 'No se puede eliminar, hay contrata con la empresa :' + @EMPRESA as Result
		   END
		 END
 

END TRY
BEGIN CATCH 
	SELECT 'ocurrio un error al eliminar' as Result
END CATCH
end
GO
/****** Object:  StoredProcedure [dbo].[Sp_EmpresaListado]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_EmpresaListado]
@IdEmpresa char(6)
as
begin
SELECT P.IdEmpresa,P.IdPersona,P.RazonSocial,P.RUC,P.DireccionFiscal,P.IdPadre,P.IdEmpresa,P.Entidad,P.IdEstado,P.Estado,P.IdCategoria,P.Categoria,P.EsContratista,P.EsPrincipal,P.EsSubContratista,
P.TotalAprobados,P.TotalDesaprobados,P.TotalDocumento,P.Enviado,P.IdUsuario,p.Digitalizado,
('Empresa: '+P.RazonSocial+', Se aprobò '+CONVERT(varchar,P.TotalAprobados)+ ' de '+CONVERT(varchar,P.TotalDocumento) +' documentos adjuntados')Mensaje FROM (
SELECT EM.IdEmpresa
      ,[IdPersona]
      ,[RazonSocial]
      ,[RUC]
      ,[DireccionFiscal]
      ,[IdPadre]
      ,EN.IdEntidad,
	  EN.Nombre Entidad
      ,(CASE WHEN ES.IdEstado='ES0201' THEN 'true' ELSE  'false'END)IdEstado,
	  ES.Nombre Estado
	  ,CA.IdCategoria
	  ,CA.Nombre Categoria
	  ,CONVERT(int,EM.EsContratista) EsContratista
	  ,CONVERT(int,EM.EsPrincipal) EsPrincipal 
	  ,CONVERT(int,EM.EsSubContratista) EsSubContratista
	  ,(SELECT COUNT(*) FROM Utb_DocumentoAdjunto DD WHERE DD.IdPersona = EM.IdPersona AND IdEstado = 'ES0401'   )TotalAprobados
	   ,(SELECT COUNT(*) FROM Utb_DocumentoAdjunto DD WHERE DD.IdPersona = EM.IdPersona AND IdEstado = 'ES0402'   )TotalDesaprobados
	   ,(SELECT COUNT(*) FROM Utb_DocumentoAdjunto DD WHERE DD.IdPersona = EM.IdPersona )TotalDocumento
	   ,(SELECT COUNT(*) FROM Utb_Digitalizacion DD WHERE DD.IdPersona = EM.IdPersona )Digitalizado
	   ,EM.Enviado
	   ,EM.IdUsuario
  FROM [DASys].[dbo].[Utb_Empresa] EM
  JOIN Utb_Estado ES ON EM.IdEstado = ES.IdEstado
  JOIN Utb_Entidad EN ON EM.IdEntidad = EN.IdEntidad
  LEFT JOIN Utb_Categoria CA ON EM.IdCategoria = CA.IdCategoria
     WHERE EM.IdEmpresa = @IdEmpresa
)P
end
GO
/****** Object:  StoredProcedure [dbo].[Sp_EmpresaMantenimiento]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_EmpresaMantenimiento]
@IdEmpresa CHAR(6),
@IdPadre CHAR(6)=NULL,
@IdCategoria  CHAR(6) = NULL,
@RazonSocial VARCHAR(100),
@RUC CHAR(11),
@DireccionFiscal VARCHAR(250),
@EsPrincipal INT = NULL,
@EsContratista INT = NULL,
@EsSubContratista INT = NULL,
@IdEstado varchar(6)
AS
SET @IdEstado =(CASE WHEN @IdEstado ='TRUE' THEN 'ES0201' ELSE 'ES0202' END)
BEGIN
BEGIN TRY
--Registrar
	IF(@IdEmpresa = '')
	BEGIN
		DECLARE @IdPersona CHAR(15) = ( CASE WHEN (SELECT COUNT(*) FROM Utb_Persona )>0 THEN (SELECT 'PE' + RIGHT('0000000000000'+CONVERT(VARCHAR(13), SUBSTRING( MAX(IdPersona),3,LEN(MAX(IdPersona)))+1), 13) FROM Utb_Persona) ELSE 'PE0000000000001' END);
		INSERT INTO Utb_Persona(IdPersona,IdEntidad,IdEstado)VALUES(@IdPersona,'EN0100','ES0101')
	    SET  @IdEmpresa = ( CASE WHEN (SELECT COUNT(*) FROM Utb_Empresa )>0 THEN (SELECT 'EM' + RIGHT('000'+CONVERT(VARCHAR(4), SUBSTRING( MAX(IdEmpresa),4,LEN(MAX(IdEmpresa)))+1), 4) FROM Utb_Empresa) ELSE 'EM0001' END);
		--Existe 
		IF NOT EXISTS (SELECT top 1 IdEmpresa from Utb_Empresa where RazonSocial = @RazonSocial)
		BEGIN
			 INSERT INTO Utb_Empresa (IdEmpresa,IdPadre,RazonSocial,RUC,IdEntidad,DireccionFiscal,EsPrincipal,EsContratista,EsSubContratista,IdEstado,IdPersona,IdCategoria)
			        VALUES(@IdEmpresa,@IdPadre,@RazonSocial,@RUC,'EN0105',@DireccionFiscal,@EsPrincipal,@EsContratista,@EsSubContratista,@IdEstado,@IdPersona,@IdCategoria)
		END
		ELSE
		BEGIN
			SELECT -1 AS IdEmpresa
		END
	END

--Actualizar
   ELSE
	BEGIN
		IF EXISTS (SELECT top 1 IdEmpresa from Utb_Empresa where RazonSocial = RazonSocial AND IdEmpresa=@IdEmpresa)
		BEGIN
		    UPDATE Utb_Empresa SET IdPadre=@IdPadre, RazonSocial=@RazonSocial,RUC=@RUC,DireccionFiscal=@DireccionFiscal
			,EsPrincipal=@EsPrincipal,EsContratista=@EsContratista,EsSubContratista=@EsSubContratista,IdEstado=@IdEstado,IdCategoria = @IdCategoria WHERE IdEmpresa=@IdEmpresa
		END
		ELSE
		BEGIN
			SELECT -1 AS IdEmpresa
		END

	END

END TRY
BEGIN CATCH 
	SELECT -2 AS IdEmpresa
END CATCH
end
GO
/****** Object:  StoredProcedure [dbo].[Sp_EmpresaPaginado]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

-- =====================================================
-- Author:  LA
-- Create date: 15/04/2018
-- Description: Listar Empresa
-- Test:EXEC [dbo].[SP_EmpresaPaginado] @WhereFilter = "WHERE P.Estado != ''  AND P.IdEmpresa ='EM0001' OR P.IdPadre ='EM0001' OR P.IdPadreSubcontratista ='EM0001' AND P.RazonSocial LIKE '%'+'ss'+'%' ", @OrderBy = 'IdEmpresa ASC', @Start = 0, @AmountRows = 10
-- =====================================================
CREATE PROCEDURE [dbo].[Sp_EmpresaPaginado]
@WhereFilter VARCHAR(MAX) = '',
@OrderBy VARCHAR (100) = '', 
@Start INT = 0,
@AmountRows INT = 0
AS
BEGIN
SET NOCOUNT ON;
    DECLARE @SentenciaSQL nvarchar(max)
	SET @SentenciaSQL =  "
	;WITH Consulta AS (
	SELECT 
	  p.IdEmpresa
	 ,P.IdPersona
	 ,P.RazonSocial
	 ,P.RUC
	 ,P.DireccionFiscal
	 ,CONVERT(int,P.EsPrincipal)EsPrincipal
	 ,CONVERT(int,P.EsContratista)EsContratista
	 ,CONVERT(int,P.EsSubContratista)EsSubContratista
	 ,P.IdEntidad
	 ,P.Entidad
	 ,P.Estado
	 ,P.TipoEmpresa
	 ,P.IdPadre
	 ,P.IdPadreSubcontratista
	 ,P.PadreSubcontratista
	 ,P.IdCategoria
	 ,P.Categoria
	 ,p.Digitalizacion
	 FROM(
	   SELECT EMP.IdPadre,EMP.IdEmpresa,PER.IdPersona,EMP.RazonSocial,EMP.RUC,EMP.DireccionFiscal, EMP.EsPrincipal,EMP.EsContratista,EMP.EsSubContratista,EN.IdEntidad,EN.Nombre Entidad, ES.Nombre Estado,
	   (CASE WHEN EMP.EsPrincipal>0 THEN 'PRINCIPAL'
	         WHEN EMP.EsContratista>0 THEN 'CONTRATISTA'
	    ELSE 'SUBCONTRATISTA' END )TipoEmpresa,
		ISNULL((SELECT top 1 (CASE WHEN   UPPER(ES.Nombre) IS NULL THEN 'REGISTRO' ELSE UPPER(ES.Nombre) END) FROM Utb_Digitalizacion DIG JOIN Utb_Estado ES ON DIG.IdEstado = ES.IdEstado WHERE  IdPersona = EMP.IdPersona ),'REGISTRO')Digitalizacion,
	 (SELECT  E.IdPadre from Utb_Empresa E where  E.IdEmpresa = EMP.IdPadre   )IdPadreSubcontratista,
	 (CASE WHEN EMP.EsPrincipal>0 THEN EMP.RazonSocial 
	 ELSE 
	   (SELECT  (CASE WHEN E.IdPadre IS NULL 
	                  THEN (SELECT EM.RazonSocial FROM Utb_Empresa EM WHERE EM.IdEmpresa = E.IdEmpresa)
			   ELSE (SELECT EM.RazonSocial FROM Utb_Empresa EM WHERE EM.IdEmpresa = E.IdPadre) END ) from Utb_Empresa E where  E.IdEmpresa = EMP.IdPadre   )
	  END)PadreSubcontratista,
	  CA.Nombre Categoria,
	  CA.IdCategoria
	 FROM Utb_Empresa EMP
	 JOIN Utb_Persona PER ON EMP.IdPersona = PER.IdPersona
	 JOIN Utb_Entidad EN ON EMP.IdEntidad = EN.IdEntidad
	 JOIN Utb_Estado ES ON  EMP.IdEstado = ES.IdEstado
	 LEFT JOIN Utb_Categoria CA ON EMP.IdCategoria = CA.IdCategoria
	 WHERE ES.IdEstado <> 'ES0203'
	 )P

 " + 
	@WhereFilter + 
	") SELECT *
	FROM Consulta
	CROSS JOIN (SELECT Count(*) AS Cantidad FROM Consulta) AS CC
	ORDER BY " +
	CASE WHEN ISNULL(@OrderBy,'') != '' THEN (@OrderBy) 
	ELSE " IdEmpresa DESC " END
	    + ' ' +
	" OFFSET " + CONVERT(VARCHAR, (@Start)) + 
	" ROWS FETCH NEXT " + CONVERT(VARCHAR, @AmountRows) + " ROWS ONLY"
	PRINT (@SentenciaSQL);
    EXECUTE sp_executesql  @stmt = @SentenciaSQL

END


GO
/****** Object:  StoredProcedure [dbo].[Sp_EntidadConsulta]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_EntidadConsulta]
@IdEntidad char(6)
as
begin
SELECT EN.IdEntidad
      ,EN.Nombre
      ,EN.IdPrincipal
      ,(CASE WHEN ES.IdEstado='ES0201' THEN 'true' ELSE  'false'END)IdEstado
	  ,ES.Nombre Estado
  FROM [dbo].[Utb_Entidad] EN
  JOIN Utb_Estado ES ON EN.IdEstado = ES.IdEstado where IdEntidad = @IdEntidad

end


GO
/****** Object:  StoredProcedure [dbo].[Sp_EntidadDropDown]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--Sp_EntidadDropDown ''
CREATE PROC [dbo].[Sp_EntidadDropDown]
@IdConsulta varchar(MAX)
AS
BEGIN
IF(@IdConsulta='')
 BEGIN
 SELECT IdEntidad Value, Nombre,Nombre,'0' Valor1  FROM Utb_Entidad where IdEntidad in( 'EN0101', 'EN0105') 
 END
ELSE
 BEGIN
 SELECT EN.IdEntidad Value, EN.Nombre,'0' Valor1   FROM Utb_Entidad EN WHERE EN.IdEntidad in( 'EN0101', 'EN0105')  and EN.IdEntidad not in (select IdEntidad from Utb_CategoriaEndidad where IdCategoria =@IdConsulta)
union
 SELECT EN.IdEntidad Value, EN.Nombre,'1' Valor1   FROM Utb_Entidad EN
 JOIN Utb_CategoriaEndidad CAT ON EN.IdEntidad = CAT.IdEntidad WHERE EN.IdEntidad in( 'EN0101', 'EN0105')  and CAT.IdCategoria = @IdConsulta 
 END
END
GO
/****** Object:  StoredProcedure [dbo].[Sp_EntidadEliminar]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_EntidadEliminar]
@IdEntidad char(6)
AS
BEGIN
BEGIN TRY
		--Existe 
     IF  EXISTS (SELECT top 1 IdEntidad from Utb_Entidad where IdEntidad = @IdEntidad)
	  BEGIN
	  	 UPDATE Utb_Entidad SET IdEstado = 'ES0203'  WHERE IdEntidad=@IdEntidad
	  END
	  ELSE
	  BEGIN
	  	SELECT -1 AS IdEntidad
	  END

END TRY
BEGIN CATCH 
	SELECT -2 AS IdEntidad
END CATCH
end
GO
/****** Object:  StoredProcedure [dbo].[Sp_EntidadMantenimiento]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_EntidadMantenimiento]
@IdEntidad CHAR(6),
@Nombre VARCHAR(50),
@IdEstado VARCHAR(100) = NULL
AS
SET @IdEstado =(CASE WHEN @IdEstado ='TRUE' THEN 'ES0201' ELSE 'ES0202' END)
BEGIN
BEGIN TRY
--Registrar
	IF(@IdEntidad = '')
	BEGIN
		 SET  @IdEntidad = ( CASE WHEN (SELECT COUNT(*) FROM Utb_Entidad )>0 THEN (SELECT 'EN' + RIGHT('000'+CONVERT(VARCHAR(4), SUBSTRING( MAX(IdEntidad),4,LEN(MAX(IdEntidad)))+1), 4) FROM Utb_Entidad) ELSE 'EN0001' END);

		--Existe 
		IF NOT EXISTS (SELECT top 1 IdEntidad from Utb_Entidad where IdEntidad = @IdEntidad)
		BEGIN
			 INSERT INTO Utb_Entidad (IdEntidad,Nombre,IdEstado)
			        VALUES(@IdEntidad,@Nombre,@IdEstado)
		END
		ELSE
		BEGIN
			SELECT -1 AS IdEntidad
		END
	END

--Actualizar
    ELSE
	BEGIN
		IF  EXISTS (SELECT top 1 IdEntidad from Utb_Entidad where IdEntidad = @IdEntidad)
		BEGIN
		    UPDATE Utb_Entidad SET Nombre=@Nombre,IdEstado=@IdEstado WHERE IdEntidad = @IdEntidad
		END
		ELSE
		BEGIN
			SELECT -1 AS IdEntidad
		END

	END

END TRY
BEGIN CATCH 
	SELECT -2 AS IdEntidad
END CATCH
end
GO
/****** Object:  StoredProcedure [dbo].[Sp_EntidadPaginado]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

-- =====================================================
-- Author:  LA
-- Create date: 15/04/2018
-- Description: Listar Entidad
-- Test:EXEC [dbo].[SP_EntidadPaginado] @WhereFilter = "WHERE P.Estado <> '' ", @OrderBy = 'IdEntidad ASC', @Start = 0, @AmountRows = 10
-- =====================================================
CREATE PROCEDURE [dbo].[Sp_EntidadPaginado]
@WhereFilter VARCHAR(MAX) = '',
@OrderBy VARCHAR (100) = '', 
@Start INT = 0,
@AmountRows INT = 0
AS
BEGIN
SET NOCOUNT ON;
    DECLARE @SentenciaSQL nvarchar(3072)
	SET @SentenciaSQL =  "
	;WITH Consulta AS (
	 SELECT 
	  p.IdEntidad
	 ,P.Nombre
	 ,P.Estado
	 FROM(
	   SELECT EN.IdEntidad,EN.Nombre, ES.Nombre Estado
	 FROM Utb_Entidad EN
	 JOIN Utb_Estado ES ON  EN.IdEstado = ES.IdEstado
	 WHERE ES.IdEstado <> 'ES0203'
	 )P
 " + 
	@WhereFilter + 
	") SELECT *
	FROM Consulta
	CROSS JOIN (SELECT Count(*) AS Cantidad FROM Consulta) AS CC
	ORDER BY " +
	CASE WHEN ISNULL(@OrderBy,'') != '' THEN (@OrderBy) 
	ELSE " IdEntidad DESC " END
	    + ' ' +
	" OFFSET " + CONVERT(VARCHAR, (@Start)) + 
	" ROWS FETCH NEXT " + CONVERT(VARCHAR, @AmountRows) + " ROWS ONLY"
	PRINT (@SentenciaSQL);
    EXECUTE sp_executesql  @stmt = @SentenciaSQL

END
GO
/****** Object:  StoredProcedure [dbo].[Sp_EstadoDropDown]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
--Sp_EstadoDropDown ''
CREATE PROC [dbo].[Sp_EstadoDropDown]
@IdConsulta varchar(15)
AS
BEGIN
 SELECT IdEstado Value, Nombre FROM Utb_Estado
END
GO
/****** Object:  StoredProcedure [dbo].[Sp_MarcaConsulta]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_MarcaConsulta]
@IdMarca CHAR(6)
AS
BEGIN
 
SELECT [IdMarca]
      ,MA.Nombre
      ,(CASE WHEN ES.IdEstado='ES0201' THEN 'true' ELSE  'false'END)IdEstado,
	  ES.Nombre Estado,
	  MA.IdEmpresa 
  FROM [dbo].[Utb_Marca] MA
  JOIN Utb_Estado ES ON  MA.IdEstado = ES.IdEstado WHERE IdMarca = @IdMarca


END
GO
/****** Object:  StoredProcedure [dbo].[Sp_MarcaDropDown]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
--Sp_MarcaDropDown ''
CREATE PROC [dbo].[Sp_MarcaDropDown]
@IdConsulta varchar(15)
AS
BEGIN
 SELECT IdMarca Value, Nombre FROM Utb_Marca where IdEmpresa  = @IdConsulta and IdEstado <> 'ES0203'
END

select * from Utb_Estado
GO
/****** Object:  StoredProcedure [dbo].[Sp_MarcaEliminar]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_MarcaEliminar]
@IdMarca CHAR(6)
AS
BEGIN
BEGIN TRY
		--Existe 
     IF  EXISTS (SELECT top 1 IdMarca from Utb_Marca where IdMarca = @IdMarca)
	  BEGIN
	  	 UPDATE Utb_Marca SET IdEstado = 'ES0203'  WHERE IdMarca=@IdMarca
	  END
	  ELSE
	  BEGIN
	  	SELECT -1 AS IdMarca
	  END

END TRY
BEGIN CATCH 
	SELECT -2 AS IdMarca
END CATCH
end
GO
/****** Object:  StoredProcedure [dbo].[Sp_MarcaMantenimiento]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_MarcaMantenimiento]
@IdMarca CHAR(6),
@Nombre VARCHAR(50),
@IdEstado varchar(6),
@IdEmpresa  CHAR(6)
AS
SET @IdEstado =(CASE WHEN @IdEstado ='TRUE' THEN 'ES0201' ELSE 'ES0202' END)
BEGIN
BEGIN TRY
--Registrar
	IF(@IdMarca = '')
	BEGIN
	    SET  @IdMarca = ( CASE WHEN (SELECT COUNT(*) FROM Utb_Marca)>0 THEN (SELECT 'MA' + RIGHT('000'+CONVERT(VARCHAR(4), SUBSTRING( MAX(IdMarca),4,LEN(MAX(IdMarca)))+1), 4) FROM Utb_Marca) ELSE 'MA0001' END);
		--Existe 
		IF NOT EXISTS (SELECT top 1 IdMarca from Utb_Marca where IdMarca = @IdMarca)
		BEGIN
			 INSERT INTO Utb_Marca (IdMarca,Nombre,IdEstado,IdEmpresa)
			        VALUES(@IdMarca,@Nombre,@IdEstado,@IdEmpresa)
					select 1 as result
		END
		ELSE
		BEGIN
			SELECT -1 AS IdMarca
		END
	END
--Actualizar
    ELSE
	BEGIN
		IF  EXISTS (SELECT top 1 IdMarca from Utb_Marca where IdMarca = @IdMarca)
		BEGIN
		    UPDATE Utb_Marca SET Nombre=@Nombre,IdEstado=@IdEstado,IdEmpresa=@IdEmpresa WHERE IdMarca = @IdMarca
			select 1 as result
		END
		ELSE
		BEGIN
			SELECT -1 AS IdMarca
		END

	END

END TRY
BEGIN CATCH 
	SELECT ISNULL(ERROR_MESSAGE(),0)  AS IdMarca
END CATCH
end
GO
/****** Object:  StoredProcedure [dbo].[Sp_MarcaPaginado]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

-- =====================================================
-- Author:  LA
-- Create date: 15/04/2018
-- Description: Listar Marca
-- Test:EXEC [dbo].[SP_MarcaPaginado] @WhereFilter = "WHERE P.Estado <> '' ", @OrderBy = 'IdMarca ASC', @Start = 0, @AmountRows = 10
-- =====================================================
CREATE PROCEDURE [dbo].[Sp_MarcaPaginado]
@WhereFilter VARCHAR(MAX) = '',
@OrderBy VARCHAR (100) = '', 
@Start INT = 0,
@AmountRows INT = 0
AS
BEGIN
SET NOCOUNT ON;
    DECLARE @SentenciaSQL nvarchar(3072)
	SET @SentenciaSQL =  "
	;WITH Consulta AS (
    SELECT 
	  p.IdMarca
	 ,P.Nombre
	 ,P.Estado
	 ,p.IdEmpresa 
	 FROM(
	   SELECT EN.IdMarca,EN.Nombre, ES.Nombre Estado,en.IdEmpresa 
	 FROM Utb_Marca EN
	 JOIN Utb_Estado ES ON  EN.IdEstado = ES.IdEstado
	 WHERE ES.IdEstado <> 'ES0203'
	 )P
 " + 
	@WhereFilter + 
	") SELECT *
	FROM Consulta
	CROSS JOIN (SELECT Count(*) AS Cantidad FROM Consulta) AS CC
	ORDER BY " +
	CASE WHEN ISNULL(@OrderBy,'') != '' THEN (@OrderBy) 
	ELSE " IdMarca DESC " END
	    + ' ' +
	" OFFSET " + CONVERT(VARCHAR, (@Start)) + 
	" ROWS FETCH NEXT " + CONVERT(VARCHAR, @AmountRows) + " ROWS ONLY"
	PRINT (@SentenciaSQL);
    EXECUTE sp_executesql  @stmt = @SentenciaSQL

END
GO
/****** Object:  StoredProcedure [dbo].[Sp_ModeloConsulta]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Sp_ModeloConsulta]
@IdModelo char(6)
as
begin
SELECT [IdModelo]
      ,MO.[Nombre]
      ,[SDK]
      ,MA.[IdMarca]
	  ,MA.Nombre Marca
      ,CON.[IdConfiguracion],
	  CON.Nombre Configuracion
      ,(CASE WHEN ES.IdEstado='ES0201' THEN 'true' ELSE  'false'END)IdEstado,
	  ES.Nombre Estado,
	  MO.IdEmpresa
  FROM [dbo].[Utb_Modelo] MO
  JOIN Utb_Marca MA ON MO.IdMarca = MA.IdMarca
  JOIN Utb_Configuracion CON ON MO.IdConfiguracion = CON.IdConfiguracion
  JOIN Utb_Estado ES ON MO.IdEstado = ES.IdEstado
   where IdModelo = @IdModelo

end



GO
/****** Object:  StoredProcedure [dbo].[Sp_ModeloDropDown]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
--Sp_ModeloDropDown ''
CREATE PROC [dbo].[Sp_ModeloDropDown]
@IdConsulta varchar(15)=''
AS
BEGIN
 SELECT IdModelo Value, Nombre FROM Utb_Modelo where ( @IdConsulta='' or IdEmpresa = @IdConsulta )
END
GO
/****** Object:  StoredProcedure [dbo].[Sp_ModeloEliminar]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_ModeloEliminar]
@IdModelo char(6)
AS
BEGIN
BEGIN TRY
		--Existe 
     IF  EXISTS (SELECT top 1 IdModelo from Utb_Modelo where IdModelo = @IdModelo)
	  BEGIN
	  	 UPDATE Utb_Modelo SET IdEstado = 'ES0203'  WHERE IdModelo=@IdModelo
	  END
	  ELSE
	  BEGIN
	  	SELECT -1 AS IdModelo
	  END

END TRY
BEGIN CATCH 
	SELECT -2 AS IdModelo
END CATCH
end
GO
/****** Object:  StoredProcedure [dbo].[Sp_ModeloMantenimiento]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_ModeloMantenimiento]
@IdModelo char(6),
@Nombre varchar(50),
@SDK int,
@IdMarca char(6),
@IdConfiguracion char(6),
@IdEstado varchar(6),
@IdEmpresa char(6)
AS
SET @IdEstado =(CASE WHEN @IdEstado ='TRUE' THEN 'ES0201' ELSE 'ES0202' END)
BEGIN
BEGIN TRY
--Registrar
	IF(@IdModelo = '')
	BEGIN
	    SET  @IdModelo = ( CASE WHEN (SELECT COUNT(*) FROM Utb_Modelo)>0 THEN (SELECT 'MO' + RIGHT('000'+CONVERT(VARCHAR(4), SUBSTRING( MAX(IdModelo),4,LEN(MAX(IdModelo)))+1), 4) FROM Utb_Modelo) ELSE 'MO0001' END);

		--Existe 
		IF NOT EXISTS (SELECT top 1 IdModelo from Utb_Modelo where IdModelo = @IdModelo)
		BEGIN
			 INSERT INTO Utb_Modelo (IdModelo,Nombre,SDK,IdMarca,IdConfiguracion,IdEstado,IdEmpresa)
			        VALUES(@IdModelo,@Nombre,@SDK,@IdMarca,@IdConfiguracion,@IdEstado,@IdEmpresa)
		END
		ELSE
		BEGIN
			SELECT -1 AS IdModelo
		END
	END

--Actualizar
    ELSE
	BEGIN
		IF  EXISTS (SELECT top 1 IdModelo from Utb_Modelo where IdModelo = @IdModelo)
		BEGIN
		    UPDATE Utb_Modelo SET Nombre=@Nombre,SDK=@SDK,IdMarca=@IdMarca,IdConfiguracion=@IdConfiguracion,IdEstado=@IdEstado,IdEmpresa=@IdEmpresa WHERE IdModelo = @IdModelo
		END
		ELSE
		BEGIN
			SELECT -1 AS IdModelo
		END

	END

END TRY
BEGIN CATCH 
	SELECT -2 AS IdModelo
END CATCH
end
GO
/****** Object:  StoredProcedure [dbo].[Sp_ModeloPaginado]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

-- =====================================================
-- Author:  LA
-- Create date: 15/04/2018
-- Description: Listar Modelo
-- Test:EXEC [dbo].[SP_ModeloPaginado] @WhereFilter = "WHERE P.Estado <> '' ", @OrderBy = 'IdModelo ASC', @Start = 0, @AmountRows = 10
-- =====================================================
CREATE PROCEDURE [dbo].[Sp_ModeloPaginado]
@WhereFilter VARCHAR(MAX) = '',
@OrderBy VARCHAR (100) = '', 
@Start INT = 0,
@AmountRows INT = 0
AS
BEGIN
SET NOCOUNT ON;
    DECLARE @SentenciaSQL nvarchar(3072)
	SET @SentenciaSQL =  "
	;WITH Consulta AS (
    SELECT 
	  p.IdModelo
	 ,P.Nombre
	 ,P.SDK
	 ,P.IdMarca
	 ,P.Marca
	 ,P.Configuracion
	 ,P.IdConfiguracion
	 ,P.Estado
	 ,p.IdEmpresa
	 FROM(
	   SELECT MO.IdModelo,MO.Nombre,MO.SDK,MAR.IdMarca,MAR.Nombre Marca,CON.IdConfiguracion,CON.Nombre Configuracion, ES.Nombre Estado,mo.IdEmpresa
	 FROM Utb_Modelo MO
	 JOIN Utb_Marca MAR ON MO.IdMarca = MAR.IdMarca
	 JOIN Utb_Configuracion CON ON MO.IdConfiguracion = CON.IdConfiguracion
	 JOIN Utb_Estado ES ON  MO.IdEstado = ES.IdEstado
	 WHERE ES.IdEstado <> 'ES0203'
	 )P
 " + 
	@WhereFilter + 
	") SELECT *
	FROM Consulta
	CROSS JOIN (SELECT Count(*) AS Cantidad FROM Consulta) AS CC
	ORDER BY " +
	CASE WHEN ISNULL(@OrderBy,'') != '' THEN (@OrderBy) 
	ELSE " IdModelo DESC " END
	    + ' ' +
	" OFFSET " + CONVERT(VARCHAR, (@Start)) + 
	" ROWS FETCH NEXT " + CONVERT(VARCHAR, @AmountRows) + " ROWS ONLY"
	PRINT (@SentenciaSQL);
    EXECUTE sp_executesql  @stmt = @SentenciaSQL

END
GO
/****** Object:  StoredProcedure [dbo].[Sp_Modulo]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_Modulo]
AS
BEGIN
SELECT [IdModulo]
      ,[Nombre]
      ,[Descripcion]
      ,[IdEstado]
  FROM [dbo].[Utb_Modulo]
END


GO
/****** Object:  StoredProcedure [dbo].[Sp_ModuloConsulta]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROC [dbo].[Sp_ModuloConsulta]
@IdModulo CHAR(6)
AS
BEGIN
 SELECT * FROM (
     SELECT MODU.IdModulo
      ,MODU.Nombre
      ,MODU.Descripcion
      ,(CASE WHEN MODU.IdEstado='ES0201' THEN 'true' ELSE  'false'END)IdEstado
	  ,ES.Nombre Estado
      ,MODU.Posicion
      ,MODU.Icono
  FROM Utb_Modulo MODU
   JOIN Utb_Estado ES ON MODU.IdEstado = ES.IdEstado

  )P WHERE P.IdModulo =@IdModulo
END 
GO
/****** Object:  StoredProcedure [dbo].[Sp_ModuloDropDown]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_ModuloDropDown]
@IdConsulta varchar(15)
AS
BEGIN
 IF(@IdConsulta='')
  BEGIN
   SELECT IdModulo Value, Nombre,'' Valor1 FROM Utb_Modulo WHERE IdEstado='ES0201'
  END
 ELSE
  BEGIN
    if not exists(select top 1 IdPerfil from Utb_PerfilModulo where IdPerfil = @IdConsulta)
	BEGIN
	  SELECT IdModulo Value, Nombre,'' Valor1 FROM Utb_Modulo WHERE IdEstado='ES0201'
	END

	ELSE
	 BEGIN
		SELECT* FROM(
		 SELECT AR.IdModulo Value, AR.Nombre, '0' Valor1 FROM Utb_Modulo AR 
		 LEFT JOIN Utb_PerfilModulo PAR ON AR.IdModulo = PAR.IdModulo WHERE PAR.IdPerfil is null
		 union
		 SELECT AR.IdModulo Value, AR.Nombre,'1' Valor1 FROM Utb_Modulo AR 
		 JOIN Utb_PerfilModulo PAR ON AR.IdModulo = PAR.IdModulo WHERE IdPerfil = @IdConsulta
		 )P GROUP BY P.Nombre,P.Valor1,P.Value
		 order by p.Value
	 END
  END
END
GO
/****** Object:  StoredProcedure [dbo].[Sp_ModuloListado]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_ModuloListado]
@Nombre varchar(200)
AS
BEGIN
SELECT [IdModulo]
      ,[Nombre]
      ,[Descripcion]
      ,[IdEstado]
	  ,Icono
  FROM [dbo].[Utb_Modulo]
END


GO
/****** Object:  StoredProcedure [dbo].[Sp_ModuloMantenimiento]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_ModuloMantenimiento]
@IdModulo CHAR(6),
@Nombre varchar(100),
@Icono varchar(100),
@EsInicio int=0,
@Posicion int,
@Descripcion varchar(100),
@IdEstado varchar(10)
AS
SET @IdEstado =(CASE WHEN @IdEstado ='TRUE' THEN 'ES0201' ELSE 'ES0202' END)
BEGIN
BEGIN TRY
--Registrar
	IF(@IdModulo = '')
	BEGIN
		--Existe 
		SET  @IdModulo = ( CASE WHEN (SELECT COUNT(*) FROM Utb_Modulo )>0 THEN (SELECT 'MO' + RIGHT('000'+CONVERT(VARCHAR(4), SUBSTRING( MAX(IdModulo),3,LEN(MAX(IdModulo)))+1), 4) FROM Utb_Modulo) ELSE 'MO0001' END);
		IF NOT EXISTS (SELECT top 1 IdModulo from Utb_Modulo where IdModulo = @IdModulo)
		BEGIN
			 INSERT INTO Utb_Modulo(IdModulo,Nombre,Icono,Posicion,Descripcion,IdEstado)
			        VALUES(@IdModulo,@Nombre,@Icono,@Posicion,@Descripcion,@IdEstado)
		END
		ELSE
		BEGIN
			SELECT -1 AS REGISTRO
		END
	END

--Actualizar
    ELSE
	BEGIN
		IF EXISTS (SELECT top 1 IdModulo from Utb_Modulo where IdModulo = @IdModulo)
		BEGIN
		    UPDATE Utb_Modulo SET Nombre=@Nombre,Icono=@Icono,Posicion=@Posicion,Descripcion=@Descripcion,IdEstado=@IdEstado WHERE IdModulo=@IdModulo
					   
		END
		ELSE
		BEGIN
			SELECT -1 AS ACTUALIZAR
		END

	END

END TRY
BEGIN CATCH 
	SELECT -2 AS IdEmpresa
END CATCH
end
GO
/****** Object:  StoredProcedure [dbo].[SP_ModuloPaginado]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[SP_ModuloPaginado]
@WhereFilter VARCHAR(MAX) = '',
@OrderBy VARCHAR (100) = '', 
@Start INT = 0,
@AmountRows INT = 0
AS
BEGIN
SET NOCOUNT ON;
    DECLARE @SentenciaSQL nvarchar(3072)
	SET @SentenciaSQL =  "

	;WITH Consulta AS (
SELECT * FROM (
    SELECT MODU.IdModulo
      ,MODU.Nombre
      ,MODU.Descripcion
      ,MODU.IdEstado
	  ,ES.Nombre Estado
      ,MODU.Posicion
      ,MODU.Icono
  FROM Utb_Modulo MODU
   JOIN Utb_Estado ES ON MODU.IdEstado = ES.IdEstado
   WHERE ES.IdEstado <> 'ES0203'
  )P
   " + 
	@WhereFilter + 
	") SELECT *
	FROM Consulta
	CROSS JOIN (SELECT Count(*) AS Cantidad FROM Consulta) AS CC
	ORDER BY " +
	CASE WHEN ISNULL(@OrderBy,'') != '' THEN (@OrderBy) 
	ELSE " IdModulo DESC " END
	    + ' ' +
	" OFFSET " + CONVERT(VARCHAR, (@Start)) + 
	" ROWS FETCH NEXT " + CONVERT(VARCHAR, @AmountRows) + " ROWS ONLY"
	PRINT (@SentenciaSQL);
    EXECUTE sp_executesql  @stmt = @SentenciaSQL

END
GO
/****** Object:  StoredProcedure [dbo].[Sp_ModuloXPerfilListado]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_ModuloXPerfilListado]
@IdPerfil varchar(200) = NULL
AS
BEGIN
 IF(@IdPerfil IS NULL)
   BEGIN
    SELECT MO.IdModulo
      ,MO.Nombre
      ,MO.Descripcion
      ,MO.IdEstado
	  ,MO.Icono
  FROM [dbo].[Utb_Modulo] MO
  LEFT JOIN Utb_PerfilModulo PRM ON MO.IdModulo = PRM.IdModulo
   ORDER BY MO.Posicion
   END
 ELSE 
  BEGIN
   SELECT MO.IdModulo
      ,MO.Nombre
      ,MO.Descripcion
      ,MO.IdEstado
	  ,MO.Icono
  FROM [dbo].[Utb_Modulo] MO
  LEFT JOIN Utb_PerfilModulo PRM ON MO.IdModulo = PRM.IdModulo
  WHERE PRM.IdPerfil = @IdPerfil
  ORDER BY MO.Posicion
  END
END


GO
/****** Object:  StoredProcedure [dbo].[Sp_Opcion]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[Sp_Opcion]
as
begin
SELECT [IdOpcion]
      ,[IdModulo]
      ,[Nombre]
      ,[Descripcion]
      ,[Url]
      ,[Controlador]
      ,[Accion]
      ,[Area]
      ,[IdEstado]
      ,[EsInicio]
  FROM [dbo].[Utb_Opcion]
end


GO
/****** Object:  StoredProcedure [dbo].[Sp_OpcionConsulta]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROC [dbo].[Sp_OpcionConsulta]
@IdOpcion CHAR(6)
AS
BEGIN
 SELECT * FROM (
   SELECT OP.IdOpcion
      ,OP.IdModulo
	  ,MODU.Nombre Modulo
      ,OP.Nombre
      ,OP.Descripcion
      ,OP.Url
      ,OP.Controlador
      ,OP.Accion
      ,OP.Area
      ,(CASE WHEN op.IdEstado='ES0201' THEN 'true' ELSE  'false'END)IdEstado
	  ,ES.Nombre Estado
      ,OP.EsInicio
      ,OP.Posicion
      ,OP.IdPadre
      ,OP.Icono
  FROM Utb_Opcion OP
  JOIN Utb_Modulo MODU ON OP.IdModulo = MODU.IdModulo
  JOIN Utb_Estado ES ON OP.IdEstado = ES.IdEstado

  )P WHERE P.IdOpcion =@IdOpcion
END 
GO
/****** Object:  StoredProcedure [dbo].[Sp_OpcionListado]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Sp_OpcionListado]
@Nombre varchar(200) = NULL
as
begin
SELECT [IdOpcion]
      ,[IdModulo]
      ,[Nombre]
      ,[Descripcion]
      ,[Url]
      ,[Controlador]
      ,[Accion]
      ,[Area]
      ,[IdEstado]
      ,[EsInicio]
	  ,IdPadre
	  ,Icono
	  ,(CASE WHEN (select COUNT(*) from Utb_Opcion where IdPadre = op.IdOpcion )>5 THEN 1 ELSE 0 END)SubMenu
  FROM [dbo].[Utb_Opcion] op where EsInicio =1 and op.IdEstado <>'ES0203'
end


GO
/****** Object:  StoredProcedure [dbo].[Sp_OpcionListadoHijo]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Sp_OpcionListadoHijo]
@Nombre varchar(200) = NULL
as
begin
SELECT [IdOpcion]
      ,[IdModulo]
      ,[Nombre]
      ,[Descripcion]
      ,[Url]
      ,[Controlador]
      ,[Accion]
      ,[Area]
      ,[IdEstado]
      ,[EsInicio]
	  ,IdPadre
	  ,Icono
  FROM [dbo].[Utb_Opcion] where EsInicio =1  and EsSubmenu =1 and  IdEstado <> 'ES0203'
end


GO
/****** Object:  StoredProcedure [dbo].[Sp_OpcionListaInicio]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Sp_OpcionListaInicio]
as
begin
SELECT [IdOpcion]
      ,[IdModulo]
      ,[Nombre]
      ,[Descripcion]
      ,[Url]
      ,[Controlador]
      ,[Accion]
      ,[Area]
      ,[IdEstado]
      ,[EsInicio]
  FROM [dbo].[Utb_Opcion] where [EsInicio] =1 and  IdEstado <> 'ES0203'
end


GO
/****** Object:  StoredProcedure [dbo].[Sp_OpcionMantenimiento]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_OpcionMantenimiento]
@IdOpcion CHAR(6)=NULL,
@IdModulo CHAR(6),
@Nombre varchar(100),
@Area varchar(100),
@Controlador varchar(100),
@Accion varchar(100),
@Icono varchar(100),
@IdPadre varchar(100) =null,
@EsInicio bit=0,
@Posicion int,
@Descripcion varchar(100),
@IdEstado varchar(10)
AS
SET @IdEstado =(CASE WHEN @IdEstado ='TRUE' THEN 'ES0201' ELSE 'ES0202' END)
BEGIN
BEGIN TRY
--Registrar
	IF(@IdOpcion = '')
	BEGIN
		--Existe 
		SET  @IdOpcion = ( CASE WHEN (SELECT COUNT(*) FROM Utb_Opcion )>0 THEN (SELECT 'OP' + RIGHT('000'+CONVERT(VARCHAR(4), SUBSTRING( MAX(IdOpcion),3,LEN(MAX(IdOpcion)))+1), 4) FROM Utb_Opcion) ELSE 'OP0001' END);
		IF NOT EXISTS (SELECT top 1 IdOpcion from Utb_Opcion where IdOpcion = @IdOpcion)
		BEGIN
			 INSERT INTO Utb_Opcion(IdOpcion,IdModulo,Nombre,Area,Controlador,Accion,Icono,IdPadre,EsInicio,Posicion,Descripcion,IdEstado)
			        VALUES(@IdOpcion,@IdModulo,@Nombre,@Area,@Controlador,@Accion,@Icono,@IdPadre,@EsInicio,@Posicion,@Descripcion,@IdEstado)
		END
		ELSE
		BEGIN
			SELECT -1 AS REGISTRO
		END
	END

--Actualizar
    ELSE
	BEGIN
		IF EXISTS (SELECT top 1 IdOpcion from Utb_Opcion where IdOpcion = @IdOpcion)
		BEGIN
		    UPDATE Utb_Opcion SET IdModulo=@IdModulo,Nombre=@Nombre,Area=@Area,Controlador=@Controlador,Accion=@Accion,Icono=@Icono,IdPadre=@IdPadre,EsInicio=@EsInicio,Posicion=@Posicion,Descripcion=@Descripcion,IdEstado=@IdEstado WHERE IdOpcion=@IdOpcion
		END
		ELSE
		BEGIN
			SELECT -1 AS ACTUALIZAR
		END

	END

END TRY
BEGIN CATCH 
	SELECT -2 AS IdEmpresa
END CATCH
end
GO
/****** Object:  StoredProcedure [dbo].[SP_OpcionPaginado]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

-- =====================================================
-- Author:  LA
-- Create date: 15/04/2018
-- Description: Listar Modulo
-- Test:EXEC SP_OpcionPaginado @WhereFilter = "WHERE P.Estado <> '' ", @OrderBy = 'IdOpcion ASC', @Start = 0, @AmountRows = 10
-- =====================================================
CREATE PROCEDURE [dbo].[SP_OpcionPaginado]
@WhereFilter VARCHAR(MAX) = '',
@OrderBy VARCHAR (100) = '', 
@Start INT = 0,
@AmountRows INT = 0
AS
BEGIN
SET NOCOUNT ON;
    DECLARE @SentenciaSQL nvarchar(3072)
	SET @SentenciaSQL =  "

	;WITH Consulta AS (
SELECT * FROM (
   SELECT OP.IdOpcion
      ,OP.IdModulo
	  ,MODU.Nombre Modulo
      ,OP.Nombre
      ,OP.Descripcion
      ,OP.Url
      ,OP.Controlador
      ,OP.Accion
      ,OP.Area
      ,OP.IdEstado
	  ,ES.Nombre Estado
      ,OP.EsInicio
      ,OP.Posicion
      ,OP.IdPadre
      ,OP.Icono
  FROM Utb_Opcion OP
  JOIN Utb_Modulo MODU ON OP.IdModulo = MODU.IdModulo
  left JOIN Utb_Estado ES ON OP.IdEstado = ES.IdEstado

  )P
   " + 
	@WhereFilter + 
	") SELECT *
	FROM Consulta
	CROSS JOIN (SELECT Count(*) AS Cantidad FROM Consulta) AS CC
	ORDER BY " +
	CASE WHEN ISNULL(@OrderBy,'') != '' THEN (@OrderBy) 
	ELSE " IdOpcion DESC " END
	    + ' ' +
	" OFFSET " + CONVERT(VARCHAR, (@Start)) + 
	" ROWS FETCH NEXT " + CONVERT(VARCHAR, @AmountRows) + " ROWS ONLY"
	PRINT (@SentenciaSQL);
    EXECUTE sp_executesql  @stmt = @SentenciaSQL

END

GO
/****** Object:  StoredProcedure [dbo].[Sp_OpcionXPerfilListado]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Sp_OpcionXPerfilListado]
@IdPerfil char(6) = NULL,
@IdUsuario char(6)
as
begin
  IF(@IdPerfil IS NULL)
   BEGIN
     SELECT OP.IdOpcion
      ,OP.IdModulo
      ,OP.Nombre
      ,OP.Descripcion
      ,OP.Url
      ,OP.Controlador
      ,OP.Accion
      ,OP.Area
      ,OP.IdEstado
      ,OP.EsInicio
	  ,OP.IdPadre
	  ,OP.Icono
	  ,(CASE WHEN (select  count(*) from Utb_Opcion where IdPadre = op.IdOpcion) >4 THEN 1 ELSE 0 END )SubMenu
	  ,1 Permitido
  FROM [dbo].[Utb_Opcion] op
  WHERE op.IdEstado <> 'ES0203'
  order by op.IdOpcion
   END
  ELSE
   BEGIN
     SELECT OP.IdOpcion
      ,OP.IdModulo
      ,OP.Nombre
      ,OP.Descripcion
      ,OP.Url
      ,OP.Controlador
      ,OP.Accion
      ,OP.Area
      ,OP.IdEstado
      ,OP.EsInicio
	  ,OP.IdPadre
	  ,OP.Icono
	  ,(CASE WHEN (select  count(*) from Utb_Opcion where IdPadre = op.IdOpcion) >4 THEN 1 ELSE 0 END )SubMenu
	  ,(case when (select COUNT(*) from Utb_PerfilOpcion where IdUsuario = @IdUsuario and IdPerfil =@IdPerfil and IdOpcion = op.IdOpcion)>0 then 1 else 0 end) Permitido
  FROM [dbo].[Utb_Opcion] op
  WHERE op.IdEstado <> 'ES0203'
  order by op.IdOpcion
   END
 
end


GO
/****** Object:  StoredProcedure [dbo].[Sp_OperadorDropDown]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
--Sp_OperadorDropDown ''
CREATE PROC [dbo].[Sp_OperadorDropDown]
@IdConsulta varchar(15)
AS
BEGIN
 SELECT IdOperador Value, Nombre,''Valor1 FROM Utb_Operador
END
GO
/****** Object:  StoredProcedure [dbo].[Sp_OperadorEliminar]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_OperadorEliminar]
@IdOperador char(6)
AS
BEGIN
BEGIN TRY
		--Existe 
     IF  EXISTS (SELECT top 1 IdOperador from Utb_Operador where IdOperador = @IdOperador)
	  BEGIN
	  	 delete from Utb_Operador   WHERE IdOperador=@IdOperador
	  END
	  ELSE
	  BEGIN
	  	SELECT -1 AS IdOperador
	  END

END TRY
BEGIN CATCH 
	SELECT -2 AS IdOperador
END CATCH
end
GO
/****** Object:  StoredProcedure [dbo].[Sp_OperadorMantenimiento]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_OperadorMantenimiento]
@IdOperador char(6),
@Nombre varchar(25),
@Descripcion varchar(500)
AS
BEGIN
BEGIN TRY
--Registrar
	IF(@IdOperador = '')
	BEGIN
		--Existe 
		IF NOT EXISTS (SELECT top 1 IdOperador from Utb_Operador where IdOperador = @IdOperador)
		BEGIN
			 INSERT INTO Utb_Operador (IdOperador,Nombre)
			        VALUES(@IdOperador,@Nombre)
		END
		ELSE
		BEGIN
			SELECT -1 AS IdOperador
		END
	END

--Actualizar
    ELSE
	BEGIN
		IF  EXISTS (SELECT top 1 IdOperador from Utb_Operador where IdOperador = @IdOperador)
		BEGIN
		    UPDATE Utb_Operador SET Nombre=@Nombre WHERE IdOperador = @IdOperador
		END
		ELSE
		BEGIN
			SELECT -1 AS IdOperador
		END

	END

END TRY
BEGIN CATCH 
	SELECT -2 AS IdOperador
END CATCH
end
GO
/****** Object:  StoredProcedure [dbo].[Sp_ParametrosCorreoConsulta]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_ParametrosCorreoConsulta] 
@IdEmpresa char(6)
AS
DECLARE @IDEMPRESATEMPORAL CHAR(6) = @IdEmpresa
DECLARE @TIPO VARCHAR(200)= (SELECT  (CASE WHEN EMP.EsPrincipal>0 THEN 'PRINCIPAL'
	         WHEN EMP.EsContratista>0 THEN 'CONTRATISTA'
	    ELSE 'SUBCONTRATISTA' END )TipoEmpresa FROM Utb_Empresa EMP WHERE EMP.IdEmpresa = @IdEmpresa)

IF(@TIPO ='SUBCONTRATISTA')
 BEGIN
       SET @IdEmpresa =(SELECT  EMP.IdPadre from Utb_Empresa EMP where  EMP.IdEmpresa = (SELECT TOP 1 E.IdPadre FROM Utb_Empresa E WHERE E.IdEmpresa = @IdEmpresa) ) 
		IF(@IdEmpresa IS NULL)
		 BEGIN
		 SET @IdEmpresa=  (SELECT TOP 1 E.IdPadre FROM Utb_Empresa E WHERE E.IdEmpresa = @IDEMPRESATEMPORAL)
	    END
 END
ELSE IF(@TIPO ='CONTRATISTA')
  BEGIN
    SET @IdEmpresa = (SELECT TOP 1 E.IdPadre FROM Utb_Empresa E WHERE E.IdEmpresa = @IdEmpresa)
  END
ELSE IF(@TIPO ='PRINCIPAL')
  BEGIN
    SET @IdEmpresa = @IdEmpresa
  END
BEGIN
 SELECT 
PR.IdParametros,
PR.Host,
PR.Port,
PR.Correo,
PR.Password,
PR.IdEmpresa,
E.RazonSocial Empresa,
E.RUC
 FROM Utb_ParametrosEnvioCorreo PR 
 JOIN Utb_Empresa E ON PR.IdEmpresa = E.IdEmpresa
 WHERE PR.IdEmpresa = @IdEmpresa
end
GO
/****** Object:  StoredProcedure [dbo].[Sp_ParametrosCorreoListado]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_ParametrosCorreoListado]
@IdEmpresa char(6)
AS
BEGIN
 SELECT 
PR.IdParametros,
PR.Host,
PR.Port,
PR.Correo,
PR.Password,
PR.IdEmpresa,
E.RazonSocial Empresa,
E.RUC
 FROM Utb_ParametrosEnvioCorreo PR 
 JOIN Utb_Empresa E ON PR.IdEmpresa = E.IdEmpresa
 WHERE PR.IdEmpresa = @IdEmpresa
end
GO
/****** Object:  StoredProcedure [dbo].[Sp_ParametrosCorreoMantenimiento]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_ParametrosCorreoMantenimiento]
@IdParametros char(6),
@Host varchar(200),
@Port int,
@Correo nvarchar(200),
@Password nvarchar(200),
@IdEmpresa char(6)
AS
BEGIN
BEGIN TRY
--Registrar
	IF(@IdParametros = '')
	BEGIN
		--Existe 
		SET  @IdParametros = ( CASE WHEN (SELECT COUNT(*) FROM Utb_ParametrosEnvioCorreo )>0 THEN (SELECT 'PA' + RIGHT('000'+CONVERT(VARCHAR(4), SUBSTRING( MAX(IdParametros),3,LEN(MAX(IdParametros)))+1), 4) FROM Utb_ParametrosEnvioCorreo) ELSE 'PA0001' END);
		IF NOT EXISTS (SELECT top 1 IdParametros from Utb_ParametrosEnvioCorreo where IdParametros = @IdParametros)
		BEGIN
			 INSERT INTO Utb_ParametrosEnvioCorreo(IdParametros,Host,Port,Correo,Password,IdEmpresa)
			        VALUES(@IdParametros,@Host,@Port,@Correo,@Password,@IdEmpresa)
		END
		ELSE
		BEGIN
			SELECT -1 AS REGISTRO
		END
	END

--Actualizar
    ELSE
	BEGIN
		IF EXISTS (SELECT top 1 IdParametros from Utb_ParametrosEnvioCorreo where IdParametros = @IdParametros)
		BEGIN
		    DECLARE @PASSWORDTEMP NVARCHAR(200)= (SELECT Password FROM Utb_ParametrosEnvioCorreo WHERE IdParametros =@IdParametros AND IdEmpresa = @IdEmpresa) 
			IF(@Password='')
			BEGIN
			 UPDATE Utb_ParametrosEnvioCorreo SET Host=@Host,Port=@Port,Correo=@Correo,IdEmpresa=@IdEmpresa WHERE IdParametros =@IdParametros
			END
			ELSE IF(@PASSWORDTEMP =@Password)
			BEGIN
			 UPDATE Utb_ParametrosEnvioCorreo SET Host=@Host,Port=@Port,Correo=@Correo,IdEmpresa=@IdEmpresa WHERE IdParametros =@IdParametros
			END
			ELSE
			BEGIN
			 UPDATE Utb_ParametrosEnvioCorreo SET Host=@Host,Port=@Port,Correo=@Correo,Password=@Password,IdEmpresa=@IdEmpresa WHERE IdParametros =@IdParametros
			END
		END
		ELSE
		BEGIN
			SELECT -1 AS ACTUALIZAR
		END

	END

END TRY
BEGIN CATCH 
	SELECT -2 AS ERROR
END CATCH
end
GO
/****** Object:  StoredProcedure [dbo].[Sp_PerfilConsulta]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROC [dbo].[Sp_PerfilConsulta]
@IdPerfil CHAR(6)
AS
BEGIN
 SELECT * FROM (
     SELECT 
	   PER.IdPerfil
      ,PER.Nombre
      ,PER.Descripcion
      ,(CASE WHEN PER.IdEstado='ES0201' THEN 'true' ELSE  'false'END)IdEstado
	  ,ES.Nombre Estado
   FROM Utb_Perfil PER
   JOIN Utb_Estado ES ON PER.IdEstado = ES.IdEstado

  )P WHERE P.IdPerfil =@IdPerfil
END 
GO
/****** Object:  StoredProcedure [dbo].[Sp_PerfilDropDown]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROC [dbo].[Sp_PerfilDropDown]
@IdConsulta varchar(15)
AS
BEGIN
 SELECT IdPerfil Value, Nombre,'' Valor1 FROM Utb_Perfil WHERE IdEstado='ES0201'
END
GO
/****** Object:  StoredProcedure [dbo].[Sp_PerfilEliminar]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROC [dbo].[Sp_PerfilEliminar]
@IdPerfil char(6)
AS
BEGIN
BEGIN TRY
		--Existe 
     IF  EXISTS (SELECT top 1 IdPerfil from Utb_Perfil where IdPerfil = @IdPerfil)
	  BEGIN
	  	 delete from Utb_Perfil   WHERE IdPerfil=@IdPerfil
	  END
	  ELSE
	  BEGIN
	  	SELECT -1 AS IdOperador
	  END

END TRY
BEGIN CATCH 
	SELECT -2 AS IdOperador
END CATCH
end
GO
/****** Object:  StoredProcedure [dbo].[Sp_PerfilMantenimiento]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_PerfilMantenimiento]
@IdPerfil CHAR(6)=NULL,
@Nombre VARCHAR(50),
@Descripcion VARCHAR(300),
@IdEstado varchar (10),
@IdModulo varchar (max)
AS
SET @IdEstado =(CASE WHEN @IdEstado ='TRUE' THEN 'ES0201' ELSE 'ES0202' END)
DELETE FROM Utb_PerfilModulo WHERE IdPerfil = @IdPerfil

DECLARE @list varchar(max)
DECLARE @pos INT
DECLARE @len INT
DECLARE @value varchar(max)

SET @list = @IdModulo

set @pos = 0
set @len = 0
BEGIN
BEGIN TRY
--Registrar
	IF(@IdPerfil = '')
	BEGIN
		--Existe 
		SET  @IdPerfil = ( CASE WHEN (SELECT COUNT(*) FROM Utb_Perfil )>0 THEN (SELECT 'PR' + RIGHT('000'+CONVERT(VARCHAR(4), SUBSTRING( MAX(IdPerfil),3,LEN(MAX(IdPerfil)))+1), 4) FROM Utb_Perfil) ELSE 'PR0001' END);
		IF NOT EXISTS (SELECT top 1 IdPerfil from Utb_Perfil where IdPerfil = @IdPerfil)
		BEGIN
			 INSERT INTO Utb_Perfil(IdPerfil,Nombre,Descripcion,IdEstado)
			        VALUES(@IdPerfil,@Nombre,@Descripcion,@IdEstado)
		END
		ELSE
		BEGIN
			SELECT -1 AS REGISTRO
		END
	END

--Actualizar
    ELSE
	BEGIN
		IF EXISTS (SELECT top 1 IdPerfil from Utb_Perfil where IdPerfil = @IdPerfil)
		BEGIN
		    UPDATE Utb_Perfil SET @Descripcion=@Descripcion, Nombre=@Nombre,IdEstado=@IdEstado WHERE IdPerfil=@IdPerfil
		END
		ELSE
		BEGIN
			SELECT -1 AS ACTUALIZAR
		END

	END

WHILE CHARINDEX(',', @list, @pos+1)>0
BEGIN
	set @len = CHARINDEX(',', @list, @pos+1) - @pos
	set @value = SUBSTRING(@list, @pos, @len)
            
	PRINT @value -- for debug porpose   

	INSERT INTO Utb_PerfilModulo(IdPerfil,IdModulo,IdEstado)VALUES(@IdPerfil,@value,@IdEstado)

	set @pos = CHARINDEX(',', @list, @pos+@len) +1
END

END TRY
BEGIN CATCH 
	SELECT -2 AS IdEmpresa
END CATCH
end

GO
/****** Object:  StoredProcedure [dbo].[SP_PerfilPaginado]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

-- =====================================================
-- Author:  LA
-- Create date: 15/04/2018
-- Description: Listar Perfil
-- Test:EXEC SP_PerfilPaginado @WhereFilter = "WHERE P.Estado <> '' ", @OrderBy = 'IdPerfil ASC', @Start = 0, @AmountRows = 10
-- =====================================================
CREATE PROCEDURE [dbo].[SP_PerfilPaginado]
@WhereFilter VARCHAR(MAX) = '',
@OrderBy VARCHAR (100) = '', 
@Start INT = 0,
@AmountRows INT = 0
AS
BEGIN
SET NOCOUNT ON;
    DECLARE @SentenciaSQL nvarchar(3072)
	SET @SentenciaSQL =  "

	;WITH Consulta AS (
SELECT * FROM (
     SELECT 
	   PER.IdPerfil
      ,PER.Nombre
      ,PER.Descripcion
      ,PER.IdEstado
	  ,ES.Nombre Estado
   FROM Utb_Perfil PER
   JOIN Utb_Estado ES ON PER.IdEstado = ES.IdEstado

  )P
   " + 
	@WhereFilter + 
	") SELECT *
	FROM Consulta
	CROSS JOIN (SELECT Count(*) AS Cantidad FROM Consulta) AS CC
	ORDER BY " +
	CASE WHEN ISNULL(@OrderBy,'') != '' THEN (@OrderBy) 
	ELSE " IdPerfil DESC " END
	    + ' ' +
	" OFFSET " + CONVERT(VARCHAR, (@Start)) + 
	" ROWS FETCH NEXT " + CONVERT(VARCHAR, @AmountRows) + " ROWS ONLY"
	PRINT (@SentenciaSQL);
    EXECUTE sp_executesql  @stmt = @SentenciaSQL

END


GO
/****** Object:  StoredProcedure [dbo].[Sp_PersonaAreaMantenimiento]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_PersonaAreaMantenimiento]
@IdPersona char(15),
@IdArea char(6)
AS
BEGIN
BEGIN TRY
--Registrar
	IF(@IdPersona = '')
	BEGIN
		--Existe 
		IF NOT EXISTS (SELECT top 1 IdPersona from Utb_PersonaArea where IdPersona = @IdPersona)
		BEGIN
			 INSERT INTO Utb_PersonaArea (IdPersona,IdArea)
			        VALUES(@IdPersona,@IdArea)
		END
		ELSE
		BEGIN
			SELECT -1 AS IdPersona
		END
	END

--Actualizar
  

END TRY
BEGIN CATCH 
	SELECT -2 AS IdPersona
END CATCH
end
GO
/****** Object:  StoredProcedure [dbo].[Sp_ProvinciaDropDown]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
--Sp_ProvinciaDropDown ''
CREATE PROC [dbo].[Sp_ProvinciaDropDown] 
@IdConsulta varchar(15)
AS
BEGIN
 select IdProvincia Value,Nombre,''Valor1  from Utb_Ubigeo where IdDistrito='00' and IdProvincia<>'00' AND IdDepartamento = @IdConsulta 
END
GO
/****** Object:  StoredProcedure [dbo].[Sp_RegimenConsulta]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_RegimenConsulta]
@IdRegimen CHAR(6)
AS
BEGIN
 SELECT 
 P.IdRegimen,
 P.Nombre,
 P.Estado,
 P.IdEstado FROM (
SELECT IdRegimen
      ,RE.Nombre
      ,ES.Nombre Estado
	  ,(CASE WHEN ES.IdEstado='ES0201' THEN 'true' ELSE  'false'END)IdEstado
  FROM Utb_Regimen RE
  JOIN Utb_Estado ES ON RE.IdEstado = ES.IdEstado
  )P WHERE P.IdRegimen = @IdRegimen
END
GO
/****** Object:  StoredProcedure [dbo].[Sp_RegimenDropDown]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_RegimenDropDown]
@IdConsulta varchar(60)= ''
AS
BEGIN
SELECT IdRegimen Value
      ,Nombre,''Valor1
  FROM Utb_Regimen  where (@IdConsulta='' or IdEmpresa = @IdConsulta)
END
GO
/****** Object:  StoredProcedure [dbo].[Sp_RegimenEliminar]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_RegimenEliminar]
@IdRegimen char(6)
AS
BEGIN
BEGIN TRY
		--Existe 
     IF  EXISTS (SELECT top 1 IdRegimen from Utb_Regimen where IdRegimen = @IdRegimen)
	  BEGIN
	  	 update Utb_Regimen set IdEstado = 'ES0203'  WHERE IdRegimen=@IdRegimen
	  END
	  ELSE
	  BEGIN
	  	SELECT -1 AS IdOperador
	  END

END TRY
BEGIN CATCH 
	SELECT -2 AS IdOperador
END CATCH
end
GO
/****** Object:  StoredProcedure [dbo].[Sp_RegimenListado]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_RegimenListado]
@Nombre varchar(60)
AS
BEGIN
 SELECT 
 P.IdRegimen,
 P.Nombre,
 P.Estado FROM (
SELECT IdRegimen
      ,RE.Nombre
      ,ES.Nombre Estado
  FROM Utb_Regimen RE
  JOIN Utb_Estado ES ON RE.IdEstado = ES.IdEstado
  )P WHERE P.Nombre = @Nombre
END
GO
/****** Object:  StoredProcedure [dbo].[Sp_RegimenMantenimiento]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_RegimenMantenimiento]
@IdRegimen CHAR(6)=NULL,
@Nombre VARCHAR(50),
@IdEstado VARCHAR(10) = NULL,
@IdEmpresa char (6) = ''
AS
SET @IdEstado =(CASE WHEN @IdEstado ='TRUE' THEN 'ES0201' ELSE 'ES0202' END)
BEGIN
BEGIN TRY
--Registrar
	IF(@IdRegimen = '')
	BEGIN
		--Existe 
		SET  @IdRegimen = ( CASE WHEN (SELECT COUNT(*) FROM Utb_Regimen )>0 THEN (SELECT 'RE' + RIGHT('000'+CONVERT(VARCHAR(4), SUBSTRING( MAX(IdRegimen),3,LEN(MAX(IdRegimen)))+1), 4) FROM Utb_Regimen) ELSE 'RE0001' END);
		IF NOT EXISTS (SELECT top 1 IdRegimen from Utb_Regimen where IdRegimen = @IdRegimen)
		BEGIN
			 INSERT INTO Utb_Regimen(IdRegimen,Nombre,IdEstado,IdEmpresa)
			        VALUES(@IdRegimen,@Nombre,@IdEstado,@IdEmpresa)
		END
		ELSE
		BEGIN
			SELECT -1 AS REGISTRO
		END
	END

--Actualizar
    ELSE
	BEGIN
		IF EXISTS (SELECT top 1 IdRegimen from Utb_Regimen where IdRegimen = @IdRegimen)
		BEGIN
		    UPDATE Utb_Regimen SET Nombre=@Nombre,IdEstado=@IdEstado,IdEmpresa=@IdEmpresa WHERE IdRegimen=@IdRegimen
		END
		ELSE
		BEGIN
			SELECT -1 AS ACTUALIZAR
		END

	END

END TRY
BEGIN CATCH 
	SELECT -2 AS IdEmpresa
END CATCH
end
GO
/****** Object:  StoredProcedure [dbo].[SP_RegimenPaginado]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

-- =====================================================
-- Author:  LA
-- Create date: 15/04/2018
-- Description: Listar Area
-- Test:EXEC [dbo].[SP_RegimenPaginado] @WhereFilter = "WHERE P.Estado <> '' ", @OrderBy = 'IdRegimen ASC', @Start = 0, @AmountRows = 10
-- =====================================================
CREATE PROCEDURE [dbo].[SP_RegimenPaginado]
@WhereFilter VARCHAR(MAX) = '',
@OrderBy VARCHAR (100) = '', 
@Start INT = 0,
@AmountRows INT = 0
AS
BEGIN
SET NOCOUNT ON;
    DECLARE @SentenciaSQL nvarchar(3072)
	SET @SentenciaSQL =  "

	;WITH Consulta AS (
	SELECT 
	 P.IdRegimen,
	 P.Nombre,
	 P.Estado,
	 P.IdEmpresa FROM (
	SELECT IdRegimen
		  ,RE.Nombre
		  ,ES.Nombre Estado
		  ,RE.IdEmpresa
	  FROM Utb_Regimen RE
	  JOIN Utb_Estado ES ON RE.IdEstado = ES.IdEstado
	  WHERE ES.IdEstado <> 'ES0203'
	  )P
	" + 
	@WhereFilter + 
	") SELECT *
	FROM Consulta
	CROSS JOIN (SELECT Count(*) AS Cantidad FROM Consulta) AS CC
	ORDER BY " +
	CASE WHEN ISNULL(@OrderBy,'') != '' THEN (@OrderBy) 
	ELSE " IdRegimen DESC " END
	    + ' ' +
	" OFFSET " + CONVERT(VARCHAR, (@Start)) + 
	" ROWS FETCH NEXT " + CONVERT(VARCHAR, @AmountRows) + " ROWS ONLY"
	PRINT (@SentenciaSQL);
    EXECUTE sp_executesql  @stmt = @SentenciaSQL

END


GO
/****** Object:  StoredProcedure [dbo].[Sp_ReporteAsistenciaPaginado]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

-- =====================================================
-- Author:  LA
-- Create date: 15/04/2018
-- Description: Listar ColaboradorDigitalizacion
-- Test:EXEC [dbo].[Sp_ReporteAsistenciaPaginado] @WhereFilter = "WHERE P.Estado <> '' ", @OrderBy = 'Id ASC', @Start = 0, @AmountRows = 10,@Parameters="COLABORADOR"
-- =====================================================
CREATE PROCEDURE [dbo].[Sp_ReporteAsistenciaPaginado]
@WhereFilter VARCHAR(MAX) = '',
@OrderBy VARCHAR (100) = '', 
@Start INT = 0,
@AmountRows INT = 0,
@Parameters VARCHAR(MAX)

AS
if(@AmountRows=-1)
  set @AmountRows =100000
BEGIN
SET NOCOUNT ON;
    DECLARE @SentenciaSQL nvarchar(MAX)
	if(@Parameters='COLABORADOR')
	BEGIN
	SET @SentenciaSQL =  "
	;WITH Consulta AS (
SELECT * FROM (
SELECT * FROM (
	select  (SELECT COUNT(Fecha) FROM Utb_Marcacion WHERE (CONVERT(varchar(10),Fecha,120))=(CONVERT(varchar(10),mr.Fecha,120)) and IdTerminal= mr.IdTerminal and IdPersona =mr.IdPersona)as Marcaciones, 
	p.IdColaborador Id, p.Nombres +' '+p.ApellidoPaterno +' '+p.ApellidoPaterno as Nombre,p.NumeroDocumento Numero, ter.Nombre Terminal,con.Nombre Condicion,emp.RazonSocial Empresa,emp.IdEmpresa,

	(CONVERT(varchar(10), mr.Fecha, 103))AS Fecha,
	(SELECT MIN(T.HoraOriginal)HoraOriginal FROM (SELECT (CONVERT(varchar(8), Fecha, 108))HoraOriginal FROM Utb_Marcacion  WHERE IdTipo='TI0301' and  IdPersona=mr.IdPersona and (CONVERT(varchar(10),Fecha,120))=(CONVERT(varchar(10),mr.Fecha,120)) and IdTerminal= mr.IdTerminal )T) 
	Entrada, 
	(SELECT MAX(T.HoraOriginal)HoraOriginal FROM (SELECT (CONVERT(varchar(8), Fecha, 108))HoraOriginal FROM Utb_Marcacion  WHERE IdTipo='TI0302' and IdPersona=mr.IdPersona and (CONVERT(varchar(10),Fecha,120))=(CONVERT(varchar(10),mr.Fecha,120)) and IdTerminal= mr.IdTerminal )T) 
	Salida,
	 CONVERT(CHAR(10),
	 CONVERT (DATETIME,(SELECT MAX(T.HoraOriginal)HoraOriginal FROM (SELECT(CONVERT(varchar(8), Fecha, 108))HoraOriginal FROM Utb_Marcacion WHERE IdTipo='TI0302' and  IdPersona=mr.IdPersona and (CONVERT(varchar(10),Fecha,120))=(CONVERT(varchar(10),mr.Fecha,120))and IdTerminal= mr.IdTerminal)T) )
		-
	 CONVERT (DATETIME,(SELECT MIN(T.HoraOriginal)HoraOriginal FROM (SELECT (CONVERT(varchar(8), Fecha, 108))HoraOriginal FROM Utb_Marcacion WHERE IdTipo='TI0301' and IdPersona=mr.IdPersona and (CONVERT(varchar(10),Fecha,120))=(CONVERT(varchar(10),mr.Fecha,120)) and IdTerminal= mr.IdTerminal )T) )
	,108)Acumulado ,'activo'Estado
	from Utb_Marcacion mr 
	join Utb_Colaborador p on mr.IdPersona = p.IdPersona 
	join Utb_Terminal ter on mr.IdTerminal = ter.IdTerminal
	join Utb_Condicion con on p.IdCondicion = con.IdCondicion
	join Utb_Regimen r on con.IdRegimen = r.IdRegimen
	JOIN Utb_Empresa emp on p.IdEmpresa = emp.IdEmpresa
group by mr.Fecha, p.IdColaborador,mr.IdPersona,p.NumeroDocumento,p.Nombres,mr.IdTerminal,p.ApellidoPaterno,p.ApellidoMaterno,ter.Nombre,con.Nombre,emp.RazonSocial,emp.IdEmpresa
)Z
GROUP BY Z.Marcaciones,Z.Id,Z.Nombre,Z.Numero,Z.Terminal,Z.Condicion,Z.IdEmpresa,Z.Empresa,Z.Fecha,Z.Entrada,Z.Salida,Z.Acumulado,Z.Estado
)P
 " + 
	@WhereFilter + 	"
	) SELECT *
	FROM Consulta
	CROSS JOIN (SELECT Count(*) AS Cantidad FROM Consulta) AS CC
	ORDER BY " +
	CASE WHEN ISNULL(@OrderBy,'') != '' THEN (@OrderBy) 
	ELSE "Fecha, Id DESC " END
	    + ' ' +
	" OFFSET " + CONVERT(VARCHAR, (@Start)) + 
	" ROWS FETCH NEXT " + CONVERT(VARCHAR, @AmountRows) + " ROWS ONLY"
	END
	IF(@Parameters='EMPRESA')
	 BEGIN
	SET @SentenciaSQL =  "
	;WITH Consulta AS (
SELECT * FROM (
SELECT * FROM (
	select  (SELECT COUNT(Fecha) FROM Utb_Marcacion WHERE (CONVERT(varchar(10),Fecha,120))=(CONVERT(varchar(10),mr.Fecha,120)) and IdTerminal= mr.IdTerminal and IdPersona =mr.IdPersona)as Marcaciones, 
	p.IdEmpresa Id, p.RazonSocial as Nombre,p.RUC Numero, ter.Nombre Terminal,'' Condicion,emp.RazonSocial Empresa,emp.IdEmpresa,

	(CONVERT(varchar(10), mr.Fecha, 103))AS Fecha,
	(SELECT MIN(T.HoraOriginal)HoraOriginal FROM (SELECT (CONVERT(varchar(8), Fecha, 108))HoraOriginal FROM Utb_Marcacion  WHERE IdTipo='TI0301' and  IdPersona=mr.IdPersona and (CONVERT(varchar(10),Fecha,120))=(CONVERT(varchar(10),mr.Fecha,120)) and IdTerminal= mr.IdTerminal )T) 
	Entrada, 
	(SELECT MAX(T.HoraOriginal)HoraOriginal FROM (SELECT (CONVERT(varchar(8), Fecha, 108))HoraOriginal FROM Utb_Marcacion  WHERE IdTipo='TI0302' and IdPersona=mr.IdPersona and (CONVERT(varchar(10),Fecha,120))=(CONVERT(varchar(10),mr.Fecha,120)) and IdTerminal= mr.IdTerminal )T) 
	Salida,
	 CONVERT(CHAR(10),
	 CONVERT (DATETIME,(SELECT MAX(T.HoraOriginal)HoraOriginal FROM (SELECT(CONVERT(varchar(8), Fecha, 108))HoraOriginal FROM Utb_Marcacion WHERE IdTipo='TI0302' and  IdPersona=mr.IdPersona and (CONVERT(varchar(10),Fecha,120))=(CONVERT(varchar(10),mr.Fecha,120))and IdTerminal= mr.IdTerminal)T) )
		-
	 CONVERT (DATETIME,(SELECT MIN(T.HoraOriginal)HoraOriginal FROM (SELECT (CONVERT(varchar(8), Fecha, 108))HoraOriginal FROM Utb_Marcacion WHERE IdTipo='TI0301' and IdPersona=mr.IdPersona and (CONVERT(varchar(10),Fecha,120))=(CONVERT(varchar(10),mr.Fecha,120)) and IdTerminal= mr.IdTerminal )T) )
	,108)Acumulado ,'activo'Estado
	from Utb_Marcacion mr 
	join Utb_Empresa p on mr.IdPersona = p.IdPersona 
	join Utb_Terminal ter on mr.IdTerminal = ter.IdTerminal
	JOIN Utb_Empresa emp on p.IdEmpresa = emp.IdEmpresa
group by mr.Fecha, p.IdEmpresa,mr.IdPersona,p.RUC,p.RazonSocial,mr.IdTerminal,ter.Nombre,emp.RazonSocial,emp.IdEmpresa
)Z
GROUP BY Z.Marcaciones,Z.Id,Z.Nombre,Z.Numero,Z.Terminal,Z.Condicion,Z.IdEmpresa,Z.Empresa,Z.Fecha,Z.Entrada,Z.Salida,Z.Acumulado,Z.Estado
)P
 " + 
	@WhereFilter + 
	") SELECT *
	FROM Consulta
	CROSS JOIN (SELECT Count(*) AS Cantidad FROM Consulta) AS CC
	ORDER BY " +
	CASE WHEN ISNULL(@OrderBy,'') != '' THEN (@OrderBy) 
	ELSE " FechaF, Id DESC " END
	    + ' ' +
	" OFFSET " + CONVERT(VARCHAR, (@Start)) + 
	" ROWS FETCH NEXT " + CONVERT(VARCHAR, @AmountRows) + " ROWS ONLY"
	END
	PRINT (@SentenciaSQL);
    EXECUTE sp_executesql  @stmt = @SentenciaSQL

END




GO
/****** Object:  StoredProcedure [dbo].[Sp_ReporteColaboradorPaginado]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

-- =====================================================
-- Author:  LA
-- Create date: 15/04/2018
-- Description: Listar Colaborador
-- Test:EXEC [dbo].[SP_ReporteColaboradorPaginado] @WhereFilter = "WHERE P.Estado != ''  AND P.Digitalizacion ='REVISION'  AND P.IdEmpresa ='EM0001' OR P.IdPadre ='EM0001' OR P.IdPadreSubcontratista ='EM0001' ", @OrderBy = 'IdColaborador ASC', @Start = 0, @AmountRows = 10
-- =====================================================
CREATE PROCEDURE [dbo].[Sp_ReporteColaboradorPaginado]
@WhereFilter VARCHAR(MAX) = '',
@OrderBy VARCHAR (100) = '', 
@Start INT = 0,
@AmountRows INT = 0
AS
BEGIN
SET NOCOUNT ON;
    DECLARE @SentenciaSQL nvarchar(max)
	SET @SentenciaSQL =  "
	;WITH Consulta AS (
	 SELECT
	  P.IdPersona
	 ,P.IdPadre
	 ,P.IdPadreSubcontratista 
	 ,P.PadreSubcontratista
	 ,P.IdEmpresa
	 ,p.IdColaborador
	 ,rtrim(ltrim(P.Nombres +' '+ P.ApellidoPaterno +' '+ ApellidoMaterno))Nombre
	 ,P.ApellidoPaterno
	 ,P.ApellidoMaterno
	 ,P.Genero
	 ,(convert(varchar(10),P.FechaNacimiento,103) )FechaNacimiento
	 ,P.IdUbicacion
	 ,P.Ubicacion
	 ,P.IdDepartamento
	 ,P.Departamento
	 ,p.Entidad
	 ,P.Empresa
	 ,p.Ubigeo
	 ,p.Categoria
	 ,p.Regimen
	 ,p.Condicion
	 ,p.Estado 
	 ,P.Digitalizacion
	 FROM(
	   SELECT CO.IdEmpresa,IdColaborador,CO.IdPersona,CO.Nombres,CO.ApellidoPaterno,CO.ApellidoMaterno,
	   (CASE WHEN CO.Genero ='M'THEN 'MASCULINO'ELSE 'FEMENINO' END)Genero,
	   CO.FechaNacimiento,UB.IdUbicacion,UB.Nombre Ubicacion,DE.IdDepartamento,DE.Nombre Departamento,EN.Nombre Entidad,
	    EM.RazonSocial Empresa,UBG.Nombre Ubigeo,CA.Nombre Categoria,REG.Nombre Regimen,COND.Nombre Condicion ,ES.Nombre Estado,
		ISNULL((SELECT top 1 (CASE WHEN   UPPER(ES.Nombre) IS NULL THEN 'REGISTRO' ELSE UPPER(ES.Nombre) END) FROM Utb_Digitalizacion DIG JOIN Utb_Estado ES ON DIG.IdEstado = ES.IdEstado WHERE  IdPersona = CO.IdPersona ),'REGISTRO')Digitalizacion,
	  
	  (SELECT  E.IdPadre from Utb_Empresa E where  E.IdEmpresa = CO.IdEmpresa   )IdPadre,
	 (SELECT  E.IdPadre from Utb_Empresa E where  E.IdEmpresa = (SELECT E.IdPadre FROM Utb_Empresa E WHERE E.IdEmpresa = CO.IdEmpresa )  )IdPadreSubcontratista,
	 (CASE WHEN EM.EsPrincipal>0 THEN EM.RazonSocial 
	 ELSE 
	   (SELECT  (CASE WHEN E.IdPadre IS NULL 
	                  THEN (SELECT EM.RazonSocial FROM Utb_Empresa EM WHERE EM.IdEmpresa = E.IdEmpresa)
			   ELSE (SELECT EM.RazonSocial FROM Utb_Empresa EM WHERE EM.IdEmpresa = E.IdPadre) END ) from Utb_Empresa E where  E.IdEmpresa = EM.IdPadre   )
	  END)PadreSubcontratista
	 FROM Utb_Colaborador CO
	 left JOIN Utb_Ubicacion UB ON CO.IdUbicacion = UB.IdUbicacion
	 left JOIN Utb_Departamento DE ON CO.IdDepartamento = DE.IdDepartamento
	 left JOIN Utb_Persona PER ON CO.IdPersona = PER.IdPersona
	 left JOIN Utb_Entidad EN ON CO.IdEntidad = EN.IdEntidad
	 left JOIN Utb_Estado ES ON PER.IdEstado = ES.IdEstado
	 left JOIN Utb_Empresa EM ON CO.IdEmpresa = EM.IdEmpresa
	 left JOIN Utb_Ubigeo UBG ON CO.IdDitrito = UBG.IdDepartamento+UBG.IdProvincia+UBG.IdDistrito 
	 left JOIN Utb_Categoria CA ON CO.IdCategoria = CA.IdCategoria
	 left JOIN Utb_Regimen REG ON CO.IdRegimen = REG.IdRegimen
	 left JOIN Utb_Condicion COND ON CO.IdCondicion = COND.IdCondicion
	 WHERE ES.IdEstado <> 'ES0203'
	 )P


 " + 
	@WhereFilter + 
	") SELECT *
	FROM Consulta
	CROSS JOIN (SELECT Count(*) AS Cantidad FROM Consulta) AS CC
	ORDER BY " +
	CASE WHEN ISNULL(@OrderBy,'') != '' THEN (@OrderBy) 
	ELSE " IdColaborador DESC " END
	    + ' ' +
	" OFFSET " + CONVERT(VARCHAR, (@Start)) + 
	" ROWS FETCH NEXT " + CONVERT(VARCHAR, @AmountRows) + " ROWS ONLY"
	PRINT (@SentenciaSQL);
    EXECUTE sp_executesql  @stmt = @SentenciaSQL

END
 
 	 
GO
/****** Object:  StoredProcedure [dbo].[Sp_ReporteDigitalizacionPaginado]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

-- =====================================================
-- Author:  LA
-- Create date: 15/04/2018
-- Description: Listar ColaboradorDigitalizacion
-- Test:EXEC [dbo].[Sp_ReporteDigitalizacionPaginado] @WhereFilter = "WHERE P.Estado <> '' ", @OrderBy = 'IdPersona ASC', @Start = 0, @AmountRows = 10,@Parameters="COLABORADOR"
-- =====================================================
CREATE PROCEDURE [dbo].[Sp_ReporteDigitalizacionPaginado]
@WhereFilter VARCHAR(MAX) = '',
@OrderBy VARCHAR (100) = '', 
@Start INT = 0,
@AmountRows INT = 0,
@Parameters VARCHAR(200)

AS
if(@AmountRows=-1)
  set @AmountRows =100000
BEGIN
SET NOCOUNT ON;
    DECLARE @SentenciaSQL nvarchar(3072)
	if(@Parameters='COLABORADOR')
	BEGIN
	SET @SentenciaSQL =  "
	;WITH Consulta AS (

   SELECT *from(
	SELECT COLA.IdColaborador Id,  COLA.IdPersona, COLA.Nombres +' '+ COLA.ApellidoPaterno +' '+ COLA.ApellidoMaterno Nombre,CA.Nombre Categoria,DOC.Nombre Documento,
	convert(varchar(20),(SELECT top 1 FechaVencimiento FROM Utb_Digitalizacion WHERE IdDocumento = DOC.IdDocumento  AND IdPersona = COLA.IdPersona) ,103)Fecha,
    (Select COUNT(*) from Utb_DocumentoAdjunto where IdPersona = COLA.IdPersona and IdDocumento =DOC.IdDocumento)Adjuntado,(CASE WHEN DOC.Obligatorio=1  THEN  'SI' ELSE 'NO' END) Obligatorio, (CASE WHEN DOC.FechaVencimiento=1  THEN  'SI' ELSE 'NO' END) FechaVencimiento,
    ISNULL((SELECT top 1 (CASE WHEN  UPPER(ES.Nombre) IS NULL THEN 'REGISTRO' ELSE UPPER(ES.Nombre) END) FROM Utb_Digitalizacion DIG JOIN Utb_Estado ES ON DIG.IdEstado = ES.IdEstado WHERE IdDocumento = DOC.IdDocumento  AND IdPersona = COLA.IdPersona ),'REGISTRO')Estado,
	(SELECT top 1 Observacion FROM Utb_Digitalizacion WHERE IdDocumento = DOC.IdDocumento  AND IdPersona = COLA.IdPersona )Observacion,
	(SELECT top 1 IdDocumentoAdjunto FROM Utb_DocumentoAdjunto WHERE IdDocumento = DOC.IdDocumento  AND IdPersona = COLA.IdPersona )IdDocumentoAdjunto,
	ISNULL((SELECT TOP 1 (CASE WHEN ES.IdEstado='ES0401'  THEN  'APROBADO' ELSE 'DESAPROBADO' END) FROM Utb_DocumentoAdjunto DOAC JOIN Utb_Estado ES ON DOAC.IdEstado = ES.IdEstado WHERE ES.IdEstado BETWEEN 'ES0401' AND 'ES0499' AND DOAC.IdDocumento = DOC.IdDocumento  AND DOAC.IdPersona = COLA.IdPersona ),'')EstadoAdjunto,
	ISNULL((SELECT TOP 1 DOAC.Observacion FROM Utb_DocumentoAdjunto DOAC JOIN Utb_Estado ES ON DOAC.IdEstado = ES.IdEstado WHERE ES.IdEstado BETWEEN 'ES0401' AND 'ES0499' AND DOAC.IdDocumento = DOC.IdDocumento  AND DOAC.IdPersona = COLA.IdPersona ),'')ObservacionAdjunto
  FROM Utb_Colaborador COLA 
  JOIN Utb_CategoriaEndidad CADE ON COLA.IdEntidad = CADE.IdEntidad and CADE.IdCategoria = COLA.IdCategoria
  JOIN Utb_Categoria CA ON COLA.IdCategoria = CA.IdCategoria
  JOIN Utb_CategoriaDocumento CADO ON COLA.IdCategoria = CADO.IdCategoria AND CADO.IdEntidad = COLA.IdEntidad
  JOIN Utb_Documento DOC ON CADO.IdDocumento = DOC.IdDocumento
  JOIN Utb_Persona PER ON COLA.IdPersona = PER.IdPersona )P
 " + 
	@WhereFilter + 
	") SELECT *
	FROM Consulta
	CROSS JOIN (SELECT Count(*) AS Cantidad FROM Consulta) AS CC
	ORDER BY " +
	CASE WHEN ISNULL(@OrderBy,'') != '' THEN (@OrderBy) 
	ELSE " IdPersona DESC " END
	    + ' ' +
	" OFFSET " + CONVERT(VARCHAR, (@Start)) + 
	" ROWS FETCH NEXT " + CONVERT(VARCHAR, @AmountRows) + " ROWS ONLY"
	END
	IF(@Parameters='EMPRESA')
	 BEGIN
	SET @SentenciaSQL =  "
	;WITH Consulta AS (

			  SELECT *from(
	SELECT EMP.IdEmpresa Id, EMP.IdEmpresa, EMP.IdPersona, EMP.RazonSocial Nombre,EMP.IdCategoria,CA.Nombre Categoria,DOC.IdDocumento,DOC.Nombre Documento,
	convert(varchar(20),(SELECT top 1 FechaVencimiento FROM Utb_Digitalizacion WHERE IdDocumento = DOC.IdDocumento  AND IdPersona = EMP.IdPersona) ,103)Fecha,
    (Select COUNT(*) from Utb_DocumentoAdjunto where IdPersona = EMP.IdPersona and IdDocumento =DOC.IdDocumento)Adjuntado,(CASE WHEN DOC.Obligatorio=1  THEN  'SI' ELSE 'NO' END) Obligatorio, (CASE WHEN DOC.FechaVencimiento=1  THEN  'SI' ELSE 'NO' END) FechaVencimiento,
    ISNULL((SELECT top 1 (CASE WHEN  UPPER(ES.Nombre) IS NULL THEN 'REGISTRO' ELSE UPPER(ES.Nombre) END) FROM Utb_Digitalizacion DIG JOIN Utb_Estado ES ON DIG.IdEstado = ES.IdEstado WHERE IdDocumento = DOC.IdDocumento  AND IdPersona = EMP.IdPersona ),'REGISTRO')Estado,
	(SELECT top 1 Observacion FROM Utb_Digitalizacion WHERE IdDocumento = DOC.IdDocumento  AND IdPersona = EMP.IdPersona )Observacion,
	(SELECT top 1 IdDocumentoAdjunto FROM Utb_DocumentoAdjunto WHERE IdDocumento = DOC.IdDocumento  AND IdPersona = EMP.IdPersona )IdDocumentoAdjunto,
	(SELECT top 1 IdDigitalizacion FROM Utb_Digitalizacion WHERE IdDocumento = DOC.IdDocumento  AND IdPersona = EMP.IdPersona )IdDigitalizacion,
	ISNULL((SELECT TOP 1 (CASE WHEN ES.IdEstado='ES0401'  THEN  'APROBADO' ELSE 'DESAPROBADO' END) FROM Utb_DocumentoAdjunto DOAC JOIN Utb_Estado ES ON DOAC.IdEstado = ES.IdEstado WHERE ES.IdEstado BETWEEN 'ES0401' AND 'ES0499' AND DOAC.IdDocumento = DOC.IdDocumento  AND DOAC.IdPersona = EMP.IdPersona ),'')EstadoAdjunto,
	ISNULL((SELECT TOP 1 DOAC.Observacion FROM Utb_DocumentoAdjunto DOAC JOIN Utb_Estado ES ON DOAC.IdEstado = ES.IdEstado WHERE ES.IdEstado BETWEEN 'ES0401' AND 'ES0499' AND DOAC.IdDocumento = DOC.IdDocumento  AND DOAC.IdPersona = EMP.IdPersona ),'')ObservacionAdjunto
  FROM Utb_Empresa EMP 
  LEFT JOIN Utb_CategoriaEndidad CADE ON EMP.IdEntidad = CADE.IdEntidad and CADE.IdCategoria = EMP.IdCategoria
  LEFT JOIN Utb_Categoria CA ON EMP.IdCategoria = CA.IdCategoria
  LEFT JOIN Utb_CategoriaDocumento CADO ON EMP.IdCategoria = CADO.IdCategoria AND CADO.IdEntidad = EMP.IdEntidad
  LEFT JOIN Utb_Documento DOC ON CADO.IdDocumento = DOC.IdDocumento
  LEFT JOIN Utb_Persona PER ON EMP.IdPersona = PER.IdPersona )P
 " + 
	@WhereFilter + 
	") SELECT *
	FROM Consulta
	CROSS JOIN (SELECT Count(*) AS Cantidad FROM Consulta) AS CC
	ORDER BY " +
	CASE WHEN ISNULL(@OrderBy,'') != '' THEN (@OrderBy) 
	ELSE " IdPersona DESC " END
	    + ' ' +
	" OFFSET " + CONVERT(VARCHAR, (@Start)) + 
	" ROWS FETCH NEXT " + CONVERT(VARCHAR, @AmountRows) + " ROWS ONLY"
	END
	PRINT (@SentenciaSQL);
    EXECUTE sp_executesql  @stmt = @SentenciaSQL

END




GO
/****** Object:  StoredProcedure [dbo].[Sp_ReporteDocumentoPaginado]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

-- =====================================================
-- Author:  LA
-- Create date: 15/04/2018
-- Description: Listar REPORTE
-- Test:EXEC [dbo].[Sp_ReporteDocumentoPaginado] @WhereFilter = "WHERE P.Estado <> '' ", @OrderBy = 'IdPersona ASC', @Start = 0, @AmountRows = 10,@Parameters="COLABORADOR"
-- =====================================================
CREATE PROCEDURE [dbo].[Sp_ReporteDocumentoPaginado]
@WhereFilter VARCHAR(MAX) = '',
@OrderBy VARCHAR (100) = '', 
@Start INT = 0,
@AmountRows INT = 0,
@Parameters VARCHAR(200)

AS
if(@AmountRows=-1)
  set @AmountRows =100000
BEGIN
SET NOCOUNT ON;
    DECLARE @SentenciaSQL nvarchar(3072)
	if(@Parameters='COLABORADOR')
	BEGIN
	SET @SentenciaSQL =  "
	;WITH Consulta AS (

		   SELECT *from(
	SELECT COLA.IdColaborador Id,  COLA.IdPersona, COLA.Nombres +' '+ COLA.ApellidoPaterno +' '+ COLA.ApellidoMaterno Nombre,CA.Nombre Categoria,DOC.Nombre Documento,
	convert(varchar(20),(SELECT top 1 FechaVencimiento FROM Utb_Digitalizacion WHERE IdDocumento = DOC.IdDocumento  AND IdPersona = COLA.IdPersona) ,103)Fecha,
    (CASE WHEN (Select COUNT(*) from Utb_DocumentoAdjunto where IdPersona = COLA.IdPersona and IdDocumento =DOC.IdDocumento)>0 THEN 'SI' ELSE 'NO' END)Adjuntado,
	(CASE WHEN DOC.Obligatorio=1  THEN  'SI' ELSE 'NO' END) Obligatorio, (CASE WHEN DOC.FechaVencimiento=1  THEN  'SI' ELSE 'NO' END) FechaVencimiento,
	(SELECT top 1 IdDocumentoAdjunto FROM Utb_DocumentoAdjunto WHERE IdDocumento = DOC.IdDocumento  AND IdPersona = COLA.IdPersona )IdDocumentoAdjunto,
	ISNULL((SELECT TOP 1 (CASE WHEN ES.IdEstado='ES0401'  THEN  'APROBADO' ELSE 'DESAPROBADO' END) FROM Utb_DocumentoAdjunto DOAC JOIN Utb_Estado ES ON DOAC.IdEstado = ES.IdEstado WHERE ES.IdEstado BETWEEN 'ES0401' AND 'ES0499' AND DOAC.IdDocumento = DOC.IdDocumento  AND DOAC.IdPersona = COLA.IdPersona ),'')EstadoAdjunto,
	ISNULL((SELECT TOP 1 DOAC.Observacion FROM Utb_DocumentoAdjunto DOAC JOIN Utb_Estado ES ON DOAC.IdEstado = ES.IdEstado WHERE ES.IdEstado BETWEEN 'ES0401' AND 'ES0499' AND DOAC.IdDocumento = DOC.IdDocumento  AND DOAC.IdPersona = COLA.IdPersona ),'')ObservacionAdjunto,
    CONVERT(VARCHAR,(SELECT DATEDIFF(day,GETDATE(), (SELECT top 1 FechaVencimiento FROM Utb_Digitalizacion WHERE IdDocumento = DOC.IdDocumento  AND IdPersona = COLA.IdPersona)))) DiasRestante,
	'activo'Estado

  FROM Utb_Colaborador COLA 
  JOIN Utb_CategoriaEndidad CADE ON COLA.IdEntidad = CADE.IdEntidad and CADE.IdCategoria = COLA.IdCategoria
  JOIN Utb_Categoria CA ON COLA.IdCategoria = CA.IdCategoria
  JOIN Utb_CategoriaDocumento CADO ON COLA.IdCategoria = CADO.IdCategoria AND CADO.IdEntidad = COLA.IdEntidad
  JOIN Utb_Documento DOC ON CADO.IdDocumento = DOC.IdDocumento
  JOIN Utb_Persona PER ON COLA.IdPersona = PER.IdPersona )P	


 " + 
	@WhereFilter + 
	") SELECT *
	FROM Consulta
	CROSS JOIN (SELECT Count(*) AS Cantidad FROM Consulta) AS CC
	ORDER BY " +
	CASE WHEN ISNULL(@OrderBy,'') != '' THEN (@OrderBy) 
	ELSE " IdPersona DESC " END
	    + ' ' +
	" OFFSET " + CONVERT(VARCHAR, (@Start)) + 
	" ROWS FETCH NEXT " + CONVERT(VARCHAR, @AmountRows) + " ROWS ONLY"
	END
	IF(@Parameters='EMPRESA')
	 BEGIN
	SET @SentenciaSQL =  "
	;WITH Consulta AS (
		  SELECT *from(
	SELECT EMP.IdEmpresa Id, EMP.IdEmpresa, EMP.IdPersona, EMP.RazonSocial Nombre,EMP.IdCategoria,CA.Nombre Categoria,DOC.IdDocumento,DOC.Nombre Documento,
	convert(varchar(20),(SELECT top 1 FechaVencimiento FROM Utb_Digitalizacion WHERE IdDocumento = DOC.IdDocumento  AND IdPersona = EMP.IdPersona) ,103)Fecha,
       (CASE WHEN (Select COUNT(*) from Utb_DocumentoAdjunto where IdPersona = EMP.IdPersona and IdDocumento =DOC.IdDocumento)>0 THEN 'SI' ELSE 'NO' END)Adjuntado,
	(CASE WHEN DOC.Obligatorio=1  THEN  'SI' ELSE 'NO' END) Obligatorio, (CASE WHEN DOC.FechaVencimiento=1  THEN  'SI' ELSE 'NO' END) FechaVencimiento,
	(SELECT top 1 IdDocumentoAdjunto FROM Utb_DocumentoAdjunto WHERE IdDocumento = DOC.IdDocumento  AND IdPersona = EMP.IdPersona )IdDocumentoAdjunto,
	(SELECT top 1 IdDigitalizacion FROM Utb_Digitalizacion WHERE IdDocumento = DOC.IdDocumento  AND IdPersona = EMP.IdPersona )IdDigitalizacion,
	ISNULL((SELECT TOP 1 (CASE WHEN ES.IdEstado='ES0401'  THEN  'APROBADO' ELSE 'DESAPROBADO' END) FROM Utb_DocumentoAdjunto DOAC JOIN Utb_Estado ES ON DOAC.IdEstado = ES.IdEstado WHERE ES.IdEstado BETWEEN 'ES0401' AND 'ES0499' AND DOAC.IdDocumento = DOC.IdDocumento  AND DOAC.IdPersona = EMP.IdPersona ),'')EstadoAdjunto,
	ISNULL((SELECT TOP 1 DOAC.Observacion FROM Utb_DocumentoAdjunto DOAC JOIN Utb_Estado ES ON DOAC.IdEstado = ES.IdEstado WHERE ES.IdEstado BETWEEN 'ES0401' AND 'ES0499' AND DOAC.IdDocumento = DOC.IdDocumento  AND DOAC.IdPersona = EMP.IdPersona ),'')ObservacionAdjunto,
	CONVERT(VARCHAR,(SELECT DATEDIFF(day,GETDATE(), (SELECT top 1 FechaVencimiento FROM Utb_Digitalizacion WHERE IdDocumento = DOC.IdDocumento  AND IdPersona = EMP.IdPersona)))) DiasRestante,
	'activo'Estado
  FROM Utb_Empresa EMP 
  LEFT JOIN Utb_CategoriaEndidad CADE ON EMP.IdEntidad = CADE.IdEntidad and CADE.IdCategoria = EMP.IdCategoria
  LEFT JOIN Utb_Categoria CA ON EMP.IdCategoria = CA.IdCategoria
  LEFT JOIN Utb_CategoriaDocumento CADO ON EMP.IdCategoria = CADO.IdCategoria AND CADO.IdEntidad = EMP.IdEntidad
  LEFT JOIN Utb_Documento DOC ON CADO.IdDocumento = DOC.IdDocumento
  LEFT JOIN Utb_Persona PER ON EMP.IdPersona = PER.IdPersona )P

 " + 
	@WhereFilter + 
	") SELECT *
	FROM Consulta
	CROSS JOIN (SELECT Count(*) AS Cantidad FROM Consulta) AS CC
	ORDER BY " +
	CASE WHEN ISNULL(@OrderBy,'') != '' THEN (@OrderBy) 
	ELSE " IdPersona DESC " END
	    + ' ' +
	" OFFSET " + CONVERT(VARCHAR, (@Start)) + 
	" ROWS FETCH NEXT " + CONVERT(VARCHAR, @AmountRows) + " ROWS ONLY"
	END
	PRINT (@SentenciaSQL);
    EXECUTE sp_executesql  @stmt = @SentenciaSQL

END







  
GO
/****** Object:  StoredProcedure [dbo].[SP_ReporteEmpresaPaginado]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

-- =====================================================
-- Author:  LA
-- Create date: 15/04/2018
-- Description: Listar Empresa
-- Test:EXEC [dbo].[SP_ReporteEmpresaPaginado] @WhereFilter = "WHERE P.Estado != ''  AND P.IdEmpresa ='EM0001' OR P.IdPadre ='EM0001' OR P.IdPadreSubcontratista ='EM0001' AND P.RazonSocial LIKE '%'+'ss'+'%' ", @OrderBy = 'IdEmpresa ASC', @Start = 0, @AmountRows = 10
-- =====================================================
CREATE PROCEDURE [dbo].[SP_ReporteEmpresaPaginado]
@WhereFilter VARCHAR(MAX) = '',
@OrderBy VARCHAR (100) = '', 
@Start INT = 0,
@AmountRows INT = 0
AS
BEGIN
SET NOCOUNT ON;
    DECLARE @SentenciaSQL nvarchar(max)
	SET @SentenciaSQL =  "
	;WITH Consulta AS (
	SELECT 
	  p.IdEmpresa
	 ,P.IdPersona
	 ,P.RazonSocial
	 ,P.RUC
	 ,P.DireccionFiscal
	 ,CONVERT(int,P.EsPrincipal)EsPrincipal
	 ,CONVERT(int,P.EsContratista)EsContratista
	 ,CONVERT(int,P.EsSubContratista)EsSubContratista
	 ,P.IdEntidad
	 ,P.Entidad
	 ,P.Estado
	 ,P.TipoEmpresa
	 ,P.IdPadre
	 ,P.IdPadreSubcontratista
	 ,P.PadreSubcontratista
	 ,P.IdCategoria
	 ,P.Categoria
	 ,p.Digitalizacion
	 FROM(
	   SELECT EMP.IdPadre,EMP.IdEmpresa,PER.IdPersona,EMP.RazonSocial,EMP.RUC,EMP.DireccionFiscal, EMP.EsPrincipal,EMP.EsContratista,EMP.EsSubContratista,EN.IdEntidad,EN.Nombre Entidad, ES.Nombre Estado,
	   (CASE WHEN EMP.EsPrincipal>0 THEN 'PRINCIPAL'
	         WHEN EMP.EsContratista>0 THEN 'CONTRATISTA'
	    ELSE 'SUBCONTRATISTA' END )TipoEmpresa,
		ISNULL((SELECT top 1 (CASE WHEN   UPPER(ES.Nombre) IS NULL THEN 'REGISTRO' ELSE UPPER(ES.Nombre) END) FROM Utb_Digitalizacion DIG JOIN Utb_Estado ES ON DIG.IdEstado = ES.IdEstado WHERE  IdPersona = EMP.IdPersona ),'REGISTRO')Digitalizacion,
	 (SELECT  E.IdPadre from Utb_Empresa E where  E.IdEmpresa = EMP.IdPadre   )IdPadreSubcontratista,
	 (CASE WHEN EMP.EsPrincipal>0 THEN EMP.RazonSocial 
	 ELSE 
	   (SELECT  (CASE WHEN E.IdPadre IS NULL 
	                  THEN (SELECT EM.RazonSocial FROM Utb_Empresa EM WHERE EM.IdEmpresa = E.IdEmpresa)
			   ELSE (SELECT EM.RazonSocial FROM Utb_Empresa EM WHERE EM.IdEmpresa = E.IdPadre) END ) from Utb_Empresa E where  E.IdEmpresa = EMP.IdPadre   )
	  END)PadreSubcontratista,
	  CA.Nombre Categoria,
	  CA.IdCategoria
	 FROM Utb_Empresa EMP
	 JOIN Utb_Persona PER ON EMP.IdPersona = PER.IdPersona
	 JOIN Utb_Entidad EN ON EMP.IdEntidad = EN.IdEntidad
	 JOIN Utb_Estado ES ON  EMP.IdEstado = ES.IdEstado
	 LEFT JOIN Utb_Categoria CA ON EMP.IdCategoria = CA.IdCategoria
	 WHERE ES.IdEstado <> 'ES0203'
	 )P

 " + 
	@WhereFilter + 
	") SELECT *
	FROM Consulta
	CROSS JOIN (SELECT Count(*) AS Cantidad FROM Consulta) AS CC
	ORDER BY " +
	CASE WHEN ISNULL(@OrderBy,'') != '' THEN (@OrderBy) 
	ELSE " IdEmpresa DESC " END
	    + ' ' +
	" OFFSET " + CONVERT(VARCHAR, (@Start)) + 
	" ROWS FETCH NEXT " + CONVERT(VARCHAR, @AmountRows) + " ROWS ONLY"
	PRINT (@SentenciaSQL);
    EXECUTE sp_executesql  @stmt = @SentenciaSQL

END


GO
/****** Object:  StoredProcedure [dbo].[Sp_ReporteMarcacionesPaginado]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

-- =====================================================
-- Author:  LA
-- Create date: 15/04/2018
-- Description: Listar ColaboradorDigitalizacion
-- Test:EXEC [dbo].[Sp_ReporteMarcacionesPaginado] @WhereFilter = "WHERE P.Estado <> '' ", @OrderBy = 'Id ASC', @Start = 0, @AmountRows = 10,@Parameters="COLABORADOR"
-- =====================================================
CREATE PROCEDURE [dbo].[Sp_ReporteMarcacionesPaginado]
@WhereFilter VARCHAR(MAX) = '',
@OrderBy VARCHAR (100) = '', 
@Start INT = 0,
@AmountRows INT = 0,
@Parameters VARCHAR(MAX)

AS
if(@AmountRows=-1)
  set @AmountRows =100000
BEGIN
SET NOCOUNT ON;
    DECLARE @SentenciaSQL nvarchar(MAX)
	if(@Parameters='COLABORADOR')
	BEGIN
	SET @SentenciaSQL =  "
	;WITH Consulta AS (
  SELECT * FROM(
	select 	(CONVERT(varchar(10), mr.Fecha, 103))AS Fecha,
	MR.Fecha FechF,
    COL.IdColaborador Id,
	COL.ApellidoPaterno + ' '+ COL.ApellidoMaterno +', '+ COL.Nombres Nombre,COL.NumeroDocumento Numero,
	T.Nombre Terminal,T.IdTerminal,emp.RazonSocial as Empresa,emp.IdEmpresa,
	CON.Nombre Condicion,
	CAT.Nombre Categoria,
	(CONVERT(varchar(8), mr.Fecha, 108)) Marcacion,
	TP.Nombre TipoMarcacion,
	'ACTIVO'Estado
	from Utb_Marcacion mr 
	JOIN Utb_Colaborador COL ON mr.IdPersona = COL.IdPersona
	JOIN Utb_Terminal  T ON mr.IdTerminal = T.IdTerminal
	JOIN Utb_Empresa EMP ON COL.IdEmpresa = EMP.IdEmpresa
	JOIN Utb_Condicion CON ON COL.IdCondicion = CON.IdCondicion
	JOIN Utb_Tipo TP ON mr.IdTipo = TP.IdTipo
	JOIN Utb_Categoria CAT ON COL.IdCategoria=CAT.IdCategoria
)P
 " + 
	@WhereFilter + 	"
	) SELECT *
	FROM Consulta
	CROSS JOIN (SELECT Count(*) AS Cantidad FROM Consulta) AS CC
	ORDER BY " +
	CASE WHEN ISNULL(@OrderBy,'') != '' THEN (@OrderBy) 
	ELSE "FechaF, Id DESC " END
	    + ' ' +
	" OFFSET " + CONVERT(VARCHAR, (@Start)) + 
	" ROWS FETCH NEXT " + CONVERT(VARCHAR, @AmountRows) + " ROWS ONLY"
	END
	IF(@Parameters='EMPRESA')
	 BEGIN
	SET @SentenciaSQL =  "
	;WITH Consulta AS (

  SELECT * FROM(
	select 	(CONVERT(varchar(10), mr.Fecha, 103))AS Fecha,
	MR.Fecha FechF,
    COL.IdEmpresa Id,
	COL.RazonSocial Nombre,COL.RUC Numero,
	T.Nombre Terminal,T.IdTerminal,emp.RazonSocial as Empresa,emp.IdEmpresa,
	'' Condicion,
	CAT.Nombre Categoria,
	(CONVERT(varchar(8), mr.Fecha, 108)) Marcacion,
	TP.Nombre TipoMarcacion,
	'ACTIVO'Estado
	from Utb_Marcacion mr 
	JOIN Utb_Empresa COL ON mr.IdPersona = COL.IdPersona
	JOIN Utb_Terminal  T ON mr.IdTerminal = T.IdTerminal
	JOIN Utb_Empresa EMP ON COL.IdEmpresa = EMP.IdEmpresa
	JOIN Utb_Tipo TP ON mr.IdTipo = TP.IdTipo
	JOIN Utb_Categoria CAT ON COL.IdCategoria=CAT.IdCategoria
)P
 " + 
	@WhereFilter + 
	") SELECT *
	FROM Consulta
	CROSS JOIN (SELECT Count(*) AS Cantidad FROM Consulta) AS CC
	ORDER BY " +
	CASE WHEN ISNULL(@OrderBy,'') != '' THEN (@OrderBy) 
	ELSE " FechaF, Id DESC " END
	    + ' ' +
	" OFFSET " + CONVERT(VARCHAR, (@Start)) + 
	" ROWS FETCH NEXT " + CONVERT(VARCHAR, @AmountRows) + " ROWS ONLY"
	END
	PRINT (@SentenciaSQL);
    EXECUTE sp_executesql  @stmt = @SentenciaSQL

END




GO
/****** Object:  StoredProcedure [dbo].[Sp_ReporteOchoMarcacionesPaginado]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

-- =====================================================
-- Author:  LA
-- Create date: 15/04/2018
-- Description: Listar ColaboradorDigitalizacion
-- Test:EXEC [dbo].[Sp_ReporteOchoMarcacionesPaginado] @WhereFilter = "WHERE P.Estado <> '' ", @OrderBy = 'Id ASC', @Start = 0, @AmountRows = 10,@Parameters="COLABORADOR"
-- =====================================================
CREATE PROCEDURE [dbo].[Sp_ReporteOchoMarcacionesPaginado]
@WhereFilter VARCHAR(MAX) = '',
@OrderBy VARCHAR (100) = '', 
@Start INT = 0,
@AmountRows INT = 0,
@Parameters VARCHAR(MAX)

AS
if(@AmountRows=-1)
  set @AmountRows =100000
BEGIN
SET NOCOUNT ON;
    DECLARE @SentenciaSQL nvarchar(MAX)
	if(@Parameters='COLABORADOR')
	BEGIN
	SET @SentenciaSQL =  "
	;WITH Consulta AS (
  SELECT * FROM (
  SELECT * FROM(
	select (SELECT COUNT(Fecha) FROM Utb_Marcacion WHERE (CONVERT(varchar(10),Fecha,120))=(CONVERT(varchar(10),mr.Fecha,120)) and IdTerminal= mr.IdTerminal and IdPersona =mr.IdPersona)as Marcaciones,
	COL.IdColaborador Id,
	COL.ApellidoPaterno + ' '+ COL.ApellidoMaterno +', '+ COL.Nombres Nombre,COL.NumeroDocumento Numero,
	T.Nombre Terminal,T.IdTerminal,emp.RazonSocial as Empresa,emp.IdEmpresa,
	(CONVERT(varchar(10), mr.Fecha, 103))AS Fecha,
    (SELECT MIN(T.Hora)Hora FROM (SELECT CONVERT(varchar(8),Fecha,108)Hora FROM Utb_Marcacion WHERE (CONVERT(varchar(10),Fecha,120))=(CONVERT(varchar(10),mr.Fecha,120)) and IdTerminal= mr.IdTerminal and IdPersona =mr.IdPersona )T) PrimeraMarcacion,
				(SELECT TOP 2 MAX(CONVERT(varchar(8),Fecha,108))Hora FROM Utb_Marcacion WHERE (CONVERT(varchar(10),Fecha,120))=(CONVERT(varchar(10),mr.Fecha,120)) and IdTerminal= mr.IdTerminal and IdPersona =mr.IdPersona) 
				 SegundaMarcacion,
				 (CASE WHEN (SELECT COUNT(Fecha) FROM Utb_Marcacion WHERE (CONVERT(varchar(10),Fecha,120))=(CONVERT(varchar(10),mr.Fecha,120)) and IdTerminal= mr.IdTerminal and IdPersona =mr.IdPersona)=3 
				    THEN (SELECT TOP 3 MAX(CONVERT(varchar(8),Fecha,108))Hora FROM Utb_Marcacion WHERE (CONVERT(varchar(10),Fecha,120))=(CONVERT(varchar(10),mr.Fecha,120)) and IdTerminal= mr.IdTerminal and IdPersona =mr.IdPersona)
					END ) 
				 TerceraMarcacion,
				  (CASE WHEN (SELECT COUNT(Fecha) FROM Utb_Marcacion WHERE (CONVERT(varchar(10),Fecha,120))=(CONVERT(varchar(10),mr.Fecha,120)) and IdTerminal= mr.IdTerminal and IdPersona =mr.IdPersona)=4
				    THEN (SELECT TOP 4 MAX(CONVERT(varchar(8),Fecha,108))Hora FROM Utb_Marcacion WHERE (CONVERT(varchar(10),Fecha,120))=(CONVERT(varchar(10),mr.Fecha,120)) and IdTerminal= mr.IdTerminal and IdPersona =mr.IdPersona)
					END )  
				 CuartaMarcacion,
				  (CASE WHEN (SELECT COUNT(Fecha) FROM Utb_Marcacion WHERE (CONVERT(varchar(10),Fecha,120))=(CONVERT(varchar(10),mr.Fecha,120)) and IdTerminal= mr.IdTerminal and IdPersona =mr.IdPersona)=5 
				    THEN (SELECT TOP 5 MAX(CONVERT(varchar(8),Fecha,108))Hora FROM Utb_Marcacion WHERE (CONVERT(varchar(10),Fecha,120))=(CONVERT(varchar(10),mr.Fecha,120)) and IdTerminal= mr.IdTerminal and IdPersona =mr.IdPersona)
					END )  
				 QuintaMarcacion,
				  (CASE WHEN (SELECT COUNT(Fecha) FROM Utb_Marcacion WHERE (CONVERT(varchar(10),Fecha,120))=(CONVERT(varchar(10),mr.Fecha,120)) and IdTerminal= mr.IdTerminal and IdPersona =mr.IdPersona)=6 
				    THEN (SELECT TOP 6 MAX(CONVERT(varchar(8),Fecha,108))Hora FROM Utb_Marcacion WHERE (CONVERT(varchar(10),Fecha,120))=(CONVERT(varchar(10),mr.Fecha,120)) and IdTerminal= mr.IdTerminal and IdPersona =mr.IdPersona)
					END )  
				 SextaMarcacion,
				  (CASE WHEN (SELECT COUNT(Fecha) FROM Utb_Marcacion WHERE (CONVERT(varchar(10),Fecha,120))=(CONVERT(varchar(10),mr.Fecha,120)) and IdTerminal= mr.IdTerminal and IdPersona =mr.IdPersona)=7 
				    THEN (SELECT TOP 7 MAX(CONVERT(varchar(8),Fecha,108))Hora FROM Utb_Marcacion WHERE (CONVERT(varchar(10),Fecha,120))=(CONVERT(varchar(10),mr.Fecha,120)) and IdTerminal= mr.IdTerminal and IdPersona =mr.IdPersona)
					END )  
				 SeptimaMarcacion,
				  (CASE WHEN (SELECT COUNT(Fecha) FROM Utb_Marcacion WHERE (CONVERT(varchar(10),Fecha,120))=(CONVERT(varchar(10),mr.Fecha,120)) and IdTerminal= mr.IdTerminal and IdPersona =mr.IdPersona)=8 
				    THEN (SELECT TOP 8 MAX(CONVERT(varchar(8),Fecha,108))Hora FROM Utb_Marcacion WHERE (CONVERT(varchar(10),Fecha,120))=(CONVERT(varchar(10),mr.Fecha,120)) and IdTerminal= mr.IdTerminal and IdPersona =mr.IdPersona)
					END )  
				 OctavaMarcacion,'ACTIVO'Estado
	from Utb_Marcacion mr 
	JOIN Utb_Colaborador COL ON mr.IdPersona = COL.IdPersona
	JOIN Utb_Terminal  T ON mr.IdTerminal = T.IdTerminal
	JOIN Utb_Empresa EMP ON COL.IdEmpresa = EMP.IdEmpresa
	GROUP BY COL.IdColaborador,COL.ApellidoMaterno,COL.ApellidoPaterno,COL.Nombres,T.Nombre,EMP.RazonSocial,EMP.IdEmpresa,mr.Fecha,mr.IdTerminal,mr.IdPersona,T.IdTerminal            
	         ,COL.NumeroDocumento

)Z
GROUP BY Z.Id,Z.Nombre,Z.Fecha,Z.Marcaciones,Z.Terminal,Z.Empresa,Z.IdEmpresa,Z.PrimeraMarcacion,Z.SegundaMarcacion,Z.TerceraMarcacion,Z.CuartaMarcacion,
         Z.QuintaMarcacion,Z.SextaMarcacion,Z.SeptimaMarcacion,Z.OctavaMarcacion,Z.IdTerminal,Z.Numero,Z.Estado
)P

 " + 
	@WhereFilter + 
	") SELECT *
	FROM Consulta
	CROSS JOIN (SELECT Count(*) AS Cantidad FROM Consulta) AS CC
	ORDER BY " +
	CASE WHEN ISNULL(@OrderBy,'') != '' THEN (@OrderBy) 
	ELSE " Id DESC " END
	    + ' ' +
	" OFFSET " + CONVERT(VARCHAR, (@Start)) + 
	" ROWS FETCH NEXT " + CONVERT(VARCHAR, @AmountRows) + " ROWS ONLY"
	END
	IF(@Parameters='EMPRESA')
	 BEGIN
	SET @SentenciaSQL =  "
	;WITH Consulta AS (

			 SELECT * FROM (
SELECT * FROM(
	select (SELECT COUNT(Fecha) FROM Utb_Marcacion WHERE (CONVERT(varchar(10),Fecha,120))=(CONVERT(varchar(10),mr.Fecha,120)) and IdTerminal= mr.IdTerminal and IdPersona =mr.IdPersona)as Marcaciones,
	COL.IdEmpresa Id,
	COL.RazonSocial Nombre,COL.RUC Numero,
	T.Nombre Terminal,T.IdTerminal,emp.RazonSocial as Empresa,emp.IdEmpresa,
	(CONVERT(varchar(10), mr.Fecha, 103))AS Fecha,
    (SELECT MIN(T.Hora)Hora FROM (SELECT CONVERT(varchar(8),Fecha,108)Hora FROM Utb_Marcacion WHERE (CONVERT(varchar(10),Fecha,120))=(CONVERT(varchar(10),mr.Fecha,120)) and IdTerminal= mr.IdTerminal and IdPersona =mr.IdPersona )T) PrimeraMarcacion,
				(SELECT TOP 2 MAX(CONVERT(varchar(8),Fecha,108))Hora FROM Utb_Marcacion WHERE (CONVERT(varchar(10),Fecha,120))=(CONVERT(varchar(10),mr.Fecha,120)) and IdTerminal= mr.IdTerminal and IdPersona =mr.IdPersona) 
				 SegundaMarcacion,
				 (CASE WHEN (SELECT COUNT(Fecha) FROM Utb_Marcacion WHERE (CONVERT(varchar(10),Fecha,120))=(CONVERT(varchar(10),mr.Fecha,120)) and IdTerminal= mr.IdTerminal and IdPersona =mr.IdPersona)=3 
				    THEN (SELECT TOP 3 MAX(CONVERT(varchar(8),Fecha,108))Hora FROM Utb_Marcacion WHERE (CONVERT(varchar(10),Fecha,120))=(CONVERT(varchar(10),mr.Fecha,120)) and IdTerminal= mr.IdTerminal and IdPersona =mr.IdPersona)
					END ) 
				 TerceraMarcacion,
				  (CASE WHEN (SELECT COUNT(Fecha) FROM Utb_Marcacion WHERE (CONVERT(varchar(10),Fecha,120))=(CONVERT(varchar(10),mr.Fecha,120)) and IdTerminal= mr.IdTerminal and IdPersona =mr.IdPersona)=4
				    THEN (SELECT TOP 4 MAX(CONVERT(varchar(8),Fecha,108))Hora FROM Utb_Marcacion WHERE (CONVERT(varchar(10),Fecha,120))=(CONVERT(varchar(10),mr.Fecha,120)) and IdTerminal= mr.IdTerminal and IdPersona =mr.IdPersona)
					END )  
				 CuartaMarcacion,
				  (CASE WHEN (SELECT COUNT(Fecha) FROM Utb_Marcacion WHERE (CONVERT(varchar(10),Fecha,120))=(CONVERT(varchar(10),mr.Fecha,120)) and IdTerminal= mr.IdTerminal and IdPersona =mr.IdPersona)=5 
				    THEN (SELECT TOP 5 MAX(CONVERT(varchar(8),Fecha,108))Hora FROM Utb_Marcacion WHERE (CONVERT(varchar(10),Fecha,120))=(CONVERT(varchar(10),mr.Fecha,120)) and IdTerminal= mr.IdTerminal and IdPersona =mr.IdPersona)
					END )  
				 QuintaMarcacion,
				  (CASE WHEN (SELECT COUNT(Fecha) FROM Utb_Marcacion WHERE (CONVERT(varchar(10),Fecha,120))=(CONVERT(varchar(10),mr.Fecha,120)) and IdTerminal= mr.IdTerminal and IdPersona =mr.IdPersona)=6 
				    THEN (SELECT TOP 6 MAX(CONVERT(varchar(8),Fecha,108))Hora FROM Utb_Marcacion WHERE (CONVERT(varchar(10),Fecha,120))=(CONVERT(varchar(10),mr.Fecha,120)) and IdTerminal= mr.IdTerminal and IdPersona =mr.IdPersona)
					END )  
				 SextaMarcacion,
				  (CASE WHEN (SELECT COUNT(Fecha) FROM Utb_Marcacion WHERE (CONVERT(varchar(10),Fecha,120))=(CONVERT(varchar(10),mr.Fecha,120)) and IdTerminal= mr.IdTerminal and IdPersona =mr.IdPersona)=7 
				    THEN (SELECT TOP 7 MAX(CONVERT(varchar(8),Fecha,108))Hora FROM Utb_Marcacion WHERE (CONVERT(varchar(10),Fecha,120))=(CONVERT(varchar(10),mr.Fecha,120)) and IdTerminal= mr.IdTerminal and IdPersona =mr.IdPersona)
					END )  
				 SeptimaMarcacion,
				  (CASE WHEN (SELECT COUNT(Fecha) FROM Utb_Marcacion WHERE (CONVERT(varchar(10),Fecha,120))=(CONVERT(varchar(10),mr.Fecha,120)) and IdTerminal= mr.IdTerminal and IdPersona =mr.IdPersona)=8 
				    THEN (SELECT TOP 8 MAX(CONVERT(varchar(8),Fecha,108))Hora FROM Utb_Marcacion WHERE (CONVERT(varchar(10),Fecha,120))=(CONVERT(varchar(10),mr.Fecha,120)) and IdTerminal= mr.IdTerminal and IdPersona =mr.IdPersona)
					END )  
				 OctavaMarcacion
	from Utb_Marcacion mr 
	JOIN Utb_Empresa COL ON mr.IdPersona = COL.IdPersona
	JOIN Utb_Terminal  T ON mr.IdTerminal = T.IdTerminal
	JOIN Utb_Empresa EMP ON COL.IdEmpresa = EMP.IdEmpresa
	GROUP BY COL.IdEmpresa,COL.RazonSocial,T.Nombre,EMP.RazonSocial,EMP.IdEmpresa,mr.Fecha,mr.IdTerminal,mr.IdPersona,T.IdTerminal            
	         ,COL.RUC

)Z
GROUP BY Z.Id,Z.Nombre,Z.Fecha,Z.Marcaciones,Z.Terminal,Z.Empresa,Z.IdEmpresa,Z.PrimeraMarcacion,Z.SegundaMarcacion,Z.TerceraMarcacion,Z.CuartaMarcacion,
         Z.QuintaMarcacion,Z.SextaMarcacion,Z.SeptimaMarcacion,Z.OctavaMarcacion,Z.IdTerminal,Z.Numero
)P
 " + 
	@WhereFilter + 
	") SELECT *
	FROM Consulta
	CROSS JOIN (SELECT Count(*) AS Cantidad FROM Consulta) AS CC
	ORDER BY " +
	CASE WHEN ISNULL(@OrderBy,'') != '' THEN (@OrderBy) 
	ELSE " Id DESC " END
	    + ' ' +
	" OFFSET " + CONVERT(VARCHAR, (@Start)) + 
	" ROWS FETCH NEXT " + CONVERT(VARCHAR, @AmountRows) + " ROWS ONLY"
	END
	PRINT (@SentenciaSQL);
    EXECUTE sp_executesql  @stmt = @SentenciaSQL

END




GO
/****** Object:  StoredProcedure [dbo].[Sp_TerminalConsulta]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_TerminalConsulta]
@IdTerminal char(6)
as begin

SELECT [IdTerminal]
      ,TER.IdEmpresa
      ,TER.Nombre
      ,[IP]
      ,[Puerto]
      ,[Foto]
      ,TER.IdModelo
	  ,MO.Nombre Modelo
      ,TER.IdMarca
	  ,MAR.Nombre Marca
      ,TER.IdConfiguracion
	  ,CON.Nombre Configuracion
      ,TER.IdArea
	  ,AR.Nombre Area
      ,(CASE WHEN ES.IdEstado='ES0201' THEN 'true' ELSE  'false'END)IdEstado
	  ,ES.Nombre Estado
  FROM [dbo].[Utb_Terminal] TER
  JOIN Utb_Estado ES ON TER.IdEstado = ES.IdEstado
  JOIN Utb_Marca MAR ON TER.IdMarca = MAR.IdMarca
  JOIN Utb_Modelo MO ON TER.IdModelo = MO.IdModelo
  JOIN Utb_Configuracion CON ON TER.IdConfiguracion = CON.IdConfiguracion
  JOIN Utb_Area AR ON TER.IdArea = AR.IdArea WHERE IdTerminal = @IdTerminal
end


GO
/****** Object:  StoredProcedure [dbo].[Sp_TerminalDropDown]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
--Sp_TerminalDropDown ''
CREATE PROC [dbo].[Sp_TerminalDropDown]
@IdConsulta varchar(15) = ''
AS
BEGIN
 SELECT IdTerminal Value, Nombre FROM Utb_Terminal where (@IdConsulta ='' or IdEmpresa = @IdConsulta)  and IdEstado <> 'ES0203'
END
GO
/****** Object:  StoredProcedure [dbo].[Sp_TerminalEliminar]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_TerminalEliminar]
@IdTerminal CHAR(6)
AS
BEGIN
BEGIN TRY
		--Existe 
     IF  EXISTS (SELECT top 1 IdTerminal from Utb_Terminal where IdTerminal = @IdTerminal)
	  BEGIN
	  	 UPDATE Utb_Terminal SET IdEstado = 'ES0203'  WHERE IdTerminal=@IdTerminal
	  END
	  ELSE
	  BEGIN
	  	SELECT -1 AS IdTerminal
	  END

END TRY
BEGIN CATCH 
	SELECT -2 AS IdTerminal
END CATCH
end
GO
/****** Object:  StoredProcedure [dbo].[Sp_TerminalMantenimiento]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_TerminalMantenimiento]
@IdTerminal CHAR(6),
@Nombre VARCHAR(50),
@IP VARCHAR(15),
@Puerto VARCHAR(5),
@Foto IMAGE = NULL,
@IdModelo CHAR(6),
@IdMarca CHAR(6),
@IdConfiguracion CHAR(6),
@IdArea CHAR(6),
@IdEstado varchar(6),
@IdEmpresa  CHAR(6)
AS
SET @IdEstado =(CASE WHEN @IdEstado ='TRUE' THEN 'ES0201' ELSE 'ES0202' END)
BEGIN
BEGIN TRY
--Registrar
	IF(@IdTerminal = '')
	BEGIN
		    SET  @IdTerminal = ( CASE WHEN (SELECT COUNT(*) FROM Utb_Terminal)>0 THEN (SELECT 'TE' + RIGHT('000'+CONVERT(VARCHAR(4), SUBSTRING( MAX(IdTerminal),4,LEN(MAX(IdTerminal)))+1), 4) FROM Utb_Terminal) ELSE 'MO0001' END);

		--Existe 
		IF NOT EXISTS (SELECT top 1 IdTerminal from Utb_Terminal where IdTerminal = @IdTerminal)
		BEGIN
			 INSERT INTO Utb_Terminal (IdTerminal,Nombre,IP,Puerto,Foto,IdModelo,IdMarca,IdConfiguracion,IdArea,IdEstado,IdEmpresa)
			        VALUES(@IdTerminal,@Nombre,@IP,@Puerto,@Foto,@IdModelo,@IdMarca,@IdConfiguracion,@IdArea,@IdEstado,@IdEmpresa)
		END
		ELSE
		BEGIN
			SELECT -1 AS IdTerminal
		END
	END

--Actualizar
    ELSE
	BEGIN
		IF  EXISTS (SELECT top 1 IdTerminal from Utb_Terminal where IdTerminal = @IdTerminal)
		BEGIN
		    UPDATE Utb_Terminal SET Nombre=@Nombre,IP=@IP,Puerto=Puerto,Foto=@Foto,IdModelo=@IdModelo,IdMarca=@IdMarca,IdConfiguracion=@IdConfiguracion,IdArea=@IdArea,IdEstado=@IdEstado,IdEmpresa=@IdEmpresa WHERE IdTerminal = @IdTerminal
		END
		ELSE
		BEGIN
			SELECT -1 AS IdTerminal
		END

	END

END TRY
BEGIN CATCH 
	SELECT -2 AS IdTerminal
END CATCH
end
GO
/****** Object:  StoredProcedure [dbo].[Sp_TerminalPaginado]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

-- =====================================================
-- Author:  LA
-- Create date: 15/04/2018
-- Description: Listar Terminal
-- Test:EXEC [dbo].[SP_TerminalPaginado] @WhereFilter = "WHERE P.Estado <> '' ", @OrderBy = 'IdTerminal ASC', @Start = 0, @AmountRows = 10
-- =====================================================
CREATE PROCEDURE [dbo].[Sp_TerminalPaginado]
@WhereFilter VARCHAR(MAX) = '',
@OrderBy VARCHAR (100) = '', 
@Start INT = 0,
@AmountRows INT = 0
AS
BEGIN
SET NOCOUNT ON;
    DECLARE @SentenciaSQL nvarchar(3072)
	SET @SentenciaSQL =  "
	;WITH Consulta AS (
   SELECT 
     P.IdEmpresa,
	  p.IdTerminal
	 ,P.Nombre
	 ,P.IP
	 ,P.Puerto
	 ,P.Foto
	 ,P.IdModelo
	 ,P.Modelo
	 ,P.Area
	 ,P.IdArea
	 ,P.IdMarca
	 ,P.Marca
	 ,P.Configuracion
	 ,P.IdConfiguracion
	 ,P.Estado
	 FROM(
	   SELECT TER.IdEmpresa, TER.IdTerminal,TER.Nombre,TER.IP,TER.Puerto,TER.Foto,MO.IdModelo, MO.Nombre Modelo,AR.IdArea,AR.Nombre Area,MAR.IdMarca,MAR.Nombre Marca,CON.IdConfiguracion,CON.Nombre Configuracion, ES.Nombre Estado
	 FROM Utb_Terminal TER
	 JOIN Utb_Modelo MO ON TER.IdModelo = MO.IdModelo
	 JOIN Utb_Area AR ON TER.IdArea = AR.IdArea
	 JOIN Utb_Marca MAR ON TER.IdMarca = MAR.IdMarca
	 JOIN Utb_Configuracion CON ON TER.IdConfiguracion = CON.IdConfiguracion
	 JOIN Utb_Estado ES ON  TER.IdEstado = ES.IdEstado
	 WHERE ES.IdEstado <> 'ES0203'
	 )P

 " + 
	@WhereFilter + 
	") SELECT *
	FROM Consulta
	CROSS JOIN (SELECT Count(*) AS Cantidad FROM Consulta) AS CC
	ORDER BY " +
	CASE WHEN ISNULL(@OrderBy,'') != '' THEN (@OrderBy) 
	ELSE " IdTerminal DESC " END
	    + ' ' +
	" OFFSET " + CONVERT(VARCHAR, (@Start)) + 
	" ROWS FETCH NEXT " + CONVERT(VARCHAR, @AmountRows) + " ROWS ONLY"
	PRINT (@SentenciaSQL);
    EXECUTE sp_executesql  @stmt = @SentenciaSQL

END
GO
/****** Object:  StoredProcedure [dbo].[Sp_TipoDropDown]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
--Sp_TipoDropDown ''
CREATE PROC [dbo].[Sp_TipoDropDown]
@IdConsulta varchar(15)
AS
BEGIN
 SELECT IdTipo Value, Nombre FROM Utb_Tipo
END
GO
/****** Object:  StoredProcedure [dbo].[Sp_TipoEliminar]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_TipoEliminar]
@IdTipo INT
AS
BEGIN
BEGIN TRY
		--Existe 
     IF  EXISTS (SELECT top 1 IdTipo from Utb_Tipo where IdTipo = @IdTipo)
	  BEGIN
	  	 UPDATE Utb_Tipo SET IdEstado = 'ES0203'  WHERE IdTipo=@IdTipo
	  END
	  ELSE
	  BEGIN
	  	SELECT -1 AS IdTipo
	  END

END TRY
BEGIN CATCH 
	SELECT -2 AS IdTipo
END CATCH
end
GO
/****** Object:  StoredProcedure [dbo].[Sp_TipoLugarDropDown]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Sp_TipoLugarDropDown]
@IdConsulta char(6)
AS
BEGIN
SELECT IdTipoLugar Value
      ,Nombre,(CASE WHEN Nombre ='Foraneo' THEN 'NO' ELSE 'SI' END) Valor1
  FROM Utb_TipoLugar 

END
GO
/****** Object:  StoredProcedure [dbo].[Sp_TipoMantenimiento]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_TipoMantenimiento]
@IdTipo CHAR(6),
@Nombre VARCHAR(50),
@IdEstado varchar(10)
AS
SET @IdEstado =(CASE WHEN @IdEstado ='TRUE' THEN 'ES0201' ELSE 'ES0202' END)
BEGIN
BEGIN TRY
--Registrar
	IF(@IdTipo = '')
	BEGIN
		--Existe 
		IF NOT EXISTS (SELECT top 1 IdTipo from Utb_Tipo where IdTipo = @IdTipo)
		BEGIN
			 INSERT INTO Utb_Tipo (IdTipo,Nombre,IdEstado)
			        VALUES(@IdTipo,@Nombre,@IdEstado)
		END
		ELSE
		BEGIN
			SELECT -1 AS IdTipo
		END
	END

--Actualizar
    ELSE
	BEGIN
		IF  EXISTS (SELECT top 1 IdTipo from Utb_Tipo where IdTipo = @IdTipo)
		BEGIN
		    UPDATE Utb_Tipo SET Nombre=@Nombre,IdEstado=@IdEstado WHERE IdTipo = @IdTipo
		END
		ELSE
		BEGIN
			SELECT -1 AS IdTipo
		END

	END

END TRY
BEGIN CATCH 
	SELECT -2 AS IdTipo
END CATCH
end
GO
/****** Object:  StoredProcedure [dbo].[Sp_TipoPaginado]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

-- =====================================================
-- Author:  LA
-- Create date: 15/04/2018
-- Description: Listar Tipo
-- Test:EXEC [dbo].[SP_TipoPaginado] @WhereFilter = "WHERE P.Estado <> '' ", @OrderBy = 'IdTipo ASC', @Start = 0, @AmountRows = 10
-- =====================================================
CREATE PROCEDURE [dbo].[Sp_TipoPaginado]
@WhereFilter VARCHAR(MAX) = '',
@OrderBy VARCHAR (100) = '', 
@Start INT = 0,
@AmountRows INT = 0
AS
BEGIN
SET NOCOUNT ON;
    DECLARE @SentenciaSQL nvarchar(3072)
	SET @SentenciaSQL =  "
	;WITH Consulta AS (
   SELECT 
	  p.IdTipo
	 ,P.Nombre
	 ,P.Estado
	 FROM(
	   SELECT TIP.IdTipo,TIP.Nombre, ES.Nombre Estado
	 FROM Utb_Tipo TIP
	 JOIN Utb_Estado ES ON  TIP.IdEstado = ES.IdEstado
	 WHERE ES.IdEstado <> 'ES0203'
	 )P

 " + 
	@WhereFilter + 
	") SELECT *
	FROM Consulta
	CROSS JOIN (SELECT Count(*) AS Cantidad FROM Consulta) AS CC
	ORDER BY " +
	CASE WHEN ISNULL(@OrderBy,'') != '' THEN (@OrderBy) 
	ELSE " IdTipo DESC " END
	    + ' ' +
	" OFFSET " + CONVERT(VARCHAR, (@Start)) + 
	" ROWS FETCH NEXT " + CONVERT(VARCHAR, @AmountRows) + " ROWS ONLY"
	PRINT (@SentenciaSQL);
    EXECUTE sp_executesql  @stmt = @SentenciaSQL

END
GO
/****** Object:  StoredProcedure [dbo].[Sp_UbicacionConsulta]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_UbicacionConsulta]
@IdUbicacion char(6)
as
begin

SELECT [IdUbicacion]
      ,UB.Nombre
      ,UB.Nivel
      ,UB.IdPrincipal
      ,UB.IdDepartamento
	  ,DEP.Nombre Departamento,
	  (CASE WHEN IdEstado='ES0201' THEN 'true' ELSE  'false'END)IdEstado
	  ,UB.IdEmpresa
  FROM [dbo].[Utb_Ubicacion] UB
  LEFT JOIN Utb_Departamento DEP ON UB.IdDepartamento = DEP.IdDepartamento where IdUbicacion = @IdUbicacion
end

GO
/****** Object:  StoredProcedure [dbo].[Sp_UbicacionDropDown]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
--Sp_UbicacionDropDown ''
CREATE PROC [dbo].[Sp_UbicacionDropDown]
@IdConsulta varchar(15)
AS
BEGIN
 SELECT IdUbicacion Value, Nombre,'' Valor1 FROM Utb_Ubicacion WHERE (@IdConsulta ='' OR IdDepartamento=@IdConsulta)
END
GO
/****** Object:  StoredProcedure [dbo].[Sp_UbicacionEliminar]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_UbicacionEliminar]
@IdUbicacion char(6)
AS
BEGIN
BEGIN TRY
		--Existe 
     IF  EXISTS (SELECT top 1 IdUbicacion from Utb_Ubicacion where IdUbicacion = @IdUbicacion)
	  BEGIN
	  	 DELETE FROM Utb_Ubicacion   WHERE IdUbicacion=@IdUbicacion
	  END
	  ELSE
	  BEGIN
	  	SELECT -1 AS IdUbicacion
	  END

END TRY
BEGIN CATCH 
	SELECT -2 AS IdUbicacion
END CATCH
end
GO
/****** Object:  StoredProcedure [dbo].[Sp_UbicacionMantenimiento]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_UbicacionMantenimiento]
@IdUbicacion CHAR(6),
@Nombre VARCHAR(50),
@Nivel INT = null,
@IdDepartamento char(6),
@IdEstado varchar(10),
@IdEmpresa CHAR(6)
AS
SET @IdEstado =(CASE WHEN @IdEstado ='TRUE' THEN 'ES0201' ELSE 'ES0202' END)
BEGIN
BEGIN TRY

--Registrar
	IF(@IdUbicacion = '')
	BEGIN
		 SET  @IdUbicacion = ( CASE WHEN (SELECT COUNT(*) FROM Utb_Ubicacion)>0 THEN (SELECT 'UB' + RIGHT('000'+CONVERT(VARCHAR(4), SUBSTRING( MAX(IdUbicacion),4,LEN(MAX(IdUbicacion)))+1), 4) FROM Utb_Ubicacion) ELSE 'UB0001' END);

		--Existe 
		IF NOT EXISTS (SELECT top 1 IdUbicacion from Utb_Ubicacion where IdUbicacion = @IdUbicacion)
		BEGIN
			 INSERT INTO Utb_Ubicacion (IdUbicacion,Nombre,IdDepartamento,IdEmpresa)
			        VALUES(@IdUbicacion,@Nombre,@IdDepartamento,@IdEmpresa)
		END
		ELSE
		BEGIN
			SELECT -1 AS IdUbicacion
		END
	END

--Actualizar
    ELSE
	BEGIN
		IF  EXISTS (SELECT top 1 IdUbicacion from Utb_Ubicacion where IdUbicacion = @IdUbicacion)
		BEGIN
		    UPDATE Utb_Ubicacion SET Nombre=@Nombre,Nivel=@Nivel,IdDepartamento=@IdDepartamento,IdEmpresa=@IdEmpresa WHERE IdUbicacion = @IdUbicacion
		END
		ELSE
		BEGIN
			SELECT -1 AS IdUbicacion
		END

	END

END TRY
BEGIN CATCH 
	SELECT -2 AS IdUbicacion
END CATCH
end
GO
/****** Object:  StoredProcedure [dbo].[Sp_UbicacionPaginado]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

-- =====================================================
-- Author:  LA
-- Create date: 15/04/2018
-- Description: Listar Ubicacion
-- Test:EXEC [dbo].[SP_UbicacionPaginado] @WhereFilter = "WHERE P.Estado <> '' ", @OrderBy = 'IdUbicacion ASC', @Start = 0, @AmountRows = 10
-- =====================================================
CREATE PROCEDURE [dbo].[Sp_UbicacionPaginado]
@WhereFilter VARCHAR(MAX) = '',
@OrderBy VARCHAR (100) = '', 
@Start INT = 0,
@AmountRows INT = 0
AS
BEGIN
SET NOCOUNT ON;
    DECLARE @SentenciaSQL nvarchar(3072)
	SET @SentenciaSQL =  "
	;WITH Consulta AS (
    SELECT 
	  p.IdUbicacion
	 ,P.Nombre
	 ,P.Estado
	 ,P.Nivel
	 ,p.Departamento
	 ,p.IdEmpresa
	 FROM(
	   SELECT TIP.IdUbicacion,TIP.Nombre,TIP.Nivel,DEP.Nombre Departamento, 'Activo' Estado,TIP.IdEmpresa
	 FROM Utb_Ubicacion TIP
	 left JOIN Utb_Departamento DEP ON TIP.IdDepartamento= dep.IdDepartamento
	 )P

 " + 
	@WhereFilter + 
	") SELECT *
	FROM Consulta
	CROSS JOIN (SELECT Count(*) AS Cantidad FROM Consulta) AS CC
	ORDER BY " +
	CASE WHEN ISNULL(@OrderBy,'') != '' THEN (@OrderBy) 
	ELSE " IdUbicacion DESC " END
	    + ' ' +
	" OFFSET " + CONVERT(VARCHAR, (@Start)) + 
	" ROWS FETCH NEXT " + CONVERT(VARCHAR, @AmountRows) + " ROWS ONLY"
	PRINT (@SentenciaSQL);
    EXECUTE sp_executesql  @stmt = @SentenciaSQL

END
GO
/****** Object:  StoredProcedure [dbo].[Sp_UDepartamentoDropDown]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
--Sp_DepartamentoDropDown ''
CREATE PROC [dbo].[Sp_UDepartamentoDropDown]
@IdConsulta varchar(15)
AS
BEGIN
 SELECT IdDepartamento Value,Nombre,''Valor1 FROM Utb_Ubigeo WHERE IdDistrito = 00 and IdProvincia = 00
END
GO
/****** Object:  StoredProcedure [dbo].[Sp_UsuarioConsulta]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROC [dbo].[Sp_UsuarioConsulta]
@IdUsuario CHAR(6)
AS
BEGIN
  	 SELECT * FROM(
	  SELECT USU.IdUsuario
      ,USU.IdPersona
      ,USU.Usuario
      ,USU.Correo
      ,USU.Foto
      ,USU.Password
      ,(CASE WHEN USU.IdEstado='ES0201' THEN 'true' ELSE  'false'END)IdEstado
	  ,ES.Nombre Estado
      ,USU.IdPerfil
	  ,PF.Nombre Perfil
	  ,USU.IdEmpresa
	  ,(CASE WHEN EM.EsPrincipal =1 THEN 'PRINCIPAL'
	         WHEN EM.EsContratista = 1 THEN 'CONTRATISTA'
			 ELSE 'SUBCONTRATISTA' END ) Tipo
  FROM Utb_Usuario USU
  JOIN Utb_Estado ES ON USU.IdEstado= ES.IdEstado
  join Utb_Perfil PF ON USU.IdPerfil = PF.IdPerfil
  JOIN Utb_Empresa EM ON USU.IdEmpresa = EM.IdEmpresa
  )P WHERE P.IdUsuario = @IdUsuario
END
GO
/****** Object:  StoredProcedure [dbo].[Sp_UsuarioEliminar]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_UsuarioEliminar]
@IdUsuario CHAR(6)
AS
BEGIN
BEGIN TRY
--Registrar
  
  UPDATE Utb_Usuario SET IdEstado = 'ES0203' WHERE IdUsuario = @IdUsuario
END TRY
BEGIN CATCH 
	SELECT -2 AS ERROR
END CATCH
end
GO
/****** Object:  StoredProcedure [dbo].[Sp_UsuarioListado]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_UsuarioListado]
@Nombre NVARCHAR(20) = NULL,
@IdEmpresaPadre NVARCHAR(10) =NULL
AS
BEGIN
SELECT * FROM (
 SELECT [IdUsuario]
      ,[IdPersona]
      ,[Usuario]
      ,[Correo]
      ,[Foto]
      ,[Password]
      ,US.IdEstado
      ,US.IdPerfil
	  ,(CASE WHEN (SELECT EsPrincipal FROM Utb_Empresa WHERE IdEmpresa = US.IdEmpresa)= 1 THEN 'PRINCIPAL'
	         WHEN (SELECT EsContratista FROM Utb_Empresa WHERE IdEmpresa = US.IdEmpresa)= 1 THEN 'CONTRATISTA'
			 WHEN (SELECT EsPrincipal FROM Utb_Empresa WHERE IdEmpresa = US.IdEmpresa)= 1 THEN 'SUBCONTRATISTA' END )TipoEmpresa,
	   US.IdEmpresa,	
	   PR.Nombre AS Perfil,
	   (CASE WHEN (SELECT EsPrincipal FROM Utb_Empresa WHERE IdEmpresa = US.IdEmpresa)= 1 THEN US.IdEmpresa
	         WHEN (SELECT EsContratista FROM Utb_Empresa WHERE IdEmpresa = US.IdEmpresa)= 1   THEN (SELECT  E.IdPadre from Utb_Empresa E where  E.IdEmpresa = US.IdEmpresa)
	    ELSE (SELECT  E.IdPadre from Utb_Empresa E where  E.IdEmpresa = (SELECT E.IdPadre FROM Utb_Empresa E WHERE E.IdEmpresa = US.IdEmpresa )) END)IdEmpresaPadre
  FROM [dbo].[Utb_Usuario] US
  JOIN Utb_Perfil PR ON US.IdPerfil = PR.IdPerfil
)P WHERE (@IdEmpresaPadre IS NULL OR P.IdEmpresaPadre = @IdEmpresaPadre) AND (@Nombre IS NULL OR P.Usuario =@Nombre)
END

GO
/****** Object:  StoredProcedure [dbo].[Sp_UsuarioLogin]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_UsuarioLogin] 
@Usuario NVARCHAR(200),
@Password NVARCHAR(100)
AS
BEGIN
DECLARE @IdUsuario char(6) = (select top 1 IdEmpresa from Utb_Usuario where Usuario = @Usuario)
SELECT [IdUsuario]
      ,[IdPersona]
      ,[Usuario]
      ,[Correo]
      ,[Foto]
      ,[Password]
      ,US.IdEstado
      ,US.IdPerfil
	  ,(CASE WHEN (SELECT EsPrincipal FROM Utb_Empresa WHERE IdEmpresa = US.IdEmpresa)= 1 THEN 'PRINCIPAL'
	         WHEN (SELECT EsContratista FROM Utb_Empresa WHERE IdEmpresa = US.IdEmpresa)= 1 THEN 'CONTRATISTA'
			 WHEN (SELECT EsPrincipal FROM Utb_Empresa WHERE IdEmpresa = US.IdEmpresa)= 1 THEN 'SUBCONTRATISTA' END )TipoEmpresa,
	   US.IdEmpresa,	
	   ISNULL(PR.Nombre,'SUPERUSUARIO') AS Perfil,
	   (CASE WHEN (SELECT EsPrincipal FROM Utb_Empresa WHERE IdEmpresa = US.IdEmpresa)= 1 THEN US.IdEmpresa
	         WHEN (SELECT EsContratista FROM Utb_Empresa WHERE IdEmpresa = US.IdEmpresa)= 1   THEN (SELECT  E.IdPadre from Utb_Empresa E where  E.IdEmpresa = US.IdEmpresa)
	    ELSE (SELECT  E.IdPadre from Utb_Empresa E where  E.IdEmpresa = (SELECT E.IdPadre FROM Utb_Empresa E WHERE E.IdEmpresa = US.IdEmpresa )) END)IdEmpresaPadre,
		(SELECT top 1 RazonSocial FROM Utb_Empresa WHERE IdEmpresa = @IdUsuario)EmpresaNombre
  FROM [dbo].[Utb_Usuario] US
  left JOIN Utb_Perfil PR ON US.IdPerfil = PR.IdPerfil
   WHERE Usuario =@Usuario and Password=@Password 
END

GO
/****** Object:  StoredProcedure [dbo].[Sp_UsuarioMantenimiento]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_UsuarioMantenimiento]
@IdUsuario CHAR(6),
@IdPersona CHAR(15) = NULL,
@Usuario varchar(200),
@Correo varchar(200),
@Password Nvarchar(200),
@IdPerfil CHAR(6) =NULL,
@IdEmpresa char(6),
@IdEstado varchar(10)
AS
SET @IdEstado =(CASE WHEN UPPER(@IdEstado) ='TRUE' THEN 'ES0201' ELSE 'ES0202' END)
DECLARE @USUARIOANTERIOR VARCHAR(100)= (SELECT TOP 1 Usuario from Utb_Usuario where IdUsuario = @IdUsuario  )
BEGIN
IF(@USUARIOANTERIOR!=@Usuario)
BEGIN
  IF EXISTS(SELECT TOP 1 IdUsuario FROM Utb_Usuario U WHERE U.Usuario = @Usuario)
  BEGIN
   -- EXISTE USUARIO
    SELECT 100 AS RESULT
  END
ELSE IF EXISTS(SELECT TOP 1 IdUsuario FROM Utb_Usuario U WHERE U.Correo = @Correo)
  BEGIN
    SELECT 200 AS RESULT
  END
END
ELSE
  begin
    BEGIN TRY
--Registrar
	IF(@IdUsuario = '')
	BEGIN
		--Existe 
		 SET  @IdPersona = ( CASE WHEN (SELECT COUNT(*) FROM Utb_Persona )>0 THEN (SELECT 'PE' + RIGHT('0000000000000'+CONVERT(VARCHAR(13), SUBSTRING( MAX(IdPersona),3,LEN(MAX(IdPersona)))+1), 13) FROM Utb_Persona) ELSE 'PE0000000000001' END);
		INSERT INTO Utb_Persona(IdPersona,IdEntidad,IdEstado)VALUES(@IdPersona,'EN0106',@IdEstado)

		SET  @IdUsuario = ( CASE WHEN (SELECT COUNT(*) FROM Utb_Usuario )>0 THEN (SELECT 'US' + RIGHT('000'+CONVERT(VARCHAR(4), SUBSTRING( MAX(IdUsuario),3,LEN(MAX(IdUsuario)))+1), 4) FROM Utb_Usuario) ELSE 'US0001' END);
		IF NOT EXISTS (SELECT top 1 IdUsuario from Utb_Usuario where IdUsuario = @IdUsuario)
		BEGIN
			 INSERT INTO Utb_Usuario(IdUsuario,IdPersona,Usuario,Correo,Password,IdPerfil,IdEmpresa,IdEstado)
			        VALUES(@IdUsuario,@IdPersona,@Usuario,@Correo,@Password,@IdPerfil,@IdEmpresa,@IdEstado)
			 SELECT 1 AS RESULT
		END
		ELSE
		BEGIN
			SELECT -10 AS REGISTRO
		END
	END

--Actualizar
    ELSE
	BEGIN
		IF EXISTS (SELECT top 1 IdUsuario from Utb_Usuario where IdUsuario = @IdUsuario)
		BEGIN
		 DECLARE @PASSWORDTEMP NVARCHAR(200)= (SELECT Password FROM Utb_Usuario WHERE IdUsuario =@IdUsuario) 
			IF(@Password='')
			BEGIN
			 UPDATE Utb_Usuario SET Usuario=@Usuario,Correo=@Correo,IdPerfil=@IdPerfil,IdEmpresa=@IdEmpresa,IdEstado=@IdEstado WHERE IdUsuario=@IdUsuario
			END
			ELSE IF(@PASSWORDTEMP =@Password)
			BEGIN
			 UPDATE Utb_Usuario SET Usuario=@Usuario,Correo=@Correo,IdPerfil=@IdPerfil,IdEmpresa=@IdEmpresa,IdEstado=@IdEstado WHERE IdUsuario=@IdUsuario
			END
			ELSE
			BEGIN
			 UPDATE Utb_Usuario SET Usuario=@Usuario,Correo=@Correo,Password=@Password,IdPerfil=@IdPerfil,IdEmpresa=@IdEmpresa,IdEstado=@IdEstado WHERE IdUsuario=@IdUsuario
			END
			SELECT 1 AS RESULT
		    
		END
		ELSE
		BEGIN
			SELECT -1 AS ACTUALIZAR
		END

	END


END TRY
BEGIN CATCH 
	SELECT -2 AS ERROR
END CATCH
  end 
end

select * from Utb_Usuario
GO
/****** Object:  StoredProcedure [dbo].[Sp_UsuarioPaginado]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

-- =====================================================
-- Author:  LA
-- Create date: 15/04/2018
-- Description: Listar Usuario
-- Test:EXEC [dbo].[SP_UsuarioPaginado] @WhereFilter = "WHERE P.Estado <> '' ", @OrderBy = 'IdUsuario ASC', @Start = 0, @AmountRows = 10
-- =====================================================
CREATE PROCEDURE [dbo].[Sp_UsuarioPaginado]
@WhereFilter VARCHAR(MAX) = '',
@OrderBy VARCHAR (100) = '', 
@Start INT = 0,
@AmountRows INT = 0
AS
BEGIN
SET NOCOUNT ON;
    DECLARE @SentenciaSQL nvarchar(3072)
	SET @SentenciaSQL =  "
	;WITH Consulta AS (
		 SELECT * FROM(
	  SELECT USU.IdUsuario
      ,USU.IdPersona
      ,USU.Usuario
      ,USU.Correo
      ,USU.Foto
      ,USU.Password
      ,USU.IdEstado
	  ,ES.Nombre Estado
      ,USU.IdPerfil
	  ,PF.Nombre Perfil
	  ,EMP.IdEmpresa
	  ,EMP.RazonSocial Empresa
  FROM Utb_Usuario USU
  JOIN Utb_Estado ES ON USU.IdEstado= ES.IdEstado
  left JOIN Utb_Empresa EMP ON USU.IdEmpresa = EMP.IdEmpresa
  join Utb_Perfil PF ON USU.IdPerfil = PF.IdPerfil WHERE ES.IdEstado <> 'ES0203'
  )P
 " + 
	@WhereFilter + 
	") SELECT *
	FROM Consulta
	CROSS JOIN (SELECT Count(*) AS Cantidad FROM Consulta) AS CC
	ORDER BY " +
	CASE WHEN ISNULL(@OrderBy,'') != '' THEN (@OrderBy) 
	ELSE " IdUsuario DESC " END
	    + ' ' +
	" OFFSET " + CONVERT(VARCHAR, (@Start)) + 
	" ROWS FETCH NEXT " + CONVERT(VARCHAR, @AmountRows) + " ROWS ONLY"
	PRINT (@SentenciaSQL);
    EXECUTE sp_executesql  @stmt = @SentenciaSQL

END

GO
/****** Object:  StoredProcedure [dbo].[Sp_UsuarioPermiso]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_UsuarioPermiso]
@IdUsuario CHAR(6),
@IdPerfil CHAR(6) =NULL,
@IdOpcion CHAR(6),
@Check int 
AS


BEGIN
BEGIN TRY
--Registrar
	IF(@Check = 0)
	BEGIN
		DELETE FROM Utb_PerfilOpcion WHERE IdUsuario = @IdUsuario AND IdPerfil =@IdPerfil  AND IdOpcion =@IdOpcion
	END

--Actualizar
    ELSE
	BEGIN
		INSERT INTO Utb_PerfilOpcion(IdUsuario,IdOpcion,IdPerfil)VALUES(@IdUsuario,@IdOpcion,@IdPerfil)

	END

END TRY
BEGIN CATCH 
	SELECT -2 AS ERROR
END CATCH
end
GO
/****** Object:  StoredProcedure [dbo].[Sp_UsuarioRecuperar]    Script Date: 01/10/2018 19:28:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Sp_UsuarioRecuperar] 
@Usuario NVARCHAR(200)
AS
BEGIN
DECLARE @IdUsuario char(6) = (select top 1 IdEmpresa from Utb_Usuario where (Usuario = @Usuario or Correo = @Usuario))
SELECT [IdUsuario]
      ,[IdPersona]
      ,[Usuario]
      ,[Correo]
      ,[Foto]
      ,[Password]
      ,US.IdEstado
      ,US.IdPerfil
	  ,(CASE WHEN (SELECT EsPrincipal FROM Utb_Empresa WHERE IdEmpresa = US.IdEmpresa)= 1 THEN 'PRINCIPAL'
	         WHEN (SELECT EsContratista FROM Utb_Empresa WHERE IdEmpresa = US.IdEmpresa)= 1 THEN 'CONTRATISTA'
			 WHEN (SELECT EsPrincipal FROM Utb_Empresa WHERE IdEmpresa = US.IdEmpresa)= 1 THEN 'SUBCONTRATISTA' END )Tipo
			 	  ,(CASE WHEN (SELECT EsPrincipal FROM Utb_Empresa WHERE IdEmpresa = US.IdEmpresa)= 1 THEN 'PRINCIPAL'
	         WHEN (SELECT EsContratista FROM Utb_Empresa WHERE IdEmpresa = US.IdEmpresa)= 1 THEN 'CONTRATISTA'
			 WHEN (SELECT EsPrincipal FROM Utb_Empresa WHERE IdEmpresa = US.IdEmpresa)= 1 THEN 'SUBCONTRATISTA' END )TipoEmpresa,
	   US.IdEmpresa,	
	   ISNULL(PR.Nombre,'SUPERUSUARIO') AS Perfil,
	   (CASE WHEN (SELECT EsPrincipal FROM Utb_Empresa WHERE IdEmpresa = US.IdEmpresa)= 1 THEN US.IdEmpresa
	         WHEN (SELECT EsContratista FROM Utb_Empresa WHERE IdEmpresa = US.IdEmpresa)= 1   THEN (SELECT  E.IdPadre from Utb_Empresa E where  E.IdEmpresa = US.IdEmpresa)
	    ELSE (SELECT  E.IdPadre from Utb_Empresa E where  E.IdEmpresa = (SELECT E.IdPadre FROM Utb_Empresa E WHERE E.IdEmpresa = US.IdEmpresa )) END)IdEmpresaPadre,
		(SELECT top 1 RazonSocial FROM Utb_Empresa WHERE IdEmpresa = @IdUsuario)EmpresaNombre
  FROM [dbo].[Utb_Usuario] US
  left JOIN Utb_Perfil PR ON US.IdPerfil = PR.IdPerfil
   WHERE (Usuario =@Usuario OR Correo =@Usuario) 
END

GO
USE [master]
GO
ALTER DATABASE [DASys] SET  READ_WRITE 
GO
