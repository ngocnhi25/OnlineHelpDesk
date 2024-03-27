using Application.UseCases.Accounts.Commands.DeleteAccount;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace Web_Api.Endpoints.Accounts
{
    public class DeleteAccount : EndpointBaseAsync
        .WithRequest<string>
        .WithActionResult<Result>
    {
        private readonly IMediator Sender;

        public DeleteAccount(IMediator sender)
        {
            Sender = sender;
        }

        [HttpDelete("api/accounts/delete/{accountId}")]
        [Authorize(Roles = "Administrator")]
        public override async Task<ActionResult<Result>> HandleAsync(
            [FromRoute(Name = "accountId")] string accountId,
            CancellationToken cancellationToken = default)
        {
            var status = await Sender.Send(new DeleteAccountCommand(accountId));
            return Ok(status);
        }
    }
}
