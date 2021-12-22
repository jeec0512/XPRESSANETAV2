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
public partial class primeraVez : System.Web.UI.Page
{
    #region CONEXION BASE DE DATOS
    string conn = System.Configuration.ConfigurationManager.ConnectionStrings["bddComprobantesConnectionString"].ConnectionString;

    
   Data_bddComprobantesDataContext dc = new Data_bddComprobantesDataContext();

    #endregion

    #region INICIAR
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblmensaje.Text = string.Empty;
            txtUserName.Text = Convert.ToString(Session["SUsername"]);
             txtCedula.Text = Convert.ToString(Session["Sruc"]);
            txtUserName.Enabled = false;
            
            ibConsultar_Click();

            txtContrasena.Focus();
        }
    }

    #endregion

    #region PROCESOS DE MANTENIMIENTO DEL USUARIO
    /*CONSULTAR USUARIO*/
    protected void ibConsultar_Click(object sender, ImageClickEventArgs e)
    {
        ibConsultar_Click();
    }

    /*GUARDAR INFORMACION*/
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        string Accion, sucursal, grupo, ruc, apellidos, nombres, Username, Email, FechaRegistro, maquina, contrasena, confirma;
        //DateTime FechaRegistro = DateTime.Today;

        int UsuarioID, nivel;
        bool firstTime, estado;
        int Tipo;


        /*CONSTANTES*/
        Accion = "GUARDAR";
        UsuarioID = 0;
        FechaRegistro = "09/09/2017";
        firstTime = false;


        ruc = txtCedula.Text.Trim();
        apellidos = txtApellidos.Text;
        nombres = txtNombres.Text;
        Username = txtUserName.Text;
        Email = txtEmail.Text;
        maquina = txtMaquina.Text;
        nivel = Convert.ToInt16(ddlNivel.SelectedValue);
        sucursal = ddlSucursal.SelectedValue.Trim();
        grupo = ddlGrupo.SelectedValue.Trim();
        Tipo = Convert.ToInt16(ddlTipo.SelectedValue);
        estado = true;
        contrasena = txtContrasena.Text.Trim();
        confirma = txtConfirma.Text.Trim();

        if (ruc.Length < 10
           || apellidos.Length <= 2
           || nombres.Length <= 2
           || Username.Length <= 2
             || Email.Length <= 2
             || maquina.Length <= 2
             || contrasena.Length < 8
             || contrasena.Length > 16
            || !buscaColaborador(ruc))
        {
            lblmensaje.Text = "Ingrese toda la información,identificación válido,nombres, apellidos,usuario, contraseña valida y deben estar entre el rango de 8 a 16 caracteres  ";
        }
        else
        {
            if (sucursal == "-1" || grupo == "-1")
            {
                lblmensaje.Text = "Ingrese sucursal y grupo";
            }
            else
            {
                if (contrasena != confirma)
                {
                    lblmensaje.Text = "La contraseña y la confirmación no coinciden y deben estar entre el rango de 8 a 16 caracteres";
                }
                else
                {
                    /*GUARDAR INFORMACION*/
                    string hash = Helper.EncodePassword(string.Concat(Username, contrasena));

                    dc.sp_abmUsuario(Accion, UsuarioID, nivel, sucursal, grupo, ruc, apellidos, nombres, Username, hash, Email, Convert.ToDateTime(FechaRegistro), Tipo, firstTime, maquina, estado);

                    blanquearObjetos();

                    lblmensaje.Text = apellidos.Trim() + " " + nombres.Trim() + " guardado correctamente";
                    Response.Redirect("~/ingresar.aspx");

                }
            }
        }
    }
    protected void txtCedula_TextChanged(object sender, EventArgs e)
    {
        ibConsultar_Click();
    }

    #endregion

    #region PROCESOS INTERNOS
    /*PROCESOS INTERNOS*/
    protected void llenarListados()
    {
        /* TRAER CUENTAS CONTABLES*/
        var cSuc = from msuc in dc.tbl_ruc
                   where msuc.activo == true
                   orderby msuc.sucursal
                   select new
                   {
                       sucursal = msuc.sucursal.Trim()
                    ,
                       nom_suc = msuc.sucursal.Trim() + " " + msuc.nom_suc.Trim()
                   };


        ddlSucursal.DataSource = cSuc;
        ddlSucursal.DataBind();

        ListItem listSuc = new ListItem("Seleccione sucursal", "-1");
        ddlSucursal.Items.Insert(0, listSuc);

        /* TRAER GRUPO*/
        var cGrupo = from mgrupo in dc.tbl_UsuarioGrupo
                     select new
                     {
                         codigo = mgrupo.codigo.Trim()
                      ,
                         descripcion = mgrupo.codigo.Trim() + " " + mgrupo.descripcion.Substring(1,50)
                     };


        ddlGrupo.DataSource = cGrupo;
        ddlGrupo.DataBind();
        ListItem listGrupo = new ListItem("Seleccione el grupo", "-1");
        ddlGrupo.Items.Insert(0, listGrupo);
    }

    /*CONSULTAR USUARIO*/
    protected void ibConsultar_Click()
    {
        string lcedula = txtCedula.Text.Trim();

        if (lcedula.Length > 0)
        {
            llenarListados();
            blanquearObjetos();

            if (!buscaColaborador(lcedula))
            {
                lblmensaje.Text = "Usuario debe pertenecer a la Empresa y debes estar activo";
            }
            else
            {
                buscaUsuario(lcedula);
                txtApellidos.Focus();
            }

        }
        else
        {
            lblmensaje.Text = "Ingrese un número de identificación válido";
            txtCedula.Focus();
        }
    }


    /*BUSCAR EN COLABORADOR*/
    protected bool buscaColaborador(string lcedula)
    {
        bool pasa = false;

        var cColabora = from tColabora in dc.tbl_colaborador
                        where tColabora.Cedula == lcedula
                            && tColabora.activo == true
                        select new
                        {
                            Cedula = tColabora.Cedula
                             ,
                            apellidos = tColabora.apellidos
                             ,
                            nombres = tColabora.nombres
                             ,
                            sucursal = tColabora.sucursal
                             ,
                            ccosto = tColabora.ccosto
                             ,
                            activo = tColabora.activo
                        };

        if (cColabora.Count() == 0)
        {
            pasa = false;
        }
        else
        {


            foreach (var registro in cColabora)
            {
                lblmensaje.Text = string.Empty;
                /*txtCedula.Text = registro.Cedula;
                txtApellidos.Text = registro.apellidos;
                txtNombres.Text = registro.nombres;
                ddlSucursal.SelectedValue = registro.sucursal;*/
                //ddlCcosto.SelectedValue = registro.ccosto;
                //chkActivo.Checked = Convert.ToBoolean(registro.activo);
            }
            pasa = true;
        }
        return pasa;
    }

    /*BUSCAR EN USUARIO*/
    protected bool buscaUsuario(string lcedula)
    {
        bool pasa = false;
        string usuario = txtUserName.Text.Trim();

        var cUsuario = from tUsuario in dc.Usuario
                       where tUsuario.ruc == lcedula
                            && tUsuario.Username == usuario
                       select new
                       {
                           UsuarioID = tUsuario.UsuarioID,
                           nivel = tUsuario.nivel,
                           sucursal = tUsuario.sucursal,
                           grupo = tUsuario.grupo,
                           ruc = tUsuario.ruc,
                           apellidos = tUsuario.apellidos,
                           nombres = tUsuario.nombres,
                           Username = tUsuario.Username,
                           Contraseña = tUsuario.Contraseña,
                           Email = tUsuario.Email,
                           FechaRegistro = tUsuario.FechaRegistro,
                           Tipo = tUsuario.Tipo,
                           firstTime = tUsuario.firstTime,
                           maquina = tUsuario.maquina,
                           estado = tUsuario.estado
                       };

        if (cUsuario.Count() == 0)
        {
            pasa = false;
        }
        else
        {
            foreach (var registro in cUsuario)
            {
                lblmensaje.Text = string.Empty;

                txtCedula.Text = registro.ruc;
                txtApellidos.Text = registro.apellidos;
                txtNombres.Text = registro.nombres;
                txtUserName.Text = registro.Username;
                txtEmail.Text = registro.Email.Trim();
                txtMaquina.Text = registro.maquina.Trim();
                ddlNivel.SelectedValue = Convert.ToString(registro.nivel);
                ddlSucursal.SelectedValue = registro.sucursal;
                ddlGrupo.SelectedValue = registro.grupo.Trim();
                ddlTipo.SelectedValue = Convert.ToString(registro.Tipo);

            }
            pasa = true;
        }
        return pasa;
    }

    /*BLANQUEAR OBJETOS*/
    protected void blanquearObjetos()
    {
        //txtCedula.Text = string.Empty;
        txtApellidos.Text = string.Empty;
        txtNombres.Text = string.Empty;
        //txtUserName.Text = string.Empty;
        txtContrasena.Text = string.Empty;
        txtConfirma.Text = string.Empty;
        txtEmail.Text = string.Empty;
        txtMaquina.Text = string.Empty;
        ddlNivel.SelectedValue = "-1";
        ddlSucursal.SelectedValue = "-1";
        ddlGrupo.SelectedValue = "-1";
        ddlTipo.SelectedValue = "-1";

    }

    #endregion
}