using System;
using System.Data;
using System.Windows.Forms;
using JOYSTONE.Datos;

namespace JOYSTONE.Forms
{
    public partial class FrmProductos : Form
    {
        private ProductoDatos _productoDatos = new ProductoDatos();
        private CategoriaDatos _categoriaDatos = new CategoriaDatos();
        private MaterialDatos _materialDatos = new MaterialDatos();

        public FrmProductos()
        {
            InitializeComponent();
        }

        private void FrmProductos_Load(object sender, EventArgs e)
        {
            CargarDesplegables();
            CargarTabla();
        }

        private void CargarDesplegables()
        {
            try
            {
                DataTable dtCategorias = _categoriaDatos.ListarCategorias();
                cmbCategoria.DataSource = dtCategorias;
                cmbCategoria.DisplayMember = "NombreCategoria";
                cmbCategoria.ValueMember = "IdCategoria";

                DataTable dtMateriales = _materialDatos.ListarMateriales();
                cmbMaterial.DataSource = dtMateriales;
                cmbMaterial.DisplayMember = "NombreMaterial";
                cmbMaterial.ValueMember = "IdMaterial";

                if (cmbCategoria.Items.Count > 0) cmbCategoria.SelectedIndex = 0;
                if (cmbMaterial.Items.Count > 0) cmbMaterial.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar listas desplegables: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CargarTabla()
        {
            try
            {
                dgvProductos.DataSource = _productoDatos.ListarProductos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al listar productos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSKU.Text) || string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtPeso.Text) || string.IsNullOrWhiteSpace(txtPrecio.Text))
            {
                MessageBox.Show("Complete todos los campos obligatorios.", "Campos Requeridos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtPeso.Text.Trim(), out decimal peso) || !decimal.TryParse(txtPrecio.Text.Trim(), out decimal precio))
            {
                MessageBox.Show("El peso y el precio deben ser valores numéricos válidos.", "Formato Incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string sku = txtSKU.Text.Trim();
                string nombre = txtNombre.Text.Trim();
                int idCategoria = Convert.ToInt32(cmbCategoria.SelectedValue);
                int idMaterial = Convert.ToInt32(cmbMaterial.SelectedValue);

                if (_productoDatos.InsertarProducto(sku, nombre, idCategoria, idMaterial, peso, precio))
                {
                    MessageBox.Show("Producto registrado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();
                    CargarTabla();
                }
                else
                {
                    MessageBox.Show("No se pudo registrar el producto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.CurrentRow != null)
            {
                int idProducto = Convert.ToInt32(dgvProductos.CurrentRow.Cells["IdProducto"].Value);
                string nombre = dgvProductos.CurrentRow.Cells["Nombre"].Value.ToString();

                DialogResult respuesta = MessageBox.Show($"¿Desea eliminar el producto '{nombre}'?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    if (_productoDatos.EliminarProducto(idProducto))
                    {
                        MessageBox.Show("Producto eliminado.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarTabla();
                        LimpiarCampos();
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione una fila para eliminar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LimpiarCampos()
        {
            txtSKU.Clear();
            txtNombre.Clear();
            txtPeso.Clear();
            txtPrecio.Clear();
            if (cmbCategoria.Items.Count > 0) cmbCategoria.SelectedIndex = 0;
            if (cmbMaterial.Items.Count > 0) cmbMaterial.SelectedIndex = 0;
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSKU.Text) || string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtPeso.Text) || string.IsNullOrWhiteSpace(txtPrecio.Text))
            {
                MessageBox.Show("Complete todos los campos obligatorios.", "Campos Requeridos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbCategoria.SelectedValue == null || cmbMaterial.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar una Categoría y un Material válidos. Registre un material primero si la lista está vacía.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtPeso.Text.Trim(), out decimal peso) || !decimal.TryParse(txtPrecio.Text.Trim(), out decimal precio))
            {
                MessageBox.Show("El peso y el precio deben ser valores numéricos válidos.", "Formato Incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string sku = txtSKU.Text.Trim();
                string nombre = txtNombre.Text.Trim();
                int idCategoria = Convert.ToInt32(cmbCategoria.SelectedValue);
                int idMaterial = Convert.ToInt32(cmbMaterial.SelectedValue);

                if (_productoDatos.InsertarProducto(sku, nombre, idCategoria, idMaterial, peso, precio))
                {
                    MessageBox.Show("Producto registrado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();
                    CargarTabla();
                }
                else
                {
                    MessageBox.Show("No se pudo registrar el producto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnEliminar_Click_1(object sender, EventArgs e)
        {
            if (dgvProductos.CurrentRow != null)
            {
                int idProducto = Convert.ToInt32(dgvProductos.CurrentRow.Cells["IdProducto"].Value);
                string nombre = dgvProductos.CurrentRow.Cells["Nombre"].Value.ToString();

                DialogResult respuesta = MessageBox.Show($"¿Desea eliminar el producto '{nombre}'?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    if (_productoDatos.EliminarProducto(idProducto))
                    {
                        MessageBox.Show("Producto eliminado.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarTabla();
                        LimpiarCampos();
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