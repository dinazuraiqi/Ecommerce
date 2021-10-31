using Ecommerce.Data;
using Ecommerce.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Areas.Admin.Models
{
    public class SQLRoleRepository : IRoleRepository
    {
        RoleManager<IdentityRole> _roleManager;
        UserManager<IdentityUser> _userManager;
        ApplicationDbContext _db;
        public SQLRoleRepository(RoleManager<IdentityRole> roleManager, ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _db = db;
            _userManager = userManager;
        }
        public async Task<Result> Add(string name)
        {
            Result result = new Result();
            try
            {
                IdentityRole role = new IdentityRole();
                role.Name = name;
                var isExist = await _roleManager.RoleExistsAsync(role.Name);
                if (isExist)
                {
                    return result;
                }
                var createResult = await _roleManager.CreateAsync(role);
                if (createResult.Succeeded)
                {
                    result.Success = true;
                    result.ResultObject = role;
                }
            }
            catch(Exception e)
            {
                result.ErrorMessage = e.Message;
            }
            return result;
        }

        public async Task<Result> Assign(RoleUserVm roleUser)
        {
            Result result = new Result();
            try
            {              
                var user = _db.ApplicationUsers.FirstOrDefault(c => c.Id == roleUser.UserId);

                var currentRoles = await _userManager.GetRolesAsync(user);
                IList<string> rolesToRemove = new List<string>();
                foreach(string roleName in currentRoles)
                {
                    if (roleName == roleUser.RoleId)
                    {
                        result.ResultObject = roleUser;
                        return result;
                    }
                    rolesToRemove.Add(roleName);
                }
                if (rolesToRemove != null)
                {
                    await _userManager.RemoveFromRolesAsync(user, rolesToRemove);
                }
                
                await _userManager.AddToRoleAsync(user, roleUser.RoleId);
                result.Success = true;
                result.ResultObject = roleUser;
            }
            catch(Exception e)
            {
                result.ErrorMessage = e.Message;
            }
            return result;
        }

        public IEnumerable<UserRoleMaping> GetUserRole()
        {
            var result = from ur in _db.UserRoles
                         join r in _db.Roles on ur.RoleId equals r.Id
                         join a in _db.ApplicationUsers on ur.UserId equals a.Id
                         select new UserRoleMaping()
                         {
                             UserId = ur.UserId,
                             RoleId = ur.RoleId,
                             UserName = a.UserName,
                             RoleName = r.Name
                         };
            return result;
        }

        public async Task<Result> Delete(string id)
        {
            Result result = new Result();
            try
            {
                var role = await _roleManager.FindByIdAsync(id);
                if (role == null)
                {
                    return result;
                }
                await _roleManager.DeleteAsync(role);
                result.Success = true;
                result.ResultObject = role;
            }
            catch(Exception e)
            {
                result.ErrorMessage = e.Message;
            }
            return result;
        }

        public IEnumerable<IdentityRole> GetAllRoles()
        {
            return _roleManager.Roles.ToList();
        }

        public async  Task<IdentityRole> GetRole(string Id)
        {
            return await _roleManager.FindByIdAsync(Id);
        }

        public async Task<IdentityRole> GetRoleByName(string name)
        {
            return await _roleManager.FindByNameAsync(name);
        }

        public async Task<Result> Update(string id, string name)
        {
            Result result = new Result();
            try
            {
                var role = await _roleManager.FindByIdAsync(id);
                if (role == null)
                {
                    return result;
                }
                role.Name = name;
                var isExist = await _roleManager.RoleExistsAsync(role.Name);                
                result.ResultObject = role;
                if (isExist)
                {
                    return result;
                }
                await _roleManager.UpdateAsync(role);
                result.Success = true;
            }
            catch(Exception e)
            {
                result.ErrorMessage = e.Message;
            }
            
            return result;
        }
    }
}
