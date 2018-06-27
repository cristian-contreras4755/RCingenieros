using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using APU.Entidad;
using APU.Herramientas;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace APU.AccesoDatos
{
    public class TanqueAccesoDatos
    {
        public List<TanqueInfo> Listar(int tanqueId)
        {
            var tanqueListaInfo = new List<TanqueInfo>();
            using (var oConexion = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
            {
                var sqlComando = new SqlCommand();
                sqlComando.Connection = oConexion;
                sqlComando.CommandText = "ObtenerTanque";
                sqlComando.CommandType = CommandType.StoredProcedure;
                sqlComando.Parameters.Add("TanqueId", SqlDbType.Int).Value = tanqueId;

                oConexion.Open();

                using (SqlDataReader dr = sqlComando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        tanqueListaInfo.Add(CargarTanqueInfo(dr));
                    }
                }
                oConexion.Close();
            }
            return tanqueListaInfo;
        }
        public List<TanqueInfo> ListarPaginado(int tanqueId, int tamanioPagina, int numeroPagina)
        {
            var tanqueListaInfo = new List<TanqueInfo>();
            using (var oConexion = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
            {
                var sqlComando = new SqlCommand();
                sqlComando.Connection = oConexion;
                sqlComando.CommandText = "ObtenerTanquePaginado";
                sqlComando.CommandType = CommandType.StoredProcedure;
                sqlComando.Parameters.Add("TanqueId", SqlDbType.Int).Value = tanqueId;
                sqlComando.Parameters.Add("TamanioPagina", SqlDbType.Int).Value = tamanioPagina;
                sqlComando.Parameters.Add("NumeroPagina", SqlDbType.Int).Value = numeroPagina;

                oConexion.Open();

                using (SqlDataReader dr = sqlComando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        tanqueListaInfo.Add(CargarTanqueInfo(dr));
                    }
                }
                oConexion.Close();
            }
            return tanqueListaInfo;
        }
        private static TanqueInfo CargarTanqueInfo(IDataReader dr)
        {
            int colCount = dr.FieldCount;
            var values = new object[colCount];

            #region Indices
            int indTanqueId = dr.GetOrdinal("TanqueId");
            int indNombre = dr.GetOrdinal("Nombre");
            int indDescripcion = dr.GetOrdinal("Descripcion");
            int indCodigo = dr.GetOrdinal("Codigo");
            int indCapacidad = dr.GetOrdinal("Capacidad");
            int indStock = dr.GetOrdinal("Stock");
            int indActivo = dr.GetOrdinal("Activo");
            int indUsuarioCreacionId = dr.GetOrdinal("UsuarioCreacionId");
            int indFechaCreacion = dr.GetOrdinal("FechaCreacion");
            int indUsuarioModificacionId = dr.GetOrdinal("UsuarioModificacionId");
            int indFechaModificacion = dr.GetOrdinal("FechaModificacion");
            int indNumeroFila = dr.GetOrdinal("NumeroFila");
            int indTotalFilas = dr.GetOrdinal("TotalFilas");
            #endregion

            var tanqueInfo = new TanqueInfo();
            dr.GetValues(values);

            #region Campos
            tanqueInfo.TanqueId = Convert.ToInt32(values[indTanqueId]);
            tanqueInfo.Nombre = Convert.ToString(values[indNombre]);
            tanqueInfo.Descripcion = Convert.ToString(values[indDescripcion]);
            tanqueInfo.Codigo = Convert.ToString(values[indCodigo]);
            tanqueInfo.Capacidad = Convert.ToDecimal(values[indCapacidad]);
            tanqueInfo.Stock = Convert.ToDecimal(values[indStock]);
            tanqueInfo.Activo = Convert.ToInt32(values[indActivo]);
            tanqueInfo.UsuarioCreacionId = Convert.ToInt32(values[indUsuarioCreacionId]);
            tanqueInfo.FechaCreacion = Convert.ToDateTime(values[indFechaCreacion]);
            tanqueInfo.UsuarioModificacionId = Convert.ToInt32(values[indUsuarioModificacionId]);
            tanqueInfo.FechaModificacion = Convert.ToDateTime(values[indFechaModificacion]);
            tanqueInfo.NumeroFila = Convert.ToInt32(values[indNumeroFila]);
            tanqueInfo.TotalFilas = Convert.ToInt32(values[indTotalFilas]);
            #endregion

            return tanqueInfo;
        }
        public int Insertar(TanqueInfo tanqueInfo)
        {
            int resultado;
            try
            {
                using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                {
                    var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "InsertarTanque", CommandType = CommandType.StoredProcedure };

                    sqlComando.Parameters.Add("Nombre", SqlDbType.VarChar).Value = tanqueInfo.Nombre;
                    sqlComando.Parameters.Add("Descripcion", SqlDbType.VarChar).Value = tanqueInfo.Descripcion;
                    sqlComando.Parameters.Add("Codigo", SqlDbType.VarChar).Value = tanqueInfo.Codigo;
                    sqlComando.Parameters.Add("Capacidad", SqlDbType.Decimal).Value = tanqueInfo.Capacidad;
                    sqlComando.Parameters.Add("Stock", SqlDbType.Decimal).Value = tanqueInfo.Stock;
                    sqlComando.Parameters.Add("Activo", SqlDbType.Int).Value = tanqueInfo.Activo;
                    sqlComando.Parameters.Add("UsuarioCreacionId", SqlDbType.Int).Value = tanqueInfo.UsuarioCreacionId;

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
        public int Actualizar(TanqueInfo tanqueInfo)
        {
            int resultado = 0;
            try
            {
                using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                {
                    var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "ActualizarTanque", CommandType = CommandType.StoredProcedure };

                    sqlComando.Parameters.Add("TanqueId", SqlDbType.Int).Value = tanqueInfo.TanqueId;
                    sqlComando.Parameters.Add("Nombre", SqlDbType.VarChar).Value = tanqueInfo.Nombre;
                    sqlComando.Parameters.Add("Descripcion", SqlDbType.VarChar).Value = tanqueInfo.Descripcion;
                    sqlComando.Parameters.Add("Codigo", SqlDbType.VarChar).Value = tanqueInfo.Codigo;
                    sqlComando.Parameters.Add("Capacidad", SqlDbType.Decimal).Value = tanqueInfo.Capacidad;
                    sqlComando.Parameters.Add("Stock", SqlDbType.Decimal).Value = tanqueInfo.Stock;
                    sqlComando.Parameters.Add("Activo", SqlDbType.Int).Value = tanqueInfo.Activo;
                    sqlComando.Parameters.Add("UsuarioModificacionId", SqlDbType.Int).Value = tanqueInfo.UsuarioModificacionId;

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
        public int Eliminar(int tanqueId)
        {
            int resultado = 0;
            try
            {
                using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                {
                    var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "EliminarTanque", CommandType = CommandType.StoredProcedure };

                    sqlComando.Parameters.Add("TanqueId", SqlDbType.Int).Value = tanqueId;

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