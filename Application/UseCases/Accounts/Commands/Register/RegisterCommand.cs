using Application.Common.Messaging;
using Microsoft.AspNetCore.Http;

namespace Application.UseCases.Accounts.Commands.Register
{
    public sealed record RegisterCommand(
        int RoleId,
        string? FullName,
        string? Email,
        IFormFile? ImageFile,
        string? Address,
        string? PhoneNumber,
        string? Gender,
        string? Birthday) : ICommand;
}
