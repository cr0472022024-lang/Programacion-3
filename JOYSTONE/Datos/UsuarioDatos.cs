using System;
using System.Data;
using System.Data.SqlClient;

namespace JOYSTONE.Datos
{
    public class UsuarioDatos : Conexion
    {
        public DataTable ValidarLogin(string usuario, string contrasena)
        {
            DataTable tabla = new DataTable();
            using (var conexion = ObtenerConexion())
            {
                conexion.Open();
                using (var comando = new SqlCommand("sp_LoginUsuario", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Usuario", usuario);
                    comando.Parameters.AddWithValue("@Contrasena", contrasena);

                    SqlDataReader leer = comando.ExecuteReader();
                    tabla.Load(leer);
                }
            }
            return tabla;
        }
    }
}