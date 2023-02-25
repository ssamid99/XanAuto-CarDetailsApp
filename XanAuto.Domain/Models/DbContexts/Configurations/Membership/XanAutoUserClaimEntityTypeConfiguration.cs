using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XanAuto.Domain.Models.Entities.Membership;

namespace XanAuto.Domain.Models.DataContexts.Configurations.Membership
{
    public class XanAutoUserClaimEntityTypeConfiguration : IEntityTypeConfiguration<XanAutoUserClaim>
    {
        public void Configure(EntityTypeBuilder<XanAutoUserClaim> builder)
        {
            builder.ToTable("UserClaims", "Membership");
        }
    }
}
