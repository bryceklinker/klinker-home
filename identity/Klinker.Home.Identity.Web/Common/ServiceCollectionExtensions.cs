using Klinker.Home.Identity.Web.Common.Storage;
using Klinker.Home.Identity.Web.Users.Entities;
using Microsoft.EntityFrameworkCore;

namespace Klinker.Home.Identity.Web.Common;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddKlinkerHomeIdentityWeb(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("Identity");
        if (string.IsNullOrEmpty(connectionString))
            throw new InvalidOperationException("Missing 'Identity' connection string");

        services.AddRazorPages();
        services.AddDbContext<KlinkerIdentityDbContext>(opts =>
        {
            opts.UseDatabase(connectionString);
        });

        services.AddIdentity<KlinkerUser, KlinkerRole>().AddEntityFrameworkStores<KlinkerIdentityDbContext>();

        services
            .AddIdentityServer()
            .AddConfigurationStore(opts =>
            {
                opts.ConfigureDbContext = o => o.UseDatabase(connectionString);
            })
            .AddOperationalStore(opts =>
            {
                opts.ConfigureDbContext = o => o.UseDatabase(connectionString);
            })
            .AddAspNetIdentity<KlinkerUser>();
        return services;
    }

    private static void UseDatabase(this DbContextOptionsBuilder builder, string connectionString)
    {
        builder.UseNpgsql(
            connectionString,
            o =>
            {
                o.MigrationsAssembly(typeof(ServiceCollectionExtensions).Assembly.GetName().Name);
            }
        );
    }
}
