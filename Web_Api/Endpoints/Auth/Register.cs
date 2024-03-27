using Application.UseCases.Accounts.Commands.Register;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace Web_Api.Endpoints.Auth
{
    public class Register : EndpointBaseAsync
        .WithRequest<RegisterCommand>
        .WithActionResult<Result>
    {
        private readonly IMediator Sender;

        public Register(IMediator sender)
        {
            Sender = sender;
        }

        [HttpPost("api/auth/register")]
        [Authorize(Roles = "Administrator")]
        public override async Task<ActionResult<Result>> HandleAsync(
            [FromForm] RegisterCommand command, 
            CancellationToken cancellationToken = default)
        {
            var status = await Sender.Send(command);
            return Ok(status);
        }
    }
}
