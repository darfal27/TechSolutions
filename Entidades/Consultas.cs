// Clases de datos para las consultas LINQ (solo propiedades)
using System;

namespace Entidades
{
    // Una línea vendida con los nombres ya unidos (resultado del JOIN en Datos)
    public class VentaDetalleInfo
    {
        public int IdVenta { get; set; }
        public DateTime Fecha { get; set; }
        public string Documento { get; set; }
        public string Cliente { get; set; }
        public string Vendedor { get; set; }
        public string Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get { return Cantidad * PrecioUnitario; } }
    }

    public class ResumenCliente
    {
        public string Documento { get; set; }
        public string Cliente { get; set; }
        public int Ventas { get; set; }
        public decimal Total { get; set; }
    }

    public class ResumenProducto
    {
        public string Producto { get; set; }
        public int Unidades { get; set; }
        public decimal Importe { get; set; }
    }
}
