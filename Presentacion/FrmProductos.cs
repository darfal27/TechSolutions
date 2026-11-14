using System;
using System.Windows.Forms;
using Entidades;
using Negocio;

namespace Presentacion
{
    public partial class FrmProductos : Form
    {
        private readonly ProductoBLL bll = new ProductoBLL();

        public FrmProductos() { InitializeComponent(); }

        private void FrmProductos_Load(object sender, EventArgs e) { Recargar(); }

        private void Recargar()
        {
            try { dgvProductos.DataSource = bll.Listar(); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error"); }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                bll.Registrar(new Producto
                {
                    Codigo = txtCodigo.Text.Trim(),
                    Nombre = txtNombre.Text.Trim(),
                    Precio = nudPrecio.Value,
                    Stock = (int)nudStock.Value
                });
                txtCodigo.Clear(); txtNombre.Clear(); nudPrecio.Value = 1; nudStock.Value = 0;
                Recargar();
            }
            catch (ArgumentException ex) { MessageBox.Show(ex.Message, "Validación"); }
            catch (UnauthorizedAccessException ex) { MessageBox.Show(ex.Message, "Sin permiso"); }
            catch (Exception ex) { MessageBox.Show("No se pudo registrar: " + ex.Message, "Error"); }
        }

        // Entrada de mercadería al producto seleccionado
        private void btnIngresarStock_Click(object sender, EventArgs e)
        {
            if (dgvProductos.CurrentRow == null) return;
            try
            {
                var p = (Producto)dgvProductos.CurrentRow.DataBoundItem;
                bll.IngresarStock(p.Id, (int)nudEntrada.Value);
                Recargar();
            }
            catch (ArgumentException ex) { MessageBox.Show(ex.Message, "Validación"); }
            catch (UnauthorizedAccessException ex) { MessageBox.Show(ex.Message, "Sin permiso"); }
            catch (Exception ex) { MessageBox.Show("No se pudo ingresar el stock: " + ex.Message, "Error"); }
        }
    }
}
