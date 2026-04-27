using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V7.Domain.Entites;
using V7.Domain.Interfaces.Specifications;

namespace V7.Infrastructure.Repositories
{
    public static class SpecificationEvalutor<T> where T : BaseEntity
    {
        // fun to build the query based on the specification
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecifications<T> spec)
        {
            var query = inputQuery;
            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }
            if (spec.Includes != null)
            {
                query = spec.Includes.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));
            }
            return query;
        }
    }
}
