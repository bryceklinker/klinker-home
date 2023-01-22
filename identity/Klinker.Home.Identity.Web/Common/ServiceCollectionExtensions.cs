using Klinker.Home.Identity.Web.Common.Storage;
using Klinker.Home.Identity.Web.Users.Entities;
using Microsoft.EntityFrameworkCore;

namespace Klinker.Home.Identity.Web.Common;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddKlinkerHomeIdentityWeb(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("Identity");
        services.AddRazorPages();
        services.AddDbContext<KlinkerIdentityDbContext>(opts =>
        {
            opts.UseNpgsql(connectionString);
        });

        services.AddIdentity<KlinkerUser, KlinkerRole>().AddEntityFrameworkStores<KlinkerIdentityDbContext>();

        services
            .AddIdentityServer()
            .AddConfigurationStore(opts =>
            {
                opts.ConfigureDbContext = o => o.UseNpgsql(connectionString);
            })
            .AddOperationalStore(opts =>
            {
                opts.ConfigureDbContext = o => o.UseNpgsql(connectionString);
            })
            .AddAspNetIdentity<KlinkerUser>();
        return services;
    }
}
