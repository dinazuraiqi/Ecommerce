using Ecommerce.Models;
using System.Collections.Generic;

namespace Ecommerce.Interfaces
{
    public interface IOrderService
    {
       void Checkout(Order order, List<Product> products);
        List<Order> GetOrders();
    }
}
