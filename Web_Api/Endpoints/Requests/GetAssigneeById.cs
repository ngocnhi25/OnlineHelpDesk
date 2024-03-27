
using Application.UseCases.Assigness.Queries.GetAssigneeById;
using Application.UseCases.Requests.Queries.GetRequestById;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace Web_Api.Endpoints.Requests
{
    public class GetAssigneeById : EndpointBaseAsync
        .WithRequest<string>
        .WithActionResult<Result>
    {

        private readonly IMediator Sender;

        public GetAssigneeById(IMediator sender)
        {
            Sender = sender;
        }

        [HttpGet("api/Assignees/{id}")]
        public async override Task<ActionResult<Result>> HandleAsync(
            [FromRoute]string id,
            CancellationToken cancellationToken = default)
        {
            var getAssigneeByIdQueries = new GetAssigneeByIdQueries { AccountId = id };
            var status = await Sender.Send(getAssigneeByIdQueries);
            return Ok(status);
        }
    }
}

