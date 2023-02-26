using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Models;

namespace Ecommerce.Interfaces
{
    public interface IProductRepository
    {
        Product GetProduct(int Id);
        Product GetProduct(string name);
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetAllProducts(decimal? lowAmount, decimal? largeAmount);
        Task<Result> Add(Product product);
        Task<Result> Update(Product productChanges);
        Task<Result> Delete(int Id);
    }
}
