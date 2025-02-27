using Swashbuckle.AspNetCore.SwaggerUI;

namespace VoxU_Backend.Extensions
{
    public static class AppExtension
    {
        public static void UserSwaggerExtension(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "VoxU-Backend");
                options.DefaultModelRendering(ModelRendering.Model);
            });

        }
    }
}
