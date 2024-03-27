
using Application.UseCases.Assigness.Queries.GetTotalRequest;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace Web_Api.Endpoints.Requests
{
	public class GetTotalRequest : EndpointBaseAsync
        .WithRequest<GetTotalRequestQuery>
        .WithActionResult<Result>
	{

        private readonly IMediator Sender;

        public GetTotalRequest(IMediator sender)
        {
            Sender = sender;
        }

        [HttpGet("api/Request/GetTotalRequest")]
        public async override Task<ActionResult<Result>> HandleAsync(
           [FromQuery] GetTotalRequestQuery request,
            CancellationToken cancellationToken = default)
        {
            var status = await Sender.Send(request);
            return Ok(status);
        }

    }
}

