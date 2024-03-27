using Application.UseCases.Accounts.Commands.ActiveAccount;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace Web_Api.Endpoints.Accounts
{
    public class ActiveAccount : EndpointBaseAsync
       .WithRequest<string>
       .WithActionResult<Result>
    {
        private readonly IMediator Sender;

        public ActiveAccount(IMediator sender)
        {
            Sender = sender;
        }

        //[Authorize(Roles = "Administrator")]
        [HttpPut("api/accounts/statusAccount/{accountId}/active")]
        public override async Task<ActionResult<Result>> HandleAsync(
            [FromRoute(Name = "accountId")] string accountId,
            CancellationToken cancellationToken = default)
        {
            var status = await Sender.Send(new ActiveAccountCommand(accountId));
            return Ok(status);
        }
    }
}
