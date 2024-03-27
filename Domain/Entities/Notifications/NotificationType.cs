namespace Domain.Entities.Notifications
{
    public class NotificationType
    {
        public int Id { get; set; }
        public string TypeName { get; set; }

        public List<NotificationQueue>? NotificationQueues { get; set; }
    }
}
