using System.Linq.Expressions;
using V7.Domain.Entites;

namespace V7.Domain.Interfaces.Specifications
{
    public interface ISpecifications<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get; set; }
        public List<Expression<Func<T,object>>> Includes { get; set; }
        
    }
}