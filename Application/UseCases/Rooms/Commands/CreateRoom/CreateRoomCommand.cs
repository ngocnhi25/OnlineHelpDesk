using Application.Common.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Rooms.Commands.CreateRoom
{
    public record class CreateRoomCommand
        (Guid DepartmentId, string RoomNumber) :ICommand;
}
