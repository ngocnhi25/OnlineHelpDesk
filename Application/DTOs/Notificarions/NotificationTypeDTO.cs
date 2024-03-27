using Application.Common.Mapppings;
using Domain.Entities.Notifications;

namespace Application.DTOs.Notificarions
{
    public class NotificationTypeDTO : IMapForm<NotificationType>
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
    }
}
