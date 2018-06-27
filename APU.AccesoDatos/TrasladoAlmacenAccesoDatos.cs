using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using APU.Entidad;
using APU.Herramientas;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace APU.AccesoDatos
{
	public class TrasladoAlmacenAccesoDatos
	{
		public int Insertar(TrasladoAlmacenInfo inventarioInfo)
		{
			int resultado = 0;
			try
			{
				using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
				{
					var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "InsertarTrasladoAlmacen", CommandType = CommandType.StoredProcedure };
					sqlComando.Parameters.Add("AlmacenOrigenId", SqlDbType.Int).Value = inventarioInfo.AlmacenOrigenId;
					sqlComando.Parameters.Add("AlmacenDestinoId", SqlDbType.Int).Value = inventarioInfo.AlmacenDestinoId;
					sqlComando.Parameters.Add("ProductoId", SqlDbType.Int).Value = inventarioInfo.ProductoId;
					sqlComando.Parameters.Add("CantidadProducto", SqlDbType.Decimal).Value = inventarioInfo.CantidadProducto;
					sqlComando.Parameters.Add("TipoNegocioId", SqlDbType.Int).Value = inventarioInfo.TipoNegocioId;
					sqlComando.Parameters.Add("UsuarioResponsableId", SqlDbType.Int).Value = inventarioInfo.UsuarioResponsableId;
					sqlComando.Parameters.Add("UsuarioCreacionId", SqlDbType.Int).Value = inventarioInfo.UsuarioCreacionId;
					sqlComando.Parameters.Add("FechaTraslado", SqlDbType.DateTime2).Value = inventarioInfo.FechaTraslado;

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
