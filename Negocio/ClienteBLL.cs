using System;
using System.Collections.Generic;
using Datos;
using Entidades;

namespace Negocio
{
    public class ClienteBLL
    {
        private readonly ClienteDAL dal = new ClienteDAL();

        public void Registrar(Cliente c)
        {
            Permisos.Exigir(Modulo.Clientes);
            if (c == null || string.IsNullOrWhiteSpace(c.Documento) || c.Documento.Length > 15)
                throw new ArgumentException("El documento es obligatorio (máximo 15 caracteres).");
            if (string.IsNullOrWhiteSpace(c.Nombre) || c.Nombre.Length > 100)
                throw new ArgumentException("El nombre es obligatorio (máximo 100 caracteres).");
            try { dal.Insertar(c); }
            catch (System.Data.SqlClient.SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            { throw new ArgumentException("Ya existe un cliente con ese documento."); }   // UNIQUE de la BD
        }

        // REPORTE: reglas antes de pedir los datos al procedimiento almacenado.
        public List<Cliente> Reporte(string nombre)
        {
            Permisos.Exigir(Modulo.Clientes);
            nombre = (nombre ?? "").Trim();                                    // regla: sin espacios de más
            if (nombre.Length > 100)                                           // regla: el procedimiento acepta hasta 100
                throw new ArgumentException("El nombre no puede tener más de 100 caracteres.");
            return dal.ListarPorNombre(nombre);
        }

        // La pantalla de ventas también necesita la lista de clientes.
        public List<Cliente> Listar()
        {
            Permisos.Exigir(Modulo.Clientes, Modulo.Ventas);
            return dal.Listar();
        }
    }
}
