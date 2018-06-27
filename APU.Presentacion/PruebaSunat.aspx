<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PruebaSunat.aspx.cs" Inherits="APU.Presentacion.PruebaSunat" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label id="lblMensaje" runat="server"></asp:Label>
            <asp:Button id="btnEnviarSunat" Text="Enviar a SUNAT" runat="server" OnClick="btnEnviarSunat_Click"/>
        </div>
    </form>
</body>
</html>
