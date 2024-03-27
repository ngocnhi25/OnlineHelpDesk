using Application.UseCases.Accounts.Commands.UpdateAccount;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace Web_Api.Endpoints.Accounts
{
    public class UpdateAccount : EndpointBaseAsync
        .WithRequest<UpdateAccountCommand>
        .WithActionResult<Result>
    {
        private readonly IMediator Sender;

        public UpdateAccount(IMediator sender)
        {
            Sender = sender;
        }

        [HttpPut("api/accounts/update")]
        [Authorize(Roles = "Administrator")]
        public override async Task<ActionResult<Result>> HandleAsync(
            [FromForm] UpdateAccountCommand command,
            CancellationToken cancellationToken = default)
        {
            var status = await Sender.Send(command);
            return Ok(status);
        }
    }
}
