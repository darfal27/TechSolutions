// CAPA PRESENTACIÓN – nueva venta: arma las líneas en pantalla y las envía a Negocio.VentaBLL (transacción)
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Entidades;
using Negocio;
using Utilidades;

namespace Presentacion
{
    public partial class FrmVenta : Form
    {
        private readonly ClienteBLL clientes = new ClienteBLL();
        private readonly ProductoBLL productos = new ProductoBLL();
        private readonly VentaBLL ventas = new VentaBLL();
        private readonly BindingList<DetalleVenta> lineas = new BindingList<DetalleVenta>();
        private Venta ultimaVenta;          // la última venta registrada (para imprimir su comprobante)
        private string ultimoCliente;

        public FrmVenta() { InitializeComponent(); }

        private void FrmVenta_Load(object sender, EventArgs e)
        {
            try
            {
                cboCliente.DataSource = clientes.Listar();
                dgvLineas.DataSource = lineas;
                dgvLineas.Columns["IdProducto"].Visible = false;
                CargarProductos();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error"); }
        }

        private void CargarProductos()
        {
            cboProducto.DataSource = productos.Listar();   // trae el stock actualizado
            MostrarStock();
        }

        private void cboProducto_SelectedIndexChanged(object sender, EventArgs e) { MostrarStock(); }

        private void MostrarStock()
        {
            var p = cboProducto.SelectedItem as Producto;
            lblStockInfo.Text = p == null ? "" : "Precio: S/ " + p.Precio.ToString("N2") + "   Stock: " + p.Stock;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            var p = cboProducto.SelectedItem as Producto;
            if (p == null) return;
            lineas.Add(new DetalleVenta
            {
                IdProducto = p.Id,
                Producto = p.Nombre,
                Cantidad = (int)nudCantidad.Value,
                PrecioUnitario = p.Precio       // referencial; el precio definitivo lo trae la BD dentro de la transacción
            });
            ActualizarTotal();
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            if (dgvLineas.CurrentRow == null) return;
            lineas.RemoveAt(dgvLineas.CurrentRow.Index);
            ActualizarTotal();
        }

        private void ActualizarTotal()
        {
            lblTotal.Text = "Total: S/ " + lineas.Sum(l => l.Subtotal).ToString("N2");
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            var cliente = cboCliente.SelectedItem as Cliente;
            try
            {
                // chkProcedimiento: false = transacción en .NET (SqlTransaction) · true = transacción en SQL Server (procedimiento)
                Venta v = ventas.Registrar(cliente == null ? 0 : cliente.Id, lineas, chkProcedimiento.Checked);
                ultimaVenta = v;
                ultimoCliente = cliente.Nombre;
                btnVistaPrevia.Enabled = btnImprimir.Enabled = true;
                MessageBox.Show("Venta N.º " + v.Id + " registrada (transacción confirmada en " +
                    (chkProcedimiento.Checked ? "SQL Server" : ".NET") + ").\nTotal: S/ " + v.Total.ToString("N2"), "Tech Solutions");
                lineas.Clear();
                ActualizarTotal();
                CargarProductos();
            }
            catch (StockInsuficienteException ex)
            {
                MessageBox.Show(ex.Message + "\nSe hizo ROLLBACK: no cambió ni el stock ni las ventas.", "Stock insuficiente");
                CargarProductos();
            }
            catch (ArgumentException ex) { MessageBox.Show(ex.Message, "Validación"); }
            catch (UnauthorizedAccessException ex) { MessageBox.Show(ex.Message, "Sin permiso"); }
            catch (Exception ex) { MessageBox.Show("La venta no se registró (ROLLBACK): " + ex.Message, "Error"); }
        }

        // ===== IMPRESIÓN DEL COMPROBANTE (Utilidades\ServicioImpresion) =====
        private void btnVistaPrevia_Click(object sender, EventArgs e) { ImprimirComprobante(false); }
        private void btnImprimir_Click(object sender, EventArgs e) { ImprimirComprobante(true); }

        private void ImprimirComprobante(bool directo)
        {
            if (ultimaVenta == null) return;
            try
            {
                var texto = Comprobante.Armar(ultimaVenta, ultimoCliente);
                string titulo = "Comprobante de venta " + ultimaVenta.Id.ToString("D6");
                if (directo) ServicioImpresion.Imprimir(titulo, texto);        // elegir impresora e imprimir
                else ServicioImpresion.VistaPrevia(titulo, texto);             // ver cómo quedará
            }
            catch (InvalidOperationException ex) { MessageBox.Show(ex.Message, "Impresión"); }   // servicio detenido o sin impresoras
            catch (Exception ex) { MessageBox.Show("No se pudo imprimir: " + ex.Message, "Impresión"); }
        }
    }
}
