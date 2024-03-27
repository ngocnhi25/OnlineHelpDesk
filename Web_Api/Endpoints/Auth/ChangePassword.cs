using Application.UseCases.Accounts.Commands.ChangePassword;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace Web_Api.Endpoints.Auth
{
    public class ChangePassword : EndpointBaseAsync
        .WithRequest<ChangePasswordCommand>
        .WithActionResult<Result>
    {

        private readonly IMediator Sender;

        public ChangePassword(IMediator sender)
        {
            Sender = sender;
        }


        [HttpPost("api/auth/change-password")]
        public override async Task<ActionResult<Result>> HandleAsync(
            ChangePasswordCommand command,
            CancellationToken cancellationToken = default)
        {
            var status = await Sender.Send(command);
            return Ok(status);
        }
    }
}
