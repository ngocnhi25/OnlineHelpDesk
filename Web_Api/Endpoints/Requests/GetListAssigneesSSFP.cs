using Application.UseCases.Assigness.Queries.GetAllAssignees;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace Web_Api.Endpoints.Requests
{
    public class GetListAssigneesSSFP : EndpointBaseAsync
        .WithRequest<FieldSSFP02>
        .WithActionResult<Result>
    {
        private readonly IMediator Sender;

        public GetListAssigneesSSFP(IMediator sender)
        {
            Sender = sender;
        }

        [HttpGet("api/Assignees/GetListAssignees")]
        [Authorize(Roles = "Facility-Heads")]
        public async override Task<ActionResult<Result>> HandleAsync(
            [FromQuery] FieldSSFP02 request,
            CancellationToken cancellationToken = default)
        {
            var status = await Sender.Send(new GetAllAssigneesQueries
                (request.SearchTerm, request.Page, request.Limit));
            return Ok(status);
        }

    }

    public class FieldSSFP02
    {
        public string? SearchTerm { get; set; }
        public int Page { get; set; }
        public int Limit { get; set; }
    }
}

