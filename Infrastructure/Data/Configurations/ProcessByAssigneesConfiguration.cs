using Domain.Entities.Requests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    internal class ProcessByAssigneesConfiguration : IEntityTypeConfiguration<ProcessByAssignees>
    {
        public void Configure(EntityTypeBuilder<ProcessByAssignees> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(p => p.Account)
                .WithMany(a => a.ProcessByAssignees)
                .HasForeignKey(p => p.AccountId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.Request)
                .WithMany(a => a.ProcessByAssignees)
                .HasForeignKey(p => p.RequestId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
