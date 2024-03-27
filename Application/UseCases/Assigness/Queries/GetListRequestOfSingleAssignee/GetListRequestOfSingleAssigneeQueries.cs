    using System;
    using Application.Common.Messaging;
    using Application.DTOs;
    using Application.DTOs.Accounts;
    using Application.DTOs.Requests;

    namespace Application.UseCases.Requests.Queries.GetListRequestOfSingleAssignee
    {
        public sealed record GetListRequestOfSingleAssigneeQueries
       (
           string AccountId ,
           string SearchTerm,
           string SortColumn,
           string SortOrder,
           string SortStatus,
           int Page,
           int Limit
       ) : IQuery<PagedList<AssigneeResponse>>;
    }

