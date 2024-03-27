using Application.UseCases.Requests.Queries.GetAllClientRequest;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using System.Security.Claims;

namespace Web_Api.Endpoints.Requests
{
    public class GetAllClientRequest : EndpointBaseAsync
        .WithRequest<FieldSSFPClient>
        .WithActionResult<Result>
    {
        private readonly IMediator Sender;

        public GetAllClientRequest(IMediator sender)
        {
            Sender = sender;
        }

        [HttpGet("api/request")]
        [Authorize(Roles = "End-Users")]
        public async override Task<ActionResult<Result>> HandleAsync(
            [FromQuery] FieldSSFPClient request,
            CancellationToken cancellationToken = default)
        {
            var accountId = User.FindFirstValue(ClaimTypes.Sid);
            var status = await Sender.Send
                (new GetAllClienRequestQueries(
                accountId,
                request.FCondition, request.SCondition, request.TCondition,
                request.SearchTerm, request.SortColumn,
                request.SortOrder, request.Page, request.Limit));
            return Ok(status);
        }
    }
    public class FieldSSFPClient
    {
        public string? FCondition { get; set; }
        public string? SCondition { get; set; }
        public string? TCondition { get; set; }
        public string? SearchTerm { get; set; }
        public string? SortColumn { get; set; }
        public string? SortOrder { get; set; }
        public int Page { get; set; }
        public int Limit { get; set; }
    }

}
