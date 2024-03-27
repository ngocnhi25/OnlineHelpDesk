using FluentValidation;

namespace Application.UseCases.Accounts.Commands.ChangePassword
{
    internal class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator() 
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email cannot be left blank.")
                .EmailAddress().WithMessage("Please enter a valid email address.");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("New password cannot be left blank.")
                .Must(password => !string.IsNullOrWhiteSpace(password)).WithMessage("New password cannot contain whitespace.")
                .Must(password => password.Any(char.IsDigit)).WithMessage("New password must contain at least one digit.")
                .Must(password => password.Any(char.IsUpper)).WithMessage("New password must contain at least one uppercase letter.")
                .Must(password => password.Any(char.IsLower)).WithMessage("New password must contain at least one lowercase letter.")
                .Must(password => password.Any(char.IsLetterOrDigit)).WithMessage("New password must contain at least one special character.")
                .Length(8, 16).WithMessage("New password must be between 8 and 16 characters in length.");

            RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithMessage("Confirm password cannot be left blank.")
            .Equal(x => x.NewPassword).WithMessage("Confirm password must match the new password.");
        }
    }
}
