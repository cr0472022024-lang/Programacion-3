using JOYSTONE;
using JOYSTONE.Datos;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace JOYSTONE.Forms
{
    public partial class FrmLogin : Form
    {
        private UsuarioDatos _usuarioDatos = new UsuarioDatos();

        public static int IdUsuarioLogueado;
        public static string NombreUsuarioLogueado;
        public static string NombreCompletoLogueado; 
        public static int IdRolLogueado;
        public static string NombreRolLogueado;    

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
                                IdUsuarioLogueado = Convert.ToInt32(dr["IdUsuario"]);
                                NombreUsuarioLogueado = dr["NombreCompleto"].ToString();
                                IdRolLogueado = Convert.ToInt32(dr["IdRol"]);

                                MessageBox.Show($"¡Bienvenido {NombreUsuarioLogueado}!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                Inicio menu = new Inicio();
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


        private void FrmLogin_Load(object sender, EventArgs e)
        {

        }

        private void btnSalir_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnIngresar_Click_1(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string clave = txtContrasena.Text.Trim();

            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(clave))
            {
                MessageBox.Show("Por favor ingrese usuario y contraseña.", "Campos Requeridos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DataTable resultado = _usuarioDatos.ValidarLogin(usuario, clave);

                if (resultado != null && resultado.Rows.Count > 0)
                {
                    DataRow fila = resultado.Rows[0];

                    IdUsuarioLogueado = Convert.ToInt32(fila["IdUsuario"]);
                    NombreUsuarioLogueado = fila["NombreUsuario"].ToString();
                    NombreCompletoLogueado = fila["NombreCompleto"].ToString();
                    IdRolLogueado = Convert.ToInt32(fila["IdRol"]);
                    NombreRolLogueado = fila["NombreRol"].ToString();

                    MessageBox.Show($"¡Bienvenido al sistema JOYSTONE, {NombreCompletoLogueado}!", "Acceso Concedido", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Inicio menu = new Inicio();
                    this.Hide();
                    menu.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos.", "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtContrasena.Clear();
                    txtContrasena.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al intentar conectar: {ex.Message}", "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}