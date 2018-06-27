<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Agencia.aspx.cs" Inherits="APU.Presentacion.Configuracion.Agencia" EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="uc1" TagName="ucProcesando" Src="~/Controles/ucProcesando.ascx" %>
<%@ Register Src="~/Controles/ucCabecera.ascx" TagPrefix="uc1" TagName="ucCabecera" %>
<%@ Register Src="~/Controles/ucMenu.ascx" TagPrefix="uc1" TagName="ucMenu" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Agencia</title>
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
        var grid = document.getElementById('grvAgencia');
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
    function VerAgencia(agenciaId) {
        PageMethods.ObtenerAgencia(agenciaId, ObtenerAgenciaOk, fnLlamadaError);
        return false;
    }
    function LimpiarAgencia() {
        var hdnAgenciaId = document.getElementById('<%=hdnAgenciaId.ClientID%>');
        var ddlEmpresa = document.getElementById('<%=ddlEmpresa.ClientID%>');
        var txtNombre = document.getElementById('<%=txtNombre.ClientID%>');
        var txtDescripcion = document.getElementById('<%=txtDescripcion.ClientID%>');
        var chkActivo = document.getElementById('<%=chkActivo.ClientID%>');
        var ddlPais = document.getElementById('<%=ddlPais.ClientID%>');
        var ddlDepartamento = document.getElementById('<%=ddlDepartamento.ClientID%>');
        var ddlProvincia = document.getElementById('<%=ddlProvincia.ClientID%>');
        var ddlDistrito = document.getElementById('<%=ddlDistrito.ClientID%>');
        var txtCiudad = document.getElementById('<%=txtCiudad.ClientID%>');
        var txtDireccion = document.getElementById('<%=txtDireccion.ClientID%>');
        var lblModificadoPor = document.getElementById('<%=lblModificadoPor.ClientID%>');
        var lblFechaModificacion = document.getElementById('<%=lblFechaModificacion.ClientID%>');

        hdnAgenciaId.value = 0;
        //ddlEmpresa.value = 0;
        document.getElementById('lblTituloAgencia').innerText = 'Agencia';
        txtNombre.value = '';
        txtDescripcion.value = '';
        chkActivo.checked = true;
        ddlPais.value = 0;
        ddlDepartamento.value = 0;
        ddlProvincia.value = 0;
        ddlDistrito.value = 0;
        txtCiudad.value = '';
        txtDireccion.value = '';
        lblModificadoPor.innerText = '';
        lblFechaModificacion.innerText = '';

        var tabAgencia = $get('<%=tabAgencia.ClientID%>');
        tabAgencia.control.set_activeTabIndex(0);
    }
    function ObtenerAgenciaOk(a) {
        var modalDialog = $find('mpeAgencia');
        if (a != null) {
            modalDialog.show();

            var tabAgencia = $get('<%=tabAgencia.ClientID%>');
            tabAgencia.control.set_activeTabIndex(0);

            var hdnAgenciaId = document.getElementById('<%=hdnAgenciaId.ClientID%>');
            var ddlEmpresa = document.getElementById('<%=ddlEmpresa.ClientID%>');
            var ddlCliente = document.getElementById('<%=ddlCliente.ClientID%>');
            var txtNombre = document.getElementById('<%=txtNombre.ClientID%>');
            var txtDescripcion = document.getElementById('<%=txtDescripcion.ClientID%>');
            var chkActivo = document.getElementById('<%=chkActivo.ClientID%>');
            var ddlPais = document.getElementById('<%=ddlPais.ClientID%>');
            var ddlDepartamento = document.getElementById('<%=ddlDepartamento.ClientID%>');
            var ddlProvincia = document.getElementById('<%=ddlProvincia.ClientID%>');
            var ddlDistrito = document.getElementById('<%=ddlDistrito.ClientID%>');
            var txtCiudad = document.getElementById('<%=txtCiudad.ClientID%>');
            var txtDireccion = document.getElementById('<%=txtDireccion.ClientID%>');
            var lblModificadoPor = document.getElementById('<%=lblModificadoPor.ClientID%>');
            var lblFechaModificacion = document.getElementById('<%=lblFechaModificacion.ClientID%>');

            hdnAgenciaId.value = a.AgenciaId;
            

            if (a.EmpresaId > 0) {
                ddlEmpresa.value = a.EmpresaId;
                ObtenerUsuarioPorEmpresa(a.EmpresaId);
            }
            if (a.ClienteId > 0) {
                ddlCliente.value = a.ClienteId;
            }

            document.getElementById('lblTituloAgencia').innerText = a.Empresa;
            txtNombre.value = a.Nombre;
            txtDescripcion.value = a.Descripcion;
            chkActivo.checked = (a.Activo > 0);
            ddlPais.value = a.PaisId;
            ddlDepartamento.value = a.DepartamentoId;

            if (a.DepartamentoId > 0) {
                ObtenerProvincia(a.DepartamentoId, a.ProvinciaId, a.DistritoId);
            }

            ddlProvincia.value = a.ProvinciaId;
            ddlDistrito.value = a.DistritoId;
            txtCiudad.value = a.Ciudad;
            txtDireccion.value = a.Direccion;
            lblModificadoPor.innerText = a.UsuarioModificacion;
            lblFechaModificacion.innerText = FormatoFecha(a.FechaModificacion);
        } else {
            modalDialog.hide();
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
    function ObtenerUsuarioPorEmpresa(emp) {
        var ddlContacto = document.getElementById('<%=ddlContacto.ClientID%>');
        if (emp == 0) {
            document.getElementById('trCliente').style.display = '';
            document.getElementById('ddlEmpresa').value = 0;
            LimpiarCombo(ddlContacto);
        } else {
            // var ddlCliente = document.getElementById('<%=ddlDistrito.ClientID%>');
            document.getElementById('trCliente').style.display = 'none';
            document.getElementById('ddlCliente').value = 0;
            PageMethods.ObtenerUsuarioPorEmpresa(emp, ObtenerUsuarioPorEmpresaSucceed, fnLlamadaError, emp);
        }
    }
    function ObtenerUsuarioPorEmpresaSucceed(result, emp) {
        var ddlContacto = document.getElementById('<%=ddlContacto.ClientID%>');
        if (result.length > 0) {
            LlenarCombo(ddlContacto, result, 'UsuarioId', 'Usuario');
            AgregarOptionACombo(ddlContacto, Seleccione_value, Seleccione);
            ddlContacto.value = Seleccione_value;
            if (emp > 0) {
                ddlContacto.value = emp;
            }
        } else {
            alert('No existen usuarios para la empresa seleccionada.');
        }
    }
    function SeleccionarCliente(cli) {
        if (cli > 0) {
            document.getElementById('trEmpresa').style.display = 'none';
            document.getElementById('ddlEmpresa').value = 0;
        } else {
            document.getElementById('trEmpresa').style.display = '';
            document.getElementById('ddlCliente').value = 0;
        }
    }
    function InsertarAgencia() {
        var modalDialog = $find('mpeAgencia');

        modalDialog.show();
        LimpiarAgencia();

        return false;
    }
    function ValidarAgencia() {
        var hdnAgenciaId = document.getElementById('<%=hdnAgenciaId.ClientID%>');
        var ddlEmpresa = document.getElementById('<%=ddlEmpresa.ClientID%>');
        var txtNombre = document.getElementById('<%=txtNombre.ClientID%>');
        var chkActivo = document.getElementById('<%=chkActivo.ClientID%>');

        if (ddlEmpresa.value.trim() == '0') {
            return MostrarValidacion(ddlEmpresa, 'Seleccione la Empresa.');
        }
        if (txtNombre.value.trim() == '') {
            return MostrarValidacion(txtNombre, 'Ingrese el Nombre de la Agencia.');
        }
    }
    function GuardarAgenciaOk(res) {
        var modalDialog = $find('mpeAgencia');
        var arr = res.split('@');
        var agenciaId = arr[0];
        var mensaje = arr[1];

        if (agenciaId > 0) {
            alert('Se registró la información de la Agencia correctamente.');
            modalDialog.hide();
            LimpiarAgencia();
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
        var modalDialog = $find('mpeAgencia');
        modalDialog.hide();
    }
</script>
    <form id="frmAgencia" runat="server" DefaultButton="btnDefault">
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
                    <asp:Label CssClass="lblTitulo" Width="100%" runat="server">AGENCIA</asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 100%;">
                    <table style="width: 100%; margin: 0;">
                        <tr>
                            <td style="width: 10%;">
                                Ver:&nbsp;<asp:DropDownList id="ddlNumeroRegistros" CssClass="ddlStandar" Width="50%" AutoPostBack="True" runat="server" OnSelectedIndexChanged="ddlNumeroRegistros_SelectedIndexChanged" />
                            </td>
                            <td style="width: 20%;">
                                <asp:LinkButton ID="imgAnadir" ForeColor="#404040" runat="server" ToolTip="" OnClientClick="return InsertarAgencia();">
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
                                <th style="width: 20%; cursor: pointer; text-decoration: underline;">Nombre</th>
                                <th style="width: 30%; cursor: pointer; text-decoration: underline;">Raz&oacute;n Social</th>
                                <th style="width: 35%; cursor: pointer; text-decoration: underline;">Direcci&oacute;n</th>
                                <th style="width: 10%; cursor: pointer; text-decoration: underline;">Activo</th>
                            </tr>
                            </thead>
                        </table>
                    </div>
                    <asp:UpdatePanel ID="divData" runat="server" UpdateMode="Conditional" style="overflow: scroll; width: 1200px; height: 420px; margin: 0 0 0px 0px;" onscroll="Onscrollfnction();">
                        <ContentTemplate>
                            <asp:GridView id="grvAgencia" AutoGenerateColumns="False" Width="1500px" RowStyle-Wrap="True" runat="server" OnRowDataBound="grvAgencia_RowDataBound">
                                <HeaderStyle CssClass="filaCabeceraGrid"></HeaderStyle>
                                <RowStyle CssClass="filaImparGrid"></RowStyle>
                                <AlternatingRowStyle CssClass="filaParGrid"></AlternatingRowStyle>
                                <Columns>
                                    <%-- 0: Acciones --%>
                                    <asp:TemplateField HeaderText="Acciones">
                                        <ItemStyle Width="5%" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ImageUrl="~/Imagenes/Iconos/editar.png" CssClass="imgButton" OnClientClick='<%# "return VerAgencia(" + Eval("AgenciaId") + ");" %>' ToolTip="Editar" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--  1: Nombre --%>
                                    <asp:TemplateField HeaderText="NumeroDocumento">
                                        <ItemStyle Width="20%" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <%# Eval("Nombre").ToString().Trim() %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- 2: Empresa --%>
                                    <asp:TemplateField HeaderText="Razón Social">
                                        <ItemStyle Width="30%" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <%# Eval("Empresa").ToString().Trim() %>
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
                            <asp:AsyncPostBackTrigger ControlID="btnGuardarAgencia" EventName="Click" />
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
    <asp:Panel ID="pnlAgencia" runat="server" CssClass="pnlModal" style="display: none;">
        <table style="width: 100%;">
            <tr id="lblTituloPopupAgencia" class="lblTituloPopup">
                <td style="width: 20%;" class="lblTituloPopup">&nbsp;</td>
                <td id="lblTituloAgencia" class="lblTituloPopup" style="width: 60%">Agencia</td>
                <td class="lblTituloPopup" style="width: 20%; text-align: right;">
                    <a onclick="return Cerrar();" style="color: #FFFFFF; text-decoration: none; cursor: pointer;">Cerrar X</a>&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <input type="hidden" id="hdnAgenciaId" value="0" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="3" style="width: 100%;">
                    <ajaxToolkit:TabContainer id="tabAgencia" runat="server" OnClientActiveTabChanged="" Height="350px" Width="600px" ActiveTabIndex="0" OnDemand="False" AutoPostBack="false"
                                              TabStripPlacement="Top" CssClass="ajax__tab_xp" ScrollBars="None" UseVerticalStripPlacement="False" VerticalStripWidth="120px">
                        <ajaxToolkit:TabPanel id="tabAgenciaInformacion" runat="server" HeaderText="Informaci&oacute;n de la Agencia" Enabled="true" ScrollBars="Auto" OnDemandMode="Always">
                            <ContentTemplate>
                                <table style="width: 100%;">
                                    <tr id="trEmpresa">
                                        <td class="lblStandar">Empresa</td>
                                        <td>
                                            <asp:DropDownList id="ddlEmpresa" CssClass="ddlStandar" Width="80%" runat="server" />
                                        </td>
                                    </tr>
                                    <tr id="trCliente">
                                        <td class="lblStandar">Cliente</td>
                                        <td>
                                            <asp:DropDownList id="ddlCliente" CssClass="ddlStandar" Width="80%" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblStandar">Nombre</td>
                                        <td>
                                            <asp:TextBox id="txtNombre" CssClass="txtStandar" Width="60%" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblStandar">Descripci&oacute;n</td>
                                        <td>
                                            <asp:TextBox id="txtDescripcion" CssClass="txtStandar" Width="80%" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblStandar">Pa&iacute;s</td>
                                        <td>
                                            <asp:DropDownList id="ddlPais" CssClass="ddlStandar" Width="60%" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblStandar">Departamento</td>
                                        <td>
                                            <asp:DropDownList id="ddlDepartamento" CssClass="ddlStandar" Width="60%" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblStandar">Provincia</td>
                                        <td>
                                            <asp:DropDownList id="ddlProvincia" CssClass="ddlStandar" Width="60%" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblStandar">Distrito</td>
                                        <td>
                                            <asp:DropDownList id="ddlDistrito" CssClass="ddlStandar" Width="60%" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblStandar">Ciudad</td>
                                        <td>
                                            <asp:TextBox id="txtCiudad" CssClass="txtStandar" Width="60%" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblStandar">Direcci&oacute;n</td>
                                        <td>
                                            <asp:TextBox id="txtDireccion" CssClass="txtStandar" Width="80%" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblStandar">Contacto</td>
                                        <td>
                                            <asp:DropDownList id="ddlContacto" CssClass="ddlStandar" Width="60%" runat="server" />
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
                <td colspan="3">&nbsp;</td>
            </tr>
            <tr>
                <td colspan="3" style="text-align: center;">
                    <asp:Button ID="btnGuardarAgencia" runat="server" CssClass="btnStandar" Text="Guardar" Width="30%" OnClientClick="return ValidarAgencia();" OnClick="btnGuardarAgencia_OnClick" />
                    <asp:Button ID="btnCancelarAgencia" runat="server" CssClass="btnStandar" Text="Cancelar" Width="30%" />
                    <asp:button id="hButton" runat="server" style="display:none;" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="mpeAgencia" PopupControlID="pnlAgencia" TargetControlID="hButton" CancelControlID="btnCancelarAgencia" Drag="True" DropShadow="True" BackgroundCssClass="FondoAplicacion" ClientIDMode="Static" runat="server" PopupDragHandleControlID="lblTituloAgencia"></ajaxToolkit:ModalPopupExtender>
        <ajaxToolkit:RoundedCornersExtender ID="rceAgencia" runat="server" BehaviorID="rcbAgencia" TargetControlID="pnlAgencia" Radius="6" Corners="All" />
    </div>
    </form>
<script type="text/javascript" src="../Scripts/tablesorter/jquery.tablesorter.js"></script>
<script type="text/javascript">
    function pageLoad(sender, args) {
        $(document).ready(function () {
            $("#grvAgencia").tablesorter({ dateFormat: 'uk' });
            SetDefaultSortOrder();
        });
    }
    function Sort(cell, sortOrder) {
        var sorting = [[cell.cellIndex, sortOrder]];
        $("#grvAgencia").trigger("sorton", [sorting]);
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
        var grvAgencia = document.getElementById('grvAgencia').clientWidth;

        var divData = centro - 20;
        var divHeader = divData;
        document.getElementById('divHeader').style.width = divHeader + 'px';

        if (dummyHeader < divHeader) {
            document.getElementById('dummyHeader').style.width = divHeader + 'px';
            document.getElementById('grvAgencia').style.width = divData + 'px';
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
        if (postBackElement.id == 'btnGuardarAgencia') {
            // Bloqueo Pantalla
            Bloquear();
        }
    }
    function EndRequest(sender, args) {
        if (postBackElement.id == 'btnGuardarAgencia') {
            // Desbloquear Pantalla
            Desbloquear();
            //RedimensionarGrid();
        }
    }
    function grvAgencia_OnDnlClick(row) {
        var indiceAgenciaId = 5;
        var agenciaIdRow = RetornarCeldaValor(row, indiceAgenciaId);

        VerAgencia(agenciaIdRow);
    }
</script>
</body>
</html>