using Ecommerce.Data;
using Ecommerce.Interfaces;
using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Repositories
{
    public class SQLOrderDetailsRepository : IOrderDetailsRepository
    {
        private readonly ApplicationDbContext context;

        public SQLOrderDetailsRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<Result> Add(OrderDetails orderDetails)
        {
            Result result = new Result();
            try
            {
                OrderDetails orderDetailsExist = context.OrderDetails.Find(orderDetails.Id);
                if (orderDetailsExist == null)
                {
                    context.OrderDetails.Add(orderDetails);
                    await context.SaveChangesAsync();
                    result.Success = true;
                    result.ResultObject = orderDetails;
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
                OrderDetails orderDetails = context.OrderDetails.Find(Id);
                if (orderDetails != null)
                {
                    result.ResultObject = orderDetails;
                    context.OrderDetails.Remove(orderDetails);
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

        public IEnumerable<OrderDetails> GetAllOrdersDetails()
        {
            return context.OrderDetails;
        }

        public OrderDetails GetOrderDEtails(int Id)
        {
            return context.OrderDetails.Find(Id);
        }

        public async Task<Result> Update(OrderDetails orderDetailsChanges)
        {
            Result result = new Result();
            try
            {
                var orderDetails = context.OrderDetails.Attach(orderDetailsChanges);
                if (orderDetails != null)
                {
                    orderDetails.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    await context.SaveChangesAsync();
                    result.Success = true;
                    result.ResultObject = orderDetailsChanges;
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
