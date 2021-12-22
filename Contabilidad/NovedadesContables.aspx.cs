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

public partial class Contabilidad_NovedadesContables : System.Web.UI.Page
{
    #region CONEXION BASE DE DATOS
    string conn = System.Configuration.ConfigurationManager.ConnectionStrings["bddComprobantesConnectionString"].ConnectionString;

    Data_bddComprobantesDataContext dc = new Data_bddComprobantesDataContext();

   

    string conn4 = System.Configuration.ConfigurationManager.ConnectionStrings["AWA_ACCOUNTINGConnectionString"].ConnectionString;
    Data_AWA_ACCOUTINGDataContext dw = new Data_AWA_ACCOUTINGDataContext();


    #endregion

    #region inicio
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            string accion = string.Empty;

            perfilUsuario();
            activarObjetos();

        }
    }
    #endregion

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
                Response.Redirect("~/inicio.aspx");
            }

            int nivel = (int)Session["SNivel"];
            int tipo = (int)Session["STipo"];

            if (nivel == 0
                || tipo == 0)
            {
                Response.Redirect("~/inicio.aspx");
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

            Response.Redirect("~/inicio.aspx");
            lblMensaje.Text = e.Message;
        }

    }

    protected void activarObjetos()
    {
        pnTitulos.Visible = true;


        lblMensaje.Text = string.Empty;
    }

    #endregion

    protected void btnTodos_Click(object sender, EventArgs e)
    {
        pnListado.Visible = true;
        btnTodos_Click();
    }

    protected void btnTodos_Click()
    {

        cargaGridTodos();
        btnExcelFe.Visible = true;
    }

    private void cargaGridTodos()
    {
        /* grvListadoFac.DataSource = retornaTodos();
         grvListadoFac.DataBind();*/

        string lsucursal = ddlSucursal2.SelectedValue.Trim();

        DateTime lfechainicio = Convert.ToDateTime(txtFechaIni.Text + " 00:00:00");
        DateTime lfechafin = Convert.ToDateTime(txtFechaFin.Text).AddDays(1);

        grvListadoFac.DataSource = dw.pawa_NovedadesAsientosContables("", lfechainicio, lfechafin);
        grvListadoFac.DataBind();
    }



    protected void btnConsolidado_Click(object sender, EventArgs e)
    {

    }

    protected void btnNovedadesAneta_Click(object sender, EventArgs e)
    {

    }



    #region PAGINACION

    public class classfac
    {
        public string FAC_SUCURSAL { get; set; }
        public string FAC_SECUENCIAL { get; set; }
        public string FAC_FECHAEMISION { get; set; }
        public string FAC_RAZONCOMPRADOR { get; set; }
        public string FAC_TOTALSINIMP { get; set; }
        public string FAC_TOTALDESCUENTO { get; set; }
        public string FAC_BASEIMPONIBLE { get; set; }
        public string FAC_VALORIMPUESTO { get; set; }
        public string FAC_IMPORTETOTAL { get; set; }
        public string FAC_VENDEDOR { get; set; }
        public string FAC_TIPOCONCEPTO { get; set; }
        public string FAC_OBSERVACION { get; set; }
        public string FAC_SRI { get; set; }




    }



    private void cargaGrid()
    {

    }




    protected void grvListadoFac_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

    }

    protected void grvListadoFac_PageIndexChanged(object sender, EventArgs e)
    {

    }
    protected void grvListadoFac_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        // int tipoConsulta = Convert.ToInt16(lblTipoConsulta.Text);

        grvListadoFac.PageIndex = e.NewPageIndex;
        // if (tipoConsulta == 1)
        // {
        cargaGrid();
        //}

        //if (tipoConsulta == 2)
        //{
        //  cargaGridTodos();
        //}
    }



    #endregion


    #region AEXEL
    protected void btnExcelFe_Click(object sender, EventArgs e)
    {
        uno();
    }
    protected void btnExcelC_Click(object sender, EventArgs e)
    {
        dos();
    }


    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
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
            grvListadoFac.AllowPaging = false;
            ///this.retornaTodos();

            grvListadoFac.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in grvListadoFac.HeaderRow.Cells)
            {
                cell.BackColor = grvListadoFac.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in grvListadoFac.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = grvListadoFac.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = grvListadoFac.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            grvListadoFac.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }


    protected void dos()
    {

    }
    #endregion
}