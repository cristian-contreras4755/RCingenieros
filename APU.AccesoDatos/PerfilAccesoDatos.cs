using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using APU.Entidad;
using APU.Herramientas;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace APU.AccesoDatos
{
    public class PerfilAccesoDatos
    {
        public List<PerfilInfo> Listar(int perfilId)
        {
            var perfilListaInfo = new List<PerfilInfo>();
            using (var oConexion = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
            {
                var sqlComando = new SqlCommand();
                sqlComando.Connection = oConexion;
                sqlComando.CommandText = "ObtenerPerfil";
                sqlComando.CommandType = CommandType.StoredProcedure;
                sqlComando.Parameters.Add("PerfilId", SqlDbType.Int).Value = perfilId;

                oConexion.Open();

                using (SqlDataReader dr = sqlComando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        perfilListaInfo.Add(CargarPerfilInfo(dr));
                    }
                }
                oConexion.Close();
            }
            return perfilListaInfo;
        }
        public List<PerfilInfo> ListarPaginado(int perfilId, int tamanioPagina, int numeroPagina)
        {
            var perfilListaInfo = new List<PerfilInfo>();
            using (var oConexion = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
            {
                var sqlComando = new SqlCommand();
                sqlComando.Connection = oConexion;
                sqlComando.CommandText = "ObtenerPerfilPaginado";
                sqlComando.CommandType = CommandType.StoredProcedure;
                sqlComando.Parameters.Add("PerfilId", SqlDbType.Int).Value = perfilId;
                sqlComando.Parameters.Add("TamanioPagina", SqlDbType.Int).Value = tamanioPagina;
                sqlComando.Parameters.Add("NumeroPagina", SqlDbType.Int).Value = numeroPagina;

                oConexion.Open();

                using (SqlDataReader dr = sqlComando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        perfilListaInfo.Add(CargarPerfilInfo(dr));
                    }
                }
                oConexion.Close();
            }
            return perfilListaInfo;
        }
        private static PerfilInfo CargarPerfilInfo(IDataReader dr)
        {
            int colCount = dr.FieldCount;
            var values = new object[colCount];

            #region Indices
            int indPerfilId = dr.GetOrdinal("PerfilId");
            int indPerfil = dr.GetOrdinal("Perfil");
            int indActivo = dr.GetOrdinal("Activo");
            int indOpcionInicioId = dr.GetOrdinal("OpcionInicioId");
            int indUsuarioCreacionId = dr.GetOrdinal("UsuarioCreacionId");
            int indFechaCreacion = dr.GetOrdinal("FechaCreacion");
            int indUsuarioModificacionId = dr.GetOrdinal("UsuarioModificacionId");
            int indUsuarioModificacion = dr.GetOrdinal("UsuarioModificacion");
            int indFechaModificacion = dr.GetOrdinal("FechaModificacion");
            int indNumeroFila = dr.GetOrdinal("NumeroFila");
            int indTotalFilas = dr.GetOrdinal("TotalFilas");
            #endregion

            var perfilInfo = new PerfilInfo();
            dr.GetValues(values);

            #region Campos
            perfilInfo.PerfilId = Convert.ToInt32(values[indPerfilId]);
            perfilInfo.Perfil = Convert.ToString(values[indPerfil]);
            perfilInfo.Activo = Convert.ToInt32(values[indActivo]);
            perfilInfo.OpcionInicioId = Convert.ToInt32(values[indOpcionInicioId]);
            perfilInfo.UsuarioCreacionId = Convert.ToInt32(values[indUsuarioCreacionId]);
            perfilInfo.FechaCreacion = Convert.ToDateTime(values[indFechaCreacion]);
            perfilInfo.UsuarioModificacionId = Convert.ToInt32(values[indUsuarioModificacionId]);
            perfilInfo.UsuarioModificacion = Convert.ToString(values[indUsuarioModificacion]);
            if (values[indFechaModificacion] != DBNull.Value) perfilInfo.FechaModificacion = Convert.ToDateTime(values[indFechaModificacion]);

            perfilInfo.NumeroFila = Convert.ToInt32(values[indNumeroFila]);
            perfilInfo.TotalFilas = Convert.ToInt32(values[indTotalFilas]);
            #endregion

            return perfilInfo;
        }
        public int Insertar(PerfilInfo perfilInfo)
        {
            int resultado;
            try
            {
                using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                {
                    var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "InsertarPerfil", CommandType = CommandType.StoredProcedure };

                    sqlComando.Parameters.Add("Nombre", SqlDbType.VarChar).Value = perfilInfo.Perfil;
                    sqlComando.Parameters.Add("OpcionInicioId", SqlDbType.Int).Value = perfilInfo.OpcionInicioId;
                    sqlComando.Parameters.Add("Activo", SqlDbType.Int).Value = perfilInfo.Activo;
                    sqlComando.Parameters.Add("UsuarioCreacionId", SqlDbType.Int).Value = perfilInfo.UsuarioCreacionId;

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
        public int Actualizar(PerfilInfo perfilInfo)
        {
            int resultado = 0;
            try
            {
                using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                {
                    var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "ActualizarPerfil", CommandType = CommandType.StoredProcedure };

                    sqlComando.Parameters.Add("PerfilId", SqlDbType.Int).Value = perfilInfo.PerfilId;
                    sqlComando.Parameters.Add("Nombre", SqlDbType.VarChar).Value = perfilInfo.Perfil;
                    sqlComando.Parameters.Add("OpcionInicioId", SqlDbType.Int).Value = perfilInfo.OpcionInicioId;
                    sqlComando.Parameters.Add("Activo", SqlDbType.Int).Value = perfilInfo.Activo;
                    sqlComando.Parameters.Add("UsuarioModificacionId", SqlDbType.Int).Value = perfilInfo.UsuarioModificacionId;

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
