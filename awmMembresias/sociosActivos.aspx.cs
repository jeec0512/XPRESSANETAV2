﻿using AjaxControlToolkit;
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

public partial class awmMembresias_sociosActivos : System.Web.UI.Page
{
    #region CONEXION BASE DE DATOS
    string conn = System.Configuration.ConfigurationManager.ConnectionStrings["bddComprobantesConnectionString"].ConnectionString;

    Data_bddComprobantesDataContext dc = new Data_bddComprobantesDataContext();

    string conn1 = System.Configuration.ConfigurationManager.ConnectionStrings["DATACOREConnectionString"].ConnectionString;

    Data_DatacoreDataContext df = new Data_DatacoreDataContext();

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

    #endregion

    protected void listaReportes(string ltipo, string laccion1, string laccion2)
    {
        int tipo = (int)Session["STipo"];
        string lsucursal;
        DateTime lfechaInicio, lfechaFin;


        lsucursal = ddlSucursal2.SelectedValue.Trim();
        lfechaInicio = Convert.ToDateTime(txtFechaIni.Text);
        lfechaFin = Convert.ToDateTime(txtFechaFin.Text);

        DateTime lfecha = DateTime.Today;

        string ano = Convert.ToString(lfecha.Year);
        string mes = llenarCeros(Convert.ToString(lfecha.Month).Trim(), '0', 2);
        string dia = llenarCeros(Convert.ToString(lfecha.Day).Trim(), '0', 2);



        if (ltipo == "1")
        {
            if (tipo == 3 || tipo == 4)
            {
                laccion1 = "DETALLE";
                laccion2 = "TOTAL";
               // grvSocios.DataSource = dc.sp_sociosActivosALaFecha_awm(laccion1,lfecha, ano, mes, dia, lsucursal);
                //grvSocios.DataBind();

                //grvTotalSocios.DataSource = dc.sp_sociosActivosALaFechaTotal_awm(laccion2,  ano, mes, dia, lsucursal,lfecha);
                //grvTotalSocios.DataBind();
            }
            else
            {
                laccion1 = "DETSUC";
                laccion2 = "TOTSUC";

                //grvSocios.DataSource = dc.sp_sociosActivosALaFecha(laccion1, ano, mes, dia, lsucursal);
                //grvSocios.DataBind();

                //grvTotalSocios.DataSource = dc.sp_sociosActivosALaFechaTotal(laccion2, ano, mes, dia, lsucursal);
                //grvTotalSocios.DataBind();
            }

        }

        if (ltipo == "2")
        {
            //grvSocios.DataSource = dc.sp_sociosActivosALaFecha(laccion1, ano, mes, dia, lsucursal);
            //grvSocios.DataBind();

            //grvTotalSocios.DataSource = dc.sp_sociosActivosALaFechaTotal(laccion2, ano, mes, dia, lsucursal);
            //grvTotalSocios.DataBind();


        }
    }

    protected void btnSocTotal_Click(object sender, EventArgs e)
    {
        string ltipo, laccion1, laccion2;

        // pnSocios.GroupingText = "Socios ANETA";

        ltipo = "1";
        laccion1 = "DETALLE";
        laccion2 = "TOTAL";

        listaReportes(ltipo, laccion1, laccion2);
        pnSocios.Visible = true;
        pnTotalSocios.Visible = true;

        btnExcelSA.Visible = true;
        btnExcelSS.Visible = true;
    }

    protected void btnSocxSuc_Click(object sender, EventArgs e)
    {
        string ltipo, laccion1, laccion2;

        //pnSocios.GroupingText = "Socios por sucursal";

        ltipo = "2";
        laccion1 = "DETSUC";
        laccion2 = "TOTSUC";

        listaReportes(ltipo, laccion1, laccion2);
        pnSocios.Visible = true;
        pnTotalSocios.Visible = true;

        btnExcelSA.Visible = true;
        btnExcelSS.Visible = true;
    }

    #region EXCEL
    protected void btnExcelSA_Click(object sender, EventArgs e)
    {
        uno();

    }
    protected void btnExcelSS_Click(object sender, EventArgs e)
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
            grvSocios.AllowPaging = false;
            /// this.BindGrid();

            grvSocios.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in grvSocios.HeaderRow.Cells)
            {
                cell.BackColor = grvSocios.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in grvSocios.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = grvSocios.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = grvSocios.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            grvSocios.RenderControl(hw);

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
            grvTotalSocios.AllowPaging = false;
            /// this.BindGrid();

            grvTotalSocios.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in grvTotalSocios.HeaderRow.Cells)
            {
                cell.BackColor = grvTotalSocios.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in grvTotalSocios.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = grvTotalSocios.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = grvTotalSocios.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            grvTotalSocios.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }
    #endregion


    protected string llenarCeros(string cadenasinceros, char llenarCon, int numeroDecaracteres)
    {
        string conceros;

        conceros = cadenasinceros;
        conceros = conceros.PadLeft(numeroDecaracteres, llenarCon);
        return conceros;
    }
}