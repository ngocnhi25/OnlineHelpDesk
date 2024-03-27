using Application.UseCases.Requests.Queries.GetRequestById;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace Web_Api.Endpoints.Requests
{
    public class GetRequestById : EndpointBaseAsync
        .WithRequest<Guid>
        .WithActionResult<Result>
    {
        private readonly IMediator Sender;

        public GetRequestById(IMediator sender)
        {
            Sender = sender;
        }

        [HttpGet("api/request/{id:guid}")]
        public async override Task<ActionResult<Result>> HandleAsync(
             [FromRoute] Guid id, 
             CancellationToken cancellationToken = default)
        {
            var newRquestQueries = new GetRequestByIdQuery(id);
            var status = await Sender.Send(newRquestQueries);
            return Ok(status);
        }
    }
}
