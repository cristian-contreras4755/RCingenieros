<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Perfil.aspx.cs" Inherits="APU.Presentacion.Seguridad.Perfil" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="uc1" TagName="ucProcesando" Src="~/Controles/ucProcesando.ascx" %>
<%@ Register Src="~/Controles/ucCabecera.ascx" TagPrefix="uc1" TagName="ucCabecera" %>
<%@ Register Src="~/Controles/ucMenu.ascx" TagPrefix="uc1" TagName="ucMenu" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Perfil</title>
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
        var hdnPerfilId = document.getElementById('<%=hdnPerfilId.ClientID%>');
        var txtNombres = document.getElementById('<%=txtNombre.ClientID%>');
        var chkActivo = document.getElementById('<%=chkActivo.ClientID%>');
    }

    function Buscar() {
        var grid = document.getElementById('grvPerfil');
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
    function VerPerfil(perfilId) {
        PageMethods.ObtenerPerfil(perfilId, ObtenerPerfilOk, fnLlamadaError);
        return false;
    }
    function LimpiarPerfil() {
        debugger;
        var hdnPerfilId = document.getElementById('<%=hdnPerfilId.ClientID%>');
        var txtNombres = document.getElementById('<%=txtNombre.ClientID%>');
        var chkActivo = document.getElementById('<%=chkActivo.ClientID%>');

        hdnPerfilId.value = 0;
        document.getElementById('lblTituloPerfil').innerText = 'Perfil';
        txtNombres.value = '';
        chkActivo.checked = true;

        var tabPerfil = $get('<%=tabPerfil.ClientID%>');
        tabPerfil.control.set_activeTabIndex(0);
    }
    function ObtenerPerfilOk(p) {
        debugger;
        var modalDialog = $find('mpePerfil');
        if (p != null) {
            modalDialog.show();

            var tabPerfil = $get('<%=tabPerfil.ClientID%>');
            tabPerfil.control.set_activeTabIndex(0);

            var hdnPerfilId = document.getElementById('<%=hdnPerfilId.ClientID%>');
            var txtNombres = document.getElementById('<%=txtNombre.ClientID%>');
            var chkActivo = document.getElementById('<%=chkActivo.ClientID%>');
            var lblModificadoPor = document.getElementById('<%=lblModificadoPor.ClientID%>');
            var lblFechaModificacion = document.getElementById('<%=lblFechaModificacion.ClientID%>');

            hdnPerfilId.value = p.PerfilId;
            document.getElementById('lblTituloPerfil').innerText = p.Perfil;
            txtNombres.value = p.Perfil;
            chkActivo.checked = (p.Activo > 0);
            lblModificadoPor.innerText = p.UsuarioModificacion;
            lblFechaModificacion.innerText = FormatoFecha(p.FechaModificacion);

        } else {
            modalDialog.hide();
        }
    }
    function InsertarPerfil() {
        var modalDialog = $find('mpePerfil');

        modalDialog.show();
        LimpiarPerfil();

        return false;
    }
    function ValidarPerfil() {
        var hdnPerfilId = document.getElementById('<%=hdnPerfilId.ClientID%>');
        var txtNombres = document.getElementById('<%=txtNombre.ClientID%>');
        var chkActivo = document.getElementById('<%=chkActivo.ClientID%>');

        if (txtNombres.value.trim() == '') {
            return MostrarValidacion(txtNombres, 'Ingrese un nombre de Perfil.');
        }
    }
    function GuardarPerfil() {
        perfilId = document.getElementById('hdnPerfilId').value;

        var nombres = document.getElementById('ctl05_ctl00_txtNombres').value;
        var perfilId = document.getElementById('ctl05_ctl01_ddlPerfil').value;
        var activo = document.getElementById('ctl05_ctl01_chkActivo').checked ? 1 : 0;

        Bloquear();
        PageMethods.GuardarPerfil(perfilId, nombres, activo, GuardarPerfilOk, fnLlamadaError);
    }
    function GuardarPerfilOk(res) {
        debugger;

        var modalDialog = $find('mpePerfil');

        var arr = res.split('@');
        var perfilId = arr[0];
        var mensaje = arr[1];

        if (perfilId > 0) {
            alert('Se registró la información del perfil correctamente.');
            modalDialog.hide();
            LimpiarPerfil();
        } else {
            alert(mensaje);
        }
        Desbloquear();
    }
    function fnLlamadaError(excepcion) {
        alert('Ha ocurrido un error interno: ' + excepcion.get_message());
        Desbloquear();
    }
</script>
    <form id="frmPerfil" runat="server" DefaultButton="btnDefault">
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
                    <asp:Label CssClass="lblTitulo" Width="100%" runat="server">PERFIL</asp:Label>
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
                                <asp:LinkButton ID="imgAnadir" ForeColor="#404040" runat="server" ToolTip="" OnClientClick="return InsertarPerfil();">
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
                                <th style="width: 30%; cursor: pointer; text-decoration: underline;">Nombre</th>
                                <th style="width: 10%; cursor: pointer; text-decoration: underline;">Activo</th>
                                <th style="width: 40%; cursor: pointer; text-decoration: underline;">Modificado Por</th>
                                <th style="width: 15%; cursor: pointer; text-decoration: underline;">Fecha </th>
                            </tr>
                            </thead>
                        </table>
                    </div>
                    <asp:UpdatePanel ID="divData" runat="server" UpdateMode="Conditional" style="overflow: scroll; width: 1200px; height: 420px; margin: 0 0 0px 0px;" onscroll="Onscrollfnction();">
                        <ContentTemplate>
                            <asp:GridView id="grvPerfil" AutoGenerateColumns="False" Width="1500px" RowStyle-Wrap="True" runat="server" OnRowDataBound="grvPerfil_RowDataBound">
                                <HeaderStyle CssClass="filaCabeceraGrid"></HeaderStyle>
                                <RowStyle CssClass="filaImparGrid"></RowStyle>
                                <AlternatingRowStyle CssClass="filaParGrid"></AlternatingRowStyle>
                                <Columns>
                                    <%-- 0: Acciones --%>
                                    <asp:TemplateField HeaderText="Acciones">
                                        <ItemStyle Width="5%" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ImageUrl="~/Imagenes/Iconos/editar.png" CssClass="imgButton" OnClientClick='<%# "return VerPerfil(" + Eval("PerfilId") + ");" %>' ToolTip="Editar" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--  1: Perfil --%>
                                    <asp:TemplateField HeaderText="Nombre">
                                        <ItemStyle Width="30%" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <%# Eval("Perfil").ToString().Trim() %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- 2: Activo --%>
                                    <asp:TemplateField HeaderText="Activo">
                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" Checked='<%# Int16.Parse(Eval("Activo").ToString()) > 0 %>' Enabled="False" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- 3: Modificado Por --%>
                                    <asp:TemplateField HeaderText="Modificado Por">
                                        <ItemStyle Width="40%" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <%# Eval("UsuarioModificacion")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- 4: Fecha Modificación --%>
                                    <asp:TemplateField HeaderText="Fecha Modificaci&oacute;n">
                                        <ItemStyle Width="15%" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <%# Eval("FechaModificacion")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- 5: PerfilId --%>
                                    <asp:TemplateField>
                                        <ItemStyle CssClass="columnaOcultaGrid" />
                                        <ItemTemplate>
                                            <%# Eval("PerfilId")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlNumeroRegistros" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="btnPaginacion" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnGuardarPerfil" EventName="Click" />
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
    <asp:Panel ID="pnlPerfil" runat="server" CssClass="pnlModal" style="display: none;">
        <table style="width: 100%;">
            <tr>
                <td id="lblTituloPerfil" class="lblTituloPopup">Perfil</td>
            </tr>
            <tr>
                <td>
                    <input type="hidden" id="hdnPerfilId" value="0" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="width: 100%;">
                    <ajaxToolkit:TabContainer id="tabPerfil" runat="server" OnClientActiveTabChanged="" Height="250px" Width="400px" ActiveTabIndex="0" OnDemand="False" AutoPostBack="false"
                                              TabStripPlacement="Top" CssClass="ajax__tab_xp" ScrollBars="None" UseVerticalStripPlacement="False" VerticalStripWidth="120px">
                        <ajaxToolkit:TabPanel id="tabPerfilInformacion" runat="server" HeaderText="Informaci&oacute;n de Perfil" Enabled="true" ScrollBars="Auto" OnDemandMode="Always">
                            <ContentTemplate>
                                <table style="width: 100%;">
                                    <tr>
                                        <td class="lblStandar">Perfil</td>
                                        <td>
                                            <asp:TextBox id="txtNombre" CssClass="txtStandar" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblStandar">Activo</td>
                                        <td>
                                            <asp:CheckBox id="chkActivo" Checked="True" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lblStandar">Opcion Inicio</td>
                                        <td>
                                            <asp:DropDownList id="ddlOpcionInicio" CssClass="ddlStandar" runat="server" />
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
                    <asp:Button ID="btnGuardarPerfil" runat="server" CssClass="btnStandar" Text="Guardar" Width="30%" OnClientClick="return ValidarPerfil();" OnClick="btnGuardarPerfil_Click" />
                    <asp:Button ID="btnCancelarPerfil" runat="server" CssClass="btnStandar" Text="Cancelar" Width="30%" />
                    <asp:button id="hButton" runat="server" style="display:none;" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="mpePerfil" PopupControlID="pnlPerfil" TargetControlID="hButton" CancelControlID="btnCancelarPerfil" Drag="True" DropShadow="True" BackgroundCssClass="FondoAplicacion" ClientIDMode="Static" runat="server" PopupDragHandleControlID="lblTituloPerfil"></ajaxToolkit:ModalPopupExtender>
        <ajaxToolkit:RoundedCornersExtender ID="rcePerfil" runat="server" BehaviorID="rcbPerfil" TargetControlID="pnlPerfil" Radius="6" Corners="All" />
    </div>
    </form>
<script type="text/javascript" src="../Scripts/tablesorter/jquery.tablesorter.js"></script>
<script type="text/javascript">
    function pageLoad(sender, args) {
        $(document).ready(function () {
            $("#grvPerfil").tablesorter({ dateFormat: 'uk' });
            SetDefaultSortOrder();
        });
    }
    function Sort(cell, sortOrder) {
        var sorting = [[cell.cellIndex, sortOrder]];
        $("#grvPerfil").trigger("sorton", [sorting]);
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
        var grvPerfil = document.getElementById('grvPerfil').clientWidth;

        var divData = centro - 20;
        var divHeader = divData;
        document.getElementById('divHeader').style.width = divHeader + 'px';

        if (dummyHeader < divHeader) {
            document.getElementById('dummyHeader').style.width = divHeader + 'px';
            document.getElementById('grvPerfil').style.width = divData + 'px';
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
        if ((postBackElement.id == 'ddlNumeroRegistros') || (postBackElement.id == 'btnPrimero') || (postBackElement.id == 'btnAnterior') || (postBackElement.id == 'btnSiguiente') ||
            (postBackElement.id == 'btnUltimo') || (postBackElement.id == 'btnPaginacion') || (postBackElement.id == 'btnGuardarPerfil')) {
            // Bloqueo Pantalla
            Bloquear(100002);
        }
    }
    function EndRequest(sender, args) {
        if ((postBackElement.id == 'ddlNumeroRegistros') || (postBackElement.id == 'btnPrimero') || (postBackElement.id == 'btnAnterior') || (postBackElement.id == 'btnSiguiente') ||
            (postBackElement.id == 'btnUltimo') || (postBackElement.id == 'btnPaginacion') || (postBackElement.id == 'btnGuardarPerfil')) {
            // Desbloquear Pantalla
            Desbloquear();
            RedimensionarGrid();
        }
    }
    function grvPerfil_OnDnlClick(row) {
        var indicePerfilId = 5;
        var perfilIdRow = RetornarCeldaValor(row, indicePerfilId);

        VerPerfil(perfilIdRow);
    }
</script>
</body>
</html>