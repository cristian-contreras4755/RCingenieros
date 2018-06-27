USE APUBD_Gasolutions
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[VentaGasolutions]') AND type in (N'U'))
DROP TABLE VentaGasolutions
GO
CREATE TABLE VentaGasolutions(
	VentaGasolutionsId INT IDENTITY(1, 1), FechaEmision DATETIME2, Gravadas NUMERIC(18, 6), IdDocumento VARCHAR(100), CalculoIgv NUMERIC(18, 6), MonedaId VARCHAR(100), MontoEnLetras VARCHAR(500), PlacaVehiculo VARCHAR(50), TipoDocumento VARCHAR(50),TotalIgv NUMERIC(18, 6), TotalVenta NUMERIC(18, 6),
	TipoDocumentoEmisor VARCHAR(100), NroDocumentoEmisor VARCHAR(100), NombreComercialEmisor VARCHAR(100), NombreLegalEmisor VARCHAR(100), DepartamentoEmisor VARCHAR(500), ProvinciaEmisor VARCHAR(500), DistritoEmisor VARCHAR(500), UbigeoEmisor VARCHAR(100), DireccionEmisor VARCHAR(500),
	TipoDocumentoReceptor VARCHAR(100), NroDocumentoReceptor VARCHAR(100), NombreComercialReceptor VARCHAR(100), NombreLegalReceptor VARCHAR(100), DireccionReceptor VARCHAR(500),
	CodigoRespuesta VARCHAR(100), Exito INT, MensajeError VARCHAR(1000), MensajeRespuesta VARCHAR(1000), NombreArchivo VARCHAR(1000), NroTicket VARCHAR(100), EstadoId INT, ComprobanteImpreso VARCHAR(1000),
	FechaCreacion DATETIME2
)
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[VentaDetalleGasolutions]') AND type in (N'U'))
DROP TABLE VentaDetalleGasolutions
GO
CREATE TABLE VentaDetalleGasolutions(
	VentaDetalleGasolutionsId INT IDENTITY(1, 1), VentaGasolutionsId INT, 
	Id INT, Cantidad NUMERIC(18, 6), CodigoItem VARCHAR(50), Descripcion VARCHAR(500), Impuesto NUMERIC(18, 6), PrecioUnitario NUMERIC(18, 6), TotalVenta NUMERIC(18, 6), UnidadMedida VARCHAR(50), TipoImpuesto VARCHAR(50), TipoPrecio VARCHAR(50),
	FechaCreacion DATETIME2
)
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ObtenerVentaGasolutionsPaginado]') AND type in (N'P', N'PC'))
DROP PROCEDURE ObtenerVentaGasolutionsPaginado
GO
CREATE PROCEDURE ObtenerVentaGasolutionsPaginado
	@VentaGasolutionsId INT, @NumeroDocumento VARCHAR(20), @TipoComprobanteId VARCHAR(2), @NumeroComprobante VARCHAR(50), @FechaEmisionInicio DATETIME2, @FechaEmisionFin DATETIME2, @EstadoId VARCHAR(2), @MonedaId VARCHAR(10), @TamanioPagina INT, @NumeroPagina INT
AS
	DECLARE @UltimaPagina INT
	SET @UltimaPagina = (SELECT COUNT(*) FROM VentaGasolutions VG
						 WHERE	(VG.VentaGasolutionsId = @VentaGasolutionsId OR @VentaGasolutionsId = 0) AND (VG.NroDocumentoReceptor = @NumeroDocumento OR @NumeroDocumento = '') AND (VG.TipoDocumento = @TipoComprobanteId OR @TipoComprobanteId = '') AND
								(VG.IdDocumento = @NumeroComprobante OR @NumeroComprobante = '') AND
								(VG.FechaEmision BETWEEN @FechaEmisionInicio AND @FechaEmisionFin) AND (VG.EstadoId = @EstadoId OR @EstadoId = 0) AND (VG.MonedaId = @MonedaId OR @MonedaId = 0)
						 )
	SET @TamanioPagina = CASE WHEN @TamanioPagina = 0 THEN @UltimaPagina ELSE @TamanioPagina END
	SELECT * FROM (
		SELECT	ROW_NUMBER() OVER (ORDER BY VG.VentaGasolutionsId DESC) NumeroFila, ISNULL(@UltimaPagina, 0) TotalFilas,
		
		VG.VentaGasolutionsId, VG.FechaEmision, VG.Gravadas, VG.IdDocumento, VG.CalculoIgv, VG.MonedaId, CASE VG.MonedaId WHEN 'PEN' THEN 'Soles' WHEN 'USD' THEN 'Dólares' ELSE '' END Moneda, CASE VG.MonedaId WHEN 'PEN' THEN 'S/' WHEN 'USD' THEN 'USD$' ELSE '' END SimboloMoneda, VG.MontoEnLetras, VG.PlacaVehiculo, VG.TipoDocumento, VG.TotalIgv, VG.TotalVenta,
		VG.TipoDocumentoEmisor, VG.NroDocumentoEmisor, VG.NombreComercialEmisor, VG.NombreLegalEmisor, VG.DepartamentoEmisor, VG.ProvinciaEmisor, VG.DistritoEmisor, VG.UbigeoEmisor, VG.DireccionEmisor,
		VG.TipoDocumentoReceptor, VG.NroDocumentoReceptor, VG.NombreComercialReceptor, VG.NombreLegalReceptor, VG.DireccionReceptor,
		VG.CodigoRespuesta, VG.Exito, VG.MensajeError, VG.MensajeRespuesta, VG.NombreArchivo, VG.NroTicket, VG.EstadoId, CASE VG.EstadoId WHEN 1 THEN 'Aceptado' WHEN 2 THEN 'Rechazado' WHEN 3 THEN 'Pendiente' WHEN 4 THEN 'Anulado' ELSE 'Pendiente' END Estado, VG.ComprobanteImpreso, VG.FechaCreacion
		FROM VentaGasolutions VG
		WHERE	(VG.VentaGasolutionsId = @VentaGasolutionsId OR @VentaGasolutionsId = 0) AND (VG.NroDocumentoReceptor = @NumeroDocumento OR @NumeroDocumento = '') AND (VG.TipoDocumento = @TipoComprobanteId OR @TipoComprobanteId = '') AND
				(VG.IdDocumento = @NumeroComprobante OR @NumeroComprobante = '') AND
				(VG.FechaEmision BETWEEN @FechaEmisionInicio AND @FechaEmisionFin) AND (VG.EstadoId = @EstadoId OR @EstadoId = 0) AND (VG.MonedaId = @MonedaId OR @MonedaId = 0)
	) AS Tabla
	WHERE	NumeroFila BETWEEN @TamanioPagina * @NumeroPagina + 1 AND @TamanioPagina * (@NumeroPagina + 1)
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ObtenerVentaGasolutions]') AND type in (N'P', N'PC'))
DROP PROCEDURE ObtenerVentaGasolutions
GO
CREATE PROCEDURE ObtenerVentaGasolutions
	@VentaGasolutionsId INT
AS
	SELECT	1 NumeroFila, 1 TotalFilas,
	VG.VentaGasolutionsId, VG.FechaEmision, VG.Gravadas, VG.IdDocumento, VG.CalculoIgv, VG.MonedaId, CASE VG.MonedaId WHEN 'PEN' THEN 'Soles' WHEN 'USD' THEN 'Dólares' ELSE '' END Moneda, CASE VG.MonedaId WHEN 'PEN' THEN 'S/' WHEN 'USD' THEN 'USD$' ELSE '' END SimboloMoneda, VG.MontoEnLetras, VG.PlacaVehiculo, VG.TipoDocumento, VG.TotalIgv, VG.TotalVenta,
	VG.TipoDocumentoEmisor, VG.NroDocumentoEmisor, VG.NombreComercialEmisor, VG.NombreLegalEmisor, VG.DepartamentoEmisor, VG.ProvinciaEmisor, VG.DistritoEmisor, VG.UbigeoEmisor, VG.DireccionEmisor,
	VG.TipoDocumentoReceptor, VG.NroDocumentoReceptor, VG.NombreComercialReceptor, VG.NombreLegalReceptor, VG.DireccionReceptor,
	VG.CodigoRespuesta, VG.Exito, VG.MensajeError, VG.MensajeRespuesta, VG.NombreArchivo, VG.NroTicket, VG.EstadoId, CASE VG.EstadoId WHEN 1 THEN 'Aceptado' WHEN 2 THEN 'Rechazado' WHEN 3 THEN 'Pendiente' WHEN 4 THEN 'Anulado' ELSE 'Pendiente' END Estado, VG.ComprobanteImpreso, VG.FechaCreacion
	FROM VentaGasolutions VG
	WHERE	(VG.VentaGasolutionsId = @VentaGasolutionsId OR @VentaGasolutionsId = 0)
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InsertarVentaGasolutions]') AND type in (N'P', N'PC'))
DROP PROCEDURE InsertarVentaGasolutions
GO
CREATE PROCEDURE InsertarVentaGasolutions
	@FechaEmision DATETIME2, @Gravadas NUMERIC(18, 6), @IdDocumento VARCHAR(100), @CalculoIgv NUMERIC(18, 6), @MonedaId VARCHAR(20), @MontoEnLetras VARCHAR(500), @PlacaVehiculo VARCHAR(50), @TipoDocumento VARCHAR(50), @TotalIgv NUMERIC(18, 6), @TotalVenta NUMERIC(18, 6),
	@TipoDocumentoEmisor VARCHAR(50), @NroDocumentoEmisor VARCHAR(50), @NombreComercialEmisor VARCHAR(500), @NombreLegalEmisor VARCHAR(500), @DepartamentoEmisor VARCHAR(500), @ProvinciaEmisor VARCHAR(500), @DistritoEmisor VARCHAR(500), @UbigeoEmisor VARCHAR(50), @DireccionEmisor VARCHAR(500),
	@TipoDocumentoReceptor VARCHAR(500), @NroDocumentoReceptor VARCHAR(500), @NombreComercialReceptor VARCHAR(500), @NombreLegalReceptor VARCHAR(500), @DireccionReceptor VARCHAR(500),
	@CodigoRespuesta VARCHAR(500), @Exito INT, @MensajeError VARCHAR(1000), @MensajeRespuesta VARCHAR(1000), @NombreArchivo VARCHAR(500), @NroTicket VARCHAR(500), @EstadoId INT, @ComprobanteImpreso VARCHAR(1000)
AS
INSERT INTO VentaGasolutions(
    FechaEmision, Gravadas, IdDocumento, CalculoIgv, MonedaId, MontoEnLetras, PlacaVehiculo, TipoDocumento, TotalIgv, TotalVenta,
	TipoDocumentoEmisor, NroDocumentoEmisor, NombreComercialEmisor, NombreLegalEmisor, DepartamentoEmisor, ProvinciaEmisor, DistritoEmisor, UbigeoEmisor, DireccionEmisor,
	TipoDocumentoReceptor, NroDocumentoReceptor, NombreComercialReceptor, NombreLegalReceptor, DireccionReceptor,
	CodigoRespuesta, Exito, MensajeError, MensajeRespuesta, NombreArchivo, NroTicket, EstadoId, ComprobanteImpreso, FechaCreacion
)
VALUES(
	@FechaEmision, @Gravadas, @IdDocumento, @CalculoIgv, @MonedaId, @MontoEnLetras, @PlacaVehiculo, @TipoDocumento, @TotalIgv, @TotalVenta,
	@TipoDocumentoEmisor, @NroDocumentoEmisor, @NombreComercialEmisor, @NombreLegalEmisor, @DepartamentoEmisor, @ProvinciaEmisor, @DistritoEmisor, @UbigeoEmisor, @DireccionEmisor,
	@TipoDocumentoReceptor, @NroDocumentoReceptor, @NombreComercialReceptor, @NombreLegalReceptor, @DireccionReceptor,
	@CodigoRespuesta, @Exito, @MensajeError, @MensajeRespuesta, @NombreArchivo, @NroTicket, @EstadoId, @ComprobanteImpreso, SYSDATETIME()
)
SELECT SCOPE_IDENTITY()
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ObtenerVentaDetalleGasolutionsPaginado]') AND type in (N'P', N'PC'))
DROP PROCEDURE ObtenerVentaDetalleGasolutionsPaginado
GO
CREATE PROCEDURE ObtenerVentaDetalleGasolutionsPaginado
	@VentaGasolutionsId INT, @NumeroDocumento VARCHAR(20), @TipoComprobanteId VARCHAR(2), @NumeroComprobante VARCHAR(50), @FechaEmisionInicio DATETIME2, @FechaEmisionFin DATETIME2, @EstadoId VARCHAR(2), @MonedaId VARCHAR(10), @TamanioPagina INT, @NumeroPagina INT
AS
	DECLARE @UltimaPagina INT
	SET @UltimaPagina = (SELECT COUNT(*) FROM VentaGasolutions VG
						 WHERE	(VG.VentaGasolutionsId = @VentaGasolutionsId OR @VentaGasolutionsId = 0) AND (VG.NroDocumentoReceptor = @NumeroDocumento OR @NumeroDocumento = '') AND (VG.TipoDocumento = @TipoComprobanteId OR @TipoComprobanteId = '') AND
								(VG.IdDocumento = @NumeroComprobante OR @NumeroComprobante = '') AND
								(VG.FechaEmision BETWEEN @FechaEmisionInicio AND @FechaEmisionFin) AND (VG.EstadoId = @EstadoId OR @EstadoId = 0) AND (VG.MonedaId = @MonedaId OR @MonedaId = 0)
						 )
	SET @TamanioPagina = CASE WHEN @TamanioPagina = 0 THEN @UltimaPagina ELSE @TamanioPagina END
	SELECT * FROM (
		SELECT	ROW_NUMBER() OVER (ORDER BY VG.VentaGasolutionsId DESC) NumeroFila, ISNULL(@UltimaPagina, 0) TotalFilas,
		
		VG.VentaGasolutionsId, VG.FechaEmision, VG.Gravadas, VG.IdDocumento, VG.CalculoIgv, VG.MonedaId, VG.MontoEnLetras, VG.PlacaVehiculo, VG.TipoDocumento, VG.TotalIgv, VG.TotalVenta,
		VG.TipoDocumentoEmisor, VG.NroDocumentoEmisor, VG.NombreComercialEmisor, VG.NombreLegalEmisor, VG.DepartamentoEmisor, VG.ProvinciaEmisor, VG.DistritoEmisor, VG.UbigeoEmisor, VG.DireccionEmisor,
		VG.TipoDocumentoReceptor, VG.NroDocumentoReceptor, VG.NombreComercialReceptor, VG.NombreLegalReceptor, VG.DireccionReceptor,
		VG.CodigoRespuesta, VG.Exito, VG.MensajeError, VG.MensajeRespuesta, VG.NombreArchivo, VG.NroTicket, VG.EstadoId, VG.ComprobanteImpreso, VG.FechaCreacion
		FROM VentaGasolutions VG
		WHERE	(VG.VentaGasolutionsId = @VentaGasolutionsId OR @VentaGasolutionsId = 0) AND (VG.NroDocumentoReceptor = @NumeroDocumento OR @NumeroDocumento = '') AND (VG.TipoDocumento = @TipoComprobanteId OR @TipoComprobanteId = '') AND
				(VG.IdDocumento = @NumeroComprobante OR @NumeroComprobante = '') AND
				(VG.FechaEmision BETWEEN @FechaEmisionInicio AND @FechaEmisionFin) AND (VG.EstadoId = @EstadoId OR @EstadoId = 0) AND (VG.MonedaId = @MonedaId OR @MonedaId = 0)
	) AS Tabla
	WHERE	NumeroFila BETWEEN @TamanioPagina * @NumeroPagina + 1 AND @TamanioPagina * (@NumeroPagina + 1)
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ObtenerVentaDetalleGasolutions]') AND type in (N'P', N'PC'))
DROP PROCEDURE ObtenerVentaDetalleGasolutions
GO
CREATE PROCEDURE ObtenerVentaDetalleGasolutions
	@VentaGasolutionsId INT, @VentaDetalleGasolutionsId INT
AS
	SELECT	1 NumeroFila, 1 TotalFilas,
	VG.VentaGasolutionsId, VG.FechaEmision, VG.Gravadas, VG.IdDocumento, VG.CalculoIgv, VG.MonedaId, VG.MontoEnLetras, VG.PlacaVehiculo, VG.TipoDocumento, VG.TotalIgv, VG.TotalVenta,
	VG.TipoDocumentoEmisor, VG.NroDocumentoEmisor, VG.NombreComercialEmisor, VG.NombreLegalEmisor, VG.DepartamentoEmisor, VG.ProvinciaEmisor, VG.DistritoEmisor, VG.UbigeoEmisor,
	VG.TipoDocumentoReceptor, VG.NroDocumentoReceptor, VG.NombreComercialReceptor, VG.NombreLegalReceptor, VG.DireccionReceptor,
	VG.CodigoRespuesta, VG.Exito, VG.MensajeError, VG.MensajeRespuesta, VG.NombreArchivo, VG.NroTicket, VG.EstadoId, VG.ComprobanteImpreso, VG.FechaCreacion,
	
	VDG.VentaDetalleGasolutionsId, VDG.VentaGasolutionsId, VDG.Id, VDG.Cantidad, VDG.CodigoItem, VDG.Descripcion, VDG.Impuesto, VDG.PrecioUnitario,
    VDG.TotalVenta, VDG.UnidadMedida, VDG.TipoImpuesto, VDG.TipoPrecio, VDG.FechaCreacion
	FROM VentaGasolutions VG
	LEFT JOIN VentaDetalleGasolutions VDG ON VDG.VentaGasolutionsId = VG.VentaGasolutionsId
	WHERE	(VG.VentaGasolutionsId = @VentaGasolutionsId OR @VentaGasolutionsId = 0) AND (VDG.VentaDetalleGasolutionsId = @VentaDetalleGasolutionsId OR @VentaDetalleGasolutionsId = 0)
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InsertarVentaDetalleGasolutions]') AND type in (N'P', N'PC'))
DROP PROCEDURE InsertarVentaDetalleGasolutions
GO
CREATE PROCEDURE InsertarVentaDetalleGasolutions
	@VentaGasolutionsId INT, @Id INT, @Cantidad NUMERIC(18, 6), @CodigoItem VARCHAR(100), @Descripcion VARCHAR(500), @Impuesto NUMERIC(18, 6), @PrecioUnitario NUMERIC(18, 6), @TotalVenta NUMERIC(18, 6), @UnidadMedida VARCHAR(50), @TipoImpuesto VARCHAR(50), @TipoPrecio VARCHAR(50)
AS
INSERT INTO VentaDetalleGasolutions(
    VentaGasolutionsId, Id, Cantidad, CodigoItem,Descripcion, Impuesto, PrecioUnitario, TotalVenta, UnidadMedida, TipoImpuesto, TipoPrecio, FechaCreacion
)
VALUES(
	@VentaGasolutionsId, @Id, @Cantidad, @CodigoItem, @Descripcion, @Impuesto, @PrecioUnitario, @TotalVenta, @UnidadMedida, @TipoImpuesto, @TipoPrecio, SYSDATETIME()
)
SELECT SCOPE_IDENTITY()
GO



TRUNCATE TABLE Opcion
GO
-- Ventas
INSERT INTO Opcion(OpcionId, OpcionPadreId, Orden, Nombre, Url, Imagen, UsuarioCreacionId, FechaCreacion, UsuarioModificacion, FechaModificacion, Activo)
VALUES(1, 0, 1, 'Ventas', '#', '', 0, SYSDATETIME(), 0, SYSDATETIME(), 1)
INSERT INTO Opcion(OpcionId, OpcionPadreId, Orden, Nombre, Url, Imagen, UsuarioCreacionId, FechaCreacion, UsuarioModificacion, FechaModificacion, Activo)
VALUES(2, 1, 1, 'Cliente', '~/Configuracion/Cliente.aspx', '', 0, SYSDATETIME(), 0, SYSDATETIME(), 1)
INSERT INTO Opcion(OpcionId, OpcionPadreId, Orden ,Nombre ,[Url],Imagen,UsuarioCreacionId, FechaCreacion, UsuarioModificacion, FechaModificacion, Activo)
VALUES (3, 1, 2, 'Venta Mercadería', '~/Operaciones/Comprobante.aspx', '', 0, GETDATE(), 0, GETDATE(), 1)
INSERT INTO Opcion(OpcionId, OpcionPadreId, Orden ,Nombre, Url, Imagen, UsuarioCreacionId, FechaCreacion, UsuarioModificacion, FechaModificacion, Activo)
VALUES (4, 1, 3, 'Ventas Realizadas', '~/Operaciones/Venta.aspx', '', 0, GETDATE(), 0, GETDATE(), 1)
-- Compras
INSERT INTO Opcion(OpcionId, OpcionPadreId, Orden, Nombre, Url, Imagen, UsuarioCreacionId, FechaCreacion, UsuarioModificacion, FechaModificacion, Activo)
VALUES(5, 0, 2, 'Compras', '#', '', 0, SYSDATETIME(), 0, SYSDATETIME(), 1)
INSERT INTO Opcion(OpcionId, OpcionPadreId, Orden, Nombre, Url, Imagen, UsuarioCreacionId, FechaCreacion, UsuarioModificacion, FechaModificacion, Activo)
VALUES (6, 5, 1, 'Proveedor', '~/Maestros/Proveedor.aspx', '', 0, GETDATE(), 0, GETDATE(), 1)
INSERT INTO Opcion(OpcionId, OpcionPadreId, Orden ,Nombre ,[Url],Imagen,UsuarioCreacionId,FechaCreacion,UsuarioModificacion,FechaModificacion,Activo)
VALUES (7, 5, 2, 'Compra Mercadería', '~/Operaciones/Compra.aspx', '', 0, GETDATE(), 0, GETDATE(), 1)
-- Inventario
INSERT INTO Opcion(OpcionId, OpcionPadreId, Orden, Nombre, Url, Imagen, UsuarioCreacionId, FechaCreacion, UsuarioModificacion, FechaModificacion, Activo)
VALUES(8, 0, 3, 'Inventario', '#', '', 0, SYSDATETIME(), 0, SYSDATETIME(), 1)
INSERT INTO Opcion(OpcionId, OpcionPadreId, Orden, Nombre, Url, Imagen, UsuarioCreacionId, FechaCreacion, UsuarioModificacion, FechaModificacion, Activo)
VALUES(9, 8, 1, 'Almacen', '~/Configuracion/Almacen.aspx', '', 0, SYSDATETIME(), 0, SYSDATETIME(), 1)
INSERT INTO Opcion(OpcionId, OpcionPadreId, Orden, Nombre, Url, Imagen, UsuarioCreacionId, FechaCreacion, UsuarioModificacion, FechaModificacion, Activo)
VALUES (10, 8, 2, 'Producto', '~/Almacen/Productos.aspx', '', 0, GETDATE(), 0, GETDATE(), 1)
INSERT INTO Opcion(OpcionId, OpcionPadreId, Orden, Nombre, Url, Imagen, UsuarioCreacionId, FechaCreacion, UsuarioModificacion, FechaModificacion, Activo)
VALUES (11, 8, 3, 'Tipo Producto', '~/Maestros/TipoProducto.aspx', '', 0, GETDATE(), 0, GETDATE(), 1)
INSERT INTO Opcion(OpcionId, OpcionPadreId, Orden, Nombre, Url, Imagen, UsuarioCreacionId, FechaCreacion, UsuarioModificacion, FechaModificacion, Activo)
VALUES (12, 8, 4, 'SubTipo Producto', '~/Maestros/SubTipoProducto.aspx', '', 0, GETDATE(), 0, GETDATE(), 1)
INSERT INTO Opcion(OpcionId, OpcionPadreId, Orden, Nombre, Url, Imagen, UsuarioCreacionId, FechaCreacion, UsuarioModificacion, FechaModificacion, Activo)
VALUES (24, 8, 5, 'Consulta Stock', '~/Almacen/ConsultaStocks.aspx', '', 0, GETDATE(), 0, GETDATE(), 1)
INSERT INTO Opcion(OpcionId, OpcionPadreId, Orden, Nombre, Url, Imagen, UsuarioCreacionId, FechaCreacion, UsuarioModificacion, FechaModificacion, Activo)
VALUES (37, 8, 6, 'Ingreso Almacen', '~/Almacen/IngresoAlmacen.aspx', '', 0, GETDATE(), 0, GETDATE(), 1)
-- Mantenimientos
INSERT INTO Opcion(OpcionId, OpcionPadreId, Orden, Nombre, Url, Imagen, UsuarioCreacionId, FechaCreacion, UsuarioModificacion, FechaModificacion, Activo)
VALUES(13, 0, 4, 'Mantenimientos', '#', '', 0, SYSDATETIME(), 0, SYSDATETIME(), 1)
INSERT INTO Opcion(OpcionId, OpcionPadreId, Orden, Nombre, Url, Imagen, UsuarioCreacionId, FechaCreacion, UsuarioModificacion, FechaModificacion, Activo)
VALUES(14, 13, 1, 'Empresa', '~/Configuracion/Empresa.aspx', '', 0, SYSDATETIME(), 0, SYSDATETIME(), 1)
INSERT INTO Opcion(OpcionId, OpcionPadreId, Orden, Nombre, Url, Imagen, UsuarioCreacionId, FechaCreacion, UsuarioModificacion, FechaModificacion, Activo)
VALUES(15, 13, 2, 'Agencia', '~/Configuracion/Agencia.aspx', '', 0, SYSDATETIME(), 0, SYSDATETIME(), 1)
INSERT INTO Opcion(OpcionId, OpcionPadreId, Orden ,Nombre, Url, Imagen, UsuarioCreacionId, FechaCreacion, UsuarioModificacion, FechaModificacion, Activo)
VALUES (16, 13, 2, 'Correlativo', '~/Parametros/Correlativo.aspx', '', 0, GETDATE(), 0, GETDATE(), 1)
INSERT INTO Opcion(OpcionId, OpcionPadreId, Orden ,Nombre, Url, Imagen, UsuarioCreacionId, FechaCreacion, UsuarioModificacion, FechaModificacion, Activo)
VALUES (17, 13, 3, 'Tipo Cambio', '~/Configuracion/TipoCambio.aspx', '', 0, GETDATE(), 0, GETDATE(), 1)
INSERT INTO Opcion(OpcionId, OpcionPadreId, Orden ,Nombre, Url, Imagen, UsuarioCreacionId, FechaCreacion, UsuarioModificacion, FechaModificacion, Activo)
VALUES (18, 13, 4, 'Parámetros', '~/Configuracion/Parametro.aspx', '', 0, GETDATE(), 0, GETDATE(), 1)
INSERT INTO Opcion(OpcionId, OpcionPadreId, Orden ,Nombre, Url, Imagen, UsuarioCreacionId, FechaCreacion, UsuarioModificacion, FechaModificacion, Activo)
VALUES (19, 13, 5, 'Turnos', '~/Configuracion/Turno.aspx', '', 0, GETDATE(), 0, GETDATE(), 1)
-- Seguridad
INSERT INTO Opcion(OpcionId, OpcionPadreId, Orden, Nombre, Url, Imagen, UsuarioCreacionId, FechaCreacion, UsuarioModificacion, FechaModificacion, Activo)
VALUES(20, 0, 5, 'Seguridad', '#', '', 0, SYSDATETIME(), 0, SYSDATETIME(), 1)
INSERT INTO Opcion(OpcionId, OpcionPadreId, Orden, Nombre, Url, Imagen, UsuarioCreacionId, FechaCreacion, UsuarioModificacion, FechaModificacion, Activo)
VALUES(21, 20, 1, 'Usuario', '~/Seguridad/Usuario.aspx', '', 0, SYSDATETIME(), 0, SYSDATETIME(), 1)
INSERT INTO Opcion(OpcionId, OpcionPadreId, Orden, Nombre, Url, Imagen, UsuarioCreacionId, FechaCreacion, UsuarioModificacion, FechaModificacion, Activo)
VALUES(22, 20, 2, 'Perfil', '~/Seguridad/Perfil.aspx', '', 0, SYSDATETIME(), 0, SYSDATETIME(), 1)
INSERT INTO Opcion(OpcionId, OpcionPadreId, Orden, Nombre, Url, Imagen, UsuarioCreacionId, FechaCreacion, UsuarioModificacion, FechaModificacion, Activo)
VALUES(23, 20, 3, 'Opción x Perfil', '~/Seguridad/OpcionPorPerfil.aspx', '', 0, SYSDATETIME(), 0, SYSDATETIME(), 1)
-- Market
INSERT INTO Opcion(OpcionId, OpcionPadreId, Orden, Nombre, Url, Imagen, UsuarioCreacionId, FechaCreacion, UsuarioModificacion, FechaModificacion, Activo)
VALUES(25, 0, 6, 'Market', '#', '', 0, SYSDATETIME(), 0, SYSDATETIME(), 1)
INSERT INTO Opcion(OpcionId, OpcionPadreId, Orden, Nombre, Url, Imagen, UsuarioCreacionId, FechaCreacion, UsuarioModificacion, FechaModificacion, Activo)
VALUES(26, 25, 1, 'Producto', '~/Almacen/Productos.aspx?TipoTiendaId=2', '', 0, SYSDATETIME(), 0, SYSDATETIME(), 1)
INSERT INTO Opcion(OpcionId, OpcionPadreId, Orden, Nombre, Url, Imagen, UsuarioCreacionId, FechaCreacion, UsuarioModificacion, FechaModificacion, Activo)
VALUES(27, 25, 2, 'Venta de Mercadería', '~/Operaciones/Comprobante.aspx?TipoTiendaId=2', '', 0, SYSDATETIME(), 0, SYSDATETIME(), 1)
INSERT INTO Opcion(OpcionId, OpcionPadreId, Orden ,Nombre, Url, Imagen, UsuarioCreacionId, FechaCreacion, UsuarioModificacion, FechaModificacion, Activo)
VALUES(28, 25, 3, 'Ventas Realizadas', '~/Operaciones/Venta.aspx?TipoTiendaId=2', '', 0, GETDATE(), 0, GETDATE(), 1)
INSERT INTO Opcion(OpcionId, OpcionPadreId, Orden, Nombre, Url, Imagen, UsuarioCreacionId, FechaCreacion, UsuarioModificacion, FechaModificacion, Activo)
VALUES(29, 25, 4, 'Proveedor', '~/Maestros/Proveedor.aspx?TipoTiendaId=2', '', 0, GETDATE(), 0, GETDATE(), 1)
INSERT INTO Opcion(OpcionId, OpcionPadreId, Orden, Nombre, Url, Imagen, UsuarioCreacionId, FechaCreacion, UsuarioModificacion, FechaModificacion, Activo)
VALUES(30, 25, 5, 'Compra Mercadería', '~/Operaciones/Compra.aspx?TipoTiendaId=2', '', 0, GETDATE(), 0, GETDATE(), 1)
-- Canastilla
INSERT INTO Opcion(OpcionId, OpcionPadreId, Orden, Nombre, Url, Imagen, UsuarioCreacionId, FechaCreacion, UsuarioModificacion, FechaModificacion, Activo)
VALUES(31, 0, 6, 'Canastilla', '#', '', 0, SYSDATETIME(), 0, SYSDATETIME(), 1)
INSERT INTO Opcion(OpcionId, OpcionPadreId, Orden, Nombre, Url, Imagen, UsuarioCreacionId, FechaCreacion, UsuarioModificacion, FechaModificacion, Activo)
VALUES(32, 31, 1, 'Producto', '~/Almacen/Productos.aspx?TipoTiendaId=3', '', 0, SYSDATETIME(), 0, SYSDATETIME(), 1)
INSERT INTO Opcion(OpcionId, OpcionPadreId, Orden, Nombre, Url, Imagen, UsuarioCreacionId, FechaCreacion, UsuarioModificacion, FechaModificacion, Activo)
VALUES(33, 31, 2, 'Venta de Mercadería', '~/Operaciones/Comprobante.aspx?TipoTiendaId=3', '', 0, SYSDATETIME(), 0, SYSDATETIME(), 1)
INSERT INTO Opcion(OpcionId, OpcionPadreId, Orden ,Nombre, Url, Imagen, UsuarioCreacionId, FechaCreacion, UsuarioModificacion, FechaModificacion, Activo)
VALUES(34, 31, 3, 'Ventas Realizadas', '~/Operaciones/Venta.aspx?TipoTiendaId=3', '', 0, GETDATE(), 0, GETDATE(), 1)
INSERT INTO Opcion(OpcionId, OpcionPadreId, Orden, Nombre, Url, Imagen, UsuarioCreacionId, FechaCreacion, UsuarioModificacion, FechaModificacion, Activo)
VALUES(35, 31, 4, 'Proveedor', '~/Maestros/Proveedor.aspx?TipoTiendaId=3', '', 0, GETDATE(), 0, GETDATE(), 1)
INSERT INTO Opcion(OpcionId, OpcionPadreId, Orden, Nombre, Url, Imagen, UsuarioCreacionId, FechaCreacion, UsuarioModificacion, FechaModificacion, Activo)
VALUES(36, 31, 5, 'Compra Mercadería', '~/Operaciones/Compra.aspx?TipoTiendaId=3', '', 0, GETDATE(), 0, GETDATE(), 1)
-- Petroamerica
INSERT INTO Opcion(OpcionId, OpcionPadreId, Orden, Nombre, Url, Imagen, UsuarioCreacionId, FechaCreacion, UsuarioModificacion, FechaModificacion, Activo)
VALUES(38, 0, 7, 'Petroamerica', '#', '', 0, SYSDATETIME(), 0, SYSDATETIME(), 1)
INSERT INTO Opcion(OpcionId, OpcionPadreId, Orden, Nombre, Url, Imagen, UsuarioCreacionId, FechaCreacion, UsuarioModificacion, FechaModificacion, Activo)
VALUES(39, 38, 1, 'Ventas', '~/Petroamerica/Venta.aspx', '', 0, SYSDATETIME(), 0, SYSDATETIME(), 1)
-- Gasolutions
INSERT INTO Opcion(OpcionId, OpcionPadreId, Orden, Nombre, Url, Imagen, UsuarioCreacionId, FechaCreacion, UsuarioModificacion, FechaModificacion, Activo)
VALUES(40, 0, 7, 'Gasolutions', '#', '', 0, SYSDATETIME(), 0, SYSDATETIME(), 1)
INSERT INTO Opcion(OpcionId, OpcionPadreId, Orden, Nombre, Url, Imagen, UsuarioCreacionId, FechaCreacion, UsuarioModificacion, FechaModificacion, Activo)
VALUES(41, 40, 1, 'Ventas', '~/Gasolutions/Venta.aspx', '', 0, SYSDATETIME(), 0, SYSDATETIME(), 1)
GO

TRUNCATE TABLE PerfilOpcion
GO
INSERT INTO PerfilOpcion(PerfilId, OpcionId) VALUES (1, 1)
INSERT INTO PerfilOpcion(PerfilId, OpcionId) VALUES (1, 2)
INSERT INTO PerfilOpcion(PerfilId, OpcionId) VALUES (1, 3)
INSERT INTO PerfilOpcion(PerfilId, OpcionId) VALUES (1, 4)
INSERT INTO PerfilOpcion(PerfilId, OpcionId) VALUES (1, 5)
INSERT INTO PerfilOpcion(PerfilId, OpcionId) VALUES (1, 6)
INSERT INTO PerfilOpcion(PerfilId, OpcionId) VALUES (1, 7)
INSERT INTO PerfilOpcion(PerfilId, OpcionId) VALUES (1, 8)
INSERT INTO PerfilOpcion(PerfilId, OpcionId) VALUES (1, 9)
INSERT INTO PerfilOpcion(PerfilId, OpcionId) VALUES (1, 10)
INSERT INTO PerfilOpcion(PerfilId, OpcionId) VALUES (1, 11)
INSERT INTO PerfilOpcion(PerfilId, OpcionId) VALUES (1, 12)
INSERT INTO PerfilOpcion(PerfilId, OpcionId) VALUES (1, 13)
INSERT INTO PerfilOpcion(PerfilId, OpcionId) VALUES (1, 14)
INSERT INTO PerfilOpcion(PerfilId, OpcionId) VALUES (1, 15)
INSERT INTO PerfilOpcion(PerfilId, OpcionId) VALUES (1, 16)
INSERT INTO PerfilOpcion(PerfilId, OpcionId) VALUES (1, 17)
INSERT INTO PerfilOpcion(PerfilId, OpcionId) VALUES (1, 18)
INSERT INTO PerfilOpcion(PerfilId, OpcionId) VALUES (1, 19)
INSERT INTO PerfilOpcion(PerfilId, OpcionId) VALUES (1, 20)
INSERT INTO PerfilOpcion(PerfilId, OpcionId) VALUES (1, 21)
INSERT INTO PerfilOpcion(PerfilId, OpcionId) VALUES (1, 22)
INSERT INTO PerfilOpcion(PerfilId, OpcionId) VALUES (1, 23)
INSERT INTO PerfilOpcion(PerfilId, OpcionId) VALUES (1, 24)
INSERT INTO PerfilOpcion(PerfilId, OpcionId) VALUES (1, 25)
INSERT INTO PerfilOpcion(PerfilId, OpcionId) VALUES (1, 26)
INSERT INTO PerfilOpcion(PerfilId, OpcionId) VALUES (1, 27)
INSERT INTO PerfilOpcion(PerfilId, OpcionId) VALUES (1, 28)
INSERT INTO PerfilOpcion(PerfilId, OpcionId) VALUES (1, 29)
INSERT INTO PerfilOpcion(PerfilId, OpcionId) VALUES (1, 30)
INSERT INTO PerfilOpcion(PerfilId, OpcionId) VALUES (1, 31)
INSERT INTO PerfilOpcion(PerfilId, OpcionId) VALUES (1, 32)
INSERT INTO PerfilOpcion(PerfilId, OpcionId) VALUES (1, 33)
INSERT INTO PerfilOpcion(PerfilId, OpcionId) VALUES (1, 34)
INSERT INTO PerfilOpcion(PerfilId, OpcionId) VALUES (1, 35)
INSERT INTO PerfilOpcion(PerfilId, OpcionId) VALUES (1, 36)
INSERT INTO PerfilOpcion(PerfilId, OpcionId) VALUES (1, 37)
-- Petroamerica
INSERT INTO PerfilOpcion(PerfilId, OpcionId) VALUES (1, 38)
INSERT INTO PerfilOpcion(PerfilId, OpcionId) VALUES (1, 39)
-- Gasolutions
INSERT INTO PerfilOpcion(PerfilId, OpcionId) VALUES (1, 40)
INSERT INTO PerfilOpcion(PerfilId, OpcionId) VALUES (1, 41)
GO