using Microsoft.AspNetCore.Identity;
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
            user.ProfilePicture = null;
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
