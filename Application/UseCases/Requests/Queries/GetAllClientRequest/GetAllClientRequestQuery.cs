using Application.Common.Messaging;
using Application.DTOs.Requests;
using Application.DTOs;

namespace Application.UseCases.Requests.Queries.GetAllClientRequest
{
    public sealed record GetAllClienRequestQueries(string? AccountId ,
        string? FCondition, string? SCondition, string? TCondition,
        string? SearchTerm, string? SortColumn, string? SortOrder, int Page, int Limit) :
        IQuery<PagedList<RequestDTO>>;
}
