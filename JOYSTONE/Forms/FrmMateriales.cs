using System;
using System.Windows.Forms;
using JOYSTONE.Datos;

namespace JOYSTONE.Forms
{
    public partial class FrmMateriales : Form
    {
        private MaterialDatos _materialDatos = new MaterialDatos();

        public FrmMateriales()
        {
            InitializeComponent();
        }

        private void FrmMateriales_Load(object sender, EventArgs e)
        {
            CargarTabla();
        }

        private void CargarTabla()
        {
            try
            {
                dgvMateriales.DataSource = _materialDatos.ListarMateriales();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar materiales: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
   

        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtPrecioGramo.Clear();
        }


        private void btnGuardar_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtPrecioGramo.Text))
            {
                MessageBox.Show("Por favor complete todos los campos obligatorios.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtPrecioGramo.Text.Trim(), out decimal precio))
            {
                MessageBox.Show("Ingrese un precio válido en formato numérico (ej. 45.50).", "Formato Incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string nombre = txtNombre.Text.Trim();
                bool exito = _materialDatos.InsertarMaterial(nombre, precio);

                if (exito)
                {
                    MessageBox.Show("Material registrado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();
                    CargarTabla();
                }
                else
                {
                    MessageBox.Show("No se pudo registrar el material.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click_1(object sender, EventArgs e)
        {
            if (dgvMateriales.CurrentRow != null)
            {
                int idMaterial = Convert.ToInt32(dgvMateriales.CurrentRow.Cells["IdMaterial"].Value);
                string nombre = dgvMateriales.CurrentRow.Cells["NombreMaterial"].Value.ToString();

                DialogResult respuesta = MessageBox.Show($"¿Está seguro de eliminar el material '{nombre}'?",
                    "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    if (_materialDatos.EliminarMaterial(idMaterial))
                    {
                        MessageBox.Show("Material eliminado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarTabla();
                        LimpiarCampos();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el material.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione una fila para eliminar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnVolver_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}