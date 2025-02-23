using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoxU_Backend.Core.Application.Enums;
using VoxU_Backend.Pesistence.Identity.Entities;

namespace VoxU_Backend.Pesistence.Identity.Seeds
{
    public static class DefaultRoles
    {
        public static async Task AddDefaultRoles(RoleManager<IdentityRole> roles)
        {

            await roles.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roles.CreateAsync(new IdentityRole(Roles.Basic.ToString()));

        }


    }
}
