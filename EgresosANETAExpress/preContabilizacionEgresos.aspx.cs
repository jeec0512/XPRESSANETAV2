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

public partial class EgresosANETAExpress_preContabilizacionEgresos : System.Web.UI.Page
{

    /************VARIABLES GENERALES***********************/
    decimal cantidad = 0;
    decimal ventabruta = 0;
    decimal descuento = 0;
    decimal ventaneta = 0;
    decimal baseimponible = 0;
    decimal iva0 = 0;
    decimal iva12 = 0;
    decimal cxp = 0;
    decimal Total = 0;


    decimal Icantidad = 0;
    decimal Iventabruta = 0;
    decimal Idescuento = 0;
    decimal Iventaneta = 0;
    decimal Ibaseimponible = 0;
    decimal Iiva0 = 0;
    decimal Iiva12 = 0;
    decimal Icxp = 0;
    decimal ITotal = 0;

    decimal debe = 0;
    decimal haber = 0;

    /******************************************************/
    #region CONEXION BASE DE DATOS
    string conn = System.Configuration.ConfigurationManager.ConnectionStrings["bddComprobantesConnectionString"].ConnectionString;

    Data_bddComprobantesDataContext dc = new Data_bddComprobantesDataContext();

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
        }
        catch (InvalidCastException e)
        {

            Response.Redirect("~/ingresar.aspx");
            lblMensaje.Text = e.Message;
        }

    }
    protected void activarObjetos()
    {
    }

    protected void btnAsiento_Click(object sender, EventArgs e)
    {
        string laccion = "DETALLE";
        string lsucursal = ddlSucursal2.SelectedValue.Trim();
        string caja = ddlCaja.SelectedValue;
        DateTime lfechaInicio = Convert.ToDateTime(txtFechaIni.Text);
        DateTime lfechaFin = Convert.ToDateTime(txtFechaFin.Text);

        string referencia = string.Empty;
        int? numero = 0;

        lblMensaje.Text = Convert.ToString(DateTime.Now);
        dc.CommandTimeout = 360;
        // grvContabilizacion.DataSource = dc.sp_verContabilizacionGastos(laccion, lfechaInicio, lfechaFin, lsucursal);
        // grvContabilizacion.DataSource = dc.sp_verContabilizacionGastosDocsSRI(laccion, lfechaInicio, lfechaFin, lsucursal,caja);
        //grvContabilizacion.DataSource = dc.sp_contabilizarGastosDocsSRI_jeec(lfechaInicio, lfechaFin, lsucursal, caja);

        grvContabilizacion.DataSource = dc.pConsultarEgresos(lfechaInicio,lfechaFin,lsucursal,caja,ref numero,ref referencia) ;
        grvContabilizacion.DataBind();


        laccion = "TOTAL";
        //grvCuadre.DataSource = dc.sp_verContabilizacionGastos(laccion, lfechaInicio, lfechaFin, lsucursal);
        grvCuadre.DataSource = dc.sp_verContabilizacionGastosDocsSRI(laccion, lfechaInicio, lfechaFin, lsucursal, caja);
        grvCuadre.DataBind();
    }


    /********************************EXCEL*******************************/
    #region AEXCEL

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    protected void btnExcelFe_Click(object sender, EventArgs e)
    {
        uno();
    }

    protected void uno()
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            //To Export all pages
            grvContabilizacion.AllowPaging = false;
            /// this.BindGrid();

            grvContabilizacion.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in grvContabilizacion.HeaderRow.Cells)
            {
                cell.BackColor = grvContabilizacion.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in grvContabilizacion.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = grvContabilizacion.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = grvContabilizacion.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            grvContabilizacion.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }


    #endregion
    protected void grvContabilizacion_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string dato;
        if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.RowType != DataControlRowType.EmptyDataRow))
        {
            dato = Convert.ToString(e.Row.Cells[1].Text);


            debe += Convert.ToDecimal(e.Row.Cells[3].Text);
            haber += Convert.ToDecimal(e.Row.Cells[4].Text);

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[2].Text = "Totales";

            e.Row.Cells[3].Text = Convert.ToString(debe);
            e.Row.Cells[4].Text = Convert.ToString(haber);

        }
    }
}