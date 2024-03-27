using Domain.Entities.Requests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    internal class RequestStatusConfiguration : IEntityTypeConfiguration<RequestStatus>
    {

        public void Configure(EntityTypeBuilder<RequestStatus> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(rs => rs.StatusName).HasMaxLength(30);
            builder.Property(rs => rs.ColorCode).HasMaxLength(10);

            builder.HasData(new RequestStatus[]
            {
                new RequestStatus { Id = 1, StatusName = "Open", ColorCode = "#3300FF" },
                new RequestStatus { Id = 2, StatusName = "Assigned", ColorCode = "#FFFF00" },
                new RequestStatus { Id = 3, StatusName = "Work in progress", ColorCode = "#FF6600" },
                new RequestStatus { Id = 4, StatusName = "Need more info", ColorCode = "#FF0033" },
                new RequestStatus { Id = 5, StatusName = "Rejected", ColorCode = "#FF0000" },
                new RequestStatus { Id = 6, StatusName = "Completed", ColorCode = "#33FF33" },
                new RequestStatus { Id = 7, StatusName = "Closed", ColorCode = "#FF0000" },
            });
        }
    }
}
