using Ecommerce.Models;
using Ecommerce.Utility;
using Microsoft.AspNetCore.Authorization;
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
            
            return RedirectToAction(nameof(Invoice),order);
        }
        
        public string GetOrderNo()
        {
            int rowCount = _orderRepository.GetAllOrders().ToList().Count() + 1;
            return rowCount.ToString("000");
        }

        public IActionResult Invoice(Order order)
        {
            return View(order);
        }

    }
}
