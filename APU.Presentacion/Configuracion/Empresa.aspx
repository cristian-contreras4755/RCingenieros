<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Empresa.aspx.cs" Inherits="APU.Presentacion.Configuracion.Empresa" EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="uc1" TagName="ucProcesando" Src="~/Controles/ucProcesando.ascx" %>
<%@ Register Src="~/Controles/ucCabecera.ascx" TagPrefix="uc1" TagName="ucCabecera" %>
<%@ Register Src="~/Controles/ucMenu.ascx" TagPrefix="uc1" TagName="ucMenu" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Empresa</title>
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
        var hdnEmpresaId = document.getElementById('<%=hdnEmpresaId.ClientID%>');
        var txtRazonSocial = document.getElementById('<%=txtRazonSocial.ClientID%>');
        var chkActivo = document.getElementById('<%=chkActivo.ClientID%>');
    }
    function Buscar() {
        var grid = document.getElementById('grvEmpresa');
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
    function VerEmpresa(empresaId) {
        Bloquear();
        PageMethods.ObtenerEmpresa(empresaId, ObtenerEmpresaOk, fnLlamadaError);
        return false;
    }
    function LimpiarEmpresa() {
        var hdnEmpresaId = document.getElementById('<%=hdnEmpresaId.ClientID%>');
        var ddlTipoDocumento = document.getElementById('<%=ddlTipoDocumento.ClientID%>');
        var txtRuc = document.getElementById('<%=txtRuc.ClientID%>');
        var txtRazonSocial = document.getElementById('<%=txtRazonSocial.ClientID%>');
        var txtDescripcion = document.getElementById('<%=txtDescripcion.ClientID%>');
        var txtCiiu = document.getElementById('<%=txtCiiu.ClientID%>');
        var chkActivo = document.getElementById('<%=chkActivo.ClientID%>');
        var ddlPais = document.getElementById('<%=ddlPais.ClientID%>');
        var ddlDepartamento = document.getElementById('<%=ddlDepartamento.ClientID%>');
        var ddlProvincia = document.getElementById('<%=ddlProvincia.ClientID%>');
        var ddlDistrito = document.getElementById('<%=ddlDistrito.ClientID%>');
        var txtCiudad = document.getElementById('<%=txtCiudad.ClientID%>');
        var txtDireccion = document.getElementById('<%=txtDireccion.ClientID%>');
        var lblModificadoPor = document.getElementById('<%=lblModificadoPor.ClientID%>');
        var lblFechaModificacion = document.getElementById('<%=lblFechaModificacion.ClientID%>');

        hdnEmpresaId.value = 0;
        document.getElementById('lblTituloEmpresa').innerText = 'Empresa';
        txtRuc.value = '';
        txtRazonSocial.value = '';
        txtDescripcion.value = '';
        txtCiiu.value = '';
        chkActivo.checked = true;
        ddlPais.value = 0;
        ddlDepartamento.value = 0;
        ddlProvincia.value = 0;
        ddlDistrito.value = 0;
        txtCiudad.value = '';
        txtDireccion.value = '';
        lblModificadoPor.innerText = '';
        lblFechaModificacion.innerText = '';

        var tabEmpresa = $get('<%=tabEmpresa.ClientID%>');
        tabEmpresa.control.set_activeTabIndex(0);
        txtRuc.focus();
    }
    function ObtenerEmpresaOk(e) {
        var modalDialog = $find('mpeEmpresa');
        if (e != null) {
            modalDialog.show();

            var tabEmpresa = $get('<%=tabEmpresa.ClientID%>');
            tabEmpresa.control.set_activeTabIndex(0);

            var hdnEmpresaId = document.getElementById('<%=hdnEmpresaId.ClientID%>');
            var ddlTipoDocumento = document.getElementById('<%=ddlTipoDocumento.ClientID%>');
            var txtRuc = document.getElementById('<%=txtRuc.ClientID%>');
            var txtRazonSocial = document.getElementById('<%=txtRazonSocial.ClientID%>');
            var txtDescripcion = document.getElementById('<%=txtDescripcion.ClientID%>');
            var txtCiiu = document.getElementById('<%=txtCiiu.ClientID%>');
            var chkActivo = document.getElementById('<%=chkActivo.ClientID%>');
            var ddlPais = document.getElementById('<%=ddlPais.ClientID%>');
            var ddlDepartamento = document.getElementById('<%=ddlDepartamento.ClientID%>');
            var ddlProvincia = document.getElementById('<%=ddlProvincia.ClientID%>');
            var ddlDistrito = document.getElementById('<%=ddlDistrito.ClientID%>');
            var txtCiudad = document.getElementById('<%=txtCiudad.ClientID%>');
            var txtDireccion = document.getElementById('<%=txtDireccion.ClientID%>');
            var hdnEmpresaImagen = document.getElementById('<%=hdnEmpresaImagen.ClientID%>');
            var lblModificadoPor = document.getElementById('<%=lblModificadoPor.ClientID%>');
            var lblFechaModificacion = document.getElementById('<%=lblFechaModificacion.ClientID%>');

            hdnEmpresaId.value = e.EmpresaId;
            ddlTipoDocumento.value = e.TipoDocumentoId;
            txtRuc.value = e.NumeroDocumento;
            document.getElementById('lblTituloEmpresa').innerText = e.RazonSocial;
            txtRazonSocial.value = e.RazonSocial;
            txtDescripcion.value = e.Descripcion;
            txtCiiu.value = e.Ciiu;
            chkActivo.checked = (e.Activo > 0);
            ddlPais.value = e.PaisId;
            ddlDepartamento.value = e.DepartamentoId;

            if (e.DepartamentoId > 0) {
                ObtenerProvincia(e.DepartamentoId, e.ProvinciaId, e.DistritoId);
            }

            ddlProvincia.value = e.ProvinciaId;
            ddlDistrito.value = e.DistritoId;
            txtCiudad.value = e.Ciudad;
            txtDireccion.value = e.Direccion;

            hdnEmpresaImagen.value = e.Imagen;

            lblModificadoPor.innerText = e.UsuarioModificacion;
            lblFechaModificacion.innerText = FormatoFecha(e.FechaModificacion);

            // Cargar Agencias
            document.getElementById('btnCargarAgencia').click();
            // Cargar Almacenes
            document.getElementById('btnCargarAlmacen').click();
        } else {
            modalDialog.hide();
        }
        Desbloquear();
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
    function InsertarEmpresa() {
        var modalDialog = $find('mpeEmpresa');

        modalDialog.show();
        LimpiarEmpresa();

        return false;
    }
    function ValidarEmpresa() {
        var hdnEmpresaId = document.getElementById('<%=hdnEmpresaId.ClientID%>');
        var ddlTipoDocumento = document.getElementById('<%=ddlTipoDocumento.ClientID%>');
        var txtRuc = document.getElementById('<%=txtRuc.ClientID%>');
        var txtRazonSocial = document.getElementById('<%=txtRazonSocial.ClientID%>');
        var chkActivo = document.getElementById('<%=chkActivo.ClientID%>');

        if (ddlTipoDocumento.value.trim() == '0') {
            return MostrarValidacion(ddlTipoDocumento, 'Seleccione el Tipo de Documento.');
        }
        var valRuc = ValidarRuc(txtRuc.value.trim());
        if (!valRuc) {
            return MostrarValidacion(txtRuc, 'El RUC ingresado no es válido.');
        }
        if (txtRazonSocial.value.trim() == '') {
            return MostrarValidacion(txtRazonSocial, 'Ingrese la Razón Social de la Empresa.');
        }
    }
    function GuardarEmpresa() {
        var nombres = document.getElementById('ctl05_ctl00_txtNombres').value;
        var empresaId = document.getElementById('ctl05_ctl01_ddlEmpresa').value;
        var activo = document.getElementById('ctl05_ctl01_chkActivo').checked ? 1 : 0;

        Bloquear();
        PageMethods.GuardarEmpresa(empresaId, nombres, activo, GuardarEmpresaOk, fnLlamadaError);
    }
    function GuardarEmpresaOk(res) {
        var modalDialog = $find('mpeEmpresa');
        var arr = res.split('@');
        var empresaId = arr[0];
        var mensaje = arr[1];

        if (empresaId > 0) {
            alert('Se registró la información de la Empresa correctamente.');
            modalDialog.hide();
            LimpiarEmpresa();
        } else {
            alert(mensaje);
        }
        Desbloquear();
    }
    function ObtenerEmpresaSunat() {
        //var nombres = document.getElementById('ctl05_ctl00_txtNombres').value;
        //var empresaId = document.getElementById('ctl05_ctl01_ddlEmpresa').value;
        //var activo = document.getElementById('ctl05_ctl01_chkActivo').checked ? 1 : 0;
        var txtRuc = document.getElementById('<%=txtRuc.ClientID%>');
        var ruc = txtRuc.value;
        Bloquear(100002);
        PageMethods.ObtenerEmpresaSunat(ruc, ObtenerEmpresaSunatOk, fnLlamadaError);
        return false;
    }
    function ObtenerEmpresaSunatOk(e) {
        if (e != null) {
            //var empresa = e.split('@');
            var txtRazonSocial = document.getElementById('<%=txtRazonSocial.ClientID%>');
            var txtCiiu = document.getElementById('<%=txtCiiu.ClientID%>');
            var txtDireccion = document.getElementById('<%=txtDireccion.ClientID%>');

            //txtRazonSocial.value = empresa[1];
            //if (empresa.length > 4) {
            //    txtDireccion.value = empresa[3];
            //    txtCiiu.value = empresa[4];
            //} else {
            //    txtDireccion.value = empresa[2];
            //    txtCiiu.value = empresa[3];
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
    function CargarLogo() {
        var hdnEmpresaImagen = document.getElementById('<%=hdnEmpresaImagen.ClientID%>');
        MostrarMensaje('Se adjunto el archivo correctamente');
        //MostrarMensaje('Ya cargo el logo: ' + hdnEmpresaImagen.value);

    }
    function VerAgencia(empresaId) {
        Bloquear();
        window.location = 'Agencia.aspx?EmpresaId=' + empresaId;
    }
    function VerAlmacen(empresaId) {
        Bloquear();
        window.location = 'Almacen.aspx?EmpresaId=' + empresaId;
    }
    function fnLlamadaError(excepcion) {
        alert('Ha ocurrido un error interno: ' + excepcion.get_message());
        Desbloquear();
    }
    function Cancelar() {
        var modalDialog = $find('mpeEmpresa');
        modalDialog.hide();
        LimpiarEmpresa();
        Desbloquear();
    }
    function Cerrar() {
        var modalDialog = $find('mpeEmpresa');
        modalDialog.hide();
    }
</script>
    <form id="frmEmpresa" runat="server" DefaultButton="btnDefault">
    <asp:Button id="btnDefault" OnClientClick="return false;" style="display: none;" runat="server" />
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
        <table style="width: 100%;">
            <tr>
                <td style="text-align: center;" colspan="5">
                    <asp:Label CssClass="lblTitulo" Width="100%" runat="server">EMPRESA</asp:Label>
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
                                <asp:LinkButton ID="imgAnadir" ForeColor="#404040" runat="server" ToolTip="" OnClientClick="return InsertarEmpresa();">
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
                                <th style="width: 10%; cursor: default; text-decoration: none;">Acciones</th>
                                <th style="width: 15%; cursor: pointer; text-decoration: underline;">RUC</th>
                                <th style="width: 30%; cursor: pointer; text-decoration: underline;">Raz&oacute;n Social</th>
                                <th style="width: 35%; cursor: pointer; text-decoration: underline;">Direcci&oacute;n</th>
                                <th style="width: 10%; cursor: pointer; text-decoration: underline;">Activo</th>
                            </tr>
                            </thead>
                        </table>
                    </div>
                    <asp:UpdatePanel ID="divData" runat="server" UpdateMode="Conditional" style="overflow: scroll; width: 1200px; height: 420px; margin: 0 0 0px 0px;" onscroll="Onscrollfnction();">
                        <ContentTemplate>
                            <asp:GridView id="grvEmpresa" AutoGenerateColumns="False" Width="1500px" RowStyle-Wrap="True" runat="server" OnRowDataBound="grvEmpresa_RowDataBound">
                                <HeaderStyle CssClass="filaCabeceraGrid"></HeaderStyle>
                                <RowStyle CssClass="filaImparGrid"></RowStyle>
                                <AlternatingRowStyle CssClass="filaParGrid"></AlternatingRowStyle>
                                <Columns>
                                    <%-- 0: Acciones --%>
                                    <asp:TemplateField HeaderText="Acciones">
                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ImageUrl="~/Imagenes/Iconos/editar.png" CssClass="imgButton" OnClientClick='<%# "return VerEmpresa(" + Eval("EmpresaId") + ");" %>' ToolTip="Editar Empresa" />&nbsp;
                                            <asp:ImageButton runat="server" ImageUrl="~/Imagenes/Iconos/agencia.png" CssClass="imgButton" OnClientClick='<%# "return VerAgencia(" + Eval("EmpresaId") + ");" %>' ToolTip="Ver Agencias" />&nbsp;
                                            <asp:ImageButton runat="server" ImageUrl="~/Imagenes/Iconos/almacen.png" CssClass="imgButton" OnClientClick='<%# "return VerAlmacen(" + Eval("EmpresaId") + ");" %>' ToolTip="Ver Almacenes" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--  1: NumeroDocumento --%>
                                    <asp:TemplateField HeaderText="NumeroDocumento">
                                        <ItemStyle Width="15%" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <%# Eval("NumeroDocumento").ToString().Trim() %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- 2: RazonSocial --%>
                                    <asp:TemplateField HeaderText="Razón Social">
                                        <ItemStyle Width="30%" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <%# Eval("RazonSocial").ToString().Trim() %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- 3: Dirección --%>
                                    <asp:TemplateField HeaderText="Dirección">
                                        <ItemStyle Width="35%" HorizontalAlign="Center" />
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
                            <asp:AsyncPostBackTrigger ControlID="ddlNumeroRegistros" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="btnPaginacion" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnGuardarEmpresa" EventName="Click" />
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
    <asp:Panel ID="pnlEmpresa" runat="server" CssClass="pnlModal" style="display: none;">
        <table style="width: 100%;">
            <tr id="lblTituloPopupEmpresa" class="lblTituloPopup">
                <td style="width: 20%;" class="lblTituloPopup">&nbsp;</td>
                <td id="lblTituloEmpresa" class="lblTituloPopup" style="width: 60%">Empresa</td>
                <td class="lblTituloPopup" style="width: 20%; text-align: right;">
                    <a onclick="return Cerrar();" style="color: #FFFFFF; text-decoration: none; cursor: pointer;">Cerrar X</a>&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <input type="hidden" id="hdnEmpresaId" value="0" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="3" style="width: 100%;">
                    <ajaxToolkit:TabContainer id="tabEmpresa" runat="server" OnClientActiveTabChanged="" Height="350px" Width="800px" ActiveTabIndex="0" OnDemand="False" AutoPostBack="false"
                                              TabStripPlacement="Top" CssClass="ajax__tab_xp" ScrollBars="None" UseVerticalStripPlacement="False" VerticalStripWidth="120px">
                        <ajaxToolkit:TabPanel id="tabEmpresaInformacion" runat="server" HeaderText="Informaci&oacute;n de la Empresa" Enabled="true" ScrollBars="Auto" OnDemandMode="Always">
                            <ContentTemplate>
                                <table style="width: 100%;">
                                    <tr>
                                        <td class="lblStandar" style="width: 25%;">Tipo Documento</td>
                                        <td>
                                            <asp:DropDownList id="ddlTipoDocumento" Enabled="false" CssClass="ddlStandar" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblStandar">RUC</td>
                                        <td>
                                            <asp:TextBox id="txtRuc" CssClass="txtStandar" Width="40%" runat="server" />&nbsp;
                                            <asp:Button id="btnSunat" Text="SUNAT" CssClass="btnStandar" Width="30%" OnClientClick="return ObtenerEmpresaSunat();" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblStandar">Raz&oacute;n Social</td>
                                        <td>
                                            <asp:TextBox id="txtRazonSocial" CssClass="txtStandar" Width="80%" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblStandar">Descripci&oacute;n</td>
                                        <td>
                                            <asp:TextBox id="txtDescripcion" CssClass="txtStandar" Width="80%" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblStandar">CIIU</td>
                                        <td>
                                            <asp:TextBox id="txtCiiu" CssClass="txtStandar" Width="80%" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblStandar">Pa&iacute;s</td>
                                        <td>
                                            <asp:DropDownList id="ddlPais" CssClass="ddlStandar" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblStandar">Departamento</td>
                                        <td>
                                            <asp:DropDownList id="ddlDepartamento" CssClass="ddlStandar" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblStandar">Provincia</td>
                                        <td>
                                            <asp:DropDownList id="ddlProvincia" CssClass="ddlStandar" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblStandar">Distrito</td>
                                        <td>
                                            <asp:DropDownList id="ddlDistrito" CssClass="ddlStandar" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblStandar">Ciudad</td>
                                        <td>
                                            <asp:TextBox id="txtCiudad" CssClass="txtStandar" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblStandar">Direcci&oacute;n</td>
                                        <td>
                                            <asp:TextBox id="txtDireccion" CssClass="txtStandar" Width="80%" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblStandar">Logo</td>
                                        <td>
                                            <%--<asp:FileUpload id="fuLogo" CssClass="txtStandar" Width="90%" runat="server" />--%>
                                            <ajaxToolkit:AsyncFileUpload ID="fuEmpresa" runat="server" PersistFile="true" CompleteBackColor="Transparent" OnClientUploadComplete="CargarLogo" UploaderStyle="Modern" UploadingBackColor="#CCFFFF" ThrobberID="lblCargaLogo" />
                                            <asp:Label ID="lblCargaLogo" runat="server" Width="20%"  Style="display: none;"><asp:Image alt="" ImageUrl="~/Imagenes/uploading.gif" runat="server"/></asp:Label>
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
                    <ajaxToolkit:TabPanel id="TabPanel1" runat="server" HeaderText="Almacenes" Enabled="true" ScrollBars="Auto" OnDemandMode="Always">
                        <ContentTemplate>
                            <br/>
                            <%--<div id="divHeaderAgencia" style="width: 1182px; overflow-x: hidden; overflow-y: hidden; margin: 0 0px 0 0px;" >--%>
                            <%--<table id="dummyHeaderAgencia" style="width: 1500px;" class="tblSinBordes">--%>
                            <table id="dummyHeaderAlmacen" style="width: 100%;" class="tblSinBordes">
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
                            <asp:UpdatePanel ID="divDataAlmacen" runat="server" UpdateMode="Conditional" style="overflow: scroll; width: 100%; height: 420px; margin: 0 0 0px 0px;" onscroll="Onscrollfnction();">
                                <ContentTemplate>
                                    <asp:GridView id="grvAlmacen" AutoGenerateColumns="False" Width="100%" RowStyle-Wrap="True" runat="server">
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
                                    <asp:AsyncPostBackTrigger ControlID="btnCargarAlmacen" EventName="Click" />
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
                    <asp:Button ID="btnGuardarEmpresa" runat="server" CssClass="btnStandar" Text="Guardar" Width="30%" OnClientClick="return ValidarEmpresa();" OnClick="btnGuardarEmpresa_OnClick" />
                    <asp:Button ID="btnCancelarEmpresa" runat="server" CssClass="btnStandar" OnClientClick="return Cancelar();" Text="Cancelar" Width="30%" />
                    <asp:button id="hButton" runat="server" style="display:none;" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="mpeEmpresa" PopupControlID="pnlEmpresa" TargetControlID="hButton" CancelControlID="btnCancelarEmpresa" Drag="True" DropShadow="True" BackgroundCssClass="FondoAplicacion" ClientIDMode="Static" runat="server" PopupDragHandleControlID="lblTituloEmpresa"></ajaxToolkit:ModalPopupExtender>
        <ajaxToolkit:RoundedCornersExtender ID="rceEmpresa" runat="server" BehaviorID="rcbEmpresa" TargetControlID="pnlEmpresa" Radius="6" Corners="All" />
    </div>
    <input type="hidden" id="hdnEmpresaImagen" runat="server" value="" />
        <asp:Button id="btnCargarAgencia" style="display: none;" OnClick="btnCargarAgencia_OnClick" runat="server"/>
    <asp:Button id="btnCargarAlmacen" style="display: none;" OnClick="btnCargarAlmacen_OnClick" runat="server"/>
    </form>
<script type="text/javascript" src="../Scripts/tablesorter/jquery.tablesorter.js"></script>
<script type="text/javascript">
    function pageLoad(sender, args) {
        $(document).ready(function () {
            $("#grvEmpresa").tablesorter({ dateFormat: 'uk' });
            SetDefaultSortOrder();
        });
    }
    function Sort(cell, sortOrder) {
        var sorting = [[cell.cellIndex, sortOrder]];
        $("#grvEmpresa").trigger("sorton", [sorting]);
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
        var grvEmpresa = document.getElementById('grvEmpresa').clientWidth;

        var divData = centro - 20;
        var divHeader = divData;
        document.getElementById('divHeader').style.width = divHeader + 'px';

        if (dummyHeader < divHeader) {
            document.getElementById('dummyHeader').style.width = divHeader + 'px';
            document.getElementById('grvEmpresa').style.width = divData + 'px';
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
        var btnSunat = document.getElementById('<%=btnSunat.ClientID%>');
        var btnGuardarEmpresa = document.getElementById('<%=btnGuardarEmpresa.ClientID%>');
        if (prm.get_isInAsyncPostBack())
            args.set_cancel(true);
        postBackElement = args.get_postBackElement();
        if (postBackElement.id == 'btnGuardarEmpresa' || postBackElement.id == btnSunat.id || postBackElement.id == 'btnCargarAgencia') {
            // Bloqueo Pantalla
            Bloquear();
        }
    }
    function EndRequest(sender, args) {
        var btnSunat = document.getElementById('<%=btnSunat.ClientID%>');
        var btnGuardarEmpresa = document.getElementById('<%=btnGuardarEmpresa.ClientID%>');
        if (postBackElement.id == 'btnGuardarEmpresa' || postBackElement.id == btnSunat.id || postBackElement.id == 'btnCargarAgencia') {
            // Desbloquear Pantalla
            Desbloquear();
            //RedimensionarGrid();
        }
    }
    function grvEmpresa_OnDnlClick(row) {
        debugger;
        var indiceEmpresaId = 5;
        var empresaIdRow = RetornarCeldaValor(row, indiceEmpresaId);

        VerEmpresa(empresaIdRow);
    }
</script>
</body>
</html>