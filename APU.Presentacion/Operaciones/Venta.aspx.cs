using System;
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
using OpenInvoicePeru.Comun.Dto.Intercambio;
using OpenInvoicePeru.Comun.Dto.Modelos;
using RestSharp;
using ZXing;

namespace APU.Presentacion.Operaciones
{
    public partial class Venta : PaginaBase
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
            LlenarCombo(ddlNumeroRegistros, tablaMaestraInfo, "NombreCorto", "NombreLargo");
            ddlNumeroRegistros.SelectedValue = "5";

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
            var usuarioInfo = ObtenerUsuarioInfo();
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

            var ventaInfoLista = new Negocio.Venta().ListarPaginado(0, rucBuscar, tipoComprobanteBuscar, fechaInicioBuscar, fechaFinBuscar, estadoBuscar, monedaBuscar, usuarioInfo.TipoNegocioId, numeroRegistros, indicePagina);
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
        public static VentaInfo ObtenerVenta(int ventaId)
        {
            return new Negocio.Venta().Listar(ventaId).FirstOrDefault();
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
            var ventaInfo = new Negocio.Venta().Listar(ventaId).FirstOrDefault();
            var ventaDetalleInfo = new Negocio.Venta().ListarDetalle(ventaId, 0);
            var empresaInfo = new Empresa().Listar(usuarioInfo.EmpresaId).FirstOrDefault();
            var clienteInfo = new Cliente().Listar(ventaInfo.ClienteId).FirstOrDefault();

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
            // _documento.PlacaVehiculo = ventaInfo.Placa;

            #region Receptor
            var receptor = new DocumentoElectronico().Receptor;
            receptor.Departamento = clienteInfo.Departamento;
            receptor.Direccion = clienteInfo.Direccion;
            receptor.Distrito = clienteInfo.Distrito;
            receptor.NombreComercial = clienteInfo.RazonSocial;
            receptor.NombreLegal = clienteInfo.RazonSocial;
            receptor.NroDocumento = clienteInfo.NumeroDocumento;
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
            //string rutaXml = HostingEnvironment.MapPath("~/Archivos/Facturacion/XML/" + _documento.IdDocumento + ".xml");
            //File.WriteAllBytes(rutaXml, Convert.FromBase64String(documentoResponse.Data.TramaXmlSinFirma));
            string rutaXml = HostingEnvironment.MapPath("~/Archivos/Facturacion/XML/Cliente/" + clienteInfo.NumeroDocumento);
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
            //string rutaXmlFirmado = HostingEnvironment.MapPath("~/Archivos/Facturacion/XML/" + _documento.IdDocumento + "_Firmado.xml");
            //File.WriteAllBytes(rutaXmlFirmado, Convert.FromBase64String(responseFirma.Data.TramaXmlFirmado));
            string rutaXmlFirmado = HostingEnvironment.MapPath("~/Archivos/Facturacion/XML/Cliente/" + clienteInfo.NumeroDocumento);
            if (!Directory.Exists(rutaXmlFirmado))
            {
                Directory.CreateDirectory(rutaXmlFirmado);
            }
            File.WriteAllBytes(rutaXmlFirmado + "/" + _documento.IdDocumento + "_Firmado.xml", Convert.FromBase64String(responseFirma.Data.TramaXmlFirmado));
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
            new Negocio.VentaSunat().Insertar(ventaSunatInfo);
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
                    facturaSB.Append(Herramientas.Helper.ObtenerTexto("~/Archivos/Plantillas/FacturaApu.html"));
                }
                if (ventaInfo.TipoComprobanteId.Equals(Constantes.TipoComprobanteBoletaVenta))
                {
                    facturaSB.Append(Herramientas.Helper.ObtenerTexto("~/Archivos/Plantillas/BoletaApu.html"));
                }

                var fechaEmision = DateTime.Now;

                facturaSB.Replace("@EMPRESA_LOGO", HttpContext.Current.Server.MapPath(empresaInfo.Imagen));
                facturaSB.Replace("@EMPRESA_NOMBRE", empresaInfo.RazonSocial);
                facturaSB.Replace("@EMPRESA_RUC", empresaInfo.NumeroDocumento);
                facturaSB.Replace("@NUMEROCOMPROBANTE", ventaInfo.NumeroSerie + "-" + ventaInfo.NumeroComprobante);
                facturaSB.Replace("@EMPRESA_DIRECCION", empresaInfo.Direccion);
                facturaSB.Replace("@EMPRESA_TELEFONO", empresaInfo.Telefono);
                facturaSB.Replace("@EMPRESA_FAX", empresaInfo.Fax);
                facturaSB.Replace("@EMPRESA_PAIS", empresaInfo.Pais);
                facturaSB.Replace("@EMPRESA_DEPARTAMENTO", empresaInfo.Departamento);
                facturaSB.Replace("@EMPRESA_PROVINCIA", empresaInfo.Provincia);
                facturaSB.Replace("@EMPRESA_DISTRITO", empresaInfo.Distrito);
                facturaSB.Replace("@FECHAEMISION", ventaInfo.FechaEmision.ToString("dd/MM/yyyy"));
                facturaSB.Replace("@AGENCIA_DIRECCION", ventaInfo.Agencia);


                var clienteInfo = new Cliente().Listar(ventaInfo.ClienteId).FirstOrDefault();
                facturaSB.Replace("@CLIENTE_NOMBRE", clienteInfo.TipoPersonaId.Equals(Constantes.TipoPersonaNatural) ? (clienteInfo.Nombres + "," + clienteInfo.ApellidoPaterno + " " + clienteInfo.ApellidoMaterno) : clienteInfo.RazonSocial);
                facturaSB.Replace("@CLIENTE_DIRECCION", clienteInfo.Direccion);
                facturaSB.Replace("@CLIENTE_DISTRITO", clienteInfo.Distrito);
                facturaSB.Replace("@CLIENTE_RUC", clienteInfo.NumeroDocumento);
                facturaSB.Replace("@CLIENTE_CODIGO", clienteInfo.Codigo);

                facturaSB.Replace("@MONEDA", ventaInfo.Moneda);

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
                foreach (var vd in ventaDetalleInfo)
                {
                    facturaSBDetalle.Append("   <tr>");
                    facturaSBDetalle.Append("       <td style=\"width: 10%; border: 1px none #0b44e9; text-align: center; vertical-align: top;\" class=\"Estilo8\">" + vd.Codigo + "</td>");
                    facturaSBDetalle.Append("       <td style=\"width: 50%; border: 1px none #0b44e9; text-align: left; vertical-align: top;\" class=\"Estilo8\">" + vd.Producto + "</td>");
                    facturaSBDetalle.Append("       <td style=\"width: 10%; border: 1px none #0b44e9; text-align: center; vertical-align: top;\" class=\"Estilo8\">" + vd.Cantidad.ToString("N2") + "</td>");
                    facturaSBDetalle.Append("       <td style=\"width: 10%; border: 1px none #0b44e9; text-align: center; vertical-align: top;\" class=\"Estilo8\">" + vd.UnidadMedida + "</td>");
                    facturaSBDetalle.Append("       <td style=\"width: 10%; border: 1px none #0b44e9; text-align: center; vertical-align: top;\" class=\"Estilo8\">" + vd.PrecioUnitario.ToString("N2") + "</td>");
                    facturaSBDetalle.Append("       <td style=\"width: 10%; border: 1px none #0b44e9; text-align: center; vertical-align: top;\" class=\"Estilo8\">" + vd.SubTotal.ToString("N2") + "</td>");
                    facturaSBDetalle.Append("   </tr>");

                    montoGravado = montoGravado + vd.SubTotal;
                }
                facturaSBDetalle.Append("</table>");

                var montoIgv = montoGravado * 0.18M;

                var montoTotal = montoGravado + montoIgv;

                facturaSB.Replace("@MONTO_GRAVADA", montoGravado.ToString("N2"));
                facturaSB.Replace("@MONTO_INAFECTA", "0.00");
                facturaSB.Replace("@MONTO_EXONERADA", "0.00");
                facturaSB.Replace("@MONTO_GRATUITA", "0.00");
                facturaSB.Replace("@MONTO_IGV", montoIgv.ToString("N2"));
                facturaSB.Replace("@MONTO_TOTAL", montoTotal.ToString("N2"));

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
                //string rutaXml = HostingEnvironment.MapPath("~/Archivos/Facturacion/XML/" + _documento.IdDocumento + ".xml");
                //File.WriteAllBytes(rutaXml, Convert.FromBase64String(documentoResponse.Data.TramaXmlSinFirma));
                string rutaXml = HostingEnvironment.MapPath("~/Archivos/Facturacion/XML/Cliente/" + clienteInfo.NumeroDocumento);
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
                //string rutaXmlFirmado = HostingEnvironment.MapPath("~/Archivos/Facturacion/XML/" + _documento.IdDocumento + "_Firmado.xml");
                //File.WriteAllBytes(rutaXmlFirmado, Convert.FromBase64String(responseFirma.Data.TramaXmlFirmado));
                string rutaXmlFirmado = HostingEnvironment.MapPath("~/Archivos/Facturacion/XML/Cliente/" + clienteInfo.NumeroDocumento);
                if (!Directory.Exists(rutaXmlFirmado))
                {
                    Directory.CreateDirectory(rutaXmlFirmado);
                }
                File.WriteAllBytes(rutaXmlFirmado + "/" + _documento.IdDocumento + "_Firmado.xml", Convert.FromBase64String(responseFirma.Data.TramaXmlFirmado));
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
                string rutaQr = HostingEnvironment.MapPath("~/Archivos/Documentos/Cliente/" + clienteInfo.NumeroDocumento);
                if (!Directory.Exists(rutaQr))
                {
                    Directory.CreateDirectory(rutaQr);
                }
                barcodeWriter.Write(codigoQR).Save(rutaQr + "/" + _documento.IdDocumento + ".bmp");
                #endregion

                facturaSB.Replace("@CODIGO_QR", HttpContext.Current.Server.MapPath("~/Archivos/Documentos/Cliente/" + clienteInfo.NumeroDocumento + "/" + _documento.IdDocumento + ".bmp"));

                facturaSB.Replace("@MONTO_LETRAS", Herramientas.NumeroALetras.numeroAletras(montoTotal));

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