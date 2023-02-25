using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XanAuto.Domain.Models.Entities.Membership;

namespace XanAuto.Domain.Models.DataContexts.Configurations.Membership
{
    public class XanAutoUserRoleEntityTypeConfiguration : IEntityTypeConfiguration<XanAutoUserRole>
    {
        public void Configure(EntityTypeBuilder<XanAutoUserRole> builder)
        {
            builder.ToTable("UserRoles", "Membership");
        }
    }
}
