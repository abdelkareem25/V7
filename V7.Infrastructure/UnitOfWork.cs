using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V7.Domain.Entites;
using V7.Domain.Interfaces;
using V7.Domain.Interfaces.Repositories;
using V7.Infrastructure.Data.Context;
using V7.Infrastructure.Repositories;

namespace V7.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly V7Db _dbContext;
        private Hashtable _repositories;

        public UnitOfWork(V7Db dbContext)
        {
            _dbContext = dbContext;
            _repositories = new Hashtable();
        }
        public async ValueTask DisposeAsync()
        => await _dbContext.DisposeAsync();

        public async Task<int> CompleteAsync()
       => await _dbContext.SaveChangesAsync();

        public IGenericRepository<T> Repository<T>() where T : BaseEntity
        {
            var type = typeof(T).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repository = new GenericRepository<T>(_dbContext);
                _repositories.Add(type, repository);
            }

            return (IGenericRepository<T>)_repositories[type]; 
        }


    }
}
