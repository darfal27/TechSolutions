// CAPA PRESENTACIÓN – usuarios: registrar (hash con sal), ver intentos fallidos y desbloquear
using System;
using System.Linq;
using System.Windows.Forms;
using Negocio;

namespace Presentacion
{
    public partial class FrmUsuarios : Form
    {
        private readonly UsuarioBLL bll = new UsuarioBLL();
        private readonly bool primerAdministrador;

        // primerAdministrador = true: aún no hay usuarios; se crea el Administrador y se cierra.
        public FrmUsuarios(bool primerAdministrador)
        {
            InitializeComponent();
            this.primerAdministrador = primerAdministrador;
        }

        private void FrmUsuarios_Load(object sender, EventArgs e)
        {
            cboRol.Items.AddRange(Permisos.Roles);
            cboRol.SelectedIndex = 0;
            if (primerAdministrador)
            {
                Text = "Configuración inicial: primer administrador";
                cboRol.Enabled = false;
                dgvUsuarios.Visible = false;
                btnDesbloquear.Visible = false;
                ClientSize = new System.Drawing.Size(ClientSize.Width, 150);
            }
            else
            {
                Recargar();
            }
        }

        private void Recargar()
        {
            // Solo se muestran las columnas seguras: nunca el hash ni la sal.
            dgvUsuarios.DataSource = bll.Listar()
                .Select(u => new { u.Id, u.Login, u.Rol, u.IntentosFallidos, u.Bloqueado })
                .ToList();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                bll.Registrar(txtLogin.Text.Trim(), txtClave.Text, cboRol.Text);
                MessageBox.Show("Usuario registrado. Se guardaron la sal y el hash, no la contraseña.");
                txtLogin.Clear(); txtClave.Clear();
                if (primerAdministrador) Close(); else Recargar();
            }
            catch (ArgumentException ex) { MessageBox.Show(ex.Message, "Validación"); }
            catch (UnauthorizedAccessException ex) { MessageBox.Show(ex.Message, "Sin permiso"); }
            catch (Exception ex) { MessageBox.Show("No se pudo registrar: " + ex.Message, "Error"); }
        }

        private void btnDesbloquear_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.CurrentRow == null) return;
            try
            {
                bll.Desbloquear((int)dgvUsuarios.CurrentRow.Cells["Id"].Value);
                Recargar();
            }
            catch (UnauthorizedAccessException ex) { MessageBox.Show(ex.Message, "Sin permiso"); }
            catch (Exception ex) { MessageBox.Show("No se pudo desbloquear: " + ex.Message, "Error"); }
        }

    }
}
