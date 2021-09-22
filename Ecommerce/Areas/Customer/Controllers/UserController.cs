using Ecommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class UserController : Controller
    {
        UserManager<IdentityUser> _userManager;
        IApplicationUserRepository _applicationUserRepository;
        public UserController(UserManager<IdentityUser> userManager, IApplicationUserRepository applicationUserRepository)
        {
            _userManager = userManager;
            _applicationUserRepository = applicationUserRepository;
        }
        public IActionResult Index()
        {
            var dd = _userManager.GetUserId(HttpContext.User);
            return View(_applicationUserRepository.GetAllUsers().ToList());
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ApplicationUser user)
        {
            if (ModelState.IsValid)
            {
                var result = await _applicationUserRepository.Add(user);
                if (result.Succeeded)
                {
                    TempData["save"] = "User has been created successfully";
                    return RedirectToAction(nameof(Index));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }


            return View();
        }

        public async Task<IActionResult> Edit(string id)
        {
            var user = _applicationUserRepository.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ApplicationUser user)
        {
            var result = await _applicationUserRepository.Update(user);
            if (result == null)
            {
                return NotFound();
            }                      
            if (result.Succeeded)
            {
                TempData["save"] = "User has been updated successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        public async Task<IActionResult> Active(string id)
        {
            var user = _applicationUserRepository.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Active(ApplicationUser user)
        {
            var result = _applicationUserRepository.Active(user);
            if (result.ResultObject == null)
            {
                return NotFound();

            }            
            if (result.Success == true)
            {
                TempData["save"] = "User has been active successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(result.ResultObject);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var user = _applicationUserRepository.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ApplicationUser user)
        {
            var result = _applicationUserRepository.Delete(user);
            if (result.ResultObject == null)
            {
                return NotFound();

            }           
            
            if (result.Success == true)
            {
                TempData["save"] = "User has been delete successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(result.ResultObject);
        }

        public async Task<IActionResult> Details(string id)
        {
            var user = _applicationUserRepository.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        public async Task<IActionResult> Locout(string id)
        {           
            var user = _applicationUserRepository.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Locout(ApplicationUser user)
        {
            var result = _applicationUserRepository.Locout(user);
            if (result.ResultObject == null)
            {
                return NotFound();

            }            
            if (result.Success == true)
            {
                TempData["save"] = "User has been lockout successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(result.ResultObject);
        }

    }
}
