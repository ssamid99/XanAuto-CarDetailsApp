using Microsoft.EntityFrameworkCore;
using XanAuto.Domain.Models.Entities.Membership;

namespace XanAuto.Domain.Models.DbContexts
{
    public partial class XanAutoDbContext
    {
        public DbSet<XanAutoRole> XanAutoRoles { get; set; }
        public DbSet<XanAutoRoleClaim> XanAutoRoleClaims { get; set; }
        public DbSet<XanAutoUser> XanAutoUsers { get; set; }
        public DbSet<XanAutoUserClaim> XanAutoUserClaims { get; set; }
        public DbSet<XanAutoUserLogin> XanAutoUserLogins { get; set; }
        public DbSet<XanAutoUserRole> XanAutoUserRoles { get; set; }
        public DbSet<XanAutoUserToken> XanAutoUserTokens { get; set; }
    }
}
