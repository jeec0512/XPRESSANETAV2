<%@ Page Title="" Language="C#" MasterPageFile="~/Ingreso/mpIngreso.master" AutoEventWireup="true" CodeFile="facturacionElectronica.aspx.cs" Inherits="Ingreso_facturacionElectronica" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" runat="Server">
    <asp:ScriptManager runat="server" ID="sm1">
    </asp:ScriptManager>
    <!-- MENSAJE-->
    <asp:Panel ID="pnMensaje2" CssClass="" runat="server" Visible="true">
        <asp:Label ID="lblMensaje" runat="server" Text="" Visible="true"></asp:Label>
        <asp:Button ID="btnIngresaProv" runat="server" Text="Ingrese el proveedor" Visible="false" />
    </asp:Panel>

	
	
	 <asp:Panel ID="pnTitulos" runat="server" Visible="true" Style="position: relative; top: 0; left: 0; height: 100vh">
	 <h2>Facturaciòn Electrònica</h2>
       
        <iframe id="ifFacturacion" src="http://192.168.1.118:8080/xpressweb/site/home.an?prmusrid=61" runat="server" style="position: relative; top: 0; left: 0; height: 100vh; width: 90vw;" frameborder="0"></iframe>
    </asp:Panel>



</asp:Content>

