using Domain.Entities.Requests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    internal class NotificationHandleRequestConfiguration : IEntityTypeConfiguration<NotificationHandleRequest>
    {

        public void Configure(EntityTypeBuilder<NotificationHandleRequest> builder)
        {
            builder.HasKey(acc => acc.Id);
            builder.Property(x => x.Id).HasMaxLength(100);

            builder.HasOne(acc => acc.Account)
                .WithMany(role => role.NotificationHandleRequests)
                .HasForeignKey(acc => acc.AccountId)
                .OnDelete(DeleteBehavior.NoAction);
            
            builder.HasOne(acc => acc.Request)
                .WithMany(role => role.NotificationHandleRequests)
                .HasForeignKey(acc => acc.RequestId);
        }
    }
}
