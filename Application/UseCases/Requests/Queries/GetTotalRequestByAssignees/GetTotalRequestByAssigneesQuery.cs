using System;
using Application.Common.Messaging;
using Application.DTOs.Requests;

namespace Application.UseCases.Requests.Queries.GetTotalRequestByAssignees
{
	public sealed record GetTotalRequestByAssigneesQuery : IQuery<CountRequestPage>
	{
        public string? AccountId { get; set; }
    }
}

