using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XanAuto.Domain.Models.Entities.Membership;

namespace XanAuto.Domain.Models.DataContexts.Configurations.Membership
{
    public class XanAutoUserLoginEntityTypeConfiguration : IEntityTypeConfiguration<XanAutoUserLogin>
    {
        public void Configure(EntityTypeBuilder<XanAutoUserLogin> builder)
        {
            builder.ToTable("UserLogins", "Membership");
        }
    }
}
