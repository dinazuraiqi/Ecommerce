using Ecommerce.Data;
using Ecommerce.Interfaces;
using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Repositories
{
    public class SQLProductTypeRepository : IProductTypeRepository
    {
        private readonly ApplicationDbContext context;

        public SQLProductTypeRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<Result> Add(ProductType productType)
        {
            Result result = new Result();
            try
            {
                ProductType productTypeExist = context.ProductTypes.Find(productType.Id);
                if (productTypeExist == null)
                {
                    context.ProductTypes.Add(productType);
                    await context.SaveChangesAsync();
                    result.Success = true;
                    result.ResultObject = productType;
                }
            }
            catch (Exception e)
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
                ProductType productType = context.ProductTypes.Find(Id);
                if (productType != null)
                {
                    result.ResultObject = productType;
                    context.ProductTypes.Remove(productType);
                    await context.SaveChangesAsync();
                    result.Success = true;
                }
            }
            catch (Exception e)
            {
                result.ErrorMessage = e.Message;
            }
            return result;
        }

        public IEnumerable<ProductType> GetAllProductTypes()
        {
            return context.ProductTypes;
        }

        public ProductType GetProductType(int Id)
        {
            return context.ProductTypes.Find(Id);
        }

        public async Task<Result> Update(ProductType productTypeChanges)
        {
            Result result = new Result();
            try
            {
                var productType = context.ProductTypes.Attach(productTypeChanges);
                if (productType != null)
                {
                    productType.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    await context.SaveChangesAsync();
                    result.Success = true;
                    result.ResultObject = productTypeChanges;
                }
            }
            catch (Exception e)
            {
                result.ErrorMessage = e.Message;
            }
            return result;
        }
    }
}
