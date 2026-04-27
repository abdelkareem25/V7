using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V7.Domain.Entites;
using V7.Domain.Interfaces.Specifications;

namespace V7.Domain.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>>GetAllAsync(ISpecifications<T> spec);
        Task<T> GetByIdAsync(ISpecifications<T> spec);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity); 
        Task DeleteAsync(T entity);
    }
}
