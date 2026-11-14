using System;
using System.Windows.Forms;
using Entidades;
using Negocio;

namespace Presentacion
{
    public partial class FrmClientes : Form
    {
        private readonly ClienteBLL bll = new ClienteBLL();

        public FrmClientes() { InitializeComponent(); }

        private void FrmClientes_Load(object sender, EventArgs e) { Recargar(); }

        private void Recargar()
        {
            try { dgvClientes.DataSource = bll.Listar(); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error"); }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                bll.Registrar(new Cliente
                {
                    Documento = txtDocumento.Text.Trim(),
                    Nombre = txtNombre.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Telefono = txtTelefono.Text.Trim()
                });
                txtDocumento.Clear(); txtNombre.Clear(); txtEmail.Clear(); txtTelefono.Clear();
                Recargar();
            }
            catch (ArgumentException ex) { MessageBox.Show(ex.Message, "Validación"); }
            catch (UnauthorizedAccessException ex) { MessageBox.Show(ex.Message, "Sin permiso"); }
            catch (Exception ex) { MessageBox.Show("No se pudo registrar: " + ex.Message, "Error"); }
        }
    }
}
