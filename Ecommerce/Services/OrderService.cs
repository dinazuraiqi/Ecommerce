using Ecommerce.Interfaces;
using Ecommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;

namespace Ecommerce.Services
{
    public class OrderService : IOrderService
    {
        private IOrderDetailsRepository _orderDetailsRepository;
        private IOrderRepository _orderRepository;
        
        public OrderService(IOrderDetailsRepository orderDetailsRepository, IOrderRepository orderRepository)
        {
            _orderDetailsRepository = orderDetailsRepository;
            _orderRepository = orderRepository;
        }

        public async void  Checkout(Order order, List<Product> products)
        {            
            decimal total = 0;
            if (products != null)
            {

                foreach (var product in products)
                {
                    OrderDetails orderDetails = new OrderDetails();
                    orderDetails.PorductId = product.Id;
                    orderDetails.Quantity = product.Quantity;
                    orderDetails.Price = product.Price;
                    total += orderDetails.Price;
                    order.OrderDetails.Add(orderDetails);
                }
            }

            order.OrderNo = GetOrderNo();
            order.TotalPrice = total;
           await _orderRepository.Add(order);
        }

        public List<Order> GetOrders()
        {
            return _orderRepository.GetAllOrders().ToList();
        }

        long GetOrderNo()
        {
            long rowCount = _orderRepository.GetMaxOrderNo() + 1;
            return rowCount;
        }
    }
}
