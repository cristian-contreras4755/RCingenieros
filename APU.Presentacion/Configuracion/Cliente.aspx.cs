using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Hosting;
using System.Web.Services;
using System.Web.UI.WebControls;
using APU.Entidad;
using APU.Herramientas;
using APU.Negocio;
using tessnet2;

namespace APU.Presentacion.Configuracion
{
    public partial class Cliente : PaginaBase
    {
        public static string captcha = "";
        public static CookieContainer cokkie = new CookieContainer();
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            if (!Page.IsPostBack)
            {
                var operacion = (Request["Operacion"] == null) ? String.Empty : Request["Operacion"];
                hdnOperacion.Value = operacion;
                CargarInicial();
                CargarDatos();
                if (operacion.Equals("N"))
                {
                    mpeCliente.Show();
                }
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
            tablaMaestraInfo = new TablaMaestra().Listar(0, Constantes.TablaTipoPersona).Where(x => x.Activo.Equals(1)).ToList();
            LlenarCombo(ddlTipoPersona, tablaMaestraInfo, "Codigo", "NombreLargo");
            ddlTipoPersona.Items.Insert(0, new ListItem(Constantes.Seleccione, Constantes.Seleccione_Value));

            //tablaMaestraInfo = new TablaMaestra().Listar(0, Constantes.TablaEstadoCivil).Where(x => x.Activo.Equals(1)).ToList();
            //LlenarCombo(ddlEstadoCivil, tablaMaestraInfo, "Codigo", "NombreLargo");
            //ddlEstadoCivil.Items.Insert(0, new ListItem(Constantes.Seleccione, Constantes.Seleccione_Value));

            tablaMaestraInfo = new TablaMaestra().Listar(0, Constantes.TablaTipoDocumento).Where(x => x.Activo.Equals(1)).ToList();
            LlenarCombo(ddlTipoDocumento, tablaMaestraInfo, "Codigo", "NombreLargo");
            ddlTipoDocumento.Items.Insert(0, new ListItem(Constantes.Seleccione, Constantes.Seleccione_Value));
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

            var clienteInfoLista = new Negocio.Cliente().ListarPaginado(0, 0, 0, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, numeroRegistros, indicePagina);
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

            RegistrarEventoCliente(txtNumeroDocumento, Constantes.EventoOnKeyUp, "return GenerarCodigo();");
            //RegistrarEventoCliente(txtNumeroDocumento, Constantes.EventoOnPaste, "return GenerarCodigo();");

            ddlTipoPersona.Attributes.Add("onchange", "return SeleccionarTipoPersona(this.value);");
            ddlPais.Attributes.Add("onchange", "return SeleccionarPais(this.value);");

            ddlDepartamento.Attributes.Add(Constantes.EventoOnChange, "return ObtenerProvincia(this.value);");
            ddlProvincia.Attributes.Add(Constantes.EventoOnChange, "return ObtenerDistrito(this.value);");

            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Seleccione", "var Seleccione = '" + Constantes.Seleccione + "';", true);
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Seleccione_value", "var Seleccione_value = '" + Constantes.Seleccione_Value + "';", true);
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "PaisPeru", "var PaisPeru = '" + Constantes.PaisPeru + "';", true);
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "TipoPersonaNatural", "var TipoPersonaNatural = '" + Constantes.TipoPersonaNatural + "';", true);
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "TipoPersonaJuridica", "var TipoPersonaJuridica = '" + Constantes.TipoPersonaJuridica + "';", true);

            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "TipoDocumentoDni", "var TipoDocumentoDni = '" + Constantes.TipoDocumentoDni + "';", true);
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "TipoDocumentoRuc", "var TipoDocumentoRuc = '" + Constantes.TipoDocumentoRuc + "';", true);
        }

        #region WebMethod
        [WebMethod]
        public static ClienteInfo ObtenerCliente(int clienteId)
        {
            return new Negocio.Cliente().Listar(clienteId).FirstOrDefault();
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
            var clienteInfo = new ClienteInfo();
            clienteInfo.ClienteId = Convert.ToInt32(hdnClienteId.Value);
            //clienteInfo.Codigo = txtCodigo.Text.Trim();
            clienteInfo.Codigo = txtNumeroDocumento.Text.Trim();
            clienteInfo.TipoPersonaId = Convert.ToInt32(ddlTipoPersona.SelectedValue);
            clienteInfo.Nombres = txtNombres.Text.Trim();
            clienteInfo.ApellidoPaterno = txtApellidoPaterno.Text.Trim();
            clienteInfo.ApellidoMaterno = txtApellidoMaterno.Text.Trim();
            clienteInfo.RazonSocial = txtRazonSocial.Text.Trim();
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
                clienteId = new Negocio.Cliente().Insertar(clienteInfo);
                if (clienteId > 0)
                {
                    script.Append("document.getElementById('hdnClienteId').value = " + clienteId + ";");
                    mensaje.Append("Se registró al cliente correctamente");
                }
                else
                {
                    mensaje.Append("Ya existe un cliente registrado con el N° de documento: " + txtNumeroDocumento.Text.Trim());
                }
            }
            else
            {
                clienteInfo.UsuarioModificacionId = usuarioInfo.UsuarioId;
                clienteId = new Negocio.Cliente().Actualizar(clienteInfo);
                if (clienteId > 0)
                {
                    script.Append("document.getElementById('hdnClienteId').value = " + clienteId + ";");
                    mensaje.Append("Se registró al cliente correctamente");
                }
                else
                {
                    mensaje.Append("Ya existe un cliente registrado con el N° de documento: " + txtNumeroDocumento.Text.Trim());
                }
            }
            script.Append("MostrarMensaje('" + mensaje + "');");
            script.Append("LimpiarCliente();");
            script.Append("var modalDialog = $find('mpeCliente'); modalDialog.hide();");
            CargarDatos();
            RegistrarScript(script.ToString(), "GuardarCliente");
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

        protected void btnCargarAgencia_OnClick(object sender, EventArgs e)
        {
            var empresaId = Convert.ToInt32(Request["EmpresaId"]);
            var clienteId = Convert.ToInt32(Request["ClienteId"]);

            grvAgencia.DataBind();

            var agenciaInfoLista = new Negocio.Agencia().ListarPaginado(0, empresaId, clienteId, 0, 0);
            grvAgencia.DataSource = agenciaInfoLista;
            grvAgencia.DataBind();

            if (agenciaInfoLista.Count > 0)
            {
                grvAgencia.HeaderRow.Attributes["style"] = "display: none";
                grvAgencia.UseAccessibleHeader = true;
                grvAgencia.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
        [WebMethod]
        public static EmpresaSunatInfo ObtenerEmpresaSunat(string ruc)
        {
            //var datosEmpresa = String.Empty;
            //captcha = genCaptcha();

            //string myurl = @"http://www.sunat.gob.pe/cl-ti-itmrconsruc/jcrS00Alias?accion=consPorRuc&nroRuc=" + ruc.Trim() + "&codigo=" + captcha.Trim().ToUpper() + "&tipdoc=1";
            //HttpWebRequest myWebRequest = (HttpWebRequest)WebRequest.Create(myurl);
            //myWebRequest.CookieContainer = cokkie;
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;

            //HttpWebResponse myhttpWebResponse = (HttpWebResponse)myWebRequest.GetResponse();
            //Stream myStream = myhttpWebResponse.GetResponseStream();
            //StreamReader myStreamReader = new StreamReader(myStream);
            //string xDat = ""; int pos = 0;
            //while (!myStreamReader.EndOfStream)
            //{
            //    // txtRuc.Text = txtRuc.Text;
            //    xDat = myStreamReader.ReadLine();
            //    pos++;

            //    switch (pos)
            //    {
            //        case 122:
            //            //txtRazonSocial.Text = getDatafromXML(xDat, 25);
            //            var razonSocialCompleta = getDatafromXML(xDat, 26);
            //            var razonSocial = razonSocialCompleta.Split(new[] { " - " }, StringSplitOptions.None);
            //            datosEmpresa += razonSocial[0] + "@";
            //            datosEmpresa += razonSocial[1] + "@";
            //            if (razonSocial.Length > 2)
            //            {
            //                datosEmpresa += razonSocial[2] + "@";
            //            }
            //            break;
            //        case 126:
            //            // txtTipoContribuyente.Text = getDatafromXML(xDat, 25);
            //            break;
            //        case 131:
            //            // txtNombreComercial.Text = getDatafromXML(xDat, 25);
            //            break;
            //        case 136:
            //            // txtFechaInscripcion.Text = getDatafromXML(xDat, 25);
            //            break;
            //        case 138:
            //            // txtFechaInicioActividad.Text = getDatafromXML(xDat, 25);
            //            break;
            //        case 142:
            //            // txtEstadoContribuyente.Text = getDatafromXML(xDat, 25);
            //            break;
            //        case 152:
            //            //  txtCondicionContribuyente.Text = getDatafromXML(xDat, 0);
            //            break;
            //        case 158:
            //            //txtDireccion.Text = getDatafromXML(xDat, 25);
            //            var direccion = getDatafromXML(xDat, 25) + "@";
            //            datosEmpresa += direccion;
            //            break;
            //        //case 176:
            //        case 201:
            //            //txtCiiu.Text = getDatafromXML(xDat, 25);
            //            var ciiu = getDatafromXML(xDat, 20);
            //            datosEmpresa += ciiu;
            //            break;
            //    }
            //}
            //return datosEmpresa;
            var empresaSunatInfo = Herramientas.Helper.ConsultaSunat(ruc);

            return empresaSunatInfo;
        }

        [WebMethod]
        public static PersonaReniecInfo ObtenerPersonaReniec(string dni)
        {
            var personaReniecInfo = Herramientas.Helper.ConsultaReniec(dni);

            return personaReniecInfo;
        }

        public static string genCaptcha()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.sunat.gob.pe/cl-ti-itmrconsruc/captcha?accion=image");
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
            request.CookieContainer = cokkie;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream responseStream = response.GetResponseStream();

            var image = new Bitmap(responseStream);
            var ocr = new Tesseract();
            //ocr.Init(@"C:\Users\Miguel\Desktop\SUNAT\PRUEBA\Content\tessdata", "eng", false);
            var path = HostingEnvironment.MapPath(@"~/Componentes/tessdata");
            ocr.Init(path, "eng", false);

            var result = ocr.DoOCR(image, Rectangle.Empty);
            foreach (Word word in result)
            {
                captcha = word.Text;
            }
            return captcha;
        }
        public static string getDatafromXML(string lineRead, int len = 0)
        {
            try
            {
                lineRead = lineRead.Trim();
                lineRead = lineRead.Remove(0, len);
                lineRead = lineRead.Replace("</td>", "");
                lineRead = lineRead.Replace("</option>", "");

                while (lineRead.Contains("  "))
                {
                    lineRead = lineRead.Replace("  ", " ");
                }
                return lineRead;
            }
            catch (Exception ex)
            {
                return "NO SE ENCONTRO RESULTADO";
            }
        }
    }
}