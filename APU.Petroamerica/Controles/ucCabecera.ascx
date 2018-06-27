<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucCabecera.ascx.cs" Inherits="APU.Petroamerica.Controles.ucCabecera" %>
<table style="width: 100%; height: 10%">
    <tr>
        <td style="width: 20%; text-align:center">
            <asp:Image id="imgLogoEmpresa" runat="server" CssClass="imgLogoEmpresa" ImageUrl="~/Imagenes/logo-primax-sm.png" />
        </td>
        <td style="width: 30%;">&nbsp;</td>
        <td style="width: 15%; border-right: 6px #00ACEE solid;">
            <asp:Image id="imgLogoRcIngenieros" Width="120px" runat="server" ImageUrl="~/Imagenes/LogoApu.png" />
        </td>
        <td style="width: 10%; text-align: center;">
            <asp:Image id="imgFoto" runat="server" CssClass="imgAvatarRedondo" />
        </td>
        <td style="width: 25%; vert-align: top; vertical-align: top;">
            <table style="width: 100%;">
                <tr>
                    <td style="width: 70%; text-align: left;" rowspan="2">
                        <asp:Label ID="lblUsuario" runat="server" CssClass="lblStandarBold" Text="Marlon Ramos Sajami"></asp:Label>
                        <br/>
                        <asp:Label ID="lblPerfil" runat="server" CssClass="lblStandarBold" Text="Administrador"></asp:Label>
                        <br/>
                        <asp:Label ID="lblTipoCambio" runat="server" CssClass="lblStandarBold" Text="Tipo Cambio: Compra: 3.25 - Venta: 3.30"></asp:Label>
                    </td>
                    <td style="width: 30%;">
                        <asp:ImageButton ID="imgSalir" runat="server" ImageUrl="~/Imagenes/Iconos/salir.png" CssClass="imgButton" ToolTip="Salir" OnClick="imgSalir_Click" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>