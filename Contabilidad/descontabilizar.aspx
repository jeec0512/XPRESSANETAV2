<%@ Page Title="" Language="C#" MasterPageFile="~/Contabilidad/mpContabilidad.master" AutoEventWireup="true"
     CodeFile="descontabilizar.aspx.cs" Inherits="Contabilidad_descontabilizar" EnableEventValidation="false" %>

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
            <legend>Descontabilizar</legend>
            <asp:Panel ID="pnDatos" runat="server" Visible="true" Style="display: flex;">
                <div style="margin-left: 2rem;">
                    <!--
                <asp:Label ID="lblSuc" runat="server" Text="Sucursal" CssClass="lblPeq"></asp:Label>

                <asp:DropDownList ID="ddlSucursal2" runat="server" CssClass="peqDdl" DataTextField="nom_suc" DataValueField="sucursal">
                </asp:DropDownList>

                <asp:DropDownList ID="ddlTipoDocumento" runat="server" CssClass="peqDdl" DataTextField="descrip" DataValueField="tipodoc">
                </asp:DropDownList>
                -->

                    <asp:Panel ID="pnAno" runat="server" Style="display: grid;" Visible="true">
                        <asp:Label runat="server" ID="Label2" Text="Año" class="titulo3"></asp:Label>
                        <asp:DropDownList ID="ddlAno" runat="server" CssClass="mainSelect" Visible="true" Enabled="true" DataTextField="ANOS" DataValueField="ANOS" AutoPostBack="True">
                        </asp:DropDownList>
                    </asp:Panel>



                    <asp:Panel ID="pnPeriodo" runat="server" Style="display: grid;" Visible="true">
                        <asp:Label runat="server" ID="Label3" Text="Período" class="titulo3"></asp:Label>
                        <asp:DropDownList ID="ddlPeriodo" runat="server" CssClass="mainSelect" Visible="true" Enabled="true" DataTextField="DESCRIP" DataValueField="PERIODO" AutoPostBack="True">
                        </asp:DropDownList>
                    </asp:Panel>

                    <asp:Panel ID="pnSecuencial" runat="server" Style="display: grid;" Visible="true">
                        <asp:Label runat="server" ID="lblSecuencial" Text="Secuencial" class="titulo3"></asp:Label>
                        <asp:TextBox runat="server" ID="txtSecuencial" CssClass="txtPeq"></asp:TextBox>
                    </asp:Panel>



                </div>
                <div style="margin-left: 2rem;">
                    <asp:Button ID="btnConsultar" runat="server" CssClass="btnProceso" Text="Descontabilizar" OnClick="btnConsultar_Click" />
                </div>
            </asp:Panel>
        </fieldset>
    </asp:Panel>


    <asp:Panel ID="pnDocumentos" CssClass="" runat="server" Visible="false">

        <fieldset id="fdDocumentos" class="fieldset-principal">
            <legend></legend>

            <asp:Panel ID="pnExcel" CssClass="" runat="server" Wrap="False">
                <asp:Button ID="btnExcelFe" runat="server" CssClass="btnProceso " Text="A Excel" Visible="false" OnClick="btnExcelFe_Click" />
            </asp:Panel>


            <asp:GridView ID="grvContabilizacion" runat="server" DataKeyNames="CODIGO_CUENTA_CONTABLE" AutoGenerateColumns="False" BackColor="White"
                BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="1" GridLines="Vertical"
                HorizontalAlign="Center" Width="90%" AllowPaging="false" AllowSorting="True" PageSize="10000"
                OnRowDataBound="grvContabilizacion_RowDataBound" ShowFooter="false" OnSelectedIndexChanged="grvContabilizacion_SelectedIndexChanged">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:CommandField ShowSelectButton="true" />
                    <asp:BoundField DataField="CODIGO_CUENTA_CONTABLE" HeaderText="Código" Visible="TRUE" />
                    <asp:BoundField DataField="NOMBRE_CUENTA_CONTABLE" HeaderText="Cuenta" Visible="TRUE" />
                    <asp:BoundField DataField="SALDO_INICIAL" HeaderText="Saldo inicial" Visible="TRUE"  DataFormatString="{0:N}"/>
                    <asp:BoundField DataField="DEBE" HeaderText="DEBE" DataFormatString="{0:N}" />
                    <asp:BoundField DataField="HABER" HeaderText="HABER" DataFormatString="{0:N}" />
                    <asp:BoundField DataField="SALDO" HeaderText="Saldo final" DataFormatString="{0:N}" />
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
                
            </asp:Panel>
            <div style="display:flex;">
                <div>
                <asp:Button ID="btnExcelDet" runat="server" CssClass="btnProceso " Text="A Excel" Visible="true" OnClick="btnExcelDet_Click" />
                <asp:Button ID="btnRegresar" runat="server" CssClass="btnProceso" Text="Regresar" OnClick="btnRegresar_Click" />
                </div>
                <div Style="font-size:1rem; color:blue;border: 1px solid blue;margin:5px;padding:10px;">
                <asp:Label runat="server" ID="lblCuenta" Visible="true" Style="font-size:1rem; color:blue;margin:5px;padding:10px;"></asp:Label>
                <asp:Label runat="server" ID="lblDetalle" Visible="true" Style="font-size:1rem; color:blue;margin:5px;padding:10px;"></asp:Label>
                </div>
                <div Style="font-size:1rem; color:blue;border: 1px solid blue;margin:5px;padding:10px;">
                <asp:Label runat="server" ID="lblSaldo" Text="Saldo inicial" Visible="true" Style="font-size:1rem; color:blue;margin:5px;padding:10px;"></asp:Label>
                <asp:Label runat="server" ID="lblSaldoInicial" Visible="true" Style="font-size:1rem; color:blue;margin:5px;padding:10px;"></asp:Label>
                </div>
                <asp:Label runat="server" ID="lblSaldoFinal" Visible="false" Style="font-size:1rem; color:blue;"></asp:Label>
            </div>

            <asp:GridView ID="grvMayores" runat="server" DataKeyNames="cod_cta" AutoGenerateColumns="False" BackColor="White"
                BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="1" GridLines="Vertical"
                HorizontalAlign="Center" Width="90%" AllowPaging="false" AllowSorting="True" PageSize="100"
                OnRowDataBound="grvMayores_RowDataBound" ShowFooter="True" HtmlEncode="False;">
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:BoundField DataField="cod_cta" HeaderText="Código" Visible="false" />
                    <asp:BoundField DataField="nom_cta" HeaderText="Cuenta" Visible="false" />
                    <asp:BoundField DataField="des_mov" HeaderText="Descripción" Visible="TRUE" />
                    <asp:BoundField DataField="doc_ref" HeaderText="Referencia" Visible="TRUE" />
                    <asp:BoundField DataField="debe" HeaderText="DEBE" DataFormatString="{0:N}"/>
                    <asp:BoundField DataField="haber" HeaderText="HABER" DataFormatString="{0:N}" />
                    <asp:BoundField DataField="saldo" HeaderText="Saldo" DataFormatString="{0:N}" />
                    <asp:BoundField DataField="fecha" HeaderText="Fecha"  DataFormatString="{0:d}"/>
                    <asp:BoundField DataField="secuencial_ejercicio" HeaderText="# ejercicio"  />
                    <asp:BoundField DataField="secuencial_tipo_documento" HeaderText="# Tipo/Doc."  />
                    <asp:BoundField DataField="cod_suc" HeaderText="Sucursal"  />
                    <asp:BoundField DataField="cod_cco" HeaderText="CCosto"  />
                    <asp:BoundField DataField="cod_ter" HeaderText="Tercero"  />
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

