using System;
using System.Collections.Generic;
using Datos;
using Entidades;

namespace Negocio
{
    public class ProductoBLL
    {
        private readonly ProductoDAL dal = new ProductoDAL();

        public void Registrar(Producto p)
        {
            Permisos.Exigir(Modulo.Productos);
            if (p == null || string.IsNullOrWhiteSpace(p.Codigo) || p.Codigo.Length > 20)
                throw new ArgumentException("El código es obligatorio (máximo 20 caracteres).");
            if (string.IsNullOrWhiteSpace(p.Nombre) || p.Nombre.Length > 100)
                throw new ArgumentException("El nombre es obligatorio (máximo 100 caracteres).");
            if (p.Precio <= 0) throw new ArgumentException("El precio debe ser mayor que cero.");
            if (p.Stock < 0) throw new ArgumentException("El stock no puede ser negativo.");
            try { dal.Insertar(p); }
            catch (System.Data.SqlClient.SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            { throw new ArgumentException("Ya existe un producto con ese código."); }     // UNIQUE de la BD
        }

        public void IngresarStock(int idProducto, int cantidad)
        {
            Permisos.Exigir(Modulo.Productos);
            if (cantidad <= 0) throw new ArgumentException("La cantidad debe ser mayor que cero.");
            dal.SumarStock(idProducto, cantidad);
        }

        // La pantalla de ventas también necesita ver productos y stock.
        public List<Producto> Listar()
        {
            Permisos.Exigir(Modulo.Productos, Modulo.Ventas);
            return dal.Listar();
        }
    }
}
