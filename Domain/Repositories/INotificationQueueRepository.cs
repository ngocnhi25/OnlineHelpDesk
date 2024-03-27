using Domain.Entities.Notifications;

namespace Domain.Repositories
{
    public interface INotificationQueueRepository : IGenericRepository<NotificationQueue>
    {

        Task<NotificationQueue> GetNotificationCreatedRequest(Guid notificationId, string accountId);
        Task<List<NotificationQueue>> GetNotificationByAccountId(string? sortIsViewed, int page, string accountId, CancellationToken cancellationToken);
    }
}
