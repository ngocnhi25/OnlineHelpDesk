using Application.UseCases.Requests.Queries.GetAllPendingRequest;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace Web_Api.Endpoints.Requests
{
    public class GetAllPendingRequest : EndpointBaseAsync
        .WithRequest<FieldSSFP3>
        .WithActionResult<Result>
    {
        private readonly IMediator Sender;

        public GetAllPendingRequest(IMediator sender)
        {
            Sender = sender;
        }

        [HttpGet("api/request/GetAllPendingRequest")]
        public async override Task<ActionResult<Result>> HandleAsync(
            [FromQuery]FieldSSFP3 request,
            CancellationToken cancellationToken = default)
        {
            var status = await Sender.Send
            (new GetAllPendingRequestQuery(
                request.SearchTerm, request.SortColumn,
                request.SortOrder, request.SortStatus, request.Page, request.Limit));
            return Ok(status);
        }
    }

    public class FieldSSFP3
    {
        public string? SearchTerm { get; set; }
        public string? SortColumn { get; set; }
        public string? SortOrder { get; set; }
        public string? SortStatus { get; set; }
        public int Page { get; set; }
        public int Limit { get; set; }
    }
}

