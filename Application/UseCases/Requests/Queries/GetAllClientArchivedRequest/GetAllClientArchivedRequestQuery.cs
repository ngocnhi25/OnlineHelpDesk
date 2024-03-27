using Application.Common.Messaging;
using Application.DTOs.Requests;
using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Requests.Queries.GetAllClientArchivedRequest
{
    public sealed record GetAllClientArchivedRequestQuery(string? AccountId,
        string? FCondition, string? SCondition, string? TCondition,
        string? SearchTerm, string? SortColumn, string? SortOrder, int Page, int Limit) :
         IQuery<PagedList<RequestDTO>>;
}
