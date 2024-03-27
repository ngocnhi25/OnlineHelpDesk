using Application.UseCases.Accounts.Commands.Login;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Departments.Commands.CreateDepartment
{
    internal class CreateDepartmentCommandValidation : AbstractValidator<CreateDepartmentCommand>
    
    {
        public CreateDepartmentCommandValidation() 
        {
            RuleFor(x => x.DepartmentName)
                .NotEmpty().WithMessage("DepartmentName cannot be blank.")
                .MaximumLength(100).WithMessage("DepartmentName does not exceed 100 characters");
        }
    }
}
