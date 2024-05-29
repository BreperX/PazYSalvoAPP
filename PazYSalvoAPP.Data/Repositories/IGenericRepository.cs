using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PazYSalvoAPP.Data.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        // Método CRUD
        Task<bool> Insertar(T model);
        Task<bool> Actualizar(T model);
        Task<T> Leer(int id); // ?
        Task<IQueryable<T>> LeerTodos(); // ?
        Task<bool> Eliminar(int id);
    }
}