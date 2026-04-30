using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V7.Domain.Entites;

namespace V7.Domain.Interfaces.Specifications
{
    public class CategoryWithProductsSpecification : BaseSpecifications<Category>
    {
        public CategoryWithProductsSpecification() : base()
        {
            Includes.Add(c => c.Product);
        }
        public CategoryWithProductsSpecification(int id) : base(c => c.Id == id)
        {
            Includes.Add(c => c.Product);
        }
    }
}
