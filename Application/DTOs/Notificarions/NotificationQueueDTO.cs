using Application.Common.Mapppings;
using Application.DTOs.Accounts;
using Application.DTOs.Requests;
using Domain.Entities.Notifications;

namespace Application.DTOs.Notificarions
{
    public class NotificationQueueDTO : IMapForm<NotificationQueue>
    {
        public Guid Id { get; set; }
        public int NotificationTypeId { get; set; }
        public Guid RequestId { get; set; }
        public string NotificationTitle { get; set; }
        public bool IsViewed { get; set; }
        public DateTime NotificationTime { get; set; }
        public DateTime? ViewedTime { get; set; }
        public NotificationTypeDTO? NotificationType { get; set; }
        public AccountSenderDTO? AccountSender { get; set; }
        public RequestDTO? Request { get; set; }
    }
}
