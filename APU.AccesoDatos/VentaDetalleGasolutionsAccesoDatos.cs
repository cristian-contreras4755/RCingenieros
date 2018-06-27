using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using APU.Entidad;
using APU.Herramientas;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace APU.AccesoDatos
{
    public class VentaDetalleGasolutionsAccesoDatos
    {
        #region Venta
        public List<VentaPetroamericaInfo> Listar(int ventaPetroamericaId, int ventaId)
        {
            var ventaListaInfo = new List<VentaPetroamericaInfo>();
            using (var oConexion = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
            {
                var sqlComando = new SqlCommand();
                sqlComando.Connection = oConexion;
                sqlComando.CommandText = "ObtenerVentaPetroamerica";
                sqlComando.CommandType = CommandType.StoredProcedure;
                sqlComando.Parameters.Add("VentaPetroamericaId", SqlDbType.Int).Value = ventaPetroamericaId;
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
        public List<VentaPetroamericaInfo> ListarPaginado(int ventaId, string numeroDocumento, string tipoComprobanteId, string serie, string correlativo, DateTime fechaInicio, DateTime fechaFin, int estadoId, int monedaId, int tamanioPagina, int numeroPagina)
        {
            var ventaListaInfo = new List<VentaPetroamericaInfo>();
            using (var oConexion = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
            {
                var sqlComando = new SqlCommand();
                sqlComando.Connection = oConexion;
                sqlComando.CommandText = "ObtenerVentaPetroamericaPaginado";
                sqlComando.CommandType = CommandType.StoredProcedure;
                sqlComando.Parameters.Add("VentaId", SqlDbType.Int).Value = ventaId;
                sqlComando.Parameters.Add("NumeroDocumento", SqlDbType.VarChar).Value = numeroDocumento;
                sqlComando.Parameters.Add("TipoComprobanteId", SqlDbType.VarChar).Value = tipoComprobanteId;
                sqlComando.Parameters.Add("Serie", SqlDbType.VarChar).Value = serie;
                sqlComando.Parameters.Add("Correlativo", SqlDbType.VarChar).Value = correlativo;
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
        private static VentaPetroamericaInfo CargarVentaInfo(IDataReader dr)
        {
            int colCount = dr.FieldCount;
            var values = new object[colCount];

            #region Indices
            int indVentaPetroamericaId = dr.GetOrdinal("VentaPetroamericaId");
            int indVentaId = dr.GetOrdinal("VentaId");

            int indAgenciaId = dr.GetOrdinal("AgenciaId");
            int indFechaEmision = dr.GetOrdinal("FechaEmision");


            int indSerieId = dr.GetOrdinal("SerieId");
            int indSerie = dr.GetOrdinal("Serie");
            int indMonedaId = dr.GetOrdinal("MonedaId");
            int indMoneda = dr.GetOrdinal("Moneda");
            int indSimboloMoneda = dr.GetOrdinal("SimboloMoneda");
            int indNumeroComprobante = dr.GetOrdinal("NumeroComprobante");
            int indMontoVenta = dr.GetOrdinal("MontoVenta");
            int indMontoImpuesto = dr.GetOrdinal("MontoImpuesto");
            int indMontoTotal = dr.GetOrdinal("MontoTotal");
            int indCantidad = dr.GetOrdinal("Cantidad");
            // int inddet_pre = dr.GetOrdinal("det_pre");
            int indTipoComprobanteId = dr.GetOrdinal("TipoComprobanteId");
            int indTipoComprobante = dr.GetOrdinal("TipoComprobante");
            int indPrecio = dr.GetOrdinal("Precio");

            int indComprobanteImpreso = dr.GetOrdinal("ComprobanteImpreso");

            int indAgencia = dr.GetOrdinal("Agencia");
            int indDireccionAgencia = dr.GetOrdinal("DireccionAgencia");
            int indImpresoraAgencia = dr.GetOrdinal("ImpresoraAgencia");

            int indRucEmpresa = dr.GetOrdinal("RucEmpresa");
            int indRazonSocialEmpresa = dr.GetOrdinal("RazonSocialEmpresa");
            int indDireccionEmpresa = dr.GetOrdinal("DireccionEmpresa");


            int indClienteId = dr.GetOrdinal("ClienteId");
            int indTipoPersonaIdCliente = dr.GetOrdinal("TipoPersonaIdCliente");
            int indCliente = dr.GetOrdinal("Cliente");
            int indTipoDocumentoIdCliente = dr.GetOrdinal("TipoDocumentoIdCliente");
            int indTipoDocumentoCliente = dr.GetOrdinal("TipoDocumentoCliente");
            int indNumeroDocumentoCliente = dr.GetOrdinal("NumeroDocumentoCliente");
            int indDireccionCliente = dr.GetOrdinal("DireccionCliente");
            int indTelefonoCliente = dr.GetOrdinal("TelefonoCliente");
            int indPlacaVehiculo = dr.GetOrdinal("PlacaVehiculo");

            int indProductoId = dr.GetOrdinal("ProductoId");
            int indProducto = dr.GetOrdinal("Producto");

            int indUsuarioCreacionId = dr.GetOrdinal("UsuarioCreacionId");
            int indUsuarioCreacion = dr.GetOrdinal("UsuarioCreacion");

            int indVentaSunatId = dr.GetOrdinal("VentaSunatId");
            int indCodigoRespuesta = dr.GetOrdinal("CodigoRespuesta");
            int indExito = dr.GetOrdinal("Exito");
            int indMensajeError = dr.GetOrdinal("MensajeError");
            int indMensajeRespuesta = dr.GetOrdinal("MensajeRespuesta");
            int indNombreArchivo = dr.GetOrdinal("NombreArchivo");
            int indNroTicket = dr.GetOrdinal("NroTicket");

            int indEstadoId = dr.GetOrdinal("EstadoId");
            int indEstado = dr.GetOrdinal("Estado");

            int indNumeroFila = dr.GetOrdinal("NumeroFila");
            int indTotalFilas = dr.GetOrdinal("TotalFilas");

            #endregion

            var ventaInfo = new VentaPetroamericaInfo();
            dr.GetValues(values);

            #region Campos
            ventaInfo.VentaPetroamericaId = Convert.ToInt32(values[indVentaPetroamericaId]);
            ventaInfo.VentaId = Convert.ToInt32(values[indVentaId]);
            ventaInfo.AgenciaId = Convert.ToInt32(values[indAgenciaId]);
            ventaInfo.ClienteId = Convert.ToInt32(values[indClienteId]);
            ventaInfo.NumeroComprobante = Convert.ToInt32(values[indNumeroComprobante]);
            ventaInfo.SerieId = Convert.ToInt32(values[indSerieId]);
            ventaInfo.Serie = Convert.ToString(values[indSerie]);
            if (values[indFechaEmision] != DBNull.Value) ventaInfo.FechaEmision = Convert.ToDateTime(values[indFechaEmision]);
            ventaInfo.MonedaId = Convert.ToInt16(values[indMonedaId]);
            ventaInfo.Moneda = Convert.ToString(values[indMoneda]);
            ventaInfo.SimboloMoneda = Convert.ToString(values[indSimboloMoneda]);
            ventaInfo.Cantidad = Convert.ToDecimal(values[indCantidad]);
            ventaInfo.MontoVenta = Convert.ToDecimal(values[indMontoVenta]);
            // ventaInfo.Descuento = Convert.ToDecimal(values[indDescuento]);
            ventaInfo.MontoImpuesto = Convert.ToDecimal(values[indMontoImpuesto]);
            ventaInfo.MontoTotal = Convert.ToDecimal(values[indMontoTotal]);
            ventaInfo.TipoComprobanteId = Convert.ToString(values[indTipoComprobanteId]);
            ventaInfo.TipoComprobante = Convert.ToString(values[indTipoComprobante]);
            ventaInfo.Cliente = Convert.ToString(values[indCliente]);
            ventaInfo.Agencia = Convert.ToString(values[indAgencia]);
            ventaInfo.DireccionAgencia = Convert.ToString(values[indDireccionAgencia]);
            ventaInfo.ImpresoraAgencia = Convert.ToString(values[indImpresoraAgencia]);

            ventaInfo.RucEmpresa = Convert.ToString(values[indRucEmpresa]);
            ventaInfo.RazonSocialEmpresa = Convert.ToString(values[indRazonSocialEmpresa]);
            ventaInfo.DireccionEmpresa = Convert.ToString(values[indDireccionEmpresa]);

            ventaInfo.TipoPersonaIdCliente = Convert.ToInt32(values[indTipoPersonaIdCliente]);
            ventaInfo.TipoDocumentoIdCliente = Convert.ToString(values[indTipoDocumentoIdCliente]);
            ventaInfo.TipoDocumentoCliente = Convert.ToString(values[indTipoDocumentoCliente]);
            ventaInfo.NumeroDocumentoCliente = Convert.ToString(values[indNumeroDocumentoCliente]);
            ventaInfo.DireccionCliente = Convert.ToString(values[indDireccionCliente]);
            ventaInfo.TelefonoCliente = Convert.ToString(values[indTelefonoCliente]);
            ventaInfo.PlacaVehiculo = Convert.ToString(values[indPlacaVehiculo]);

            ventaInfo.Precio = Convert.ToDecimal(values[indPrecio]);

            ventaInfo.ComprobanteImpreso = Convert.ToString(values[indComprobanteImpreso]);

            //ventaInfo.Activo = Convert.ToInt32(values[indActivo]);

            ventaInfo.UsuarioCreacionId = Convert.ToInt32(values[indUsuarioCreacionId]);

            if (values[indUsuarioCreacion] != DBNull.Value) ventaInfo.UsuarioCreacion = Convert.ToString(values[indUsuarioCreacion]);
            //ventaInfo.FechaCreacion = Convert.ToDateTime(values[indFechaCreacion]);
            //if (values[indUsuarioModificacionId] != DBNull.Value) ventaInfo.UsuarioModificacionId = Convert.ToInt32(values[indUsuarioModificacionId]);
            //if (values[indUsuarioModificacion] != DBNull.Value) ventaInfo.UsuarioModificacion = Convert.ToString(values[indUsuarioModificacion]);
            //if (values[indFechaModificacion] != DBNull.Value) ventaInfo.FechaModificacion = Convert.ToDateTime(values[indFechaModificacion]);
            ventaInfo.NumeroFila = Convert.ToInt32(values[indNumeroFila]);
            ventaInfo.TotalFilas = Convert.ToInt32(values[indTotalFilas]);

            ventaInfo.ProductoId = Convert.ToInt32(values[indProductoId]);
            ventaInfo.Producto = Convert.ToString(values[indProducto]);

            if (values[indVentaSunatId] != DBNull.Value) ventaInfo.VentaSunatId = Convert.ToInt32(values[indVentaSunatId]);
            if (values[indCodigoRespuesta] != DBNull.Value) ventaInfo.CodigoRespuesta = Convert.ToString(values[indCodigoRespuesta]);
            if (values[indExito] != DBNull.Value) ventaInfo.Exito = Convert.ToInt32(values[indExito]);
            if (values[indMensajeError] != DBNull.Value) ventaInfo.MensajeError = Convert.ToString(values[indMensajeError]);
            if (values[indMensajeRespuesta] != DBNull.Value) ventaInfo.MensajeRespuesta = Convert.ToString(values[indMensajeRespuesta]);
            if (values[indNombreArchivo] != DBNull.Value) ventaInfo.NombreArchivo = Convert.ToString(values[indNombreArchivo]);
            if (values[indNroTicket] != DBNull.Value) ventaInfo.NroTicket = Convert.ToString(values[indNroTicket]);

            ventaInfo.EstadoId = Convert.ToInt32(values[indEstadoId]);
            ventaInfo.Estado = Convert.ToString(values[indEstado]);

            // if (values[indPila] != DBNull.Value) ventaInfo.Pila = Convert.ToInt32(values[indPila]);
            #endregion

            return ventaInfo;
        }

        public int Insertar(VentaPetroamericaInfo ventaInfo)
        {
            int resultado;
            try
            {
                using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                {
                    var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "InsertarVentaPetroamerica", CommandType = CommandType.StoredProcedure };

                    sqlComando.Parameters.Add("VentaId", SqlDbType.Int).Value = ventaInfo.VentaId;
                    sqlComando.Parameters.Add("AgenciaId", SqlDbType.Int).Value = ventaInfo.AgenciaId;
                    sqlComando.Parameters.Add("FechaEmision", SqlDbType.DateTime2).Value = ventaInfo.FechaEmision;
                    sqlComando.Parameters.Add("SerieId", SqlDbType.Int).Value = ventaInfo.SerieId;
                    sqlComando.Parameters.Add("Serie", SqlDbType.VarChar).Value = ventaInfo.Serie;
                    sqlComando.Parameters.Add("NumeroComprobante", SqlDbType.Int).Value = ventaInfo.NumeroComprobante;
                    sqlComando.Parameters.Add("MontoVenta", SqlDbType.Decimal).Value = ventaInfo.MontoVenta;
                    sqlComando.Parameters.Add("MontoImpuesto", SqlDbType.Decimal).Value = ventaInfo.MontoImpuesto;
                    sqlComando.Parameters.Add("MontoTotal", SqlDbType.Decimal).Value = ventaInfo.MontoTotal;
                    sqlComando.Parameters.Add("Cantidad", SqlDbType.Decimal).Value = ventaInfo.Cantidad;
                    sqlComando.Parameters.Add("Precio", SqlDbType.Decimal).Value = ventaInfo.Precio;
                    sqlComando.Parameters.Add("MonedaId", SqlDbType.Int).Value = ventaInfo.MonedaId;
                    sqlComando.Parameters.Add("Moneda", SqlDbType.VarChar).Value = ventaInfo.Moneda;
                    sqlComando.Parameters.Add("SimboloMoneda", SqlDbType.VarChar).Value = ventaInfo.SimboloMoneda;
                    sqlComando.Parameters.Add("TipoComprobanteId", SqlDbType.VarChar).Value = ventaInfo.TipoComprobanteId;
                    sqlComando.Parameters.Add("TipoComprobante", SqlDbType.VarChar).Value = ventaInfo.TipoComprobante;
                    sqlComando.Parameters.Add("PlacaVehiculo", SqlDbType.VarChar).Value = ventaInfo.PlacaVehiculo;
                    sqlComando.Parameters.Add("ClienteId", SqlDbType.Int).Value = ventaInfo.ClienteId;
                    sqlComando.Parameters.Add("TipoPersonaIdCliente", SqlDbType.Int).Value = ventaInfo.TipoPersonaIdCliente;
                    sqlComando.Parameters.Add("Cliente", SqlDbType.VarChar).Value = ventaInfo.Cliente;
                    sqlComando.Parameters.Add("TipoDocumentoIdCliente", SqlDbType.Int).Value = ventaInfo.TipoDocumentoIdCliente;
                    sqlComando.Parameters.Add("TipoDocumentoCliente", SqlDbType.VarChar).Value = ventaInfo.TipoDocumentoCliente;

                    sqlComando.Parameters.Add("NumeroDocumentoCliente", SqlDbType.VarChar).Value = ventaInfo.NumeroDocumentoCliente;
                    sqlComando.Parameters.Add("DireccionCliente", SqlDbType.VarChar).Value = ventaInfo.DireccionCliente;
                    sqlComando.Parameters.Add("TelefonoCliente", SqlDbType.VarChar).Value = ventaInfo.TelefonoCliente;
                    sqlComando.Parameters.Add("Agencia", SqlDbType.VarChar).Value = ventaInfo.Agencia;
                    sqlComando.Parameters.Add("DireccionAgencia", SqlDbType.VarChar).Value = ventaInfo.DireccionAgencia;
                    sqlComando.Parameters.Add("ImpresoraAgencia", SqlDbType.VarChar).Value = ventaInfo.ImpresoraAgencia;
                    sqlComando.Parameters.Add("RucEmpresa", SqlDbType.VarChar).Value = ventaInfo.RucEmpresa;
                    sqlComando.Parameters.Add("RazonSocialEmpresa", SqlDbType.VarChar).Value = ventaInfo.RazonSocialEmpresa;
                    sqlComando.Parameters.Add("DireccionEmpresa", SqlDbType.VarChar).Value = ventaInfo.DireccionEmpresa;
                    sqlComando.Parameters.Add("ProductoId", SqlDbType.Int).Value = ventaInfo.ProductoId;
                    sqlComando.Parameters.Add("Producto", SqlDbType.VarChar).Value = ventaInfo.Producto;
                    sqlComando.Parameters.Add("UsuarioCreacionId", SqlDbType.Int).Value = ventaInfo.UsuarioCreacionId;
                    sqlComando.Parameters.Add("UsuarioCreacion", SqlDbType.VarChar).Value = ventaInfo.UsuarioCreacion;
                    sqlComando.Parameters.Add("VentaSunatId", SqlDbType.Int).Value = ventaInfo.VentaSunatId;
                    sqlComando.Parameters.Add("CodigoRespuesta", SqlDbType.VarChar).Value = ventaInfo.CodigoRespuesta;
                    sqlComando.Parameters.Add("Exito", SqlDbType.Int).Value = ventaInfo.Exito;
                    sqlComando.Parameters.Add("MensajeError", SqlDbType.VarChar).Value = ventaInfo.MensajeError;
                    sqlComando.Parameters.Add("MensajeRespuesta", SqlDbType.VarChar).Value = ventaInfo.MensajeRespuesta;
                    sqlComando.Parameters.Add("NombreArchivo", SqlDbType.VarChar).Value = ventaInfo.NombreArchivo;
                    sqlComando.Parameters.Add("NroTicket", SqlDbType.VarChar).Value = ventaInfo.NroTicket;
                    sqlComando.Parameters.Add("EstadoId", SqlDbType.Int).Value = ventaInfo.EstadoId;
                    sqlComando.Parameters.Add("Estado", SqlDbType.VarChar).Value = ventaInfo.Estado;
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
        public int InsertarLote(List<VentaPetroamericaInfo> ventaListaInfo)
        {
            int resultado;
            try
            {
                foreach (var ventaInfo in ventaListaInfo)
                {
                    using (var oConnection = new SqlConnection(HelperAccesoDatos.GetCadenaConexion()))
                    {
                        var sqlComando = new SqlCommand { Connection = oConnection, CommandText = "InsertarVentaPetroamerica", CommandType = CommandType.StoredProcedure };

                        sqlComando.Parameters.Add("VentaId", SqlDbType.Int).Value = ventaInfo.VentaId;
                        sqlComando.Parameters.Add("AgenciaId", SqlDbType.Int).Value = ventaInfo.AgenciaId;
                        sqlComando.Parameters.Add("FechaEmision", SqlDbType.DateTime2).Value = ventaInfo.FechaEmision;
                        sqlComando.Parameters.Add("SerieId", SqlDbType.Int).Value = ventaInfo.SerieId;
                        sqlComando.Parameters.Add("Serie", SqlDbType.VarChar).Value = ventaInfo.Serie;
                        sqlComando.Parameters.Add("NumeroComprobante", SqlDbType.Int).Value = ventaInfo.NumeroComprobante;
                        sqlComando.Parameters.Add("MontoVenta", SqlDbType.Decimal).Value = ventaInfo.MontoVenta;
                        sqlComando.Parameters.Add("MontoImpuesto", SqlDbType.Decimal).Value = ventaInfo.MontoImpuesto;
                        sqlComando.Parameters.Add("MontoTotal", SqlDbType.Decimal).Value = ventaInfo.MontoTotal;
                        sqlComando.Parameters.Add("Cantidad", SqlDbType.Decimal).Value = ventaInfo.Cantidad;
                        sqlComando.Parameters.Add("Precio", SqlDbType.Decimal).Value = ventaInfo.Precio;
                        sqlComando.Parameters.Add("MonedaId", SqlDbType.Int).Value = ventaInfo.MonedaId;
                        sqlComando.Parameters.Add("Moneda", SqlDbType.VarChar).Value = ventaInfo.Moneda;
                        sqlComando.Parameters.Add("SimboloMoneda", SqlDbType.VarChar).Value = ventaInfo.SimboloMoneda;
                        sqlComando.Parameters.Add("TipoComprobanteId", SqlDbType.VarChar).Value = ventaInfo.TipoComprobanteId;
                        sqlComando.Parameters.Add("TipoComprobante", SqlDbType.VarChar).Value = ventaInfo.TipoComprobante;
                        sqlComando.Parameters.Add("PlacaVehiculo", SqlDbType.VarChar).Value = ventaInfo.PlacaVehiculo;
                        sqlComando.Parameters.Add("ClienteId", SqlDbType.Int).Value = ventaInfo.ClienteId;
                        sqlComando.Parameters.Add("TipoPersonaIdCliente", SqlDbType.Int).Value = ventaInfo.TipoPersonaIdCliente;
                        sqlComando.Parameters.Add("Cliente", SqlDbType.VarChar).Value = ventaInfo.Cliente;
                        sqlComando.Parameters.Add("TipoDocumentoIdCliente", SqlDbType.Int).Value = ventaInfo.TipoDocumentoIdCliente;
                        sqlComando.Parameters.Add("TipoDocumentoCliente", SqlDbType.VarChar).Value = ventaInfo.TipoDocumentoCliente;

                        sqlComando.Parameters.Add("NumeroDocumentoCliente", SqlDbType.VarChar).Value = ventaInfo.NumeroDocumentoCliente;
                        sqlComando.Parameters.Add("DireccionCliente", SqlDbType.VarChar).Value = ventaInfo.DireccionCliente;
                        sqlComando.Parameters.Add("TelefonoCliente", SqlDbType.VarChar).Value = ventaInfo.TelefonoCliente;
                        sqlComando.Parameters.Add("Agencia", SqlDbType.VarChar).Value = ventaInfo.Agencia;
                        sqlComando.Parameters.Add("DireccionAgencia", SqlDbType.VarChar).Value = ventaInfo.DireccionAgencia;
                        sqlComando.Parameters.Add("ImpresoraAgencia", SqlDbType.VarChar).Value = ventaInfo.ImpresoraAgencia;
                        sqlComando.Parameters.Add("RucEmpresa", SqlDbType.VarChar).Value = ventaInfo.RucEmpresa;
                        sqlComando.Parameters.Add("RazonSocialEmpresa", SqlDbType.VarChar).Value = ventaInfo.RazonSocialEmpresa;
                        sqlComando.Parameters.Add("DireccionEmpresa", SqlDbType.VarChar).Value = ventaInfo.DireccionEmpresa;
                        sqlComando.Parameters.Add("ProductoId", SqlDbType.Int).Value = ventaInfo.ProductoId;
                        sqlComando.Parameters.Add("Producto", SqlDbType.VarChar).Value = ventaInfo.Producto;
                        sqlComando.Parameters.Add("UsuarioCreacionId", SqlDbType.Int).Value = ventaInfo.UsuarioCreacionId;
                        sqlComando.Parameters.Add("UsuarioCreacion", SqlDbType.VarChar).Value = ventaInfo.UsuarioCreacion;
                        sqlComando.Parameters.Add("VentaSunatId", SqlDbType.Int).Value = ventaInfo.VentaSunatId;
                        sqlComando.Parameters.Add("CodigoRespuesta", SqlDbType.VarChar).Value = ventaInfo.CodigoRespuesta;
                        sqlComando.Parameters.Add("Exito", SqlDbType.Int).Value = ventaInfo.Exito;
                        sqlComando.Parameters.Add("MensajeError", SqlDbType.VarChar).Value = ventaInfo.MensajeError;
                        sqlComando.Parameters.Add("MensajeRespuesta", SqlDbType.VarChar).Value = ventaInfo.MensajeRespuesta;
                        sqlComando.Parameters.Add("NombreArchivo", SqlDbType.VarChar).Value = ventaInfo.NombreArchivo;
                        sqlComando.Parameters.Add("NroTicket", SqlDbType.VarChar).Value = ventaInfo.NroTicket;
                        sqlComando.Parameters.Add("EstadoId", SqlDbType.Int).Value = ventaInfo.EstadoId;
                        sqlComando.Parameters.Add("Estado", SqlDbType.VarChar).Value = ventaInfo.Estado;
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