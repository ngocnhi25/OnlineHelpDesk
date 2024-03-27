using Application.Common.Messaging;

namespace Application.UseCases.Accounts.Commands.ActiveAccount
{
    public sealed record ActiveAccountCommand(string AccountId) : ICommand;
}
