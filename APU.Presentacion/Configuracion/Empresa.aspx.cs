using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Services;
using System.Web.UI.WebControls;
using APU.Entidad;
using APU.Herramientas;
using APU.Negocio;
using System.Drawing;
using System.IO;
using System.Net;
using tessnet2;
using System.Web.Hosting;
using System.Web.UI.HtmlControls;
using AjaxControlToolkit;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace APU.Presentacion.Configuracion
{
    public partial class Empresa : PaginaBase
    {
        public static string captcha = "";
        public static CookieContainer cokkie = new CookieContainer();
        string[] nrosRuc = new string[] { };
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            fuEmpresa.UploadedComplete += new EventHandler<AsyncFileUploadEventArgs>(fuEmpresa_UploadedComplete);
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

            tablaMaestraInfo = new TablaMaestra().Listar(0, Constantes.TablaTipoDocumento).Where(x => x.Activo.Equals(1)).ToList();
            LlenarCombo(ddlTipoDocumento, tablaMaestraInfo, "Codigo", "NombreLargo");
            //ddlTipoDocumento.Items.Insert(0, new ListItem(Constantes.Seleccione, Constantes.Seleccione_Value));
            ddlTipoDocumento.SelectedValue = Constantes.TipoDocumentoRuc.ToString();
            // ddlTipoDocumento.Enabled = false;

            var paisInfo = new Pais().Listar(0).ToList();
            LlenarCombo(ddlPais, paisInfo, "PaisId", "Descripcion");
            ddlPais.Items.Insert(0, new ListItem(Constantes.Seleccione, Constantes.Seleccione_Value));
            //var opcionInfo = new Opcion().ListarOpciones(usuarioInfo.EmpresaId).Where(o=>o.Activo.Equals(1)).ToList();
            //LlenarCombo(ddlOpcionInicio, opcionInfo, "OpcionId", "Url");

            var ubigeoInfo = new Ubigeo().Listar(0, 0, Constantes.TipoUbigeoDepartamento);
            LlenarCombo(ddlDepartamento, ubigeoInfo, "UbigeoId", "Nombre");
            ddlDepartamento.Items.Insert(0, new ListItem(Constantes.Seleccione, Constantes.Seleccione_Value));

            //ubigeoInfo = new Ubigeo().Listar(0, clienteInfo.UbigeoDepartamentoId, Constantes.TipoUbigeoProvincia);
        }
        private void CargarDatos()
        {
            var script = new StringBuilder("");

            grvEmpresa.DataBind();
            int numeroRegistros = Convert.ToInt16(ddlNumeroRegistros.SelectedValue);
            int numeroPagina = Convert.ToInt16(ViewState["PageIndex"]);

            int indicePagina = numeroPagina == 0 ? 0 : numeroPagina - 1;
            pageIndex = indicePagina + 1;

            var empresaInfoLista = new Negocio.Empresa().ListarPaginado(0, numeroRegistros, indicePagina);
            grvEmpresa.DataSource = empresaInfoLista;
            grvEmpresa.DataBind();

            if (empresaInfoLista.Count > 0)
            {
                grvEmpresa.HeaderRow.Attributes["style"] = "display: none";
                grvEmpresa.UseAccessibleHeader = true;
                grvEmpresa.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            rowCount = empresaInfoLista.Count > 0 ? empresaInfoLista.First().TotalFilas : 0;
            pageCount = CalcPageCount(rowCount);
            ViewState["PageCount"] = pageCount;

            #region Texto del Pie de Página
            if (empresaInfoLista.Count > 0)
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

            ddlDepartamento.Attributes.Add(Constantes.EventoOnChange, "return ObtenerProvincia(this.value);");
            ddlProvincia.Attributes.Add(Constantes.EventoOnChange, "return ObtenerDistrito(this.value);");

            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Seleccione", "var Seleccione = '" + Constantes.Seleccione + "';", true);
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Seleccione_value", "var Seleccione_value = '" + Constantes.Seleccione_Value + "';", true);

            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "TipoDocumentoRuc", "var TipoDocumentoRuc = '" + Constantes.TipoDocumentoRuc + "';", true);

            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "PaisPeru", "var PaisPeru = '" + Constantes.PaisPeru + "';", true);
        }

        #region WebMethod
        [WebMethod]
        public static EmpresaInfo ObtenerEmpresa(int empresaId)
        {
            return new Negocio.Empresa().Listar(empresaId).FirstOrDefault();
        }
        #endregion

        protected void grvEmpresa_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add(Constantes.EventoOnClick, "var fila = SeleccionarFila(this); if (typeof GridView_OnClick == 'function'){GridView_OnClick(fila);}");
                e.Row.Attributes.Add(Constantes.EventoOnDblClick, "var fila = SeleccionarFila(this); if (typeof grvEmpresa_OnDnlClick == 'function'){grvEmpresa_OnDnlClick(fila);}");
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

        protected void btnGuardarEmpresa_OnClick(object sender, EventArgs e)
        {
            var usuarioInfo = ObtenerUsuarioInfo();
            var script = new StringBuilder(String.Empty);
            var mensaje = String.Empty;
            var empresaId = Convert.ToInt32(hdnEmpresaId.Value);

            #region Datos Empresa
            var empresaInfo = new EmpresaInfo();
            empresaInfo.EmpresaId = Convert.ToInt32(hdnEmpresaId.Value);
            empresaInfo.TipoDocumentoId = Convert.ToInt32(ddlTipoDocumento.SelectedValue);
            empresaInfo.NumeroDocumento = txtRuc.Text.Trim();
            empresaInfo.RazonSocial = txtRazonSocial.Text.Trim();
            empresaInfo.Descripcion = txtDescripcion.Text.Trim();
            empresaInfo.Ciiu = txtCiiu.Text.Trim();
            empresaInfo.PaisId = Convert.ToInt32(ddlPais.SelectedValue);
            empresaInfo.DepartamentoId = Convert.ToInt32(ddlDepartamento.SelectedValue);
            empresaInfo.ProvinciaId = Convert.ToInt32(Request.Form[ddlProvincia.UniqueID]);
            empresaInfo.DistritoId = Convert.ToInt32(Request.Form[ddlDistrito.UniqueID]);
            empresaInfo.Ciudad = txtCiudad.Text.Trim();
            empresaInfo.Direccion = txtDireccion.Text.Trim();

            //var rutaFoto = Server.MapPath("~/Archivos/Imagenes/Empresa/") + txtRuc.Text.Trim();

            //if (fuEmpresa.HasFile)
            //{
            //    if (!Directory.Exists(rutaFoto))
            //    {
            //        Directory.CreateDirectory(rutaFoto);
            //    }
            //    var nombreFoto = fuEmpresa.FileName;
            //    nombreFoto = nombreFoto.Substring(0, nombreFoto.Length - 4);
            //    var extensionFoto = fuEmpresa.FileName.Substring(nombreFoto.Length, 4);

            //    var nombreArchivo = nombreFoto + DateTime.Now.ToString("ddMMyyyyhhmmss") + extensionFoto;
            //    fuEmpresa.SaveAs(rutaFoto + "\\" + nombreArchivo);
            //    empresaInfo.Imagen = "~/Archivos/Imagenes/Empresa/" + txtRuc.Text.Trim() + "/" + nombreArchivo;

            //    if (empresaInfo.EmpresaId.Equals(usuarioInfo.EmpresaId))
            //    {
            //        usuarioInfo.ImagenEmpresa = empresaInfo.Imagen;
            //        Session["UsuarioInfo"] = usuarioInfo;
            //    }
            //}
            //else
            //{
            //    empresaInfo.Imagen = hdnEmpresaImagen.Value;
            //}

            //empresaInfo.Imagen = hdnEmpresaImagen.Value;

            var sEmpresaImagen = Session["sEmpresaImagen"];
            if (sEmpresaImagen != null)
            {
                empresaInfo.Imagen = sEmpresaImagen.ToString();
            }
            else
            {
                empresaInfo.Imagen = hdnEmpresaImagen.Value;
            }
            
            empresaInfo.Activo = (chkActivo.Checked) ? 1 : 0;
            #endregion

            if (empresaId.Equals(0))
            {
                empresaInfo.UsuarioCreacionId = usuarioInfo.UsuarioId;
                empresaId = new Negocio.Empresa().Insertar(empresaInfo);
                if (empresaId > 0)
                {
                    script.Append("document.getElementById('hdnEmpresaId').value = " + empresaId + ";");
                    mensaje = "Se registró la Empresa correctamente";
                }
                else
                {
                    mensaje = "Ya existe una Empresa registrado con el nombre: " + txtRazonSocial.Text.Trim();
                }
            }
            else
            {
                empresaInfo.UsuarioModificacionId = usuarioInfo.UsuarioId;
                empresaId = new Negocio.Empresa().Actualizar(empresaInfo);
                if (empresaId > 0)
                {
                    mensaje = "Se actualizó la Empresa correctamente";
                }
                else
                {
                    mensaje = "Ya existe una Empresa registrada con el nombre: " + txtRazonSocial.Text.Trim();
                }
            }
            script.Append("MostrarMensaje('" + mensaje + "');");
            script.Append("LimpiarEmpresa();");
            script.Append("var modalDialog = $find('mpeEmpresa'); modalDialog.hide();");

            CargarDatos();
            Session.Remove("sEmpresaImagen");
            RegistrarScript(script.ToString(), "GuardarEmpresa");
        }

        protected void btnSunat_Click(object sender, EventArgs e)
        {
            try
            {
                genCaptcha();

                string myurl = @"http://www.sunat.gob.pe/cl-ti-itmrconsruc/jcrS00Alias?accion=consPorRuc&nroRuc=" + txtRuc.Text.Trim() + "&codigo=" + captcha.Trim().ToUpper() + "&tipdoc=1";
                HttpWebRequest myWebRequest = (HttpWebRequest)WebRequest.Create(myurl);
                myWebRequest.CookieContainer = cokkie;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;

                HttpWebResponse myhttpWebResponse = (HttpWebResponse)myWebRequest.GetResponse();
                Stream myStream = myhttpWebResponse.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myStream);
                string xDat = ""; int pos = 0;
                while (!myStreamReader.EndOfStream)
                {
                    txtRuc.Text = txtRuc.Text;
                    xDat = myStreamReader.ReadLine();
                    pos++;

                    switch (pos)
                    {
                        case 122:
                            txtRazonSocial.Text = getDatafromXML(xDat, 25);
                            break;
                        case 126:
                            // txtTipoContribuyente.Text = getDatafromXML(xDat, 25);
                            break;
                        case 131:
                            // txtNombreComercial.Text = getDatafromXML(xDat, 25);
                            break;
                        case 136:
                            // txtFechaInscripcion.Text = getDatafromXML(xDat, 25);
                            break;
                        case 138:
                            // txtFechaInicioActividad.Text = getDatafromXML(xDat, 25);
                            break;
                        case 142:
                            // txtEstadoContribuyente.Text = getDatafromXML(xDat, 25);
                            break;
                        case 152:
                           //  txtCondicionContribuyente.Text = getDatafromXML(xDat, 0);
                            break;
                        case 158:
                            txtDireccion.Text = getDatafromXML(xDat, 25);
                            break;
                        case 176:
                            txtCiiu.Text = getDatafromXML(xDat, 25);
                            break;
                    }

                }
            }
            catch (Exception ex)
            {

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
            //    xDat = myStreamReader.ReadLine();
            //    pos++;

            //    switch (pos)
            //    {
            //        case 122:
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

            var empresaSunat = Herramientas.Helper.ConsultaSunat(ruc);

            return empresaSunat;
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
        void fuEmpresa_UploadedComplete(object sender, AsyncFileUploadEventArgs e)
        {
            var mensaje = String.Empty;
            var script = new StringBuilder("");
            try
            {
                if (fuEmpresa.HasFile)
                {
                    var fechaHora = DateTime.Now.ToString("ddMMyyyyhhmmss");
                    string nombreArchivo = Path.GetFileNameWithoutExtension(e.FileName);
                    string extension = Path.GetExtension(e.FileName);
                    string rutaGuardar = MapPath("~/Archivos/Imagenes/Empresa/" + txtRuc.Text.Trim() + "/" + nombreArchivo + "_" + fechaHora + extension);
                    var directorio = MapPath("~/Archivos/Imagenes/Empresa/" + txtRuc.Text.Trim());

                    if (!Directory.Exists(directorio))
                    {
                        Directory.CreateDirectory(directorio);
                    }

                    fuEmpresa.SaveAs(rutaGuardar);
                    hdnEmpresaImagen.Value = rutaGuardar;

                    // Session["sEmpresaImagen"] = rutaGuardar;
                    Session["sEmpresaImagen"] = "~/Archivos/Imagenes/Empresa/" + txtRuc.Text.Trim() + "/" + nombreArchivo + "_" + fechaHora + extension;

                    script.Append("document.getElementById('" + hdnEmpresaImagen.ClientID + "').value = '" + rutaGuardar + "';");
                    // script.Append("alert(document.getElementById('" + hdnEmpresaImagen.ClientID + "').value);");
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

        protected void btnCargarAlmacen_OnClick(object sender, EventArgs e)
        {
            var empresaId = Convert.ToInt32(Request["EmpresaId"]);

            grvAlmacen.DataBind();

            var almacenInfoLista = new Negocio.Almacen().ListarPaginado(0, empresaId, 0, 0);
            grvAlmacen.DataSource = almacenInfoLista;
            grvAlmacen.DataBind();

            if (almacenInfoLista.Count > 0)
            {
                grvAlmacen.HeaderRow.Attributes["style"] = "display: none";
                grvAlmacen.UseAccessibleHeader = true;
                grvAlmacen.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
    }
}