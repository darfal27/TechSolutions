// CAPA PRESENTACIÓN – menú: cada rol ve SOLO las opciones que le corresponden
// + CIERRE DE SESIÓN POR INACTIVIDAD con el control Timer.
using System;
using System.Configuration;
using System.Linq;
using System.Windows.Forms;
using Entidades;
using Negocio;

namespace Presentacion
{
    public partial class FrmPrincipal : Form
    {
        // ===== INACTIVIDAD (1/5): datos que usa el Timer =====
        private readonly int limiteInactividad;       // segundos permitidos sin actividad (App.config)
        private int segundosInactivo = 0;             // cuántos segundos lleva sin actividad
        private const int SegundosAviso = 30;   // segundos para que salga la advertencia
        private bool avisoMostrado = false;
        private FiltroActividad filtro;
        private System.Drawing.Point ultimaPosicion;     // dónde estaba el cursor en el Tick anterior
        public bool CerroSesion { get; private set; }             // true = "Cerrar sesión" (vuelve el login)
        public bool CerradaPorInactividad { get; private set; }   // true = lo cerró el Timer (vuelve el login)

        public FrmPrincipal(Usuario u)
        {
            InitializeComponent();
            lblInactividad.Visible = false; 
            filtro = new FiltroActividad(delegate { segundosInactivo = 0; avisoMostrado = false; });
            lblBienvenida.Text = "Bienvenido, " + u.Login + "  (rol: " + u.Rol + ")";
            limiteInactividad = LeerLimite();

            mnuClientes.Visible = Permisos.Puede(u.Rol, Modulo.Clientes);
            mnuReporteClientes.Visible = Permisos.Puede(u.Rol, Modulo.Clientes);
            mnuReporteVentas.Visible = Permisos.Puede(u.Rol, Modulo.Ventas);
            mnuProductos.Visible = Permisos.Puede(u.Rol, Modulo.Productos);
            mnuVentas.Visible = Permisos.Puede(u.Rol, Modulo.Ventas);
            mnuConsultas.Visible = Permisos.Puede(u.Rol, Modulo.Consultas);
            mnuUsuarios.Visible = Permisos.Puede(u.Rol, Modulo.Usuarios);
        }

        // Lee <appSettings> "SegundosInactividad" de App.config
        private static int LeerLimite()
        {
            int n;
            return int.TryParse(ConfigurationManager.AppSettings["SegundosInactividad"], out n) && n >= 10 ? n : 10;
        }

        // ===== INACTIVIDAD (2/5): al abrir el menú se enciende el Timer y el detector de actividad =====
        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            // INACTIVIDAD (4/5): teclas y clics reinician la cuenta (filtro) y el movimiento del mouse también (ver Tick)
            filtro = new FiltroActividad(delegate { segundosInactivo = 0; });
            ultimaPosicion = Cursor.Position;
            Application.AddMessageFilter(filtro);
            tmrInactividad.Interval = 1000;   // 1000 ms = el evento Tick ocurre cada segundo
            tmrInactividad.Start();           // enciende el Timer (igual que Enabled = true)
        }

        // ===== INACTIVIDAD (3/5): cada segundo suma 1; al llegar al límite cierra la sesión =====
        private void tmrInactividad_Tick(object sender, EventArgs e)
        {
            if (Cursor.Position != ultimaPosicion)
            {
                ultimaPosicion = Cursor.Position;
                segundosInactivo = 0;
                avisoMostrado = false;
            }

            segundosInactivo++;

            // El contador solo se ve desde el segundo del aviso en adelante
            lblInactividad.Visible = segundosInactivo >= SegundosAviso;
            lblInactividad.ForeColor = System.Drawing.Color.Red;
            lblInactividad.Text = "Inactividad: " + segundosInactivo + " s de " + limiteInactividad + " s";

            if (segundosInactivo >= SegundosAviso && !avisoMostrado)
            {
                avisoMostrado = true;
            }

            if (limiteInactividad - segundosInactivo <= 0) CerrarPorInactividad();
        }

        // ===== INACTIVIDAD (5/5): cierra las ventanas abiertas, avisa y vuelve al login =====
        private void CerrarPorInactividad()
        {
            tmrInactividad.Stop();                // apaga el Timer (igual que Enabled = false)
            CerradaPorInactividad = true;

            // Cierra las ventanas abiertas desde el menú (Clientes, Venta...), excepto este menú y el login
            foreach (Form f in Application.OpenForms.Cast<Form>().Where(w => w != this && !(w is FrmLogin)).ToList())
                f.Close();

            // BeginInvoke: el aviso y el cierre ocurren cuando ya terminaron las ventanas hijas
            BeginInvoke(new Action(delegate
            {
                MessageBox.Show(this, "La sesión se cerró por inactividad.", "Tech Solutions");
                Close();                          // FrmLogin vuelve a aparecer
            }));
        }

        // Al cerrar el menú (por la razón que sea) se apaga el detector y el Timer
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            tmrInactividad.Stop();
            if (filtro != null) Application.RemoveMessageFilter(filtro);
            base.OnFormClosed(e);                 // aquí se dispara FormClosed que escucha FrmLogin
        }

        private void mnuClientes_Click(object sender, EventArgs e) { using (var f = new FrmClientes()) f.ShowDialog(); }
        private void mnuReporteClientes_Click(object sender, EventArgs e) { using (var f = new FrmReporteClientes()) f.ShowDialog(); }
        private void mnuReporteVentas_Click(object sender, EventArgs e) { using (var f = new FrmReporteVentas()) f.ShowDialog(); }
        private void mnuProductos_Click(object sender, EventArgs e) { using (var f = new FrmProductos()) f.ShowDialog(); }
        private void mnuVentas_Click(object sender, EventArgs e) { using (var f = new FrmVenta()) f.ShowDialog(); }
        private void mnuConsultas_Click(object sender, EventArgs e) { using (var f = new FrmConsultas()) f.ShowDialog(); }
        private void mnuUsuarios_Click(object sender, EventArgs e) { using (var f = new FrmUsuarios(false)) f.ShowDialog(); }

        private void mnuCerrarSesion_Click(object sender, EventArgs e)
        {
            CerroSesion = true;
            Close();
        }
    }
}
