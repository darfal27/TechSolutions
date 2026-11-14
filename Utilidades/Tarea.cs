// Proyecto: Utilidades (Biblioteca de clases reutilizable)
// HILOS: ejecuta un trabajo largo (consulta, reporte) en un Thread aparte para que la pantalla no se congele.
// Importante: un hilo NO puede tocar los controles de la pantalla; el resultado vuelve al hilo de la pantalla
// con "enHiloUI" (WinForms: a => BeginInvoke(a)).
using System;
using System.Threading;

namespace Utilidades
{
    public static class Tarea
    {
        public static Thread Ejecutar<T>(Func<T> trabajo, Action<T> alTerminar, Action<Exception> alFallar, Action<Action> enHiloUI)
        {
            Thread hilo = new Thread(delegate ()
            {
                T resultado = default(T);
                Exception error = null;
                try { resultado = trabajo(); }                 // se ejecuta en el hilo nuevo
                catch (Exception ex) { error = ex; }

                try
                {
                    enHiloUI(delegate ()                      // se vuelve al hilo de la pantalla
                    {
                        if (error != null) alFallar(error); else alTerminar(resultado);
                    });
                }
                catch (InvalidOperationException) { }          // la ventana ya se cerró: no hay a quién avisar
            });
            hilo.IsBackground = true;                          // no impide cerrar la aplicación
            hilo.Name = "TechSolutions-Trabajo";
            hilo.Start();
            return hilo;
        }
    }
}
