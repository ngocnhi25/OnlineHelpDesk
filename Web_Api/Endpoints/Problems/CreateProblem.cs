using Application.UseCases.Problems.Commands.CreateProblem;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace Web_Api.Endpoints.Problems
{
    public class CreateProblem : EndpointBaseAsync
       .WithRequest<CreateProblemCommand>
       .WithActionResult<Result>
    {

        private readonly IMediator Sender;

        public CreateProblem(IMediator sender)
        {
            Sender = sender;
        }

        [HttpPost("api/problems/create")]
        public override async Task<ActionResult<Result>> HandleAsync(CreateProblemCommand command, CancellationToken cancellationToken = default)
        {
            var status = await Sender.Send(command);
            return Ok(status);
        }
    }
}
