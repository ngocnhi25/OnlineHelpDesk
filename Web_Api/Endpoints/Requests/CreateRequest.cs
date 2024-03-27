using Application.UseCases.Requests.Commands.CreateRequest;
using Ardalis.ApiEndpoints;
using Infrastructure.sHubs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SharedKernel;

namespace Web_Api.Endpoints.Requests
{
    public class CreateRequest : EndpointBaseAsync
     .WithRequest<CreateRequestCommand>
     .WithActionResult<Result>
    {

        private readonly IMediator Sender;
        public readonly IHubContext<NotificationHub> _hubContext;

        public CreateRequest(IMediator sender, IHubContext<NotificationHub> hubContext)
        {
            Sender = sender;
            _hubContext = hubContext;
        }


        [HttpPost("api/requests/create")]
        [Authorize(Roles = "End-Users")]
        public override async Task<ActionResult<Result>> HandleAsync(CreateRequestCommand command, CancellationToken cancellationToken = default)
        {
            var status = await Sender.Send(command);
            return status;
        }
    }
}