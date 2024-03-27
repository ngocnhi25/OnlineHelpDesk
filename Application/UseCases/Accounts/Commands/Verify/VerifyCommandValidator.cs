using FluentValidation;

namespace Application.UseCases.Accounts.Commands.Verify
{
    internal class VerifyCommandValidator : AbstractValidator<VerifyCommand>
    {
        public VerifyCommandValidator() 
        {
            RuleFor(x => x.Email)
                 .NotEmpty().WithMessage("Email cannot be left blank.")
                 .EmailAddress().WithMessage("Please enter a valid email address.");
            RuleFor(x => x.VerifyCode)
                .NotEmpty().WithMessage("Code cannot be left blank.")
                .Matches("^[0-9]+$").WithMessage("The verification code should contain only numbers.")
                .Length(6);
        } 
    }
}
