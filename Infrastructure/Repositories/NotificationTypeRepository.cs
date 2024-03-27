using Domain.Entities.Notifications;
using Domain.Repositories;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public sealed class NotificationTypeRepository : GenericRepository<NotificationType>, INotificationTypeRepository
    {
        public NotificationTypeRepository(OHDDbContext dbContext)
                   : base(dbContext) { }
    }
}
