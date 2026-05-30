using V7.Domain.Entites;
using V7.Domain.Interfaces.Specifications;

namespace V7.Domain.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>>GetAllAsync(ISpecifications<T> spec);
        Task<IReadOnlyList<T>>GetAllAsync();
        Task<T> GetEntityAsync(ISpecifications<T> spec);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity); 
        Task DeleteAsync(T entity);
        Task<int> GetCountAsync(ISpecifications<T> spec);
        Task<T> GetByIdAsync(int id); // edit: added this method to get entity by id without specification
    }
}
