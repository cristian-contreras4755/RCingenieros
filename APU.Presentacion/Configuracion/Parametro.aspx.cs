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
    public partial class Parametro : PaginaBase
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            if (!Page.IsPostBack)
            {
                var tablaId = Convert.ToInt32(Request["TablaId"]);
                CargarInicial();
                //CargarDatos(tablaId);
                CargarDatos(1);
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

            tablaMaestraInfo = new Negocio.TablaMaestra().Listar(0, 0).Where(t=>t.Codigo.Equals("0")).ToList();
            LlenarCombo(ddlTablaMaestra, tablaMaestraInfo, "TablaId", "NombreLargo");
        }
        private void CargarDatos(int tablaId)
        {
            var script = new StringBuilder("");

            grvTablaMaestra.DataBind();
            //int numeroRegistros = Convert.ToInt16(ddlNumeroRegistros.SelectedValue);
            int numeroRegistros = 1000000;
            int numeroPagina = Convert.ToInt16(ViewState["PageIndex"]);

            int indicePagina = numeroPagina == 0 ? 0 : numeroPagina - 1;
            pageIndex = indicePagina + 1;

            var tablaMaestraInfoLista = new TablaMaestra().ListarPaginado(0, tablaId, numeroRegistros, indicePagina).ToList();
            grvTablaMaestra.DataSource = tablaMaestraInfoLista;
            grvTablaMaestra.DataBind();

            if (tablaMaestraInfoLista.Count > 0)
            {
                grvTablaMaestra.HeaderRow.Attributes["style"] = "display: none";
                grvTablaMaestra.UseAccessibleHeader = true;
                grvTablaMaestra.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            rowCount = tablaMaestraInfoLista.Count > 0 ? tablaMaestraInfoLista.First().TotalFilas : 0;
            pageCount = CalcPageCount(rowCount);
            ViewState["PageCount"] = pageCount;

            #region Texto del Pie de Página
            if (tablaMaestraInfoLista.Count > 0)
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
            var tablaId = Convert.ToInt32(Request["TablaId"]);
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
            CargarDatos(tablaId);
            RefreshPageButtons();
        }
        protected void PageSortingEventHandler(object sender, CommandEventArgs e)
        {
            var tablaId = Convert.ToInt32(Request["TablaId"]);
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
            CargarDatos(tablaId);
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
            var tablaId = Convert.ToInt32(Request["TablaId"]);
            ViewState["PageCount"] = null;
            ViewState["PageIndex"] = null;
            CargarDatos(tablaId);
        }

        protected void btnPaginacion_Click(object sender, EventArgs e)
        {
            var tablaId = Convert.ToInt32(Request["TablaId"]);
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
            CargarDatos(tablaId);
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
        public static TablaMaestraInfo ObtenerTablaMaestra(int tablaMaestraId, int tablaId)
        {
            return new Negocio.TablaMaestra().Listar(tablaMaestraId, tablaId).FirstOrDefault();
        }
        #endregion

        protected void grvTablaMaestra_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add(Constantes.EventoOnClick, "var fila = SeleccionarFila(this); if (typeof GridView_OnClick == 'function'){GridView_OnClick(fila);}");
                e.Row.Attributes.Add(Constantes.EventoOnDblClick, "var fila = SeleccionarFila(this); if (typeof grvParametro_OnDnlClick == 'function'){grvParametro_OnDnlClick(fila);}");
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
        protected void btnGuardarTablaMaestra_OnClick(object sender, EventArgs e)
        {
            //var tablaId = Convert.ToInt32(Request["EmpresaId"]);
            var tablaId = Convert.ToInt32(ddlTablaMaestra.SelectedValue);

            var usuarioInfo = ObtenerUsuarioInfo();
            var script = new StringBuilder(String.Empty);
            var mensaje = String.Empty;
            var tablaMaestraId = Convert.ToInt32(hdnTablaMaestraId.Value);

            #region Datos Parametro
            var tablaMaestraInfo = new TablaMaestraInfo();
            tablaMaestraInfo.TablaId = tablaId;
            tablaMaestraInfo.TablaMaestraId = Convert.ToInt32(hdnTablaMaestraId.Value);
            tablaMaestraInfo.Codigo = txtCodigo.Text.Trim();
            tablaMaestraInfo.NombreCorto = txtNombreCorto.Text.Trim();
            tablaMaestraInfo.NombreLargo = txtNombreLargo.Text.Trim();
            tablaMaestraInfo.Editable = 0;
            tablaMaestraInfo.Activo = (chkActivo.Checked) ? 1 : 0;
            #endregion

            if (tablaMaestraId.Equals(0))
            {
                tablaMaestraInfo.UsuarioCreacionId = usuarioInfo.UsuarioId;
                tablaMaestraId = new Negocio.TablaMaestra().Insertar(tablaMaestraInfo);
                if (tablaMaestraId > 0)
                {
                    script.Append("document.getElementById('hdnTablaMaestraId').value = " + tablaMaestraId + ";");
                    mensaje = "Se registró el Parámetro correctamente";
                }
                else
                {
                    mensaje = "Ya existe un Parámetro registrado con el nombre: " + txtNombreLargo.Text.Trim();
                }
            }
            else
            {
                tablaMaestraInfo.UsuarioModificacionId = usuarioInfo.UsuarioId;
                tablaMaestraId = new Negocio.TablaMaestra().Actualizar(tablaMaestraInfo);
                if (tablaMaestraId > 0)
                {
                    mensaje = "Se actualizó el Parámetro correctamente";
                }
                //else
                //{
                //    mensaje = "Ya existe un Parámetro registrado con el nombre: " + txtNombreLargo.Text.Trim();
                //}
            }
            script.Append("MostrarMensaje('" + mensaje + "');");
            script.Append("LimpiarTablaMaestra();");
            script.Append("var modalDialog = $find('mpeTablaMaestra'); modalDialog.hide();");

            CargarDatos(tablaId);
            RegistrarScript(script.ToString(), "GuardarTablaMaestra");
        }

        protected void ddlTablaMaestra_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var tablaId = Convert.ToInt32(Request["ddlTablaMaestra"]);
            CargarDatos(tablaId);
        }
    }
}