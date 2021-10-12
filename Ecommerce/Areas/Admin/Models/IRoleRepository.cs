using Ecommerce.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Areas.Admin.Models
{
    public interface IRoleRepository
    {
        Task<IdentityRole> GetRole(string Id);
        Task<IdentityRole> GetRoleByName(string name);
        IEnumerable<IdentityRole> GetAllRoles();
        Task<Result> Add(string name);
        Task<Result> Update(string id, string name);
        Task<Result> Delete(string id);
        Task<Result> Assign(RoleUserVm roleUser);
        IEnumerable<UserRoleMaping> GetUserRole();

    }
}
