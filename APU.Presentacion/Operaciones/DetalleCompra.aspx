<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DetalleCompra.aspx.cs" Inherits="APU.Presentacion.Operaciones.DetalleCompra" EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="uc1" TagName="ucProcesando" Src="~/Controles/ucProcesando.ascx" %>
<%@ Register Src="~/Controles/ucCabecera.ascx" TagPrefix="uc1" TagName="ucCabecera" %>
<%@ Register Src="~/Controles/ucMenu.ascx" TagPrefix="uc1" TagName="ucMenu" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Detalle Compra</title>
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
        PageMethods.ObtenerVenta(ventaId, ObtenerVentaOk, fnLlamadaError);
        return false;
    }
    function VerProveedor() {
        //alert('1');
        var mdCliente = $find('mpeCliente');
        mdCliente.show();
        var txtNumeroDocumentoPersonaNatural = document.getElementById('<%=txtRuc.ClientID%>');
        txtNumeroDocumentoPersonaNatural.focus();
        //alert('2');
    }
    function VerProducto() {
        //alert('1');
        var mdCliente = $find('mpeProducto');
        mdCliente.show();
        var txtNumeroDocumentoPersonaNatural = document.getElementById('<%=txtCodigoBuscar.ClientID%>');
        txtNumeroDocumentoPersonaNatural.focus();
        //alert('2');
    }
    function LimpiarVenta() {
        var hdnVentaId = document.getElementById('<%=hdnVentaId.ClientID%>');
        <%--var ddlEmpresa = document.getElementById('<%=ddlEmpresa.ClientID%>');
        var txtCodigo = document.getElementById('<%=txtCodigo.ClientID%>');
        var txtNombre = document.getElementById('<%=txtNombre.ClientID%>');
        var txtDescripcion = document.getElementById('<%=txtDescripcion.ClientID%>');--%>
       

        hdnVentaId.value = 0;
        //ddlEmpresa.value = 0;
        //txtNombre.value = '';
        //txtCodigo.value = '';
        //txtDescripcion.value = '';
        //chkActivo.checked = true;
        //txtDireccion.value = '';
        //lblModificadoPor.innerText = '';
        //lblFechaModificacion.innerText = '';
        <%--
        var tabVenta = $get('<%=tabVenta.ClientID%>');
        tabVenta.control.set_activeTabIndex(0);--%>
    }
    function ObtenerVentaOk(v) {
        var modalDialog = $find('mpeVenta');
        if (v != null) {
            modalDialog.show();
             <%--
            var tabVenta = $get('<%=tabVenta.ClientID%>');
            tabVenta.control.set_activeTabIndex(0);--%>

            var hdnVentaId = document.getElementById('<%=hdnVentaId.ClientID%>');
            <%--var ddlEmpresa = document.getElementById('<%=ddlEmpresa.ClientID%>');
            var txtCodigo = document.getElementById('<%=txtCodigo.ClientID%>');
            var txtNombre = document.getElementById('<%=txtNombre.ClientID%>');
            var txtDescripcion = document.getElementById('<%=txtDescripcion.ClientID%>');--%>
            
            hdnVentaId.value = v.VentaId;
            //ddlEmpresa.value = v.EmpresaId;
            //txtCodigo.value = v.Codigo;
            document.getElementById('lblTituloVenta').innerText = v.Nombre;
            //txtNombre.value = v.Nombre;
            //txtDescripcion.value = v.Descripcion;
            //chkActivo.checked = (v.Activo > 0);
            //txtDireccion.value = v.Direccion;
            //lblModificadoPor.innerText = v.UsuarioModificacion;
            //lblFechaModificacion.innerText = FormatoFecha(v.FechaModificacion);
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
    function SeleccionarCliente(proveedorId, proveedor, tipoDocumentoId, numeroDocumento, direccion, email) {
        var mdCliente = $find('mpeCliente');
        var hdnProveedorId = document.getElementById('<%=hdnProveedorId.ClientID%>');
        var txtProveedor = document.getElementById('<%=txtProveedor.ClientID%>');
        
        var txtNumeroDocumentoProveedor = document.getElementById('<%=txtNumeroDocumentoProveedor.ClientID%>');
        var txtDireccion = document.getElementById('<%=txtDireccion.ClientID%>');
        

        hdnProveedorId.value = proveedorId;
        txtProveedor.value = proveedor;
        
       
        txtNumeroDocumentoProveedor.value = numeroDocumento;
        txtDireccion.value = direccion;
        

        mdCliente.hide();
        return false;
    }
    function SeleccionarProducto(productoId, codigo, descripcion, unidadMedidaId, precio) {
        debugger;
       
        var mdProducto = $find('mpeProducto');
        var hdnProductoId = document.getElementById('<%=hdnProductoId.ClientID%>');
        var txtProducto = document.getElementById('<%=txtProductoCodigo.ClientID%>');
        var txtProductoDescripcion = document.getElementById('<%=txtProductoDescripcion.ClientID%>');
        var ddlUnidad = document.getElementById('<%=ddlUnidad.ClientID%>');      
        var txtPrecio = document.getElementById('<%=txtPrecio.ClientID%>');    

        hdnProductoId.value = productoId;
        txtProducto.value = codigo;
        //txtPrecio.value = precio;
        
        ddlUnidad.value = unidadMedidaId;
        txtProductoDescripcion.value = descripcion;      

        mdProducto.hide(); 
        return false;
    }
    function LimpiarProductoDetalle() {
        var hdnProductoId = document.getElementById('<%=hdnProductoId.ClientID%>');
        var hdnCompraDetalleid = document.getElementById('<%=hdnCompraDetalleid.ClientID%>');
        var txtProducto = document.getElementById('<%=txtProductoCodigo.ClientID%>');
        var txtProductoDescripcion = document.getElementById('<%=txtProductoDescripcion.ClientID%>');
        var ddlUnidad = document.getElementById('<%=ddlUnidad.ClientID%>');     
        var txtPrecio = document.getElementById('<%=txtPrecio.ClientID%>');  
        var txtCantidad = document.getElementById('<%=txtCantidad.ClientID%>');  
        var txtSubTotal = document.getElementById('<%=txtSubTotal.ClientID%>');  
        var txtIgv= document.getElementById('<%=txtIgv.ClientID%>');  
        var txtTotal = document.getElementById('<%=txtTotal.ClientID%>');  
        var ddlAlmacen = document.getElementById('<%=ddlAlmacen.ClientID%>');       

        hdnProductoId.value = 0;
        hdnCompraDetalleid.value = 0;
        txtProducto.value = '';
        txtProductoDescripcion.value = '';
        ddlUnidad.value = '0';
        txtPrecio.value = '0.00';
        txtCantidad.value = '0';
        txtSubTotal.value = '0.00';
        txtIgv.value = '0.00';
        txtTotal.value = '0.00';        
        ddlAlmacen.value = '0';

        return false;

    }
    function ValidarComprobante() {
        var tipoComprobanteId = document.getElementById('<%=ddlTipoComprobante.ClientID%>');
        var txtSerieComprobante = document.getElementById('<%=txtSerie.ClientID%>');
        var txtNumeroComprobante = document.getElementById('<%=txtNumeroDocumento.ClientID%>');
        var ddlMoneda = document.getElementById('<%=ddlMoneda.ClientID%>');
        var txtFechaEmision = document.getElementById('<%=txtFechaEmision.ClientID%>');
              
        var txtProveedor = document.getElementById('<%=txtProveedor.ClientID%>');        
        var hdnProveedorId = document.getElementById('<%=hdnProveedorId.ClientID%>');
        var hdnCompraId = document.getElementById('<%=hdnCompraId.ClientID%>');
        var ddlMotivoIngreso = document.getElementById('<%=ddlMotivoIngreso.ClientID%>');
        

        if (tipoComprobanteId.value.trim() == '0') {
            return MostrarValidacion(tipoComprobanteId, 'Seleccione tipo Comprobante.');
        }
        if (txtSerieComprobante.value.trim() == '') {
            return MostrarValidacion(txtSerieComprobante, 'Ingrese serie comprobante.');
        }
        if (txtNumeroComprobante.value.trim() == '') {
            return MostrarValidacion(txtNumeroComprobante, 'Ingrese número comprobante.');
        }
        if (ddlMoneda.value.trim() == '0') {
            return MostrarValidacion(ddlMoneda, 'Seleccione moneda Comprobante.');
        }
        if (txtFechaEmision.value.trim() == '') {
            return MostrarValidacion(txtFechaEmision, 'Ingrese fecha emisión comprobante.');
        }
        if (ddlMotivoIngreso.value.trim() == '0' || ddlMotivoIngreso.value.trim() == '') {
            return MostrarValidacion(ddlMotivoIngreso, 'Seleccione motivo ingreso.');
        }
        
        if (txtProveedor.value.trim() == '') {
            return MostrarValidacion(txtProveedor, 'Ingrese el proveedor.');
        }        

        var grvItem = document.getElementById('<%=grvVenta.ClientID%>');
        if (grvItem != null) {
            var rows = grvItem.rows;
            if (rows.length == 0) {
                //return ValidarGuardar();
                MostrarMensaje('Debe ingresar el Detalle de la Compra.');
                return false;
            }
        } else {
            MostrarMensaje('Debe ingresar el Detalle de la Compra.');
            return false;
        }
        return true;

    }
    function VerBandeja() {

        window.location = 'Compra.aspx';
        return false;
    }
    function GenerarComprobante() {

        var validarComprobante = ValidarComprobante();
        if (validarComprobante) {
            var tipoComprobanteId = document.getElementById('<%=ddlTipoComprobante.ClientID%>');
            var txtSerieComprobante = document.getElementById('<%=txtSerie.ClientID%>');
            var txtNumeroComprobante = document.getElementById('<%=txtNumeroDocumento.ClientID%>');
            var ddlMoneda = document.getElementById('<%=ddlMoneda.ClientID%>');
            var txtFechaEmisionComprobante = document.getElementById('<%=txtFechaEmision.ClientID%>');
            var ddlMotivoIngreso = document.getElementById('<%=ddlMotivoIngreso.ClientID%>');
            
            var txtProveedor = document.getElementById('<%=txtProveedor.ClientID%>');
            var txtGlosa = document.getElementById('<%=txtGlosa.ClientID%>');
            var ddlAlmacen = document.getElementById('<%=ddlAlmacen.ClientID%>');

            var hdnProveedorId = document.getElementById('<%=hdnProveedorId.ClientID%>');
            var hdnCompraId = document.getElementById('<%=hdnCompraId.ClientID%>');
        
            var proveedorId = hdnProveedorId.value;
            var compraId = hdnCompraId.value;
            var serie = txtSerieComprobante.value;
            var numeroComprobante = txtNumeroComprobante.value;
            var monedaId = ddlMoneda.value;            

            var fechaEmision = txtFechaEmisionComprobante.value;            
            
            var montoVenta = parseFloat(lblComprobanteSubTotal.innerText.replace(/,/g, ''));
            var montoImpuesto = parseFloat(lblComprobanteIgv.innerText.replace(/,/g, ''));
            var montoTotal = parseFloat(lblComprobanteTotal.innerText.replace(/,/g, ''));

            Bloquear();
            PageMethods.GenerarComprobante(compraId, proveedorId, tipoComprobanteId.value, serie, numeroComprobante, fechaEmision, monedaId, 
                                           montoVenta, montoImpuesto, montoTotal, txtGlosa.value, ddlMotivoIngreso.value, GenerarComprobanteOk, fnLlamadaError);
        }       
        
    }

    function GenerarComprobanteOk(c) {
        var hdnCompraId = document.getElementById('<%=hdnCompraId.ClientID%>');
        var arrayComprobante = c.split('@');
        var compraId = arrayComprobante[0];
        var mensaje = arrayComprobante[1];
        if (compraId > 0) {
           
            hdnCompraId.value = compraId;
            MostrarMensaje(mensaje);
            window.location.href = 'Compra.aspx';
        } else {
            MostrarMensaje(mensaje);
        }
        Desbloquear();
        
    }

    function AnularComprobante() {
        var hdnCompraId = document.getElementById('<%=hdnCompraId.ClientID%>');
        var compraId = hdnCompraId.value;

        Bloquear();
        PageMethods.AnularComprobante(compraId, AnularComprobanteOk, fnLlamadaError);
    }
    function AnularComprobanteOk(c) {
        //var hdnCompraId = document.getElementById('<%=hdnCompraId.ClientID%>');
        var arrayComprobante = c.split('@');
        var compraId = arrayComprobante[0];
        var mensaje = arrayComprobante[1];
        if (compraId > 0) {
           
            //hdnCompraId.value = compraId;
            MostrarMensaje(mensaje);
            window.location.href = 'Compra.aspx';
        } else {
            MostrarMensaje(mensaje);
        }
        Desbloquear();
        
    }
    function ValidarProductoDetalle() {
        var hdnCompraId = document.getElementById('<%=hdnCompraId.ClientID%>');
        var hdnProductoId = document.getElementById('<%=hdnProductoId.ClientID%>');
        var txtProducto = document.getElementById('<%=txtProductoCodigo.ClientID%>');        
        var txtPrecio = document.getElementById('<%=txtPrecio.ClientID%>');  
        var txtCantidad = document.getElementById('<%=txtCantidad.ClientID%>'); 
        var ddlAlmacen = document.getElementById('<%=ddlAlmacen.ClientID%>');
        
        if (txtProducto.value.trim() == '') {
            return MostrarValidacion(txtProducto, 'Ingrese el codigo producto.');
        }
        if (ddlAlmacen.value.trim() == '0') {
            return MostrarValidacion(ddlAlmacen, 'Seleccione almacén.');
        }
        if (txtPrecio.value.trim() == '' || txtPrecio.value.trim() == '0.00') {
            return MostrarValidacion(txtPrecio, 'Ingrese el precio del producto.');
        }
        if (txtCantidad.value.trim() == '' || txtCantidad.value.trim()=='0') {
            return MostrarValidacion(txtCantidad, 'Ingrese la cantidad de productos.');
        }        
    }
    function CalcularMontoFinal() {
        var lblComprobanteSubTotal = document.getElementById('<%=lblComprobanteSubTotal.ClientID%>');
        var lblComprobanteIgv = document.getElementById('<%=lblComprobanteIgv.ClientID%>');
        var lblComprobanteTotal = document.getElementById('<%=lblComprobanteTotal.ClientID%>');

        var grvItem = document.getElementById('<%=grvVenta.ClientID%>');
        if (grvItem != null) {
            var rows = grvItem.rows;
            if (rows.length > 0) {
                var subTotal = 0, igv = 0, total = 0;
                for (var i = 1; i < rows.length; i++) {
                    subTotal += parseFloat(rows[i].cells[6].innerText.replace(/,/g, '')); 
                    igv += parseFloat(rows[i].cells[7].innerText.replace(/,/g, ''));
                    total += parseFloat(rows[i].cells[8].innerText.replace(/,/g, ''));
                }
                lblComprobanteSubTotal.innerText = FormatoDecimal(subTotal, 2, '');
                lblComprobanteIgv.innerText = FormatoDecimal(igv, 2, '');
                lblComprobanteTotal.innerText = FormatoDecimal(total, 2, '');
            }
        } else {
            lblComprobanteSubTotal.innerText = FormatoDecimal(0, 2, '');
            lblComprobanteIgv.innerText = FormatoDecimal(0, 2, '');
            lblComprobanteTotal.innerText = FormatoDecimal(0, 2, '');
        }
    }
    function CalcularMontos() {
        var txtPrecio = document.getElementById('<%=txtPrecio.ClientID%>');  
        var txtCantidad = document.getElementById('<%=txtCantidad.ClientID%>');

        var precio = txtPrecio.value == '' ? 0 : txtPrecio.value;
        var cantidad = txtCantidad.value == '' ? 0 : txtCantidad.value;

        var total = (parseFloat(precio) * parseFloat(cantidad)).toFixed(2);
        var subtotal = (total / (1 + tasaIgv)).toFixed(2);
        var igv = (total - subtotal).toFixed(2);

        var txtSubTotal = document.getElementById('<%=txtSubTotal.ClientID%>');  
        var txtIgv= document.getElementById('<%=txtIgv.ClientID%>');  
        var txtTotal = document.getElementById('<%=txtTotal.ClientID%>');  

        txtSubTotal.value = FormatoDecimal(subtotal, 2, '');
        txtIgv.value = FormatoDecimal(igv, 2, '');
        txtTotal.value = FormatoDecimal(total, 2, '');
        
    }
    function ObtenerMotivoIngreso() {
        var ddlComprobante = document.getElementById('ddlTipoComprobante').value;
        Bloquear();
        PageMethods.ObtenerMotivoIngreso(ddlComprobante, ObtenerMotivoIngresoOk, fnLlamadaError);
    }
    function ObtenerMotivoIngresoOk(lista) {       
        if (lista.length > 0) {           
            var ddlMotivoIngreso = document.getElementById('ddlMotivoIngreso');
            LlenarCombo(ddlMotivoIngreso, lista, 'Codigo', 'NombreLargo', true);
            AgregarOptionACombo(ddlMotivoIngreso, Seleccione_value, Seleccione);
        } else {
            LimpiarCombo(ddlMotivoIngreso);
            AgregarOptionACombo(ddlMotivoIngreso, Seleccione_value, Seleccione);
        }
        Desbloquear();
        
    }
    function fnLlamadaError(excepcion) {
        alert('Ha ocurrido un error interno: ' + excepcion.get_message());
        Desbloquear();
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
                <td style="text-align: center;" colspan="10">
                    <asp:Label CssClass="lblTitulo" Width="100%" runat="server">REGISTRO COMPRA</asp:Label>
                </td>
            </tr>
            <tr>
                    <td style="width: 100%;">&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 100%;">
                    <table class="border-radius" style="width: 100%;">
                        <tr>
                            <td style="width: 10%;">&nbsp;</td>
                            <td style="width: 10%;">&nbsp;</td>
                            <td style="width: 10%;">&nbsp;</td>
                            <td style="width: 10%;">&nbsp;</td>
                            <td style="width: 10%;">&nbsp;</td>
                            <td style="width: 10%;">&nbsp;</td>
                            <td style="width: 10%;">&nbsp;</td>
                            <td style="width: 10%;">&nbsp;</td>
                            <td style="width: 10%;">&nbsp;</td>
                            <td style="width: 10%;">&nbsp;</td>
                        </tr>
                        
                        <tr>
                            <td class="lblStandar">Tipo Comprob.</td>
                            <td colspan="2">
                                <asp:DropDownList id="ddlTipoComprobante" CssClass="ddlStandar" Width="90%" runat="server" />
                            </td>
                            <td class="lblStandar">Serie</td>
                            <td>
                                <asp:TextBox id="txtSerie" CssClass="txtStandar" Width="90%" runat="server" MaxLength="6"></asp:TextBox>
                            </td>
                            <td class="lblStandar">N&deg; Documento</td>
                            <td>
                                <asp:TextBox id="txtNumeroDocumento" CssClass="txtStandar" Width="90%" runat="server"></asp:TextBox>
                            </td>                            
                            <td class="lblStandar">Fec. Emisi&oacute;n</td>
                            <td>
                                <asp:TextBox id="txtFechaEmision" CssClass="txtStandarFecha" Width="80%" runat="server"></asp:TextBox>
                                <asp:ImageButton ID="imgFechaEmision" CssClass="imgCalendario" ImageUrl="~/Imagenes/Iconos/calendario.png" runat="server"/>
                                <ajaxToolkit:CalendarExtender runat="server" ID="ceFechaNacimiento" BehaviorID="bceFechaNacimiento" CssClass="custom-calendar" TargetControlID="txtFechaEmision" Format="dd/MM/yyyy" PopupButtonID="imgFechaEmision" />
                            </td>  
                            <td class="lblStandar"></td>                          
                        </tr>
                        <tr>
                            <td class="lblStandar">Motivo Ingreso</td>
                            <td colspan="2">
                                <asp:DropDownList id="ddlMotivoIngreso" CssClass="ddlStandar" Width="90%" runat="server" />
                            </td>
                            <td class="lblStandar">Moneda</td>
                            <td colspan="2">
                                <asp:DropDownList id="ddlMoneda" CssClass="ddlStandar" Width="90%" runat="server"/>
                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>                        
                        <tr>
                            <td style="width: 10%;">&nbsp;</td>
                            <td style="width: 10%;">&nbsp;</td>
                            <td style="width: 10%;">&nbsp;</td>
                            <td style="width: 10%;">&nbsp;</td>
                            <td style="width: 10%;">&nbsp;</td>
                            <td style="width: 10%;">&nbsp;</td>
                            <td style="width: 10%;">&nbsp;</td>
                            <td style="width: 10%;">&nbsp;</td>
                            <td style="width: 10%;">&nbsp;</td>
                            <td style="width: 10%;">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="lblStandar">Proveedor</td>
                            <td colspan="3">
                                <asp:TextBox id="txtProveedor" CssClass="txtStandar" Width="90%" runat="server"></asp:TextBox>
                                <img id="imgBuscarCliente" alt="Buscar Proveedor" style="cursor: pointer;" src="../Imagenes/Iconos/buscar.png" onclick="return VerProveedor();" />
                            </td>                                                    
                            <td class="lblStandar">RUC</td>
                            <td>
                                <asp:TextBox id="txtNumeroDocumentoProveedor" CssClass="txtStandar" Width="90%" runat="server" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td class="lblStandar">Direcci&oacute;n</td>
                            <td colspan="3">
                                <asp:TextBox id="txtDireccion" CssClass="txtStandar" Width="90%" runat="server" ReadOnly="true" ></asp:TextBox>
                            </td>
                        </tr>                        
                        <tr>
                            <td class="lblStandar">Glosa</td>
                            <td colspan="9">
                                <asp:TextBox id="txtGlosa" CssClass="txtStandar" Width="97%" runat="server"></asp:TextBox>
                            </td> 
                        </tr>               
                        <tr>
                            <td colspan="10">&nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                    <td style="width: 100%;">&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 100%;">
                    <table class="border-radius" style="width: 100%;">
                        <tr>
                            <td style="width: 10%;">&nbsp;</td>
                            <td style="width: 10%;">&nbsp;</td>
                            <td style="width: 10%;">&nbsp;</td>
                            <td style="width: 10%;">&nbsp;</td>
                            <td style="width: 10%;">&nbsp;</td>
                            <td style="width: 10%;">&nbsp;</td>
                            <td style="width: 10%;">&nbsp;</td>
                            <td style="width: 10%;">&nbsp;</td>
                            <td style="width: 10%;">&nbsp;</td>
                            <td style="width: 10%;">&nbsp;</td>
                        </tr>                      
                        <tr id="trDatosProducto">
                            <td class="lblStandar">Codigo Producto</td>
                            <td>
                                <asp:TextBox id="txtProductoCodigo" CssClass="txtStandar" Width="80%" runat="server"></asp:TextBox>
                                <img id="imgBuscarProducto" alt="Buscar Producto" style="cursor: pointer;" src="../Imagenes/Iconos/buscar.png" onclick="return VerProducto();" />
                            </td>
                            <td class="lblStandar">Producto</td>
                            <td colspan="3">
                                <asp:TextBox id="txtProductoDescripcion" CssClass="txtStandar" Width="98%" runat="server"></asp:TextBox>
                            </td>
                            <td class="lblStandar">Unidad</td>
                            <td>
                                <asp:DropDownList id="ddlUnidad" CssClass="ddlStandar" Width="90%" runat="server" Enabled="false"></asp:DropDownList>
                            </td>
                            <td class="lblStandar">Almacén</td>
                            <td>
                                <asp:DropDownList id="ddlAlmacen" CssClass="ddlStandar" Width="90%" runat="server"/>
                            </td>
                        </tr>
                        <tr id="trMontosProducto">
                            <td class="lblStandar">Precio Compra</td>
                            <td>
                                <asp:TextBox id="txtPrecio" CssClass="txtStandarMonto" Width="90%" runat="server"></asp:TextBox>
                            </td>
                            <td class="lblStandar">Cantidad</td>
                            <td>
                                <asp:TextBox id="txtCantidad" CssClass="txtStandarEntero" Width="90%" runat="server"></asp:TextBox>
                            </td>
                            <td class="lblStandar">Sub Total</td>
                            <td>
                                <asp:TextBox id="txtSubTotal" CssClass="txtStandarMonto" Width="90%" runat="server"></asp:TextBox>
                            </td>
                            <td class="lblStandar">IGV</td>
                            <td>
                                <asp:TextBox id="txtIgv" CssClass="txtStandarMonto" Width="90%" runat="server"></asp:TextBox>
                            </td>
                            <td class="lblStandar">Total</td>
                            <td>
                                <asp:TextBox id="txtTotal" CssClass="txtStandarMonto" Width="90%" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center;" colspan="10">
                                <asp:Label ID="lblDetalleCompra" Width="100%" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr id="trOpcionesItem">
                            <td colspan="2" style="text-align: center;">
                                <asp:Button id="btnAgregarItem" CssClass="btnStandar" Text="Agregar Item" Width="80%" runat="server" OnClientClick="return ValidarProductoDetalle();" OnClick="GuardarCompraDetalle" />
                            </td>
                            <td colspan="2" style="text-align: center;">
                                <asp:Button id="btnCancelar" CssClass="btnStandar" Text="Cancelar" Width="80%" runat="server" OnClientClick="return LimpiarProductoDetalle();" />
                            </td>
                            <td colspan="2" style="text-align: center;">
                                
                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="10">&nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="10" style="width: 100%;">
                                <table id="dummyHeader" style="width: 100%;" class="tblSinBordes">
                                      <thead>
                                        <tr class="filaCabeceraGrid">                                
                                            <th style="width: 5%; cursor: pointer; text-decoration: underline;">Item</th>
                                            <th style="width: 10%; cursor: pointer; text-decoration: underline;">Codigo</th>
                                            <th style="width: 25%; cursor: pointer; text-decoration: underline;">Descripcion</th>
                                            <th style="width: 5%; cursor: pointer; text-decoration: underline;">Unidad</th>
                                            <th style="width: 5%; cursor: pointer; text-decoration: underline;">Cantidad</th>
                                            <th style="width: 10%; cursor: pointer; text-decoration: underline;">Precio Unitario</th>
                                            <th style="width: 10%; cursor: pointer; text-decoration: underline;">SubTotal</th>
                                            <th style="width: 10%; cursor: pointer; text-decoration: underline;">IGV</th>
                                            <th style="width: 10%; cursor: pointer; text-decoration: underline;">Total</th>   
                                            <th style="width: 10%; cursor: default; text-decoration: none;">Acciones</th>
                                            <th>&nbsp;&nbsp;&nbsp;&nbsp;</th>                                   
                                        </tr>
                                      </thead>
                                  </table>
                                <asp:UpdatePanel ID="divData" runat="server" UpdateMode="Conditional" style="overflow-y: scroll; width: 100%; margin: 0 0 0px 0px;" onscroll="Onscrollfnction();">
                                    <ContentTemplate>
                                        <asp:GridView id="grvVenta" AutoGenerateColumns="False" Width="100%" RowStyle-Wrap="True" runat="server" OnRowDataBound="grvVenta_RowDataBound">
                                            <HeaderStyle CssClass="filaCabeceraGrid"></HeaderStyle>
                                            <RowStyle CssClass="filaImparGrid"></RowStyle>
                                            <AlternatingRowStyle CssClass="filaParGrid"></AlternatingRowStyle>
                                            <Columns>                                   
                                                <%--  0: Indice --%>
                                                <asp:TemplateField HeaderText="Nombre">
                                                    <ItemStyle Width="5%" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                          <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%-- 1: # Codigo --%>
                                                <asp:TemplateField HeaderText="# Comprobante">
                                                    <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <%# Eval("Codigo").ToString().Trim() %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%-- 2: Producto --%>
                                                <asp:TemplateField HeaderText="Cliente">
                                                    <ItemStyle Width="25%" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <%# Eval("Producto").ToString().Trim() %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%-- 3: UnidadMedida --%>
                                                <asp:TemplateField HeaderText="Moneda">
                                                    <ItemStyle Width="5%" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                       <%# Eval("UnidadMedida").ToString().Trim() %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%-- 4: Cantidad --%>
                                                <asp:TemplateField HeaderText="Cantidad">
                                                    <ItemStyle Width="5%" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                       <%# Convert.ToDecimal(Eval("Cantidad")).ToString("N2") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%-- 5: PrecioUnitario --%>
                                                <asp:TemplateField HeaderText="PrecioUnitario">
                                                    <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                       <%# Convert.ToDecimal(Eval("PrecioUnitario")).ToString("N2")  %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%-- 6: Monto --%>
                                                <asp:TemplateField HeaderText="Monto">
                                                    <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <%# Convert.ToDecimal(Eval("SubTotal")).ToString("N2")  %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%-- 7: MontoImpuesto --%>
                                                <asp:TemplateField HeaderText="MontoImpuesto">
                                                    <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <%# Convert.ToDecimal(Eval("Igv")).ToString("N2")  %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%-- 8: Monto Total --%>
                                                <asp:TemplateField HeaderText="Monto Total">
                                                    <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <%# Convert.ToDecimal(Eval("Total")).ToString("N2")  %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <%-- 9: ProductoId --%>
                                                <asp:TemplateField HeaderText="ProductoId">
                                                   <ItemStyle CssClass="columnaOcultaGrid" />
                                                    <ItemTemplate>
                                                        <%# Eval("ProductoId")  %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%-- 10: ProductoId --%>
                                                <asp:TemplateField HeaderText="UnidadMedidaId">
                                                   <ItemStyle CssClass="columnaOcultaGrid" />
                                                    <ItemTemplate>
                                                        <%# Eval("UnidadMedidaId")  %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <%-- 11: ProductoId --%>
                                                <asp:TemplateField HeaderText="UnidadMedidaId">
                                                   <ItemStyle CssClass="columnaOcultaGrid" />
                                                    <ItemTemplate>
                                                        <%# Eval("ComprasDetalleId")  %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%-- 0: Acciones --%>
                                                <asp:TemplateField HeaderText="Acciones">
                                                    <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                    <ItemTemplate>                                            
                                                        <asp:LinkButton ID="linkEliminar" runat="server" CommandArgument='<%# Eval("ComprasDetalleId")%>' Visible='<%# (Convert.ToInt32(hdnEstadoComprobanteId.Value) == APU.Herramientas.Constantes.EstadoComprobanteCompraAnulado)  ? false : true %>'
                                                                                OnClientClick="return confirm('¿Está seguro que quiere eliminar el item seleccionado?');" OnClick="EliminarItem" >
                                                                                <asp:Image ID="imgEliminar" ImageUrl="~/Imagenes/Iconos/borrar.png" runat="server"/>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>                                                                                   
                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>
                                    <Triggers>                            
                                        <asp:AsyncPostBackTrigger ControlID="btnPaginacion" EventName="Click" />      
                                        <asp:AsyncPostBackTrigger ControlID="btnAgregarItem" EventName="Click" />                             
                                    </Triggers>
                                </asp:UpdatePanel>
                                <div id="divFooter" style="width: 1182px; overflow-x: hidden; overflow-y: hidden; margin: 0 0px 0 0px;" >
                                    <table style="display:none">
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
                </td>
            </tr>
            <tr>
                    <td style="width: 100%;">&nbsp;</td>
            </tr>
            <tr>
                 <td style="width: 100%;">
                        <div style="width: 30%; float: right;">
                            <table class="border-radius" style="width: 100%; text-align: right;">
                                <tr>
                                    <td class="filaCabeceraGrid" style="width: 40%; border: 1px solid #000000;">Sub Total</td>
                                    <td style="width: 60%; border: 1px solid #000000;">
                                        <asp:Label id="lblComprobanteSubTotal" CssClass="lblStandarMonto" Text="0.00" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="filaCabeceraGrid" style="width: 40%; border: 1px solid #000000;">IGV</td>
                                    <td style="width: 60%; border: 1px solid #000000;">
                                        <asp:Label id="lblComprobanteIgv" CssClass="lblStandarMonto" Text="0.00" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="filaCabeceraGrid" style="width: 40%; border: 1px solid #000000;">Total</td>
                                    <td style="width: 60%; border: 1px solid #000000;">
                                        <asp:Label id="lblComprobanteTotal" CssClass="lblStandarMonto" Text="0.00" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="width: 100%; text-align: center;">
                                        <%--<asp:Button id="btnGenerarComprobante" CssClass="btnStandar" Text="Generar Factura" OnClientClick="return ValidarComprobante();" Width="80%" runat="server" OnClick="btnGenerarComprobante_Click"/>--%>
                                        <%--<asp:Button id="btnGuardar" CssClass="btnStandar" Text="Guardar Comprobante" Width="80%" runat="server" OnClientClick="return ValidarCompra();" OnClick="btnGuardarCompra_OnClick" /> --%>
                                        <input type="button" id="btnGuardar" value="Guardar Comprobante" class="btnStandar" style="width: 80%;" onclick="return GenerarComprobante();"/>
                                        <input type="button" id="btnAnular" value="Anular Comprobante" class="btnStandar" style="width: 80%;display:none;" onclick="return AnularComprobante();"/>
                                        <input type="button" id="btnCerrar" value="Cerrar" class="btnStandar" style="width: 80%;display:none;" onclick="return VerBandeja();"/>
                                    </td>
                                </tr>
                               
                            </table>
                        </div>
                        <br/>
                    </td>
            </tr>    
            
        </table>
    </div>
    <div id="derecha"></div>
        <asp:Panel ID="pnlCliente" runat="server" CssClass="pnlModal" DefaultButton="btnBuscarCliente" style="width: 800px; display: none;">
                <table style="width: 100%;">
                    <tr>
                        <td id="lblTituloCliente" class="lblTituloPopup" colspan="4">BUSCAR PROVEEDOR</td>
                    </tr>                                       
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>                 
                    <tr id="trPersonaJuridica">
                        <td class="lblStandar">RUC</td>
                        <td>
                            <asp:TextBox id="txtRuc" CssClass="txtStandar" runat="server"></asp:TextBox>
                        </td>
                        <td class="lblStandar">Raz&oacute;n Social</td>
                        <td>
                            <asp:TextBox id="txtRazonSocialCliente" CssClass="txtStandar" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <table style="width: 100%;" class="tblSinBordes">
                                <thead>
                                    <tr class="filaCabeceraGrid">
                                        <th style="width: 20%; cursor: default; text-decoration: none;">Acci&oacute;n</th>
                                        <th style="width: 30%; cursor: default; text-decoration: underline;">Ruc</th>
                                        <th style="width: 40%; cursor: pointer; text-decoration: underline;">Proveedor</th>                                       
                                        <th>&nbsp;&nbsp;&nbsp;&nbsp;</th>
                                    </tr>
                                </thead>
                            </table>
                            <asp:UpdatePanel ID="upCliente" runat="server" UpdateMode="Conditional" style="overflow-y: scroll; height: 200px; margin: 0 0 0px 0px;" onscroll="Onscrollfnction();">
                                <ContentTemplate>
                                    <asp:GridView id="grvCliente" AutoGenerateColumns="False" Width="100%" RowStyle-Wrap="True" runat="server">
                                        <HeaderStyle CssClass="filaCabeceraGrid"></HeaderStyle>
                                        <RowStyle CssClass="filaImparGrid"></RowStyle>
                                        <AlternatingRowStyle CssClass="filaParGrid"></AlternatingRowStyle>
                                        <Columns>
                                            <%-- 0: Acciones --%>
                                            <asp:TemplateField HeaderText="Acciones">
                                                <ItemStyle Width="20%" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:ImageButton runat="server" ImageUrl="~/Imagenes/Iconos/seleccionar.png" CssClass="imgButton" OnClientClick='<%# "return SeleccionarCliente(" + Eval("ProveedorId") + ",\"" + Eval("Nombre").ToString() + "\"," + Eval("TipoDocumentoId") + ",\"" + Eval("NumeroDocumento") + "\",\"" + Eval("Direccion") + "\",\"" + Eval("Correo") + "\");" %>' ToolTip="Seleccionar" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--  1: Nombre --%>
                                            <asp:TemplateField HeaderText="Ruc">
                                                <ItemStyle Width="30%" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <%# Eval("NumeroDocumento").ToString().Trim() %>                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- 2: # Documento --%>
                                            <asp:TemplateField HeaderText="Proveedor">
                                                <ItemStyle Width="40%" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <%# Eval("Nombre").ToString().Trim() %>
                                                </ItemTemplate>
                                            </asp:TemplateField>                                          
                                           <asp:TemplateField>
                                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                   
                                                </ItemTemplate>
                                            </asp:TemplateField>   
                                        </Columns>
                                    </asp:GridView>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnBuscarCliente" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">&nbsp;
                            <input type="hidden" id="hdnClienteId" value="0" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center;">
                            <asp:Button ID="btnBuscarCliente" runat="server" CssClass="btnStandar" Text="Buscar" Width="30%" OnClick="btnBuscarProveedor_Click" />
                            <asp:Button ID="btnCancelarCliente" runat="server" CssClass="btnStandar" Text="Cancelar" Width="30%" />
                            <asp:button id="hButton1" runat="server" style="display:none;" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        <ajaxToolkit:ModalPopupExtender ID="mpeCliente" PopupControlID="pnlCliente" TargetControlID="hButton1" CancelControlID="btnCancelarCliente" Drag="True" DropShadow="True" BackgroundCssClass="FondoAplicacion" ClientIDMode="Static" runat="server" PopupDragHandleControlID="lblTituloCliente"></ajaxToolkit:ModalPopupExtender>
        <ajaxToolkit:RoundedCornersExtender ID="rceCliente" runat="server" BehaviorID="rcbCliente" TargetControlID="pnlCliente" Radius="6" Corners="All" />
        <asp:Panel ID="pnlProducto" runat="server" CssClass="pnlModal" DefaultButton="btnBuscarProducto" style="width: 800px; display: none;">
                <table style="width: 100%;">
                    <tr>
                        <td id="lblTituloProducto" class="lblTituloPopup" colspan="4">BUSCAR PRODUCTO</td>
                    </tr>                                       
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>                 
                    <tr>
                        <td class="lblStandar">Codigo</td>
                        <td>
                            <asp:TextBox id="txtCodigoBuscar" CssClass="txtStandar" runat="server"></asp:TextBox>
                        </td>
                        <td class="lblStandar">Producto</td>
                        <td>
                            <asp:TextBox id="txtProductoBuscar" CssClass="txtStandar" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <table style="width: 100%;" class="tblSinBordes">
                                <thead>
                                    <tr class="filaCabeceraGrid">
                                        <th style="width: 10%; cursor: default; text-decoration: none;">Acci&oacute;n</th>
                                        <th style="width: 20%; cursor: default; text-decoration: underline;">Codigo</th>
                                        <th style="width: 40%; cursor: pointer; text-decoration: underline;">Producto</th>
                                        <th style="width: 10%; cursor: pointer; text-decoration: underline;">Medida</th>      
                                        <th style="width: 19%; cursor: pointer; text-decoration: underline;">Stock</th>                                           
                                        <th style="width:1%;">&nbsp;&nbsp;&nbsp;&nbsp;</th>
                                    </tr>
                                </thead>
                            </table>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" style="overflow-y: scroll; height: 200px; margin: 0 0 0px 0px;" onscroll="Onscrollfnction();">
                                <ContentTemplate>
                                    <asp:GridView id="grvProducto" AutoGenerateColumns="False" Width="100%" RowStyle-Wrap="True" runat="server">
                                        <HeaderStyle CssClass="filaCabeceraGrid"></HeaderStyle>
                                        <RowStyle CssClass="filaImparGrid"></RowStyle>
                                        <AlternatingRowStyle CssClass="filaParGrid"></AlternatingRowStyle>
                                        <Columns>
                                            <%-- 0: Acciones --%>
                                            <asp:TemplateField HeaderText="Acciones">
                                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:ImageButton runat="server" ImageUrl="~/Imagenes/Iconos/seleccionar.png" CssClass="imgButton" 
                                                        OnClientClick='<%# "return SeleccionarProducto(" + Eval("ProductoId") + ",\"" + Eval("Codigo").ToString() + "\",\"" + Eval("Producto") + "\",\"" + Eval("UnidadMedidaId")+ "\",\"" + Eval("PrecioCompra") + "\");" %>' ToolTip="Seleccionar" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--  1: Nombre --%>
                                            <asp:TemplateField HeaderText="Ruc">
                                                <ItemStyle Width="20%" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <%# Eval("Codigo").ToString().Trim() %>                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- 2: # Documento --%>
                                            <asp:TemplateField HeaderText="Producto">
                                                <ItemStyle Width="40%" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <%# Eval("Producto").ToString().Trim() %>
                                                </ItemTemplate>
                                            </asp:TemplateField>   
                                             <asp:TemplateField HeaderText="UnidadMedida">
                                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <%# Eval("UnidadMedida").ToString().Trim() %>
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                             <asp:TemplateField HeaderText="PrecioCompra">
                                                <ItemStyle Width="19%" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <%# Eval("StockActual").ToString() %>
                                                </ItemTemplate>
                                            </asp:TemplateField>   
                                        </Columns>
                                    </asp:GridView>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnBuscarProducto" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">&nbsp;
                           
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center;">
                            <asp:Button ID="btnBuscarProducto" runat="server" CssClass="btnStandar" Text="Buscar" Width="30%" OnClick="btnBuscarProducto_Click"  />
                            <asp:Button ID="btnCancelarProducto" runat="server" CssClass="btnStandar" Text="Cancelar" Width="30%" />
                            <asp:button id="hButton2" runat="server" style="display:none;" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        <ajaxToolkit:ModalPopupExtender ID="mpeProducto" PopupControlID="pnlProducto" TargetControlID="hButton2" CancelControlID="btnCancelarProducto" Drag="True" DropShadow="True" BackgroundCssClass="FondoAplicacion" ClientIDMode="Static" runat="server" PopupDragHandleControlID="lblTituloProducto"></ajaxToolkit:ModalPopupExtender>
        <ajaxToolkit:RoundedCornersExtender ID="rceProducto" runat="server" BehaviorID="rcbProducto" TargetControlID="pnlProducto" Radius="6" Corners="All" />

        <input type="hidden" id="hdnVentaId" value="0" runat="server" />
        <input type="hidden" id="hdnProveedorId" value="0" runat="server" />
        <input type="hidden" id="hdnProductoId" value="0" runat="server" />
        <input type="hidden" id="hdnCompraId" value="0" runat="server" />
        <input type="hidden" id="hdnCompraDetalleid" value="0" runat="server" />
        <input type="hidden" id="hdnEstadoComprobanteId" value="0" runat="server" />
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
    function grvVentas_OnDnlClick(row) {
        return false;
        var indiceCodigo = 1;
        var indiceProducto = 2;
        var indiceCantidad = 4;
        var indicePrecioUnitario = 5;
        var indiceSubTotal = 6;
        var indiceIgv = 7;
        var indiceTotal = 8;
        var indiceProductoId = 9;
        var indiceUnidadMedidaId = 10;
        var indiceCompraDetalleId = 11;
        
        var codigoProductoRow = RetornarCeldaValor(row, indiceCodigo);
        var productoRow = RetornarCeldaValor(row, indiceProducto);
        var cantidadRow = RetornarCeldaValor(row, indiceCantidad);
        var precioRow = RetornarCeldaValor(row, indicePrecioUnitario);
        var productoIdRow = RetornarCeldaValor(row, indiceProductoId);
        var unidadMedidaIdRow = RetornarCeldaValor(row, indiceUnidadMedidaId);
        var subTotalRow = RetornarCeldaValor(row, indiceSubTotal);
        var subIgvRow = RetornarCeldaValor(row, indiceIgv);
        var totalRow = RetornarCeldaValor(row, indiceTotal);
        var compraDetalleIdRow = RetornarCeldaValor(row, indiceCompraDetalleId);

        var hdnProductoId = document.getElementById('<%=hdnProductoId.ClientID%>');
        var hdnCompraDetalleid = document.getElementById('<%=hdnCompraDetalleid.ClientID%>');
        var txtProductoCodigo = document.getElementById('<%=txtProductoCodigo.ClientID%>'); 
        var txtProducto = document.getElementById('<%=txtProductoDescripcion.ClientID%>'); 
        var txtPrecio = document.getElementById('<%=txtPrecio.ClientID%>');  
        var txtCantidad = document.getElementById('<%=txtCantidad.ClientID%>');
        var ddlUnidadMedida = document.getElementById('<%=ddlUnidad.ClientID%>');

        var txtSubTotal = document.getElementById('<%=txtSubTotal.ClientID%>');
        var txtIgv = document.getElementById('<%=txtIgv.ClientID%>');
        var txtTotal= document.getElementById('<%=txtTotal.ClientID%>');

        hdnProductoId.value = productoIdRow;
        txtProductoCodigo.value = codigoProductoRow;
        txtProducto.value = productoRow;
        txtCantidad.value = cantidadRow;
        txtPrecio.value = precioRow;
        ddlUnidadMedida.value = unidadMedidaIdRow;

        txtSubTotal.value = subTotalRow;
        txtIgv.value = subIgvRow;
        txtTotal.value = totalRow;
        hdnCompraDetalleid.value = compraDetalleIdRow;

        //VerVenta(ventaIdRow);
    }
</script>
</body>
</html>