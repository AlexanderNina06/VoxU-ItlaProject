using Microsoft.AspNetCore.Identity;
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
            user.ProfilePicture = null;
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
