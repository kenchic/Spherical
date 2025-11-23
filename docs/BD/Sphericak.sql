USE [Spherical]
GO
/****** Object:  User [Spherical]    Script Date: 02/02/2025 6:35:02 PM ******/
CREATE USER [Spherical] FOR LOGIN [Spherical] WITH DEFAULT_SCHEMA=[Spherical]
GO
/****** Object:  Schema [Spherical]    Script Date: 02/02/2025 6:35:02 PM ******/
CREATE SCHEMA [Spherical]
GO
/****** Object:  Table [Spherical].[Catalogo]    Script Date: 02/02/2025 6:35:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Spherical].[Catalogo](
	[Id] [varchar](20) NOT NULL,
	[idSistema] [varchar](10) NOT NULL,
	[Empresa] [varchar](20) NULL,
	[Descripcion] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Catalogo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Spherical].[CatalogoDetalle]    Script Date: 02/02/2025 6:35:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Spherical].[CatalogoDetalle](
	[Id] [varchar](20) NOT NULL,
	[idCatalogo] [varchar](20) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[ValorCadena] [varchar](10) NULL,
	[ValorNumero] [int] NULL,
	[ValorDecimal] [decimal](18, 5) NULL,
	[Activo] [bit] NULL,
 CONSTRAINT [PK_CatalogoDetalle] PRIMARY KEY CLUSTERED 
(
	[idCatalogo] ASC,
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Spherical].[Cliente]    Script Date: 02/02/2025 6:35:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Spherical].[Cliente](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[idCiudad] [varchar](20) NOT NULL,
	[Empresa] [varchar](20) NOT NULL,
	[Identificacion] [varchar](20) NOT NULL,
	[Nombre1] [varchar](25) NOT NULL,
	[Nombre2] [varchar](25) NULL,
	[Apellido1] [varchar](25) NOT NULL,
	[Apellido2] [varchar](25) NULL,
	[Direccion] [varchar](200) NOT NULL,
	[Telefono] [varchar](50) NOT NULL,
	[Celular] [varchar](50) NULL,
	[Correo] [varchar](100) NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Clientes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Spherical].[Elemento]    Script Date: 02/02/2025 6:35:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Spherical].[Elemento](
	[Id] [smallint] IDENTITY(1,1) NOT NULL,
	[idGrupoElemento] [tinyint] NOT NULL,
	[idUnidadMedida] [tinyint] NOT NULL,
	[Empresa] [varchar](20) NOT NULL,
	[Referencia] [varchar](50) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[Mt2] [float] NOT NULL,
	[Peso] [float] NOT NULL,
	[Rotacion] [bit] NOT NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Elementos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Spherical].[Factura]    Script Date: 02/02/2025 6:35:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Spherical].[Factura](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[idCliente] [int] NOT NULL,
	[Empresa] [varchar](20) NOT NULL,
	[Estado] [varchar](20) NOT NULL,
 CONSTRAINT [PK_Factura] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Spherical].[Grupo]    Script Date: 02/02/2025 6:35:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Spherical].[Grupo](
	[Id] [varchar](50) NOT NULL,
	[Empresa] [varchar](20) NOT NULL,
	[Descripcion] [varchar](500) NOT NULL,
	[Activar] [bit] NOT NULL,
 CONSTRAINT [PK_Grupo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Spherical].[GrupoRol]    Script Date: 02/02/2025 6:35:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Spherical].[GrupoRol](
	[idGrupo] [varchar](50) NOT NULL,
	[idRol] [varchar](50) NOT NULL,
 CONSTRAINT [PK_GrupoRol] PRIMARY KEY CLUSTERED 
(
	[idGrupo] ASC,
	[idRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Spherical].[GrupoUsuario]    Script Date: 02/02/2025 6:35:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Spherical].[GrupoUsuario](
	[idGrupo] [varchar](50) NOT NULL,
	[idUsuario] [varchar](50) NOT NULL,
 CONSTRAINT [PK_GrupoUsuario] PRIMARY KEY CLUSTERED 
(
	[idGrupo] ASC,
	[idUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Spherical].[ListaPrecio]    Script Date: 02/02/2025 6:35:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Spherical].[ListaPrecio](
	[Id] [tinyint] IDENTITY(1,1) NOT NULL,
	[Empresa] [varchar](20) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_ListasPrecios] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Spherical].[Menu]    Script Date: 02/02/2025 6:35:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Spherical].[Menu](
	[Id] [varchar](50) NOT NULL,
	[Empresa] [varchar](20) NOT NULL,
	[Sistema] [varchar](10) NULL,
	[idMenu] [varchar](50) NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Url] [varchar](100) NULL,
	[Orden] [smallint] NOT NULL,
	[Icono] [varchar](100) NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Menu] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Spherical].[Opcion]    Script Date: 02/02/2025 6:35:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Spherical].[Opcion](
	[Id] [varchar](50) NOT NULL,
	[Empresa] [varchar](20) NOT NULL,
	[Descripcion] [varchar](500) NOT NULL,
	[Consultar] [bit] NOT NULL,
	[Crear] [bit] NOT NULL,
	[Editar] [bit] NOT NULL,
	[Eliminar] [bit] NOT NULL,
	[Anular] [bit] NOT NULL,
	[Activar] [bit] NOT NULL,
 CONSTRAINT [PK_Opcion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Spherical].[Parametro]    Script Date: 02/02/2025 6:35:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Spherical].[Parametro](
	[Codigo] [varchar](20) NOT NULL,
	[idSistema] [varchar](10) NOT NULL,
	[Empresa] [varchar](20) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[Descripcion] [varchar](500) NULL,
	[Valor] [varchar](200) NOT NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_ParametroSistema] PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Spherical].[Permiso]    Script Date: 02/02/2025 6:35:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Spherical].[Permiso](
	[Id] [varchar](50) NOT NULL,
	[Empresa] [varchar](20) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Descripcion] [varchar](500) NOT NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Permiso] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Spherical].[PermisoMenu]    Script Date: 02/02/2025 6:35:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Spherical].[PermisoMenu](
	[idPermiso] [varchar](50) NOT NULL,
	[idMenu] [varchar](50) NOT NULL,
 CONSTRAINT [PK_PermisoMenu] PRIMARY KEY CLUSTERED 
(
	[idPermiso] ASC,
	[idMenu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Spherical].[PermisoOpcion]    Script Date: 02/02/2025 6:35:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Spherical].[PermisoOpcion](
	[idPermiso] [varchar](50) NOT NULL,
	[idOpcion] [varchar](50) NOT NULL,
	[Consultar] [bit] NOT NULL,
	[Crear] [bit] NOT NULL,
	[Editar] [bit] NOT NULL,
	[Eliminar] [bit] NOT NULL,
	[Anular] [bit] NOT NULL,
	[Activar] [bit] NOT NULL,
 CONSTRAINT [PK_PermisoOpcion] PRIMARY KEY CLUSTERED 
(
	[idPermiso] ASC,
	[idOpcion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Spherical].[Proyecto]    Script Date: 02/02/2025 6:35:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Spherical].[Proyecto](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[idCliente] [int] NOT NULL,
	[Ciudad] [varchar](20) NOT NULL,
	[Empresa] [varchar](20) NOT NULL,
	[Nombre] [varchar](200) NOT NULL,
	[Tipo] [varchar](100) NOT NULL,
	[Direccion] [varchar](100) NULL,
	[Telefono] [varchar](50) NULL,
	[Observacion] [varchar](500) NULL,
	[Fecha] [date] NOT NULL,
	[FormaContacto] [varchar](50) NULL,
	[SistemaMedida] [varchar](50) NULL,
	[IdentificacionResponsable] [varchar](15) NULL,
	[NombreResponsable] [varchar](200) NULL,
	[TelResponsable] [varchar](50) NULL,
	[Estado] [varchar](20) NOT NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Proyectos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Spherical].[Rol]    Script Date: 02/02/2025 6:35:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Spherical].[Rol](
	[Id] [varchar](50) NOT NULL,
	[Empresa] [varchar](20) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Descripcion] [varchar](500) NOT NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Rol] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Spherical].[RolPermiso]    Script Date: 02/02/2025 6:35:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Spherical].[RolPermiso](
	[idRol] [varchar](50) NOT NULL,
	[idPermiso] [varchar](50) NOT NULL,
 CONSTRAINT [PK_RolPermiso] PRIMARY KEY CLUSTERED 
(
	[idRol] ASC,
	[idPermiso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Spherical].[Sistema]    Script Date: 02/02/2025 6:35:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Spherical].[Sistema](
	[Id] [varchar](10) NOT NULL,
	[Version] [varchar](20) NOT NULL,
 CONSTRAINT [PK_Sistema] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Spherical].[Ticket]    Script Date: 02/02/2025 6:35:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Spherical].[Ticket](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Empresa] [varchar](20) NOT NULL,
	[Titulo] [varchar](50) NOT NULL,
	[Descripcion] [varchar](max) NOT NULL,
	[Tipo] [varchar](5) NOT NULL,
	[Prioridad] [varchar](5) NOT NULL,
	[FechaCreacion] [datetime] NOT NULL,
	[Estado] [varchar](20) NOT NULL,
 CONSTRAINT [PK_Ticket] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [Spherical].[Usuario]    Script Date: 02/02/2025 6:35:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Spherical].[Usuario](
	[Id] [varchar](50) NOT NULL,
	[Empresa] [varchar](20) NOT NULL,
	[Identificacion] [varchar](50) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[Apellido] [varchar](100) NOT NULL,
	[Usuario] [varchar](15) NOT NULL,
	[Clave] [varchar](50) NOT NULL,
	[Correo] [varchar](100) NOT NULL,
	[Admin] [bit] NOT NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Spherical].[UsuarioRol]    Script Date: 02/02/2025 6:35:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Spherical].[UsuarioRol](
	[idUsuario] [varchar](50) NOT NULL,
	[idRol] [varchar](50) NOT NULL,
 CONSTRAINT [PK_UsuarioRol] PRIMARY KEY CLUSTERED 
(
	[idUsuario] ASC,
	[idRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [Spherical].[Catalogo] ([Id], [idSistema], [Empresa], [Descripcion]) VALUES (N'EMPRESAS', N'TACTIC', N'BASE', N'Empresas segun el cliente ')
GO
INSERT [Spherical].[Catalogo] ([Id], [idSistema], [Empresa], [Descripcion]) VALUES (N'ESTADOS_FACTURA', N'FORDWARD', N'BASE', N'Estados de los registros de la tabla Fordward.Factrua')
GO
INSERT [Spherical].[Catalogo] ([Id], [idSistema], [Empresa], [Descripcion]) VALUES (N'ESTADOS_PROYECTO', N'CONTROL', N'BASE', N'Estados de los registros de la tabla Control.Proyecto')
GO
INSERT [Spherical].[Catalogo] ([Id], [idSistema], [Empresa], [Descripcion]) VALUES (N'ESTADOS_TICKET', N'TACTIC', N'BASE', N'Estados de los registros de la tabla Tacticl.Ticket')
GO
INSERT [Spherical].[CatalogoDetalle] ([Id], [idCatalogo], [Nombre], [ValorCadena], [ValorNumero], [ValorDecimal], [Activo]) VALUES (N'AYZCORP', N'EMPRESAS', N'Empresa AyzCorp', NULL, NULL, NULL, 1)
GO
INSERT [Spherical].[CatalogoDetalle] ([Id], [idCatalogo], [Nombre], [ValorCadena], [ValorNumero], [ValorDecimal], [Activo]) VALUES (N'BASE', N'EMPRESAS', N'Empresa base donde se busca la configuración del sistema', NULL, NULL, NULL, 1)
GO
INSERT [Spherical].[CatalogoDetalle] ([Id], [idCatalogo], [Nombre], [ValorCadena], [ValorNumero], [ValorDecimal], [Activo]) VALUES (N'G', N'ESTADOS_FACTURA', N'Generada', NULL, NULL, NULL, 1)
GO
INSERT [Spherical].[CatalogoDetalle] ([Id], [idCatalogo], [Nombre], [ValorCadena], [ValorNumero], [ValorDecimal], [Activo]) VALUES (N'X', N'ESTADOS_FACTURA', N'Anulada', NULL, NULL, NULL, 1)
GO
INSERT [Spherical].[CatalogoDetalle] ([Id], [idCatalogo], [Nombre], [ValorCadena], [ValorNumero], [ValorDecimal], [Activo]) VALUES (N'C', N'ESTADOS_PROYECTO', N'Contrato', NULL, NULL, NULL, 1)
GO
INSERT [Spherical].[CatalogoDetalle] ([Id], [idCatalogo], [Nombre], [ValorCadena], [ValorNumero], [ValorDecimal], [Activo]) VALUES (N'N', N'ESTADOS_PROYECTO', N'Nuevo', NULL, NULL, NULL, 1)
GO
INSERT [Spherical].[CatalogoDetalle] ([Id], [idCatalogo], [Nombre], [ValorCadena], [ValorNumero], [ValorDecimal], [Activo]) VALUES (N'X', N'ESTADOS_PROYECTO', N'Cancelado', NULL, NULL, NULL, 1)
GO
INSERT [Spherical].[CatalogoDetalle] ([Id], [idCatalogo], [Nombre], [ValorCadena], [ValorNumero], [ValorDecimal], [Activo]) VALUES (N'C', N'ESTADOS_TICKET', N'Completado', NULL, NULL, NULL, 1)
GO
INSERT [Spherical].[CatalogoDetalle] ([Id], [idCatalogo], [Nombre], [ValorCadena], [ValorNumero], [ValorDecimal], [Activo]) VALUES (N'I', N'ESTADOS_TICKET', N'Sin Iniciar', NULL, NULL, NULL, 1)
GO
INSERT [Spherical].[CatalogoDetalle] ([Id], [idCatalogo], [Nombre], [ValorCadena], [ValorNumero], [ValorDecimal], [Activo]) VALUES (N'P', N'ESTADOS_TICKET', N'En Progreso', NULL, NULL, NULL, 1)
GO
INSERT [Spherical].[CatalogoDetalle] ([Id], [idCatalogo], [Nombre], [ValorCadena], [ValorNumero], [ValorDecimal], [Activo]) VALUES (N'X', N'ESTADOS_TICKET', N'Cancelado', NULL, NULL, NULL, 1)
GO
INSERT [Spherical].[Grupo] ([Id], [Empresa], [Descripcion], [Activar]) VALUES (N'GPlus', N'AYZCORP', N'Grupo Plus ', 1)
GO
INSERT [Spherical].[GrupoRol] ([idGrupo], [idRol]) VALUES (N'GPlus', N'RFacturaPlus')
GO
INSERT [Spherical].[GrupoRol] ([idGrupo], [idRol]) VALUES (N'GPlus', N'RInventarioPlus')
GO
INSERT [Spherical].[GrupoRol] ([idGrupo], [idRol]) VALUES (N'GPlus', N'RParametroPlus')
GO
INSERT [Spherical].[GrupoRol] ([idGrupo], [idRol]) VALUES (N'GPlus', N'RProyectoPlus')
GO
INSERT [Spherical].[GrupoRol] ([idGrupo], [idRol]) VALUES (N'GPlus', N'RUsuarioPlus')
GO
INSERT [Spherical].[GrupoUsuario] ([idGrupo], [idUsuario]) VALUES (N'GPlus', N'UPlus')
GO
INSERT [Spherical].[Menu] ([Id], [Empresa], [Sistema], [idMenu], [Nombre], [Url], [Orden], [Icono], [Activo]) VALUES (N'MFactura', N'AYZCORP', N'FORDWARD', NULL, N'Facturas', NULL, 1, NULL, 1)
GO
INSERT [Spherical].[Menu] ([Id], [Empresa], [Sistema], [idMenu], [Nombre], [Url], [Orden], [Icono], [Activo]) VALUES (N'MFacturaConsultar', N'AYZCORP', N'FORDWARD', N'MFactura', N'Consultar', N'/Factura/Consultar', 1, NULL, 1)
GO
INSERT [Spherical].[Menu] ([Id], [Empresa], [Sistema], [idMenu], [Nombre], [Url], [Orden], [Icono], [Activo]) VALUES (N'MInventario', N'AYZCORP', N'LINEUP', NULL, N'Inventario', NULL, 1, NULL, 1)
GO
INSERT [Spherical].[Menu] ([Id], [Empresa], [Sistema], [idMenu], [Nombre], [Url], [Orden], [Icono], [Activo]) VALUES (N'MIventarioConsultar', N'AYZCORP', N'LINEUP', N'MInventario', N'Consultar', N'/Inventario/Consultar', 1, NULL, 1)
GO
INSERT [Spherical].[Menu] ([Id], [Empresa], [Sistema], [idMenu], [Nombre], [Url], [Orden], [Icono], [Activo]) VALUES (N'MParametro', N'AYZCORP', N'TACTIC', NULL, N'Patrametros', NULL, 1, NULL, 1)
GO
INSERT [Spherical].[Menu] ([Id], [Empresa], [Sistema], [idMenu], [Nombre], [Url], [Orden], [Icono], [Activo]) VALUES (N'MParametroConsultar', N'AZYCORP', N'TACTIC', N'MParametro', N'Consultar', N'/Parametro/Consultar', 1, NULL, 1)
GO
INSERT [Spherical].[Menu] ([Id], [Empresa], [Sistema], [idMenu], [Nombre], [Url], [Orden], [Icono], [Activo]) VALUES (N'MProyecto', N'AYZCORP', N'CONTROL', NULL, N'Proyectos', NULL, 1, NULL, 1)
GO
INSERT [Spherical].[Menu] ([Id], [Empresa], [Sistema], [idMenu], [Nombre], [Url], [Orden], [Icono], [Activo]) VALUES (N'MProyectoConsultar', N'AYZCORP', N'CONTROL', N'MProyecto', N'Consultar', N'/Proyecto/Consultar', 1, NULL, 1)
GO
INSERT [Spherical].[Menu] ([Id], [Empresa], [Sistema], [idMenu], [Nombre], [Url], [Orden], [Icono], [Activo]) VALUES (N'MUsuario', N'AYZCORP', N'DEFENDER', NULL, N'Usuarios', NULL, 1, NULL, 1)
GO
INSERT [Spherical].[Menu] ([Id], [Empresa], [Sistema], [idMenu], [Nombre], [Url], [Orden], [Icono], [Activo]) VALUES (N'MUsuarioConsultar', N'AYZCORP', N'DEFENDER', N'MUsuario', N'Consultar', N'/Usuario/Consultar', 1, NULL, 1)
GO
INSERT [Spherical].[Opcion] ([Id], [Empresa], [Descripcion], [Consultar], [Crear], [Editar], [Eliminar], [Anular], [Activar]) VALUES (N'OFactura', N'AYZCORP', N'Opciones de la pagina facturacion', 1, 1, 1, 1, 1, 0)
GO
INSERT [Spherical].[Opcion] ([Id], [Empresa], [Descripcion], [Consultar], [Crear], [Editar], [Eliminar], [Anular], [Activar]) VALUES (N'OInventario', N'AYZCORP', N'Opciones de la pagina inventario', 1, 1, 1, 1, 0, 1)
GO
INSERT [Spherical].[Opcion] ([Id], [Empresa], [Descripcion], [Consultar], [Crear], [Editar], [Eliminar], [Anular], [Activar]) VALUES (N'OParametro', N'AYZCORP', N'Opciones de la pagina administracion', 1, 1, 1, 1, 0, 1)
GO
INSERT [Spherical].[Opcion] ([Id], [Empresa], [Descripcion], [Consultar], [Crear], [Editar], [Eliminar], [Anular], [Activar]) VALUES (N'OProyecto', N'AYZCORP', N'Opciones de la pagina proyectos', 1, 1, 1, 1, 0, 1)
GO
INSERT [Spherical].[Opcion] ([Id], [Empresa], [Descripcion], [Consultar], [Crear], [Editar], [Eliminar], [Anular], [Activar]) VALUES (N'OUsuario', N'AYZCORP', N'Opciones de la pagina usuarios', 1, 1, 1, 1, 0, 1)
GO
INSERT [Spherical].[Permiso] ([Id], [Empresa], [Nombre], [Descripcion], [Activo]) VALUES (N'PFacturaMenu', N'AYZCORP', N'Menu Factura', N'Permiso para ver menu factura', 1)
GO
INSERT [Spherical].[Permiso] ([Id], [Empresa], [Nombre], [Descripcion], [Activo]) VALUES (N'PFacturaPlus', N'AYZCORP', N'Opcion Factura Plus', N'Permite consutlar , crear , editar, eliminar una Factura', 1)
GO
INSERT [Spherical].[Permiso] ([Id], [Empresa], [Nombre], [Descripcion], [Activo]) VALUES (N'PInventarioMenu', N'AYZCORP', N'Menu Inventario', N'Permiso para ver menu invenario', 1)
GO
INSERT [Spherical].[Permiso] ([Id], [Empresa], [Nombre], [Descripcion], [Activo]) VALUES (N'PInventarioPlus', N'AYZCORP', N'Opciones Inventario Plus', N'Permite consutlar crear editar eliminar el inventario', 1)
GO
INSERT [Spherical].[Permiso] ([Id], [Empresa], [Nombre], [Descripcion], [Activo]) VALUES (N'PParametroMenu', N'AYZCORP', N'Menu Parametros', N'Permiso para ver menu parametros', 1)
GO
INSERT [Spherical].[Permiso] ([Id], [Empresa], [Nombre], [Descripcion], [Activo]) VALUES (N'PParametroPlus', N'AYZCORP', N'Opciones Parametros', N'Permite consultar, crear, editar, eliminar un Parametro', 1)
GO
INSERT [Spherical].[Permiso] ([Id], [Empresa], [Nombre], [Descripcion], [Activo]) VALUES (N'PProyectoMenu', N'AYZCORP', N'Menu Proyecto', N'Permiso para ver menu proyectos', 1)
GO
INSERT [Spherical].[Permiso] ([Id], [Empresa], [Nombre], [Descripcion], [Activo]) VALUES (N'PProyectoPlus', N'AYZCORP', N'Opciones Proyecto Plus', N'Permite consultar, crear, editar, eliminar un Proyecto', 1)
GO
INSERT [Spherical].[Permiso] ([Id], [Empresa], [Nombre], [Descripcion], [Activo]) VALUES (N'PUsuarioMenu', N'AYZCORP', N'Menu Usuarios', N'Permiso para ver menu usuario', 1)
GO
INSERT [Spherical].[Permiso] ([Id], [Empresa], [Nombre], [Descripcion], [Activo]) VALUES (N'PUsuarioPlus', N'AYZCORP', N'Opciones Usuario Plus', N'Permite consultar, crear, editar, eliminar un Usuario', 1)
GO
INSERT [Spherical].[PermisoMenu] ([idPermiso], [idMenu]) VALUES (N'PFacturaMenu', N'MFactura')
GO
INSERT [Spherical].[PermisoMenu] ([idPermiso], [idMenu]) VALUES (N'PInventarioMenu', N'MInventario')
GO
INSERT [Spherical].[PermisoMenu] ([idPermiso], [idMenu]) VALUES (N'PParametroMenu', N'MParametro')
GO
INSERT [Spherical].[PermisoMenu] ([idPermiso], [idMenu]) VALUES (N'PProyectoMenu', N'MProyecto')
GO
INSERT [Spherical].[PermisoMenu] ([idPermiso], [idMenu]) VALUES (N'PUsuarioMenu', N'MUsuario')
GO
INSERT [Spherical].[PermisoOpcion] ([idPermiso], [idOpcion], [Consultar], [Crear], [Editar], [Eliminar], [Anular], [Activar]) VALUES (N'PFacturaPlus', N'OFactura', 1, 1, 1, 1, 1, 0)
GO
INSERT [Spherical].[PermisoOpcion] ([idPermiso], [idOpcion], [Consultar], [Crear], [Editar], [Eliminar], [Anular], [Activar]) VALUES (N'PInventarioPlus', N'OInventario', 1, 1, 1, 1, 0, 1)
GO
INSERT [Spherical].[PermisoOpcion] ([idPermiso], [idOpcion], [Consultar], [Crear], [Editar], [Eliminar], [Anular], [Activar]) VALUES (N'PParametroPlus', N'OParametro', 1, 1, 1, 1, 0, 1)
GO
INSERT [Spherical].[PermisoOpcion] ([idPermiso], [idOpcion], [Consultar], [Crear], [Editar], [Eliminar], [Anular], [Activar]) VALUES (N'PProyectoPlus', N'OProyecto', 1, 1, 1, 1, 0, 0)
GO
INSERT [Spherical].[PermisoOpcion] ([idPermiso], [idOpcion], [Consultar], [Crear], [Editar], [Eliminar], [Anular], [Activar]) VALUES (N'PUsuarioPlus', N'OUsuario', 1, 1, 1, 1, 0, 1)
GO
INSERT [Spherical].[Rol] ([Id], [Empresa], [Nombre], [Descripcion], [Activo]) VALUES (N'RFacturaPlus', N'AYZCORP', N'Factura Plus', N'Rol para gestionar como admin del modulo factura', 1)
GO
INSERT [Spherical].[Rol] ([Id], [Empresa], [Nombre], [Descripcion], [Activo]) VALUES (N'RInventarioPlus', N'AYZCORP', N'Inventario Plus', N'Rol para gestionar como admin el modulo inventario', 1)
GO
INSERT [Spherical].[Rol] ([Id], [Empresa], [Nombre], [Descripcion], [Activo]) VALUES (N'RParametroPlus', N'AYZCORP', N'Parametro Plus', N'Rol para gestionar como admin del modulo parametros', 1)
GO
INSERT [Spherical].[Rol] ([Id], [Empresa], [Nombre], [Descripcion], [Activo]) VALUES (N'RProyectoPlus', N'AYZCORP', N'Proyecto Plus', N'Rol para gestionar como admin del modulo proyectos', 1)
GO
INSERT [Spherical].[Rol] ([Id], [Empresa], [Nombre], [Descripcion], [Activo]) VALUES (N'RUsuarioPlus', N'AYZCORP', N'Usuario Plus', N'Rol para gestionar como admin del modulo usuarios', 1)
GO
INSERT [Spherical].[RolPermiso] ([idRol], [idPermiso]) VALUES (N'RFacturaPlus', N'PFacturaMenu')
GO
INSERT [Spherical].[RolPermiso] ([idRol], [idPermiso]) VALUES (N'RInventarioPlus', N'PInventarioMenu')
GO
INSERT [Spherical].[RolPermiso] ([idRol], [idPermiso]) VALUES (N'RParametroPlus', N'PParametroMenu')
GO
INSERT [Spherical].[RolPermiso] ([idRol], [idPermiso]) VALUES (N'RProyectoPlus', N'PProyectoMenu')
GO
INSERT [Spherical].[RolPermiso] ([idRol], [idPermiso]) VALUES (N'RUsuarioPlus', N'PUsuarioMenu')
GO
INSERT [Spherical].[Sistema] ([Id], [Version]) VALUES (N'CONTROL', N'v22.01')
GO
INSERT [Spherical].[Sistema] ([Id], [Version]) VALUES (N'DEFENDER', N'v22.01')
GO
INSERT [Spherical].[Sistema] ([Id], [Version]) VALUES (N'FORDWARD', N'v22.01')
GO
INSERT [Spherical].[Sistema] ([Id], [Version]) VALUES (N'LINEUP', N'v22.01')
GO
INSERT [Spherical].[Sistema] ([Id], [Version]) VALUES (N'TACTIC', N'v22.01')
GO
INSERT [Spherical].[Usuario] ([Id], [Empresa], [Identificacion], [Nombre], [Apellido], [Usuario], [Clave], [Correo], [Admin], [Activo]) VALUES (N'UPlus', N'AYZCORP', N'123456789', N'Usuario', N'Plus', N'UPlus', N'123', N'uplus@ayzcorp.com', 1, 1)
GO
ALTER TABLE [Spherical].[CatalogoDetalle] ADD  CONSTRAINT [DF_CatalogoDetalle_Activo]  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [Spherical].[Menu] ADD  CONSTRAINT [DF_bdMenu_Activo]  DEFAULT ((0)) FOR [Activo]
GO
ALTER TABLE [Spherical].[Ticket] ADD  CONSTRAINT [DF_Ticket_Estado]  DEFAULT ('P') FOR [Estado]
GO
ALTER TABLE [Spherical].[Catalogo]  WITH CHECK ADD  CONSTRAINT [FK_Catalogo_Sistema] FOREIGN KEY([idSistema])
REFERENCES [Spherical].[Sistema] ([Id])
GO
ALTER TABLE [Spherical].[Catalogo] CHECK CONSTRAINT [FK_Catalogo_Sistema]
GO
ALTER TABLE [Spherical].[CatalogoDetalle]  WITH CHECK ADD  CONSTRAINT [FK_CatalogoDetalle_Catalogo] FOREIGN KEY([idCatalogo])
REFERENCES [Spherical].[Catalogo] ([Id])
GO
ALTER TABLE [Spherical].[CatalogoDetalle] CHECK CONSTRAINT [FK_CatalogoDetalle_Catalogo]
GO
ALTER TABLE [Spherical].[GrupoRol]  WITH CHECK ADD  CONSTRAINT [FK_GrupoRol_Grupo] FOREIGN KEY([idGrupo])
REFERENCES [Spherical].[Grupo] ([Id])
GO
ALTER TABLE [Spherical].[GrupoRol] CHECK CONSTRAINT [FK_GrupoRol_Grupo]
GO
ALTER TABLE [Spherical].[GrupoRol]  WITH CHECK ADD  CONSTRAINT [FK_GrupoRol_Rol] FOREIGN KEY([idRol])
REFERENCES [Spherical].[Rol] ([Id])
GO
ALTER TABLE [Spherical].[GrupoRol] CHECK CONSTRAINT [FK_GrupoRol_Rol]
GO
ALTER TABLE [Spherical].[GrupoUsuario]  WITH CHECK ADD  CONSTRAINT [FK_GrupoUsuario_Grupo] FOREIGN KEY([idGrupo])
REFERENCES [Spherical].[Grupo] ([Id])
GO
ALTER TABLE [Spherical].[GrupoUsuario] CHECK CONSTRAINT [FK_GrupoUsuario_Grupo]
GO
ALTER TABLE [Spherical].[GrupoUsuario]  WITH CHECK ADD  CONSTRAINT [FK_GrupoUsuario_Usuario] FOREIGN KEY([idUsuario])
REFERENCES [Spherical].[Usuario] ([Id])
GO
ALTER TABLE [Spherical].[GrupoUsuario] CHECK CONSTRAINT [FK_GrupoUsuario_Usuario]
GO
ALTER TABLE [Spherical].[Menu]  WITH CHECK ADD  CONSTRAINT [FK_MenuPadre] FOREIGN KEY([idMenu])
REFERENCES [Spherical].[Menu] ([Id])
GO
ALTER TABLE [Spherical].[Menu] CHECK CONSTRAINT [FK_MenuPadre]
GO
ALTER TABLE [Spherical].[Parametro]  WITH CHECK ADD  CONSTRAINT [FK_Parametro_Sistema] FOREIGN KEY([idSistema])
REFERENCES [Spherical].[Sistema] ([Id])
GO
ALTER TABLE [Spherical].[Parametro] CHECK CONSTRAINT [FK_Parametro_Sistema]
GO
ALTER TABLE [Spherical].[PermisoMenu]  WITH CHECK ADD  CONSTRAINT [FK_PermisoMenu_Menu] FOREIGN KEY([idMenu])
REFERENCES [Spherical].[Menu] ([Id])
GO
ALTER TABLE [Spherical].[PermisoMenu] CHECK CONSTRAINT [FK_PermisoMenu_Menu]
GO
ALTER TABLE [Spherical].[PermisoMenu]  WITH CHECK ADD  CONSTRAINT [FK_PermisoMenu_Permiso] FOREIGN KEY([idPermiso])
REFERENCES [Spherical].[Permiso] ([Id])
GO
ALTER TABLE [Spherical].[PermisoMenu] CHECK CONSTRAINT [FK_PermisoMenu_Permiso]
GO
ALTER TABLE [Spherical].[PermisoOpcion]  WITH CHECK ADD  CONSTRAINT [FK_PermisoOpcion_Opcion] FOREIGN KEY([idOpcion])
REFERENCES [Spherical].[Opcion] ([Id])
GO
ALTER TABLE [Spherical].[PermisoOpcion] CHECK CONSTRAINT [FK_PermisoOpcion_Opcion]
GO
ALTER TABLE [Spherical].[PermisoOpcion]  WITH CHECK ADD  CONSTRAINT [FK_PermisoOpcion_Permiso] FOREIGN KEY([idPermiso])
REFERENCES [Spherical].[Permiso] ([Id])
GO
ALTER TABLE [Spherical].[PermisoOpcion] CHECK CONSTRAINT [FK_PermisoOpcion_Permiso]
GO
ALTER TABLE [Spherical].[Proyecto]  WITH CHECK ADD  CONSTRAINT [FK_Proyecto_Cliente] FOREIGN KEY([idCliente])
REFERENCES [Spherical].[Cliente] ([Id])
GO
ALTER TABLE [Spherical].[Proyecto] CHECK CONSTRAINT [FK_Proyecto_Cliente]
GO
ALTER TABLE [Spherical].[RolPermiso]  WITH CHECK ADD  CONSTRAINT [FK_RolPermiso_Permiso] FOREIGN KEY([idPermiso])
REFERENCES [Spherical].[Permiso] ([Id])
GO
ALTER TABLE [Spherical].[RolPermiso] CHECK CONSTRAINT [FK_RolPermiso_Permiso]
GO
ALTER TABLE [Spherical].[RolPermiso]  WITH CHECK ADD  CONSTRAINT [FK_RolPermiso_Rol] FOREIGN KEY([idRol])
REFERENCES [Spherical].[Rol] ([Id])
GO
ALTER TABLE [Spherical].[RolPermiso] CHECK CONSTRAINT [FK_RolPermiso_Rol]
GO
ALTER TABLE [Spherical].[UsuarioRol]  WITH CHECK ADD  CONSTRAINT [FK_UsuarioRol_Rol] FOREIGN KEY([idRol])
REFERENCES [Spherical].[Rol] ([Id])
GO
ALTER TABLE [Spherical].[UsuarioRol] CHECK CONSTRAINT [FK_UsuarioRol_Rol]
GO
ALTER TABLE [Spherical].[UsuarioRol]  WITH CHECK ADD  CONSTRAINT [FK_UsuarioRol_Usuario] FOREIGN KEY([idUsuario])
REFERENCES [Spherical].[Usuario] ([Id])
GO
ALTER TABLE [Spherical].[UsuarioRol] CHECK CONSTRAINT [FK_UsuarioRol_Usuario]
GO
/****** Object:  StoredProcedure [Spherical].[GetMenuUsuario]    Script Date: 02/02/2025 6:35:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Spherical].[GetMenuUsuario]  
(
	@Usuario VARCHAR(50) = '',
	@Empresa VARCHAR(20) = ''
)
AS 
BEGIN

	CREATE TABLE ##TablaTemporal (Id varchar(50), Nombre varchar(50), Url varchar(50), Orden int, Padre varchar(50), Icono Varchar(100) )

	DECLARE @idMenu AS Varchar(50) 
	DECLARE @idMenuPadre AS Varchar(50) 
	DECLARE @Nombre AS Varchar(50)
	DECLARE @Url AS Varchar(100)
	DECLARE @Orden AS smallint
	DECLARE @Image AS Varchar(100)
	DECLARE @exite AS smallint

	--Construir Usuario - Menu
	DECLARE UsuarioMenu 
	CURSOR FOR 
		SELECT M.id, M.idMenu, M.Nombre, M.Url, M.Orden, M.Icono
		FROM Spherical.Menu M
		INNER JOIN Spherical.PermisoMenu PM ON M.Id = PM.idMenu
		INNER JOIN Spherical.Permiso P ON PM.idPermiso = P.Id
		INNER JOIN Spherical.RolPermiso RP ON P.Id = RP.idPermiso
		INNER JOIN Spherical.Rol R ON RP.idRol = R.Id
		INNER JOIN Spherical.GrupoRol GR ON R.Id = GR.idRol
		INNER JOIN Spherical.Grupo G ON GR.idGrupo = G.Id
		INNER JOIN Spherical.GrupoUsuario GS ON G.Id = GS.idGrupo
		INNER JOIN Spherical.Usuario U ON GS.idUsuario = U.Id and UPPER(U.Id) = UPPER(@Usuario) AND UPPER(U.Empresa) = UPPER(@Empresa) 

	OPEN UsuarioMenu
	FETCH NEXT FROM UsuarioMenu INTO @idMenu, @idMenuPadre, @Nombre, @Url, @Orden, @Image
	WHILE @@fetch_status = 0
	BEGIN
		IF ( @idMenuPadre is null)
		BEGIN			
			SELECT @exite = Id from ##TablaTemporal where Id = @idMenu
			IF (@exite IS NULL)
			BEGIN
				INSERT INTO ##TablaTemporal VALUES (@idMenu, @Nombre, @Url, @Orden, null, @Image)			
				Set @idMenuPadre = @idMenu
			END
			INSERT INTO ##TablaTemporal
			SELECT M.id, M.Nombre, M.Url, M.Orden, @idMenuPadre, M.Icono FROM Spherical.Menu M WHERE M.idMenu = @idMenu
		END
		ELSE
		BEGIN
			SELECT @exite = Id from ##TablaTemporal where Id = @idMenuPadre
			IF (@exite IS NULL)
			BEGIN	
				INSERT INTO ##TablaTemporal
				SELECT Id, Nombre, Url, Orden, null, Icono FROM Spherical.Menu WHERE Id = @idMenuPadre

			END

			SET @exite = NULL
			SELECT @exite = Id from ##TablaTemporal where Id = @idMenu
			IF (@exite IS NULL)
				INSERT INTO ##TablaTemporal VALUES (@idMenu, @Nombre, @Url, @idMenuPadre, @Orden, @Image)
		END	
		FETCH NEXT FROM UsuarioMenu INTO  @idMenu, @idMenuPadre, @Nombre, @Url, @Orden, @Image
	END

	CLOSE UsuarioMenu
	DEALLOCATE UsuarioMenu

	SELECT * FROM ##TablaTemporal ORDER BY Orden
	DROP TABLE ##TablaTemporal
END


GO
