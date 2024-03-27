using Domain.Entities.Accounts;
using System.Text.Json.Serialization;

namespace Domain.Entities.Requests
{
    public class NotificationRemark
    {
        public Guid Id { get; set; }
        public Guid RequestId { get; set; }
        public string AccountId { get; set; }
        public bool IsSeen { get; set; }
        public int Unwatchs {  get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        [JsonIgnore]
        public Account Account { get; set; }
        [JsonIgnore]
        public Request Request { get; set; }
    }
}
