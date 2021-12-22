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
public partial class Contabilidad_descontabilizar : System.Web.UI.Page
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
            listarAnos();
            listarPeriodos();


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

            var cSucursal = dc.sp_listarSucursal("", "", nivel, 0, sucursal);

            ddlSucursal2.DataSource = cSucursal;
            ddlSucursal2.DataBind();

            var cTipoDoc = dc.sp_ListarTipoDoc("");
            ddlTipoDocumento.DataSource = cTipoDoc;
            ddlTipoDocumento.DataBind();


            // var cSucursal = dc.sp_listarSucursal("", "", nivel, 0, sucursal);
            //ddlSucursal2.DataSource = cSucursal;
            // ddlSucursal2.DataBind();
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
        pnDatos.Visible = true;


        lblMensaje.Text = string.Empty;
    }

    protected bool fechaValida()
    {
        bool fechaValida = false;
        var date1 = DateTime.Now;

        DateTime dt1 = DateTime.Now;
        DateTime dt2 = dt1;

        var culture = CultureInfo.CreateSpecificCulture("es-MX");
        var styles = DateTimeStyles.None;


        // fechaValida = DateTime.TryParse(date1, culture, styles, out dt1);
        fechaValida = true;

        return fechaValida;
    }

    protected void listarAnos()
    {
        var cAno = dc.sp_ListarAnos("NOBLOQUEO", "N");
        ddlAno.DataSource = cAno;
        ddlAno.DataBind();

        ListItem listMod = new ListItem("Seleccione el año", "-1");
        ddlAno.Items.Insert(0, listMod);
    }
    protected void listarPeriodos()
    {
        var cPeriodo = dc.sp_ListarPeriodos("NOBLOQUEO", "N");
        ddlPeriodo.DataSource = cPeriodo;
        ddlPeriodo.DataBind();

        ListItem listMod = new ListItem("Seleccione el período", "-1");
        ddlPeriodo.Items.Insert(0, listMod);

    }

    #endregion


    protected void ibConsultar_Click(object sender, ImageClickEventArgs e)
    {
        ibConsultar_Click();
    }

    protected void ibConsultar_Click()
    {

        btnConsultarMayor_Click();

    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        btnConsultar_Click();
    }

    protected void btnConsultarMayor_Click()
    {
        string lAccion;
        DateTime lFechaIni = DateTime.Today;
        DateTime lFechaFin = DateTime.Today;
        string tipoDoc = ddlTipoDocumento.SelectedValue;
        string sucursal = ddlSucursal2.SelectedValue;
        string cuenta = lblCuenta.Text;
        decimal saldo = Convert.ToDecimal(lblSaldoInicial.Text);
        lAccion = "CONSULTAR";

        int year = Convert.ToInt32(ddlAno.SelectedValue);
        short periodo = Convert.ToInt16(ddlPeriodo.SelectedValue);

        //var cDocumentos = dc.sp_LibroMayorxCuenta_v2(lAccion, lFechaIni, lFechaFin, sucursal, cuenta);

        var cDocumentos = dw.pLibroMayorCuenta(year, periodo, cuenta, saldo);

        grvMayores.DataSource = cDocumentos;
        grvMayores.DataBind();

        pnDocumentos.Visible = true;

    }

    protected void btnConsultar_Click()
    {
        string lAccion;
        DateTime lFechaIni = DateTime.Today;
        DateTime lFechaFin = DateTime.Today;
        string tipoDoc = ddlTipoDocumento.SelectedValue;
        string sucursal = ddlSucursal2.SelectedValue;
        int lusuario = Convert.ToInt32(Session["SUsuarioID"]);
        int secuencial = 0;

        string referencia = string.Empty;
        int? numero = 0;

        lAccion = "CONSULTAR";
        secuencial = Convert.ToInt32(txtSecuencial.Text);

        int year = Convert.ToInt32(ddlAno.SelectedValue);
        int periodo = Convert.ToInt32(ddlPeriodo.SelectedValue);

        dw.CommandTimeout = 360;
        //var cDocumentos = dc.sp_LibroMayorGeneral_v2(lAccion, lFechaIni, lFechaFin, sucursal);
        //var cDocumentos = dc.pLibroMayorGeneral(year,periodo);
        var cDocumentos = dw.pAnularComprobante(secuencial, year, periodo, lusuario, ref numero, ref referencia);

        lblMensaje.Text = "Comprobante (s) anulados";

        pnDocumentos.Visible = true;

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

    protected void dos()
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
            grvMayores.AllowPaging = false;
            /// this.BindGrid();

            grvMayores.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in grvMayores.HeaderRow.Cells)
            {
                cell.BackColor = grvMayores.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in grvMayores.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = grvMayores.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = grvMayores.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            grvMayores.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }

    #endregion

    #region SUMA DE VALORES
    protected void grvContabilizacion_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string dato;
        if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.RowType != DataControlRowType.EmptyDataRow))
        {
            dato = Convert.ToString(e.Row.Cells[3].Text);


            debe += Convert.ToDecimal(e.Row.Cells[3].Text);
            haber += Convert.ToDecimal(e.Row.Cells[4].Text);
            saldo += Convert.ToDecimal(e.Row.Cells[5].Text);

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[2].Text = "Totales";

            e.Row.Cells[3].Text = Convert.ToString(debe);
            e.Row.Cells[4].Text = Convert.ToString(haber);
            e.Row.Cells[5].Text = Convert.ToString(saldo);

        }
    }

    protected void grvMayores_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string dato;
        if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.RowType != DataControlRowType.EmptyDataRow))
        {
            dato = Convert.ToString(e.Row.Cells[3].Text);


            debe += Convert.ToDecimal(e.Row.Cells[4].Text);
            haber += Convert.ToDecimal(e.Row.Cells[5].Text);
            //  e.Row.Cells[4].Text = Convert.ToString(Convert.ToDecimal(e.Row.Cells[2].Text) - Convert.ToDecimal(e.Row.Cells[3].Text));


        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[3].Text = "Totales";

            e.Row.Cells[4].Text = Convert.ToString(debe);
            e.Row.Cells[5].Text = Convert.ToString(haber);
            //e.Row.Cells[5].Text = Convert.ToString(debe - haber);


        }
    }
    #endregion
    protected void grvContabilizacion_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblCuenta.Text = Convert.ToString(grvContabilizacion.SelectedValue).Trim();
        //lblDetalle .Text = this.grvContabilizacion.Rows[this.grvContabilizacion.SelectedIndex].Cells[1].Text;
        lblDetalle.Text = this.grvContabilizacion.Rows[this.grvContabilizacion.SelectedIndex].Cells[2].Text;
        lblSaldoInicial.Text = this.grvContabilizacion.Rows[this.grvContabilizacion.SelectedIndex].Cells[3].Text;

        ibConsultar_Click();
        pnDocumentos.Visible = false;
        pnMayores.Visible = true;


    }




    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        pnDocumentos.Visible = true;
        pnMayores.Visible = false;

    }
}