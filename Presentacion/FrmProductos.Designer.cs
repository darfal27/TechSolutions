namespace Presentacion
{
    partial class FrmProductos
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblCodigo = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblPrecio = new System.Windows.Forms.Label();
            this.nudPrecio = new System.Windows.Forms.NumericUpDown();
            this.lblStock = new System.Windows.Forms.Label();
            this.nudStock = new System.Windows.Forms.NumericUpDown();
            this.btnRegistrar = new System.Windows.Forms.Button();
            this.dgvProductos = new System.Windows.Forms.DataGridView();
            this.lblEntrada = new System.Windows.Forms.Label();
            this.nudEntrada = new System.Windows.Forms.NumericUpDown();
            this.btnIngresarStock = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrecio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEntrada)).BeginInit();
            this.SuspendLayout();
            this.lblCodigo.Name = "lblCodigo"; this.lblCodigo.Text = "Código:";
            this.lblCodigo.Location = new System.Drawing.Point(20, 23); this.lblCodigo.AutoSize = true;
            this.txtCodigo.Name = "txtCodigo"; this.txtCodigo.MaxLength = 20;
            this.txtCodigo.Location = new System.Drawing.Point(110, 20); this.txtCodigo.Size = new System.Drawing.Size(160, 23);
            this.lblNombre.Name = "lblNombre"; this.lblNombre.Text = "Nombre:";
            this.lblNombre.Location = new System.Drawing.Point(20, 58); this.lblNombre.AutoSize = true;
            this.txtNombre.Name = "txtNombre"; this.txtNombre.MaxLength = 100;
            this.txtNombre.Location = new System.Drawing.Point(110, 55); this.txtNombre.Size = new System.Drawing.Size(300, 23);
            this.lblPrecio.Name = "lblPrecio"; this.lblPrecio.Text = "Precio (S/):";
            this.lblPrecio.Location = new System.Drawing.Point(20, 93); this.lblPrecio.AutoSize = true;
            this.nudPrecio.Name = "nudPrecio"; this.nudPrecio.DecimalPlaces = 2; this.nudPrecio.Minimum = 1; this.nudPrecio.Maximum = 1000000;
            this.nudPrecio.Value = 1;
            this.nudPrecio.Location = new System.Drawing.Point(110, 90); this.nudPrecio.Size = new System.Drawing.Size(120, 23);
            this.lblStock.Name = "lblStock"; this.lblStock.Text = "Stock inicial:";
            this.lblStock.Location = new System.Drawing.Point(20, 128); this.lblStock.AutoSize = true;
            this.nudStock.Name = "nudStock"; this.nudStock.Minimum = 0; this.nudStock.Maximum = 100000;
            this.nudStock.Location = new System.Drawing.Point(110, 125); this.nudStock.Size = new System.Drawing.Size(120, 23);
            this.btnRegistrar.Name = "btnRegistrar"; this.btnRegistrar.Text = "Registrar";
            this.btnRegistrar.Location = new System.Drawing.Point(440, 90); this.btnRegistrar.Size = new System.Drawing.Size(120, 32);
            this.btnRegistrar.Click += new System.EventHandler(this.btnRegistrar_Click);
            this.dgvProductos.Name = "dgvProductos";
            this.dgvProductos.AllowUserToAddRows = false; this.dgvProductos.AllowUserToDeleteRows = false; this.dgvProductos.ReadOnly = true;
            this.dgvProductos.MultiSelect = false;
            this.dgvProductos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProductos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProductos.Location = new System.Drawing.Point(20, 165); this.dgvProductos.Size = new System.Drawing.Size(640, 200);
            this.lblEntrada.Name = "lblEntrada"; this.lblEntrada.Text = "Ingresar al producto seleccionado:";
            this.lblEntrada.Location = new System.Drawing.Point(20, 382); this.lblEntrada.AutoSize = true;
            this.nudEntrada.Name = "nudEntrada"; this.nudEntrada.Minimum = 1; this.nudEntrada.Maximum = 100000; this.nudEntrada.Value = 1;
            this.nudEntrada.Location = new System.Drawing.Point(230, 379); this.nudEntrada.Size = new System.Drawing.Size(80, 23);
            this.btnIngresarStock.Name = "btnIngresarStock"; this.btnIngresarStock.Text = "Ingresar stock";
            this.btnIngresarStock.Location = new System.Drawing.Point(330, 375); this.btnIngresarStock.Size = new System.Drawing.Size(130, 30);
            this.btnIngresarStock.Click += new System.EventHandler(this.btnIngresarStock_Click);
            this.Controls.Add(this.lblCodigo); this.Controls.Add(this.txtCodigo);
            this.Controls.Add(this.lblNombre); this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.lblPrecio); this.Controls.Add(this.nudPrecio);
            this.Controls.Add(this.lblStock); this.Controls.Add(this.nudStock);
            this.Controls.Add(this.btnRegistrar); this.Controls.Add(this.dgvProductos);
            this.Controls.Add(this.lblEntrada); this.Controls.Add(this.nudEntrada); this.Controls.Add(this.btnIngresarStock);
            this.ClientSize = new System.Drawing.Size(680, 425);
            this.Name = "FrmProductos"; this.Text = "Productos e inventario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.FrmProductos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudPrecio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudEntrada)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblPrecio;
        private System.Windows.Forms.NumericUpDown nudPrecio;
        private System.Windows.Forms.Label lblStock;
        private System.Windows.Forms.NumericUpDown nudStock;
        private System.Windows.Forms.Button btnRegistrar;
        private System.Windows.Forms.DataGridView dgvProductos;
        private System.Windows.Forms.Label lblEntrada;
        private System.Windows.Forms.NumericUpDown nudEntrada;
        private System.Windows.Forms.Button btnIngresarStock;
    }
}
