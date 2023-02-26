using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Models;

namespace Ecommerce.Interfaces
{
    public interface IOrderDetailsRepository
    {
        OrderDetails GetOrderDEtails(int Id);
        IEnumerable<OrderDetails> GetAllOrdersDetails();
        Task<Result> Add(OrderDetails orderDetails);
        Task<Result> Update(OrderDetails orderDetailsChanges);
        Task<Result> Delete(int Id);
    }
}
