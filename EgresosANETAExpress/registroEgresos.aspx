﻿<%@ Page Title="" Language="C#" MasterPageFile="~/EgresosANETAExpress/mpEgresosANETAExpress.master" AutoEventWireup="true" CodeFile="registroEgresos.aspx.cs" 
    Inherits="EgresosANETAExpress_registroEgresos"  EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" Runat="Server">
     <asp:ScriptManager runat="server" ID="sm1">
    </asp:ScriptManager>
    <!-- MENSAJE-->
    <asp:Panel ID="pnMensaje2" CssClass="" runat="server" Visible="true">
        <asp:Label ID="lblMensaje" runat="server" Text="" Visible="true"></asp:Label>
        <asp:Button ID="btnIngresaProv" runat="server" Text="Ingrese el proveedor" Visible="false" />
    </asp:Panel>
    
    <!-- CABECERA INGRESO DE SUCURSAL Y FECHAs  -->
    <asp:Panel ID="pnTitulos" CssClass="" runat="server" Visible="true" Style="width: 100%; overflow: auto;">
        <h2>Registro de egresos</h2>
            <iframe id="ifControlEst" src=" http://192.168.1.118:8080/xpressapp/site/egresos.an?prmusrid=750" runat="server" width="100%" height="800px;" frameborder="0"></iframe>
    </asp:Panel>
</asp:Content>

