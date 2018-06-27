using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using APU.Entidad;
using APU.Herramientas;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace APU.AccesoDatos
{
    public class VentaSunatAccesoDatos
    {
        public List<VentaSunatInfo> Listar(int ventaSunatId, int ventaId)
        {
            var ventaSunatListaInfo = new List<VentaSunatInfo>();
            using (var oConexion = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
            {
                var sqlComando = new SqlCommand();
                sqlComando.Connection = oConexion;
                sqlComando.CommandText = "ObtenerVentaSunat";
                sqlComando.CommandType = CommandType.StoredProcedure;
                sqlComando.Parameters.Add("VentaSunatId", SqlDbType.Int).Value = ventaSunatId;
                sqlComando.Parameters.Add("VentaId", SqlDbType.Int).Value = ventaId;

                oConexion.Open();

                using (SqlDataReader dr = sqlComando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ventaSunatListaInfo.Add(CargarVentaSunatInfo(dr));
                    }
                }
                oConexion.Close();
            }
            return ventaSunatListaInfo;
        }
        public List<VentaSunatInfo> ListarPaginado(int surtidorId, int tamanioPagina, int numeroPagina)
        {
            var ventaSunatListaInfo = new List<VentaSunatInfo>();
            using (var oConexion = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
            {
                var sqlComando = new SqlCommand();
                sqlComando.Connection = oConexion;
                sqlComando.CommandText = "ObtenerVentaSunatPaginado";
                sqlComando.CommandType = CommandType.StoredProcedure;
                sqlComando.Parameters.Add("SurtidorId", SqlDbType.Int).Value = surtidorId;
                sqlComando.Parameters.Add("TamanioPagina", SqlDbType.Int).Value = tamanioPagina;
                sqlComando.Parameters.Add("NumeroPagina", SqlDbType.Int).Value = numeroPagina;

                oConexion.Open();

                using (SqlDataReader dr = sqlComando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ventaSunatListaInfo.Add(CargarVentaSunatInfo(dr));
                    }
                }
                oConexion.Close();
            }
            return ventaSunatListaInfo;
        }
        private static VentaSunatInfo CargarVentaSunatInfo(IDataReader dr)
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

            var ventaSunatInfo = new VentaSunatInfo();
            dr.GetValues(values);

            #region Campos
            //ventaSunatInfo.IslaId = Convert.ToInt32(values[indSurtidorId]);
            //ventaSunatInfo.Nombre = Convert.ToString(values[indNombre]);
            //ventaSunatInfo.Descripcion = Convert.ToString(values[indDescripcion]);
            //ventaSunatInfo.Codigo = Convert.ToString(values[indCodigo]);
            //ventaSunatInfo.Activo = Convert.ToInt32(values[indActivo]);
            ventaSunatInfo.UsuarioCreacionId = Convert.ToInt32(values[indUsuarioCreacionId]);
            ventaSunatInfo.FechaCreacion = Convert.ToDateTime(values[indFechaCreacion]);
            ventaSunatInfo.UsuarioModificacionId = Convert.ToInt32(values[indUsuarioModificacionId]);
            ventaSunatInfo.FechaModificacion = Convert.ToDateTime(values[indFechaModificacion]);
            //ventaSunatInfo.NumeroFila = Convert.ToInt32(values[indNumeroFila]);
            //ventaSunatInfo.TotalFilas = Convert.ToInt32(values[indTotalFilas]);
            #endregion

            return ventaSunatInfo;
        }
        public int Insertar(VentaSunatInfo ventaSunatInfo)
        {
            int resultado;
            try
            {
                using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                {
                    var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "InsertarVentaSunat", CommandType = CommandType.StoredProcedure };

                    sqlComando.Parameters.Add("VentaId", SqlDbType.Int).Value = ventaSunatInfo.VentaId;
                    sqlComando.Parameters.Add("CodigoRespuesta", SqlDbType.VarChar).Value = ventaSunatInfo.CodigoRespuesta;
                    sqlComando.Parameters.Add("Exito", SqlDbType.Int).Value = ventaSunatInfo.Exito;
                    sqlComando.Parameters.Add("MensajeError", SqlDbType.VarChar).Value = ventaSunatInfo.MensajeError;
                    sqlComando.Parameters.Add("MensajeRespuesta", SqlDbType.VarChar).Value = ventaSunatInfo.MensajeRespuesta;
                    sqlComando.Parameters.Add("NombreArchivo", SqlDbType.VarChar).Value = ventaSunatInfo.NombreArchivo;
                    sqlComando.Parameters.Add("Pila", SqlDbType.VarChar).Value = ventaSunatInfo.Pila;
                    sqlComando.Parameters.Add("TramaZipCdr", SqlDbType.VarChar).Value = ventaSunatInfo.TramaZipCdr;
                    sqlComando.Parameters.Add("NroTicket", SqlDbType.VarChar).Value = ventaSunatInfo.NroTicket;
                    sqlComando.Parameters.Add("UsuarioCreacionId", SqlDbType.Int).Value = ventaSunatInfo.UsuarioCreacionId;

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