using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Serialization;
using APU.Gasolutions.Archivos.Comprobante;
using APU.Gasolutions.WS_Gasolutions.Local;
using APU.Herramientas;
using APU.Negocio;
using OpenInvoicePeru.Comun.Dto.Intercambio;
using OpenInvoicePeru.Comun.Dto.Modelos;
using RestSharp;
using VentaDetalleGasolutionsInfo = APU.Entidad.VentaDetalleGasolutionsInfo;
using VentaGasolutionsInfo = APU.Entidad.VentaGasolutionsInfo;

namespace APU.Gasolutions
{
    public partial class Prueba : System.Web.UI.Page
    {
        public static DocumentoElectronico _documento;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private static readonly string BaseUrl = ConfigurationManager.AppSettings["APU.Sunat.BaseUrl"];
        private static readonly string UrlSunat = ConfigurationManager.AppSettings["APU.Sunat.UrlSunat"];
        private static readonly string FormatoFecha = ConfigurationManager.AppSettings["APU.Sunat.FormatoFecha"];
        protected void btnDeserializarXml_Click(object sender, EventArgs e)
        {
            var data = new EnviarDocumentoResponse();

            Invoice factura = null;
            string path = @"D:\ASOLUTIONS\Gasolutions\Liquidos\Ejemplos CPE XML\factura_descuento_ok.xml";
            //string path = @"D:\ASOLUTIONS\Gasolutions\Liquidos\Ejemplos CPE XML\NOTA CREDITO\B001-00000016.xml";

            XmlSerializer serializer = new XmlSerializer(typeof(Invoice));
            StreamReader reader = new StreamReader(path);
            factura = (Invoice)serializer.Deserialize(reader);
            reader.Close();

            StreamReader readerX = new StreamReader(path);
            var ventaXml = readerX.ReadToEnd();
            readerX.Close();
            


            _documento = new DocumentoElectronico();

            #region Documento
            _documento.CalculoDetraccion = 0;
            _documento.CalculoIgv = Decimal.Divide(18, 100);
            _documento.CalculoIsc = 0;

            #region Emisor
            var emisor = new DocumentoElectronico().Emisor;
            emisor.Departamento = factura.AccountingSupplierParty.Party.PostalAddress.CountrySubentity;
            emisor.Direccion = factura.AccountingSupplierParty.Party.PostalAddress.StreetName;
            emisor.Distrito = factura.AccountingSupplierParty.Party.PostalAddress.District;
            emisor.NombreComercial = factura.AccountingSupplierParty.Party.PartyName.Name;
            emisor.NombreLegal = factura.AccountingSupplierParty.Party.PartyLegalEntity.RegistrationName;
            emisor.NroDocumento = factura.AccountingSupplierParty.CustomerAssignedAccountID; // "10421895452";
            emisor.Provincia = factura.AccountingSupplierParty.Party.PostalAddress.CityName;
            emisor.TipoDocumento = factura.AccountingSupplierParty.AdditionalAccountID;
            emisor.Ubigeo = factura.AccountingSupplierParty.Party.PostalAddress.ID;
            emisor.Urbanizacion = String.Empty;
            _documento.Emisor = emisor;
            #endregion

            _documento.Exoneradas = 0;
            _documento.FechaEmision = factura.IssueDate.ToString("dd/MM/yyyy");
            _documento.Gratuitas = 0;
            //_documento.Gravadas = factura.LegalMonetaryTotal.PayableAmount;
            _documento.Gravadas = ((InvoiceAdditionalInformationAdditionalMonetaryTotalPayableAmount) factura.AdditionalInformation.AdditionalMonetaryTotal.Items[1]).Value;
            _documento.IdDocumento = factura.ID;
            _documento.Inafectas = 0;

            #region Items
            //var itemId = 1;
            foreach (var vd in factura.InvoiceLine)
            {
                var item = new DetalleDocumento();
                item.Cantidad = vd.InvoicedQuantity;
                item.CodigoItem = vd.Item.SellersItemIdentification.ID;
                item.Descripcion = vd.Item.Description;
                //item.Id = itemId;
                //itemId++;
                item.Impuesto = vd.TaxTotal.TaxAmount;
                item.PrecioUnitario = vd.Price.PriceAmount;
                item.TotalVenta = vd.LineExtensionAmount;
                item.UnidadMedida = vd.UnitCode;
                item.Descuento = 0;
                item.Id = vd.ID;
                item.ImpuestoSelectivo = 0;
                item.OtroImpuesto = 0;
                item.PrecioReferencial = 0;
                item.TipoImpuesto = "10";
                item.TipoPrecio = "01";
                _documento.Items.Add(item);
            }
            #endregion

            _documento.Moneda = factura.DocumentCurrencyCode;
            _documento.MontoAnticipo = 0;
            _documento.MontoDetraccion = 0;
            _documento.MontoEnLetras = NumeroALetras.numeroAletras(factura.LegalMonetaryTotal.PayableAmount);
            _documento.MontoPercepcion = 0;
            _documento.PlacaVehiculo = String.Empty;

            #region Receptor
            var receptor = new DocumentoElectronico().Receptor;
            receptor.NombreComercial = factura.AccountingCustomerParty.Party.PartyLegalEntity.RegistrationName;
            receptor.NombreLegal = factura.AccountingCustomerParty.Party.PartyLegalEntity.RegistrationName;
            receptor.NroDocumento = factura.AccountingCustomerParty.CustomerAssignedAccountID;
            receptor.TipoDocumento = factura.AccountingCustomerParty.AdditionalAccountID;
            receptor.Direccion = String.Empty;
            _documento.Receptor = receptor;
            #endregion

            _documento.TipoDocumento = factura.InvoiceTypeCode;
            _documento.TotalIgv = factura.TaxTotal.TaxAmount;
            _documento.TotalIsc = 0;
            _documento.TotalOtrosTributos = 0;
            _documento.TotalVenta = factura.LegalMonetaryTotal.PayableAmount;
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
            string rutaXml = HostingEnvironment.MapPath("~/Archivos/Facturacion/XML/Cliente/" + receptor.NroDocumento);
            if (!Directory.Exists(rutaXml))
            {
                Directory.CreateDirectory(rutaXml);
            }
            File.WriteAllBytes(rutaXml + "/" + _documento.IdDocumento + ".xml", Convert.FromBase64String(documentoResponse.Data.TramaXmlSinFirma));
            #endregion

            var respuestaSunatInfo = new RespuestaSunatInfo();
            var firmaInfo = new RespuestaSunatInfo().Firma;
            var respuestaInfo = new RespuestaSunatInfo().Respuesta;

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
            string rutaXmlFirmado = HostingEnvironment.MapPath("~/Archivos/Facturacion/XML/Cliente/" + receptor.NroDocumento);
            if (!Directory.Exists(rutaXmlFirmado))
            {
                Directory.CreateDirectory(rutaXmlFirmado);
            }
            File.WriteAllBytes(rutaXmlFirmado + "/" + _documento.IdDocumento + "_Firmado.xml", Convert.FromBase64String(responseFirma.Data.TramaXmlFirmado));

            firmaInfo.Exito = responseFirma.Data.Exito;
            firmaInfo.MensajeError = responseFirma.Data.MensajeError;
            firmaInfo.Pila = responseFirma.Data.Pila;
            firmaInfo.ResumenFirma = responseFirma.Data.ResumenFirma;
            firmaInfo.TramaXmlFirmado = responseFirma.Data.TramaXmlFirmado;
            firmaInfo.ValorFirma = responseFirma.Data.ValorFirma;

            respuestaSunatInfo.Firma = firmaInfo;
            #endregion

            #region Envio SUNAT
            var sendBill = new EnviarDocumentoRequest
            {
                Ruc = _documento.Emisor.NroDocumento,
                UsuarioSol = "FACTRC18",
                ClaveSol = "rcfact2018",

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

            data = responseSendBill.Data;

            var estadoComprobante = Constantes.EstadoComprobanteRechazado;

            var resumenFirma = String.Empty;

            if (!responseSendBill.Data.Exito)
            {
                estadoComprobante = Constantes.EstadoComprobanteRechazado;
            }
            else
            {
                estadoComprobante = Constantes.EstadoComprobanteAceptado;

                string rutaCdr = HostingEnvironment.MapPath("~/Archivos/Facturacion/CDR/Cliente/" + receptor.NroDocumento);
                if (!Directory.Exists(rutaCdr))
                {
                    Directory.CreateDirectory(rutaCdr);
                }
                File.WriteAllBytes(rutaCdr + "/" + responseSendBill.Data.NombreArchivo + ".zip", Convert.FromBase64String(responseSendBill.Data.TramaZipCdr));

                respuestaInfo.CodigoRespuesta = responseSendBill.Data.CodigoRespuesta;
                respuestaInfo.Exito = responseSendBill.Data.Exito;
                respuestaInfo.MensajeError = responseSendBill.Data.MensajeError;
                respuestaInfo.MensajeRespuesta = responseSendBill.Data.MensajeRespuesta;
                respuestaInfo.NombreArchivo = responseSendBill.Data.NombreArchivo;
                respuestaInfo.Pila = responseSendBill.Data.Pila;
                respuestaInfo.TramaZipCdr = responseSendBill.Data.TramaZipCdr;

                respuestaSunatInfo.Respuesta = respuestaInfo;

                #region Envio Servidor APUFact
                var ventaInfo = new VentaGasolutionsInfo();

                ventaInfo.FechaEmision = DateTime.ParseExact(_documento.FechaEmision, "dd/MM/yyyy", null);
                ventaInfo.Gravadas = _documento.Gravadas;
                ventaInfo.IdDocumento = _documento.IdDocumento;
                ventaInfo.CalculoIgv = _documento.CalculoIgv;
                ventaInfo.MonedaId = _documento.Moneda;
                ventaInfo.MontoEnLetras = _documento.MontoEnLetras;
                ventaInfo.PlacaVehiculo = _documento.PlacaVehiculo;
                ventaInfo.TipoDocumento = _documento.TipoDocumento;
                ventaInfo.TotalIgv = _documento.TotalIgv;
                ventaInfo.TotalVenta = _documento.TotalVenta;

                ventaInfo.TipoDocumentoEmisor = _documento.Emisor.TipoDocumento;
                ventaInfo.NroDocumentoEmisor = _documento.Emisor.NroDocumento;
                ventaInfo.NombreComercialEmisor = _documento.Emisor.NombreComercial;
                ventaInfo.NombreLegalEmisor = _documento.Emisor.NombreLegal;
                ventaInfo.DepartamentoEmisor = _documento.Emisor.Departamento;
                ventaInfo.ProvinciaEmisor = _documento.Emisor.Provincia;
                ventaInfo.DistritoEmisor = _documento.Emisor.Distrito;
                ventaInfo.UbigeoEmisor = _documento.Emisor.Ubigeo;
                ventaInfo.DireccionEmisor = _documento.Emisor.Direccion;

                ventaInfo.TipoDocumentoReceptor = _documento.Receptor.TipoDocumento;
                ventaInfo.NroDocumentoReceptor = _documento.Receptor.NroDocumento;
                ventaInfo.NombreComercialReceptor = _documento.Receptor.NombreComercial;
                ventaInfo.NombreLegalReceptor = _documento.Receptor.NombreLegal;
                ventaInfo.DireccionReceptor = _documento.Receptor.Direccion;

                ventaInfo.CodigoRespuesta = data.CodigoRespuesta;
                ventaInfo.Exito = data.Exito ? 1 : 0;
                ventaInfo.MensajeError = String.Empty;
                ventaInfo.MensajeRespuesta = data.MensajeRespuesta;
                ventaInfo.NombreArchivo = data.NombreArchivo;
                ventaInfo.NroTicket = String.Empty;

                ventaInfo.EstadoId = estadoComprobante;
                ventaInfo.ComprobanteImpreso = String.Empty;

                #region Venta Detalle
                var itemGasolutions = _documento.Items.FirstOrDefault();
                ventaInfo.Item = new VentaDetalleGasolutionsInfo
                {
                    Cantidad = itemGasolutions.Cantidad,
                    CodigoItem = itemGasolutions.CodigoItem,
                    Descripcion = itemGasolutions.Descripcion,
                    Id = itemGasolutions.Id,
                    Impuesto = itemGasolutions.Impuesto,
                    PrecioUnitario = itemGasolutions.PrecioUnitario,
                    TipoImpuesto = itemGasolutions.TipoImpuesto,
                    TipoPrecio = itemGasolutions.TipoPrecio,
                    TotalVenta = itemGasolutions.TotalVenta
                };
                #endregion

                var ws = new WS_Gasolutions.Local.ServiceTerminalHostClient();
                // var ws = new WS_Gasolutions.GasolutionsClient();

                // var ventaGasolutionsInfo = new VentaGasolutionsInfo();

                // var ventaId = ws.VentaGasolutions_Insertar(ventaInfo);

                //resumenFirma = ws.VentaGasolutions_InsertarXml(ventaXml);
                #endregion
            }

            //var data = responseSendBill.Data;
            data = responseSendBill.Data;
            #endregion
        }

        protected void btnDeserializarXmlGnv_OnClick(object sender, EventArgs e)
        {
            var data = new EnviarDocumentoResponse();

            Archivos.Comprobante.Envelope factura = null;
            //string path = @"D:\ASOLUTIONS\Gasolutions\Ejemplos CPE XML\Factura.xml";
            string path = @"D:\ASOLUTIONS\Gasolutions\GNV\FacturaGnv.xml";

            XmlSerializer serializer = new XmlSerializer(typeof(Archivos.Comprobante.Envelope));
            StreamReader reader = new StreamReader(path);
            factura = (Archivos.Comprobante.Envelope)serializer.Deserialize(reader);
            reader.Close();

            StreamReader readerX = new StreamReader(path);
            var ventaXml = readerX.ReadToEnd();
            readerX.Close();

            // factura = (Archivos.Comprobante.Envelope)serializer.Deserialize(ventaXml);
            

            _documento = new DocumentoElectronico();

            #region Documento
            _documento.CalculoDetraccion = 0;
            _documento.CalculoIgv = Decimal.Divide(18, 100);
            _documento.CalculoIsc = 0;

            #region Emisor
            var emisor = new DocumentoElectronico().Emisor;
            emisor.Departamento = String.Empty;
            emisor.Direccion = factura.Body.GenerarDocumento.oDocumento.direccionAdquiriente;
            emisor.Distrito = String.Empty;
            emisor.NombreComercial = String.Empty;
            emisor.NombreLegal = String.Empty;
            emisor.NroDocumento = factura.Body.GenerarDocumento.oDocumento.rucEmisor;
            emisor.Provincia = String.Empty;
            emisor.TipoDocumento = "6";
            emisor.Ubigeo = String.Empty;
            emisor.Urbanizacion = String.Empty;
            _documento.Emisor = emisor;
            #endregion

            _documento.Exoneradas = 0;
            _documento.FechaEmision = factura.Body.GenerarDocumento.oDocumento.fechaEmision.ToString("dd/MM/yyyy");
            _documento.Gratuitas = 0;
            _documento.Gravadas = factura.Body.GenerarDocumento.oDocumento.operacion.Operacion.monto;
            _documento.IdDocumento = factura.Body.GenerarDocumento.oDocumento.numeracion;
            _documento.Inafectas = 0;

            #region Items
            //foreach (var vd in factura.Body.GenerarDocumento.oDocumento.item)
            //{
            var vd = factura.Body.GenerarDocumento.oDocumento.item;
                var item = new DetalleDocumento();
                item.Cantidad = factura.Body.GenerarDocumento.oDocumento.cantidadItem;
                item.CodigoItem = vd.Item.codigoProducto;
                item.Descripcion = factura.Body.GenerarDocumento.oDocumento.descripcionItem;
                //item.Id = itemId;
                //itemId++;
                item.Impuesto = factura.Body.GenerarDocumento.oDocumento.tributos.Tributo.valor;
                item.PrecioUnitario = factura.Body.GenerarDocumento.oDocumento.precioUnitario;
                item.TotalVenta = factura.Body.GenerarDocumento.oDocumento.totalVenta;
                item.UnidadMedida = factura.Body.GenerarDocumento.oDocumento.unidadItem;
                item.Descuento = 0;
                item.Id = 1;
                item.ImpuestoSelectivo = 0;
                item.OtroImpuesto = 0;
                item.PrecioReferencial = 0;
                item.TipoImpuesto = "10";
                item.TipoPrecio = "01";
                _documento.Items.Add(item);
            //}
            #endregion

            _documento.Moneda = factura.Body.GenerarDocumento.oDocumento.tipoMoneda;
            _documento.MontoAnticipo = 0;
            _documento.MontoDetraccion = 0;
            _documento.MontoEnLetras = NumeroALetras.numeroAletras(factura.Body.GenerarDocumento.oDocumento.operacion.Operacion.monto);
            _documento.MontoPercepcion = 0;
            _documento.PlacaVehiculo = String.Empty;

            #region Receptor
            var receptor = new DocumentoElectronico().Receptor;
            receptor.NombreComercial = factura.Body.GenerarDocumento.oDocumento.nombreAdquiriente;
            receptor.NombreLegal = factura.Body.GenerarDocumento.oDocumento.nombreAdquiriente;
            receptor.NroDocumento = factura.Body.GenerarDocumento.oDocumento.numeroDocumentoAdquiriente;
            receptor.TipoDocumento = factura.Body.GenerarDocumento.oDocumento.tipoDocumentoAdquiriente;
            receptor.Direccion = factura.Body.GenerarDocumento.oDocumento.direccionAdquiriente;
            _documento.Receptor = receptor;
            #endregion

            _documento.TipoDocumento = factura.Body.GenerarDocumento.oDocumento.tipoDocumento.PadLeft(2, '0');
            _documento.TotalIgv = factura.Body.GenerarDocumento.oDocumento.tributos.Tributo.valor;
            _documento.TotalIsc = 0;
            _documento.TotalOtrosTributos = 0;
            _documento.TotalVenta = factura.Body.GenerarDocumento.oDocumento.totalVenta;
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
            string rutaXml = HostingEnvironment.MapPath("~/Archivos/Facturacion/XML/Cliente/" + receptor.NroDocumento);
            if (!Directory.Exists(rutaXml))
            {
                Directory.CreateDirectory(rutaXml);
            }
            File.WriteAllBytes(rutaXml + "/" + _documento.IdDocumento + ".xml", Convert.FromBase64String(documentoResponse.Data.TramaXmlSinFirma));
            #endregion

            var respuestaSunatInfo = new RespuestaSunatInfo();
            var firmaInfo = new RespuestaSunatInfo().Firma;
            var respuestaInfo = new RespuestaSunatInfo().Respuesta;

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
            string rutaXmlFirmado = HostingEnvironment.MapPath("~/Archivos/Facturacion/XML/Cliente/" + receptor.NroDocumento);
            if (!Directory.Exists(rutaXmlFirmado))
            {
                Directory.CreateDirectory(rutaXmlFirmado);
            }
            File.WriteAllBytes(rutaXmlFirmado + "/" + _documento.IdDocumento + "_Firmado.xml", Convert.FromBase64String(responseFirma.Data.TramaXmlFirmado));

            firmaInfo.Exito = responseFirma.Data.Exito;
            firmaInfo.MensajeError = responseFirma.Data.MensajeError;
            firmaInfo.Pila = responseFirma.Data.Pila;
            firmaInfo.ResumenFirma = responseFirma.Data.ResumenFirma;
            firmaInfo.TramaXmlFirmado = responseFirma.Data.TramaXmlFirmado;
            firmaInfo.ValorFirma = responseFirma.Data.ValorFirma;

            respuestaSunatInfo.Firma = firmaInfo;
            #endregion

            #region Envio SUNAT
            var sendBill = new EnviarDocumentoRequest
            {
                Ruc = _documento.Emisor.NroDocumento,
                UsuarioSol = "FACTRC18",
                ClaveSol = "rcfact2018",

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

            data = responseSendBill.Data;

            var estadoComprobante = Constantes.EstadoComprobanteRechazado;

            var resumenFirma = String.Empty;

            if (!responseSendBill.Data.Exito)
            {
                estadoComprobante = Constantes.EstadoComprobanteRechazado;
            }
            else
            {
                estadoComprobante = Constantes.EstadoComprobanteAceptado;

                string rutaCdr = HostingEnvironment.MapPath("~/Archivos/Facturacion/CDR/Cliente/" + receptor.NroDocumento);
                if (!Directory.Exists(rutaCdr))
                {
                    Directory.CreateDirectory(rutaCdr);
                }
                File.WriteAllBytes(rutaCdr + "/" + responseSendBill.Data.NombreArchivo + ".zip", Convert.FromBase64String(responseSendBill.Data.TramaZipCdr));

                respuestaInfo.CodigoRespuesta = responseSendBill.Data.CodigoRespuesta;
                respuestaInfo.Exito = responseSendBill.Data.Exito;
                respuestaInfo.MensajeError = responseSendBill.Data.MensajeError;
                respuestaInfo.MensajeRespuesta = responseSendBill.Data.MensajeRespuesta;
                respuestaInfo.NombreArchivo = responseSendBill.Data.NombreArchivo;
                respuestaInfo.Pila = responseSendBill.Data.Pila;
                respuestaInfo.TramaZipCdr = responseSendBill.Data.TramaZipCdr;

                respuestaSunatInfo.Respuesta = respuestaInfo;

                #region Envio Servidor APUFact
                var ventaInfo = new VentaGasolutionsInfo();

                ventaInfo.FechaEmision = DateTime.ParseExact(_documento.FechaEmision, "dd/MM/yyyy", null);
                ventaInfo.Gravadas = _documento.Gravadas;
                ventaInfo.IdDocumento = _documento.IdDocumento;
                ventaInfo.CalculoIgv = _documento.CalculoIgv;
                ventaInfo.MonedaId = _documento.Moneda;
                ventaInfo.MontoEnLetras = _documento.MontoEnLetras;
                ventaInfo.PlacaVehiculo = _documento.PlacaVehiculo;
                ventaInfo.TipoDocumento = _documento.TipoDocumento;
                ventaInfo.TotalIgv = _documento.TotalIgv;
                ventaInfo.TotalVenta = _documento.TotalVenta;

                ventaInfo.TipoDocumentoEmisor = _documento.Emisor.TipoDocumento;
                ventaInfo.NroDocumentoEmisor = _documento.Emisor.NroDocumento;
                ventaInfo.NombreComercialEmisor = _documento.Emisor.NombreComercial;
                ventaInfo.NombreLegalEmisor = _documento.Emisor.NombreLegal;
                ventaInfo.DepartamentoEmisor = _documento.Emisor.Departamento;
                ventaInfo.ProvinciaEmisor = _documento.Emisor.Provincia;
                ventaInfo.DistritoEmisor = _documento.Emisor.Distrito;
                ventaInfo.UbigeoEmisor = _documento.Emisor.Ubigeo;
                ventaInfo.DireccionEmisor = _documento.Emisor.Direccion;

                ventaInfo.TipoDocumentoReceptor = _documento.Receptor.TipoDocumento;
                ventaInfo.NroDocumentoReceptor = _documento.Receptor.NroDocumento;
                ventaInfo.NombreComercialReceptor = _documento.Receptor.NombreComercial;
                ventaInfo.NombreLegalReceptor = _documento.Receptor.NombreLegal;
                ventaInfo.DireccionReceptor = _documento.Receptor.Direccion;

                ventaInfo.CodigoRespuesta = data.CodigoRespuesta;
                ventaInfo.Exito = data.Exito ? 1 : 0;
                ventaInfo.MensajeError = String.Empty;
                ventaInfo.MensajeRespuesta = data.MensajeRespuesta;
                ventaInfo.NombreArchivo = data.NombreArchivo;
                ventaInfo.NroTicket = String.Empty;

                ventaInfo.EstadoId = estadoComprobante;
                ventaInfo.ComprobanteImpreso = String.Empty;

                //var ws = new WS_Gasolutions.GasolutionsClient();
                var ventaGasolutionsInfo = new VentaGasolutionsInfo();
                // resumenFirma = ws.VentaGasolutions_InsertarGnvXml(ventaXml);
                #endregion
            }

            //var data = responseSendBill.Data;
            data = responseSendBill.Data;
            #endregion
        }

        protected void btnNotaCredito_OnClick(object sender, EventArgs e)
        {
            var data = new EnviarDocumentoResponse();

            CreditNote notaCredito = null;
            // string path = @"D:\ASOLUTIONS\Gasolutions\Liquidos\Ejemplos CPE XML\Nota de credito.xml";
            string path = @"D:\ASOLUTIONS\Gasolutions\Liquidos\Ejemplos CPE XML\NOTA CREDITO\B001-00000016.xml";

            XmlSerializer serializer = new XmlSerializer(typeof(CreditNote));
            StreamReader reader = new StreamReader(path);
            notaCredito = (CreditNote)serializer.Deserialize(reader);
            reader.Close();

            StreamReader readerX = new StreamReader(path);
            var notaCreditoXml = readerX.ReadToEnd();
            readerX.Close();


            #region Documento Electrónico
            var documento = new DocumentoElectronico
            {
                Emisor = new Contribuyente()
                {
                    Departamento = notaCredito.AccountingSupplierParty.Party.PostalAddress.CityName,
                    Direccion = notaCredito.AccountingSupplierParty.Party.PostalAddress.StreetName,
                    Distrito = notaCredito.AccountingSupplierParty.Party.PostalAddress.District,
                    NombreComercial = notaCredito.AccountingSupplierParty.Party.PartyName.Name,
                    NombreLegal = notaCredito.AccountingSupplierParty.Party.PartyName.Name,
                    NroDocumento = notaCredito.AccountingSupplierParty.CustomerAssignedAccountID,
                    Provincia = notaCredito.AccountingSupplierParty.Party.PostalAddress.CountrySubentity,
                    TipoDocumento = "6",
                    Ubigeo = notaCredito.AccountingSupplierParty.Party.PostalAddress.ID,
                    Urbanizacion = "",
                },
                Receptor = new Contribuyente
                {
                    NroDocumento = notaCredito.AccountingCustomerParty.CustomerAssignedAccountID,
                    TipoDocumento = notaCredito.AccountingCustomerParty.AdditionalAccountID,
                    NombreLegal = notaCredito.AccountingCustomerParty.Party.PartyLegalEntity.RegistrationName,
                    NombreComercial = notaCredito.AccountingCustomerParty.Party.PartyLegalEntity.RegistrationName,
                    Direccion = String.Empty
                },
                IdDocumento = notaCredito.ID,
                FechaEmision = notaCredito.IssueDate.ToString("dd/MM/yyyy"),
                //FechaEmision = SetDate(notaCredito.IssueDate.ToString("dd/MM/yyyy")),
                Moneda = Constantes.MonedaSolesSunat,
                MontoEnLetras = Herramientas.NumeroALetras.numeroAletras(notaCredito.LegalMonetaryTotal.PayableAmount),
                PlacaVehiculo = String.Empty,
                CalculoIgv = 0.18m,
                CalculoIsc = 0m,
                CalculoDetraccion = 0m,
                TipoDocumento = "07",
                TotalIgv = notaCredito.TaxTotal.TaxAmount,
                TotalVenta = notaCredito.LegalMonetaryTotal.PayableAmount,
                Gravadas = notaCredito.AdditionalInformation.AdditionalMonetaryTotal.PayableAmount,
                Discrepancias = new List<Discrepancia>
                    {
                        new Discrepancia
                        {
                            //NroReferencia = "FF11-001",
                            NroReferencia = notaCredito.DiscrepancyResponse.ReferenceID,
                            //Tipo = "01",
                            Tipo = notaCredito.DiscrepancyResponse.ResponseCode,
                            Descripcion = notaCredito.DiscrepancyResponse.Description
                        }
                    },
                Relacionados = new List<DocumentoRelacionado>
                    {
                        new DocumentoRelacionado
                        {
                            //NroDocumento = "FF11-001",
                            NroDocumento = notaCredito.BillingReference.InvoiceDocumentReference.ID,
                            //TipoDocumento = "01"
                            TipoDocumento = notaCredito.BillingReference.InvoiceDocumentReference.DocumentTypeCode,
                        }
                    }
            };
            var items = new List<DetalleDocumento>();
            var cont = 0;
            foreach (var vd in notaCredito.CreditNoteLine)
            {
                cont++;
                var item = new DetalleDocumento
                {
                    Id = cont,
                    Cantidad = vd.CreditedQuantity,
                    PrecioReferencial = vd.PricingReference.AlternativeConditionPrice.PriceAmount,
                    PrecioUnitario = vd.Price.PriceAmount,
                    TipoPrecio = vd.PricingReference.AlternativeConditionPrice.PriceTypeCode,
                    CodigoItem = String.Empty,
                    Descripcion = vd.Item.Description,
                    UnidadMedida = vd.UnitCode,
                    Impuesto = vd.TaxTotal.TaxAmount,
                    TipoImpuesto = "10", // Gravada
                    TotalVenta = vd.LineExtensionAmount,
                    Suma = vd.PricingReference.AlternativeConditionPrice.PriceAmount
                };
                items.Add(item);
            }
            documento.Items = items;
            #endregion


            var documentoResponse = RestHelper<DocumentoElectronico, DocumentoResponse>.Execute("GenerarNotaCredito", documento);

            if (!documentoResponse.Exito)
            {
                throw new InvalidOperationException(documentoResponse.MensajeError);
            }



            var respuestaSunatInfo = new RespuestaSunatInfo();
            var firmaInfo = new RespuestaSunatInfo().Firma;
            var respuestaInfo = new RespuestaSunatInfo().Respuesta;



            // Firmado del Documento.
            string rutaCertificado = HostingEnvironment.MapPath("~/Archivos/Facturacion/certificado.pfx");
            var firmado = new FirmadoRequest
            {
                TramaXmlSinFirma = documentoResponse.TramaXmlSinFirma,
                CertificadoDigital = Convert.ToBase64String(File.ReadAllBytes(rutaCertificado)),
                PasswordCertificado = "9dGxdmm5JHKwKsXc",
                UnSoloNodoExtension = false
            };

            var responseFirma = RestHelper<FirmadoRequest, FirmadoResponse>.Execute("Firmar", firmado);

            if (!responseFirma.Exito)
            {
                throw new InvalidOperationException(responseFirma.MensajeError);
            }

            var documentoRequest = new EnviarDocumentoRequest
            {
                Ruc = documento.Emisor.NroDocumento,
                UsuarioSol = "FACTRC18",
                ClaveSol = "rcfact2018",
                EndPointUrl = UrlSunat,
                IdDocumento = documento.IdDocumento,
                TipoDocumento = documento.TipoDocumento,
                TramaXmlFirmado = responseFirma.TramaXmlFirmado
            };

            var enviarDocumentoResponse = RestHelper<EnviarDocumentoRequest, EnviarDocumentoResponse>.Execute("EnviarDocumento", documentoRequest);

            var estadoComprobante = Constantes.EstadoComprobanteRechazado;

            var resumenFirma = String.Empty;

            if (!enviarDocumentoResponse.Exito)
            {
                estadoComprobante = Constantes.EstadoComprobanteRechazado;
                //throw new InvalidOperationException(enviarDocumentoResponse.MensajeError);
            }
            else
            {
                estadoComprobante = Constantes.EstadoComprobanteAnulado;

                estadoComprobante = Constantes.EstadoComprobanteAceptado;

                string rutaCdr = HostingEnvironment.MapPath("~/Archivos/Facturacion/CDR/Cliente/" + documento.Receptor.NroDocumento);
                if (!Directory.Exists(rutaCdr))
                {
                    Directory.CreateDirectory(rutaCdr);
                }
                File.WriteAllBytes(rutaCdr + "/" + enviarDocumentoResponse.NombreArchivo + ".zip", Convert.FromBase64String(enviarDocumentoResponse.TramaZipCdr));

                respuestaInfo.CodigoRespuesta = enviarDocumentoResponse.CodigoRespuesta;
                respuestaInfo.Exito = enviarDocumentoResponse.Exito;
                respuestaInfo.MensajeError = enviarDocumentoResponse.MensajeError;
                respuestaInfo.MensajeRespuesta = enviarDocumentoResponse.MensajeRespuesta;
                respuestaInfo.NombreArchivo = enviarDocumentoResponse.NombreArchivo;
                respuestaInfo.Pila = enviarDocumentoResponse.Pila;
                respuestaInfo.TramaZipCdr = enviarDocumentoResponse.TramaZipCdr;

                respuestaSunatInfo.Respuesta = respuestaInfo;

                #region Envio Servidor APUFact
                var ventaInfo = new VentaGasolutionsInfo();

                ventaInfo.FechaEmision = DateTime.ParseExact(documento.FechaEmision, "dd/MM/yyyy", null);
                ventaInfo.Gravadas = documento.Gravadas;
                ventaInfo.IdDocumento = documento.IdDocumento;
                ventaInfo.CalculoIgv = documento.CalculoIgv;
                ventaInfo.MonedaId = documento.Moneda;
                ventaInfo.MontoEnLetras = documento.MontoEnLetras;
                ventaInfo.PlacaVehiculo = documento.PlacaVehiculo;
                ventaInfo.TipoDocumento = documento.TipoDocumento;
                ventaInfo.TotalIgv = documento.TotalIgv;
                ventaInfo.TotalVenta = documento.TotalVenta;

                ventaInfo.TipoDocumentoEmisor = documento.Emisor.TipoDocumento;
                ventaInfo.NroDocumentoEmisor = documento.Emisor.NroDocumento;
                ventaInfo.NombreComercialEmisor = documento.Emisor.NombreComercial;
                ventaInfo.NombreLegalEmisor = documento.Emisor.NombreLegal;
                ventaInfo.DepartamentoEmisor = documento.Emisor.Departamento;
                ventaInfo.ProvinciaEmisor = documento.Emisor.Provincia;
                ventaInfo.DistritoEmisor = documento.Emisor.Distrito;
                ventaInfo.UbigeoEmisor = documento.Emisor.Ubigeo;
                ventaInfo.DireccionEmisor = documento.Emisor.Direccion;

                ventaInfo.TipoDocumentoReceptor = documento.Receptor.TipoDocumento;
                ventaInfo.NroDocumentoReceptor = documento.Receptor.NroDocumento;
                ventaInfo.NombreComercialReceptor = documento.Receptor.NombreComercial;
                ventaInfo.NombreLegalReceptor = documento.Receptor.NombreLegal;
                ventaInfo.DireccionReceptor = documento.Receptor.Direccion;

                ventaInfo.CodigoRespuesta = enviarDocumentoResponse.CodigoRespuesta;
                ventaInfo.Exito = enviarDocumentoResponse.Exito ? 1 : 0;
                ventaInfo.MensajeError = String.Empty;
                ventaInfo.MensajeRespuesta = enviarDocumentoResponse.MensajeRespuesta;
                ventaInfo.NombreArchivo = enviarDocumentoResponse.NombreArchivo;
                ventaInfo.NroTicket = String.Empty;

                ventaInfo.EstadoId = estadoComprobante;
                ventaInfo.ComprobanteImpreso = String.Empty;

                #region Venta Detalle
                var itemGasolutions = documento.Items.FirstOrDefault();
                ventaInfo.Item = new VentaDetalleGasolutionsInfo
                {
                    Cantidad = itemGasolutions.Cantidad,
                    CodigoItem = itemGasolutions.CodigoItem,
                    UnidadMedida = itemGasolutions.UnidadMedida,
                    Descripcion = itemGasolutions.Descripcion,
                    Id = itemGasolutions.Id,
                    Impuesto = itemGasolutions.Impuesto,
                    PrecioUnitario = itemGasolutions.PrecioUnitario,
                    TipoImpuesto = itemGasolutions.TipoImpuesto,
                    TipoPrecio = itemGasolutions.TipoPrecio,
                    TotalVenta = itemGasolutions.TotalVenta
                };
                #endregion

                var ws = new WS_Gasolutions.Local.ServiceTerminalHostClient();
                // var ventaGasolutionsInfo = new VentaGasolutionsInfo();

                // var z = (VentaGasolutions_InsertarNotaCreditoXmlRequest) notaCreditoXml;

                var z = new VentaGasolutions_InsertarNotaCreditoXmlRequest();
                z.notaCreditoXml = notaCreditoXml;

                resumenFirma = ws.VentaGasolutions_InsertarNotaCreditoXml(z).ToString();

                #endregion
            }
        }
        public string SetDate(string date)
        {
            string setdate = null;
            if (date != null && date != "")
            {
                DateTime dt = DateTime.ParseExact(date, "d/M/yyyy", CultureInfo.InvariantCulture);
                setdate = dt.ToString("dd/MM/yyyy");
            }
            return setdate;
        }

        protected void btnNotaDebito_OnClick(object sender, EventArgs e)
        {
            var data = new EnviarDocumentoResponse();

            DebitNote notaDebito = null;
            string path = @"D:\ASOLUTIONS\Gasolutions\Liquidos\Ejemplos CPE XML\Nota de debito.xml";

            XmlSerializer serializer = new XmlSerializer(typeof(DebitNote));
            StreamReader reader = new StreamReader(path);
            notaDebito = (DebitNote)serializer.Deserialize(reader);
            reader.Close();

            StreamReader readerX = new StreamReader(path);
            var notaDebitoXml = readerX.ReadToEnd();
            readerX.Close();


            #region Documento Electrónico
            var documento = new DocumentoElectronico
            {
                Emisor = new Contribuyente()
                {
                    Departamento = notaDebito.AccountingSupplierParty.Party.PostalAddress.CityName,
                    Direccion = notaDebito.AccountingSupplierParty.Party.PostalAddress.StreetName,
                    Distrito = notaDebito.AccountingSupplierParty.Party.PostalAddress.District,
                    NombreComercial = notaDebito.AccountingSupplierParty.Party.PartyName.Name,
                    NombreLegal = notaDebito.AccountingSupplierParty.Party.PartyName.Name,
                    NroDocumento = notaDebito.AccountingSupplierParty.CustomerAssignedAccountID,
                    Provincia = notaDebito.AccountingSupplierParty.Party.PostalAddress.CountrySubentity,
                    TipoDocumento = notaDebito.AccountingSupplierParty.AdditionalAccountID,
                    Ubigeo = notaDebito.AccountingSupplierParty.Party.PostalAddress.ID,
                    Urbanizacion = "",
                },
                Receptor = new Contribuyente
                {
                    NroDocumento = notaDebito.AccountingCustomerParty.CustomerAssignedAccountID,
                    TipoDocumento = notaDebito.AccountingCustomerParty.AdditionalAccountID,
                    NombreLegal = notaDebito.AccountingCustomerParty.Party.PartyLegalEntity.RegistrationName,
                    NombreComercial = notaDebito.AccountingCustomerParty.Party.PartyLegalEntity.RegistrationName,
                    Direccion = String.Empty
                },
                IdDocumento = notaDebito.ID,
                FechaEmision = notaDebito.IssueDate.ToString("dd/MM/yyyy"),
                //FechaEmision = SetDate(notaCredito.IssueDate.ToString("dd/MM/yyyy")),
                Moneda = Constantes.MonedaSolesSunat,
                MontoEnLetras = String.Empty, // Herramientas.NumeroALetras.numeroAletras(notaDebito.LegalMonetaryTotal.PayableAmount),
                PlacaVehiculo = String.Empty,
                CalculoIgv = 0.18m,
                CalculoIsc = 0m,
                CalculoDetraccion = 0m,
                TipoDocumento = "08",
                //TotalIgv = notaDebito.TaxTotal.TaxAmount,
                TotalVenta = notaDebito.RequestedMonetaryTotal.PayableAmount,
                Gravadas = notaDebito.AdditionalInformation.AdditionalMonetaryTotal.PayableAmount,
                Discrepancias = new List<Discrepancia>
                    {
                        new Discrepancia
                        {
                            //NroReferencia = "FF11-001",
                            NroReferencia = notaDebito.DiscrepancyResponse.ReferenceID,
                            //Tipo = "01",
                            Tipo = notaDebito.DiscrepancyResponse.ResponseCode,
                            Descripcion = notaDebito.DiscrepancyResponse.Description
                        }
                    },
                Relacionados = new List<DocumentoRelacionado>
                    {
                        new DocumentoRelacionado
                        {
                            //NroDocumento = "FF11-001",
                            NroDocumento = notaDebito.BillingReference.InvoiceDocumentReference.ID,
                            //TipoDocumento = "01"
                            TipoDocumento = notaDebito.BillingReference.InvoiceDocumentReference.DocumentTypeCode,
                        }
                    }
            };
            var items = new List<DetalleDocumento>();
            var cont = 0;
            foreach (var vd in notaDebito.DebitNoteLine)
            {
                cont++;
                var item = new DetalleDocumento
                {
                    Id = cont,
                    Cantidad = vd.DebitedQuantity,
                    PrecioReferencial = vd.PricingReference.AlternativeConditionPrice.PriceAmount,
                    PrecioUnitario = vd.Price.PriceAmount,
                    TipoPrecio = vd.PricingReference.AlternativeConditionPrice.PriceTypeCode,
                    CodigoItem = String.Empty,
                    Descripcion = vd.Item.Description,
                    UnidadMedida = vd.UnitCode,
                    Impuesto = vd.TaxTotal.TaxAmount,
                    TipoImpuesto = "10", // Gravada
                    TotalVenta = vd.LineExtensionAmount,
                    Suma = vd.PricingReference.AlternativeConditionPrice.PriceAmount
                };
                items.Add(item);
            }
            documento.Items = items;
            #endregion


            var documentoResponse = RestHelper<DocumentoElectronico, DocumentoResponse>.Execute("GenerarNotaDebito", documento);

            if (!documentoResponse.Exito)
            {
                throw new InvalidOperationException(documentoResponse.MensajeError);
            }



            var respuestaSunatInfo = new RespuestaSunatInfo();
            var firmaInfo = new RespuestaSunatInfo().Firma;
            var respuestaInfo = new RespuestaSunatInfo().Respuesta;



            // Firmado del Documento.
            string rutaCertificado = HostingEnvironment.MapPath("~/Archivos/Facturacion/certificado.pfx");
            var firmado = new FirmadoRequest
            {
                TramaXmlSinFirma = documentoResponse.TramaXmlSinFirma,
                CertificadoDigital = Convert.ToBase64String(File.ReadAllBytes(rutaCertificado)),
                PasswordCertificado = "9dGxdmm5JHKwKsXc",
                UnSoloNodoExtension = false
            };

            var responseFirma = RestHelper<FirmadoRequest, FirmadoResponse>.Execute("Firmar", firmado);

            if (!responseFirma.Exito)
            {
                throw new InvalidOperationException(responseFirma.MensajeError);
            }

            var documentoRequest = new EnviarDocumentoRequest
            {
                Ruc = documento.Emisor.NroDocumento,
                UsuarioSol = "FACTRC18",
                ClaveSol = "rcfact2018",
                EndPointUrl = UrlSunat,
                IdDocumento = documento.IdDocumento,
                TipoDocumento = documento.TipoDocumento,
                TramaXmlFirmado = responseFirma.TramaXmlFirmado
            };

            var enviarDocumentoResponse = RestHelper<EnviarDocumentoRequest, EnviarDocumentoResponse>.Execute("EnviarDocumento", documentoRequest);

            var estadoComprobante = Constantes.EstadoComprobanteRechazado;

            var resumenFirma = String.Empty;

            if (!enviarDocumentoResponse.Exito)
            {
                estadoComprobante = Constantes.EstadoComprobanteRechazado;
                //throw new InvalidOperationException(enviarDocumentoResponse.MensajeError);
            }
            else
            {
                estadoComprobante = Constantes.EstadoComprobanteAnulado;

                estadoComprobante = Constantes.EstadoComprobanteAceptado;

                string rutaCdr = HostingEnvironment.MapPath("~/Archivos/Facturacion/CDR/Cliente/" + documento.Receptor.NroDocumento);
                if (!Directory.Exists(rutaCdr))
                {
                    Directory.CreateDirectory(rutaCdr);
                }
                File.WriteAllBytes(rutaCdr + "/" + enviarDocumentoResponse.NombreArchivo + ".zip", Convert.FromBase64String(enviarDocumentoResponse.TramaZipCdr));

                respuestaInfo.CodigoRespuesta = enviarDocumentoResponse.CodigoRespuesta;
                respuestaInfo.Exito = enviarDocumentoResponse.Exito;
                respuestaInfo.MensajeError = enviarDocumentoResponse.MensajeError;
                respuestaInfo.MensajeRespuesta = enviarDocumentoResponse.MensajeRespuesta;
                respuestaInfo.NombreArchivo = enviarDocumentoResponse.NombreArchivo;
                respuestaInfo.Pila = enviarDocumentoResponse.Pila;
                respuestaInfo.TramaZipCdr = enviarDocumentoResponse.TramaZipCdr;

                respuestaSunatInfo.Respuesta = respuestaInfo;

                #region Envio Servidor APUFact
                var ventaInfo = new VentaGasolutionsInfo();

                ventaInfo.FechaEmision = DateTime.ParseExact(documento.FechaEmision, "dd/MM/yyyy", null);
                ventaInfo.Gravadas = documento.Gravadas;
                ventaInfo.IdDocumento = documento.IdDocumento;
                ventaInfo.CalculoIgv = documento.CalculoIgv;
                ventaInfo.MonedaId = documento.Moneda;
                ventaInfo.MontoEnLetras = documento.MontoEnLetras;
                ventaInfo.PlacaVehiculo = documento.PlacaVehiculo;
                ventaInfo.TipoDocumento = documento.TipoDocumento;
                ventaInfo.TotalIgv = documento.TotalIgv;
                ventaInfo.TotalVenta = documento.TotalVenta;

                ventaInfo.TipoDocumentoEmisor = documento.Emisor.TipoDocumento;
                ventaInfo.NroDocumentoEmisor = documento.Emisor.NroDocumento;
                ventaInfo.NombreComercialEmisor = documento.Emisor.NombreComercial;
                ventaInfo.NombreLegalEmisor = documento.Emisor.NombreLegal;
                ventaInfo.DepartamentoEmisor = documento.Emisor.Departamento;
                ventaInfo.ProvinciaEmisor = documento.Emisor.Provincia;
                ventaInfo.DistritoEmisor = documento.Emisor.Distrito;
                ventaInfo.UbigeoEmisor = documento.Emisor.Ubigeo;
                ventaInfo.DireccionEmisor = documento.Emisor.Direccion;

                ventaInfo.TipoDocumentoReceptor = documento.Receptor.TipoDocumento;
                ventaInfo.NroDocumentoReceptor = documento.Receptor.NroDocumento;
                ventaInfo.NombreComercialReceptor = documento.Receptor.NombreComercial;
                ventaInfo.NombreLegalReceptor = documento.Receptor.NombreLegal;
                ventaInfo.DireccionReceptor = documento.Receptor.Direccion;

                ventaInfo.CodigoRespuesta = enviarDocumentoResponse.CodigoRespuesta;
                ventaInfo.Exito = enviarDocumentoResponse.Exito ? 1 : 0;
                ventaInfo.MensajeError = String.Empty;
                ventaInfo.MensajeRespuesta = enviarDocumentoResponse.MensajeRespuesta;
                ventaInfo.NombreArchivo = enviarDocumentoResponse.NombreArchivo;
                ventaInfo.NroTicket = String.Empty;

                ventaInfo.EstadoId = estadoComprobante;
                ventaInfo.ComprobanteImpreso = String.Empty;

                //var ws = new WS_Gasolutions.Local.GasolutionsClient();
                var ventaGasolutionsInfo = new VentaGasolutionsInfo();

                //resumenFirma = ws.VentaGasolutions_InsertarNotaDebitoXml(notaDebitoXml);
                #endregion
            }
        }
    }
}





































