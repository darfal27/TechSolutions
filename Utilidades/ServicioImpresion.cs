// Proyecto: Utilidades · Referencias: System.Drawing, System.Windows.Forms, System.ServiceProcess
// GESTIÓN DEL SERVICIO DE IMPRESIÓN en .NET:
//   - comprueba el servicio "Cola de impresión" (Spooler) de Windows y que haya impresoras
//   - PrintDocument arma las páginas (paginación, título, número de página)
//   - PrintPreviewDialog = vista previa · PrintDialog = elegir impresora y copias, luego imprimir
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Windows.Forms;

namespace Utilidades
{
    public static class ServicioImpresion
    {
        // ---------- estado del servicio ----------
        public static bool ServicioActivo()
        {
            try
            {
                using (var sc = new ServiceController("Spooler"))
                    return sc.Status == ServiceControllerStatus.Running;
            }
            catch (Exception) { return false; }
        }

        public static List<string> Impresoras()
        {
            return PrinterSettings.InstalledPrinters.Cast<string>().ToList();
        }

        public static string ImpresoraPredeterminada()
        {
            var ps = new PrinterSettings();
            return ps.IsValid ? ps.PrinterName : "(ninguna)";
        }

        private static void Validar()
        {
            if (!ServicioActivo())
                throw new InvalidOperationException("El servicio de impresión (Cola de impresión) de Windows no está en ejecución.");
            if (Impresoras().Count == 0)
                throw new InvalidOperationException("No hay impresoras instaladas en este equipo.");
        }

        // ---------- vista previa e impresión ----------
        public static void VistaPrevia(string titulo, IList<string> lineas)
        {
            Validar();
            using (PrintDocument doc = Crear(titulo, lineas))
            using (var vista = new PrintPreviewDialog())
            {
                vista.Document = doc;
                vista.Width = 900;
                vista.Height = 700;
                vista.ShowDialog();
            }
        }

        // Devuelve false si el usuario canceló el cuadro de impresión
        public static bool Imprimir(string titulo, IList<string> lineas)
        {
            Validar();
            using (PrintDocument doc = Crear(titulo, lineas))
            using (var dialogo = new PrintDialog())
            {
                dialogo.Document = doc;
                dialogo.UseEXDialog = true;
                if (dialogo.ShowDialog() != DialogResult.OK) return false;
                doc.Print();
                return true;
            }
        }

        // ---------- armado de las páginas ----------
        private static PrintDocument Crear(string titulo, IList<string> lineas)
        {
            var doc = new PrintDocument();
            doc.DocumentName = titulo;
            int indice = 0, pagina = 0;
            Font fuente = null, fuenteTitulo = null;

            doc.BeginPrint += delegate
            {
                indice = 0; pagina = 0;
                fuente = new Font("Consolas", 9f);
                fuenteTitulo = new Font("Arial", 13f, FontStyle.Bold);
            };
            doc.PrintPage += delegate (object s, PrintPageEventArgs e)
            {
                pagina++;
                float x = e.MarginBounds.Left, y = e.MarginBounds.Top;
                e.Graphics.DrawString(titulo, fuenteTitulo, Brushes.Black, x, y);
                y += fuenteTitulo.GetHeight(e.Graphics) + 12;

                float alto = fuente.GetHeight(e.Graphics);
                float limite = e.MarginBounds.Bottom - 2 * alto;           // deja lugar al pie de página
                while (indice < lineas.Count && y + alto <= limite)
                {
                    e.Graphics.DrawString(lineas[indice], fuente, Brushes.Black, x, y);
                    y += alto;
                    indice++;
                }
                e.Graphics.DrawString("Página " + pagina, fuente, Brushes.Gray, x, e.MarginBounds.Bottom - alto);
                e.HasMorePages = indice < lineas.Count;                     // true = hay otra página
            };
            doc.EndPrint += delegate
            {
                if (fuente != null) fuente.Dispose();
                if (fuenteTitulo != null) fuenteTitulo.Dispose();
            };
            return doc;
        }

        // ---------- texto en columnas ----------
        public static List<string> FormatearTabla(string[] encabezados, IList<string[]> filas)
        {
            int n = encabezados.Length;
            var ancho = new int[n];
            for (int i = 0; i < n; i++)
            {
                int max = encabezados[i].Length;
                foreach (string[] f in filas) max = Math.Max(max, (f[i] ?? "").Length);
                ancho[i] = Math.Min(30, max);
            }
            Func<string[], string> unir = f =>
                string.Join("  ", Enumerable.Range(0, n).Select(i => Ajustar(f[i], ancho[i])));

            var r = new List<string>();
            r.Add(unir(encabezados));
            r.Add(new string('-', ancho.Sum() + 2 * (n - 1)));
            foreach (string[] f in filas) r.Add(unir(f));
            return r;
        }

        // Convierte cualquier lista de objetos (sus propiedades públicas) en líneas de texto
        public static List<string> FormatearLista(IEnumerable items)
        {
            var filas = new List<string[]>();
            PropertyInfo[] props = null;
            foreach (object o in items)
            {
                if (props == null) props = o.GetType().GetProperties();
                object actual = o;
                filas.Add(props.Select(p => Texto(p.GetValue(actual, null))).ToArray());
            }
            if (props == null) return new List<string> { "(sin resultados)" };
            return FormatearTabla(props.Select(p => p.Name).ToArray(), filas);
        }

        private static string Texto(object v)
        {
            if (v == null) return "";
            if (v is decimal) return ((decimal)v).ToString("N2");
            if (v is DateTime) return ((DateTime)v).ToString("dd/MM/yyyy HH:mm");
            return v.ToString();
        }

        private static string Ajustar(string s, int ancho)
        {
            s = s ?? "";
            if (s.Length > ancho) s = s.Substring(0, ancho - 1) + "…";
            return s.PadRight(ancho);
        }
    }
}
