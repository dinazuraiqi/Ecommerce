using Ecommerce.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class SQLProductTypeRepository : IProductTypeRepository
    {
        private readonly ApplicationDbContext context;

        public SQLProductTypeRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public ProductType Add(ProductType productType)
        {
            context.ProductTypes.Add(productType);
            context.SaveChanges();
            return productType;
        }

        public ProductType Delete(int Id)
        {
            ProductType productType = context.ProductTypes.Find(Id);
            if (productType != null)
            {
                context.ProductTypes.Remove(productType);
                context.SaveChanges();
            }
            return productType;
        }

        public IEnumerable<ProductType> GetAllProductTypes()
        {
            return context.ProductTypes;
        }

        public ProductType GetProductType(int Id)
        {
            return context.ProductTypes.Find(Id);
        }

        public ProductType Update(ProductType productTypeChanges)
        {
            var productType = context.ProductTypes.Attach(productTypeChanges);
            productType.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return productTypeChanges;
        }
    }
}
