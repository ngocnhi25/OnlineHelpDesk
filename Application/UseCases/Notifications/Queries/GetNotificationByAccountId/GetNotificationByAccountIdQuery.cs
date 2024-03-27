using Application.Common.Messaging;
using Application.DTOs.Notificarions;

namespace Application.UseCases.Notifications.Queries.GetNotificationByAccountId
{
    public sealed record GetNotificationByAccountIdQuery(string? SortIsViewed, int Page, string? AccountId) : IQuery<List<NotificationQueueDTO>>;
}
