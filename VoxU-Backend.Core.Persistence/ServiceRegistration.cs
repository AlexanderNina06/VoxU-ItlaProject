using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoxU_Backend.Core.Application.Interfaces.Repositories;
using VoxU_Backend.Core.Application.Interfaces.Services;
using VoxU_Backend.Core.Application.Services;
using VoxU_Backend.Core.Persistence.Contexts;
using VoxU_Backend.Core.Persistence.Repositories;

namespace VoxU_Backend.Core.Persistence
{
    public static class ServiceRegistration
    {

        public static void AddPersistenceLayer(this IServiceCollection service, IConfiguration configuration)
        {

            //Si la configuracion UseInMemoryDatabase del appsetting es true, entonces utiliza la db en memoria
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                service.AddDbContext<ApplicationContext>(options => options.UseInMemoryDatabase("AppDb"));
            }
            else
            {
               var connectionString = configuration.GetConnectionString("DefaultConnection");
                service.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(connectionString, options => options.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));
                
                //Acceso a las opciones de sqlserver y configuro donde se ubicaran las migraciones pasandole el assembly donde se ubica el applicationContext
            }

            service.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            service.AddTransient<IPublicationRepository, PublicationsRepository>();
            service.AddTransient<ICommentsRepository, CommentsRepository>();
            service.AddTransient<IRepliesService, RepliesService>();

        }

    }
}
