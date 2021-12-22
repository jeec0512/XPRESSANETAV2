using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Contabilidad_mpContabilidad : System.Web.UI.MasterPage
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

        menu = "CONTABILIDAD";

        submenu = "AUTRETENCION";
        lAutRetencion.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);

        submenu = "ANULARETENCION";
        lAnularretencion.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);

        submenu = "EGRESOPROVEEDOR";
        lEgresoproveedor.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);

        submenu = "REGISTROEGRESOS";
        lRegEgresos.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);

        submenu = "REGISTROINGRESOS";
        lRegIngresos.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);

        submenu = "REGISTROCHEQUES";
        lRegCheques.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);

        submenu = "DIFERENCIADIARIO";
        lDifDiarios.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);

        submenu = "DETALLECUENTAS";
        lDetCuentas.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);

        submenu = "MAYORES";
        lMayores.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);

        submenu = "ABREPERIODOS";
        lAbrePeriodos.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);

        submenu = "MAYORIZAR";
        lMayorizar.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);

        submenu = "REGREPCONTABLES";
        lRegRepContables.Visible = LoginService.calificarOpcion(accion, grupo, menu, submenu, boton);
    }
    #endregion  
}
