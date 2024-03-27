using Application.Common.Messaging;

namespace Application.UseCases.Requests.Commands.ProcessUpdateStatusRequest
{
    public sealed record ProcessUpdateStatusRequestCommand(string Id, string RequestStatusId, string? Reason, string AccountIdSender, string FullNameAccountSender) : ICommand;
}
