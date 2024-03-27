using Application.UseCases.Accounts.Commands.Verify;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace Web_Api.Endpoints.Auth
{
    public class Verify : EndpointBaseAsync
        .WithRequest<VerifyCommand>
        .WithActionResult<Result>
    {

        private readonly IMediator Sender;

        public Verify(IMediator sender)
        {
            Sender = sender;
        }


        [HttpPost("api/auth/verify")]
        public override async Task<ActionResult<Result>> HandleAsync(
            VerifyCommand command,
            CancellationToken cancellationToken = default)
        {
            var status = await Sender.Send(command);
            return Ok(status);
        }
    }
}
