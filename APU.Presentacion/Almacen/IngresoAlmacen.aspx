<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IngresoAlmacen.aspx.cs" Inherits="APU.Presentacion.Almacen.IngresoAlmacen" EnableEventValidation="false" %>
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
    function VerVenta(ventaId) {
        window.location.href = 'DetalleCompra.aspx?CompraId=' + ventaId;
        //AbrirVentana('DetalleCompra.aspx?CompraId=' + ventaId, 'RevisarSolicitudEnCurso', 1000, 800, true);
        return false;
    }
    function AbrirVentana(url, title, w, h, scroll) {
        var left = (screen.width / 2) - (w / 2);
        var top = (screen.height / 2) - (h / 2);
        return window.open(url, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=' + (scroll ? '1' : '0') + ', resizable=0, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left).focus();
    }
    function LimpiarIngresoAlmacen() {
        var ddlAlmacen = document.getElementById('<%=ddlAlmacen.ClientID%>');
        var txtCantidad = document.getElementById('<%=txtCantidad.ClientID%>');       
        var hdnProductoId = document.getElementById('<%=hdnProductoId.ClientID%>');   
        var hdnCompraDetalleId = document.getElementById('<%=hdnCompraDetalleId.ClientID%>');  
        
        ddlAlmacen.value = '0';
        txtCantidad.value = '';
        hdnProductoId.value = '0';
        hdnCompraDetalleId.value = '0';

        
    }
    function IngresarAlmacen(codigoProducto, cantidad, compraDetalleId) {
        
        var modalDialog = $find('mpeVenta');
        modalDialog.show();
        var txtCantidad = document.getElementById('<%=txtCantidad.ClientID%>');  
        var hdnProductoId = document.getElementById('<%=hdnProductoId.ClientID%>');   
        var hdnCompraDetalleId = document.getElementById('<%=hdnCompraDetalleId.ClientID%>'); 

        txtCantidad.value = cantidad;
        hdnProductoId.value = codigoProducto;
        hdnCompraDetalleId.value = compraDetalleId;
    }
    function InsertarVenta() {
        VerVenta(0);

        return false;
    }
    function ValidarIngresoAlmacen() {
        var ddlAlmacen = document.getElementById('<%=ddlAlmacen.ClientID%>');
        var txtCantidad = document.getElementById('<%=txtCantidad.ClientID%>');   

        if (ddlAlmacen.value.trim() == '0') {
            return MostrarValidacion(ddlAlmacen, 'Seleccione Almacen.');
        }
        if (txtCantidad.value.trim() == '' || txtCantidad.value.trim() == '0') {
            return MostrarValidacion(txtCantidad, 'Ingrese una cantidad valida.');
        }
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
                    <table style="width: 100%; margin: 0;" border="1">
                        <tr>
                            <td style="text-align: center;" colspan="8">
                                <asp:Label CssClass="lblTitulo" Width="100%" runat="server">INGRESO PRODUCTOS AL ALMACEN</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 10%;">
                                Ver:&nbsp;<asp:DropDownList id="ddlNumeroRegistros" CssClass="ddlStandar" AutoPostBack="True" Width="50%" runat="server" OnSelectedIndexChanged="ddlNumeroRegistros_SelectedIndexChanged" />
                            </td>
                            <td style="width: 10%;" class="lblStandar">Fecha Inicio</td>
                            <td style="width: 15%;">
                                <asp:TextBox id="txtFechaInicio" CssClass="txtStandarFecha" runat="server"></asp:TextBox>
                                <asp:ImageButton ID="imgFechaInicio" CssClass="imgCalendario" ImageUrl="~/Imagenes/Iconos/calendario.png" runat="server"/>
                                <ajaxToolkit:CalendarExtender runat="server" ID="ceFechaInicio" BehaviorID="bceFechaInicio" CssClass="custom-calendar" TargetControlID="txtFechaInicio" Format="dd/MM/yyyy" PopupButtonID="imgFechaInicio" />
                            </td>
                            <td style="width: 10%;" class="lblStandar">Fecha Fin</td>
                            <td style="width: 15%;">
                                <asp:TextBox id="txtFechaFin" CssClass="txtStandarFecha" runat="server"></asp:TextBox>
                                <asp:ImageButton ID="imgFechaFin" CssClass="imgCalendario" ImageUrl="~/Imagenes/Iconos/calendario.png" runat="server"/>
                                <ajaxToolkit:CalendarExtender runat="server" ID="ceFechaFin" BehaviorID="bceFechaFin" CssClass="custom-calendar" TargetControlID="txtFechaFin" Format="dd/MM/yyyy" PopupButtonID="imgFechaFin" />
                            </td>
                            <td style="width: 15%;"><asp:Button id="btnBuscar" CssClass="btnStandar" Text="Buscar" Width="80%" runat="server" /></td>
                            <td style="width: 25%;">Buscar:
                                <asp:TextBox id="txtBuscar" CssClass="txtStandar" Width="80%" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="width: 100%;">
                    <div id="divHeader" style="width: 1182px; overflow-x: hidden; overflow-y: hidden; margin: 0 0px 0 0px;" >
                        <table id="dummyHeader" style="width: 1250px;" class="tblSinBordes">
                            <thead>
                            <tr class="filaCabeceraGrid">
                                <th style="width: 5%; cursor: default; text-decoration: none;">Acciones</th>
                                <th style="width: 10%; cursor: pointer; text-decoration: underline;">Tipo Comprobante</th>
                                <th style="width: 10%; cursor: pointer; text-decoration: underline;"># Comprobante</th>
                                <th style="width: 15%; cursor: pointer; text-decoration: underline;">Codigo Producto</th>
                                <th style="width: 25%; cursor: pointer; text-decoration: underline;">Producto</th>
                                <th style="width: 10%; cursor: pointer; text-decoration: underline;">Unidad Medida</th>
                                <th style="width: 10%; cursor: pointer; text-decoration: underline;">Cantidad</th>
                                <th style="width: 10%; cursor: pointer; text-decoration: underline;">Precio</th>                                                              
                            </tr>
                            </thead>
                        </table>
                    </div>
                    <asp:UpdatePanel ID="divData" runat="server" UpdateMode="Conditional" style="overflow: scroll; width: 1200px; height: 420px; margin: 0 0 0px 0px;" onscroll="Onscrollfnction();">
                        <ContentTemplate>
                            <asp:GridView id="grvVenta" AutoGenerateColumns="False" Width="1250px" RowStyle-Wrap="True" runat="server" OnRowDataBound="grvVenta_RowDataBound">
                                <HeaderStyle CssClass="filaCabeceraGrid"></HeaderStyle>
                                <RowStyle CssClass="filaImparGrid"></RowStyle>
                                <AlternatingRowStyle CssClass="filaParGrid"></AlternatingRowStyle>
                                <Columns>
                                    <%-- 0: Acciones --%>
                                    <asp:TemplateField HeaderText="Acciones">
                                        <ItemStyle Width="5%" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ImageUrl="~/Imagenes/Iconos/editar.png" CssClass="imgButton" OnClientClick='<%# "return IngresarAlmacen("+Eval("ProductoId")+","+Eval("Cantidad")+","+Eval("ComprasDetalleId") +");" %>' ToolTip="Editar" />
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
                                           <%# Eval("NumeroSerie") + " - " + Eval("NumeroComprobante")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- 3: Cliente --%>
                                    <asp:TemplateField HeaderText="Cliente">
                                        <ItemStyle Width="15%" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <%# Eval("Codigo")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- 4: Moneda --%>
                                    <asp:TemplateField HeaderText="Moneda">
                                        <ItemStyle Width="25%" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                          <%# Eval("Producto")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- 5: Fecha Emisión --%>
                                    <asp:TemplateField HeaderText="Fecha Emisi&oacute;n">
                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                             <%# Eval("UnidadMedida")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- 6: Fecha Vencimiento --%>
                                    <asp:TemplateField HeaderText="Fecha Vencimiento">
                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <%# Eval("Cantidad")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- 7: Monto --%>
                                    <asp:TemplateField HeaderText="Monto">
                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                          <%# Convert.ToDecimal(Eval("PrecioUnitario")).ToString("N2") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                    
                                                                      
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlNumeroRegistros" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="btnPaginacion" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click" />
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
                <td id="lblTituloVenta" class="lblTituloPopup" style="width: 60%">Ingreso Almacen</td>
                <td class="lblTituloPopup" style="width: 20%; text-align: right;">
                    <a onclick="return Cerrar();" style="color: #FFFFFF; text-decoration: none; cursor: pointer;">Cerrar X</a>&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <input type="hidden" id="hdnProductoId" value="0" runat="server" />
                    <input type="hidden" id="hdnCompraDetalleId" value="0" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="3" style="width: 100%;">
                    <ajaxToolkit:TabContainer id="tabVenta" runat="server" OnClientActiveTabChanged="" Height="300px" Width="600px" ActiveTabIndex="0" OnDemand="False" AutoPostBack="false"
                                              TabStripPlacement="Top" CssClass="ajax__tab_xp" ScrollBars="None" UseVerticalStripPlacement="False" VerticalStripWidth="120px">
                        <ajaxToolkit:TabPanel id="tabVentaInformacion" runat="server" HeaderText="Ingreso Almacen" Enabled="true" ScrollBars="Auto" OnDemandMode="Always">
                            <ContentTemplate>
                                <table style="width: 100%;">
                                    <tr>
                                        <td class="lblStandar">Almacen</td>
                                        <td>
                                            <asp:DropDownList id="ddlAlmacen" CssClass="ddlStandar" runat="server" Width="40%" />
                                        </td>
                                    </tr>                                       
                                    <tr>
                                        <td class="lblStandar">Cantidad</td>
                                        <td>
                                            <asp:TextBox id="txtCantidad" CssClass="txtStandarEntero" runat="server" Width="40%" />
                                        </td>
                                    </tr>  
                                    <tr>
                                        <td class="lblStandar">Fecha</td>
                                        <td>
                                             <asp:TextBox id="txtFecha" CssClass="txtStandarFecha" Width="40%" runat="server"></asp:TextBox>
                                            <asp:ImageButton ID="imgFecha" CssClass="imgCalendario" ImageUrl="~/Imagenes/Iconos/calendario.png" runat="server"/>
                                            <ajaxToolkit:CalendarExtender runat="server" ID="ceFecha" BehaviorID="bceFecha" CssClass="custom-calendar" TargetControlID="txtFecha" Format="dd/MM/yyyy" PopupButtonID="imgFecha" />
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
                    <asp:Button ID="btnGuardar" runat="server" CssClass="btnStandar" Text="Guardar" Width="30%" OnClientClick="return ValidarIngresoAlmacen();" OnClick="btnGuardarIngresoAlmacen_OnClick" />
                    <asp:Button ID="btnCancelar" runat="server" CssClass="btnStandar" Text="Cancelar" Width="30%" />
                    <asp:button id="hButton" runat="server" style="display:none;" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="mpeVenta" PopupControlID="pnlVenta" TargetControlID="hButton" CancelControlID="btnCancelar" Drag="True" DropShadow="True" BackgroundCssClass="FondoAplicacion" ClientIDMode="Static" runat="server" PopupDragHandleControlID="lblTituloVenta"></ajaxToolkit:ModalPopupExtender>
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