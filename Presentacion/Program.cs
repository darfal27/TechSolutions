using System;
using System.Windows.Forms;
using Negocio;

namespace Presentacion
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                // Si todavía no hay usuarios, primero se crea el Administrador.
                var bll = new UsuarioBLL();
                if (!bll.HayUsuarios())
                {
                    Application.Run(new FrmUsuarios(true));
                    if (!bll.HayUsuarios()) return;   // cerró sin registrar
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo conectar a la base de datos TechSolutions.\n" +
                    "Ejecute 01_BD_TechSolutions.sql y revise la cadena de conexión en App.config.\n\n" + ex.Message,
                    "Tech Solutions", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Application.Run(new FrmLogin());
        }
    }
}
