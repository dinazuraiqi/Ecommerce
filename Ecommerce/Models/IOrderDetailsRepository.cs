using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    interface IOrderDetailsRepository
    {
        OrderDetails GetOrderDEtails(int Id);
        IEnumerable<OrderDetails> GetAllOrdersDetails();
        OrderDetails Add(OrderDetails orderDetails);
        OrderDetails Update(OrderDetails orderDetailsChanges);
        OrderDetails Delete(int Id);
    }
}
