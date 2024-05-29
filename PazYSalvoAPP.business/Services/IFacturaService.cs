using PazYSalvoAPP.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PazYSalvoAPP.Business.Services
{
    public interface IFacturaService
    {
        Task<bool> Insertar(Factura model);
        Task<bool> Actualizar(Factura model);
        Task<Factura> Leer(int id); // ?
        Task<IQueryable<Factura>> LeerTodos(); // ?
        Task<bool> Eliminar(int id);

        Task<Factura> ObtenerFacturasPorServicio(int servicioId);
    }
}
