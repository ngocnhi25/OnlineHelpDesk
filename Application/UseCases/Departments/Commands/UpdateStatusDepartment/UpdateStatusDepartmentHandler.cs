using System;
using Application.Common.Messaging;
using Application.UseCases.Rooms.Commands.UpdateStatusRoom;
using Domain.Repositories;
using SharedKernel;

namespace Application.UseCases.Departments.Commands.UpdateStatusDepartment
{
    public class UpdateStatusDepartmentHandler
        : ICommandHandler<UpdateStatusDepartmentCommand>
    {
        private readonly IUnitOfWorkRepository _repo;

        public UpdateStatusDepartmentHandler(IUnitOfWorkRepository repo)
        {
            _repo = repo;
        }

        public async Task<Result> Handle(UpdateStatusDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = await _repo.departmentRepo.GetDepartmentById(request.Id);
            if (department == null)
            {
                return Result.Failure(new Error("Error", "No data exists"), "Cannot find Department by Id.");
            }

            var result = await _repo.departmentRepo.CountRoomInActive(request.Id);
            if( result > 0)
            {
                return Result.Failure(new Error("Error", "UpdateStatusDepartment")
                    , "Before changing the status department, please check the room status");
            }


            // Toggle the RoomStatus
           department.StatusDepartment = !department.StatusDepartment;

            _repo.departmentRepo.Update(department);
            await _repo.SaveChangesAsync(cancellationToken);

            return Result.Success("Update successfully!");
        }
    }
}

