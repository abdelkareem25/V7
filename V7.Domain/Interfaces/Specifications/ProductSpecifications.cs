using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using V7.Domain.Entites;

namespace V7.Domain.Interfaces.Specifications
{
    public class ProductSpecifications : BaseSpecifications<Product>
    {
        public ProductSpecifications():base()
        {
            Includes.Add(p => p.Category);
        }
        // byID
        public ProductSpecifications(int id) :base(p => p.Id == id)
        {
            Includes.Add(p => p.Category);
        }
    }
}
