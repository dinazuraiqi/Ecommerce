using Ecommerce.Data;
using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductTypesController : Controller
    {       
        
        private IProductTypeRepository _productTypeRepository;

        public ProductTypesController(IProductTypeRepository productTypeRepository)
        {           
            _productTypeRepository = productTypeRepository;
        }

        public IActionResult Index()
        {
            return View(_productTypeRepository.GetAllProductTypes());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductType productType)
        {
            if (ModelState.IsValid)
            {                
                _productTypeRepository.Add(productType);
                TempData["save"] = "Product type has been saved";
                return RedirectToAction(nameof(Index));
            }

            return View(productType);
        }

        public ActionResult Edit(int id)
        {         
            var productType = _productTypeRepository.GetProductType(id);
            if (productType == null)
            {
                return NotFound();
            }
            return View(productType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductType productType)
        {
            if (ModelState.IsValid)
            {
                _productTypeRepository.Update(productType);
                TempData["edit"] = "Product type has been updated";
                return RedirectToAction(nameof(Index));
            }

            return View(productType);
        }

        public ActionResult Details(int id)
        {            
            var productType = _productTypeRepository.GetProductType(id);
            if (productType == null)
            {
                return NotFound();
            }
            return View(productType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(ProductType productType)
        {
            return RedirectToAction(nameof(Index));

        }

        public ActionResult Delete(int id)
        {            
            var productType = _productTypeRepository.GetProductType(id);
            if (productType == null)
            {
                return NotFound();
            }
            return View(productType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, ProductType productType)
        {            
            if (id != productType.Id)
            {
                return NotFound();
            }

            productType = _productTypeRepository.GetProductType(id);
            if (productType == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _productTypeRepository.Delete(id);
                TempData["delete"] = "Product type has been deleted";
                return RedirectToAction(nameof(Index));
            }

            return View(productType);
        }



    }
}
