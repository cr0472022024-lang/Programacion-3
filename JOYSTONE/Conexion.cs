using System;
using System.Data;
using System.Data.SqlClient;

namespace SistemaJoyeria
{
    public class Conexion
    {
        // Cambia Server=localhost por la instancia de tu SQL Server (ej. localhost o .\SQLEXPRESS)
        private static string cadenaConexion = "DESKTOP-SEHGQ58\\SQLEXPRESS; Database=JoyeriaDB; Integrated Security=True;";

        public static SqlConnection ObtenerConexion()
        {
            SqlConnection cn = new SqlConnection(cadenaConexion);
            if (cn.State == ConnectionState.Closed)
            {
                cn.Open();
            }
            return cn;
        }
    }
}