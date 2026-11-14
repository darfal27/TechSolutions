// CAPA PRESENTACIÓN – detecta teclado y clics del usuario en TODA la aplicación.
// Application.AddMessageFilter recibe los mensajes de cualquier ventana (también de las que se abren desde el menú),
// así que usar Clientes, Productos o Venta cuenta como actividad.
// OJO: el MOVIMIENTO del mouse NO se detecta aquí. Windows envía WM_MOUSEMOVE aunque el mouse esté quieto
// (al repintar una ventana bajo el cursor), y eso reiniciaba el contador y la sesión nunca se cerraba.
// El movimiento se mide en FrmPrincipal comparando la posición del cursor en cada Tick.
using System;
using System.Windows.Forms;

namespace Presentacion
{
    internal sealed class FiltroActividad : IMessageFilter
    {
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_SYSKEYDOWN = 0x0104;
        private const int WM_NCLBUTTONDOWN = 0x00A1;
        private const int WM_LBUTTONDOWN = 0x0201;
        private const int WM_RBUTTONDOWN = 0x0204;
        private const int WM_MOUSEWHEEL = 0x020A;

        private readonly Action alHaberActividad;

        public FiltroActividad(Action alHaberActividad) { this.alHaberActividad = alHaberActividad; }

        public bool PreFilterMessage(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_KEYDOWN:
                case WM_SYSKEYDOWN:
                case WM_NCLBUTTONDOWN:
                case WM_LBUTTONDOWN:
                case WM_RBUTTONDOWN:
                case WM_MOUSEWHEEL:
                    alHaberActividad();
                    break;
            }
            return false;   // false = el mensaje sigue su camino normal
        }
    }
}
