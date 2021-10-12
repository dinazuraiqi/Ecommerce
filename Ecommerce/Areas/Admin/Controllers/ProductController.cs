using Ecommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]    
    public class ProductController : Controller
    {
        
        private IWebHostEnvironment _he;
        private IProductRepository _productRepository;
        private IProductTypeRepository _productTypeRepository;
        private ISpecialTagRepository _specialTagRepository;

        public ProductController(IProductRepository productRepository, IWebHostEnvironment he,
            IProductTypeRepository productTypeRepository, ISpecialTagRepository specialTagRepository)
        {
            _productRepository = productRepository;
            _productTypeRepository = productTypeRepository;
            _specialTagRepository = specialTagRepository;
            _he = he;
        }
                
        public IActionResult Index()
        {
            return View(_productRepository.GetAllProducts());
        }

        [HttpPost]
        public IActionResult Index(decimal? lowAmount, decimal? largeAmount)

        {
            return View(_productRepository.GetAllProducts(lowAmount, largeAmount));
        }

        public IActionResult Create()
        {
            ViewData["productTypeId"] = new SelectList(_productTypeRepository.GetAllProductTypes(), "Id", "Type");
            ViewData["TagId"] = new SelectList(_specialTagRepository.GetAllSpecialTags(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                var searchProduct = _productRepository.GetProduct(product.Name);
                if (searchProduct != null)
                {
                    ViewBag.message = "This product is already exist";
                    ViewData["productTypeId"] = new SelectList(_productTypeRepository.GetAllProductTypes(), "Id", "Type");
                    ViewData["TagId"] = new SelectList(_specialTagRepository.GetAllSpecialTags(), "Id", "Name");
                    return View(product);
                }

                if (image != null)
                {
                    var name = Path.Combine(_he.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    product.Image = "Images/" + image.FileName;
                }

                if (image == null)
                {
                    product.Image = "Images/noimage.PNG";
                }
                var result =await _productRepository.Add(product);
                if (result.Success == true)
                {
                    TempData["save"] = "Product has been saved";
                    return RedirectToAction(nameof(Index));
                }                
            }

            return View(product);
        }

        public ActionResult Edit(int id)
        {                       
            var product = _productRepository.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }

            ViewData["productTypeId"] = new SelectList(_productTypeRepository.GetAllProductTypes(), "Id", "Type");
            ViewData["TagId"] = new SelectList(_specialTagRepository.GetAllSpecialTags(), "Id", "Name");
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product product, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    var name = Path.Combine(_he.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    product.Image = "Images/" + image.FileName;
                }

                if (image == null)
                {
                    product.Image = "Images/noimage.PNG";
                }
                var result = await _productRepository.Update(product);
                if (result.Success == true)
                {
                    TempData["edit"] = "Product has been updated";
                    return RedirectToAction(nameof(Index));
                }
                
            }

            return View(product);
        }

        public ActionResult Details(int id)
        {
            ViewData["productTypeId"] = new SelectList(_productTypeRepository.GetAllProductTypes(), "Id", "Type");
            ViewData["TagId"] = new SelectList(_specialTagRepository.GetAllSpecialTags(), "Id", "Name");
            var product = _productRepository.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        public ActionResult Delete(int id)
        {           
            var product = _productRepository.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["productTypeId"] = new SelectList(_productTypeRepository.GetAllProductTypes(), "Id", "Type");
            ViewData["TagId"] = new SelectList(_specialTagRepository.GetAllSpecialTags(), "Id", "Name");
            return View(product);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var result =await _productRepository.Delete(id);
            if (result.ResultObject == null)
            {
                return NotFound();
            }

            if (result.Success == true)
            {
                TempData["delete"] = "Product has been deleted";
                return RedirectToAction(nameof(Index));
            }
            return View(result.ResultObject);
        }


    }
}
