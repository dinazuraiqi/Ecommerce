using Ecommerce.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class SQLProductTypesRepository : IProductTypesRepository
    {
        private readonly ApplicationDbContext context;

        public SQLProductTypesRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public ProductTypes Add(ProductTypes productTypes)
        {
            context.ProductTypes.Add(productTypes);
            context.SaveChanges();
            return productTypes;
        }

        public ProductTypes Delete(int Id)
        {
            ProductTypes productTypes = context.ProductTypes.Find(Id);
            if (productTypes != null)
            {
                context.ProductTypes.Remove(productTypes);
                context.SaveChanges();
            }
            return productTypes;
        }

        public IEnumerable<ProductTypes> GetAllProductTypes()
        {
            return context.ProductTypes;
        }

        public ProductTypes GetProductTypes(int Id)
        {
            return context.ProductTypes.Find(Id);
        }

        public ProductTypes Update(ProductTypes productTypesChanges)
        {
            var productTypes = context.ProductTypes.Attach(productTypesChanges);
            productTypes.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return productTypesChanges;
        }
    }
}
