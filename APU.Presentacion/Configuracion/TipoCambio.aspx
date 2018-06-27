<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TipoCambio.aspx.cs" Inherits="APU.Presentacion.Configuracion.TipoCambio" EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="uc1" TagName="ucProcesando" Src="~/Controles/ucProcesando.ascx" %>
<%@ Register Src="~/Controles/ucCabecera.ascx" TagPrefix="uc1" TagName="ucCabecera" %>
<%@ Register Src="~/Controles/ucMenu.ascx" TagPrefix="uc1" TagName="ucMenu" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Tipo Cambio</title>
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
        var grid = document.getElementById('grvTipoCambio');
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
    function VerTipoCambio(tipoCambioId) {
        PageMethods.ObtenerTipoCambio(tipoCambioId, ObtenerTipoCambioOk, fnLlamadaError);
        return false;
    }
    function LimpiarTipoCambio() {
        var hdnTipoCambioId = document.getElementById('<%=hdnTipoCambioId.ClientID%>');
        var ddlTipoCotizacion = document.getElementById('<%=ddlTipoCotizacion.ClientID%>');
        var txtFecha = document.getElementById('<%=txtFecha.ClientID%>');
        var txtCompra = document.getElementById('<%=txtCompra.ClientID%>');
        var txtVenta = document.getElementById('<%=txtVenta.ClientID%>');
        var lblModificadoPor = document.getElementById('<%=lblModificadoPor.ClientID%>');
        var lblFechaModificacion = document.getElementById('<%=lblFechaModificacion.ClientID%>');

        hdnTipoCambioId.value = 0;
        ddlTipoCotizacion.value = 0;
        document.getElementById('lblTituloTipoCambio').innerText = 'Tipo de Cambio';
        txtFecha.value = '';
        txtCompra.value = '';
        txtVenta.value = '';
        lblModificadoPor.innerText = '';
        lblFechaModificacion.innerText = '';
    }
    function ObtenerTipoCambioOk(tc) {
        var modalDialog = $find('mpeTipoCambio');
        if (tc != null) {
            modalDialog.show();

            var hdnTipoCambioId = document.getElementById('<%=hdnTipoCambioId.ClientID%>');
            var ddlTipoCotizacion = document.getElementById('<%=ddlTipoCotizacion.ClientID%>');
            var txtFecha = document.getElementById('<%=txtFecha.ClientID%>');
            var txtCompra = document.getElementById('<%=txtCompra.ClientID%>');
            var txtVenta = document.getElementById('<%=txtVenta.ClientID%>');
            var lblModificadoPor = document.getElementById('<%=lblModificadoPor.ClientID%>');
            var lblFechaModificacion = document.getElementById('<%=lblFechaModificacion.ClientID%>');

            hdnTipoCambioId.value = tc.TipoCambioId;
            ddlTipoCotizacion.value = tc.TipoCotizacionId;
            document.getElementById('lblTituloTipoCambio').innerText = tc.TipoCotizacion;
            txtFecha.value = FormatoFecha(tc.Fecha);
            txtCompra.value = FormatoDecimal(tc.Compra, 2, '');
            txtVenta.value = FormatoDecimal(tc.Venta, 2, '');
            lblModificadoPor.innerText = tc.UsuarioModificacion;
            lblFechaModificacion.innerText = FormatoFecha(tc.FechaModificacion);
        } else {
            modalDialog.hide();
        }
    }
    function ObtenerUsuarioPorEmpresa(emp) {
        <%--var ddlContacto = document.getElementById('<%=ddlContacto.ClientID%>');
        if (emp == 0) {
            LimpiarCombo(ddlContacto);
        } else {
            PageMethods.ObtenerUsuarioPorEmpresa(emp, ObtenerUsuarioPorEmpresaSucceed, fnLlamadaError, emp);
        }--%>
    }
    function ObtenerUsuarioPorEmpresaSucceed(result, emp) {
        <%--var ddlContacto = document.getElementById('<%=ddlContacto.ClientID%>');
        if (result.length > 0) {
            LlenarCombo(ddlContacto, result, 'UsuarioId', 'Usuario');
            AgregarOptionACombo(ddlContacto, Seleccione_value, Seleccione);
            ddlContacto.value = Seleccione_value;
            if (emp > 0) {
                ddlContacto.value = emp;
            }
        } else {
            alert('No existen usuarios para la empresa seleccionada.');
        }--%>
    }
    function InsertarTipoCambio() {
        var modalDialog = $find('mpeTipoCambio');

        modalDialog.show();
        LimpiarTipoCambio();

        return false;
    }
    function ValidarTipoCambio() {
        <%--var hdnAgenciaId = document.getElementById('<%=hdnAgenciaId.ClientID%>');
        var ddlEmpresa = document.getElementById('<%=ddlEmpresa.ClientID%>');
        var txtNombre = document.getElementById('<%=txtNombre.ClientID%>');
        var chkActivo = document.getElementById('<%=chkActivo.ClientID%>');

        if (ddlEmpresa.value.trim() == '0') {
            return MostrarValidacion(ddlEmpresa, 'Seleccione la Empresa.');
        }
        if (txtNombre.value.trim() == '') {
            return MostrarValidacion(txtNombre, 'Ingrese el Nombre de la Agencia.');
        }--%>
    }
    function GuardarTipoCambioOk(res) {
        var modalDialog = $find('mpeTipoCambio');
        var arr = res.split('@');
        var tipoCambioId = arr[0];
        var mensaje = arr[1];

        if (tipoCambioId > 0) {
            alert('Se registró la información de la Tipo de Cambio correctamente.');
            modalDialog.hide();
            LimpiarTipoCambio();
        } else {
            alert(mensaje);
        }
        Desbloquear();
    }
    function fnLlamadaError(excepcion) {
        alert('Ha ocurrido un error interno: ' + excepcion.get_message());
        Desbloquear();
    }
    function Cerrar() {
        var modalDialog = $find('mpeTipoCambio');
        modalDialog.hide();
    }
</script>
    <form id="frmTipoCambio" runat="server" DefaultButton="btnDefault">
    <uc1:ucProcesando runat="server" id="ucProcesando" />
    <asp:Button id="btnDefault" OnClientClick="return false;" style="display: none;" runat="server" />
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
        <table style="width: 100%;">
            <tr>
                <td style="text-align: center;" colspan="5">
                    <asp:Label CssClass="lblTitulo" Width="100%" runat="server">TIPO DE CAMBIO</asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 100%;">
                    <table style="width: 100%; margin: 0;">
                        <tr>
                            <td style="width: 10%;">
                                Ver:&nbsp;<asp:DropDownList id="ddlNumeroRegistros" CssClass="ddlStandar" AutoPostBack="True" Width="50%" runat="server" OnSelectedIndexChanged="ddlNumeroRegistros_SelectedIndexChanged" />
                            </td>
                            <td style="width: 20%;">
                                <asp:LinkButton ID="imgAnadir" ForeColor="#404040" runat="server" ToolTip="" OnClientClick="return InsertarTipoCambio();">
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
                        <table id="dummyHeader" style="width: 1500px;" class="tblSinBordes">
                            <thead>
                            <tr class="filaCabeceraGrid">
                                <th style="width: 5%; cursor: default; text-decoration: none;">Acciones</th>
                                <th style="width: 20%; cursor: pointer; text-decoration: underline;">Tipo Cotizacion</th>
                                <th style="width: 30%; cursor: pointer; text-decoration: underline;">Fecha</th>
                                <th style="width: 35%; cursor: pointer; text-decoration: underline;">Compra</th>
                                <th style="width: 10%; cursor: pointer; text-decoration: underline;">Venta</th>
                            </tr>
                            </thead>
                        </table>
                    </div>
                    <asp:UpdatePanel ID="divData" runat="server" UpdateMode="Conditional" style="overflow: scroll; width: 1200px; height: 420px; margin: 0 0 0px 0px;" onscroll="Onscrollfnction();">
                        <ContentTemplate>
                            <asp:GridView id="grvTipoCambio" AutoGenerateColumns="False" Width="1500px" RowStyle-Wrap="True" runat="server" OnRowDataBound="grvTipoCambio_RowDataBound">
                                <HeaderStyle CssClass="filaCabeceraGrid"></HeaderStyle>
                                <RowStyle CssClass="filaImparGrid"></RowStyle>
                                <AlternatingRowStyle CssClass="filaParGrid"></AlternatingRowStyle>
                                <Columns>
                                    <%-- 0: Acciones --%>
                                    <asp:TemplateField HeaderText="Acciones">
                                        <ItemStyle Width="5%" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ImageUrl="~/Imagenes/Iconos/editar.png" CssClass="imgButton" OnClientClick='<%# "return VerTipoCambio(" + Eval("TipoCambioId") + ");" %>' ToolTip="Editar" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--  1: Tipo Cotizacion --%>
                                    <asp:TemplateField HeaderText="Tipo Cotizacion">
                                        <ItemStyle Width="20%" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <%# Eval("TipoCotizacion").ToString().Trim() %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- 2: Fecha --%>
                                    <asp:TemplateField HeaderText="Fecha">
                                        <ItemStyle Width="30%" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <%# Eval("Fecha").ToString().Trim() %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- 3: Compra --%>
                                    <asp:TemplateField HeaderText="Compra">
                                        <ItemStyle Width="35%" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <%# Eval("Compra")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- 4: Venta --%>
                                    <asp:TemplateField HeaderText="Compra">
                                        <ItemStyle Width="35%" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <%# Eval("Venta")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlNumeroRegistros" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="btnPaginacion" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnGuardarTipoCambio" EventName="Click" />
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
    <asp:Panel ID="pnlTipoCambio" runat="server" CssClass="pnlModal" style="display: none;">
        <table style="width: 100%;">
            <tr id="lblTituloPopupTipoCambio" class="lblTituloPopup">
                <td style="width: 20%;" class="lblTituloPopup">&nbsp;</td>
                <td id="lblTituloTipoCambio" class="lblTituloPopup" style="width: 60%">Tipo de Cambio</td>
                <td class="lblTituloPopup" style="width: 20%; text-align: right;">
                    <a onclick="return Cerrar();" style="color: #FFFFFF; text-decoration: none; cursor: pointer;">Cerrar X</a>&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <input type="hidden" id="hdnTipoCambioId" value="0" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="3" style="width: 100%;">
                    <ajaxToolkit:TabContainer id="tabTipoCambio" runat="server" OnClientActiveTabChanged="" Height="350px" Width="600px" ActiveTabIndex="0" OnDemand="False" AutoPostBack="false"
                                              TabStripPlacement="Top" CssClass="ajax__tab_xp" ScrollBars="None" UseVerticalStripPlacement="False" VerticalStripWidth="120px">
                        <ajaxToolkit:TabPanel id="tabTipoCambioInformacion" runat="server" HeaderText="Informaci&oacute;n del Tipo de Cambio" Enabled="true" ScrollBars="Auto" OnDemandMode="Always">
                            <ContentTemplate>
                                <table style="width: 100%;">
                                    <tr>
                                        <td class="lblStandar">Tipo de Cotizacion</td>
                                        <td>
                                            <asp:DropDownList id="ddlTipoCotizacion" CssClass="ddlStandar" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblStandar">Fecha</td>
                                        <td>
                                            <asp:TextBox id="txtFecha" CssClass="txtStandarFecha" runat="server"></asp:TextBox>
                                            <asp:ImageButton ID="imgFecha" CssClass="imgCalendario" ImageUrl="~/Imagenes/Iconos/calendario.png" runat="server"/>
                                            <ajaxToolkit:CalendarExtender runat="server" ID="ceFecha" BehaviorID="bceFecha" CssClass="custom-calendar" TargetControlID="txtFecha" Format="dd/MM/yyyy" PopupButtonID="imgFecha" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblStandar">Compra</td>
                                        <td>
                                            <asp:TextBox id="txtCompra" CssClass="txtStandarMonto" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblStandar">Venta</td>
                                        <td>
                                            <asp:TextBox id="txtVenta" CssClass="txtStandarMonto" runat="server" />
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
                    <asp:Button ID="btnGuardarTipoCambio" runat="server" CssClass="btnStandar" Text="Guardar" Width="30%" OnClientClick="return ValidarTipoCambio();" OnClick="btnGuardarTipoCambio_OnClick" />
                    <asp:Button ID="btnCancelarTipoCambio" runat="server" CssClass="btnStandar" Text="Cancelar" Width="30%" />
                    <asp:button id="hButton" runat="server" style="display:none;" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="mpeTipoCambio" PopupControlID="pnlTipoCambio" TargetControlID="hButton" CancelControlID="btnCancelarTipoCambio" Drag="True" DropShadow="True" BackgroundCssClass="FondoAplicacion" ClientIDMode="Static" runat="server" PopupDragHandleControlID="lblTituloTipoCambio"></ajaxToolkit:ModalPopupExtender>
        <ajaxToolkit:RoundedCornersExtender ID="rceTipoCambio" runat="server" BehaviorID="rcbTipoCambio" TargetControlID="pnlTipoCambio" Radius="6" Corners="All" />
    </div>
    </form>
<script type="text/javascript" src="../Scripts/tablesorter/jquery.tablesorter.js"></script>
<script type="text/javascript">
    function pageLoad(sender, args) {
        $(document).ready(function () {
            $("#grvTipoCambio").tablesorter({ dateFormat: 'uk' });
            SetDefaultSortOrder();
        });
    }
    function Sort(cell, sortOrder) {
        var sorting = [[cell.cellIndex, sortOrder]];
        $("#grvTipoCambio").trigger("sorton", [sorting]);
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

        var dummyHeader = document.getElementById('dummyHeader').clientWidth;
        var grvTipoCambio = document.getElementById('grvTipoCambio').clientWidth;

        var divData = centro - 20;
        var divHeader = divData;
        document.getElementById('divHeader').style.width = divHeader + 'px';

        if (dummyHeader < divHeader) {
            document.getElementById('dummyHeader').style.width = divHeader + 'px';
            document.getElementById('grvTipoCambio').style.width = divData + 'px';
        }

        document.getElementById('divData').style.width = (divData + 17) + 'px';
        document.getElementById('divFooter').style.width = divHeader + 'px';
    }
    RedimensionarGrid();
    window.onresize = function (event) {
        RedimensionarGrid();
    };
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
        if (postBackElement.id == 'btnGuardarTipoCambio') {
            // Bloqueo Pantalla
            Bloquear();
        }
    }
    function EndRequest(sender, args) {
        if (postBackElement.id == 'btnGuardarTipoCambio') {
            // Desbloquear Pantalla
            Desbloquear();
            //RedimensionarGrid();
        }
    }
    function grvTipoCambio_OnDnlClick(row) {
        debugger;
        var indiceTipoCambioId = 5;
        var tipoCambioIdRow = RetornarCeldaValor(row, indiceTipoCambioId);

        VerTipoCambio(tipoCambioIdRow);
    }
</script>
</body>
</html>