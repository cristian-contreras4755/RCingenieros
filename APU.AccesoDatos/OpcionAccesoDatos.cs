using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using APU.Entidad;

namespace APU.AccesoDatos
{
    public class OpcionAccesoDatos
    {
        public List<OpcionInfo> ListarOpciones(int perfilId)
        {
            var opcionListaInfo = new List<OpcionInfo>();
            using (var oConexion = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
            {
                var sqlComando = new SqlCommand { Connection = oConexion, CommandText = "ObtenerOpcionPorPerfil", CommandType = CommandType.StoredProcedure };

                sqlComando.Parameters.Add("PerfilId", SqlDbType.Int).Value = perfilId;

                oConexion.Open();

                using (SqlDataReader dr = sqlComando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        opcionListaInfo.Add(CargarOpcionesInfo(dr));
                    }
                }
                oConexion.Close();
            }
            return opcionListaInfo;
        }
        public List<OpcionInfo> Listar(int opcionId)
        {
            var opcionListaInfo = new List<OpcionInfo>();
            using (var oConexion = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
            {
                var sqlComando = new SqlCommand { Connection = oConexion, CommandText = "ObtenerOpcion", CommandType = CommandType.StoredProcedure };

                sqlComando.Parameters.Add("OpcionId", SqlDbType.Int).Value = opcionId;

                oConexion.Open();

                using (SqlDataReader dr = sqlComando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        opcionListaInfo.Add(CargarOpcionesInfo(dr));
                    }
                }
                oConexion.Close();
            }
            return opcionListaInfo;
        }
        private static OpcionInfo CargarOpcionesInfo(IDataReader dr)
        {
            int colCount = dr.FieldCount;
            var values = new object[colCount];

            #region Indices
            int indPerfilId = dr.GetOrdinal("PerfilId");
            int indOpcionId = dr.GetOrdinal("OpcionId");
            int indOpcionPadreId = dr.GetOrdinal("OpcionPadreId");
            int indNombre = dr.GetOrdinal("Nombre");
            int indActivo = dr.GetOrdinal("Activo");
            int indUrl = dr.GetOrdinal("Url");
            int indOrden = dr.GetOrdinal("Orden");
            #endregion

            var opcionInfo = new OpcionInfo();
            dr.GetValues(values);

            #region Campos
            opcionInfo.PerfilId = Convert.ToInt32(values[indPerfilId]);
            opcionInfo.OpcionId = Convert.ToInt32(values[indOpcionId]);
            opcionInfo.OpcionPadreId = Convert.ToInt32(values[indOpcionPadreId]);
            opcionInfo.Nombre = Convert.ToString(values[indNombre]);
            opcionInfo.Activo = Convert.ToInt16(values[indActivo]);
            opcionInfo.Url = Convert.ToString(values[indUrl]);
            opcionInfo.Orden = Convert.ToInt32(values[indOrden]);
            #endregion

            return opcionInfo;
        }
    }
}