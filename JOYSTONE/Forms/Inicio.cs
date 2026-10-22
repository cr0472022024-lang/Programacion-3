using JoyeriaApp.Vistas;
using JOYSTONE.Forms;
using System;
using System.Windows.Forms;

namespace JOYSTONE.Forms
{
    public partial class Inicio : Form
    {
        public Inicio()
        {
            InitializeComponent();
        }

        private void Inicio_Load(object sender, EventArgs e)
        {
            lblUsuarioLogueado.Text = $"Usuario: {FrmLogin.NombreCompletoLogueado}";
            lblRolLogueado.Text = $"Rol: {FrmLogin.NombreRolLogueado}";
        }

        private void categoriasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCategorias frm = new FrmCategorias();
            frm.ShowDialog();
        }

        private void materialesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmMateriales frm = new FrmMateriales();
            frm.ShowDialog();
        }

        
        private void salirDelSistemaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void mantenimientoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show("¿Desea cerrar la sesión actual?", "Cerrar Sesión", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (respuesta == DialogResult.Yes)
            {
                this.Close(); 
            }
        }

        private void tallerToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void categoríasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCategorias frm = new FrmCategorias();
            frm.ShowDialog();
        }

        private void materialesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            FrmMateriales frm = new FrmMateriales();
            frm.ShowDialog();
        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void productosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmProductos frm = new FrmProductos();
            frm.ShowDialog();
        }
    }
}