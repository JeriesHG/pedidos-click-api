/****** Object:  Table [dbo].[sysdiagrams]    Script Date: 9/10/2020 10:58:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sysdiagrams](
	[name] [sysname] NOT NULL,
	[principal_id] [int] NOT NULL,
	[diagram_id] [int] IDENTITY(1,1) NOT NULL,
	[version] [int] NULL,
	[definition] [varbinary](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[diagram_id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UK_principal_name] UNIQUE NONCLUSTERED 
(
	[principal_id] ASC,
	[name] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[t_Categoria]    Script Date: 9/10/2020 10:58:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[t_Categoria](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdCliente] [int] NULL,
	[IdTienda] [int] NULL,
	[Categoria] [varchar](255) NULL,
	[Imagen] [varchar](max) NULL,
	[IdUsuarioCreo] [int] NULL,
	[FechaCreo] [datetime] NULL,
	[IdUsuarioBorro] [int] NULL,
	[Borrado] [bit] NULL,
	[FechaBorrado] [datetime] NULL,
 CONSTRAINT [PK_t_Categoria] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[t_CategoriaTienda]    Script Date: 9/10/2020 10:58:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[t_CategoriaTienda](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdCliente] [int] NULL,
	[CategoriaTienda] [varchar](255) NULL,
	[Activo] [bit] NULL,
	[Imagen] [varchar](max) NULL,
	[FechaCreo] [datetime] NULL,
	[IdUsuarioCreo] [int] NULL,
	[FechaBorro] [datetime] NULL,
	[IdUsuarioBorro] [int] NULL,
	[Borrado] [bit] NULL,
 CONSTRAINT [PK_t_CategoriaTienda] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[t_Cliente]    Script Date: 9/10/2020 10:58:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[t_Cliente](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](255) NULL,
	[Logo] [varchar](max) NULL,
	[ValidoHasta] [date] NULL,
	[Valido] [bit] NULL,
	[Borrado] [bit] NULL,
 CONSTRAINT [PK_t_Cliente] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[t_Mensajes]    Script Date: 9/10/2020 10:58:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[t_Mensajes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](255) NULL,
	[Correo] [varchar](255) NULL,
	[Telefono] [varchar](255) NULL,
	[Empresa] [varchar](255) NULL,
	[Mensaje] [varchar](1000) NULL,
	[FechaCreacion] [datetime] NULL,
 CONSTRAINT [PK_t_Mensajes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[t_Orden]    Script Date: 9/10/2020 10:58:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[t_Orden](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdOrdenEstado] [int] NULL,
	[IdCliente] [int] NULL,
	[IdTienda] [int] NULL,
	[IdAsignado] [int] NULL,
	[Nombre] [varchar](255) NULL,
	[Telefono] [varchar](255) NULL,
	[CorreoElectronico] [varchar](255) NULL,
	[Identidad] [varchar](255) NULL,
	[Calle] [varchar](255) NULL,
	[Avenida] [varchar](255) NULL,
	[Colonia] [varchar](255) NULL,
	[Ciudad] [varchar](255) NULL,
	[Comentario] [varchar](1000) NULL,
	[FechaCreo] [datetime] NULL,
	[Lat] [varchar](50) NULL,
	[Long] [varchar](50) NULL,
	[IdZona] [int] NULL,
 CONSTRAINT [PK_t_Orden] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[t_OrdenDetalle]    Script Date: 9/10/2020 10:58:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[t_OrdenDetalle](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdOrden] [int] NULL,
	[IdProducto] [int] NULL,
	[Cantidad] [int] NULL,
	[Precio] [decimal](16, 4) NULL,
	[FechaCreo] [datetime] NULL,
	[FechaBorro] [datetime] NULL,
	[IdUsuarioBorro] [int] NULL,
	[Borrado] [bit] NULL,
 CONSTRAINT [PK_t_OrdenDetalle] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[t_OrdenEstado]    Script Date: 9/10/2020 10:58:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[t_OrdenEstado](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Estado] [varchar](50) NULL,
	[FechaCreo] [datetime] NULL,
 CONSTRAINT [PK_t_OrdenEstado] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[t_Producto]    Script Date: 9/10/2020 10:58:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[t_Producto](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdCategoria] [int] NULL,
	[IdTienda] [int] NULL,
	[Producto] [varchar](255) NULL,
	[Descripcion] [varchar](500) NULL,
	[Precio] [decimal](16, 4) NULL,
	[Costo] [decimal](16, 4) NULL,
	[Activo] [bit] NULL,
	[Imagen] [varchar](max) NULL,
	[FechaCreo] [datetime] NULL,
	[IdUsuarioCreo] [int] NULL,
	[FechaBorro] [datetime] NULL,
	[IdUsuarioBorro] [int] NULL,
	[Borrado] [bit] NULL,
 CONSTRAINT [PK_t_Producto] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[t_Rol]    Script Date: 9/10/2020 10:58:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[t_Rol](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Rol] [varchar](255) NULL,
	[Borrado] [bit] NULL,
 CONSTRAINT [PK_t_Rol] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[t_Sesion]    Script Date: 9/10/2020 10:58:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[t_Sesion](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [int] NULL,
	[FechaCreo] [datetime] NULL,
	[FechaExpira] [datetime] NULL,
 CONSTRAINT [PK_t_Sesion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[t_Tienda]    Script Date: 9/10/2020 10:58:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[t_Tienda](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdCliente] [int] NULL,
	[IdCategoriaTienda] [int] NULL,
	[Nombre] [varchar](50) NULL,
	[Logo] [varchar](max) NULL,
	[Banner] [varchar](255) NULL,
	[Borrado] [bit] NULL,
	[Observaciones] [varchar](max) NULL,
	[Paginaweb] [varchar](50) NULL,
 CONSTRAINT [PK_t_Tienda] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[t_Usuario]    Script Date: 9/10/2020 10:58:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[t_Usuario](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClienteId] [int] NULL,
	[RolId] [int] NULL,
	[Nombre] [varchar](255) NULL,
	[Apellido] [varchar](255) NULL,
	[Usuario] [varchar](100) NULL,
	[Contrasena] [varchar](255) NULL,
	[Correo] [varchar](255) NULL,
	[CreadoFecha] [datetime] NULL,
	[Borrado] [bit] NULL,
	[BorradoFecha] [datetime] NULL,
 CONSTRAINT [PK_t_Usuario] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[t_Zona]    Script Date: 9/10/2020 10:58:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[t_Zona](
	[Id] [int] NOT NULL,
	[Valor] [decimal](16, 4) NOT NULL,
 CONSTRAINT [PK_t_Zona] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[t_ZonaDetalle]    Script Date: 9/10/2020 10:58:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[t_ZonaDetalle](
	[Id] [int] NOT NULL,
	[IdZona] [int] NOT NULL,
	[Colonia] [nvarchar](max) NULL,
 CONSTRAINT [PK_t_ZonaDetalle] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[t_Categoria] ADD  CONSTRAINT [DF_t_Categoria_Borrado]  DEFAULT ((0)) FOR [Borrado]
GO
ALTER TABLE [dbo].[t_Cliente] ADD  CONSTRAINT [DF_t_Cliente_Valido]  DEFAULT ((0)) FOR [Valido]
GO
ALTER TABLE [dbo].[t_Cliente] ADD  CONSTRAINT [DF_t_Cliente_Borrado]  DEFAULT ((0)) FOR [Borrado]
GO
ALTER TABLE [dbo].[t_OrdenDetalle] ADD  CONSTRAINT [DF_t_OrdenDetalle_Borrado]  DEFAULT ((0)) FOR [Borrado]
GO
ALTER TABLE [dbo].[t_Producto] ADD  CONSTRAINT [DF_t_Producto_Activo]  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[t_Producto] ADD  CONSTRAINT [DF_t_Producto_Borrado]  DEFAULT ((0)) FOR [Borrado]
GO
ALTER TABLE [dbo].[t_Rol] ADD  CONSTRAINT [DF_t_Rol_Borrado]  DEFAULT ((0)) FOR [Borrado]
GO
ALTER TABLE [dbo].[t_Tienda] ADD  CONSTRAINT [DF_t_Tienda_Borrado]  DEFAULT ((0)) FOR [Borrado]
GO
ALTER TABLE [dbo].[t_Categoria]  WITH CHECK ADD  CONSTRAINT [FK_t_Categoria_t_Cliente] FOREIGN KEY([IdCliente])
REFERENCES [dbo].[t_Cliente] ([Id])
GO
ALTER TABLE [dbo].[t_Categoria] CHECK CONSTRAINT [FK_t_Categoria_t_Cliente]
GO
ALTER TABLE [dbo].[t_Categoria]  WITH CHECK ADD  CONSTRAINT [FK_t_Categoria_t_Tienda] FOREIGN KEY([IdTienda])
REFERENCES [dbo].[t_Tienda] ([Id])
GO
ALTER TABLE [dbo].[t_Categoria] CHECK CONSTRAINT [FK_t_Categoria_t_Tienda]
GO
ALTER TABLE [dbo].[t_Categoria]  WITH CHECK ADD  CONSTRAINT [FK_t_Categoria_t_Usuario] FOREIGN KEY([IdUsuarioCreo])
REFERENCES [dbo].[t_Usuario] ([Id])
GO
ALTER TABLE [dbo].[t_Categoria] CHECK CONSTRAINT [FK_t_Categoria_t_Usuario]
GO
ALTER TABLE [dbo].[t_Categoria]  WITH CHECK ADD  CONSTRAINT [FK_t_Categoria_t_Usuario1] FOREIGN KEY([IdUsuarioBorro])
REFERENCES [dbo].[t_Usuario] ([Id])
GO
ALTER TABLE [dbo].[t_Categoria] CHECK CONSTRAINT [FK_t_Categoria_t_Usuario1]
GO
ALTER TABLE [dbo].[t_CategoriaTienda]  WITH CHECK ADD  CONSTRAINT [FK_t_CategoriaTienda_t_Cliente] FOREIGN KEY([IdCliente])
REFERENCES [dbo].[t_Cliente] ([Id])
GO
ALTER TABLE [dbo].[t_CategoriaTienda] CHECK CONSTRAINT [FK_t_CategoriaTienda_t_Cliente]
GO
ALTER TABLE [dbo].[t_CategoriaTienda]  WITH CHECK ADD  CONSTRAINT [FK_t_CategoriaTienda_t_Usuario] FOREIGN KEY([IdUsuarioCreo])
REFERENCES [dbo].[t_Usuario] ([Id])
GO
ALTER TABLE [dbo].[t_CategoriaTienda] CHECK CONSTRAINT [FK_t_CategoriaTienda_t_Usuario]
GO
ALTER TABLE [dbo].[t_CategoriaTienda]  WITH CHECK ADD  CONSTRAINT [FK_t_CategoriaTienda_t_Usuario1] FOREIGN KEY([IdUsuarioBorro])
REFERENCES [dbo].[t_Usuario] ([Id])
GO
ALTER TABLE [dbo].[t_CategoriaTienda] CHECK CONSTRAINT [FK_t_CategoriaTienda_t_Usuario1]
GO
ALTER TABLE [dbo].[t_Orden]  WITH CHECK ADD  CONSTRAINT [FK_t_Orden_t_Cliente] FOREIGN KEY([IdCliente])
REFERENCES [dbo].[t_Cliente] ([Id])
GO
ALTER TABLE [dbo].[t_Orden] CHECK CONSTRAINT [FK_t_Orden_t_Cliente]
GO
ALTER TABLE [dbo].[t_Orden]  WITH CHECK ADD  CONSTRAINT [FK_t_Orden_t_OrdenEstado] FOREIGN KEY([IdOrdenEstado])
REFERENCES [dbo].[t_OrdenEstado] ([Id])
GO
ALTER TABLE [dbo].[t_Orden] CHECK CONSTRAINT [FK_t_Orden_t_OrdenEstado]
GO
ALTER TABLE [dbo].[t_Orden]  WITH CHECK ADD  CONSTRAINT [FK_t_Orden_t_Tienda] FOREIGN KEY([IdTienda])
REFERENCES [dbo].[t_Tienda] ([Id])
GO
ALTER TABLE [dbo].[t_Orden] CHECK CONSTRAINT [FK_t_Orden_t_Tienda]
GO
ALTER TABLE [dbo].[t_Orden]  WITH CHECK ADD  CONSTRAINT [FK_t_Orden_t_Usuario] FOREIGN KEY([IdAsignado])
REFERENCES [dbo].[t_Usuario] ([Id])
GO
ALTER TABLE [dbo].[t_Orden] CHECK CONSTRAINT [FK_t_Orden_t_Usuario]
GO
ALTER TABLE [dbo].[t_Orden]  WITH CHECK ADD  CONSTRAINT [FK_t_Orden_t_Zona] FOREIGN KEY([IdZona])
REFERENCES [dbo].[t_Zona] ([Id])
GO
ALTER TABLE [dbo].[t_Orden] CHECK CONSTRAINT [FK_t_Orden_t_Zona]
GO
ALTER TABLE [dbo].[t_OrdenDetalle]  WITH CHECK ADD  CONSTRAINT [FK_t_OrdenDetalle_t_Orden] FOREIGN KEY([IdOrden])
REFERENCES [dbo].[t_Orden] ([Id])
GO
ALTER TABLE [dbo].[t_OrdenDetalle] CHECK CONSTRAINT [FK_t_OrdenDetalle_t_Orden]
GO
ALTER TABLE [dbo].[t_OrdenDetalle]  WITH CHECK ADD  CONSTRAINT [FK_t_OrdenDetalle_t_Producto] FOREIGN KEY([IdProducto])
REFERENCES [dbo].[t_Producto] ([Id])
GO
ALTER TABLE [dbo].[t_OrdenDetalle] CHECK CONSTRAINT [FK_t_OrdenDetalle_t_Producto]
GO
ALTER TABLE [dbo].[t_OrdenDetalle]  WITH CHECK ADD  CONSTRAINT [FK_t_OrdenDetalle_t_Usuario] FOREIGN KEY([IdUsuarioBorro])
REFERENCES [dbo].[t_Usuario] ([Id])
GO
ALTER TABLE [dbo].[t_OrdenDetalle] CHECK CONSTRAINT [FK_t_OrdenDetalle_t_Usuario]
GO
ALTER TABLE [dbo].[t_Producto]  WITH CHECK ADD  CONSTRAINT [FK_t_Producto_t_Categoria] FOREIGN KEY([IdCategoria])
REFERENCES [dbo].[t_Categoria] ([Id])
GO
ALTER TABLE [dbo].[t_Producto] CHECK CONSTRAINT [FK_t_Producto_t_Categoria]
GO
ALTER TABLE [dbo].[t_Producto]  WITH CHECK ADD  CONSTRAINT [FK_t_Producto_t_Tienda] FOREIGN KEY([IdTienda])
REFERENCES [dbo].[t_Tienda] ([Id])
GO
ALTER TABLE [dbo].[t_Producto] CHECK CONSTRAINT [FK_t_Producto_t_Tienda]
GO
ALTER TABLE [dbo].[t_Producto]  WITH CHECK ADD  CONSTRAINT [FK_t_Producto_t_Usuario] FOREIGN KEY([IdUsuarioCreo])
REFERENCES [dbo].[t_Usuario] ([Id])
GO
ALTER TABLE [dbo].[t_Producto] CHECK CONSTRAINT [FK_t_Producto_t_Usuario]
GO
ALTER TABLE [dbo].[t_Producto]  WITH CHECK ADD  CONSTRAINT [FK_t_Producto_t_Usuario1] FOREIGN KEY([IdUsuarioBorro])
REFERENCES [dbo].[t_Usuario] ([Id])
GO
ALTER TABLE [dbo].[t_Producto] CHECK CONSTRAINT [FK_t_Producto_t_Usuario1]
GO
ALTER TABLE [dbo].[t_Sesion]  WITH CHECK ADD  CONSTRAINT [FK_t_Sesion_t_Usuario] FOREIGN KEY([UserId])
REFERENCES [dbo].[t_Usuario] ([Id])
GO
ALTER TABLE [dbo].[t_Sesion] CHECK CONSTRAINT [FK_t_Sesion_t_Usuario]
GO
ALTER TABLE [dbo].[t_Tienda]  WITH CHECK ADD  CONSTRAINT [FK_t_Tienda_t_CategoriaTienda] FOREIGN KEY([IdCategoriaTienda])
REFERENCES [dbo].[t_CategoriaTienda] ([Id])
GO
ALTER TABLE [dbo].[t_Tienda] CHECK CONSTRAINT [FK_t_Tienda_t_CategoriaTienda]
GO
ALTER TABLE [dbo].[t_Usuario]  WITH CHECK ADD  CONSTRAINT [FK_t_Usuario_t_Cliente] FOREIGN KEY([ClienteId])
REFERENCES [dbo].[t_Cliente] ([Id])
GO
ALTER TABLE [dbo].[t_Usuario] CHECK CONSTRAINT [FK_t_Usuario_t_Cliente]
GO
ALTER TABLE [dbo].[t_Usuario]  WITH CHECK ADD  CONSTRAINT [FK_t_Usuario_t_Rol] FOREIGN KEY([RolId])
REFERENCES [dbo].[t_Rol] ([Id])
GO
ALTER TABLE [dbo].[t_Usuario] CHECK CONSTRAINT [FK_t_Usuario_t_Rol]
GO
ALTER TABLE [dbo].[t_ZonaDetalle]  WITH CHECK ADD  CONSTRAINT [FK_t_ZonaDetalle_t_Zona] FOREIGN KEY([IdZona])
REFERENCES [dbo].[t_Zona] ([Id])
GO
ALTER TABLE [dbo].[t_ZonaDetalle] CHECK CONSTRAINT [FK_t_ZonaDetalle_t_Zona]
GO
/****** Object:  StoredProcedure [dbo].[sp_alterdiagram]    Script Date: 9/10/2020 10:58:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

	CREATE PROCEDURE [dbo].[sp_alterdiagram]
	(
		@diagramname 	sysname,
		@owner_id	int	= null,
		@version 	int,
		@definition 	varbinary(max)
	)
	WITH EXECUTE AS 'dbo'
	AS
	BEGIN
		set nocount on
	
		declare @theId 			int
		declare @retval 		int
		declare @IsDbo 			int
		
		declare @UIDFound 		int
		declare @DiagId			int
		declare @ShouldChangeUID	int
	
		if(@diagramname is null)
		begin
			RAISERROR ('Invalid ARG', 16, 1)
			return -1
		end
	
		execute as caller;
		select @theId = DATABASE_PRINCIPAL_ID();	 
		select @IsDbo = IS_MEMBER(N'db_owner'); 
		if(@owner_id is null)
			select @owner_id = @theId;
		revert;
	
		select @ShouldChangeUID = 0
		select @DiagId = diagram_id, @UIDFound = principal_id from dbo.sysdiagrams where principal_id = @owner_id and name = @diagramname 
		
		if(@DiagId IS NULL or (@IsDbo = 0 and @theId <> @UIDFound))
		begin
			RAISERROR ('Diagram does not exist or you do not have permission.', 16, 1);
			return -3
		end
	
		if(@IsDbo <> 0)
		begin
			if(@UIDFound is null or USER_NAME(@UIDFound) is null) -- invalid principal_id
			begin
				select @ShouldChangeUID = 1 ;
			end
		end

		-- update dds data			
		update dbo.sysdiagrams set definition = @definition where diagram_id = @DiagId ;

		-- change owner
		if(@ShouldChangeUID = 1)
			update dbo.sysdiagrams set principal_id = @theId where diagram_id = @DiagId ;

		-- update dds version
		if(@version is not null)
			update dbo.sysdiagrams set version = @version where diagram_id = @DiagId ;

		return 0
	END
	
GO
/****** Object:  StoredProcedure [dbo].[SP_Categoria_Crear]    Script Date: 9/10/2020 10:58:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Javier Lopez
-- Create Date: 2020/04/11
-- Description: Crear categoria
-- =============================================
CREATE PROCEDURE [dbo].[SP_Categoria_Crear] (@sesionId VARCHAR(255),
@categoria VARCHAR(255), @tiendaId int)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON

	DECLARE @usuarioId INT
	DECLARE @clienteId INT

	SELECT
		@usuarioId = u.Id
	   ,@clienteId = u.ClienteId
	FROM t_Sesion s WITH (NOLOCK)
	INNER JOIN t_Usuario u WITH (NOLOCK)
		ON u.Id = s.UserId


	INSERT INTO t_Categoria (IdCliente, Categoria, IdUsuarioCreo, FechaCreo, IdUsuarioBorro, Borrado, FechaBorrado, IdTienda)
		VALUES (@clienteId, @categoria, @usuarioId, GETDATE(), NULL, 0, NULL, @tiendaId);

END
GO
/****** Object:  StoredProcedure [dbo].[SP_Categoria_List]    Script Date: 9/10/2020 10:58:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Javier Lopez
-- Create Date: 2020/04/11
-- Description: Listar Categorias
-- =============================================
CREATE PROCEDURE [dbo].[SP_Categoria_List] (@clientId int, @tiendaId int)
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON


	SELECT
		c.Id
	   ,c.Categoria
	   ,c.FechaCreo
	FROM t_Categoria c WITH (NOLOCK)
	INNER JOIN t_Usuario u WITH (NOLOCK)
		on u.Id = c.IdUsuarioCreo
	where u.ClienteId = @clientId
	  and c.IdTienda = @tiendaId

END
GO
/****** Object:  StoredProcedure [dbo].[sp_creatediagram]    Script Date: 9/10/2020 10:58:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

	CREATE PROCEDURE [dbo].[sp_creatediagram]
	(
		@diagramname 	sysname,
		@owner_id		int	= null, 	
		@version 		int,
		@definition 	varbinary(max)
	)
	WITH EXECUTE AS 'dbo'
	AS
	BEGIN
		set nocount on
	
		declare @theId int
		declare @retval int
		declare @IsDbo	int
		declare @userName sysname
		if(@version is null or @diagramname is null)
		begin
			RAISERROR (N'E_INVALIDARG', 16, 1);
			return -1
		end
	
		execute as caller;
		select @theId = DATABASE_PRINCIPAL_ID(); 
		select @IsDbo = IS_MEMBER(N'db_owner');
		revert; 
		
		if @owner_id is null
		begin
			select @owner_id = @theId;
		end
		else
		begin
			if @theId <> @owner_id
			begin
				if @IsDbo = 0
				begin
					RAISERROR (N'E_INVALIDARG', 16, 1);
					return -1
				end
				select @theId = @owner_id
			end
		end
		-- next 2 line only for test, will be removed after define name unique
		if EXISTS(select diagram_id from dbo.sysdiagrams where principal_id = @theId and name = @diagramname)
		begin
			RAISERROR ('The name is already used.', 16, 1);
			return -2
		end
	
		insert into dbo.sysdiagrams(name, principal_id , version, definition)
				VALUES(@diagramname, @theId, @version, @definition) ;
		
		select @retval = @@IDENTITY 
		return @retval
	END
	
GO
/****** Object:  StoredProcedure [dbo].[sp_dropdiagram]    Script Date: 9/10/2020 10:58:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

	CREATE PROCEDURE [dbo].[sp_dropdiagram]
	(
		@diagramname 	sysname,
		@owner_id	int	= null
	)
	WITH EXECUTE AS 'dbo'
	AS
	BEGIN
		set nocount on
		declare @theId 			int
		declare @IsDbo 			int
		
		declare @UIDFound 		int
		declare @DiagId			int
	
		if(@diagramname is null)
		begin
			RAISERROR ('Invalid value', 16, 1);
			return -1
		end
	
		EXECUTE AS CALLER;
		select @theId = DATABASE_PRINCIPAL_ID();
		select @IsDbo = IS_MEMBER(N'db_owner'); 
		if(@owner_id is null)
			select @owner_id = @theId;
		REVERT; 
		
		select @DiagId = diagram_id, @UIDFound = principal_id from dbo.sysdiagrams where principal_id = @owner_id and name = @diagramname 
		if(@DiagId IS NULL or (@IsDbo = 0 and @UIDFound <> @theId))
		begin
			RAISERROR ('Diagram does not exist or you do not have permission.', 16, 1)
			return -3
		end
	
		delete from dbo.sysdiagrams where diagram_id = @DiagId;
	
		return 0;
	END
	
GO
/****** Object:  StoredProcedure [dbo].[sp_helpdiagramdefinition]    Script Date: 9/10/2020 10:58:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

	CREATE PROCEDURE [dbo].[sp_helpdiagramdefinition]
	(
		@diagramname 	sysname,
		@owner_id	int	= null 		
	)
	WITH EXECUTE AS N'dbo'
	AS
	BEGIN
		set nocount on

		declare @theId 		int
		declare @IsDbo 		int
		declare @DiagId		int
		declare @UIDFound	int
	
		if(@diagramname is null)
		begin
			RAISERROR (N'E_INVALIDARG', 16, 1);
			return -1
		end
	
		execute as caller;
		select @theId = DATABASE_PRINCIPAL_ID();
		select @IsDbo = IS_MEMBER(N'db_owner');
		if(@owner_id is null)
			select @owner_id = @theId;
		revert; 
	
		select @DiagId = diagram_id, @UIDFound = principal_id from dbo.sysdiagrams where principal_id = @owner_id and name = @diagramname;
		if(@DiagId IS NULL or (@IsDbo = 0 and @UIDFound <> @theId ))
		begin
			RAISERROR ('Diagram does not exist or you do not have permission.', 16, 1);
			return -3
		end

		select version, definition FROM dbo.sysdiagrams where diagram_id = @DiagId ; 
		return 0
	END
	
GO
/****** Object:  StoredProcedure [dbo].[sp_helpdiagrams]    Script Date: 9/10/2020 10:58:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

	CREATE PROCEDURE [dbo].[sp_helpdiagrams]
	(
		@diagramname sysname = NULL,
		@owner_id int = NULL
	)
	WITH EXECUTE AS N'dbo'
	AS
	BEGIN
		DECLARE @user sysname
		DECLARE @dboLogin bit
		EXECUTE AS CALLER;
			SET @user = USER_NAME();
			SET @dboLogin = CONVERT(bit,IS_MEMBER('db_owner'));
		REVERT;
		SELECT
			[Database] = DB_NAME(),
			[Name] = name,
			[ID] = diagram_id,
			[Owner] = USER_NAME(principal_id),
			[OwnerID] = principal_id
		FROM
			sysdiagrams
		WHERE
			(@dboLogin = 1 OR USER_NAME(principal_id) = @user) AND
			(@diagramname IS NULL OR name = @diagramname) AND
			(@owner_id IS NULL OR principal_id = @owner_id)
		ORDER BY
			4, 5, 1
	END
	
GO
/****** Object:  StoredProcedure [dbo].[sp_listado_ordenes]    Script Date: 9/10/2020 10:58:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_listado_ordenes]
(
@fecha_1 datetime, @fecha_2  datetime
)
as
begin

select 
			T0.Id
			,T0.FechaCreo
			,T0.Nombre 'Nombre Cliente'
			,T0.Telefono
			,T0.IdTienda 'Codigo de Tienda'
			,T1.Nombre'Nombre Tienda'
			,T0.IdZona 'Zona Envio'			
			,T0.Colonia
			,T0.Ciudad
			,T2.Valor 'Valor Envio'
			,sum(T3.Cantidad*T3.Precio)'Total Orden'
from t_Orden T0
INNER JOIN t_Tienda T1 on T0.IdTienda=T1.Id
INNER JOIN t_Zona T2 on T0.IdZona=T2.Id
INNER JOIN t_OrdenDetalle T3 on T0.id=T3.Id
where T0.FechaCreo between @fecha_1 and @fecha_2

group by 			T0.Id
			,T0.FechaCreo
			,T0.Nombre
			,T0.Telefono
			,T0.IdTienda
			,T1.Nombre
			,T0.IdZona			
			,T0.Colonia
			,T0.Ciudad
			,T2.Valor 
end
GO
/****** Object:  StoredProcedure [dbo].[sp_renamediagram]    Script Date: 9/10/2020 10:58:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

	CREATE PROCEDURE [dbo].[sp_renamediagram]
	(
		@diagramname 		sysname,
		@owner_id		int	= null,
		@new_diagramname	sysname
	
	)
	WITH EXECUTE AS 'dbo'
	AS
	BEGIN
		set nocount on
		declare @theId 			int
		declare @IsDbo 			int
		
		declare @UIDFound 		int
		declare @DiagId			int
		declare @DiagIdTarg		int
		declare @u_name			sysname
		if((@diagramname is null) or (@new_diagramname is null))
		begin
			RAISERROR ('Invalid value', 16, 1);
			return -1
		end
	
		EXECUTE AS CALLER;
		select @theId = DATABASE_PRINCIPAL_ID();
		select @IsDbo = IS_MEMBER(N'db_owner'); 
		if(@owner_id is null)
			select @owner_id = @theId;
		REVERT;
	
		select @u_name = USER_NAME(@owner_id)
	
		select @DiagId = diagram_id, @UIDFound = principal_id from dbo.sysdiagrams where principal_id = @owner_id and name = @diagramname 
		if(@DiagId IS NULL or (@IsDbo = 0 and @UIDFound <> @theId))
		begin
			RAISERROR ('Diagram does not exist or you do not have permission.', 16, 1)
			return -3
		end
	
		-- if((@u_name is not null) and (@new_diagramname = @diagramname))	-- nothing will change
		--	return 0;
	
		if(@u_name is null)
			select @DiagIdTarg = diagram_id from dbo.sysdiagrams where principal_id = @theId and name = @new_diagramname
		else
			select @DiagIdTarg = diagram_id from dbo.sysdiagrams where principal_id = @owner_id and name = @new_diagramname
	
		if((@DiagIdTarg is not null) and  @DiagId <> @DiagIdTarg)
		begin
			RAISERROR ('The name is already used.', 16, 1);
			return -2
		end		
	
		if(@u_name is null)
			update dbo.sysdiagrams set [name] = @new_diagramname, principal_id = @theId where diagram_id = @DiagId
		else
			update dbo.sysdiagrams set [name] = @new_diagramname where diagram_id = @DiagId
		return 0
	END
	
GO
/****** Object:  StoredProcedure [dbo].[sp_upgraddiagrams]    Script Date: 9/10/2020 10:58:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

	CREATE PROCEDURE [dbo].[sp_upgraddiagrams]
	AS
	BEGIN
		IF OBJECT_ID(N'dbo.sysdiagrams') IS NOT NULL
			return 0;
	
		CREATE TABLE dbo.sysdiagrams
		(
			name sysname NOT NULL,
			principal_id int NOT NULL,	-- we may change it to varbinary(85)
			diagram_id int PRIMARY KEY IDENTITY,
			version int,
	
			definition varbinary(max)
			CONSTRAINT UK_principal_name UNIQUE
			(
				principal_id,
				name
			)
		);


		/* Add this if we need to have some form of extended properties for diagrams */
		/*
		IF OBJECT_ID(N'dbo.sysdiagram_properties') IS NULL
		BEGIN
			CREATE TABLE dbo.sysdiagram_properties
			(
				diagram_id int,
				name sysname,
				value varbinary(max) NOT NULL
			)
		END
		*/

		IF OBJECT_ID(N'dbo.dtproperties') IS NOT NULL
		begin
			insert into dbo.sysdiagrams
			(
				[name],
				[principal_id],
				[version],
				[definition]
			)
			select	 
				convert(sysname, dgnm.[uvalue]),
				DATABASE_PRINCIPAL_ID(N'dbo'),			-- will change to the sid of sa
				0,							-- zero for old format, dgdef.[version],
				dgdef.[lvalue]
			from dbo.[dtproperties] dgnm
				inner join dbo.[dtproperties] dggd on dggd.[property] = 'DtgSchemaGUID' and dggd.[objectid] = dgnm.[objectid]	
				inner join dbo.[dtproperties] dgdef on dgdef.[property] = 'DtgSchemaDATA' and dgdef.[objectid] = dgnm.[objectid]
				
			where dgnm.[property] = 'DtgSchemaNAME' and dggd.[uvalue] like N'_EA3E6268-D998-11CE-9454-00AA00A3F36E_' 
			return 2;
		end
		return 1;
	END
	
GO
/****** Object:  StoredProcedure [dbo].[sp_ventas_por_categoria]    Script Date: 9/10/2020 10:58:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_ventas_por_categoria]
(
@fecha_1 datetime, @fecha_2  datetime
)
as
begin

select 
			T4.Categoria
			,sum(T2.Valor) 'Total Envio'
			,count(T0.id) 'Cantidad de Ordenes'
			,sum(T3.Cantidad*T3.Precio)'Total Ordenes'
from t_Orden T0
INNER JOIN t_Tienda T1 on T0.IdTienda=T1.Id
INNER JOIN t_Zona T2 on T0.IdZona=T2.Id
INNER JOIN t_OrdenDetalle T3 on T0.id=T3.Id
Inner Join t_Categoria T4 on T1.idCategoria=t4.Id
where T0.FechaCreo between @fecha_1 and @fecha_2
group by 			
			T4.Categoria
			
end
GO
/****** Object:  StoredProcedure [dbo].[sp_ventas_por_tienda]    Script Date: 9/10/2020 10:58:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_ventas_por_tienda]
(
@fecha_1 datetime, @fecha_2  datetime
)
as
begin

select 
			T0.IdTienda 'Codigo de Tienda'
			,T1.Nombre'Nombre Tienda'
			,sum(T2.Valor) 'Total Envio'
			,count(T0.id) 'Cantidad de Ordenes'
			,sum(T3.Cantidad*T3.Precio)'Total Ordenes'
from t_Orden T0
INNER JOIN t_Tienda T1 on T0.IdTienda=T1.Id
INNER JOIN t_Zona T2 on T0.IdZona=T2.Id
INNER JOIN t_OrdenDetalle T3 on T0.id=T3.Id
where T0.FechaCreo between @fecha_1 and @fecha_2
group by 			
			T0.IdTienda
			,T1.Nombre
			
end
GO
