using Application.Common.Messaging;

namespace Application.UseCases.Problems.Commands.CreateProblem
{
    public sealed record CreateProblemCommand(string Title) : ICommand;
}
