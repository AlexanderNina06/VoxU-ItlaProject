using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoxU_Backend.Core.Application.DTOS.Account;
using VoxU_Backend.Core.Application.Interfaces.Services;
using VoxU_Backend.Core.Domain.Settings;
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
           
            Services.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));

            Services.AddTransient<IAccountService, AccountService>();


            Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; //Decimos que el schema de autenticacion va a ser bajo el jsonWebToken por deafult
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                //Agregamso y Activamos las opciones de autenticacion
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false; // No tenemos certificado https, esta funcionalidad trabaja con dicho certificado
                options.SaveToken = false;
                options.TokenValidationParameters = new TokenValidationParameters // Los token validations parameters son los parametros con los cuales agregaremos propiedades de validacion
                {                                                                 // para el token
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration["JWTSettings:Issuer"],
                    ValidAudience = configuration["JWTSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:Key"])),

                };
                //Eventos
                //Cuando enviamos un token se debe a varias circumstancias, 1- Que el token no sea valido, 2- Alguna exepcion a nivel de sistema,
                //3 - El token utilizado es valido pero no tiene permisos para acceder a la seccion y, cuya seccion el token no tiene permisos para acceder
                options.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = c =>
                    {
                        c.NoResult();
                        c.Response.StatusCode = 500;
                        c.Response.ContentType = "text/plain";
                        return c.Response.WriteAsync(c.Exception.ToString());
                    },

                    OnChallenge = c =>
                    {
                        c.HandleResponse();
                        c.Response.StatusCode = 401;
                        c.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new JWTResponse { HasError = true, Error = "You are not authorized" });
                        return c.Response.WriteAsync(result);
                    },

                    OnForbidden = c =>
                    {
                        c.Response.StatusCode = 403;
                        c.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new JWTResponse { HasError = true, Error = "Access Denied" });
                        return c.Response.WriteAsync(result);
                    },

                };
            });
            //Services.ConfigureApplicationCookie(options =>
            //{
            //    options.LoginPath = "/User";
            //    options.AccessDeniedPath = "/User/AccessDenied";
            //});
        }

    }
}
