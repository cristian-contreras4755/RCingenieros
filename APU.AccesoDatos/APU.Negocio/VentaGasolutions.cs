using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web.Hosting;
using System.Xml.Serialization;
using APU.AccesoDatos;
using APU.Entidad;
using APU.Herramientas;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using OpenInvoicePeru.Comun.Dto.Intercambio;
using OpenInvoicePeru.Comun.Dto.Modelos;
using RestSharp;

namespace APU.Negocio
{
    public class VentaGasolutions
    {
        private readonly VentaGasolutionsAccesoDatos _ventaAccesoDatos;
        public VentaGasolutions()
        {
            _ventaAccesoDatos = new VentaGasolutionsAccesoDatos();
        }
        #region Venta
        public List<VentaGasolutionsInfo> Listar(int ventaGasolutionsId)
        {
            return _ventaAccesoDatos.Listar(ventaGasolutionsId);
        }
        public List<VentaGasolutionsInfo> ListarPaginado(int ventaId, string numeroDocumento, string tipoComprobanteId, string numeroComprobante, DateTime fechaInicio, DateTime fechaFin, int estadoId, int monedaId, int tamanioPagina, int numeroPagina)
        {
            return _ventaAccesoDatos.ListarPaginado(ventaId, numeroDocumento, tipoComprobanteId, numeroComprobante, fechaInicio, fechaFin, estadoId, monedaId, tamanioPagina, numeroPagina);
        }
        public int Insertar(VentaGasolutionsInfo ventaInfo)
        {
            int ventaId = 0;
            try
            {
                ventaId = _ventaAccesoDatos.Insertar(ventaInfo);

                if (ventaInfo.Item != null)
                {
                    var ventaDetalleInfo = new VentaDetalleGasolutionsInfo
                    {
                        // VentaGasolutionsId = ventaInfo.Item.VentaGasolutionsId,
                        VentaGasolutionsId = ventaId,
                        Id = ventaInfo.Item.Id,
                        Cantidad = ventaInfo.Item.Cantidad,
                        CodigoItem = ventaInfo.Item.CodigoItem,
                        Descripcion = ventaInfo.Item.Descripcion,
                        Impuesto = ventaInfo.Item.Impuesto,
                        PrecioUnitario = ventaInfo.Item.PrecioUnitario,
                        TotalVenta = ventaInfo.Item.TotalVenta,
                        UnidadMedida = ventaInfo.Item.UnidadMedida,
                        TipoImpuesto = ventaInfo.Item.TipoImpuesto,
                        TipoPrecio = ventaInfo.Item.TipoPrecio
                    };
                    _ventaAccesoDatos.InsertarDetalle(ventaDetalleInfo);
                }


            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaLogicaNegocio);
                if (rethrow)
                    throw;
            }
            return ventaId;
        }


        public int InsertarLote(List<VentaGasolutionsInfo> ventaListaInfo)
        {
            int resultado = 0;
            try
            {
                resultado = _ventaAccesoDatos.InsertarLote(ventaListaInfo);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaLogicaNegocio);
                if (rethrow)
                    throw;
            }
            return resultado;
        }
        public int Actualizar(VentaInfo ventaInfo)
        {
            int resultado = 0;
            try
            {
                resultado = _ventaAccesoDatos.Actualizar(ventaInfo);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaLogicaNegocio);
                if (rethrow)
                    throw;
            }
            return resultado;
        }
        public int Eliminar(int ventaId)
        {
            int resultado = 0;
            try
            {
                resultado = _ventaAccesoDatos.Eliminar(ventaId);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaLogicaNegocio);
                if (rethrow)
                    throw;
            }
            return resultado;
        }

        private static readonly string BaseUrl = ConfigurationManager.AppSettings["APU.Sunat.BaseUrl"];
        private static readonly string UrlSunat = ConfigurationManager.AppSettings["APU.Sunat.UrlSunat"];
        private static readonly string FormatoFecha = ConfigurationManager.AppSettings["APU.Sunat.FormatoFecha"];
        public static DocumentoElectronico _documento;
        public string Insertar(string ventaXml)
        {
          // ventaXml = "<Invoice><ID>B001-00012059</ID><IssueDate> 2018 - 05 - 16 </ IssueDate >   < InvoiceTypeCode > 03 </ InvoiceTypeCode >   < DocumentCurrencyCode > PEN </ DocumentCurrencyCode >   < AccountingSupplierParty >   < CustomerAssignedAccountID > 20565676742 </ CustomerAssignedAccountID >   < AdditionalAccountID > 6 </ AdditionalAccountID >   < Party >    < PartyName >     < Name > Procesadora y Operadora de Combustibles del Peru S.A.C.</ Name >    </ PartyName >    < PostalAddress >     < ID > 150126 </ ID >     < StreetName > Lima </ StreetName >     < CityName > Lima </ CityName >     < CountrySubentity > Lima </ CountrySubentity >     < District > Lima </ District >     < Country >      < IdentificationCode > PE </ IdentificationCode >     </ Country >    </ PostalAddress >    < PartyLegalEntity >     < RegistrationName > Procesadora y Operadora de Combustibles del Peru S.A.C.</ RegistrationName >    </ PartyLegalEntity >   </ Party >  </ AccountingSupplierParty >  < AccountingCustomerParty >   < CustomerAssignedAccountID > 00000000 </ CustomerAssignedAccountID >   < AdditionalAccountID > 0 </ AdditionalAccountID >   < Party >   < PostalAddress >         < StreetName > Sin Direccion </ StreetName >        </ PostalAddress >    < PartyLegalEntity >     < RegistrationName > Clientes Varios </ RegistrationName >    </ PartyLegalEntity >   </ Party >  </ AccountingCustomerParty >  < TaxTotal >   < TaxAmount > 1.53 </ TaxAmount >   < TaxSubtotal >    < TaxAmount > 1.53 </ TaxAmount >    < TaxCategory >     < TaxScheme >      < ID > 1000 </ ID >      < Name > IGV </ Name >      < TaxTypeCode > VAT </ TaxTypeCode >     </ TaxScheme >    </ TaxCategory >   </ TaxSubtotal >  </ TaxTotal >  < LegalMonetaryTotal >   < PayableAmount > 10.00 </ PayableAmount >     </ LegalMonetaryTotal >  < InvoiceLine >   < ID > 1 </ ID >   < InvoicedQuantity > 0.890 </ InvoicedQuantity >    < UnitCode > GLL </ UnitCode >   < LineExtensionAmount > 10.00 </ LineExtensionAmount >    < PricingReference >    < AlternativeConditionPrice >     < PriceAmount > 11.24 </ PriceAmount >     < PriceTypeCode > 01 </ PriceTypeCode >    </ AlternativeConditionPrice >   </ PricingReference >   < TaxTotal >    < TaxAmount > 1.53 </ TaxAmount >      < TaxSubtotal >      < TaxAmount > 1.53 </ TaxAmount >     < TaxCategory >      < TaxExemptionReasonCode > 10 </ TaxExemptionReasonCode >       < TaxScheme >       < ID > 1000 </ ID >        < Name > IGV </ Name >        < TaxTypeCode > VAT </ TaxTypeCode >      </ TaxScheme >     </ TaxCategory >    </ TaxSubtotal >   </ TaxTotal >   < Item >    < Description > DB5 </ Description >    < SellersItemIdentification >     < ID > 3 </ ID >     </ SellersItemIdentification >   </ Item >   < Price >    < PriceAmount > 9.52 </ PriceAmount >    </ Price >  </ InvoiceLine >   < AdditionalInformation >   < AdditionalMonetaryTotal >    < ID > 1005 </ ID >     < PayableAmount > 8.47 </ PayableAmount >    < ID > 2005 </ ID >     < PayableAmount currencyID='"PEN"' >0.00</PayableAmount></AdditionalMonetaryTotal><SUNATCosts><RoadTransport>              < LicensePlateID ></ LicensePlateID ></ RoadTransport ></ SUNATCosts ></ AdditionalInformation >< Adjuntos ><Libre1></Libre1>   <Libre2></Libre2 >   < Libre3 ></ Libre3 >   < Libre4 ></ Libre4 >   <Libre5></Libre5>   < Libre6 ></ Libre6 >   < Observacion ></ Observacion >   < MontoPalabras > DIEZ Y  00 / 100 SOLES </ MontoPalabras >   < MailReceptor ></ MailReceptor >   < Impresora ></ Impresora >   < Copias > 1 </ Copias >  </ Adjuntos > </Invoice> ";

            int resultado = 0;
            var resumenFirma = String.Empty;
            try
            {


                var serializer = new XmlSerializer(typeof(Invoice));
                Invoice comprobante;

                using (TextReader reader = new StringReader(ventaXml))
                {
                    comprobante = (Invoice)serializer.Deserialize(reader);
                }


                _documento = new DocumentoElectronico();

                #region Documento
                _documento.CalculoDetraccion = 0;
                _documento.CalculoIgv = Decimal.Divide(18, 100);
                _documento.CalculoIsc = 0;

                #region Emisor
                var emisor = new DocumentoElectronico().Emisor;
                emisor.Departamento = comprobante.AccountingSupplierParty.Party.PostalAddress.CountrySubentity;
                emisor.Direccion = comprobante.AccountingSupplierParty.Party.PostalAddress.StreetName;
                emisor.Distrito = comprobante.AccountingSupplierParty.Party.PostalAddress.District;
                emisor.NombreComercial = comprobante.AccountingSupplierParty.Party.PartyName.Name;
                emisor.NombreLegal = comprobante.AccountingSupplierParty.Party.PartyLegalEntity.RegistrationName;
                emisor.NroDocumento = comprobante.AccountingSupplierParty.CustomerAssignedAccountID; // "10421895452";
                emisor.Provincia = comprobante.AccountingSupplierParty.Party.PostalAddress.CityName;
                emisor.TipoDocumento = comprobante.AccountingSupplierParty.AdditionalAccountID;
                emisor.Ubigeo = comprobante.AccountingSupplierParty.Party.PostalAddress.ID;
                emisor.Urbanizacion = String.Empty;
                _documento.Emisor = emisor;
                #endregion

                _documento.Exoneradas = 0;
                _documento.FechaEmision = comprobante.IssueDate.ToString("dd/MM/yyyy");
                _documento.Gratuitas = 0;
                _documento.Gravadas = comprobante.LegalMonetaryTotal.PayableAmount;
                _documento.IdDocumento = comprobante.ID;
                _documento.Inafectas = 0;

                _documento.PlacaVehiculo = comprobante.AdditionalInformation.SUNATCosts.RoadTransport.LicensePlateID;

                #region Items
                //var itemId = 1;
                foreach (var vd in comprobante.InvoiceLine)
                {
                    var item = new DetalleDocumento();
                    item.Cantidad = vd.InvoicedQuantity;
                    item.CodigoItem = vd.Item.SellersItemIdentification.ID;
                    item.Descripcion = vd.Item.Description;
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

                _documento.Moneda = comprobante.DocumentCurrencyCode;
                _documento.MontoAnticipo = 0;
                _documento.MontoDetraccion = 0;
                _documento.MontoEnLetras = NumeroALetras.numeroAletras(comprobante.LegalMonetaryTotal.PayableAmount);
                _documento.MontoPercepcion = 0;

                #region Receptor
                var receptor = new DocumentoElectronico().Receptor;
                receptor.NombreComercial = comprobante.AccountingCustomerParty.Party.PartyLegalEntity.RegistrationName;
                receptor.NombreLegal = comprobante.AccountingCustomerParty.Party.PartyLegalEntity.RegistrationName;
                receptor.NroDocumento = comprobante.AccountingCustomerParty.CustomerAssignedAccountID;
                receptor.TipoDocumento = comprobante.AccountingCustomerParty.AdditionalAccountID;
                receptor.Direccion = String.Empty;
                _documento.Receptor = receptor;
                #endregion

                _documento.TipoDocumento = comprobante.InvoiceTypeCode;
                _documento.TotalIgv = comprobante.TaxTotal.TaxAmount;
                _documento.TotalIsc = 0;
                _documento.TotalOtrosTributos = 0;
                _documento.TotalVenta = comprobante.LegalMonetaryTotal.PayableAmount;

                _documento.DescuentoGlobal = ((InvoiceAdditionalInformationAdditionalMonetaryTotalPayableAmount) comprobante.AdditionalInformation.AdditionalMonetaryTotal.Items[3]).Value;
                #endregion

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
                var rutaXml = HostingEnvironment.MapPath("~/Archivos/Facturacion/XML/Cliente/" + _documento.Receptor.NroDocumento);
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
                string rutaXmlFirmado = HostingEnvironment.MapPath("~/Archivos/Facturacion/XML/Cliente/" + _documento.Receptor.NroDocumento);
                if (!Directory.Exists(rutaXmlFirmado))
                {
                    Directory.CreateDirectory(rutaXmlFirmado);
                }
                File.WriteAllBytes(rutaXmlFirmado + "/" + _documento.IdDocumento + "_Firmado.xml", Convert.FromBase64String(responseFirma.Data.TramaXmlFirmado));

                resumenFirma = responseFirma.Data.ResumenFirma;
                #endregion






                #region Envío servidor APUFact
                //var ventaInfo = new VentaGasolutionsInfo();
                var ventaInfo = new WS_Gasolutions.VentaGasolutionsInfo();

                ventaInfo._fechaEmision = DateTime.ParseExact(_documento.FechaEmision, "dd/MM/yyyy", null);
                ventaInfo._gravadas = _documento.Gravadas;
                ventaInfo._idDocumento = _documento.IdDocumento;
                ventaInfo._calculoIgv = _documento.CalculoIgv;
                ventaInfo._monedaId = _documento.Moneda;
                ventaInfo._montoEnLetras = _documento.MontoEnLetras;
                ventaInfo._placaVehiculo = _documento.PlacaVehiculo;
                ventaInfo._tipoDocumento = _documento.TipoDocumento;
                ventaInfo._totalIgv = _documento.TotalIgv;
                ventaInfo._totalVenta = _documento.TotalVenta;

                ventaInfo._descuento = _documento.DescuentoGlobal;

                ventaInfo._tipoDocumentoEmisor = _documento.Emisor.TipoDocumento;
                ventaInfo._nroDocumentoEmisor = _documento.Emisor.NroDocumento;
                ventaInfo._nombreComercialEmisor = _documento.Emisor.NombreComercial;
                ventaInfo._nombreLegalEmisor = _documento.Emisor.NombreLegal;
                ventaInfo._departamentoEmisor = _documento.Emisor.Departamento;
                ventaInfo._provinciaEmisor = _documento.Emisor.Provincia;
                ventaInfo._distritoEmisor = _documento.Emisor.Distrito;
                ventaInfo._ubigeoEmisor = _documento.Emisor.Ubigeo;
                ventaInfo._direccionEmisor = _documento.Emisor.Direccion;

                ventaInfo._tipoDocumentoReceptor = _documento.Receptor.TipoDocumento;
                ventaInfo._nroDocumentoReceptor = _documento.Receptor.NroDocumento;
                ventaInfo._nombreComercialReceptor = _documento.Receptor.NombreComercial;
                ventaInfo._nombreLegalReceptor = _documento.Receptor.NombreLegal;
                ventaInfo._direccionReceptor = _documento.Receptor.Direccion;

                ventaInfo._codigoRespuesta = String.Empty;
                ventaInfo._exito = 0;
                ventaInfo._mensajeError = String.Empty;
                ventaInfo._mensajeRespuesta = String.Empty;
                ventaInfo._nombreArchivo = String.Empty;
                ventaInfo._nroTicket = String.Empty;
                ventaInfo._estadoId = 0;

                ventaInfo._comprobanteImpreso = String.Empty;

                //var itemInfo = new Entidad.VentaDetalleGasolutionsInfo();
                var itemInfo = new WS_Gasolutions.VentaDetalleGasolutionsInfo();

                foreach (var vd in comprobante.InvoiceLine)
                {
                    itemInfo._cantidad = vd.InvoicedQuantity;
                    itemInfo._codigoItem = vd.Item.SellersItemIdentification.ID;
                    itemInfo._descripcion = vd.Item.Description;
                    itemInfo._impuesto = vd.TaxTotal.TaxAmount;
                    itemInfo._precioUnitario = vd.Price.PriceAmount;
                    itemInfo._totalVenta = vd.LineExtensionAmount;
                    itemInfo._unidadMedida = vd.UnitCode;
                    //itemInfo.Descuento = 0;
                    itemInfo._id = vd.ID;
                    //itemInfo.ImpuestoSelectivo = 0;
                    //itemInfo.OtroImpuesto = 0;
                    //itemInfo.PrecioReferencial = 0;
                    itemInfo._tipoImpuesto = "10";
                    itemInfo._tipoPrecio = "01";
                }

                ventaInfo.Itemk__BackingField = itemInfo;

                // new VentaGasolutions().Insertar(ventaInfo);
                // resultado = _ventaAccesoDatos.Insertar(ventaInfo);


                var ws = new WS_Gasolutions.GasolutionsClient();
                //var ventaGasolutionsInfo = new VentaGasolutionsInfo();

                var ventaId = ws.VentaGasolutions_Insertar(ventaInfo);


                #endregion
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaLogicaNegocio);
                if (rethrow)
                    throw;
            }
            return resumenFirma;
        }










        public static DocumentoElectronico documento;
        public string InsertarNotaCredito(string notaCreditoXml)
        {
            int resultado = 0;
            var resumenFirma = String.Empty;
            try
            {
                var serializer = new XmlSerializer(typeof(CreditNote));
                CreditNote comprobante;

                using (TextReader reader = new StringReader(notaCreditoXml))
                {
                    comprobante = (CreditNote)serializer.Deserialize(reader);
                }

                #region Documento Electrónico
                var documento = new DocumentoElectronico
                {
                    Emisor = new Contribuyente()
                    {
                        Departamento = comprobante.AccountingSupplierParty.Party.PostalAddress.CityName,
                        Direccion = comprobante.AccountingSupplierParty.Party.PostalAddress.StreetName,
                        Distrito = comprobante.AccountingSupplierParty.Party.PostalAddress.District,
                        NombreComercial = comprobante.AccountingSupplierParty.Party.PartyName.Name,
                        NombreLegal = comprobante.AccountingSupplierParty.Party.PartyName.Name,
                        NroDocumento = comprobante.AccountingSupplierParty.CustomerAssignedAccountID,
                        Provincia = comprobante.AccountingSupplierParty.Party.PostalAddress.CountrySubentity,
                        TipoDocumento = "6",
                        Ubigeo = comprobante.AccountingSupplierParty.Party.PostalAddress.ID,
                        Urbanizacion = "",
                    },
                    Receptor = new Contribuyente
                    {
                        NroDocumento = comprobante.AccountingCustomerParty.CustomerAssignedAccountID,
                        TipoDocumento = comprobante.AccountingCustomerParty.AdditionalAccountID,
                        NombreLegal = comprobante.AccountingCustomerParty.Party.PartyLegalEntity.RegistrationName,
                        NombreComercial = comprobante.AccountingCustomerParty.Party.PartyLegalEntity.RegistrationName,
                        Direccion = String.Empty
                    },
                    IdDocumento = comprobante.ID,
                    FechaEmision = comprobante.IssueDate.ToString("dd/MM/yyyy"),
                    Moneda = Constantes.MonedaSolesSunat,
                    MontoEnLetras = Herramientas.NumeroALetras.numeroAletras(comprobante.LegalMonetaryTotal.PayableAmount),
                    PlacaVehiculo = String.Empty,
                    CalculoIgv = 0.18m,
                    CalculoIsc = 0m,
                    CalculoDetraccion = 0m,
                    TipoDocumento = "07",
                    TotalIgv = comprobante.TaxTotal.TaxAmount,
                    TotalVenta = comprobante.LegalMonetaryTotal.PayableAmount,
                    Gravadas = comprobante.AdditionalInformation.AdditionalMonetaryTotal.PayableAmount,
                    Discrepancias = new List<Discrepancia>
                    {
                        new Discrepancia
                        {
                            //NroReferencia = "FF11-001",
                            NroReferencia = comprobante.DiscrepancyResponse.ReferenceID,
                            //Tipo = "01",
                            Tipo = comprobante.DiscrepancyResponse.ResponseCode,
                            Descripcion = comprobante.DiscrepancyResponse.Description
                        }
                    },
                    Relacionados = new List<DocumentoRelacionado>
                    {
                        new DocumentoRelacionado
                        {
                            //NroDocumento = "FF11-001",
                            NroDocumento = comprobante.BillingReference.InvoiceDocumentReference.ID,
                            //TipoDocumento = "01"
                            TipoDocumento = comprobante.BillingReference.InvoiceDocumentReference.DocumentTypeCode,
                        }
                    }
                };
                var items = new List<DetalleDocumento>();
                var cont = 0;
                foreach (var vd in comprobante.CreditNoteLine)
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
                    throw new InvalidOperationException(documentoResponse.MensajeError + " - " + comprobante.IssueDate.ToString("dd/MM/yyyy") + " - " + documento.FechaEmision);
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

                resumenFirma = responseFirma.ResumenFirma;

                #region Envio Servidor APUFact
                //var ventaInfo = new VentaGasolutionsInfo();
                var ventaInfo = new WS_Gasolutions.VentaGasolutionsInfo();

                ventaInfo._fechaEmision = DateTime.ParseExact(documento.FechaEmision, "dd/MM/yyyy", null);
                ventaInfo._gravadas = documento.Gravadas;
                ventaInfo._idDocumento = documento.IdDocumento;
                ventaInfo._calculoIgv = documento.CalculoIgv;
                ventaInfo._monedaId = documento.Moneda;
                ventaInfo._montoEnLetras = documento.MontoEnLetras;
                ventaInfo._placaVehiculo = documento.PlacaVehiculo;
                ventaInfo._tipoDocumento = documento.TipoDocumento;
                ventaInfo._totalIgv = documento.TotalIgv;
                ventaInfo._totalVenta = documento.TotalVenta;

                ventaInfo._tipoDocumentoEmisor = documento.Emisor.TipoDocumento;
                ventaInfo._nroDocumentoEmisor = documento.Emisor.NroDocumento;
                ventaInfo._nombreComercialEmisor = documento.Emisor.NombreComercial;
                ventaInfo._nombreLegalEmisor = documento.Emisor.NombreLegal;
                ventaInfo._departamentoEmisor = documento.Emisor.Departamento;
                ventaInfo._provinciaEmisor = documento.Emisor.Provincia;
                ventaInfo._distritoEmisor = documento.Emisor.Distrito;
                ventaInfo._ubigeoEmisor = documento.Emisor.Ubigeo;
                ventaInfo._direccionEmisor = documento.Emisor.Direccion;

                ventaInfo._tipoDocumentoReceptor = documento.Receptor.TipoDocumento;
                ventaInfo._nroDocumentoReceptor = documento.Receptor.NroDocumento;
                ventaInfo._nombreComercialReceptor = documento.Receptor.NombreComercial;
                ventaInfo._nombreLegalReceptor = documento.Receptor.NombreLegal;
                ventaInfo._direccionReceptor = documento.Receptor.Direccion;

                ventaInfo._codigoRespuesta = String.Empty;
                ventaInfo._exito = 0;
                ventaInfo._mensajeError = String.Empty;
                ventaInfo._mensajeRespuesta = String.Empty;
                ventaInfo._nombreArchivo = String.Empty;
                ventaInfo._nroTicket = String.Empty;

                ventaInfo._estadoId = 0;
                ventaInfo._comprobanteImpreso = String.Empty;



                // resultado = _ventaAccesoDatos.Insertar(ventaInfo);

                var ws = new WS_Gasolutions.GasolutionsClient();
                var ventaId = ws.VentaGasolutions_Insertar(ventaInfo);
                #endregion
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaLogicaNegocio);
                if (rethrow)
                    throw;
            }
            return resumenFirma;
        }

        public string InsertarNotaDebito(string notaDebitoXml)
        {
            int resultado = 0;
            var resumenFirma = String.Empty;
            try
            {
                var serializer = new XmlSerializer(typeof(DebitNote));
                DebitNote comprobante;

                using (TextReader reader = new StringReader(notaDebitoXml))
                {
                    comprobante = (DebitNote)serializer.Deserialize(reader);
                }

                #region Documento Electrónico
                var documento = new DocumentoElectronico
                {
                    Emisor = new Contribuyente()
                    {
                        Departamento = comprobante.AccountingSupplierParty.Party.PostalAddress.CityName,
                        Direccion = comprobante.AccountingSupplierParty.Party.PostalAddress.StreetName,
                        Distrito = comprobante.AccountingSupplierParty.Party.PostalAddress.District,
                        NombreComercial = comprobante.AccountingSupplierParty.Party.PartyName.Name,
                        NombreLegal = comprobante.AccountingSupplierParty.Party.PartyName.Name,
                        NroDocumento = comprobante.AccountingSupplierParty.CustomerAssignedAccountID,
                        Provincia = comprobante.AccountingSupplierParty.Party.PostalAddress.CountrySubentity,
                        TipoDocumento = comprobante.AccountingSupplierParty.AdditionalAccountID,
                        Ubigeo = comprobante.AccountingSupplierParty.Party.PostalAddress.ID,
                        Urbanizacion = "",
                    },
                    Receptor = new Contribuyente
                    {
                        NroDocumento = comprobante.AccountingCustomerParty.CustomerAssignedAccountID,
                        TipoDocumento = comprobante.AccountingCustomerParty.AdditionalAccountID,
                        NombreLegal = comprobante.AccountingCustomerParty.Party.PartyLegalEntity.RegistrationName,
                        NombreComercial = comprobante.AccountingCustomerParty.Party.PartyLegalEntity.RegistrationName,
                        Direccion = String.Empty
                    },
                    IdDocumento = comprobante.ID,
                    FechaEmision = comprobante.IssueDate.ToString("dd/MM/yyyy"),
                    Moneda = Constantes.MonedaSolesSunat,
                    //MontoEnLetras = Herramientas.NumeroALetras.numeroAletras(comprobante.LegalMonetaryTotal.PayableAmount),
                    MontoEnLetras = String.Empty, // Herramientas.NumeroALetras.numeroAletras(notaDebito.LegalMonetaryTotal.PayableAmount),
                    PlacaVehiculo = String.Empty,
                    CalculoIgv = 0.18m,
                    CalculoIsc = 0m,
                    CalculoDetraccion = 0m,
                    TipoDocumento = "07",
                    //TotalIgv = notaDebito.TaxTotal.TaxAmount,
                    TotalVenta = comprobante.RequestedMonetaryTotal.PayableAmount,
                    Gravadas = comprobante.AdditionalInformation.AdditionalMonetaryTotal.PayableAmount,
                    Discrepancias = new List<Discrepancia>
                    {
                        new Discrepancia
                        {
                            //NroReferencia = "FF11-001",
                            NroReferencia = comprobante.DiscrepancyResponse.ReferenceID,
                            //Tipo = "01",
                            Tipo = comprobante.DiscrepancyResponse.ResponseCode,
                            Descripcion = comprobante.DiscrepancyResponse.Description
                        }
                    },
                    Relacionados = new List<DocumentoRelacionado>
                    {
                        new DocumentoRelacionado
                        {
                            //NroDocumento = "FF11-001",
                            NroDocumento = comprobante.BillingReference.InvoiceDocumentReference.ID,
                            //TipoDocumento = "01"
                            TipoDocumento = comprobante.BillingReference.InvoiceDocumentReference.DocumentTypeCode
                        }
                    }
                };
                var items = new List<DetalleDocumento>();
                var cont = 0;
                foreach (var vd in comprobante.DebitNoteLine)
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


                var documentoResponse = RestHelper<DocumentoElectronico, DocumentoResponse>.Execute("GenerarNotaCredito", documento);

                if (!documentoResponse.Exito)
                {
                    throw new InvalidOperationException(documentoResponse.MensajeError + " - " + comprobante.IssueDate.ToString("dd/MM/yyyy") + " - " + documento.FechaEmision);
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

                resumenFirma = responseFirma.ResumenFirma;

                #region Envio Servidor APUFact
                //var ventaInfo = new VentaGasolutionsInfo();
                var ventaInfo = new WS_Gasolutions.VentaGasolutionsInfo();

                ventaInfo._fechaEmision = DateTime.ParseExact(documento.FechaEmision, "dd/MM/yyyy", null);
                ventaInfo._gravadas = documento.Gravadas;
                ventaInfo._idDocumento = documento.IdDocumento;
                ventaInfo._calculoIgv = documento.CalculoIgv;
                ventaInfo._monedaId = documento.Moneda;
                ventaInfo._montoEnLetras = documento.MontoEnLetras;
                ventaInfo._placaVehiculo = documento.PlacaVehiculo;
                ventaInfo._tipoDocumento = documento.TipoDocumento;
                ventaInfo._totalIgv = documento.TotalIgv;
                ventaInfo._totalVenta = documento.TotalVenta;

                ventaInfo._tipoDocumentoEmisor = documento.Emisor.TipoDocumento;
                ventaInfo._nroDocumentoEmisor = documento.Emisor.NroDocumento;
                ventaInfo._nombreComercialEmisor = documento.Emisor.NombreComercial;
                ventaInfo._nombreLegalEmisor = documento.Emisor.NombreLegal;
                ventaInfo._departamentoEmisor = documento.Emisor.Departamento;
                ventaInfo._provinciaEmisor = documento.Emisor.Provincia;
                ventaInfo._distritoEmisor = documento.Emisor.Distrito;
                ventaInfo._ubigeoEmisor = documento.Emisor.Ubigeo;
                ventaInfo._direccionEmisor = documento.Emisor.Direccion;

                ventaInfo._tipoDocumentoReceptor = documento.Receptor.TipoDocumento;
                ventaInfo._nroDocumentoReceptor = documento.Receptor.NroDocumento;
                ventaInfo._nombreComercialReceptor = documento.Receptor.NombreComercial;
                ventaInfo._nombreLegalReceptor = documento.Receptor.NombreLegal;
                ventaInfo._direccionReceptor = documento.Receptor.Direccion;

                ventaInfo._codigoRespuesta = String.Empty;
                ventaInfo._exito = 0;
                ventaInfo._mensajeError = String.Empty;
                ventaInfo._mensajeRespuesta = String.Empty;
                ventaInfo._nombreArchivo = String.Empty;
                ventaInfo._nroTicket = String.Empty;

                ventaInfo._estadoId = 0;
                ventaInfo._comprobanteImpreso = String.Empty;

                // resultado = _ventaAccesoDatos.Insertar(ventaInfo);
                var ws = new WS_Gasolutions.GasolutionsClient();
                var ventaId = ws.VentaGasolutions_Insertar(ventaInfo);
                #endregion
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaLogicaNegocio);
                if (rethrow)
                    throw;
            }
            return resumenFirma;
        }
        #endregion

        #region Venta Detalle
        public List<VentaDetalleInfo> ListarDetalle(int ventaId, int ventaDetalleId)
        {
            return _ventaAccesoDatos.ListarDetalle(ventaId, ventaDetalleId);
        }
        public List<VentaDetalleInfo> ListarDetallePaginado(int ventaId, int ventaDetalleId, int tamanioPagina, int numeroPagina)
        {
            return _ventaAccesoDatos.ListarDetallePaginado(ventaId, ventaDetalleId, tamanioPagina, numeroPagina);
        }
        public int InsertarDetalle(VentaDetalleInfo ventaDetalleInfo)
        {
            int resultado = 0;
            try
            {
                resultado = _ventaAccesoDatos.InsertarDetalle(ventaDetalleInfo);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaLogicaNegocio);
                if (rethrow)
                    throw;
            }
            return resultado;
        }
        public int ActualizarDetalle(VentaDetalleInfo ventaDetalleInfo)
        {
            int resultado = 0;
            try
            {
                resultado = _ventaAccesoDatos.ActualizarDetalle(ventaDetalleInfo);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaLogicaNegocio);
                if (rethrow)
                    throw;
            }
            return resultado;
        }
        public int EliminarDetalle(int ventaDetalleId)
        {
            int resultado = 0;
            try
            {
                resultado = _ventaAccesoDatos.EliminarDetalle(ventaDetalleId);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaLogicaNegocio);
                if (rethrow)
                    throw;
            }
            return resultado;
        }
        #endregion


        #region GNV
        public string InsertarGnv(string ventaXml)
        {
            int resultado = 0;
            var resumenFirma = String.Empty;

            try
            {
                var serializer = new XmlSerializer(typeof(Envelope));
                Envelope comprobante;

                using (var reader = new StringReader(ventaXml))
                {
                    comprobante = (Envelope)serializer.Deserialize(reader);
                }

                _documento = new DocumentoElectronico();

                #region Documento
                _documento.CalculoDetraccion = 0;
                _documento.CalculoIgv = Decimal.Divide(18, 100);
                _documento.CalculoIsc = 0;

                #region Emisor
                var emisor = new DocumentoElectronico().Emisor;
                emisor.Departamento = String.Empty;
                emisor.Direccion = comprobante.Body.GenerarDocumento.oDocumento.direccionAdquiriente;
                emisor.Distrito = String.Empty;
                emisor.NombreComercial = String.Empty;
                emisor.NombreLegal = String.Empty;
                emisor.NroDocumento = comprobante.Body.GenerarDocumento.oDocumento.rucEmisor;
                emisor.Provincia = String.Empty;
                emisor.TipoDocumento = "6";
                emisor.Ubigeo = String.Empty;
                emisor.Urbanizacion = String.Empty;
                _documento.Emisor = emisor;
                #endregion

                _documento.Exoneradas = 0;
                _documento.FechaEmision = comprobante.Body.GenerarDocumento.oDocumento.fechaEmision.ToString("dd/MM/yyyy");
                _documento.Gratuitas = 0;
                _documento.Gravadas = comprobante.Body.GenerarDocumento.oDocumento.operacion.Operacion.monto;
                _documento.IdDocumento = comprobante.Body.GenerarDocumento.oDocumento.numeracion;
                _documento.Inafectas = 0;

                #region Items
                //foreach (var vd in factura.Body.GenerarDocumento.oDocumento.item)
                //{
                var vd = comprobante.Body.GenerarDocumento.oDocumento.item;
                var item = new DetalleDocumento();
                item.Cantidad = comprobante.Body.GenerarDocumento.oDocumento.cantidadItem;
                item.CodigoItem = vd.Item.codigoProducto;
                item.Descripcion = comprobante.Body.GenerarDocumento.oDocumento.descripcionItem;
                //item.Id = itemId;
                //itemId++;
                item.Impuesto = comprobante.Body.GenerarDocumento.oDocumento.tributos.Tributo.valor;
                item.PrecioUnitario = comprobante.Body.GenerarDocumento.oDocumento.precioUnitario;
                item.TotalVenta = comprobante.Body.GenerarDocumento.oDocumento.totalVenta;
                item.UnidadMedida = comprobante.Body.GenerarDocumento.oDocumento.unidadItem;
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

                _documento.Moneda = comprobante.Body.GenerarDocumento.oDocumento.tipoMoneda;
                _documento.MontoAnticipo = 0;
                _documento.MontoDetraccion = 0;
                _documento.MontoEnLetras = NumeroALetras.numeroAletras(comprobante.Body.GenerarDocumento.oDocumento.operacion.Operacion.monto);
                _documento.MontoPercepcion = 0;
                _documento.PlacaVehiculo = String.Empty;

                #region Receptor
                var receptor = new DocumentoElectronico().Receptor;
                receptor.NombreComercial = comprobante.Body.GenerarDocumento.oDocumento.nombreAdquiriente;
                receptor.NombreLegal = comprobante.Body.GenerarDocumento.oDocumento.nombreAdquiriente;
                receptor.NroDocumento = comprobante.Body.GenerarDocumento.oDocumento.numeroDocumentoAdquiriente;
                receptor.TipoDocumento = comprobante.Body.GenerarDocumento.oDocumento.tipoDocumentoAdquiriente;
                receptor.Direccion = comprobante.Body.GenerarDocumento.oDocumento.direccionAdquiriente;
                _documento.Receptor = receptor;
                #endregion

                _documento.TipoDocumento = comprobante.Body.GenerarDocumento.oDocumento.tipoDocumento.PadLeft(2, '0');
                _documento.TotalIgv = comprobante.Body.GenerarDocumento.oDocumento.tributos.Tributo.valor;
                _documento.TotalIsc = 0;
                _documento.TotalOtrosTributos = 0;
                _documento.TotalVenta = comprobante.Body.GenerarDocumento.oDocumento.totalVenta;
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
                var firmaInfo = respuestaSunatInfo.Firma;
                var respuestaInfo = respuestaSunatInfo.Respuesta;

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

                resumenFirma = firmaInfo.ResumenFirma;

                #region Envío servidor APUFact
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

                //ventaInfo.CodigoRespuesta = data.CodigoRespuesta;
                //ventaInfo.Exito = data.Exito ? 1 : 0;
                //ventaInfo.MensajeError = String.Empty;
                //ventaInfo.MensajeRespuesta = data.MensajeRespuesta;
                //ventaInfo.NombreArchivo = data.NombreArchivo;
                //ventaInfo.NroTicket = String.Empty;
                //ventaInfo.EstadoId = estadoComprobante;

                ventaInfo.CodigoRespuesta = String.Empty;
                ventaInfo.Exito = 0;
                ventaInfo.MensajeError = String.Empty;
                ventaInfo.MensajeRespuesta = String.Empty;
                ventaInfo.NombreArchivo = String.Empty;
                ventaInfo.NroTicket = String.Empty;
                ventaInfo.EstadoId = 0;

                ventaInfo.ComprobanteImpreso = String.Empty;

                // new VentaGasolutions().Insertar(ventaInfo);

                resultado = _ventaAccesoDatos.Insertar(ventaInfo);
                #endregion
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaLogicaNegocio);
                if (rethrow)
                    throw;
            }

            return resumenFirma;
        }

        public Facturacion GenerarDocumento(Documento documento)
        {
            var facturacion = new Facturacion();


            _documento = new DocumentoElectronico();

            #region Documento
            _documento.CalculoDetraccion = 0;
            _documento.CalculoIgv = Decimal.Divide(18, 100);
            _documento.CalculoIsc = 0;

            #region Emisor
            var emisor = new DocumentoElectronico().Emisor;
            emisor.Departamento = String.Empty;
            emisor.Direccion = String.Empty;
            emisor.Distrito = String.Empty;
            emisor.NombreComercial = String.Empty;
            emisor.NombreLegal = String.Empty;
            emisor.NroDocumento = documento.RucEmisor;
            emisor.Provincia = String.Empty;
            emisor.TipoDocumento = "6";
            emisor.Ubigeo = String.Empty;
            emisor.Urbanizacion = String.Empty;
            _documento.Emisor = emisor;
            #endregion

            _documento.Exoneradas = 0;
            _documento.FechaEmision = documento.FechaEmision.ToString("dd/MM/yyyy");

            if (documento.Operacion.Where(x => x.TipoOperacion.Equals(21)).ToList().Count > 0)
            {
                _documento.Gratuitas = documento.Operacion.FirstOrDefault(x => x.TipoOperacion.Equals(21)).Monto;
            }
            else
            {
                _documento.Gratuitas = 0;
            }

            if (documento.Operacion.Where(x => x.TipoOperacion.Equals(10)).ToList().Count > 0)
            {
                _documento.Gravadas = documento.Operacion.FirstOrDefault(x => x.TipoOperacion.Equals(10)).Monto;
            }
            else
            {
                _documento.Gravadas = 0;
            }
            
            _documento.IdDocumento = documento.Numeracion;
            _documento.Inafectas = 0;

            #region Items
            var vd = documento.Item.FirstOrDefault();
            var item = new DetalleDocumento();
            item.Cantidad = documento.CantidadItem;
            item.CodigoItem = vd.CodigoProducto.ToString();
            item.Descripcion = documento.DescripcionItem;
            //item.Id = itemId;
            //itemId++;
            // 1: IGV   2: ISC  3: Otros
            if (documento.Tributo.Where(x => x.TipoTributo.Equals(1)).ToList().Count > 0)
            {
                item.Impuesto = documento.Tributo.FirstOrDefault(x => x.TipoTributo.Equals(1)).Valor;
            }

            item.PrecioUnitario = documento.PrecioUnitario;
            item.TotalVenta = documento.TotalVenta;
            item.UnidadMedida = documento.UnidadItem;
            item.Descuento = 0;
            item.Id = 1;
            item.ImpuestoSelectivo = 0;
            item.OtroImpuesto = 0;
            item.PrecioReferencial = 0;
            item.TipoImpuesto = "10";
            item.TipoPrecio = "01";
            _documento.Items.Add(item);
            #endregion

            _documento.Moneda = documento.TipoMoneda;
            _documento.MontoAnticipo = 0;
            _documento.MontoDetraccion = 0;
            _documento.MontoEnLetras = NumeroALetras.numeroAletras(documento.TotalVenta);
            _documento.MontoPercepcion = 0;
            _documento.PlacaVehiculo = String.Empty;

            #region Receptor
            var receptor = new DocumentoElectronico().Receptor;
            receptor.NombreComercial = documento.NombreAdquirente;
            receptor.NombreLegal = documento.NombreAdquirente;
            receptor.NroDocumento = documento.NumeroDocumentoAdquirente;
            receptor.TipoDocumento = documento.TipoDocumentoAfectado;
            receptor.Direccion = documento.DireccionAdquirente;
            _documento.Receptor = receptor;
            #endregion

            _documento.TipoDocumento = documento.TipoDocumento.ToString().PadLeft(2, '0');


            // 1: IGV   2: ISC  3: Otros
            if (documento.Tributo.Where(x => x.TipoTributo.Equals(1)).ToList().Count > 0)
            {
                _documento.TotalIgv = documento.Tributo.FirstOrDefault(x=>x.TipoTributo.Equals(10)).Valor;
            }


            _documento.TotalIsc = 0;
            _documento.TotalOtrosTributos = 0;
            _documento.TotalVenta = documento.TotalVenta;
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

            var resumenFirma = String.Empty;
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
            var firmaInfo = respuestaSunatInfo.Firma;
            var respuestaInfo = respuestaSunatInfo.Respuesta;

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

            resumenFirma = firmaInfo.ResumenFirma;


            #region Facturación
            facturacion.MensajeError = String.Empty;
            facturacion.SolicitudProcesada = true;
            facturacion.ValorResumen = responseFirma.Data.ResumenFirma;
            facturacion.ValorFirma = responseFirma.Data.ValorFirma;
            #endregion


            #region Envío servidor APUFact
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

            ventaInfo.CodigoRespuesta = String.Empty;
            ventaInfo.Exito = 0;
            ventaInfo.MensajeError = String.Empty;
            ventaInfo.MensajeRespuesta = String.Empty;
            ventaInfo.NombreArchivo = String.Empty;
            ventaInfo.NroTicket = String.Empty;
            ventaInfo.EstadoId = 0;

            ventaInfo.ComprobanteImpreso = String.Empty;

            // new VentaGasolutions().Insertar(ventaInfo);

            _ventaAccesoDatos.Insertar(ventaInfo);
            #endregion


            return facturacion;
        }
        #endregion
    }
}