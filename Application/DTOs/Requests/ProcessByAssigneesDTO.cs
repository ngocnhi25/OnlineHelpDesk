using Application.Common.Mapppings;
using Application.DTOs.Accounts;
using Domain.Entities.Requests;

namespace Application.DTOs.Requests
{
    public class ProcessByAssigneesDTO : IMapForm<ProcessByAssignees>
    {
        public Guid Id { get; set; }
        public Guid RequestId { get; set; }
        public AccountDTO Account { get; set; }
    }
}

