using System;
using Application.Common.Messaging;
using Application.DTOs;
using Application.DTOs.Requests;

namespace Application.UseCases.Assigness.Queries.GetAllPendingRequestOfAssignee
{
	public sealed record GetAllPendingRequestOfAssigneeQueries(
        string AccountId,
        string? SearchTerm, string? SortColumn, string? SortOrder,
        string? SortStatus, int Page, int Limit)
        : IQuery<PagedList<RequestDTO>>;
}

