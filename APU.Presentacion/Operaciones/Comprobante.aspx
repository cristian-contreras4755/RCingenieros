<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Comprobante.aspx.cs" Inherits="APU.Presentacion.Operaciones.Comprobante" EnableEventValidation="false" %>
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
            border: 1px none #000000;
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
    function LimpiarVenta() {
        <%--var ddlEmpresa = document.getElementById('<%=ddlEmpresa.ClientID%>');
        var txtCodigo = document.getElementById('<%=txtCodigo.ClientID%>');
        var txtNombre = document.getElementById('<%=txtNombre.ClientID%>');
        var txtDescripcion = document.getElementById('<%=txtDescripcion.ClientID%>');--%>
        <%--var txtDireccion = document.getElementById('<%=txtDireccion.ClientID%>');--%>

        hdnVentaId.value = 0;
        //ddlEmpresa.value = 0;
        //txtNombre.value = '';
        //txtCodigo.value = '';
        //txtDescripcion.value = '';
        //chkActivo.checked = true;
        txtDireccion.value = '';
        //lblModificadoPor.innerText = '';
        //lblFechaModificacion.innerText = '';

        <%--var tabVenta = $get('<%=tabVenta.ClientID%>');
        tabVenta.control.set_activeTabIndex(0);--%>
    }
    function ObtenerVentaOk(v) {
        var modalDialog = $find('mpeVenta');
        if (v != null) {
            modalDialog.show();

            <%--var tabVenta = $get('<%=tabVenta.ClientID%>');
            tabVenta.control.set_activeTabIndex(0);--%>

            <%--var ddlEmpresa = document.getElementById('<%=ddlEmpresa.ClientID%>');
            var txtCodigo = document.getElementById('<%=txtCodigo.ClientID%>');
            var txtNombre = document.getElementById('<%=txtNombre.ClientID%>');
            var txtDescripcion = document.getElementById('<%=txtDescripcion.ClientID%>');--%>
            <%--var txtDireccion = document.getElementById('<%=txtDireccion.ClientID%>');--%>

            //hdnVentaId.value = v.VentaId;
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
        <%--var hdnVentaId = document.getElementById('<%=hdnVentaId.ClientID%>');
        var chkActivo = document.getElementById('<%=chkActivo.ClientID%>');--%>

        //if (ddlEmpresa.value.trim() == '0') {
        //    return MostrarValidacion(ddlEmpresa, 'Seleccione la Empresa.');
        //}
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
    function VerCliente() {
        var mdCliente = $find('mpeCliente');
        mdCliente.show();
        var txtNumeroDocumentoPersonaNatural = document.getElementById('<%=txtNumeroDocumentoPersonaNatural.ClientID%>');
        txtNumeroDocumentoPersonaNatural.focus();
    }
    function VerProducto() {
        var mdProducto = $find('mpeProducto');
        mdProducto.show();
        var txtCodigoBuscar = document.getElementById('<%=txtCodigoBuscar.ClientID%>');
        txtCodigoBuscar.focus();
    }
    function SeleccionarTipoPersona() {
        var rbtPersonaNatural = document.getElementById('<%=rbtPersonaNatural.ClientID%>');
        var ddlTipoDocumentoPersonaNatural = document.getElementById('<%=ddlTipoDocumentoPersonaNatural.ClientID%>');
        var txtNumeroDocumentoPersonaNatural = document.getElementById('<%=txtNumeroDocumentoPersonaNatural.ClientID%>');
        var txtNombresCliente = document.getElementById('<%=txtNombresCliente.ClientID%>');
        var txtApellidoPaternoCliente = document.getElementById('<%=txtApellidoPaternoCliente.ClientID%>');
        var txtApellidoMaternoCliente = document.getElementById('<%=txtApellidoMaternoCliente.ClientID%>');
        var rbtPersonaJuridica = document.getElementById('<%=rbtPersonaJuridica.ClientID%>');
        var txtRuc = document.getElementById('<%=txtRuc.ClientID%>');
        var txtRazonSocialCliente = document.getElementById('<%=txtRazonSocialCliente.ClientID%>');
        if (rbtPersonaNatural.checked) {
            document.getElementById('trPersonaNatural1').style.display = '';
            document.getElementById('trPersonaNatural2').style.display = '';
            document.getElementById('trPersonaNatural3').style.display = '';
            document.getElementById('trPersonaJuridica').style.display = 'none';
            ddlTipoDocumentoPersonaNatural.value = 1;
            txtNumeroDocumentoPersonaNatural.value = '';
            txtNombresCliente.value = '';
            txtApellidoPaternoCliente.value = '';
            txtApellidoMaternoCliente.value = '';
            txtNumeroDocumentoPersonaNatural.focus();
        }
        if (rbtPersonaJuridica.checked) {
            document.getElementById('trPersonaNatural1').style.display = 'none';
            document.getElementById('trPersonaNatural2').style.display = 'none';
            document.getElementById('trPersonaNatural3').style.display = 'none';
            document.getElementById('trPersonaJuridica').style.display = '';
            txtRuc.value = '';
            txtRuc.focus();
            txtRazonSocialCliente.value = '';
        }
    }
    function SeleccionarCliente(clienteId, nombres, apellidoPaterno, apellidoMaterno, tipoDocumentoId, numeroDocumento, direccion, email, tipoPersonaId, razonSocial) {
        // SeleccionarCliente(" + Eval("ClienteId") + ", " + Eval("Nombres") + ", " + Eval("ApellidoPaterno") + ", " + Eval("ApellidoMaterno") + ", " + Eval("TipoDocumentoId") + ", " + Eval("NumeroDocumento") + ", " + Eval("Direccion") + ", " + Eval("Email") + ");"
        var mdCliente = $find('mpeCliente');
        var hdnClienteId = document.getElementById('<%=hdnClienteId.ClientID%>');
        var txtCliente = document.getElementById('<%=txtCliente.ClientID%>');
        var ddlTipoDocumento = document.getElementById('<%=ddlTipoDocumento.ClientID%>');
        var txtNumeroDocumento = document.getElementById('<%=txtNumeroDocumento.ClientID%>');
        var txtDireccion = document.getElementById('<%=txtDireccion.ClientID%>');
        var txtEmail = document.getElementById('<%=txtEmail.ClientID%>');
        var txtGlosa = document.getElementById('<%=txtGlosa.ClientID%>');

        hdnClienteId.value = clienteId;
        if (tipoPersonaId == 1) {
            txtCliente.value = nombres + ', ' + apellidoPaterno + ' ' + apellidoMaterno;
        }
        if (tipoPersonaId == 2) {
            txtCliente.value = razonSocial;
        }
        
        ddlTipoDocumento.value = tipoDocumentoId;
        txtNumeroDocumento.value = numeroDocumento;
        txtDireccion.value = direccion;
        txtEmail.value = email;

        mdCliente.hide();
        LimpiarCliente();
        txtGlosa.focus();
        return false;
    }
    function LimpiarCliente() {
        var rbtPersonaNatural = document.getElementById('<%=rbtPersonaNatural.ClientID%>');
        var rbtPersonaJuridica = document.getElementById('<%=rbtPersonaJuridica.ClientID%>');
        var ddlTipoDocumentoPersonaNatural = document.getElementById('<%=ddlTipoDocumentoPersonaNatural.ClientID%>');
        var txtNumeroDocumentoPersonaNatural = document.getElementById('<%=txtNumeroDocumentoPersonaNatural.ClientID%>');
        var txtNombresCliente = document.getElementById('<%=txtNombresCliente.ClientID%>');
        var txtApellidoPaternoCliente = document.getElementById('<%=txtApellidoPaternoCliente.ClientID%>');
        var txtApellidoMaternoCliente = document.getElementById('<%=txtApellidoMaternoCliente.ClientID%>');
        var txtRuc = document.getElementById('<%=txtRuc.ClientID%>');
        var txtRazonSocialCliente = document.getElementById('<%=txtRazonSocialCliente.ClientID%>');

        rbtPersonaNatural.checked = true;
        document.getElementById('trPersonaNatural1').style.display = '';
        document.getElementById('trPersonaNatural2').style.display = '';
        document.getElementById('trPersonaNatural3').style.display = '';
        document.getElementById('trPersonaJuridica').style.display = 'none';
        ddlTipoDocumentoPersonaNatural.value = 1;
        txtNumeroDocumentoPersonaNatural.value = '';
        txtNombresCliente.value = '';
        txtApellidoPaternoCliente.value = '';
        txtApellidoMaternoCliente.value = '';

        rbtPersonaJuridica.checked = false;
        ddlTipoDocumentoPersonaNatural.value = 0;
        txtNumeroDocumentoPersonaNatural.value = '';
        txtNombresCliente.value = '';
        txtApellidoPaternoCliente.value = '';
        txtApellidoPaternoCliente.value = '';
        txtApellidoMaternoCliente.value = '';
        txtRuc.value = '';
        txtRazonSocialCliente.value = '';
    }
    function SeleccionarProducto(productoId, producto, codigo, precio, unidadMedidaId) {
        var mdProducto = $find('mpeProducto');
        var hdnProductoId = document.getElementById('<%=hdnProductoId.ClientID%>');
        var hdnProducto = document.getElementById('<%=hdnProducto.ClientID%>');
        var txtProducto = document.getElementById('<%=txtProductoCodigo.ClientID%>');
        var txtPrecio = document.getElementById('<%=txtPrecio.ClientID%>');
        var txtCantidad = document.getElementById('<%=txtCantidad.ClientID%>');
        var txtProductoDescripcion = document.getElementById('<%=txtProductoDescripcion.ClientID%>');
        var ddlUnidad = document.getElementById('<%=ddlUnidad.ClientID%>');

        hdnProductoId.value = productoId;
        hdnProducto.value = producto;
        txtProducto.value = codigo;
        txtPrecio.value = precio;
        ddlUnidad.value = unidadMedidaId;
        txtProductoDescripcion.value = producto;

        mdProducto.hide();
        LimpiarProductoBuscar();
        txtCantidad.focus();
        return false;
    }
    function LimpiarProductoBuscar() {
        var txtCodigoBuscar = document.getElementById('<%=txtCodigoBuscar.ClientID%>');
        var txtProductoBuscar = document.getElementById('<%=txtProductoBuscar.ClientID%>');

        txtCodigoBuscar.value = '';
        txtProductoBuscar.value = '';
    }
    function CalcularOperacionCantidadPrecio() {
        var txtPrecio = document.getElementById('<%=txtPrecio.ClientID%>');
        var txtCantidad = document.getElementById('<%=txtCantidad.ClientID%>');
        var txtSubTotal = document.getElementById('<%=txtSubTotal.ClientID%>');
        var txtIgv = document.getElementById('<%=txtIgv.ClientID%>');
        var txtTotal = document.getElementById('<%=txtTotal.ClientID%>');

        var precio = (txtPrecio.value == '') ? 0 : parseFloat(txtPrecio.value);
        var cantidad = (txtCantidad.value == '') ? 0 : parseFloat(txtCantidad.value);

        var subTotal = precio * cantidad;
        txtSubTotal.value = FormatoDecimal(subTotal, 2, '');
        var igv = subTotal * PorcentajeIgv;
        txtIgv.value = FormatoDecimal(igv, 2, '');
        var montoTotal = subTotal + igv;
        txtTotal.value = FormatoDecimal(montoTotal, 2, '');
    }
    function CalcularOperacionSubTotal() {
        var txtPrecio = document.getElementById('<%=txtPrecio.ClientID%>');
        var txtCantidad = document.getElementById('<%=txtCantidad.ClientID%>');
        var txtSubTotal = document.getElementById('<%=txtSubTotal.ClientID%>');
        var txtIgv = document.getElementById('<%=txtIgv.ClientID%>');
        var txtTotal = document.getElementById('<%=txtTotal.ClientID%>');

        var precio = (txtPrecio.value == '') ? 0 : parseFloat(txtPrecio.value);
        var cantidad = (txtCantidad.value == '') ? 0 : parseFloat(txtCantidad.value);
        var subTotal = (txtSubTotal.value == '') ? 0 : parseFloat(txtSubTotal.value);

        if (precio > 0) {
            cantidad = subTotal / precio;
            txtCantidad.value = FormatoDecimal(cantidad, 2, '');
        }

        //var subTotal = precio * cantidad;
        //txtSubTotal.value = FormatoDecimal(subTotal, 2, '');
        var igv = subTotal * PorcentajeIgv;
        txtIgv.value = FormatoDecimal(igv, 2, '');
        var montoTotal = subTotal + igv;
        txtTotal.value = FormatoDecimal(montoTotal, 2, '');
    }
    function ValidarGuardar() {
        var hdnProductoId = document.getElementById('<%=hdnProductoId.ClientID%>');
        var txtProductoCodigo = document.getElementById('<%=txtProductoCodigo.ClientID%>');
        var imgVerProducto = document.getElementById('imgVerProducto');
        var ddlUnidad = document.getElementById('<%=ddlUnidad.ClientID%>');
        var txtPrecio = document.getElementById('<%=txtPrecio.ClientID%>');
        var txtCantidad = document.getElementById('<%=txtCantidad.ClientID%>');
        var txtSubTotal = document.getElementById('<%=txtSubTotal.ClientID%>');
        var txtIgv = document.getElementById('<%=txtIgv.ClientID%>');
        var txtTotal = document.getElementById('<%=txtTotal.ClientID%>');

        if (txtProductoCodigo.value.trim() == '') {
            return MostrarValidacion(txtProductoCodigo, 'Seleccione un Producto.');
        }
        if (ddlUnidad.value.trim() == '0') {
            return MostrarValidacion(ddlUnidad, 'Seleccione una Unidad.');
        }
        if (txtCantidad.value.trim() == '') {
            return MostrarValidacion(txtCantidad, 'Ingrese la Cantidad.');
        }
    }
    function ValidarComprobante() {
        var txtCliente = document.getElementById('<%=txtCliente.ClientID%>');
        var ddlTipoDocumento = document.getElementById('<%=ddlTipoDocumento.ClientID%>');
        var txtNumeroDocumento = document.getElementById('<%=txtNumeroDocumento.ClientID%>');
        var txtDireccion = document.getElementById('<%=txtDireccion.ClientID%>');
        var txtEmail = document.getElementById('<%=txtEmail.ClientID%>');

        var ddlTipoComprobante = document.getElementById('<%=ddlTipoComprobante.ClientID%>');
        var ddlSerie = document.getElementById('<%=ddlSerie.ClientID%>');
        var txtNumeroComprobante = document.getElementById('<%=txtNumeroComprobante.ClientID%>');
        var ddlMoneda = document.getElementById('<%=ddlMoneda.ClientID%>');
        var txtFechaEmision = document.getElementById('<%=txtFechaEmision.ClientID%>');
        var txtDescuento = document.getElementById('<%=txtFechaEmision.ClientID%>');

        var txtProductoCodigo = document.getElementById('<%=txtProductoCodigo.ClientID%>');

        if (ddlTipoComprobante.value.trim() == '0') {
            return MostrarValidacion(ddlTipoComprobante, 'Seleccione el Tipo de Comprobante.');
        }
        if (ddlSerie.value.trim() == '0') {
            return MostrarValidacion(ddlSerie, 'Seleccione la Serie.');
        }
        if (txtNumeroComprobante.value.trim() == '') {
            return MostrarValidacion(txtNumeroComprobante, 'Ingrese el número de comprobante.');
        }
        if (ddlMoneda.value.trim() == '0') {
            return MostrarValidacion(ddlSerie, 'Seleccione la Moneda.');
        }
        if (txtFechaEmision.value.trim() == '') {
            return MostrarValidacion(txtFechaEmision, 'Ingrese la Fecha de Emisión.');
        }
        if (txtCliente.value.trim() == '') {
            return MostrarValidacion(txtCliente, 'Seleccione al cliente.');
        }
        if (ddlTipoDocumento.value.trim() == '0') {
            return MostrarValidacion(ddlTipoDocumento, 'Seleccione el Tipo de Documento del Cliente.');
        }
        if (txtNumeroDocumento.value.trim() == '') {
            return MostrarValidacion(txtNumeroDocumento, 'Ingrese el Número de Documento del Cliente.');
        }
        if (txtDireccion.value.trim() == '') {
            return MostrarValidacion(txtDireccion, 'Ingrese la Dirección del Cliente.');
        }
        if (txtEmail.value.trim() == '') {
            return MostrarValidacion(txtEmail, 'Ingrese el Email del Cliente.');
        }
        var grvItem = document.getElementById('<%=grvItem.ClientID%>');
        if (grvItem != null) {
            var rows = grvItem.rows;
            if (rows.length == 0) {
                //return ValidarGuardar();
                MostrarMensaje('Debe ingresar el Detalle de la Venta.');
                return false;
            }
        } else {
            MostrarMensaje('Debe ingresar el Detalle de la Venta.');
            return false;
        }
        return true;
    }
    function CalcularMontoFinal() {
        var lblComprobanteSubTotal = document.getElementById('<%=lblComprobanteSubTotal.ClientID%>');
        var lblComprobanteIgv = document.getElementById('<%=lblComprobanteIgv.ClientID%>');
        var lblComprobanteTotal = document.getElementById('<%=lblComprobanteTotal.ClientID%>');

        var grvItem = document.getElementById('<%=grvItem.ClientID%>');
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
        }
    }
    function LimpiarItem() {
        var hdnProductoId = document.getElementById('<%=hdnProductoId.ClientID%>');
        var txtProductoCodigo = document.getElementById('<%=txtProductoCodigo.ClientID%>');
        var txtProductoDescripcion = document.getElementById('<%=txtProductoDescripcion.ClientID%>');
        var ddlUnidad = document.getElementById('<%=ddlUnidad.ClientID%>');
        var txtPrecio = document.getElementById('<%=txtPrecio.ClientID%>');
        var txtCantidad = document.getElementById('<%=txtCantidad.ClientID%>');
        var txtSubTotal = document.getElementById('<%=txtSubTotal.ClientID%>');
        var txtIgv = document.getElementById('<%=txtIgv.ClientID%>');
        var txtTotal = document.getElementById('<%=txtTotal.ClientID%>');

        hdnProductoId.value = 0;
        txtProductoCodigo.value = '';
        txtProductoDescripcion.value = '';
        ddlUnidad.value = 0;
        txtPrecio.value = '';
        txtCantidad.value = '';
        txtSubTotal.value = '';
        txtIgv.value = '';
        txtTotal.value = '';
        return false;
    }
    function EliminarItem() {

    }
    function Cancelar() {
        return LimpiarItem();
    }
    function ObtenerCorrelativo() {
        var ddlTipoComprobante = document.getElementById('<%=ddlTipoComprobante.ClientID%>');
        var ddlSerie = document.getElementById('<%=ddlSerie.ClientID%>');
        <%--var btnGenerarComprobante = document.getElementById('<%=btnGenerarComprobante.ClientID%>');--%>
        var btnGenerarComprobante = document.getElementById('btnGenerarComprobante');
        <%--var btnImprimirComprobante = document.getElementById('<%=btnImprimirComprobante.ClientID%>');--%>
        var btnImprimirComprobante = document.getElementById('btnImprimirComprobante');

        var tipoComprobanteId = ddlTipoComprobante.value;
        var serieId = ddlSerie.value;

        switch (tipoComprobanteId) {
            case TipoComprobanteFactura:
                btnGenerarComprobante.innerText = 'Generar Factura';
                btnGenerarComprobante.value = 'Generar Factura';
                btnImprimirComprobante.innerText = 'Imprimir Factura';
                btnImprimirComprobante.value = 'Imprimir Factura';
                break;
            case TipoComprobanteBoletaVenta:
                btnGenerarComprobante.innerText = 'Generar Boleta';
                btnGenerarComprobante.value = 'Generar Boleta';
                btnImprimirComprobante.innerText = 'Imprimir Boleta';
                btnImprimirComprobante.value = 'Imprimir Boleta';
                break;
            default:
                btnGenerarComprobante.innerText = 'Generar Comprobante';
                btnGenerarComprobante.value = 'Generar Comprobante';
                btnImprimirComprobante.innerText = 'Imprimir Comprobante';
                btnImprimirComprobante.value = 'Imprimir Comprobante';
                break;
        }

        if (tipoComprobanteId > 0 && serieId > 0) {
            Bloquear();
            PageMethods.ObtenerCorrelativo(tipoComprobanteId, serieId, ObtenerCorrelativoOk, fnLlamadaError);
        }
        return false;
    }
    function ObtenerCorrelativoOk(c) {
        var txtNumeroComprobante = document.getElementById('<%=txtNumeroComprobante.ClientID%>');
        if (c.length > 0) {
            var r = c[0];
            txtNumeroComprobante.value = r['Actual'];
        } else {
            MostrarMensaje('No se encontró un correlativo para la serie seleccionada');
        }
        Desbloquear();
    }
    function GenerarComprobante() {
        var validaComprobante = ValidarComprobante();

        if (validaComprobante) {
            var ddlTipoComprobante = document.getElementById('<%=ddlTipoComprobante.ClientID%>');
            var ddlSerie = document.getElementById('<%=ddlSerie.ClientID%>');
            var txtNumeroComprobante = document.getElementById('<%=txtNumeroComprobante.ClientID%>');
            var ddlMoneda = document.getElementById('<%=ddlMoneda.ClientID%>');
            var txtFechaEmision = document.getElementById('<%=txtFechaEmision.ClientID%>');
            var txtDescuento = document.getElementById('<%=txtFechaEmision.ClientID%>');
            var txtNumeroPlaca = document.getElementById('<%=txtNumeroPlaca.ClientID%>');

            var hdnClienteId = document.getElementById('<%=hdnClienteId.ClientID%>');
            var txtGlosa = document.getElementById('<%=txtGlosa.ClientID%>');

            var lblComprobanteSubTotal = document.getElementById('<%=lblComprobanteSubTotal.ClientID%>');
            var lblComprobanteIgv = document.getElementById('<%=lblComprobanteIgv.ClientID%>');
            var lblComprobanteTotal = document.getElementById('<%=lblComprobanteTotal.ClientID%>');

            var clienteId = hdnClienteId.value;
            var tipoComprobanteId = ddlTipoComprobante.value;
            var serieId = ddlSerie.value;
            var serie = ddlSerie.options[ddlSerie.selectedIndex].text;
            var numeroComprobante = txtNumeroComprobante.value;
            var numeroGuia = '';
            var glosa = txtGlosa.value;
            var estadoId = 1;
            var formaPagoId = 1;
            var fechaEmision = txtFechaEmision.value;
            var fechaVencimiento = txtFechaEmision.value;
            var fechaPago = txtFechaEmision.value;
            var monedaId = ddlMoneda.value;
            var cantidad = 0;
            var montoVenta = parseFloat(lblComprobanteSubTotal.innerText);
            var montoImpuesto = parseFloat(lblComprobanteIgv.innerText);
            var montoTotal = parseFloat(lblComprobanteTotal.innerText);
            var numeroPlaca = txtNumeroPlaca.value;
            Bloquear();
            PageMethods.GenerarComprobante(clienteId, tipoComprobanteId, serieId, serie, numeroComprobante, numeroGuia, glosa, estadoId, formaPagoId, fechaEmision, fechaVencimiento, fechaPago, monedaId, cantidad,
                montoVenta, montoImpuesto, montoTotal, numeroPlaca, GenerarComprobanteOk, fnLlamadaError);   
        }
    }
    function GenerarComprobanteOk(c) {
        var hdnVentaId = document.getElementById('<%=hdnVentaId.ClientID%>');
        var arrayComprobante = c.split('@');
        var ventaId = arrayComprobante[0];
        var mensaje = arrayComprobante[1];
        if (ventaId > 0) {
            hdnVentaId.value = ventaId;
            var btnImprimirComprobante = document.getElementById('btnImprimirComprobante');
            btnImprimirComprobante.style.display = '';
            MostrarMensaje(mensaje);
            var btnGenerarComprobante = document.getElementById('btnGenerarComprobante');
            btnGenerarComprobante.disabled = true;
        }
        Desbloquear();
    }
    function ImprimirComprobante() {
        var ventaId = 0;
        var hdnVentaId = document.getElementById('<%=hdnVentaId.ClientID%>');
        ventaId = hdnVentaId.value;
        Bloquear();
        PageMethods.ImprimirComprobante(ventaId, ImprimirComprobanteOk, fnLlamadaError);
    }
    function ImprimirComprobanteOk(c) {
        if (c != '') {
            var arrayComprobante = c.split('@');
            if (arrayComprobante.length > 1) {
                var mensaje = arrayComprobante[0];
                var nombreComprobante = arrayComprobante[1];
                MostrarMensaje(mensaje);
                var txtNumeroDocumento = document.getElementById('<%=txtNumeroDocumento.ClientID%>');
                AbrirVentana('../Archivos/Documentos/Cliente/' + txtNumeroDocumento.value + '/' + nombreComprobante,
                    'Comprobante',
                    800,
                    600);
            }
        } else {
            MostrarMensaje(c);
        }
        Desbloquear();
    }
    function ObtenerCliente(e) {
        if ((e.keyCode == 13) && (e.srcElement.id == 'txtCodigoCliente')) { // Presiona Enter
            var txtCodigoCliente = document.getElementById('<%=txtCodigoCliente.ClientID%>');
            var codigo = txtCodigoCliente.value;
            var clienteId = 0;
            Bloquear();
            PageMethods.ObtenerCliente(clienteId, codigo, ObtenerClienteOk, fnLlamadaError);
            return false;
        }
    }
    function ObtenerClienteOk(c) {
        if (c.length > 0) {
            var r = c[0];
            SeleccionarCliente(r.ClienteId, r.Nombres, r.ApellidoPaterno, r.ApellidoMaterno, r.TipoDocumentoId, r.NumeroDocumento, r.Direccion, r.Correo, r.TipoPersonaId, r.RazonSocial);
        } else {
            MostrarMensaje('Cliente no encontrado.');
            LimpiarCliente();
        }
        Desbloquear();
        return false;
    }
    function EnviarSunat() {
        // var ventaId = 2024;
        var ventaId = 2031;
        debugger;
        Bloquear();
        PageMethods.EnviarSunat(ventaId, EnviarSunatOk, fnLlamadaError);
        return false;
    }
    function EnviarSunatOk(m) {
        if (m != '') {
            alert(m);
        }
        Desbloquear();
        return false;
    }
    function VerClienteNuevo() {
        // N: Nuevo
        AbrirVentana('../Configuracion/Cliente.aspx?Operacion=N', 'Registro de Cliente', 1000, 700);
        return false;
    }
    function SeleccionarPestaniaItem(id) {
        switch (id) {
            case 'btnProducto':
                document.getElementById('btnProducto').style.cursor = 'default';
                document.getElementById('trDatosProducto').style.display = '';
                document.getElementById('trDatosVenta').style.display = '';

                document.getElementById('btnImpuesto').style.cursor = 'pointer';
                document.getElementById('trImpuestos').style.display = 'none';
                document.getElementById('btnDescuento').style.cursor = 'pointer';
                document.getElementById('trDescuento').style.display = 'none';
                break;
            case 'btnImpuesto':
                document.getElementById('btnProducto').style.cursor = 'pointer';
                document.getElementById('trDatosProducto').style.display = 'none';
                document.getElementById('trDatosVenta').style.display = 'none';

                document.getElementById('btnImpuesto').style.cursor = 'default';
                document.getElementById('trImpuestos').style.display = '';
                document.getElementById('btnDescuento').style.cursor = 'pointer';
                document.getElementById('trDescuento').style.display = 'none';
                break;
            case 'btnDescuento':
                document.getElementById('btnProducto').style.cursor = 'pointer';
                document.getElementById('trDatosProducto').style.display = 'none';
                document.getElementById('trDatosVenta').style.display = 'none';

                document.getElementById('btnImpuesto').style.cursor = 'pointer';
                document.getElementById('trImpuestos').style.display = 'none';
                document.getElementById('btnDescuento').style.cursor = 'default';
                document.getElementById('trDescuento').style.display = '';
                break;
            //default:
            //    alert('Default case');
        }
    }
    function fnLlamadaError(excepcion) {
        alert('Ha ocurrido un error interno: ' + excepcion.get_message());
        Desbloquear();
    }
</script>
    <form id="frmComprobante" runat="server" DefaultButton="btnDefault">
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
                    <td style="text-align: center; width: 100%;">
                        <asp:Label CssClass="lblTitulo" Width="100%" runat="server">COMPROBANTE</asp:Label>
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
                                <td class="lblStandar" style="width: 10%;">Tipo Comprob.</td>
                                <td colspan="2" style="width: 20%;">
                                    <asp:DropDownList id="ddlTipoComprobante" CssClass="ddlStandar" Width="90%" runat="server" />
                                </td>
                                <td class="lblStandar" style="width: 10%;">Serie</td>
                                <td style="width: 10%;">
                                    <asp:DropDownList id="ddlSerie" CssClass="ddlStandar" Width="90%" runat="server" />
                                </td>
                                <td class="lblStandar" style="width: 10%;">N&deg; Documento</td>
                                <td style="width: 10%;">
                                    <asp:TextBox id="txtNumeroComprobante" CssClass="txtStandarCantidad" Width="90%" runat="server"></asp:TextBox>
                                </td>
                                <td style="width: 10%;">&nbsp;</td>
                                <td class="lblStandar" style="width: 10%;">N&deg; Placa</td>
                                <td style="width: 10%;">
                                    <asp:TextBox id="txtNumeroPlaca" CssClass="txtStandar" Width="90%" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="lblStandar">Moneda</td>
                                <td colspan="2">
                                    <asp:DropDownList id="ddlMoneda" CssClass="ddlStandar" Width="90%" runat="server"/>
                                </td>
                                <td class="lblStandar">Fec. Emisi&oacute;n</td>
                                <td>
                                    <asp:TextBox id="txtFechaEmision" CssClass="txtStandarFecha" Width="80%" runat="server"></asp:TextBox>
                                    <asp:ImageButton ID="imgFechaEmision" CssClass="imgCalendario" ImageUrl="~/Imagenes/Iconos/calendario.png" runat="server"/>
                                    <ajaxToolkit:CalendarExtender runat="server" ID="ceFechaEmision" BehaviorID="bceFechaEmision" CssClass="custom-calendar" TargetControlID="txtFechaEmision" Format="dd/MM/yyyy" PopupButtonID="imgFechaEmision" />
                                </td>
                                <td class="lblStandar">Descuento</td>
                                <td>
                                    <asp:TextBox id="txtDescuento" CssClass="txtStandarMonto" Width="90%" runat="server"></asp:TextBox>
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="10">&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="lblStandar">C&oacute;digo</td>
                                <td colspan="2">
                                    <asp:TextBox id="txtCodigoCliente" CssClass="txtStandar" runat="server"></asp:TextBox>
                                    <img id="imgVerCliente" alt="Buscar Cliente" style="cursor: pointer;" src="../Imagenes/Iconos/buscar.png" onclick="return VerCliente();" />
                                    <img id="imgVerClienteNuevo" alt="Nuevo Cliente" style="cursor: pointer;" src="../Imagenes/Iconos/nuevo_cliente.png" onclick="return VerClienteNuevo();" />
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="lblStandar">Cliente</td>
                                <td colspan="3">
                                    <asp:TextBox id="txtCliente" CssClass="txtStandar" Width="90%" ReadOnly="true" runat="server"></asp:TextBox>
                                </td>
                                <td>
                    
                                </td>
                                <td class="lblStandar">Tipo Doc.</td>
                                <td colspan="2">
                                    <asp:DropDownList id="ddlTipoDocumento" CssClass="ddlStandar" Width="90%" runat="server"/>
                                </td>
                                <td class="lblStandar">N&deg; Documento</td>
                                <td>
                                    <asp:TextBox id="txtNumeroDocumento" CssClass="txtStandar" Width="90%" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="lblStandar">Direcci&oacute;n</td>
                                <td colspan="3">
                                    <asp:TextBox id="txtDireccion" CssClass="txtStandar" Width="90%" runat="server"></asp:TextBox>
                                </td>
                                <td>&nbsp;</td>
                                <td class="lblStandar">Email</td>
                                <td colspan="3">
                                    <asp:TextBox id="txtEmail" CssClass="txtStandar" Width="90%" runat="server"></asp:TextBox>&nbsp;
                                    <img class="imgAyuda" src="../Imagenes/Iconos/interrogacion.png" data-toggle="tooltip" data-placement="top" title="Para más de un correo separe con , ó ;"/>
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="lblStandar">Glosa</td>
                                <td colspan="4">
                                    <asp:TextBox id="txtGlosa" CssClass="txtStandar" TextMode="MultiLine" Width="90%" runat="server"></asp:TextBox>
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%;">&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 100%;">
                        <input type="button" id="btnProducto" class="btnStandar" value="Producto" onclick="return SeleccionarPestaniaItem(this.id);" style="width: 8%;" />
                        <input type="button" id="btnImpuesto" class="btnStandar" value="IGV / ISC" onclick="return SeleccionarPestaniaItem(this.id);" style="width: 8%;" />
                        <input type="button" id="btnDescuento" class="btnStandar" value="Gratuita / Descuento" onclick="return SeleccionarPestaniaItem(this.id);" style="width: 8%;" />
                    </td>
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
                                <td class="lblStandar">Producto</td>
                                <td colspan="2">
                                    <asp:TextBox id="txtProductoCodigo" CssClass="txtStandar" Width="90%" ReadOnly="True" runat="server"></asp:TextBox>
                                    <img id="imgVerProducto" alt="Buscar Producto" style="cursor: pointer;" src="../Imagenes/Iconos/buscar.png" onclick="return VerProducto();" />
                                </td>
                                <td class="lblStandar">Descripci&oacute;n</td>
                                <td colspan="4">
                                    <asp:TextBox id="txtProductoDescripcion" CssClass="txtStandar" Width="90%" runat="server"></asp:TextBox>
                                </td>
                                <td class="lblStandar">Unidad</td>
                                <td>
                                    <asp:DropDownList id="ddlUnidad" CssClass="ddlStandar" Width="90%" runat="server"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr id="trDatosVenta">
                                <td class="lblStandar">Precio</td>
                                <td>
                                    <asp:TextBox id="txtPrecio" CssClass="txtStandarMonto" ReadOnly="true" Width="90%" runat="server"></asp:TextBox>
                                </td>
                                <td class="lblStandar">Cantidad</td>
                                <td>
                                    <asp:TextBox id="txtCantidad" CssClass="txtStandarMonto" Width="90%" runat="server"></asp:TextBox>
                                </td>
                                <td class="lblStandar">Sub Total</td>
                                <td>
                                    <asp:TextBox id="txtSubTotal" CssClass="txtStandarMonto" Width="90%" runat="server"></asp:TextBox>
                                </td>
                                <td class="lblStandar">IGV</td>
                                <td>
                                    <asp:TextBox id="txtIgv" CssClass="txtStandarMonto" ReadOnly="true" Width="90%" runat="server"></asp:TextBox>
                                </td>
                                <td class="lblStandar">Total</td>
                                <td>
                                    <asp:TextBox id="txtTotal" CssClass="txtStandarMonto" ReadOnly="true" Width="90%" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr id="trImpuestos" style="display: none;">
                                <td class="lblStandar">Tipo IGV</td>
                                <td colspan="2">
                                    <asp:DropDownList id="ddlTipoIgv" CssClass="ddlStandar" Width="90%" runat="server"></asp:DropDownList>
                                </td>
                                <td class="lblStandar">Total IGV Item</td>
                                <td>
                                    <asp:TextBox id="txtTotalIgvItem" CssClass="txtStandarMonto" runat="server"></asp:TextBox>
                                </td>
                                <td class="lblStandar">Tipo C&aacute;lculo ISC</td>
                                <td colspan="2">
                                    <asp:DropDownList id="ddlTipoCalculoIsc" CssClass="ddlStandar" Width="90%" runat="server"></asp:DropDownList>
                                </td>
                                <td class="lblStandar">ISC
                                    <asp:Label id="lblPorcentajeIsc" Text="" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox id="txtIsc" CssClass="txtStandarMonto" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr id="trDescuento" style="display: none;">
                                <td class="lblStandar">Total Venta Gratuita</td>
                                <td>
                                    <asp:CheckBox id="chkTotalVentaGratuita" runat="server" />
                                </td>
                                <td>&nbsp;</td>
                                <td class="lblStandar">Descuento Item</td>
                                <td>
                                    <asp:TextBox id="txtDescuentoItem" CssClass="txtStandarMonto" runat="server"></asp:TextBox>
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="10">&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: center;">
                                    <asp:Button id="btnGuardar" CssClass="btnStandar" Text="Agregar" OnClientClick="return ValidarGuardar();" Width="80%" runat="server" OnClick="btnGuardar_Click" />
                                </td>
                                <td colspan="2" style="text-align: center; display: none;">
                                    <asp:Button id="btnEliminarItem" CssClass="btnStandar" Text="Eliminar" OnClientClick="return EliminarItem();" Enabled="false" OnClick="btnEliminarItem_OnClick" Width="80%" runat="server" />
                                </td>
                                <td colspan="2" style="text-align: center;">
                                    <asp:Button id="btnCancelar" CssClass="btnStandar" Text="Cancelar" OnClientClick="return Cancelar();" Width="80%" runat="server" />
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
                                            <th style="width: 5%; cursor: default; text-decoration: none;">Acci&oacute;n</th>
                                            <th style="width: 10%; cursor: default; text-decoration: underline;">C&oacute;digo</th>
                                            <th style="width: 30%; cursor: pointer; text-decoration: underline;">Descripci&oacute;n</th>
                                            <th style="width: 5%; cursor: pointer; text-decoration: underline;">Unidad</th>
                                            <th style="width: 10%; cursor: pointer; text-decoration: underline;">Cantidad</th>
                                            <th style="width: 10%; cursor: pointer; text-decoration: underline;">P. Unitario</th>
                                            <th style="width: 10%; cursor: pointer; text-decoration: underline;">Sub Total</th>
                                            <th style="width: 10%; cursor: pointer; text-decoration: underline;">I.G.V.</th>
                                            <th style="width: 10%; cursor: pointer; text-decoration: underline;">Total</th>
                                            <th>&nbsp;&nbsp;&nbsp;&nbsp;</th>
                                        </tr>
                                        </thead>
                                    </table>
                                    <asp:UpdatePanel ID="divData" runat="server" UpdateMode="Conditional" style="overflow-y: scroll; width: 100%; margin: 0 0 0px 0px;" onscroll="Onscrollfnction();">
                                        <ContentTemplate>
                                            <asp:GridView id="grvItem" AutoGenerateColumns="False" RowStyle-Wrap="True" Width="100%" runat="server">
                                                <HeaderStyle CssClass="filaCabeceraGrid"></HeaderStyle>
                                                <RowStyle CssClass="filaImparGrid"></RowStyle>
                                                <AlternatingRowStyle CssClass="filaParGrid"></AlternatingRowStyle>
                                                <Columns>
                                                    <%-- 0: Acciones--%>
                                                    <asp:TemplateField HeaderText="Acciones">
                                                        <ItemStyle Width="5%" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEliminarItem" runat="server" CommandArgument='<%# Eval("VentaDetalleId") %>' OnClick="btnEliminarItem_OnClick" ToolTip="Eliminar Adjunto">
                                                                <asp:Image ID="imgEliminar" ImageUrl="~/Imagenes/Iconos/borrar.png" runat="server"/>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%--  0: Código --%>
                                                    <asp:TemplateField HeaderText="C&oacute;digo">
                                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <%# Eval("Codigo").ToString().Trim() %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--  1: Descripcion --%>
                                                    <asp:TemplateField HeaderText="Descripcion">
                                                        <ItemStyle Width="30%" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <%# Eval("Producto").ToString().Trim() %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--  2: Unidad --%>
                                                    <asp:TemplateField HeaderText="Unidad">
                                                        <ItemStyle Width="5%" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <%# Eval("UnidadMedida").ToString().Trim() %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--  3: Cantidad --%>
                                                    <asp:TemplateField HeaderText="Cantidad">
                                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <%# Eval("Cantidad").ToString().Trim() %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--  3: Precio Unitario --%>
                                                    <asp:TemplateField HeaderText="Precio">
                                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <%# Eval("PrecioUnitario").ToString().Trim() %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--  4: SubTotal --%>
                                                    <asp:TemplateField HeaderText="SubTotal">
                                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <%# Eval("SubTotal").ToString().Trim() %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--  5: Igv --%>
                                                    <asp:TemplateField HeaderText="Igv">
                                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <%# Eval("Igv").ToString().Trim() %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--  6: Total --%>
                                                    <asp:TemplateField HeaderText="Monto Total">
                                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <%# Eval("MontoTotal").ToString().Trim() %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click" />
                                            <%--<asp:AsyncPostBackTrigger ControlID="btnGenerarComprobante" EventName="Click" />--%>
                                        </Triggers>
                                    </asp:UpdatePanel>
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
                                        <input type="button" id="btnGenerarComprobante" value="Generar Factura" class="btnStandar" style="width: 80%;" onclick="return GenerarComprobante();"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="width: 100%; text-align: center;">
                                        <%--<asp:Button id="btnImprimirComprobante" CssClass="btnStandar" Text="Imprimir Factura" OnClientClick="return ImprimirComprobante();" Width="80%" runat="server" OnClick="btnImprimirComprobante_OnClick"/>--%>
                                        <input type="button" id="btnImprimirComprobante" value="Imprimir Factura" class="btnStandar" style="width: 80%; display: none;" onclick="return ImprimirComprobante();"/>
                                    </td>
                                </tr>
                                <%--<tr>
                                    <td colspan="2" style="width: 100%; text-align: center;">
                                        <input type="button" id="btnEnviarSunat" value="Enviar SUNAT" class="btnStandar" style="width: 80%;" onclick="return EnviarSunat();"/>
                                    </td>
                                </tr>--%>
                            </table>
                        </div>
                        <br/>
                    </td>
                </tr>
            </table>
        </div>
        <div id="derecha"></div>
            <asp:Panel ID="pnlCliente" runat="server" CssClass="pnlModal" style="width: 800px; display: none;" DefaultButton="btnBuscarCliente">
                <table style="width: 100%;">
                    <tr>
                        <td id="lblTituloCliente" class="lblTituloPopup" colspan="4">BUSCAR CLIENTE</td>
                    </tr>
                    <tr>
                        <td colspan="2" class="lblStandar">
                            <asp:RadioButton id="rbtPersonaNatural" Text="Persona Natural" GroupName="TipoPersona" Checked="True" runat="server"/>&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:RadioButton id="rbtPersonaJuridica" Text="Persona Jur&iacute;dica" GroupName="TipoPersona" runat="server"/>
                        </td>
                        <td class="lblStandar">C&oacute;digo</td>
                        <td>
                            <asp:TextBox id="txtCodigoClienteBuscar" CssClass="txtStandar" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="trPersonaNatural1">
                        <td class="lblStandar">Tipo Documento</td>
                        <td>
                            <asp:DropDownList id="ddlTipoDocumentoPersonaNatural" CssClass="ddlStandar" Width="60%" runat="server"/>
                        </td>
                        <td class="lblStandar"># Documento</td>
                        <td>
                            <asp:TextBox id="txtNumeroDocumentoPersonaNatural" CssClass="txtStandar" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="trPersonaNatural2">
                        <td class="lblStandar">Nombres</td>
                        <td colspan="2">
                            <asp:TextBox id="txtNombresCliente" CssClass="txtStandar" runat="server"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr id="trPersonaNatural3">
                        <td class="lblStandar">Apellido Paterno</td>
                        <td>
                            <asp:TextBox id="txtApellidoPaternoCliente" CssClass="txtStandar" runat="server"></asp:TextBox>
                        </td>
                        <td class="lblStandar">Apellido Materno</td>
                        <td>
                            <asp:TextBox id="txtApellidoMaternoCliente" CssClass="txtStandar" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="trPersonaJuridica" style="display: none;">
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
                                        <th style="width: 10%; cursor: default; text-decoration: none;">Acci&oacute;n</th>
                                        <th style="width: 30%; cursor: default; text-decoration: underline;">Nombre</th>
                                        <th style="width: 15%; cursor: pointer; text-decoration: underline;"># Documento</th>
                                        <th style="width: 30%; cursor: pointer; text-decoration: underline;">Direcci&oacute;n</th>
                                        <th style="width: 15%; cursor: pointer; text-decoration: underline;">Email</th>
                                        <th>&nbsp;&nbsp;&nbsp;&nbsp;</th>
                                    </tr>
                                </thead>
                            </table>
                            <asp:UpdatePanel ID="upCliente" runat="server" UpdateMode="Conditional" style="overflow-y: scroll; height: 200px; margin: 0 0 0px 0px;" onscroll="Onscrollfnction();">
                                <ContentTemplate>
                                    <asp:GridView id="grvCliente" AutoGenerateColumns="False" Width="100%" RowStyle-Wrap="True" runat="server" OnRowDataBound="grvCliente_RowDataBound">
                                        <HeaderStyle CssClass="filaCabeceraGrid"></HeaderStyle>
                                        <RowStyle CssClass="filaImparGrid"></RowStyle>
                                        <AlternatingRowStyle CssClass="filaParGrid"></AlternatingRowStyle>
                                        <Columns>
                                            <%-- 0: Acciones --%>
                                            <asp:TemplateField HeaderText="Acciones">
                                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:ImageButton runat="server" ImageUrl="~/Imagenes/Iconos/seleccionar.png" CssClass="imgButton" OnClientClick='<%# "return SeleccionarCliente(" + Eval("ClienteId") + ",\"" + Eval("Nombres").ToString() + "\",\"" + Eval("ApellidoPaterno") + "\",\"" + Eval("ApellidoMaterno") + "\"," + Eval("TipoDocumentoId") + ",\"" + Eval("NumeroDocumento") + "\",\"" + Eval("Direccion") + "\",\"" + Eval("Correo") + "\"," + Eval("TipoPersonaId") + ",\"" + Eval("RazonSocial") + "\");" %>' ToolTip="Seleccionar" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--  1: Nombre --%>
                                            <asp:TemplateField HeaderText="Nombre">
                                                <ItemStyle Width="30%" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <%--<%# Eval("Nombres").ToString().Trim() + ", " + Eval("ApellidoPaterno").ToString().Trim() + " " + Eval("ApellidoMaterno").ToString().Trim() %>--%>
                                                    <%# ObtenerNombreCliente(Convert.ToInt32(Eval("TipoPersonaId")), Eval("Nombres").ToString().Trim(), Eval("ApellidoPaterno").ToString().Trim(), Eval("ApellidoMaterno").ToString().Trim(), Eval("RazonSocial").ToString().Trim()) %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- 2: # Documento --%>
                                            <asp:TemplateField HeaderText="# Documento">
                                                <ItemStyle Width="15%" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <%# Eval("NumeroDocumento").ToString().Trim() %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- 3: Dirección --%>
                                            <asp:TemplateField HeaderText="Dirección">
                                                <ItemStyle Width="30%" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <%# Eval("Direccion")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- 4: Email --%>
                                            <asp:TemplateField HeaderText="Email">
                                                <ItemStyle Width="15%" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <%# Eval("Correo")%>
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
                            <input type="hidden" id="hdnProductoId" value="0" runat="server" />
                            <input type="hidden" id="hdnProducto" value="" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center;">
                            <asp:Button ID="btnBuscarCliente" runat="server" CssClass="btnStandar" Text="Buscar" Width="30%" OnClick="btnBuscarCliente_Click" />
                            <asp:Button ID="btnCancelarCliente" runat="server" CssClass="btnStandar" Text="Cancelar" Width="30%" />
                            <asp:button id="hButton1" runat="server" OnClientClick="return false;" style="display:none;" />
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
                                            <th style="width: 19%; cursor: pointer; text-decoration: underline;">Precio</th>                                           
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
                                                            OnClientClick='<%# "return SeleccionarProducto(" + Eval("ProductoId") + ",\"" + Eval("Producto") + "\",\"" + Eval("Codigo").ToString() +
                                                                "\",\"" + Eval("PrecioNormal") + "\",\"" + Eval("UnidadMedidaId") + "\");" %>' ToolTip="Seleccionar" />
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
                                                 <asp:TemplateField HeaderText="PrecioNormal">
                                                    <ItemStyle Width="19%" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <%# Eval("PrecioNormal").ToString() %>
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
                            <td colspan="4">&nbsp;</td>
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
        </div>
        <input type="hidden" id="hdnVentaId" value="0" runat="server" />
        <input type="hidden" id="hdnVentaDetalleId" runat="server" />
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
    // RedimensionarGrid();
    window.onresize = function (event) {
        // RedimensionarGrid();
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
        if (postBackElement.id == 'btnGuardarVenta' || postBackElement.id == 'btnBuscarCliente' || postBackElement.id == 'btnBuscarProducto' || postBackElement.id == 'btnGuardar') {
            // Bloqueo Pantalla
            Bloquear(100002);
        }
    }
    function EndRequest(sender, args) {
        if (postBackElement.id == 'btnGuardarVenta' || postBackElement.id == 'btnBuscarCliente' || postBackElement.id == 'btnBuscarProducto' || postBackElement.id == 'btnGuardar') {
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
    function grvCliente_OnDnlClick(row) {
        debugger;
        alert(row);
    }
    var ayuda = $('.imgAyuda');
    if (ayuda != null) {
        $('.imgAyuda').tooltip({ trigger: 'click' })
            .on('mouseleave', function () {
                $('.imgAyuda').tooltip('hide');
            });
    }
</script>
</body>
</html>