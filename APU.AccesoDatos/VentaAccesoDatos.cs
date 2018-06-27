using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using APU.Entidad;
using APU.Herramientas;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace APU.AccesoDatos
{
    public class VentaAccesoDatos
    {
        #region Venta
        public List<VentaInfo> Listar(int ventaId)
        {
            var ventaListaInfo = new List<VentaInfo>();
            using (var oConexion = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
            {
                var sqlComando = new SqlCommand();
                sqlComando.Connection = oConexion;
                sqlComando.CommandText = "ObtenerVenta";
                sqlComando.CommandType = CommandType.StoredProcedure;
                sqlComando.Parameters.Add("VentaId", SqlDbType.Int).Value = ventaId;

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
        public List<VentaInfo> Listar(string ventas)
        {
            var ventaListaInfo = new List<VentaInfo>();
            using (var oConexion = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
            {
                var sqlComando = new SqlCommand();
                sqlComando.Connection = oConexion;
                sqlComando.CommandText = "ObtenerVentas";
                sqlComando.CommandType = CommandType.StoredProcedure;
                sqlComando.Parameters.Add("Ventas", SqlDbType.VarChar).Value = ventas;

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
        public List<VentaInfo> ListarPaginado(int ventaId, string numeroDocumento, string tipoComprobanteId, DateTime fechaInicio, DateTime fechaFin, int estadoId, int monedaId, int tipoNegocioId, int tamanioPagina, int numeroPagina)
        {
            var ventaListaInfo = new List<VentaInfo>();
            using (var oConexion = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
            {
                var sqlComando = new SqlCommand();
                sqlComando.Connection = oConexion;
                sqlComando.CommandText = "ObtenerVentaPaginado";
                sqlComando.CommandType = CommandType.StoredProcedure;
                sqlComando.Parameters.Add("VentaId", SqlDbType.Int).Value = ventaId;
                sqlComando.Parameters.Add("NumeroDocumento", SqlDbType.VarChar).Value = numeroDocumento;
                sqlComando.Parameters.Add("TipoComprobanteId", SqlDbType.VarChar).Value = tipoComprobanteId;
                sqlComando.Parameters.Add("FechaEmisionInicio", SqlDbType.DateTime).Value = fechaInicio;
                sqlComando.Parameters.Add("FechaEmisionFin", SqlDbType.DateTime).Value = fechaFin;
                sqlComando.Parameters.Add("EstadoId", SqlDbType.Int).Value = estadoId;
                sqlComando.Parameters.Add("MonedaId", SqlDbType.Int).Value = monedaId;
                sqlComando.Parameters.Add("TipoNegocioId", SqlDbType.Int).Value = tipoNegocioId;
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
        private static VentaInfo CargarVentaInfo(IDataReader dr)
        {
            int colCount = dr.FieldCount;
            var values = new object[colCount];

            #region Indices
            int indVentaId = dr.GetOrdinal("VentaId");
            int indAgenciaId = dr.GetOrdinal("AgenciaId");
            int indAgencia = dr.GetOrdinal("Agencia");
            int indClienteId = dr.GetOrdinal("ClienteId");
            int indCliente = dr.GetOrdinal("Cliente");
            int indNumeroDocumento = dr.GetOrdinal("NumeroDocumento");
            int indDireccion = dr.GetOrdinal("Direccion");
            int indTipoComprobanteId = dr.GetOrdinal("TipoComprobanteId");
            int indTipoComprobante = dr.GetOrdinal("TipoComprobante");
            int indNumeroComprobante = dr.GetOrdinal("NumeroComprobante");
            int indNumeroSerie = dr.GetOrdinal("NumeroSerie");
            int indNumeroGuia = dr.GetOrdinal("NumeroGuia");
            int indGlosa = dr.GetOrdinal("Glosa");
            int indEstadoId = dr.GetOrdinal("EstadoId");
            int indEstado = dr.GetOrdinal("Estado");
            int indFormaPagoId = dr.GetOrdinal("FormaPagoId");
            int indFormaPago = dr.GetOrdinal("FormaPago");
            int indFechaEmision = dr.GetOrdinal("FechaEmision");
            int indFechaVencimiento = dr.GetOrdinal("FechaVencimiento");
            int indFechaPago = dr.GetOrdinal("FechaPago");
            int indMonedaId = dr.GetOrdinal("MonedaId");
            int indMoneda = dr.GetOrdinal("Moneda");
            int indSimboloMoneda = dr.GetOrdinal("SimboloMoneda");
            int indTipoCambio = dr.GetOrdinal("TipoCambio");
            int indCantidad = dr.GetOrdinal("Cantidad");
            int indMontoVenta = dr.GetOrdinal("MontoVenta");
            int indDescuento = dr.GetOrdinal("Descuento");
            int indMontoImpuesto = dr.GetOrdinal("MontoImpuesto");
            int indMontoTotal = dr.GetOrdinal("MontoTotal");
            int indComprobanteImpreso = dr.GetOrdinal("ComprobanteImpreso");
            int indActivo = dr.GetOrdinal("Activo");
            int indUsuarioCreacionId = dr.GetOrdinal("UsuarioCreacionId");
            int indUsuarioCreacion = dr.GetOrdinal("UsuarioCreacion");
            int indFechaCreacion = dr.GetOrdinal("FechaCreacion");
            int indUsuarioModificacionId = dr.GetOrdinal("UsuarioModificacionId");
            int indUsuarioModificacion = dr.GetOrdinal("UsuarioModificacion");
            int indFechaModificacion = dr.GetOrdinal("FechaModificacion");
            int indNumeroFila = dr.GetOrdinal("NumeroFila");
            int indTotalFilas = dr.GetOrdinal("TotalFilas");

            int indTipoNegocioId = dr.GetOrdinal("TipoNegocioId");
            int indComprobanteRelacionadoId = dr.GetOrdinal("ComprobanteRelacionadoId");
            
            int indVentaSunatId = dr.GetOrdinal("VentaSunatId");
            int indCodigoRespuesta = dr.GetOrdinal("CodigoRespuesta");
            int indExito = dr.GetOrdinal("Exito");
            int indMensajeError = dr.GetOrdinal("MensajeError");
            int indMensajeRespuesta = dr.GetOrdinal("MensajeRespuesta");
            int indNombreArchivo = dr.GetOrdinal("NombreArchivo");
            // int indPila = dr.GetOrdinal("Pila");
            // int indTramaZipCdr = dr.GetOrdinal("TramaZipCdr");
            #endregion

            var ventaInfo = new VentaInfo();
            dr.GetValues(values);

            #region Campos
            ventaInfo.VentaId = Convert.ToInt32(values[indVentaId]);
            ventaInfo.AgenciaId = Convert.ToInt32(values[indAgenciaId]);
            ventaInfo.Agencia = Convert.ToString(values[indAgencia]);
            ventaInfo.ClienteId = Convert.ToInt32(values[indClienteId]);
            ventaInfo.Cliente = Convert.ToString(values[indCliente]);
            ventaInfo.NumeroDocumento = Convert.ToString(values[indNumeroDocumento]);
            ventaInfo.Direccion = Convert.ToString(values[indDireccion]);
            ventaInfo.TipoComprobanteId = Convert.ToString(values[indTipoComprobanteId]);
            ventaInfo.TipoComprobante = Convert.ToString(values[indTipoComprobante]);
            ventaInfo.NumeroComprobante = Convert.ToString(values[indNumeroComprobante]);
            ventaInfo.NumeroSerie = Convert.ToString(values[indNumeroSerie]);
            ventaInfo.NumeroGuia = Convert.ToString(values[indNumeroGuia]);
            ventaInfo.Glosa = Convert.ToString(values[indGlosa]);
            ventaInfo.EstadoId = Convert.ToInt32(values[indEstadoId]);
            ventaInfo.Estado = Convert.ToString(values[indEstado]);
            ventaInfo.FormaPagoId = Convert.ToInt32(values[indFormaPagoId]);
            ventaInfo.FormaPago = Convert.ToString(values[indFormaPago]);

            if (values[indFechaEmision] != DBNull.Value) ventaInfo.FechaEmision = Convert.ToDateTime(values[indFechaEmision]);
            if (values[indFechaVencimiento] != DBNull.Value) ventaInfo.FechaVencimiento = Convert.ToDateTime(values[indFechaVencimiento]);
            if (values[indFechaPago] != DBNull.Value) ventaInfo.FechaPago = Convert.ToDateTime(values[indFechaPago]);

            ventaInfo.MonedaId = Convert.ToInt32(values[indMonedaId]);
            ventaInfo.Moneda = Convert.ToString(values[indMoneda]);
            ventaInfo.SimboloMoneda = Convert.ToString(values[indSimboloMoneda]);

            ventaInfo.TipoCambio = Convert.ToDecimal(values[indTipoCambio]);
            ventaInfo.Cantidad = Convert.ToDecimal(values[indCantidad]);
            ventaInfo.MontoVenta = Convert.ToDecimal(values[indMontoVenta]);
            ventaInfo.Descuento = Convert.ToDecimal(values[indDescuento]);
            ventaInfo.MontoImpuesto = Convert.ToDecimal(values[indMontoImpuesto]);
            ventaInfo.MontoTotal = Convert.ToDecimal(values[indMontoTotal]);

            ventaInfo.ComprobanteImpreso = Convert.ToString(values[indComprobanteImpreso]);

            ventaInfo.Activo = Convert.ToInt32(values[indActivo]);
            ventaInfo.TipoNegocioId = Convert.ToInt32(values[indTipoNegocioId]);

            ventaInfo.ComprobanteRelacionadoId = Convert.ToInt32(values[indComprobanteRelacionadoId]);
            

            ventaInfo.UsuarioCreacionId = Convert.ToInt32(values[indUsuarioCreacionId]);

            if (values[indUsuarioCreacion] != DBNull.Value) ventaInfo.UsuarioCreacion = Convert.ToString(values[indUsuarioCreacion]);
            ventaInfo.FechaCreacion = Convert.ToDateTime(values[indFechaCreacion]);
            if (values[indUsuarioModificacionId] != DBNull.Value) ventaInfo.UsuarioModificacionId = Convert.ToInt32(values[indUsuarioModificacionId]);
            if (values[indUsuarioModificacion] != DBNull.Value) ventaInfo.UsuarioModificacion = Convert.ToString(values[indUsuarioModificacion]);
            if (values[indFechaModificacion] != DBNull.Value) ventaInfo.FechaModificacion = Convert.ToDateTime(values[indFechaModificacion]);
            ventaInfo.NumeroFila = Convert.ToInt32(values[indNumeroFila]);
            ventaInfo.TotalFilas = Convert.ToInt32(values[indTotalFilas]);

            if (values[indVentaSunatId] != DBNull.Value) ventaInfo.VentaSunatId = Convert.ToInt32(values[indVentaSunatId]);
            if (values[indCodigoRespuesta] != DBNull.Value) ventaInfo.CodigoRespuesta = Convert.ToString(values[indCodigoRespuesta]);
            if (values[indExito] != DBNull.Value) ventaInfo.Exito = Convert.ToInt32(values[indExito]);
            if (values[indMensajeError] != DBNull.Value) ventaInfo.MensajeError = Convert.ToString(values[indMensajeError]);
            if (values[indMensajeRespuesta] != DBNull.Value) ventaInfo.MensajeRespuesta = Convert.ToString(values[indMensajeRespuesta]);
            if (values[indNombreArchivo] != DBNull.Value) ventaInfo.NombreArchivo = Convert.ToString(values[indNombreArchivo]);
            // if (values[indPila] != DBNull.Value) ventaInfo.Pila = Convert.ToInt32(values[indPila]);
            #endregion

            return ventaInfo;
        }
        public int Insertar(VentaInfo ventaInfo)
        {
            int resultado;
            try
            {
                using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                {
                    var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "InsertarVenta", CommandType = CommandType.StoredProcedure };

                    sqlComando.Parameters.Add("AgenciaId", SqlDbType.Int).Value = ventaInfo.AgenciaId;
                    sqlComando.Parameters.Add("ClienteId", SqlDbType.Int).Value = ventaInfo.ClienteId;
                    sqlComando.Parameters.Add("TipoComprobanteId", SqlDbType.VarChar).Value = ventaInfo.TipoComprobanteId;
                    sqlComando.Parameters.Add("NumeroComprobante", SqlDbType.VarChar).Value = ventaInfo.NumeroComprobante;
                    sqlComando.Parameters.Add("NumeroSerie", SqlDbType.VarChar).Value = ventaInfo.NumeroSerie;
                    sqlComando.Parameters.Add("NumeroGuia", SqlDbType.VarChar).Value = ventaInfo.NumeroGuia;
                    sqlComando.Parameters.Add("Glosa", SqlDbType.VarChar).Value = ventaInfo.Glosa;
                    sqlComando.Parameters.Add("EstadoId", SqlDbType.Int).Value = ventaInfo.EstadoId;
                    sqlComando.Parameters.Add("FormaPagoId", SqlDbType.Int).Value = ventaInfo.FormaPagoId;
                    sqlComando.Parameters.Add("FechaEmision", SqlDbType.DateTime2).Value = ventaInfo.FechaEmision;
                    sqlComando.Parameters.Add("FechaVencimiento", SqlDbType.DateTime2).Value = ventaInfo.FechaVencimiento;
                    sqlComando.Parameters.Add("FechaPago", SqlDbType.DateTime2).Value = ventaInfo.FechaPago;
                    sqlComando.Parameters.Add("MonedaId", SqlDbType.Int).Value = ventaInfo.MonedaId;
                    sqlComando.Parameters.Add("TipoCambio", SqlDbType.Decimal).Value = ventaInfo.TipoCambio;
                    sqlComando.Parameters.Add("Cantidad", SqlDbType.Decimal).Value = ventaInfo.Cantidad;
                    sqlComando.Parameters.Add("MontoVenta", SqlDbType.Decimal).Value = ventaInfo.MontoVenta;
                    sqlComando.Parameters.Add("Descuento", SqlDbType.Decimal).Value = ventaInfo.Descuento;
                    sqlComando.Parameters.Add("MontoImpuesto", SqlDbType.Decimal).Value = ventaInfo.MontoImpuesto;
                    sqlComando.Parameters.Add("MontoTotal", SqlDbType.Decimal).Value = ventaInfo.MontoTotal;
                    sqlComando.Parameters.Add("ComprobanteImpreso", SqlDbType.VarChar).Value = ventaInfo.ComprobanteImpreso;
                    sqlComando.Parameters.Add("Activo", SqlDbType.Int).Value = ventaInfo.Activo;
                    sqlComando.Parameters.Add("TipoNegocioId", SqlDbType.Int).Value = ventaInfo.TipoNegocioId;
                    sqlComando.Parameters.Add("ComprobanteRelacionadoId", SqlDbType.Int).Value = ventaInfo.ComprobanteRelacionadoId;
                    sqlComando.Parameters.Add("UsuarioCreacionId", SqlDbType.Int).Value = ventaInfo.UsuarioCreacionId;

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

            int indPlaca = dr.GetOrdinal("Placa");

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

            ventaDetalleInfo.Placa = Convert.ToString(values[indPlaca]);

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
                    sqlComando.Parameters.Add("Placa", SqlDbType.VarChar).Value = ventaDetalleInfo.Placa;
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
                    sqlComando.Parameters.Add("Placa", SqlDbType.VarChar).Value = ventaDetalleInfo.Placa;
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