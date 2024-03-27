using FluentValidation;

namespace Application.UseCases.Accounts.Commands.Login
{
    internal class LoginCommandValidation : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidation()
        {
            RuleFor(x => x.AccountId)
                .NotEmpty().WithMessage("AccountId cannot be left blank.")
                .Must(username => !username.Contains(" ")).WithMessage("AccountId cannot contain spaces.")
                .MaximumLength(20);
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password cannot be left blank.")
                .Must(username => !username.Contains(" ")).WithMessage("Password cannot contain spaces.");
        }
    }
}
