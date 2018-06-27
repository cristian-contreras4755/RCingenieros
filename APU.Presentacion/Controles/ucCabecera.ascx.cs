using System;
using System.Web.UI;
using APU.Entidad;

namespace APU.Presentacion.Controles
{
    public partial class ucCabecera : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var usuarioInfo = (UsuarioInfo) Session["UsuarioInfo"];
                var guid = Guid.NewGuid();

                if (usuarioInfo != null)
                {
                    if (usuarioInfo.ImagenEmpresa.Equals(String.Empty))
                    {
                        imgLogoEmpresa.Visible = false;
                    }
                    else
                    {
                        imgLogoEmpresa.Visible = true;
                        imgLogoEmpresa.ImageUrl = usuarioInfo.ImagenEmpresa + "?v=" + guid.ToString();
                    }
                    
                    lblUsuario.Text = usuarioInfo.Nombres + " " + usuarioInfo.ApellidoPaterno + " " + usuarioInfo.ApellidoMaterno;
                    lblPerfil.Text = usuarioInfo.Perfil;

                    var tipoCambioInfo = "Tipo Cambio: Compra: 3.25 - Venta: 3.30";

                    if (usuarioInfo.Foto.Equals(String.Empty))
                    {
                        imgFoto.Visible = false;
                    }
                    else
                    {
                        imgFoto.Visible = true;
                        imgFoto.ImageUrl = usuarioInfo.Foto + "?v=" + guid.ToString();
                    }
                }
            }
            // imgLogoEmpresa.ImageUrl = "~/Imagenes/logo-primax-sm.png?v=" + DateTime.Now.Ticks.ToString();
            imgLogoRcIngenieros.ImageUrl = "~/Imagenes/LogoApu.png?v=" + DateTime.Now.Ticks.ToString();
        }

        protected void imgSalir_Click(object sender, ImageClickEventArgs e)
        {
            Session.Remove("UsuarioInfo");
            Session.RemoveAll();
            Response.Redirect("~/Login.aspx");
        }
    }
}