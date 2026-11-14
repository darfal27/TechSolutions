namespace Presentacion
{
    partial class FrmVenta
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblCliente = new System.Windows.Forms.Label();
            this.cboCliente = new System.Windows.Forms.ComboBox();
            this.lblProducto = new System.Windows.Forms.Label();
            this.cboProducto = new System.Windows.Forms.ComboBox();
            this.lblStockInfo = new System.Windows.Forms.Label();
            this.lblCantidad = new System.Windows.Forms.Label();
            this.nudCantidad = new System.Windows.Forms.NumericUpDown();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.dgvLineas = new System.Windows.Forms.DataGridView();
            this.btnQuitar = new System.Windows.Forms.Button();
            this.lblTotal = new System.Windows.Forms.Label();
            this.btnRegistrar = new System.Windows.Forms.Button();
            this.chkProcedimiento = new System.Windows.Forms.CheckBox();
            this.btnVistaPrevia = new System.Windows.Forms.Button();
            this.btnImprimir = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLineas)).BeginInit();
            this.SuspendLayout();
            this.lblCliente.Name = "lblCliente"; this.lblCliente.Text = "Cliente:";
            this.lblCliente.Location = new System.Drawing.Point(20, 23); this.lblCliente.AutoSize = true;
            this.cboCliente.Name = "cboCliente"; this.cboCliente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCliente.Location = new System.Drawing.Point(110, 20); this.cboCliente.Size = new System.Drawing.Size(400, 23);
            this.lblProducto.Name = "lblProducto"; this.lblProducto.Text = "Producto:";
            this.lblProducto.Location = new System.Drawing.Point(20, 63); this.lblProducto.AutoSize = true;
            this.cboProducto.Name = "cboProducto"; this.cboProducto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProducto.Location = new System.Drawing.Point(110, 60); this.cboProducto.Size = new System.Drawing.Size(400, 23);
            this.cboProducto.SelectedIndexChanged += new System.EventHandler(this.cboProducto_SelectedIndexChanged);
            this.lblStockInfo.Name = "lblStockInfo";
            this.lblStockInfo.Location = new System.Drawing.Point(110, 88); this.lblStockInfo.AutoSize = true;
            this.lblCantidad.Name = "lblCantidad"; this.lblCantidad.Text = "Cantidad:";
            this.lblCantidad.Location = new System.Drawing.Point(20, 118); this.lblCantidad.AutoSize = true;
            this.nudCantidad.Name = "nudCantidad"; this.nudCantidad.Minimum = 1; this.nudCantidad.Maximum = 10000; this.nudCantidad.Value = 1;
            this.nudCantidad.Location = new System.Drawing.Point(110, 115); this.nudCantidad.Size = new System.Drawing.Size(80, 23);
            this.btnAgregar.Name = "btnAgregar"; this.btnAgregar.Text = "Agregar a la venta";
            this.btnAgregar.Location = new System.Drawing.Point(210, 111); this.btnAgregar.Size = new System.Drawing.Size(150, 30);
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            this.dgvLineas.Name = "dgvLineas";
            this.dgvLineas.AllowUserToAddRows = false; this.dgvLineas.AllowUserToDeleteRows = false; this.dgvLineas.ReadOnly = true;
            this.dgvLineas.MultiSelect = false;
            this.dgvLineas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLineas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLineas.Location = new System.Drawing.Point(20, 160); this.dgvLineas.Size = new System.Drawing.Size(560, 190);
            this.btnQuitar.Name = "btnQuitar"; this.btnQuitar.Text = "Quitar línea";
            this.btnQuitar.Location = new System.Drawing.Point(20, 360); this.btnQuitar.Size = new System.Drawing.Size(110, 30);
            this.btnQuitar.Click += new System.EventHandler(this.btnQuitar_Click);
            this.lblTotal.Name = "lblTotal"; this.lblTotal.Text = "Total: S/ 0.00";
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTotal.Location = new System.Drawing.Point(300, 364); this.lblTotal.AutoSize = true;
            this.btnRegistrar.Name = "btnRegistrar"; this.btnRegistrar.Text = "Registrar venta";
            this.btnRegistrar.Location = new System.Drawing.Point(430, 358); this.btnRegistrar.Size = new System.Drawing.Size(150, 36);
            this.btnRegistrar.Click += new System.EventHandler(this.btnRegistrar_Click);
            this.chkProcedimiento.Name = "chkProcedimiento";
            this.chkProcedimiento.Text = "Usar procedimiento almacenado (transacción en SQL Server)";
            this.chkProcedimiento.Location = new System.Drawing.Point(20, 402); this.chkProcedimiento.AutoSize = true;
            this.btnVistaPrevia.Name = "btnVistaPrevia"; this.btnVistaPrevia.Text = "Vista previa del comprobante"; this.btnVistaPrevia.Enabled = false;
            this.btnVistaPrevia.Location = new System.Drawing.Point(20, 430); this.btnVistaPrevia.Size = new System.Drawing.Size(200, 30);
            this.btnVistaPrevia.Click += new System.EventHandler(this.btnVistaPrevia_Click);
            this.btnImprimir.Name = "btnImprimir"; this.btnImprimir.Text = "Imprimir comprobante"; this.btnImprimir.Enabled = false;
            this.btnImprimir.Location = new System.Drawing.Point(230, 430); this.btnImprimir.Size = new System.Drawing.Size(170, 30);
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            this.Controls.Add(this.chkProcedimiento); this.Controls.Add(this.btnVistaPrevia); this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.lblCliente); this.Controls.Add(this.cboCliente);
            this.Controls.Add(this.lblProducto); this.Controls.Add(this.cboProducto); this.Controls.Add(this.lblStockInfo);
            this.Controls.Add(this.lblCantidad); this.Controls.Add(this.nudCantidad); this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.dgvLineas); this.Controls.Add(this.btnQuitar);
            this.Controls.Add(this.lblTotal); this.Controls.Add(this.btnRegistrar);
            this.ClientSize = new System.Drawing.Size(600, 475);
            this.Name = "FrmVenta"; this.Text = "Nueva venta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.FrmVenta_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLineas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.ComboBox cboCliente;
        private System.Windows.Forms.Label lblProducto;
        private System.Windows.Forms.ComboBox cboProducto;
        private System.Windows.Forms.Label lblStockInfo;
        private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.NumericUpDown nudCantidad;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.DataGridView dgvLineas;
        private System.Windows.Forms.Button btnQuitar;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Button btnRegistrar;
        private System.Windows.Forms.CheckBox chkProcedimiento;
        private System.Windows.Forms.Button btnVistaPrevia;
        private System.Windows.Forms.Button btnImprimir;
    }
}
