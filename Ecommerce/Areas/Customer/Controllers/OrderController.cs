using Ecommerce.Models;
using Ecommerce.Utility;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class OrderController : Controller
    {
        private IOrderDetailsRepository _orderDetailsRepository;
        private IOrderRepository _orderRepository;

        public OrderController(IOrderDetailsRepository orderDetailsRepository,
            IOrderRepository orderRepository)
        {
            _orderDetailsRepository = orderDetailsRepository;
            _orderRepository = orderRepository;
        }
        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Checkout(Order order)
        {
            List<Product> products = HttpContext.Session.Get<List<Product>>("products");
            if (products != null)
            {
                foreach (var product in products)
                {
                    OrderDetails orderDetails = new OrderDetails();
                    orderDetails.PorductId = product.Id;
                    order.OrderDetails.Add(orderDetails);
                }
            }

            order.OrderNo = GetOrderNo();
            _orderRepository.Add(order);
           
            HttpContext.Session.Set("products", new List<Product>());
            return View();
        }
        
        public string GetOrderNo()
        {
            int rowCount = _orderRepository.GetAllOrders().ToList().Count() + 1;
            return rowCount.ToString("000");
        }

    }
}
