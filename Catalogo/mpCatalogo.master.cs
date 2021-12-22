﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Catalogo_mpCatalogo : System.Web.UI.MasterPage
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
        string accion, grupo, menu, submenu, boton;

        submenu = string.Empty;
        boton = string.Empty;



        accion = string.Empty;
        grupo = (string)Session["Sgrupo"];

        menu = "CATALOGO";
            submenu = "MATRIZ";
            lMatriz.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);
        
            submenu = "SUCURSAL";
            lSucursal.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);
        
            submenu = "PRODUCTO";
            lProducto.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);
        
            submenu = "COLABORADOR";
            lColaborador.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);

            submenu = "COLABORADOR2";
            lColaborador2.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);

            submenu = "COLABORADOR3";
            lColaborador3.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);
        
            submenu = "VENDEDOR";
            lVendedor.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);
        
            submenu = "PLANDECUENTAS";
            lCuentas.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);

            
            submenu = "ROLESUSU";
            lRolesUsu.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);

            submenu = "USUARIO";
            lUsuario.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);

            submenu = "ACTIVARUSU";
            lActivarUsu.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);

            submenu = "MENUDIN";
            lMenuDin.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);
			
			submenu = "PERSONA";
            lMPersona.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);
			


    }
    #endregion  
}
