

using Application.Common.Messaging;

namespace Application.UseCases.Rooms.Commands.UpdateStatusRoom
{
	public sealed record UpdateStatusRoomCommand(
       Guid Id , Boolean statusRoom) : ICommand;
}

