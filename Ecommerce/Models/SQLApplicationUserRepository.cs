using Ecommerce.Areas.Identity.Pages.Account;
using Ecommerce.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Ecommerce.Models
{
    public class SQLApplicationUserRepository : IApplicationUserRepository
    {
        private readonly ApplicationDbContext context;
        UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly SignInManager<IdentityUser> _signInManager;
        public SQLApplicationUserRepository(UserManager<IdentityUser> userManager,
            ApplicationDbContext context,
            ILogger<RegisterModel> logger,
            SignInManager<IdentityUser> signInManager)
        {
            this.context = context;
            _userManager = userManager;
            _logger = logger;
            _signInManager = signInManager;
        }
        public Result Active(ApplicationUser user)
        {
            Result result = new Result();
            try
            {
                ApplicationUser userToActive = context.ApplicationUsers.Find(user.Id);
                if (userToActive != null)
                {
                    result.ResultObject = userToActive;
                    userToActive.LockoutEnd = DateTime.Now.AddDays(-1);
                    context.SaveChanges();
                    result.Success = true;
                }
            }
            catch(Exception e)
            {
                result.ErrorMessage = e.Message;
            }
                                                  
            return result;
        }
        
        public async Task<IdentityResult> Add(ApplicationUser user)
        {
            IdentityResult result = new IdentityResult();
            user.Email = user.UserName;
            result = await _userManager.CreateAsync(user, user.PasswordHash);
            if (result.Succeeded && user.UserName != "Admin@Admin.com")
            {                
                await _userManager.AddToRoleAsync(user, "User");                
            }
            if (result.Succeeded)
            {
                _logger.LogInformation("User created a new account with password.");                            

                await _signInManager.SignInAsync(user, isPersistent: false);
            }
            return result;
        }

        public Result Delete(ApplicationUser user)
        {
            Result result = new Result();
            try
            {
                ApplicationUser userToDelete = context.ApplicationUsers.Find(user.Id);
                if (userToDelete != null)
                {
                    result.ResultObject = userToDelete;
                    context.ApplicationUsers.Remove(userToDelete);
                    context.SaveChanges();
                    result.Success = true;
                }                
            }
            catch (Exception e)
            {
                result.ErrorMessage = e.Message;
            }
            
            return result;
        }

        public IEnumerable<ApplicationUser> GetAllUsers()
        {
            return context.ApplicationUsers;
        }

        public IEnumerable<ApplicationUser> GetActiveUsers()
        {
            return context.ApplicationUsers.Where(f => f.LockoutEnd < DateTime.Now || f.LockoutEnd == null).ToList();
        }

        public ApplicationUser GetUser(string Id)
        {
            return context.ApplicationUsers.Find(Id);
        }

        public ApplicationUser GetUserByName(string name)
        {
            return context.ApplicationUsers.FirstOrDefault(c => c.UserName == name);
        }

        public Result Locout(ApplicationUser user)
        {
            Result result = new Result();
            try
            {
                ApplicationUser userToActive = context.ApplicationUsers.Find(user.Id);
                if (userToActive != null)
                {
                    result.ResultObject = userToActive;
                    userToActive.LockoutEnd = DateTime.Now.AddYears(100);
                    context.SaveChanges();
                    result.Success = true;
                }
            }
            catch (Exception e)
            {
                result.ErrorMessage = e.Message;
            }

            return result;
        }

        public async Task<IdentityResult> Update(ApplicationUser userChanges)
        {
            IdentityResult result = null;
            ApplicationUser userToUpdate = context.ApplicationUsers.Find(userChanges.Id);
            if(userToUpdate !=null)
            {                 
                userToUpdate.FirstName = userChanges.FirstName;
                userToUpdate.LastName = userChanges.LastName;
                if (userToUpdate != null)
                {
                    result = await _userManager.UpdateAsync(userToUpdate);
                }
            }          
            return result;
        }
    }
}
