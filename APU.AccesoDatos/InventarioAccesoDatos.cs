using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using APU.Entidad;
using APU.Herramientas;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace APU.AccesoDatos
{
	public class InventarioAccesoDatos
	{
		public List<InventarioInfo> Listar(int almacenId, int productoId, int tipoNegocioId)
		{
			var inventarioListaInfo = new List<InventarioInfo>();
			using (var oConexion = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
			{
				var sqlComando = new SqlCommand();
				sqlComando.Connection = oConexion;
				sqlComando.CommandText = "ObtenerInventario";
				sqlComando.CommandType = CommandType.StoredProcedure;
				sqlComando.Parameters.Add("AlmacenId", SqlDbType.Int).Value = almacenId;
				sqlComando.Parameters.Add("ProductoId", SqlDbType.Int).Value = productoId;
			    sqlComando.Parameters.Add("TipoNegocioId", SqlDbType.Int).Value = tipoNegocioId;

                oConexion.Open();

				using (SqlDataReader dr = sqlComando.ExecuteReader())
				{
					while (dr.Read())
					{
						inventarioListaInfo.Add(CargarInventarioInfo(dr));
					}
				}
				oConexion.Close();
			}
			return inventarioListaInfo;
		}
		public List<InventarioInfo> ListarPaginado(int inventarioId, int almacenId, int tipoNegocioId, int tamanioPagina, int numeroPagina)
		{
			var inventarioListaInfo = new List<InventarioInfo>();
			using (var oConexion = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
			{
				var sqlComando = new SqlCommand();
				sqlComando.Connection = oConexion;
				sqlComando.CommandText = "ObtenerInventarioPaginado";
				sqlComando.CommandType = CommandType.StoredProcedure;
				sqlComando.Parameters.Add("InventarioId", SqlDbType.Int).Value = inventarioId;
				sqlComando.Parameters.Add("AlmacenId", SqlDbType.Int).Value = almacenId;
			    sqlComando.Parameters.Add("TipoNegocioId", SqlDbType.Int).Value = tipoNegocioId;
                sqlComando.Parameters.Add("TamanioPagina", SqlDbType.Int).Value = tamanioPagina;
				sqlComando.Parameters.Add("NumeroPagina", SqlDbType.Int).Value = numeroPagina;

				oConexion.Open();

				using (SqlDataReader dr = sqlComando.ExecuteReader())
				{
					while (dr.Read())
					{
						inventarioListaInfo.Add(CargarInventarioPagInfo(dr));
					}
				}
				oConexion.Close();
			}
			return inventarioListaInfo;
		}
		private static InventarioInfo CargarInventarioInfo(IDataReader dr)
		{
			int colCount = dr.FieldCount;
			var values = new object[colCount];

			#region Indices
			int indInventarioId = dr.GetOrdinal("InventarioId");
			int indAlmacenId = dr.GetOrdinal("AlmacenId");
			int indProductoId = dr.GetOrdinal("ProductoId");
			int indAlmacen = dr.GetOrdinal("Almacen");
			int indProducto = dr.GetOrdinal("Producto");
			int indInventarioActual = dr.GetOrdinal("InventarioActual");
			int indInventarioMinimo = dr.GetOrdinal("InventarioMinimo");
		    int indTipoNegocioId = dr.GetOrdinal("TipoNegocioId");
            #endregion

            var inventarioInfo = new InventarioInfo();
			dr.GetValues(values);

			#region Campos
			inventarioInfo.InventarioId = Convert.ToInt32(values[indInventarioId]);
			inventarioInfo.AlmacenId = Convert.ToInt32(values[indAlmacenId]);
			inventarioInfo.ProductoId = Convert.ToInt32(values[indProductoId]);
			inventarioInfo.Almacen = Convert.ToString(values[indAlmacen]);
			inventarioInfo.Producto = Convert.ToString(values[indProducto]);
			
			inventarioInfo.InventarioActual = Convert.ToDecimal(values[indInventarioActual]);
			inventarioInfo.InventarioMinimo = Convert.ToDecimal(values[indInventarioMinimo]);
		    inventarioInfo.TipoNegocioId = Convert.ToInt32(values[indTipoNegocioId]);

            #endregion

            return inventarioInfo;
		}

		private static InventarioInfo CargarInventarioPagInfo(IDataReader dr)
		{
			int colCount = dr.FieldCount;
			var values = new object[colCount];

			#region Indices
			int indInventarioId = dr.GetOrdinal("InventarioId");
			int indAlmacenId = dr.GetOrdinal("AlmacenId");
			int indProductoId = dr.GetOrdinal("ProductoId");
			int indAlmacen = dr.GetOrdinal("Almacen");
			int indProducto = dr.GetOrdinal("Producto");
			int indInventarioActual = dr.GetOrdinal("InventarioActual");
			int indInventarioMinimo = dr.GetOrdinal("InventarioMinimo");
			int indCodigo = dr.GetOrdinal("Codigo");
			int indTipoProducto = dr.GetOrdinal("TipoProducto");
			int indUnidadMedida = dr.GetOrdinal("UnidadMedida");
		    int indTipoNegocioId = dr.GetOrdinal("TipoNegocioId");
            int indNumeroFila = dr.GetOrdinal("NumeroFila");
			int indTotalFilas = dr.GetOrdinal("TotalFilas");

			#endregion

			var inventarioInfo = new InventarioInfo();
			dr.GetValues(values);

			#region Campos
			inventarioInfo.InventarioId = Convert.ToInt32(values[indInventarioId]);
			inventarioInfo.AlmacenId = Convert.ToInt32(values[indAlmacenId]);
			inventarioInfo.ProductoId = Convert.ToInt32(values[indProductoId]);
			inventarioInfo.Almacen = Convert.ToString(values[indAlmacen]);
			inventarioInfo.Producto = Convert.ToString(values[indProducto]);
			inventarioInfo.InventarioActual = Convert.ToDecimal(values[indInventarioActual]);
			inventarioInfo.InventarioMinimo = Convert.ToDecimal(values[indInventarioMinimo]);
			inventarioInfo.Codigo = Convert.ToString(values[indCodigo]);
			inventarioInfo.TipoProducto = Convert.ToString(values[indTipoProducto]);
			inventarioInfo.UnidadMedida = Convert.ToString(values[indUnidadMedida]);
		    inventarioInfo.TipoNegocioId = Convert.ToInt32(values[indTipoNegocioId]);

            inventarioInfo.NumeroFila = Convert.ToInt32(values[indNumeroFila]);
			inventarioInfo.TotalFilas = Convert.ToInt32(values[indTotalFilas]);


			#endregion

			return inventarioInfo;
		}
		public int Actualizar(InventarioInfo inventarioInfo)
		{
			int resultado = 0;
			try
			{
				using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
				{
					var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "ActualizarInventario", CommandType = CommandType.StoredProcedure };

					sqlComando.Parameters.Add("AlmacenId", SqlDbType.Int).Value = inventarioInfo.AlmacenId;
					sqlComando.Parameters.Add("ProductoId", SqlDbType.Int).Value = inventarioInfo.ProductoId;					
					sqlComando.Parameters.Add("InventarioActual", SqlDbType.Decimal).Value = inventarioInfo.InventarioActual;
				    sqlComando.Parameters.Add("TipoNegocioId", SqlDbType.Int).Value = inventarioInfo.TipoNegocioId;
                    sqlComando.Parameters.Add("UsuarioModificacionId", SqlDbType.Int).Value = inventarioInfo.UsuarioModificacionId;

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

		public int Insertar(InventarioInfo inventarioInfo)
		{
			int resultado = 0;
			try
			{
				using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
				{
					var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "InsertarInventario", CommandType = CommandType.StoredProcedure };

					sqlComando.Parameters.Add("AlmacenId", SqlDbType.Int).Value = inventarioInfo.AlmacenId;
					sqlComando.Parameters.Add("ProductoId", SqlDbType.Int).Value = inventarioInfo.ProductoId;
					sqlComando.Parameters.Add("NuevoInventario", SqlDbType.Decimal).Value = inventarioInfo.InventarioActual;
				    sqlComando.Parameters.Add("TipoNegocioId", SqlDbType.Int).Value = inventarioInfo.TipoNegocioId;
                    sqlComando.Parameters.Add("UsuarioCreacionId", SqlDbType.Int).Value = inventarioInfo.UsuarioCreacionId;

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
	}
}