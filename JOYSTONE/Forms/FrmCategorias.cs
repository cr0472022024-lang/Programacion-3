using System;
using System.Windows.Forms;
using JOYSTONE.Datos; 
namespace JoyeriaApp.Vistas
{
    public partial class FrmCategorias : Form
    {
        private CategoriaDatos _categoriaDatos = new CategoriaDatos();

        public FrmCategorias()
        {
            InitializeComponent();
        }

        private void FrmCategorias_Load(object sender, EventArgs e)
        {
            CargarTabla();
        }

        private void CargarTabla()
        {
            try
            {
                dgvCategorias.DataSource = _categoriaDatos.ListarCategorias();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar categorías: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

      

        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtDescripcion.Clear();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtDescripcion_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre de la categoría es obligatorio.", "Campos Requeridos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string nombre = txtNombre.Text.Trim();
                string descripcion = txtDescripcion.Text.Trim();

                bool exito = _categoriaDatos.InsertarCategoria(nombre, descripcion);

                if (exito)
                {
                    MessageBox.Show("Categoría registrada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();
                    CargarTabla();
                }
                else
                {
                    MessageBox.Show("No se pudo registrar la categoría.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click_1(object sender, EventArgs e)
        {
            if (dgvCategorias.CurrentRow != null)
            {
                int idCategoria = Convert.ToInt32(dgvCategorias.CurrentRow.Cells["IdCategoria"].Value);
                string nombre = dgvCategorias.CurrentRow.Cells["NombreCategoria"].Value.ToString();

                DialogResult respuesta = MessageBox.Show($"¿Desea eliminar la categoría '{nombre}'?",
                    "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    if (_categoriaDatos.EliminarCategoria(idCategoria))
                    {
                        MessageBox.Show("Categoría eliminada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarTabla();
                        LimpiarCampos();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar la categoría.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione una fila de la tabla para eliminar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnVolver_Click_1(object sender, EventArgs e)
        {
            
                this.Close();
            
        }
    }
}