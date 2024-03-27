using Domain.Entities.Requests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    internal class ProblemConfiguration : IEntityTypeConfiguration<Problem>
    {
        public void Configure(EntityTypeBuilder<Problem> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(d => d.Title).HasMaxLength(300);

            builder.HasData(new Problem[]
            {
                new Problem { Id = 1, Title = "Fire and Safety Hazards", IsDisplay = true },
                new Problem { Id = 2, Title = "Poor Ventilation", IsDisplay = true },
                new Problem { Id = 3, Title = "Bullying and Harassment", IsDisplay = true },
                new Problem { Id = 4, Title = "Inadequate Facilities", IsDisplay = true },
                new Problem { Id = 5, Title = "Health and Sanitation Issues", IsDisplay = true },
                new Problem { Id = 6, Title = "Transportation Challenges", IsDisplay = true },
            });
        }
    }
}
