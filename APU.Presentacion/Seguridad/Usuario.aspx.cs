using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Services;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using APU.Entidad;
using APU.Herramientas;
using APU.Negocio;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace APU.Presentacion.Seguridad
{
    public partial class Usuario : PaginaBase
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            fuUsuario.UploadedComplete += new EventHandler<AsyncFileUploadEventArgs>(fuUsuario_UploadedComplete);
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
            tablaMaestraInfo = new TablaMaestra().Listar(0, Constantes.TablaSexo).Where(x => x.Activo.Equals(1)).ToList();
            LlenarCombo(ddlSexo, tablaMaestraInfo, "Codigo", "NombreLargo");
            ddlSexo.Items.Insert(0, new ListItem(Constantes.Seleccione, Constantes.Seleccione_Value));

            tablaMaestraInfo = new TablaMaestra().Listar(0, Constantes.TablaEstadoCivil).Where(x => x.Activo.Equals(1)).ToList();
            LlenarCombo(ddlEstadoCivil, tablaMaestraInfo, "Codigo", "NombreLargo");
            ddlEstadoCivil.Items.Insert(0, new ListItem(Constantes.Seleccione, Constantes.Seleccione_Value));

            tablaMaestraInfo = new TablaMaestra().Listar(0, Constantes.TablaTipoDocumento).Where(x => x.Activo.Equals(1)).ToList();
            LlenarCombo(ddlTipoDocumento, tablaMaestraInfo, "Codigo", "NombreLargo");
            ddlTipoDocumento.Items.Insert(0, new ListItem(Constantes.Seleccione, Constantes.Seleccione_Value));
            #endregion

            #region Configuración
            var perfilInfo = new Negocio.Perfil().Listar(0).Where(x => x.Activo.Equals(1)).ToList();
            LlenarCombo(ddlPerfil, perfilInfo, "PerfilId", "Perfil");
            ddlPerfil.Items.Insert(0, new ListItem(Constantes.Seleccione, Constantes.Seleccione_Value));

            tablaMaestraInfo = new TablaMaestra().Listar(0, Constantes.TablaCargo).Where(x => x.Activo.Equals(1)).ToList();
            LlenarCombo(ddlCargo, tablaMaestraInfo, "Codigo", "NombreLargo");
            ddlCargo.Items.Insert(0, new ListItem(Constantes.Seleccione, Constantes.Seleccione_Value));

            var empresaInfo = new Empresa().Listar(0).Where(x => x.Activo.Equals(1)).ToList();
            LlenarCombo(ddlEmpresa, empresaInfo, "EmpresaId", "RazonSocial");
            ddlEmpresa.Items.Insert(0, new ListItem(Constantes.Seleccione, Constantes.Seleccione_Value));

            tablaMaestraInfo = new TablaMaestra().Listar(0, Constantes.TablaTipoNegocio).Where(x => x.Activo.Equals(1)).ToList();
            LlenarCombo(ddlTipoNegocio, tablaMaestraInfo, "Codigo", "NombreLargo");
            ddlTipoNegocio.Items.Insert(0, new ListItem(Constantes.Seleccione, Constantes.Seleccione_Value));

            var departamentoInfo = new Departamento().Listar(0).Where(x => x.Activo.Equals(1)).ToList();
            LlenarCombo(ddlDepartamento, departamentoInfo, "DepartamentoId", "Nombre");
            ddlDepartamento.Items.Insert(0, new ListItem(Constantes.Seleccione, Constantes.Seleccione_Value));
            #endregion
        }
        private void CargarDatos()
        {
            var script = new StringBuilder("");

            grvUsuario.DataBind();
            int numeroRegistros = Convert.ToInt16(ddlNumeroRegistros.SelectedValue);
            int numeroPagina = Convert.ToInt16(ViewState["PageIndex"]);

            int indicePagina = numeroPagina == 0 ? 0 : numeroPagina - 1;
            pageIndex = indicePagina + 1;

            var usuarioInfoLista = new Negocio.Usuario().ListarPaginado(0, String.Empty, String.Empty, String.Empty, 0, numeroRegistros, indicePagina);
            grvUsuario.DataSource = usuarioInfoLista;
            grvUsuario.DataBind();

            if (usuarioInfoLista.Count > 0)
            {
                grvUsuario.HeaderRow.Attributes["style"] = "display: none";
                grvUsuario.UseAccessibleHeader = true;
                grvUsuario.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            rowCount = usuarioInfoLista.Count > 0 ? usuarioInfoLista.First().TotalFilas : 0;
            pageCount = CalcPageCount(rowCount);
            ViewState["PageCount"] = pageCount;

            #region Texto del Pie de Página
            if (usuarioInfoLista.Count > 0)
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
            RegistrarEventoCliente(ddlEmpresa, Constantes.EventoOnChange, "return SeleccionarEmpresa();");
            RegistrarEventoCliente(ddlAgencia, Constantes.EventoOnChange, "return SeleccionarAgencia();");

            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Seleccione", "var Seleccione = '" + Constantes.Seleccione + "';", true);
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Seleccione_value", "var Seleccione_value = '" + Constantes.Seleccione_Value + "';", true);

            txtContrasenia.Attributes.Add("type", "password");
        }

        #region WebMethod
        [WebMethod]
        public static UsuarioInfo ObtenerUsuario(int usuarioId)
        {
            return new Negocio.Usuario().Listar(usuarioId, String.Empty, String.Empty, String.Empty, 0, 0).FirstOrDefault();
        }
        [WebMethod]
        public static string GuardarUsuario(int usuarioId, string nombres, string apellidoPaterno, string apellidoMaterno, int sexoId, int estadoCivilId, string correo, string telefono, string celular,
                                            int tipoDocumentoId, string numeroDocumento, string codigo, string login, string contrasenia, int perfilId, int empresaId, int departamentoId,
                                            int cargoId, string correoTrabajo, string telefonoTrabajo,
            int activo)
        {
            var mensaje = String.Empty;

            #region Datos Usuario
            var usuarioInfo = new UsuarioInfo();
            usuarioInfo.UsuarioId = usuarioId;
            usuarioInfo.Nombres = nombres;
            usuarioInfo.ApellidoPaterno = apellidoPaterno;
            usuarioInfo.ApellidoMaterno = apellidoMaterno;
            usuarioInfo.SexoId = sexoId;
            usuarioInfo.EstadoCivilId = estadoCivilId;
            usuarioInfo.Correo = correo;
            usuarioInfo.Telefono = telefono;
            usuarioInfo.Celular = celular;
            usuarioInfo.TipoDocumentoId = tipoDocumentoId;
            usuarioInfo.NumeroDocumento = numeroDocumento;

            usuarioInfo.Codigo = codigo;
            usuarioInfo.Login = login;
            usuarioInfo.Password = contrasenia;
            usuarioInfo.PerfilId = perfilId;
            usuarioInfo.EmpresaId = empresaId;
            usuarioInfo.DepartamentoId = departamentoId;
            usuarioInfo.Foto = String.Empty;
            usuarioInfo.CargoId = cargoId;
            usuarioInfo.CorreoTrabajo = correoTrabajo;
            usuarioInfo.TelefonoTrabajo = telefonoTrabajo;
            usuarioInfo.Activo = activo;
            #endregion

            if (usuarioId.Equals(0))
            {
                usuarioId = new Negocio.Usuario().Insertar(usuarioInfo);
                if (usuarioId < 0)
                {
                    mensaje = usuarioId + "@" + "Ya existe un cliente registrado con el N° de documento: " + numeroDocumento;
                    return mensaje;
                }
            }
            else
            {
                usuarioId = new Negocio.Usuario().Actualizar(usuarioInfo);
                if (usuarioId < 0)
                {
                    mensaje = usuarioId + "@" + "Ya existe un cliente registrado con el N° de documento: " + numeroDocumento;
                    return mensaje;
                }
            }
            mensaje = usuarioId + "@" + String.Empty;
            return mensaje;
        }
        #endregion

        void fuUsuario_UploadedComplete(object sender, AsyncFileUploadEventArgs e)
        {
            var mensaje = String.Empty;
            var script = new StringBuilder("");
            try
            {
                if (fuUsuario.HasFile)
                {
                    var fechaHora = DateTime.Now.ToString("ddMMyyyyhhmmss");
                    string nombreArchivo = Path.GetFileNameWithoutExtension(e.FileName);
                    string extension = Path.GetExtension(e.FileName);
                    string rutaGuardar = MapPath("~/Archivos/Imagenes/Usuario/" + txtLogin.Text.Trim() + "/" + nombreArchivo + "_" + fechaHora + extension);
                    var directorio = MapPath("~/Archivos/Imagenes/Usuario/" + txtLogin.Text.Trim());

                    if (!Directory.Exists(directorio))
                    {
                        Directory.CreateDirectory(directorio);
                    }

                    fuUsuario.SaveAs(rutaGuardar);
                    hdnUsuarioImagen.Value = rutaGuardar;

                    Session["sUsuarioImagen"] = "~/Archivos/Imagenes/Usuario/" + txtLogin.Text.Trim() + "/" + nombreArchivo + "_" + fechaHora + extension;

                    script.Append("document.getElementById('" + hdnUsuarioImagen.ClientID + "').value = '" + rutaGuardar + "';");
                }
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaPresentacion);
                mensaje = rethrow ? ex.Message : Constantes.ExcepcionPoliticaPresentacion;
                script.Append("MostrarMensaje('" + mensaje + "');");
            }
            RegistrarScript(script.ToString(), "CargaLogo");
        }

        protected void btnGuardarUsuario_Click_(object sender, EventArgs e)
        {
            var mensaje = new StringBuilder(String.Empty);
            var usuarioId = Convert.ToInt32(hdnUsuarioId.Value);

            #region Datos Usuario
            var usuarioInfo = new UsuarioInfo();
            usuarioInfo.UsuarioId = Convert.ToInt32(hdnUsuarioId.Value);
            usuarioInfo.Nombres = txtNombres.Text.Trim();
            usuarioInfo.ApellidoPaterno = txtApellidoPaterno.Text.Trim();
            usuarioInfo.ApellidoMaterno = txtApellidoMaterno.Text.Trim();
            usuarioInfo.SexoId = Convert.ToInt32(ddlSexo.SelectedValue);
            usuarioInfo.EstadoCivilId = Convert.ToInt32(ddlEstadoCivil.SelectedValue);
            usuarioInfo.Correo = txtCorreo.Text.Trim();
            usuarioInfo.Telefono = txtTelefono.Text.Trim();
            usuarioInfo.Celular = txtCelular.Text.Trim();
            usuarioInfo.TipoDocumentoId = Convert.ToInt32(ddlTipoDocumento.SelectedValue);
            usuarioInfo.NumeroDocumento = txtNumeroDocumento.Text.Trim();

            usuarioInfo.Codigo = txtCodigo.Text.Trim();
            usuarioInfo.Login = txtLogin.Text.Trim();
            usuarioInfo.Password = txtContrasenia.Text.Trim();
            usuarioInfo.PerfilId = Convert.ToInt32(ddlPerfil.SelectedValue);
            usuarioInfo.EmpresaId = Convert.ToInt32(ddlEmpresa.SelectedValue);
            usuarioInfo.DepartamentoId = Convert.ToInt32(ddlDepartamento.SelectedValue);
            usuarioInfo.Foto = String.Empty;
            usuarioInfo.CargoId = Convert.ToInt32(ddlCargo.SelectedValue);
            usuarioInfo.CorreoTrabajo = txtCorreoTrabajo.Text.Trim();
            usuarioInfo.TelefonoTrabajo = txtTelefonoTrabajo.Text.Trim();
            usuarioInfo.Activo = (chkActivo.Checked) ? 1 : 0;
            #endregion

            if (usuarioId.Equals(0))
            {
                usuarioId = new Negocio.Usuario().Insertar(usuarioInfo);
                if (usuarioId > 0)
                {
                    mensaje.Append("document.getElementById('hdnUsuarioId').value = " + usuarioId);
                    mensaje.Append(usuarioId + "@" + "Se registró al usuario correctamente");
                }
                else
                {
                    mensaje.Append(usuarioId + "@" + "Ya existe un usuario registrado con el Código: " + txtNumeroDocumento.Text.Trim());
                }
            }
            else
            {
                usuarioId = new Negocio.Usuario().Actualizar(usuarioInfo);
                if (usuarioId > 0)
                {
                    mensaje.Append("document.getElementById('hdnUsuarioId').value = " + usuarioId);
                    mensaje.Append(usuarioId + "@" + "Se registró al usuario correctamente");
                }
                else
                {
                    mensaje.Append(usuarioId + "@" + "Ya existe un usuario registrado con el Código: " + txtNumeroDocumento.Text.Trim());
                }
            }
            CargarDatos();
            RegistrarScript("MostrarMensaje('" + mensaje + "'); LimpiarUsuario();", "GuardarUsuario");
        }

        protected void grvUsuario_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add(Constantes.EventoOnClick, "var fila = SeleccionarFila(this); if (typeof GridView_OnClick == 'function'){GridView_OnClick(fila);}");
                e.Row.Attributes.Add(Constantes.EventoOnDblClick, "var fila = SeleccionarFila(this); if (typeof grvUsuario_OnDnlClick == 'function'){grvUsuario_OnDnlClick(fila);}");
            }
        }

        protected void btnGuardarUsuario_OnClick_(object sender, EventArgs e)
        {
            try
            {
                var usuarioSesionInfo = ObtenerUsuarioInfo();

                var script = new StringBuilder(String.Empty);
                var mensaje = new StringBuilder(String.Empty);
                var usuarioId = Convert.ToInt32(hdnUsuarioId.Value);

                #region Datos Usuario
                var usuarioInfo = new UsuarioInfo();
                usuarioInfo.UsuarioId = Convert.ToInt32(hdnUsuarioId.Value);
                usuarioInfo.Nombres = txtNombres.Text.Trim();
                usuarioInfo.ApellidoPaterno = txtApellidoPaterno.Text.Trim();
                usuarioInfo.ApellidoMaterno = txtApellidoMaterno.Text.Trim();
                usuarioInfo.SexoId = Convert.ToInt32(ddlSexo.SelectedValue);
                usuarioInfo.EstadoCivilId = Convert.ToInt32(ddlEstadoCivil.SelectedValue);
                usuarioInfo.Correo = txtCorreo.Text.Trim();
                usuarioInfo.Telefono = txtTelefono.Text.Trim();
                usuarioInfo.Celular = txtCelular.Text.Trim();
                usuarioInfo.TipoDocumentoId = Convert.ToInt32(ddlTipoDocumento.SelectedValue);
                usuarioInfo.NumeroDocumento = txtNumeroDocumento.Text.Trim();

                usuarioInfo.Codigo = txtCodigo.Text.Trim();
                usuarioInfo.Login = txtLogin.Text.Trim();
                usuarioInfo.Password = txtContrasenia.Text.Trim();
                usuarioInfo.PerfilId = Convert.ToInt32(ddlPerfil.SelectedValue);
                usuarioInfo.EmpresaId = Convert.ToInt32(ddlEmpresa.SelectedValue);

                usuarioInfo.DepartamentoId = Convert.ToInt32(ddlDepartamento.SelectedValue);

                var rutaFoto = Server.MapPath("~/Archivos/Imagenes/Usuario/") + txtLogin.Text.Trim();

                if (fuFotoUsuario.HasFile)
                {
                    if (!Directory.Exists(rutaFoto))
                    {
                        Directory.CreateDirectory(rutaFoto);
                    }
                    var nombreFoto = fuFotoUsuario.FileName;
                    nombreFoto = nombreFoto.Substring(0, nombreFoto.Length - 4);
                    var extensionFoto = fuFotoUsuario.FileName.Substring(nombreFoto.Length, 4);

                    var nombreArchivo = nombreFoto + DateTime.Now.ToString("ddMMyyyyhhmmss") + extensionFoto;
                    fuFotoUsuario.SaveAs(rutaFoto + "\\" + nombreArchivo);
                    usuarioInfo.Foto = "~/Archivos/Imagenes/Usuario/" + txtLogin.Text.Trim() + "/" + nombreArchivo;

                    if (usuarioSesionInfo.UsuarioId.Equals(usuarioInfo.UsuarioId))
                    {
                        usuarioSesionInfo.Foto = usuarioInfo.Foto;
                        Session["UsuarioInfo"] = usuarioSesionInfo;
                    }
                }
                else
                {
                    usuarioInfo.Foto = hdnUsuarioImagen.Value;
                }

                usuarioInfo.CargoId = Convert.ToInt32(ddlCargo.SelectedValue);
                usuarioInfo.CorreoTrabajo = txtCorreoTrabajo.Text.Trim();
                usuarioInfo.TelefonoTrabajo = txtTelefonoTrabajo.Text.Trim();
                usuarioInfo.Activo = (chkActivo.Checked) ? 1 : 0;
                #endregion

                if (usuarioId.Equals(0))
                {
                    usuarioInfo.UsuarioCreacionId = usuarioSesionInfo.UsuarioId;
                    usuarioId = new Negocio.Usuario().Insertar(usuarioInfo);
                    if (usuarioId > 0)
                    {
                        script.Append("document.getElementById('hdnUsuarioId').value = " + usuarioId + ";");
                        mensaje.Append("Se registró al usuario correctamente");

                        //script.Append("MostrarMensaje('" + mensaje + "');");
                        script.Append("LimpiarUsuario();");
                        script.Append("var modalDialog = $find('mpeUsuario'); modalDialog.hide();");
                        CargarDatos();
                        //RegistrarScript(script.ToString(), "GuardarUsuario");
                    }
                    else
                    {
                        mensaje.Append("Ya existe un usuario registrado con el Login: " + txtCodigo.Text.Trim());
                        script.Append("document.getElementById('" + txtLogin.ClientID + "').focus();");

                        //script.Append("MostrarMensaje('" + mensaje + "');");
                        //RegistrarScript(script.ToString(), "GuardarUsuario");
                    }
                }
                else
                {
                    usuarioInfo.UsuarioModificacionId = usuarioSesionInfo.UsuarioId;
                    usuarioId = new Negocio.Usuario().Actualizar(usuarioInfo);
                    if (usuarioId > 0)
                    {
                        script.Append("document.getElementById('hdnUsuarioId').value = " + usuarioId + ";");
                        mensaje.Append("Se registró al usuario correctamente");

                        //script.Append("MostrarMensaje('" + mensaje + "');");
                        script.Append("LimpiarUsuario();");
                        script.Append("var modalDialog = $find('mpeUsuario'); modalDialog.hide();");
                        CargarDatos();
                        //RegistrarScript(script.ToString(), "GuardarUsuario");
                    }
                    else
                    {
                        mensaje.Append("Ya existe un usuario registrado con el Login: " + txtCodigo.Text.Trim());
                        script.Append("document.getElementById('" + txtLogin.ClientID + "').focus();");

                        //script.Append("MostrarMensaje('" + mensaje + "');");
                        //RegistrarScript(script.ToString(), "GuardarUsuario");
                    }
                }
                script.Append("MostrarMensaje('" + mensaje + "');");
                RegistrarScript(script.ToString(), "GuardarUsuario");
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaPresentacion);
                var mensajeError = rethrow ? ex.Message : Constantes.ExcepcionPoliticaPresentacion;
                RegistrarScript("MostrarMensaje('" + mensajeError + "');", "GuardarUsuario");
                //mensaje = rethrow ? ex.Message : Constantes.ExcepcionPoliticaPresentacion;
                //script.Append("MostrarMensaje('" + mensaje + "');");
            }
            //script.Append("MostrarMensaje('" + mensaje + "');");
            //script.Append("LimpiarUsuario();");
            //script.Append("var modalDialog = $find('mpeUsuario'); modalDialog.hide();");
            //CargarDatos();
            //RegistrarScript(script.ToString(), "GuardarUsuario");
        }
        [WebMethod]
        public static List<AgenciaInfo> ObtenerAgencia(int empresaId)
        {
            return new Agencia().Listar(0).Where(a => a.EmpresaId.Equals(empresaId)).ToList();
        }

        protected void btnGuardarUsuario_OnClick(object sender, EventArgs e)
        {
            var script = new StringBuilder(String.Empty);
            var mensaje = new StringBuilder(String.Empty);
            try
            {
                var usuarioSesionInfo = ObtenerUsuarioInfo();

                
                var usuarioId = Convert.ToInt32(hdnUsuarioId.Value);

                #region Datos Usuario
                var usuarioInfo = new UsuarioInfo();
                usuarioInfo.UsuarioId = Convert.ToInt32(hdnUsuarioId.Value);
                usuarioInfo.Nombres = txtNombres.Text.Trim();
                usuarioInfo.ApellidoPaterno = txtApellidoPaterno.Text.Trim();
                usuarioInfo.ApellidoMaterno = txtApellidoMaterno.Text.Trim();
                usuarioInfo.SexoId = Convert.ToInt32(ddlSexo.SelectedValue);
                usuarioInfo.EstadoCivilId = Convert.ToInt32(ddlEstadoCivil.SelectedValue);
                usuarioInfo.Correo = txtCorreo.Text.Trim();
                usuarioInfo.Telefono = txtTelefono.Text.Trim();
                usuarioInfo.Celular = txtCelular.Text.Trim();
                usuarioInfo.TipoDocumentoId = Convert.ToInt32(ddlTipoDocumento.SelectedValue);
                usuarioInfo.NumeroDocumento = txtNumeroDocumento.Text.Trim();

                usuarioInfo.Codigo = txtCodigo.Text.Trim();
                usuarioInfo.Login = txtLogin.Text.Trim();
                usuarioInfo.Password = txtContrasenia.Text.Trim();
                usuarioInfo.PerfilId = Convert.ToInt32(ddlPerfil.SelectedValue);
                usuarioInfo.EmpresaId = Convert.ToInt32(ddlEmpresa.SelectedValue);
                usuarioInfo.TipoNegocioId = Convert.ToInt32(ddlTipoNegocio.SelectedValue);
                usuarioInfo.AgenciaId = Convert.ToInt32(hdnAgenciaId.Value);
                usuarioInfo.DepartamentoId = Convert.ToInt32(ddlDepartamento.SelectedValue);

                //var rutaFoto = Server.MapPath("~/Archivos/Imagenes/Usuario/") + txtLogin.Text.Trim();

                //if (fuFotoUsuario.HasFile)
                //{

                //    if (!Directory.Exists(rutaFoto))
                //    {
                //        Directory.CreateDirectory(rutaFoto);
                //    }
                //    var nombreFoto = fuFotoUsuario.FileName;
                //    nombreFoto = nombreFoto.Substring(0, nombreFoto.Length - 4);
                //    var extensionFoto = fuFotoUsuario.FileName.Substring(nombreFoto.Length, 4);

                //    var nombreArchivo = nombreFoto + DateTime.Now.ToString("ddMMyyyyhhmmss") + extensionFoto;

                //    fuFotoUsuario.SaveAs(rutaFoto + "\\" + nombreArchivo);
                //    usuarioInfo.Foto = "~/Archivos/Imagenes/Usuario/" + txtLogin.Text.Trim() + "/" + nombreArchivo;

                //    if (usuarioSesionInfo.UsuarioId.Equals(usuarioInfo.UsuarioId))
                //    {
                //        usuarioSesionInfo.Foto = usuarioInfo.Foto;
                //        Session["UsuarioInfo"] = usuarioSesionInfo;
                //    }
                //}
                //else
                //{
                //    usuarioInfo.Foto = hdnUsuarioImagen.Value;
                //}

                var sUsuarioImagen = Session["sUsuarioImagen"];
                if (sUsuarioImagen != null)
                {
                    usuarioInfo.Foto = sUsuarioImagen.ToString();
                }
                else
                {
                    usuarioInfo.Foto = hdnUsuarioImagen.Value;
                }
                
                usuarioInfo.CargoId = Convert.ToInt32(ddlCargo.SelectedValue);
                usuarioInfo.CorreoTrabajo = txtCorreoTrabajo.Text.Trim();
                usuarioInfo.TelefonoTrabajo = txtTelefonoTrabajo.Text.Trim();
                usuarioInfo.Activo = (chkActivo.Checked) ? 1 : 0;
                #endregion


                if (usuarioId.Equals(0))
                {
                    usuarioInfo.UsuarioCreacionId = usuarioSesionInfo.UsuarioId;
                    usuarioId = new Negocio.Usuario().Insertar(usuarioInfo);

                    if (usuarioId > 0)
                    {
                        script.Append("document.getElementById('hdnUsuarioId').value = " + usuarioId + ";");
                        mensaje.Append("Se registró al usuario correctamente");

                        script.Append("LimpiarUsuario();");
                        script.Append("var modalDialog = $find('mpeUsuario'); modalDialog.hide();");
                        CargarDatos();
                    }
                    else
                    {
                        mensaje.Append("Ya existe un usuario registrado con el Login: " + txtCodigo.Text.Trim());
                        script.Append("document.getElementById('" + txtLogin.ClientID + "').focus();");
                    }
                }
                else
                {
                    usuarioInfo.UsuarioModificacionId = usuarioSesionInfo.UsuarioId;
                    usuarioId = new Negocio.Usuario().Actualizar(usuarioInfo);

                    if (usuarioId > 0)
                    {
                        script.Append("document.getElementById('hdnUsuarioId').value = " + usuarioId + ";");
                        mensaje.Append("Se registró al usuario correctamente");
                        script.Append("LimpiarUsuario();");
                        script.Append("var modalDialog = $find('mpeUsuario'); modalDialog.hide();");
                        CargarDatos();
                    }
                    else
                    {
                        mensaje.Append("Ya existe un usuario registrado con el Login: " + txtCodigo.Text.Trim());
                        script.Append("document.getElementById('" + txtLogin.ClientID + "').focus();");
                    }
                }

                script.Append("MostrarMensaje('" + mensaje + "');");
                RegistrarScript(script.ToString(), "GuardarUsuario");
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaPresentacion);
                mensaje.Append(rethrow ? ex.Message : Constantes.ExcepcionPoliticaPresentacion);

                script.Append("MostrarMensaje('" + mensaje + "');");
                RegistrarScript(script.ToString(), "GuardarUsuario");
            }
        }
    }
}