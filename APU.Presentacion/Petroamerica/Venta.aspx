<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Venta.aspx.cs" Inherits="APU.Presentacion.Petroamerica.VentaPetroamerica" EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="uc1" TagName="ucProcesando" Src="~/Controles/ucProcesando.ascx" %>
<%@ Register Src="~/Controles/ucCabecera.ascx" TagPrefix="uc1" TagName="ucCabecera" %>
<%@ Register Src="~/Controles/ucMenu.ascx" TagPrefix="uc1" TagName="ucMenu" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Ventas</title>
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
<script type="text/javascript">
    function Buscar() {
        var grid = document.getElementById('grvVenta');
        var terms = document.getElementById('txtBuscar').value.toUpperCase();
        var ele, eleNombre, eleTelefono, eleCorreo, eleDepartamento, eleCargo, eleDirector;
        var cellNombre, cellTelefono, cellCorreo, cellDepartamento, cellCargo, cellDirector;
        cellNombre = 1, cellTelefono = 2, cellCorreo = 3, cellDepartamento = 4, cellCargo = 5, cellDirector = 6;
        if (grid != null) {
            if (grid.rows.length > 0) {
                for (var r = 1; r < grid.rows.length; r++) {
                    eleNombre = grid.rows[r].cells[cellNombre].innerText.replace(/<[^>]+>/g, "");
                    eleTelefono = grid.rows[r].cells[cellTelefono].innerText.replace(/<[^>]+>/g, "");
                    eleCorreo = grid.rows[r].cells[cellCorreo].innerText.replace(/<[^>]+>/g, "");
                    eleDepartamento = grid.rows[r].cells[cellDepartamento].innerText.replace(/<[^>]+>/g, "");
                    eleCargo = grid.rows[r].cells[cellCargo].innerText.replace(/<[^>]+>/g, "");
                    eleDirector = grid.rows[r].cells[cellDirector].innerText.replace(/<[^>]+>/g, "");
                    ele = eleNombre + eleTelefono + eleCorreo + eleDepartamento + eleCargo + eleDirector;
                    if (ele.toUpperCase().indexOf(terms) >= 0) {
                        grid.rows[r].style.display = '';
                    } else {
                        grid.rows[r].style.display = 'none';
                    }
                }
            }
        }
    }
    function Onscrollfnction() {
        var div = document.getElementById('divData');
        var div2 = document.getElementById('divHeader');
        div2.scrollLeft = div.scrollLeft;
        return false;
    }
    function VerVenta(ventaPetroamericaId, ventaId) {
        PageMethods.ObtenerVenta(ventaPetroamericaId, ventaId, ObtenerVentaOk, fnLlamadaError);
        return false;
    }
    function LimpiarVenta() {
        var hdnVentaPetroamericaId = document.getElementById('<%=hdnVentaPetroamericaId.ClientID%>');
        var hdnVentaId = document.getElementById('<%=hdnVentaId.ClientID%>');
        <%--var ddlEmpresa = document.getElementById('<%=ddlEmpresa.ClientID%>');
        var txtCodigo = document.getElementById('<%=txtCodigo.ClientID%>');
        var txtNombre = document.getElementById('<%=txtNombre.ClientID%>');
        var txtDescripcion = document.getElementById('<%=txtDescripcion.ClientID%>');--%>
        var chkActivo = document.getElementById('<%=chkActivo.ClientID%>');
        var txtDireccion = document.getElementById('<%=txtDireccion.ClientID%>');
        var lblModificadoPor = document.getElementById('<%=lblModificadoPor.ClientID%>');
        var lblFechaModificacion = document.getElementById('<%=lblFechaModificacion.ClientID%>');

        hdnVentaId.value = 0;
        hdnVentaPetroamericaId.value = 0;
        //ddlEmpresa.value = 0;
        //txtNombre.value = '';
        //txtCodigo.value = '';
        //txtDescripcion.value = '';
        chkActivo.checked = true;
        txtDireccion.value = '';
        lblModificadoPor.innerText = '';
        lblFechaModificacion.innerText = '';

        var tabVenta = $get('<%=tabVenta.ClientID%>');
        tabVenta.control.set_activeTabIndex(0);
    }
    function ObtenerVentaOk(v) {
        var modalDialog = $find('mpeVenta');
        if (v != null) {
            modalDialog.show();

            var tabVenta = $get('<%=tabVenta.ClientID%>');
            tabVenta.control.set_activeTabIndex(0);

            var hdnVentaId = document.getElementById('<%=hdnVentaId.ClientID%>');
            var hdnVentaPetroamericaId = document.getElementById('<%=hdnVentaPetroamericaId.ClientID%>');
            var txtCliente = document.getElementById('<%=txtCliente.ClientID%>');
            var lblNumeroDocumento = document.getElementById('<%=lblNumeroDocumento.ClientID%>');
            var ddlTipoComprobante = document.getElementById('<%=ddlTipoComprobante.ClientID%>');
            var txtNumeroComprobante = document.getElementById('<%=txtNumeroComprobante.ClientID%>');
            var txtNumeroSerie = document.getElementById('<%=txtNumeroSerie.ClientID%>');
            var txtNumeroGuia = document.getElementById('<%=txtNumeroGuia.ClientID%>');
            var txtGlosa = document.getElementById('<%=txtGlosa.ClientID%>');
            var lblNumeroComprobante = document.getElementById('<%=lblNumeroComprobante.ClientID%>');
            var lnkNumeroComprobante = document.getElementById('lnkNumeroComprobante');
            
            var chkActivo = document.getElementById('<%=chkActivo.ClientID%>');
            var txtDireccion = document.getElementById('<%=txtDireccion.ClientID%>');
            var lblModificadoPor = document.getElementById('<%=lblModificadoPor.ClientID%>');
            var lblFechaModificacion = document.getElementById('<%=lblFechaModificacion.ClientID%>');

            var lblMensajeSunat = document.getElementById('<%=lblMensajeSunat.ClientID%>');

            hdnVentaPetroamericaId.value = v.VentaPetroamericaId;
            hdnVentaId.value = v.VentaId;
            txtCliente.value = v.Cliente;
            lblNumeroDocumento.innerText = v.NumeroDocumentoCliente;
            ddlTipoComprobante.value = v.TipoComprobanteId;
            document.getElementById('lblTituloVenta').innerText = v.Serie + '-' + v.NumeroComprobante;

            lblNumeroComprobante.innerText = v.Serie + '-' + v.NumeroComprobante;
            lnkNumeroComprobante.onclick = function () { return ImprimirComprobante(v.NumeroDocumentoCliente, v.ComprobanteImpreso); };

            txtNumeroComprobante.value = v.NumeroComprobante;
            txtNumeroSerie.value = v.Serie;
            //txtNumeroGuia.value = v.NumeroGuia;
            //txtGlosa.value = v.Glosa;

            // txtDireccion.value = v.Direccion;
            //chkActivo.checked = (v.Activo > 0);
            // txtDireccion.value = v.Direccion;
            //lblModificadoPor.innerText = v.UsuarioModificacion;
            //lblFechaModificacion.innerText = FormatoFecha(v.FechaModificacion);

            if (v.Exito) {
                lblMensajeSunat.innerText = v.MensajeRespuesta;
            } else {
                lblMensajeSunat.innerText = v.MensajeError;
            }
        } else {
            modalDialog.hide();
        }
    }
    function InsertarVenta() {
        var modalDialog = $find('mpeVenta');

        modalDialog.show();
        LimpiarVenta();

        return false;
    }
    function ValidarVenta() {
        var hdnVentaId = document.getElementById('<%=hdnVentaId.ClientID%>');
        var chkActivo = document.getElementById('<%=chkActivo.ClientID%>');

        if (ddlEmpresa.value.trim() == '0') {
            return MostrarValidacion(ddlEmpresa, 'Seleccione la Empresa.');
        }
        //if (txtNombre.value.trim() == '') {
        //    return MostrarValidacion(txtNombre, 'Ingrese el Nombre de la Venta.');
        //}
    }
    function GuardarVentaOk(res) {
        var modalDialog = $find('mpeVenta');
        var arr = res.split('@');
        var ventaId = arr[0];
        var mensaje = arr[1];

        if (ventaId > 0) {
            alert('Se registró la información de la Venta correctamente.');
            modalDialog.hide();
            LimpiarVenta();
        } else {
            alert(mensaje);
        }
        Desbloquear();
    }
    function fnLlamadaError(excepcion) {
        alert('Ha ocurrido un error interno: ' + excepcion.get_message());
        Desbloquear();
    }
    function ImprimirComprobante(numeroDocumento, comprobanteImpreso) {
        // AbrirVentana('../Archivos/Documentos/Cliente/20519069262/Factura_14112017095950.pdf', 'Comprobante', 800, 600);
        AbrirVentana('../Archivos/Documentos/Cliente/' + numeroDocumento + '/' + comprobanteImpreso, 'Comprobante', 800, 600);
    }
    function Cerrar() {
        var modalDialog = $find('mpeVenta');
        modalDialog.hide();
    }
</script>
    <form id="frmVenta" runat="server" DefaultButton="btnDefault">
    <uc1:ucProcesando runat="server" id="ucProcesando" />
    <asp:ScriptManager EnablePageMethods="True" runat="server"></asp:ScriptManager>
    <div id="contenedor">
    <div id="cabecera">
        <uc1:ucCabecera runat="server" id="ucCabecera" />
    </div>
    <div id="menu">
        <uc1:ucMenu runat="server" ID="ucMenu" />
    </div>
    <div id="izquierda"></div>
    <div id="centro">
        <br/>
        <asp:Button id="btnDefault" OnClientClick="return false;" style="display: none;" runat="server" />
        <table style="width: 100%;">
            <tr>
                <td style="width: 100%;">
                    <table style="width: 100%; margin: 0;">
                        <tr>
                            <td style="text-align: center;" colspan="5">
                                <asp:Label CssClass="lblTitulo" Width="100%" runat="server">VENTAS</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                <table style="width: 100%;">
                                    <tr>
                                        <td class="lblStandar" style="width: 10%;">RUC</td>
                                        <td style="width: 15%;">
                                            <asp:TextBox id="txtNumeroDocumentoBuscar" CssClass="txtStandar" Width="80%" runat="server"></asp:TextBox>
                                        </td>
                                        <td class="lblStandar" style="width: 10%;">Tipo Comprobante</td>
                                        <td style="width: 15%;">
                                            <asp:DropDownList id="ddlTipoComprobanteBuscar" CssClass="ddlStandar" Width="80%" runat="server"></asp:DropDownList>
                                        </td>
                                        <td class="lblStandar" style="width: 10%;">Fecha Emisi&oacute;n</td>
                                        <td style="width: 15%;">
                                            De&nbsp;<asp:TextBox id="txtFechaEmisionInicioBuscar" CssClass="txtStandarRight" Width="35%" runat="server"></asp:TextBox>&nbsp;
                                            Hasta&nbsp;<asp:TextBox id="txtFechaEmisionFinBuscar" CssClass="txtStandarRight" Width="35%" runat="server"></asp:TextBox>
                                        </td>
                                        <td class="lblStandar" style="width: 10%;">Estado</td>
                                        <td style="width: 15%;">
                                            <asp:DropDownList id="ddlEstadoBuscar" CssClass="ddlStandar" Width="80%" runat="server">
                                                <asp:ListItem Text="Todos" Value="0" />
                                                <asp:ListItem Text="Aceptado" Value="1" />
                                                <asp:ListItem Text="Rechazado" Value="2" />
                                                <asp:ListItem Text="Pendiente" Value="3" />
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblStandar">Moneda</td>
                                        <td>
                                            <asp:DropDownList id="ddlMonedaBuscar" CssClass="ddlStandar" Width="80%" runat="server"></asp:DropDownList>
                                        </td>
                                        <td class="lblStandar">Estaci&oacute;n</td>
                                        <td>
                                            <asp:DropDownList id="ddlAgencia" CssClass="ddlStandar" Width="80%" runat="server"></asp:DropDownList>
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:Button ID="btnCargarVentas" runat="server" CssClass="btnStandar" Text="Buscar" Width="50%" OnClick="btnCargarVentas_OnClick" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 10%;">
                                Ver:&nbsp;
                                <asp:DropDownList id="ddlNumeroRegistros" CssClass="ddlStandar" Width="50%" AutoPostBack="True" runat="server" OnSelectedIndexChanged="ddlNumeroRegistros_SelectedIndexChanged">
                                    <%--<asp:ListItem Text="Todos" Value="0" />--%>
                                    <asp:ListItem Text="20" Value="20" />
                                    <asp:ListItem Text="50" Value="50" />
                                    <asp:ListItem Text="100" Value="100" />
                                </asp:DropDownList>
                            </td>
                            <td style="width: 20%; visibility: hidden;">
                                <asp:LinkButton ID="imgAnadir" ForeColor="#404040" runat="server" ToolTip="" OnClientClick="return InsertarVenta();">
                                    <asp:Image runat="server" ImageUrl="~/Imagenes/Iconos/nuevo.png" BorderStyle="None" ToolTip="Nuevo"/>&nbsp;Nuevo
                                </asp:LinkButton>
                            </td>
                            <td style="width: 20%;">&nbsp;</td>
                            <td style="width: 20%;">&nbsp;</td>
                            <td style="width: 30%;">Buscar:
                                <asp:TextBox id="txtBuscar" CssClass="txtStandar" Width="80%" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="width: 100%;">
                    <div id="divHeader" style="width: 1182px; overflow-x: hidden; overflow-y: hidden; margin: 0 0px 0 0px;" >
                        <table id="dummyHeader" style="width: 2000px;" class="tblSinBordes">
                            <thead>
                            <tr class="filaCabeceraGrid">
                                <th style="width: 5%; cursor: default; text-decoration: none;">Acciones</th>
                                <th style="width: 5%; cursor: pointer; text-decoration: underline;">Estado</th>
                                <th style="width: 5%; cursor: pointer; text-decoration: underline;">Tipo Comprobante</th>
                                <th style="width: 7%; cursor: pointer; text-decoration: underline;"># Comprobante</th>
                                <th style="width: 10%; cursor: pointer; text-decoration: underline;">Cliente</th>
                                <th style="width: 5%; cursor: pointer; text-decoration: underline;">Moneda</th>
                                <th style="width: 5%; cursor: pointer; text-decoration: underline;">Fecha Emisi&oacute;n</th>
                                <th style="width: 6%; cursor: pointer; text-decoration: underline;">Monto</th>
                                <th style="width: 6%; cursor: pointer; text-decoration: underline;">IGV</th>
                                <th style="width: 6%; cursor: pointer; text-decoration: underline;">Monto Total</th>
                                <th style="width: 5%; cursor: pointer; text-decoration: underline;">Fecha Vencimiento</th>
                                <th style="width: 25%; cursor: pointer; text-decoration: underline;">Respuesta</th>
                                <th style="width: 10%; cursor: pointer; text-decoration: underline;">Agencia</th>
                            </tr>
                            </thead>
                        </table>
                    </div>
                    <asp:UpdatePanel ID="divData" runat="server" UpdateMode="Conditional" style="overflow: scroll; width: 1200px; height: 420px; margin: 0 0 0px 0px;" onscroll="Onscrollfnction();">
                        <ContentTemplate>
                            <asp:GridView id="grvVenta" AutoGenerateColumns="False" Width="2000px" RowStyle-Wrap="True" runat="server" OnRowDataBound="grvVenta_RowDataBound">
                                <HeaderStyle CssClass="filaCabeceraGrid"></HeaderStyle>
                                <RowStyle CssClass="filaImparGrid"></RowStyle>
                                <AlternatingRowStyle CssClass="filaParGrid"></AlternatingRowStyle>
                                <Columns>
                                    <%-- 0: Acciones --%>
                                    <asp:TemplateField HeaderText="Acciones">
                                        <ItemStyle Width="5%" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ImageUrl="~/Imagenes/Iconos/editar.png" CssClass="imgButton" OnClientClick='<%# "return VerVenta(" + Eval("VentaPetroamericaId") + "," + Eval("VentaId") + ");" %>' ToolTip="Editar" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- 10: Estado --%>
                                    <asp:TemplateField HeaderText="Estado">
                                        <ItemStyle Width="5%" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <%--<%# Convert.ToInt32(Eval("Exito"))>0?"Aceptado":"Rechazado" %>--%>
                                            <%# ObtenerEstado(Convert.ToInt32(Eval("Exito")), Eval("MensajeError").ToString()) %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--  1: TipoComprobante --%>
                                    <asp:TemplateField HeaderText="Nombre">
                                        <ItemStyle Width="5%" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <%# Eval("TipoComprobante").ToString().Trim() %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- 2: # Comprobante --%>
                                    <asp:TemplateField HeaderText="# Comprobante">
                                        <ItemStyle Width="7%" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <%--<asp:HyperLink Target="_blank" Text='<%# Eval("Serie").ToString().Trim() + "-" + Eval("NumeroComprobante").ToString().Trim() %>' NavigateUrl='<%# Page.ResolveClientUrl("~/Archivos/Documentos/Cliente") + "/" + Eval("NumeroDocumentoCliente") + "/" + Eval("ComprobanteImpreso") + "?v=" + Guid.NewGuid().ToString() %>' runat="server" data-toogle="tooltip" ToolTip="Descargar PDF" title="Descargar PDF"></asp:HyperLink>--%>
                                            <asp:HyperLink Target="_blank" Text='<%# Eval("Serie").ToString().Trim() + "-" + Eval("NumeroComprobante").ToString().Trim() %>' onclick=<%# "ImprimirComprobante('" + Eval("NumeroDocumentoCliente") + "', '" + Eval("ComprobanteImpreso") + "');" %> runat="server" data-toogle="tooltip" ToolTip="Descargar PDF" title="Descargar PDF" style="cursor: pointer;"></asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- 3: Cliente --%>
                                    <asp:TemplateField HeaderText="Cliente">
                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <%# Eval("Cliente")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- 4: Moneda --%>
                                    <asp:TemplateField HeaderText="Moneda">
                                        <ItemStyle Width="5%" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <%# Eval("SimboloMoneda")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- 5: Fecha Emisión --%>
                                    <asp:TemplateField HeaderText="Fecha Emisi&oacute;n">
                                        <ItemStyle Width="5%" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <%# Convert.ToDateTime(Eval("FechaEmision")).ToString("dd/MM/yyyy") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- 7: Monto --%>
                                    <asp:TemplateField HeaderText="Monto">
                                        <ItemStyle Width="6%" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <%# Convert.ToDecimal(Eval("MontoVenta")).ToString("N2") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- 8: MontoImpuesto --%>
                                    <asp:TemplateField HeaderText="MontoImpuesto">
                                        <ItemStyle Width="6%" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <%# Convert.ToDecimal(Eval("MontoImpuesto")).ToString("N2") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- 9: Monto Total --%>
                                    <asp:TemplateField HeaderText="Monto Total">
                                        <ItemStyle Width="6%" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <%# Convert.ToDecimal(Eval("MontoTotal")).ToString("N2") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- 6: Fecha Vencimiento --%>
                                    <asp:TemplateField HeaderText="Fecha Vencimiento">
                                        <ItemStyle Width="5%" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <%--<%# Convert.ToDateTime(Eval("FechaVencimiento")).ToString("dd/MM/yyyy") %>--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- 11: Forma Pago --%>
                                    <asp:TemplateField HeaderText="Forma Pago">
                                        <ItemStyle Width="25%" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <%--<%# Convert.ToInt32(Eval("Exito"))>0?Eval("MensajeRespuesta"):Eval("MensajeError") %>--%>
                                            <%# ObtenerRespuesta(Convert.ToInt32(Eval("Exito")), Eval("MensajeError").ToString(), Eval("MensajeRespuesta").ToString()) %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- 12: Agencia --%>
                                    <asp:TemplateField HeaderText="Agencia">
                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <%# Eval("Agencia") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlNumeroRegistros" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="btnPaginacion" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnGuardarVenta" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnCargarVentas" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <div id="divFooter" style="width: 1182px; overflow-x: hidden; overflow-y: hidden; margin: 0 0px 0 0px;" >
                        <table style="width: 100%">
                            <tr class="filaPaginacionGrid">
                                <td style="width: 40%; text-align: left;">
                                    <asp:Label ID="lblPaginacion" CssClass="lblStandarBold" runat="server"></asp:Label>
                                </td>
                                <td style="width: 10%;">&nbsp;</td>
                                <td style="text-align: right; width: 50%;">
                                    <input type="button" id="btnPrimero" class="btnPaginacion" runat="server" style="width: 24%;" value="Primero" onclick="Paginacion(this);" />
                                    <input type="button" id="btnAnterior" class="btnPaginacion" runat="server" style="width: 24%;" value="Anterior" onclick="Paginacion(this);" />
                                    <input type="button" id="btnSiguiente" class="btnPaginacion" runat="server" style="width: 24%;" value="Siguiente" onclick="Paginacion(this);" />
                                    <input type="button" id="btnUltimo" class="btnPaginacion" runat="server" style="width: 24%;" value="&Uacute;ltimo" onclick="Paginacion(this);" />
                                    <input type="hidden" id="hdnCommandArgument" value="" runat="server" />
                                    <asp:Button id="btnPaginacion" runat="server" style="display: none;" OnClick="btnPaginacion_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div id="derecha"></div>
    <asp:Panel ID="pnlVenta" runat="server" CssClass="pnlModal" style="display: none;">
        <table style="width: 100%;">
            <tr id="lblTituloPopupVenta" class="lblTituloPopup">
                <td style="width: 20%;" class="lblTituloPopup">&nbsp;</td>
                <td id="lblTituloVenta" class="lblTituloPopup" style="width: 60%">Venta</td>
                <td class="lblTituloPopup" style="width: 20%; text-align: right;">
                    <a onclick="return Cerrar();" style="color: #FFFFFF; text-decoration: none; cursor: pointer;">Cerrar X</a>&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <input type="hidden" id="hdnVentaPetroamericaId" value="0" runat="server" />
                    <input type="hidden" id="hdnVentaId" value="0" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="3" style="width: 100%;">
                    <ajaxToolkit:TabContainer id="tabVenta" runat="server" OnClientActiveTabChanged="" Height="400px" Width="600px" ActiveTabIndex="0" OnDemand="False" AutoPostBack="false"
                                              TabStripPlacement="Top" CssClass="ajax__tab_xp" ScrollBars="None" UseVerticalStripPlacement="False" VerticalStripWidth="120px">
                        <ajaxToolkit:TabPanel id="tabVentaInformacion" runat="server" HeaderText="Informaci&oacute;n de Venta" Enabled="true" ScrollBars="Auto" OnDemandMode="Always">
                            <ContentTemplate>
                                <table style="width: 100%;">
                                    <tr>
                                        <td class="lblStandar" style="width: 30%;">Cliente</td>
                                        <td>
                                            <asp:TextBox id="txtCliente" CssClass="txtStandar" Width="80%" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblStandar">N&deg; Documento</td>
                                        <td>
                                            <asp:Label id="lblNumeroDocumento" CssClass="lblStandar" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblStandar">Tipo Comprobante</td>
                                        <td>
                                            <asp:DropDownList id="ddlTipoComprobante" CssClass="ddlStandar" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblStandar"># Comprobante</td>
                                        <td>
                                            <asp:TextBox id="txtNumeroComprobante" CssClass="txtStandar" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblStandar"># serie</td>
                                        <td>
                                            <asp:TextBox id="txtNumeroSerie" CssClass="txtStandar" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblStandar"># Gu&iacute;a</td>
                                        <td>
                                            <asp:TextBox id="txtNumeroGuia" CssClass="txtStandar" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblStandar">Glosa</td>
                                        <td>
                                            <asp:TextBox id="txtGlosa" CssClass="txtStandar" TextMode="MultiLine" Rows="3" Width="80%" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblStandar">Direcci&oacute;n</td>
                                        <td>
                                            <asp:TextBox id="txtDireccion" CssClass="txtStandar" Width="80%" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblStandar">Activo</td>
                                        <td>
                                            <asp:CheckBox id="chkActivo" Checked="True" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblStandar">Modificado Por</td>
                                        <td>
                                            <asp:Label id="lblModificadoPor" CssClass="lblStandar" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblStandar">Fecha Modificaci&oacute;n</td>
                                        <td>
                                            <asp:Label id="lblFechaModificacion" CssClass="lblStandar" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblStandar">Documento</td>
                                        <td>
                                            <a id="lnkNumeroComprobante" style="cursor: pointer;">
                                                <asp:Label ID="lblNumeroComprobante" Text="Factura" runat="server"></asp:Label>
                                            </a>
                                            &nbsp;&nbsp;
                                            <a onclick="return ImprimirComprobanteNuevo();" style="cursor: pointer; visibility: hidden;">Nuevo</a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblStandar">Mensaje Sunat</td>
                                        <td>
                                            <asp:Label ID="lblMensajeSunat" CssClass="lblStandar" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </ajaxToolkit:TabPanel>
                    </ajaxToolkit:TabContainer>
                </td>
            </tr>
            <tr>
                <td colspan="3">&nbsp;</td>
            </tr>
            <tr>
                <td colspan="3" style="text-align: center;">
                    <input type="button" id="Imprimir" value="Imprimir" class="btnStandar" style="width: 20%;" onclick="return ImprimirSoloComprobante();"/>
                    <asp:Button ID="btnGuardarVenta" runat="server" CssClass="btnStandar" Text="Guardar" Width="30%" style="display: none;" OnClientClick="return ValidarVenta();" OnClick="btnGuardarVenta_OnClick" />
                    <input type="button" id="btnEnviarSunat" value="Enviar SUNAT" class="btnStandar" style="width: 30%; display: none;" onclick="return EnviarSunat();"/>
                    <asp:Button ID="btnCancelarVenta" runat="server" CssClass="btnStandar" Text="Cancelar" Width="30%" />
                    <asp:button id="hButton" runat="server" style="display:none;" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="mpeVenta" PopupControlID="pnlVenta" TargetControlID="hButton" CancelControlID="btnCancelarVenta" Drag="True" DropShadow="True" BackgroundCssClass="FondoAplicacion" ClientIDMode="Static" runat="server" PopupDragHandleControlID="lblTituloPopupVenta"></ajaxToolkit:ModalPopupExtender>
        <ajaxToolkit:RoundedCornersExtender ID="rceVenta" runat="server" BehaviorID="rcbVenta" TargetControlID="pnlVenta" Radius="6" Corners="All" />
    </div>
    </form>
<script type="text/javascript" src="../Scripts/tablesorter/jquery.tablesorter.js"></script>
<script type="text/javascript">
    function pageLoad(sender, args) {
        $(document).ready(function () {
            $("#grvVenta").tablesorter({ dateFormat: 'uk' });
            SetDefaultSortOrder();
        });
    }
    function Sort(cell, sortOrder) {
        var sorting = [[cell.cellIndex, sortOrder]];
        $("#grvVenta").trigger("sorton", [sorting]);
        if (sortOrder == 0) {
            sortOrder = 1;
            cell.className = "sortDesc";
        }
        else {
            sortOrder = 0;
            cell.className = "sortAsc";
        }
        cell.setAttribute("onclick", "Sort(this, " + sortOrder + ")");
        cell.onclick = function () { Sort(this, sortOrder); };
        document.getElementById("divData").scrollTop = 0;
    }
    function SetDefaultSortOrder() {
        var gvHeader = document.getElementById("dummyHeader");
        var headers = gvHeader.getElementsByTagName("th");
        for (var i = 1; i < headers.length; i++) {
            headers[i].setAttribute("onclick", "Sort(this, 1)");
            headers[i].onclick = function () { Sort(this, 1); };
            headers[i].className = "sortDesc";
        }
    }
    function RedimensionarGrid() {
        var centro = document.getElementById('centro').clientWidth;
        var izquierda = document.getElementById('izquierda').clientWidth;
        var derecha = document.getElementById('derecha').clientWidth;
        var divData = centro - 20;
        var divHeader = divData;
        document.getElementById('divHeader').style.width = divHeader + 'px';
        document.getElementById('divData').style.width = (divData + 17) + 'px';
        document.getElementById('divFooter').style.width = divHeader + 'px';
    }
    RedimensionarGrid();
    RedimensionarPopup();
    window.onresize = function (event) {
        RedimensionarGrid();
        RedimensionarPopup();
    };
    function RedimensionarPopup() {
        document.getElementById('pnlVenta').style.position = 'absolute';
        document.getElementById('pnlVenta').style.top = '50%';
        document.getElementById('pnlVenta').style.left = '50%';

        document.getElementById('pnlVenta').style.display = 'table';
        document.getElementById('pnlVenta').style.margin = '0 auto';
        document.getElementById('pnlVenta').style.width = '900px';
        document.getElementById('tabVenta').style.width = '880px';
    }
    function Paginacion(obj) {
        switch (obj.id) {
        case 'btnPrimero':
            document.getElementById('hdnCommandArgument').value = 'First';
            document.getElementById('btnPaginacion').click();
            // return false;
            break;
        case 'btnAnterior':
            document.getElementById('hdnCommandArgument').value = 'Prev';
            document.getElementById('btnPaginacion').click();
            // return false;
            break;
        case 'btnSiguiente':
            document.getElementById('hdnCommandArgument').value = 'Next';
            document.getElementById('btnPaginacion').click();
            // return false;
            break;
        case 'btnUltimo':
            document.getElementById('hdnCommandArgument').value = 'Last';
            document.getElementById('btnPaginacion').click();
            // return false;
            break;
        default:
            return false;
        }
    }
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_initializeRequest(InitializeRequest);
    prm.add_endRequest(EndRequest);
    var postBackElement;
    function InitializeRequest(sender, args) {
        if (prm.get_isInAsyncPostBack())
            args.set_cancel(true);
        postBackElement = args.get_postBackElement();
        if (postBackElement.id == 'btnGuardarVenta' || postBackElement.id == 'btnCargarVentas') {
            // Bloqueo Pantalla
            Bloquear();
        }
    }
    function EndRequest(sender, args) {
        if (postBackElement.id == 'btnGuardarVenta' || postBackElement.id == 'btnCargarVentas') {
            // Desbloquear Pantalla
            Desbloquear();
            //RedimensionarGrid();
        }
    }
    function grvVenta_OnDnlClick(row) {
        var indiceVentaId = 5;
        var ventaIdRow = RetornarCeldaValor(row, indiceVentaId);

        VerVenta(ventaIdRow);
    }
    function EnviarSunat() {
        var hdnVentaId = document.getElementById('<%=hdnVentaId.ClientID%>');
        // var ventaId = 2031;
        var ventaId = hdnVentaId.value;
        Bloquear(100002);
        PageMethods.EnviarSunat(ventaId, EnviarSunatOk, fnLlamadaError);
        return false;
    }
    function EnviarSunatOk(r) {
        if (r != null) {
            var lblMensajeSunat = document.getElementById('<%=lblMensajeSunat.ClientID%>');
            var mensaje = '';
            if (r.Exito) {
                mensaje = r.MensajeRespuesta;
            } else {
                mensaje = r.MensajeError;
            }
            lblMensajeSunat.innerText = mensaje;
            document.getElementById('btnCargarVentas').click();
        }
        Desbloquear();
        return false;
    }
    function ImprimirComprobanteNuevo() {
        var ventaId = 0;
        var hdnVentaId = document.getElementById('<%=hdnVentaId.ClientID%>');
        ventaId = hdnVentaId.value;
        Bloquear(100002);
        PageMethods.ImprimirComprobante(ventaId, ImprimirComprobanteOk, fnLlamadaError);
    }
    function ImprimirComprobanteOk(c) {
        if (c != '') {
            var arrayComprobante = c.split('@');
            if (arrayComprobante.length > 1) {
                var mensaje = arrayComprobante[0];
                var nombreComprobante = arrayComprobante[1];
                document.getElementById('btnCargarVentas').click();
                MostrarMensaje(mensaje);
                var lblNumeroDocumento = document.getElementById('<%=lblNumeroDocumento.ClientID%>');
                AbrirVentana('../Archivos/Documentos/Cliente/' + lblNumeroDocumento.innerText + '/' + nombreComprobante,
                    'Comprobante',
                    800,
                    600);
            }
        } else {
            MostrarMensaje(c);
        }
        Desbloquear();
    }
    function ImprimirSoloComprobante() {
        debugger;
        var hdnVentaPetroamericaId = document.getElementById('<%=hdnVentaPetroamericaId.ClientID%>');
        var hdnVentaId = document.getElementById('<%=hdnVentaId.ClientID%>');
        var ventaPetroamericaId = hdnVentaPetroamericaId.value;
        var ventaId = hdnVentaId.value;
        Bloquear(100002);
        PageMethods.ImprimirSoloComprobante(ventaPetroamericaId, ventaId, ImprimirSoloComprobanteOk, fnLlamadaError);
        return false;
    }
    function ImprimirSoloComprobanteOk(c) {
        var arrayComprobante = c.split('@');
        if (arrayComprobante.length > 1) {
            var mensaje = arrayComprobante[0];
            var nombreComprobante = arrayComprobante[1];
            document.getElementById('btnCargarVentas').click();
            MostrarMensaje(mensaje);
            var lblNumeroDocumento = document.getElementById('<%=lblNumeroDocumento.ClientID%>');

            var lnkNumeroComprobante = document.getElementById('lnkNumeroComprobante');
            lnkNumeroComprobante.onclick = function () {
                return ImprimirComprobante(lblNumeroDocumento.innerText, nombreComprobante);
            };

            window.open('../Archivos/Documentos/Cliente/' + lblNumeroDocumento.innerText + '/' + nombreComprobante, '_blank');
        } else {
            MostrarMensaje(c);
        }
        Desbloquear();
    }
    function ImprimirComprobante(numeroDocumento, comprobanteImpreso) {
        debugger;
        if (comprobanteImpreso == '') {
            alert('Primero debe ingresar al comprobante e Imprimir.');
            return false;
        }
        window.open('../Archivos/Documentos/Cliente/' + numeroDocumento + '/' + comprobanteImpreso, '_blank');
    }
</script>
</body>
</html>