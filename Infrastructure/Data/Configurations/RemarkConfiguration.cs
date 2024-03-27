using Domain.Entities.Requests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    internal class RemarkConfiguration : IEntityTypeConfiguration<Remark>
    {
        public void Configure(EntityTypeBuilder<Remark> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(r => r.Comment).HasMaxLength(300);

            builder.HasOne(r => r.Account)
                .WithMany(a => a.Remarks)
                .HasForeignKey(r => r.AccountId)
                .OnDelete(DeleteBehavior.NoAction); 

            builder.HasOne(r => r.Request)
                .WithMany(r => r.Remarks)
                .HasForeignKey(r => r.RequestId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
