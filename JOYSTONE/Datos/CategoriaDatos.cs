using System;
using System.Data;
using System.Data.SqlClient;

namespace JOYSTONE.Datos
{
    public class CategoriaDatos : Conexion
    {
        public DataTable ListarCategorias()
        {
            DataTable tabla = new DataTable();
            using (var conexion = ObtenerConexion())
            {
                conexion.Open();
                using (var comando = new SqlCommand("sp_ListarCategorias", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    SqlDataReader leer = comando.ExecuteReader();
                    tabla.Load(leer);
                }
            }
            return tabla;
        }

        public bool InsertarCategoria(string nombre, string descripcion)
        {
            using (var conexion = ObtenerConexion())
            {
                conexion.Open();
                using (var comando = new SqlCommand("sp_InsertarCategoria", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Nombre", nombre);
                    comando.Parameters.AddWithValue("@Descripcion", string.IsNullOrEmpty(descripcion) ? (object)DBNull.Value : descripcion);

                    int filasAfectadas = comando.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
        }

        public bool EliminarCategoria(int idCategoria)
        {
            using (var conexion = ObtenerConexion())
            {
                conexion.Open();
                using (var comando = new SqlCommand("sp_EliminarCategoria", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@IdCategoria", idCategoria);

                    int filasAfectadas = comando.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
        }
    }
}