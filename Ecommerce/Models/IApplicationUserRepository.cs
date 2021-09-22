﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public interface IApplicationUserRepository
    {
        ApplicationUser GetUser(int Id);
        ApplicationUser GetUser(string name);
        IEnumerable<ApplicationUser> GetAllUsers();
        Task<IdentityResult> Add(ApplicationUser user);
        Task<IdentityResult> Update(ApplicationUser userChanges);
        Result Delete(ApplicationUser user);
        Result Active(ApplicationUser user);
        Result Locout(ApplicationUser user);

    }
}
