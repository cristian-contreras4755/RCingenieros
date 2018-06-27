using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Services;
using System.Web.UI.WebControls;
using APU.Entidad;
using APU.Herramientas;
using APU.Negocio;

namespace APU.Presentacion.Maestros
{
    public partial class Proveedor : PaginaBase
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
            var tablaMaestraInfo = new TablaMaestra().Listar(0, Constantes.TablaTamanioPagina).Where(x => x.Activo.Equals(1)).ToList();
            LlenarCombo(ddlNumeroRegistros, tablaMaestraInfo, "NombreCorto", "NombreLargo");
            ddlNumeroRegistros.SelectedValue = "5";

			#region Información Personal
			//var listaEmpresa = new Negocio.Empresa().Listar(0);
			//LlenarCombo(ddlEmpresa, listaEmpresa, "EmpresaId", "RazonSocial");
			//ddlEmpresa.Items.Insert(0, new ListItem(Constantes.Seleccione, Constantes.Seleccione_Value));
			//tablaMaestraInfo = new TablaMaestra().Listar(0, Constantes.TablaTipoPersona).Where(x => x.Activo.Equals(1)).ToList();
			//LlenarCombo(ddlTipoPersona, tablaMaestraInfo, "Codigo", "NombreLargo");
			//ddlTipoPersona.Items.Insert(0, new ListItem(Constantes.Seleccione, Constantes.Seleccione_Value));

			//tablaMaestraInfo = new TablaMaestra().Listar(0, Constantes.TablaEstadoCivil).Where(x => x.Activo.Equals(1)).ToList();
			//LlenarCombo(ddlEstadoCivil, tablaMaestraInfo, "Codigo", "NombreLargo");
			//ddlEstadoCivil.Items.Insert(0, new ListItem(Constantes.Seleccione, Constantes.Seleccione_Value));

			tablaMaestraInfo = new TablaMaestra().Listar(0, Constantes.TablaTipoDocumento).Where(x => x.Activo.Equals(1) && x.Codigo.Equals("6")).ToList();
            LlenarCombo(ddlTipoDocumento, tablaMaestraInfo, "Codigo", "NombreLargo");
            // ddlTipoDocumento.Items.Insert(0, new ListItem(Constantes.Seleccione, Constantes.Seleccione_Value));
            #endregion

            #region Configuración
            var paisInfo = new Pais().Listar(0).ToList();
            LlenarCombo(ddlPais, paisInfo, "PaisId", "Descripcion");
            ddlPais.Items.Insert(0, new ListItem(Constantes.Seleccione, Constantes.Seleccione_Value));
            //var perfilInfo = new Negocio.Perfil().Listar(0).Where(x => x.Activo.Equals(1)).ToList();
            //LlenarCombo(ddlPerfil, perfilInfo, "PerfilId", "Perfil");
            //ddlPerfil.Items.Insert(0, new ListItem(Constantes.Seleccione, Constantes.Seleccione_Value));

            //tablaMaestraInfo = new TablaMaestra().Listar(0, Constantes.TablaCargo).Where(x => x.Activo.Equals(1)).ToList();
            //LlenarCombo(ddlCargo, tablaMaestraInfo, "Codigo", "NombreLargo");
            //ddlCargo.Items.Insert(0, new ListItem(Constantes.Seleccione, Constantes.Seleccione_Value));

            //var empresaInfo = new Negocio.Empresa().Listar(0).Where(x => x.Activo.Equals(1)).ToList();
            //LlenarCombo(ddlEmpresa, empresaInfo, "EmpresaId", "RazonSocial");
            //ddlEmpresa.Items.Insert(0, new ListItem(Constantes.Seleccione, Constantes.Seleccione_Value));

            var ubigeoInfo = new Ubigeo().Listar(0, 0, Constantes.TipoUbigeoDepartamento);
            LlenarCombo(ddlDepartamento, ubigeoInfo, "UbigeoId", "Nombre");
            ddlDepartamento.Items.Insert(0, new ListItem(Constantes.Seleccione, Constantes.Seleccione_Value));
            #endregion
        }
        private void CargarDatos()
        {
            var script = new StringBuilder("");

            grvCliente.DataBind();
            int numeroRegistros = Convert.ToInt16(ddlNumeroRegistros.SelectedValue);
            int numeroPagina = Convert.ToInt16(ViewState["PageIndex"]);

            int indicePagina = numeroPagina == 0 ? 0 : numeroPagina - 1;
            pageIndex = indicePagina + 1;

			var clienteInfoLista = new Negocio.Proveedor().ListarPaginado(0, String.Empty, String.Empty, numeroRegistros, indicePagina);
            grvCliente.DataSource = clienteInfoLista;
            grvCliente.DataBind();

            if (clienteInfoLista.Count > 0)
            {
                grvCliente.HeaderRow.Attributes["style"] = "display: none";
                grvCliente.UseAccessibleHeader = true;
                grvCliente.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            rowCount = clienteInfoLista.Count > 0 ? clienteInfoLista.First().TotalFilas : 0;
            pageCount = CalcPageCount(rowCount);
            ViewState["PageCount"] = pageCount;

            #region Texto del Pie de Página
            if (clienteInfoLista.Count > 0)
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

            //ddlTipoPersona.Attributes.Add("onchange", "return SeleccionarTipoPersona(this.value);");
            ddlPais.Attributes.Add("onchange", "return SeleccionarPais(this.value);");

            ddlDepartamento.Attributes.Add(Constantes.EventoOnChange, "return ObtenerProvincia(this.value);");
            ddlProvincia.Attributes.Add(Constantes.EventoOnChange, "return ObtenerDistrito(this.value);");

            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Seleccione", "var Seleccione = '" + Constantes.Seleccione + "';", true);
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Seleccione_value", "var Seleccione_value = '" + Constantes.Seleccione_Value + "';", true);
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "PaisPeru", "var PaisPeru = '" + Constantes.PaisPeru + "';", true);
        }

        #region WebMethod
        [WebMethod]
        public static ProveedorInfo ObtenerCliente(int clienteId)
        {
            return new Negocio.Proveedor().Listar(clienteId).FirstOrDefault();
        }
        #endregion

        protected void grvCliente_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add(Constantes.EventoOnClick, "var fila = SeleccionarFila(this); if (typeof GridView_OnClick == 'function'){GridView_OnClick(fila);}");
                e.Row.Attributes.Add(Constantes.EventoOnDblClick, "var fila = SeleccionarFila(this); if (typeof grvCliente_OnDnlClick == 'function'){grvCliente_OnDnlClick(fila);}");
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

        protected void btnGuardarCliente_OnClick(object sender, EventArgs e)
        {
            var usuarioInfo = ObtenerUsuarioInfo();

            var script = new StringBuilder(String.Empty);
            var mensaje = new StringBuilder(String.Empty);
            var clienteId = Convert.ToInt32(hdnClienteId.Value);

            #region Datos Cliente
            var clienteInfo = new ProveedorInfo();
            clienteInfo.ProveedorId = Convert.ToInt32(hdnClienteId.Value);
            clienteInfo.Nombre = txtNombres.Text.Trim();
            clienteInfo.Descripcion = txtDescripcion.Text.Trim();
            clienteInfo.TipoDocumentoId = Convert.ToInt32(ddlTipoDocumento.SelectedValue);
            clienteInfo.NumeroDocumento = txtNumeroDocumento.Text.Trim();
            clienteInfo.PaisId = Convert.ToInt32(ddlPais.SelectedValue);
            clienteInfo.DepartamentoId = Convert.ToInt32(ddlDepartamento.SelectedValue);
            clienteInfo.ProvinciaId = Convert.ToInt32(Request.Form[ddlProvincia.UniqueID]);
            clienteInfo.DistritoId = Convert.ToInt32(Request.Form[ddlDistrito.UniqueID]);
            clienteInfo.Ciudad = txtCiudad.Text.Trim();
            clienteInfo.Direccion = txtDireccion.Text.Trim();
            clienteInfo.Telefono = txtTelefono.Text.Trim();
            clienteInfo.Celular = txtCelular.Text.Trim();
            clienteInfo.Fax = txtFax.Text.Trim();
            clienteInfo.Correo = txtCorreo.Text.Trim();
            clienteInfo.Contacto = txtContacto.Text.Trim();
            clienteInfo.Url = txtUrl.Text.Trim();
            clienteInfo.Imagen = String.Empty;
            clienteInfo.Activo = (chkActivo.Checked) ? 1 : 0;
            #endregion

            if (clienteId.Equals(0))
            {
                clienteInfo.UsuarioCreacionId = usuarioInfo.UsuarioId;
                clienteId = new Negocio.Proveedor().Insertar(clienteInfo);
                if (clienteId > 0)
                {
                    script.Append("document.getElementById('hdnClienteId').value = " + clienteId + ";");
                    mensaje.Append("Se registró al proveedor correctamente");
                }
                else
                {
                    mensaje.Append("Ya existe un proveedor registrado con el N° de documento: " + txtNumeroDocumento.Text.Trim());
                }
            }
            else
            {
                clienteInfo.UsuarioModificacionId = usuarioInfo.UsuarioId;
                clienteId = new Negocio.Proveedor().Actualizar(clienteInfo);
                if (clienteId > 0)
                {
                    script.Append("document.getElementById('hdnClienteId').value = " + clienteId + ";");
                    mensaje.Append("Se actualizó al proveedor correctamente");
                }
                else
                {
                    mensaje.Append("Ya existe un proveedor registrado con el N° de documento: " + txtNumeroDocumento.Text.Trim());
                }
            }
            script.Append("MostrarMensaje('" + mensaje + "');");
            script.Append("LimpiarCliente();");
            script.Append("var modalDialog = $find('mpeCliente'); modalDialog.hide();");
            CargarDatos();
            RegistrarScript(script.ToString(), "GuardarCliente");
        }
        [WebMethod]
        public static EmpresaSunatInfo ObtenerEmpresaSunat(string ruc)
        {
            var empresaSunatInfo = Herramientas.Helper.ConsultaSunat(ruc);

            return empresaSunatInfo;
        }
    }
}