<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cliente.aspx.cs" Inherits="APU.Presentacion.Configuracion.Cliente" EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="uc1" TagName="ucProcesando" Src="~/Controles/ucProcesando.ascx" %>
<%@ Register Src="~/Controles/ucCabecera.ascx" TagPrefix="uc1" TagName="ucCabecera" %>
<%@ Register Src="~/Controles/ucMenu.ascx" TagPrefix="uc1" TagName="ucMenu" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Clientes</title>
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
        function DeclararControles() {
            var hdnClienteId = document.getElementById('<%=hdnClienteId.ClientID%>');
            var txtNombres = document.getElementById('<%=txtNombres.ClientID%>');
            var txtApellidoPaterno = document.getElementById('<%=txtApellidoPaterno.ClientID%>');
            var txtApellidoMaterno = document.getElementById('<%=txtApellidoMaterno.ClientID%>');
            <%--var ddlSexo = document.getElementById('<%=ddlSexo.ClientID%>');
            var ddlEstadoCivil = document.getElementById('<%=ddlEstadoCivil.ClientID%>');--%>
            var txtCorreo = document.getElementById('<%=txtCorreo.ClientID%>');
            var txtTelefono = document.getElementById('<%=txtTelefono.ClientID%>');
            var txtCelular = document.getElementById('<%=txtCelular.ClientID%>');
            var ddlTipoDocumento = document.getElementById('<%=ddlTipoDocumento.ClientID%>');
            var txtNumeroDocumento = document.getElementById('<%=txtNumeroDocumento.ClientID%>');

            var txtCodigo = document.getElementById('<%=txtCodigo.ClientID%>');
            <%--var txtLogin = document.getElementById('<%=txtLogin.ClientID%>');
            var txtContrasenia = document.getElementById('<%=txtContrasenia.ClientID%>');
            var ddlPerfil = document.getElementById('<%=ddlPerfil.ClientID%>');
            var ddlEmpresa = document.getElementById('<%=ddlEmpresa.ClientID%>');--%>
            var ddlDepartamento = document.getElementById('<%=ddlDepartamento.ClientID%>');
            <%--var ddlCargo = document.getElementById('<%=ddlCargo.ClientID%>');
            var txtCorreoTrabajo = document.getElementById('<%=txtCorreoTrabajo.ClientID%>');
            var txtTelefonoTrabajo = document.getElementById('<%=txtTelefonoTrabajo.ClientID%>');--%>
            var chkActivo = document.getElementById('<%=chkActivo.ClientID%>');
        }

        function Buscar() {
            var grid = document.getElementById('grvCliente');
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
            //if (div.scrollLeft > div2.scrollLeft) {
            //alert('divData: ' + div.scrollLeft);
            //alert('divHeader: ' + div2.scrollLeft);
            //}
            //****** Scrolling HeaderDiv along with DataDiv ******
            div2.scrollLeft = div.scrollLeft;
            return false;
        }
        function VerCliente(clienteId) {
            PageMethods.ObtenerCliente(clienteId, ObtenerClienteOk, fnLlamadaError);
            return false;
        }
        function LimpiarCliente() {
            var hdnClienteId = document.getElementById('<%=hdnClienteId.ClientID%>');
        var txtCodigo = document.getElementById('<%=txtCodigo.ClientID%>');
        var ddlTipoPersona = document.getElementById('<%=ddlTipoPersona.ClientID%>');
        var trRazonSocial = document.getElementById('trRazonSocial');
        var txtNombres = document.getElementById('<%=txtNombres.ClientID%>');
        var txtApellidoPaterno = document.getElementById('<%=txtApellidoPaterno.ClientID%>');
        var txtApellidoMaterno = document.getElementById('<%=txtApellidoMaterno.ClientID%>');
        var txtRazonSocial = document.getElementById('<%=txtRazonSocial.ClientID%>');
        var txtDescripcion = document.getElementById('<%=txtDescripcion.ClientID%>');
        var ddlTipoDocumento = document.getElementById('<%=ddlTipoDocumento.ClientID%>');
        var txtNumeroDocumento = document.getElementById('<%=txtNumeroDocumento.ClientID%>');
        var ddlPais = document.getElementById('<%=ddlPais.ClientID%>');
        var ddlDepartamento = document.getElementById('<%=ddlDepartamento.ClientID%>');
        var ddlProvincia = document.getElementById('<%=ddlProvincia.ClientID%>');
        var ddlDistrito = document.getElementById('<%=ddlDistrito.ClientID%>');
        var txtCiudad = document.getElementById('<%=txtCiudad.ClientID%>');
        var txtDireccion = document.getElementById('<%=txtDireccion.ClientID%>');
        var txtTelefono = document.getElementById('<%=txtTelefono.ClientID%>');
        var txtCelular = document.getElementById('<%=txtCelular.ClientID%>');
        var txtFax = document.getElementById('<%=txtFax.ClientID%>');
        var txtCorreo = document.getElementById('<%=txtCorreo.ClientID%>');
        var txtContacto = document.getElementById('<%=txtContacto.ClientID%>');
        var txtUrl = document.getElementById('<%=txtUrl.ClientID%>');
        var fuImagen = document.getElementById('<%=fuImagen.ClientID%>');
        var chkActivo = document.getElementById('<%=chkActivo.ClientID%>');
        var lblModificadoPor = document.getElementById('<%=lblModificadoPor.ClientID%>');
        var lblFechaModificacion = document.getElementById('<%=lblFechaModificacion.ClientID%>');

        hdnClienteId.value = 0;
        txtCodigo.value = '';
        ddlTipoPersona.value = 0;
        document.getElementById('lblTituloCliente').innerText = 'Cliente';

        trRazonSocial.style.display = 'none';
        txtRazonSocial.value = '';
        txtNombres.value = '';
        txtApellidoPaterno.value = '';
        txtApellidoMaterno.value = '';
        txtDescripcion.value = '';
        ddlTipoDocumento.value = 0;
        txtNumeroDocumento.value = '';
        ddlPais.value = 0;
        ddlDepartamento.value = 0;
        ddlProvincia.value = 0;
        ddlDistrito.value = 0;
        txtCiudad.value = '';
        txtDireccion.value = '';
        txtTelefono.value = '';
        txtCelular.value = '';
        txtFax.value = '';
        txtCorreo.value = '';
        txtContacto.value = '';

        chkActivo.checked = true;
        lblModificadoPor.innerText = '';
        lblFechaModificacion.innerText = '';

        var tabCliente = $get('<%=tabCliente.ClientID%>');
        tabCliente.control.set_activeTabIndex(0);
        }
        function ObtenerClienteOk(c) {
            var modalDialog = $find('mpeCliente');
            if (c != null) {
                modalDialog.show();
                ResimensionarPopup();

                var tabCliente = $get('<%=tabCliente.ClientID%>');
            tabCliente.control.set_activeTabIndex(0);

            var hdnClienteId = document.getElementById('<%=hdnClienteId.ClientID%>');
            var txtCodigo = document.getElementById('<%=txtCodigo.ClientID%>');
            var ddlTipoPersona = document.getElementById('<%=ddlTipoPersona.ClientID%>');
            var trRazonSocial = document.getElementById('trRazonSocial');
            var txtNombres = document.getElementById('<%=txtNombres.ClientID%>');
            var txtApellidoPaterno = document.getElementById('<%=txtApellidoPaterno.ClientID%>');
            var txtApellidoMaterno = document.getElementById('<%=txtApellidoMaterno.ClientID%>');
            var txtRazonSocial = document.getElementById('<%=txtRazonSocial.ClientID%>');
            var txtDescripcion = document.getElementById('<%=txtDescripcion.ClientID%>');
            var ddlTipoDocumento = document.getElementById('<%=ddlTipoDocumento.ClientID%>');
            var txtNumeroDocumento = document.getElementById('<%=txtNumeroDocumento.ClientID%>');
            var ddlPais = document.getElementById('<%=ddlPais.ClientID%>');
            var ddlDepartamento = document.getElementById('<%=ddlDepartamento.ClientID%>');
            var ddlProvincia = document.getElementById('<%=ddlProvincia.ClientID%>');
            var ddlDistrito = document.getElementById('<%=ddlDistrito.ClientID%>');
            var txtCiudad = document.getElementById('<%=txtCiudad.ClientID%>');
            var txtDireccion = document.getElementById('<%=txtDireccion.ClientID%>');
            var txtTelefono = document.getElementById('<%=txtTelefono.ClientID%>');
            var txtCelular = document.getElementById('<%=txtCelular.ClientID%>');
            var txtFax = document.getElementById('<%=txtFax.ClientID%>');
            var txtCorreo = document.getElementById('<%=txtCorreo.ClientID%>');
            var txtContacto = document.getElementById('<%=txtContacto.ClientID%>');
            var txtUrl = document.getElementById('<%=txtUrl.ClientID%>');
            var fuImagen = document.getElementById('<%=fuImagen.ClientID%>');
            var chkActivo = document.getElementById('<%=chkActivo.ClientID%>');
            var lblModificadoPor = document.getElementById('<%=lblModificadoPor.ClientID%>');
            var lblFechaModificacion = document.getElementById('<%=lblFechaModificacion.ClientID%>');

            hdnClienteId.value = c.ClienteId;

            if (c.TipoPersonaId == TipoPersonaNatural) {
                document.getElementById('lblTituloCliente').innerText = c.Nombres;
            } else {
                document.getElementById('lblTituloCliente').innerText = c.RazonSocial;
            }

            txtCodigo.value = c.Codigo;
            ddlTipoPersona.value = c.TipoPersonaId;

            SeleccionarTipoPersona(c.TipoPersonaId);

            ddlTipoDocumento.value = c.TipoDocumentoId;
            txtNumeroDocumento.value = c.NumeroDocumento;
            txtRazonSocial.value = c.RazonSocial;
            txtDescripcion.value = c.Descripcion;
            txtNombres.value = c.Nombres;

                txtApellidoPaterno.value = c.ApellidoPaterno;
                txtApellidoMaterno.value = c.ApellidoMaterno;

            ddlPais.value = c.PaisId;

            if (c.PaisId == PaisPeru) {
                ddlDepartamento.disabled = false;
                ddlDepartamento.value = c.DepartamentoId;

                if (c.DepartamentoId > 0) {
                    ObtenerProvincia(c.DepartamentoId, c.ProvinciaId, c.DistritoId);
                }

                ddlProvincia.disabled = false;
                ddlProvincia.value = c.ProvinciaId;
                ddlDistrito.disabled = false;
                ddlDistrito.value = c.DistritoId;
            }

            txtCiudad.value = c.Ciudad;
            txtDireccion.value = c.Direccion;
            txtTelefono.value = c.Telefono;
            txtCelular.value = c.Celular;
            txtCorreo.value = c.Correo;
            txtContacto.value = c.Contacto;
            txtUrl.value = c.Url;
            fuImagen.value = c.Imagen;
            chkActivo.checked = (c.Activo > 0);
            lblModificadoPor.innerText = c.UsuarioModificacion;
            lblFechaModificacion.innerText = FormatoFecha(c.FechaModificacion);
        } else {
            modalDialog.hide();
        }
        }
        function InsertarCliente() {
            var modalDialog = $find('mpeCliente');

            modalDialog.show();
            ResimensionarPopup();
            LimpiarCliente();

            return false;
        }
        function ValidarCliente() {
            var hdnClienteId = document.getElementById('<%=hdnClienteId.ClientID%>');
        var txtCodigo = document.getElementById('<%=txtCodigo.ClientID%>');
        var ddlTipoPersona = document.getElementById('<%=ddlTipoPersona.ClientID%>');
        var trRazonSocial = document.getElementById('trRazonSocial');
        var txtNombres = document.getElementById('<%=txtNombres.ClientID%>');
        var txtApellidoPaterno = document.getElementById('<%=txtApellidoPaterno.ClientID%>');
        var txtApellidoMaterno = document.getElementById('<%=txtApellidoMaterno.ClientID%>');

        var txtRazonSocial = document.getElementById('<%=txtRazonSocial.ClientID%>');
        var txtDescripcion = document.getElementById('<%=txtDescripcion.ClientID%>');
        var ddlTipoDocumento = document.getElementById('<%=ddlTipoDocumento.ClientID%>');
        var txtNumeroDocumento = document.getElementById('<%=txtNumeroDocumento.ClientID%>');
        var ddlPais = document.getElementById('<%=ddlPais.ClientID%>');
        var ddlDepartamento = document.getElementById('<%=ddlDepartamento.ClientID%>');
        var ddlProvincia = document.getElementById('<%=ddlProvincia.ClientID%>');
        var ddlDistrito = document.getElementById('<%=ddlDistrito.ClientID%>');
        var txtCiudad = document.getElementById('<%=txtCiudad.ClientID%>');
        var txtDireccion = document.getElementById('<%=txtDireccion.ClientID%>');
        var txtTelefono = document.getElementById('<%=txtTelefono.ClientID%>');
        var txtCelular = document.getElementById('<%=txtCelular.ClientID%>');
        var txtFax = document.getElementById('<%=txtFax.ClientID%>');
        var txtCorreo = document.getElementById('<%=txtCorreo.ClientID%>');
        var txtContacto = document.getElementById('<%=txtContacto.ClientID%>');
        var txtUrl = document.getElementById('<%=txtUrl.ClientID%>');
        var fuImagen = document.getElementById('<%=fuImagen.ClientID%>');
        var chkActivo = document.getElementById('<%=chkActivo.ClientID%>');

        if (ddlTipoPersona.value.trim() == '0') {
            return MostrarValidacion(ddlTipoPersona, 'Seleccione el Tipo de Persona.');
        }
        if (ddlTipoPersona.value.trim() == '1') {
            if (txtNombres.value.trim() == '') {
                return MostrarValidacion(txtNombres, 'Ingrese el Nombre.');
            }
        } else {
            if (txtRazonSocial.value.trim() == '') {
                return MostrarValidacion(txtRazonSocial, 'Ingrese la Razón Social.');
            }
        }
            //if (txtApellidoPaterno.value.trim() == '') {
            //    return MostrarValidacion(txtApellidoPaterno, 'Ingrese el Apellido Paterno.');
            //}
            //if (txtApellidoMaterno.value.trim() == '') {
            //    return MostrarValidacion(txtApellidoMaterno, 'Ingrese el Apellido Materno.');
            //}
            //if (ddlTipoPersona.value.trim() == 1) {
            //    trRazonSocial.style.display = 'none';
            //} else {
            //    trRazonSocial.style.display = '';
            //}
            if (ddlTipoDocumento.value.trim() == '0') {
                return MostrarValidacion(ddlTipoDocumento, 'Seleccione el Tipo de Documento.');
            }
            if (txtNumeroDocumento.value.trim() == '') {
                return MostrarValidacion(txtNumeroDocumento, 'Ingrese el Número de Documento.');
            }
            if (ddlTipoPersona.value.trim() == '2') {
                var valRuc = ValidarRuc(txtNumeroDocumento.value.trim());
                if (!valRuc) {
                    return MostrarValidacion(txtNumeroDocumento, 'El RUC ingresado no es válido.');
                }
            }

            //if (ddlPais.value.trim() == '0') {
            //    return MostrarValidacion(ddlPais, 'Seleccione el País.');
            //}
            //if (ddlProvincia.value.trim() == '0') {
            //    return MostrarValidacion(ddlPais, 'Seleccione la Provincia.');
            //}
            //if (ddlDistrito.value.trim() == '0') {
            //    return MostrarValidacion(ddlDistrito, 'Seleccione el Distrito.');
            //}
            //if (txtCiudad.value.trim() == '') {
            //    return MostrarValidacion(txtCiudad, 'Ingrese la Ciudad.');
            //}
            //if (txtDireccion.value.trim() == '') {
            //    return MostrarValidacion(txtDireccion, 'Ingrese la Dirección.');
            //}
            //if (txtTelefono.value.trim() == '') {
            //    return MostrarValidacion(txtTelefono, 'Ingrese el Teléfono.');
            //}
            //if (txtCelular.value.trim() == '') {
            //    return MostrarValidacion(txtCelular, 'Ingrese el Celular.');
            //}
            //if (txtFax.value.trim() == '') {
            //    return MostrarValidacion(txtFax, 'Ingrese el Fax.');
            //}
            //if (!ValidarCorreo(txtCorreo.value.toLowerCase())) {
            //    return MostrarValidacion(txtCorreo, 'Ingrese el Correo.');
            //}
            //if (txtContacto.value.trim() == '') {
            //    return MostrarValidacion(txtContacto, 'Ingrese el Contacto.');
            //}
            //if (txtUrl.value.trim() == '') {
            //    return MostrarValidacion(txtUrl, 'Ingrese la Url.');
            //}
        }
        function fnLlamadaError(excepcion) {
            alert('Ha ocurrido un error interno: ' + excepcion.get_message());
            // Desbloquear Pantalla
            Desbloquear();
        }
        function MostrarContrasenia(target) {
            var d = document;
            var tag = d.getElementById(target);
            var tag2 = d.getElementById('btnMostrarContrasenia');

            if (tag2.value == 'Mostrar') {
                tag.setAttribute('type', 'text');
                tag2.value = 'Ocultar';

            } else {
                tag.setAttribute('type', 'password');
                tag2.value = 'Mostrar';
            }
        }
        function GenerarCodigo() {
            var txtNumeroDocumento = document.getElementById('<%=txtNumeroDocumento.ClientID%>');
            var txtCodigo = document.getElementById('<%=txtCodigo.ClientID%>');
            txtCodigo.value = txtNumeroDocumento.value;
            return false;
        }
        function VerAgencia(clienteId) {
            Bloquear();
            window.location = 'Agencia.aspx?ClienteId=' + clienteId;
        }
        function Cerrar() {
            var modalDialog = $find('mpeCliente');
            modalDialog.hide();
        }
    </script>
    <form id="frmCliente" runat="server" DefaultButton="btnDefault">
        <uc1:ucProcesando runat="server" ID="ucProcesando" />
    <asp:Button id="btnDefault" OnClientClick="return false;" style="display: none;" runat="server" />
        <asp:ScriptManager EnablePageMethods="True" runat="server"></asp:ScriptManager>
        <div id="contenedor">
            <div id="cabecera">
                <uc1:ucCabecera runat="server" ID="ucCabecera" />
            </div>
            <div id="menu">
                <uc1:ucMenu runat="server" ID="ucMenu" />
            </div>
            <div id="izquierda"></div>
            <div id="centro">
                <br />
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center;" colspan="5">
                            <asp:Label CssClass="lblTitulo" Width="100%" runat="server">CLIENTE</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%;">
                            <table style="width: 100%; margin: 0;">
                                <tr>
                                    <td style="width: 10%;">Ver:&nbsp;<asp:DropDownList ID="ddlNumeroRegistros" CssClass="ddlStandar" AutoPostBack="True" Width="50%" runat="server" OnSelectedIndexChanged="ddlNumeroRegistros_SelectedIndexChanged" />
                                    </td>
                                    <td style="width: 20%;">
                                        <asp:LinkButton ID="imgAnadir" ForeColor="#404040" runat="server" ToolTip="" OnClientClick="return InsertarCliente();">
                                            <asp:Image runat="server" ImageUrl="~/Imagenes/Iconos/nuevo.png" BorderStyle="None" ToolTip="Nuevo"/>&nbsp;Nuevo
                                        </asp:LinkButton>
                                    </td>
                                    <td style="width: 20%;">&nbsp;</td>
                                    <td style="width: 20%;">&nbsp;</td>
                                    <td style="width: 30%;">Buscar:
                                        <asp:TextBox ID="txtBuscar" CssClass="txtStandar" Width="80%" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%;">
                            <div id="divHeader" style="width: 1182px; overflow-x: hidden; overflow-y: hidden; margin: 0 0px 0 0px;">
                                <table id="dummyHeader" style="width: 2000px;" class="tblSinBordes">
                                    <thead>
                                        <tr class="filaCabeceraGrid">
                                            <th style="width: 5%; cursor: default; text-decoration: none;">Acciones</th>
                                            <th style="width: 20%; cursor: pointer; text-decoration: underline;">Nombre</th>
                                            <th style="width: 10%; cursor: pointer; text-decoration: underline;">Tipo Documento</th>
                                            <th style="width: 15%; cursor: pointer; text-decoration: underline;">N&uacute;mero Documento</th>
                                            <th style="width: 15%; cursor: pointer; text-decoration: underline;">Correo Electr&oacute;nico</th>
                                            <th style="width: 15%; cursor: pointer; text-decoration: underline;">Contacto</th>
                                            <th style="width: 20%; cursor: pointer; text-decoration: underline;">Activo</th>
                                        </tr>
                                    </thead>
                                </table>
                            </div>
                            <asp:UpdatePanel ID="divData" runat="server" UpdateMode="Conditional" style="overflow: scroll; width: 1200px; height: 420px; margin: 0 0 0px 0px;" onscroll="Onscrollfnction();">
                                <ContentTemplate>
                                    <asp:GridView ID="grvCliente" AutoGenerateColumns="False" Width="2000px" RowStyle-Wrap="True" runat="server" OnRowDataBound="grvCliente_RowDataBound">
                                        <HeaderStyle CssClass="filaCabeceraGrid"></HeaderStyle>
                                        <RowStyle CssClass="filaImparGrid"></RowStyle>
                                        <AlternatingRowStyle CssClass="filaParGrid"></AlternatingRowStyle>
                                        <Columns>
                                            <%-- 0: Acciones --%>
                                            <asp:TemplateField HeaderText="Acciones">
                                                <ItemStyle Width="5%" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:ImageButton runat="server" ImageUrl="~/Imagenes/Iconos/editar.png" CssClass="imgButton" OnClientClick='<%# "return VerCliente(" + Eval("ClienteId") + ");" %>' ToolTip="Editar" />
                                                    <asp:ImageButton runat="server" ImageUrl="~/Imagenes/Iconos/agencia.png" CssClass="imgButton" OnClientClick='<%# "return VerAgencia(" + Eval("ClienteId") + ");" %>' ToolTip="Ver Agencias" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--  1: Nombre --%>
                                            <asp:TemplateField HeaderText="Nombre">
                                                <ItemStyle Width="20%" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <%# ObtenerNombreCliente(Convert.ToInt32(Eval("TipoPersonaID")), Eval("Nombres").ToString().Trim(), Eval("ApellidoPaterno").ToString().Trim(), Eval("ApellidoMaterno").ToString().Trim(), Eval("RazonSocial").ToString().Trim()) %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- 2: Tipo Documento --%>
                                            <asp:TemplateField HeaderText="Tipo Documento">
                                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <%# Eval("TipoDocumento")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- 3: Numero Documento --%>
                                            <asp:TemplateField HeaderText="N° Documento">
                                                <ItemStyle Width="15%" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <%# Eval("NumeroDocumento")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- 4: Correo Electronico --%>
                                            <asp:TemplateField HeaderText="Correo Electronico">
                                                <ItemStyle Width="15%" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <%# Eval("Correo")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- 5: Contacto --%>
                                            <asp:TemplateField HeaderText="Contacto">
                                                <ItemStyle Width="15%" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <%# Eval("Contacto")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- 6: Activo --%>
                                            <asp:TemplateField HeaderText="Activo">
                                                <ItemStyle Width="20%" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:CheckBox runat="server" Checked='<%# Int16.Parse(Eval("Activo").ToString()) > 0 %>' Enabled="False" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- 7: ClienteId --%>
                                            <asp:TemplateField>
                                                <ItemStyle CssClass="columnaOcultaGrid" />
                                                <ItemTemplate>
                                                    <%# Eval("ClienteId")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlNumeroRegistros" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="btnPaginacion" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="btnGuardarCliente" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <div id="divFooter" style="width: 1182px; overflow-x: hidden; overflow-y: hidden; margin: 0 0px 0 0px;">
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
                                            <asp:Button ID="btnPaginacion" runat="server" Style="display: none;" OnClick="btnPaginacion_Click" />
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
            <asp:Panel ID="pnlCliente" runat="server" CssClass="pnlModal" Style="display: none;">
                <table style="width: 100%;">
                    <tr id="lblTituloPopupCliente" class="lblTituloPopup">
                        <td style="width: 10%;" class="lblTituloPopup">&nbsp;</td>
                        <td id="lblTituloCliente" class="lblTituloPopup" style="width: 80%">Cliente</td>
                        <td class="lblTituloPopup" style="width: 10%; text-align: right;">
                            <a onclick="return Cerrar();" style="color: #FFFFFF; text-decoration: none; cursor: pointer;">Cerrar X</a>&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <input type="hidden" id="hdnClienteId" value="0" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <ajaxToolkit:TabContainer ID="tabCliente" runat="server" OnClientActiveTabChanged="" Height="400px" Width="800px" ActiveTabIndex="0" OnDemand="False" AutoPostBack="false"
                                                      TabStripPlacement="Top" CssClass="ajax__tab_xp" ScrollBars="None" UseVerticalStripPlacement="False" VerticalStripWidth="120px" style="margin: 10px;">
                            <%--<ajaxToolkit:TabContainer ID="tabCliente" runat="server" OnClientActiveTabChanged="" Height="400px" Width="98%" ActiveTabIndex="0" OnDemand="False" AutoPostBack="false"--%>
                                <ajaxToolkit:TabPanel ID="tabClienteInformacionPersonal" runat="server" HeaderText="Informaci&oacute;n Personal" Enabled="true" ScrollBars="Auto" OnDemandMode="Always">
                                    <ContentTemplate>
                                        <table style="width: 100%;">
                                            <tr>
                                                <td class="lblStandar" style="width: 20%;">C&oacute;digo</td>
                                                <td style="width: 30%;">
                                                    <asp:TextBox ID="txtCodigo" CssClass="txtStandar" ReadOnly="True" Width="90%" runat="server" />
                                                    <img class="imgAyuda" src="../Imagenes/Iconos/interrogacion.png" data-toggle="tooltip" data-placement="top" title="Para Persona Natural el DNI, para Empresas el RUC."/>
                                                </td>
                                                <td class="lblStandar" style="width: 20%;">Tipo Persona</td>
                                                <td style="width: 30%;">
                                                    <asp:DropDownList ID="ddlTipoPersona" CssClass="ddlStandar" Width="90%" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="lblStandar">Tipo Documento</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlTipoDocumento" CssClass="ddlStandar" Width="90%" runat="server" />
                                                </td>
                                                <td class="lblStandar">N&deg; Documento</td>
                                                <td>
                                                    <asp:TextBox ID="txtNumeroDocumento" CssClass="txtStandar" Width="90%" runat="server" />
                                                </td>
                                            </tr>
                                            <tr id="trRazonSocial" style="display: none;">
                                                <td class="lblStandar">Raz&oacute;n Social</td>
                                                <td colspan="2">
                                                    <asp:TextBox ID="txtRazonSocial" CssClass="txtStandar" Width="90%" runat="server" />
                                                </td>
                                                <td>
                                                    <asp:Button id="btnSunat" Text="SUNAT" CssClass="btnStandar" Width="30%" OnClientClick="return ObtenerEmpresaSunat();" runat="server" />
                                                </td>
                                            </tr>
                                            <tr id="trPersonaNatural1">
                                                <td class="lblStandar">Nombres</td>
                                                <td>
                                                    <asp:TextBox ID="txtNombres" CssClass="txtStandar" Width="90%" runat="server" />
                                                </td>
                                                <td class="lblStandar"></td>
                                                <td>
                                                    <asp:Button id="btnReniec" Text="RENIEC" CssClass="btnStandar" Width="30%" OnClientClick="return ObtenerPersonaReniec();" runat="server" />
                                                </td>
                                            </tr>
                                            <tr id="trPersonaNatural2">
                                                <td class="lblStandar">Apellido Paterno</td>
                                                <td>
                                                    <asp:TextBox ID="txtApellidoPaterno" CssClass="txtStandar" Width="90%" runat="server" />
                                                </td>
                                                <td class="lblStandar">Apellido Materno</td>
                                                <td>
                                                    <asp:TextBox ID="txtApellidoMaterno" CssClass="txtStandar" Width="90%" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="lblStandar">Descripci&oacute;n</td>
                                                <td colspan="2">
                                                    <asp:TextBox ID="txtDescripcion" CssClass="txtStandar" Width="90%" runat="server" />
                                                </td>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="lblStandar">Pa&iacute;s</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlPais" CssClass="ddlStandar" Width="90%" runat="server" />
                                                </td>
                                                <td class="lblStandar">Departamento</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlDepartamento" CssClass="ddlStandar" Enabled="False" Width="90%" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="lblStandar">Provincia</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlProvincia" CssClass="ddlStandar" Enabled="False" Width="90%" runat="server" />
                                                </td>
                                                <td class="lblStandar">Distrito</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlDistrito" CssClass="ddlStandar" Enabled="False" Width="90%" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="lblStandar">Ciudad</td>
                                                <td>
                                                    <asp:TextBox ID="txtCiudad" CssClass="txtStandar" Width="90%" runat="server" />
                                                </td>
                                                <td class="lblStandar" style="width: 20%;"></td>
                                                <td style="width: 30%;">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="lblStandar">Direcci&oacute;n</td>
                                                <td colspan="2">
                                                    <asp:TextBox ID="txtDireccion" CssClass="txtStandar" Width="90%" runat="server" />
                                                </td>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="lblStandar">Tel&eacute;fono</td>
                                                <td>
                                                    <asp:TextBox ID="txtTelefono" CssClass="txtStandar" Width="90%" runat="server" />
                                                </td>
                                                <td class="lblStandar">Celular</td>
                                                <td>
                                                    <asp:TextBox ID="txtCelular" CssClass="txtStandar" Width="90%" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="lblStandar">Fax</td>
                                                <td>
                                                    <asp:TextBox ID="txtFax" CssClass="txtStandar" Width="90%" runat="server" />
                                                </td>
                                                <td class="lblStandar">Placa</td>
                                                <td>
                                                    <asp:TextBox ID="txtPlaca" CssClass="txtStandar" Width="90%" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="lblStandar">Correo</td>
                                                <td colspan="2">
                                                    <asp:TextBox ID="txtCorreo" CssClass="txtStandar" Width="90%" runat="server" />&nbsp;
                                                    <img class="imgAyuda" src="../Imagenes/Iconos/interrogacion.png" data-toggle="tooltip" data-placement="top" title="Para más de un correo separe con , ó ;"/>
                                                </td>
                                                <td>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="lblStandar">Contacto</td>
                                                <td colspan="2">
                                                    <asp:TextBox ID="txtContacto" CssClass="txtStandar" Width="90%" runat="server" />
                                                </td>
                                                <td style="width: 30%;">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="lblStandar">Url</td>
                                                <td colspan="2">
                                                    <asp:TextBox ID="txtUrl" CssClass="txtStandar" Width="90%" runat="server" />
                                                </td>
                                                <td style="width: 30%;">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="lblStandar">Imagen</td>
                                                <td colspan="2">
                                                    <asp:FileUpload ID="fuImagen" Width="90%" runat="server" />
                                                </td>
                                                <td style="width: 30%;">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="lblStandar">Activo</td>
                                                <td>
                                                    <asp:CheckBox ID="chkActivo" Checked="True" runat="server" />
                                                </td>
                                                <td class="lblStandar" style="width: 20%;"></td>
                                                <td style="width: 30%;">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="lblStandar">Modificado Por</td>
                                                <td>
                                                    <asp:Label ID="lblModificadoPor" CssClass="lblStandar" runat="server" />
                                                </td>
                                                <td class="lblStandar">Fecha Modificaci&oacute;n</td>
                                                <td>
                                                    <asp:Label ID="lblFechaModificacion" CssClass="lblStandar" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>
                                <ajaxToolkit:TabPanel id="tabClienteAgencia" runat="server" HeaderText="Agencias" Enabled="true" ScrollBars="Auto" OnDemandMode="Always">
                                    <ContentTemplate>
                                        <br/>
                                        <%--<div id="divHeaderAgencia" style="width: 1182px; overflow-x: hidden; overflow-y: hidden; margin: 0 0px 0 0px;" >--%>
                                        <%--<table id="dummyHeaderAgencia" style="width: 1500px;" class="tblSinBordes">--%>
                                        <table id="dummyHeaderAgencia" style="width: 100%;" class="tblSinBordes">
                                            <thead>
                                            <tr class="filaCabeceraGrid">
                                                <th style="width: 30%; cursor: pointer; text-decoration: underline;">Nombre</th>
                                                <th style="width: 60%; cursor: pointer; text-decoration: underline;">Direcci&oacute;n</th>
                                                <th style="width: 10%; cursor: pointer; text-decoration: underline;">Activo</th>
                                                <th>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</th>
                                            </tr>
                                            </thead>
                                        </table>
                                        <%--</div>--%>
                                        <%--<div id="divDataAgencia" style="overflow: scroll; width: 1200px; height: 420px; margin: 0 0 0px 0px;">--%>
                                        <%--<asp:GridView id="grvAgencia" AutoGenerateColumns="False" Width="1500px" RowStyle-Wrap="True" runat="server">--%>
                                        <asp:UpdatePanel ID="divDataAgencia" runat="server" UpdateMode="Conditional" style="overflow: scroll; width: 100%; height: 420px; margin: 0 0 0px 0px;" onscroll="Onscrollfnction();">
                                            <ContentTemplate>
                                                <asp:GridView id="grvAgencia" AutoGenerateColumns="False" Width="100%" RowStyle-Wrap="True" runat="server">
                                                    <HeaderStyle CssClass="filaCabeceraGrid"></HeaderStyle>
                                                    <RowStyle CssClass="filaImparGrid"></RowStyle>
                                                    <AlternatingRowStyle CssClass="filaParGrid"></AlternatingRowStyle>
                                                    <Columns>
                                                        <%--  1: Nombre --%>
                                                        <asp:TemplateField HeaderText="NumeroDocumento">
                                                            <ItemStyle Width="30%" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <%# Eval("Nombre").ToString().Trim() %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%-- 3: Dirección --%>
                                                        <asp:TemplateField HeaderText="Dirección">
                                                            <ItemStyle Width="60%" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <%# Eval("Direccion")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%-- 4: Activo --%>
                                                        <asp:TemplateField HeaderText="Activo">
                                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" Checked='<%# Int16.Parse(Eval("Activo").ToString()) > 0 %>' Enabled="False" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnCargarAgencia" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                        <%--</div>--%>
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
                            <asp:Button ID="btnGuardarCliente" runat="server" CssClass="btnStandar" Text="Guardar" Width="30%" OnClientClick="return ValidarCliente();" OnClick="btnGuardarCliente_OnClick" />
                            <asp:Button ID="btnCancelarCliente" runat="server" CssClass="btnStandar" Text="Cancelar" Width="30%" />
                            <asp:Button ID="hButton" runat="server" Style="display: none;" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <ajaxToolkit:ModalPopupExtender ID="mpeCliente" PopupControlID="pnlCliente" TargetControlID="hButton" CancelControlID="btnCancelarCliente" OnOkScript="return ValidarAnularOperacion();" Drag="True" DropShadow="True" BackgroundCssClass="FondoAplicacion" ClientIDMode="Static" runat="server" PopupDragHandleControlID="lblTituloPopupCliente"></ajaxToolkit:ModalPopupExtender>
            <ajaxToolkit:RoundedCornersExtender ID="rceCliente" runat="server" BehaviorID="rcbCliente" TargetControlID="pnlCliente" Radius="6" Corners="All" />
            <%--<cc1:ModalPopupExtender ID="mpeAnularOperacion" TargetControlID="btnAnularOperacion" PopupControlID="pnlAnularOperacion" CancelControlID="btnCancelarAnularOperacion" OnOkScript="return ValidarAnularOperacion();" DropShadow="True" BackgroundCssClass="FondoAplicacion" ClientIDMode="Static" runat="server"></cc1:ModalPopupExtender>--%>
        </div>
    <asp:Button id="btnCargarAgencia" style="display: none;" OnClick="btnCargarAgencia_OnClick" runat="server"/>
        <input type="hidden" id="hdnOperacion" value="" runat="server" />
    </form>
    <script type="text/javascript" src="../Scripts/tablesorter/jquery.tablesorter.js"></script>
    <script type="text/javascript">
        function pageLoad(sender, args) {
            $(document).ready(function () {
                $("#grvCliente").tablesorter({ dateFormat: 'uk' });
                SetDefaultSortOrder();
            });
            //var modalPopup = $find('mpeCliente');
            //modalPopup.add_shown(function () {
            //    modalPopup._backgroundElement.addEventListener('click', function () {
            //        modalPopup.hide();
            //    });
            //});
        }

        function Sort(cell, sortOrder) {
            var sorting = [[cell.cellIndex, sortOrder]];
            $("#grvCliente").trigger("sorton", [sorting]);
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
            //var divData = principal - 20;
            var divData = centro - 20;
            var divHeader = divData;
            document.getElementById('divHeader').style.width = divHeader + 'px';
            document.getElementById('divData').style.width = (divData + 17) + 'px';
            document.getElementById('divFooter').style.width = divHeader + 'px';

            //debugger;
            //var pnlCliente = document.getElementById('pnlCliente');
            //alert('pnlCliente - width: ' + pnlCliente.clientWidth + ' - ' + 'pnlCliente - Height: ' + pnlCliente.clientHeight);
            //alert('pnlCliente - width: ' + pnlCliente.clientWidth + ' - ' + 'pnlCliente - Height: ' + pnlCliente.clientHeight);

            //var tabCliente = document.getElementById('divHeader');
            //alert('tabCliente - width: ' + tabCliente.clientWidth + ' - ' + 'tabCliente - Height: ' + tabCliente.clientHeight);
            //alert('tabCliente - width: ' + tabCliente.clientWidth + ' - ' + 'tabCliente - Height: ' + tabCliente.clientHeight);
        }
        RedimensionarGrid();
        ResimensionarPopup();
        window.onresize = function (event) {
            RedimensionarGrid();
            ResimensionarPopup();
        };
        function ResimensionarPopup() {
            //debugger;
            //var pnlCliente = document.getElementById('pnlCliente');
            //alert('pnlCliente - width: ' + pnlCliente.clientWidth + ' - ' + 'pnlCliente - Height: ' + pnlCliente.clientHeight);
            //alert('pnlCliente - width: ' + pnlCliente.clientWidth + ' - ' + 'pnlCliente - Height: ' + pnlCliente.clientHeight);

            //var tabCliente = document.getElementById('tabCliente');
            //alert('tabCliente - width: ' + tabCliente.clientWidth + ' - ' + 'tabCliente - Height: ' + tabCliente.clientHeight);
            //alert('tabCliente - width: ' + tabCliente.clientWidth + ' - ' + 'tabCliente - Height: ' + tabCliente.clientHeight);

            document.getElementById('pnlCliente').style.position = 'absolute';
            document.getElementById('pnlCliente').style.top = '50%';
            document.getElementById('pnlCliente').style.left = '50%';

            document.getElementById('pnlCliente').style.display = 'table';
            document.getElementById('pnlCliente').style.margin = '0 auto';
            document.getElementById('pnlCliente').style.width = '900px';
            document.getElementById('tabCliente').style.width = '880px';
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
                (postBackElement.id == 'btnUltimo') || (postBackElement.id == 'btnPaginacion') || (postBackElement.id == 'btnGuardarCliente') || postBackElement.id == 'btnCargarAgencia') {
                // Bloqueo Pantalla
                Bloquear();
            }
        }
        function EndRequest(sender, args) {
            if ((postBackElement.id == 'ddlNumeroRegistros') || (postBackElement.id == 'btnPrimero') || (postBackElement.id == 'btnAnterior') || (postBackElement.id == 'btnSiguiente') ||
                (postBackElement.id == 'btnUltimo') || (postBackElement.id == 'btnPaginacion') || (postBackElement.id == 'btnGuardarCliente') || postBackElement.id == 'btnCargarAgencia') {
                // Desbloquear Pantalla
                Desbloquear();
                RedimensionarGrid();
            }
        }
        function grvCliente_OnDnlClick(row) {
            var indiceClienteId = 7;
            var clienteIdRow = RetornarCeldaValor(row, indiceClienteId);
            VerCliente(clienteIdRow);
        }
        function SeleccionarTipoPersona(tip) {
            var trRazonSocial = document.getElementById('trRazonSocial');
            var trPersonaNatural1 = document.getElementById('trPersonaNatural1');
            var trPersonaNatural2 = document.getElementById('trPersonaNatural2');
            var ddlTipoDocumento = document.getElementById('<%=ddlTipoDocumento.ClientID%>');
            if (tip == TipoPersonaNatural) {
                trRazonSocial.style.display = 'none';
                trPersonaNatural1.style.display = '';
                trPersonaNatural2.style.display = '';
                ddlTipoDocumento.value = TipoDocumentoDni;
            } else {
                trRazonSocial.style.display = '';
                trPersonaNatural1.style.display = 'none';
                trPersonaNatural2.style.display = 'none';
                ddlTipoDocumento.value = TipoDocumentoRuc;
            }
        }
        function SeleccionarPais(pai) {
            //var trRazonSocial = document.getElementById('trRazonSocial');
            var ddlDepartamento = document.getElementById('<%=ddlDepartamento.ClientID%>');
            var ddlProvincia = document.getElementById('<%=ddlProvincia.ClientID%>');
            var ddlDistrito = document.getElementById('<%=ddlDistrito.ClientID%>');
            // Perú
            if (pai == 203) {
                ddlDepartamento.disabled = false;
                ddlProvincia.disabled = false;
                ddlDistrito.disabled = false;
            } else {
                ddlDepartamento.disabled = true;
                ddlProvincia.disabled = true;
                ddlDistrito.disabled = true;
            }
        }
        function ObtenerProvincia(dep, pro, dis) {
            <%--var ddlDepartamento = document.getElementById('<%=ddlDepartamento.ClientID%>');--%>
            var ddlProvincia = document.getElementById('<%=ddlProvincia.ClientID%>');
            var ddlDistrito = document.getElementById('<%=ddlDistrito.ClientID%>');

            //if (ddlDepartamento.value == 0) {
            if (dep == 0) {
                LimpiarCombo(ddlProvincia);
            } else {
                //PageMethods.ObtenerProvincia(ddlDepartamento.value, ObtenerProvinciaSucceed, fnLlamadaError);
                PageMethods.ObtenerProvincia(dep, ObtenerProvinciaSucceed, fnLlamadaError, pro + '@' + dis);
            }
            LimpiarCombo(ddlDistrito);
        }
        function ObtenerProvinciaSucceed(result, pro_dis) {
            var pro_dis_arr = pro_dis.split('@');
            var pro = pro_dis_arr[0];
            var dis = pro_dis_arr[1];

            var ddlProvincia = document.getElementById('<%=ddlProvincia.ClientID%>');
            if (result.length > 0) {
                LlenarCombo(ddlProvincia, result, 'UbigeoId', 'Nombre');
                AgregarOptionACombo(ddlProvincia, Seleccione_value, Seleccione);
                ddlProvincia.value = Seleccione_value;
                if (pro > 0) {
                    ddlProvincia.value = pro;
                    ObtenerDistrito(pro, dis);
                }
            } else {
                alert('No existen provincias para el departamento seleccionado.');
            }
        }
        function ObtenerDistrito(pro, dis) {
            var ddlProvincia = document.getElementById('<%=ddlProvincia.ClientID%>');
            var ddlDistrito = document.getElementById('<%=ddlDistrito.ClientID%>');
            if (pro == 0) {
                LimpiarCombo(ddlDistrito);
            } else {
                PageMethods.ObtenerDistrito(pro, ObtenerDistritoSucceed, fnLlamadaError, dis);
            }
        }
        function ObtenerDistritoSucceed(result, dis) {
            var ddlDistrito = document.getElementById('<%=ddlDistrito.ClientID%>');
            if (result.length > 0) {
                LlenarCombo(ddlDistrito, result, 'UbigeoId', 'Nombre');
                AgregarOptionACombo(ddlDistrito, Seleccione_value, Seleccione);
                ddlDistrito.value = Seleccione_value;
                if (dis > 0) {
                    ddlDistrito.value = dis;
                }
            } else {
                alert('No existen distritos para el departamento seleccionado.');
            }
        }
        function ObtenerEmpresaSunat() {
            //var nombres = document.getElementById('ctl05_ctl00_txtNombres').value;
            //var empresaId = document.getElementById('ctl05_ctl01_ddlEmpresa').value;
            //var activo = document.getElementById('ctl05_ctl01_chkActivo').checked ? 1 : 0;
            var txtRuc = document.getElementById('<%=txtNumeroDocumento.ClientID%>');
            var ruc = txtRuc.value;
            Bloquear(100002);
            PageMethods.ObtenerEmpresaSunat(ruc, ObtenerEmpresaSunatOk, fnLlamadaError);
            return false;
        }
        function ObtenerEmpresaSunatOk(e) {
            if (e != null) {
                // var empresa = e.split('@');
                var txtRazonSocial = document.getElementById('<%=txtRazonSocial.ClientID%>');
                var txtDireccion = document.getElementById('<%=txtDireccion.ClientID%>');

                //txtRazonSocial.value = empresa[1];
                //if (empresa.length > 4) {
                //    txtDireccion.value = empresa[3];
                //    //txtCiiu.value = empresa[4];
                //} else {
                //    txtDireccion.value = empresa[2];
                //    //txtCiiu.value = empresa[3];
                //}
                if (e.RazonSocial == 'Error') {
                    MostrarMensaje('No se pudo encontrar la Empresa. Ingrese la Razón Social.');
                    txtRazonSocial.focus();
                } else {
                    txtRazonSocial.value = e.RazonSocial;
                    txtDireccion.value = e.Direccion;   
                }
            }
            Desbloquear();
            return false;
        }
        function ObtenerPersonaReniec() {
            var txtDni = document.getElementById('<%=txtNumeroDocumento.ClientID%>');
            var dni = txtDni.value;
            Bloquear(100002);
            PageMethods.ObtenerPersonaReniec(dni, ObtenerPersonaReniecOk, fnLlamadaError);
            return false;
        }
        function ObtenerPersonaReniecOk(p) {
            var txtNombres = document.getElementById('<%=txtNombres.ClientID%>');
            var txtApellidoPaterno = document.getElementById('<%=txtApellidoPaterno.ClientID%>');
            var txtApellidoMaterno = document.getElementById('<%=txtApellidoMaterno.ClientID%>');
            debugger;
            if (p.CodVerificacion != null) {

                var nombreCompleto = p.Nombre.trim();
                var nombreCompletoArray = nombreCompleto.split(',');

                var apellidos = nombreCompletoArray[0].trim();
                var nombres = nombreCompletoArray[1].trim();

                var apellidoArray = apellidos.split(' ');
                var apellidoPaterno = apellidoArray[0].trim();
                var apellidoMaterno = apellidoArray[1].trim();

                txtNombres.value = nombres;
                txtApellidoPaterno.value = apellidoPaterno;
                txtApellidoMaterno.value = apellidoMaterno;
            } else {
                MostrarMensaje('No se pudo encontrar la Persona. Ingrese sus Nombres.');
                txtNombres.focus();
            }
            Desbloquear();
            return false;
        }
        //window.onload = function () {
            var ayuda = $('.imgAyuda');
            if (ayuda != null) {
                $('.imgAyuda').tooltip({ trigger: 'click' })
                    .on('mouseleave', function () {
                        $('.imgAyuda').tooltip('hide');
                    });
            }
        //};
    </script>
</body>
</html>