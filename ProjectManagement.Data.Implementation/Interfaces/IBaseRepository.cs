using ProjectManagement.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagement.Data.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        IQueryable<T> Get();

        T Get(long id);

        Task<T> Add(T entity);

        Task<T> Update(T entity);

        Task<int> Delete(long id);

    }
}
