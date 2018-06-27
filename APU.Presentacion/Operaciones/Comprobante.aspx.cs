using System;
using System.Web;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
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
    public partial class Comprobante : PaginaBase
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

            var tablaMaestraInfo = new TablaMaestra().Listar(0, Constantes.TablaTipoComprobante).Where(x => x.Activo.Equals(1)).ToList();
            LlenarCombo(ddlTipoComprobante, tablaMaestraInfo, "Codigo", "NombreLargo");
            // ddlTipoComprobante.Items.Insert(0, new ListItem(Constantes.Seleccione, Constantes.Seleccione_Value));
            ddlTipoComprobante.SelectedValue = Constantes.TipoComprobanteFactura.ToString();

            tablaMaestraInfo = new TablaMaestra().Listar(0, Constantes.TablaSerie).Where(x => x.Activo.Equals(1)).ToList();
            LlenarCombo(ddlSerie, tablaMaestraInfo, "Codigo", "NombreCorto");
            ddlSerie.Items.Insert(0, new ListItem(Constantes.Seleccione, Constantes.Seleccione_Value));

            tablaMaestraInfo = new TablaMaestra().Listar(0, Constantes.TablaMoneda).Where(x => x.Activo.Equals(1)).ToList();
            LlenarCombo(ddlMoneda, tablaMaestraInfo, "Codigo", "NombreLargo");
            // ddlMoneda.Items.Insert(0, new ListItem(Constantes.Seleccione, Constantes.Seleccione_Value));
            ddlMoneda.SelectedValue = Constantes.MonedaSoles.ToString();

            tablaMaestraInfo = new TablaMaestra().Listar(0, Constantes.TablaTipoDocumento).Where(x => x.Activo.Equals(1)).ToList();
            LlenarCombo(ddlTipoDocumento, tablaMaestraInfo, "Codigo", "NombreLargo");
            ddlTipoDocumento.Items.Insert(0, new ListItem(Constantes.Seleccione, Constantes.Seleccione_Value));

            LlenarCombo(ddlTipoDocumentoPersonaNatural, tablaMaestraInfo, "Codigo", "NombreLargo");
            ddlTipoDocumentoPersonaNatural.SelectedValue = Constantes.TipoDocumentoDni.ToString();

            tablaMaestraInfo = new TablaMaestra().Listar(0, Constantes.TablaUnidades).Where(x => x.Activo.Equals(1)).ToList();
            LlenarCombo(ddlUnidad, tablaMaestraInfo, "Codigo", "NombreCorto");
            ddlUnidad.Items.Insert(0, new ListItem(Constantes.Seleccione, Constantes.Seleccione_Value));

            tablaMaestraInfo = new TablaMaestra().Listar(0, Constantes.TablaTipoImpuestoIgv).Where(x => x.Activo.Equals(1)).ToList();
            LlenarCombo(ddlTipoIgv, tablaMaestraInfo, "Codigo", "NombreCorto");
            ddlTipoIgv.Items.Insert(0, new ListItem(Constantes.Seleccione, Constantes.Seleccione_Value));

            tablaMaestraInfo = new TablaMaestra().Listar(0, Constantes.TablaTipoSistemaCalculoIsc).Where(x => x.Activo.Equals(1)).ToList();
            LlenarCombo(ddlTipoCalculoIsc, tablaMaestraInfo, "Codigo", "NombreCorto");
            ddlTipoCalculoIsc.Items.Insert(0, new ListItem(Constantes.Seleccione, Constantes.Seleccione_Value));

            txtFechaEmision.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }
        private void CargarDatos()
        {
            var script = new StringBuilder("");

            //grvVenta.DataBind();
            //int numeroRegistros = Convert.ToInt16(ddlNumeroRegistros.SelectedValue);
            int numeroPagina = Convert.ToInt16(ViewState["PageIndex"]);

            int indicePagina = numeroPagina == 0 ? 0 : numeroPagina - 1;
            pageIndex = indicePagina + 1;

            //var ventaInfoLista = new Negocio.Venta().ListarPaginado(0, numeroRegistros, indicePagina);
            //grvVenta.DataSource = ventaInfoLista;
            //grvVenta.DataBind();

            //if (ventaInfoLista.Count > 0)
            //{
            //    grvVenta.HeaderRow.Attributes["style"] = "display: none";
            //    grvVenta.UseAccessibleHeader = true;
            //    grvVenta.HeaderRow.TableSection = TableRowSection.TableHeader;
            //}
            //rowCount = ventaInfoLista.Count > 0 ? ventaInfoLista.First().TotalFilas : 0;
            pageCount = CalcPageCount(rowCount);
            ViewState["PageCount"] = pageCount;

            #region Texto del Pie de Página
            //if (ventaInfoLista.Count > 0)
            //{
            //    if (numeroRegistros == 0)
            //    {
            //        lblPaginacion.Text = "Página " + pageIndex.ToString("") + " de 1, con un Total de " + rowCount.ToString("") + " registros";
            //        script.Append("document.getElementById('lblPaginacion').innerText = '");
            //        script.Append("Página " + pageIndex.ToString("") + " de 1, con un Total de " + rowCount.ToString("") + " registros';");
            //    }
            //    else
            //    {
            //        lblPaginacion.Text = "Página " + pageIndex.ToString("") + " de " + pageCount.ToString("") + ", con un Total de " + rowCount.ToString("") + " registros";
            //        script.Append("document.getElementById('lblPaginacion').innerText = '");
            //        script.Append("Página " + pageIndex.ToString("") + " de " + pageCount.ToString("") + ", con un Total de " + rowCount.ToString("") + " registros';");
            //    }
            //}
            //else
            //{
            //    lblPaginacion.Text = "No se obtuvieron resultados";
            //    script.Append("document.getElementById('lblPaginacion').innerText = 'No se obtuvieron resultados';");
            //}
            #endregion

            //RefreshPageButtons();
            RegistrarScript(script.ToString(), "Paginacion");
        }

        private int CalcPageCount(int totalRows)
        {
            //int numeroRegistros = Convert.ToInt16(ddlNumeroRegistros.SelectedValue);
            //numeroRegistros = (numeroRegistros == 0) ? totalRows : numeroRegistros;

            //numeroRegistros = (numeroRegistros == 0) ? 1 : numeroRegistros;

            //int cociente = totalRows % numeroRegistros;

            //if (cociente > 0)
            //{
            //    return ((int)(totalRows / numeroRegistros)) + 1;
            //}
            //else
            //{
            //    return (int)(totalRows / numeroRegistros);
            //}
            return 0;
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
            //RefreshPageButtons();
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
            //RefreshPageButtons();
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
            //var accion = hdnCommandArgument.Value;
            //switch (accion)
            //{
            //    case "First":
            //        pageIndex = 1;
            //        break;
            //    case "Prev":
            //        pageIndex = pageIndex - 1;
            //        break;
            //    case "Next":
            //        pageIndex = pageIndex + 1;
            //        break;
            //    case "Last":
            //        pageIndex = pageCount;
            //        break;
            //}
            //ViewState["PageIndex"] = pageIndex;
            //CargarDatos();
            //RefreshPageButtons();
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            RegistrarEventoCliente(txtCodigoCliente, Constantes.EventoOnKeyPress, "return ObtenerCliente(event);");
            RegistrarEventoCliente(ddlTipoComprobante, Constantes.EventoOnChange, "return ObtenerCorrelativo();");
            RegistrarEventoCliente(ddlSerie, Constantes.EventoOnChange, "return ObtenerCorrelativo();");

            RegistrarEventoCliente(rbtPersonaNatural, Constantes.EventoOnClick, "return SeleccionarTipoPersona();");
            RegistrarEventoCliente(rbtPersonaJuridica, Constantes.EventoOnClick, "return SeleccionarTipoPersona();");

            RegistrarEventoCliente(txtCantidad, Constantes.EventoOnBlur, "return CalcularOperacionCantidadPrecio();");
            RegistrarEventoCliente(txtPrecio, Constantes.EventoOnBlur, "return CalcularOperacionCantidadPrecio();");
            RegistrarEventoCliente(txtSubTotal, Constantes.EventoOnBlur, "return CalcularOperacionSubTotal();");

            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Seleccione", "var Seleccione = '" + Constantes.Seleccione + "';", true);
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Seleccione_value", "var Seleccione_value = '" + Constantes.Seleccione_Value + "';", true);

            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "PorcentajeIgv", "var PorcentajeIgv = '" + Negocio.Helper.ObtenerValorParametro("PORCENTAJE_IGV") + "';", true);

            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "TipoComprobanteFactura", "var TipoComprobanteFactura = '" + Constantes.TipoComprobanteFactura + "';", true);
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "TipoComprobanteBoletaVenta", "var TipoComprobanteBoletaVenta = '" + Constantes.TipoComprobanteBoletaVenta + "';", true);
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
            //var ventaId = Convert.ToInt32(hdnVentaId.Value);

            #region Datos Venta
            var ventaInfo = new VentaInfo();
            //ventaInfo.VentaId = Convert.ToInt32(hdnVentaId.Value);
            //ventaInfo.Codigo = txtCodigo.Text.Trim();
            //ventaInfo.Nombre = txtNombre.Text.Trim();
            //ventaInfo.Descripcion = txtDescripcion.Text.Trim();
            //ventaInfo.Direccion = txtDireccion.Text.Trim();
            //ventaInfo.Activo = (chkActivo.Checked) ? 1 : 0;
            #endregion

            //if (ventaId.Equals(0))
            //{
            //    ventaInfo.UsuarioCreacionId = usuarioInfo.UsuarioId;
            //    ventaId = new Negocio.Venta().Insertar(ventaInfo);
            //    if (ventaId > 0)
            //    {
            //        script.Append("document.getElementById('hdnVentaId').value = " + ventaId + ";");
            //        mensaje = "Se registró la Venta correctamente";
            //    }
            //    else
            //    {
            //        mensaje = "Ya existe una Venta registrado con el número de comprobante: " + txtNumeroComprobante.Text.Trim();
            //    }
            //}
            //else
            //{
            //    ventaInfo.UsuarioModificacionId = usuarioInfo.UsuarioId;
            //    ventaId = new Negocio.Venta().Actualizar(ventaInfo);
            //    if (ventaId > 0)
            //    {
            //        mensaje = "Se actualizó la Venta correctamente";
            //    }
            //    else
            //    {
            //        mensaje = "Ya existe una Venta registrada con el número de comprobante: " + txtNumeroComprobante.Text.Trim();
            //    }
            //}
            script.Append("MostrarMensaje('" + mensaje + "');");
            script.Append("LimpiarVenta();");
            script.Append("var modalDialog = $find('mpeVenta'); modalDialog.hide();");

            CargarDatos();
            RegistrarScript(script.ToString(), "GuardarVenta");
        }

        protected void imgBuscarCliente_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {

        }

        protected void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            var tipoPersonaId = rbtPersonaNatural.Checked ? Constantes.TipoPersonaNatural : Constantes.TipoPersonaJuridica;
            var tipoDocumentoId = Convert.ToInt32(ddlTipoDocumentoPersonaNatural.SelectedValue);
            var numeroDocumento = txtNumeroDocumentoPersonaNatural.Text.Trim();
            var nombres = txtNombresCliente.Text.Trim();
            var apellidoPaterno = txtApellidoPaternoCliente.Text.Trim();
            var apellidoMaterno = txtApellidoMaternoCliente.Text.Trim();
            var ruc = txtRuc.Text.Trim();
            var razonSocial = txtRazonSocialCliente.Text.Trim();
            var codigo = txtCodigoClienteBuscar.Text.Trim();
            var clienteInfoLista = new Cliente().ListarPaginado(0, tipoPersonaId, tipoDocumentoId, numeroDocumento, nombres, apellidoPaterno, apellidoMaterno, ruc, razonSocial, codigo, 0, 0);
            grvCliente.DataSource = clienteInfoLista;
            grvCliente.DataBind();

            if (clienteInfoLista.Count > 0)
            {
                grvCliente.HeaderRow.Attributes["style"] = "display: none";
                grvCliente.UseAccessibleHeader = true;
                grvCliente.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            //rowCount = ventaInfoLista.Count > 0 ? ventaInfoLista.First().TotalFilas : 0;
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
        protected void btnBuscarProducto_Click(object sender, EventArgs e)
        {
            var usuarioInfo = ObtenerUsuarioInfo();
            var tipoTiendaId = (Request["TipoTiendaId"] == null) ? 0 : Convert.ToInt32(Request["TipoTiendaId"]);
            var codigo = txtCodigoBuscar.Text.Trim();
            var descripcion = txtProductoBuscar.Text.Trim();

            //var productoInfoLista = new Producto().ListarPaginado(0, codigo, razonSocial, 0, 0);
            var productoInfoLista = new List<ProductoInfo>();

            //switch (tipoTiendaId)
            //{
            //    case 0:
            //        productoInfoLista = new Negocio.Producto().ListarPaginado(0, codigo, descripcion, 0, 0).ToList();
            //        break;
            //    case 1: // Estación
            //        productoInfoLista = new Negocio.Producto().ListarPaginado(0, codigo, descripcion, 0, 0).Where(p => p.DisponibleEstacion.Equals(1)).ToList();
            //        break;
            //    case 2: // Market
            //        productoInfoLista = new Negocio.Producto().ListarPaginado(0, codigo, descripcion, 0, 0).Where(p => p.DisponibleMarket.Equals(1)).ToList();
            //        break;
            //    case 3: // Canastilla
            //        productoInfoLista = new Negocio.Producto().ListarPaginado(0, codigo, descripcion, 0, 0).Where(p => p.DisponibleCanastilla.Equals(1)).ToList();
            //        break;
            //}

            productoInfoLista = new Negocio.Producto().ListarPaginado(0, String.Empty, String.Empty, 0, 0).ToList();
            if (usuarioInfo.TipoNegocioId > 0)
            {
                productoInfoLista = productoInfoLista.Where(p => p.TipoNegocioId.Contains(usuarioInfo.TipoNegocioId.ToString())).ToList();
            }

            grvProducto.DataSource = productoInfoLista;
            grvProducto.DataBind();

            if (productoInfoLista.Count > 0)
            {
                grvProducto.HeaderRow.Attributes["style"] = "display: none";
                grvProducto.UseAccessibleHeader = true;
                grvProducto.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            //script.Append("");
        }
        [WebMethod]
        public static List<TablaMaestraInfo> ObtenerTablaMaestra(int tablaId, string codigo)
        {
            var tablaMaestraListaInfo = new TablaMaestra().Listar(0, tablaId).Where(x => x.Activo.Equals(1)).ToList();
            if (!codigo.Equals(String.Empty))
            {
                tablaMaestraListaInfo = new TablaMaestra().Listar(0, Constantes.TablaSerie).Where(x => x.Activo.Equals(1)).Where(y => y.Codigo.Equals(codigo)).ToList();
            }
            return tablaMaestraListaInfo;
        }
        [WebMethod]
        public static List<CorrelativoInfo> ObtenerCorrelativo(string tipoComprobanteId, int serieId)
        {
            var correlativoListaInfo = new Correlativo().Listar(tipoComprobanteId, serieId, 0).Where(x => x.Activo.Equals(1)).ToList();
            
            return correlativoListaInfo;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            var mensaje = String.Empty;
            try
            {
                var script = new StringBuilder();
                var usuarioInfo = ObtenerUsuarioInfo();

                var ventaDetalleListaInfo = (List<VentaDetalleInfo>)Session["VentaDetalle"];
                if (ventaDetalleListaInfo == null)
                {
                    ventaDetalleListaInfo = new List<VentaDetalleInfo>();
                }

                var ventaDetalleInfo = new VentaDetalleInfo();
                ventaDetalleInfo.VentaDetalleId = ventaDetalleListaInfo.Count + 1;
                ventaDetalleInfo.VentaId = Convert.ToInt32(hdnVentaId.Value);
                ventaDetalleInfo.ProductoId = Convert.ToInt32(hdnProductoId.Value);
                ventaDetalleInfo.Producto = hdnProducto.Value;
                ventaDetalleInfo.Codigo = Request["txtProductoCodigo"];
                ventaDetalleInfo.UnidadMedida = ddlUnidad.SelectedItem.Text;
                ventaDetalleInfo.Cantidad = Convert.ToDecimal(txtCantidad.Text);
                ventaDetalleInfo.PrecioUnitario = Convert.ToDecimal(Request["txtPrecio"]);
                ventaDetalleInfo.SubTotal = Convert.ToDecimal(txtSubTotal.Text);
                ventaDetalleInfo.Descuento = 0;
                ventaDetalleInfo.Igv = Convert.ToDecimal(Request["txtIgv"]);
                ventaDetalleInfo.MontoTotal = Convert.ToDecimal(Request["txtTotal"]);
                ventaDetalleInfo.Placa = Convert.ToString(Request["txtNumeroPlaca"]);
                ventaDetalleInfo.UsuarioCreacionId = usuarioInfo.UsuarioId;

                ventaDetalleListaInfo.Add(ventaDetalleInfo);
                Session["VentaDetalle"] = ventaDetalleListaInfo;

                grvItem.DataSource = ventaDetalleListaInfo;
                grvItem.DataBind();

                if (ventaDetalleListaInfo.Count > 0)
                {
                    grvItem.HeaderRow.Attributes["style"] = "display: none";
                    grvItem.UseAccessibleHeader = true;
                    grvItem.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                script.Append("CalcularMontoFinal();LimpiarItem();");
                RegistrarScript(script.ToString(), "CalcularMontoComprobante");
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaPresentacion);
                mensaje = rethrow ? ex.Message : Constantes.ExcepcionPoliticaPresentacion;
                RegistrarScript("MostrarMensaje('" + mensaje + "');", "CalcularMontoComprobante");
            }
        }

        [WebMethod]
        public static string GenerarComprobante(int clienteId, string tipoComprobanteId, int serieId, string serie, string numeroComprobante, string numeroGuia, string glosa, int estadoId, int formaPagoId,
                                                string fechaEmision, string fechaVencimiento, string fechaPago, int monedaId, decimal cantidad, decimal montoVenta, decimal montoImpuesto, decimal montoTotal, string numeroPlaca)
        {
            var mensaje = String.Empty;
            try
            {
                var usuarioInfo = ObtenerUsuarioInfo();
                var ventaId = 0;
                // var clienteInfo = new Cliente().Listar(clienteId).FirstOrDefault();

                #region Venta
                var ventaInfo = new VentaInfo();

                ventaInfo.ClienteId = clienteId;
                ventaInfo.UsuarioCreacionId = usuarioInfo.UsuarioId;
                ventaInfo.AgenciaId = usuarioInfo.AgenciaId;
                ventaInfo.TipoComprobanteId = tipoComprobanteId;
                ventaInfo.NumeroComprobante = numeroComprobante;
                ventaInfo.NumeroSerie = serie;
                ventaInfo.NumeroGuia = numeroGuia;
                ventaInfo.Glosa = glosa;
                ventaInfo.EstadoId = estadoId;
                ventaInfo.FormaPagoId = formaPagoId;
                ventaInfo.FechaEmision = DateTime.ParseExact(fechaEmision, "dd/MM/yyyy", null);
                ventaInfo.FechaVencimiento = DateTime.ParseExact(fechaVencimiento, "dd/MM/yyyy", null);
                ventaInfo.FechaPago = DateTime.ParseExact(fechaPago, "dd/MM/yyyy", null);
                ventaInfo.MonedaId = monedaId;
                ventaInfo.TipoCambio = 3;
                ventaInfo.Cantidad = cantidad;
                ventaInfo.MontoVenta = montoVenta;
                ventaInfo.MontoImpuesto = montoImpuesto;
                ventaInfo.MontoTotal = montoTotal;
                ventaInfo.ComprobanteImpreso = String.Empty;
                ventaInfo.Activo = 1;
                ventaInfo.TipoNegocioId = usuarioInfo.TipoNegocioId;

                ventaId = new Negocio.Venta().Insertar(ventaInfo);

                var correlativoInfo = new Correlativo().Listar(tipoComprobanteId, serieId, 0).FirstOrDefault();
                // var numeroCorrelativoNuevo = Convert.ToInt32(numeroComprobante);
                correlativoInfo.Actual = (Convert.ToInt32(numeroComprobante) + 1).ToString().PadLeft(8, '0');
                new Correlativo().Actualizar(correlativoInfo);
                #endregion

                #region Venta Detalle
                var ventaDetalleListaInfo = (List<VentaDetalleInfo>)HttpContext.Current.Session["VentaDetalle"];
                //var ventaDetalleListaInfo = (List<VentaDetalleInfo>)grvItem.DataSource;
                foreach (var vd in ventaDetalleListaInfo)
                {
                    var ventaDetalleInfo = new VentaDetalleInfo();
                    ventaDetalleInfo.VentaId = ventaId;
                    ventaDetalleInfo.ProductoId = vd.ProductoId;
                    ventaDetalleInfo.Cantidad = vd.Cantidad;
                    ventaDetalleInfo.PrecioUnitario = vd.PrecioUnitario;
                    ventaDetalleInfo.SubTotal = vd.SubTotal;
                    ventaDetalleInfo.Igv = vd.Igv;
                    ventaDetalleInfo.MontoTotal = vd.MontoTotal;
                    ventaDetalleInfo.Placa = vd.Placa;
                    ventaDetalleInfo.UsuarioCreacionId = usuarioInfo.UsuarioId;

                    new Negocio.Venta().InsertarDetalle(ventaDetalleInfo);
                }
                #endregion

                #region Tabla Movimientos
                var movimientosInfo = new MovimientosInfo();
                movimientosInfo.OperacionId = ventaId;
                movimientosInfo.TipoMovimientoId = Constantes.TipoMovimientoVenta;
                movimientosInfo.FechaOperacion = DateTime.Now;
                movimientosInfo.Glosa = String.Empty;
                movimientosInfo.UsuarioCreacionId = usuarioInfo.UsuarioId;

                new Negocio.Movimientos().InsertarMovimientos(movimientosInfo);
                #endregion

                var almacenId = 0;
                switch (usuarioInfo.TipoNegocioId)
                {
                    case 1:
                        almacenId = 1;
                        break;
                    case 2:
                        almacenId = 2;
                        break;
                    case 3:
                        almacenId = 3;
                        break;
                    case 4:
                        almacenId = 4;
                        break;
                    default:
                        break;
                }

                #region Actualizar Inventario
                foreach (var vd in ventaDetalleListaInfo)
                {
                    var inventarioOrigenInfo = new InventarioInfo();
                    inventarioOrigenInfo.AlmacenId = almacenId; // Convert.ToInt32(ddlAlmacenOrigen.SelectedValue);
                    inventarioOrigenInfo.ProductoId = vd.ProductoId;
                    inventarioOrigenInfo.InventarioActual = (-1) * vd.Cantidad ; //se reduce el stock
                    inventarioOrigenInfo.TipoNegocioId = usuarioInfo.TipoNegocioId;
                    inventarioOrigenInfo.UsuarioModificacionId = usuarioInfo.UsuarioId;

                    var inventarioOrigenId = new Negocio.Inventario().Actualizar(inventarioOrigenInfo);
                }
                #endregion

                // Negocio.Helper.ActualizarColumnasTabla("Cliente", new string[] { "Direccion" }, new string[] { "" }, new string[] { "ClienteId" }, new string[] { ventaInfo.ClienteId.ToString() });

                HttpContext.Current.Session.Remove("VentaDetalle");

                mensaje = ventaId + "@" + "El Comprobante " + (serie + "-" + numeroComprobante) + " se generó correctamente.";
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaPresentacion);
                mensaje = "-1";
                mensaje = "@" + (rethrow ? ex.Message : Constantes.ExcepcionPoliticaPresentacion);
            }
            return mensaje;
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
                //facturaSBDetalle.Append("<table style=\"width: 100%; height: 600px; border-collapse: collapse; border: 1px solid #0b44e9;\">");
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
                var codigoQR = ventaInfo.NumeroDocumento + "|" + ventaInfo.TipoComprobante + "|" + ventaInfo.NumeroSerie + "|" +
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

                // Negocio.Helper.ActualizarColumnasTabla("Venta", new string[] {"ComprobanteImpreso"}, new string[] { comprobanteImpreso }, new string[]{"VentaId"}, new string[]{ventaInfo.VentaId.ToString()} );
                Negocio.Helper.ActualizarColumnasTabla("Venta", new string[] { "ComprobanteImpreso" }, new string[] { nombreArchivoFactura }, new string[] { "VentaId" }, new string[] { ventaInfo.VentaId.ToString() });

                #region Envío Correo
                //var correos = clienteInfo.Correo.Replace(",", ";");
                //var correoArray = correos.Split(';').ToList();
                //var asunto = "APUFact: Factura Electrónica serie " + ventaInfo.NumeroSerie + " número " + ventaInfo.NumeroComprobante + " del " + ventaInfo.FechaEmision.ToString("dd/MM/yyyy") +
                //             " emitida por " + empresaInfo.RazonSocial + " para " +
                //             (clienteInfo.TipoPersonaId.Equals(Constantes.TipoPersonaNatural) ? (clienteInfo.Nombres + "," + clienteInfo.ApellidoPaterno + " " + clienteInfo.ApellidoMaterno) : clienteInfo.RazonSocial);
                //Negocio.Email.Enviar(correoArray, correoArray, correoArray, asunto, "Se adjunta el comprobante.", comprobanteImpreso);
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

        protected void grvCliente_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var clienteInfo = ((ClienteInfo)e.Row.DataItem);
                e.Row.Attributes.Add(Constantes.EventoOnClick, "var fila = SeleccionarFila(this); if (typeof GridView_OnClick == 'function'){GridView_OnClick(fila);}");
                //e.Row.Attributes.Add(Constantes.EventoOnDblClick, "var fila = SeleccionarFila(this); if (typeof grvCliente_OnDnlClick == 'function'){grvCliente_OnDnlClick(fila);}");
                //e.Row.Attributes.Add(Constantes.EventoOnDblClick, "var fila = SeleccionarFila(this); if (typeof grvCliente_OnDnlClick == 'function'){grvCliente_OnDnlClick(fila);}");
               // e.Row.Attributes.Add(Constantes.EventoOnDblClick, "ObtenerNombreCliente(" + clienteInfo.TipoPersonaId + ", " + "'" + clienteInfo.Nombres + "'" + ", '" +
                                                                                       // "'" + ");}");
            }
        }
        [WebMethod]
        public static List<ClienteInfo> ObtenerCliente(int clienteId, string codigo)
        {
            var clienteInfo = new Cliente().ListarPaginado(clienteId, 0, 0, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, codigo, 0, 0).Where(x => x.Activo.Equals(1)).ToList();

            return clienteInfo;
        }

        protected void btnEliminarItem_OnClick(object sender, EventArgs e)
        {
            var script = new StringBuilder();

            var lnkEliminar = (LinkButton) sender;
            var ventaDetalleId = Convert.ToInt32(lnkEliminar.CommandArgument);

            var ventaDetalleListaInfo = (List<VentaDetalleInfo>)Session["VentaDetalle"];

            var ventaDetalleEliminarInfo = ventaDetalleListaInfo.SingleOrDefault(i => i.VentaDetalleId.Equals(ventaDetalleId));

            ventaDetalleListaInfo.Remove(ventaDetalleEliminarInfo);

            Session["VentaDetalle"] = ventaDetalleListaInfo;

            grvItem.DataSource = ventaDetalleListaInfo;
            grvItem.DataBind();

            if (ventaDetalleListaInfo.Count > 0)
            {
                grvItem.HeaderRow.Attributes["style"] = "display: none";
                grvItem.UseAccessibleHeader = true;
                grvItem.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            script.Append("CalcularMontoFinal(); LimpiarItem();");
            RegistrarScript(script.ToString(), "CalcularMontoComprobante");
        }


        // https://github.com/FrameworkPeru/ClienteOpenInvoicePeru-net40/blob/master/Clientes%20OpenInvoicePeru/OpenInvoicePeru.ApiClientCSharp/Program.cs
        private static readonly string BaseUrl = ConfigurationManager.AppSettings["APU.Sunat.BaseUrl"];
        private static readonly string UrlSunat = ConfigurationManager.AppSettings["APU.Sunat.UrlSunat"];
        private static readonly string FormatoFecha = ConfigurationManager.AppSettings["APU.Sunat.FormatoFecha"];
        [WebMethod]
        //public static string EnviarSunat(int ventaId)
        //public async void EnviarSunat(int ventaId)
        public static string EnviarSunat(int ventaId)
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
                throw new ApplicationException(responseSendBill.Data.MensajeError);
            }
            string rutaCdr = HostingEnvironment.MapPath("~/Archivos/Facturacion/CDR/" + responseSendBill.Data.NombreArchivo + ".zip");
            File.WriteAllBytes(rutaCdr, Convert.FromBase64String(responseSendBill.Data.TramaZipCdr));

            //Console.WriteLine("Respuesta de SUNAT:");
            //Console.WriteLine(responseSendBill.Data.MensajeRespuesta);
            mensaje = responseSendBill.Data.MensajeRespuesta;
            #endregion

            return mensaje;
        }
    }
}