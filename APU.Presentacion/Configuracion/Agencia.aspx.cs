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
    public partial class Agencia : PaginaBase
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            if (!Page.IsPostBack)
            {
                var empresaId = Convert.ToInt32(Request["EmpresaId"]);
                var clienteId = Convert.ToInt32(Request["ClienteId"]);
                CargarInicial(empresaId, clienteId);
                CargarDatos(empresaId, clienteId);
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
        private void CargarInicial(int empresaId, int clienteId)
        {
            var scriptCargaInicial = new StringBuilder("");
            var usuarioInfo = ObtenerUsuarioInfo();

            var tablaMaestraInfo = new TablaMaestra().Listar(0, Constantes.TablaTamanioPagina).Where(x => x.Activo.Equals(1)).ToList();
            LlenarCombo(ddlNumeroRegistros, tablaMaestraInfo, "NombreCorto", "NombreLargo");
            ddlNumeroRegistros.SelectedValue = "5";

            if (empresaId > 0)
            {
                var empresaInfo = new Negocio.Empresa().Listar(empresaId);
                LlenarCombo(ddlEmpresa, empresaInfo, "EmpresaId", "RazonSocial");
                ddlEmpresa.Items.Insert(0, new ListItem(Constantes.Seleccione, Constantes.Seleccione_Value));
                ddlEmpresa.SelectedValue = empresaId.ToString();
                scriptCargaInicial.Append("document.getElementById('" + ddlEmpresa.ClientID + "').value = " + empresaId + ";");
                scriptCargaInicial.Append("document.getElementById('trCliente').style.display = 'none';");
            }
            else
            {
                if (clienteId > 0)
                {
                    var clienteInfo = new Negocio.Cliente().Listar(clienteId);
                    LlenarCombo(ddlCliente, clienteInfo, "ClienteId", "Cliente");
                    ddlCliente.Items.Insert(0, new ListItem(Constantes.Seleccione, Constantes.Seleccione_Value));
                    ddlCliente.SelectedValue = clienteId.ToString();
                    scriptCargaInicial.Append("document.getElementById('" + ddlCliente.ClientID + "').value = " + clienteId + ";");
                    scriptCargaInicial.Append("document.getElementById('trEmpresa').style.display = 'none';");
                }
                else
                {
                    var empresaInfo = new Negocio.Empresa().Listar(0);
                    LlenarCombo(ddlEmpresa, empresaInfo, "EmpresaId", "RazonSocial");
                    ddlEmpresa.Items.Insert(0, new ListItem(Constantes.Seleccione, Constantes.Seleccione_Value));

                    var clienteInfo = new Negocio.Cliente().Listar(0).Where(c=>c.TipoPersonaId.Equals(Constantes.TipoPersonaJuridica)).ToList();
                    LlenarCombo(ddlCliente, clienteInfo, "ClienteId", "Cliente");
                    ddlCliente.Items.Insert(0, new ListItem(Constantes.Seleccione, Constantes.Seleccione_Value));
                }
            }

            var paisInfo = new Pais().Listar(0).ToList();
            LlenarCombo(ddlPais, paisInfo, "PaisId", "Descripcion");
            ddlPais.Items.Insert(0, new ListItem(Constantes.Seleccione, Constantes.Seleccione_Value));

            var ubigeoInfo = new Ubigeo().Listar(0, 0, Constantes.TipoUbigeoDepartamento);
            LlenarCombo(ddlDepartamento, ubigeoInfo, "UbigeoId", "Nombre");
            ddlDepartamento.Items.Insert(0, new ListItem(Constantes.Seleccione, Constantes.Seleccione_Value));

            RegistrarScript(scriptCargaInicial.ToString(), "CargaInicial");
        }
        private void CargarDatos(int empresaId, int clienteId)
        {
            var script = new StringBuilder("");

            grvAgencia.DataBind();
            int numeroRegistros = Convert.ToInt16(ddlNumeroRegistros.SelectedValue);
            int numeroPagina = Convert.ToInt16(ViewState["PageIndex"]);

            int indicePagina = numeroPagina == 0 ? 0 : numeroPagina - 1;
            pageIndex = indicePagina + 1;

            var agenciaInfoLista = new Negocio.Agencia().ListarPaginado(0, empresaId, clienteId, numeroRegistros, indicePagina);
            grvAgencia.DataSource = agenciaInfoLista;
            grvAgencia.DataBind();

            if (agenciaInfoLista.Count > 0)
            {
                grvAgencia.HeaderRow.Attributes["style"] = "display: none";
                grvAgencia.UseAccessibleHeader = true;
                grvAgencia.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            rowCount = agenciaInfoLista.Count > 0 ? agenciaInfoLista.First().TotalFilas : 0;
            pageCount = CalcPageCount(rowCount);
            ViewState["PageCount"] = pageCount;

            #region Texto del Pie de Página
            if (agenciaInfoLista.Count > 0)
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
            var clienteId = Convert.ToInt32(Request["ClienteId"]);
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
            CargarDatos(empresaId, clienteId);
            RefreshPageButtons();
        }
        protected void PageSortingEventHandler(object sender, CommandEventArgs e)
        {
            var empresaId = Convert.ToInt32(Request["EmpresaId"]);
            var clienteId = Convert.ToInt32(Request["ClienteId"]);
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
            CargarDatos(empresaId, clienteId);
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
            var clienteId = Convert.ToInt32(Request["ClienteId"]);
            // tipoFiltro = String.IsNullOrEmpty(Request["filtro"]) ? 0 : Convert.ToInt16(Request["filtro"]);
            ViewState["PageCount"] = null;
            ViewState["PageIndex"] = null;
            CargarDatos(empresaId, clienteId);
        }

        protected void btnPaginacion_Click(object sender, EventArgs e)
        {
            var empresaId = Convert.ToInt32(Request["EmpresaId"]);
            var clienteId = Convert.ToInt32(Request["ClienteId"]);
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
            CargarDatos(empresaId, clienteId);
            RefreshPageButtons();
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            RegistrarEventoCliente(txtBuscar, Constantes.EventoOnKeyUp, "return Buscar();");

            ddlEmpresa.Attributes.Add(Constantes.EventoOnChange, "return ObtenerUsuarioPorEmpresa(this.value);");
            ddlCliente.Attributes.Add(Constantes.EventoOnChange, "return SeleccionarCliente(this.value);");
            ddlDepartamento.Attributes.Add(Constantes.EventoOnChange, "return ObtenerProvincia(this.value);");
            ddlProvincia.Attributes.Add(Constantes.EventoOnChange, "return ObtenerDistrito(this.value);");

            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Seleccione", "var Seleccione = '" + Constantes.Seleccione + "';", true);
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Seleccione_value", "var Seleccione_value = '" + Constantes.Seleccione_Value + "';", true);
        }

        #region WebMethod
        [WebMethod]
        public static AgenciaInfo ObtenerAgencia(int agenciaId)
        {
            return new Negocio.Agencia().Listar(agenciaId).FirstOrDefault();
        }
        #endregion

        protected void grvAgencia_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add(Constantes.EventoOnClick, "var fila = SeleccionarFila(this); if (typeof GridView_OnClick == 'function'){GridView_OnClick(fila);}");
                e.Row.Attributes.Add(Constantes.EventoOnDblClick, "var fila = SeleccionarFila(this); if (typeof grvAgencia_OnDnlClick == 'function'){grvAgencia_OnDnlClick(fila);}");
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
        protected void btnGuardarAgencia_OnClick(object sender, EventArgs e)
        {
            var empresaId = Convert.ToInt32(Request["EmpresaId"]);
            var clienteId = Convert.ToInt32(Request["ClienteId"]);

            var usuarioInfo = ObtenerUsuarioInfo();
            var script = new StringBuilder(String.Empty);
            var mensaje = String.Empty;
            var agenciaId = Convert.ToInt32(hdnAgenciaId.Value);

            #region Datos Agencia
            var agenciaInfo = new AgenciaInfo();
            agenciaInfo.AgenciaId = Convert.ToInt32(hdnAgenciaId.Value);
            agenciaInfo.EmpresaId = Convert.ToInt32(ddlEmpresa.SelectedValue);
            agenciaInfo.Nombre = txtNombre.Text.Trim();
            agenciaInfo.Descripcion = txtDescripcion.Text.Trim();
            agenciaInfo.PaisId = Convert.ToInt32(ddlPais.SelectedValue);
            agenciaInfo.DepartamentoId = Convert.ToInt32(ddlDepartamento.SelectedValue);
            agenciaInfo.ProvinciaId = Convert.ToInt32(Request.Form[ddlProvincia.UniqueID]);
            agenciaInfo.DistritoId = Convert.ToInt32(Request.Form[ddlDistrito.UniqueID]);
            agenciaInfo.Ciudad = txtCiudad.Text.Trim();
            agenciaInfo.Direccion = txtDireccion.Text.Trim();
            agenciaInfo.ContactoId = Convert.ToInt32(Request.Form[ddlContacto.UniqueID]);
            agenciaInfo.Activo = (chkActivo.Checked) ? 1 : 0;
            #endregion

            if (agenciaId.Equals(0))
            {
                agenciaInfo.UsuarioCreacionId = usuarioInfo.UsuarioId;
                agenciaId = new Negocio.Agencia().Insertar(agenciaInfo);
                if (agenciaId > 0)
                {
                    script.Append("document.getElementById('hdnAgenciaId').value = " + agenciaId + ";");
                    mensaje = "Se registró la Agencia correctamente";
                }
                else
                {
                    mensaje = "Ya existe una Agencia registrado con el nombre: " + txtNombre.Text.Trim();
                }
            }
            else
            {
                agenciaInfo.UsuarioModificacionId = usuarioInfo.UsuarioId;
                agenciaId = new Negocio.Agencia().Actualizar(agenciaInfo);
                if (agenciaId > 0)
                {
                    mensaje = "Se actualizó la Agencia correctamente";
                }
                else
                {
                    mensaje = "Ya existe una Agencia registrada con el nombre: " + txtNombre.Text.Trim();
                }
            }
            script.Append("MostrarMensaje('" + mensaje + "');");
            script.Append("LimpiarAgencia();");
            script.Append("var modalDialog = $find('mpeAgencia'); modalDialog.hide();");

            CargarDatos(empresaId, clienteId);
            RegistrarScript(script.ToString(), "GuardarAgencia");
        }
    }
}