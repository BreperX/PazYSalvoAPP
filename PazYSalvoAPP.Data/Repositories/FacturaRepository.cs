using Microsoft.EntityFrameworkCore;
using PazYSalvoAPP.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PazYSalvoAPP.Data.Repositories
{
    // Definición de la clase FacturaRepository que implementa la interfaz IGenericRepository<Factura>
    public class FacturaRepository : IGenericRepository<Factura>
    {
        // 1- Establecer conexión
        // Declaración de un campo privado readonly llamado _context de tipo PazSalvoContext
        private readonly PazSalvoContext _context;

        // Constructor de la clase que recibe una instancia de PazSalvoContext
        public FacturaRepository(PazSalvoContext context)
        {
            _context = context; // Asignación de la instancia de PazSalvoContext al campo _context
        }

        // Método para actualizar una factura en la base de datos
        public async Task<bool> Actualizar(Factura model)
        {
            bool result = default(bool); // Inicialización de una variable booleana llamada result

            try
            {
                _context.Facturas.Update(model); // Actualización de la factura en el contexto
                await _context.SaveChangesAsync(); // Guardar los cambios en la base de datos

                return !result; // Devolver el valor inverso de result (true si se actualizó correctamente, false si no)
            }
            catch (Exception ex) // Captura de excepciones
            {
                return result; // Devolver el valor por defecto de result (false)
            }
        }

        // Método para eliminar una factura de la base de datos por su ID
        public async Task<bool> Eliminar(int id)
        {
            bool result = default(bool); // Inicialización de una variable booleana llamada result

            if (id == default(int)) return result;

            var factura = _context.Facturas.FirstOrDefault(f => f.Id == id); // Buscar la factura por su ID

            if (factura == null) return result; // Si la factura no se encontró, devolver el valor por defecto de result (false)

            try
            {
                _context.Facturas.Remove(factura); // Eliminar la factura del contexto
                await _context.SaveChangesAsync(); // Guardar los cambios en la base de datos

                return !result; // Devolver el valor inverso de result (true si se eliminó correctamente, false si no)
            }
            catch (Exception ex) // Captura de excepciones
            {
                return result; // Devolver el valor por defecto de result (false)
            }
        }

        // Método para insertar una nueva factura en la base de datos
        public async Task<bool> Insertar(Factura model)
        {
            bool result = default(bool); // Inicialización de una variable booleana llamada result

            try
            {
                _context.Facturas.Add(model); // Agregar la factura al contexto
                await _context.SaveChangesAsync(); // Guardar los cambios en la base de datos

                return !result; // Devolver el valor inverso de result (true si se insertó correctamente, false si no)
            }
            catch (Exception ex) // Captura de excepciones
            {
                return result; // Devolver el valor por defecto de result (false)
            }
        }

        // Método para leer una factura de la base de datos por su ID
        public async Task<Factura> Leer(int id)
        {
            if (id == default(int)) return null; // Verificar si el ID es cero, si es así, devolver null

            var factura = _context.Facturas.FirstAsync(f => f.Id == id); // Buscar la factura por su ID

            if (factura == null) return null; // Si la factura no se encontró, devolver null

            return await factura; // Devolver la factura encontrada
        }

        // Método para leer todas las facturas de la base de datos
        public async Task<IQueryable<Factura>> LeerTodos()
        {
            IQueryable<Factura> listaDeFacturas = _context.Facturas; // Obtener todas las facturas del contexto

            return listaDeFacturas; // Devolver la lista de facturas
        }
    }
}