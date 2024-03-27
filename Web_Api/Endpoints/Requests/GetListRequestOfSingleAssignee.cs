using System.Collections.Generic;
using Application.UseCases.Requests.Queries.GetListRequestOfSingleAssignee;
using Application.UseCases.Requests.Queries.GetRequestById;
using Ardalis.ApiEndpoints;
using MailKit.Search;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using static Web_Api.Endpoints.Requests.GetListRequestOfSingleAssignee;

namespace Web_Api.Endpoints.Requests
{
    public class GetListRequestOfSingleAssignee : EndpointBaseAsync
        .WithRequest<ListRequestAssigneeSSFP>
        .WithActionResult<Result>
    {
        private readonly IMediator Sender;

        public GetListRequestOfSingleAssignee(IMediator sender)
        {
            Sender = sender;
        }

        [HttpGet("api/Assignee/ListRequestOfAssignee/{id}")]
        public async override Task<ActionResult<Result>> HandleAsync(
           [FromQuery] ListRequestAssigneeSSFP request ,
            CancellationToken cancellationToken = default)
        {
            var newRquestQueries = new GetListRequestOfSingleAssigneeQueries(
                request.AccountId,
                request.SearchTerm!, request.SortColumn!,
                request.SortOrder!, request.SortStatus,request.Page, request.Limit);
            var status = await Sender.Send(newRquestQueries);
            return Ok(status);
        }

        public class ListRequestAssigneeSSFP
        {
            [FromRoute(Name = "id")]
            public string AccountId { get; set; }
            public string? SearchTerm { get; set; }
            public string? SortColumn { get; set; }
            public string? SortOrder { get; set; }
            public string? SortStatus { get; set; }
            public int Page { get; set; }
            public int Limit { get; set; }
        }
    }
}

