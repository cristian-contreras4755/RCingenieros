<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RecuperarClave.aspx.cs" Inherits="APU.Presentacion.Seguridad.RecuperarClave" %>
<%@ Register TagPrefix="uc1" TagName="ucProcesando" Src="~/Controles/ucProcesando.ascx" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Recuperar Clave</title>
</head>
<body oncontextmenu="JavaScript:return true;" onload="Desbloquear();" onunload="Bloquear();" onbeforeunload="">
<form id="frmRecuperarClave" runat="server" DefaultButton="btnDefault">
    <uc1:ucProcesando runat="server" ID="ucProcesando" />
    <asp:ScriptManager EnablePageMethods="True" runat="server"></asp:ScriptManager>
    <div id="contenido">
        <br/>
        <br/>
        <br/>
        <br/>
        <table style="width: 100%;">
            <tr>
                <td style="width: 25%;">&nbsp;</td>
                <td style="width: 50%;">
                    <table style="width: 100%;" class="border">
                        <tr>
                            <td colspan="4" id="lblTitulo" class="lblTituloPopup">Recuperar Contrase&ntilde;a</td>
                        </tr>
                        <tr>
                            <td colspan="4" >&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 20%;">&nbsp;</td>
                            <td class="lblStandar" style="width: 25%;">Usuario</td>
                            <td style="width: 35%;">
                                <asp:TextBox ID="txtUsuario" CssClass="txtStandar" ReadOnly="True" Width="80%" runat="server"></asp:TextBox>
                            </td>
                            <td style="width: 20%;">&nbsp;</td>
                        </tr>
                        <tr id="trClaveAnterior" style="display: none;">
                            <td>&nbsp;</td>
                            <td class="lblStandar">Clave Anterior</td>
                            <td>
                                <asp:TextBox ID="txtClaveAnterior" CssClass="txtStandar" Width="80%" runat="server"></asp:TextBox>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td></td>
                            <td class="lblStandar">Clave</td>
                            <td>
                                <asp:TextBox ID="txtClave" CssClass="txtStandar" Width="80%" runat="server"></asp:TextBox>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td class="lblStandar">Repetir Clave</td>
                            <td>
                                <asp:TextBox ID="txtRepetirClave" CssClass="txtStandar" Width="80%" runat="server"></asp:TextBox>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td colspan="4">&nbsp;</td>
                        </tr>
                        <tr>
                            <td></td>
                            <td colspan="2" style="text-align: center;">
                                <%--<asp:Button ID="btnGuardar" CssClass="btnStandar" Text="Guardar" Width="30%" OnClientClick="return ValidarClave();" runat="server" OnClick="btnGuardar_Click"/>--%>
                                <input type="button" id="btnGuardar" class="btnStandar" value="Guardar" style="width: 30%;" onclick="return ValidarClave();" />
                                <asp:Button ID="btnGuardarHidden" CssClass="btnStandar" Text="Guardar" style="display: none;" Width="30%" runat="server" OnClick="btnGuardar_Click"/>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td colspan="4">&nbsp;</td>
                        </tr>
                    </table>
                </td>
                <td style="width: 25%;">&nbsp;</td>
            </tr>
        </table>
        <input type="hidden" id="hdnGuid" value="" runat="server" />
    </div>
    <script type="text/javascript">
        var guid = document.getElementById('hdnGuid').value;
        // document.getElementById('trClaveAnterior').style.display = (guid.trim() == '') ? 'inline' : 'none';
        document.getElementById('trClaveAnterior').style.display = (guid.trim() == '') ? '' : 'none';
        var email = document.getElementById('txtUsuario');

        function ValidarClave() {
            var claveAnterior = document.getElementById('txtClaveAnterior');
            var clave = document.getElementById('txtClave');
            var repetirClave = document.getElementById('txtRepetirClave');
            if (guid.trim() == '') {
                // Cambiar Clave
                if (claveAnterior.value == '') {
                    return MostrarValidacion(claveAnterior, 'Ingrese su clave anterior.');
                }
            }
            // Recuperar Clave
            if (clave.value == '') {
                return MostrarValidacion(clave, 'Ingrese la nueva clave.');
            }
            if (repetirClave.value == '') {
                return MostrarValidacion(repetirClave, 'Repita la nueva clave.');
            }
            if (clave.value != repetirClave.value) {
                return MostrarValidacion(repetirClave, 'La nueva clave ingresada no coincide.');
            }

            if (guid.trim() == '') {
                if (clave.value == claveAnterior.value) {
                    return MostrarValidacion(clave, 'La nueva clave debe ser diferente a la anterior.');
                }
                // Bloqueo Pantalla
                Bloquear();
                PageMethods.ValidarClaveAnterior(email.value, claveAnterior.value, ValidarClaveAnteriorCliente, fnLlamadaError);
            } else {
                document.getElementById('btnGuardarHidden').click();
            }
        }
        function ValidarClaveAnteriorCliente(result, context) {
            var claveAnterior = document.getElementById('txtClaveAnterior');
            if (result <= 0) {
                // Desbloquear Pantalla
                Desbloquear();
                return MostrarValidacion(claveAnterior, 'La clave anterior es incorrecta.');
            } else {
                document.getElementById('btnGuardarHidden').click();
            }
        }
        function fnLlamadaError(excepcion) {
            alert('Ha ocurrido un error interno: ' + excepcion.get_message());
        }
        function IrLogin() {
            window.location = 'Login.aspx';
        }
        function Desbloquear() {
            var tiempo = 0;
            var procesando = document.getElementById('divProcesando');
            var contenido = document.getElementById('contenido');
            if (procesando != null) {
                window.setTimeout('document.getElementById("divProcesando").style.display="none"; document.getElementById("divCargando").style.display="none"; document.getElementById("contenido").style.display="";', tiempo);
            }
        }
        function Bloquear() {
            var procesando = document.getElementById('divProcesando');
            var contenido = document.getElementById('contenido');
            var heights = document.documentElement.clientHeight;
            var body = document.body,
                html = document.documentElement;

            var height = Math.max(body.scrollHeight, body.offsetHeight, html.clientHeight, html.scrollHeight, html.offsetHeight);

            if (procesando != null) {
                document.getElementById('divProcesando').style.height = height + 'px';
                document.getElementById('divProcesando').style.display = '';
                document.getElementById('divCargando').style.display = '';
            }
        }
    </script>
    <asp:Button id="btnDefault" OnClientClick="return false;" runat="server" />
</form>
</body>
</html>