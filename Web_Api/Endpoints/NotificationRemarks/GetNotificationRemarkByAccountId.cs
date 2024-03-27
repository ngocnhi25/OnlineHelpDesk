using Application.UseCases.NotifiRemark.Queries.GetNotificationRemarkByRequestIdAndAccountId;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using System.Security.Claims;

namespace Web_Api.Endpoints.NotificationRemarks
{
    public class GetNotificationRemarkByAccountId : EndpointBaseAsync
        .WithRequest<GetNotificationRemarkByAccountIdQueries>
        .WithActionResult<Result>
    {
        private readonly IMediator Sender;

        public GetNotificationRemarkByAccountId(IMediator sender)
        {
            Sender = sender;
        }
        [HttpGet("api/notiRemarkByAccountId")]
        [Authorize]
        public async override Task<ActionResult<Result>> HandleAsync(
             [FromHeader] GetNotificationRemarkByAccountIdQueries request,
             CancellationToken cancellationToken = default)
        {
            var accountId = User.FindFirstValue(ClaimTypes.Sid);
            var status = await Sender.Send(new GetNotificationRemarkByAccountIdQueries(accountId));
            return Ok(status);
        }
    }
}
