using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using APU.Entidad;
using APU.Herramientas;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace APU.AccesoDatos
{
	public class ProductoAccesoDatos
	{
		public List<ProductoInfo> ListarPaginado(int productoId, string codigo, string producto, int tamanioPagina, int numeroPagina)
		{
			var clienteListaInfo = new List<ProductoInfo>();
			using (var oConexion = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
			{
				var sqlComando = new SqlCommand();
				sqlComando.Connection = oConexion;
				sqlComando.CommandText = "ObtenerProductoPaginado";
				sqlComando.CommandType = CommandType.StoredProcedure;
				sqlComando.Parameters.Add("ProductoId", SqlDbType.Int).Value = productoId;
				sqlComando.Parameters.Add("Codigo", SqlDbType.VarChar).Value = codigo;
				sqlComando.Parameters.Add("Producto", SqlDbType.VarChar).Value = producto;
				sqlComando.Parameters.Add("TamanioPagina", SqlDbType.Int).Value = tamanioPagina;
				sqlComando.Parameters.Add("NumeroPagina", SqlDbType.Int).Value = numeroPagina;

				oConexion.Open();

				using (SqlDataReader dr = sqlComando.ExecuteReader())
				{
					while (dr.Read())
					{
						clienteListaInfo.Add(CargarProductoInfo(dr));
					}
				}
				oConexion.Close();
			}
			return clienteListaInfo;
		}
		private static ProductoInfo CargarProductoInfo(IDataReader dr)
		{
			int colCount = dr.FieldCount;
			var values = new object[colCount];

			#region Indices
			int indProductoId = dr.GetOrdinal("ProductoId");
			int indCodigo = dr.GetOrdinal("Codigo");
			int indTipoProductoId = dr.GetOrdinal("TipoProductoId");
			int indTipoProducto = dr.GetOrdinal("TipoProducto");
			int indSubTipoProductoId = dr.GetOrdinal("SubTipoProductoId");
			int indSubTipoProducto = dr.GetOrdinal("SubTipoProducto");
			int indProducto = dr.GetOrdinal("Producto");
			int indDescripcion = dr.GetOrdinal("Descripcion");
			int indActivo = dr.GetOrdinal("Activo");
			int indMarca = dr.GetOrdinal("Marca");
			int indPrecioNormal = dr.GetOrdinal("PrecioNormal");
			int indPrecioDescuento = dr.GetOrdinal("PrecioDescuento");
			int indPrecioCompra = dr.GetOrdinal("PrecioCompra");
		    int indUnidadMedidaId = dr.GetOrdinal("UnidadMedidaId");
            int indUnidadMedida = dr.GetOrdinal("UnidadMedida");
		    int indDisponibleEstacion = dr.GetOrdinal("DisponibleEstacion");
		    int indDisponibleMarket = dr.GetOrdinal("DisponibleMarket");
		    int indDisponibleCanastilla = dr.GetOrdinal("DisponibleCanastilla");
            int indStockActual = dr.GetOrdinal("StockActual");
		    int indTipoNegocioId = dr.GetOrdinal("TipoNegocioId");
            //int indUsuarioCreacionId = dr.GetOrdinal("UsuarioCreacionId");
            ////int indFechaCreacion = dr.GetOrdinal("FechaCreacion");
            //int indUsuarioModificacionId = dr.GetOrdinal("UsuarioModificacionId");
            //int indFechaModificacion = dr.GetOrdinal("FechaModificacion");
            int indNumeroFila = dr.GetOrdinal("NumeroFila");
			int indTotalFilas = dr.GetOrdinal("TotalFilas");
			#endregion

			var productoInfo = new ProductoInfo();
			dr.GetValues(values);

			#region Campos
			productoInfo.ProductoId = Convert.ToInt32(values[indProductoId]);
			productoInfo.Codigo = Convert.ToString(values[indCodigo]);
			productoInfo.TipoProductoId = Convert.ToInt32(values[indTipoProductoId]);
			productoInfo.TipoProducto = Convert.ToString(values[indTipoProducto]);
			if (values[indSubTipoProductoId] != DBNull.Value) productoInfo.SubTipoProductoId = Convert.ToInt32(values[indSubTipoProductoId]);
			if (values[indSubTipoProducto] != DBNull.Value) productoInfo.SubTipoProducto = Convert.ToString(values[indSubTipoProducto]);
			productoInfo.Producto = Convert.ToString(values[indProducto]);
			productoInfo.Descripcion = Convert.ToString(values[indDescripcion]);
			productoInfo.Marca = Convert.ToString(values[indMarca]);
			productoInfo.PrecioNormal = Convert.ToDecimal(values[indPrecioNormal]);
			productoInfo.PrecioDescuento = Convert.ToDecimal(values[indPrecioDescuento]);
			productoInfo.PrecioCompra = Convert.ToDecimal(values[indPrecioCompra]);
			productoInfo.Activo = Convert.ToInt32(values[indActivo]);
		    if (values[indUnidadMedida] != DBNull.Value) productoInfo.UnidadMedida = Convert.ToString(values[indUnidadMedida]);
            productoInfo.UnidadMedidaId = Convert.ToInt32(values[indUnidadMedidaId]);
		    productoInfo.DisponibleEstacion = Convert.ToInt32(values[indDisponibleEstacion]);
		    productoInfo.DisponibleMarket = Convert.ToInt32(values[indDisponibleMarket]);
		    productoInfo.DisponibleCanastilla = Convert.ToInt32(values[indDisponibleCanastilla]);

			if (values[indStockActual] != DBNull.Value) productoInfo.StockActual = Convert.ToInt32(values[indStockActual]);

		    productoInfo.TipoNegocioId = Convert.ToString(values[indTipoNegocioId]);
            //if (values[indTipoTiendaId] != DBNull.Value) productoInfo.TipoTiendaId = Convert.ToInt32(values[indTipoTiendaId]);
            //tipoProductoInfo.UsuarioCreacionId = Convert.ToInt32(values[indUsuarioCreacionId]);
            //tipoProductoInfo.FechaCreacion = Convert.ToDateTime(values[indFechaCreacion]);
            //tipoProductoInfo.UsuarioModificacionId = Convert.ToInt32(values[indUsuarioModificacionId]);
            //tipoProductoInfo.FechaModificacion = Convert.ToDateTime(values[indFechaModificacion]);
            productoInfo.NumeroFila = Convert.ToInt32(values[indNumeroFila]);
			productoInfo.TotalFilas = Convert.ToInt32(values[indTotalFilas]);
			#endregion

			return productoInfo;
		}

		public int Insertar(ProductoInfo productoInfo)
		{
			int resultado;
			try
			{
				using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
				{
					var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "InsertarProducto", CommandType = CommandType.StoredProcedure };

					sqlComando.Parameters.Add("Codigo", SqlDbType.VarChar).Value = productoInfo.Codigo;
					sqlComando.Parameters.Add("Nombre", SqlDbType.VarChar).Value = productoInfo.Producto;
					sqlComando.Parameters.Add("Descripcion", SqlDbType.VarChar).Value = productoInfo.Descripcion;
					sqlComando.Parameters.Add("Marca", SqlDbType.VarChar).Value = productoInfo.Marca;
					sqlComando.Parameters.Add("TipoProductoId", SqlDbType.Int).Value = productoInfo.TipoProductoId;
					sqlComando.Parameters.Add("SubTipoProductoId", SqlDbType.Int).Value = productoInfo.SubTipoProductoId;
					sqlComando.Parameters.Add("UnidadMedidaId", SqlDbType.Int).Value = productoInfo.UnidadMedidaId;
					sqlComando.Parameters.Add("PrecioNormal", SqlDbType.Decimal).Value = productoInfo.PrecioNormal;
					sqlComando.Parameters.Add("PrecioDescuento", SqlDbType.Decimal).Value = productoInfo.PrecioDescuento;
					sqlComando.Parameters.Add("PrecioCompra", SqlDbType.Decimal).Value = productoInfo.PrecioCompra;
					sqlComando.Parameters.Add("Activo", SqlDbType.Int).Value = productoInfo.Activo;
					sqlComando.Parameters.Add("UsuarioCreacionId", SqlDbType.Int).Value = productoInfo.UsuarioCreacionId;

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

		public int Actualizar(ProductoInfo productoInfo)
		{
			int resultado;
			try
			{
				using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
				{
					var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "ActualizarProducto", CommandType = CommandType.StoredProcedure };

					sqlComando.Parameters.Add("ProductoId", SqlDbType.Int).Value = productoInfo.ProductoId;
					sqlComando.Parameters.Add("Codigo", SqlDbType.VarChar).Value = productoInfo.Codigo;
					sqlComando.Parameters.Add("Nombre", SqlDbType.VarChar).Value = productoInfo.Producto;
					sqlComando.Parameters.Add("Descripcion", SqlDbType.VarChar).Value = productoInfo.Descripcion;
					sqlComando.Parameters.Add("Marca", SqlDbType.VarChar).Value = productoInfo.Marca;
					sqlComando.Parameters.Add("TipoProductoId", SqlDbType.Int).Value = productoInfo.TipoProductoId;
					sqlComando.Parameters.Add("SubTipoProductoId", SqlDbType.Int).Value = productoInfo.SubTipoProductoId;
					sqlComando.Parameters.Add("UnidadMedidaId", SqlDbType.Int).Value = productoInfo.UnidadMedidaId;
					sqlComando.Parameters.Add("PrecioNormal", SqlDbType.Decimal).Value = productoInfo.PrecioNormal;
					sqlComando.Parameters.Add("PrecioDescuento", SqlDbType.Decimal).Value = productoInfo.PrecioDescuento;
					sqlComando.Parameters.Add("PrecioCompra", SqlDbType.Decimal).Value = productoInfo.PrecioCompra;
					sqlComando.Parameters.Add("Activo", SqlDbType.Int).Value = productoInfo.Activo;
					sqlComando.Parameters.Add("UsuarioModificacionId", SqlDbType.Int).Value = productoInfo.UsuarioModificacionId;

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

	    public List<ProductoTipoTiendaInfo> ListarProductoTipoTienda(int productoId, int tipoTiendaId)
	    {
	        var productoTipoTiendaListaInfo = new List<ProductoTipoTiendaInfo>();
	        using (var oConexion = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
	        {
	            var sqlComando = new SqlCommand();
	            sqlComando.Connection = oConexion;
	            sqlComando.CommandText = "ObtenerProductoTipoTienda";
	            sqlComando.CommandType = CommandType.StoredProcedure;
	            sqlComando.Parameters.Add("ProductoId", SqlDbType.Int).Value = productoId;
	            sqlComando.Parameters.Add("TipoTiendaId", SqlDbType.Int).Value = tipoTiendaId;

                oConexion.Open();

	            using (SqlDataReader dr = sqlComando.ExecuteReader())
	            {
	                while (dr.Read())
	                {
	                    productoTipoTiendaListaInfo.Add(CargarProductoTipoTiendaInfo(dr));
	                }
	            }
	            oConexion.Close();
	        }
	        return productoTipoTiendaListaInfo;
	    }
	    private static ProductoTipoTiendaInfo CargarProductoTipoTiendaInfo(IDataReader dr)
	    {
	        int colCount = dr.FieldCount;
	        var values = new object[colCount];

	        #region Indices
	        int indProductoId = dr.GetOrdinal("ProductoId");
	        int indTipoTiendaId = dr.GetOrdinal("TipoTiendaId");
	        int indUsuarioCreacionId = dr.GetOrdinal("UsuarioCreacionId");
	        int indFechaCreacion = dr.GetOrdinal("FechaCreacion");
	        int indUsuarioModificacionId = dr.GetOrdinal("UsuarioModificacionId");
	        int indFechaModificacion = dr.GetOrdinal("FechaModificacion");
	        #endregion

	        var productoTipoTiendaInfo = new ProductoTipoTiendaInfo();
	        dr.GetValues(values);

            #region Campos
	        productoTipoTiendaInfo.ProductoId = Convert.ToInt32(values[indProductoId]);
	        if (values[indTipoTiendaId] != DBNull.Value) productoTipoTiendaInfo.TipoTiendaId = Convert.ToInt32(values[indTipoTiendaId]);
	        productoTipoTiendaInfo.UsuarioCreacionId = Convert.ToInt32(values[indUsuarioCreacionId]);
	        productoTipoTiendaInfo.FechaCreacion = Convert.ToDateTime(values[indFechaCreacion]);
	        productoTipoTiendaInfo.UsuarioModificacionId = Convert.ToInt32(values[indUsuarioModificacionId]);
	        if (values[indFechaModificacion] != DBNull.Value) productoTipoTiendaInfo.FechaModificacion = Convert.ToDateTime(values[indFechaModificacion]);
            #endregion

            return productoTipoTiendaInfo;
	    }
	    public int InsertarProductoTipoTienda(ProductoTipoTiendaInfo productoTipoTiendaInfo)
	    {
	        int resultado;
	        try
	        {
	            using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
	            {
	                var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "InsertarProductoTipoTienda", CommandType = CommandType.StoredProcedure };

	                sqlComando.Parameters.Add("ProductoId", SqlDbType.Int).Value = productoTipoTiendaInfo.ProductoId;
	                sqlComando.Parameters.Add("TipoTiendaId", SqlDbType.Int).Value = productoTipoTiendaInfo.TipoTiendaId;
                    sqlComando.Parameters.Add("UsuarioCreacionId", SqlDbType.Int).Value = productoTipoTiendaInfo.UsuarioCreacionId;

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
        public int EliminarProductoTipoTienda(int productoId, int tipoTiendaId)
	    {
	        int resultado = 0;
	        try
	        {
	            using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
	            {
	                var sqlComando = new SqlCommand
	                {
	                    Connection = oConnection,
	                    CommandText = "EliminarProductoTipoTienda",
	                    CommandType = CommandType.StoredProcedure
	                };

	                sqlComando.Parameters.Add("ProductoId", SqlDbType.Int).Value = productoId;
	                sqlComando.Parameters.Add("TipoTiendaId", SqlDbType.Int).Value = tipoTiendaId;

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
	}
}