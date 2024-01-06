using Application.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration
            .GetConnectionString("AppConnectionString");

        if (connectionString is null)
        {
            throw new InvalidOperationException("AppConnectionString is missing from configuration");
        }
        
        services.AddDbContext<AppDbContext>(
            options => options.UseNpgsql(connectionString));

        return services;
    }
}