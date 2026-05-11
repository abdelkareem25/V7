using System.Linq.Expressions;
using V7.Domain.Entites;

namespace V7.Domain.Interfaces.Specifications
{
    public class BaseSpecifications<T> : ISpecifications<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get ; set ; }
        public List<Expression<Func<T, object>>> Includes { get ; set ; } = new List<Expression<Func<T, object>>>();

        //GetAll
        public BaseSpecifications()
        {
           // Includes = new List<Expression<Func<T, object>>>();

        }

        //GetById
        public BaseSpecifications(Expression<Func<T, bool>> criteriaExpression)
        {
            Criteria = criteriaExpression;
            // Includes = new List<Expression<Func<T, object>>>();
        }
    }
}
