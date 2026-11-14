namespace Presentacion
{
    partial class FrmConsultas
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblConsulta = new System.Windows.Forms.Label();
            this.cboConsulta = new System.Windows.Forms.ComboBox();
            this.lblBuscar = new System.Windows.Forms.Label();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.chkStockBajo = new System.Windows.Forms.CheckBox();
            this.nudUmbral = new System.Windows.Forms.NumericUpDown();
            this.lblTop = new System.Windows.Forms.Label();
            this.nudTop = new System.Windows.Forms.NumericUpDown();
            this.btnConsultar = new System.Windows.Forms.Button();
            this.dgvResultado = new System.Windows.Forms.DataGridView();
            this.lblFilas = new System.Windows.Forms.Label();
            this.btnVistaPrevia = new System.Windows.Forms.Button();
            this.btnImprimir = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudUmbral)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultado)).BeginInit();
            this.SuspendLayout();
            this.lblConsulta.Name = "lblConsulta"; this.lblConsulta.Text = "Consulta:";
            this.lblConsulta.Location = new System.Drawing.Point(20, 23); this.lblConsulta.AutoSize = true;
            this.cboConsulta.Name = "cboConsulta"; this.cboConsulta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboConsulta.Location = new System.Drawing.Point(110, 20); this.cboConsulta.Size = new System.Drawing.Size(280, 23);
            this.cboConsulta.SelectedIndexChanged += new System.EventHandler(this.cboConsulta_SelectedIndexChanged);
            this.lblBuscar.Name = "lblBuscar"; this.lblBuscar.Text = "Buscar texto:";
            this.lblBuscar.Location = new System.Drawing.Point(20, 58); this.lblBuscar.AutoSize = true;
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Location = new System.Drawing.Point(110, 55); this.txtBuscar.Size = new System.Drawing.Size(280, 23);
            this.chkStockBajo.Name = "chkStockBajo"; this.chkStockBajo.Text = "Solo con stock menor o igual a:";
            this.chkStockBajo.Location = new System.Drawing.Point(110, 90); this.chkStockBajo.AutoSize = true;
            this.nudUmbral.Name = "nudUmbral"; this.nudUmbral.Minimum = 0; this.nudUmbral.Maximum = 100000; this.nudUmbral.Value = 5;
            this.nudUmbral.Location = new System.Drawing.Point(330, 88); this.nudUmbral.Size = new System.Drawing.Size(60, 23);
            this.lblTop.Name = "lblTop"; this.lblTop.Text = "Top:";
            this.lblTop.Location = new System.Drawing.Point(420, 23); this.lblTop.AutoSize = true;
            this.nudTop.Name = "nudTop"; this.nudTop.Minimum = 1; this.nudTop.Maximum = 100; this.nudTop.Value = 5;
            this.nudTop.Location = new System.Drawing.Point(455, 20); this.nudTop.Size = new System.Drawing.Size(60, 23);
            this.btnConsultar.Name = "btnConsultar"; this.btnConsultar.Text = "Consultar";
            this.btnConsultar.Location = new System.Drawing.Point(540, 52); this.btnConsultar.Size = new System.Drawing.Size(120, 32);
            this.btnConsultar.Click += new System.EventHandler(this.btnConsultar_Click);
            this.dgvResultado.Name = "dgvResultado";
            this.dgvResultado.AllowUserToAddRows = false; this.dgvResultado.AllowUserToDeleteRows = false; this.dgvResultado.ReadOnly = true;
            this.dgvResultado.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvResultado.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvResultado.Location = new System.Drawing.Point(20, 130); this.dgvResultado.Size = new System.Drawing.Size(640, 250);
            this.lblFilas.Name = "lblFilas"; this.lblFilas.Text = "Filas: 0";
            this.lblFilas.Location = new System.Drawing.Point(20, 392); this.lblFilas.AutoSize = true;
            this.btnVistaPrevia.Name = "btnVistaPrevia"; this.btnVistaPrevia.Text = "Vista previa";
            this.btnVistaPrevia.Location = new System.Drawing.Point(400, 386); this.btnVistaPrevia.Size = new System.Drawing.Size(120, 28);
            this.btnVistaPrevia.Click += new System.EventHandler(this.btnVistaPrevia_Click);
            this.btnImprimir.Name = "btnImprimir"; this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.Location = new System.Drawing.Point(530, 386); this.btnImprimir.Size = new System.Drawing.Size(130, 28);
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            this.Controls.Add(this.btnVistaPrevia); this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.lblConsulta); this.Controls.Add(this.cboConsulta);
            this.Controls.Add(this.lblBuscar); this.Controls.Add(this.txtBuscar);
            this.Controls.Add(this.chkStockBajo); this.Controls.Add(this.nudUmbral);
            this.Controls.Add(this.lblTop); this.Controls.Add(this.nudTop);
            this.Controls.Add(this.btnConsultar); this.Controls.Add(this.dgvResultado); this.Controls.Add(this.lblFilas);
            this.ClientSize = new System.Drawing.Size(680, 420);
            this.Name = "FrmConsultas"; this.Text = "Consultas (LINQ)";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.AcceptButton = this.btnConsultar;
            this.Load += new System.EventHandler(this.FrmConsultas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudUmbral)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResultado)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblConsulta;
        private System.Windows.Forms.ComboBox cboConsulta;
        private System.Windows.Forms.Label lblBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.CheckBox chkStockBajo;
        private System.Windows.Forms.NumericUpDown nudUmbral;
        private System.Windows.Forms.Label lblTop;
        private System.Windows.Forms.NumericUpDown nudTop;
        private System.Windows.Forms.Button btnConsultar;
        private System.Windows.Forms.DataGridView dgvResultado;
        private System.Windows.Forms.Label lblFilas;
        private System.Windows.Forms.Button btnVistaPrevia;
        private System.Windows.Forms.Button btnImprimir;
    }
}
