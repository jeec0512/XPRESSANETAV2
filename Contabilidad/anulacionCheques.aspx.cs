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

public partial class Contabilidad_anulacionCheques : System.Web.UI.Page
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
            int userId = Convert.ToInt32(Session["SUsuarioID"]);
            string accion = string.Empty;
            string controlEstudiantil = "http://192.168.1.118:8080/xpressapp/admin/banco.an?prmusrid=" + userId;
            


            perfilUsuario();
            activarObjetos();

            ifControlEst.Attributes["src"] = controlEstudiantil;

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


            var cSucursal = dc.sp_listarSucursal("", "", nivel, 0, sucursal);
            /*
            ddlSucursal2.DataSource = cSucursal;
            ddlSucursal2.DataBind();*/
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

    protected void btnNotas_Click(object sender, EventArgs e)
    {
        string usuario, Username, sUrl, sScript;

        usuario = Convert.ToString(Session["SUsuarioID"]);
        int userId = Convert.ToInt32(Session["SUsuarioID"]);

        if (usuario == "" || usuario == null)
            Response.Redirect("~/inicio.aspx");

        Username = (string)Session["SUsername"];

        sUrl = "http://192.168.1.118:8080/xpressapp/admin/banco.an?prmusrid=" + userId;


        
        sScript = "<script language =javascript> ";

        sScript += "window.open('" + sUrl + "',null,'toolbar=0,scrollbars=1,location=0,statusbar=0,menubar=0,resizable=1,width=1300,height=800,left=100,top=100');";
        sScript += "</script> ";

        Response.Write(sScript);

    }
}