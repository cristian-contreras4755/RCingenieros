<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucProcesando.ascx.cs" Inherits="APU.Gasolutions.Controles.ucProcesando" %>
<div id="divProcesando" style="position: absolute; display: none; text-align: center; height: 100%; width: 100%; top: 0; bottom: 0;"></div>
<div id="divCargando" class="border-radius-8" style="vertical-align: middle; text-align: center; border-style: none; border-width: 1px; border-color: #000000; height: 40%; width: 30%; top: 30%; left: 35%; margin: 0 auto; position: fixed; display: inline; filter: alpha(opacity=100); opacity: 1.0; -ms-filter: progid:DXImageTransform.Microsoft.Alpha(Opacity=100);">
    <asp:Image ImageUrl="~/Imagenes/loader.gif" ID="cargando" AlternateText="Por favor espere.." Runat="server"></asp:Image>
</div>