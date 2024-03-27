using Application.Common.Messaging;
using Domain.Entities.Departments;
using Domain.Repositories;
using SharedKernel;

namespace Application.UseCases.Departments.Commands.CreateDepartment
{
    public sealed class CreateDepartmentCommandHandler : ICommandHandler<CreateDepartmentCommand>
    {

        private readonly IUnitOfWorkRepository _repo;

        public CreateDepartmentCommandHandler(IUnitOfWorkRepository repo)
        {
            _repo = repo;
        }

        public async Task<Result> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var check = await _repo.departmentRepo.GetDepartmentByName(request.DepartmentName);
            if( check != null)
            {
                return Result.Failure(new Error("Error.CreateDepartment", "This name already exists!"),
                 " This name already exists!");
            }

            var department = new Department
            {
                DepartmentName = request.DepartmentName,
                StatusDepartment = true
            };

            _repo.departmentRepo.Add(department);

            try
            {
                await _repo.SaveChangesAsync(cancellationToken);
                return Result.Success("Create Department successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Error error = new("Error.DepartmentHandler", "There is an error saving data!");
                return Result.Failure(error, "Create department failed!");
            }
        }
    }
}
