using Domain.Entities.Requests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    internal class RequestConfiguration : IEntityTypeConfiguration<Request>
    {
        public void Configure(EntityTypeBuilder<Request> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(r => r.Description).HasMaxLength(300);
            builder.Property(r => r.SeveralLevel).HasMaxLength(20);
            builder.Property(r => r.Reason).HasMaxLength(300);
            builder.Property(e => e.Enable).IsRequired();
            builder.Property(r => r.Date);
            builder.HasOne(r => r.Account)
                .WithMany(a => a.Requests)
                .HasForeignKey(r => r.AccountId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(r => r.Room)
                .WithMany(a => a.Requests)
                .HasForeignKey(r => r.RoomId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(r => r.RequestStatus)
                .WithMany(a => a.Requests)
                .HasForeignKey(r => r.RequestStatusId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(r => r.Problem)
                .WithMany(a => a.Requests)
                .HasForeignKey(r => r.ProblemId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
