namespace Presentacion
{
    partial class FrmLogin
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
            this.btnIngresar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // lblLogin
            this.lblLogin.Name = "lblLogin";
            this.lblLogin.Text = "Usuario:";
            this.lblLogin.Location = new System.Drawing.Point(20, 23);
            this.lblLogin.AutoSize = true;
            // txtLogin
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.Location = new System.Drawing.Point(120, 20);
            this.txtLogin.Size = new System.Drawing.Size(180, 23);
            this.txtLogin.MaxLength = 30;
            // lblClave
            this.lblClave.Name = "lblClave";
            this.lblClave.Text = "Contraseña:";
            this.lblClave.Location = new System.Drawing.Point(20, 58);
            this.lblClave.AutoSize = true;
            // txtClave
            this.txtClave.Name = "txtClave";
            this.txtClave.Location = new System.Drawing.Point(120, 55);
            this.txtClave.Size = new System.Drawing.Size(180, 23);
            this.txtClave.UseSystemPasswordChar = true;
            // btnIngresar
            this.btnIngresar.Name = "btnIngresar";
            this.btnIngresar.Text = "Ingresar";
            this.btnIngresar.Location = new System.Drawing.Point(120, 95);
            this.btnIngresar.Size = new System.Drawing.Size(120, 32);
            this.btnIngresar.Click += new System.EventHandler(this.btnIngresar_Click);
            // FrmLogin
            this.Controls.Add(this.lblLogin);
            this.Controls.Add(this.txtLogin);
            this.Controls.Add(this.lblClave);
            this.Controls.Add(this.txtClave);
            this.Controls.Add(this.btnIngresar);
            this.ClientSize = new System.Drawing.Size(340, 150);
            this.Name = "FrmLogin";
            this.Text = "Tech Solutions - Iniciar sesión";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.AcceptButton = this.btnIngresar;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblLogin;
        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.Label lblClave;
        private System.Windows.Forms.TextBox txtClave;
        private System.Windows.Forms.Button btnIngresar;
    }
}
