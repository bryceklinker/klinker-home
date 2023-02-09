using Klinker.Home.Identity.Web.Common.Storage;
using Klinker.Home.Identity.Web.Users.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Klinker.Home.Identity.Web.Common;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddKlinkerHomeIdentityWeb(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("Identity");

        services.AddDbContext<KlinkerIdentityDbContext>(opts =>
        {
            opts.UseDatabase(connectionString);
        });

        services
            .AddIdentity<KlinkerUser, KlinkerRole>()
            .AddEntityFrameworkStores<KlinkerIdentityDbContext>()
            .AddDefaultTokenProviders();

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
        services.ConfigureApplicationCookie(opts =>
        {
            opts.Cookie.SameSite = SameSiteMode.Lax;
            opts.LoginPath = new PathString("/login");
        });
        services.AddRouting(opts => opts.LowercaseUrls = true);
        services.AddRazorPages();
        return services;
    }

    private static void UseDatabase(this DbContextOptionsBuilder builder, string? connectionString)
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
