using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Services;
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

namespace APU.Presentacion.Gasolutions
{
    public partial class VentaGasolutions : PaginaBase
    {
        #region Variables Privadas
        public static DocumentoElectronico _documento;
        #endregion
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            if (!Page.IsPostBack)
            {
                CargarInicial();
                CargarDatos();
            }
            else
            {
                if (ViewState["PageIndex"] != null)
                {
                    pageIndex = Convert.ToInt32(ViewState["PageIndex"]);
                }
                if (ViewState["PageCount"] != null)
                {
                    pageCount = Convert.ToInt32(ViewState["PageCount"]);
                }
            }
        }
        private void CargarInicial()
        {
            var usuarioInfo = ObtenerUsuarioInfo();

            var tablaMaestraInfo = new TablaMaestra().Listar(0, Constantes.TablaTamanioPagina).Where(x => x.Activo.Equals(1)).ToList();
            //LlenarCombo(ddlNumeroRegistros, tablaMaestraInfo, "NombreCorto", "NombreLargo");
            //ddlNumeroRegistros.SelectedValue = "5";

            tablaMaestraInfo = new TablaMaestra().Listar(0, Constantes.TablaTipoComprobante).Where(x => x.Activo.Equals(1)).ToList();
            LlenarCombo(ddlTipoComprobante, tablaMaestraInfo, "Codigo", "NombreLargo");

            LlenarCombo(ddlTipoComprobanteBuscar, tablaMaestraInfo, "Codigo", "NombreLargo");
            ddlTipoComprobanteBuscar.Items.Insert(0, new ListItem("Todos", String.Empty));

            tablaMaestraInfo = new TablaMaestra().Listar(0, Constantes.TablaMoneda).Where(x => x.Activo.Equals(1)).ToList();
            LlenarCombo(ddlMonedaBuscar, tablaMaestraInfo, "Codigo", "NombreLargo");
            ddlMonedaBuscar.Items.Insert(0, new ListItem("Todos", "0"));

            var fechaHoy = DateTime.Now;
            txtFechaEmisionInicioBuscar.Text = "01/" + fechaHoy.Month.ToString().PadLeft(2, '0') + "/" + fechaHoy.Year;
            txtFechaEmisionFinBuscar.Text = fechaHoy.ToString("dd/MM/yyyy");
        }
        private void CargarDatos()
        {
            var script = new StringBuilder("");

            var rucBuscar = txtNumeroDocumentoBuscar.Text.Trim();
            var tipoComprobanteBuscar = ddlTipoComprobanteBuscar.SelectedValue;
            var fechaInicioBuscar = DateTime.ParseExact(txtFechaEmisionInicioBuscar.Text.Trim(), "dd/MM/yyyy", null);
            var fechaFinBuscar = DateTime.ParseExact(txtFechaEmisionFinBuscar.Text.Trim(), "dd/MM/yyyy", null);
            var estadoBuscar = Convert.ToInt32(ddlEstadoBuscar.SelectedValue);
            var monedaBuscar = Convert.ToInt32(ddlMonedaBuscar.SelectedValue);

            grvVenta.DataBind();
            int numeroRegistros = Convert.ToInt16(ddlNumeroRegistros.SelectedValue);
            int numeroPagina = Convert.ToInt16(ViewState["PageIndex"]);

            int indicePagina = numeroPagina == 0 ? 0 : numeroPagina - 1;
            pageIndex = indicePagina + 1;

            //var ventaInfoLista = new Negocio.VentaPetroamerica().ListarPaginado(0, rucBuscar, tipoComprobanteBuscar, "", "", fechaInicioBuscar, fechaFinBuscar, estadoBuscar, monedaBuscar, numeroRegistros, indicePagina);
            var ventaInfoLista = new Negocio.VentaGasolutions().ListarPaginado(0, rucBuscar, tipoComprobanteBuscar, "", fechaInicioBuscar, fechaFinBuscar, estadoBuscar, monedaBuscar, numeroRegistros, indicePagina);
            grvVenta.DataSource = ventaInfoLista;
            grvVenta.DataBind();

            if (ventaInfoLista.Count > 0)
            {
                grvVenta.HeaderRow.Attributes["style"] = "display: none";
                grvVenta.UseAccessibleHeader = true;
                grvVenta.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            rowCount = ventaInfoLista.Count > 0 ? ventaInfoLista.First().TotalFilas : 0;
            pageCount = CalcPageCount(rowCount);
            ViewState["PageCount"] = pageCount;

            #region Texto del Pie de Página
            if (ventaInfoLista.Count > 0)
            {
                if (numeroRegistros == 0)
                {
                    lblPaginacion.Text = "Página " + pageIndex.ToString("") + " de 1, con un Total de " + rowCount.ToString("") + " registros";
                    script.Append("document.getElementById('lblPaginacion').innerText = '");
                    script.Append("Página " + pageIndex.ToString("") + " de 1, con un Total de " + rowCount.ToString("") + " registros';");
                }
                else
                {
                    lblPaginacion.Text = "Página " + pageIndex.ToString("") + " de " + pageCount.ToString("") + ", con un Total de " + rowCount.ToString("") + " registros";
                    script.Append("document.getElementById('lblPaginacion').innerText = '");
                    script.Append("Página " + pageIndex.ToString("") + " de " + pageCount.ToString("") + ", con un Total de " + rowCount.ToString("") + " registros';");
                }
            }
            else
            {
                lblPaginacion.Text = "No se obtuvieron resultados";
                script.Append("document.getElementById('lblPaginacion').innerText = 'No se obtuvieron resultados';");
            }
            #endregion

            RefreshPageButtons();
            RegistrarScript(script.ToString(), "Paginacion");
        }

        private int CalcPageCount(int totalRows)
        {
            int numeroRegistros = Convert.ToInt16(ddlNumeroRegistros.SelectedValue);
            numeroRegistros = (numeroRegistros == 0) ? totalRows : numeroRegistros;

            numeroRegistros = (numeroRegistros == 0) ? 1 : numeroRegistros;

            int cociente = totalRows % numeroRegistros;

            if (cociente > 0)
            {
                return ((int)(totalRows / numeroRegistros)) + 1;
            }
            else
            {
                return (int)(totalRows / numeroRegistros);
            }
        }
        protected int pageIndex = 1;
        protected int pageCount = 0;
        protected int rowCount = 0;
        private const int CONST_PAGE_SIZE = 5;
        protected void PageChangeEventHandler(object sender, CommandEventArgs e)
        {
            //tipoFiltro = String.IsNullOrEmpty(Request["filtro"]) ? 0 : Convert.ToInt16(Request["filtro"]);
            switch (e.CommandArgument.ToString())
            {
                case "First":
                    pageIndex = 1;
                    break;
                case "Prev":
                    pageIndex = pageIndex - 1;
                    break;
                case "Next":
                    pageIndex = pageIndex + 1;
                    break;
                case "Last":
                    pageIndex = pageCount;
                    break;
            }
            ViewState["PageIndex"] = pageIndex;
            CargarDatos();
            RefreshPageButtons();
        }
        protected void PageSortingEventHandler(object sender, CommandEventArgs e)
        {
            //tipoFiltro = String.IsNullOrEmpty(Request["filtro"]) ? 0 : Convert.ToInt16(Request["filtro"]);
            switch (e.CommandArgument.ToString())
            {
                case "First":
                    pageIndex = 1;
                    break;
                case "Prev":
                    pageIndex = pageIndex - 1;
                    break;
                case "Next":
                    pageIndex = pageIndex + 1;
                    break;
                case "Last":
                    pageIndex = pageCount;
                    break;
            }
            ViewState["PageIndex"] = pageIndex;
            CargarDatos();
            RefreshPageButtons();
        }
        private void RefreshPageButtons()
        {
            var script = new StringBuilder("");
            btnPrimero.Disabled = false;
            script.Append("document.getElementById('btnPrimero').disabled = false;");
            script.Append("document.getElementById('btnPrimero').setAttribute('onclick', 'Paginacion(btnPrimero)');");

            btnAnterior.Disabled = false;
            script.Append("document.getElementById('btnAnterior').disabled = false;");
            script.Append("document.getElementById('btnAnterior').setAttribute('onclick', 'Paginacion(btnAnterior)');");
            btnSiguiente.Disabled = false;
            script.Append("document.getElementById('btnSiguiente').disabled = false;");
            script.Append("document.getElementById('btnSiguiente').setAttribute('onclick', 'Paginacion(btnSiguiente)');");
            btnPrimero.Disabled = false;
            script.Append("document.getElementById('btnUltimo').disabled = false;");
            script.Append("document.getElementById('btnUltimo').setAttribute('onclick', 'Paginacion(btnUltimo)');");
            // if this is the first page, disable previous page
            if (pageIndex == 1)
            {
                btnAnterior.Disabled = true;
                script.Append("document.getElementById('btnAnterior').disabled = true;");
                btnPrimero.Disabled = true;
                script.Append("document.getElementById('btnPrimero').disabled = true;");
                //if the page count is more than 1, enable next button               
                if (pageCount <= 1)
                {
                    btnSiguiente.Disabled = true;
                    script.Append("document.getElementById('btnSiguiente').disabled = true;");
                    btnPrimero.Disabled = true;
                    script.Append("document.getElementById('btnUltimo').disabled = true;");
                }
            }
            else
            {
                if (pageIndex == pageCount)
                {
                    btnSiguiente.Disabled = true;
                    script.Append("document.getElementById('btnSiguiente').disabled = true;");
                    btnPrimero.Disabled = true;
                    script.Append("document.getElementById('btnUltimo').disabled = true;");
                }
            }
            RegistrarScript(script.ToString(), "Paginacion");
        }

        protected void ddlNumeroRegistros_SelectedIndexChanged(object sender, EventArgs e)
        {
            // tipoFiltro = String.IsNullOrEmpty(Request["filtro"]) ? 0 : Convert.ToInt16(Request["filtro"]);
            ViewState["PageCount"] = null;
            ViewState["PageIndex"] = null;
            CargarDatos();
        }

        protected void btnPaginacion_Click(object sender, EventArgs e)
        {
            var accion = hdnCommandArgument.Value;
            switch (accion)
            {
                case "First":
                    pageIndex = 1;
                    break;
                case "Prev":
                    pageIndex = pageIndex - 1;
                    break;
                case "Next":
                    pageIndex = pageIndex + 1;
                    break;
                case "Last":
                    pageIndex = pageCount;
                    break;
            }
            ViewState["PageIndex"] = pageIndex;
            CargarDatos();
            RefreshPageButtons();
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            RegistrarEventoCliente(txtBuscar, Constantes.EventoOnKeyUp, "return Buscar();");


            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Seleccione", "var Seleccione = '" + Constantes.Seleccione + "';", true);
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Seleccione_value", "var Seleccione_value = '" + Constantes.Seleccione_Value + "';", true);
        }

        #region WebMethod
        [WebMethod]
        public static VentaGasolutionsInfo ObtenerVenta(int ventaId)
        {
            //return new Negocio.Venta().Listar(ventaId).FirstOrDefault();
            return new Negocio.VentaGasolutions().Listar(ventaId).FirstOrDefault();
        }
        #endregion

        protected void grvVenta_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add(Constantes.EventoOnClick, "var fila = SeleccionarFila(this); if (typeof GridView_OnClick == 'function'){GridView_OnClick(fila);}");
                e.Row.Attributes.Add(Constantes.EventoOnDblClick, "var fila = SeleccionarFila(this); if (typeof grvVentas_OnDnlClick == 'function'){grvVentas_OnDnlClick(fila);}");
            }
        }
        protected void btnGuardarVenta_OnClick(object sender, EventArgs e)
        {
            var usuarioInfo = ObtenerUsuarioInfo();
            var script = new StringBuilder(String.Empty);
            var mensaje = String.Empty;
            var ventaId = Convert.ToInt32(hdnVentaId.Value);

            #region Datos Venta
            var ventaInfo = new VentaInfo();
            ventaInfo.VentaId = Convert.ToInt32(hdnVentaId.Value);
            //ventaInfo.Codigo = txtCodigo.Text.Trim();
            //ventaInfo.Nombre = txtNombre.Text.Trim();
            //ventaInfo.Descripcion = txtDescripcion.Text.Trim();
            //ventaInfo.Direccion = txtDireccion.Text.Trim();
            ventaInfo.Activo = (chkActivo.Checked) ? 1 : 0;
            #endregion

            if (ventaId.Equals(0))
            {
                ventaInfo.UsuarioCreacionId = usuarioInfo.UsuarioId;
                ventaId = new Negocio.Venta().Insertar(ventaInfo);
                if (ventaId > 0)
                {
                    script.Append("document.getElementById('hdnVentaId').value = " + ventaId + ";");
                    mensaje = "Se registró la Venta correctamente";
                }
                else
                {
                    mensaje = "Ya existe una Venta registrado con el número de comprobante: " + txtNumeroComprobante.Text.Trim();
                }
            }
            else
            {
                ventaInfo.UsuarioModificacionId = usuarioInfo.UsuarioId;
                ventaId = new Negocio.Venta().Actualizar(ventaInfo);
                if (ventaId > 0)
                {
                    mensaje = "Se actualizó la Venta correctamente";
                }
                else
                {
                    mensaje = "Ya existe una Venta registrada con el número de comprobante: " + txtNumeroComprobante.Text.Trim();
                }
            }
            script.Append("MostrarMensaje('" + mensaje + "');");
            script.Append("LimpiarVenta();");
            script.Append("var modalDialog = $find('mpeVenta'); modalDialog.hide();");

            CargarDatos();
            RegistrarScript(script.ToString(), "GuardarVenta");
        }
        public string ObtenerNombreCliente(int tipoPersonaId, string nombres, string apellidoPaterno, string apellidoMaterno, string razonSocial)
        {
            var nombreCliente = String.Empty;

            if (tipoPersonaId.Equals(Constantes.TipoPersonaNatural))
            {
                nombreCliente = nombres + ", " + apellidoPaterno + " " + apellidoMaterno;
            }
            if (tipoPersonaId.Equals(Constantes.TipoPersonaJuridica))
            {
                nombreCliente = razonSocial;
            }

            return nombreCliente;
        }
        // https://github.com/FrameworkPeru/ClienteOpenInvoicePeru-net40/blob/master/Clientes%20OpenInvoicePeru/OpenInvoicePeru.ApiClientCSharp/Program.cs
        private static readonly string BaseUrl = ConfigurationManager.AppSettings["APU.Sunat.BaseUrl"];
        private static readonly string UrlSunat = ConfigurationManager.AppSettings["APU.Sunat.UrlSunat"];
        private static readonly string FormatoFecha = ConfigurationManager.AppSettings["APU.Sunat.FormatoFecha"];
        [WebMethod]
        //public static string EnviarSunat(int ventaId)
        public static EnviarDocumentoResponse EnviarSunat(int ventaId)
        {
            var mensaje = String.Empty;

            var usuarioInfo = ObtenerUsuarioInfo();
            var ventaInfo = new Negocio.VentaGasolutions().Listar(ventaId).FirstOrDefault();
            var ventaDetalleInfo = new Negocio.VentaGasolutions().ListarDetalle(ventaId, 0).FirstOrDefault();
            // var empresaInfo = new Empresa().Listar(usuarioInfo.EmpresaId).FirstOrDefault();
            // var clienteInfo = new Cliente().Listar(ventaInfo.ClienteId).FirstOrDefault();

            _documento = new DocumentoElectronico();

            #region Documento
            _documento.CalculoDetraccion = 0;
            _documento.CalculoIgv = Decimal.Divide(18, 100);
            _documento.CalculoIsc = 0;
            _documento.DescuentoGlobal = ventaInfo.Descuento;
            // _documento.DocAnticipo = String.Empty;

            #region Emisor
            var emisor = new DocumentoElectronico().Emisor;
            emisor.Departamento = ventaInfo.DepartamentoEmisor;
            emisor.Direccion = ventaInfo.DireccionEmisor;
            emisor.Distrito = ventaInfo.DistritoEmisor;
            emisor.NombreComercial = ventaInfo.NombreComercialEmisor;
            emisor.NombreLegal = ventaInfo.NombreComercialEmisor;
            emisor.NroDocumento = ventaInfo.NroDocumentoEmisor;
            emisor.Provincia = ventaInfo.ProvinciaEmisor;
            emisor.TipoDocumento = ventaInfo.TipoDocumentoEmisor;
            emisor.Ubigeo = ventaInfo.UbigeoEmisor;
            emisor.Urbanizacion = String.Empty;
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
            //var item = new DetalleDocumento();
            //item.Cantidad = vd.Cantidad;
            //item.Descripcion = vd.Producto;
            //item.Id = itemId;
            //itemId++;
            //item.Impuesto = vd.Igv;
            //item.PrecioUnitario = vd.PrecioUnitario;
            //item.Suma = vd.SubTotal;
            //item.TotalVenta = vd.MontoTotal;
            //item.UnidadMedida = vd.UnidadMedida;
            //item.Descuento = 0;
            //item.ImpuestoSelectivo = 0;
            //item.OtroImpuesto = 0;
            //item.PrecioReferencial = 0;
            //item.TipoImpuesto = "10";
            //item.TipoPrecio = "01";
            //_documento.Items.Add(item);

            var item = new DetalleDocumento();
            item.Cantidad = ventaDetalleInfo.Cantidad;
            item.Descripcion = ventaDetalleInfo.Producto;
            item.Id = itemId;
            itemId++;
            item.Impuesto = 0.18m;
            item.PrecioUnitario = 0m;
            item.Suma = 0m;
            item.TotalVenta = 0m;
            item.UnidadMedida = String.Empty;
            item.Descuento = 0;
            item.ImpuestoSelectivo = 0;
            item.OtroImpuesto = 0;
            item.PrecioReferencial = 0;
            item.TipoImpuesto = "10";
            item.TipoPrecio = "01";
            _documento.Items.Add(item);
            //}
            #endregion

            //_documento.Moneda = ventaInfo.MonedaId.Equals(Constantes.MonedaSoles) ? Constantes.MonedaSolesSunat : Constantes.MonedaDolaresSunat;
            _documento.Moneda = Constantes.MonedaSolesSunat;
            _documento.MontoAnticipo = 0;
            _documento.MontoDetraccion = 0;
            _documento.MontoEnLetras = Herramientas.NumeroALetras.numeroAletras(ventaInfo.TotalVenta);
            _documento.MontoPercepcion = 0;
            _documento.PlacaVehiculo = ventaInfo.PlacaVehiculo;

            #region Receptor
            var receptor = new DocumentoElectronico().Receptor;
            receptor.Departamento = String.Empty;
            receptor.Direccion = ventaInfo.DireccionReceptor;
            receptor.Distrito = String.Empty;
            receptor.NombreComercial = ventaInfo.NombreComercialReceptor;
            receptor.NombreLegal = ventaInfo.NombreLegalReceptor;
            //receptor.NombreComercial = "RENE ABARCA URBANO";
            //receptor.NombreLegal = "RENE ABARCA URBANO";
            //receptor.NroDocumento = clienteInfo.NumeroDocumento;
            receptor.NroDocumento = ventaInfo.NroDocumentoReceptor;
            receptor.Provincia = String.Empty;
            receptor.TipoDocumento = ventaInfo.TipoDocumentoReceptor;
            receptor.Ubigeo = String.Empty;
            receptor.Urbanizacion = String.Empty;
            _documento.Receptor = receptor;
            #endregion

            _documento.TipoDocumento = ventaInfo.TipoDocumento;
            _documento.TotalIgv = ventaInfo.TotalIgv;
            _documento.TotalIsc = 0;
            _documento.TotalOtrosTributos = 0;
            _documento.TotalVenta = ventaInfo.TotalVenta;
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
            string rutaXml = HostingEnvironment.MapPath("~/Archivos/Facturacion/XML/" + _documento.IdDocumento + ".xml");
            File.WriteAllBytes(rutaXml, Convert.FromBase64String(documentoResponse.Data.TramaXmlSinFirma));
            #endregion

            #region Firma
            string rutaCertificado = HostingEnvironment.MapPath("~/Archivos/Facturacion/certificado.pfx");
            // var rutaCertificado = @"D:\ASOLUTIONS\APU\APU.Presentacion\certificado.pfx";
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
            string rutaXmlFirmado = HostingEnvironment.MapPath("~/Archivos/Facturacion/XML/" + _documento.IdDocumento + "_Firmado.xml");
            File.WriteAllBytes(rutaXmlFirmado, Convert.FromBase64String(responseFirma.Data.TramaXmlFirmado));
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

            if (!responseSendBill.Data.Exito)
            {
                // throw new ApplicationException(responseSendBill.Data.MensajeError);
            }
            else
            {
                string rutaCdr = HostingEnvironment.MapPath("~/Archivos/Facturacion/CDR/" + responseSendBill.Data.NombreArchivo + ".zip");
                File.WriteAllBytes(rutaCdr, Convert.FromBase64String(responseSendBill.Data.TramaZipCdr));
            }

            var data = responseSendBill.Data;
            mensaje = data.Exito ? data.MensajeRespuesta : data.MensajeError;

            var ventaSunatInfo = new VentaSunatInfo();
            ventaSunatInfo.VentaId = ventaId;
            ventaSunatInfo.CodigoRespuesta = (data.CodigoRespuesta == null) ? String.Empty : data.CodigoRespuesta;
            ventaSunatInfo.Exito = data.Exito ? 1 : 0;
            ventaSunatInfo.MensajeError = (data.MensajeError == null)?String.Empty : data.MensajeError;
            ventaSunatInfo.MensajeRespuesta = (data.MensajeRespuesta == null) ? String.Empty : data.MensajeRespuesta;
            ventaSunatInfo.NombreArchivo = (data.NombreArchivo == null) ? String.Empty : data.NombreArchivo;
            ventaSunatInfo.Pila = (data.Pila == null) ? String.Empty : data.Pila;
            ventaSunatInfo.TramaZipCdr = (data.TramaZipCdr == null) ? String.Empty : data.TramaZipCdr;
            ventaSunatInfo.UsuarioCreacionId = usuarioInfo.UsuarioId;
            new VentaSunat().Insertar(ventaSunatInfo);
            #endregion

            return data;
        }
        [WebMethod]
        public static string ImprimirComprobante(int ventaId)
        {
            var mensaje = String.Empty;
            try
            {
                var usuarioInfo = ObtenerUsuarioInfo();

                var ventaInfo = new Negocio.Venta().Listar(ventaId).FirstOrDefault();
                var ventaDetalleInfo = new Negocio.Venta().ListarDetalle(ventaId, 0);

                var empresaInfo = new Empresa().Listar(usuarioInfo.EmpresaId).FirstOrDefault();

                var facturaSB = new StringBuilder();

                if (ventaInfo.TipoComprobanteId.Equals(Constantes.TipoComprobanteFactura))
                {
                    facturaSB.Append(Herramientas.Helper.ObtenerTexto("~/Archivos/Plantillas/Factura.html"));
                }
                if (ventaInfo.TipoComprobanteId.Equals(Constantes.TipoComprobanteBoletaVenta))
                {
                    facturaSB.Append(Herramientas.Helper.ObtenerTexto("~/Archivos/Plantillas/Boleta.html"));
                }

                var fechaEmision = DateTime.Now;

                facturaSB.Replace("@EMPRESA_LOGO", HttpContext.Current.Server.MapPath(empresaInfo.Imagen));
                facturaSB.Replace("@EMPRESA_RAZON_SOCIAL", empresaInfo.RazonSocial);
                facturaSB.Replace("@EMPRESA_RUC", empresaInfo.NumeroDocumento);
                facturaSB.Replace("@NUMERO_FACTURA", ventaInfo.NumeroSerie + "-" + ventaInfo.NumeroComprobante);
                facturaSB.Replace("@EMPRESA_DIRECCION", empresaInfo.Direccion);
                facturaSB.Replace("@EMPRESA_TELEFONO", empresaInfo.Telefono);
                facturaSB.Replace("@EMPRESA_FAX", empresaInfo.Fax);
                facturaSB.Replace("@EMPRESA_PAIS", empresaInfo.Pais);
                facturaSB.Replace("@EMPRESA_DEPARTAMENTO", empresaInfo.Departamento);
                facturaSB.Replace("@EMPRESA_PROVINCIA", empresaInfo.Provincia);
                facturaSB.Replace("@EMPRESA_DISTRITO", empresaInfo.Distrito);
                facturaSB.Replace("@FECHA_EMISION", fechaEmision.ToString("dd/MM/yyyy"));

                var clienteInfo = new Cliente().Listar(ventaInfo.ClienteId).FirstOrDefault();
                facturaSB.Replace("@CLIENTE_RAZON_SOCIAL", clienteInfo.TipoPersonaId.Equals(Constantes.TipoPersonaNatural) ? (clienteInfo.Nombres + "," + clienteInfo.ApellidoPaterno + " " + clienteInfo.ApellidoMaterno) : clienteInfo.RazonSocial);
                facturaSB.Replace("@CLIENTE_DIRECCION", clienteInfo.Direccion);
                facturaSB.Replace("@CLIENTE_DISTRITO", clienteInfo.Distrito);
                facturaSB.Replace("@CLIENTE_RUC", clienteInfo.NumeroDocumento);
                facturaSB.Replace("@CLIENTE_CODIGO", clienteInfo.Codigo);

                facturaSB.Replace("@MONEDA", ventaInfo.Moneda);
                facturaSB.Replace("@MONTO_GRAVADA", ventaInfo.MontoVenta.ToString("N2"));
                facturaSB.Replace("@MONTO_INAFECTA", "0.00");
                facturaSB.Replace("@MONTO_EXONERADA", "0.00");
                facturaSB.Replace("@MONTO_GRATUITA", "0.00");
                facturaSB.Replace("@MONTO_IGV", ventaInfo.MontoImpuesto.ToString("N2"));
                facturaSB.Replace("@MONTO_TOTAL", ventaInfo.MontoTotal.ToString("N2"));

                var facturaSBDetalle = new StringBuilder("");
                facturaSBDetalle.Append("<table style=\"width: 100%; border-collapse: collapse; border: 1px solid black;\">");
                facturaSBDetalle.Append("   <tr>");
                facturaSBDetalle.Append("       <td style=\"width: 10%; border: 1px solid black; text-align: center;\">CODIGO</td>");
                facturaSBDetalle.Append("       <td style=\"width: 10%; border: 1px solid black; text-align: center;\">CANT</td>");
                facturaSBDetalle.Append("       <td style=\"width: 40%; border: 1px solid black; text-align: center;\">DESCRIPCION DEL PRODUCTO</td>");
                facturaSBDetalle.Append("       <td style=\"width: 10%; border: 1px solid black; text-align: center;\">PRECIO. UNIT</td>");
                facturaSBDetalle.Append("       <td style=\"width: 10%; border: 1px solid black; text-align: center;\">DESCT %</td>");
                facturaSBDetalle.Append("       <td style=\"width: 10%; border: 1px solid black; text-align: center;\">DESCT IMPORTE</td>");
                facturaSBDetalle.Append("       <td style=\"width: 10%; border: 1px solid black; text-align: center;\">TOTAL</td>");
                facturaSBDetalle.Append("   </tr>");

                foreach (var vd in ventaDetalleInfo)
                {
                    facturaSBDetalle.Append("   <tr>");
                    facturaSBDetalle.Append("       <td style=\"width: 10%; border: 1px solid black; text-align: center;\">" + vd.Codigo + "</td>");
                    facturaSBDetalle.Append("       <td style=\"width: 10%; border: 1px solid black; text-align: center;\">" + vd.Cantidad.ToString("N2") + "</td>");
                    facturaSBDetalle.Append("       <td style=\"width: 40%; border: 1px solid black; text-align: center;\">" + vd.Producto + "</td>");
                    facturaSBDetalle.Append("       <td style=\"width: 10%; border: 1px solid black; text-align: center;\">" + vd.PrecioUnitario.ToString("N2") + "</td>");
                    facturaSBDetalle.Append("       <td style=\"width: 10%; border: 1px solid black; text-align: center;\">" + "" + "</td>");
                    facturaSBDetalle.Append("       <td style=\"width: 10%; border: 1px solid black; text-align: center;\">" + "" + "</td>");
                    facturaSBDetalle.Append("       <td style=\"width: 10%; border: 1px solid black; text-align: center;\">" + vd.MontoTotal.ToString("N2") + "</td>");
                    facturaSBDetalle.Append("   </tr>");
                }
                facturaSBDetalle.Append("</table>");

                facturaSB.Replace("@PRODUCTO_DETALLE", facturaSBDetalle.ToString());







                #region Generar XML
                _documento = new DocumentoElectronico();

                #region Documento
                _documento.CalculoDetraccion = 0;
                _documento.CalculoIgv = Decimal.Divide(18, 100);
                _documento.CalculoIsc = 0;
                _documento.DescuentoGlobal = ventaInfo.Descuento;

                #region Emisor
                var emisor = new DocumentoElectronico().Emisor;
                emisor.Departamento = empresaInfo.Departamento;
                emisor.Direccion = empresaInfo.Direccion;
                emisor.Distrito = empresaInfo.Distrito;
                emisor.NombreComercial = empresaInfo.RazonSocial;
                emisor.NombreLegal = empresaInfo.RazonSocial;
                emisor.NroDocumento = "10421895452";
                emisor.Provincia = empresaInfo.Provincia;
                emisor.TipoDocumento = empresaInfo.TipoDocumentoId.ToString();
                emisor.Ubigeo = empresaInfo.Departamento;
                emisor.Urbanizacion = empresaInfo.Direccion;
                _documento.Emisor = emisor;
                #endregion

                _documento.Exoneradas = 0;
                _documento.FechaEmision = ventaInfo.FechaEmision.ToString("dd/MM/yyyy");
                _documento.Gratuitas = 0;
                _documento.Gravadas = ventaInfo.MontoVenta;
                _documento.IdDocumento = ventaInfo.NumeroSerie + "-" + ventaInfo.NumeroComprobante;
                _documento.Inafectas = 0;

                #region Items
                var itemId = 1;
                foreach (var vd in ventaDetalleInfo)
                {
                    var item = new DetalleDocumento();
                    item.Cantidad = vd.Cantidad;
                    item.Descripcion = vd.Producto;
                    item.Id = itemId;
                    itemId++;
                    item.Impuesto = vd.Igv;
                    item.PrecioUnitario = vd.PrecioUnitario;
                    item.Suma = vd.SubTotal;
                    item.TotalVenta = vd.MontoTotal;
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
                _documento.MontoEnLetras = Herramientas.NumeroALetras.numeroAletras(ventaInfo.MontoVenta);
                _documento.MontoPercepcion = 0;

                #region Receptor
                var receptor = new DocumentoElectronico().Receptor;
                receptor.Departamento = clienteInfo.Departamento;
                receptor.Direccion = clienteInfo.Direccion;
                receptor.Distrito = clienteInfo.Distrito;
                receptor.NombreComercial = clienteInfo.RazonSocial;
                receptor.NombreLegal = clienteInfo.RazonSocial;
                receptor.NroDocumento = "10415787796";
                receptor.Provincia = clienteInfo.Provincia;
                receptor.TipoDocumento = clienteInfo.TipoDocumentoId.ToString();
                receptor.Ubigeo = clienteInfo.Departamento;
                receptor.Urbanizacion = clienteInfo.Direccion;
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
                string rutaXml = HostingEnvironment.MapPath("~/Archivos/Facturacion/XML/" + _documento.IdDocumento + ".xml");
                File.WriteAllBytes(rutaXml, Convert.FromBase64String(documentoResponse.Data.TramaXmlSinFirma));
                #endregion

                #region Firma
                string rutaCertificado = HostingEnvironment.MapPath("~/Archivos/Facturacion/certificado.pfx");
                var firmado = new FirmadoRequest
                {
                    TramaXmlSinFirma = documentoResponse.Data.TramaXmlSinFirma,
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
                string rutaXmlFirmado = HostingEnvironment.MapPath("~/Archivos/Facturacion/XML/" + _documento.IdDocumento + "_Firmado.xml");
                File.WriteAllBytes(rutaXmlFirmado, Convert.FromBase64String(responseFirma.Data.TramaXmlFirmado));
                #endregion
                #endregion

                #region Codigo QR
                var codigoQR = ventaInfo.NumeroDocumento + "|" + ventaInfo.TipoComprobanteId + "|" + ventaInfo.NumeroSerie + "|" +
                               ventaInfo.NumeroComprobante + "|" + ventaInfo.MontoImpuesto + "|" + ventaInfo.MontoTotal + "|" +
                               ventaInfo.FechaEmision + "|" + clienteInfo.TipoDocumentoId + "|" + clienteInfo.NumeroDocumento + "|" +
                               responseFirma.Data.ResumenFirma + "|" + responseFirma.Data.ValorFirma;

                // instantiate a writer object
                var barcodeWriter = new BarcodeWriter();

                // set the barcode format
                barcodeWriter.Format = BarcodeFormat.PDF_417;

                // write text and generate a 2-D barcode as a bitmap
                // barcodeWriter.Write(codigoQR).Save(@"C:\Users\jeremy\Desktop\generated.bmp");
                barcodeWriter.Write(codigoQR).Save(HostingEnvironment.MapPath("~/Archivos/Documentos/Cliente/" + clienteInfo.NumeroDocumento + "/" + _documento.IdDocumento + ".bmp"));
                #endregion

                facturaSB.Replace("@CODIGO_QR", HttpContext.Current.Server.MapPath("~/Archivos/Documentos/Cliente/" + clienteInfo.NumeroDocumento + "/" + _documento.IdDocumento + ".bmp"));

                facturaSB.Replace("@MONTO_LETRAS", Herramientas.NumeroALetras.numeroAletras(ventaInfo.MontoVenta));




                facturaSB = Herramientas.Helper.EncodeHtml(facturaSB);

                var htmlToPdfFactura = new NReco.PdfGenerator.HtmlToPdfConverter();
                var pdfBytesFactura = htmlToPdfFactura.GeneratePdf(facturaSB.ToString());
                string nombreArchivoFactura = String.Empty;

                if (ventaInfo.TipoComprobanteId.Equals(Constantes.TipoComprobanteFactura))
                {
                    nombreArchivoFactura = "Factura_" + fechaEmision.ToString("ddMMyyyyhhmmss") + ".pdf";
                }
                if (ventaInfo.TipoComprobanteId.Equals(Constantes.TipoComprobanteBoletaVenta))
                {
                    nombreArchivoFactura = "Boleta_" + fechaEmision.ToString("ddMMyyyyhhmmss") + ".pdf";
                }

                string pdfFactura = HostingEnvironment.MapPath("~/Archivos/Documentos/Cliente/" + clienteInfo.NumeroDocumento + "");

                if (!Directory.Exists(pdfFactura))
                {
                    Directory.CreateDirectory(pdfFactura);
                }

                var comprobanteImpreso = pdfFactura + "\\" + nombreArchivoFactura;
                File.WriteAllBytes(comprobanteImpreso, pdfBytesFactura);

                // Negocio.Helper.ActualizarColumnasTabla("Venta", new string[] {"ComprobanteImpreso"}, new string[] { comprobanteImpreso }, new string[]{"VentaId"}, new string[]{ventaInfo.VentaId.ToString()} );
                Negocio.Helper.ActualizarColumnasTabla("Venta", new string[] { "ComprobanteImpreso" }, new string[] { nombreArchivoFactura }, new string[] { "VentaId" }, new string[] { ventaInfo.VentaId.ToString() });

                #region Envío Correo
                var correos = clienteInfo.Correo.Replace(",", ";");
                var correoArray = correos.Split(';').ToList();
                var asunto = "APUFact: Factura Electrónica serie " + ventaInfo.NumeroSerie + " número " + ventaInfo.NumeroComprobante + " del " + ventaInfo.FechaEmision.ToString("dd/MM/yyyy") +
                             " emitida por " + empresaInfo.RazonSocial + " para " +
                             (clienteInfo.TipoPersonaId.Equals(Constantes.TipoPersonaNatural) ? (clienteInfo.Nombres + "," + clienteInfo.ApellidoPaterno + " " + clienteInfo.ApellidoMaterno) : clienteInfo.RazonSocial);
                Negocio.Email.Enviar(correoArray, correoArray, correoArray, asunto, "Se adjunta el comprobante.", comprobanteImpreso);
                #endregion

                mensaje = "El Comprobante se generó correctamente" + "@" + nombreArchivoFactura;
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaPresentacion);
                mensaje = rethrow ? ex.Message : Constantes.ExcepcionPoliticaPresentacion;
            }
            return mensaje;
        }

        [WebMethod]
        public static string ImprimirSoloComprobante(int ventaPetroamericaId, int ventaId)
        {
            var mensaje = String.Empty;

            try
            {
                var usuarioInfo = ObtenerUsuarioInfo();
                var ventaInfo = new Negocio.VentaPetroamerica().Listar(ventaPetroamericaId, ventaId).FirstOrDefault();
                //var ventaDetalleInfo = new List<VentaDetalleInfo>();

                _documento = new DocumentoElectronico();

                #region Documento
                _documento.CalculoDetraccion = 0;
                _documento.CalculoIgv = Decimal.Divide(18, 100);
                _documento.CalculoIsc = 0;
                //_documento.DescuentoGlobal = ventaInfo.Descuento;
                _documento.DescuentoGlobal = 0;

                #region Emisor
                var emisor = new DocumentoElectronico().Emisor;
                emisor.Departamento = "";
                emisor.Direccion = ventaInfo.DireccionEmpresa;
                emisor.Distrito = "";
                emisor.NombreComercial = ventaInfo.RazonSocialEmpresa;
                emisor.NombreLegal = ventaInfo.RazonSocialEmpresa;
                // emisor.NroDocumento = "10421895452";
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
                _documento.MontoEnLetras = Herramientas.NumeroALetras.numeroAletras(ventaInfo.MontoVenta);
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
                string rutaXml = HostingEnvironment.MapPath("~/Archivos/Facturacion/XML/Cliente/" + ventaInfo.NumeroDocumentoCliente);
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
                string rutaXmlFirmado = HostingEnvironment.MapPath("~/Archivos/Facturacion/XML/Cliente/" + ventaInfo.NumeroDocumentoCliente);
                if (!Directory.Exists(rutaXmlFirmado))
                {
                    Directory.CreateDirectory(rutaXmlFirmado);
                }
                File.WriteAllBytes(rutaXmlFirmado + "/" + _documento.IdDocumento + "_Firmado.xml", Convert.FromBase64String(responseFirma.Data.TramaXmlFirmado));
                #endregion

                #region Impresión Comprobante
                //var fechaEmision = DateTime.Now;
                var fechaEmision = ventaInfo.FechaEmision;

                var rutaPdfFactura = HostingEnvironment.MapPath("~/Archivos/Documentos/Cliente/" + ventaInfo.NumeroDocumentoCliente + "");

                var htmlToComprobante = new NReco.PdfGenerator.HtmlToPdfConverter();
                var nombreArchivoComprobante = String.Empty;
                var comprobanteSB = new StringBuilder();

                if (ventaInfo.TipoComprobanteId.Equals(Constantes.TipoComprobanteFactura))
                {
                    comprobanteSB.Append(Herramientas.Helper.ObtenerTexto("~/Archivos/Plantillas/FacturaPetroamerica.html"));
                    #region Factura
                    comprobanteSB.Replace("@EMPRESA_LOGO", HttpContext.Current.Server.MapPath("~/Imagenes/petroamerica_logo.png"));
                    comprobanteSB.Replace("@EMPRESA_NOMBRE", ventaInfo.RazonSocialEmpresa);
                    comprobanteSB.Replace("@EMPRESA_DIRECCION", ventaInfo.DireccionEmpresa);
                    comprobanteSB.Replace("@GRIFO_DIRECCION", ventaInfo.DireccionAgencia);
                    comprobanteSB.Replace("@EMPRESA_RUC", ventaInfo.RucEmpresa);
                    comprobanteSB.Replace("@IMPRESORA", ventaInfo.ImpresoraAgencia);
                    comprobanteSB.Replace("@NUMEROCOMPROBANTE", ventaInfo.Serie + "-" + ventaInfo.NumeroComprobante);
                    comprobanteSB.Replace("@FECHA", fechaEmision.ToString("dd/MM/yyyy - HH:mm:ss"));
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
                    nombreArchivoComprobante = "Factura_" + fechaEmision.ToString("ddMMyyyyhhmmss") + ".pdf";
                }
                if (ventaInfo.TipoComprobanteId.Equals(Constantes.TipoComprobanteBoletaVenta))
                {
                    comprobanteSB.Append(Herramientas.Helper.ObtenerTexto("~/Archivos/Plantillas/BoletaPetroamerica.html"));
                    #region Boleta
                    comprobanteSB.Replace("@EMPRESA_LOGO", HttpContext.Current.Server.MapPath("~/Imagenes/petroamerica_logo.png"));
                    comprobanteSB.Replace("@EMPRESA_NOMBRE", ventaInfo.RazonSocialEmpresa);
                    comprobanteSB.Replace("@EMPRESA_DIRECCION", ventaInfo.DireccionEmpresa);
                    comprobanteSB.Replace("@GRIFO_DIRECCION", ventaInfo.DireccionAgencia);
                    comprobanteSB.Replace("@EMPRESA_RUC", ventaInfo.RucEmpresa);
                    comprobanteSB.Replace("@IMPRESORA", ventaInfo.ImpresoraAgencia);
                    comprobanteSB.Replace("@NUMEROCOMPROBANTE", ventaInfo.Serie + "-" + ventaInfo.NumeroComprobante);
                    comprobanteSB.Replace("@FECHA", fechaEmision.ToString("dd/MM/yyyy - HH:mm:ss"));
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
                    nombreArchivoComprobante = "Boleta_" + fechaEmision.ToString("ddMMyyyyhhmmss") + ".pdf";
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
                //margins.Left = 0.5F;
                //htmlToComprobante.Margins = margins;

                var pdfBytesFactura = htmlToComprobante.GeneratePdf(comprobanteSB.ToString());


                //var comprobanteImpreso = rutaPdfFactura + "\\" + ventaInfo.NumeroDocumentoCliente + "\\" + nombreArchivoComprobante;
                var comprobanteImpreso = rutaPdfFactura + "\\" + nombreArchivoComprobante;
                File.WriteAllBytes(comprobanteImpreso, pdfBytesFactura);
                #endregion

                #region Envio SUNAT
                //var ventaSunatInfo = new VentaSunatInfo();
                //ventaSunatInfo.VentaId = ventaId;
                //ventaSunatInfo.CodigoRespuesta = String.Empty;
                //ventaSunatInfo.Exito = 0;
                //ventaSunatInfo.MensajeError = String.Empty;
                //ventaSunatInfo.MensajeRespuesta = String.Empty;
                //ventaSunatInfo.NombreArchivo = String.Empty;
                //ventaSunatInfo.Pila = String.Empty;
                //ventaSunatInfo.TramaZipCdr = String.Empty;
                //ventaSunatInfo.NroTicket = String.Empty;

                //ventaSunatInfo.ComprobanteImpreso = nombreArchivoComprobante;

                //ventaSunatInfo.FechaCreacion = DateTime.Now;
                //ventaSunatInfo.UsuarioCreacionId = usuarioInfo.UsuarioCreacionId;

                //new VentaSunat().Insertar(ventaSunatInfo);
                #endregion



                mensaje = "El Comprobante se generó correctamente" + "@" + nombreArchivoComprobante;
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaPresentacion);
                mensaje = rethrow ? ex.Message : Constantes.ExcepcionPoliticaPresentacion;
            }
            return mensaje;
        }

        public string ObtenerEstado(int exito, string mensajeError)
        {
            var est = String.Empty;

            if (exito > 0)
            {
                est = "Aceptado";
            }
            else
            {
                if (mensajeError.Length > 0)
                {
                    est = "Rechazado";
                }
                else
                {
                    est = "Pendiente";
                }
            }

            return est;
        }
        public string ObtenerRespuesta(int exito, string mensajeError, string mensajeRespuesta)
        {
            var res = String.Empty;

            if (exito > 0)
            {
                res = mensajeRespuesta;
            }
            else
            {
                if (mensajeError.Length > 0)
                {
                    res = mensajeError;
                }
                else
                {
                    res = String.Empty;
                }
            }

            return res;
        }

        protected void btnCargarVentas_OnClick(object sender, EventArgs e)
        {
            CargarDatos();
        }
    }
}