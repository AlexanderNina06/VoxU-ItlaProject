using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
namespace VoxU_Backend.Extensions
{
    public static class ServiceExtension
    {
      
        public static void AddSwaggerExtension(this IServiceCollection service)
        {
            service.AddSwaggerGen(options =>
            {
                
                List<string> xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*xml", searchOption: SearchOption.TopDirectoryOnly).ToList(); 

                xmlFiles.ForEach(xmlfile => options.IncludeXmlComments(xmlfile));

                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "VoxU-Backend",
                    Description = "This api is for social media purposes",
                    Contact = new OpenApiContact
                    {
                        Email = "20222860@itla.edu.do",
                        Name = "Enrique, Jeison, Nicol",
                        Url = new Uri("https://www.itla.edu.do")
                    }
                });


                options.DescribeAllParametersInCamelCase();

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Description = "Input your bearer token using the following format: Bearer {Input your token here}"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer" // Apuntamos a la configuracion de security que realizamos arriba,
                            },
                            Scheme = "Bearer",
                            Name = "Bearer",
                            In = ParameterLocation.Header

                        }, new List<string>()
                    }
                });
            });

        }

        public static void AddApiVersioningExtension(this IServiceCollection Service)
        {
            Service.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });
        }

    }
}
