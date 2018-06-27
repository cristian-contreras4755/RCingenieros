using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using PA.Entidad;
//using PA.Herramientas;

namespace APU.Petroamerica.Controles
{
    public partial class ucMenu : System.Web.UI.UserControl
    {
        private int opt = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            //var usuarioInfo = (UsuarioInfo) Session["UsuarioInfo"];
            //hdnPerfil.Value = usuarioInfo?.PerfilId.ToString() ?? "0";
            //opt = String.IsNullOrEmpty(Request["opcion"]) ? 0 : Convert.ToInt32(Request["opcion"]);

            //if (usuarioInfo != null)
            //{
            //    // var opcionInfoLista = new Negocio.Opcion().ListarOpciones(usuarioInfo.PerfilId);
            //    var opcionInfoLista = new List<OpcionInfo>();
            //    var padre = opcionInfoLista.Where(p => p.OpcionPadreId == 0).Where(v => v.Activo == 1).OrderBy(o => o.Orden).ToList();
                
            //    var sbMenu = new StringBuilder("<div id=\"menu\">");

            //    sbMenu.Append("<ul>");
            //    sbMenu.Append("<li>");
            //    sbMenu.Append("<a href=\"");
            //    //sbMenu.Append(Page.ResolveClientUrl(usuarioInfo.OpcionInicio));
            //    sbMenu.Append(Page.ResolveClientUrl("~/Inicio.aspx"));
            //    sbMenu.Append("#");
            //    sbMenu.Append("\">Inicio</a>");

            //    sbMenu.Append("</li>");

            //    if (usuarioInfo.PerfilId.Equals(Constantes.PerfilMaster) || usuarioInfo.PerfilId.Equals(Constantes.PerfilWebMaster) || usuarioInfo.PerfilId.Equals(Constantes.PerfilAdministrador))
            //    {
            //        #region Opciones en duro
            //        // Ventas
            //        sbMenu.Append("<li>");
            //        sbMenu.Append("<a href=\"");
            //        sbMenu.Append(Page.ResolveClientUrl("~/Operaciones/Venta.aspx"));
            //        sbMenu.Append("#");
            //        sbMenu.Append("\">Ventas</a>");
            //        sbMenu.Append("</li>");

            //        // Resumen Diario
            //        sbMenu.Append("<li>");
            //        sbMenu.Append("<a href=\"");
            //        sbMenu.Append(Page.ResolveClientUrl("~/Operaciones/ResumenDiario.aspx"));
            //        sbMenu.Append("#");
            //        sbMenu.Append("\">Resumen Diario</a>");
            //        sbMenu.Append("</li>");

            //        // Comunicaciòn de Baja
            //        sbMenu.Append("<li>");
            //        sbMenu.Append("<a href=\"");
            //        sbMenu.Append(Page.ResolveClientUrl("~/Operaciones/ComunicacionBaja.aspx"));
            //        sbMenu.Append("#");
            //        sbMenu.Append("\">Comunicacion Baja</a>");
            //        sbMenu.Append("</li>");
            //        #endregion
            //    }

            //    #region Carga de Opciones
            //    for (int i = 0; i < padre.Count; i++)
            //    {
            //        sbMenu.Append("<li>");
            //        sbMenu.Append("<a href=\"#\">");
            //        sbMenu.Append(padre[i].Nombre);
            //        sbMenu.Append("</a>");
            //        var hijo = opcionInfoLista.Where(h => h.OpcionPadreId == padre[i].OpcionId).Where(v => v.Activo == 1).OrderBy(o => o.Orden).ToList();

            //        if (hijo.Count > 0)
            //        {
            //            sbMenu.Append("<ul class=\"sub-menu\">");
            //            for (int j = 0; j < hijo.Count; j++)
            //            {
            //                sbMenu.Append("<li>");

            //                sbMenu.Append("<a href=\"");
            //                sbMenu.Append(Page.ResolveClientUrl(hijo[j].Url));

            //                sbMenu.Append("\" ");
            //                sbMenu.Append("onclick=\"Bloquear();\" ");
            //                sbMenu.Append(">");

            //                sbMenu.Append(hijo[j].Nombre.Trim());
            //                sbMenu.Append("</a>");
            //                sbMenu.Append("</li>");
            //            }
            //            sbMenu.Append("</ul>");
            //        }
            //        sbMenu.Append("</li>");
            //    }
            //    #endregion

            //    sbMenu.Append("</ul>");
            //    sbMenu.Append("</div>");
            //    litMenu.Text = sbMenu.ToString();
            //}
        }
    }
}