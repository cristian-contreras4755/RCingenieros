using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using APU.Entidad;
using APU.Herramientas;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace APU.AccesoDatos
{
	public class MovimientosAccesoDatos
	{	
				
		public int InsertarMovimientos(MovimientosInfo movimientosInfo)
		{
			int resultado;
			try
			{
				using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
				{
					var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "InsertarMovimientos", CommandType = CommandType.StoredProcedure };

					//sqlComando.Parameters.Add("ComprasDetalleId", SqlDbType.Int).Value = ventaInfo.ComprasDetalleId;
					sqlComando.Parameters.Add("OperacionId", SqlDbType.Int).Value = movimientosInfo.OperacionId;
					sqlComando.Parameters.Add("TipoMovimientoId", SqlDbType.Int).Value = movimientosInfo.TipoMovimientoId;
					sqlComando.Parameters.Add("FechaOperacion", SqlDbType.DateTime2).Value = movimientosInfo.FechaOperacion;
					sqlComando.Parameters.Add("Glosa", SqlDbType.VarChar).Value = movimientosInfo.Glosa;
										
					sqlComando.Parameters.Add("UsuarioCreacionId", SqlDbType.Int).Value = movimientosInfo.UsuarioCreacionId;

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
