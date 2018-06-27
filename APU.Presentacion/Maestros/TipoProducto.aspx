<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TipoProducto.aspx.cs" Inherits="APU.Presentacion.Maestros.TipoProducto" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="uc1" TagName="ucProcesando" Src="~/Controles/ucProcesando.ascx" %>
<%@ Register Src="~/Controles/ucCabecera.ascx" TagPrefix="uc1" TagName="ucCabecera" %>
<%@ Register Src="~/Controles/ucMenu.ascx" TagPrefix="uc1" TagName="ucMenu" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Tipo Producto</title>
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
        var txtNombre = document.getElementById('<%=txtNombre.ClientID%>');
        var txtDescripcion = document.getElementById('<%=txtDescripcion.ClientID%>');       
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
                    ele = eleNombre + eleTelefono;
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
    function VerUsuario(usuarioId) {
        PageMethods.ObtenerTipoProducto(usuarioId, ObtenerUsuarioOk, fnLlamadaError);
        return false;
    }
    function LimpiarUsuario() {
        debugger;
        var hdnUsuarioId = document.getElementById('<%=hdnUsuarioId.ClientID%>');
        var txtNombre = document.getElementById('<%=txtNombre.ClientID%>');
        var txtDescripcion= document.getElementById('<%=txtDescripcion.ClientID%>');        
        var chkActivo = document.getElementById('<%=chkActivo.ClientID%>');      

        hdnUsuarioId.value = 0;
        document.getElementById('lblTituloUsuario').innerText = 'Tipo Producto';
        txtNombre.value = '';
        txtDescripcion.value = '';        
        chkActivo.checked = true;

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
            var txtNombre = document.getElementById('<%=txtNombre.ClientID%>');
            var txtDescripcion = document.getElementById('<%=txtDescripcion.ClientID%>');
            var chkActivo = document.getElementById('<%=chkActivo.ClientID%>');
                       

            hdnUsuarioId.value = u.TipoProductoId;
            document.getElementById('lblTituloUsuario').innerText = u.Nombre;
            txtNombre.value = u.Nombre;
            txtDescripcion.value = u.Descripcion;          
                       
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
        debugger;
        var hdnUsuarioId = document.getElementById('<%=hdnUsuarioId.ClientID%>');
        var txtNombre = document.getElementById('<%=txtNombre.ClientID%>');
        var txtDescripcion = document.getElementById('<%=txtDescripcion.ClientID%>');       
          
        if (txtNombre.value.trim() == '') {
            return MostrarValidacion(txtNombre, 'Ingrese Nombre Producto.');
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
        debugger;

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
                            <asp:Label CssClass="lblTitulo" Width="100%" runat="server">TIPO DE PRODUCTO</asp:Label>
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
                                <table id="dummyHeader" style="width: 1200px;" class="tblSinBordes">
                                    <thead>
                                        <tr class="filaCabeceraGrid">
                                            <th style="width: 10%; cursor: default; text-decoration: none;">Acciones</th>                                            
                                            <th style="width: 40%; cursor: pointer; text-decoration: underline;">Nombre</th>                                            
                                            <th style="width: 40%; cursor: pointer; text-decoration: underline;">Descripcion</th>                                          
                                            <th style="width: 10%; cursor: pointer; text-decoration: underline;">Activo</th>
                                        </tr>
                                    </thead>
                                </table>
                            </div>
                            <asp:UpdatePanel ID="divData" runat="server" UpdateMode="Conditional" style="overflow: scroll; width: 1200px; height: 420px; margin: 0 0 0px 0px;" onscroll="Onscrollfnction();">
                                <ContentTemplate>
                                    <asp:GridView id="grvUsuario" AutoGenerateColumns="False" Width="1200px" RowStyle-Wrap="True" runat="server" OnRowDataBound="grvUsuario_RowDataBound">
                                        <HeaderStyle CssClass="filaCabeceraGrid"></HeaderStyle>
                                        <RowStyle CssClass="filaImparGrid"></RowStyle>
                                        <AlternatingRowStyle CssClass="filaParGrid"></AlternatingRowStyle>
                                        <Columns>
                                            <%-- 0: Acciones --%>
                                            <asp:TemplateField HeaderText="Acciones">
                                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:ImageButton runat="server" ImageUrl="~/Imagenes/Iconos/editar.png" CssClass="imgButton" OnClientClick='<%# "return VerUsuario(" + Eval("TipoProductoId") + ");" %>' ToolTip="Editar" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <%-- 1: Nombre --%>
                                            <asp:TemplateField HeaderText="Nombre">
                                                <ItemStyle Width="40%" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <%# Eval("Nombre")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--  2: Descripcion --%>
                                            <asp:TemplateField HeaderText="Descripcion">
                                                <ItemStyle Width="40%" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                     <%# Eval("Descripcion")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- 3: Activo --%>
                                            <asp:TemplateField>
                                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <%# Convert.ToInt16(Eval("Activo")) > 0 ? "Si" : "No"%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <%-- 4: TipoProductoId --%>
                                            <asp:TemplateField>
                                                <ItemStyle CssClass="columnaOcultaGrid" />
                                                <ItemTemplate>
                                                    <%# Eval("TipoProductoId")%>
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
                    <tr id="lblTituloPopupTipoProducto" class="lblTituloPopup">
                        <td style="width: 20%;" class="lblTituloPopup">&nbsp;</td>
                        <td id="lblTituloUsuario" class="lblTituloPopup" style="width: 60%">Tipo de Producto</td>
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
                            <ajaxToolkit:TabContainer id="tabUsuario" runat="server" OnClientActiveTabChanged="" Height="350px" Width="600px" ActiveTabIndex="0" OnDemand="False" AutoPostBack="false"
                                                      TabStripPlacement="Top" CssClass="ajax__tab_xp" ScrollBars="None" UseVerticalStripPlacement="False" VerticalStripWidth="120px">
                                <ajaxToolkit:TabPanel id="tabUsuarioInformacionPersonal" runat="server" HeaderText="Datos Principales" Enabled="true" ScrollBars="Auto" OnDemandMode="Always">
                                    <ContentTemplate>
                                        <table style="width: 100%;">
                                            <tr>
                                                <td class="lblStandar">Nombre</td>
                                                <td>
                                                    <asp:TextBox id="txtNombre" CssClass="txtStandar" runat="server" Width="300px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="lblStandar">Descripcion</td>
                                                <td>
                                                    <asp:TextBox id="txtDescripcion" CssClass="txtStandarMultiline" TextMode="MultiLine" Width="300px" Rows="6" runat="server" />
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
                        <td colspan="3">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="3" style="text-align: center;">
                            <asp:Button ID="btnGuardarUsuario" runat="server" CssClass="btnStandar" Text="Guardar" Width="30%" OnClientClick="return ValidarUsuario();" OnClick="btnGuardarUsuario_Click" />
                            <asp:Button ID="btnCancelarUsuario" runat="server" CssClass="btnStandar" Text="Cancelar" Width="30%" />
                            <asp:button id="hButton" runat="server" style="display:none;" />
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

            //var divData = principal - 20;
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
            debugger;
            if (prm.get_isInAsyncPostBack())
                args.set_cancel(true);
            postBackElement = args.get_postBackElement();
            if ((postBackElement.id == 'ddlNumeroRegistros') || (postBackElement.id == 'btnPrimero') || (postBackElement.id == 'btnAnterior') || (postBackElement.id == 'btnSiguiente') ||
                (postBackElement.id == 'btnUltimo') || (postBackElement.id == 'btnPaginacion') || (postBackElement.id == 'btnGuardarUsuario')) {
                // Bloqueo Pantalla
                Bloquear();
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
            var indiceUsuarioId = 4;
            var usuarioIdRow = RetornarCeldaValor(row, indiceUsuarioId);
            VerUsuario(usuarioIdRow);
        }
    </script>
</body>
</html>
