using Application.UseCases.Assigness.Queries.GetAllPendingRequestOfAssignee;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace Web_Api.Endpoints.Requests
{
    public class GetAllPendingRequestOfAssignee : EndpointBaseAsync
        .WithRequest<FieldSSFP5>
        .WithActionResult<Result>
    {
        private readonly IMediator Sender;

        public GetAllPendingRequestOfAssignee(IMediator sender)
        {
            Sender = sender;
        }

        [HttpGet("api/request/GetAllPendingRequestOfAssignee/{id}")]
        public async override Task<ActionResult<Result>> HandleAsync(
            [FromQuery]FieldSSFP5 request, CancellationToken cancellationToken = default)
        {
            var status = await Sender.Send
            (new GetAllPendingRequestOfAssigneeQueries(
                request.AccountId,
                 request.SearchTerm, request.SortColumn,
                 request.SortOrder, request.SortStatus, request.Page, request.Limit));
            return Ok(status);
        }
    }
    public class FieldSSFP5
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

