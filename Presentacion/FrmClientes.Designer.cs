namespace Presentacion
{
    partial class FrmClientes
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblDocumento = new System.Windows.Forms.Label();
            this.txtDocumento = new System.Windows.Forms.TextBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblTelefono = new System.Windows.Forms.Label();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.btnRegistrar = new System.Windows.Forms.Button();
            this.dgvClientes = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientes)).BeginInit();
            this.SuspendLayout();
            this.lblDocumento.Name = "lblDocumento"; this.lblDocumento.Text = "DNI / RUC:";
            this.lblDocumento.Location = new System.Drawing.Point(20, 23); this.lblDocumento.AutoSize = true;
            this.txtDocumento.Name = "txtDocumento"; this.txtDocumento.MaxLength = 15;
            this.txtDocumento.Location = new System.Drawing.Point(110, 20); this.txtDocumento.Size = new System.Drawing.Size(160, 23);
            this.lblNombre.Name = "lblNombre"; this.lblNombre.Text = "Nombre:";
            this.lblNombre.Location = new System.Drawing.Point(20, 58); this.lblNombre.AutoSize = true;
            this.txtNombre.Name = "txtNombre"; this.txtNombre.MaxLength = 100;
            this.txtNombre.Location = new System.Drawing.Point(110, 55); this.txtNombre.Size = new System.Drawing.Size(300, 23);
            this.lblEmail.Name = "lblEmail"; this.lblEmail.Text = "Email:";
            this.lblEmail.Location = new System.Drawing.Point(20, 93); this.lblEmail.AutoSize = true;
            this.txtEmail.Name = "txtEmail"; this.txtEmail.MaxLength = 100;
            this.txtEmail.Location = new System.Drawing.Point(110, 90); this.txtEmail.Size = new System.Drawing.Size(300, 23);
            this.lblTelefono.Name = "lblTelefono"; this.lblTelefono.Text = "Teléfono:";
            this.lblTelefono.Location = new System.Drawing.Point(20, 128); this.lblTelefono.AutoSize = true;
            this.txtTelefono.Name = "txtTelefono"; this.txtTelefono.MaxLength = 15;
            this.txtTelefono.Location = new System.Drawing.Point(110, 125); this.txtTelefono.Size = new System.Drawing.Size(160, 23);
            this.btnRegistrar.Name = "btnRegistrar"; this.btnRegistrar.Text = "Registrar";
            this.btnRegistrar.Location = new System.Drawing.Point(440, 90); this.btnRegistrar.Size = new System.Drawing.Size(120, 32);
            this.btnRegistrar.Click += new System.EventHandler(this.btnRegistrar_Click);
            this.dgvClientes.Name = "dgvClientes";
            this.dgvClientes.AllowUserToAddRows = false; this.dgvClientes.AllowUserToDeleteRows = false; this.dgvClientes.ReadOnly = true;
            this.dgvClientes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvClientes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvClientes.Location = new System.Drawing.Point(20, 165); this.dgvClientes.Size = new System.Drawing.Size(640, 220);
            this.Controls.Add(this.lblDocumento); this.Controls.Add(this.txtDocumento);
            this.Controls.Add(this.lblNombre); this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.lblEmail); this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblTelefono); this.Controls.Add(this.txtTelefono);
            this.Controls.Add(this.btnRegistrar); this.Controls.Add(this.dgvClientes);
            this.ClientSize = new System.Drawing.Size(680, 405);
            this.Name = "FrmClientes"; this.Text = "Clientes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.FrmClientes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblDocumento;
        private System.Windows.Forms.TextBox txtDocumento;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblTelefono;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.Button btnRegistrar;
        private System.Windows.Forms.DataGridView dgvClientes;
    }
}
