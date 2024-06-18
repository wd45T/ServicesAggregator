using Aggregator.Infrastructure.DataAccess;
using Aggregator.Infrastructure.DataAccess.Interceptors;
using Aggregator.Infrastructure.Utils;
using Aggregator.InterfaceAdapters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Aggregator.Infrastructure.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ISaveChangesInterceptor, DateTimeInterceptor>();

        services.AddDbContext<IUnitOfWork, UnitOfWork>((sp, options) =>
        {
            options.UseNpgsql(configuration.GetConnectionString("PostgresConnection"));

            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
        });

        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        return services;
    }
}
