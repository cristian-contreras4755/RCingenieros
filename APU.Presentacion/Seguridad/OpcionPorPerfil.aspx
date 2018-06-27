<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OpcionPorPerfil.aspx.cs" Inherits="APU.Presentacion.Seguridad.OpcionPorPerfil" %>
<%@ Register TagPrefix="uc1" TagName="ucProcesando" Src="~/Controles/ucProcesando.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ucCabecera" Src="~/Controles/ucCabecera.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ucMenu" Src="~/Controles/ucMenu.ascx" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<script type="text/javascript">
    function OnTreeClick(evt) {
        //debugger;
        var src = window.event != window.undefined ? window.event.srcElement : evt.target
        var isChkBoxClick = (src.tagName.toLowerCase() == "input" && src.type == "checkbox");
        if (isChkBoxClick) {
            var parentTable = GetParentByTagName("table", src);
            var nxtSibling = parentTable.nextSibling;
            if (nxtSibling && nxtSibling.nodeType == 1)//check if nxt sibling is not null & is an element node
            {
                if (nxtSibling.tagName.toLowerCase() == "div") //if node has children
                {
                    //check or uncheck children at all levels
                    CheckUncheckChildren(parentTable.nextSibling, src.checked);
                }
            }
            //check or uncheck parents at all levels
            CheckUncheckParents(src, src.checked);
        }
    }
    function CheckUncheckChildren(childContainer, check) {
        var childChkBoxes = childContainer.getElementsByTagName("input");
        var childChkBoxCount = childChkBoxes.length;
        for (var i = 0; i < childChkBoxCount; i++) {
            childChkBoxes[i].checked = check;
        }
    }
    function CheckUncheckParents(srcChild, check) {
        var parentDiv = GetParentByTagName("div", srcChild);
        var parentNodeTable = parentDiv.previousSibling;

        if (parentNodeTable) {
            var checkUncheckSwitch;

            if (check) //checkbox checked
            {
                checkUncheckSwitch = true;
            }
            else //checkbox unchecked
            {
                var isAllSiblingsUnChecked = AreAllSiblingsUnChecked(srcChild);
                if (!isAllSiblingsUnChecked)
                    checkUncheckSwitch = true;
                else
                    checkUncheckSwitch = false;
            }
            var inpElemsInParentTable = parentNodeTable.getElementsByTagName("input");
            if (inpElemsInParentTable.length > 0) {
                var parentNodeChkBox = inpElemsInParentTable[0];
                parentNodeChkBox.checked = checkUncheckSwitch;
                //do the same recursively
                CheckUncheckParents(parentNodeChkBox, checkUncheckSwitch);
            }
        }
    }
    function AreAllSiblingsUnChecked(chkBox) {
        var parentDiv = GetParentByTagName("div", chkBox);
        var childCount = parentDiv.children.length;
        for (var i = 0; i < childCount; i++) {
            if (parentDiv.children[i].nodeType == 1) //check if the child node is an element node
            {
                if (parentDiv.children[i].tagName.toLowerCase() == "table") {
                    var prevChkBox = parentDiv.children[i].getElementsByTagName("input")[0];
                    //if any of sibling nodes are not checked, return false
                    if (prevChkBox.checked) {
                        return false;
                    }
                }
            }
        }
        return true;
    }
    //utility function to get the container of an element by tagname
    function GetParentByTagName(parentTagName, childElementObj) {
        var parent = childElementObj.parentNode;
        while (parent.tagName.toLowerCase() != parentTagName.toLowerCase()) {
            parent = parent.parentNode;
        }
        return parent;
    }
</script>
<body oncontextmenu="JavaScript:return true;" onload="Desbloquear();" onunload="Bloquear();" onbeforeunload="">
    <form id="frmOpcionPorPerfil" runat="server" DefaultButton="btnDefault">
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
                        <td style="width: 5%;">&nbsp;</td>
                        <td style="width: 90%;">
                            <table style="width: 100%;" class="tblSinBordes">
                                <tr>
                                    <td style="width: 10%; background-color: #ffffff;" ></td>
                                    <td style="width: 10%; background-color: #ffffff;"></td>
                                    <td style="width: 10%; background-color: #ffffff;"></td>
                                    <td style="width: 10%; background-color: #ffffff;"></td>
                                    <td style="width: 10%; background-color: #ffffff;"></td>
                                    <td style="width: 10%; background-color: #ffffff;"></td>
                                    <td style="width: 10%; background-color: #ffffff;"></td>
                                    <td style="width: 10%; background-color: #ffffff;"></td>
                                    <td style="width: 10%; background-color: #ffffff;"></td>
                                    <td style="width: 10%; background-color: #ffffff;"></td>
                                </tr>
                                <tr>
                                    <td colspan="10">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td class="lblTitulo">Opciones Por Perfil</td>                                            
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="10" style="width: 10%; background-color: #ffffff; height:10px"></td>                               
                                </tr>
                                <tr class="filaBusquedaGrid">
                                    <td style="width: 10%; text-align: center" ></td>                                   
                                    <td colspan="2" class="lblStandar">Seleccionar Perfil&nbsp;                                    
                                    </td>
                                    <td style="width: 10%; text-align: center" >
                                        <asp:DropDownList ID="ddlNumeroRegistros" CssClass="ddlStandar" runat="server" OnSelectedIndexChanged="ddlNumeroRegistros_SelectedIndexChanged" AutoPostBack="True">                                       
                                        </asp:DropDownList>
                                    </td>  
                                    <td colspan="4"></td>
                                    <td style="width: 10%;" class="lblStandar"></td>
                                    <td style="width: 10%; text-align: left;">                                    
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%;" colspan="10" class="lblStandar">
                                        <asp:UpdatePanel ID="upPerfil" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <table style="width: 100%;" class="tblSinBordes">
                                                    <tr class="filaCabeceraGrid">                                                    
                                                        <td style="width: 100%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                    </tr>
                                                </table>
                                                <asp:Panel ID="pnlGrid" Width="100%" Height="350px" style="overflow-y: scroll;" runat="server">                                               
                                                    <asp:TreeView ID="trvOpciones" runat="server" ShowCheckBoxes="All">
                                                    </asp:TreeView>
                                                </asp:Panel>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="ddlNumeroRegistros" EventName="SelectedIndexChanged" />                                            
                                                <asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click" />                                            
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="10">
                                        <input type="hidden" id="hdnPerfilId" value="0" runat="server" />&nbsp;                                    
                                    </td>
                                </tr>
                                <tr>                                
                                    <td colspan="10" style="text-align: center;">
                                        <asp:Button ID="btnGuardar" Text="Guardar" CssClass="btnStandar" Width="10%" runat="server" OnClick="btnGuardar_Click"/>
                                    </td>                                
                                </tr>
                            </table>
                        </td>
                        <td style="width: 5%;">&nbsp;</td>
                    </tr>
                </table>
            </div>
            <div id="derecha"></div>
        </div>
        <asp:Button id="btnDefault" OnClientClick="return false;" runat="server" />
    </form>
    <script type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_initializeRequest(InitializeRequest);
        prm.add_endRequest(EndRequest);
        var postBackElement;
        function InitializeRequest(sender, args) {
            if (prm.get_isInAsyncPostBack())
                args.set_cancel(true);
            postBackElement = args.get_postBackElement();
            if (postBackElement.id == 'btnGuardar') {
                // Bloqueo Pantalla
                Bloquear();
            }
        }
        function EndRequest(sender, args) {
            if (postBackElement.id == 'btnGuardar') {
                // Desbloquear Pantalla
                Desbloquear();
                //RedimensionarGrid();
            }
        }
    </script>
</body>
</html>