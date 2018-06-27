using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using APU.Entidad;
using APU.Herramientas;
using APU.Negocio;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using OpenInvoicePeru.Comun.Dto.Intercambio;
using OpenInvoicePeru.Comun.Dto.Modelos;
using RestSharp;
using ZXing;

namespace APU.Gasolutions
{
    public partial class ConsultaComprobante : PaginaBase
    {
        public static DocumentoElectronico _documento;
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
        }
        private static readonly string BaseUrl = ConfigurationManager.AppSettings["APU.Sunat.BaseUrl"];
        //private static readonly string UrlSunat = "https://e-beta.sunat.gob.pe/ol-ti-itcpfegem-beta/billService";
        private static readonly string UrlSunat = ConfigurationManager.AppSettings["APU.Sunat.UrlSunat"];
        private static readonly string FormatoFecha = ConfigurationManager.AppSettings["APU.Sunat.FormatoFecha"];
        [WebMethod]
        public static string ImprimirComprobanteAntes(string numeroDocumento, string tipoDocumentoId, string serie, string correlativo, string fechaEmision, decimal montoTotal)
        {
            var mensaje = String.Empty;
            try
            {
                var fecha = DateTime.ParseExact(fechaEmision, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                // var ventaInfoLista = new Negocio.VentaPetroamerica().ListarPaginado(0, numeroDocumento.Trim(), tipoDocumentoId, serie, correlativo, fecha, fecha, 0, 0, String.Empty, 0, 0);
                var ventaInfoLista = new Negocio.VentaGasolutions().ListarPaginado(0, String.Empty, tipoDocumentoId, serie + "-" + correlativo, fecha, fecha, 0, 0, 0, 0);
                var ventaGasolutionsDetalleInfo = new VentaDetalleGasolutionsInfo();

                ventaInfoLista = ventaInfoLista.Where(v => v.TotalVenta.Equals(montoTotal)).ToList();
                var ventaInfo = new VentaGasolutionsInfo();
                if (ventaInfoLista.Count > 0)
                {
                    ventaInfo = ventaInfoLista.FirstOrDefault();
                    ventaGasolutionsDetalleInfo = new Negocio.VentaGasolutions().ListarDetalle(ventaInfo.VentaGasolutionsId, 0).FirstOrDefault();
                }
                else
                {
                    mensaje = "No se ha encontrado el comprobante." + "@" + "" + "@" + ventaInfo.NroDocumentoReceptor;
                    return mensaje;
                }

                var ventaDetalleInfo = new List<VentaDetalleInfo>();

                _documento = new DocumentoElectronico();

                #region Documento
                _documento.CalculoDetraccion = 0;
                _documento.CalculoIgv = Decimal.Divide(18, 100);
                _documento.CalculoIsc = 0;
                _documento.DescuentoGlobal = 0;

                #region Emisor
                var emisor = new DocumentoElectronico().Emisor;
                emisor.Departamento = ventaInfo.DepartamentoEmisor;
                emisor.Direccion = ventaInfo.DireccionEmisor;
                emisor.Distrito = ventaInfo.DistritoEmisor;
                emisor.NombreComercial = ventaInfo.NombreComercialEmisor;
                emisor.NombreLegal = ventaInfo.NombreLegalEmisor;
                emisor.NroDocumento = ventaInfo.NroDocumentoEmisor;
                emisor.Provincia = ventaInfo.ProvinciaEmisor;
                emisor.TipoDocumento = "6";
                emisor.Ubigeo = ventaInfo.UbigeoEmisor;
                emisor.Urbanizacion = "";
                _documento.Emisor = emisor;
                #endregion

                _documento.Exoneradas = 0;
                _documento.FechaEmision = ventaInfo.FechaEmision.ToString("dd/MM/yyyy");
                _documento.Gratuitas = 0;
                _documento.Gravadas = ventaInfo.Gravadas;
                _documento.IdDocumento = ventaInfo.IdDocumento;
                _documento.Inafectas = 0;

                #region Items
                var itemId = 1;
                //foreach (var vd in ventaDetalleInfo)
                //{
                var item = new DetalleDocumento();
                //item.Cantidad = vd.Cantidad;
                item.Cantidad = ventaGasolutionsDetalleInfo.Cantidad;

                //item.Descripcion = vd.Producto;
                item.Descripcion = ventaGasolutionsDetalleInfo.Descripcion;
                //item.Id = itemId;
                item.Id = ventaGasolutionsDetalleInfo.Id;
                itemId++;
                //item.Impuesto = vd.Igv;
                item.Impuesto = ventaGasolutionsDetalleInfo.Impuesto;
                //item.PrecioUnitario = vd.PrecioUnitario;
                item.PrecioUnitario = ventaGasolutionsDetalleInfo.PrecioUnitario;
                //item.Suma = vd.SubTotal;
                item.Suma = ventaGasolutionsDetalleInfo.TotalVenta;
                //item.TotalVenta = vd.MontoTotal;
                item.TotalVenta = ventaGasolutionsDetalleInfo.TotalVenta;
                //item.UnidadMedida = vd.UnidadMedida;
                item.UnidadMedida = ventaGasolutionsDetalleInfo.UnidadMedida;
                item.Descuento = 0;
                item.ImpuestoSelectivo = 0;
                item.OtroImpuesto = 0;
                item.PrecioReferencial = 0;
                item.TipoImpuesto = "10";
                item.TipoPrecio = "01";
                _documento.Items.Add(item);
                //}
                #endregion

                _documento.Moneda = ventaInfo.MonedaId.Equals(Constantes.MonedaSoles) ? Constantes.MonedaSolesSunat : Constantes.MonedaDolaresSunat;
                _documento.MontoAnticipo = 0;
                _documento.MontoDetraccion = 0;
                _documento.MontoEnLetras = Herramientas.NumeroALetras.numeroAletras(ventaInfo.TotalVenta);
                _documento.MontoPercepcion = 0;
                _documento.PlacaVehiculo = ventaInfo.PlacaVehiculo;

                #region Receptor
                var receptor = new DocumentoElectronico().Receptor;
                receptor.Departamento = "";
                receptor.Direccion = ventaInfo.DireccionReceptor;
                receptor.Distrito = "";
                receptor.NombreComercial = ventaInfo.NombreComercialReceptor;
                receptor.NombreLegal = ventaInfo.NombreLegalReceptor;
                receptor.NroDocumento = ventaInfo.NroDocumentoReceptor;
                receptor.Provincia = "";
                receptor.TipoDocumento = ventaInfo.TipoDocumentoReceptor;
                receptor.Ubigeo = "";
                receptor.Urbanizacion = "";
                _documento.Receptor = receptor;
                #endregion

                _documento.TipoDocumento = ventaInfo.TipoDocumento;
                _documento.TotalIgv = ventaInfo.TotalIgv;
                _documento.TotalIsc = 0;
                _documento.TotalOtrosTributos = 0;
                _documento.TotalVenta = ventaInfo.TotalVenta;
                #endregion

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

                var rutaPdfFactura = HostingEnvironment.MapPath("~/Archivos/Documentos/Cliente/" + ventaInfo.NroDocumentoReceptor + "");

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

                // string rutaXml = HostingEnvironment.MapPath("~/Archivos/Facturacion/XML/" + _documento.IdDocumento + ".xml");
                string rutaXml = HostingEnvironment.MapPath("~/Archivos/Facturacion/XML/Cliente");
                if (!Directory.Exists(rutaXml))
                {
                    Directory.CreateDirectory(rutaXml);
                }
                File.WriteAllBytes(rutaXml + "/" + _documento.IdDocumento + ".xml", Convert.FromBase64String(documentoResponse.Data.TramaXmlSinFirma));
                #endregion

                #region Firma
                string rutaCertificado = HostingEnvironment.MapPath("~/Archivos/Facturacion/certificado.pfx");
                var firmado = new FirmadoRequest
                {
                    TramaXmlSinFirma = documentoResponse.Data.TramaXmlSinFirma,
                    CertificadoDigital = Convert.ToBase64String(File.ReadAllBytes(rutaCertificado)),
                    PasswordCertificado = "9dGxdmm5JHKwKsXc",
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
                //string rutaXmlFirmado = HostingEnvironment.MapPath("~/Archivos/Facturacion/XML/Cliente/" + _documento.IdDocumento + "_Firmado.xml");
                string rutaXmlFirmado = HostingEnvironment.MapPath("~/Archivos/Facturacion/XML/Cliente");
                if (!Directory.Exists(rutaXmlFirmado))
                {
                    Directory.CreateDirectory(rutaXmlFirmado);
                }
                File.WriteAllBytes(rutaXmlFirmado + "/" + _documento.IdDocumento + "_Firmado.xml", Convert.FromBase64String(responseFirma.Data.TramaXmlFirmado));
                #endregion

                #region Impresión Comprobante
                var htmlToComprobante = new NReco.PdfGenerator.HtmlToPdfConverter();
                var nombreArchivoComprobante = String.Empty;
                var comprobanteSB = new StringBuilder();

                if (ventaInfo.TipoDocumento.Equals(Constantes.TipoComprobanteFactura))
                {
                    comprobanteSB.Append(Herramientas.Helper.ObtenerTexto("~/Archivos/Plantillas/FacturaJulcan.html"));
                    #region Factura
                    //comprobanteSB.Replace("@EMPRESA_LOGO", HttpContext.Current.Server.MapPath("~/Imagenes/petroamerica_logo.png"));
                    comprobanteSB.Replace("@EMPRESA_LOGO", String.Empty);
                    comprobanteSB.Replace("@EMPRESA_NOMBRE", ventaInfo.NombreComercialEmisor);
                    comprobanteSB.Replace("@EMPRESA_DIRECCION", ventaInfo.DireccionEmisor);
                    comprobanteSB.Replace("@GRIFO_DIRECCION", String.Empty);
                    comprobanteSB.Replace("@EMPRESA_RUC", ventaInfo.NroDocumentoEmisor);
                    comprobanteSB.Replace("@IMPRESORA", String.Empty);
                    comprobanteSB.Replace("@NUMEROCOMPROBANTE", ventaInfo.IdDocumento);
                    comprobanteSB.Replace("@FECHA", fechaEmision);
                    comprobanteSB.Replace("@FECHA", fechaEmision);

                    comprobanteSB.Replace("@CLIENTE_NOMBRE", ventaInfo.NombreComercialReceptor);
                    comprobanteSB.Replace("@CLIENTE_RUC", ventaInfo.NroDocumentoReceptor);
                    comprobanteSB.Replace("@CLIENTE_PLACA", ventaInfo.PlacaVehiculo);
                    comprobanteSB.Replace("@CLIENTE_DIRECCION", ventaInfo.DireccionReceptor);
                    comprobanteSB.Replace("@CANTIDAD", ventaGasolutionsDetalleInfo.Cantidad.ToString("N2"));
                    comprobanteSB.Replace("@PRECIO", ventaGasolutionsDetalleInfo.PrecioUnitario.ToString("N2"));
                    comprobanteSB.Replace("@MONTO_VENTA", ventaInfo.Gravadas.ToString("N2"));
                    comprobanteSB.Replace("@PRODUCTO", ventaGasolutionsDetalleInfo.Descripcion);
                    comprobanteSB.Replace("@MONEDA_SIMBOLO", ventaInfo.SimboloMoneda);
                    comprobanteSB.Replace("@MONTO_IMPUESTO", ventaInfo.TotalIgv.ToString("N2"));
                    comprobanteSB.Replace("@MONTO_TOTAL", ventaInfo.TotalVenta.ToString("N2"));
                    comprobanteSB.Replace("@VENDEDOR", String.Empty);
                    comprobanteSB.Replace("@CODIGO_HASH", responseFirma.Data.ResumenFirma);

                    comprobanteSB.Replace("@TEXTO_MONTO_TOTAL", NumeroALetras.numeroAletras(ventaInfo.TotalVenta));
                    #endregion

                    nombreArchivoComprobante = "Factura_" + DateTime.Now.ToString("ddMMyyyyhhmmss"); // + ".pdf";
                }
                if (ventaInfo.TipoDocumento.Equals(Constantes.TipoComprobanteBoletaVenta))
                {
                    comprobanteSB.Append(Herramientas.Helper.ObtenerTexto("~/Archivos/Plantillas/BoletaJulcan.html"));
                    #region Boleta
                    //comprobanteSB.Replace("@EMPRESA_LOGO", HttpContext.Current.Server.MapPath("~/Imagenes/petroamerica_logo.png"));
                    comprobanteSB.Replace("@EMPRESA_LOGO", String.Empty);
                    comprobanteSB.Replace("@EMPRESA_NOMBRE", ventaInfo.NombreComercialEmisor);
                    comprobanteSB.Replace("@EMPRESA_DIRECCION", ventaInfo.DireccionEmisor);
                    comprobanteSB.Replace("@GRIFO_DIRECCION", String.Empty);
                    comprobanteSB.Replace("@EMPRESA_RUC", ventaInfo.NroDocumentoEmisor);
                    comprobanteSB.Replace("@IMPRESORA", String.Empty);
                    comprobanteSB.Replace("@NUMEROCOMPROBANTE", ventaInfo.IdDocumento);
                    comprobanteSB.Replace("@FECHA", fechaEmision);
                    comprobanteSB.Replace("@CLIENTE_NOMBRE", ventaInfo.NombreComercialReceptor);
                    comprobanteSB.Replace("@CLIENTE_DIRECCION", ventaInfo.DireccionReceptor);
                    comprobanteSB.Replace("@CANTIDAD", ventaGasolutionsDetalleInfo.Cantidad.ToString("N2"));
                    comprobanteSB.Replace("@PRECIO", ventaGasolutionsDetalleInfo.PrecioUnitario.ToString("N2"));
                    comprobanteSB.Replace("@MONTO_VENTA", ventaInfo.Gravadas.ToString("N2"));
                    comprobanteSB.Replace("@PRODUCTO", ventaGasolutionsDetalleInfo.Descripcion);
                    comprobanteSB.Replace("@MONEDA_SIMBOLO", ventaInfo.SimboloMoneda);
                    comprobanteSB.Replace("@MONTO_IMPUESTO", ventaInfo.TotalIgv.ToString("N2"));
                    comprobanteSB.Replace("@MONTO_TOTAL", ventaInfo.TotalVenta.ToString("N2"));
                    comprobanteSB.Replace("@VENDEDOR", String.Empty);
                    comprobanteSB.Replace("@CODIGO_HASH", responseFirma.Data.ResumenFirma);

                    comprobanteSB.Replace("@TEXTO_MONTO_TOTAL", NumeroALetras.numeroAletras(ventaInfo.TotalVenta));
                    #endregion

                    nombreArchivoComprobante = "Boleta_" + DateTime.Now.ToString("ddMMyyyyhhmmss"); // + ".pdf";
                }

                if (!Directory.Exists(rutaPdfFactura))
                {
                    Directory.CreateDirectory(rutaPdfFactura);
                }

                #region Codigo QR
                var codigoQR = ventaInfo.NroDocumentoReceptor + "|" + ventaInfo.TipoDocumento + "|" + ventaInfo.IdDocumento.Substring(0, 4) + "|" +
                               ventaInfo.IdDocumento.Substring(5, 8) + "|" + ventaInfo.TotalIgv + "|" + ventaInfo.TotalVenta + "|" +
                               ventaInfo.FechaEmision + "|" + ventaInfo.TipoDocumentoReceptor + "|" + ventaInfo.NroDocumentoReceptor + "|" +
                               responseFirma.Data.ResumenFirma + "|" + responseFirma.Data.ValorFirma;

                // instantiate a writer object
                var barcodeWriter = new ZXing.BarcodeWriter();
                // set the barcode format
                barcodeWriter.Format = BarcodeFormat.PDF_417;
                // write text and generate a 2-D barcode as a bitmap
                // barcodeWriter.Write(codigoQR).Save(@"C:\Users\jeremy\Desktop\generated.bmp");
                //barcodeWriter.Write(codigoQR).Save(HostingEnvironment.MapPath("~/Archivos/Documentos/Cliente/" + ventaInfo.NumeroDocumentoCliente + "/" + _documento.IdDocumento + ".bmp"));
                barcodeWriter.Write(codigoQR).Save(rutaPdfFactura + "/" + _documento.IdDocumento + ".bmp");
                #endregion

                comprobanteSB.Replace("@CODIGO_QR", HttpContext.Current.Server.MapPath("~/Archivos/Documentos/Cliente/" + ventaInfo.NroDocumentoReceptor + "/" + _documento.IdDocumento + ".bmp"));


                comprobanteSB = Herramientas.Helper.EncodeHtml(comprobanteSB);
                //var margins = new PageMargins();
                //margins.Left = 1;
                //htmlToComprobante.Margins = margins;

                var pdfBytesFactura = htmlToComprobante.GeneratePdf(comprobanteSB.ToString());


                // var comprobanteImpreso = rutaPdfFactura + "\\" + nombreArchivoComprobante + ".pdf";
                File.WriteAllBytes(rutaPdfFactura + "/" + nombreArchivoComprobante + ".pdf", pdfBytesFactura);
                File.WriteAllBytes(rutaPdfFactura + "/" + nombreArchivoComprobante + ".xml", Convert.FromBase64String(responseFirma.Data.TramaXmlFirmado));
                #endregion

                mensaje = "El Comprobante se generó correctamente" + "@" + nombreArchivoComprobante + "@" + ventaInfo.NroDocumentoReceptor;
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaPresentacion);
                mensaje = rethrow ? ex.Message : Constantes.ExcepcionPoliticaPresentacion;
                // var sex = new SmartException(ex, usuarioInfo.Matricula, codigoOperacion.ToString());
                //RegistrarScript("MostrarMensaje('" + mensaje.Replace("'", "") + "');", "ErrorConsultaComprobante");
                mensaje = (mensaje + "-" + ex.Source) + "@" + "" + "@" + numeroDocumento;
            }
            return mensaje;
        }

        private static readonly string PlantillaBoleta = ConfigurationManager.AppSettings["APU.Archivos.Plantillas.Boleta"];
        private static readonly string PlantillaFactura = ConfigurationManager.AppSettings["APU.Archivos.Plantillas.Factura"];
        [WebMethod]
        //public static string ImprimirComprobante(int ventaPetroamericaId, int ventaId)
        public static string ImprimirComprobante(string numeroDocumento, string tipoDocumentoId, string serie, string correlativo, string fechaEmision, decimal montoTotalComprobante)
        {
            var mensaje = String.Empty;
            try
            {
                var empresaInfo = new Empresa().Listar(0).FirstOrDefault();
                var agenciaInfo = new Agencia().Listar(0).FirstOrDefault();
                //var ventaInfo = new Negocio.VentaGasolutions().Listar(ventaPetroamericaId).FirstOrDefault();


                var ventaInfoLista = new Negocio.VentaGasolutions().ListarPaginado(0, numeroDocumento, tipoDocumentoId, serie + "-" + correlativo, DateTime.ParseExact(fechaEmision, "dd/MM/yyyy", null), DateTime.ParseExact(fechaEmision, "dd/MM/yyyy", null), 0, 0, 0, 0);
                var ventaGasolutionsDetalleInfoLista = new List<VentaDetalleGasolutionsInfo>();

                //var ventaGasolutionsDetalleInfo = new Negocio.VentaGasolutions().ListarDetalle(ventaPetroamericaId, 0).ToList();
                //var ventaGasolutionsDetalleInfo = new Negocio.VentaGasolutions().ListarDetalle(ventaInfo.VentaGasolutionsId, 0).ToList();

                ventaInfoLista = ventaInfoLista.Where(v => v.TotalVenta.Equals(montoTotalComprobante)).ToList();
                var ventaInfo = new VentaGasolutionsInfo();
                if (ventaInfoLista.Count > 0)
                {
                    ventaInfo = ventaInfoLista.FirstOrDefault();
                    ventaGasolutionsDetalleInfoLista = new Negocio.VentaGasolutions().ListarDetalle(ventaInfo.VentaGasolutionsId, 0);
                }
                else
                {
                    mensaje = "No se ha encontrado el comprobante." + "@" + "" + "@" + ventaInfo.NroDocumentoReceptor;
                    return mensaje;
                }

                var ventaDetalleInfo = new List<VentaDetalleInfo>();

                _documento = new DocumentoElectronico();

                #region Documento
                _documento.CalculoDetraccion = 0;
                _documento.CalculoIgv = Decimal.Divide(18, 100);
                _documento.CalculoIsc = 0;
                _documento.DescuentoGlobal = 0;

                #region Emisor
                var emisor = new DocumentoElectronico().Emisor;
                emisor.Departamento = ventaInfo.DepartamentoEmisor;
                emisor.Direccion = ventaInfo.DireccionEmisor;
                emisor.Distrito = ventaInfo.DistritoEmisor;
                emisor.NombreComercial = ventaInfo.NombreComercialEmisor;
                emisor.NombreLegal = ventaInfo.NombreLegalEmisor;
                emisor.NroDocumento = ventaInfo.NroDocumentoEmisor;
                emisor.Provincia = ventaInfo.ProvinciaEmisor;
                emisor.TipoDocumento = "6";
                emisor.Ubigeo = ventaInfo.UbigeoEmisor;
                emisor.Urbanizacion = "";
                _documento.Emisor = emisor;
                #endregion

                _documento.Exoneradas = 0;
                _documento.FechaEmision = ventaInfo.FechaEmision.ToString("dd/MM/yyyy");
                _documento.Gratuitas = 0;
                _documento.Gravadas = ventaInfo.Gravadas;
                _documento.IdDocumento = ventaInfo.IdDocumento;
                _documento.Inafectas = 0;

                #region Items
                var itemId = 1;
                foreach (var vd in ventaGasolutionsDetalleInfoLista)
                {
                    var item = new DetalleDocumento();
                    item.Cantidad = vd.Cantidad;
                    item.Descripcion = vd.Descripcion;
                    item.Id = vd.Id;
                    itemId++;
                    item.Impuesto = vd.Impuesto;
                    item.PrecioUnitario = vd.PrecioUnitario;
                    item.Suma = vd.TotalVenta;
                    item.TotalVenta = vd.TotalVenta;
                    item.UnidadMedida = vd.UnidadMedida;
                    item.Descuento = 0;
                    item.ImpuestoSelectivo = 0;
                    item.OtroImpuesto = 0;
                    item.PrecioReferencial = 0;
                    item.TipoImpuesto = "10";
                    item.TipoPrecio = "01";
                    _documento.Items.Add(item);
                }
                #endregion

                _documento.Moneda = ventaInfo.MonedaId.Equals(Constantes.MonedaSoles) ? Constantes.MonedaSolesSunat : Constantes.MonedaDolaresSunat;
                _documento.MontoAnticipo = 0;
                _documento.MontoDetraccion = 0;
                _documento.MontoEnLetras = Herramientas.NumeroALetras.numeroAletras(ventaInfo.TotalVenta);
                _documento.MontoPercepcion = 0;
                _documento.PlacaVehiculo = ventaInfo.PlacaVehiculo;

                #region Receptor
                var receptor = new DocumentoElectronico().Receptor;
                receptor.Departamento = "";
                receptor.Direccion = ventaInfo.DireccionReceptor;
                receptor.Distrito = "";
                receptor.NombreComercial = ventaInfo.NombreComercialReceptor;
                receptor.NombreLegal = ventaInfo.NombreLegalReceptor;
                receptor.NroDocumento = ventaInfo.NroDocumentoReceptor;
                receptor.Provincia = "";
                receptor.TipoDocumento = ventaInfo.TipoDocumentoReceptor;
                receptor.Ubigeo = "";
                receptor.Urbanizacion = "";
                _documento.Receptor = receptor;
                #endregion

                _documento.TipoDocumento = ventaInfo.TipoDocumento;
                _documento.TotalIgv = ventaInfo.TotalIgv;
                _documento.TotalIsc = 0;
                _documento.TotalOtrosTributos = 0;
                _documento.TotalVenta = ventaInfo.TotalVenta;
                #endregion

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

                var rutaPdfFactura = HostingEnvironment.MapPath("~/Archivos/Documentos/Cliente/" + ventaInfo.NroDocumentoReceptor + "");

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

                // string rutaXml = HostingEnvironment.MapPath("~/Archivos/Facturacion/XML/" + _documento.IdDocumento + ".xml");
                string rutaXml = HostingEnvironment.MapPath("~/Archivos/Documentos/Cliente") + "/" + ventaInfo.NroDocumentoReceptor;
                if (!Directory.Exists(rutaXml))
                {
                    Directory.CreateDirectory(rutaXml);
                }
                File.WriteAllBytes(rutaXml + "/" + _documento.IdDocumento + ".xml", Convert.FromBase64String(documentoResponse.Data.TramaXmlSinFirma));
                #endregion

                #region Firma
                string rutaCertificado = HostingEnvironment.MapPath("~/Archivos/Facturacion/certificado.pfx");
                var firmado = new FirmadoRequest
                {
                    TramaXmlSinFirma = documentoResponse.Data.TramaXmlSinFirma,
                    CertificadoDigital = Convert.ToBase64String(File.ReadAllBytes(rutaCertificado)),
                    PasswordCertificado = "9dGxdmm5JHKwKsXc",
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
                //string rutaXmlFirmado = HostingEnvironment.MapPath("~/Archivos/Facturacion/XML/Cliente/" + _documento.IdDocumento + "_Firmado.xml");
                string rutaXmlFirmado = HostingEnvironment.MapPath("~/Archivos/Documentos/Cliente") + "/" + ventaInfo.NroDocumentoReceptor;
                if (!Directory.Exists(rutaXmlFirmado))
                {
                    Directory.CreateDirectory(rutaXmlFirmado);
                }
                File.WriteAllBytes(rutaXmlFirmado + "/" + _documento.IdDocumento + "_Firmado.xml", Convert.FromBase64String(responseFirma.Data.TramaXmlFirmado));
                #endregion

                #region Impresión Comprobante
                var htmlToComprobante = new NReco.PdfGenerator.HtmlToPdfConverter();
                var nombreArchivoComprobante = String.Empty;
                var comprobanteSB = new StringBuilder();

                if (ventaInfo.TipoDocumento.Equals(Constantes.TipoComprobanteFactura))
                {
                    //comprobanteSB.Append(Herramientas.Helper.ObtenerTexto("~/Archivos/Plantillas/FacturaPowerGas.html"));
                    comprobanteSB.Append(Herramientas.Helper.ObtenerTexto(PlantillaFactura));
                    #region Factura

                    comprobanteSB.Replace("@EMPRESA_LOGO", HttpContext.Current.Server.MapPath(empresaInfo.Imagen));
                    comprobanteSB.Replace("@EMPRESA_NOMBRE", ventaInfo.NombreComercialEmisor);
                    comprobanteSB.Replace("@EMPRESA_RUC", ventaInfo.NroDocumentoEmisor);
                    comprobanteSB.Replace("@NUMEROCOMPROBANTE", ventaInfo.IdDocumento);
                    comprobanteSB.Replace("@EMPRESA_DIRECCION", ventaInfo.DireccionEmisor);
                    comprobanteSB.Replace("@EMPRESA_TELEFONO", String.Empty);
                    comprobanteSB.Replace("@EMPRESA_FAX", String.Empty);
                    comprobanteSB.Replace("@EMPRESA_PAIS", empresaInfo.Pais);
                    comprobanteSB.Replace("@EMPRESA_DEPARTAMENTO", empresaInfo.Departamento);
                    comprobanteSB.Replace("@EMPRESA_PROVINCIA", empresaInfo.Provincia);
                    comprobanteSB.Replace("@EMPRESA_DISTRITO", empresaInfo.Distrito);
                    comprobanteSB.Replace("@FECHAEMISION", ventaInfo.FechaEmision.ToString("dd/MM/yyyy"));
                    comprobanteSB.Replace("@AGENCIA_DIRECCION", agenciaInfo.Direccion);


                    //var clienteInfo = new Cliente().Listar(ventaInfo.ClienteId).FirstOrDefault();
                    comprobanteSB.Replace("@CLIENTE_NOMBRE", ventaInfo.NombreComercialReceptor);
                    comprobanteSB.Replace("@CLIENTE_DIRECCION", ventaInfo.DireccionReceptor);
                    comprobanteSB.Replace("@CLIENTE_DISTRITO", String.Empty);
                    comprobanteSB.Replace("@CLIENTE_RUC", ventaInfo.NroDocumentoReceptor);
                    comprobanteSB.Replace("@CLIENTE_CODIGO", String.Empty);

                    comprobanteSB.Replace("@MONEDA", ventaInfo.Moneda);

                    var facturaSBDetalle = new StringBuilder("");
                    facturaSBDetalle.Append("<table style=\"width: 100%; border-collapse: collapse;\">");
                    facturaSBDetalle.Append("   <thead>");
                    facturaSBDetalle.Append("       <tr>");
                    facturaSBDetalle.Append("           <td style=\"width: 10%; border-bottom: 1px solid #0b44e9; text-align: center;\" class=\"Estilo67\">ITEM</td>");
                    facturaSBDetalle.Append("           <td style=\"width: 50%; border-bottom: 1px solid #0b44e9; text-align: center;\" class=\"Estilo67\">DESCRIPCI&Oacute;N</td>");
                    facturaSBDetalle.Append("           <td style=\"width: 10%; border-bottom: 1px solid #0b44e9; text-align: center;\" class=\"Estilo67\">CANT</td>");
                    facturaSBDetalle.Append("           <td style=\"width: 10%; border-bottom: 1px solid #0b44e9; text-align: center;\" class=\"Estilo67\">UND</td>");
                    facturaSBDetalle.Append("           <td style=\"width: 10%; border-bottom: 1px solid #0b44e9; text-align: center;\" class=\"Estilo67\">VALOR UNIT</td>");
                    facturaSBDetalle.Append("           <td style=\"width: 10%; border-bottom: 1px solid #0b44e9; text-align: center;\" class=\"Estilo67\">SUBTOTAL</td>");
                    facturaSBDetalle.Append("       </tr>");
                    facturaSBDetalle.Append("   </thead>");

                    var montoGravado = 0M;
                    var montoImpuestoTotal = 0M;
                    foreach (var vd in ventaGasolutionsDetalleInfoLista)
                    {
                        facturaSBDetalle.Append("   <tr>");
                        facturaSBDetalle.Append("       <td style=\"width: 10%; border: 1px none #0b44e9; text-align: center; vertical-align: top;\" class=\"Estilo8\">" + vd.CodigoItem + "</td>");
                        facturaSBDetalle.Append("       <td style=\"width: 50%; border: 1px none #0b44e9; text-align: left; vertical-align: top;\" class=\"Estilo8\">" + vd.Descripcion + "</td>");
                        facturaSBDetalle.Append("       <td style=\"width: 10%; border: 1px none #0b44e9; text-align: center; vertical-align: top;\" class=\"Estilo8\">" + ((vd.TotalVenta - vd.Impuesto) / vd.PrecioUnitario).ToString("N2") + "</td>");
                        facturaSBDetalle.Append("       <td style=\"width: 10%; border: 1px none #0b44e9; text-align: center; vertical-align: top;\" class=\"Estilo8\">" + vd.UnidadMedida + "</td>");
                        facturaSBDetalle.Append("       <td style=\"width: 10%; border: 1px none #0b44e9; text-align: center; vertical-align: top;\" class=\"Estilo8\">" + vd.PrecioUnitario.ToString("N2") + "</td>");
                        //facturaSBDetalle.Append("       <td style=\"width: 10%; border: 1px none #0b44e9; text-align: center; vertical-align: top;\" class=\"Estilo8\">" + (vd.Cantidad * vd.PrecioUnitario).ToString("N2") + "</td>");
                        facturaSBDetalle.Append("       <td style=\"width: 10%; border: 1px none #0b44e9; text-align: center; vertical-align: top;\" class=\"Estilo8\">" + (vd.TotalVenta - vd.Impuesto).ToString("N2") + "</td>");
                        facturaSBDetalle.Append("   </tr>");

                        montoGravado = montoGravado + vd.TotalVenta;
                    }
                    facturaSBDetalle.Append("</table>");

                    var montoIgv = montoGravado * 0.18M;

                    var montoTotal = montoGravado + montoIgv;

                    comprobanteSB.Replace("@MONTO_GRAVADA", ventaInfo.Gravadas.ToString("N2"));
                    comprobanteSB.Replace("@MONTO_INAFECTA", "0.00");
                    comprobanteSB.Replace("@MONTO_EXONERADA", "0.00");
                    comprobanteSB.Replace("@MONTO_GRATUITA", "0.00");
                    comprobanteSB.Replace("@MONTO_IGV", ventaInfo.TotalIgv.ToString("N2"));
                    comprobanteSB.Replace("@MONTO_TOTAL", ventaInfo.TotalVenta.ToString("N2"));

                    comprobanteSB.Replace("@PRODUCTO_DETALLE", facturaSBDetalle.ToString());
                    #endregion

                    nombreArchivoComprobante = "Factura_" + DateTime.Now.ToString("ddMMyyyyhhmmss"); // + ".pdf";
                }
                if (ventaInfo.TipoDocumento.Equals(Constantes.TipoComprobanteBoletaVenta))
                {
                    //comprobanteSB.Append(Herramientas.Helper.ObtenerTexto("~/Archivos/Plantillas/BoletaPowerGas.html"));
                    comprobanteSB.Append(Herramientas.Helper.ObtenerTexto(PlantillaBoleta));
                    #region Boleta
                    comprobanteSB.Replace("@EMPRESA_LOGO", HttpContext.Current.Server.MapPath(empresaInfo.Imagen));
                    comprobanteSB.Replace("@EMPRESA_NOMBRE", ventaInfo.NombreComercialEmisor);
                    comprobanteSB.Replace("@EMPRESA_RUC", ventaInfo.NroDocumentoEmisor);
                    comprobanteSB.Replace("@NUMEROCOMPROBANTE", ventaInfo.IdDocumento);
                    comprobanteSB.Replace("@EMPRESA_DIRECCION", ventaInfo.DireccionEmisor);
                    comprobanteSB.Replace("@EMPRESA_TELEFONO", String.Empty);
                    comprobanteSB.Replace("@EMPRESA_FAX", String.Empty);
                    comprobanteSB.Replace("@EMPRESA_PAIS", empresaInfo.Pais);
                    comprobanteSB.Replace("@EMPRESA_DEPARTAMENTO", empresaInfo.Departamento);
                    comprobanteSB.Replace("@EMPRESA_PROVINCIA", empresaInfo.Provincia);
                    comprobanteSB.Replace("@EMPRESA_DISTRITO", empresaInfo.Distrito);
                    comprobanteSB.Replace("@FECHAEMISION", ventaInfo.FechaEmision.ToString("dd/MM/yyyy"));
                    comprobanteSB.Replace("@AGENCIA_DIRECCION", agenciaInfo.Direccion);


                    //var clienteInfo = new Cliente().Listar(ventaInfo.ClienteId).FirstOrDefault();
                    comprobanteSB.Replace("@CLIENTE_NOMBRE", ventaInfo.NombreComercialReceptor);
                    comprobanteSB.Replace("@CLIENTE_DIRECCION", ventaInfo.DireccionReceptor);
                    comprobanteSB.Replace("@CLIENTE_DISTRITO", String.Empty);
                    comprobanteSB.Replace("@CLIENTE_RUC", ventaInfo.NroDocumentoReceptor);
                    comprobanteSB.Replace("@CLIENTE_CODIGO", String.Empty);

                    comprobanteSB.Replace("@MONEDA", ventaInfo.Moneda);

                    var facturaSBDetalle = new StringBuilder("");
                    facturaSBDetalle.Append("<table style=\"width: 100%; border-collapse: collapse;\">");
                    facturaSBDetalle.Append("   <thead>");
                    facturaSBDetalle.Append("       <tr>");
                    facturaSBDetalle.Append("           <td style=\"width: 10%; border-bottom: 1px solid #0b44e9; text-align: center;\" class=\"Estilo67\">ITEM</td>");
                    facturaSBDetalle.Append("           <td style=\"width: 50%; border-bottom: 1px solid #0b44e9; text-align: center;\" class=\"Estilo67\">DESCRIPCI&Oacute;N</td>");
                    facturaSBDetalle.Append("           <td style=\"width: 10%; border-bottom: 1px solid #0b44e9; text-align: center;\" class=\"Estilo67\">CANT</td>");
                    facturaSBDetalle.Append("           <td style=\"width: 10%; border-bottom: 1px solid #0b44e9; text-align: center;\" class=\"Estilo67\">UND</td>");
                    facturaSBDetalle.Append("           <td style=\"width: 10%; border-bottom: 1px solid #0b44e9; text-align: center;\" class=\"Estilo67\">VALOR UNIT</td>");
                    facturaSBDetalle.Append("           <td style=\"width: 10%; border-bottom: 1px solid #0b44e9; text-align: center;\" class=\"Estilo67\">SUBTOTAL</td>");
                    facturaSBDetalle.Append("       </tr>");
                    facturaSBDetalle.Append("   </thead>");

                    var montoGravado = 0M;
                    var montoImpuestoTotal = 0M;
                    foreach (var vd in ventaGasolutionsDetalleInfoLista)
                    {
                        facturaSBDetalle.Append("   <tr>");
                        facturaSBDetalle.Append("       <td style=\"width: 10%; border: 1px none #0b44e9; text-align: center; vertical-align: top;\" class=\"Estilo8\">" + vd.CodigoItem + "</td>");
                        facturaSBDetalle.Append("       <td style=\"width: 50%; border: 1px none #0b44e9; text-align: left; vertical-align: top;\" class=\"Estilo8\">" + vd.Descripcion + "</td>");
                        facturaSBDetalle.Append("       <td style=\"width: 10%; border: 1px none #0b44e9; text-align: center; vertical-align: top;\" class=\"Estilo8\">" + ((vd.TotalVenta - vd.Impuesto) / vd.PrecioUnitario).ToString("N2") + "</td>");
                        facturaSBDetalle.Append("       <td style=\"width: 10%; border: 1px none #0b44e9; text-align: center; vertical-align: top;\" class=\"Estilo8\">" + vd.UnidadMedida + "</td>");
                        facturaSBDetalle.Append("       <td style=\"width: 10%; border: 1px none #0b44e9; text-align: center; vertical-align: top;\" class=\"Estilo8\">" + vd.PrecioUnitario.ToString("N2") + "</td>");
                        facturaSBDetalle.Append("       <td style=\"width: 10%; border: 1px none #0b44e9; text-align: center; vertical-align: top;\" class=\"Estilo8\">" + (vd.TotalVenta - vd.Impuesto).ToString("N2") + "</td>");
                        facturaSBDetalle.Append("   </tr>");

                        montoGravado = montoGravado + vd.TotalVenta;
                    }
                    facturaSBDetalle.Append("</table>");

                    var montoIgv = montoGravado * 0.18M;

                    var montoTotal = montoGravado + montoIgv;

                    comprobanteSB.Replace("@MONTO_GRAVADA", ventaInfo.Gravadas.ToString("N2"));
                    comprobanteSB.Replace("@MONTO_INAFECTA", "0.00");
                    comprobanteSB.Replace("@MONTO_EXONERADA", "0.00");
                    comprobanteSB.Replace("@MONTO_GRATUITA", "0.00");
                    comprobanteSB.Replace("@MONTO_IGV", ventaInfo.TotalIgv.ToString("N2"));
                    comprobanteSB.Replace("@MONTO_TOTAL", ventaInfo.TotalVenta.ToString("N2"));

                    comprobanteSB.Replace("@PRODUCTO_DETALLE", facturaSBDetalle.ToString());
                    #endregion

                    nombreArchivoComprobante = "Boleta_" + DateTime.Now.ToString("ddMMyyyyhhmmss"); // + ".pdf";
                }

                if (!Directory.Exists(rutaPdfFactura))
                {
                    Directory.CreateDirectory(rutaPdfFactura);
                }

                #region Codigo QR
                var codigoQR = ventaInfo.NroDocumentoReceptor + "|" + ventaInfo.TipoDocumento + "|" + ventaInfo.IdDocumento.Substring(0, 4) + "|" +
                               ventaInfo.IdDocumento.Substring(5, ventaInfo.IdDocumento.Length - 5) + "|" + ventaInfo.TotalIgv + "|" + ventaInfo.TotalVenta + "|" +
                               ventaInfo.FechaEmision + "|" + ventaInfo.TipoDocumentoReceptor + "|" + ventaInfo.NroDocumentoReceptor + "|" +
                               responseFirma.Data.ResumenFirma + "|" + responseFirma.Data.ValorFirma;

                // instantiate a writer object
                var barcodeWriter = new BarcodeWriter();
                // set the barcode format
                barcodeWriter.Format = BarcodeFormat.PDF_417;
                // write text and generate a 2-D barcode as a bitmap
                // barcodeWriter.Write(codigoQR).Save(@"C:\Users\jeremy\Desktop\generated.bmp");
                //barcodeWriter.Write(codigoQR).Save(HostingEnvironment.MapPath("~/Archivos/Documentos/Cliente/" + ventaInfo.NumeroDocumentoCliente + "/" + _documento.IdDocumento + ".bmp"));
                barcodeWriter.Write(codigoQR).Save(rutaPdfFactura + "/" + _documento.IdDocumento + ".bmp");
                #endregion

                comprobanteSB.Replace("@CODIGO_QR", HttpContext.Current.Server.MapPath("~/Archivos/Documentos/Cliente/" + ventaInfo.NroDocumentoReceptor + "/" + _documento.IdDocumento + ".bmp"));
                comprobanteSB.Replace("@MONTO_LETRAS", Herramientas.NumeroALetras.numeroAletras(ventaInfo.TotalVenta));

                comprobanteSB = Herramientas.Helper.EncodeHtml(comprobanteSB);
                //var margins = new PageMargins();
                //margins.Left = 1;
                //htmlToComprobante.Margins = margins;

                var pdfBytesFactura = htmlToComprobante.GeneratePdf(comprobanteSB.ToString());


                // var comprobanteImpreso = rutaPdfFactura + "\\" + nombreArchivoComprobante + ".pdf";
                File.WriteAllBytes(rutaPdfFactura + "/" + nombreArchivoComprobante + ".pdf", pdfBytesFactura);
                File.WriteAllBytes(rutaPdfFactura + "/" + nombreArchivoComprobante + ".xml", Convert.FromBase64String(responseFirma.Data.TramaXmlFirmado));

                Negocio.Helper.ActualizarColumnasTabla("VentaGasolutions", new string[] { "ComprobanteImpreso" }, new string[] { nombreArchivoComprobante + ".pdf" }, new string[] { "VentaGasolutionsId" }, new string[] { ventaInfo.VentaGasolutionsId.ToString() });

                #endregion

                mensaje = "El Comprobante se generó correctamente" + "@" + (nombreArchivoComprobante) + "@" + ventaInfo.NroDocumentoReceptor;
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaPresentacion);
                mensaje = rethrow ? ex.Message : Constantes.ExcepcionPoliticaPresentacion;
                mensaje = (mensaje + "-" + ex.Source) + "@" + "" + "@" + "";
            }
            return mensaje;
        }
    }
}