using Application.Common.Messaging;
using Microsoft.AspNetCore.Http;

namespace Application.UseCases.Accounts.Commands.UpdateAccount
{
    public sealed record UpdateAccountCommand(
        string? AccountId,
        int RoleId,
        string? FullName,
        IFormFile? ImageFile,
        string? Address,
        string? PhoneNumber,
        string? Gender,
        string? Birthday) : ICommand;
}
