using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using APU.Entidad;
using APU.Herramientas;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace APU.AccesoDatos
{
    public class TipoCambioAccesoDatos
    {
        public List<TipoCambioInfo> Listar(int tipoCambioId)
        {
            var tipoCambioListaInfo = new List<TipoCambioInfo>();
            using (var oConexion = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
            {
                var sqlComando = new SqlCommand();
                sqlComando.Connection = oConexion;
                sqlComando.CommandText = "ObtenerTipoCambio";
                sqlComando.CommandType = CommandType.StoredProcedure;
                sqlComando.Parameters.Add("TipoCambioId", SqlDbType.Int).Value = tipoCambioId;

                oConexion.Open();

                using (SqlDataReader dr = sqlComando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        tipoCambioListaInfo.Add(CargarTipoCambioInfo(dr));
                    }
                }
                oConexion.Close();
            }
            return tipoCambioListaInfo;
        }
        public List<TipoCambioInfo> ListarPaginado(int tipoCambioId, int tipoCotizacionId, string fecha, int tamanioPagina, int numeroPagina)
        {
            var tipoCambioListaInfo = new List<TipoCambioInfo>();
            using (var oConexion = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
            {
                var sqlComando = new SqlCommand();
                sqlComando.Connection = oConexion;
                sqlComando.CommandText = "ObtenerTipoCambioPaginado";
                sqlComando.CommandType = CommandType.StoredProcedure;
                sqlComando.Parameters.Add("TipoCambioId", SqlDbType.Int).Value = tipoCambioId;
                sqlComando.Parameters.Add("TipoCotizacionId", SqlDbType.Int).Value = tipoCotizacionId;
                sqlComando.Parameters.Add("Fecha", SqlDbType.VarChar).Value = fecha;
                sqlComando.Parameters.Add("TamanioPagina", SqlDbType.Int).Value = tamanioPagina;
                sqlComando.Parameters.Add("NumeroPagina", SqlDbType.Int).Value = numeroPagina;

                oConexion.Open();

                using (SqlDataReader dr = sqlComando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        tipoCambioListaInfo.Add(CargarTipoCambioInfo(dr));
                    }
                }
                oConexion.Close();
            }
            return tipoCambioListaInfo;
        }
        private static TipoCambioInfo CargarTipoCambioInfo(IDataReader dr)
        {
            int colCount = dr.FieldCount;
            var values = new object[colCount];

            #region Indices
            int indTipoCambioId = dr.GetOrdinal("TipoCambioId");
            int indTipoCotizacionId = dr.GetOrdinal("TipoCotizacionId");
            int indTipoCotizacion = dr.GetOrdinal("TipoCotizacion");
            int indFecha = dr.GetOrdinal("Fecha");
            int indVenta = dr.GetOrdinal("Venta");
            int indCompra = dr.GetOrdinal("Compra");
            int indUsuarioCreacionId = dr.GetOrdinal("UsuarioCreacionId");
            int indUsuarioCreacion = dr.GetOrdinal("UsuarioCreacion");
            int indFechaCreacion = dr.GetOrdinal("FechaCreacion");
            int indUsuarioModificacionId = dr.GetOrdinal("UsuarioModificacionId");
            int indUsuarioModificacion = dr.GetOrdinal("UsuarioModificacion");
            int indFechaModificacion = dr.GetOrdinal("FechaModificacion");
            int indNumeroFila = dr.GetOrdinal("NumeroFila");
            int indTotalFilas = dr.GetOrdinal("TotalFilas");
            #endregion

            var tipoCambioInfo = new TipoCambioInfo();
            dr.GetValues(values);

            #region Campos
            tipoCambioInfo.TipoCambioId = Convert.ToInt32(values[indTipoCambioId]);
            tipoCambioInfo.TipoCotizacionId = Convert.ToInt32(values[indTipoCotizacionId]);
            tipoCambioInfo.TipoCotizacion = Convert.ToString(values[indTipoCotizacion]);
            tipoCambioInfo.Fecha = Convert.ToDateTime(values[indFecha]);
            tipoCambioInfo.Venta = Convert.ToDecimal(values[indVenta]);
            tipoCambioInfo.Compra = Convert.ToDecimal(values[indCompra]);
            tipoCambioInfo.UsuarioCreacionId = Convert.ToInt32(values[indUsuarioCreacionId]);

            if (values[indUsuarioCreacion] != DBNull.Value) tipoCambioInfo.UsuarioCreacion = Convert.ToString(values[indUsuarioCreacion]);

            tipoCambioInfo.FechaCreacion = Convert.ToDateTime(values[indFechaCreacion]);

            if (values[indUsuarioModificacionId] != DBNull.Value) tipoCambioInfo.UsuarioModificacionId = Convert.ToInt32(values[indUsuarioModificacionId]);
            if (values[indUsuarioModificacion] != DBNull.Value) tipoCambioInfo.UsuarioModificacion = Convert.ToString(values[indUsuarioModificacion]);
            if (values[indFechaModificacion] != DBNull.Value) tipoCambioInfo.FechaModificacion = Convert.ToDateTime(values[indFechaModificacion]);
            tipoCambioInfo.NumeroFila = Convert.ToInt32(values[indNumeroFila]);
            tipoCambioInfo.TotalFilas = Convert.ToInt32(values[indTotalFilas]);
            #endregion

            return tipoCambioInfo;
        }
        public int Insertar(TipoCambioInfo tipoCambioInfo)
        {
            int resultado;
            try
            {
                using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                {
                    var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "InsertarTipoCambio", CommandType = CommandType.StoredProcedure };

                    sqlComando.Parameters.Add("TipoCotizacionId", SqlDbType.Int).Value = tipoCambioInfo.TipoCotizacionId;
                    sqlComando.Parameters.Add("Fecha", SqlDbType.DateTime2).Value = tipoCambioInfo.Fecha;
                    sqlComando.Parameters.Add("Venta", SqlDbType.Decimal).Value = tipoCambioInfo.Venta;
                    sqlComando.Parameters.Add("Compra", SqlDbType.Decimal).Value = tipoCambioInfo.Compra;
                    sqlComando.Parameters.Add("UsuarioCreacionId", SqlDbType.Int).Value = tipoCambioInfo.UsuarioCreacionId;

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
        public int Actualizar(TipoCambioInfo tipoCambioInfo)
        {
            int resultado = 0;
            try
            {
                using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                {
                    var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "ActualizarTipoCambio", CommandType = CommandType.StoredProcedure };

                    sqlComando.Parameters.Add("TipoCambioId", SqlDbType.Int).Value = tipoCambioInfo.TipoCambioId;
                    sqlComando.Parameters.Add("TipoCotizacionId", SqlDbType.Int).Value = tipoCambioInfo.TipoCotizacionId;
                    sqlComando.Parameters.Add("Fecha", SqlDbType.DateTime2).Value = tipoCambioInfo.Fecha;
                    sqlComando.Parameters.Add("Venta", SqlDbType.Decimal).Value = tipoCambioInfo.Venta;
                    sqlComando.Parameters.Add("Compra", SqlDbType.Decimal).Value = tipoCambioInfo.Compra;
                    sqlComando.Parameters.Add("UsuarioModificacionId", SqlDbType.Int).Value = tipoCambioInfo.UsuarioModificacionId;

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
        public int Eliminar(int tipoCambioId)
        {
            int resultado = 0;
            try
            {
                using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                {
                    var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "EliminarTipoCambio", CommandType = CommandType.StoredProcedure };

                    sqlComando.Parameters.Add("TipoCambioId", SqlDbType.Int).Value = tipoCambioId;

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