using Ecommerce.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class SQLOrderDetailsRepository : IOrderDetailsRepository
    {
        private readonly ApplicationDbContext context;

        public SQLOrderDetailsRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public OrderDetails Add(OrderDetails orderDetails)
        {
            context.OrderDetails.Add(orderDetails);
            context.SaveChanges();
            return orderDetails;
        }

        public OrderDetails Delete(int Id)
        {
            OrderDetails orderDetails = context.OrderDetails.Find(Id);
            if (orderDetails != null)
            {
                context.OrderDetails.Remove(orderDetails);
                context.SaveChanges();
            }
            return orderDetails;
        }

        public IEnumerable<OrderDetails> GetAllOrdersDetails()
        {
            return context.OrderDetails;
        }

        public OrderDetails GetOrderDEtails(int Id)
        {
            return context.OrderDetails.Find(Id);
        }

        public OrderDetails Update(OrderDetails orderDetailsChanges)
        {
            var orderDetails = context.OrderDetails.Attach(orderDetailsChanges);
            orderDetails.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return orderDetailsChanges;
        }
    }
}
