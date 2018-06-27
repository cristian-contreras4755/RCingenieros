using System;
using System.Web.UI;
//using PA.Entidad;

namespace APU.Petroamerica.Controles
{
    public partial class ucCabecera : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!Page.IsPostBack)
            //{
            //    var usuarioInfo = (UsuarioInfo) Session["UsuarioInfo"];
            //    var guid = Guid.NewGuid();

            //    if (usuarioInfo != null)
            //    {
            //        //if (usuarioInfo.ImagenEmpresa.Equals(String.Empty))
            //        if (true)
            //        {
            //            imgLogoEmpresa.Visible = false;
            //        }
            //        else
            //        {
            //            imgLogoEmpresa.Visible = true;
            //            imgLogoEmpresa.ImageUrl = usuarioInfo.ImagenEmpresa + "?v=" + guid.ToString();
            //        }

            //        //lblUsuario.Text = usuarioInfo.Nombres + " " + usuarioInfo.ApellidoPaterno + " " + usuarioInfo.ApellidoMaterno;
            //        lblUsuario.Text = usuarioInfo.Usuario;
            //        lblPerfil.Text = usuarioInfo.Perfil;

            //        var tipoCambioInfo = "Tipo Cambio: Compra: 3.25 - Venta: 3.30";

            //        //if (usuarioInfo.Foto.Equals(String.Empty))
            //        if (true)
            //        {
            //            imgFoto.Visible = false;
            //        }
            //        else
            //        {
            //            imgFoto.Visible = true;
            //            imgFoto.ImageUrl = usuarioInfo.Foto + "?v=" + guid.ToString();
            //        }
            //    }
            //}
            //imgLogoRcIngenieros.ImageUrl = "~/Imagenes/LogoApu.png?v=" + DateTime.Now.Ticks.ToString();
        }

        protected void imgSalir_Click(object sender, ImageClickEventArgs e)
        {
            Session.Remove("UsuarioInfo");
            Session.RemoveAll();
            Response.Redirect("~/Seguridad/Login.aspx");
        }
    }
}