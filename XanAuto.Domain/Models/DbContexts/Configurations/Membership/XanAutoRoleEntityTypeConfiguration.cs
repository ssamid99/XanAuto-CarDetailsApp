using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XanAuto.Domain.Models.Entities.Membership;

namespace XanAuto.Domain.Models.DataContexts.Configurations.Membership
{
    public class XanAutoRoleEntityTypeConfiguration : IEntityTypeConfiguration<XanAutoRole>
    {
        public void Configure(EntityTypeBuilder<XanAutoRole> builder)
        {
            builder.ToTable("Roles", "Membership");
        }
    }
}
