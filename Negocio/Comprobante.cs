// CAPA NEGOCIO – arma el texto del comprobante de una venta (la Presentación solo lo manda a imprimir)
using System;
using System.Collections.Generic;
using System.Linq;
using Entidades;
using Utilidades;

namespace Negocio
{
    public static class Comprobante
    {
        public static List<string> Armar(Venta v, string cliente)
        {
            var l = new List<string>();
            l.Add("TECH SOLUTIONS - COMPROBANTE DE VENTA");
            l.Add("");
            l.Add("Venta N.º : " + v.Id.ToString("D6"));
            l.Add("Fecha     : " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
            l.Add("Cliente   : " + cliente);
            l.Add("Vendedor  : " + (Sesion.Actual == null ? "" : Sesion.Actual.Login));
            l.Add("");
            var filas = v.Detalles.Select(d => new[]
                { d.Producto, d.Cantidad.ToString(), d.PrecioUnitario.ToString("N2"), d.Subtotal.ToString("N2") }).ToList();
            l.AddRange(ServicioImpresion.FormatearTabla(new[] { "Producto", "Cant", "P.Unit", "Subtotal" }, filas));
            l.Add("");
            l.Add("TOTAL: S/ " + v.Total.ToString("N2"));
            return l;
        }
    }
}
