using Ecommerce.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class SQLApplicationUserRepository : IApplicationUserRepository
    {
        private readonly ApplicationDbContext context;
        UserManager<IdentityUser> _userManager;
        public SQLApplicationUserRepository(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            this.context = context;
            _userManager = userManager;
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
                    int rowAffected = context.SaveChanges();
                    if (rowAffected > 0)
                    {
                        result.Success = true;                        
                    }
                }
            }
            catch(Exception e)
            {
                result.Error = e.Message;
            }
                                                  
            return result;
        }
        
        public async Task<IdentityResult> Add(ApplicationUser user)
        {
            IdentityResult result = new IdentityResult();
            ApplicationUser userToAdd = context.ApplicationUsers.Find(user.Id);
            if (userToAdd != null)
            {
                result = await _userManager.CreateAsync(user, user.PasswordHash);
                if (result.Succeeded)
                {
                    var isSaveRole = await _userManager.AddToRoleAsync(user, "User");
                }
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
                    context.ApplicationUsers.Remove(user);
                    int rowAffected = context.SaveChanges();
                    if (rowAffected > 0)
                    {
                        result.Success = true;                        
                    }
                }                
            }
            catch (Exception e)
            {
                result.Error = e.Message;
            }
            
            return result;
        }

        public IEnumerable<ApplicationUser> GetAllUsers()
        {
            return context.ApplicationUsers;
        }

        public ApplicationUser GetUser(int Id)
        {
            return context.ApplicationUsers.Find(Id);
        }

        public ApplicationUser GetUser(string name)
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
                    int rowAffected = context.SaveChanges();
                    if (rowAffected > 0)
                    {
                        result.Success = true;
                    }
                }
            }
            catch (Exception e)
            {
                result.Error = e.Message;
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
