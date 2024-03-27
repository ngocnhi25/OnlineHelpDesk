using Domain.Entities.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    internal class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.RoleName).HasMaxLength(20);

            builder.HasOne(c => c.RoleTypes)
                .WithMany(c => c.Role)
                .HasForeignKey(c => c.RoleTypeId);

            builder.HasData(new Role[]
            {
                new Role { Id = 1, RoleTypeId = 1, RoleName = "Student" },
                new Role { Id = 2, RoleTypeId = 1, RoleName = "Teacher" },
                new Role { Id = 3, RoleTypeId = 2, RoleName = "Request Handler" },
                new Role { Id = 4, RoleTypeId = 3, RoleName = "Assignees" },
                new Role { Id = 5, RoleTypeId = 4, RoleName = "Admin" },
            });
        }
    }
}
