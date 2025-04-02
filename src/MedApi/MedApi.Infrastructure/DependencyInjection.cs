namespace MedApi.Infrastructure;

using MedApi.Application.Interfaces;
using MedApi.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        string connectionName = "DefaultConnection";

        var connectionString = configuration.GetConnectionString(connectionName)
            ?? throw new ArgumentNullException(nameof(configuration),
            $"Connection string '{connectionName}' not found.");

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString).UseSnakeCaseNamingConvention();
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAppointmentRepository, AppointmentRepository>();
        services.AddScoped<IDoctorRepository, DoctorRepository>();
        services.AddScoped<IPatientRepository, PatientRepository>();
        services.AddScoped<IAppointmentQueryRepository, AppointmentQueryRepository>();

        return services;
    }
}
