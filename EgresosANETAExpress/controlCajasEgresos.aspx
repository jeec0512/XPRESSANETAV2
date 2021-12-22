<%@ Page Title="" Language="C#" MasterPageFile="~/EgresosANETAExpress/mpEgresosANETAExpress.master" AutoEventWireup="true" CodeFile="controlCajasEgresos.aspx.cs" Inherits="EgresosANETAExpress_controlCajasEgresos" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" runat="Server">
    <asp:ScriptManager runat="server" ID="sm1">
    </asp:ScriptManager>
    <!-- MENSAJE-->
    <asp:Panel ID="pnMensaje2" CssClass="" runat="server" Visible="true">
        <asp:Label ID="lblMensaje" runat="server" Text="" Visible="true"></asp:Label>
        <asp:Button ID="btnIngresaProv" runat="server" Text="Ingrese el proveedor" Visible="false" />
    </asp:Panel>
    <h2>Control cajas de egresos</h2>
    <!-- CABECERA INGRESO DE SUCURSAL Y FECHAs  -->
					
    </asp:Panel>
	
	<asp:Panel ID="pnTitulos" runat="server" Visible="true" Style="position: relative; top: 0; left: 0;width:82vw; height: 100vh;">
        <iframe id="ifControlEst" src="http://192.168.1.118:8080/xpressapp/report/controlcaja.an?prmusrid=750" runat="server" style="position: relative; top: 0; left: 0; height: 100vh; width: 80vw;" frameborder="0"></iframe>
    </asp:Panel>

</asp:Content>

