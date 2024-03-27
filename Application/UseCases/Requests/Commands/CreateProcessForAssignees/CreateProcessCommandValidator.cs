using FluentValidation;

namespace Application.UseCases.Requests.Commands.CreateProcessForAssignees
{
    public class CreateProcessCommandValidator : AbstractValidator<CreateProcessCommand>
    {
		public CreateProcessCommandValidator()
		{
            RuleFor(scc => scc.AccountId)
                .NotEmpty().WithMessage("Account ID is required to be filled in");
            RuleFor(scc => scc.RequestId)
                .NotEmpty().WithMessage("RequestId is required to be filled in");

        }
	}
}

