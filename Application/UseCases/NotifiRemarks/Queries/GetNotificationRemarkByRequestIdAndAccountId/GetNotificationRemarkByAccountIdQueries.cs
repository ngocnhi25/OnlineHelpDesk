using Application.Common.Messaging;
using Domain.Entities.Requests;

namespace Application.UseCases.NotifiRemark.Queries.GetNotificationRemarkByRequestIdAndAccountId
{
    public sealed record GetNotificationRemarkByAccountIdQueries(string? AccountId) : IQuery<List<NotificationRemark>>
    {
    }

}
