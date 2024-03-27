using System;
using Application.Common.Messaging;

namespace Application.UseCases.Departments.Commands.UpdateStatusDepartment
{
	public sealed record UpdateStatusDepartmentCommand(
       Guid Id) : ICommand;

}

