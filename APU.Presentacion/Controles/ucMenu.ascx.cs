using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using APU.Entidad;

namespace APU.Presentacion.Controles
{
    public partial class ucMenu : System.Web.UI.UserControl
    {
        private int opt = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            var usuarioInfo = (UsuarioInfo)Session["UsuarioInfo"];
            hdnPerfil.Value = usuarioInfo?.PerfilId.ToString() ?? "0";
            opt = String.IsNullOrEmpty(Request["opcion"]) ? 0 : Convert.ToInt32(Request["opcion"]);

            if (usuarioInfo != null)
            {
                var opcionInfoLista = new Negocio.Opcion().ListarOpciones(usuarioInfo.PerfilId);
                var padre = opcionInfoLista.Where(p => p.OpcionPadreId == 0).Where(v => v.Activo == 1).OrderBy(o => o.Orden).ToList();
                
                var sbMenu = new StringBuilder("<div id=\"menu\">");

                sbMenu.Append("<ul>");
                sbMenu.Append("<li>");
                sbMenu.Append("<a href=\"");
                //sbMenu.Append(Page.ResolveClientUrl(usuarioInfo.OpcionInicio));
                sbMenu.Append(Page.ResolveClientUrl("~/Inicio.aspx"));
                sbMenu.Append("#");
                sbMenu.Append("\">Inicio</a>");

                sbMenu.Append("</li>");

                #region Carga de Opciones
                for (int i = 0; i < padre.Count; i++)
                {
                    sbMenu.Append("<li>");
                    sbMenu.Append("<a href=\"#\">");
                    sbMenu.Append(padre[i].Nombre);
                    sbMenu.Append("</a>");
                    var hijo = opcionInfoLista.Where(h => h.OpcionPadreId == padre[i].OpcionId).Where(v => v.Activo == 1).OrderBy(o => o.Orden).ToList();

                    if (hijo.Count > 0)
                    {
                        sbMenu.Append("<ul class=\"sub-menu\">");
                        for (int j = 0; j < hijo.Count; j++)
                        {
                            sbMenu.Append("<li>");

                            sbMenu.Append("<a href=\"");
                            sbMenu.Append(Page.ResolveClientUrl(hijo[j].Url));

                            sbMenu.Append("\" ");
                            sbMenu.Append("onclick=\"Bloquear();\" ");
                            sbMenu.Append(">");

                            sbMenu.Append(hijo[j].Nombre.Trim());
                            sbMenu.Append("</a>");
                            sbMenu.Append("</li>");
                        }
                        sbMenu.Append("</ul>");
                    }
                    sbMenu.Append("</li>");
                }
                #endregion

                sbMenu.Append("</ul>");
                sbMenu.Append("</div>");
                litMenu.Text = sbMenu.ToString();
            }
        }
    }
}