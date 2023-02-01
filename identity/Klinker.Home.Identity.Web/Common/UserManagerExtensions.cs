using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Klinker.Home.Identity.Web.Common;

public static class UserManagerExtensions
{
    public static async Task<bool> DoAnyUsersExistAsync<TUser>(this UserManager<TUser> manager)
        where TUser : class
    {
        return await manager.Users.AnyAsync().ConfigureAwait(false);
    }
}
