using Ecommerce.Data;
using Ecommerce.Interfaces;
using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductTypeController : Controller
    {       
        
        private IProductTypeRepository _productTypeRepository;

        public ProductTypeController(IProductTypeRepository productTypeRepository)
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
                var result =await _productTypeRepository.Add(productType);
                if (result.Success == true)
                {
                    TempData["save"] = "Product type has been saved";
                    return RedirectToAction(nameof(Index));
                }                
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
                var result = await _productTypeRepository.Update(productType);
                if (result.Success == true)
                {
                    TempData["edit"] = "Product type has been updated";
                    return RedirectToAction(nameof(Index));
                }               
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
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var result = await _productTypeRepository.Delete(id);
            if (result.ResultObject == null)
            {
                return NotFound();
            }
            if (result.Success == true)
            {
                TempData["delete"] = "Product type has been deleted";
                return RedirectToAction(nameof(Index));
            }

            return View(result.ResultObject);

        }



    }
}
