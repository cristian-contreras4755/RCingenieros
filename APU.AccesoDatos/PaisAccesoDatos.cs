using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using APU.Entidad;
using APU.Herramientas;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace APU.AccesoDatos
{
    public class PaisAccesoDatos
    {
        public List<PaisInfo> Listar(int paisId)
        {
            var paisListaInfo = new List<PaisInfo>();
            using (var oConexion = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
            {
                var sqlComando = new SqlCommand();
                sqlComando.Connection = oConexion;
                sqlComando.CommandText = "ObtenerPais";
                sqlComando.CommandType = CommandType.StoredProcedure;
                sqlComando.Parameters.Add("PaisId", SqlDbType.Int).Value = paisId;

                oConexion.Open();

                using (SqlDataReader dr = sqlComando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        paisListaInfo.Add(CargarPaisInfo(dr));
                    }
                }
                oConexion.Close();
            }
            return paisListaInfo;
        }
        public List<PaisInfo> ListarPaginado(int paisId, int tamanioPagina, int numeroPagina)
        {
            var paisListaInfo = new List<PaisInfo>();
            using (var oConexion = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
            {
                var sqlComando = new SqlCommand();
                sqlComando.Connection = oConexion;
                sqlComando.CommandText = "ObtenerPaisPaginado";
                sqlComando.CommandType = CommandType.StoredProcedure;
                sqlComando.Parameters.Add("PaisId", SqlDbType.Int).Value = paisId;
                sqlComando.Parameters.Add("TamanioPagina", SqlDbType.Int).Value = tamanioPagina;
                sqlComando.Parameters.Add("NumeroPagina", SqlDbType.Int).Value = numeroPagina;

                oConexion.Open();

                using (SqlDataReader dr = sqlComando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        paisListaInfo.Add(CargarPaisInfo(dr));
                    }
                }
                oConexion.Close();
            }
            return paisListaInfo;
        }
        private static PaisInfo CargarPaisInfo(IDataReader dr)
        {
            int colCount = dr.FieldCount;
            var values = new object[colCount];

            #region Indices
            int indPaisId = dr.GetOrdinal("PaisId");
            int indCodigoPais = dr.GetOrdinal("CodigoPais");
            int indDescripcion = dr.GetOrdinal("Descripcion");
            int indActivo = dr.GetOrdinal("Activo");
            int indUsuarioCreacionId = dr.GetOrdinal("UsuarioCreacionId");
            int indFechaCreacion = dr.GetOrdinal("FechaCreacion");
            int indUsuarioModificacionId = dr.GetOrdinal("UsuarioModificacionId");
            int indFechaModificacion = dr.GetOrdinal("FechaModificacion");
            int indNumeroFila = dr.GetOrdinal("NumeroFila");
            int indTotalFilas = dr.GetOrdinal("TotalFilas");
            #endregion

            var paisInfo = new PaisInfo();
            dr.GetValues(values);

            #region Campos
            paisInfo.PaisId = Convert.ToInt32(values[indPaisId]);
            paisInfo.CodigoPais = Convert.ToString(values[indCodigoPais]);
            paisInfo.Descripcion = Convert.ToString(values[indDescripcion]);
            paisInfo.Activo = Convert.ToInt32(values[indActivo]);
            paisInfo.UsuarioCreacionId = Convert.ToInt32(values[indUsuarioCreacionId]);
            paisInfo.FechaCreacion = Convert.ToDateTime(values[indFechaCreacion]);
            if (values[indUsuarioModificacionId] != DBNull.Value) paisInfo.UsuarioModificacionId = Convert.ToInt32(values[indUsuarioModificacionId]);
            if (values[indFechaModificacion] != DBNull.Value) paisInfo.FechaModificacion = Convert.ToDateTime(values[indFechaModificacion]);
            paisInfo.NumeroFila = Convert.ToInt32(values[indNumeroFila]);
            paisInfo.TotalFilas = Convert.ToInt32(values[indTotalFilas]);
            #endregion

            return paisInfo;
        }
        public int Insertar(PaisInfo paisInfo)
        {
            int resultado;
            try
            {
                using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                {
                    var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "InsertarPais", CommandType = CommandType.StoredProcedure };

                    sqlComando.Parameters.Add("EmpresaId", SqlDbType.Int).Value = paisInfo.PaisId;
                    //sqlComando.Parameters.Add("TipoDocumentoId", SqlDbType.Int).Value = paisInfo.TipoDocumentoId;
                    //sqlComando.Parameters.Add("NumeroDocumento", SqlDbType.VarChar).Value = paisInfo.NumeroDocumento;
                    sqlComando.Parameters.Add("Descripcion", SqlDbType.VarChar).Value = paisInfo.Descripcion;
                    //sqlComando.Parameters.Add("RazonSocial", SqlDbType.VarChar).Value = paisInfo.RazonSocial;
                    //sqlComando.Parameters.Add("Ciiu", SqlDbType.VarChar).Value = paisInfo.Ciiu;
                    //sqlComando.Parameters.Add("Direccion", SqlDbType.VarChar).Value = paisInfo.Direccion;
                    sqlComando.Parameters.Add("Activo", SqlDbType.Int).Value = paisInfo.Activo;
                    sqlComando.Parameters.Add("UsuarioCreacionId", SqlDbType.Int).Value = paisInfo.UsuarioCreacionId;

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
        public int Actualizar(EmpresaInfo empresaInfo)
        {
            int resultado = 0;
            try
            {
                using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                {
                    var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "ActualizarEmpresa", CommandType = CommandType.StoredProcedure };

                    sqlComando.Parameters.Add("EmpresaId", SqlDbType.Int).Value = empresaInfo.EmpresaId;
                    sqlComando.Parameters.Add("TipoDocumentoId", SqlDbType.Int).Value = empresaInfo.TipoDocumentoId;
                    sqlComando.Parameters.Add("NumeroDocumento", SqlDbType.VarChar).Value = empresaInfo.NumeroDocumento;
                    sqlComando.Parameters.Add("Descripcion", SqlDbType.VarChar).Value = empresaInfo.Descripcion;
                    sqlComando.Parameters.Add("RazonSocial", SqlDbType.VarChar).Value = empresaInfo.RazonSocial;
                    sqlComando.Parameters.Add("Ciiu", SqlDbType.VarChar).Value = empresaInfo.Ciiu;
                    sqlComando.Parameters.Add("Direccion", SqlDbType.VarChar).Value = empresaInfo.Direccion;
                    sqlComando.Parameters.Add("Activo", SqlDbType.Int).Value = empresaInfo.Activo;
                    sqlComando.Parameters.Add("UsuarioModificacionId", SqlDbType.Int).Value = empresaInfo.UsuarioModificacionId;

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
        public int Eliminar(int empresaId)
        {
            int resultado = 0;
            try
            {
                using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                {
                    var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "EliminarEmpresa", CommandType = CommandType.StoredProcedure };

                    sqlComando.Parameters.Add("EmpresaId", SqlDbType.Int).Value = empresaId;

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