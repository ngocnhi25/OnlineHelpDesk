using Application.UseCases.Requests.Commands.CreateRequest;
using Application.UseCases.Rooms.Commands.CreateRoom;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace Web_Api.Endpoints.Room
{
    public class CreateRoom: EndpointBaseAsync
        .WithRequest<CreateRoomCommand>
        .WithActionResult<Result>
    {

        private readonly IMediator Sender;

        public CreateRoom(IMediator sender)
        {
            Sender = sender;
        }

        [HttpPost("api/room/create_room")]
        public override async Task<ActionResult<Result>> HandleAsync(CreateRoomCommand command, CancellationToken cancellationToken = default)
        {
            var status = await Sender.Send(command);
            return Ok(status);
        }
    }
}
