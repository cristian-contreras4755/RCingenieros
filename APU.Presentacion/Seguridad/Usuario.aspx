<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Usuario.aspx.cs" Inherits="APU.Presentacion.Seguridad.Usuario" EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="uc1" TagName="ucProcesando" Src="~/Controles/ucProcesando.ascx" %>
<%@ Register Src="~/Controles/ucCabecera.ascx" TagPrefix="uc1" TagName="ucCabecera" %>
<%@ Register Src="~/Controles/ucMenu.ascx" TagPrefix="uc1" TagName="ucMenu" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Usuarios</title>
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
        var hdnUsuarioId = document.getElementById('<%=hdnUsuarioId.ClientID%>');
        var txtNombres = document.getElementById('<%=txtNombres.ClientID%>');
        var txtApellidoPaterno = document.getElementById('<%=txtApellidoPaterno.ClientID%>');
        var txtApellidoMaterno = document.getElementById('<%=txtApellidoMaterno.ClientID%>');
        var ddlSexo = document.getElementById('<%=ddlSexo.ClientID%>');
        var ddlEstadoCivil = document.getElementById('<%=ddlEstadoCivil.ClientID%>');
        var txtCorreo = document.getElementById('<%=txtCorreo.ClientID%>');
        var txtTelefono = document.getElementById('<%=txtTelefono.ClientID%>');
        var txtCelular = document.getElementById('<%=txtCelular.ClientID%>');
        var ddlTipoDocumento = document.getElementById('<%=ddlTipoDocumento.ClientID%>');
        var txtNumeroDocumento = document.getElementById('<%=txtNumeroDocumento.ClientID%>');

        var txtCodigo = document.getElementById('<%=txtCodigo.ClientID%>');
        var txtLogin = document.getElementById('<%=txtLogin.ClientID%>');
        var txtContrasenia = document.getElementById('<%=txtContrasenia.ClientID%>');
        var ddlPerfil = document.getElementById('<%=ddlPerfil.ClientID%>');
        var ddlEmpresa = document.getElementById('<%=ddlEmpresa.ClientID%>');
        var ddlDepartamento = document.getElementById('<%=ddlDepartamento.ClientID%>');
        var ddlCargo = document.getElementById('<%=ddlCargo.ClientID%>');
        var txtCorreoTrabajo = document.getElementById('<%=txtCorreoTrabajo.ClientID%>');
        var txtTelefonoTrabajo = document.getElementById('<%=txtTelefonoTrabajo.ClientID%>');
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
    function VerUsuario(usuarioId) {
        PageMethods.ObtenerUsuario(usuarioId, ObtenerUsuarioOk, fnLlamadaError);
        return false;
    }
    function LimpiarUsuario() {
        var hdnUsuarioId = document.getElementById('<%=hdnUsuarioId.ClientID%>');
        var txtNombres = document.getElementById('<%=txtNombres.ClientID%>');
        var txtApellidoPaterno = document.getElementById('<%=txtApellidoPaterno.ClientID%>');
        var txtApellidoMaterno = document.getElementById('<%=txtApellidoMaterno.ClientID%>');
        var ddlSexo = document.getElementById('<%=ddlSexo.ClientID%>');
        var ddlEstadoCivil = document.getElementById('<%=ddlEstadoCivil.ClientID%>');
        var txtCorreo = document.getElementById('<%=txtCorreo.ClientID%>');
        var txtTelefono = document.getElementById('<%=txtTelefono.ClientID%>');
        var txtCelular = document.getElementById('<%=txtCelular.ClientID%>');
        var ddlTipoDocumento = document.getElementById('<%=ddlTipoDocumento.ClientID%>');
        var txtNumeroDocumento = document.getElementById('<%=txtNumeroDocumento.ClientID%>');

        var txtCodigo = document.getElementById('<%=txtCodigo.ClientID%>');
        var txtLogin = document.getElementById('<%=txtLogin.ClientID%>');
        var txtContrasenia = document.getElementById('<%=txtContrasenia.ClientID%>');
        var ddlPerfil = document.getElementById('<%=ddlPerfil.ClientID%>');
        var ddlEmpresa = document.getElementById('<%=ddlEmpresa.ClientID%>');
        var ddlTipoNegocio = document.getElementById('<%=ddlTipoNegocio.ClientID%>');
        var ddlDepartamento = document.getElementById('<%=ddlDepartamento.ClientID%>');
        var ddlCargo = document.getElementById('<%=ddlCargo.ClientID%>');
        var txtCorreoTrabajo = document.getElementById('<%=txtCorreoTrabajo.ClientID%>');
        var txtTelefonoTrabajo = document.getElementById('<%=txtTelefonoTrabajo.ClientID%>');
        var chkActivo = document.getElementById('<%=chkActivo.ClientID%>');

        hdnUsuarioId.value = 0;
        document.getElementById('lblTituloUsuario').innerText = 'Usuario';
        txtNombres.value = '';
        txtApellidoPaterno.value = '';
        txtApellidoMaterno.value = '';
        ddlSexo.value = 0;
        ddlEstadoCivil.value = 0;
        txtCorreo.value = '';
        txtTelefono.value = '';
        txtCelular.value = '';
        ddlTipoDocumento.value = 0;
        txtNumeroDocumento.value = '';

        txtCodigo.value = '';
        txtLogin.value = '';
        txtContrasenia.value = '';
        ddlPerfil.value = 0;
        ddlEmpresa.value = 0;
        ddlTipoNegocio.value = 0;
        ddlDepartamento.value = 0;
        ddlCargo.value = 0;
        txtCorreoTrabajo.value = '';
        txtTelefonoTrabajo.value = '';
        chkActivo.checked = true;

        var tabUsuario = $get('<%=tabUsuario.ClientID%>');
        tabUsuario.control.set_activeTabIndex(0);
    }
    function ObtenerUsuarioOk(u) {
        var modalDialog = $find('mpeUsuario');
        if (u != null) {
            modalDialog.show();

            var tabUsuario = $get('<%=tabUsuario.ClientID%>');
            tabUsuario.control.set_activeTabIndex(0);

            var hdnUsuarioId = document.getElementById('<%=hdnUsuarioId.ClientID%>');
            var txtNombres = document.getElementById('<%=txtNombres.ClientID%>');
            var txtApellidoPaterno = document.getElementById('<%=txtApellidoPaterno.ClientID%>');
            var txtApellidoMaterno = document.getElementById('<%=txtApellidoMaterno.ClientID%>');
            var ddlSexo = document.getElementById('<%=ddlSexo.ClientID%>');
            var ddlEstadoCivil = document.getElementById('<%=ddlEstadoCivil.ClientID%>');
            var txtCorreo = document.getElementById('<%=txtCorreo.ClientID%>');
            var txtTelefono = document.getElementById('<%=txtTelefono.ClientID%>');
            var txtCelular = document.getElementById('<%=txtCelular.ClientID%>');
            var ddlTipoDocumento = document.getElementById('<%=ddlTipoDocumento.ClientID%>');
            var txtNumeroDocumento = document.getElementById('<%=txtNumeroDocumento.ClientID%>');
            var imgFoto = document.getElementById('<%=imgFoto.ClientID%>');

            var txtCodigo = document.getElementById('<%=txtCodigo.ClientID%>');
            var txtLogin = document.getElementById('<%=txtLogin.ClientID%>');
            var txtContrasenia = document.getElementById('<%=txtContrasenia.ClientID%>');
            var ddlPerfil = document.getElementById('<%=ddlPerfil.ClientID%>');
            var ddlEmpresa = document.getElementById('<%=ddlEmpresa.ClientID%>');
            var ddlTipoNegocio = document.getElementById('<%=ddlTipoNegocio.ClientID%>');
            var ddlDepartamento = document.getElementById('<%=ddlDepartamento.ClientID%>');
            var ddlCargo = document.getElementById('<%=ddlCargo.ClientID%>');
            var txtCorreoTrabajo = document.getElementById('<%=txtCorreoTrabajo.ClientID%>');
            var txtTelefonoTrabajo = document.getElementById('<%=txtTelefonoTrabajo.ClientID%>');
            var chkActivo = document.getElementById('<%=chkActivo.ClientID%>');
            var hdnAgenciaId = document.getElementById('<%=hdnAgenciaId.ClientID%>');

            hdnUsuarioId.value = u.UsuarioId;
            document.getElementById('lblTituloUsuario').innerText = u.Nombres;
            txtNombres.value = u.Nombres;
            txtApellidoPaterno.value = u.ApellidoPaterno;
            txtApellidoMaterno.value = u.ApellidoMaterno;
            ddlSexo.value = u.SexoId;
            ddlEstadoCivil.value = u.EstadoCivilId;
            txtCorreo.value = u.Correo;
            txtTelefono.value = u.Telefono;
            txtCelular.value = u.Celular;
            ddlTipoDocumento.value = u.TipoDocumentoId;
            txtNumeroDocumento.value = u.NumeroDocumento;
            // imgFoto.src
            // var rutaFoto = '~/Archivos/Imagenes/Usuario/admin/foto07112017075449.jpg';
            var nombreFoto = u.Foto.replace('~/Archivos/Imagenes/Usuario/admin/', '');
            // alert(u.Foto);
            imgFoto.src = '../Archivos/Imagenes/Usuario/' + u.Login + '/' + nombreFoto;
            // alert(imgFoto.src);

            txtCodigo.value = u.Codigo;
            txtLogin.value = u.Login;
            txtContrasenia.value = u.Password;
            ddlPerfil.value = u.PerfilId;
            ddlEmpresa.value = u.EmpresaId;

            hdnAgenciaId.value = u.AgenciaId;

            if (u.EmpresaId > 0) {
                PageMethods.ObtenerAgencia(u.EmpresaId, ObtenerAgenciaOk, fnLlamadaError);
            }

            ddlTipoNegocio.value = u.TipoNegocioId;

            ddlDepartamento.value = u.DepartamentoId;
            ddlCargo.value = u.CargoId;
            txtCorreoTrabajo.value = u.CorreoTrabajo;
            txtTelefonoTrabajo.value = u.TelefonoTrabajo;
            chkActivo.checked = (u.Activo > 0);
        } else {
            modalDialog.hide();
        }
    }
    function InsertarUsuario() {
        var modalDialog = $find('mpeUsuario');

        modalDialog.show();
        LimpiarUsuario();

        return false;
    }
    function ValidarUsuario() {
        var tabUsuario = $get('<%=tabUsuario.ClientID%>');

        var hdnUsuarioId = document.getElementById('<%=hdnUsuarioId.ClientID%>');
        var txtNombres = document.getElementById('<%=txtNombres.ClientID%>');
        var txtApellidoPaterno = document.getElementById('<%=txtApellidoPaterno.ClientID%>');
        var txtApellidoMaterno = document.getElementById('<%=txtApellidoMaterno.ClientID%>');
        var ddlSexo = document.getElementById('<%=ddlSexo.ClientID%>');
        var ddlEstadoCivil = document.getElementById('<%=ddlEstadoCivil.ClientID%>');
        var txtCorreo = document.getElementById('<%=txtCorreo.ClientID%>');
        var txtTelefono = document.getElementById('<%=txtTelefono.ClientID%>');
        var txtCelular = document.getElementById('<%=txtCelular.ClientID%>');
        var ddlTipoDocumento = document.getElementById('<%=ddlTipoDocumento.ClientID%>');
        var txtNumeroDocumento = document.getElementById('<%=txtNumeroDocumento.ClientID%>');

        var txtCodigo = document.getElementById('<%=txtCodigo.ClientID%>');
        var txtLogin = document.getElementById('<%=txtLogin.ClientID%>');
        var txtContrasenia = document.getElementById('<%=txtContrasenia.ClientID%>');
        var ddlPerfil = document.getElementById('<%=ddlPerfil.ClientID%>');
        var ddlEmpresa = document.getElementById('<%=ddlEmpresa.ClientID%>');
        var ddlTipoNegocio = document.getElementById('<%=ddlTipoNegocio.ClientID%>');
        var ddlDepartamento = document.getElementById('<%=ddlDepartamento.ClientID%>');
        var ddlCargo = document.getElementById('<%=ddlCargo.ClientID%>');
        var txtCorreoTrabajo = document.getElementById('<%=txtCorreoTrabajo.ClientID%>');
        var txtTelefonoTrabajo = document.getElementById('<%=txtTelefonoTrabajo.ClientID%>');
        var chkActivo = document.getElementById('<%=chkActivo.ClientID%>');

        if (txtNombres.value.trim() == '') {
            tabUsuario.control.set_activeTabIndex(0);
            return MostrarValidacion(txtNombres, 'Ingrese los Nombres.');
        }
        if (txtApellidoPaterno.value.trim() == '') {
            tabUsuario.control.set_activeTabIndex(0);
            return MostrarValidacion(txtApellidoPaterno, 'Ingrese el Apellido Paterno.');
        }
        if (txtApellidoMaterno.value.trim() == '') {
            tabUsuario.control.set_activeTabIndex(0);
            return MostrarValidacion(txtApellidoMaterno, 'Ingrese el Apellido Materno.');
        }
        if (ddlSexo.value.trim() == '0') {
            tabUsuario.control.set_activeTabIndex(0);
            return MostrarValidacion(ddlSexo, 'Seleccione el Género.');
        }
        if (ddlEstadoCivil.value.trim() == '0') {
            tabUsuario.control.set_activeTabIndex(0);
            return MostrarValidacion(ddlEstadoCivil, 'Seleccione el Estado Civil.');
        }
        if (!ValidarCorreo(txtCorreo.value.toLowerCase())) {
            tabUsuario.control.set_activeTabIndex(0);
            return MostrarValidacion(txtCorreo, 'Ingrese el Correo.');
        }
        if (txtTelefono.value.trim() == '') {
            tabUsuario.control.set_activeTabIndex(0);
            return MostrarValidacion(txtTelefono, 'Ingrese el Teléfono.');
        }
        if (txtCelular.value.trim() == '') {
            tabUsuario.control.set_activeTabIndex(0);
            return MostrarValidacion(txtCelular, 'Ingrese el Celular.');
        }
        if (ddlTipoDocumento.value.trim() == '0') {
            tabUsuario.control.set_activeTabIndex(0);
            return MostrarValidacion(ddlTipoDocumento, 'Seleccione el Tipo de Documento.');
        }
        if (txtNumeroDocumento.value.trim() == '') {
            tabUsuario.control.set_activeTabIndex(0);
            return MostrarValidacion(txtNumeroDocumento, 'Ingrese el Número de Documento.');
        }

        //if (txtCodigo.value.trim() == '') {
        //    return MostrarValidacion(txtCodigo, 'Ingrese el Código.');
        //}
        if (txtLogin.value.trim() == '') {
            tabUsuario.control.set_activeTabIndex(1);
            return MostrarValidacion(txtLogin, 'Ingrese el Login.');
        }
        if (txtContrasenia.value.trim() == '') {
            tabUsuario.control.set_activeTabIndex(1);
            return MostrarValidacion(txtContrasenia, 'Ingrese la Contraseña.');
        }
        if (ddlPerfil.value.trim() == '0') {
            tabUsuario.control.set_activeTabIndex(1);
            return MostrarValidacion(ddlPerfil, 'Seleccione el Perfil.');
        }
        //if (ddlEmpresa.value.trim() == '0') {
        //    return MostrarValidacion(ddlEmpresa, 'Seleccione la Empresa.');
        //}
        if (ddlTipoNegocio.value.trim() == '0') {
            tabUsuario.control.set_activeTabIndex(1);
            return MostrarValidacion(ddlTipoNegocio, 'Seleccione un Tipo de Negocio.');
        }
        //if (ddlDepartamento.value.trim() == '0') {
        //    return MostrarValidacion(ddlDepartamento, 'Seleccione el Departamento.');
        //}
        //if (ddlCargo.value.trim() == '0') {
        //    return MostrarValidacion(ddlCargo, 'Seleccione el Cargo.');
        //}
        //if (txtCorreoTrabajo.value.trim() == '') {
        //    return MostrarValidacion(txtCorreoTrabajo, 'Ingrese el Correo de Trabajo.');
        //}
        //if (txtTelefonoTrabajo.value.trim() == '') {
        //    return MostrarValidacion(txtTelefonoTrabajo, 'Ingrese el Teléfono de Trabajo.');
        //}
    }
    function SeleccionarEmpresa() {
        var ddlEmpresa = document.getElementById('<%=ddlEmpresa.ClientID%>');
        var emp = ddlEmpresa.value;
        if (emp > 0) {
            PageMethods.ObtenerAgencia(emp, ObtenerAgenciaOk, fnLlamadaError);
        }
    }
    function ObtenerAgenciaOk(a) {
        var ddlAgencia = document.getElementById('<%=ddlAgencia.ClientID%>');
        if (a.length > 0) {
            LlenarCombo(ddlAgencia, a, 'AgenciaId', 'Nombre');
            AgregarOptionACombo(ddlAgencia, Seleccione_value, Seleccione);

            var hdnAgenciaId = document.getElementById('<%=hdnAgenciaId.ClientID%>');
            // if (hdnAgenciaId.value > 0) {
                ddlAgencia.value = hdnAgenciaId.value;
            // }
        }
    }
    function SeleccionarAgencia() {
        var hdnAgenciaId = document.getElementById('<%=hdnAgenciaId.ClientID%>');
        var ddlAgencia = document.getElementById('<%=ddlAgencia.ClientID%>');

        if (ddlAgencia.value > 0) {
            hdnAgenciaId.value = ddlAgencia.value;    
        }
    }
    function GuardarUsuario() {
        var usuarioId = document.getElementById('hdnUsuarioId').value;

        var nombres = document.getElementById('ctl05_ctl00_txtNombres').value;
        var apellidoPaterno = document.getElementById('ctl05_ctl00_txtApellidoPaterno').value;
        var apellidoMaterno = document.getElementById('ctl05_ctl00_txtApellidoMaterno').value;
        var sexoId = document.getElementById('ctl05_ctl00_ddlSexo').value;
        var estadoCivilId = document.getElementById('ctl05_ctl00_ddlEstadoCivil').value;
        var correo = document.getElementById('ctl05_ctl00_txtCorreo').value;
        var telefono = document.getElementById('ctl05_ctl00_txtTelefono').value;
        var celular = document.getElementById('ctl05_ctl00_txtCelular').value;
        var tipoDocumentoId = document.getElementById('ctl05_ctl00_ddlTipoDocumento').value;
        var numeroDocumento = document.getElementById('ctl05_ctl00_txtNumeroDocumento').value;

        var codigo = document.getElementById('ctl05_ctl01_txtCodigo').value;
        var login = document.getElementById('ctl05_ctl01_txtLogin').value;
        var contrasenia = document.getElementById('ctl05_ctl01_txtContrasenia').value;
        var perfilId = document.getElementById('ctl05_ctl01_ddlPerfil').value;
        var empresaId = document.getElementById('ctl05_ctl01_ddlEmpresa').value;
        var departamentoId = document.getElementById('ctl05_ctl01_ddlDepartamento').value;
        var cargoId = document.getElementById('ctl05_ctl01_ddlCargo').value;
        var correoTrabajo = document.getElementById('ctl05_ctl01_txtCorreoTrabajo').value;
        var telefonoTrabajo = document.getElementById('ctl05_ctl01_txtTelefonoTrabajo').value;
        var activo = document.getElementById('ctl05_ctl01_chkActivo').checked ? 1 : 0;

        Bloquear();
        PageMethods.GuardarUsuario(usuarioId, nombres, apellidoPaterno, apellidoMaterno, sexoId, estadoCivilId, correo, telefono, celular, tipoDocumentoId, numeroDocumento,
            codigo, login, contrasenia, perfilId, empresaId, departamentoId, cargoId, correoTrabajo, telefonoTrabajo, activo, GuardarUsuarioOk, fnLlamadaError);
    }
    function GuardarUsuarioOk(res) {
        var modalDialog = $find('mpeUsuario');

        var arr = res.split('@');
        var usuarioId = arr[0];
        var mensaje = arr[1];

        if (usuarioId > 0) {
            alert('Se registró la información del usuario correctamente.');
            modalDialog.hide();
            LimpiarUsuario();
        } else {
            alert(mensaje);
        }
        Desbloquear();
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
    function CargarLogo() {
        var hdnUsuarioImagen = document.getElementById('<%=hdnUsuarioImagen.ClientID%>');
        MostrarMensaje('Se adjuntó el archivo correctamente');
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
                            <asp:Label CssClass="lblTitulo" Width="100%" runat="server">USUARIO</asp:Label>
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
                                        <asp:LinkButton ID="imgAnadir" ForeColor="#404040" runat="server" ToolTip="" OnClientClick="return InsertarUsuario();">
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
                                            <th style="width: 10%; cursor: pointer; text-decoration: underline;">Login</th>
                                            <th style="width: 20%; cursor: pointer; text-decoration: underline;">Nombre</th>
                                            <th style="width: 8%; cursor: pointer; text-decoration: underline;">Telef. Trabajo</th>
                                            <th style="width: 15%; cursor: pointer; text-decoration: underline;">Correo Trabajo</th>
                                            <th style="width: 12%; cursor: pointer; text-decoration: underline;">Departamento</th>
                                            <th style="width: 15%; cursor: pointer; text-decoration: underline;">Cargo</th>
                                            <th style="width: 15%; cursor: pointer; text-decoration: underline;">Director</th>
                                        </tr>
                                    </thead>
                                </table>
                            </div>
                            <asp:UpdatePanel ID="divData" runat="server" UpdateMode="Conditional" style="overflow: scroll; width: 1200px; height: 400px; margin: 0 0 0px 0px;" onscroll="Onscrollfnction();">
                                <ContentTemplate>
                                    <asp:GridView id="grvUsuario" AutoGenerateColumns="False" Width="2000px" RowStyle-Wrap="True" runat="server" OnRowDataBound="grvUsuario_RowDataBound">
                                        <HeaderStyle CssClass="filaCabeceraGrid"></HeaderStyle>
                                        <RowStyle CssClass="filaImparGrid"></RowStyle>
                                        <AlternatingRowStyle CssClass="filaParGrid"></AlternatingRowStyle>
                                        <Columns>
                                            <%-- 0: Acciones --%>
                                            <asp:TemplateField HeaderText="Acciones">
                                                <ItemStyle Width="5%" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:ImageButton runat="server" ImageUrl="~/Imagenes/Iconos/editar.png" CssClass="imgButton" OnClientClick='<%# "return VerUsuario(" + Eval("UsuarioId") + ");" %>' ToolTip="Editar" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- 1: Login --%>
                                            <asp:TemplateField HeaderText="Login">
                                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <%# Eval("Login")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--  2: Nombre --%>
                                            <asp:TemplateField HeaderText="Nombre">
                                                <ItemStyle Width="20%" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <%# Eval("Nombres").ToString().Trim() + ", " + Eval("ApellidoPaterno").ToString().Trim() + " " + Eval("ApellidoMaterno").ToString().Trim() %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- 3: Telef. Trabajo --%>
                                            <asp:TemplateField HeaderText="Telef. Trabajo">
                                                <ItemStyle Width="8%" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <%# Eval("TelefonoTrabajo")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- 4: Correo Trabajo --%>
                                            <asp:TemplateField HeaderText="Correo Trabajo">
                                                <ItemStyle Width="15%" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <%# Eval("CorreoTrabajo")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- 5: Departamento --%>
                                            <asp:TemplateField HeaderText="Departamento">
                                                <ItemStyle Width="12%" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <%# Eval("Departamento")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- 6: Cargo --%>
                                            <asp:TemplateField HeaderText="Cargo">
                                                <ItemStyle Width="15%" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <%# Eval("Cargo")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- 7: Director --%>
                                            <asp:TemplateField HeaderText="Director">
                                                <ItemStyle Width="15%" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <%# Eval("Director")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- 8: UsuarioId --%>
                                            <asp:TemplateField>
                                                <ItemStyle CssClass="columnaOcultaGrid" />
                                                <ItemTemplate>
                                                    <%# Eval("UsuarioId")%>
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
                                        <td style="text-align: right; width: 60%;">
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
            <asp:Panel ID="pnlUsuario" runat="server" CssClass="pnlModal" style="display: none;">
                <table style="width: 100%;">
                    <tr>
                        <td id="lblTituloUsuario" class="lblTituloPopup">Empleado</td>
                    </tr>
                    <tr>
                        <td>
                            <input type="hidden" id="hdnUsuarioId" value="0" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%;">
                            <ajaxToolkit:TabContainer id="tabUsuario" runat="server" OnClientActiveTabChanged="" Height="350px" Width="600px" ActiveTabIndex="0" OnDemand="False" AutoPostBack="false"
                                                      TabStripPlacement="Top" CssClass="ajax__tab_xp" ScrollBars="None" UseVerticalStripPlacement="False" VerticalStripWidth="120px">
                                <ajaxToolkit:TabPanel id="tabUsuarioInformacionPersonal" runat="server" HeaderText="Informaci&oacute;n Personal" Enabled="true" ScrollBars="Auto" OnDemandMode="Always">
                                    <ContentTemplate>
                                        <table style="width: 100%;">
                                            <tr>
                                                <td class="lblStandar">Nombres</td>
                                                <td>
                                                    <asp:TextBox id="txtNombres" CssClass="txtStandar" Width="60%" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="lblStandar">Apellido Paterno</td>
                                                <td>
                                                    <asp:TextBox id="txtApellidoPaterno" CssClass="txtStandar" Width="60%" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="lblStandar">Apellido Materno</td>
                                                <td>
                                                    <asp:TextBox id="txtApellidoMaterno" CssClass="txtStandar" Width="60%" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="lblStandar">Sexo</td>
                                                <td>
                                                    <asp:DropDownList id="ddlSexo" CssClass="ddlStandar" Width="60%" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="lblStandar">Estado Civil</td>
                                                <td>
                                                    <asp:DropDownList id="ddlEstadoCivil" CssClass="ddlStandar" Width="60%" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="lblStandar">Correo</td>
                                                <td>
                                                    <asp:TextBox id="txtCorreo" CssClass="txtStandar" Width="60%" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="lblStandar">Tel&eacute;fono</td>
                                                <td>
                                                    <asp:TextBox id="txtTelefono" CssClass="txtStandar" Width="60%" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="lblStandar">Celular</td>
                                                <td>
                                                    <asp:TextBox id="txtCelular" CssClass="txtStandar" Width="60%" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="lblStandar">Tipo Documento</td>
                                                <td>
                                                    <asp:DropDownList id="ddlTipoDocumento" CssClass="ddlStandar" Width="60%" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="lblStandar">N&uacute;mero Documento</td>
                                                <td>
                                                    <asp:TextBox id="txtNumeroDocumento" CssClass="txtStandar" Width="60%" runat="server" />
                                                </td>
                                            </tr>
                                            <tr style="display: none;">
                                                <td class="lblStandar">Foto</td>
                                                <td>
                                                    <ajaxToolkit:AsyncFileUpload ID="fuFotoUsuario" runat="server" PersistFile="true" CompleteBackColor="Transparent" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="lblStandar">Logo</td>
                                                <td>
                                                    <ajaxToolkit:AsyncFileUpload ID="fuUsuario" runat="server" PersistFile="true" CompleteBackColor="Transparent" OnClientUploadComplete="CargarLogo" UploaderStyle="Modern" UploadingBackColor="#CCFFFF" ThrobberID="lblCargaLogo" />
                                                    <asp:Label ID="lblCargaLogo" runat="server" Width="20%"  Style="display: none;"><asp:Image alt="" ImageUrl="~/Imagenes/uploading.gif" runat="server"/></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td>
                                                    <asp:Image id="imgFoto" runat="server" CssClass="imgAvatarRedondo" />
                                                    <input type="hidden" id="hdnUsuarioImagen" runat="server" value="" />
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>
                                <ajaxToolkit:TabPanel id="tabUsuarioConfiguracion" runat="server" HeaderText="Configuraci&oacute;n" Enabled="true" ScrollBars="Auto" OnDemandMode="Always">
                                    <ContentTemplate>
                                        <table style="width: 100%;">
                                            <tr>
                                                <td class="lblStandar">Login</td>
                                                <td>
                                                    <asp:TextBox id="txtLogin" CssClass="txtStandar" Width="60%" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="lblStandar">Contrase&ntilde;a</td>
                                                <td>
                                                    <asp:TextBox id="txtContrasenia" CssClass="txtStandar" Width="60%" runat="server" />
                                                    <input type="button" id="btnMostrarContrasenia" value="Mostrar" class="btnStandar" onclick="MostrarContrasenia('<%=txtContrasenia.ClientID%>');" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="lblStandar">C&oacute;digo</td>
                                                <td>
                                                    <asp:TextBox id="txtCodigo" CssClass="txtStandar" Width="60%" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="lblStandar">Perfil</td>
                                                <td>
                                                    <asp:DropDownList id="ddlPerfil" CssClass="ddlStandar" Width="60%" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="lblStandar">Empresa</td>
                                                <td>
                                                    <asp:DropDownList id="ddlEmpresa" CssClass="ddlStandar" Width="60%" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="lblStandar">Tipo Negocio</td>
                                                <td>
                                                    <asp:DropDownList id="ddlTipoNegocio" CssClass="ddlStandar" Width="60%" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="lblStandar">Agencia</td>
                                                <td>
                                                    <asp:DropDownList id="ddlAgencia" CssClass="ddlStandar" Width="60%" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="lblStandar">Departamento</td>
                                                <td>
                                                    <asp:DropDownList id="ddlDepartamento" CssClass="ddlStandar" Width="60%" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="lblStandar">Cargo</td>
                                                <td>
                                                    <asp:DropDownList id="ddlCargo" CssClass="ddlStandar" Width="60%" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="lblStandar">Correo</td>
                                                <td>
                                                    <asp:TextBox id="txtCorreoTrabajo" CssClass="txtStandar" Width="60%" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="lblStandar">Tel&eacute;fono</td>
                                                <td>
                                                    <asp:TextBox id="txtTelefonoTrabajo" CssClass="txtStandar" Width="60%" runat="server" />
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
                            </ajaxToolkit:TabContainer>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: center;">
                            <asp:Button ID="btnGuardarUsuario" runat="server" CssClass="btnStandar" Text="Guardar" Width="30%" OnClientClick="return ValidarUsuario();" OnClick="btnGuardarUsuario_OnClick" />
                            <asp:Button ID="btnCancelarUsuario" runat="server" CssClass="btnStandar" Text="Cancelar" Width="30%" />
                            <asp:button id="hButton" runat="server" style="display:none;" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <ajaxToolkit:ModalPopupExtender ID="mpeUsuario" PopupControlID="pnlUsuario" TargetControlID="hButton" CancelControlID="btnCancelarUsuario" Drag="True" DropShadow="True" BackgroundCssClass="FondoAplicacion" ClientIDMode="Static" runat="server" PopupDragHandleControlID="lblTituloUsuario"></ajaxToolkit:ModalPopupExtender>
            <ajaxToolkit:RoundedCornersExtender ID="rceUsuario" runat="server" BehaviorID="rcbUsuario" TargetControlID="pnlUsuario" Radius="6" Corners="All" />
            <input type="hidden" id="hdnAgenciaId" runat="server" value="0" />
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
            //var divData = principal - 20;
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
            if ((postBackElement.id == 'ddlNumeroRegistros') || (postBackElement.id == 'btnPrimero') || (postBackElement.id == 'btnAnterior') || (postBackElement.id == 'btnSiguiente') ||
                (postBackElement.id == 'btnUltimo') || (postBackElement.id == 'btnPaginacion') || (postBackElement.id == 'btnGuardarUsuario')) {
                // Bloqueo Pantalla
                Bloquear(100002);
            }
        }
        function EndRequest(sender, args) {
            debugger;
            if ((postBackElement.id == 'ddlNumeroRegistros') || (postBackElement.id == 'btnPrimero') || (postBackElement.id == 'btnAnterior') || (postBackElement.id == 'btnSiguiente') ||
                (postBackElement.id == 'btnUltimo') || (postBackElement.id == 'btnPaginacion') || (postBackElement.id == 'btnGuardarUsuario')) {
                // Desbloquear Pantalla
                Desbloquear();
                RedimensionarGrid();
            }
        }
        function grvUsuario_OnDnlClick(row) {
            debugger;
            var indiceUsuarioId = 7;
            var usuarioIdRow = RetornarCeldaValor(row, indiceUsuarioId);
            VerUsuario(usuarioIdRow);
        }
    </script>
</body>
</html>