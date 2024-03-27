using Application.Common.Messaging;
using Application.DTOs;
using Application.DTOs.Requests;

namespace Application.UseCases.Requests.Queries.GetAllRequestOfAssigneeProcessing
{
    public sealed record GetAllRequestOfAssigneeProcessingQuery(
        string AccountIdAssignees,
        string? SearchTerm,
        string? SortColumn,
        string? SortOrder,
        string? Department,
        string? Room,
        string? SeveralLevel,
        string? Status,
        int Page,
        int Limit
     ) : IQuery<PagedList<RequestDTO>>;
}
