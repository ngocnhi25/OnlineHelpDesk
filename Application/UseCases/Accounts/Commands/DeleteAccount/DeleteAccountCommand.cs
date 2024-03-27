using Application.Common.Messaging;

namespace Application.UseCases.Accounts.Commands.DeleteAccount
{
    public sealed record DeleteAccountCommand(string AccountId) : ICommand;
}
