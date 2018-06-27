using System;
using System.Linq;
using System.Text;
using System.Web.Services;
using System.Web.UI.WebControls;
using APU.Entidad;
using APU.Herramientas;
using APU.Negocio;

namespace APU.Presentacion.Seguridad
{
    public partial class Perfil : PaginaBase
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

            var opcionInfo = new Opcion().ListarOpciones(usuarioInfo.PerfilId).Where(o=>o.Activo.Equals(1)).ToList();
            LlenarCombo(ddlOpcionInicio, opcionInfo, "OpcionId", "Url");
            // ddlOpcionInicio

            #region Información Personal
            //tablaMaestraInfo = new TablaMaestra().Listar(0, Constantes.TablaSexo).Where(x => x.Activo.Equals(1)).ToList();
            //LlenarCombo(ddlSexo, tablaMaestraInfo, "Codigo", "NombreLargo");
            //ddlSexo.Items.Insert(0, new ListItem(Constantes.Seleccione, Constantes.Seleccione_Value));

            //tablaMaestraInfo = new TablaMaestra().Listar(0, Constantes.TablaEstadoCivil).Where(x => x.Activo.Equals(1)).ToList();
            //LlenarCombo(ddlEstadoCivil, tablaMaestraInfo, "Codigo", "NombreLargo");
            //ddlEstadoCivil.Items.Insert(0, new ListItem(Constantes.Seleccione, Constantes.Seleccione_Value));

            //tablaMaestraInfo = new TablaMaestra().Listar(0, Constantes.TablaTipoDocumento).Where(x => x.Activo.Equals(1)).ToList();
            //LlenarCombo(ddlTipoDocumento, tablaMaestraInfo, "Codigo", "NombreLargo");
            //ddlTipoDocumento.Items.Insert(0, new ListItem(Constantes.Seleccione, Constantes.Seleccione_Value));
            //#endregion

            //#region Configuración
            //var perfilInfo = new Perfil().Listar(0).Where(x => x.Activo.Equals(1)).ToList();
            //LlenarCombo(ddlPerfil, perfilInfo, "PerfilId", "Perfil");
            //ddlPerfil.Items.Insert(0, new ListItem(Constantes.Seleccione, Constantes.Seleccione_Value));

            //tablaMaestraInfo = new TablaMaestra().Listar(0, Constantes.TablaCargo).Where(x => x.Activo.Equals(1)).ToList();
            //LlenarCombo(ddlCargo, tablaMaestraInfo, "Codigo", "NombreLargo");
            //ddlCargo.Items.Insert(0, new ListItem(Constantes.Seleccione, Constantes.Seleccione_Value));

            //var empresaInfo = new Empresa().Listar(0).Where(x => x.Activo.Equals(1)).ToList();
            //LlenarCombo(ddlEmpresa, empresaInfo, "EmpresaId", "RazonSocial");
            //ddlEmpresa.Items.Insert(0, new ListItem(Constantes.Seleccione, Constantes.Seleccione_Value));

            //var departamentoInfo = new Departamento().Listar(0).Where(x => x.Activo.Equals(1)).ToList();
            //LlenarCombo(ddlDepartamento, departamentoInfo, "DepartamentoId", "Nombre");
            //ddlDepartamento.Items.Insert(0, new ListItem(Constantes.Seleccione, Constantes.Seleccione_Value));
            #endregion
        }
        private void CargarDatos()
        {
            var script = new StringBuilder("");

            grvPerfil.DataBind();
            int numeroRegistros = Convert.ToInt16(ddlNumeroRegistros.SelectedValue);
            int numeroPagina = Convert.ToInt16(ViewState["PageIndex"]);

            int indicePagina = numeroPagina == 0 ? 0 : numeroPagina - 1;
            pageIndex = indicePagina + 1;

            var perfilInfoLista = new Negocio.Perfil().ListarPaginado(0, numeroRegistros, indicePagina);
            grvPerfil.DataSource = perfilInfoLista;
            grvPerfil.DataBind();

            if (perfilInfoLista.Count > 0)
            {
                grvPerfil.HeaderRow.Attributes["style"] = "display: none";
                grvPerfil.UseAccessibleHeader = true;
                grvPerfil.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            rowCount = perfilInfoLista.Count > 0 ? perfilInfoLista.First().TotalFilas : 0;
            pageCount = CalcPageCount(rowCount);
            ViewState["PageCount"] = pageCount;

            #region Texto del Pie de Página
            if (perfilInfoLista.Count > 0)
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
        }

        #region WebMethod
        [WebMethod]
        public static PerfilInfo ObtenerPerfil(int perfilId)
        {
            return new Negocio.Perfil().Listar(perfilId).FirstOrDefault();
        }
        #endregion

        protected void btnGuardarPerfil_Click(object sender, EventArgs e)
        {
            var usuarioInfo = ObtenerUsuarioInfo();
            var script = new StringBuilder(String.Empty);
            var mensaje = String.Empty;
            var perfilId = Convert.ToInt32(hdnPerfilId.Value);

            #region Datos Perfil
            var perfilInfo = new PerfilInfo();
            perfilInfo.PerfilId = Convert.ToInt32(hdnPerfilId.Value);
            perfilInfo.Perfil = txtNombre.Text.Trim();
            perfilInfo.OpcionInicioId = Convert.ToInt32(ddlOpcionInicio.SelectedValue);
            perfilInfo.Activo = (chkActivo.Checked) ? 1 : 0;
            #endregion

            if (perfilId.Equals(0))
            {
                perfilInfo.UsuarioCreacionId = usuarioInfo.UsuarioId;
                perfilId = new Negocio.Perfil().Insertar(perfilInfo);
                if (perfilId > 0)
                {
                    script.Append("document.getElementById('hdnPerfilId').value = " + perfilId + ";");
                    mensaje = "Se registró el perfil correctamente";
                }
                else
                {
                    mensaje = "Ya existe un perfil registrado con el nombre: " + txtNombre.Text.Trim();
                }
            }
            else
            {
                perfilInfo.UsuarioModificacionId = usuarioInfo.UsuarioId;
                perfilId = new Negocio.Perfil().Actualizar(perfilInfo);
                if (perfilId > 0)
                {
                    mensaje = "Se actualizó el perfil correctamente";
                }
                else
                {
                    mensaje = "Ya existe un perfil registrado con el nombre: " + txtNombre.Text.Trim();
                }
            }
            script.Append("MostrarMensaje('" + mensaje + "');");
            script.Append("LimpiarPerfil();");
            script.Append("var modalDialog = $find('mpePerfil'); modalDialog.hide();");

            CargarDatos();
            //RegistrarScript("MostrarMensaje('" + mensaje + "'); LimpiarPerfil();", "GuardarPerfil");
            RegistrarScript(script.ToString(), "GuardarPerfil");
        }

        protected void grvPerfil_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add(Constantes.EventoOnClick, "var fila = SeleccionarFila(this); if (typeof GridView_OnClick == 'function'){GridView_OnClick(fila);}");
                e.Row.Attributes.Add(Constantes.EventoOnDblClick, "var fila = SeleccionarFila(this); if (typeof grvPerfil_OnDnlClick == 'function'){grvPerfil_OnDnlClick(fila);}");
            }
        }
    }
}