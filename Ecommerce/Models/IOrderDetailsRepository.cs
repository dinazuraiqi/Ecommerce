using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models
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
