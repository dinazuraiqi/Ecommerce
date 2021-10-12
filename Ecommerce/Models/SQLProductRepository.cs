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
        public async Task<Result> Add(Product product)
        {
            Result result = new Result();
            try
            {
                var searchProduct = context.Products.FirstOrDefault(p => p.Name == product.Name);
                if (searchProduct == null)
                {                   
                    context.Products.Add(product);
                    await context.SaveChangesAsync();
                    result.Success = true;
                    result.ResultObject = product;
                }                               
            }
            catch(Exception e)
            {
                result.ErrorMessage = e.Message;
            }
           
            return result;
        }

        public async Task<Result> Delete(int Id)
        {
            Result result = new Result();
            try
            {
                Product product = context.Products.Find(Id);
                if (product != null)
                {
                    result.ResultObject = product;
                    context.Products.Remove(product);
                    await context.SaveChangesAsync();
                    result.Success = true;                    
                }
            }
            catch(Exception e)
            {
                result.ErrorMessage = e.Message;
            }
            
            return result;
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

        public async Task<Result> Update(Product productChanges)
        {
            Result result = new Result();
            try
            {
                var product = context.Products.Attach(productChanges);
                if (product != null)
                {
                    result.ResultObject = productChanges;                                     
                    product.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    await context.SaveChangesAsync();
                    result.Success = true;
                }
            }
            catch(Exception e)
            {
                result.ErrorMessage = e.Message;
            }
            
            return result;
        }
    }
}
