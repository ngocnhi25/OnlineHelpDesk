
using Application.Common.Messaging;
using Application.DTOs;
using Application.DTOs.Accounts;
using Application.DTOs.Requests;

namespace Application.UseCases.Assigness.Queries.GetAllAssignees
{
 public sealed record GetAllAssigneesQueries
        (string? SearchTerm, int Page, int Limit)
        : IQuery<PagedList<AccountResponse>>;
}

