using Application.Common.Mapppings;
using Application.DTOs.Accounts;
using Application.DTOs.Departments;
using Application.DTOs.Remarks;
using Domain.Entities.Requests;

namespace Application.DTOs.Requests
{
    public class RequestDTO : IMapForm<Request>
	{
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string SeveralLevel { get; set; }
        public int RequestStatusId { get; set; }
        public string Reason { get; set; }
        public Boolean Enable { get; set; }
        public DateTime? Date { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public RoomDTO Room { get; set; }
        public ProblemDTO Problem { get; set; }
        public RequestStatusDTO RequestStatus { get; set; }
        public AccountDTO Account { get; set; }
        public List<ProcessByAssigneesDTO> ProcessByAssignees { get; set; }
        public List<RemarkDTO>? Remarks { get; set; }
    }
}
