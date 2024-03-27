using Application.UseCases.Requests.Commands.CreateRequest;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Requests.Commands.UpdateRequest
{
    public class UpdateRequestCommandValidation : AbstractValidator<UpdateRequestCommand>
    {
        public UpdateRequestCommandValidation()
        {
            RuleFor( r => r.Id)
               .NotEmpty().WithMessage("ID can not empty ! ");
        }
    }
}
