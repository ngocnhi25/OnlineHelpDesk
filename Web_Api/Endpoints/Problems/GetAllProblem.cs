using Application.UseCases.Problems.Queries.GetAllProblem;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace Web_Api.Endpoints.Problems
{
    public class GetAllProblem : EndpointBaseAsync
        .WithRequest<GetAllProblemQuery>
        .WithActionResult<Result>
    {
        private readonly IMediator Sender;

        public GetAllProblem(IMediator sender)
        {
            Sender = sender;
        }

        [HttpGet("api/problems")]
        [Authorize]
        public async override Task<ActionResult<Result>> HandleAsync(
            [FromQuery] GetAllProblemQuery request,
            CancellationToken cancellationToken = default)
        {
            var status = await Sender.Send(request);
            return Ok(status);
        }
    }
}
