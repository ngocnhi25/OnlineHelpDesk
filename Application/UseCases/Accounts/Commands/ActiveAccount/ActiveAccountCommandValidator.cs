using FluentValidation;

namespace Application.UseCases.Accounts.Commands.ActiveAccount
{
    internal class ActiveAccountCommandValidator : AbstractValidator<ActiveAccountCommand>
    {
        public ActiveAccountCommandValidator()
        {
            RuleFor(x => x.AccountId)
                .NotEmpty().WithMessage("Account code cannot be left blank.")
                .Matches("^[a-zA-Z0-9]+$").WithMessage("Account code should only contain letters and numbers.")
                .MaximumLength(20);
        }
    }
}
