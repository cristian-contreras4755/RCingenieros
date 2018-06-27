using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using APU.Presentacion.WS_BETA;

namespace APU.Presentacion
{
    public partial class PruebaSunat : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnEnviarSunat_Click(object sender, EventArgs e)
        {
            try
            {
                //var sunat = new WS_SUNAT.billServiceClient();
                //sunat.sendBill(@"D:\DESARROLLO\SMART\SMART.Presentacion\XML\CDA\Final\Casos con maxima informacion\20100066603-RC-20110522-1.xml", null);
                //lblMensaje.Text = "Exito";

                byte[] byteArray = File.ReadAllBytes(@"D:\DESARROLLO\SMART\SMART.Presentacion\XML\CDA\Final\Casos con maxima informacion" + "\\" + "20100066603-01-F001-1.zip");

                // WS_SUNAT.billServiceClient ws = new WS_SUNAT.billServiceClient();
                WS_BETA.billServiceClient ws = new billServiceClient();
                ws.Open();
                byte[] oRetorno;
                oRetorno = ws.sendBill("20100066603-01-F001-1.zip", byteArray, "");
                lblMensaje.Text = "Exito";
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                lblMensaje.Text = "Error: " + exception.Message;
            }
        }
    }
}