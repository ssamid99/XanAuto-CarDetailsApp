using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XanAuto.Domain.Models.Entities.Membership;

namespace XanAuto.Domain.Models.DataContexts.Configurations.Membership
{
    public class XanAutoRoleClaimEntityTypeConfiguration : IEntityTypeConfiguration<XanAutoRoleClaim>
    {
        public void Configure(EntityTypeBuilder<XanAutoRoleClaim> builder)
        {
            builder.ToTable("RoleClaims", "Membership");
        }
    }
}
