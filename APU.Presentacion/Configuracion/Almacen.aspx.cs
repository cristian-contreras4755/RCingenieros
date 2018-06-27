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
    public partial class Almacen : PaginaBase
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            if (!Page.IsPostBack)
            {
                var empresaId = Convert.ToInt32(Request["EmpresaId"]);
                CargarInicial();
                CargarDatos(empresaId);
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

            var empresaInfo = new Negocio.Empresa().Listar(0);
            LlenarCombo(ddlEmpresa, empresaInfo, "EmpresaId", "RazonSocial");
            ddlEmpresa.Items.Insert(0, new ListItem(Constantes.Seleccione, Constantes.Seleccione_Value));
        }
        private void CargarDatos(int empresaId)
        {
            var script = new StringBuilder("");

            grvAlmacen.DataBind();
            int numeroRegistros = Convert.ToInt16(ddlNumeroRegistros.SelectedValue);
            int numeroPagina = Convert.ToInt16(ViewState["PageIndex"]);

            int indicePagina = numeroPagina == 0 ? 0 : numeroPagina - 1;
            pageIndex = indicePagina + 1;

            var almacenInfoLista = new Negocio.Almacen().ListarPaginado(0, empresaId, numeroRegistros, indicePagina);
            grvAlmacen.DataSource = almacenInfoLista;
            grvAlmacen.DataBind();

            if (almacenInfoLista.Count > 0)
            {
                grvAlmacen.HeaderRow.Attributes["style"] = "display: none";
                grvAlmacen.UseAccessibleHeader = true;
                grvAlmacen.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            rowCount = almacenInfoLista.Count > 0 ? almacenInfoLista.First().TotalFilas : 0;
            pageCount = CalcPageCount(rowCount);
            ViewState["PageCount"] = pageCount;

            #region Texto del Pie de Página
            if (almacenInfoLista.Count > 0)
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
            var empresaId = Convert.ToInt32(Request["EmpresaId"]);
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
            CargarDatos(empresaId);
            RefreshPageButtons();
        }
        protected void PageSortingEventHandler(object sender, CommandEventArgs e)
        {
            var empresaId = Convert.ToInt32(Request["EmpresaId"]);
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
            CargarDatos(empresaId);
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
            var empresaId = Convert.ToInt32(Request["EmpresaId"]);
            // tipoFiltro = String.IsNullOrEmpty(Request["filtro"]) ? 0 : Convert.ToInt16(Request["filtro"]);
            ViewState["PageCount"] = null;
            ViewState["PageIndex"] = null;
            CargarDatos(empresaId);
        }

        protected void btnPaginacion_Click(object sender, EventArgs e)
        {
            var empresaId = Convert.ToInt32(Request["EmpresaId"]);
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
            CargarDatos(empresaId);
            RefreshPageButtons();
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            RegistrarEventoCliente(txtBuscar, Constantes.EventoOnKeyUp, "return Buscar();");

            ddlEmpresa.Attributes.Add(Constantes.EventoOnChange, "return ObtenerUsuarioPorEmpresa(this.value);");

            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Seleccione", "var Seleccione = '" + Constantes.Seleccione + "';", true);
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Seleccione_value", "var Seleccione_value = '" + Constantes.Seleccione_Value + "';", true);
        }

        #region WebMethod
        [WebMethod]
        public static AlmacenInfo ObtenerAlmacen(int almacenId)
        {
            return new Negocio.Almacen().Listar(almacenId).FirstOrDefault();
        }
        #endregion

        protected void grvAlmacen_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add(Constantes.EventoOnClick, "var fila = SeleccionarFila(this); if (typeof GridView_OnClick == 'function'){GridView_OnClick(fila);}");
                e.Row.Attributes.Add(Constantes.EventoOnDblClick, "var fila = SeleccionarFila(this); if (typeof grvAlmacen_OnDnlClick == 'function'){grvAlmacen_OnDnlClick(fila);}");
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
        protected void btnGuardarAlmacen_OnClick(object sender, EventArgs e)
        {
            var empresaId = Convert.ToInt32(Request["EmpresaId"]);

            var usuarioInfo = ObtenerUsuarioInfo();
            var script = new StringBuilder(String.Empty);
            var mensaje = String.Empty;
            var almacenId = Convert.ToInt32(hdnAlmacenId.Value);

            #region Datos Almacen
            var almacenInfo = new AlmacenInfo();
            almacenInfo.AlmacenId = Convert.ToInt32(hdnAlmacenId.Value);
            almacenInfo.EmpresaId = Convert.ToInt32(ddlEmpresa.SelectedValue);
            almacenInfo.Codigo = txtCodigo.Text.Trim();
            almacenInfo.Nombre = txtNombre.Text.Trim();
            almacenInfo.Descripcion = txtDescripcion.Text.Trim();
            almacenInfo.Direccion = txtDireccion.Text.Trim();
            almacenInfo.Activo = (chkActivo.Checked) ? 1 : 0;
            #endregion

            if (almacenId.Equals(0))
            {
                almacenInfo.UsuarioCreacionId = usuarioInfo.UsuarioId;
                almacenId = new Negocio.Almacen().Insertar(almacenInfo);
                if (almacenId > 0)
                {
                    script.Append("document.getElementById('hdnAlmacenId').value = " + almacenId + ";");
                    mensaje = "Se registró la Almacen correctamente";
                }
                else
                {
                    mensaje = "Ya existe una Almacen registrado con el nombre: " + txtNombre.Text.Trim();
                }
            }
            else
            {
                almacenInfo.UsuarioModificacionId = usuarioInfo.UsuarioId;
                almacenId = new Negocio.Almacen().Actualizar(almacenInfo);
                if (almacenId > 0)
                {
                    mensaje = "Se actualizó la Almacen correctamente";
                }
                else
                {
                    mensaje = "Ya existe una Almacen registrada con el nombre: " + txtNombre.Text.Trim();
                }
            }
            script.Append("MostrarMensaje('" + mensaje + "');");
            script.Append("LimpiarAlmacen();");
            script.Append("var modalDialog = $find('mpeAlmacen'); modalDialog.hide();");

            CargarDatos(empresaId);
            RegistrarScript(script.ToString(), "GuardarAlmacen");
        }
    }
}