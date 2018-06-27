using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using APU.Entidad;
using APU.Herramientas;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace APU.AccesoDatos
{
    public class IslaAccesoDatos
    {
        public List<IslaInfo> Listar(int islaId)
        {
            var islaListaInfo = new List<IslaInfo>();
            using (var oConexion = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
            {
                var sqlComando = new SqlCommand();
                sqlComando.Connection = oConexion;
                sqlComando.CommandText = "ObtenerIsla";
                sqlComando.CommandType = CommandType.StoredProcedure;
                sqlComando.Parameters.Add("IslaId", SqlDbType.Int).Value = islaId;

                oConexion.Open();

                using (SqlDataReader dr = sqlComando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        islaListaInfo.Add(CargarIslaInfo(dr));
                    }
                }
                oConexion.Close();
            }
            return islaListaInfo;
        }
        public List<IslaInfo> ListarPaginado(int islaId, int tamanioPagina, int numeroPagina)
        {
            var islaListaInfo = new List<IslaInfo>();
            using (var oConexion = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
            {
                var sqlComando = new SqlCommand();
                sqlComando.Connection = oConexion;
                sqlComando.CommandText = "ObtenerIslaPaginado";
                sqlComando.CommandType = CommandType.StoredProcedure;
                sqlComando.Parameters.Add("IslaId", SqlDbType.Int).Value = islaId;
                sqlComando.Parameters.Add("TamanioPagina", SqlDbType.Int).Value = tamanioPagina;
                sqlComando.Parameters.Add("NumeroPagina", SqlDbType.Int).Value = numeroPagina;

                oConexion.Open();

                using (SqlDataReader dr = sqlComando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        islaListaInfo.Add(CargarIslaInfo(dr));
                    }
                }
                oConexion.Close();
            }
            return islaListaInfo;
        }
        private static IslaInfo CargarIslaInfo(IDataReader dr)
        {
            int colCount = dr.FieldCount;
            var values = new object[colCount];

            #region Indices
            int indIslaId = dr.GetOrdinal("IslaId");
            int indNombre = dr.GetOrdinal("Nombre");
            int indDescripcion = dr.GetOrdinal("Descripcion");
            int indCodigo = dr.GetOrdinal("Codigo");
            int indActivo = dr.GetOrdinal("Activo");
            int indUsuarioCreacionId = dr.GetOrdinal("UsuarioCreacionId");
            int indFechaCreacion = dr.GetOrdinal("FechaCreacion");
            int indUsuarioModificacionId = dr.GetOrdinal("UsuarioModificacionId");
            int indFechaModificacion = dr.GetOrdinal("FechaModificacion");
            int indNumeroFila = dr.GetOrdinal("NumeroFila");
            int indTotalFilas = dr.GetOrdinal("TotalFilas");
            #endregion

            var islaInfo = new IslaInfo();
            dr.GetValues(values);

            #region Campos
            islaInfo.IslaId = Convert.ToInt32(values[indIslaId]);
            islaInfo.Nombre = Convert.ToString(values[indNombre]);
            islaInfo.Descripcion = Convert.ToString(values[indDescripcion]);
            islaInfo.Codigo = Convert.ToString(values[indCodigo]);
            islaInfo.Activo = Convert.ToInt32(values[indActivo]);
            islaInfo.UsuarioCreacionId = Convert.ToInt32(values[indUsuarioCreacionId]);
            islaInfo.FechaCreacion = Convert.ToDateTime(values[indFechaCreacion]);
            islaInfo.UsuarioModificacionId = Convert.ToInt32(values[indUsuarioModificacionId]);
            islaInfo.FechaModificacion = Convert.ToDateTime(values[indFechaModificacion]);
            islaInfo.NumeroFila = Convert.ToInt32(values[indNumeroFila]);
            islaInfo.TotalFilas = Convert.ToInt32(values[indTotalFilas]);
            #endregion

            return islaInfo;
        }
        public int Insertar(IslaInfo islaInfo)
        {
            int resultado;
            try
            {
                using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                {
                    var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "InsertarIsla", CommandType = CommandType.StoredProcedure };

                    sqlComando.Parameters.Add("Nombre", SqlDbType.VarChar).Value = islaInfo.Nombre;
                    sqlComando.Parameters.Add("Descripcion", SqlDbType.VarChar).Value = islaInfo.Descripcion;
                    sqlComando.Parameters.Add("Codigo", SqlDbType.VarChar).Value = islaInfo.Codigo;
                    sqlComando.Parameters.Add("Activo", SqlDbType.Int).Value = islaInfo.Activo;
                    sqlComando.Parameters.Add("UsuarioCreacionId", SqlDbType.Int).Value = islaInfo.UsuarioCreacionId;

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
        public int Actualizar(IslaInfo islaInfo)
        {
            int resultado = 0;
            try
            {
                using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                {
                    var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "ActualizarIsla", CommandType = CommandType.StoredProcedure };

                    sqlComando.Parameters.Add("IslaId", SqlDbType.Int).Value = islaInfo.IslaId;
                    sqlComando.Parameters.Add("Nombre", SqlDbType.VarChar).Value = islaInfo.Nombre;
                    sqlComando.Parameters.Add("Descripcion", SqlDbType.VarChar).Value = islaInfo.Descripcion;
                    sqlComando.Parameters.Add("Codigo", SqlDbType.VarChar).Value = islaInfo.Codigo;
                    sqlComando.Parameters.Add("Activo", SqlDbType.Int).Value = islaInfo.Activo;
                    sqlComando.Parameters.Add("UsuarioModificacionId", SqlDbType.Int).Value = islaInfo.UsuarioModificacionId;

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
        public int Eliminar(int islaId)
        {
            int resultado = 0;
            try
            {
                using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                {
                    var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "EliminarIsla", CommandType = CommandType.StoredProcedure };

                    sqlComando.Parameters.Add("IslaId", SqlDbType.Int).Value = islaId;

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