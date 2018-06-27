using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using APU.Entidad;
using APU.Herramientas;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace APU.AccesoDatos
{
    public class CaraAccesoDatos
    {
        public List<CaraInfo> Listar(int caraId)
        {
            var caraListaInfo = new List<CaraInfo>();
            using (var oConexion = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
            {
                var sqlComando = new SqlCommand();
                sqlComando.Connection = oConexion;
                sqlComando.CommandText = "ObtenerCara";
                sqlComando.CommandType = CommandType.StoredProcedure;
                sqlComando.Parameters.Add("CaraId", SqlDbType.Int).Value = caraId;

                oConexion.Open();

                using (SqlDataReader dr = sqlComando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        caraListaInfo.Add(CargarCaraInfo(dr));
                    }
                }
                oConexion.Close();
            }
            return caraListaInfo;
        }
        public List<CaraInfo> ListarPaginado(int caraId, int tamanioPagina, int numeroPagina)
        {
            var caraListaInfo = new List<CaraInfo>();
            using (var oConexion = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
            {
                var sqlComando = new SqlCommand();
                sqlComando.Connection = oConexion;
                sqlComando.CommandText = "ObtenerCaraPaginado";
                sqlComando.CommandType = CommandType.StoredProcedure;
                sqlComando.Parameters.Add("CaraId", SqlDbType.Int).Value = caraId;
                sqlComando.Parameters.Add("TamanioPagina", SqlDbType.Int).Value = tamanioPagina;
                sqlComando.Parameters.Add("NumeroPagina", SqlDbType.Int).Value = numeroPagina;

                oConexion.Open();

                using (SqlDataReader dr = sqlComando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        caraListaInfo.Add(CargarCaraInfo(dr));
                    }
                }
                oConexion.Close();
            }
            return caraListaInfo;
        }
        private static CaraInfo CargarCaraInfo(IDataReader dr)
        {
            int colCount = dr.FieldCount;
            var values = new object[colCount];

            #region Indices
            int indCaraId = dr.GetOrdinal("CaraId");
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

            var caraInfo = new CaraInfo();
            dr.GetValues(values);

            #region Campos
            caraInfo.CaraId = Convert.ToInt32(values[indCaraId]);
            caraInfo.Nombre = Convert.ToString(values[indNombre]);
            caraInfo.Descripcion = Convert.ToString(values[indDescripcion]);
            caraInfo.Codigo = Convert.ToString(values[indCodigo]);
            caraInfo.Activo = Convert.ToInt32(values[indActivo]);
            caraInfo.UsuarioCreacionId = Convert.ToInt32(values[indUsuarioCreacionId]);
            caraInfo.FechaCreacion = Convert.ToDateTime(values[indFechaCreacion]);
            caraInfo.UsuarioModificacionId = Convert.ToInt32(values[indUsuarioModificacionId]);
            caraInfo.FechaModificacion = Convert.ToDateTime(values[indFechaModificacion]);
            caraInfo.NumeroFila = Convert.ToInt32(values[indNumeroFila]);
            caraInfo.TotalFilas = Convert.ToInt32(values[indTotalFilas]);
            #endregion

            return caraInfo;
        }
        public int Insertar(CaraInfo caraInfo)
        {
            int resultado;
            try
            {
                using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                {
                    var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "InsertarCara", CommandType = CommandType.StoredProcedure };

                    sqlComando.Parameters.Add("Nombre", SqlDbType.VarChar).Value = caraInfo.Nombre;
                    sqlComando.Parameters.Add("Descripcion", SqlDbType.VarChar).Value = caraInfo.Descripcion;
                    sqlComando.Parameters.Add("Codigo", SqlDbType.VarChar).Value = caraInfo.Codigo;
                    sqlComando.Parameters.Add("Activo", SqlDbType.Int).Value = caraInfo.Activo;
                    sqlComando.Parameters.Add("UsuarioCreacionId", SqlDbType.Int).Value = caraInfo.UsuarioCreacionId;

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
        public int Actualizar(CaraInfo caraInfo)
        {
            int resultado = 0;
            try
            {
                using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                {
                    var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "ActualizarCara", CommandType = CommandType.StoredProcedure };

                    sqlComando.Parameters.Add("CaraId", SqlDbType.Int).Value = caraInfo.CaraId;
                    sqlComando.Parameters.Add("Nombre", SqlDbType.VarChar).Value = caraInfo.Nombre;
                    sqlComando.Parameters.Add("Descripcion", SqlDbType.VarChar).Value = caraInfo.Descripcion;
                    sqlComando.Parameters.Add("Codigo", SqlDbType.VarChar).Value = caraInfo.Codigo;
                    sqlComando.Parameters.Add("Activo", SqlDbType.Int).Value = caraInfo.Activo;
                    sqlComando.Parameters.Add("UsuarioModificacionId", SqlDbType.Int).Value = caraInfo.UsuarioModificacionId;

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
        public int Eliminar(int caraId)
        {
            int resultado = 0;
            try
            {
                using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                {
                    var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "EliminarCara", CommandType = CommandType.StoredProcedure };

                    sqlComando.Parameters.Add("CaraId", SqlDbType.Int).Value = caraId;

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