using Application.UseCases.Accounts.Commands.BannedAccount;
using Ardalis.ApiEndpoints;
using Infrastructure.sHubs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SharedKernel;

namespace Web_Api.Endpoints.Accounts
{
    public class BannedAccount : EndpointBaseAsync
        .WithRequest<string>
        .WithActionResult<Result>
    {
        private readonly IMediator Sender;
        public readonly IHubContext<BannedHub> _hubContext;

        public BannedAccount(IMediator sender, IHubContext<BannedHub> hubContext)
        {
            Sender = sender;
            _hubContext = hubContext;
        }


        //[Authorize(Roles = "Administrator")]
        [HttpPut("api/accounts/statusAccount/{accountId}/banned")]
        public override async Task<ActionResult<Result>> HandleAsync(
            [FromRoute(Name = "accountId")] string accountId,
            CancellationToken cancellationToken = default)
        {
            var status = await Sender.Send(new BannedAccountCommand(accountId));
            if (status != null && status.IsSuccess == true)
            {
                await _hubContext.Clients.Group(accountId).SendAsync("LogoutAccountWhenBanned", "Your account is banned from the website");

            }
            return Ok(status);
        }
    }
}
