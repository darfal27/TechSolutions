using System;
using System.Collections.Generic;

namespace Entidades
{
    public class Venta
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int IdCliente { get; set; }
        public int IdUsuario { get; set; }
        public decimal Total { get; set; }
        public List<DetalleVenta> Detalles { get; set; } = new List<DetalleVenta>();
    }

    public class DetalleVenta
    {
        public int IdProducto { get; set; }
        public string Producto { get; set; }          // solo para mostrar en pantalla
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }   // la BD lo trae al descontar el stock
        public decimal Subtotal { get { return Cantidad * PrecioUnitario; } }
    }
}
