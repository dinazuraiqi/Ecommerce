using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public interface IOrderRepository
    {
        Order GetOrder(int Id);
        IEnumerable<Order> GetAllOrders();
        Order Add(Order order);
        Order Update(Order orderChanges);
        Order Delete(int Id);
    }
}
