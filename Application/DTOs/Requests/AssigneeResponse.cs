using System;
using Application.Common.Mapppings;
using Domain.Entities.Accounts;
using Domain.Entities.Requests;

namespace Application.DTOs.Requests
{
	public class AssigneeResponse : IMapForm<ProcessByAssignees>
    {
        public Guid Id { get; set; }
        public Guid RequestId { get; set; }
        public string AccountId { get; set; }

        public RequestDTO Request { get; set; }
    }
}

