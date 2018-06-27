using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using APU.Entidad;
using APU.Herramientas;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace APU.AccesoDatos
{
	public class CompraAccesoDatos
	{
		public List<CompraInfo> ListarPaginado(int compraId, int proveedorId, int tipoNegocioId, int tamanioPagina, int numeroPagina)
		{
			var clienteListaInfo = new List<CompraInfo>();
			using (var oConexion = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
			{
				var sqlComando = new SqlCommand();
				sqlComando.Connection = oConexion;
				sqlComando.CommandText = "ObtenerComprasPaginado";
				sqlComando.CommandType = CommandType.StoredProcedure;
				sqlComando.Parameters.Add("CompraId", SqlDbType.Int).Value = compraId;
				sqlComando.Parameters.Add("ProveedorId", SqlDbType.Int).Value = proveedorId;
			    sqlComando.Parameters.Add("TipoNegocioId", SqlDbType.Int).Value = tipoNegocioId;
                sqlComando.Parameters.Add("TamanioPagina", SqlDbType.Int).Value = tamanioPagina;
				sqlComando.Parameters.Add("NumeroPagina", SqlDbType.Int).Value = numeroPagina;

				oConexion.Open();

				using (SqlDataReader dr = sqlComando.ExecuteReader())
				{
					while (dr.Read())
					{
						clienteListaInfo.Add(CargarCompraInfoPag(dr));
					}
				}
				oConexion.Close();
			}
			return clienteListaInfo;
		}

		private static CompraInfo CargarCompraInfoPag(IDataReader dr)
		{
			int colCount = dr.FieldCount;
			var values = new object[colCount];

			#region Indices
			int indCompraId = dr.GetOrdinal("CompraId");
			int indProveedorId = dr.GetOrdinal("ProveedorId");
			int indProveedor = dr.GetOrdinal("Proveedor");
			int indGlosa = dr.GetOrdinal("Glosa");
			int indNumeroComprobante = dr.GetOrdinal("NumeroComprobante");
			int indNumeroSerie = dr.GetOrdinal("NumeroSerie");
			int indTipoDocumentoId = dr.GetOrdinal("TipoDocumentoId");
			int indTipoDocumento = dr.GetOrdinal("TipoDocumento");
			int indMoneda = dr.GetOrdinal("Moneda");
			int indMonedaId = dr.GetOrdinal("MonedaId");
			int indFechaEmision = dr.GetOrdinal("FechaEmision");
			int indSubTotal = dr.GetOrdinal("SubTotal");
			int indIgv = dr.GetOrdinal("Igv");
			int indTotal = dr.GetOrdinal("Total");
			int indMotivoIngresoId = dr.GetOrdinal("MotivoIngresoId");
			int indTipoNegocioId = dr.GetOrdinal("TipoNegocioId");
			int indEstadoComprobanteId = dr.GetOrdinal("EstadoComprobanteId");
			int indEstadoComprobante = dr.GetOrdinal("EstadoComprobante");
			int indNumeroFila = dr.GetOrdinal("NumeroFila");
			int indTotalFilas = dr.GetOrdinal("TotalFilas");
			#endregion

			var compraInfo = new CompraInfo();
			dr.GetValues(values);

			#region Campos
			compraInfo.CompraId = Convert.ToInt32(values[indCompraId]);
			compraInfo.ProveedorId = Convert.ToInt32(values[indProveedorId]);
			compraInfo.Proveedor = Convert.ToString(values[indProveedor]);
			compraInfo.NumeroComprobante = Convert.ToString(values[indNumeroComprobante]);
			compraInfo.NumeroSerie = Convert.ToString(values[indNumeroSerie]);
			compraInfo.TipoDocumento = Convert.ToString(values[indTipoDocumento]);
			compraInfo.Moneda = Convert.ToString(values[indMoneda]);
			compraInfo.FechaEmision = Convert.ToDateTime(values[indFechaEmision]);
			compraInfo.EstadoComprobante = Convert.ToString(values[indEstadoComprobante]);

			if (values[indSubTotal] != DBNull.Value) compraInfo.SubTotal = Convert.ToDecimal(values[indSubTotal]);
			if (values[indIgv] != DBNull.Value) compraInfo.Igv = Convert.ToDecimal(values[indIgv]);
			if (values[indTotal] != DBNull.Value) compraInfo.Total = Convert.ToDecimal(values[indTotal]);
			compraInfo.MonedaId = Convert.ToInt32(values[indMonedaId]);
			compraInfo.TipoDocumentoId = Convert.ToInt32(values[indTipoDocumentoId]);
			if (values[indEstadoComprobanteId] != DBNull.Value) compraInfo.EstadoComprobanteId = Convert.ToInt32(values[indEstadoComprobanteId]);
			if (values[indGlosa] != DBNull.Value) compraInfo.Glosa = Convert.ToString(values[indGlosa]);
			if (values[indMotivoIngresoId] != DBNull.Value) compraInfo.MotivoIngresoId = Convert.ToInt32(values[indMotivoIngresoId]);
		    compraInfo.TipoNegocioId = Convert.ToInt32(values[indTipoNegocioId]);
            compraInfo.NumeroFila = Convert.ToInt32(values[indNumeroFila]);
			compraInfo.TotalFilas = Convert.ToInt32(values[indTotalFilas]);
			#endregion

			return compraInfo;
		}

		public List<ComprasDetalleInfo> ListarComprasDetalle(int compraDetalleId, int compraId)
		{
			var clienteListaInfo = new List<ComprasDetalleInfo>();
			using (var oConexion = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
			{
				var sqlComando = new SqlCommand();
				sqlComando.Connection = oConexion;
				sqlComando.CommandText = "ObtenerCompraDetalle";
				sqlComando.CommandType = CommandType.StoredProcedure;
				sqlComando.Parameters.Add("CompraId", SqlDbType.Int).Value = compraId;
				sqlComando.Parameters.Add("CompraDetalleId", SqlDbType.Int).Value = compraDetalleId;				

				oConexion.Open();

				using (SqlDataReader dr = sqlComando.ExecuteReader())
				{
					while (dr.Read())
					{
						clienteListaInfo.Add(CargarDetalleCompra(dr));
					}
				}
				oConexion.Close();
			}
			return clienteListaInfo;
		}

		private static ComprasDetalleInfo CargarDetalleCompra(IDataReader dr)
		{
			int colCount = dr.FieldCount;
			var values = new object[colCount];

			#region Indices
			int indCompraId = dr.GetOrdinal("CompraId");
			int indComprasDetalleId = dr.GetOrdinal("ComprasDetalleId");
			int indProductoId = dr.GetOrdinal("ProductoId");
			int indProducto = dr.GetOrdinal("Producto");
			int indCodigo= dr.GetOrdinal("Codigo");
			int indCantidad = dr.GetOrdinal("Cantidad");
			int indPrecioUnitario = dr.GetOrdinal("PrecioUnitario");
			int indUnidadMedidaId = dr.GetOrdinal("UnidadMedidaId");
			int indUnidadMedida = dr.GetOrdinal("UnidadMedida");
			int indSubTotal = dr.GetOrdinal("SubTotal");
			int indIgv= dr.GetOrdinal("Igv");
			int indTotal = dr.GetOrdinal("Total");
			int indAlmacenId = dr.GetOrdinal("AlmacenId");
			//int indFechaModificacion = dr.GetOrdinal("FechaModificacion");
			//int indNumeroFila = dr.GetOrdinal("NumeroFila");
			//int indTotalFilas = dr.GetOrdinal("TotalFilas");
			#endregion

			var tipoProductoInfo = new ComprasDetalleInfo();
			dr.GetValues(values);

			#region Campos
			tipoProductoInfo.CompraId = Convert.ToInt32(values[indCompraId]);
			tipoProductoInfo.ComprasDetalleId = Convert.ToInt32(values[indComprasDetalleId]);
			tipoProductoInfo.ProductoId = Convert.ToInt32(values[indProductoId]);
			tipoProductoInfo.Producto = Convert.ToString(values[indProducto]);
			tipoProductoInfo.Codigo = Convert.ToString(values[indCodigo]);
			tipoProductoInfo.Cantidad = Convert.ToDecimal(values[indCantidad]);
			tipoProductoInfo.PrecioUnitario = Convert.ToDecimal(values[indPrecioUnitario]);
			tipoProductoInfo.UnidadMedidaId = Convert.ToInt32(values[indUnidadMedidaId]);
			tipoProductoInfo.UnidadMedida = Convert.ToString(values[indUnidadMedida]);
			tipoProductoInfo.SubTotal = Convert.ToDecimal(values[indSubTotal]);
			tipoProductoInfo.Igv = Convert.ToDecimal(values[indIgv]);
			tipoProductoInfo.Total = Convert.ToDecimal(values[indTotal]);
			tipoProductoInfo.AlmacenId = Convert.ToInt32(values[indAlmacenId]);

			//tipoProductoInfo.Activo = Convert.ToInt32(values[indActivo]);
			//tipoProductoInfo.UsuarioCreacionId = Convert.ToInt32(values[indUsuarioCreacionId]);
			//tipoProductoInfo.FechaCreacion = Convert.ToDateTime(values[indFechaCreacion]);
			//tipoProductoInfo.UsuarioModificacionId = Convert.ToInt32(values[indUsuarioModificacionId]);
			//tipoProductoInfo.FechaModificacion = Convert.ToDateTime(values[indFechaModificacion]);
			//tipoProductoInfo.NumeroFila = Convert.ToInt32(values[indNumeroFila]);
			//tipoProductoInfo.TotalFilas = Convert.ToInt32(values[indTotalFilas]);
			#endregion

			return tipoProductoInfo;
		}

		public int InsertarCompraDetalle(ComprasDetalleInfo ventaInfo)
		{
			int resultado;
			try
			{
				using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
				{
					var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "InsertarComprasDetalle", CommandType = CommandType.StoredProcedure };

					//sqlComando.Parameters.Add("ComprasDetalleId", SqlDbType.Int).Value = ventaInfo.ComprasDetalleId;
					sqlComando.Parameters.Add("CompraId", SqlDbType.Int).Value = ventaInfo.CompraId;
					sqlComando.Parameters.Add("ProductoId", SqlDbType.Int).Value = ventaInfo.ProductoId;
					sqlComando.Parameters.Add("Cantidad", SqlDbType.Decimal).Value = ventaInfo.Cantidad;
					sqlComando.Parameters.Add("PrecioUnitario", SqlDbType.Decimal).Value = ventaInfo.PrecioUnitario;

					sqlComando.Parameters.Add("SubTotal", SqlDbType.Decimal).Value = ventaInfo.SubTotal;
					sqlComando.Parameters.Add("Igv", SqlDbType.Decimal).Value = ventaInfo.Igv;
					sqlComando.Parameters.Add("Total", SqlDbType.Decimal).Value = ventaInfo.Total;

					sqlComando.Parameters.Add("AlmacenId", SqlDbType.Int).Value = ventaInfo.AlmacenId;
					//sqlComando.Parameters.Add("Activo", SqlDbType.Int).Value = ventaInfo.Activo;
					sqlComando.Parameters.Add("UsuarioCreacionId", SqlDbType.Int).Value = ventaInfo.UsuarioCreacionId;

					oConnection.Open();
					resultado = Convert.ToInt32(sqlComando.ExecuteScalar());

					oConnection.Close();
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return resultado;
		}
		public int ActualizarCompraDetalle(ComprasDetalleInfo compraDetalleInfo)
		{
			int resultado = 0;
			try
			{
				using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
				{
					var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "ActualizarComprasDetalle", CommandType = CommandType.StoredProcedure };

					sqlComando.Parameters.Add("ComprasDetalleId", SqlDbType.Int).Value = compraDetalleInfo.ComprasDetalleId;
					sqlComando.Parameters.Add("CompraId", SqlDbType.Int).Value = compraDetalleInfo.CompraId;
					sqlComando.Parameters.Add("ProductoId", SqlDbType.Int).Value = compraDetalleInfo.ProductoId;
					sqlComando.Parameters.Add("Cantidad", SqlDbType.Decimal).Value = compraDetalleInfo.Cantidad;
					sqlComando.Parameters.Add("PrecioUnitario", SqlDbType.Decimal).Value = compraDetalleInfo.PrecioUnitario;

					sqlComando.Parameters.Add("SubTotal", SqlDbType.Decimal).Value = compraDetalleInfo.SubTotal;
					sqlComando.Parameters.Add("Igv", SqlDbType.Decimal).Value = compraDetalleInfo.Igv;
					sqlComando.Parameters.Add("Total", SqlDbType.Decimal).Value = compraDetalleInfo.Total;
					sqlComando.Parameters.Add("UsuarioModificacionId", SqlDbType.Int).Value = compraDetalleInfo.UsuarioModificacionId;

					oConnection.Open();
					resultado = Convert.ToInt32(sqlComando.ExecuteScalar());

					oConnection.Close();
				}
			}
			catch (Exception ex)
			{
				bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaAccesoDatos);
				if (rethrow)
					throw ex;
			}
			return resultado;
		}
		public int EliminarCompraDetalle(int ventaId)
		{
			int resultado = 0;
			try
			{
				using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
				{
					var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "EliminarComprasDetalle", CommandType = CommandType.StoredProcedure };

					sqlComando.Parameters.Add("ComprasDetalleId", SqlDbType.Int).Value = ventaId;

					oConnection.Open();
					resultado = sqlComando.ExecuteNonQuery();

					oConnection.Close();
				}
			}
			catch (Exception ex)
			{
				bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaAccesoDatos);
				if (rethrow)
					throw;
			}
			return resultado;
		}

		public int InsertarCompra(CompraInfo compraInfo)
		{
			int resultado;
			try
			{
				using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
				{
					var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "InsertarCompras", CommandType = CommandType.StoredProcedure };
					
					sqlComando.Parameters.Add("ProveedorId", SqlDbType.Int).Value = compraInfo.ProveedorId;
					sqlComando.Parameters.Add("NumeroComprobante", SqlDbType.VarChar).Value = compraInfo.NumeroComprobante;
					sqlComando.Parameters.Add("NumeroSerie", SqlDbType.VarChar).Value = compraInfo.NumeroSerie;
					sqlComando.Parameters.Add("MonedaId", SqlDbType.Int).Value = compraInfo.MonedaId;
					sqlComando.Parameters.Add("TipoDocumentoId", SqlDbType.Int).Value = compraInfo.TipoDocumentoId;
					sqlComando.Parameters.Add("FechaEmision", SqlDbType.DateTime).Value = compraInfo.FechaEmision.Date;
					sqlComando.Parameters.Add("EstadoComprobanteId", SqlDbType.Int).Value = compraInfo.EstadoComprobanteId;
					sqlComando.Parameters.Add("Glosa", SqlDbType.VarChar).Value = compraInfo.Glosa;
					sqlComando.Parameters.Add("MontoCompra", SqlDbType.Decimal).Value = compraInfo.SubTotal;
					sqlComando.Parameters.Add("MontoIgv", SqlDbType.Decimal).Value = compraInfo.Igv;
					sqlComando.Parameters.Add("MontoTotal", SqlDbType.Decimal).Value = compraInfo.Total;
					sqlComando.Parameters.Add("MotivoingresoId", SqlDbType.Int).Value = compraInfo.MotivoIngresoId;
				    sqlComando.Parameters.Add("TipoNegocioId", SqlDbType.Int).Value = compraInfo.TipoNegocioId;
                    sqlComando.Parameters.Add("UsuarioCreacionId", SqlDbType.Int).Value = compraInfo.UsuarioCreacionId;

					oConnection.Open();
					resultado = Convert.ToInt32(sqlComando.ExecuteScalar());

					oConnection.Close();
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return resultado;
		}

		public int ActualizarCompra(CompraInfo compraInfo)
		{
			int resultado;
			try
			{
				using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
				{
					var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "ActualizarCompras", CommandType = CommandType.StoredProcedure };
					
					//sqlComando.Parameters.Add("ComprasDetalleId", SqlDbType.Int).Value = ventaInfo.ComprasDetalleId;
					sqlComando.Parameters.Add("CompraId", SqlDbType.Int).Value = compraInfo.CompraId;
					sqlComando.Parameters.Add("ProveedorId", SqlDbType.Int).Value = compraInfo.ProveedorId;
					sqlComando.Parameters.Add("NumeroComprobante", SqlDbType.VarChar).Value = compraInfo.NumeroComprobante;
					sqlComando.Parameters.Add("NumeroSerie", SqlDbType.VarChar).Value = compraInfo.NumeroSerie;
					sqlComando.Parameters.Add("MonedaId", SqlDbType.Int).Value = compraInfo.MonedaId;
					sqlComando.Parameters.Add("TipoDocumentoId", SqlDbType.Int).Value = compraInfo.TipoDocumentoId;
					//sqlComando.Parameters.Add("FormaPagoId", SqlDbType.Int).Value = ventaInfo.FormaPagoId;
					sqlComando.Parameters.Add("FechaEmision", SqlDbType.DateTime).Value = compraInfo.FechaEmision.Date;
					//sqlComando.Parameters.Add("FechaVencimiento", SqlDbType.DateTime).Value = ventaInfo.FechaVencimiento.Date;
					//sqlComando.Parameters.Add("FechaPago", SqlDbType.DateTime).Value = ventaInfo.FechaPago == DateTime.MinValue ? (object)System.DBNull.Value : ventaInfo.FechaPago.Date;
					sqlComando.Parameters.Add("EstadoComprobanteId", SqlDbType.Int).Value = compraInfo.EstadoComprobanteId;
					sqlComando.Parameters.Add("Glosa", SqlDbType.VarChar).Value = compraInfo.Glosa;
					sqlComando.Parameters.Add("MontoCompra", SqlDbType.Decimal).Value = compraInfo.SubTotal;
					sqlComando.Parameters.Add("MontoIgv", SqlDbType.Decimal).Value = compraInfo.Igv;
					sqlComando.Parameters.Add("MontoTotal", SqlDbType.Decimal).Value = compraInfo.Total;
					sqlComando.Parameters.Add("MotivoIngresoId", SqlDbType.Int).Value = compraInfo.MotivoIngresoId;
					sqlComando.Parameters.Add("UsuarioModificacionId", SqlDbType.Int).Value = compraInfo.UsuarioModificacionId;

					oConnection.Open();
					resultado = Convert.ToInt32(sqlComando.ExecuteScalar());

					oConnection.Close();
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return resultado;
		}

		public List<ComprasDetalleInfo> ListarComprasDetalleAlmacen(string fechaInicio, string fechaFin, int tamanioPagina, int numeroPagina)
		{
			var clienteListaInfo = new List<ComprasDetalleInfo>();
			using (var oConexion = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
			{
				var sqlComando = new SqlCommand();
				sqlComando.Connection = oConexion;
				sqlComando.CommandText = "ObtenerCompraDetalleAlmacen";
				sqlComando.CommandType = CommandType.StoredProcedure;
				sqlComando.Parameters.Add("FechaInicio", SqlDbType.VarChar).Value = fechaInicio;
				sqlComando.Parameters.Add("FechaFin", SqlDbType.VarChar).Value = fechaFin;
				sqlComando.Parameters.Add("TamanioPagina", SqlDbType.Int).Value = tamanioPagina;
				sqlComando.Parameters.Add("NumeroPagina", SqlDbType.Int).Value = numeroPagina;

				oConexion.Open();

				using (SqlDataReader dr = sqlComando.ExecuteReader())
				{
					while (dr.Read())
					{
						clienteListaInfo.Add(CargarDetalleCompraAlmacen(dr));
					}
				}
				oConexion.Close();
			}
			return clienteListaInfo;
		}
		private static ComprasDetalleInfo CargarDetalleCompraAlmacen(IDataReader dr)
		{
			int colCount = dr.FieldCount;
			var values = new object[colCount];

			#region Indices
			int indCompraId = dr.GetOrdinal("CompraId");
			int indComprasDetalleId = dr.GetOrdinal("ComprasDetalleId");
			int indProductoId = dr.GetOrdinal("ProductoId");
			int indProducto = dr.GetOrdinal("Producto");
			int indCodigo = dr.GetOrdinal("Codigo");
			int indCantidad = dr.GetOrdinal("Cantidad");
			int indPrecioUnitario = dr.GetOrdinal("PrecioUnitario");
			int indUnidadMedidaId = dr.GetOrdinal("UnidadMedidaId");
			int indUnidadMedida = dr.GetOrdinal("UnidadMedida");
			int indSubTotal = dr.GetOrdinal("SubTotal");
			int indIgv = dr.GetOrdinal("Igv");
			int indTotal = dr.GetOrdinal("Total");

			int indNumeroSerie = dr.GetOrdinal("NumeroSerie");
			int indNumeroComprobante = dr.GetOrdinal("NumeroComprobante");
			int indTipoDocumento = dr.GetOrdinal("TipoDocumento");
			//int indUsuarioModificacionId = dr.GetOrdinal("UsuarioModificacionId");
			//int indFechaModificacion = dr.GetOrdinal("FechaModificacion");
			int indNumeroFila = dr.GetOrdinal("NumeroFila");
			int indTotalFilas = dr.GetOrdinal("TotalFilas");
			#endregion

			var tipoProductoInfo = new ComprasDetalleInfo();
			dr.GetValues(values);

			#region Campos
			tipoProductoInfo.CompraId = Convert.ToInt32(values[indCompraId]);
			tipoProductoInfo.ComprasDetalleId = Convert.ToInt32(values[indComprasDetalleId]);
			tipoProductoInfo.ProductoId = Convert.ToInt32(values[indProductoId]);
			tipoProductoInfo.Producto = Convert.ToString(values[indProducto]);
			tipoProductoInfo.Codigo = Convert.ToString(values[indCodigo]);
			tipoProductoInfo.Cantidad = Convert.ToDecimal(values[indCantidad]);
			tipoProductoInfo.PrecioUnitario = Convert.ToDecimal(values[indPrecioUnitario]);
			tipoProductoInfo.UnidadMedidaId = Convert.ToInt32(values[indUnidadMedidaId]);
			tipoProductoInfo.UnidadMedida = Convert.ToString(values[indUnidadMedida]);
			tipoProductoInfo.SubTotal = Convert.ToDecimal(values[indSubTotal]);
			tipoProductoInfo.Igv = Convert.ToDecimal(values[indIgv]);
			tipoProductoInfo.Total = Convert.ToDecimal(values[indTotal]);

			tipoProductoInfo.NumeroComprobante = Convert.ToString(values[indNumeroComprobante]);
			tipoProductoInfo.NumeroSerie = Convert.ToString(values[indNumeroSerie]);
			tipoProductoInfo.TipoDocumento = Convert.ToString(values[indTipoDocumento]);

			//tipoProductoInfo.Activo = Convert.ToInt32(values[indActivo]);
			//tipoProductoInfo.UsuarioCreacionId = Convert.ToInt32(values[indUsuarioCreacionId]);
			//tipoProductoInfo.FechaCreacion = Convert.ToDateTime(values[indFechaCreacion]);
			//tipoProductoInfo.UsuarioModificacionId = Convert.ToInt32(values[indUsuarioModificacionId]);
			//tipoProductoInfo.FechaModificacion = Convert.ToDateTime(values[indFechaModificacion]);
			tipoProductoInfo.NumeroFila = Convert.ToInt32(values[indNumeroFila]);
			tipoProductoInfo.TotalFilas = Convert.ToInt32(values[indTotalFilas]);
			#endregion

			return tipoProductoInfo;
		}
	}
}
