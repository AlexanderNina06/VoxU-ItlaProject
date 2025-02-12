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
    public static class BasicUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser user = new();
            user.Name = "John";
            user.UserName = "123454567";
            user.User = "John02";
            user.LastName = "Doe";
            user.Email = "Johndoe@itla.edu.do";
            user.EmailConfirmed = true;
            user.PhoneNumber = "809-222-3442";
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

                }

            }



        }


    }
}
