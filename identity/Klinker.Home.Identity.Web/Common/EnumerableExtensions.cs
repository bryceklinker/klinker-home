using System.Collections.Immutable;
using Microsoft.EntityFrameworkCore;

namespace Klinker.Home.Identity.Web.Common;

public static class EnumerableExtensions
{
    public static async Task<ImmutableArray<T>> ToImmutableArrayAsync<T>(this IQueryable<T> source)
    {
        var items = await source.ToArrayAsync().ConfigureAwait(false);
        return items.ToImmutableArray();
    }
}
