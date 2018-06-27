<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="APU.Presentacion.Login" %>
<%@ Register Src="~/Controles/ucProcesando.ascx" TagPrefix="uc1" TagName="ucProcesando" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Acceso al Sistema</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <%--<link href="~/Estilos/aerp.css" rel="stylesheet" />--%>
    <style type="text/css">
        #contenedor {
            height: 100%;
            width: 100%;
        }
         #cabecera {
             /*background-color: red;*/
             /*height: 10%;*/
             height: 0%;
         }
        #menu {
            /*background-color: yellow;*/
            height: 5%;
            width: 100%;
        }
        #izquierda {
            /*background-color: orange;*/
            height: 60%;
            float: left;
            width: 5%;
        }
        #centro {
            /*background-color: blue;*/
            height: 60%;
            float: left;
            width: 90%;
        }
        #derecha {
            /*background-color: green;*/
            height: 60%;
            float: right;
            width: 5%;
        }
        #pie {
            /*background-color: brown;*/
            text-align: center;
            margin: auto;
            height: 35%;
            clear: both;
        }
        #logo {
            /*background-color: violet;*/
            float: left;
            width: 40%;
            position: relative;
            top: 20%;
            height: 50%;
            text-align: center;
        }
        #formulario {
            /*background-color: gray;*/
            /*float: right;*/
            margin: auto;
            width: 70%;
            position: relative;
            top: 20%;
            height: 50%;

            /*border-left: 6px #00ACEE solid;*/

            /*border: 6px #00ACEE solid;*/

            display: table;

            /*border: 1px #014386 solid;

            -webkit-border-radius: 20px;
            -moz-border-radius: 20px;
            border-radius: 20px;*/
        }
        #formulario table {
            -webkit-border-radius: 20px;
            -moz-border-radius: 20px;
            border-radius: 20px;

            /*border: 1px #000000 solid;*/

            /*background: #1e5799;
            background: -moz-linear-gradient(top, #1e5799 0%, #2989d8 50%, #2989d8 51%, #1e5799 1000%);
            background: -webkit-linear-gradient(top, #1e5799 0%, #2989d8 50%, #2989d8 51%, #1e5799 100%);
            background: linear-gradient(to bottom, #1e5799 0%, #2989d8 50%, #2989d8 51%, #1e5799 100%);
            filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#1e5799', endColorstr='#7db9e8',GradientType=0);*/
        }
        td {
            border: 1px none black;
        }
        .divRedondeado{
            border: 1px #014386 solid;

            -webkit-border-radius: 6px;
            -moz-border-radius: 6px;
            border-radius: 6px;

            margin: auto;
            text-align: center;
            width: 40%;
        }
    </style>
</head>
<body oncontextmenu="JavaScript:return true;" onload="Desbloquear();" onunload="Bloquear();" onbeforeunload="">
    <form id="frmLogin" runat="server" DefaultButton="btnIniciarSesion">
        <uc1:ucProcesando runat="server" id="ucProcesando" />
        <asp:ScriptManager EnablePageMethods="True" runat="server"></asp:ScriptManager>
        <div id="contenedor">
            <div id="cabecera"></div>
            <div id="menu"></div>
            <div id="izquierda"></div>
            <div id="centro">
                <%--<div id="logo" style="height: 50%; display: table; top: 50%;">
                    <asp:Image id="imgLogoRcIngenieros" runat="server" Width="80%" ImageUrl="~/Imagenes/LogoPrimax.png" />
                    <br/>
                </div>--%>
                <div id="formulario">
                    <%--<table style="border: 1px none red; margin: 0 auto; position: relative; top: 7%; vertical-align:middle; width: 100%;">--%>
                    <table style="width: 100%;">
                        <tr>
                            <td colspan="4" style="text-align: center; ">
                                <asp:Image id="imgLogoApu" ImageUrl="~/Imagenes/LoginApu.png" Height="100%" runat="server" />
                            </td>
                        </tr>
                    </table>
                    <br />
                    <div class="divRedondeado">
                        <table style="margin: 0 auto; position: relative; top: 7%; vertical-align:middle; width: 100%;">
                            <tr>
                                <td style="width: 10%;">&nbsp;</td>
                                <td style="width: 30%;">&nbsp;</td>
                                <td style="width: 50%;">&nbsp;</td>
                                <td style="width: 10%;">&nbsp;</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td colspan="2" style="color:#014386;font-family:SairaSemiCondensed; font-size:12pt; font-weight: bold;">Usuario</td>
                                <%--<td>&nbsp;</td>--%>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td></td>
                                <%--<td style="color:#014386;font-family:Roboto; font-size:12pt; font-weight: bold;">Usuario</td>--%>
                                <%--<td>&nbsp;</td>--%>
                                <td colspan="2">
                                    <asp:TextBox id="txtUsuario" runat="server" CssClass="txtStandar" Width="100%"></asp:TextBox>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td colspan="4">&nbsp;</td>
                            </tr>
                            <tr>
                                <td></td>
                                <td colspan="2" style="color:#014386;font-family:SairaSemiCondensed;font-size:12pt; font-weight: bold;">Contrase&ntilde;a</td>
                                <td>&nbsp;</td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <%--<td style="color:#014386;font-family:Roboto;font-size:12pt; font-weight: bold;">Contrase&ntilde;a</td>--%>
                                <td colspan="2">
                                    <asp:TextBox id="txtPassword" TextMode="Password" runat="server" CssClass="txtStandar" Width="100%"></asp:TextBox>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td colspan="2">
                                    <span id="lblMensaje" class="lblMensaje"></span>
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center;">
                                    <asp:Button id="btnIniciarSesion" Text="INICIAR SESI&Oacute;N" CssClass="btnStandar" Width="60%" OnClientClick="return IniciarSesion();" runat="server"/>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center;">
                                    <asp:LinkButton ID="lnkRecordar" Text="Recuperar Contraseña&nbsp;" CssClass="vinculo" style="color: #ff4500; font-family: SairaSemiCondensed; font-size: 10pt; text-align: center;" OnClientClick="return RecuperarClave();" runat="server"></asp:LinkButton>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">&nbsp;</td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div id="derecha"></div>
            <div id="pie">
                <asp:Image id="imgPieApu" runat="server" ImageUrl="~/Imagenes/FondoApu.png" />
            </div>
        </div>
        <asp:Button id="btnDefault" OnClientClick="return false;" runat="server" />
    </form>
    <script type="text/javascript">
        function IniciarSesion() {
            var usuario = document.getElementById('txtUsuario');
            if (usuario.value.trim() == '') {
                return MostrarValidacion(usuario, 'Ingrese el Usuario.');
            }
            //if (!ValidarCorreo(usuario.value)) {
            //    return MostrarValidacion(usuario, 'El usuario debe ser un email: aaaa@bbb.ccc.dd');
            //}
            var clave = document.getElementById('txtPassword');
            if (clave.value.trim() == '') {
                return MostrarValidacion(clave, 'Ingrese la contraseña.');
            }
            // Bloqueo Pantalla
            Bloquear();
            PageMethods.IniciarSesion(usuario.value, clave.value, IniciarSesionOk, fnLlamadaError);
            return false;
        }
        function IniciarSesionOk(res) {
            if (res != '') {
                document.getElementById('btnIniciarSesion').className = 'btnStandarSeleccionado';
                document.getElementById('btnIniciarSesion').value = 'Inicio Satisfactorio';
                document.getElementById('lblMensaje').innerText = 'Nombre de Usuario o Clave correcto.';
                // window.location.href = getRootWebSitePath() + res.replace('~', '');
                window.location.href = res;
            } else {
                document.getElementById('lblMensaje').innerText = 'Nombre de Usuario o Clave equivocada.';
                Desbloquear();
            }
        }
        function fnLlamadaError(excepcion) {
            alert('Ha ocurrido un error interno: ' + excepcion.get_message());
            // Desbloquear Pantalla
            Desbloquear();
        }
        function RecuperarClave() {
            var usuario = document.getElementById('txtUsuario');
            if (usuario.value.trim() == '') {
                return MostrarValidacion(usuario, 'Ingrese el Usuario.');
            }
            Bloquear();
            PageMethods.RecuperarClave(usuario.value.trim(), RecuperarClaveOk, fnLlamadaError);
            return false;
        }
        function RecuperarClaveOk(r) {
            if (r != '') {
                document.getElementById('lblMensaje').innerText = r;
            } else {
                document.getElementById('lblMensaje').innerText = 'Nombre de Usuario equivocado.';
            }
            Desbloquear();
            return false;
        }
    </script>
</body>
</html>