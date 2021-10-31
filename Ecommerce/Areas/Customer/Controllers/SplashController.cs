using Ecommerce.Areas.Admin.Models;
using Ecommerce.Data;
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
    public class SplashController : Controller
    {
        static IRoleRepository _roleRepository;
        static IApplicationUserRepository _applicationUserRepository;
        static UserManager<IdentityUser> _userManager;        
        public SplashController(IRoleRepository roleRepository, IApplicationUserRepository applicationUserRepository,
            UserManager<IdentityUser> userManager)
        {
            _roleRepository = roleRepository;
            _applicationUserRepository = applicationUserRepository;
            _userManager = userManager;            
        }
        public async Task<IActionResult> Index()
        {
            var roles = _roleRepository.GetAllRoles();           
            if (roles.Count<IdentityRole>() == 0)
            {
                await _roleRepository.Add("Admin");
                await _roleRepository.Add("Super user");
                await _roleRepository.Add("User");
            }

            var user = _applicationUserRepository.GetUserByName("Admin@Admin.com");            
            if (user == null)
            {
                user = new ApplicationUser();
                user.UserName = "Admin@Admin.com";
                user.PasswordHash = "P@ssword.Ecommerce2021";

                await _applicationUserRepository.Add(user);
                
                await  _userManager.AddToRoleAsync(user, "Admin");
            }

            return RedirectToAction("Index","Home");
        }
    }
}
