using MedApi.API;
using MedApi.Application;
using MedApi.Infrastructure;
using MedApi.Infrastructure.Seeds;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore; // O usar catch (Exception ex) si prefieres genérico

var builder = WebApplication.CreateBuilder(args);

// Configuración en cascada de todas las capas
builder.Services
    .AddApplication(builder.Configuration)
    .AddInfrastructure(builder.Configuration)
    .AddAPI(builder.Configuration);

var app = builder.Build();

// Configurar migraciones y seeding
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        
        // Esperar a que SQL Server esté listo (especialmente útil en Docker)
        var maxAttempts = 10;
        for (int i = 0; i < maxAttempts; i++)
        {
            try
            {
                if (context.Database.CanConnect())
                {
                    context.Database.Migrate();
                    await DatabaseSeeder.SeedAsync(context);
                    break;
                }
            }
            catch (SqlException) when (i < maxAttempts - 1)
            {
                await Task.Delay(5000); // Esperar 5 segundos antes de reintentar
            }
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating or seeding the database");
    }
}

// Configuración del pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("FrontendPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Seed database (opcional, puedes moverlo a Infra si prefieres)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    await DatabaseSeeder.SeedAsync(context);
}

app.Run();