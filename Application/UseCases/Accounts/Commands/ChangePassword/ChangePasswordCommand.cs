using Application.Common.Messaging;

namespace Application.UseCases.Accounts.Commands.ChangePassword
{
    public sealed record ChangePasswordCommand(string Email, string NewPassword, string ConfirmPassword) : ICommand;
}
