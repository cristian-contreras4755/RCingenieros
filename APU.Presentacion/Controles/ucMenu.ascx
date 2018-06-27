<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucMenu.ascx.cs" Inherits="APU.Presentacion.Controles.ucMenu" %>
<table style="width: 100%; background-color: #014386; height: 30px;">
    <tr>
        <td style="width: 5%;">&nbsp;</td>
        <td style="width: 90%;">
            <table style="width: 100%; background-color: #014386; height: 30px;">
                <tr>
                    <td id="tdMenu" style="width: 100%;">
                        <asp:Literal ID="litMenu" runat="server" />
                    </td>
                </tr>
            </table>
        </td>
        <td style="width: 5%;">&nbsp;</td>
    </tr>
</table>
<input type="hidden" id="hdnPerfil" runat="server"/>
<script type="text/javascript">
    var perfil = parseInt(document.getElementById('<%= hdnPerfil.ClientID %>').value);
    var inicio = document.getElementById('ucMenu_58');
    var ruta = '#';
    if (inicio != null) {
        switch (perfil) {
        case PerfilAnalista:
            ruta = '../Credito/SolicitudesPorEvaluar.aspx';
            break;
        case PerfilAdministrador:
            ruta = '../Comercial/Seguimiento.aspx?filtro=5&opcion=54';
        default:
            ruta = '#';
            break;
        }
        inicio.href = ruta;
    }
</script>