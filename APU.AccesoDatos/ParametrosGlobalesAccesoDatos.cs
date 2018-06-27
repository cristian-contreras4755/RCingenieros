using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using APU.Entidad;

namespace APU.AccesoDatos
{
    public class ParametrosGlobalesAccesoDatos
    {

        public List<ParametrosGlobalesInfo> Listar(int codigoParametro)
        {
            var codigoListaParametro = new List<ParametrosGlobalesInfo>();
            using (var oConexion = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
            {
                var sqlComando = new SqlCommand { Connection = oConexion, CommandText = "ObtenerParametrosGlobales", CommandType = CommandType.StoredProcedure };

                sqlComando.Parameters.Add("ParametroId", SqlDbType.Int).Value = codigoParametro;

                oConexion.Open();

                using (SqlDataReader dr = sqlComando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        codigoListaParametro.Add(CargarParametrosGlobales(dr));
                    }
                }
                oConexion.Close();
            }
            return codigoListaParametro;
        }
        public List<ParametrosGlobalesInfo> ListarParametrosGlobalesPaginado(int codigoParametro, int tamanioPagina, int numeroPagina)
        {
            var codigoListaParametro = new List<ParametrosGlobalesInfo>();
            using (var oConexion = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
            {
                var sqlComando = new SqlCommand { Connection = oConexion, CommandText = "ObtenerParametrosGlobalesPaginado", CommandType = CommandType.StoredProcedure };

                sqlComando.Parameters.Add("ParametroId", SqlDbType.Int).Value = codigoParametro;
                sqlComando.Parameters.Add("PageSize", SqlDbType.Int).Value = tamanioPagina;
                sqlComando.Parameters.Add("PageNumber", SqlDbType.Int).Value = numeroPagina;

                oConexion.Open();

                using (SqlDataReader dr = sqlComando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        codigoListaParametro.Add(CargarParametrosGlobalesPagInfo(dr));
                    }
                }
                oConexion.Close();
            }
            return codigoListaParametro;
        }

        private static ParametrosGlobalesInfo CargarParametrosGlobales(IDataReader dr)
        {
            int colCount = dr.FieldCount;
            var values = new object[colCount];

            #region Indices
            int indCodigoParametro = dr.GetOrdinal("ParametroId");
            int indNombreParametro = dr.GetOrdinal("NombreParametro");
            int indValorParametro = dr.GetOrdinal("ValorParametro");
            int indDescripcion = dr.GetOrdinal("Descripcion");
            int indEditable = dr.GetOrdinal("Editable");

            #endregion

            var ParametrosGlobalesInfo = new ParametrosGlobalesInfo();
            dr.GetValues(values);

            #region Campos
            ParametrosGlobalesInfo.CodigoParametro = Convert.ToInt32(values[indCodigoParametro]);
            ParametrosGlobalesInfo.NombreParametro = Convert.ToString(values[indNombreParametro]);
            ParametrosGlobalesInfo.ValorParametro = Convert.ToString(values[indValorParametro]);
            ParametrosGlobalesInfo.Descripcion = Convert.ToString(values[indDescripcion]);
            ParametrosGlobalesInfo.Editable = Convert.ToInt32(values[indEditable]);

            #endregion

            return ParametrosGlobalesInfo;
        }

        private static ParametrosGlobalesInfo CargarParametrosGlobalesPagInfo(IDataReader dr)
        {
            int colCount = dr.FieldCount;
            var values = new object[colCount];

            #region Indices
            int indCodigoParametro = dr.GetOrdinal("ParametroId");
            int indNombreParametro = dr.GetOrdinal("NombreParametro");
            int indValorParametro = dr.GetOrdinal("ValorParametro");
            int indDescripcion = dr.GetOrdinal("Descripcion");
            int indEditable = dr.GetOrdinal("Editable");
            int indNumeroFila = dr.GetOrdinal("NumeroFila");
            int indTotalFilas = dr.GetOrdinal("TotalFilas");
            #endregion

            var ParametrosGlobalesInfo = new ParametrosGlobalesInfo();
            dr.GetValues(values);

            #region Campos

            ParametrosGlobalesInfo.CodigoParametro = Convert.ToInt32(values[indCodigoParametro]);
            ParametrosGlobalesInfo.NombreParametro = Convert.ToString(values[indNombreParametro]);
            ParametrosGlobalesInfo.ValorParametro = Convert.ToString(values[indValorParametro]);
            ParametrosGlobalesInfo.Descripcion = Convert.ToString(values[indDescripcion]);
            ParametrosGlobalesInfo.Editable = Convert.ToInt32(values[indEditable]);
            ParametrosGlobalesInfo.NumeroFila = Convert.ToInt32(values[indNumeroFila]);
            ParametrosGlobalesInfo.TotalFilas = Convert.ToInt32(values[indTotalFilas]);
            #endregion

            return ParametrosGlobalesInfo;
        }

        public int Insertar(ParametrosGlobalesInfo oParametrosGlobalesInfo)
        {
            int resultado;
            try
            {
                using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                {
                    var sqlComando = new SqlCommand
                    {
                        Connection = oConnection,
                        CommandText = "InsertarParametrosGlobales",
                        CommandType = CommandType.StoredProcedure
                    };

                    sqlComando.Parameters.Add("NombreParametro", SqlDbType.VarChar).Value = oParametrosGlobalesInfo.NombreParametro;
                    sqlComando.Parameters.Add("ValorParametro", SqlDbType.VarChar).Value = oParametrosGlobalesInfo.ValorParametro;
                    sqlComando.Parameters.Add("Descripcion", SqlDbType.VarChar).Value = oParametrosGlobalesInfo.Descripcion;
                    sqlComando.Parameters.Add("Editable", SqlDbType.VarChar).Value = oParametrosGlobalesInfo.Editable;
                    sqlComando.Parameters.Add("UsuarioIdCreacion", SqlDbType.Int).Value = oParametrosGlobalesInfo.UsuarioIdCreacion;

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

        public int Actualizar(ParametrosGlobalesInfo oParametrosGlobalesInfo)
        {
            int resultado;
            try
            {
                using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                {
                    var sqlComando = new SqlCommand
                    {
                        Connection = oConnection,
                        CommandText = "ActualizarParametrosGlobales",
                        CommandType = CommandType.StoredProcedure
                    };

                    sqlComando.Parameters.Add("ParametroId", SqlDbType.Int).Value = oParametrosGlobalesInfo.CodigoParametro;
                    sqlComando.Parameters.Add("NombreParametro", SqlDbType.VarChar).Value = oParametrosGlobalesInfo.NombreParametro;
                    sqlComando.Parameters.Add("ValorParametro", SqlDbType.VarChar).Value = oParametrosGlobalesInfo.ValorParametro;
                    sqlComando.Parameters.Add("Descripcion", SqlDbType.VarChar).Value = oParametrosGlobalesInfo.Descripcion;
                    sqlComando.Parameters.Add("Editable", SqlDbType.VarChar).Value = oParametrosGlobalesInfo.Editable;
                    sqlComando.Parameters.Add("UsuarioIdModificacion", SqlDbType.Int).Value = oParametrosGlobalesInfo.UsuarioIdModificacion;


                    oConnection.Open();
                    resultado = Convert.ToInt32(sqlComando.ExecuteScalar());

                    oConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return resultado;
        }

        public int Eliminar(int codigoEmpresa)
        {
            int resultado;
            try
            {
                using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                {
                    var sqlComando = new SqlCommand
                    {
                        Connection = oConnection,
                        CommandText = "EliminarParametrosGlobales",
                        CommandType = CommandType.StoredProcedure
                    };
                    sqlComando.Parameters.Add("ParametroId", SqlDbType.Int).Value = codigoEmpresa;

                    oConnection.Open();
                    resultado = sqlComando.ExecuteNonQuery();
                    oConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return resultado;
        }
    }
}