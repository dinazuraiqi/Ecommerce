using Ecommerce.Data;
using Ecommerce.Interfaces;
using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Repositories
{
    public class SQLOrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext context;

        public SQLOrderRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<Result> Add(Order order)
        {
            Result result = new Result();
            try
            {
                Order orderExist = context.Orders.Find(order.Id);
                if (orderExist == null)
                {
                    context.Orders.Add(order);
                    await context.SaveChangesAsync();
                    result.Success = true;
                    result.ResultObject = order;
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
                Order order = context.Orders.Find(Id);
                if (order != null)
                {
                    result.ResultObject = order;
                    context.Orders.Remove(order);
                    await context.SaveChangesAsync();
                    result.Success = true;
                }
            }
            catch (Exception e)
            {
                result.ErrorMessage = e.Message;
            }

            return result; ;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return context.Orders;
        }

        public long GetMaxOrderNo()
        {
            try
            {
                var result = context.Orders.Max(o => o.OrderNo);
                return result;
            }
            catch(Exception e)
            {
                return 0;
            }
            
        }

        public Order GetOrder(int Id)
        {
            return context.Orders.Find(Id);
        }

        public async Task<Result> Update(Order orderChanges)
        {
            Result result = new Result();
            try
            {
                var order = context.Orders.Attach(orderChanges);
                if (order != null)
                {
                    order.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    await context.SaveChangesAsync();
                    result.Success = true;
                    result.ResultObject = orderChanges;
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
