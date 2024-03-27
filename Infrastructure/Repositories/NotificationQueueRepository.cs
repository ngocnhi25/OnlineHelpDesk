using Domain.Entities.Notifications;
using Domain.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public sealed class NotificationQueueRepository : GenericRepository<NotificationQueue>, INotificationQueueRepository
    {
        public NotificationQueueRepository(OHDDbContext dbContext)
                   : base(dbContext) { }

        public async Task<NotificationQueue> GetNotificationCreatedRequest(Guid notificationId, string accountId)
        {
            var list = await _dbContext.Set<NotificationQueue>()
                .Include(c => c.AccountSender)
                .Include(c => c.NotificationType)
                .Include(c => c.Request)
                    .ThenInclude(r => r.Problem)
                .Where(n => n.Id == notificationId && n.AccountId == accountId)
                .SingleOrDefaultAsync();
            return list;
        }

        public async Task<List<NotificationQueue>> GetNotificationByAccountId(string? sortIsViewed, int page, string accountId, CancellationToken cancellationToken)
        {
            IQueryable<NotificationQueue> list = _dbContext.Set<NotificationQueue>()
                .Include(c => c.AccountSender)
                .Include(c => c.NotificationType)
                .Include(c => c.Request)
                    .ThenInclude(r => r.Problem)
                .Where(n => n.AccountId == accountId);

            if (sortIsViewed == "IsViewedTrue")
            {
                list = list.Where(a =>
                a.IsViewed == true);
            } else if (sortIsViewed == "IsViewedFalse")
            {
                list = list.Where(a =>
                a.IsViewed == false);
            }

            var listNoti = await list
                .Skip((page - 1) * page)
                .Take(10)
                .OrderByDescending(c => c.NotificationTime)
                .ToListAsync(cancellationToken);

            return listNoti;
        }
    }
}
