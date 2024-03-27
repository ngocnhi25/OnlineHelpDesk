using Domain.Entities.Notifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    internal class NotificationQueueConfiguration : IEntityTypeConfiguration<NotificationQueue>
    {
        public void Configure(EntityTypeBuilder<NotificationQueue> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(r => r.NotificationType)
                .WithMany(a => a.NotificationQueues)
                .HasForeignKey(r => r.NotificationTypeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(r => r.Account)
                .WithMany(a => a.NotificationQueuesAccount)
                .HasForeignKey(r => r.AccountId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(r => r.AccountSender)
                .WithMany(a => a.NotificationQueuesAccountSender)
                .HasForeignKey(r => r.AccountSenderId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(r => r.Request)
                .WithMany(a => a.NotificationQueues)
                .HasForeignKey(r => r.RequestId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
