using Microsoft.AspNetCore.Identity;
using VoxU_Backend.Core.Persistence;
using VoxU_Backend.Extensions;
using VoxU_Backend.Pesistence.Identity;
using VoxU_Backend.Pesistence.Identity.Entities;
using VoxU_Backend.Pesistence.Identity.Seeds;
using VoxU_Backend.Core.Application;
using VoxU_Backend.Persistence.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPersistenceLayer(builder.Configuration);
builder.Services.AddApplicationLayer();
builder.Services.AddSharedLayer(builder.Configuration);
builder.Services.AddIdentityLayer(builder.Configuration);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerExtension();
builder.Services.AddApiVersioningExtension();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddHealthChecks();

var app = builder.Build();

// Configure the HTTP request pipeline.

using (var scope = app.Services.CreateScope()) // Cada vez que hacemos injecciones de dependencia esto es lo que ocurre detras, estas se inicializan con el starup, en este caso no usamos startup, pero simplemente creamos una injeccion propia que se ejecuta antes del app.run
{
    var services = scope.ServiceProvider; // Recupero servicios 

    try
    { // Realizo las injecciones de dependencia 
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>(); // Con los servicios puedo recuperar esos objetos - clases a injectar 
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        // Ahora creamos los roles primero antes que los usuarios
        await DefaultRoles.AddDefaultRoles(roleManager);
        await BasicUser.SeedAsync(userManager, roleManager);
        await AdminUser.SeedAsync(userManager, roleManager);

    }
    catch (Exception ex)
    {

    }


}

//Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UserSwaggerExtension();
app.UseHealthChecks("/health");
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
