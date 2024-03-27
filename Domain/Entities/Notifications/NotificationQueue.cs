using Domain.Entities.Accounts;
using Domain.Entities.Requests;
using System.Text.Json.Serialization;

namespace Domain.Entities.Notifications
{
    public class NotificationQueue
    {
        public Guid Id { get; set; }
        public int NotificationTypeId { get; set; }
        public string AccountId { get; set; }
        public string? AccountSenderId { get; set; }
        public Guid RequestId { get; set; }
        public string NotificationTitle { get; set; }
        public bool IsViewed { get; set; }
        public DateTime NotificationTime { get; set; }
        public DateTime? ViewedTime { get; set; }
        [JsonIgnore]
        public NotificationType? NotificationType { get; set; }
        [JsonIgnore]
        public Account? Account { get; set; }
        [JsonIgnore]
        public Account? AccountSender { get; set; }
        [JsonIgnore]
        public Request? Request { get; set; }
    }
}
