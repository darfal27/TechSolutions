// CAPA PRESENTACIÓN – consultas LINQ (las consultas están en Negocio\ConsultaBLL.cs)
// HILOS: la consulta se ejecuta en un Thread aparte (Utilidades\Tarea) para que la ventana no se congele.
// IMPRESIÓN: el resultado se puede ver en vista previa o imprimir (Utilidades\ServicioImpresion).
// El combo solo muestra las consultas que el rol del usuario puede ver.
using System;
using System.Collections;
using System.Windows.Forms;
using Negocio;
using Utilidades;

namespace Presentacion
{
    public partial class FrmConsultas : Form
    {
        private readonly ConsultaBLL bll = new ConsultaBLL();
        private IEnumerable resultadoActual;                    // lo que se está mostrando (para imprimirlo)

        private const string QProductos = "Productos (buscar / stock bajo)";
        private const string QClientes = "Clientes (buscar)";
        private const string QVentasCliente = "Ventas por cliente";
        private const string QTop = "Productos más vendidos";

        public FrmConsultas() { InitializeComponent(); }

        private void FrmConsultas_Load(object sender, EventArgs e)
        {
            if (Permisos.PuedeAlguno(Modulo.Productos, Modulo.Ventas)) cboConsulta.Items.Add(QProductos);
            if (Permisos.PuedeAlguno(Modulo.Clientes, Modulo.Ventas)) cboConsulta.Items.Add(QClientes);
            if (Permisos.PuedeAlguno(Modulo.Ventas)) { cboConsulta.Items.Add(QVentasCliente); cboConsulta.Items.Add(QTop); }
            if (cboConsulta.Items.Count > 0) cboConsulta.SelectedIndex = 0;
        }

        // Cada consulta usa solo algunos filtros: los demás se desactivan.
        private void cboConsulta_SelectedIndexChanged(object sender, EventArgs e)
        {
            string q = cboConsulta.Text;
            txtBuscar.Enabled = q == QProductos || q == QClientes;
            chkStockBajo.Enabled = nudUmbral.Enabled = q == QProductos;
            nudTop.Enabled = q == QTop;
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            // 1) Se leen los controles AQUÍ (hilo de la pantalla): un hilo no puede tocarlos.
            string q = cboConsulta.Text;
            string texto = txtBuscar.Text;
            bool stockBajo = chkStockBajo.Checked;
            int umbral = (int)nudUmbral.Value;
            int top = (int)nudTop.Value;

            btnConsultar.Enabled = false;
            lblFilas.Text = "Consultando...";

            // 2) La consulta corre en otro hilo; 3) el resultado vuelve al hilo de la pantalla (BeginInvoke).
            Tarea.Ejecutar<IEnumerable>(
                delegate { return Ejecutar(q, texto, stockBajo, umbral, top); },
                delegate (IEnumerable resultado) { Mostrar(resultado); },
                delegate (Exception ex) { Fallo(ex); },
                delegate (Action a) { BeginInvoke(a); });
        }

        // Se ejecuta en el HILO NUEVO: no usa ningún control de la pantalla.
        private IEnumerable Ejecutar(string q, string texto, bool stockBajo, int umbral, int top)
        {
            switch (q)
            {
                case QProductos: return bll.BuscarProductos(texto, stockBajo, umbral);
                case QClientes: return bll.BuscarClientes(texto);
                case QVentasCliente: return bll.VentasPorCliente();
                case QTop: return bll.ProductosMasVendidos(top);
                default: return new object[0];
            }
        }

        private void Mostrar(IEnumerable resultado)
        {
            resultadoActual = resultado;
            dgvResultado.DataSource = resultado;
            foreach (DataGridViewColumn c in dgvResultado.Columns)
                if (c.ValueType == typeof(decimal)) c.DefaultCellStyle.Format = "N2";
            lblFilas.Text = "Filas: " + dgvResultado.Rows.Count;
            btnConsultar.Enabled = true;
        }

        private void Fallo(Exception ex)
        {
            btnConsultar.Enabled = true;
            lblFilas.Text = "";
            if (ex is UnauthorizedAccessException) MessageBox.Show(ex.Message, "Sin permiso");
            else MessageBox.Show("Error en la consulta: " + ex.Message, "Error");
        }

        // ===== IMPRESIÓN del resultado =====
        private void btnVistaPrevia_Click(object sender, EventArgs e) { ImprimirResultado(false); }
        private void btnImprimir_Click(object sender, EventArgs e) { ImprimirResultado(true); }

        private void ImprimirResultado(bool directo)
        {
            if (resultadoActual == null) { MessageBox.Show("Primero haga una consulta."); return; }
            try
            {
                var texto = ServicioImpresion.FormatearLista(resultadoActual);
                string titulo = "Tech Solutions - " + cboConsulta.Text;
                if (directo) ServicioImpresion.Imprimir(titulo, texto);
                else ServicioImpresion.VistaPrevia(titulo, texto);
            }
            catch (InvalidOperationException ex) { MessageBox.Show(ex.Message, "Impresión"); }
            catch (Exception ex) { MessageBox.Show("No se pudo imprimir: " + ex.Message, "Impresión"); }
        }
    }
}
