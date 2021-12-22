<%@ Page Title="" Language="C#" MasterPageFile="~/Contabilidad/mpContabilidad.master" AutoEventWireup="true" CodeFile="registroContEgresos.aspx.cs"
    Inherits="Contabilidad_registroContEgresos" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contenidoPrincipal" runat="Server">
    <asp:ScriptManager runat="server" ID="sm1">
    </asp:ScriptManager>

    <!-- MENSAJE!-->
    <asp:Panel ID="pnMensaje2" CssClass="" runat="server" Visible="true">
        <asp:Label ID="lblMensaje" runat="server" Text="" Visible="true"></asp:Label>
        <asp:Button ID="btnIngresaProv" runat="server" Text="Ingrese el proveedor" Visible="false" />
    </asp:Panel>

    <asp:Panel ID="Panel1" CssClass="" runat="server" Visible="true">

        <fieldset id="Fieldset1" class="fieldset-principal">
            <legend>Rango de fechas</legend>
            <asp:Panel ID="pnFechas" CssClass="pnPeq" runat="server" Visible="true">
                <asp:Label ID="lblFechaIni" runat="server" Text="Fecha de inicio" CssClass="lblPeq"></asp:Label>
                <asp:TextBox runat="server" ID="txtFechaIni" CssClass="txtPeq"></asp:TextBox>
                <act1:CalendarExtender ID="Calfecha" PopupButtonID="" runat="server" TargetControlID="txtFechaIni" Format="dd/MM/yyyy"></act1:CalendarExtender>
                <act1:MaskedEditExtender ID="maskFecha" runat="server" TargetControlID="txtFechaIni" Mask="99/99/9999"
                    MessageValidatorTip="true"
                    OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="date" InputDirection="RightToLeft"
                    AcceptNegative="Left"
                    DisplayMoney="Left" ErrorTooltipEnabled="True" />

                <asp:Label ID="lblFechaFin" runat="server" Text="Fecha de finalización" CssClass="lblPeq"></asp:Label>
                <asp:TextBox runat="server" ID="txtFechaFin" CssClass="txtPeq"></asp:TextBox>
                <act1:CalendarExtender ID="CalendarExtender1" PopupButtonID="" runat="server" TargetControlID="txtFechaFin"
                    Format="dd/MM/yyyy"></act1:CalendarExtender>
                <act1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtFechaFin" Mask="99/99/9999"
                    MessageValidatorTip="true"
                    OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="date" InputDirection="RightToLeft"
                    AcceptNegative="Left"
                    DisplayMoney="Left" ErrorTooltipEnabled="True" />
            </asp:Panel>
        </fieldset>
    </asp:Panel>
    <!-- CABECERA INGRESO DE SUCURSAL Y FECHAs  !-->
    <asp:Panel ID="pnTitulos" CssClass="" runat="server" Visible="true">

        <fieldset id="fdTitulos" class="fieldset-principal">
            <legend>Registro contable de Ingresos y egresos</legend>
            <asp:Panel ID="pnDatos" CssClass="pnPeq" runat="server" Visible="true">
                <div id="Div1" runat="server" visible="true">
                    <asp:Label ID="lblSuc" runat="server" Text="Sucursal" CssClass="lblPeq"></asp:Label>
                    <asp:DropDownList ID="ddlSucursal2" runat="server" CssClass="peqDdl" DataTextField="nom_suc" DataValueField="sucursal">
                    </asp:DropDownList>
                </div>
                <div id="Div3" runat="server" visible="true">
                    <asp:Label ID="lblCaja" runat="server" Text="Sucursal" CssClass="lblPeq"></asp:Label>
                    <asp:Panel ID="pnCaja" runat="server" CssClass="pnFormDdl">
                    <asp:DropDownList ID="ddlCaja" runat="server">
                        <asp:ListItem Value="S">S CAJA GENERAL</asp:ListItem>
                        <asp:ListItem Value="Z">Z CAJA CHICA</asp:ListItem>
                    </asp:DropDownList>
                </asp:Panel>
                    </div>
                <!--<asp:DropDownList ID="ddlTipoDocumento" runat="server" CssClass="peqDdl" DataTextField="descrip" DataValueField="tipodoc">
                </asp:DropDownList> -->

                <div id="Div2" runat="server" style="margin: 2rem;">

                    <asp:Button ID="btnIngresos" runat="server" CssClass="btnProceso" Text="Proceso registro contable de Ingresos" OnClick="btnIngresos_Click" Style="margin-bottom: 1rem; padding-top: 1rem;" Visible="false" />
                    <asp:Button ID="btnEgresos" runat="server" CssClass="btnProceso" Text="Proceso registro contable de Egresos" OnClick="btnEgresos_Click" Style="margin-bottom: 1rem; padding-top: 1rem" />
                    <asp:Button ID="btnAutoconsumos" runat="server" CssClass="btnProceso" Text="Proceso registro contable de Autoconsumos" Style="margin-bottom: 1rem; padding-top: 1rem" OnClick="btnAutoconsumos_Click" />

                </div>
                <div id="procesandoCnt" style="display: none; width: 30vw; height: 30vh; background-color: rgba(128, 128, 128,0.5); z-index: 999">
                    <p style="color: rgb(255, 0, 0); font-size: large">
                        Procesando...
                    </p>
                </div>
            </asp:Panel>
        </fieldset>
    </asp:Panel>







    <script type="text/javascript">
        function desactivarBoton() {
            document.getElementById("contenidoPrincipal_contenidoPrincipal_btnIngresos").style.display = "none";
            document.getElementById("contenidoPrincipal_contenidoPrincipal_btnEgresos").style.display = "none";
            document.getElementById("procesandoCnt").style.display = "block";
        }
        function activarBoton() {
            document.getElementById("contenidoPrincipal_contenidoPrincipal_btnConsultar").disabled = false;
            document.getElementById("contenidoPrincipal_contenidoPrincipal_btnExcelFe").disabled = false;
            document.getElementById("procesandoCnt").style.display = "none";
        }
        window.onbeforeunload = desactivarBoton;

    </script>
</asp:Content>

