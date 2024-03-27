
using Application.Common.Messaging;
using Application.DTOs;
using Application.DTOs.Requests;

namespace Application.UseCases.Requests.Queries.GetAllRequest
{
    public sealed record GetAllRequestQuery
        (string? SearchTerm, string? SortColumn, string? SortOrder,string? SortStatus ,int Page, int Limit)
        : IQuery<PagedList<RequestDTO>>;
}

