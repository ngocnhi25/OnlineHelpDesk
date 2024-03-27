using Domain.Entities.Requests;
using Domain.Repositories;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class NotificationHandleRequestRepository : GenericRepository<NotificationHandleRequest>, INotificationHandleRequestRepository
    {
        public NotificationHandleRequestRepository(OHDDbContext dbContext)
           : base(dbContext) { }

    }
}

