using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SistemaJoyeria
{
    public partial class FrmLogin : Form
    {
        // Variables globales para la sesión
        public static int IdUsuarioLogueado;
        public static string NombreUsuarioLogueado;
        public static int IdRolLogueado;

        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string clave = txtContrasena.Text.Trim();

            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(clave))
            {
                MessageBox.Show("Por favor, ingrese usuario y contraseña.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection cn = Conexion.ObtenerConexion())
                {
                    string query = @"SELECT IdUsuario, NombreCompleto, IdRol 
                                     FROM Usuarios 
                                     WHERE NombreUsuario = @Usuario AND Contrasena = @Clave AND Estado = 1";

                    using (SqlCommand cmd = new SqlCommand(query, cn))
                    {
                        cmd.Parameters.AddWithValue("@Usuario", usuario);
                        cmd.Parameters.AddWithValue("@Clave", clave);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                // Guardar datos de sesión
                                IdUsuarioLogueado = Convert.ToInt32(dr["IdUsuario"]);
                                NombreUsuarioLogueado = dr["NombreCompleto"].ToString();
                                IdRolLogueado = Convert.ToInt32(dr["IdRol"]);

                                MessageBox.Show($"¡Bienvenido {NombreUsuarioLogueado}!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // Abrir Menú Principal
                                FrmMenuPrincipal menu = new FrmMenuPrincipal();
                                this.Hide();
                                menu.ShowDialog();
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Usuario o contraseña incorrectos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar con la base de datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}