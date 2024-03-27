using Application.UseCases.Assigness.Queries.GetAssigneeById;
using Application.UseCases.Requests.Queries.GetTotalRequestByAssignees;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace Web_Api.Endpoints.Requests
{
    public class GetTotalRequestByAssignees : EndpointBaseAsync
        .WithRequest<string>
        .WithActionResult<Result>
    {
        private readonly IMediator Sender;

        public GetTotalRequestByAssignees(IMediator sender)
        {
            Sender = sender;
        }

        [HttpGet("api/Request/GetTotalRequestByAssignees/{id}")]
        public async override Task<ActionResult<Result>> HandleAsync(
           [FromRoute] string id,
            CancellationToken cancellationToken = default)
        {
            var getTotalRequestByAssignees = new GetTotalRequestByAssigneesQuery { AccountId = id };
            var status = await Sender.Send(getTotalRequestByAssignees);
            return Ok(status);
        }
    }
}

