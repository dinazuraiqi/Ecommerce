using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public interface IProductRepository
    {
        Product GetProduct(int Id);
        Product GetProduct(string name);
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetAllProducts(decimal? lowAmount, decimal? largeAmount);
        Product Add(Product product);
        Product Update(Product productChanges);
        Product Delete(int Id);
    }
}
