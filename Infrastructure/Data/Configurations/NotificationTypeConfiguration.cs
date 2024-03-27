using Domain.Entities.Notifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    internal class NotificationTypeConfiguration : IEntityTypeConfiguration<NotificationType>
    {

        public void Configure(EntityTypeBuilder<NotificationType> builder)
        {
            builder.HasKey(acc => acc.Id);
            builder.Property(r => r.TypeName).HasMaxLength(300);

            builder.HasData(new NotificationType[]
            {
                new NotificationType { Id = 1, TypeName = "Create request" },
                new NotificationType { Id = 2, TypeName = "Assigned request" },
                new NotificationType { Id = 3, TypeName = "Update request" },
                new NotificationType { Id = 4, TypeName = "Chat" },
            });
        }
    }
}
