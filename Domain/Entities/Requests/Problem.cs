using System.Text.Json.Serialization;

namespace Domain.Entities.Requests
{
    public class Problem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsDisplay { get; set; }
        public List<Request>? Requests { get; set; }
    }
}
