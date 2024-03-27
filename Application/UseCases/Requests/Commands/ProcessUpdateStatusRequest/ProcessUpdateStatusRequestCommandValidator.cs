using FluentValidation;

namespace Application.UseCases.Requests.Commands.ProcessUpdateStatusRequest
{
    public class ProcessUpdateStatusRequestCommandValidator : AbstractValidator<ProcessUpdateStatusRequestCommand>
    {
        public ProcessUpdateStatusRequestCommandValidator()
        {
            RuleFor(r => r.Id)
               .NotEmpty().WithMessage("ID can not empty ! ");
        }
    }
}
