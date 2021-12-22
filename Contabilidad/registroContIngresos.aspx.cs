using AjaxControlToolkit;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


public partial class Contabilidad_registroContIngresos : System.Web.UI.Page
{
    #region VARIABLES GENERALES
    decimal debe = 0;
    decimal haber = 0;
    decimal saldo = 0;
    #endregion
    #region CONEXION BASE DE DATOS
    string conn = System.Configuration.ConfigurationManager.ConnectionStrings["bddComprobantesConnectionString"].ConnectionString;

    Data_bddComprobantesDataContext dc = new Data_bddComprobantesDataContext();

    string conn4 = System.Configuration.ConfigurationManager.ConnectionStrings["AWA_ACCOUNTINGConnectionString"].ConnectionString;
    Data_AWA_ACCOUTINGDataContext dw = new Data_AWA_ACCOUTINGDataContext();

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            string accion = string.Empty;

            perfilUsuario();
            activarObjetos();

        }
    }

    #region PROCESOS INTERNOS

    protected void perfilUsuario()
    {
        try
        {
            string grupo = (string)Session["SGrupo"];
            string sucursal = (string)Session["SSucursal"];
            if (grupo == ""
               || grupo == null
               || sucursal == ""
               || sucursal == null)
            {
                Response.Redirect("~/ingresar.aspx");
            }

            int nivel = (int)Session["SNivel"];
            int tipo = (int)Session["STipo"];

            if (nivel == 0
                || tipo == 0)
            {
                Response.Redirect("~/ingresar.aspx");
            }

            DateTime lfecha = DateTime.Today;
            txtFechaIni.Text = Convert.ToString(lfecha);

            txtFechaFin.Text = Convert.ToString(lfecha);
            var cSucursal = dc.sp_listarSucursal("", "", nivel, 0, sucursal);


            ddlSucursal2.DataSource = cSucursal;
            ddlSucursal2.DataBind();

            ListItem listSuc = new ListItem("Todas las Escuelas", "-1");
            ddlSucursal2.Items.Insert(0, listSuc);

            var cTipoDoc = dc.sp_ListarTipoDoc("");
            ddlTipoDocumento.DataSource = cTipoDoc;
            ddlTipoDocumento.DataBind();

        }
        catch (InvalidCastException e)
        {

            Response.Redirect("~/ingresar.aspx");
            lblMensaje.Text = e.Message;
        }

    }

    protected void activarObjetos()
    {
        pnTitulos.Visible = true;
        pnDatos.Visible = true;


        lblMensaje.Text = string.Empty;
    }


    #endregion


    protected void btnIngresos_Click(object sender, EventArgs e)
    {
        btnIngresos_Click();
    }



    #region AEXCEL

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    protected void btnExcelFe_Click(object sender, EventArgs e)
    {
        uno();
    }
    protected void btnExcelDet_Click(object sender, EventArgs e)
    {
        dos();
    }
    protected void uno()
    {

    }
    protected void dos()
    {

    }

    #endregion

    #region SUMA DE VALORES
    protected void grvDocumentoCabecera_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string dato;
        if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.RowType != DataControlRowType.EmptyDataRow))
        {
            dato = Convert.ToString(e.Row.Cells[2].Text);

            /* debe += Convert.ToDecimal(e.Row.Cells[4].Text);
             haber += Convert.ToDecimal(e.Row.Cells[5].Text);
             saldo += Convert.ToDecimal(e.Row.Cells[6].Text);*/

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[1].Text = "Totales";

            e.Row.Cells[3].Text = Convert.ToString(debe);
            e.Row.Cells[4].Text = Convert.ToString(haber);
            e.Row.Cells[5].Text = Convert.ToString(saldo);

        }
    }

    protected void grvDiarios_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string dato;
        if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.RowType != DataControlRowType.EmptyDataRow))
        {
            dato = Convert.ToString(e.Row.Cells[3].Text);


            debe += Convert.ToDecimal(e.Row.Cells[2].Text);
            haber += Convert.ToDecimal(e.Row.Cells[3].Text);
            //saldo = debe - haber;

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[1].Text = "Totales";

            e.Row.Cells[2].Text = Convert.ToString(debe);
            e.Row.Cells[3].Text = Convert.ToString(haber);
            e.Row.Cells[4].Text = Convert.ToString(debe - haber);

        }
    }
    #endregion

    #region PARA SELECCIONAR UN REGISTRO
    protected void grvContabilizacion_SelectedIndexChanged(object sender, EventArgs e)
    {



    }




    protected void btnRegresar_Click(object sender, EventArgs e)
    {


    }

    protected void ibConsultar_Click(object sender, ImageClickEventArgs e)
    {
        ibConsultar_Click();
    }

    protected void ibConsultar_Click()
    {

        btnConsultarMayor_Click();

    }



    protected void btnConsultarMayor_Click()
    {
        string lAccion;
        DateTime lFechaIni = DateTime.Today;
        DateTime lFechaFin = DateTime.Today;
        string tipoDoc = ddlTipoDocumento.SelectedValue;
        string sucursal = ddlSucursal2.SelectedValue;

        lAccion = "CONSULTAR";


        int lusuario = Convert.ToInt32(Session["SUsuarioID"]);

        //var cDocumentos = dc.sp_libroDiarioxId_V2(lAccion, lFechaIni, lFechaFin, sucursal, cuenta);


    }
    #endregion


    protected void grvDocumentoCabecera_SelectedIndexChanged(object sender, EventArgs e)
    {

        ibConsultar_Click();

    }

    protected void btnRegresar_Click1(object sender, EventArgs e)
    {

    }

    protected void btnAutoconsumos_Click(object sender, EventArgs e)
    {
        string lAccion;
        DateTime lFechaIni = Convert.ToDateTime(txtFechaIni.Text);
        DateTime lFechaFin = Convert.ToDateTime(txtFechaFin.Text);
        string tipoDoc = ddlTipoDocumento.SelectedValue;
        string sucursal = ddlSucursal2.SelectedValue;
        string referencia = string.Empty;
        int? numero = 0;


        lAccion = "CONSULTAR";


        int lusuario = Convert.ToInt32(Session["SUsuarioID"]);

        dw.CommandTimeout = 1080;

        //var cDocumentos = dw.sp_contabilizacionEgresos_v5(lAccion, lFechaIni, lFechaFin, lusuario);

        var cDocumentos = dw.pBatchContabilizacionAutoconsumos(lFechaIni, lFechaFin, sucursal, lusuario, true, ref numero, ref referencia);



        lblMensaje.Text = "El registro contable de AUTOCONSUMOS se ha generado correctamente.";
    }

    protected void btnIngresos_Click()
    {
        string lAccion;
        DateTime lFechaIni = Convert.ToDateTime(txtFechaIni.Text);
        DateTime lFechaFin = Convert.ToDateTime(txtFechaFin.Text);
        string tipoDoc = ddlTipoDocumento.SelectedValue;
        string sucursal = ddlSucursal2.SelectedValue;
        string referencia = string.Empty;
        int? numero = 0;
        lAccion = "CONSULTAR";

        if (sucursal == "-1")
        {
            sucursal = null;
        }

        int lusuario = Convert.ToInt32(Session["SUsuarioID"]);

        dw.CommandTimeout = 360;

        //var cDocumentos = dw.sp_contabilizacionVentas_v3(lAccion, lFechaIni, lFechaFin, lusuario);

        var cDocumentos = dw.pBatchContabilizacionVentas(lFechaIni, lFechaFin, sucursal, lusuario, true, ref numero, ref referencia);

        lblMensaje.Text = "El registro contable de INGRESOS se ha generado correctamente.";


    }
    protected void btnEgresos_Click(object sender, EventArgs e)
    {
        string lAccion;
        DateTime lFechaIni = Convert.ToDateTime(txtFechaIni.Text);
        DateTime lFechaFin = Convert.ToDateTime(txtFechaFin.Text);
        string tipoDoc = ddlTipoDocumento.SelectedValue;
        string sucursal = ddlSucursal2.SelectedValue;
        string referencia = string.Empty;
        int? numero = 0;

        lAccion = "CONSULTAR";


        int lusuario = Convert.ToInt32(Session["SUsuarioID"]);

        dw.CommandTimeout = 1080;

        //var cDocumentos = dw.sp_contabilizacionEgresos_v5(lAccion, lFechaIni, lFechaFin, lusuario);

        //var cDocumentos = dw.pBatchContabilizacionEgresos(lFechaIni, lFechaFin, sucursal, lusuario, ref numero, ref referencia);

        //var cDocumentos = dw.pBatchContabilizacionEgresos(lFechaIni, lFechaFin, sucursal, lusuario, "S", true, ref numero, ref referencia);

        lblMensaje.Text = "El registro contable de EGRESOS se ha generado correctamente.";

    }
}