using Ecommerce.Areas.Admin.Models;
using Ecommerce.Models;
using Ecommerce.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace Ecommerce.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IProductRepository _productRepository;        
        public HomeController(ILogger<HomeController> logger, IProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;           
        }

        public IActionResult Index(int? page)
        {
            return View(_productRepository.GetAllProducts().ToPagedList(page ?? 1, 9));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public ActionResult Detail(int id)
        {          
            var product = _productRepository.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ActionName("Detail")]
        public ActionResult ProductDetail(int id)
        {
            List<Product> products = new List<Product>();            
            var product = _productRepository.GetProduct(id);
            product.Quantity = 1;
            if (product == null)
            {
                return NotFound();
            }

            products = HttpContext.Session.Get<List<Product>>("products");
            if (products == null)
            {
                products = new List<Product>();
            }
            products.Add(product);
            HttpContext.Session.Set("products", products);
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public ActionResult Remove(int id)
        {            
            var products = HttpContext.Session.Get<List<Product>>("products");
            if (products != null)
            {
                var product = products.FirstOrDefault(p => p.Id == id);
                if (product != null)
                {
                    products.Remove(product);
                    HttpContext.Session.Set("products", products);
                }
            }          
           
            return RedirectToAction(nameof(Index));
        }

        [ActionName("Remove")]
        public IActionResult RemoveFromCart(int? id)
        {
            List<Product> products = HttpContext.Session.Get<List<Product>>("products");
            if (products != null)
            {
                var product = products.FirstOrDefault(p => p.Id == id);
                if (product != null)
                {
                    products.Remove(product);
                    HttpContext.Session.Set("products", products);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Cart()
        {
            List<Product> products = HttpContext.Session.Get<List<Product>>("products");
            if (products != null)
            {
                foreach (var product in products)
                {
                    product.TotalPrice = product.Price;
                    if (product.Quantity != 1)
                    {
                        product.Price = product.TotalPrice / product.Quantity;
                    }
                }
            }                
            else 
            {
                products = new List<Product>();
            }
            return View(products);
        }

        [HttpPost]       
        public IActionResult Cart(List<Product> products)
        {
            foreach(var product in products)
            {
                product.Price = product.Quantity * product.Price;
            }
            HttpContext.Session.Set("products", products);
            return RedirectToAction("Checkout", "Order"); 
        }

    }
}
