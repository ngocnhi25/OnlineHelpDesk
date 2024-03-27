using Application.UseCases.Accounts.Commands.StaySignIn;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace Web_Api.Endpoints.Auth
{
    public class StaySignedIn : EndpointBaseAsync
        .WithRequest<StaySignInCommand>
        .WithActionResult<Result>
    {

        private readonly IMediator Sender;

        public StaySignedIn(IMediator sender)
        {
            Sender = sender;
        }


        [HttpPut("api/auth/stay-sign-in")]
        public override async Task<ActionResult<Result>> HandleAsync(
            StaySignInCommand command,
            CancellationToken cancellationToken = default)
        {
            var status = await Sender.Send(command);
            return Ok(status);
        }
    }
}
