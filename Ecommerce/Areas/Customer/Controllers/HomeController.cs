﻿using Ecommerce.Models;
using Ecommerce.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;


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

        public IActionResult Index()
        {
            return View(_productRepository.GetAllProducts());
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

        public IActionResult Cart()
        {
            List<Product> products = HttpContext.Session.Get<List<Product>>("products");
            if (products == null)
            {
                products = new List<Product>();
            }
            return View(products);
        }

    }
}
