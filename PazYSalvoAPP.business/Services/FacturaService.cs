using PazYSalvoAPP.Data.Context;
using PazYSalvoAPP.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PazYSalvoAPP.Business.Services
{
    public class FacturaService : IFacturaService
    {
        private readonly IGenericRepository<Factura> _modelRepository;
        public FacturaService(IGenericRepository<Factura> modelRepository)
        {
            _modelRepository = modelRepository;
        }
        public async Task<bool> Actualizar(Factura model)
        {
            return await _modelRepository.Actualizar(model);
        }

        public async Task<bool> Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Insertar(Factura model)
        {
            return await _modelRepository.Insertar(model);
        }

        public async Task<Factura> Leer(int id)
        {
            return await _modelRepository.Leer(id);
        }

        public async Task<IQueryable<Factura>> LeerTodos()
        {
            return await _modelRepository.LeerTodos();
        }

        public async Task<Factura> ObtenerFacturasPorServicio(int id)
        {
            var facturasPorServicio = await _modelRepository.LeerTodos();
            Factura facturas = facturasPorServicio.Where(f => f.ServicioAdquiridoId == id).FirstOrDefault();

            return facturas;
        }
    }
}