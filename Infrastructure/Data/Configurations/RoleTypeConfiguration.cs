using Domain.Entities.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    internal class RoleTypeConfiguration : IEntityTypeConfiguration<RoleType>
    {
        public void Configure(EntityTypeBuilder<RoleType> builder)
        {
            builder.HasKey(rt => rt.Id);
            builder.Property(rt => rt.RoleTypeName).HasMaxLength(20);

            builder.HasData(new RoleType[]
            {
                new RoleType { Id = 1, RoleTypeName = "End-Users" },
                new RoleType { Id = 2, RoleTypeName = "Facility-Heads" },
                new RoleType { Id = 3, RoleTypeName = "Assignees" },
                new RoleType { Id = 4, RoleTypeName = "Administrator" },
            });
        }
    }
}
