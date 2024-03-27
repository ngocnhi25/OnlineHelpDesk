using Application.UseCases.NotifiRemarks.Command;
using Application.UseCases.Requests.Commands.UpdateRequest;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace Web_Api.Endpoints.NotificationRemarks
{
    public class UpdateUnwatchsSeenOnNotifiRemark : EndpointBaseAsync
        .WithRequest<UpdateUnwatchsSeenOnNotifiRemarkCommand>
        .WithActionResult<Result>
    {
        private readonly IMediator Sender;

        public UpdateUnwatchsSeenOnNotifiRemark(IMediator sender)
        {
            Sender = sender;
        }

        [HttpPost("api/noti/update_notiremark")]
        [Authorize]
        public async override Task<ActionResult<Result>> HandleAsync(
            UpdateUnwatchsSeenOnNotifiRemarkCommand request
            ,CancellationToken cancellationToken = default)
        {
            var status = await Sender.Send(request);
            return status;
        }
    }
}
