namespace Presentacion
{
    partial class FrmUsuarios
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblLogin = new System.Windows.Forms.Label();
            this.txtLogin = new System.Windows.Forms.TextBox();
            this.lblClave = new System.Windows.Forms.Label();
            this.txtClave = new System.Windows.Forms.TextBox();
            this.lblRol = new System.Windows.Forms.Label();
            this.cboRol = new System.Windows.Forms.ComboBox();
            this.btnRegistrar = new System.Windows.Forms.Button();
            this.dgvUsuarios = new System.Windows.Forms.DataGridView();
            this.btnDesbloquear = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios)).BeginInit();
            this.SuspendLayout();
            // lblLogin
            this.lblLogin.Name = "lblLogin"; this.lblLogin.Text = "Usuario:";
            this.lblLogin.Location = new System.Drawing.Point(20, 23); this.lblLogin.AutoSize = true;
            // txtLogin
            this.txtLogin.Name = "txtLogin"; this.txtLogin.MaxLength = 30;
            this.txtLogin.Location = new System.Drawing.Point(120, 20); this.txtLogin.Size = new System.Drawing.Size(180, 23);
            // lblClave
            this.lblClave.Name = "lblClave"; this.lblClave.Text = "Contraseña:";
            this.lblClave.Location = new System.Drawing.Point(20, 58); this.lblClave.AutoSize = true;
            // txtClave
            this.txtClave.Name = "txtClave"; this.txtClave.UseSystemPasswordChar = true;
            this.txtClave.Location = new System.Drawing.Point(120, 55); this.txtClave.Size = new System.Drawing.Size(180, 23);
            // lblRol
            this.lblRol.Name = "lblRol"; this.lblRol.Text = "Rol:";
            this.lblRol.Location = new System.Drawing.Point(20, 93); this.lblRol.AutoSize = true;
            // cboRol
            this.cboRol.Name = "cboRol"; this.cboRol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRol.Location = new System.Drawing.Point(120, 90); this.cboRol.Size = new System.Drawing.Size(180, 23);
            // btnRegistrar
            this.btnRegistrar.Name = "btnRegistrar"; this.btnRegistrar.Text = "Registrar";
            this.btnRegistrar.Location = new System.Drawing.Point(330, 53); this.btnRegistrar.Size = new System.Drawing.Size(120, 32);
            this.btnRegistrar.Click += new System.EventHandler(this.btnRegistrar_Click);
            // dgvUsuarios
            this.dgvUsuarios.Name = "dgvUsuarios";
            this.dgvUsuarios.AllowUserToAddRows = false; this.dgvUsuarios.AllowUserToDeleteRows = false;
            this.dgvUsuarios.ReadOnly = true; this.dgvUsuarios.MultiSelect = false;
            this.dgvUsuarios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUsuarios.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvUsuarios.Location = new System.Drawing.Point(20, 135); this.dgvUsuarios.Size = new System.Drawing.Size(520, 190);
            // btnDesbloquear
            this.btnDesbloquear.Name = "btnDesbloquear"; this.btnDesbloquear.Text = "Desbloquear seleccionado";
            this.btnDesbloquear.Location = new System.Drawing.Point(20, 335); this.btnDesbloquear.Size = new System.Drawing.Size(170, 32);
            this.btnDesbloquear.Click += new System.EventHandler(this.btnDesbloquear_Click);
            // FrmUsuarios
            this.Controls.Add(this.lblLogin); this.Controls.Add(this.txtLogin);
            this.Controls.Add(this.lblClave); this.Controls.Add(this.txtClave);
            this.Controls.Add(this.lblRol); this.Controls.Add(this.cboRol);
            this.Controls.Add(this.btnRegistrar); this.Controls.Add(this.dgvUsuarios); this.Controls.Add(this.btnDesbloquear);
            this.ClientSize = new System.Drawing.Size(560, 385);
            this.Name = "FrmUsuarios"; this.Text = "Usuarios";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.FrmUsuarios_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblLogin;
        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.Label lblClave;
        private System.Windows.Forms.TextBox txtClave;
        private System.Windows.Forms.Label lblRol;
        private System.Windows.Forms.ComboBox cboRol;
        private System.Windows.Forms.Button btnRegistrar;
        private System.Windows.Forms.DataGridView dgvUsuarios;
        private System.Windows.Forms.Button btnDesbloquear;
    }
}
