using Ecommerce.Interfaces;
using Ecommerce.Models;
using Ecommerce.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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

        private IOrderService _orderService;
        UserManager<IdentityUser> _userManager;
        public OrderController(IOrderDetailsRepository orderDetailsRepository,
            IOrderRepository orderRepository,
            IOrderService orderService,
            UserManager<IdentityUser> userManager)
        {
            _orderService = orderService;
            _userManager = userManager;
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
            order.UserId = _userManager.GetUserId(HttpContext.User);
            _orderService.Checkout(order, products);
            return RedirectToAction(nameof(Invoice),order);
        }
        
        

        public IActionResult Invoice(Order order)
        {
            return View(order);
        }

        public IActionResult OrdersList()
        {
            var orders = _orderService.GetOrders();
            return View(orders);
        }

    }
}
