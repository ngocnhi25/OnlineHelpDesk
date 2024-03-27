using Application.Common.Messaging;

namespace Application.UseCases.Accounts.Commands.SendMailVerifyCode
{
    public sealed record SendMailVerifyCodeCommand(string Email) : ICommand;
}
