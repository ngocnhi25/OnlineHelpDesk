using Application.Common.Messaging;
using Application.DTOs.Accounts;

namespace Application.UseCases.Accounts.Commands.StaySignIn
{
    public sealed record StaySignInCommand(string AccountId, string RefreshToken) : ICommand<StaySignInResponse>;
}
