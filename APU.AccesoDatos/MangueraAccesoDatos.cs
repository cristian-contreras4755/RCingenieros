using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using APU.Entidad;
using APU.Herramientas;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace APU.AccesoDatos
{
    public class MangueraAccesoDatos
    {
        public List<MangueraInfo> Listar(int mangueraId)
        {
            var mangueraListaInfo = new List<MangueraInfo>();
            using (var oConexion = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
            {
                var sqlComando = new SqlCommand();
                sqlComando.Connection = oConexion;
                sqlComando.CommandText = "ObtenerManguera";
                sqlComando.CommandType = CommandType.StoredProcedure;
                sqlComando.Parameters.Add("MangueraId", SqlDbType.Int).Value = mangueraId;

                oConexion.Open();

                using (SqlDataReader dr = sqlComando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        mangueraListaInfo.Add(CargarMangueraInfo(dr));
                    }
                }
                oConexion.Close();
            }
            return mangueraListaInfo;
        }
        public List<MangueraInfo> ListarPaginado(int mangueraId, int tamanioPagina, int numeroPagina)
        {
            var mangueraListaInfo = new List<MangueraInfo>();
            using (var oConexion = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
            {
                var sqlComando = new SqlCommand();
                sqlComando.Connection = oConexion;
                sqlComando.CommandText = "ObtenerMangueraPaginado";
                sqlComando.CommandType = CommandType.StoredProcedure;
                sqlComando.Parameters.Add("MangueraId", SqlDbType.Int).Value = mangueraId;
                sqlComando.Parameters.Add("TamanioPagina", SqlDbType.Int).Value = tamanioPagina;
                sqlComando.Parameters.Add("NumeroPagina", SqlDbType.Int).Value = numeroPagina;

                oConexion.Open();

                using (SqlDataReader dr = sqlComando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        mangueraListaInfo.Add(CargarMangueraInfo(dr));
                    }
                }
                oConexion.Close();
            }
            return mangueraListaInfo;
        }
        private static MangueraInfo CargarMangueraInfo(IDataReader dr)
        {
            int colCount = dr.FieldCount;
            var values = new object[colCount];

            #region Indices
            int indMangueraId = dr.GetOrdinal("MangueraId");
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

            var mangueraInfo = new MangueraInfo();
            dr.GetValues(values);

            #region Campos
            mangueraInfo.MangueraId = Convert.ToInt32(values[indMangueraId]);
            mangueraInfo.Nombre = Convert.ToString(values[indNombre]);
            mangueraInfo.Descripcion = Convert.ToString(values[indDescripcion]);
            mangueraInfo.Codigo = Convert.ToString(values[indCodigo]);
            mangueraInfo.Activo = Convert.ToInt32(values[indActivo]);
            mangueraInfo.UsuarioCreacionId = Convert.ToInt32(values[indUsuarioCreacionId]);
            mangueraInfo.FechaCreacion = Convert.ToDateTime(values[indFechaCreacion]);
            mangueraInfo.UsuarioModificacionId = Convert.ToInt32(values[indUsuarioModificacionId]);
            mangueraInfo.FechaModificacion = Convert.ToDateTime(values[indFechaModificacion]);
            mangueraInfo.NumeroFila = Convert.ToInt32(values[indNumeroFila]);
            mangueraInfo.TotalFilas = Convert.ToInt32(values[indTotalFilas]);
            #endregion

            return mangueraInfo;
        }
        public int Insertar(MangueraInfo mangueraInfo)
        {
            int resultado;
            try
            {
                using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                {
                    var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "InsertarManguera", CommandType = CommandType.StoredProcedure };

                    sqlComando.Parameters.Add("Nombre", SqlDbType.VarChar).Value = mangueraInfo.Nombre;
                    sqlComando.Parameters.Add("Descripcion", SqlDbType.VarChar).Value = mangueraInfo.Descripcion;
                    sqlComando.Parameters.Add("Codigo", SqlDbType.VarChar).Value = mangueraInfo.Codigo;
                    sqlComando.Parameters.Add("Activo", SqlDbType.Int).Value = mangueraInfo.Activo;
                    sqlComando.Parameters.Add("UsuarioCreacionId", SqlDbType.Int).Value = mangueraInfo.UsuarioCreacionId;

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
        public int Actualizar(MangueraInfo mangueraInfo)
        {
            int resultado = 0;
            try
            {
                using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                {
                    var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "ActualizarManguera", CommandType = CommandType.StoredProcedure };

                    sqlComando.Parameters.Add("MangueraId", SqlDbType.Int).Value = mangueraInfo.CaraId;
                    sqlComando.Parameters.Add("Nombre", SqlDbType.VarChar).Value = mangueraInfo.Nombre;
                    sqlComando.Parameters.Add("Descripcion", SqlDbType.VarChar).Value = mangueraInfo.Descripcion;
                    sqlComando.Parameters.Add("Codigo", SqlDbType.VarChar).Value = mangueraInfo.Codigo;
                    sqlComando.Parameters.Add("Activo", SqlDbType.Int).Value = mangueraInfo.Activo;
                    sqlComando.Parameters.Add("UsuarioModificacionId", SqlDbType.Int).Value = mangueraInfo.UsuarioModificacionId;

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
        public int Eliminar(int mangueraId)
        {
            int resultado = 0;
            try
            {
                using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                {
                    var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "EliminarManguera", CommandType = CommandType.StoredProcedure };

                    sqlComando.Parameters.Add("MangueraId", SqlDbType.Int).Value = mangueraId;

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