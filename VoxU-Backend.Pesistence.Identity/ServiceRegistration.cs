using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoxU_Backend.Core.Application.Interfaces.Services;
using VoxU_Backend.Pesistence.Identity.Context;
using VoxU_Backend.Pesistence.Identity.Entities;
using VoxU_Backend.Pesistence.Identity.Service;

namespace VoxU_Backend.Pesistence.Identity
{
    public static class ServiceRegistration
    {
        public static void AddIdentityLayer(this IServiceCollection Services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                Services.AddDbContext<IdentityContext>(options => options.UseInMemoryDatabase("IdentityDb"));

            }
            else
            {
                Services.AddDbContext<IdentityContext>(options =>
                {
                    options.EnableSensitiveDataLogging();
                    options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"),
                        m => m.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName));
                });
            }

            //Injecting identity
            Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();

            Services.AddAuthentication();
            Services.AddScoped<IAccountService, AccountService>();

            //Services.ConfigureApplicationCookie(options =>
            //{
            //    options.LoginPath = "/User";
            //    options.AccessDeniedPath = "/User/AccessDenied";
            //});
        }

    }
}
