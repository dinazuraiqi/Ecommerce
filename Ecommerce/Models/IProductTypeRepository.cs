using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public interface IProductTypeRepository
    {
        ProductType GetProductType(int Id);
        IEnumerable<ProductType> GetAllProductTypes();
        ProductType Add(ProductType productType);
        ProductType Update(ProductType productTypeChanges);
        ProductType Delete(int Id);
    }
}
