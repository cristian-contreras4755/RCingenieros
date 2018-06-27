using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using APU.Entidad;
using APU.Herramientas;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace APU.AccesoDatos
{
    public class CorrelativoAccesoDatos
    {
        public List<CorrelativoInfo> Listar(string tipoComprobanteId, int serieId, int correlativoId)
        {
            var correlativoListaInfo = new List<CorrelativoInfo>();
            using (var oConexion = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
            {
                var sqlComando = new SqlCommand();
                sqlComando.Connection = oConexion;
                sqlComando.CommandText = "ObtenerCorrelativo";
                sqlComando.CommandType = CommandType.StoredProcedure;
                sqlComando.Parameters.Add("TipoComprobanteId", SqlDbType.VarChar).Value = tipoComprobanteId;
                sqlComando.Parameters.Add("SerieId", SqlDbType.Int).Value = serieId;
                sqlComando.Parameters.Add("CorrelativoId", SqlDbType.Int).Value = correlativoId;

                oConexion.Open();

                using (SqlDataReader dr = sqlComando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        correlativoListaInfo.Add(CargarCorrelativoInfo(dr));
                    }
                }
                oConexion.Close();
            }
            return correlativoListaInfo;
        }
        public List<CorrelativoInfo> ListarPaginado(string tipoComprobanteId, int serieId, int correlativoId, int tamanioPagina, int numeroPagina)
        {
            var correlativoListaInfo = new List<CorrelativoInfo>();
            using (var oConexion = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
            {
                var sqlComando = new SqlCommand();
                sqlComando.Connection = oConexion;
                sqlComando.CommandText = "ObtenerCorrelativoPaginado";
                sqlComando.CommandType = CommandType.StoredProcedure;
                sqlComando.Parameters.Add("TipoComprobanteId", SqlDbType.VarChar).Value = tipoComprobanteId;
                sqlComando.Parameters.Add("SerieId", SqlDbType.Int).Value = serieId;
                sqlComando.Parameters.Add("CorrelativoId", SqlDbType.Int).Value = correlativoId;
                sqlComando.Parameters.Add("TamanioPagina", SqlDbType.Int).Value = tamanioPagina;
                sqlComando.Parameters.Add("NumeroPagina", SqlDbType.Int).Value = numeroPagina;

                oConexion.Open();

                using (SqlDataReader dr = sqlComando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        correlativoListaInfo.Add(CargarCorrelativoInfo(dr));
                    }
                }
                oConexion.Close();
            }
            return correlativoListaInfo;
        }
        private static CorrelativoInfo CargarCorrelativoInfo(IDataReader dr)
        {
            int colCount = dr.FieldCount;
            var values = new object[colCount];

            #region Indices
            int indCorrelativoId = dr.GetOrdinal("CorrelativoId");
            int indTipoComprobanteId = dr.GetOrdinal("TipoComprobanteId");
            int indTipoComprobante = dr.GetOrdinal("TipoComprobante");
            int indSerieId = dr.GetOrdinal("SerieId");
            int indSerie = dr.GetOrdinal("Serie");
            int indInicio = dr.GetOrdinal("Inicio");
            int indFin = dr.GetOrdinal("Fin");
            int indActual = dr.GetOrdinal("Actual");
            int indActivo = dr.GetOrdinal("Activo");
            int indUsuarioCreacionId = dr.GetOrdinal("UsuarioCreacionId");
            int indUsuarioCreacion = dr.GetOrdinal("UsuarioCreacion");
            int indFechaCreacion = dr.GetOrdinal("FechaCreacion");
            int indUsuarioModificacionId = dr.GetOrdinal("UsuarioModificacionId");
            int indUsuarioModificacion = dr.GetOrdinal("UsuarioModificacion");
            int indFechaModificacion = dr.GetOrdinal("FechaModificacion");
            int indNumeroFila = dr.GetOrdinal("NumeroFila");
            int indTotalFilas = dr.GetOrdinal("TotalFilas");
            #endregion

            var correlativoInfo = new CorrelativoInfo();
            dr.GetValues(values);

            #region Campos
            correlativoInfo.CorrelativoId = Convert.ToInt32(values[indCorrelativoId]);
            correlativoInfo.TipoComprobanteId = Convert.ToString(values[indTipoComprobanteId]);
            correlativoInfo.TipoComprobante = Convert.ToString(values[indTipoComprobante]);
            correlativoInfo.SerieId = Convert.ToInt32(values[indSerieId]);
            correlativoInfo.Serie = Convert.ToString(values[indSerie]);
            correlativoInfo.Inicio = Convert.ToString(values[indInicio]);
            correlativoInfo.Fin = Convert.ToString(values[indFin]);
            correlativoInfo.Actual = Convert.ToString(values[indActual]);
            correlativoInfo.Activo = Convert.ToInt32(values[indActivo]);

            correlativoInfo.UsuarioCreacionId = Convert.ToInt32(values[indUsuarioCreacionId]);

            if (values[indUsuarioCreacion] != DBNull.Value) correlativoInfo.UsuarioCreacion = Convert.ToString(values[indUsuarioCreacion]);
            correlativoInfo.FechaCreacion = Convert.ToDateTime(values[indFechaCreacion]);
            if (values[indUsuarioModificacionId] != DBNull.Value) correlativoInfo.UsuarioModificacionId = Convert.ToInt32(values[indUsuarioModificacionId]);
            if (values[indUsuarioModificacion] != DBNull.Value) correlativoInfo.UsuarioModificacion = Convert.ToString(values[indUsuarioModificacion]);
            if (values[indFechaModificacion] != DBNull.Value) correlativoInfo.FechaModificacion = Convert.ToDateTime(values[indFechaModificacion]);
            correlativoInfo.NumeroFila = Convert.ToInt32(values[indNumeroFila]);
            correlativoInfo.TotalFilas = Convert.ToInt32(values[indTotalFilas]);
            #endregion

            return correlativoInfo;
        }
        public int Insertar(CorrelativoInfo correlativoInfo)
        {
            int resultado;
            try
            {
                using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                {
                    var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "InsertarCorrelativo", CommandType = CommandType.StoredProcedure };

                    sqlComando.Parameters.Add("TipoComprobanteId", SqlDbType.VarChar).Value = correlativoInfo.TipoComprobanteId;
                    sqlComando.Parameters.Add("SerieId", SqlDbType.VarChar).Value = correlativoInfo.SerieId;
                    sqlComando.Parameters.Add("Inicio", SqlDbType.VarChar).Value = correlativoInfo.Inicio;
                    sqlComando.Parameters.Add("Fin", SqlDbType.VarChar).Value = correlativoInfo.Fin;
                    sqlComando.Parameters.Add("Actual", SqlDbType.VarChar).Value = correlativoInfo.Actual;
                    sqlComando.Parameters.Add("Activo", SqlDbType.Int).Value = correlativoInfo.Activo;
                    sqlComando.Parameters.Add("UsuarioCreacionId", SqlDbType.Int).Value = correlativoInfo.UsuarioCreacionId;

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
        public int Actualizar(CorrelativoInfo correlativoInfo)
        {
            int resultado = 0;
            try
            {
                using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                {
                    var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "ActualizarCorrelativo", CommandType = CommandType.StoredProcedure };

                    sqlComando.Parameters.Add("CorrelativoId", SqlDbType.Int).Value = correlativoInfo.CorrelativoId;
                    sqlComando.Parameters.Add("TipoComprobanteId", SqlDbType.VarChar).Value = correlativoInfo.TipoComprobanteId;
                    sqlComando.Parameters.Add("SerieId", SqlDbType.VarChar).Value = correlativoInfo.SerieId;
                    sqlComando.Parameters.Add("Inicio", SqlDbType.VarChar).Value = correlativoInfo.Inicio;
                    sqlComando.Parameters.Add("Fin", SqlDbType.VarChar).Value = correlativoInfo.Fin;
                    sqlComando.Parameters.Add("Actual", SqlDbType.VarChar).Value = correlativoInfo.Actual;
                    sqlComando.Parameters.Add("Activo", SqlDbType.Int).Value = correlativoInfo.Activo;
                    sqlComando.Parameters.Add("UsuarioModificacionId", SqlDbType.Int).Value = correlativoInfo.UsuarioModificacionId;

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
        public int Eliminar(int correlativoId)
        {
            int resultado = 0;
            try
            {
                using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                {
                    var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "EliminarCorrelativo", CommandType = CommandType.StoredProcedure };

                    sqlComando.Parameters.Add("CorrelativoId", SqlDbType.Int).Value = correlativoId;

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