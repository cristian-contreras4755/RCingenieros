using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using APU.Entidad;
using APU.Herramientas;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace APU.AccesoDatos
{
    public class PerfilOpcionAccesoDatos
    {
        public List<PerfilOpcionInfo> Listar(int perfilId)
        {
            var perfilOpcionListaInfo = new List<PerfilOpcionInfo>();
            using (var oConexion = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
            {
                var sqlComando = new SqlCommand();
                sqlComando.Connection = oConexion;
                sqlComando.CommandText = "ObtenerPerfilOpcion";
                sqlComando.CommandType = CommandType.StoredProcedure;
                sqlComando.Parameters.Add("PerfilId", SqlDbType.Int).Value = perfilId;

                oConexion.Open();

                using (SqlDataReader dr = sqlComando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        perfilOpcionListaInfo.Add(CargarPerfilOpcionInfo(dr));
                    }
                }
                oConexion.Close();
            }
            return perfilOpcionListaInfo;
        }
        private static PerfilOpcionInfo CargarPerfilOpcionInfo(IDataReader dr)
        {
            int colCount = dr.FieldCount;
            var values = new object[colCount];

            #region Indices
            int indPerfilId = dr.GetOrdinal("PerfilId");
            int indOpcionId = dr.GetOrdinal("OpcionId");
            int indOpcionPadreId = dr.GetOrdinal("OpcionPadreId");
            int indNombre = dr.GetOrdinal("Nombre");
            int indOrden = dr.GetOrdinal("Orden");
            int indActivo = dr.GetOrdinal("Activo");
            #endregion

            var perfilOpcionInfo = new PerfilOpcionInfo();
            dr.GetValues(values);

            #region Campos
            perfilOpcionInfo.PerfilId = Convert.ToInt32(values[indPerfilId]);
            perfilOpcionInfo.OpcionId = Convert.ToInt32(values[indOpcionId]);
            perfilOpcionInfo.OpcionPadreId = Convert.ToInt32(values[indOpcionPadreId]);
            perfilOpcionInfo.Nombre = Convert.ToString(values[indNombre]);
            perfilOpcionInfo.Orden = Convert.ToInt32(values[indOrden]);
            perfilOpcionInfo.Activo = Convert.ToInt32(values[indActivo]);
            #endregion

            return perfilOpcionInfo;
        }
        public int Insertar(PerfilOpcionInfo perfilOpcionInfo)
        {
            int resultado;
            try
            {
                using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                {
                    var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "InsertarPerfilOpcion", CommandType = CommandType.StoredProcedure };

                    sqlComando.Parameters.Add("PerfilId", SqlDbType.Int).Value = perfilOpcionInfo.PerfilId;
                    sqlComando.Parameters.Add("OpcionId", SqlDbType.Int).Value = perfilOpcionInfo.OpcionId;

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
        public int Eliminar(int perfilId, int opcionId)
        {
            int resultado = 0;
            try
            {
                using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                {
                    var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "EliminarPerfilOpcion", CommandType = CommandType.StoredProcedure };

                    sqlComando.Parameters.Add("PerfilId", SqlDbType.Int).Value = perfilId;
                    sqlComando.Parameters.Add("OpcionId", SqlDbType.Int).Value = opcionId;

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
