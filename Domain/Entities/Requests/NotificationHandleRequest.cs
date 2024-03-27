using Domain.Entities.Accounts;
using System.Text.Json.Serialization;

namespace Domain.Entities.Requests
{
    public class NotificationHandleRequest
    {
        public Guid Id { get; set; }
        public Guid RequestId { get; set; }
        public string AccountId { get; set; }
        public string Purpose { get; set; }
        public bool IsSeen { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        [JsonIgnore]
        public Account Account { get; set; }
        [JsonIgnore]
        public Request Request { get; set; }
    }
}
