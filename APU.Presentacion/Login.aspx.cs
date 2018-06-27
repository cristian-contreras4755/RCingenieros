using System;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI.HtmlControls;
using APU.Herramientas;
using APU.Negocio;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace APU.Presentacion
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //base.Page_Load(sender, e);
            if (!Page.IsPostBack)
            {
                var guid = Guid.NewGuid();
                imgLogoApu.ImageUrl = "~/Imagenes/LoginApu.png?v=" + guid.ToString();
                txtUsuario.Focus();
            }
        }
        [WebMethod]
        public static string IniciarSesion(string usuario, string clave)
        {
            //var ws = new WS_SMART.SmartClient();
            var respuesta = String.Empty;
            //string hash = Herramientas.Helper.EncodePassword(clave);
            string hash = clave;
            var usuarioInfo = new Negocio.Usuario().Listar(0, usuario.Trim(), hash, String.Empty, 0, 0).Where(u => u.Activo.Equals(1)).ToList();
            if (usuarioInfo.Count > 0)
            {
                var returnUrl = HttpContext.Current.Request.QueryString["ReturnURL"];
                HttpContext.Current.Session["UsuarioInfo"] = usuarioInfo.First();

                // var perfil = new Negocio.Perfil().Lista

                // respuesta = "OK";
                respuesta = usuarioInfo.First().OpcionInicio;

                //if (usuarioInfo.First().UltimoIngreso.Equals(String.Empty) && usuarioInfo.First().Clave.Equals(Helper.EncodePassword(Constantes.PASSWORD_DEFAULT)))
                //{
                //    #region Obligar cambio clave
                //    return Herramientas.Helper.RutaAplicacion() + "/Seguridad/RecuperarClave.aspx?Email=" + usuarioInfo.First().Email + "&amp;Guid=";
                //    #endregion
                //}

                //if (string.IsNullOrEmpty(returnUrl))
                //{
                //    respuesta = RetornarRutaDestino(usuarioInfo.First().PerfilId);
                //}
                //else
                //{
                //    respuesta = returnUrl;
                //}
                //ws.Helper_ActualizarColumnasTabla("Usuario", new string[] { "UltimoIngreso" }, new string[] { DateTime.Now.ToString("yyyyMMdd HH:mm:ss") }, new string[] { "UsuarioId" }, new string[] { usuarioInfo.First().UsuarioId.ToString("") });
                var request = HttpContext.Current.Request;
                respuesta = request.Url.Scheme + "://" + request.ServerVariables["HTTP_HOST"] + request.ApplicationPath + "~/Inicio.aspx".Replace("~", String.Empty);
            }

            return respuesta;
            // return request.Url.Scheme + "://" + request.ServerVariables["HTTP_HOST"] + request.ApplicationPath + respuesta.Replace("~", String.Empty)
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            var guid = Guid.NewGuid();
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
        }
        [WebMethod]
        public static string RecuperarClave(string usuario)
        {
            var mensaje = String.Empty;
            ;
            try
            {
                // var plantilla = ws.PlantillaCorreo_ListarPlantillaCorreo(Constantes.CorreoRecuperarClave).First();
                var usuarioListaInfo = new Negocio.Usuario().ListarLogin(usuario).ToList();

                //var para = txtUsuario.Text.Trim();
                //var asunto = plantilla.Asunto;

                if (usuarioListaInfo.Count > 0)
                {
                    var usuarioInfo = usuarioListaInfo.FirstOrDefault();
                    var guid = Guid.NewGuid();
                    Negocio.Helper.ActualizarValorTabla("Usuario", "Guid", guid.ToString(), "UsuarioId", usuarioInfo.UsuarioId.ToString());

                    // var mensaje = new StringBuilder();
                    // mensaje.Append(Herramientas.Helper.ObtenerTextoRutaFisica(ConfigurationManager.AppSettings["SMART.Archivos.Correos"] + plantilla.Cuerpo));
                    // mensaje.Replace("@URL", Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath + "/Seguridad/RecuperarClave.aspx");
                    // mensaje.Replace("@EMAIL", para);
                    // mensaje.Replace("@GUID", guid.ToString());

                    var request = HttpContext.Current.Request;
                    var url = request.Url.Scheme + "://" + request.ServerVariables["HTTP_HOST"] + request.ApplicationPath + "/Seguridad/RecuperarClave.aspx";

                    var asunto = "APUFact: Recuperar Clave";
                    var cuerpo = "Para recuperar su clave presione click en el siguiente enlace: ";
                    cuerpo = cuerpo + "<a href=" + url + "?Login=" + usuario + "&Guid=" + guid + "\">Restaure su contrase&ntilde;a</a>";

                    Email.Enviar(usuario, String.Empty, String.Empty, asunto, cuerpo.ToString(), String.Empty);
                    mensaje = "Se acaba de enviar un mensaje al correo registrado.";
                }
                else
                {
                    mensaje = "El usuario no se encuentra registrado en el sistema.";
                }
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, Constantes.ExcepcionPoliticaPresentacion);
                mensaje = rethrow ? ex.Message : Constantes.ExcepcionPoliticaPresentacion;
            }
            return mensaje;
        }
    }
}