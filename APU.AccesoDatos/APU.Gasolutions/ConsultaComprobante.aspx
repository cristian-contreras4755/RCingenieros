<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConsultaComprobante.aspx.cs" Inherits="APU.Gasolutions.ConsultaComprobante" %>
<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=15.1.1.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>
<%@ Register Src="~/Controles/ucProcesando.ascx" TagPrefix="uc1" TagName="ucProcesando" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Consulta de Comprobante</title>
    <style type="text/css">
        #contenedor {
            height: 100%;
            width: 100%;
        }
        #cabecera {
            /*background-color: red;*/
            height: 10%;
        }
        #menu {
            /*background-color: yellow;*/
            height: 5%;
        }
        #izquierda {
            /*background-color: orange;*/
            height: 85%;
            float: left;
            width: 5%;
        }
        #centro {
            /*background-color: blue;*/
            /*border: 1px solid red;*/
            height: 85%;
            float: left;
            width: 90%;
        }
        #derecha {
            /*background-color: green;*/
            height: 85%;
            float: right;
            width: 5%;
        }
        #pie {
            /*background-color: brown;*/
            height: 0%;
            clear: both;
        }
        td {
            border: 1px none black;
        }
        .tblSinBordes {
            border-collapse: collapse;
            /*border-style: solid;*/
        }
    </style>
</head>
<body oncontextmenu="JavaScript:return true;" onload="Desbloquear();" onunload="Bloquear();" onbeforeunload="">
    <form id="frmConsultaComprobante" runat="server">
        <uc1:ucProcesando runat="server" ID="ucProcesando1" />
        <asp:ScriptManager EnablePageMethods="True" runat="server"></asp:ScriptManager>
        <div id="contenedor">
            <table style="width: 100%;">
                <tr>
                    <td style="text-align: center;" colspan="3">
                        <asp:Label CssClass="lblTitulo" Width="100%" runat="server">VENTAS</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 30%;">&nbsp;</td>
                    <td style="width: 40%;">
                        <table style="width: 100%;">
                            <tr>
                                <td class="lblStandar">Tipo Documento</td>
                                <td>
                                    <asp:DropDownList id="ddlTipoDocumento" CssClass="ddlStandar" Width="80%" runat="server">
                                        <asp:ListItem Text="Seleccione" Value="0" />
                                        <asp:ListItem Text="Factura" Value="01" />
                                        <asp:ListItem Text="Boleta" Value="03" />
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="lblStandar">Serie</td>
                                <td>
                                    <asp:TextBox id="txtSerie" CssClass="txtStandar" Width="80%" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="lblStandar">Correlativo</td>
                                <td>
                                    <asp:TextBox id="txtCorrelativo" CssClass="txtStandarRight" Width="80%" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="lblStandar">Fecha de Emisi&oacute;n</td>
                                <td>
                                    <asp:TextBox id="txtFechaEmision" CssClass="txtStandarFecha" Width="60%" runat="server"></asp:TextBox>&nbsp;
                                    <asp:ImageButton ID="imgFechaEmision" CssClass="imgCalendario" ImageUrl="~/Scripts/dhtmlxCalendar/calendar.gif" runat="server"/>
                                    <ajaxToolkit:CalendarExtender runat="server" ID="ceFechaEmision" BehaviorID="bceFechaEmision" CssClass="custom-calendar" TargetControlID="txtFechaEmision" Format="dd/MM/yyyy" PopupButtonID="imgFechaEmision" />
                                </td>
                            </tr>
                            <tr>
                                <td class="lblStandar">Monto Total</td>
                                <td>
                                    <asp:TextBox id="txtMontoTotal" CssClass="txtStandarMonto" Width="80%" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="3" style="text-align: center;">
                                    <input type="button" id="btnVerDocumento" runat="server" value="Ver Documento" style="width: 20%;" class="btnStandar" onclick="return VerDocumento();" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="3" style="text-align: center;">
                                    <a id="lnkNumeroComprobantePdf" style="cursor: pointer; visibility: hidden;">
                                        <asp:Label ID="lblNumeroComprobantePdf" Text="Descargar PDF" runat="server"></asp:Label>
                                    </a>&nbsp;
                                    <a id="lnkNumeroComprobanteXml" style="cursor: pointer; visibility: hidden;">
                                        <asp:Label ID="lblNumeroComprobantexml" Text="Descargar XML" runat="server"></asp:Label>
                                    </a>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 30%;">&nbsp;</td>
                </tr>
            </table>
        </div>
    </form>
    <script type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_initializeRequest(InitializeRequest);
        prm.add_endRequest(EndRequest);
        var postBackElement;
        function InitializeRequest(sender, args) {
            if (prm.get_isInAsyncPostBack())
                args.set_cancel(true);
            postBackElement = args.get_postBackElement();
            if (postBackElement.id == 'btnVerDocumento' || postBackElement.id == 'btnCargarVentas') {
                // Bloqueo Pantalla
                Bloquear();
            }
        }
        function EndRequest(sender, args) {
            if (postBackElement.id == 'btnVerDocumento' || postBackElement.id == 'btnCargarVentas') {
                // Desbloquear Pantalla
                Desbloquear();
                //RedimensionarGrid();
            }
        }
        function VerDocumento() {
            // debugger;
            Bloquear(100002);
            var numeroDocumentoValue = "";
            var tipoDocumentoId = document.getElementById('ddlTipoDocumento');
            var tipoDocumentoIdValue = tipoDocumentoId.value;
            var serie = document.getElementById('txtSerie');
            var serieValue = serie.value.trim();
            var correlativo = document.getElementById('txtCorrelativo');
            var correlativoValue = correlativo.value.trim();
            var fechaEmision = document.getElementById('txtFechaEmision');
            var fechaEmisionValue = fechaEmision.value.trim();
            var montoTotal = document.getElementById('txtMontoTotal');
            var montoTotalValue = montoTotal.value.trim().replace(/,/g, '');
            montoTotalValue = (montoTotalValue == '') ? 0 : montoTotalValue;
            montoTotalValue = parseFloat(montoTotalValue);

            if (tipoDocumentoIdValue == '0') {
                return MostrarValidacion(tipoDocumentoId, 'Seleccione el Tipo de Comprobante.');
            }
            if (serieValue == '') {
                return MostrarValidacion(serie, 'Ingrese la serie.');
            }
            if (correlativoValue == '') {
                return MostrarValidacion(correlativo, 'Ingrese el correlativo.');
            }
            if (fechaEmisionValue == '') {
                return MostrarValidacion(fechaEmision, 'Ingrese la Fecha de Emisión.');
            }
            if (montoTotalValue == 0) {
                return MostrarValidacion(montoTotal, 'El monto del comprobante debe ser mayor a 0.');
            }

            PageMethods.ImprimirComprobante(numeroDocumentoValue, tipoDocumentoIdValue, serieValue, correlativoValue, fechaEmisionValue, montoTotalValue, ImprimirComprobanteOk, fnLlamadaError);
            //PageMethods.ImprimirComprobante(tipoDocumetoId, ImprimirComprobanteOk, fnLlamadaError);
            return false;
        }
        function ImprimirComprobanteOk(c) {
            var lnkNumeroComprobantePdf = document.getElementById('lnkNumeroComprobantePdf');
            var lnkNumeroComprobanteXml = document.getElementById('lnkNumeroComprobanteXml');
            if (c != '') {

                var arrayComprobante = c.split('@');
                var mensaje = arrayComprobante[0];
                var nombreComprobante = arrayComprobante[1];
                var numeroDocumentoCliente = arrayComprobante[2];

                MostrarMensaje(mensaje);

                if (nombreComprobante == '') {
                    lnkNumeroComprobantePdf.style.visibility = 'hidden';
                    lnkNumeroComprobanteXml.style.visibility = 'hidden';
                } else {
                    lnkNumeroComprobantePdf.style.visibility = 'visible';
                    lnkNumeroComprobantePdf.onclick = function () {
                        window.open('Archivos/Documentos/Cliente/' + numeroDocumentoCliente + '/' + nombreComprobante + '.pdf', '_blank');
                    };

                    lnkNumeroComprobanteXml.style.visibility = 'visible';
                    lnkNumeroComprobanteXml.onclick = function () {
                        window.open('Archivos/Documentos/Cliente/' + numeroDocumentoCliente + '/' + nombreComprobante + '.xml', '_blank');
                    };   
                }
            } else {
                MostrarMensaje(c);
                lnkNumeroComprobantePdf.style.visibility = 'hidden';
                lnkNumeroComprobanteXml.style.visibility = 'hidden';
            }
            Desbloquear();
        }
        function fnLlamadaError(excepcion) {
            alert('Ha ocurrido un error interno: ' + excepcion.get_message());
            Desbloquear();
        }
    </script>
</body>
</html>