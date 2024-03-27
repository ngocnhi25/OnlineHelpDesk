using Domain.Entities.Accounts;
using Domain.Entities.Departments;
using Domain.Entities.Notifications;
using System.Text.Json.Serialization;

namespace Domain.Entities.Requests
{
    public class Request
    {
        public Guid Id { get; set; }
        public string AccountId { get; set; }
        public Guid RoomId { get; set; }
        public int RequestStatusId { get; set; }
        public int ProblemId { get; set; }
        public string Description {  get; set; }
        public string SeveralLevel { get; set; }
        public string? Reason { get; set; }
        public Boolean Enable { get; set; }
        public DateTime? Date {  get; set; }
        public DateTime CreatedAt { get; set;}
        public DateTime? UpdateAt { get; set;}

        [JsonIgnore]
        public Account? Account { get; set; }
        [JsonIgnore]
        public Room? Room { get; set; }
        [JsonIgnore]
        public RequestStatus? RequestStatus { get; set; } 
        [JsonIgnore]
        public Problem? Problem { get; set; }

        public List<ProcessByAssignees>? ProcessByAssignees { get; set; }
        public List<Remark>? Remarks { get; set; }
        public List<NotificationRemark>? NotificationRemarks { get; set; }
        public List<NotificationHandleRequest>? NotificationHandleRequests { get; set; }
        public List<NotificationQueue>? NotificationQueues { get; set; }
    }
}
