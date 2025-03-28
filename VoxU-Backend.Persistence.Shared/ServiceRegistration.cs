using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoxU_Backend.Core.Application.Interfaces.Services;
using VoxU_Backend.Core.Domain.Settings;
using VoxU_Backend.Persistence.Shared.Options;
using VoxU_Backend.Persistence.Shared.Service;

namespace VoxU_Backend.Persistence.Shared
{
    public static class ServiceRegistration
    {
        public static void AddSharedLayer(this IServiceCollection services, IConfiguration configuration)
        {

            //Recuperar settings y agregarlos al mailSettings
            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
            services.Configure<GeminiSettings>(configuration.GetSection("GeminiSettings"));
            services.Configure<ClaudinaryOptions>(configuration.GetSection("Cloudinary"));

            services.AddTransient<IEmailService, EmailService>();
            services.AddScoped<IChatbotService, ChatbotService>();
            services.AddHttpClient<IChatbotService, ChatbotService>();
            services.AddScoped<ICloudinaryService, CloudinaryService>();
            services.AddSingleton<ICloudinaryService>(provider =>
            new CloudinaryService(provider.GetRequiredService<IConfiguration>()));
        }
    }
}
