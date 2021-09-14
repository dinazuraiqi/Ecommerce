using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SpecialTagController : Controller
    {
        private ISpecialTagRepository _specialTagRepository;
        public SpecialTagController(ISpecialTagRepository specialTagRepository)
        {
            _specialTagRepository = specialTagRepository;
        }
        public IActionResult Index()
        {
            return View(_specialTagRepository.GetAllSpecialTags());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SpecialTag specialTag)
        {
            if (ModelState.IsValid)
            {
                _specialTagRepository.Add(specialTag);
                TempData["save"] = "Tag has been saved";
                return RedirectToAction(nameof(Index));
            }

            return View(specialTag);
        }

        public ActionResult Edit(int id)
        {
            var specialTag = _specialTagRepository.GetSpecialTag(id);
            if (specialTag == null)
            {
                return NotFound();
            }
            return View(specialTag);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SpecialTag specialTag)
        {
            if (ModelState.IsValid)
            {
                _specialTagRepository.Update(specialTag);
                TempData["edit"] = "Tag has been updated";
                return RedirectToAction(nameof(Index));
            }

            return View(specialTag);
        }

        public ActionResult Details(int id)
        {
            var specialTag = _specialTagRepository.GetSpecialTag(id);
            if (specialTag == null)
            {
                return NotFound();
            }
            return View(specialTag);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(SpecialTag specialTag)
        {
            return RedirectToAction(nameof(Index));

        }

        public ActionResult Delete(int id)
        {
            var specialTag = _specialTagRepository.GetSpecialTag(id);
            if (specialTag == null)
            {
                return NotFound();
            }
            return View(specialTag);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, SpecialTag specialTag)
        {
            if (id != specialTag.Id)
            {
                return NotFound();
            }

           specialTag = _specialTagRepository.GetSpecialTag(id);
            if (specialTag == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _specialTagRepository.Delete(id);
                TempData["delete"] = "Tag has been deleted";
                return RedirectToAction(nameof(Index));
            }

            return View(specialTag);
        }

    }
}
