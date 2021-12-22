using System;
using System.Data;
using System.Linq;


public partial class inicio : System.Web.UI.Page
{
    #region CONEXION BASE DE DATOS
    string conn1 = System.Configuration.ConfigurationManager.ConnectionStrings["bddComprobantesConnectionString"].ConnectionString;

    Data_bddComprobantesDataContext dc = new Data_bddComprobantesDataContext();

    #endregion ENDCONEXION

    #region LOAD
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }
    #endregion ENDLOAD

    #region BOTON INICIARSESION
    protected void btnIniciarSesion_Click(object sender, EventArgs e)
    {
        bool firstTime;

        //se declara la variable usuario de tipo string y se le indica que reemplaze los carácteres que sean:
        // ; y -- para evitar sql inyection lo mismo para contraseña.

        string usuario = this.txtUsuario.Text.Replace(";", "").Replace("--", "");
        string contraseña = this.txtContraseña.Text.Replace(";", "").Replace("--", "");


        //Se manda llamar al método Autenticar que está parametrizado y se le pasan los valores correspondientes.

        if (LoginService.Autenticar2(usuario, contraseña))
        {


            inicializarSesion(usuario, contraseña);

            firstTime = Convert.ToBoolean(Session["SfirstTime"]);

            //Manda a la principal en caso de ser correcto el login
            if (firstTime)
            {
                Response.Redirect("~/primeraVez.aspx");
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
        }
        else
        {
            //Mensaje de error en caso de no ser usuario registrado
            lblMensaje.Text = "Usuario/Contraseña incorrecta";
        }
    }

    protected void inicializarSesion(string usuario, string contraseña)
    {
        /*  string Accion, sucursal, grupo, ruc, apellidos, nombres, Username, Email, FechaRegistro, maquina, confirma;
          //DateTime FechaRegistro = DateTime.Today;

          int UsuarioID, nivel;
          bool firstTime, estado;
          int Tipo;*/



        /*INICIALIZAR VARIABLES DEL USUARIO*/

        DateTime fecha = Convert.ToDateTime("09/09/2017");
        string contrasena = Helper.EncodePassword(string.Concat(usuario, contraseña));

        var cTraer = dc.sp_abmUsuario("TRAER", 0, 0, "", "", "", "", "", usuario, contrasena, "", fecha, 0, false, "", false);

        if (cTraer.Count() <= 0)
        {

        }
        else
        {
            var cUsuario = from tUsuario in dc.Usuario
                           where tUsuario.Username == usuario
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
                               estado = tUsuario.estado,
                               foto = tUsuario.foto
                           };

            if (cUsuario.Count() == 0)
            {

            }
            else
            {
                foreach (var registro in cUsuario)
                {
                    Session["SUsuarioID"] = registro.UsuarioID;
                    Session["Snivel"] = registro.nivel;
                    Session["Ssucursal"] = registro.sucursal;
                    Session["Sgrupo"] = registro.grupo;
                    Session["Sruc"] = registro.ruc;
                    Session["Sapellidos"] = registro.apellidos;
                    Session["Snombres"] = registro.nombres;
                    Session["SUsername"] = registro.Username;
                    Session["SContraseña"] = registro.Contraseña;
                    Session["SEmail"] = registro.Email;
                    Session["SFechaRegistro"] = registro.FechaRegistro;
                    Session["STipo"] = registro.Tipo;
                    Session["SfirstTime"] = registro.firstTime;
                    Session["Smaquina"] = registro.maquina;
                    Session["Sestado"] = registro.estado;
                    Session["Sfoto"] = registro.foto;
                }
            }

        };


    }
    #endregion END INICIARSESION

}