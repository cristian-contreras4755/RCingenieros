using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Services;
using System.Web.UI.WebControls;
using APU.Entidad;
using APU.Herramientas;
using APU.Negocio;

namespace APU.Presentacion.Configuracion
{
    public partial class TipoCambio : PaginaBase
    {
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

            tablaMaestraInfo = new TablaMaestra().Listar(0, Constantes.TablaTipoCotizacion).Where(x => x.Activo.Equals(1)).ToList();
            LlenarCombo(ddlTipoCotizacion, tablaMaestraInfo, "Codigo", "NombreCorto");
            ddlTipoCotizacion.Items.Insert(0, new ListItem(Constantes.Seleccione, Constantes.Seleccione_Value));
        }
        private void CargarDatos()
        {
            var script = new StringBuilder("");

            grvTipoCambio.DataBind();
            int numeroRegistros = Convert.ToInt16(ddlNumeroRegistros.SelectedValue);
            int numeroPagina = Convert.ToInt16(ViewState["PageIndex"]);

            int indicePagina = numeroPagina == 0 ? 0 : numeroPagina - 1;
            pageIndex = indicePagina + 1;

            var tipoCambioInfoLista = new Negocio.TipoCambio().ListarPaginado(0, 0, String.Empty, numeroRegistros, indicePagina);
            grvTipoCambio.DataSource = tipoCambioInfoLista;
            grvTipoCambio.DataBind();

            if (tipoCambioInfoLista.Count > 0)
            {
                grvTipoCambio.HeaderRow.Attributes["style"] = "display: none";
                grvTipoCambio.UseAccessibleHeader = true;
                grvTipoCambio.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            rowCount = tipoCambioInfoLista.Count > 0 ? tipoCambioInfoLista.First().TotalFilas : 0;
            pageCount = CalcPageCount(rowCount);
            ViewState["PageCount"] = pageCount;

            #region Texto del Pie de Página
            if (tipoCambioInfoLista.Count > 0)
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

            //ddlEmpresa.Attributes.Add(Constantes.EventoOnChange, "return ObtenerUsuarioPorEmpresa(this.value);");
            //ddlDepartamento.Attributes.Add(Constantes.EventoOnChange, "return ObtenerProvincia(this.value);");
            //ddlProvincia.Attributes.Add(Constantes.EventoOnChange, "return ObtenerDistrito(this.value);");

            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Seleccione", "var Seleccione = '" + Constantes.Seleccione + "';", true);
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Seleccione_value", "var Seleccione_value = '" + Constantes.Seleccione_Value + "';", true);
        }

        #region WebMethod
        [WebMethod]
        public static TipoCambioInfo ObtenerTipoCambio(int tipoCambioId)
        {
            return new Negocio.TipoCambio().Listar(tipoCambioId).FirstOrDefault();
        }
        #endregion

        protected void grvTipoCambio_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add(Constantes.EventoOnClick, "var fila = SeleccionarFila(this); if (typeof GridView_OnClick == 'function'){GridView_OnClick(fila);}");
                e.Row.Attributes.Add(Constantes.EventoOnDblClick, "var fila = SeleccionarFila(this); if (typeof grvTipoCambio_OnDnlClick == 'function'){grvTipoCambio_OnDnlClick(fila);}");
            }
        }

        [WebMethod]
        public static List<UbigeoInfo> ObtenerProvincia(int codigoDepartamento)
        {
            var ubigeoInfo = new Ubigeo().Listar(0, codigoDepartamento, Constantes.TipoUbigeoProvincia);
            return ubigeoInfo.ToList();
        }
        [WebMethod]
        public static List<UbigeoInfo> ObtenerDistrito(int codigoProvincia)
        {
            var ubigeoInfo = new Ubigeo().Listar(0, codigoProvincia, Constantes.TipoUbigeoDistrito);
            return ubigeoInfo.ToList();
        }
        [WebMethod]
        public static List<UsuarioInfo> ObtenerUsuarioPorEmpresa(int empresaId)
        {
            var usuarioInfo = new Usuario().Listar(0, String.Empty, String.Empty, String.Empty, 0, empresaId);
            return usuarioInfo.ToList();
        }
        protected void btnGuardarTipoCambio_OnClick(object sender, EventArgs e)
        {
            var usuarioInfo = ObtenerUsuarioInfo();
            var script = new StringBuilder(String.Empty);
            var mensaje = String.Empty;
            var tipoCambioId = Convert.ToInt32(hdnTipoCambioId.Value);

            #region Datos Tipo Cambio
            var tipoCambioInfo = new TipoCambioInfo();
            tipoCambioInfo.TipoCambioId = Convert.ToInt32(hdnTipoCambioId.Value);
            tipoCambioInfo.Fecha = DateTime.ParseExact(txtFecha.Text.Trim(), "dd/MM/yyyy", null);
            tipoCambioInfo.TipoCotizacionId = Convert.ToInt32(ddlTipoCotizacion.SelectedValue);
            tipoCambioInfo.Compra = Convert.ToDecimal(txtCompra.Text.Trim());
            tipoCambioInfo.Venta = Convert.ToDecimal(txtVenta.Text.Trim());
            #endregion

            if (tipoCambioId.Equals(0))
            {
                tipoCambioInfo.UsuarioCreacionId = usuarioInfo.UsuarioId;
                tipoCambioId = new Negocio.TipoCambio().Insertar(tipoCambioInfo);
                if (tipoCambioId > 0)
                {
                    script.Append("document.getElementById('hdnTipoCambioId').value = " + tipoCambioId + ";");
                    mensaje = "Se registró el Tipo de Cambio correctamente";
                }
                else
                {
                    // mensaje = "Ya existe una Agencia registrado con el nombre: " + txtNombre.Text.Trim();
                }
            }
            else
            {
                tipoCambioInfo.UsuarioModificacionId = usuarioInfo.UsuarioId;
                tipoCambioId = new Negocio.TipoCambio().Actualizar(tipoCambioInfo);
                if (tipoCambioId > 0)
                {
                    mensaje = "Se actualizó el Tipo de Cambio correctamente";
                }
                else
                {
                    // mensaje = "Ya existe una Agencia registrada con el nombre: " + txtNombre.Text.Trim();
                }
            }
            script.Append("MostrarMensaje('" + mensaje + "');");
            script.Append("LimpiarTipoCambio();");
            script.Append("var modalDialog = $find('mpeTipoCambio'); modalDialog.hide();");

            CargarDatos();
            RegistrarScript(script.ToString(), "GuardarTipoCambio");
        }
    }
}