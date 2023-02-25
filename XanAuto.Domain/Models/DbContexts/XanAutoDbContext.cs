using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using XanAuto.Domain.Models.Entities;
using XanAuto.Domain.Models.Entities.Membership;

namespace XanAuto.Domain.Models.DbContexts
{
    public partial class XanAutoDbContext : IdentityDbContext<XanAutoUser, XanAutoRole, int, XanAutoUserClaim, XanAutoUserRole, XanAutoUserLogin, XanAutoRoleClaim, XanAutoUserToken>
    {
        public XanAutoDbContext(DbContextOptions options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(XanAutoDbContext).Assembly);
        }
    }
}
