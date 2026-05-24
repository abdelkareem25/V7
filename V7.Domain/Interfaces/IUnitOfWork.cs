using V7.Domain.Entites;
using V7.Domain.Interfaces.Repositories;

namespace V7.Domain.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IGenericRepository<T> Repository<T>() where T : BaseEntity;
        Task<int> CompleteAsync();
    }
}
