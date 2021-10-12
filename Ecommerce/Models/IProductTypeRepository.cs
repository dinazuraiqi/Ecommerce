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
        Task<Result> Add(ProductType productType);
        Task<Result> Update(ProductType productTypeChanges);
        Task<Result> Delete(int Id);
    }
}
