using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using APU.Entidad;
using APU.Herramientas;
using APU.Negocio;
using OpenInvoicePeru.Comun.Dto.Intercambio;
using OpenInvoicePeru.Comun.Dto.Modelos;
using RestSharp;

namespace APU.ServicioWindows
{
    public partial class APUServicioWindows : ServiceBase
    {
        private double intervalo = 0;
        public static DocumentoElectronico _documento;
        public APUServicioWindows()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            intervalo = Convert.ToDouble(ConfigurationManager.AppSettings.Get("Intervalo"));
            try
            {
                Thread.Sleep(10000);
                Thread.Sleep(3000);

                //var oParametro = new EMSNegocio.Parametro();
                //var oParametroInfo = new EMSEntidad.ParametroEmailingInfo();
                //oParametroInfo.NombreParametro = "EM_ESTADO_EJECUCION_SERVICIO";
                //oParametroInfo.ValorParametro = "S";
                //oParametro.ActualizarParametroEmailing(oParametroInfo);

                timEnvioSunat.AutoReset = true;
                timEnvioSunat.Enabled = true;
                timEnvioSunat.Interval = intervalo;
                timEnvioSunat.Start();
            }
            catch (Exception ex)
            {
                timEnvioSunat.AutoReset = true;
                timEnvioSunat.Enabled = true;
                timEnvioSunat.Interval = intervalo;
                timEnvioSunat.Start();
            }
        }
        protected override void OnStop()
        {
            timEnvioSunat.AutoReset = false;
            timEnvioSunat.Enabled = false;
        }

        private string EstadoServicio()
        {
            var sc = new ServiceController(this.ServiceName);
            var estadoServicio = sc.Status;

            switch (sc.Status)
            {
                case ServiceControllerStatus.Running:
                    //return "Running";
                    return "1";
                case ServiceControllerStatus.Stopped:
                    //return "Stopped";
                    return "2";
                case ServiceControllerStatus.Paused:
                    //return "Paused";
                    return "3";
                case ServiceControllerStatus.StopPending:
                    //return "Stopping";
                    return "4";
                case ServiceControllerStatus.StartPending:
                    //return "Starting";
                    return "5";
                default:
                    //return "Status Changing";
                    return "0";
            }
        }

        private void timEnvioSunat_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            var estadoServicio = EstadoServicio();
            try
            {
                //var oParametro = new EMSNegocio.Parametro();
                //var oParametroInfo = new EMSEntidad.ParametroEmailingInfo();
                //oParametroInfo = oParametro.ListarParametroEmailing("EM_ESTADO_EJECUCION_SERVICIO").First();
                //var estadoEjecucion = oParametroInfo.ValorParametro;

                //var estadoProceso = Negocio.Helper.ObtenerValorParametro("ESTADO_SERVICIO_WINDOWS").ToString();
                //if (estadoProceso.Equals("N"))
                //{
                //    EnvioSunat();
                //}

                this.timEnvioSunat.Stop();
                EnvioSunat();
                //this.timEnvioSunat.Start();
            }
            catch (Exception ex)
            {
                this.timEnvioSunat.Start();
                // throw ex;
            }
            finally
            {
                this.timEnvioSunat.Start();
            }
        }
        // https://github.com/FrameworkPeru/ClienteOpenInvoicePeru-net40/blob/master/Clientes%20OpenInvoicePeru/OpenInvoicePeru.ApiClientCSharp/Program.cs
        private static readonly string BaseUrl = "http://localhost:88/APU.WebApi/api";
        private static readonly string UrlSunat = "https://e-beta.sunat.gob.pe/ol-ti-itcpfegem-beta/billService";
        private const string FormatoFecha = "yyyy-MM-dd";
        public void EnvioSunat()
        {
            try
            {
                var estadoProceso = Negocio.Helper.ObtenerValorParametro("ESTADO_SERVICIO_WINDOWS").ToString();

                if (estadoProceso.Equals("P"))
                {
                    return;
                }

                if (estadoProceso.Equals("N"))
                {
                    Negocio.Helper.ActualizarColumnasTabla("ParametrosGlobales", new string[] { "ValorParametro" }, new string[] { "P" }, new string[] { "NombreParametro" }, new string[] { "ESTADO_SERVICIO_WINDOWS" });
                    var ventaPendienteListaInfo = new Venta().Listar(0).Where(v => v.Exito.Equals(0)).ToList();

                    foreach (var v in ventaPendienteListaInfo)
                    {
                        //var ventaId = 1;
                        var ventaId = v.VentaId;
                        var mensaje = String.Empty;

                        //var usuarioInfo = ObtenerUsuarioInfo();
                        var usuarioInfo = new Usuario().Listar(1, "", "", "", 0, 0).FirstOrDefault();
                        var ventaInfo = new Venta().Listar(ventaId).FirstOrDefault();
                        var ventaDetalleInfo = new Venta().ListarDetalle(ventaId, 0);
                        var empresaInfo = new Empresa().Listar(usuarioInfo.EmpresaId).FirstOrDefault();
                        var clienteInfo = new Cliente().Listar(ventaInfo.ClienteId).FirstOrDefault();

                        _documento = new DocumentoElectronico();

                        #region Documento
                        _documento.CalculoDetraccion = 0;
                        _documento.CalculoIgv = Decimal.Divide(18, 100);
                        _documento.CalculoIsc = 0;
                        _documento.DescuentoGlobal = ventaInfo.Descuento;
                        // _documento.DocAnticipo = String.Empty;

                        #region Emisor
                        var emisor = new DocumentoElectronico().Emisor;
                        emisor.Departamento = empresaInfo.Departamento;
                        //emisor.Departamento = "LIMA";
                        emisor.Direccion = empresaInfo.Direccion;
                        //emisor.Direccion = "LADERAS DE CHILLON";
                        emisor.Distrito = empresaInfo.Distrito;
                        //emisor.Distrito = "PUENTE PIEDRA";
                        emisor.NombreComercial = empresaInfo.RazonSocial;
                        emisor.NombreLegal = empresaInfo.RazonSocial;
                        //emisor.NombreComercial = "ABARCA URBANO";
                        //emisor.NombreLegal = "MIGUEL ABARCA URBANO";
                        //emisor.NroDocumento = empresaInfo.NumeroDocumento;
                        emisor.NroDocumento = "10421895452";
                        emisor.Provincia = empresaInfo.Provincia;
                        //emisor.Provincia = "LIMA";
                        emisor.TipoDocumento = empresaInfo.TipoDocumentoId.ToString();
                        emisor.Ubigeo = empresaInfo.Departamento;
                        emisor.Urbanizacion = empresaInfo.Direccion;
                        //emisor.Urbanizacion = "LADERAS DE CHILLON";
                        _documento.Emisor = emisor;
                        #endregion

                        _documento.Exoneradas = 0;
                        _documento.FechaEmision = ventaInfo.FechaEmision.ToString("dd/MM/yyyy");
                        //_documento.FechaEmision = "2017-11-27";
                        _documento.Gratuitas = 0;
                        _documento.Gravadas = ventaInfo.MontoVenta;
                        //_documento.Gravadas = 1000;
                        _documento.IdDocumento = ventaInfo.NumeroSerie + "-" + ventaInfo.NumeroComprobante;
                        _documento.Inafectas = 0;

                        #region Items
                        var itemId = 1;
                        foreach (var vd in ventaDetalleInfo)
                        {
                            var item = new DetalleDocumento();
                            item.Cantidad = vd.Cantidad;
                            // item.CodigoItem = vd.Codigo;

                            item.Descripcion = vd.Producto;
                            //item.Descuento = vd.Descuento;
                            item.Id = itemId;
                            itemId++;
                            item.Impuesto = vd.Igv;
                            //item.ImpuestoSelectivo = 0;
                            //item.OtroImpuesto = 0;
                            //item.PrecioReferencial = 0;
                            item.PrecioUnitario = vd.PrecioUnitario;
                            item.Suma = vd.SubTotal;
                            //item.TipoImpuesto = "10";
                            //item.TipoPrecio = "01";
                            item.TotalVenta = vd.MontoTotal;
                            item.UnidadMedida = vd.UnidadMedida;
                            //item.CodigoItem = "A0001";
                            item.Descuento = 0;
                            //item.Id = 1;
                            item.ImpuestoSelectivo = 0;
                            item.OtroImpuesto = 0;
                            item.PrecioReferencial = 0;
                            // item.PrecioUnitario = 50;
                            item.TipoImpuesto = "10";
                            item.TipoPrecio = "01";
                            // item.UnidadMedida = "NIU";
                            _documento.Items.Add(item);
                        }
                        #endregion

                        _documento.Moneda = ventaInfo.MonedaId.Equals(Constantes.MonedaSoles) ? Constantes.MonedaSolesSunat : Constantes.MonedaDolaresSunat;
                        // _documento.MonedaAnticipo = 0.18;
                        _documento.MontoAnticipo = 0;
                        _documento.MontoDetraccion = 0;
                        _documento.MontoEnLetras = Herramientas.NumeroALetras.numeroAletras(ventaInfo.MontoVenta);
                        //_documento.MontoEnLetras = "MIL";
                        _documento.MontoPercepcion = 0;
                        // _documento.PlacaVehiculo = ventaInfo.Placa;

                        #region Receptor
                        var receptor = new DocumentoElectronico().Receptor;
                        receptor.Departamento = clienteInfo.Departamento;
                        receptor.Direccion = clienteInfo.Direccion;
                        receptor.Distrito = clienteInfo.Distrito;
                        receptor.NombreComercial = clienteInfo.RazonSocial;
                        receptor.NombreLegal = clienteInfo.RazonSocial;
                        //receptor.NombreComercial = "RENE ABARCA URBANO";
                        //receptor.NombreLegal = "RENE ABARCA URBANO";
                        //receptor.NroDocumento = clienteInfo.NumeroDocumento;
                        receptor.NroDocumento = "10415787796";
                        receptor.Provincia = clienteInfo.Provincia;
                        receptor.TipoDocumento = clienteInfo.TipoDocumentoId.ToString();
                        receptor.Ubigeo = clienteInfo.Departamento;
                        receptor.Urbanizacion = clienteInfo.Direccion;
                        _documento.Receptor = receptor;
                        #endregion

                        // _documento.TipoDocAnticipo = 0;
                        _documento.TipoDocumento = ventaInfo.TipoComprobanteId;
                        // _documento.TipoOperacion = ventaInfo.TipoOperacionId;
                        _documento.TotalIgv = ventaInfo.MontoImpuesto;
                        //_documento.TotalIgv = 180;
                        _documento.TotalIsc = 0;
                        _documento.TotalOtrosTributos = 0;
                        _documento.TotalVenta = ventaInfo.MontoTotal;
                        //_documento.TotalVenta = 1180;
                        #endregion

                        //var proxy = new HttpClient { BaseAddress = new Uri(ConfigurationManager.AppSettings["UrlOpenInvoicePeruApi"]) };

                        string metodoApi;
                        switch (_documento.TipoDocumento)
                        {
                            case "07":
                                metodoApi = "api/GenerarNotaCredito";
                                break;
                            case "08":
                                metodoApi = "api/GenerarNotaDebito";
                                break;
                            default:
                                metodoApi = "api/GenerarFactura";
                                break;
                        }

                        //var response = await proxy.PostAsJsonAsync(metodoApi, _documento);
                        //var respuesta = await response.Content.ReadAsAsync<DocumentoResponse>();

                        #region Generando XML
                        var client = new RestClient(BaseUrl);

                        var requestInvoice = new RestRequest("GenerarFactura", Method.POST)
                        {
                            RequestFormat = DataFormat.Json
                        };

                        requestInvoice.AddBody(_documento);

                        var documentoResponse = client.Execute<DocumentoResponse>(requestInvoice);

                        if (!documentoResponse.Data.Exito)
                        {
                            throw new ApplicationException(documentoResponse.Data.MensajeError);
                        }
                        //string rutaXml = HostingEnvironment.MapPath("~/Archivos/Facturacion/XML/" + _documento.IdDocumento + ".xml");
                        string rutaXml = @"D:\ASOLUTIONS\APU\APU.Presentacion\Archivos\Facturacion\XML\" + _documento.IdDocumento + ".xml";
                        File.WriteAllBytes(rutaXml, Convert.FromBase64String(documentoResponse.Data.TramaXmlSinFirma));
                        #endregion

                        #region Firma
                        //string rutaCertificado = HostingEnvironment.MapPath("~/Archivos/Facturacion/certificado.pfx");
                        var rutaCertificado = @"D:\ASOLUTIONS\APU\APU.Presentacion\certificado.pfx";
                        var firmado = new FirmadoRequest
                        {
                            TramaXmlSinFirma = documentoResponse.Data.TramaXmlSinFirma,
                            //CertificadoDigital = Convert.ToBase64String(File.ReadAllBytes("certificado.pfx")),
                            CertificadoDigital = Convert.ToBase64String(File.ReadAllBytes(rutaCertificado)),
                            PasswordCertificado = string.Empty,
                            UnSoloNodoExtension = false
                        };

                        var requestFirma = new RestRequest("Firmar", Method.POST)
                        {
                            RequestFormat = DataFormat.Json
                        };
                        requestFirma.AddBody(firmado);

                        var responseFirma = client.Execute<FirmadoResponse>(requestFirma);

                        if (!responseFirma.Data.Exito)
                        {
                            throw new ApplicationException(responseFirma.Data.MensajeError);
                        }
                        //string rutaXmlFirmado = HostingEnvironment.MapPath("~/Archivos/Facturacion/XML/" + _documento.IdDocumento + "_Firmado.xml");
                        string rutaXmlFirmado = @"D:\ASOLUTIONS\APU\APU.Presentacion\Archivos\Facturacion\XML\" + _documento.IdDocumento + "_Firmado.xml";
                        File.WriteAllBytes(rutaXmlFirmado, Convert.FromBase64String(responseFirma.Data.TramaXmlFirmado));
                        #endregion

                        #region Envio SUNAT
                        var sendBill = new EnviarDocumentoRequest
                        {
                            //Ruc = _documento.Emisor.NroDocumento,
                            Ruc = "10421895452",
                            //UsuarioSol = "MODDATOS",
                            UsuarioSol = "MMINSIOT",
                            //ClaveSol = "MODDATOS",
                            ClaveSol = "saywalcod",
                            EndPointUrl = UrlSunat,
                            IdDocumento = _documento.IdDocumento,
                            TipoDocumento = _documento.TipoDocumento,
                            TramaXmlFirmado = responseFirma.Data.TramaXmlFirmado
                        };

                        var requestSendBill = new RestRequest("EnviarDocumento", Method.POST)
                        {
                            RequestFormat = DataFormat.Json
                        };
                        requestSendBill.AddBody(sendBill);

                        var responseSendBill = client.Execute<EnviarDocumentoResponse>(requestSendBill);

                        if (!responseSendBill.Data.Exito)
                        {
                            // throw new ApplicationException(responseSendBill.Data.MensajeError);
                        }
                        else
                        {
                            //string rutaCdr = HostingEnvironment.MapPath("~/Archivos/Facturacion/CDR/" + responseSendBill.Data.NombreArchivo + ".zip");
                            string rutaCdr = @"D:\ASOLUTIONS\APU\APU.Presentacion\Archivos\Facturacion\CDR\" + responseSendBill.Data.NombreArchivo + ".zip";
                            File.WriteAllBytes(rutaCdr, Convert.FromBase64String(responseSendBill.Data.TramaZipCdr));
                        }

                        //Console.WriteLine("Respuesta de SUNAT:");
                        //Console.WriteLine(responseSendBill.Data.MensajeRespuesta);
                        var data = responseSendBill.Data;
                        mensaje = data.Exito ? data.MensajeRespuesta : data.MensajeError;

                        var ventaSunatInfo = new VentaSunatInfo();
                        ventaSunatInfo.VentaId = ventaId;
                        ventaSunatInfo.CodigoRespuesta = (data.CodigoRespuesta == null) ? String.Empty : data.CodigoRespuesta;
                        ventaSunatInfo.Exito = data.Exito ? 1 : 0;
                        ventaSunatInfo.MensajeError = (data.MensajeError == null) ? String.Empty : data.MensajeError;
                        ventaSunatInfo.MensajeRespuesta = (data.MensajeRespuesta == null) ? String.Empty : data.MensajeRespuesta;
                        ventaSunatInfo.NombreArchivo = (data.NombreArchivo == null) ? String.Empty : data.NombreArchivo;
                        ventaSunatInfo.Pila = (data.Pila == null) ? String.Empty : data.Pila;
                        ventaSunatInfo.TramaZipCdr = (data.TramaZipCdr == null) ? String.Empty : data.TramaZipCdr;
                        ventaSunatInfo.UsuarioCreacionId = usuarioInfo.UsuarioId;
                        new VentaSunat().Insertar(ventaSunatInfo);
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                // Console.WriteLine(e);
                System.Diagnostics.EventLog.WriteEntry("MyEventSource", ex.StackTrace, System.Diagnostics.EventLogEntryType.Warning);
            }
        }
    }
}