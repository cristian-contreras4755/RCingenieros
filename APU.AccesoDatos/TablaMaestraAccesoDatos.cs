using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APU.Entidad;
using APU.Herramientas;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace APU.AccesoDatos
{
    public class TablaMaestraAccesoDatos
    {
        public List<TablaMaestraInfo> Listar(int tablaMaestraId, int tablaId)
        {
            var tablaMaestraListaInfo = new List<TablaMaestraInfo>();
            using (var oConexion = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
            {
                var sqlComando = new SqlCommand();
                sqlComando.Connection = oConexion;
                sqlComando.CommandText = "ObtenerTablaMaestra";
                sqlComando.CommandType = CommandType.StoredProcedure;
                sqlComando.Parameters.Add("TablaMaestraId", SqlDbType.Int).Value = tablaMaestraId;
                sqlComando.Parameters.Add("TablaId", SqlDbType.Int).Value = tablaId;

                oConexion.Open();

                using (SqlDataReader dr = sqlComando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        tablaMaestraListaInfo.Add(CargarTablaMaestraInfo(dr));
                    }
                }
                oConexion.Close();
            }
            return tablaMaestraListaInfo;
        }
        public List<TablaMaestraInfo> ListarPaginado(int tablaMaestraId, int tablaId, int tamanioPagina, int numeroPagina)
        {
            var tablaMaestraListaInfo = new List<TablaMaestraInfo>();
            using (var oConexion = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
            {
                var sqlComando = new SqlCommand();
                sqlComando.Connection = oConexion;
                sqlComando.CommandText = "ObtenerTablaMaestraPaginado";
                sqlComando.CommandType = CommandType.StoredProcedure;
                sqlComando.Parameters.Add("TablaMaestraId", SqlDbType.Int).Value = tablaMaestraId;
                sqlComando.Parameters.Add("TablaId", SqlDbType.Int).Value = tablaId;
                sqlComando.Parameters.Add("TamanioPagina", SqlDbType.Int).Value = tamanioPagina;
                sqlComando.Parameters.Add("NumeroPagina", SqlDbType.Int).Value = numeroPagina;

                oConexion.Open();

                using (SqlDataReader dr = sqlComando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        tablaMaestraListaInfo.Add(CargarTablaMaestraInfo(dr));
                    }
                }
                oConexion.Close();
            }
            return tablaMaestraListaInfo;
        }
        private static TablaMaestraInfo CargarTablaMaestraInfo(IDataReader dr)
        {
            int colCount = dr.FieldCount;
            var values = new object[colCount];

            #region Indices
            int indTablaMaestraId = dr.GetOrdinal("TablaMaestraId");
            int indTablaId = dr.GetOrdinal("TablaId");
            int indCodigo = dr.GetOrdinal("Codigo");
            int indNombreCorto = dr.GetOrdinal("NombreCorto");
            int indNombreLargo = dr.GetOrdinal("NombreLargo");
            int indActivo = dr.GetOrdinal("Activo");
            int indEditable = dr.GetOrdinal("Editable");
            int indUsuarioCreacionId = dr.GetOrdinal("UsuarioCreacionId");
            int indUsuarioCreacion = dr.GetOrdinal("UsuarioCreacion");
            int indFechaCreacion = dr.GetOrdinal("FechaCreacion");
            int indUsuarioModificacionId = dr.GetOrdinal("UsuarioModificacionId");
            int indUsuarioModificacion = dr.GetOrdinal("UsuarioModificacion");
            int indFechaModificacion = dr.GetOrdinal("FechaModificacion");
            #endregion

            var tablaMaestraInfo = new TablaMaestraInfo();
            dr.GetValues(values);

            #region Campos
            tablaMaestraInfo.TablaMaestraId = Convert.ToInt32(values[indTablaMaestraId]);
            tablaMaestraInfo.TablaId = Convert.ToInt32(values[indTablaId]);
            tablaMaestraInfo.Codigo = Convert.ToString(values[indCodigo]);
            tablaMaestraInfo.NombreCorto = Convert.ToString(values[indNombreCorto]);
            tablaMaestraInfo.NombreLargo = Convert.ToString(values[indNombreLargo]);
            tablaMaestraInfo.Activo = Convert.ToInt32(values[indActivo]);
            tablaMaestraInfo.Editable = Convert.ToInt32(values[indEditable]);
            tablaMaestraInfo.UsuarioCreacionId = Convert.ToInt32(values[indUsuarioCreacionId]);
            tablaMaestraInfo.UsuarioCreacion = Convert.ToString(values[indUsuarioCreacion]);
            tablaMaestraInfo.FechaCreacion = Convert.ToDateTime(values[indFechaCreacion]);
            tablaMaestraInfo.UsuarioModificacionId = Convert.ToInt32(values[indUsuarioModificacionId]);
            tablaMaestraInfo.UsuarioModificacion = Convert.ToString(values[indUsuarioModificacion]);
            if (values[indFechaModificacion] != DBNull.Value) tablaMaestraInfo.FechaModificacion = Convert.ToDateTime(values[indFechaModificacion]);
            #endregion

            return tablaMaestraInfo;
        }
        public int Insertar(TablaMaestraInfo tablaMaestraInfo)
        {
            int resultado;
            try
            {
                using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                {
                    var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "InsertarTablaMaestra", CommandType = CommandType.StoredProcedure };

                    sqlComando.Parameters.Add("TablaId", SqlDbType.Int).Value = tablaMaestraInfo.TablaId;
                    sqlComando.Parameters.Add("Codigo", SqlDbType.VarChar).Value = tablaMaestraInfo.Codigo;
                    sqlComando.Parameters.Add("NombreCorto", SqlDbType.VarChar).Value = tablaMaestraInfo.NombreCorto;
                    sqlComando.Parameters.Add("NombreLargo", SqlDbType.VarChar).Value = tablaMaestraInfo.NombreLargo;
                    sqlComando.Parameters.Add("Editable", SqlDbType.Int).Value = tablaMaestraInfo.Editable;
                    sqlComando.Parameters.Add("Activo", SqlDbType.Int).Value = tablaMaestraInfo.Activo;

                    sqlComando.Parameters.Add("UsuarioCreacionId", SqlDbType.Int).Value = tablaMaestraInfo.UsuarioModificacionId;

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
        public int Actualizar(TablaMaestraInfo tablaMaestraInfo)
        {
            int resultado = 0;
            try
            {
                using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                {
                    var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "ActualizarTablaMaestra", CommandType = CommandType.StoredProcedure };

                    sqlComando.Parameters.Add("TablaMaestraId", SqlDbType.Int).Value = tablaMaestraInfo.TablaMaestraId;
                    sqlComando.Parameters.Add("Codigo", SqlDbType.VarChar).Value = tablaMaestraInfo.Codigo;
                    sqlComando.Parameters.Add("NombreCorto", SqlDbType.VarChar).Value = tablaMaestraInfo.NombreCorto;
                    sqlComando.Parameters.Add("NombreLargo", SqlDbType.VarChar).Value = tablaMaestraInfo.NombreLargo;
                    sqlComando.Parameters.Add("Editable", SqlDbType.Int).Value = tablaMaestraInfo.Editable;
                    sqlComando.Parameters.Add("Activo", SqlDbType.Int).Value = tablaMaestraInfo.Activo;
                    sqlComando.Parameters.Add("UsuarioModificacionId", SqlDbType.Int).Value = tablaMaestraInfo.UsuarioModificacionId;

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
        public int Eliminar(int perfilId)
        {
            int resultado = 0;
            try
            {
                using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                {
                    var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "EliminarPerfil", CommandType = CommandType.StoredProcedure };

                    sqlComando.Parameters.Add("PerfilId", SqlDbType.Int).Value = perfilId;

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