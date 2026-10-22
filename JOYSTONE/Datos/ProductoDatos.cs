using System;
using System.Data;
using System.Data.SqlClient;

namespace JOYSTONE.Datos
{
    public class ProductoDatos : Conexion
    {
        public DataTable ListarProductos()
        {
            DataTable tabla = new DataTable();
            using (var conexion = ObtenerConexion())
            {
                conexion.Open();
                using (var comando = new SqlCommand("sp_ListarProductos", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    SqlDataReader leer = comando.ExecuteReader();
                    tabla.Load(leer);
                }
            }
            return tabla;
        }

        public bool InsertarProducto(string sku, string nombre, int idCategoria, int idMaterial, decimal peso, decimal precio)
        {
            using (var conexion = ObtenerConexion())
            {
                conexion.Open();
                using (var comando = new SqlCommand("sp_InsertarProducto", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@CodigoSKU", sku);
                    comando.Parameters.AddWithValue("@Nombre", nombre);
                    comando.Parameters.AddWithValue("@IdCategoria", idCategoria);
                    comando.Parameters.AddWithValue("@IdMaterial", idMaterial);
                    comando.Parameters.AddWithValue("@PesoGramos", peso);
                    comando.Parameters.AddWithValue("@PrecioVenta", precio);

                    comando.ExecuteNonQuery();
                    return true;
                }
            }
        }
        public bool EliminarProducto(int idProducto)
        {
            using (var conexion = ObtenerConexion())
            {
                conexion.Open();
                using (var comando = new SqlCommand("sp_EliminarProducto", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@IdProducto", idProducto);

                    int filasAfectadas = comando.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
        }
    }
}