using Microsoft.Extensions.DependencyInjection;

namespace Klinker.Home.Identity.Web.Tests.Support;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection Remove<T>(this IServiceCollection services)
    {
        var descriptor = services.FirstOrDefault(s => s.ServiceType == typeof(T));
        if (descriptor != null) services.Remove(descriptor);
        return services;
    }
}