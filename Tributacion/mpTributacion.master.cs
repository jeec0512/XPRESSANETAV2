﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Tributacion_mpTributacion : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            calificarOpciones();
        }
    }

    #region CALIFICAR OPCIONES
    protected void calificarOpciones()
    {
        /*CALIFICAR OPCIONES*/
        string accion, grupo, menu,submenu, boton;

        submenu = string.Empty;
        boton = string.Empty;



        accion = string.Empty;
        grupo = (string)Session["Sgrupo"];

        menu = "TRIBUTACION";
        submenu = "";

        /*
        lRetencion.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);
        submenu = "SUCURSAL";
        lSucursal.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);
        submenu = "PRODUCTO";
        lProducto.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);
        submenu = "COLABORADOR";
        lColaborador.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);
        submenu = "VENDEDOR";
        lVendedor.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);
        submenu = "PLANDECUENTAS";
        lCuentas.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);
        submenu = "USUARIO";
        lUsuario.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);
        */
    }
    #endregion  
}
