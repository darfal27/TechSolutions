// CAPA PRESENTACIÓN: pide el nombre, llama a Negocio y muestra el reporte. No tiene SQL.
// HILOS: los datos se piden en un Thread aparte (Utilidades\Tarea) y el reporte se dibuja al volver.
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;      // ReportViewer, ReportDataSource, ReportParameter (paquete NuGet)
using Entidades;
using Negocio;
using Utilidades;

namespace Presentacion
{
    public partial class FrmReporteClientes : Form
    {
        private readonly ClienteBLL bll = new ClienteBLL();

        public FrmReporteClientes()
        {
            InitializeComponent();
        }

        private void btnVer_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text;            // se lee aquí: un hilo no puede tocar los controles
            btnVer.Enabled = false;

            Tarea.Ejecutar<List<Cliente>>(
                delegate { return bll.Reporte(nombre); },                      // 1) DATOS en otro hilo: Negocio → Datos → procedimiento almacenado
                delegate (List<Cliente> datos) { Mostrar(datos, nombre); },    // 2) de vuelta en la pantalla: se dibuja el reporte
                delegate (Exception ex) { Fallo(ex); },
                delegate (Action a) { BeginInvoke(a); });
        }

        private void Mostrar(List<Cliente> datos, string nombre)
        {
            try
            {
                // DISEÑO: el reporte que está dentro del .exe
                reportViewer1.LocalReport.ReportEmbeddedResource = "Presentacion.ReporteClientes.rdlc";

                // Entregar DATOS y PARÁMETRO (mismos nombres que en el .rdlc)
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", datos));
                string filtro = nombre.Trim() == "" ? "Todos" : nombre.Trim();
                reportViewer1.LocalReport.SetParameters(new ReportParameter("Filtro", filtro));

                // MOSTRAR (desde el visor se imprime o se exporta a PDF)
                reportViewer1.RefreshReport();
            }
            catch (Exception ex) { MessageBox.Show("No se pudo mostrar el reporte: " + ex.Message, "Error"); }
            btnVer.Enabled = true;
        }

        private void Fallo(Exception ex)
        {
            btnVer.Enabled = true;
            if (ex is ArgumentException) MessageBox.Show(ex.Message, "Validación");                      // regla de Negocio
            else if (ex is UnauthorizedAccessException) MessageBox.Show(ex.Message, "Sin permiso");     // el rol no tiene permiso
            else MessageBox.Show("No se pudo mostrar el reporte: " + ex.Message, "Error");             // por ejemplo, sin conexión
        }
    }
}
