// CAPA PRESENTACIÓN: pide el rango de fechas, llama a Negocio y muestra el reporte de ventas.
// Mismo patrón que FrmReporteClientes; el procedimiento usp_ReporteVentas recibe @Desde y @Hasta.
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using Entidades;
using Negocio;
using Utilidades;

namespace Presentacion
{
    public partial class FrmReporteVentas : Form
    {
        private readonly VentaBLL bll = new VentaBLL();

        public FrmReporteVentas()
        {
            InitializeComponent();
            dtpDesde.Value = DateTime.Today.AddMonths(-1);
            dtpHasta.Value = DateTime.Today;
        }

        private void btnVer_Click(object sender, EventArgs e)
        {
            DateTime desde = dtpDesde.Value.Date;      // se leen aquí (hilo de la pantalla)
            DateTime hasta = dtpHasta.Value.Date;
            btnVer.Enabled = false;

            Tarea.Ejecutar<List<VentaDetalleInfo>>(
                delegate { return bll.Reporte(desde, hasta); },
                delegate (List<VentaDetalleInfo> datos) { Mostrar(datos, desde, hasta); },
                delegate (Exception ex) { Fallo(ex); },
                delegate (Action a) { BeginInvoke(a); });
        }

        private void Mostrar(List<VentaDetalleInfo> datos, DateTime desde, DateTime hasta)
        {
            try
            {
                reportViewer1.LocalReport.ReportEmbeddedResource = "Presentacion.ReporteVentas.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", datos));
                string filtro = "Del " + desde.ToString("dd/MM/yyyy") + " al " + hasta.ToString("dd/MM/yyyy");
                reportViewer1.LocalReport.SetParameters(new ReportParameter("Filtro", filtro));
                reportViewer1.RefreshReport();
            }
            catch (Exception ex) { MessageBox.Show("No se pudo mostrar el reporte: " + ex.Message, "Error"); }
            btnVer.Enabled = true;
        }

        private void Fallo(Exception ex)
        {
            btnVer.Enabled = true;
            if (ex is ArgumentException) MessageBox.Show(ex.Message, "Validación");
            else if (ex is UnauthorizedAccessException) MessageBox.Show(ex.Message, "Sin permiso");
            else MessageBox.Show("No se pudo mostrar el reporte: " + ex.Message, "Error");
        }
    }
}
