using Application.UseCases.Accounts.Commands.Login;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace Web_Api.Endpoints.Auth
{
    public class Login : EndpointBaseAsync
        .WithRequest<LoginCommand>
        .WithActionResult<Result>
    {

        private readonly IMediator Sender;

        public Login(IMediator sender)
        {
            Sender = sender;
        }


        [HttpPost("api/auth/login")]
        public override async Task<ActionResult<Result>> HandleAsync(
            LoginCommand command,
            CancellationToken cancellationToken = default)
        {
            var status = await Sender.Send(command);
            return Ok(status);
        }
    }
}
