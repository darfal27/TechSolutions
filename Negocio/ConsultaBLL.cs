// CAPA NEGOCIO – consultas con LINQ.
// Patrón de la clase: Datos trae las listas SIN filtrar y aquí LINQ filtra (Where), ordena (OrderBy),
// agrupa (GroupBy) y calcula (Sum, Count, Take).
using System;
using System.Collections.Generic;
using System.Linq;
using Datos;
using Entidades;

namespace Negocio
{
    public class ConsultaBLL
    {
        private readonly ProductoDAL productos = new ProductoDAL();
        private readonly ClienteDAL clientes = new ClienteDAL();
        private readonly VentaDAL ventas = new VentaDAL();

        // Productos cuyo nombre o código contiene el texto; opcionalmente solo los de stock bajo.
        public List<Producto> BuscarProductos(string texto, bool soloStockBajo, int umbral)
        {
            Permisos.Exigir(Modulo.Productos, Modulo.Ventas);
            texto = (texto ?? "").Trim();
            return productos.Listar()
                .Where(p => texto.Length == 0 || Contiene(p.Nombre, texto) || Contiene(p.Codigo, texto))
                .Where(p => !soloStockBajo || p.Stock <= umbral)
                .OrderBy(p => p.Stock).ThenBy(p => p.Nombre)      // primero los que menos stock tienen
                .ToList();
        }

        // Clientes cuyo nombre o documento contiene el texto.
        public List<Cliente> BuscarClientes(string texto)
        {
            Permisos.Exigir(Modulo.Clientes, Modulo.Ventas);
            texto = (texto ?? "").Trim();
            return clientes.Listar()
                .Where(c => texto.Length == 0 || Contiene(c.Nombre, texto) || Contiene(c.Documento, texto))
                .OrderBy(c => c.Nombre)
                .ToList();
        }

        // Cuántas ventas y cuánto dinero suma cada cliente (GroupBy + Sum + Count).
        public List<ResumenCliente> VentasPorCliente()
        {
            Permisos.Exigir(Modulo.Ventas);
            return ventas.ListarDetalle()
                .GroupBy(d => new { d.Documento, d.Cliente })
                .Select(g => new ResumenCliente
                {
                    Documento = g.Key.Documento,
                    Cliente = g.Key.Cliente,
                    Ventas = g.Select(x => x.IdVenta).Distinct().Count(),
                    Total = g.Sum(x => x.Subtotal)
                })
                .OrderByDescending(r => r.Total)
                .ToList();
        }

        // Los "top" productos más vendidos (por unidades).
        public List<ResumenProducto> ProductosMasVendidos(int top)
        {
            Permisos.Exigir(Modulo.Ventas);
            return ventas.ListarDetalle()
                .GroupBy(d => d.Producto)
                .Select(g => new ResumenProducto
                {
                    Producto = g.Key,
                    Unidades = g.Sum(x => x.Cantidad),
                    Importe = g.Sum(x => x.Subtotal)
                })
                .OrderByDescending(r => r.Unidades)
                .Take(top)
                .ToList();
        }

        // Búsqueda sin distinguir mayúsculas de minúsculas
        private static bool Contiene(string campo, string texto)
        {
            return campo != null && campo.IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0;
        }
    }
}
