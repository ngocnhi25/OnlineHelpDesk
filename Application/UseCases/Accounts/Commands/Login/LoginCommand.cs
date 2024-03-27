using Application.Common.Messaging;
using Application.DTOs.Auth;

namespace Application.UseCases.Accounts.Commands.Login
{
    public sealed record LoginCommand(string AccountId, string Password) : ICommand<LoginDTO>;
}
