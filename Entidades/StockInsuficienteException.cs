using System;

namespace Entidades
{
    // Se lanza dentro de la transacción de venta; provoca el ROLLBACK de toda la venta.
    public class StockInsuficienteException : Exception
    {
        public StockInsuficienteException(string mensaje) : base(mensaje) { }
    }
}
