using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AuticationBDD
{
    public class Conexion
    {
        string cadena = @"Data Source=192.168.1.159\EXPRESS;Initial Catalog=bddComprobantes;ID=sistemaerp;Password=s1st3m43rp";

        private SqlConnection conexion;

        public Conexion() {
            obtenerConeccion();
        }

        public SqlConnection obtenerConeccion() {
            if (null == conexion) { conexion = new SqlConnection(cadena); }
            return conexion;
        }
    }
}