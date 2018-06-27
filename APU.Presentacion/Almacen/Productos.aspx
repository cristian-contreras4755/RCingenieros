<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="APU.Presentacion.Almacen.Productos" EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="uc1" TagName="ucProcesando" Src="~/Controles/ucProcesando.ascx" %>
<%@ Register Src="~/Controles/ucCabecera.ascx" TagPrefix="uc1" TagName="ucCabecera" %>
<%@ Register Src="~/Controles/ucMenu.ascx" TagPrefix="uc1" TagName="ucMenu" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Productos</title>
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
            height: 0%;
            clear: both;
        }
        td {
            border: 1px none black;
        }
        .tblSinBordes {
            border-collapse: collapse;
        }
    </style>
</head>
<body oncontextmenu="JavaScript:return true;" onload="Desbloquear();" onunload="Bloquear();" onbeforeunload="">
<script type="text/javascript">
    function DeclararControles() {
        var hdnUsuarioId = document.getElementById('<%=hdnUsuarioId.ClientID%>');
        var txtNombre = document.getElementById('<%=txtNombre.ClientID%>');
        var txtDescripcion = document.getElementById('<%=txtDescripcion.ClientID%>');
        var txtMarca = document.getElementById('<%=txtMarca.ClientID%>');
        var ddlTipoProducto = document.getElementById('<%=ddlTipoProducto.ClientID%>');
        var ddlSubTipoProducto = document.getElementById('<%=ddlSubTipoProducto.ClientID%>');
        var txtPrecioNormal = document.getElementById('<%=txtPrecioNormal.ClientID%>');      
               
        var chkActivo = document.getElementById('<%=chkActivo.ClientID%>');
    }

    function Buscar() {
        var grid = document.getElementById('grvUsuario');
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
                    ele = eleNombre + eleTelefono + eleCorreo + eleDepartamento + eleCargo;
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
    function VerProducto(productoId) {
        PageMethods.ObtenerProducto(productoId, ObtenerUsuarioOk, fnLlamadaError);
        return false;
    }
    function ObtenerSubTipoProducto() {
         var ddlTipoProducto = document.getElementById('<%=ddlTipoProducto.ClientID%>');
        PageMethods.ObtenerSubTipoProducto(ddlTipoProducto.value, ObtenerSubTipoProductoOk, fnLlamadaError);
        return false;
    }
    function LimpiarProducto() {
        debugger;
        var hdnUsuarioId = document.getElementById('<%=hdnUsuarioId.ClientID%>');
        var txtCodigo = document.getElementById('<%=txtCodigo.ClientID%>');
        var txtNombre = document.getElementById('<%=txtNombre.ClientID%>');
        var txtDescripcion = document.getElementById('<%=txtDescripcion.ClientID%>');
        var txtMarca = document.getElementById('<%=txtMarca.ClientID%>');
        var ddlTipoProducto = document.getElementById('<%=ddlTipoProducto.ClientID%>');
        var ddlSubTipoProducto = document.getElementById('<%=ddlSubTipoProducto.ClientID%>');
        var ddlUnidadMedida= document.getElementById('<%=ddlUnidadMedida.ClientID%>');
        var txtPrecioNormal = document.getElementById('<%=txtPrecioNormal.ClientID%>');      
        var txtPrecioDescuento = document.getElementById('<%=txtPrecioDescuento.ClientID%>');      
        var txtPrecioCompra = document.getElementById('<%=txtPrecioCompra.ClientID%>');


        var chkActivo = document.getElementById('<%=chkActivo.ClientID%>');
        var chkCanastilla = document.getElementById('<%=chkCanastilla.ClientID%>');
        var chkEstacion = document.getElementById('<%=chkEstacion.ClientID%>');
        var chkMarket = document.getElementById('<%=chkMarket.ClientID%>');

        var lstTipoNegocio = document.getElementById('<%=lstTipoNegocio.ClientID%>');

        $('#' + lstTipoNegocio.id).ready(function () {
            $('#' + lstTipoNegocio.id + ' option').each(function () {
                $(this).prop('selected', false);
            });
            $('#' + lstTipoNegocio.id).multiselect('refresh');
        });

        hdnUsuarioId.value = 0;
        txtCodigo.value = '';
        document.getElementById('lblTituloUsuario').innerText = 'Producto';
        txtNombre.value = '';
        txtDescripcion.value = '';
        txtMarca.value = '';
        ddlTipoProducto.value = 0;
        ddlUnidadMedida.value = 0;
        txtPrecioNormal.value = '0.00';
        txtPrecioDescuento.value = '0.00';
        txtPrecioCompra.value = '0.00';
        LimpiarCombo(ddlSubTipoProducto);
        AgregarOptionACombo(ddlSubTipoProducto, Seleccione_value, Seleccione);


        chkActivo.checked = true;

        chkCanastilla.checked = false;
        chkEstacion.checked = false;
        chkMarket.checked = false;

        var tabUsuario = $get('<%=tabUsuario.ClientID%>');
        tabUsuario.control.set_activeTabIndex(0);
    }
    function ObtenerUsuarioOk(u) {
        debugger;
        var modalDialog = $find('mpeUsuario');
        if (u != null) {
            modalDialog.show();

            var tabUsuario = $get('<%=tabUsuario.ClientID%>');
            tabUsuario.control.set_activeTabIndex(0);

            var hdnUsuarioId = document.getElementById('<%=hdnUsuarioId.ClientID%>');
            var txtCodigo = document.getElementById('<%=txtCodigo.ClientID%>');
            var txtNombre = document.getElementById('<%=txtNombre.ClientID%>');
            var txtDescripcion = document.getElementById('<%=txtDescripcion.ClientID%>');
            var txtMarca = document.getElementById('<%=txtMarca.ClientID%>');
            var ddlTipoProducto = document.getElementById('<%=ddlTipoProducto.ClientID%>');
            var ddlSubTipoProducto = document.getElementById('<%=ddlSubTipoProducto.ClientID%>');
            var ddlUnidadMedida = document.getElementById('<%=ddlUnidadMedida.ClientID%>');
            var txtPrecioNormal = document.getElementById('<%=txtPrecioNormal.ClientID%>');      
            var txtPrecioDescuento = document.getElementById('<%=txtPrecioDescuento.ClientID%>');      
            var txtPrecioCompra = document.getElementById('<%=txtPrecioCompra.ClientID%>');

            var lstTipoNegocio = document.getElementById('<%=lstTipoNegocio.ClientID%>');

            var chkEstacion = document.getElementById('<%=chkEstacion.ClientID%>');
            var chkMarket = document.getElementById('<%=chkMarket.ClientID%>');
            var chkCanastilla = document.getElementById('<%=chkCanastilla.ClientID%>');

            var chkActivo = document.getElementById('<%=chkActivo.ClientID%>');

            hdnUsuarioId.value = u.ProductoId;
            document.getElementById('lblTituloUsuario').innerText = u.Producto;
            txtCodigo.value = u.Codigo;
            txtNombre.value = u.Producto;
            txtDescripcion.value = u.Descripcion;
            txtMarca.value = u.Marca;
            ddlTipoProducto.value = u.TipoProductoId;
            ddlUnidadMedida.value = u.UnidadMedidaId;
           
            txtPrecioNormal.value = u.PrecioNormal.toFixed(2);
            txtPrecioDescuento.value = u.PrecioDescuento.toFixed(2);
            txtPrecioCompra.value = u.PrecioCompra.toFixed(2);

            ObtenerSubTipoProducto();
            ddlSubTipoProducto.value = u.SubTipoProductoId;

            document.getElementById('<%=btnBuscarStock.ClientID%>').click();

            PageMethods.ObtenerProductoTipoNegocio(u.ProductoId, ObtenerProductoTipoNegocioOk, fnLlamadaError);

            chkEstacion.checked = (u.DisponibleEstacion > 0);
            chkMarket.checked = (u.DisponibleMarket > 0);
            chkCanastilla.checked = (u.DisponibleCanastilla > 0);

            chkActivo.checked = (u.Activo > 0);
        } else {
            modalDialog.hide();
        }
    }
    function ObtenerSubTipoProductoOk(lista) {
        var ddlSubTipoProducto = document.getElementById('<%=ddlSubTipoProducto.ClientID%>');
        LlenarCombo(ddlSubTipoProducto, lista, 'TipoProductoId', 'Nombre', true);
        AgregarOptionACombo(ddlSubTipoProducto, Seleccione_value, Seleccione);
    }
    function ObtenerProductoTipoNegocioOk(p) {
        if (p != null) {
            var lstTipoNegocio = document.getElementById('<%=lstTipoNegocio.ClientID%>');

            $('#' + lstTipoNegocio.id).ready(function () {
                $('#' + lstTipoNegocio.id + ' option').each(function () {
                    $(this).prop('selected', false);
                });
                $('#' + lstTipoNegocio.id).multiselect('refresh');
            });
            
                
            $('#' + lstTipoNegocio.id).ready(function () {
                $('#' + lstTipoNegocio.id + ' option').each(function () {
                    var j = $(this).val();
                    for (var i = 0; i < p.length; ++i) {
                        var row = p[i];
                        if (j == row.TipoTiendaId) {
                            $(this).prop('selected', true);
                        }
                    }
                });
                $('#' + lstTipoNegocio.id).multiselect('refresh');
            });
        }
    }
    function InsertarProducto() {
        var modalDialog = $find('mpeUsuario');

        modalDialog.show();
        LimpiarProducto();

        return false;
    }
    function ValidarUsuario() {
        debugger;
        var hdnUsuarioId = document.getElementById('<%=hdnUsuarioId.ClientID%>');
        var txtNombre = document.getElementById('<%=txtNombre.ClientID%>');
        var txtDescripcion = document.getElementById('<%=txtDescripcion.ClientID%>');
        var txtMarca = document.getElementById('<%=txtMarca.ClientID%>');
        var ddlTipoProducto = document.getElementById('<%=ddlTipoProducto.ClientID%>');
        var ddlSubTipoProducto = document.getElementById('<%=ddlSubTipoProducto.ClientID%>');
        var txtPrecioNormal = document.getElementById('<%=txtPrecioNormal.ClientID%>');      

        var chkEstacion = document.getElementById('<%=chkEstacion.ClientID%>');
        var chkMarket = document.getElementById('<%=chkMarket.ClientID%>');
        var chkCanastilla = document.getElementById('<%=chkCanastilla.ClientID%>');

        var lstTipoNegocio = document.getElementById('<%=lstTipoNegocio.ClientID%>');

        var chkActivo = document.getElementById('<%=chkActivo.ClientID%>');
        var tabUsuario = $get('<%=tabUsuario.ClientID%>');
       

        if (txtNombre.value.trim() == '') {
            return MostrarValidacion(txtNombre, 'Ingrese Nombre Producto.');
        }
       
        if (ddlTipoProducto.value.trim() == '0') {
            return MostrarValidacion(ddlTipoProducto, 'Seleccione Tipo Producto.');
        }

        if (ddlSubTipoProducto.value.trim() == '0' || ddlSubTipoProducto.value.trim() == '') {
            return MostrarValidacion(ddlSubTipoProducto, 'Seleccione Sub Tipo Producto.');
        }
       
        //if (txtMarca.value.trim() == '') {
        //    return MostrarValidacion(txtMarca, 'Ingrese Marca Producto.');
        //}
        if (txtPrecioNormal.value.trim() == '') {
            tabUsuario.control.set_activeTabIndex(1);
            return MostrarValidacion(txtPrecioNormal, 'Ingrese el Precio Normal.');
        }
        //if (!(chkEstacion.checked && chkMarket.checked && chkCanastilla.checked)) {
        //    return MostrarMensaje('Debe seleccionar un negocio.');
        //}

        var tipoNegocio = '';
        for (var i = 0; i < lstTipoNegocio.options.length; ++i) {
            if (lstTipoNegocio.options[i].selected) {
                // alert(lstTipoNegocio.options[i].value);
                tipoNegocio = tipoNegocio + lstTipoNegocio.options[i].value + ',';
            }
        }
        tipoNegocio = tipoNegocio.substring(0, tipoNegocio.length - 1);
        alert(tipoNegocio);

        if (tipoNegocio.length == 0) {
            MostrarMensaje('Debe seleccionar un Tipo de Negocio.');
            return false;
        }

        //return false;
    }

    function fnLlamadaError(excepcion) {
        alert('Ha ocurrido un error interno: ' + excepcion.get_message());
        // Desbloquear Pantalla
        Desbloquear();
    }
    function Cerrar() {
        var modalDialog = $find('mpeUsuario');
        modalDialog.hide();
    }
</script>
    <form id="frmUsuario" runat="server" DefaultButton="btnDefault">
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
                            <asp:Label CssClass="lblTitulo" Width="100%" runat="server">PRODUCTOS</asp:Label>
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
                                        <asp:LinkButton ID="imgAnadir" ForeColor="#404040" runat="server" ToolTip="" OnClientClick="return InsertarProducto();">
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
                                            <th style="width: 10%; cursor: pointer; text-decoration: underline;">Codigo</th>
                                            <th style="width: 15%; cursor: pointer; text-decoration: underline;">Nombre</th>                                            
                                            <th style="width: 15%; cursor: pointer; text-decoration: underline;">Tipo Producto</th>
                                            <th style="width: 15%; cursor: pointer; text-decoration: underline;">Sub Tipo</th>
                                            <th style="width: 15%; cursor: pointer; text-decoration: underline;">Marca</th>
                                            <th style="width: 15%; cursor: pointer; text-decoration: underline;">Precio Normal</th>
                                            <th style="width: 10%; cursor: pointer; text-decoration: underline;">Activo</th>
                                        </tr>
                                    </thead>
                                </table>
                            </div>
                            <asp:UpdatePanel ID="divData" runat="server" UpdateMode="Conditional" style="overflow: scroll; width: 1200px; height: 420px; margin: 0 0 0px 0px;" onscroll="Onscrollfnction();">
                                <ContentTemplate>
                                    <asp:GridView id="grvUsuario" AutoGenerateColumns="False" Width="1500px" RowStyle-Wrap="True" runat="server" OnRowDataBound="grvUsuario_RowDataBound">
                                        <HeaderStyle CssClass="filaCabeceraGrid"></HeaderStyle>
                                        <RowStyle CssClass="filaImparGrid"></RowStyle>
                                        <AlternatingRowStyle CssClass="filaParGrid"></AlternatingRowStyle>
                                        <Columns>
                                            <%-- 0: Acciones --%>
                                            <asp:TemplateField HeaderText="Acciones">
                                                <ItemStyle Width="5%" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:ImageButton runat="server" ImageUrl="~/Imagenes/Iconos/editar.png" CssClass="imgButton" OnClientClick='<%# "return VerProducto(" + Eval("ProductoId") + ");" %>' ToolTip="Editar" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <%-- 1: Codigo --%>
                                            <asp:TemplateField HeaderText="Telef. Trabajo">
                                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <%# Eval("Codigo")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--  2: Nombre --%>
                                            <asp:TemplateField HeaderText="Nombre">
                                                <ItemStyle Width="15%" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                      <%# Eval("Producto")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>                                           
                                            <%-- 3: Tipo Producto --%>
                                            <asp:TemplateField HeaderText="Correo Trabajo">
                                                <ItemStyle Width="15%" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <%# Eval("TipoProducto")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- 4: Sub Tipo --%>
                                            <asp:TemplateField HeaderText="Departamento">
                                                <ItemStyle Width="15%" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <%# Eval("SubTipoProducto")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- 5: Marca --%>
                                            <asp:TemplateField HeaderText="Cargo">
                                                <ItemStyle Width="15%" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <%# Eval("Marca")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- 6: Precio Normal --%>
                                            <asp:TemplateField HeaderText="Director">
                                                <ItemStyle Width="15%" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <%# Eval("PrecioNormal")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- 7: Activo --%>
                                            <asp:TemplateField>
                                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                     <%# Convert.ToInt16(Eval("Activo")) > 0 ? "Si" : "No"%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <%-- 8: ProductoId --%>
                                            <asp:TemplateField>
                                                <ItemStyle CssClass="columnaOcultaGrid" />
                                                <ItemTemplate>
                                                    <%# Eval("ProductoId")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlNumeroRegistros" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="btnPaginacion" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btnGuardarUsuario" EventName="Click" />
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
            <%--<div id="pie">pie</div>--%>
            <asp:Panel ID="pnlUsuario" runat="server" CssClass="pnlModal" style="display: none;">
                <table style="width: 100%;">
                    <tr id="lblTituloProductos" class="lblTituloPopup">
                        <td style="width: 20%;" class="lblTituloPopup">&nbsp;</td>
                        <td id="lblTituloUsuario" class="lblTituloPopup" style="width: 60%">Producto</td>
                        <td class="lblTituloPopup" style="width: 20%; text-align: right;">
                            <a onclick="return Cerrar();" style="color: #FFFFFF; text-decoration: none; cursor: pointer;">Cerrar X</a>&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <input type="hidden" id="hdnUsuarioId" value="0" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="width: 100%;">
                            <ajaxToolkit:TabContainer id="tabUsuario" runat="server" OnClientActiveTabChanged="" Height="450px" Width="880px" ActiveTabIndex="0" OnDemand="False" AutoPostBack="false"
                                                      TabStripPlacement="Top" CssClass="ajax__tab_xp" ScrollBars="None" UseVerticalStripPlacement="False" VerticalStripWidth="120px">
                                <ajaxToolkit:TabPanel id="tabDatosProducto" runat="server" HeaderText="Informaci&oacute;n Producto" Enabled="true" ScrollBars="Auto" OnDemandMode="Always" TabIndex="0">
                                    <ContentTemplate>
                                        <table style="width: 100%;">
                                             <tr>
                                                <td class="lblStandar">C&oacute;digo</td>
                                                <td>
                                                    <asp:TextBox id="txtCodigo" CssClass="txtStandar" runat="server" Width="60%" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="lblStandar">Nombre</td>
                                                <td>
                                                    <asp:TextBox id="txtNombre" CssClass="txtStandar" runat="server" Width="60%" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="lblStandar">Descripci&oacute;n</td>
                                                <td>
                                                    <asp:TextBox id="txtDescripcion" CssClass="txtStandarMultiline" TextMode="MultiLine" Width="300px" Rows="6" runat="server" />
                                                </td>
                                            </tr>
                                             <tr>
                                                <td class="lblStandar">Tipo Producto</td>
                                                <td>
                                                    <asp:DropDownList id="ddlTipoProducto" CssClass="ddlStandar" runat="server" Width="60%" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="lblStandar">Sub TipoProducto</td>
                                                <td>
                                                    <asp:DropDownList id="ddlSubTipoProducto" CssClass="ddlStandar" runat="server" Width="60%" />
                                                </td>
                                            </tr> 
                                             <tr>
                                                <td class="lblStandar">Unidad Medida</td>
                                                <td>
                                                    <asp:DropDownList id="ddlUnidadMedida" CssClass="ddlStandar" runat="server" Width="60%" />
                                                </td>
                                            </tr>                                             
                                            <tr>
                                                <td class="lblStandar">Marca</td>
                                                <td>
                                                    <asp:TextBox id="txtMarca" CssClass="txtStandar" runat="server" Width="60%" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="lblStandar">Disponible en</td>
                                                <td>
                                                    <asp:CheckBox ID="chkEstacion" runat="server" Text="Estaci&oacute;n" style="display: none;" />
                                                    <asp:CheckBox ID="chkCanastilla" runat="server" Text="Canastilla" style="display: none;" />
                                                    <asp:CheckBox ID="chkMarket" runat="server" Text="Market" style="display: none;" />
                                                    <%--<asp:DropDownList id="ddlTipoNegocio" CssClass="txtStandar" runat="server" />--%>
                                                    <asp:ListBox ID="lstTipoNegocio" runat="server" SelectionMode="Multiple" CssClass="ddlStandar" Width="60%">
                                                        <asp:ListItem Text="Todos" Value="0" />
                                                        <asp:ListItem Text="Estacion" Value="1" />
                                                        <asp:ListItem Text="Canastilla" Value="2" />
                                                        <asp:ListItem Text="Market" Value="3" />
                                                        <asp:ListItem Text="Pyme" Value="4" />
                                                    </asp:ListBox>
                                                </td>
                                            </tr>
                                             <tr>
                                                <td class="lblStandar">Imagen</td>
                                                <td>
                                                    <asp:FileUpload id="fuImagen" Width="90%" runat="server" />
                                                </td>                                                
                                            </tr>
                                            <tr>
                                                <td class="lblStandar">Activo</td>
                                                <td>
                                                    <asp:CheckBox id="chkActivo" Checked="True" runat="server" />
                                                </td>
                                            </tr>   
                                        </table>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>
                                <ajaxToolkit:TabPanel id="tabDatosPrecio" runat="server" HeaderText="Precio Producto" Enabled="true" ScrollBars="Auto" OnDemandMode="Always" TabIndex="1">
                                    <ContentTemplate>
                                        <table style="width: 100%;">
                                            <tr>
                                                <td class="lblStandar">Precio Normal</td>
                                                <td>
                                                    <asp:TextBox id="txtPrecioNormal" CssClass="txtStandarMonto" runat="server" />
                                                </td>
                                            </tr>                                            
                                            <tr>
                                                <td class="lblStandar">Precio Descuento</td>
                                                <td>
                                                    <asp:TextBox id="txtPrecioDescuento" CssClass="txtStandarMonto" runat="server" />
                                                </td>
                                            </tr>   
                                            <tr>
                                                <td class="lblStandar">Precio Compra</td>
                                                <td>
                                                    <asp:TextBox id="txtPrecioCompra" CssClass="txtStandarMonto" runat="server" />
                                                </td>
                                            </tr>   
                                            <tr>
                                                <td class="lblStandar">&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                <td>
                                                   
                                                </td>
                                            </tr>
                                            <tr>
                                                 <td colspan="2">                                                    
                                                    <asp:UpdatePanel ID="upStock" runat="server" UpdateMode="Conditional" style="overflow-y: scroll; height: 150px; margin: 0 0 0px 0px;" onscroll="Onscrollfnction();">
                                                        <ContentTemplate>
                                                            <asp:GridView id="grvStockProducto" AutoGenerateColumns="False" Width="100%" RowStyle-Wrap="True" runat="server">
                                                                <HeaderStyle CssClass="filaCabeceraGrid"></HeaderStyle>
                                                                <RowStyle CssClass="filaImparGrid"></RowStyle>
                                                                <AlternatingRowStyle CssClass="filaParGrid"></AlternatingRowStyle>
                                                                <Columns>
                                                                    <%-- 0: Almacen --%>
                                                                    <asp:TemplateField HeaderText="Almacen">
                                                                        <ItemStyle Width="40%" HorizontalAlign="Center" />
                                                                        <ItemTemplate>
                                                                             <%# Eval("Almacen").ToString().Trim() %> 
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <%--  1: Inv Actual --%>
                                                                     <asp:TemplateField HeaderText="Stock">
                                                                        <ItemStyle Width="30%" HorizontalAlign="Center" />
                                                                        <ItemTemplate>
                                                                            <%# Convert.ToDecimal(Eval("InventarioActual")).ToString("###,##0.#0") %>                                                    
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <%-- 2: Inv Minimo --%>
                                                                    <asp:TemplateField HeaderText="Stock Minimo">
                                                                        <ItemStyle Width="25%" HorizontalAlign="Center" />
                                                                        <ItemTemplate>
                                                                            <%# Convert.ToDecimal(Eval("InventarioMinimo")).ToString("###,##0.#0") %>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>                                          
                                                                    <asp:TemplateField>
                                                                        <ItemStyle Width="5%" HorizontalAlign="Center" />
                                                                        <ItemTemplate>
                                                   
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>  
                                                                </Columns>
                                                            </asp:GridView>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnBuscarStock" EventName="Click" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
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
                            <asp:Button ID="btnGuardarUsuario" runat="server" CssClass="btnStandar" Text="Guardar" Width="30%" OnClientClick="return ValidarUsuario();" OnClick="btnGuardarUsuario_OnClick" />
                            <asp:Button ID="btnCancelarUsuario" runat="server" CssClass="btnStandar" Text="Cancelar" Width="30%" />                             
                            <asp:Button id="hButton" runat="server" style="display:none;" />
                            <asp:Button id="btnBuscarStock" runat="server" Width="0%"  style="display:none;" OnClick="btnCargarStock_OnClick" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <ajaxToolkit:ModalPopupExtender ID="mpeUsuario" PopupControlID="pnlUsuario" TargetControlID="hButton" CancelControlID="btnCancelarUsuario" Drag="True" DropShadow="True" BackgroundCssClass="FondoAplicacion" ClientIDMode="Static" runat="server" PopupDragHandleControlID="lblTituloUsuario"></ajaxToolkit:ModalPopupExtender>
            <ajaxToolkit:RoundedCornersExtender ID="rceUsuario" runat="server" BehaviorID="rcbUsuario" TargetControlID="pnlUsuario" Radius="6" Corners="All" />
            
        </div>
    </form>
    <script type="text/javascript" src="../Scripts/tablesorter/jquery.tablesorter.js"></script>
    <script type="text/javascript">
        function pageLoad(sender, args) {
            $(document).ready(function () {
                $("#grvUsuario").tablesorter({ dateFormat: 'uk' });
                SetDefaultSortOrder();
            });           
        }

        function Sort(cell, sortOrder) {
            var sorting = [[cell.cellIndex, sortOrder]];
            $("#grvUsuario").trigger("sorton", [sorting]);
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
            var grvUsuario = document.getElementById('grvUsuario').clientWidth;


            var divData = centro - 20;
            var divHeader = divData;
            document.getElementById('divHeader').style.width = divHeader + 'px';

            if (dummyHeader < divHeader) {
                document.getElementById('dummyHeader').style.width = divHeader + 'px';
                document.getElementById('grvUsuario').style.width = divData + 'px';
            }

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
            document.getElementById('pnlUsuario').style.position = 'absolute';
            document.getElementById('pnlUsuario').style.top = '50%';
            document.getElementById('pnlUsuario').style.left = '50%';

            document.getElementById('pnlUsuario').style.display = 'table';
            document.getElementById('pnlUsuario').style.margin = '0 auto';
            document.getElementById('pnlUsuario').style.width = '900px';
            document.getElementById('tabUsuario').style.width = '880px';
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
            if ((postBackElement.id == 'ddlNumeroRegistros') || (postBackElement.id == 'btnPrimero') || (postBackElement.id == 'btnAnterior') || (postBackElement.id == 'btnSiguiente') ||
                (postBackElement.id == 'btnUltimo') || (postBackElement.id == 'btnPaginacion')) {
                // Bloqueo Pantalla
                Bloquear();
            }
        }
        function EndRequest(sender, args) {
            if ((postBackElement.id == 'ddlNumeroRegistros') || (postBackElement.id == 'btnPrimero') || (postBackElement.id == 'btnAnterior') || (postBackElement.id == 'btnSiguiente') ||
                (postBackElement.id == 'btnUltimo') || (postBackElement.id == 'btnPaginacion')) {
                // Desbloquear Pantalla
                Desbloquear();
                RedimensionarGrid();
            }
        }
        function grvUsuario_OnDnlClick(row) {
            var indiceUsuarioId = 7;
            var usuarioIdRow = RetornarCeldaValor(row, indiceUsuarioId);
            VerUsuario(usuarioIdRow);
        }
    </script>
<link href="../Scripts/multiselect/css/bootstrap-multiselect.css" rel="stylesheet" type="text/css" />
<script src="../Scripts/multiselect/js/bootstrap-multiselect.js" type="text/javascript"></script>
<script type="text/javascript">
    $(function () {
        $('[id*=lstTipoNegocio]').multiselect({
            includeSelectAllOption: true, nonSelectedText: 'Ninguno Seleccionado'
        });
    });
</script>
</body>
</html>