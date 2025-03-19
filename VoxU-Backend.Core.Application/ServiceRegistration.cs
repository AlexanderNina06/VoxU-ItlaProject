using AutoMapper;
using AutoMapper.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VoxU_Backend.Core.Application.Interfaces.Services;
using VoxU_Backend.Core.Application.Mapper;
using VoxU_Backend.Core.Application.Services;

namespace VoxU_Backend.Core.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(cfg => cfg.Internal().MethodMappingEnabled = false, typeof(GeneralProfile).Assembly);

            services.AddTransient<IPublicationService, PublicationService>();
            services.AddTransient<ISellpublicationService, SellPublicationService>();
            services.AddTransient<ICommentService, CommentsService>();
            services.AddTransient<IRepliesService, RepliesService>();
            services.AddTransient<IBookService, BookService>();


        }

    }
}
