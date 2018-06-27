<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Prueba.aspx.cs" Inherits="APU.Gasolutions.Prueba" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button id="btnDeserializarXml" Text="Deserializar Xml" runat="server" OnClick="btnDeserializarXml_Click"/>&nbsp;
            <asp:Button id="btnDeserializarXmlGnv" Text="Deserializar Xml GNV" runat="server" OnClick="btnDeserializarXmlGnv_OnClick"/>
            
            <asp:Button id="btnNotaCredito" Text="Nota Credito" runat="server" OnClick="btnNotaCredito_OnClick"/>&nbsp;
            <asp:Button id="btnNotaDebito" Text="Nota Debito" runat="server" OnClick="btnNotaDebito_OnClick"/>&nbsp;
        </div>
    </form>
</body>
</html>