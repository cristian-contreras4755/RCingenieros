<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Compra.aspx.cs" Inherits="APU.Presentacion.Operaciones.Compra" EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="uc1" TagName="ucProcesando" Src="~/Controles/ucProcesando.ascx" %>
<%@ Register Src="~/Controles/ucCabecera.ascx" TagPrefix="uc1" TagName="ucCabecera" %>
<%@ Register Src="~/Controles/ucMenu.ascx" TagPrefix="uc1" TagName="ucMenu" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Compras</title>
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
    function VerVenta(ventaId) {
        var tipoTiendaId = document.getElementById('<%=hdnTipoTiendaId.ClientID%>');
        var tipoTiendaIdValue = tipoTiendaId.value;
        window.location.href = 'DetalleCompra.aspx?CompraId=' + ventaId + '&TipoTiendaId=' + tipoTiendaIdValue;
        //AbrirVentana('DetalleCompra.aspx?CompraId=' + ventaId, 'RevisarSolicitudEnCurso', 1000, 800, true);
        return false;
    }
    function AbrirVentana(url, title, w, h, scroll) {
        var left = (screen.width / 2) - (w / 2);
        var top = (screen.height / 2) - (h / 2);
        return window.open(url, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=' + (scroll ? '1' : '0') + ', resizable=0, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left).focus();
    }
    function LimpiarVenta() {
        var hdnVentaId = document.getElementById('<%=hdnVentaId.ClientID%>');
        var chkActivo = document.getElementById('<%=chkActivo.ClientID%>');
        var txtDireccion = document.getElementById('<%=txtDireccion.ClientID%>');
        var lblModificadoPor = document.getElementById('<%=lblModificadoPor.ClientID%>');
        var lblFechaModificacion = document.getElementById('<%=lblFechaModificacion.ClientID%>');

        hdnVentaId.value = 0;
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
            var chkActivo = document.getElementById('<%=chkActivo.ClientID%>');
            var txtDireccion = document.getElementById('<%=txtDireccion.ClientID%>');
            var lblModificadoPor = document.getElementById('<%=lblModificadoPor.ClientID%>');
            var lblFechaModificacion = document.getElementById('<%=lblFechaModificacion.ClientID%>');

            hdnVentaId.value = v.VentaId;
            document.getElementById('lblTituloVenta').innerText = v.Nombre;
            chkActivo.checked = (v.Activo > 0);
            txtDireccion.value = v.Direccion;
            lblModificadoPor.innerText = v.UsuarioModificacion;
            lblFechaModificacion.innerText = FormatoFecha(v.FechaModificacion);
        } else {
            modalDialog.hide();
        }
    }
    function InsertarVenta() {
        VerVenta(0);

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
    function Cerrar() {
        var modalDialog = $find('mpeVenta');
        modalDialog.hide();
    }
</script>
    <form id="frmVenta" runat="server" DefaultButton="btnDefault">
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
                <td style="width: 100%;">
                    <table style="width: 100%; margin: 0;">
                        <tr>
                            <td style="text-align: center;" colspan="5">
                                <asp:Label CssClass="lblTitulo" Width="100%" runat="server">COMPRAS</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 10%;">
                                Ver:&nbsp;<asp:DropDownList id="ddlNumeroRegistros" CssClass="ddlStandar" AutoPostBack="True" Width="50%" runat="server" OnSelectedIndexChanged="ddlNumeroRegistros_SelectedIndexChanged" />
                            </td>
                            <td style="width: 20%;">
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
                        <table id="dummyHeader" style="width: 1500px;" class="tblSinBordes">
                            <thead>
                            <tr class="filaCabeceraGrid">
                                <th style="width: 5%; cursor: default; text-decoration: none;">Acciones</th>
                                <th style="width: 10%; cursor: pointer; text-decoration: underline;">Tipo Comprobante</th>
                                <th style="width: 10%; cursor: pointer; text-decoration: underline;"># Comprobante</th>
                                <th style="width: 15%; cursor: pointer; text-decoration: underline;">Proveedor</th>
                                <th style="width: 10%; cursor: pointer; text-decoration: underline;">Moneda</th>
                                <th style="width: 10%; cursor: pointer; text-decoration: underline;">Fecha Emisi&oacute;n</th>                                
                                <th style="width: 10%; cursor: pointer; text-decoration: underline;">Monto</th>
                                <th style="width: 10%; cursor: pointer; text-decoration: underline;">IGV</th>
                                <th style="width: 10%; cursor: pointer; text-decoration: underline;">Monto Total</th>
                                <th style="width: 10%; cursor: pointer; text-decoration: underline;">Estado</th>                                                             
                            </tr>
                            </thead>
                        </table>
                    </div>
                    <asp:UpdatePanel ID="divData" runat="server" UpdateMode="Conditional" style="overflow: scroll; width: 1200px; height: 420px; margin: 0 0 0px 0px;" onscroll="Onscrollfnction();">
                        <ContentTemplate>
                            <asp:GridView id="grvVenta" AutoGenerateColumns="False" Width="1500px" RowStyle-Wrap="True" runat="server" OnRowDataBound="grvVenta_RowDataBound">
                                <HeaderStyle CssClass="filaCabeceraGrid"></HeaderStyle>
                                <RowStyle CssClass="filaImparGrid"></RowStyle>
                                <AlternatingRowStyle CssClass="filaParGrid"></AlternatingRowStyle>
                                <Columns>
                                    <%-- 0: Acciones --%>
                                    <asp:TemplateField HeaderText="Acciones">
                                        <ItemStyle Width="5%" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ImageUrl="~/Imagenes/Iconos/editar.png" CssClass="imgButton" OnClientClick='<%# "return VerVenta(" + Eval("CompraId") + ");" %>' ToolTip="Editar" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--  1: TipoComprobante --%>
                                    <asp:TemplateField HeaderText="Nombre">
                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                             <%# Eval("TipoDocumento")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- 2: # Comprobante --%>
                                    <asp:TemplateField HeaderText="# Comprobante">
                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                           <%# Eval("NumeroSerie").ToString() + "-" + Eval("NumeroComprobante").ToString() %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- 3: Cliente --%>
                                    <asp:TemplateField HeaderText="Cliente">
                                        <ItemStyle Width="15%" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <%# Eval("Proveedor")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- 4: Moneda --%>
                                    <asp:TemplateField HeaderText="Moneda">
                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                          <%# Eval("Moneda")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- 5: Fecha Emisión --%>
                                    <asp:TemplateField HeaderText="Fecha Emisi&oacute;n">
                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <%# Convert.ToDateTime(Eval("FechaEmision")).ToString("dd/MM/yyyy") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                    
                                    <%-- 7: Monto --%>
                                    <asp:TemplateField HeaderText="Monto">
                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <%# Convert.ToDecimal(Eval("SubTotal")).ToString("N2") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- 8: MontoImpuesto --%>
                                    <asp:TemplateField HeaderText="MontoImpuesto">
                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <%# Convert.ToDecimal(Eval("Igv")).ToString("N2") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- 9: Monto Total --%>
                                    <asp:TemplateField HeaderText="Monto Total">
                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <%# Convert.ToDecimal(Eval("Total")).ToString("N2") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- 10: Estado --%>
                                    <asp:TemplateField HeaderText="Estado">
                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                           <%# Eval("EstadoComprobante")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                                                       
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlNumeroRegistros" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="btnPaginacion" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnGuardarVenta" EventName="Click" />
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
            <tr>
                <td id="lblTituloVenta" class="lblTituloPopup">Venta</td>
            </tr>
            <tr>
                <td>
                    <input type="hidden" id="hdnVentaId" value="0" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="width: 100%;">
                    <ajaxToolkit:TabContainer id="tabVenta" runat="server" OnClientActiveTabChanged="" Height="350px" Width="600px" ActiveTabIndex="0" OnDemand="False" AutoPostBack="false"
                                              TabStripPlacement="Top" CssClass="ajax__tab_xp" ScrollBars="None" UseVerticalStripPlacement="False" VerticalStripWidth="120px">
                        <ajaxToolkit:TabPanel id="tabVentaInformacion" runat="server" HeaderText="Informaci&oacute;n de Venta" Enabled="true" ScrollBars="Auto" OnDemandMode="Always">
                            <ContentTemplate>
                                <table style="width: 100%;">
                                    <tr>
                                        <td class="lblStandar" style="width: 30%;">Cliente</td>
                                        <td>
                                            <asp:TextBox id="txtCliente" CssClass="txtStandar" runat="server" />
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
                                </table>
                            </ContentTemplate>
                        </ajaxToolkit:TabPanel>
                    </ajaxToolkit:TabContainer>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: center;">
                    <asp:Button ID="btnGuardarVenta" runat="server" CssClass="btnStandar" Text="Guardar" Width="30%" OnClientClick="return ValidarVenta();" OnClick="btnGuardarVenta_OnClick" />
                    <asp:Button ID="btnCancelarVenta" runat="server" CssClass="btnStandar" Text="Cancelar" Width="30%" />
                    <asp:button id="hButton" runat="server" style="display:none;" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="mpeVenta" PopupControlID="pnlVenta" TargetControlID="hButton" CancelControlID="btnCancelarVenta" Drag="True" DropShadow="True" BackgroundCssClass="FondoAplicacion" ClientIDMode="Static" runat="server" PopupDragHandleControlID="lblTituloVenta"></ajaxToolkit:ModalPopupExtender>
        <ajaxToolkit:RoundedCornersExtender ID="rceVenta" runat="server" BehaviorID="rcbVenta" TargetControlID="pnlVenta" Radius="6" Corners="All" />
    </div>
        <input type="hidden" id="hdnTipoTiendaId" value="0" runat="server" />
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
        debugger;
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
        if (postBackElement.id == 'btnGuardarVenta') {
            // Bloqueo Pantalla
            Bloquear();
        }
    }
    function EndRequest(sender, args) {
        if (postBackElement.id == 'btnGuardarVenta') {
            // Desbloquear Pantalla
            Desbloquear();
            //RedimensionarGrid();
        }
    }
    function grvVenta_OnDnlClick(row) {
        debugger;
        var indiceVentaId = 5;
        var ventaIdRow = RetornarCeldaValor(row, indiceVentaId);

        VerVenta(ventaIdRow);
    }
</script>
</body>
</html>