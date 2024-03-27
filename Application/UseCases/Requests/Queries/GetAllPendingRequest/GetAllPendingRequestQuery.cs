using System;
using Application.Common.Messaging;
using Application.DTOs;
using Application.DTOs.Requests;

namespace Application.UseCases.Requests.Queries.GetAllPendingRequest
{
	public sealed record GetAllPendingRequestQuery
        (string? SearchTerm, string? SortColumn, string? SortOrder,
        string? SortStatus, int Page, int Limit)
        : IQuery<PagedList<RequestDTO>>;
}

