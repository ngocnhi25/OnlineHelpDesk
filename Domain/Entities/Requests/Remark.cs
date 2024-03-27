using Domain.Entities.Accounts;
using System.Text.Json.Serialization;

namespace Domain.Entities.Requests
{
    public class Remark
    {
        public Guid Id { get; set; }
        public Guid RequestId { get; set; }
        public string AccountId { get; set; }
        public string Comment { get; set; }
        public DateTime CreateAt { get; set; }
        public bool Enable { get; set; }

        public Account? Account { get; set; }
        public Request? Request { get; set; }
    }
}
