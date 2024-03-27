using Application.Common.Messaging;

namespace Application.UseCases.Requests.Commands.UpdateRequest
{
    public sealed record class UpdateRequestCommand(
        Guid Id , string AccountId ,int? RequestStatusId, Boolean? Enable) : ICommand ;
}
