using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using APU.Entidad;
using APU.Herramientas;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace APU.AccesoDatos
{
    public class SurtidorAccesoDatos
    {
        public List<SurtidorInfo> Listar(int surtidorId)
        {
            var surtidorListaInfo = new List<SurtidorInfo>();
            using (var oConexion = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
            {
                var sqlComando = new SqlCommand();
                sqlComando.Connection = oConexion;
                sqlComando.CommandText = "ObtenerSurtidor";
                sqlComando.CommandType = CommandType.StoredProcedure;
                sqlComando.Parameters.Add("SurtidorId", SqlDbType.Int).Value = surtidorId;

                oConexion.Open();

                using (SqlDataReader dr = sqlComando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        surtidorListaInfo.Add(CargarSurtidorInfo(dr));
                    }
                }
                oConexion.Close();
            }
            return surtidorListaInfo;
        }
        public List<SurtidorInfo> ListarPaginado(int surtidorId, int tamanioPagina, int numeroPagina)
        {
            var surtidorListaInfo = new List<SurtidorInfo>();
            using (var oConexion = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
            {
                var sqlComando = new SqlCommand();
                sqlComando.Connection = oConexion;
                sqlComando.CommandText = "ObtenerSurtidorPaginado";
                sqlComando.CommandType = CommandType.StoredProcedure;
                sqlComando.Parameters.Add("SurtidorId", SqlDbType.Int).Value = surtidorId;
                sqlComando.Parameters.Add("TamanioPagina", SqlDbType.Int).Value = tamanioPagina;
                sqlComando.Parameters.Add("NumeroPagina", SqlDbType.Int).Value = numeroPagina;

                oConexion.Open();

                using (SqlDataReader dr = sqlComando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        surtidorListaInfo.Add(CargarSurtidorInfo(dr));
                    }
                }
                oConexion.Close();
            }
            return surtidorListaInfo;
        }
        private static SurtidorInfo CargarSurtidorInfo(IDataReader dr)
        {
            int colCount = dr.FieldCount;
            var values = new object[colCount];

            #region Indices
            int indSurtidorId = dr.GetOrdinal("SurtidorId");
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

            var surtidorInfo = new SurtidorInfo();
            dr.GetValues(values);

            #region Campos
            surtidorInfo.IslaId = Convert.ToInt32(values[indSurtidorId]);
            surtidorInfo.Nombre = Convert.ToString(values[indNombre]);
            surtidorInfo.Descripcion = Convert.ToString(values[indDescripcion]);
            surtidorInfo.Codigo = Convert.ToString(values[indCodigo]);
            surtidorInfo.Activo = Convert.ToInt32(values[indActivo]);
            surtidorInfo.UsuarioCreacionId = Convert.ToInt32(values[indUsuarioCreacionId]);
            surtidorInfo.FechaCreacion = Convert.ToDateTime(values[indFechaCreacion]);
            surtidorInfo.UsuarioModificacionId = Convert.ToInt32(values[indUsuarioModificacionId]);
            surtidorInfo.FechaModificacion = Convert.ToDateTime(values[indFechaModificacion]);
            surtidorInfo.NumeroFila = Convert.ToInt32(values[indNumeroFila]);
            surtidorInfo.TotalFilas = Convert.ToInt32(values[indTotalFilas]);
            #endregion

            return surtidorInfo;
        }
        public int Insertar(SurtidorInfo surtidorInfo)
        {
            int resultado;
            try
            {
                using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                {
                    var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "InsertarSurtidor", CommandType = CommandType.StoredProcedure };

                    sqlComando.Parameters.Add("Nombre", SqlDbType.VarChar).Value = surtidorInfo.Nombre;
                    sqlComando.Parameters.Add("Descripcion", SqlDbType.VarChar).Value = surtidorInfo.Descripcion;
                    sqlComando.Parameters.Add("Codigo", SqlDbType.VarChar).Value = surtidorInfo.Codigo;
                    sqlComando.Parameters.Add("Activo", SqlDbType.Int).Value = surtidorInfo.Activo;
                    sqlComando.Parameters.Add("UsuarioCreacionId", SqlDbType.Int).Value = surtidorInfo.UsuarioCreacionId;

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
        public int Actualizar(SurtidorInfo surtidorInfo)
        {
            int resultado = 0;
            try
            {
                using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                {
                    var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "ActualizarSurtidor", CommandType = CommandType.StoredProcedure };

                    sqlComando.Parameters.Add("SurtidorId", SqlDbType.Int).Value = surtidorInfo.IslaId;
                    sqlComando.Parameters.Add("Nombre", SqlDbType.VarChar).Value = surtidorInfo.Nombre;
                    sqlComando.Parameters.Add("Descripcion", SqlDbType.VarChar).Value = surtidorInfo.Descripcion;
                    sqlComando.Parameters.Add("Codigo", SqlDbType.VarChar).Value = surtidorInfo.Codigo;
                    sqlComando.Parameters.Add("Activo", SqlDbType.Int).Value = surtidorInfo.Activo;
                    sqlComando.Parameters.Add("UsuarioModificacionId", SqlDbType.Int).Value = surtidorInfo.UsuarioModificacionId;

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
        public int Eliminar(int surtidorId)
        {
            int resultado = 0;
            try
            {
                using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                {
                    var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "EliminarSurtidor", CommandType = CommandType.StoredProcedure };

                    sqlComando.Parameters.Add("SurtidorId", SqlDbType.Int).Value = surtidorId;

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