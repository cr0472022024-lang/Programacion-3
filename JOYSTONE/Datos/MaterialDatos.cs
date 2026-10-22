using System;
using System.Data;
using System.Data.SqlClient;

namespace JOYSTONE.Datos
{
    public class MaterialDatos : Conexion
    {
        public DataTable ListarMateriales()
        {
            DataTable tabla = new DataTable();
            using (var conexion = ObtenerConexion())
            {
                conexion.Open();
                using (var comando = new SqlCommand("sp_ListarMateriales", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    SqlDataReader leer = comando.ExecuteReader();
                    tabla.Load(leer);
                }
            }
            return tabla;
        }

        public bool InsertarMaterial(string nombre, decimal precioPorGramo)
        {
            using (var conexion = ObtenerConexion())
            {
                conexion.Open();
                using (var comando = new SqlCommand("sp_InsertarMaterial", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Nombre", nombre);
                    comando.Parameters.AddWithValue("@Precio", precioPorGramo);

                    int filasAfectadas = comando.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
        }

        public bool EliminarMaterial(int idMaterial)
        {
            using (var conexion = ObtenerConexion())
            {
                conexion.Open();
                using (var comando = new SqlCommand("sp_EliminarMaterial", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@IdMaterial", idMaterial);

                    int filasAfectadas = comando.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
        }
    }
}
