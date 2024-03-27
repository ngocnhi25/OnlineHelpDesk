using Domain.Entities.Requests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    internal class NotificationRemarkConfiguration : IEntityTypeConfiguration<NotificationRemark>
    {

        public void Configure(EntityTypeBuilder<NotificationRemark> builder)
        {
            builder.HasKey(acc => acc.Id);

            builder.HasOne(acc => acc.Account)
                .WithMany(role => role.NotificationRemarks)
                .HasForeignKey(acc => acc.AccountId)
                .OnDelete(DeleteBehavior.NoAction);
            
            builder.HasOne(acc => acc.Request)
                .WithMany(role => role.NotificationRemarks)
                .HasForeignKey(acc => acc.RequestId);
        }
    }
}
