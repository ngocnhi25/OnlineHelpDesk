using FluentValidation;

namespace Application.UseCases.Accounts.Commands.StaySignIn
{
    internal class StaySignInCommandValidator : AbstractValidator<StaySignInCommand>
    {
        public StaySignInCommandValidator()
        {
            RuleFor(x => x.AccountId)
                .NotEmpty().WithMessage("AccountId cannot be left blank.")
                .Must(username => !username.Contains(" ")).WithMessage("AccountId cannot contain spaces.")
                .MaximumLength(20);
            RuleFor(x => x.RefreshToken)
                .NotEmpty().WithMessage("Refresh token cannot be left blank.")
                .Must(username => !username.Contains(" ")).WithMessage("Refresh token cannot contain spaces.");
        }
    }
}
