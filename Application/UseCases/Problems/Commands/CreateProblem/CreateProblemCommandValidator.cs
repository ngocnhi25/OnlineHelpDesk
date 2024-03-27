using FluentValidation;

namespace Application.UseCases.Problems.Commands.CreateProblem
{
    internal class CreateProblemCommandValidator : AbstractValidator<CreateProblemCommand>
    {
        public CreateProblemCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title cannot be left blank.")
                .MaximumLength(300).WithMessage("FullName must not exceed 300 characters.")
                .MinimumLength(3).WithMessage("FullName cannot be less than 3 characters.");
        }
    }
}
