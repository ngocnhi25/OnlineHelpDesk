using Domain.Entities.Accounts;
using System.Text.Json.Serialization;

namespace Domain.Entities.Requests
{
    public class ProcessByAssignees
    {
        public Guid Id { get; set; }
        public Guid RequestId { get; set; }
        public string AccountId { get; set; }

        [JsonIgnore]
        public Account? Account { get; set; }
        [JsonIgnore]
        public Request? Request { get; set; }
    }
}
