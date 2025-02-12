using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoxU_Backend.Pesistence.Identity.Entities;
using VoxU_Backend.Core.Application.Enums;

namespace VoxU_Backend.Pesistence.Identity.Seeds
{
    public static class AdminUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser user = new();
            user.Name = "Enrique";
            user.UserName = "20221860";
            user.User = "Enrique01";
            user.LastName = "Pena";
            user.Email = "20221860@itla.edu.do";
            user.EmailConfirmed = true;
            user.PhoneNumber = "809-236-2155";
            user.PhoneNumberConfirmed = true;
            user.ProfilePicture = "https://th.bing.com/th/id/R.f6cfbed7dd27ab87a330f905ac3d95a3?rik=n1ZTdyVp2HWn%2bw&riu=http%3a%2f%2fwww.musicnewstime.com%2fwp-content%2fuploads%2f2011%2f07%2fenrique-iglesias.jpg&ehk=bkNQ5B7biT3ehhaEWO4FrWqiC1Zy%2bbzWSrn238%2fyAc8%3d&risl=&pid=ImgRaw&r=0";
            user.Created_At = DateTime.Now;
            
            if (userManager.Users.All(u => u.Id != user.Id))
            {
                var email = userManager.FindByEmailAsync(user.Email);
                
                if (email == null)
                 { 
                      await userManager.CreateAsync(user);
                      await userManager.AddToRoleAsync(user, Roles.Basic.ToString());
                      await userManager.AddToRoleAsync(user, Roles.Admin.ToString());
                 }

            }



         }



    }
}
