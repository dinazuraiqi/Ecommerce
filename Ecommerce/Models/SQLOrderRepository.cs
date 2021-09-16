using Ecommerce.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class SQLOrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext context;

        public SQLOrderRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public Order Add(Order order)
        {
            context.Orders.Add(order);
            context.SaveChanges();
            return order;
        }

        public Order Delete(int Id)
        {
            Order order = context.Orders.Find(Id);
            if (order != null)
            {
                context.Orders.Remove(order);
                context.SaveChanges();
            }
            return order;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return context.Orders;
        }

        public Order GetOrder(int Id)
        {
            return context.Orders.Find(Id);
        }

        public Order Update(Order orderChanges)
        {
            var order = context.Orders.Attach(orderChanges);
            order.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return orderChanges;
        }
    }
}
