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
        
        private ApplicationDbContext _db;
        private IProductTypesRepository _productTypesRepository;

        public ProductTypesController(ApplicationDbContext db, IProductTypesRepository productTypesRepository)
        {
            _db = db;
            _productTypesRepository = productTypesRepository;
        }

        public IActionResult Index()
        {
            return View(_productTypesRepository.GetAllProductTypes());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductTypes productTypes)
        {
            if (ModelState.IsValid)
            {                
                _productTypesRepository.Add(productTypes);
                TempData["save"] = "Product type has been saved";
                return RedirectToAction(nameof(Index));
            }

            return View(productTypes);
        }

        public ActionResult Edit(int id)
        {         
            var productType = _productTypesRepository.GetProductTypes(id);
            if (productType == null)
            {
                return NotFound();
            }
            return View(productType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductTypes productTypes)
        {
            if (ModelState.IsValid)
            {
                _productTypesRepository.Update(productTypes);
                TempData["edit"] = "Product type has been updated";
                return RedirectToAction(nameof(Index));
            }

            return View(productTypes);
        }

        public ActionResult Details(int id)
        {            
            var productType = _productTypesRepository.GetProductTypes(id);
            if (productType == null)
            {
                return NotFound();
            }
            return View(productType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(ProductTypes productTypes)
        {
            return RedirectToAction(nameof(Index));

        }

        public ActionResult Delete(int id)
        {            
            var productType = _productTypesRepository.GetProductTypes(id);
            if (productType == null)
            {
                return NotFound();
            }
            return View(productType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, ProductTypes productTypes)
        {            
            if (id != productTypes.Id)
            {
                return NotFound();
            }

            var productType = _productTypesRepository.GetProductTypes(id);
            if (productType == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _productTypesRepository.Delete(id);
                TempData["delete"] = "Product type has been deleted";
                return RedirectToAction(nameof(Index));
            }

            return View(productTypes);
        }



    }
}
