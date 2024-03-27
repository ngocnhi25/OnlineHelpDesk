using Application.Common.Messaging;
using Domain.Entities.Departments;
using Domain.Repositories;
using SharedKernel;

namespace Application.UseCases.Rooms.Commands.CreateRoom
{
    public sealed class CreateRoomCommandHandler: ICommandHandler<CreateRoomCommand>
    {
        private readonly IUnitOfWorkRepository _repo;

        public CreateRoomCommandHandler(IUnitOfWorkRepository repo)
        {
            _repo = repo;
        }

        public async Task<Result> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            var checkRoom = await _repo.roomRepo.GetRoomByRoomNumber(request.RoomNumber);
            if( checkRoom != null)
            {
                return Result.Failure(new Error("Error.CreateRoom", "This name already exists!"),
                " This name already exists!");
            }

            var department = await _repo.departmentRepo.GetDepartmentById(request.DepartmentId);
            if (department == null)
            {
                return Result.Failure(new Error("Error", "CreateRoomHandler"), "Cannot find Department by Id.");
            }
            if (department!.StatusDepartment == false)
            {
                return Result.Failure(new Error("Error", "CreateRoomHandler"), "Department does not working . Create room Fail!");
            }

            var room = new Room
            {
                DepartmentId = request.DepartmentId,
                RoomNumber = request.RoomNumber,
                RoomStatus = true
            };

            _repo.roomRepo.Add(room);

            try
            {
                await _repo.SaveChangesAsync(cancellationToken);

                return Result.Success("Create Room successfully!");
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Error error = new("Error.RoomCommandHandler", "There is an error saving data!");
                return Result.Failure(error, "Create Room failed!");
            }
        }
    }
}
