using Ecommerce.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class SQLProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext context;

        public SQLProductRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public Product Add(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
            return product;
        }

        public Product Delete(int Id)
        {
            Product product = context.Products.Find(Id);
            if (product != null)
            {
                context.Products.Remove(product);
                context.SaveChanges();
            }
            return product;
        }

        public IEnumerable<Product> GetAllProducts()
        {           
            return context.Products.Include(c => c.ProductType).Include(c => c.SpecialTag).ToList();
        }

        public IEnumerable<Product> GetAllProducts(decimal? lowAmount, decimal? largeAmount)
        {
            var products = context.Products.Include(c => c.ProductType).Include(c => c.SpecialTag)
                .Where(c => c.Price >= lowAmount && c.Price <= largeAmount).ToList();
            if (lowAmount == null || largeAmount == null)
            {
                products = context.Products.Include(c => c.ProductType).Include(c => c.SpecialTag).ToList();
            }

            return products;
        }

        public Product GetProduct(int Id)
        {
            return context.Products.Include(c => c.ProductType).Include(c => c.SpecialTag)
                .FirstOrDefault(c => c.Id == Id);

        }

        public Product GetProduct(string  name)
        {
            return context.Products.FirstOrDefault(c => c.Name == name);

        }

        public Product Update(Product productChanges)
        {
            var product = context.Products.Attach(productChanges);
            product.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return productChanges;
        }
    }
}
