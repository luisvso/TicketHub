using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketHub.Interfaces.IRepository
{
    public interface IBaseRepository<T> where T : class
    {

        Task<IEnumerable<T>> GetAll();
        Task<T> GetById<Tid>(Tid id);
        Task<T> Create(T entity);
        Task Update(T entity);
        Task Delete(T id);
        Task<int> SaveChangesAsync();

    }
}