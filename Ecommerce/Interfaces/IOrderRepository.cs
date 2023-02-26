using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Models;

namespace Ecommerce.Interfaces
{
    public interface IOrderRepository
    {
        Order GetOrder(int Id);
        IEnumerable<Order> GetAllOrders();
        Task<Result> Add(Order order);
        Task<Result> Update(Order orderChanges);
        Task<Result> Delete(int Id);
        long GetMaxOrderNo();

    }
}
