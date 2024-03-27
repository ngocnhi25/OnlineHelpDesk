using Application.Common.Messaging;

namespace Application.UseCases.Departments.Commands.CreateDepartment
{
    public record class CreateDepartmentCommand(string DepartmentName): ICommand;
}
