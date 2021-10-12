using Ecommerce.Areas.Admin.Models;
using Ecommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleController : Controller
    {
        IRoleRepository _roleRepository;
        IApplicationUserRepository _applicationUserRepository;

        public RoleController(IRoleRepository roleRepository, IApplicationUserRepository applicationUserRepository)
        {
            _roleRepository = roleRepository;
            _applicationUserRepository = applicationUserRepository;
        }
        public IActionResult Index()
        {
            var roles = _roleRepository.GetAllRoles();
            ViewBag.Roles = roles;
            return View();
        }

        public async Task<IActionResult> Create()
        {
            return await Task.FromResult(View());
        }


        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {            
            var result = await _roleRepository.Add(name);
            if (result.ResultObject == null)
            {
                ViewBag.mgs = "This role is aldeady exist";
                ViewBag.name = name;
                return View();
            }           
            if (result.Success)
            {
                TempData["save"] = "Role has been saved successfully";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public async Task<IActionResult> Edit(string id)
        {
            var role = await _roleRepository.GetRole(id);
            if (role == null)
            {
                return NotFound();
            }
            ViewBag.id = role.Id;
            ViewBag.name = role.Name;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, string name)
        {

            var result = await _roleRepository.Update(id,name);
           
            if (result.ResultObject == null) 
            {
                return NotFound();
            }            
            else if (result.ResultObject != null && result.Success == false)
            {
                ViewBag.mgs = "This role is aldeady exist";
                ViewBag.id = id;
                ViewBag.name = name;
                return View();
            }           
            else if (result.Success)
            {
                TempData["save"] = "Role has been updated successfully";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleRepository.GetRole(id);
            if (role == null)
            {
                return NotFound();
            }
            ViewBag.id = role.Id;
            ViewBag.name = role.Name;
            return View();
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(string id)
        {
            var result = await _roleRepository.Delete(id);
            if (result.ResultObject == null)
            {
                return NotFound();
            }
            
            if (result.Success)
            {
                TempData["delete"] = "Role has been deleted successfully";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public async Task<IActionResult> Assign()
        {

            ViewData["UserId"] = new SelectList(_applicationUserRepository.GetActiveUsers(), "Id", "UserName");
            ViewData["RoleId"] = new SelectList(_roleRepository.GetAllRoles(), "Name", "Name");
            return await Task.FromResult(View());
        }

        [HttpPost]
        public async Task<IActionResult> Assign(RoleUserVm roleUser)
        {
            var result  =await _roleRepository.Assign(roleUser);

            if (result.ResultObject == null)
            {
                return NotFound();
            }
            else if (result.ResultObject != null && result.Success == false)
            {
                ViewBag.mgs = "This user already assign this role.";
                ViewData["UserId"] = new SelectList(_applicationUserRepository.GetActiveUsers(), "Id", "UserName");
                ViewData["RoleId"] = new SelectList(_roleRepository.GetAllRoles(), "Name", "Name");
                return View();
            }
            else if (result.Success)
            {
                TempData["save"] = "User Role assigned.";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public ActionResult AssignUserRole()
        {
            var result = _roleRepository.GetUserRole();
            ViewBag.UserRoles = result;
            return View();
        }

    }
}
