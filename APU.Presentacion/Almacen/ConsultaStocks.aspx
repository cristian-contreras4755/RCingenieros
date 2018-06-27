<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConsultaStocks.aspx.cs" Inherits="APU.Presentacion.Almacen.ConsultaStocks" EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="uc1" TagName="ucProcesando" Src="~/Controles/ucProcesando.ascx" %>
<%@ Register Src="~/Controles/ucCabecera.ascx" TagPrefix="uc1" TagName="ucCabecera" %>
<%@ Register Src="~/Controles/ucMenu.ascx" TagPrefix="uc1" TagName="ucMenu" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Stocks Productos</title>
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
        var ele, eleCodigoProducto, eleProducto, eleAlmacen;
        var cellCodigoProducto, cellProducto, cellAlmacen;
        cellCodigoProducto = 1, cellProducto = 2, cellAlmacen = 3;
        if (grid != null) {
            if (grid.rows.length > 0) {
                for (var r = 1; r < grid.rows.length; r++) {
                    eleCodigoProducto = grid.rows[r].cells[cellCodigoProducto].innerText.replace(/<[^>]+>/g, "");
                    eleProducto = grid.rows[r].cells[cellProducto].innerText.replace(/<[^>]+>/g, "");
                    eleAlmacen = grid.rows[r].cells[cellAlmacen].innerText.replace(/<[^>]+>/g, "");                    
                    ele = eleCodigoProducto + eleProducto + eleAlmacen;
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
    function VerInventario(inventarioId) {
        PageMethods.ObtenerInventario(inventarioId, ObtenerInventarioOk, fnLlamadaError);
        return false;
    }
    function VerTraslado(inventarioId) {
        PageMethods.ObtenerInventario(inventarioId, ObtenerTrasladoOk, fnLlamadaError);
        return false;
    }
   
    function LimpiarVenta()
    {
        var hdnVentaId = document.getElementById('<%=hdnVentaId.ClientID%>');
        var hdnStockActual = document.getElementById('<%=hdnStockActual.ClientID%>');
        
        hdnVentaId.value = 0;
        hdnStockActual.value = 0;
        
        var tabVenta = $get('<%=tabVenta.ClientID%>');
        tabVenta.control.set_activeTabIndex(0);
    }
    function ObtenerInventarioOk(v)
    {
        var modalDialog = $find('mpeVenta');
        if (v != null) {
            modalDialog.show();

            var tabVenta = $get('<%=tabVenta.ClientID%>');
            tabVenta.control.set_activeTabIndex(0);

            var hdnVentaId = document.getElementById('<%=hdnVentaId.ClientID%>');
            
            var txtProducto = document.getElementById('<%=txtProducto.ClientID%>');
            var txtTipoProducto = document.getElementById('<%=txtTipoProducto.ClientID%>');
            var txtStock = document.getElementById('<%=txtStock.ClientID%>');
            var txtStockMinimo = document.getElementById('<%=txtStockMinimo.ClientID%>');

            hdnVentaId.value = v.InventarioId;
            
            txtProducto.value = v.Producto;
            txtTipoProducto.value = v.TipoProducto;            
            txtStock.value = v.InventarioActual;
            txtStockMinimo.value = v.InventarioMinimo;
            
        } else {
            modalDialog.hide();
        }
    }
    function ObtenerTrasladoOk(v) {
        var modalDialog = $find('mpeTraslado');
        if (v != null) {
            modalDialog.show();

            var tabVenta = $get('<%=tabTraslado.ClientID%>');
            tabVenta.control.set_activeTabIndex(0);

            var hdnVentaId = document.getElementById('<%=hdnVentaId.ClientID%>');
            var hdnProductoId = document.getElementById('<%=hdnProductoId.ClientID%>');
            var txtProductoTraslado = document.getElementById('<%=txtProductoTraslado.ClientID%>');
            var ddlAlmacenOrigen = document.getElementById('<%=ddlAlmacenOrigen.ClientID%>');          
            var hdnStockActual = document.getElementById('<%=hdnStockActual.ClientID%>');

            hdnVentaId.value = v.InventarioId;
            hdnStockActual.value = v.InventarioActual;
            hdnProductoId.value = v.ProductoId;
            txtProductoTraslado.value = v.Producto;
            //txtTipoProducto.value = v.TipoProducto;
            //document.getElementById('lblTituloVenta').innerText = v.Nombre;
            ddlAlmacenOrigen.value = v.AlmacenId;
            //txtStockMinimo.value = v.InventarioMinimo;
            
        } else {
            modalDialog.hide();
        }
    }
    function InsertarVenta() {
        VerVenta(0);

        return false;
    }
    function ValidarInventario() {
        var hdnVentaId = document.getElementById('<%=hdnVentaId.ClientID%>');        
        var txtStock = document.getElementById('<%=txtStock.ClientID%>');
        var txtStockMinimo = document.getElementById('<%=txtStockMinimo.ClientID%>');

        if (txtStock.value.trim() == '0' || txtStock.value.trim() == '') {
            return MostrarValidacion(txtStock, 'Ingrese cantidad valida.');
        }
        if (txtStockMinimo.value.trim() == '0' || txtStockMinimo.value.trim() == '') {
            return MostrarValidacion(txtStockMinimo, 'Ingrese cantidad valida.');
        }
        //if (txtNombre.value.trim() == '') {
        //    return MostrarValidacion(txtNombre, 'Ingrese el Nombre de la Venta.');
        //}
    }
    function ValidarTraslado() {
        var hdnVentaId = document.getElementById('<%=hdnVentaId.ClientID%>');        
        var txtCantidad = document.getElementById('<%=txtCantidad.ClientID%>');
        var txtFechaTraslado = document.getElementById('<%=txtFechaTraslado.ClientID%>');
        var ddlAlmacenDestino = document.getElementById('<%=ddlAlmacenDestino.ClientID%>');
        var ddlAlmacenOrigen = document.getElementById('<%=ddlAlmacenOrigen.ClientID%>');
        var ddlResponsable = document.getElementById('<%=ddlResponsable.ClientID%>');

        if (ddlAlmacenDestino.value == '0' ) {
            return MostrarValidacion(ddlAlmacenDestino, 'Seleccione Almacen Destino.');
        } else if (ddlAlmacenOrigen.value == ddlAlmacenDestino.value) {
            return MostrarValidacion(ddlAlmacenDestino, 'Almacen Destino debe ser diferente al Almacen Origen.');
        }

        if (ddlResponsable.value == '0') {
            return MostrarValidacion(ddlResponsable, 'Seleccione Responsable.');
        }

        if (txtCantidad.value.trim() == '0' || txtCantidad.value.trim() == '') {
            return MostrarValidacion(txtCantidad, 'Ingrese cantidad valida.');
        }
        var stockActual = document.getElementById('<%=hdnStockActual.ClientID%>'); 
        if (parseInt(txtCantidad.value.trim()) > parseInt(stockActual.value)) {
            return MostrarValidacion(txtCantidad, 'La cantidad ingresada supera el stock actual.');
        }
        if (txtFechaTraslado.value.trim() == '') {
            return MostrarValidacion(txtFechaTraslado, 'Ingrese fecha traslado.');
        }
       
    }
    function GuardarVentaOk(res) {
        var modalDialog = $find('mpeVenta');
        var arr = res.split('@');
        var ventaId = arr[0];
        var mensaje = arr[1];

        if (ventaId > 0) {
            alert('Se registró la información correctamente.');
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
                                <asp:Label CssClass="lblTitulo" Width="100%" runat="server">STOCKS DE PRODUCTOS</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 10%;">
                                Ver:&nbsp;<asp:DropDownList id="ddlNumeroRegistros" CssClass="ddlStandar" AutoPostBack="True" runat="server" Width="50%" OnSelectedIndexChanged="ddlNumeroRegistros_SelectedIndexChanged" />
                            </td>
                            <td class="lblStandar" style="width: 20%;text-align:right;">Almacen
                                
                            </td>
                            <td style="width: 20%;">
                                <asp:DropDownList id="ddlAlmacen" CssClass="ddlStandar" AutoPostBack="True" runat="server" OnSelectedIndexChanged="ddlAlmacen_SelectedIndexChanged" />
                            </td>
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
                                <th style="width: 10%; cursor: default; text-decoration: none;">Acciones</th>
                                <th style="width: 10%; cursor: pointer; text-decoration: underline;">Código Producto</th>
                                <th style="width: 20%; cursor: pointer; text-decoration: underline;">Producto</th>
                                <th style="width: 20%; cursor: pointer; text-decoration: underline;">Almacen</th> 
                                <th style="width: 10%; cursor: pointer; text-decoration: underline;">Stock Actual</th>
                                <th style="width: 10%; cursor: pointer; text-decoration: underline;">Stock Minimo</th>                               
                                <th style="width: 20%; cursor: pointer; text-decoration: underline;"></th>                                
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
                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ImageUrl="~/Imagenes/Iconos/editar.png" CssClass="imgButton" OnClientClick='<%# "return VerInventario(" + Eval("InventarioId") + ");" %>' ToolTip="Editar" />
                                            <asp:ImageButton runat="server" ImageUrl="~/Imagenes/Iconos/compras.png" CssClass="imgButton" OnClientClick='<%# "return VerTraslado(" + Eval("InventarioId") + ");" %>' ToolTip="Trasladar" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="">
                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                           <%# Eval("Codigo")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- 3: Cliente --%>
                                    <asp:TemplateField HeaderText="Producto">
                                        <ItemStyle Width="20%" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <%# Eval("Producto")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--  1: TipoComprobante --%>
                                    <asp:TemplateField HeaderText="Almacen">
                                        <ItemStyle Width="20%" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                             <%# Eval("Almacen")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- 2: # Comprobante --%>                                    
                                    <%-- 4: Moneda --%>
                                    <asp:TemplateField HeaderText="Moneda">
                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                          <%# Convert.ToDecimal(Eval("InventarioActual")).ToString("###,##0.#0") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- 5: Fecha Emisión --%>
                                    <asp:TemplateField HeaderText="Fecha Emisi&oacute;n">
                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <%# Convert.ToDecimal(Eval("InventarioMinimo")).ToString("###,##0.#0") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                   
                                    <%-- 7: Monto --%>
                                    <asp:TemplateField HeaderText="Monto">
                                        <ItemStyle Width="20%" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                          
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
            <tr id="lblTituloPopupVenta" class="lblTituloPopup">
                <td style="width: 20%;" class="lblTituloPopup">&nbsp;</td>
                <td id="lblTituloVenta" class="lblTituloPopup" style="width: 60%">Actualización Stock</td>
                <td class="lblTituloPopup" style="width: 20%; text-align: right;">
                    <a onclick="return Cerrar();" style="color: #FFFFFF; text-decoration: none; cursor: pointer;">Cerrar X</a>&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <input type="hidden" id="hdnVentaId" value="0" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="3" style="width: 100%;">
                    <ajaxToolkit:TabContainer id="tabVenta" runat="server" OnClientActiveTabChanged="" Height="350px" Width="600px" ActiveTabIndex="0" OnDemand="False" AutoPostBack="false"
                                              TabStripPlacement="Top" CssClass="ajax__tab_xp" ScrollBars="None" UseVerticalStripPlacement="False" VerticalStripWidth="120px">
                        <ajaxToolkit:TabPanel id="tabVentaInformacion" runat="server" HeaderText="Stock Producto" Enabled="true" ScrollBars="Auto" OnDemandMode="Always">
                            <ContentTemplate>
                                <table style="width: 100%;">
                                    <tr>
                                        <td class="lblStandar" style="width: 30%;">Producto</td>
                                        <td>
                                            <asp:TextBox id="txtProducto" CssClass="txtStandar" runat="server" ReadOnly="true"  Width="90%" />
                                        </td>
                                    </tr>                                    
                                    <tr>
                                        <td class="lblStandar">Tipo Producto</td>
                                        <td>
                                            <asp:TextBox id="txtTipoProducto" CssClass="txtStandar" runat="server" ReadOnly="true"  Width="90%" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblStandar">Stock</td>
                                        <td>
                                            <asp:TextBox id="txtStock" CssClass="txtStandarEntero" runat="server" Width="40%" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblStandar">Stock Minimo</td>
                                        <td>
                                            <asp:TextBox id="txtStockMinimo" CssClass="txtStandarEntero" runat="server" Width="40%" />
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
                    <asp:Button ID="btnGuardarVenta" runat="server" CssClass="btnStandar" Text="Guardar" Width="30%" OnClientClick="return ValidarInventario();" OnClick="btnGuardarVenta_OnClick" />
                    <asp:Button ID="btnCancelarVenta" runat="server" CssClass="btnStandar" Text="Cancelar" Width="30%" />
                    <asp:button id="hButton" runat="server" style="display:none;" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="mpeVenta" PopupControlID="pnlVenta" TargetControlID="hButton" CancelControlID="btnCancelarVenta" Drag="True" DropShadow="True" BackgroundCssClass="FondoAplicacion" ClientIDMode="Static" runat="server" PopupDragHandleControlID="lblTituloVenta"></ajaxToolkit:ModalPopupExtender>
    <ajaxToolkit:RoundedCornersExtender ID="rceVenta" runat="server" BehaviorID="rcbVenta" TargetControlID="pnlVenta" Radius="6" Corners="All" />
    <asp:Panel ID="pnlTraslado" runat="server" CssClass="pnlModal" style="display: none;">
        <table style="width: 100%;">
            <tr>
                <td id="lblTituloTraslado" class="lblTituloPopup">Traslado Productos</td>
            </tr>
            <tr>
                <td>
                    <input type="hidden" id="hdnProductoId" value="0" runat="server" />
                    <input type="hidden" id="hdnStockActual" value="0" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="width: 100%;">
                    <ajaxToolkit:TabContainer id="tabTraslado" runat="server" OnClientActiveTabChanged="" Height="250px" Width="600px" ActiveTabIndex="0" OnDemand="False" AutoPostBack="false"
                                              TabStripPlacement="Top" CssClass="ajax__tab_xp" ScrollBars="None" UseVerticalStripPlacement="False" VerticalStripWidth="120px">
                        <ajaxToolkit:TabPanel id="tabTrasladoDetalle" runat="server" HeaderText="Stock Producto" Enabled="true" ScrollBars="Auto" OnDemandMode="Always">
                            <ContentTemplate>
                                <table style="width: 100%;">
                                    <tr>
                                        <td class="lblStandar">Almacen Origen</td>
                                        <td>
                                            <asp:DropDownList id="ddlAlmacenOrigen" CssClass="ddlStandar" runat="server" Enabled="false" Width="40%" />
                                        </td>
                                    </tr> 
                                    <tr>
                                        <td class="lblStandar">Almacen Destino</td>
                                        <td>
                                            <asp:DropDownList id="ddlAlmacenDestino" CssClass="ddlStandar" runat="server" Width="40%" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblStandar">Responsable</td>
                                        <td>
                                            <asp:DropDownList id="ddlResponsable" CssClass="ddlStandar" runat="server" Width="90%" />
                                        </td>
                                    </tr>  
                                    <tr>
                                        <td class="lblStandar" style="width: 30%;">Producto</td>
                                        <td>
                                            <asp:TextBox id="txtProductoTraslado" CssClass="txtStandar" runat="server" ReadOnly="true"  Width="90%" />
                                        </td>
                                    </tr>    
                                    <tr>
                                        <td class="lblStandar">Cantidad</td>
                                        <td>
                                            <asp:TextBox id="txtCantidad" CssClass="txtStandarEntero" runat="server" Width="40%" />
                                        </td>
                                    </tr>  
                                    <tr>
                                        <td class="lblStandar">Fecha Traslado</td>
                                        <td>
                                             <asp:TextBox id="txtFechaTraslado" CssClass="txtStandarFecha" Width="40%" runat="server"></asp:TextBox>
                                            <asp:ImageButton ID="imgFechaTraslado" CssClass="imgCalendario" ImageUrl="~/Imagenes/Iconos/calendario.png" runat="server"/>
                                            <ajaxToolkit:CalendarExtender runat="server" ID="ceFechaNacimiento" BehaviorID="bceFechaNacimiento" CssClass="custom-calendar" TargetControlID="txtFechaTraslado" Format="dd/MM/yyyy" PopupButtonID="imgFechaTraslado" />
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
                    <asp:Button ID="btnGuardarTraslado" runat="server" CssClass="btnStandar" Text="Guardar" Width="30%" OnClientClick="return ValidarTraslado();" OnClick="btnGuardarTraslado_OnClick" />
                    <asp:Button ID="btnCancelarTraslado" runat="server" CssClass="btnStandar" Text="Cancelar" Width="30%" />
                    <asp:button id="Button1" runat="server" style="display:none;" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="mpeTraslado" PopupControlID="pnlTraslado" TargetControlID="Button1" CancelControlID="btnCancelarTraslado" Drag="True" DropShadow="True" BackgroundCssClass="FondoAplicacion" ClientIDMode="Static" runat="server" PopupDragHandleControlID="lblTituloTraslado"></ajaxToolkit:ModalPopupExtender>
    <ajaxToolkit:RoundedCornersExtender ID="rceTraslado" runat="server" BehaviorID="rcbTraslado" TargetControlID="pnlTraslado" Radius="6" Corners="All" />
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