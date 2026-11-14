namespace Presentacion
{
    partial class FrmPrincipal
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.mnuClientes = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuReporteClientes = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuReporteVentas = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuProductos = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuVentas = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuConsultas = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuUsuarios = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCerrarSesion = new System.Windows.Forms.ToolStripMenuItem();
            this.lblBienvenida = new System.Windows.Forms.Label();
            this.lblInactividad = new System.Windows.Forms.Label();
            this.tmrInactividad = new System.Windows.Forms.Timer(this.components);
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // mnuClientes
            this.mnuClientes.Name = "mnuClientes";
            this.mnuClientes.Text = "Clientes";
            this.mnuClientes.Click += new System.EventHandler(this.mnuClientes_Click);
            // mnuReporteClientes
            this.mnuReporteClientes.Name = "mnuReporteClientes";
            this.mnuReporteClientes.Text = "Reporte de clientes";
            this.mnuReporteClientes.Click += new System.EventHandler(this.mnuReporteClientes_Click);
            // mnuReporteVentas
            this.mnuReporteVentas.Name = "mnuReporteVentas";
            this.mnuReporteVentas.Text = "Reporte de ventas";
            this.mnuReporteVentas.Click += new System.EventHandler(this.mnuReporteVentas_Click);
            // mnuProductos
            this.mnuProductos.Name = "mnuProductos";
            this.mnuProductos.Text = "Productos";
            this.mnuProductos.Click += new System.EventHandler(this.mnuProductos_Click);
            // mnuVentas
            this.mnuVentas.Name = "mnuVentas";
            this.mnuVentas.Text = "Nueva venta";
            this.mnuVentas.Click += new System.EventHandler(this.mnuVentas_Click);
            // mnuConsultas
            this.mnuConsultas.Name = "mnuConsultas";
            this.mnuConsultas.Text = "Consultas";
            this.mnuConsultas.Click += new System.EventHandler(this.mnuConsultas_Click);
            // mnuUsuarios
            this.mnuUsuarios.Name = "mnuUsuarios";
            this.mnuUsuarios.Text = "Usuarios";
            this.mnuUsuarios.Click += new System.EventHandler(this.mnuUsuarios_Click);
            // mnuCerrarSesion
            this.mnuCerrarSesion.Name = "mnuCerrarSesion";
            this.mnuCerrarSesion.Text = "Cerrar sesión";
            this.mnuCerrarSesion.Click += new System.EventHandler(this.mnuCerrarSesion_Click);
            // menu
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.mnuClientes, this.mnuReporteClientes, this.mnuProductos, this.mnuVentas, this.mnuReporteVentas, this.mnuConsultas, this.mnuUsuarios, this.mnuCerrarSesion });
            this.menu.Name = "menu";
            // lblBienvenida
            this.lblBienvenida.Name = "lblBienvenida";
            this.lblBienvenida.Location = new System.Drawing.Point(20, 50);
            this.lblBienvenida.AutoSize = true;
            this.lblBienvenida.Font = new System.Drawing.Font("Segoe UI", 12F);
            // lblInactividad
            this.lblInactividad.Name = "lblInactividad";
            this.lblInactividad.Location = new System.Drawing.Point(20, 270);
            this.lblInactividad.AutoSize = true;
            this.lblInactividad.ForeColor = System.Drawing.Color.Firebrick;
            // tmrInactividad
            this.tmrInactividad.Interval = 1000;
            this.tmrInactividad.Tick += new System.EventHandler(this.tmrInactividad_Tick);
            // FrmPrincipal
            this.Controls.Add(this.lblInactividad);
            this.Controls.Add(this.lblBienvenida);
            this.Controls.Add(this.menu);
            this.MainMenuStrip = this.menu;
            this.ClientSize = new System.Drawing.Size(900, 300);
            this.Name = "FrmPrincipal";
            this.Text = "Tech Solutions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.FrmPrincipal_Load);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem mnuClientes;
        private System.Windows.Forms.ToolStripMenuItem mnuReporteClientes;
        private System.Windows.Forms.ToolStripMenuItem mnuReporteVentas;
        private System.Windows.Forms.ToolStripMenuItem mnuProductos;
        private System.Windows.Forms.ToolStripMenuItem mnuVentas;
        private System.Windows.Forms.ToolStripMenuItem mnuConsultas;
        private System.Windows.Forms.ToolStripMenuItem mnuUsuarios;
        private System.Windows.Forms.ToolStripMenuItem mnuCerrarSesion;
        private System.Windows.Forms.Label lblBienvenida;
        private System.Windows.Forms.Label lblInactividad;
        private System.Windows.Forms.Timer tmrInactividad;
    }
}
