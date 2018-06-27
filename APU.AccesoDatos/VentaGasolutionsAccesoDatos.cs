using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using APU.Entidad;
using APU.Herramientas;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace APU.AccesoDatos
{
    public class VentaGasolutionsAccesoDatos
    {
        #region Venta
        public List<VentaGasolutionsInfo> Listar(int ventaGasolutionsId)
        {
            var ventaListaInfo = new List<VentaGasolutionsInfo>();
            using (var oConexion = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
            {
                var sqlComando = new SqlCommand();
                sqlComando.Connection = oConexion;
                sqlComando.CommandText = "ObtenerVentaGasolutions";
                sqlComando.CommandType = CommandType.StoredProcedure;
                sqlComando.Parameters.Add("VentaGasolutionsId", SqlDbType.Int).Value = ventaGasolutionsId;

                oConexion.Open();

                using (SqlDataReader dr = sqlComando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ventaListaInfo.Add(CargarVentaInfo(dr));
                    }
                }
                oConexion.Close();
            }
            return ventaListaInfo;
        }
        public List<VentaGasolutionsInfo> ListarPaginado(int ventaId, string numeroDocumento, string tipoComprobanteId, string numeroComprobante, DateTime fechaInicio, DateTime fechaFin, int estadoId, int monedaId, int tamanioPagina, int numeroPagina)
        {
            var ventaListaInfo = new List<VentaGasolutionsInfo>();
            using (var oConexion = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
            {
                var sqlComando = new SqlCommand();
                sqlComando.Connection = oConexion;
                sqlComando.CommandText = "ObtenerVentaGasolutionsPaginado";
                sqlComando.CommandType = CommandType.StoredProcedure;
                sqlComando.Parameters.Add("VentaGasolutionsId", SqlDbType.Int).Value = ventaId;
                sqlComando.Parameters.Add("NumeroDocumento", SqlDbType.VarChar).Value = numeroDocumento;
                sqlComando.Parameters.Add("TipoComprobanteId", SqlDbType.VarChar).Value = tipoComprobanteId;
                sqlComando.Parameters.Add("NumeroComprobante", SqlDbType.VarChar).Value = numeroComprobante;
                sqlComando.Parameters.Add("FechaEmisionInicio", SqlDbType.DateTime).Value = fechaInicio;
                sqlComando.Parameters.Add("FechaEmisionFin", SqlDbType.DateTime).Value = fechaFin;
                sqlComando.Parameters.Add("EstadoId", SqlDbType.Int).Value = estadoId;
                sqlComando.Parameters.Add("MonedaId", SqlDbType.Int).Value = monedaId;
                sqlComando.Parameters.Add("TamanioPagina", SqlDbType.Int).Value = tamanioPagina;
                sqlComando.Parameters.Add("NumeroPagina", SqlDbType.Int).Value = numeroPagina;

                oConexion.Open();

                using (SqlDataReader dr = sqlComando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ventaListaInfo.Add(CargarVentaInfo(dr));
                    }
                }
                oConexion.Close();
            }
            return ventaListaInfo;
        }
        private static VentaGasolutionsInfo CargarVentaInfo(IDataReader dr)
        {
            int colCount = dr.FieldCount;
            var values = new object[colCount];

            #region Indices
            int indVentaGasolutionsId = dr.GetOrdinal("VentaGasolutionsId");

            int indFechaEmision = dr.GetOrdinal("FechaEmision");
            int indGravadas = dr.GetOrdinal("Gravadas");
            int indIdDocumento = dr.GetOrdinal("IdDocumento");
            int indCalculoIgv = dr.GetOrdinal("CalculoIgv");
            int indMonedaId = dr.GetOrdinal("MonedaId");
            int indMoneda = dr.GetOrdinal("Moneda");
            int indSimboloMoneda = dr.GetOrdinal("SimboloMoneda");
            int indMontoEnLetras = dr.GetOrdinal("MontoEnLetras");
            int indPlacaVehiculo = dr.GetOrdinal("PlacaVehiculo");
            int indTipoDocumento = dr.GetOrdinal("TipoDocumento");
            int indTotalIgv = dr.GetOrdinal("TotalIgv");
            int indTotalVenta = dr.GetOrdinal("TotalVenta");
            int indDescuento = dr.GetOrdinal("Descuento");

            int indTipoDocumentoEmisor = dr.GetOrdinal("TipoDocumentoEmisor");
            int indNroDocumentoEmisor = dr.GetOrdinal("NroDocumentoEmisor");
            int indNombreComercialEmisor = dr.GetOrdinal("NombreComercialEmisor");
            int indNombreLegalEmisor = dr.GetOrdinal("NombreLegalEmisor");
            int indDepartamentoEmisor = dr.GetOrdinal("DepartamentoEmisor");
            int indProvinciaEmisor = dr.GetOrdinal("ProvinciaEmisor");
            int indDistritoEmisor = dr.GetOrdinal("DistritoEmisor");
            int indUbigeoEmisor = dr.GetOrdinal("UbigeoEmisor");
            int indDireccionEmisor = dr.GetOrdinal("DireccionEmisor");

            int indTipoDocumentoReceptor = dr.GetOrdinal("TipoDocumentoReceptor");
            int indNroDocumentoReceptor = dr.GetOrdinal("NroDocumentoReceptor");
            int indNombreComercialReceptor = dr.GetOrdinal("NombreComercialReceptor");
            int indNombreLegalReceptor = dr.GetOrdinal("NombreLegalReceptor");
            int indDireccionReceptor = dr.GetOrdinal("DireccionReceptor");

            int indCodigoRespuesta = dr.GetOrdinal("CodigoRespuesta");
            int indExito = dr.GetOrdinal("Exito");
            int indMensajeError = dr.GetOrdinal("MensajeError");
            int indMensajeRespuesta = dr.GetOrdinal("MensajeRespuesta");
            int indNombreArchivo = dr.GetOrdinal("NombreArchivo");
            int indNroTicket = dr.GetOrdinal("NroTicket");

            int indEstadoId = dr.GetOrdinal("EstadoId");
            int indEstado = dr.GetOrdinal("Estado");
            int indComprobanteImpreso = dr.GetOrdinal("ComprobanteImpreso");

            int indFechaCreacion = dr.GetOrdinal("FechaCreacion");

            int indNumeroFila = dr.GetOrdinal("NumeroFila");
            int indTotalFilas = dr.GetOrdinal("TotalFilas");
            #endregion

            var ventaInfo = new VentaGasolutionsInfo();
            dr.GetValues(values);

            #region Campos
            ventaInfo.VentaGasolutionsId = Convert.ToInt32(values[indVentaGasolutionsId]);
            if (values[indFechaEmision] != DBNull.Value) ventaInfo.FechaEmision = Convert.ToDateTime(values[indFechaEmision]);
            ventaInfo.Gravadas = Convert.ToDecimal(values[indGravadas]);
            ventaInfo.IdDocumento = Convert.ToString(values[indIdDocumento]);
            ventaInfo.CalculoIgv = Convert.ToDecimal(values[indCalculoIgv]);
            ventaInfo.MonedaId = Convert.ToString(values[indMonedaId]);
            ventaInfo.Moneda = Convert.ToString(values[indMoneda]);
            ventaInfo.SimboloMoneda = Convert.ToString(values[indSimboloMoneda]);
            ventaInfo.MontoEnLetras = Convert.ToString(values[indMontoEnLetras]);
            ventaInfo.PlacaVehiculo = Convert.ToString(values[indPlacaVehiculo]);
            ventaInfo.TipoDocumento = Convert.ToString(values[indTipoDocumento]);
            ventaInfo.TotalIgv = Convert.ToDecimal(values[indTotalIgv]);
            ventaInfo.TotalVenta = Convert.ToDecimal(values[indTotalVenta]);
            ventaInfo.Descuento = Convert.ToDecimal(values[indDescuento]);

            ventaInfo.TipoDocumentoEmisor = Convert.ToString(values[indTipoDocumentoEmisor]);
            ventaInfo.NroDocumentoEmisor = Convert.ToString(values[indNroDocumentoEmisor]);
            ventaInfo.NombreComercialEmisor = Convert.ToString(values[indNombreComercialEmisor]);
            ventaInfo.NombreLegalEmisor = Convert.ToString(values[indNombreLegalEmisor]);
            ventaInfo.DepartamentoEmisor = Convert.ToString(values[indDepartamentoEmisor]);
            ventaInfo.ProvinciaEmisor = Convert.ToString(values[indProvinciaEmisor]);
            ventaInfo.DistritoEmisor = Convert.ToString(values[indDistritoEmisor]);
            ventaInfo.UbigeoEmisor = Convert.ToString(values[indUbigeoEmisor]);
            ventaInfo.DireccionEmisor = Convert.ToString(values[indDireccionEmisor]);

            ventaInfo.TipoDocumentoReceptor = Convert.ToString(values[indTipoDocumentoReceptor]);
            ventaInfo.NroDocumentoReceptor = Convert.ToString(values[indNroDocumentoReceptor]);
            ventaInfo.NombreComercialReceptor = Convert.ToString(values[indNombreComercialReceptor]);
            ventaInfo.NombreLegalReceptor = Convert.ToString(values[indNombreLegalReceptor]);
            ventaInfo.DireccionReceptor = Convert.ToString(values[indDireccionReceptor]);

            if (values[indCodigoRespuesta] != DBNull.Value) ventaInfo.CodigoRespuesta = Convert.ToString(values[indCodigoRespuesta]);
            if (values[indExito] != DBNull.Value) ventaInfo.Exito = Convert.ToInt32(values[indExito]);
            if (values[indMensajeError] != DBNull.Value) ventaInfo.MensajeError = Convert.ToString(values[indMensajeError]);
            if (values[indMensajeRespuesta] != DBNull.Value) ventaInfo.MensajeRespuesta = Convert.ToString(values[indMensajeRespuesta]);
            if (values[indNombreArchivo] != DBNull.Value) ventaInfo.NombreArchivo = Convert.ToString(values[indNombreArchivo]);
            if (values[indNroTicket] != DBNull.Value) ventaInfo.NroTicket = Convert.ToString(values[indNroTicket]);

            ventaInfo.EstadoId = Convert.ToInt32(values[indEstadoId]);
            ventaInfo.Estado = Convert.ToString(values[indEstado]);
            ventaInfo.ComprobanteImpreso = Convert.ToString(values[indComprobanteImpreso]);
            ventaInfo.FechaCreacion = Convert.ToDateTime(values[indFechaCreacion]);
            ventaInfo.NumeroFila = Convert.ToInt32(values[indNumeroFila]);
            ventaInfo.TotalFilas = Convert.ToInt32(values[indTotalFilas]);
            #endregion

            return ventaInfo;
        }

        public int Insertar(VentaGasolutionsInfo ventaInfo)
        {
            int resultado;
            try
            {
                using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                {
                    var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "InsertarVentaGasolutions", CommandType = CommandType.StoredProcedure };

                    sqlComando.Parameters.Add("FechaEmision", SqlDbType.DateTime2).Value = ventaInfo.FechaEmision;
                    sqlComando.Parameters.Add("Gravadas", SqlDbType.Decimal).Value = ventaInfo.Gravadas;
                    sqlComando.Parameters.Add("IdDocumento", SqlDbType.VarChar).Value = ventaInfo.IdDocumento;
                    sqlComando.Parameters.Add("CalculoIgv", SqlDbType.Decimal).Value = ventaInfo.CalculoIgv;
                    sqlComando.Parameters.Add("MonedaId", SqlDbType.VarChar).Value = ventaInfo.MonedaId;
                    sqlComando.Parameters.Add("MontoEnLetras", SqlDbType.VarChar).Value = ventaInfo.MontoEnLetras;
                    sqlComando.Parameters.Add("PlacaVehiculo", SqlDbType.VarChar).Value = ventaInfo.PlacaVehiculo;
                    sqlComando.Parameters.Add("TipoDocumento", SqlDbType.VarChar).Value = ventaInfo.TipoDocumento;
                    sqlComando.Parameters.Add("TotalIgv", SqlDbType.Decimal).Value = ventaInfo.TotalIgv;
                    sqlComando.Parameters.Add("TotalVenta", SqlDbType.Decimal).Value = ventaInfo.TotalVenta;
                    sqlComando.Parameters.Add("Descuento", SqlDbType.Decimal).Value = ventaInfo.Descuento;

                    sqlComando.Parameters.Add("TipoDocumentoEmisor", SqlDbType.VarChar).Value = ventaInfo.TipoDocumentoEmisor;
                    sqlComando.Parameters.Add("NroDocumentoEmisor", SqlDbType.VarChar).Value = ventaInfo.NroDocumentoEmisor;
                    sqlComando.Parameters.Add("NombreComercialEmisor", SqlDbType.VarChar).Value = ventaInfo.NombreComercialEmisor;
                    sqlComando.Parameters.Add("NombreLegalEmisor", SqlDbType.VarChar).Value = ventaInfo.NombreLegalEmisor;
                    sqlComando.Parameters.Add("DepartamentoEmisor", SqlDbType.VarChar).Value = ventaInfo.DepartamentoEmisor;
                    sqlComando.Parameters.Add("ProvinciaEmisor", SqlDbType.VarChar).Value = ventaInfo.ProvinciaEmisor;
                    sqlComando.Parameters.Add("DistritoEmisor", SqlDbType.VarChar).Value = ventaInfo.DistritoEmisor;
                    sqlComando.Parameters.Add("UbigeoEmisor", SqlDbType.VarChar).Value = ventaInfo.UbigeoEmisor;
                    sqlComando.Parameters.Add("DireccionEmisor", SqlDbType.VarChar).Value = ventaInfo.DireccionEmisor;

                    sqlComando.Parameters.Add("TipoDocumentoReceptor", SqlDbType.VarChar).Value = ventaInfo.TipoDocumentoReceptor;
                    sqlComando.Parameters.Add("NroDocumentoReceptor", SqlDbType.VarChar).Value = ventaInfo.NroDocumentoReceptor;
                    sqlComando.Parameters.Add("NombreComercialReceptor", SqlDbType.VarChar).Value = ventaInfo.NombreComercialReceptor;
                    sqlComando.Parameters.Add("NombreLegalReceptor", SqlDbType.VarChar).Value = ventaInfo.NombreLegalReceptor;
                    sqlComando.Parameters.Add("DireccionReceptor", SqlDbType.VarChar).Value = ventaInfo.DireccionReceptor;

                    sqlComando.Parameters.Add("CodigoRespuesta", SqlDbType.VarChar).Value = ventaInfo.CodigoRespuesta;
                    sqlComando.Parameters.Add("Exito", SqlDbType.Int).Value = ventaInfo.Exito;
                    sqlComando.Parameters.Add("MensajeError", SqlDbType.VarChar).Value = ventaInfo.MensajeError;
                    sqlComando.Parameters.Add("MensajeRespuesta", SqlDbType.VarChar).Value = ventaInfo.MensajeRespuesta;
                    sqlComando.Parameters.Add("NombreArchivo", SqlDbType.VarChar).Value = ventaInfo.NombreArchivo;
                    sqlComando.Parameters.Add("NroTicket", SqlDbType.VarChar).Value = ventaInfo.NroTicket;
                    sqlComando.Parameters.Add("EstadoId", SqlDbType.Int).Value = ventaInfo.EstadoId;
                    sqlComando.Parameters.Add("ComprobanteImpreso", SqlDbType.VarChar).Value = ventaInfo.ComprobanteImpreso;

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
        public int InsertarDetalle(VentaDetalleGasolutionsInfo ventaInfo)
        {
            int resultado;
            try
            {
                using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                {
                    var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "InsertarVentaDetalleGasolutions", CommandType = CommandType.StoredProcedure };

                    sqlComando.Parameters.Add("VentaGasolutionsId", SqlDbType.Int).Value = ventaInfo.VentaGasolutionsId;
                    sqlComando.Parameters.Add("Id", SqlDbType.Int).Value = ventaInfo.Id;
                    sqlComando.Parameters.Add("Cantidad", SqlDbType.Decimal).Value = ventaInfo.Cantidad;
                    sqlComando.Parameters.Add("CodigoItem", SqlDbType.VarChar).Value = ventaInfo.CodigoItem;
                    sqlComando.Parameters.Add("Descripcion", SqlDbType.VarChar).Value = ventaInfo.Descripcion;
                    sqlComando.Parameters.Add("Impuesto", SqlDbType.Decimal).Value = ventaInfo.Impuesto;
                    sqlComando.Parameters.Add("PrecioUnitario", SqlDbType.Decimal).Value = ventaInfo.PrecioUnitario;
                    sqlComando.Parameters.Add("TotalVenta", SqlDbType.Decimal).Value = ventaInfo.TotalVenta;
                    sqlComando.Parameters.Add("UnidadMedida", SqlDbType.VarChar).Value = ventaInfo.UnidadMedida;
                    sqlComando.Parameters.Add("TipoImpuesto", SqlDbType.VarChar).Value = ventaInfo.TipoImpuesto;
                    sqlComando.Parameters.Add("TipoPrecio", SqlDbType.VarChar).Value = ventaInfo.TipoPrecio;

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
        public int InsertarLote(List<VentaGasolutionsInfo> ventaListaInfo)
        {
            int resultado;
            try
            {
                foreach (var ventaInfo in ventaListaInfo)
                {
                    using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                    {
                        var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "InsertarVentaGasolutions", CommandType = CommandType.StoredProcedure };

                        sqlComando.Parameters.Add("FechaEmision", SqlDbType.DateTime2).Value = ventaInfo.FechaEmision;
                        sqlComando.Parameters.Add("Gravadas", SqlDbType.Decimal).Value = ventaInfo.Gravadas;
                        sqlComando.Parameters.Add("IdDocumento", SqlDbType.VarChar).Value = ventaInfo.IdDocumento;
                        sqlComando.Parameters.Add("CalculoIgv", SqlDbType.Decimal).Value = ventaInfo.CalculoIgv;
                        sqlComando.Parameters.Add("MonedaId", SqlDbType.VarChar).Value = ventaInfo.MonedaId;
                        sqlComando.Parameters.Add("MontoEnLetras", SqlDbType.VarChar).Value = ventaInfo.MontoEnLetras;
                        sqlComando.Parameters.Add("PlacaVehiculo", SqlDbType.VarChar).Value = ventaInfo.PlacaVehiculo;
                        sqlComando.Parameters.Add("TipoDocumento", SqlDbType.VarChar).Value = ventaInfo.TipoDocumento;
                        sqlComando.Parameters.Add("TotalIgv", SqlDbType.Decimal).Value = ventaInfo.TotalIgv;
                        sqlComando.Parameters.Add("TotalVenta", SqlDbType.Decimal).Value = ventaInfo.TotalVenta;
                        sqlComando.Parameters.Add("Descuento", SqlDbType.Decimal).Value = ventaInfo.Descuento;

                        sqlComando.Parameters.Add("TipoDocumentoEmisor", SqlDbType.VarChar).Value = ventaInfo.TipoDocumentoEmisor;
                        sqlComando.Parameters.Add("NroDocumentoEmisor", SqlDbType.VarChar).Value = ventaInfo.NroDocumentoEmisor;
                        sqlComando.Parameters.Add("NombreComercialEmisor", SqlDbType.VarChar).Value = ventaInfo.NombreComercialEmisor;
                        sqlComando.Parameters.Add("NombreLegalEmisor", SqlDbType.VarChar).Value = ventaInfo.NombreLegalEmisor;
                        sqlComando.Parameters.Add("DepartamentoEmisor", SqlDbType.VarChar).Value = ventaInfo.DepartamentoEmisor;
                        sqlComando.Parameters.Add("ProvinciaEmisor", SqlDbType.VarChar).Value = ventaInfo.ProvinciaEmisor;
                        sqlComando.Parameters.Add("DistritoEmisor", SqlDbType.VarChar).Value = ventaInfo.DistritoEmisor;
                        sqlComando.Parameters.Add("UbigeoEmisor", SqlDbType.VarChar).Value = ventaInfo.UbigeoEmisor;
                        sqlComando.Parameters.Add("DireccionEmisor", SqlDbType.VarChar).Value = ventaInfo.DireccionEmisor;

                        sqlComando.Parameters.Add("TipoDocumentoReceptor", SqlDbType.VarChar).Value = ventaInfo.TipoDocumentoReceptor;
                        sqlComando.Parameters.Add("NroDocumentoReceptor", SqlDbType.VarChar).Value = ventaInfo.NroDocumentoReceptor;
                        sqlComando.Parameters.Add("NombreComercialReceptor", SqlDbType.VarChar).Value = ventaInfo.NombreComercialReceptor;
                        sqlComando.Parameters.Add("NombreLegalReceptor", SqlDbType.VarChar).Value = ventaInfo.NombreLegalReceptor;
                        sqlComando.Parameters.Add("DireccionReceptor", SqlDbType.VarChar).Value = ventaInfo.DireccionReceptor;

                        sqlComando.Parameters.Add("CodigoRespuesta", SqlDbType.VarChar).Value = ventaInfo.CodigoRespuesta;
                        sqlComando.Parameters.Add("Exito", SqlDbType.Int).Value = ventaInfo.Exito;
                        sqlComando.Parameters.Add("MensajeError", SqlDbType.VarChar).Value = ventaInfo.MensajeError;
                        sqlComando.Parameters.Add("MensajeRespuesta", SqlDbType.VarChar).Value = ventaInfo.MensajeRespuesta;
                        sqlComando.Parameters.Add("NombreArchivo", SqlDbType.VarChar).Value = ventaInfo.NombreArchivo;
                        sqlComando.Parameters.Add("NroTicket", SqlDbType.VarChar).Value = ventaInfo.NroTicket;
                        sqlComando.Parameters.Add("EstadoId", SqlDbType.Int).Value = ventaInfo.EstadoId;
                        sqlComando.Parameters.Add("ComprobanteImpreso", SqlDbType.VarChar).Value = ventaInfo.ComprobanteImpreso;

                        oConnection.Open();
                        resultado = Convert.ToInt32(sqlComando.ExecuteScalar());

                        oConnection.Close();
                    }
                }
                resultado = 1;
            }
            catch (Exception ex)
            {
                resultado = 0;
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaAccesoDatos);
                if (rethrow)
                    throw ex;
            }
            return resultado;
        }
        public int Actualizar(VentaInfo ventaInfo)
        {
            int resultado = 0;
            try
            {
                using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                {
                    var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "ActualizarVenta", CommandType = CommandType.StoredProcedure };

                    sqlComando.Parameters.Add("VentaId", SqlDbType.Int).Value = ventaInfo.VentaId;
                    //sqlComando.Parameters.Add("EmpresaId", SqlDbType.Int).Value = ventaInfo.EmpresaId;
                    //sqlComando.Parameters.Add("Codigo", SqlDbType.VarChar).Value = ventaInfo.Codigo;
                    //sqlComando.Parameters.Add("Nombre", SqlDbType.VarChar).Value = ventaInfo.Nombre;
                    //sqlComando.Parameters.Add("Descripcion", SqlDbType.VarChar).Value = ventaInfo.Descripcion;
                    //sqlComando.Parameters.Add("Direccion", SqlDbType.VarChar).Value = ventaInfo.Direccion;
                    sqlComando.Parameters.Add("Activo", SqlDbType.Int).Value = ventaInfo.Activo;
                    sqlComando.Parameters.Add("UsuarioModificacionId", SqlDbType.Int).Value = ventaInfo.UsuarioModificacionId;

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
        public int Eliminar(int ventaId)
        {
            int resultado = 0;
            try
            {
                using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                {
                    var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "EliminarVenta", CommandType = CommandType.StoredProcedure };

                    sqlComando.Parameters.Add("VentaId", SqlDbType.Int).Value = ventaId;

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
        #endregion

        #region Venta Detalle
        public List<VentaDetalleInfo> ListarDetalle(int ventaId, int ventaDetalleId)
        {
            var ventaDetalleListaInfo = new List<VentaDetalleInfo>();
            using (var oConexion = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
            {
                var sqlComando = new SqlCommand();
                sqlComando.Connection = oConexion;
                sqlComando.CommandText = "ObtenerVentaDetalle";
                sqlComando.CommandType = CommandType.StoredProcedure;
                sqlComando.Parameters.Add("VentaId", SqlDbType.Int).Value = ventaId;
                sqlComando.Parameters.Add("VentaDetalleId", SqlDbType.Int).Value = ventaDetalleId;

                oConexion.Open();

                using (SqlDataReader dr = sqlComando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ventaDetalleListaInfo.Add(CargarVentaDetalleInfo(dr));
                    }
                }
                oConexion.Close();
            }
            return ventaDetalleListaInfo;
        }
        public List<VentaDetalleInfo> ListarDetallePaginado(int ventaId, int ventaDetalleId, int tamanioPagina, int numeroPagina)
        {
            var ventaDetalleListaInfo = new List<VentaDetalleInfo>();
            using (var oConexion = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
            {
                var sqlComando = new SqlCommand();
                sqlComando.Connection = oConexion;
                sqlComando.CommandText = "ObtenerVentaDetallePaginado";
                sqlComando.CommandType = CommandType.StoredProcedure;
                sqlComando.Parameters.Add("VentaId", SqlDbType.Int).Value = ventaId;
                sqlComando.Parameters.Add("VentaDetalleId", SqlDbType.Int).Value = ventaDetalleId;
                sqlComando.Parameters.Add("TamanioPagina", SqlDbType.Int).Value = tamanioPagina;
                sqlComando.Parameters.Add("NumeroPagina", SqlDbType.Int).Value = numeroPagina;

                oConexion.Open();

                using (SqlDataReader dr = sqlComando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ventaDetalleListaInfo.Add(CargarVentaDetalleInfo(dr));
                    }
                }
                oConexion.Close();
            }
            return ventaDetalleListaInfo;
        }
        private static VentaDetalleInfo CargarVentaDetalleInfo(IDataReader dr)
        {
            int colCount = dr.FieldCount;
            var values = new object[colCount];

            #region Indices
            int indVentaId = dr.GetOrdinal("VentaId");
            int indVentaDetalleId = dr.GetOrdinal("VentaDetalleId");
            int indProductoId = dr.GetOrdinal("ProductoId");
            int indCodigo = dr.GetOrdinal("Codigo");
            int indProducto = dr.GetOrdinal("Producto");
            int indUnidadMedidaId = dr.GetOrdinal("UnidadMedidaId");
            int indUnidadMedida = dr.GetOrdinal("UnidadMedida");
            int indCantidad = dr.GetOrdinal("Cantidad");
            int indPrecioUnitario = dr.GetOrdinal("PrecioUnitario");
            int indSubTotal = dr.GetOrdinal("SubTotal");
            int indDescuento = dr.GetOrdinal("Descuento");
            int indIgv = dr.GetOrdinal("Igv");
            int indMontoTotal = dr.GetOrdinal("MontoTotal");

            int indUsuarioCreacionId = dr.GetOrdinal("UsuarioCreacionId");
            int indUsuarioCreacion = dr.GetOrdinal("UsuarioCreacion");
            int indFechaCreacion = dr.GetOrdinal("FechaCreacion");
            int indUsuarioModificacionId = dr.GetOrdinal("UsuarioModificacionId");
            int indUsuarioModificacion = dr.GetOrdinal("UsuarioModificacion");
            int indFechaModificacion = dr.GetOrdinal("FechaModificacion");
            int indNumeroFila = dr.GetOrdinal("NumeroFila");
            int indTotalFilas = dr.GetOrdinal("TotalFilas");
            #endregion

            var ventaDetalleInfo = new VentaDetalleInfo();
            dr.GetValues(values);

            #region Campos
            ventaDetalleInfo.VentaId = Convert.ToInt32(values[indVentaId]);
            ventaDetalleInfo.VentaDetalleId = Convert.ToInt32(values[indVentaDetalleId]);
            ventaDetalleInfo.ProductoId = Convert.ToInt32(values[indProductoId]);
            ventaDetalleInfo.Codigo = Convert.ToString(values[indCodigo]);
            ventaDetalleInfo.Producto = Convert.ToString(values[indProducto]);
            ventaDetalleInfo.UnidadMedidaId = Convert.ToInt32(values[indUnidadMedidaId]);
            ventaDetalleInfo.UnidadMedida = Convert.ToString(values[indUnidadMedida]);
            ventaDetalleInfo.Cantidad = Convert.ToDecimal(values[indCantidad]);
            ventaDetalleInfo.PrecioUnitario = Convert.ToDecimal(values[indPrecioUnitario]);
            ventaDetalleInfo.SubTotal = Convert.ToDecimal(values[indSubTotal]);
            ventaDetalleInfo.Descuento = Convert.ToDecimal(values[indDescuento]);
            ventaDetalleInfo.Igv = Convert.ToDecimal(values[indIgv]);
            ventaDetalleInfo.MontoTotal = Convert.ToDecimal(values[indMontoTotal]);

            if (values[indUsuarioCreacionId] != DBNull.Value) ventaDetalleInfo.UsuarioCreacionId = Convert.ToInt32(values[indUsuarioCreacionId]);
            if (values[indUsuarioCreacion] != DBNull.Value) ventaDetalleInfo.UsuarioCreacion = Convert.ToString(values[indUsuarioCreacion]);
            ventaDetalleInfo.FechaCreacion = Convert.ToDateTime(values[indFechaCreacion]);
            if (values[indUsuarioModificacionId] != DBNull.Value) ventaDetalleInfo.UsuarioModificacionId = Convert.ToInt32(values[indUsuarioModificacionId]);
            if (values[indUsuarioModificacion] != DBNull.Value) ventaDetalleInfo.UsuarioModificacion = Convert.ToString(values[indUsuarioModificacion]);
            if (values[indFechaModificacion] != DBNull.Value) ventaDetalleInfo.FechaModificacion = Convert.ToDateTime(values[indFechaModificacion]);
            ventaDetalleInfo.NumeroFila = Convert.ToInt32(values[indNumeroFila]);
            ventaDetalleInfo.TotalFilas = Convert.ToInt32(values[indTotalFilas]);
            #endregion

            return ventaDetalleInfo;
        }
        public int InsertarDetalle(VentaDetalleInfo ventaDetalleInfo)
        {
            int resultado;
            try
            {
                using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                {
                    var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "InsertarVentaDetalle", CommandType = CommandType.StoredProcedure };

                    sqlComando.Parameters.Add("VentaId", SqlDbType.Int).Value = ventaDetalleInfo.VentaId;
                    sqlComando.Parameters.Add("ProductoId", SqlDbType.Int).Value = ventaDetalleInfo.ProductoId;
                    sqlComando.Parameters.Add("Cantidad", SqlDbType.Decimal).Value = ventaDetalleInfo.Cantidad;
                    sqlComando.Parameters.Add("PrecioUnitario", SqlDbType.Decimal).Value = ventaDetalleInfo.PrecioUnitario;
                    sqlComando.Parameters.Add("SubTotal", SqlDbType.Decimal).Value = ventaDetalleInfo.SubTotal;
                    sqlComando.Parameters.Add("Descuento", SqlDbType.Decimal).Value = ventaDetalleInfo.Descuento;
                    sqlComando.Parameters.Add("Igv", SqlDbType.Decimal).Value = ventaDetalleInfo.Igv;
                    sqlComando.Parameters.Add("MontoTotal", SqlDbType.Decimal).Value = ventaDetalleInfo.MontoTotal;
                    sqlComando.Parameters.Add("UsuarioCreacionId", SqlDbType.Int).Value = ventaDetalleInfo.UsuarioCreacionId;

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
        public int ActualizarDetalle(VentaDetalleInfo ventaDetalleInfo)
        {
            int resultado = 0;
            try
            {
                using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                {
                    var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "ActualizarVentaDetalle", CommandType = CommandType.StoredProcedure };

                    sqlComando.Parameters.Add("VentaDetalleId", SqlDbType.Int).Value = ventaDetalleInfo.VentaDetalleId;
                    sqlComando.Parameters.Add("ProductoId", SqlDbType.Int).Value = ventaDetalleInfo.ProductoId;
                    sqlComando.Parameters.Add("Cantidad", SqlDbType.Decimal).Value = ventaDetalleInfo.Cantidad;
                    sqlComando.Parameters.Add("PrecioUnitario", SqlDbType.Decimal).Value = ventaDetalleInfo.PrecioUnitario;
                    sqlComando.Parameters.Add("SubTotal", SqlDbType.Decimal).Value = ventaDetalleInfo.SubTotal;
                    sqlComando.Parameters.Add("Descuento", SqlDbType.Decimal).Value = ventaDetalleInfo.Descuento;
                    sqlComando.Parameters.Add("Igv", SqlDbType.Decimal).Value = ventaDetalleInfo.Igv;
                    sqlComando.Parameters.Add("MontoTotal", SqlDbType.Decimal).Value = ventaDetalleInfo.MontoTotal;
                    sqlComando.Parameters.Add("UsuarioModificacionId", SqlDbType.Int).Value = ventaDetalleInfo.UsuarioModificacionId;

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
        public int EliminarDetalle(int ventaDetalleId)
        {
            int resultado = 0;
            try
            {
                using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                {
                    var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "EliminarVentaDetalle", CommandType = CommandType.StoredProcedure };

                    sqlComando.Parameters.Add("VentaDetalleId", SqlDbType.Int).Value = ventaDetalleId;

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
        #endregion
    }
}