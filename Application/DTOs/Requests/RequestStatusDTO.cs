using Application.Common.Mapppings;
using Domain.Entities.Requests;

namespace Application.DTOs.Requests
{
    public class RequestStatusDTO : IMapForm<RequestStatus>
    {
        public int Id { get; set; }
        public string StatusName { get; set; }
        public string ColorCode { get; set; }
    }
}

