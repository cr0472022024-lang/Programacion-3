using System.Configuration;
using System.Data.SqlClient;

namespace JOYSTONE.Datos
{
    public abstract class Conexion
    {
        public static SqlConnection ObtenerConexion()
        {
            string stringConnection = ConfigurationManager.ConnectionStrings["ConexionJoyeria"]?.ConnectionString;
            return new SqlConnection(stringConnection);
        }
    }
}
