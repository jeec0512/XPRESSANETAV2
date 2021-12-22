﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Contabilidad/mpContabilidad.master" AutoEventWireup="true" CodeFile="detalleMovCuentas.aspx.cs" 
    Inherits="Contabilidad_detalleMovCuentas"   EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" Runat="Server">
    <asp:ScriptManager runat="server" ID="sm1">
    </asp:ScriptManager>
    <!-- MENSAJE!-->
    <asp:Panel ID="pnMensaje2" CssClass="" runat="server" Visible="true">
        <asp:Label ID="lblMensaje" runat="server" Text="" Visible="true"></asp:Label>
        <asp:Button ID="btnIngresaProv" runat="server" Text="Ingrese el proveedor" Visible="false" />
    </asp:Panel>

    <!-- CABECERA INGRESO DE SUCURSAL Y FECHAs  !-->
    <asp:Panel ID="pnTitulos" CssClass="" runat="server" Visible="true">

        <fieldset id="fdTitulos" class="fieldset-principal">
            <legend>Libro mayor</legend>
            <asp:Panel ID="pnDatos" CssClass="pnPeq" runat="server" Visible="true">
                <!--
                <asp:Label ID="lblSuc" runat="server" Text="Sucursal" CssClass="lblPeq"></asp:Label>

                <asp:DropDownList ID="ddlSucursal2" runat="server" CssClass="peqDdl" DataTextField="nom_suc" DataValueField="sucursal">
                </asp:DropDownList>

                <asp:DropDownList ID="ddlTipoDocumento" runat="server" CssClass="peqDdl" DataTextField="descrip" DataValueField="tipodoc">
                </asp:DropDownList>
                -->
                <asp:TextBox runat="server" ID="txtFechaIni" CssClass="txtPeq"></asp:TextBox>
                <act1:CalendarExtender ID="Calfecha" PopupButtonID="" runat="server" TargetControlID="txtFechaIni" Format="dd/MM/yyyy"></act1:CalendarExtender>
                <act1:MaskedEditExtender ID="maskFecha" runat="server" TargetControlID="txtFechaIni" Mask="99/99/9999"
                    MessageValidatorTip="true"
                    OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="date" InputDirection="RightToLeft"
                    AcceptNegative="Left"
                    DisplayMoney="Left" ErrorTooltipEnabled="True" />

                <asp:TextBox runat="server" ID="txtFechaFin" CssClass="txtPeq"></asp:TextBox>
                <act1:CalendarExtender ID="CalendarExtender1" PopupButtonID="" runat="server" TargetControlID="txtFechaFin"
                    Format="dd/MM/yyyy"></act1:CalendarExtender>
                <act1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtFechaFin" Mask="99/99/9999"
                    MessageValidatorTip="true"
                    OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="date" InputDirection="RightToLeft"
                    AcceptNegative="Left"
                    DisplayMoney="Left" ErrorTooltipEnabled="True" />



                <asp:Button ID="btnConsultar" runat="server" CssClass="btnProceso" Text="Consultar"
                    OnClick="btnConsultar_Click" />
            </asp:Panel>
        </fieldset>
    </asp:Panel>


    <asp:Panel ID="pnDocumentos" CssClass="" runat="server" Visible="true">

        <fieldset id="fdDocumentos" class="fieldset-principal">
            <legend></legend>

            <asp:Panel ID="pnExcel" CssClass="" runat="server" Wrap="False">
                <asp:Button ID="btnExcelFe" runat="server" CssClass="btnProceso " Text="A Excel" Visible="true" OnClick="btnExcelFe_Click" />
            </asp:Panel>


            <asp:GridView ID="grvContabilizacion" runat="server" DataKeyNames="cod_cta" AutoGenerateColumns="False" BackColor="White"
                BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="1" GridLines="Vertical"
                HorizontalAlign="Center" Width="90%" AllowPaging="false" AllowSorting="True" PageSize="100"
                OnRowDataBound="grvContabilizacion_RowDataBound" ShowFooter="True" OnSelectedIndexChanged="grvContabilizacion_SelectedIndexChanged">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
				<asp:CommandField ShowSelectButton="true" />
                    <asp:BoundField DataField="cod_cta" HeaderText="Código" Visible="TRUE" />
                    <asp:BoundField DataField="nom_cta" HeaderText="Cuenta" Visible="TRUE" />
                    <asp:BoundField DataField="debe" HeaderText="DEBE" DataFormatString="{0:N}" />
                    <asp:BoundField DataField="haber" HeaderText="HABER" DataFormatString="{0:N}" />
                    <asp:BoundField DataField="saldo" HeaderText="Saldo" DataFormatString="{0:N}" />

                    
                </Columns>
                <FooterStyle BackColor="#CCCCCC" ForeColor="red" Font-Size="Medium" />
                <HeaderStyle BackColor="#0C80BF" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#999999" ForeColor="black" HorizontalAlign="Center" />
                <RowStyle BackColor="#EEEEEE" ForeColor="black" />
                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#0000A9" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#000065" />
            </asp:GridView>



        </fieldset>
    </asp:Panel>
    <!--<asp:BoundField DataField="saldo" HeaderText="Saldo" DataFormatString="{0:N}" />-->
    <asp:Panel ID="pnMayores" CssClass="" runat="server" Visible="false">

        <fieldset id="fsMayores" class="fieldset-principal">
            <legend>Detalle Mayor</legend>
            <asp:Panel ID="Panel1" CssClass="" runat="server" Wrap="False">
                <asp:Button ID="btnExcelDet" runat="server" CssClass="btnProceso " Text="A Excel" Visible="true" OnClick="btnExcelDet_Click" />
                <asp:Button ID="btnRegresar" runat="server" CssClass="btnProceso" Text="Regresar" OnClick="btnRegresar_Click" />
            </asp:Panel>

            <asp:Label runat="server" ID="lblCuenta"></asp:Label>

            <asp:GridView ID="grvMayores" runat="server" DataKeyNames="cod_cta" AutoGenerateColumns="False" BackColor="White"
                BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="1" GridLines="Vertical"
                HorizontalAlign="Center" Width="90%" AllowPaging="false" AllowSorting="True" PageSize="100"
                OnRowDataBound="grvMayores_RowDataBound" ShowFooter="True" HtmlEncode="False;">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:BoundField DataField="cod_cta" HeaderText="Código" Visible="TRUE" />
                    <asp:BoundField DataField="nom_cta" HeaderText="Cuenta" Visible="TRUE" />
                    <asp:BoundField DataField="debe" HeaderText="DEBE" DataFormatString="{0:N}" />
                    <asp:BoundField DataField="haber" HeaderText="HABER" DataFormatString="{0:N}" />
                    <asp:BoundField DataField="haber" HeaderText="Saldo" DataFormatString="{0:N}" />
                    <asp:BoundField DataField="des_mov" HeaderText="Descripción" Visible="TRUE" />
                    <asp:BoundField DataField="doc_ref" HeaderText="Referencia" Visible="TRUE" />


                </Columns>
                <FooterStyle BackColor="#CCCCCC" ForeColor="red" Font-Size="Medium" />
                <HeaderStyle BackColor="#0C80BF" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#999999" ForeColor="black" HorizontalAlign="Center" />
                <RowStyle BackColor="#EEEEEE" ForeColor="black" />
                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#0000A9" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#000065" />
            </asp:GridView>
        </fieldset>

    </asp:Panel>
</asp:Content>

