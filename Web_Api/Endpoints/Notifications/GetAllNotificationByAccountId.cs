using Application.UseCases.Notifications.Queries.GetNotificationByAccountId;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using System.Security.Claims;

namespace Web_Api.Endpoints.Notifications
{
    public class GetAllNotificationByAccountId : EndpointBaseAsync
       .WithRequest<FieldGetNotification>
       .WithActionResult<Result>
    {
        private readonly IMediator Sender;

        public GetAllNotificationByAccountId(IMediator sender)
        {
            Sender = sender;
        }

        [HttpGet("api/notifications/getByAccountId")]
        [Authorize]
        public async override Task<ActionResult<Result>> HandleAsync(
             [FromQuery] FieldGetNotification request,
             CancellationToken cancellationToken = default)
        {
            var accountId = User.FindFirstValue(ClaimTypes.Sid);
            var status = await Sender.Send(new GetNotificationByAccountIdQuery(request.SortIsViewed, request.Page, accountId));
            return Ok(status);
        }
    }

    public class FieldGetNotification
    {
        public string? SortIsViewed { get; set; }
        public int Page { get; set; }
    }
}
