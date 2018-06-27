using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using APU.Entidad;
using APU.Herramientas;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
namespace APU.AccesoDatos
{
	public class TipoProductoAccesoDatos
	{
		public List<TipoProductoInfo> ListarPaginado(int tipoProductoId, int tamanioPagina, int numeroPagina)
		{
			var clienteListaInfo = new List<TipoProductoInfo>();
			using (var oConexion = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
			{
				var sqlComando = new SqlCommand();
				sqlComando.Connection = oConexion;
				sqlComando.CommandText = "ObtenerTipoProductoPaginado";
				sqlComando.CommandType = CommandType.StoredProcedure;
				sqlComando.Parameters.Add("TipoProductoId", SqlDbType.Int).Value = tipoProductoId;
				sqlComando.Parameters.Add("TamanioPagina", SqlDbType.Int).Value = tamanioPagina;
				sqlComando.Parameters.Add("NumeroPagina", SqlDbType.Int).Value = numeroPagina;
				//sqlComando.Parameters.Add("Nivel", SqlDbType.Int).Value = nivel;

				oConexion.Open();

				using (SqlDataReader dr = sqlComando.ExecuteReader())
				{
					while (dr.Read())
					{
						clienteListaInfo.Add(CargarTipoProductoInfo(dr));
					}
				}
				oConexion.Close();
			}
			return clienteListaInfo;
		}
		public List<TipoProductoInfo> ListarPaginadoSubProducto(int tipoProductoId, int tamanioPagina, int numeroPagina)
		{
			var clienteListaInfo = new List<TipoProductoInfo>();
			using (var oConexion = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
			{
				var sqlComando = new SqlCommand();
				sqlComando.Connection = oConexion;
				sqlComando.CommandText = "ObtenerSubTipoProductoPaginado";
				sqlComando.CommandType = CommandType.StoredProcedure;
				sqlComando.Parameters.Add("TipoProductoId", SqlDbType.Int).Value = tipoProductoId;
				sqlComando.Parameters.Add("TamanioPagina", SqlDbType.Int).Value = tamanioPagina;
				sqlComando.Parameters.Add("NumeroPagina", SqlDbType.Int).Value = numeroPagina;
				//sqlComando.Parameters.Add("Nivel", SqlDbType.Int).Value = nivel;

				oConexion.Open();

				using (SqlDataReader dr = sqlComando.ExecuteReader())
				{
					while (dr.Read())
					{
						clienteListaInfo.Add(CargarSubTipoProductoInfo(dr));
					}
				}
				oConexion.Close();
			}
			return clienteListaInfo;
		}

		public List<TipoProductoInfo> Listar(int tipoProductoId, int tipoProductoPadreId, int nivel)
		{
			var clienteListaInfo = new List<TipoProductoInfo>();
			using (var oConexion = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
			{
				var sqlComando = new SqlCommand();
				sqlComando.Connection = oConexion;
				sqlComando.CommandText = "ObtenerTipoProducto";
				sqlComando.CommandType = CommandType.StoredProcedure;
				sqlComando.Parameters.Add("TipoProductoId", SqlDbType.Int).Value = tipoProductoId;
				sqlComando.Parameters.Add("TipoProductoPadreId", SqlDbType.Int).Value = tipoProductoPadreId;
				sqlComando.Parameters.Add("Nivel", SqlDbType.Int).Value = nivel;

				oConexion.Open();

				using (SqlDataReader dr = sqlComando.ExecuteReader())
				{
					while (dr.Read())
					{
						clienteListaInfo.Add(CargarTipoProductoLista(dr));
					}
				}
				oConexion.Close();
			}
			return clienteListaInfo;
		}
		private static TipoProductoInfo CargarTipoProductoInfo(IDataReader dr)
		{
			int colCount = dr.FieldCount;
			var values = new object[colCount];

			#region Indices
			int indTipoProductoId = dr.GetOrdinal("TipoProductoId");			
			int indNombre = dr.GetOrdinal("Nombre");
			int indDescripcion = dr.GetOrdinal("Descripcion");	
			int indActivo = dr.GetOrdinal("Activo");
			//int indUsuarioCreacionId = dr.GetOrdinal("UsuarioCreacionId");
			////int indFechaCreacion = dr.GetOrdinal("FechaCreacion");
			//int indUsuarioModificacionId = dr.GetOrdinal("UsuarioModificacionId");
			//int indFechaModificacion = dr.GetOrdinal("FechaModificacion");
			int indNumeroFila = dr.GetOrdinal("NumeroFila");
			int indTotalFilas = dr.GetOrdinal("TotalFilas");
			#endregion

			var tipoProductoInfo = new TipoProductoInfo();
			dr.GetValues(values);

			#region Campos
			tipoProductoInfo.TipoProductoId = Convert.ToInt32(values[indTipoProductoId]);
			tipoProductoInfo.Nombre = Convert.ToString(values[indNombre]);
			tipoProductoInfo.Descripcion = Convert.ToString(values[indDescripcion]);
			
			tipoProductoInfo.Activo = Convert.ToInt32(values[indActivo]);
			//tipoProductoInfo.UsuarioCreacionId = Convert.ToInt32(values[indUsuarioCreacionId]);
			//tipoProductoInfo.FechaCreacion = Convert.ToDateTime(values[indFechaCreacion]);
			//tipoProductoInfo.UsuarioModificacionId = Convert.ToInt32(values[indUsuarioModificacionId]);
			//tipoProductoInfo.FechaModificacion = Convert.ToDateTime(values[indFechaModificacion]);
			tipoProductoInfo.NumeroFila = Convert.ToInt32(values[indNumeroFila]);
			tipoProductoInfo.TotalFilas = Convert.ToInt32(values[indTotalFilas]);
			#endregion

			return tipoProductoInfo;
		}
		private static TipoProductoInfo CargarSubTipoProductoInfo(IDataReader dr)
		{
			int colCount = dr.FieldCount;
			var values = new object[colCount];

			#region Indices
			int indTipoProductoId = dr.GetOrdinal("TipoProductoId");
			int indNombre = dr.GetOrdinal("Nombre");
			int indDescripcion = dr.GetOrdinal("Descripcion");
			int indActivo = dr.GetOrdinal("Activo");
			int indTipoProducto = dr.GetOrdinal("TipoProducto");
			int indTipoProductoPadreId = dr.GetOrdinal("TipoProductoPadreId");
			////int indFechaCreacion = dr.GetOrdinal("FechaCreacion");
			//int indUsuarioModificacionId = dr.GetOrdinal("UsuarioModificacionId");
			//int indFechaModificacion = dr.GetOrdinal("FechaModificacion");
			int indNumeroFila = dr.GetOrdinal("NumeroFila");
			int indTotalFilas = dr.GetOrdinal("TotalFilas");
			#endregion

			var tipoProductoInfo = new TipoProductoInfo();
			dr.GetValues(values);

			#region Campos
			tipoProductoInfo.TipoProductoId = Convert.ToInt32(values[indTipoProductoId]);
			tipoProductoInfo.Nombre = Convert.ToString(values[indNombre]);
			tipoProductoInfo.Descripcion = Convert.ToString(values[indDescripcion]);

			tipoProductoInfo.Activo = Convert.ToInt32(values[indActivo]);
			tipoProductoInfo.TipoProducto = Convert.ToString(values[indTipoProducto]);
			//tipoProductoInfo.FechaCreacion = Convert.ToDateTime(values[indFechaCreacion]);
			//tipoProductoInfo.UsuarioModificacionId = Convert.ToInt32(values[indUsuarioModificacionId]);
			//tipoProductoInfo.FechaModificacion = Convert.ToDateTime(values[indFechaModificacion]);
			tipoProductoInfo.NumeroFila = Convert.ToInt32(values[indNumeroFila]);
			tipoProductoInfo.TotalFilas = Convert.ToInt32(values[indTotalFilas]);
			if (values[indTipoProductoPadreId] != DBNull.Value) tipoProductoInfo.TipoProductoPadreId = Convert.ToInt32(values[indTipoProductoPadreId]);
			#endregion

			return tipoProductoInfo;
		}

		private static TipoProductoInfo CargarTipoProductoLista(IDataReader dr)
		{
			int colCount = dr.FieldCount;
			var values = new object[colCount];

			#region Indices
			int indTipoProductoId = dr.GetOrdinal("TipoProductoId");
			int indNombre = dr.GetOrdinal("Nombre");
			int indDescripcion = dr.GetOrdinal("Descripcion");
			int indActivo = dr.GetOrdinal("Activo");
			int indTipoProductoPadreId = dr.GetOrdinal("TipoProductoPadreId");
			//int indUsuarioCreacionId = dr.GetOrdinal("UsuarioCreacionId");
			////int indFechaCreacion = dr.GetOrdinal("FechaCreacion");
			//int indUsuarioModificacionId = dr.GetOrdinal("UsuarioModificacionId");
			//int indFechaModificacion = dr.GetOrdinal("FechaModificacion");
			//int indNumeroFila = dr.GetOrdinal("NumeroFila");
			//int indTotalFilas = dr.GetOrdinal("TotalFilas");
			#endregion

			var tipoProductoInfo = new TipoProductoInfo();
			dr.GetValues(values);

			#region Campos
			tipoProductoInfo.TipoProductoId = Convert.ToInt32(values[indTipoProductoId]);
			tipoProductoInfo.Nombre = Convert.ToString(values[indNombre]);
			tipoProductoInfo.Descripcion = Convert.ToString(values[indDescripcion]);
			tipoProductoInfo.TipoProductoPadreId = Convert.ToInt32(values[indTipoProductoPadreId]);
			tipoProductoInfo.Activo = Convert.ToInt32(values[indActivo]);
			//tipoProductoInfo.UsuarioCreacionId = Convert.ToInt32(values[indUsuarioCreacionId]);
			//tipoProductoInfo.FechaCreacion = Convert.ToDateTime(values[indFechaCreacion]);
			//tipoProductoInfo.UsuarioModificacionId = Convert.ToInt32(values[indUsuarioModificacionId]);
			//tipoProductoInfo.FechaModificacion = Convert.ToDateTime(values[indFechaModificacion]);
			//tipoProductoInfo.NumeroFila = Convert.ToInt32(values[indNumeroFila]);
			//tipoProductoInfo.TotalFilas = Convert.ToInt32(values[indTotalFilas]);
			#endregion

			return tipoProductoInfo;
		}

		public int Insertar(TipoProductoInfo tipoProductoInfo)
		{
			int resultado;
			try
			{
				using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
				{
					var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "InsertarTipoProducto", CommandType = CommandType.StoredProcedure };

					sqlComando.Parameters.Add("Nombre", SqlDbType.VarChar).Value = tipoProductoInfo.Nombre;
					sqlComando.Parameters.Add("Descripcion", SqlDbType.VarChar).Value = tipoProductoInfo.Descripcion;
					sqlComando.Parameters.Add("Nivel", SqlDbType.Int).Value = tipoProductoInfo.Nivel;
					sqlComando.Parameters.Add("TipoProductoPadreId", SqlDbType.Int).Value = tipoProductoInfo.TipoProductoPadreId > 0 ? tipoProductoInfo.TipoProductoPadreId : (object)System.DBNull.Value;
					sqlComando.Parameters.Add("Activo", SqlDbType.Int).Value = tipoProductoInfo.Activo;
					sqlComando.Parameters.Add("UsuarioCreacionId", SqlDbType.Int).Value = tipoProductoInfo.UsuarioCreacionId;

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

		public int Actualizar(TipoProductoInfo tipoProductoInfo)
		{
			int resultado;
			try
			{
				using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
				{
					var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "ActualizarTipoProducto", CommandType = CommandType.StoredProcedure };

					sqlComando.Parameters.Add("TipoProductoId", SqlDbType.Int).Value = tipoProductoInfo.TipoProductoId;
					sqlComando.Parameters.Add("Nombre", SqlDbType.VarChar).Value = tipoProductoInfo.Nombre;
					sqlComando.Parameters.Add("Descripcion", SqlDbType.VarChar).Value = tipoProductoInfo.Descripcion;
					sqlComando.Parameters.Add("Nivel", SqlDbType.Int).Value = tipoProductoInfo.Nivel;
					sqlComando.Parameters.Add("TipoProductoPadreId", SqlDbType.Int).Value = tipoProductoInfo.TipoProductoPadreId > 0 ? tipoProductoInfo.TipoProductoPadreId : (object)System.DBNull.Value;
					sqlComando.Parameters.Add("Activo", SqlDbType.Int).Value = tipoProductoInfo.Activo;
					sqlComando.Parameters.Add("UsuarioModificacionId", SqlDbType.Int).Value = tipoProductoInfo.UsuarioModificacionId;

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
	}
}
