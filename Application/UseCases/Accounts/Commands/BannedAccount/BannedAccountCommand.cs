using Application.Common.Messaging;

namespace Application.UseCases.Accounts.Commands.BannedAccount
{
    public sealed record BannedAccountCommand(string AccountId) : ICommand;
}
