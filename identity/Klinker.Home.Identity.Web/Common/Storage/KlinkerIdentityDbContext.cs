using Klinker.Home.Identity.Web.Users.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Klinker.Home.Identity.Web.Common.Storage;

public class KlinkerIdentityDbContext : IdentityDbContext<KlinkerUser, KlinkerRole, Guid>
{
    public KlinkerIdentityDbContext(DbContextOptions<KlinkerIdentityDbContext> options)
        : base(options) { }
}
