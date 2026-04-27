using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V7.Domain.Entites;
using V7.Domain.Interfaces.Repositories;
using V7.Domain.Interfaces.Specifications;
using V7.Infrastructure.Data.Context;

namespace V7.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly V7Db _db;

        public GenericRepository(V7Db db)
        {
            _db = db;
        }
        
        public async Task<IReadOnlyList<T>> GetAllAsync(ISpecifications<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<T> GetByIdAsync(ISpecifications<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }
        //public async Task<IReadOnlyList<T>> GetAllAsync()
        //{
        //    return aw ait _db.Set<T>().ToListAsync();
        //}

        //public async Task<T> GetByIdAsync(int id)
        //{
        //    return await _db.Set<T>().FindAsync(id);
        //}
        
        public async Task AddAsync(T entity)
        {
            await _db.Set<T>().AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _db.Set<T>().Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _db.Set<T>().Update(entity);
            await _db.SaveChangesAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecifications<T> spec)
        {
            return SpecificationEvalutor<T>.GetQuery(_db.Set<T>(), spec);
        }

    }
}
