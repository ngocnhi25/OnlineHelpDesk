using System;
using Application.UseCases.Requests.Commands.UpdateRequest;
using Application.UseCases.Rooms.Commands.UpdateStatusRoom;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace Web_Api.Endpoints.Room
{
	public class UpdateStatusRoom : EndpointBaseAsync
        .WithRequest<UpdateStatusRoomCommand>
        .WithActionResult<Result>
    {
        private readonly IMediator Sender;

        public UpdateStatusRoom(IMediator sender)
        {
            Sender = sender;
        }


        [HttpPost("api/room/update_statusRoom")]
        public async override Task<ActionResult<Result>> HandleAsync(
            UpdateStatusRoomCommand request,
            CancellationToken cancellationToken = default)
        {
            var status = await Sender.Send(request);
            return status;
        }
    }
}

