using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Config
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(p=>p.PrivateNumber).IsRequired().HasMaxLength(11);
            builder.Property(p=>p.MonthSalary).HasColumnType("decimal(18,2)");
        }
    }
}