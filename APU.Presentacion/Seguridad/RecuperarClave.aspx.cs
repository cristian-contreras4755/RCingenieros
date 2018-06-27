using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using APU.Entidad;
using APU.Herramientas;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace APU.Presentacion.Seguridad
{
    public partial class RecuperarClave : System.Web.UI.Page
    {
        #region Variables
        private string login = String.Empty;
        private string guid = String.Empty;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            var script = new StringBuilder("");
            txtClaveAnterior.Attributes.Add("type", "password");
            txtClave.Attributes.Add("type", "password");
            txtRepetirClave.Attributes.Add("type", "password");

            #region Referencia a Librerias
            // var guid = Guid.NewGuid();
            var head = (HtmlHead)Page.Header;

            var linkMeta = new HtmlMeta();
            linkMeta.Attributes.Add("name", "viewport");
            linkMeta.Attributes.Add("content", "width=device-width, initial-scale=1.0");
            head.Controls.Add(linkMeta);

            var linkSmart = new HtmlLink();
            linkSmart.Attributes.Add("href", Page.ResolveClientUrl("~/Estilos/apu.css?v=" + guid.ToString()));
            linkSmart.Attributes.Add("type", "text/css");
            linkSmart.Attributes.Add("rel", "stylesheet");
            head.Controls.Add(linkSmart);

            var linkBootstrap = new HtmlLink();
            linkBootstrap.Attributes.Add("href", Page.ResolveClientUrl("~/Estilos/Bootstrap/css/bootstrap.min.css"));
            linkBootstrap.Attributes.Add("type", "text/css");
            linkBootstrap.Attributes.Add("rel", "stylesheet");
            head.Controls.Add(linkBootstrap);

            var scriptJquery = new HtmlGenericControl("script");
            scriptJquery.Attributes.Add("type", "text/javascript");
            scriptJquery.Attributes.Add("src", Page.ResolveClientUrl("~/Scripts/jQuery/jquery.js"));
            Page.Header.Controls.Add(scriptJquery);

            var scriptBootstrap = new HtmlGenericControl("script");
            scriptBootstrap.Attributes.Add("type", "text/javascript");
            scriptBootstrap.Attributes.Add("src", Page.ResolveClientUrl("~/Estilos/Bootstrap/js/bootstrap.min.js"));
            Page.Header.Controls.Add(scriptBootstrap);

            var scriptAerp = new HtmlGenericControl("script");
            scriptAerp.Attributes.Add("type", "text/javascript");
            scriptAerp.Attributes.Add("src", Page.ResolveClientUrl("~/Scripts/apu.js?v=" + guid.ToString()));
            Page.Header.Controls.Add(scriptAerp);

            var scriptJqueryMask = new HtmlGenericControl("script");
            scriptJqueryMask.Attributes.Add("type", "text/javascript");
            scriptJqueryMask.Attributes.Add("src", Page.ResolveClientUrl("~/Scripts/jQuery/jquery.mask.js"));
            Page.Header.Controls.Add(scriptJqueryMask);

            var scriptAutoNumeric = new HtmlGenericControl("script");
            scriptAutoNumeric.Attributes.Add("type", "text/javascript");
            scriptAutoNumeric.Attributes.Add("src", Page.ResolveClientUrl("~/Scripts/autoNumeric/autoNumeric.js"));
            Page.Header.Controls.Add(scriptAutoNumeric);

            var scriptCurrency = new HtmlGenericControl("script");
            scriptCurrency.Attributes.Add("type", "text/javascript");
            scriptCurrency.Attributes.Add("src", Page.ResolveClientUrl("~/Scripts/accounting/accounting.min.js"));
            Page.Header.Controls.Add(scriptCurrency);
            #endregion

            login = String.IsNullOrEmpty(Request["Login"]) ? String.Empty : Request["Login"];
            guid = String.IsNullOrEmpty(Request["Guid"]) ? String.Empty : Request["Guid"];
            hdnGuid.Value = guid;

            if (!Page.IsPostBack)
            {
                if (!login.Trim().Equals(String.Empty))
                {
                    var usuarioInfoLista = new Negocio.Usuario().ListarLogin(login).ToList();
                    txtUsuario.Text = login;
                    if (usuarioInfoLista.Count > 0)
                    {
                        if (guid.Equals(String.Empty))
                        {
                            #region Cambio de Contraseña
                            txtClaveAnterior.Focus();
                            script.Append("document.getElementById('lblTitulo').innerText = 'Cambio de Contraseña';");
                            RegistrarScript(script.ToString(), "IniciarTabs");
                            #endregion
                        }
                        else
                        {
                            #region Recuperar Contraseña
                            txtClave.Focus();
                            script.Append("document.getElementById('lblTitulo').innerText = 'Recuperar Contraseña';");
                            RegistrarScript(script.ToString(), "Iniciar");
                            #endregion
                        }
                    }
                }
            }
        }
        public void RegistrarScript(string script, string nombreScript)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), nombreScript, script, true);
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = String.Empty;
            try
            {
                var usuarioInfo = new Negocio.Usuario().Listar(0, txtUsuario.Text.Trim(), String.Empty, String.Empty, 0, 0).FirstOrDefault();

                login = String.IsNullOrEmpty(Request["Login"]) ? String.Empty : Request["Login"];
                guid = String.IsNullOrEmpty(Request["Guid"]) ? String.Empty : Request["Guid"];

                if (guid.Equals(usuarioInfo.Guid) || usuarioInfo.Guid.Trim().Equals(String.Empty))
                {
                    Negocio.Helper.ActualizarColumnasTabla("Usuario", new string[] { "Password", "UsuarioModificacionId", "Guid", "FechaModificacion" },
                        new string[] { txtClave.Text, usuarioInfo.UsuarioId.ToString(), String.Empty, DateTime.Now.ToString("yyyyMMdd HH:mm:ss") },
                        new string[] { "UsuarioId" }, new string[] { usuarioInfo.UsuarioId.ToString("") });
                    mensaje = "Se actualizó la clave correctamente.";
                }
                else
                {
                    mensaje = "El enlace generado ha expirado. Por favor genere uno nuevamente.";
                }
                RegistrarScript("MostrarMensaje('" + mensaje + "'); IrLogin();");
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaPresentacion);
                mensaje = rethrow ? ex.Message : Constantes.ExcepcionPoliticaPresentacion;
                RegistrarScript("MostrarMensaje('" + mensaje + "');");
            }
        }
        public void RegistrarScript(string script)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "script", script, true);
        }
        [WebMethod]
        public static int ValidarClaveAnterior(string email, string clave)
        {
            int respuesta = 0;
            var usuarioInfo = new UsuarioInfo();
            var usuarioInfoLista = new List<UsuarioInfo>();

            usuarioInfoLista = new Negocio.Usuario().Listar(0, email, String.Empty, String.Empty, 0, 0).ToList();
            //oUsuarioInfoLista = ws.Usuario_ListarPorEmail(email).Where(u => u.Clave == clave).ToList();

            if (usuarioInfoLista.Count > 0)
            {
                usuarioInfo = usuarioInfoLista.First();
                //respuesta = (usuarioInfo.Password == Herramientas.Helper.EncodePassword(clave)) ? 1 : 0;
                respuesta = (usuarioInfo.Password == clave) ? 1 : 0;
            }

            return respuesta;
        }
    }
}