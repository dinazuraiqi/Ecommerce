using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public interface IProductTypesRepository
    {
        ProductTypes GetProductTypes(int Id);
        IEnumerable<ProductTypes> GetAllProductTypes();
        ProductTypes Add(ProductTypes productTypes);
        ProductTypes Update(ProductTypes productTypesChanges);
        ProductTypes Delete(int Id);
    }
}
