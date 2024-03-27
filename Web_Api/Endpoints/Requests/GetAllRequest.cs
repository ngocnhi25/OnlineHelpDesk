using Application.UseCases.Requests.Queries.GetAllRequest;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace Web_Api.Endpoints.Requests
{
    public class GetAllRequest : EndpointBaseAsync
        .WithRequest<FieldSSFP2>
        .WithActionResult<Result>
    {
        private readonly IMediator Sender;

        public GetAllRequest(IMediator sender)
        {
            Sender = sender;
        }

        [HttpGet("api/request/getAll")]
        public async override Task<ActionResult<Result>> HandleAsync(
            [FromQuery] FieldSSFP2 request,
            CancellationToken cancellationToken = default)
        {
            var status = await Sender.Send
                (new GetAllRequestQuery(
                request.SearchTerm, request.SortColumn,
                request.SortOrder,request.SortStatus ,request.Page, request.Limit));
            return Ok(status);
        }
    }

    public class FieldSSFP2
    {
        public string? SearchTerm { get; set; }
        public string? SortColumn { get; set; }
        public string? SortOrder { get; set; }
        public string? SortStatus { get; set; }
        public int Page { get; set; }
        public int Limit { get; set; }
    }
}

