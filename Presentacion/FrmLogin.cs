// CAPA PRESENTACIÓN – inicio de sesión (la regla de los 3 intentos está en Negocio\UsuarioBLL)
using System;
using System.Windows.Forms;
using Negocio;

namespace Presentacion
{
    public partial class FrmLogin : Form
    {
        private readonly UsuarioBLL bll = new UsuarioBLL();

        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                var u = bll.IniciarSesion(txtLogin.Text.Trim(), txtClave.Text);
                txtClave.Clear();
                var principal = new FrmPrincipal(u);
                principal.FormClosed += Principal_FormClosed;
                principal.Show();
                Hide();
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show(ex.Message, "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtClave.Clear();
                txtClave.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo conectar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // "Cerrar sesión" o cierre por inactividad vuelven al login; cerrar con la X termina la aplicación.
        private void Principal_FormClosed(object sender, FormClosedEventArgs e)
        {
            var principal = (FrmPrincipal)sender;
            bll.CerrarSesion();
            if (principal.CerroSesion || principal.CerradaPorInactividad)
            {
                txtLogin.Clear();
                txtClave.Clear();
                Show();
                txtLogin.Focus();
            }
            else
            {
                Application.Exit();
            }
        }
    }
}
