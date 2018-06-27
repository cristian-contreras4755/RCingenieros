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
using NReco.PdfGenerator;
using OpenInvoicePeru.Comun.Dto.Intercambio;
using OpenInvoicePeru.Comun.Dto.Modelos;
using RestSharp;
using ZXing;



namespace APU.Petroamerica
{
    public partial class ConsultaComprobante : PaginaBase
    {
        #region Variables Privadas
        public static DocumentoElectronico _documento;
        #endregion
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
        }
        private static readonly string BaseUrl = ConfigurationManager.AppSettings["APU.Sunat.BaseUrl"];
        //private static readonly string UrlSunat = "https://e-beta.sunat.gob.pe/ol-ti-itcpfegem-beta/billService";
        private static readonly string UrlSunat = ConfigurationManager.AppSettings["APU.Sunat.UrlSunat"];
        private static readonly string FormatoFecha = ConfigurationManager.AppSettings["APU.Sunat.FormatoFecha"];
        [WebMethod]
        public static string ImprimirComprobante(string numeroDocumento, string tipoDocumentoId, string serie, string correlativo, string fechaEmision, decimal montoTotal)
        {
            var mensaje = String.Empty;
            try
            {
                var fecha = DateTime.ParseExact(fechaEmision, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                var ventaInfoLista = new Negocio.VentaPetroamerica().ListarPaginado(0, numeroDocumento.Trim(), tipoDocumentoId, serie, correlativo, fecha, fecha, 0, 0, String.Empty,  0, 0);
                ventaInfoLista = ventaInfoLista.Where(v => v.MontoTotal.Equals(montoTotal)).ToList();
                var ventaInfo = new VentaPetroamericaInfo();
                if (ventaInfoLista.Count > 0)
                {
                    ventaInfo = ventaInfoLista.FirstOrDefault();
                }
                else
                {
                    mensaje = "No se ha encontrado el comprobante." + "@" + "" + "@" + ventaInfo.NumeroDocumentoCliente;
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
                emisor.Departamento = "";
                emisor.Direccion = ventaInfo.DireccionEmpresa;
                emisor.Distrito = "";
                emisor.NombreComercial = ventaInfo.RazonSocialEmpresa;
                emisor.NombreLegal = ventaInfo.RazonSocialEmpresa;
                emisor.NroDocumento = ventaInfo.RucEmpresa;
                emisor.Provincia = "";
                emisor.TipoDocumento = "6";
                emisor.Ubigeo = "";
                emisor.Urbanizacion = "";
                _documento.Emisor = emisor;
                #endregion

                _documento.Exoneradas = 0;
                _documento.FechaEmision = ventaInfo.FechaEmision.ToString("dd/MM/yyyy");
                _documento.Gratuitas = 0;
                _documento.Gravadas = ventaInfo.MontoVenta;
                _documento.IdDocumento = ventaInfo.Serie + "-" + ventaInfo.NumeroComprobante;
                _documento.Inafectas = 0;

                #region Items
                var itemId = 1;
                //foreach (var vd in ventaDetalleInfo)
                //{
                var item = new DetalleDocumento();
                //item.Cantidad = vd.Cantidad;
                item.Cantidad = ventaInfo.Cantidad;

                //item.Descripcion = vd.Producto;
                item.Descripcion = ventaInfo.Producto;
                //item.Id = itemId;
                item.Id = ventaInfo.ProductoId;
                itemId++;
                //item.Impuesto = vd.Igv;
                item.Impuesto = ventaInfo.MontoImpuesto;
                //item.PrecioUnitario = vd.PrecioUnitario;
                item.PrecioUnitario = ventaInfo.Precio;
                //item.Suma = vd.SubTotal;
                item.Suma = ventaInfo.MontoVenta;
                //item.TotalVenta = vd.MontoTotal;
                item.TotalVenta = ventaInfo.MontoTotal;
                //item.UnidadMedida = vd.UnidadMedida;
                item.UnidadMedida = "NIU";
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
                _documento.MontoEnLetras = Herramientas.NumeroALetras.numeroAletras(ventaInfo.MontoTotal);
                _documento.MontoPercepcion = 0;
                _documento.PlacaVehiculo = ventaInfo.PlacaVehiculo;

                #region Receptor
                var receptor = new DocumentoElectronico().Receptor;
                receptor.Departamento = "";
                receptor.Direccion = ventaInfo.DireccionCliente;
                receptor.Distrito = "";
                receptor.NombreComercial = ventaInfo.Cliente;
                receptor.NombreLegal = ventaInfo.Cliente;
                receptor.NroDocumento = ventaInfo.NumeroDocumentoCliente;
                receptor.Provincia = "";
                receptor.TipoDocumento = ventaInfo.TipoDocumentoIdCliente;
                receptor.Ubigeo = "";
                receptor.Urbanizacion = "";
                _documento.Receptor = receptor;
                #endregion

                _documento.TipoDocumento = ventaInfo.TipoComprobanteId;
                _documento.TotalIgv = ventaInfo.MontoImpuesto;
                _documento.TotalIsc = 0;
                _documento.TotalOtrosTributos = 0;
                _documento.TotalVenta = ventaInfo.MontoTotal;
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

                var rutaPdfFactura = HostingEnvironment.MapPath("~/Archivos/Documentos/Cliente/" + ventaInfo.NumeroDocumentoCliente + "");

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

                if (ventaInfo.TipoComprobanteId.Equals(Constantes.TipoComprobanteFactura))
                {
                    comprobanteSB.Append(Herramientas.Helper.ObtenerTexto("~/Archivos/Plantillas/FacturaPetroamerica.html"));
                    #region Factura
                    //comprobanteSB.Replace("@EMPRESA_LOGO", HttpContext.Current.Server.MapPath("~/Imagenes/petroamerica_logo.png"));
                    comprobanteSB.Replace("@EMPRESA_LOGO", String.Empty);
                    comprobanteSB.Replace("@EMPRESA_NOMBRE", ventaInfo.RazonSocialEmpresa);
                    comprobanteSB.Replace("@EMPRESA_DIRECCION", ventaInfo.DireccionEmpresa);
                    comprobanteSB.Replace("@GRIFO_DIRECCION", ventaInfo.DireccionAgencia);
                    comprobanteSB.Replace("@EMPRESA_RUC", ventaInfo.RucEmpresa);
                    comprobanteSB.Replace("@IMPRESORA", ventaInfo.ImpresoraAgencia);
                    comprobanteSB.Replace("@NUMEROCOMPROBANTE", ventaInfo.Serie + "-" + ventaInfo.NumeroComprobante.ToString().PadLeft(8, '0'));
                    comprobanteSB.Replace("@FECHA", fechaEmision);
                    
                    comprobanteSB.Replace("@CLIENTE_NOMBRE", ventaInfo.Cliente);
                    comprobanteSB.Replace("@CLIENTE_RUC", ventaInfo.NumeroDocumentoCliente);
                    comprobanteSB.Replace("@CLIENTE_PLACA", ventaInfo.PlacaVehiculo);
                    comprobanteSB.Replace("@CLIENTE_DIRECCION", ventaInfo.DireccionCliente);
                    comprobanteSB.Replace("@CANTIDAD", ventaInfo.Cantidad.ToString("N2"));
                    comprobanteSB.Replace("@PRECIO", ventaInfo.Precio.ToString("N2"));
                    comprobanteSB.Replace("@MONTO_VENTA", ventaInfo.MontoVenta.ToString("N2"));
                    comprobanteSB.Replace("@PRODUCTO", ventaInfo.Producto);
                    comprobanteSB.Replace("@MONEDA_SIMBOLO", ventaInfo.SimboloMoneda);
                    comprobanteSB.Replace("@MONTO_VENTA", ventaInfo.MontoVenta.ToString("N2"));
                    comprobanteSB.Replace("@MONTO_IMPUESTO", ventaInfo.MontoImpuesto.ToString("N2"));
                    comprobanteSB.Replace("@MONTO_TOTAL", ventaInfo.MontoTotal.ToString("N2"));
                    comprobanteSB.Replace("@VENDEDOR", ventaInfo.UsuarioCreacion);
                    comprobanteSB.Replace("@CODIGO_HASH", responseFirma.Data.ResumenFirma);

                    comprobanteSB.Replace("@TEXTO_MONTO_TOTAL", NumeroALetras.numeroAletras(ventaInfo.MontoTotal));
                    #endregion

                    nombreArchivoComprobante = "Factura_" + DateTime.Now.ToString("ddMMyyyyhhmmss"); // + ".pdf";
                }
                if (ventaInfo.TipoComprobanteId.Equals(Constantes.TipoComprobanteBoletaVenta))
                {
                    comprobanteSB.Append(Herramientas.Helper.ObtenerTexto("~/Archivos/Plantillas/BoletaPetroamerica.html"));
                    #region Boleta
                    //comprobanteSB.Replace("@EMPRESA_LOGO", HttpContext.Current.Server.MapPath("~/Imagenes/petroamerica_logo.png"));
                    comprobanteSB.Replace("@EMPRESA_LOGO", String.Empty);
                    comprobanteSB.Replace("@EMPRESA_NOMBRE", ventaInfo.RazonSocialEmpresa);
                    comprobanteSB.Replace("@EMPRESA_DIRECCION", ventaInfo.DireccionEmpresa);
                    comprobanteSB.Replace("@GRIFO_DIRECCION", ventaInfo.DireccionAgencia);
                    comprobanteSB.Replace("@EMPRESA_RUC", ventaInfo.RucEmpresa);
                    comprobanteSB.Replace("@IMPRESORA", ventaInfo.ImpresoraAgencia);
                    comprobanteSB.Replace("@NUMEROCOMPROBANTE", ventaInfo.Serie + "-" + ventaInfo.NumeroComprobante.ToString().PadLeft(8, '0'));
                    comprobanteSB.Replace("@FECHA", fechaEmision);
                    comprobanteSB.Replace("@CLIENTE_NOMBRE", ventaInfo.Cliente);
                    comprobanteSB.Replace("@CLIENTE_DIRECCION", ventaInfo.DireccionCliente);
                    comprobanteSB.Replace("@CANTIDAD", ventaInfo.Cantidad.ToString("N2"));
                    comprobanteSB.Replace("@PRECIO", ventaInfo.Precio.ToString("N2"));
                    comprobanteSB.Replace("@MONTO_VENTA", ventaInfo.MontoVenta.ToString("N2"));
                    comprobanteSB.Replace("@PRODUCTO", ventaInfo.Producto);
                    comprobanteSB.Replace("@MONEDA_SIMBOLO", ventaInfo.SimboloMoneda);
                    comprobanteSB.Replace("@MONTO_VENTA", ventaInfo.MontoVenta.ToString("N2"));
                    comprobanteSB.Replace("@MONTO_IMPUESTO", ventaInfo.MontoImpuesto.ToString("N2"));
                    comprobanteSB.Replace("@MONTO_TOTAL", ventaInfo.MontoTotal.ToString("N2"));
                    comprobanteSB.Replace("@VENDEDOR", ventaInfo.UsuarioCreacion);
                    comprobanteSB.Replace("@CODIGO_HASH", responseFirma.Data.ResumenFirma);

                    comprobanteSB.Replace("@TEXTO_MONTO_TOTAL", NumeroALetras.numeroAletras(ventaInfo.MontoTotal));
                    #endregion

                    nombreArchivoComprobante = "Boleta_" + DateTime.Now.ToString("ddMMyyyyhhmmss"); // + ".pdf";
                }

                if (!Directory.Exists(rutaPdfFactura))
                {
                    Directory.CreateDirectory(rutaPdfFactura);
                }

                #region Codigo QR
                var codigoQR = ventaInfo.NumeroDocumentoCliente + "|" + ventaInfo.TipoComprobanteId + "|" + ventaInfo.Serie + "|" +
                               ventaInfo.NumeroComprobante + "|" + ventaInfo.MontoImpuesto + "|" + ventaInfo.MontoTotal + "|" +
                               ventaInfo.FechaEmision + "|" + ventaInfo.TipoDocumentoIdCliente + "|" + ventaInfo.NumeroDocumentoCliente + "|" +
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

                comprobanteSB.Replace("@CODIGO_QR", HttpContext.Current.Server.MapPath("~/Archivos/Documentos/Cliente/" + ventaInfo.NumeroDocumentoCliente + "/" + _documento.IdDocumento + ".bmp"));


                comprobanteSB = Herramientas.Helper.EncodeHtml(comprobanteSB);
                //var margins = new PageMargins();
                //margins.Left = 1;
                //htmlToComprobante.Margins = margins;

                var pdfBytesFactura = htmlToComprobante.GeneratePdf(comprobanteSB.ToString());


                // var comprobanteImpreso = rutaPdfFactura + "\\" + nombreArchivoComprobante + ".pdf";
                File.WriteAllBytes(rutaPdfFactura + "/" + nombreArchivoComprobante + ".pdf", pdfBytesFactura);
                File.WriteAllBytes(rutaPdfFactura + "/" + nombreArchivoComprobante + ".xml", Convert.FromBase64String(responseFirma.Data.TramaXmlFirmado));
                #endregion

                mensaje = "El Comprobante se generó correctamente" + "@" + nombreArchivoComprobante + "@" + ventaInfo.NumeroDocumentoCliente;
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
    }
}