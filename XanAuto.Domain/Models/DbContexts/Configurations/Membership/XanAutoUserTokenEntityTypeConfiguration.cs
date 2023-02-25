using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XanAuto.Domain.Models.Entities.Membership;

namespace XanAuto.Domain.Models.DataContexts.Configurations.Membership
{
    public class XanAutoUserTokenEntityTypeConfiguration : IEntityTypeConfiguration<XanAutoUserToken>
    {
        public void Configure(EntityTypeBuilder<XanAutoUserToken> builder)
        {
            builder.ToTable("UserTokens", "Membership");
        }
    }
}
