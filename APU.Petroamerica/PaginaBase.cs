using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace APU.Petroamerica
{
    public class PaginaBase : System.Web.UI.Page
    {
        protected virtual void Page_Load(object sender, EventArgs e)
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

            if ((Request.Browser.Browser.ToLower() == "ie") && (Request.Browser.MajorVersion < 9))
            {
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.Private);
                HttpContext.Current.Response.Cache.SetMaxAge(TimeSpan.FromMilliseconds(1));
            }
            else
            {
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);//IE set to not cache
                HttpContext.Current.Response.Cache.SetNoStore();//Firefox/Chrome not to cache
                HttpContext.Current.Response.Cache.SetExpires(DateTime.UtcNow); //for safe measure expire it immediately
            }

            if (Session["UsuarioInfo"] == null)
            {
                var returnUrl = Server.UrlEncode(Request.Url.PathAndQuery);
                //Response.Redirect("~/Seguridad/Login.aspx?returnUrl=" + returnUrl, true);
                // Response.Redirect("~/Seguridad/Login.aspx", true);
            }
        }
        public void LlenarCombo(DropDownList combo, object ds, string dataValue, string dataField)
        {
            combo.DataValueField = dataValue;
            combo.DataTextField = dataField;
            combo.DataSource = ds;
            combo.DataBind();
        }
        /// <summary>
        /// Carga un DropDownList con los campos de la tabla de Parametrización
        /// </summary>
        /// <param name="combo">Control DopDownList a cargar</param>
        /// <param name="ds">Origen de datos para poblar el combo</param>
        /// <param name="nombreCorto">Si es true cargará el nombre Corto de lo contrario el Nombre Largo</param>
        public void LlenarComboParametro(DropDownList combo, object ds, bool nombreCorto)
        {
            combo.DataValueField = "Codigo";
            combo.DataTextField = nombreCorto ? "NombreCorto" : "NombreLargo";
            combo.DataSource = ds;
            combo.DataBind();
        }
        public void RegistrarEventoCliente(WebControl control, string evento, string funcion)
        {
            control.Attributes.Add(evento, funcion);
        }
        public void RegistrarScript(string script, string nombreScript)
        {
            var guid = Guid.NewGuid();
            ScriptManager.RegisterStartupScript(this, GetType(), nombreScript + "_" + guid.ToString(), script, true);
        }
        //public static UsuarioInfo ObtenerUsuarioInfo()
        //{
        //    var oUsuarioInfo = (UsuarioInfo)System.Web.HttpContext.Current.Session["UsuarioInfo"];

        //    if (oUsuarioInfo == null)
        //    {
        //        var returnURL = System.Web.HttpUtility.UrlEncode(System.Web.HttpContext.Current.Request.Url.PathAndQuery);
        //        System.Web.HttpContext.Current.Response.Redirect("~/Seguridad/Login.aspx?returnURL=" + returnURL, true);
        //    }

        //    return oUsuarioInfo;
        //}      
    }
}