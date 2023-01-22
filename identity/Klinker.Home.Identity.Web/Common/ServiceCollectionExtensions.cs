using Microsoft.EntityFrameworkCore;

namespace Klinker.Home.Identity.Web.Common;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddKlinkerHomeIdentityWeb(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("Identity");
        services.AddIdentityServer()
            .AddConfigurationStore(opts =>
            {
                opts.ConfigureDbContext = o => o.UseNpgsql(connectionString);
            })
            .AddOperationalStore(opts =>
            {
                opts.ConfigureDbContext = o => o.UseNpgsql(connectionString);
            });
        return services;
    }
}