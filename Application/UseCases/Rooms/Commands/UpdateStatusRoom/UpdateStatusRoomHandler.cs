using Application.Common.Messaging;
using Domain.Repositories;
using SharedKernel;

namespace Application.UseCases.Rooms.Commands.UpdateStatusRoom
{
    public sealed class UpdateStatusRoomHandler : ICommandHandler<UpdateStatusRoomCommand>
    {
        private readonly IUnitOfWorkRepository _repo;

        public UpdateStatusRoomHandler(IUnitOfWorkRepository repo)
        {
            _repo = repo;
        }

        public async Task<Result> Handle(UpdateStatusRoomCommand request, CancellationToken cancellationToken)
        {
            var room = await _repo.roomRepo.GetRoomById(request.Id);
            if (room == null)
            {
                return Result.Failure(new Error("Error", "UpdateStatusRoomHandler"), "Cannot find Room by Id.");
            }

            var department = await _repo.departmentRepo.GetDepartmentById(room.DepartmentId);
            if( department == null)
            {
                return Result.Failure(new Error("Error", "UpdateStatusRoomHandler"), "Cannot find Department by Id.");
            }
            if( department!.StatusDepartment == false)
            {
                return Result.Failure(new Error("Error", "UpdateStatusRoomHandler"), "Department does not working . Change Status Room Fail !!");
            }

            // Toggle the RoomStatus
            room.RoomStatus = !room.RoomStatus;

            _repo.roomRepo.Update(room);
            await _repo.SaveChangesAsync(cancellationToken);

            return Result.Success("Update Status Room successfully!");
        }
    }
}
