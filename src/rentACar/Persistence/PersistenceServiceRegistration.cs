using Application.Services.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Persistence.Repositories;

namespace Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BaseDbContext>(
            options => options.UseSqlServer(configuration.GetConnectionString("RentACarConnectionString"), b => b.MigrationsAssembly("WebAPI"))
        );

        services.AddScoped<IBusinessRepository, BusinessRepository>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IBusinessImageRepository, BusinessImageRepository>();
        services.AddScoped<IWorkingHourRepository, WorkingHourRepository>();
        services.AddScoped<IBusinessServiceRepository, BusinessServiceRepository>();
        services.AddScoped<IUserDeviceRepository, UserDeviceRepository>();
        services.AddScoped<INotificationRepository, NotificationRepository>();

        return services;
    }
}
