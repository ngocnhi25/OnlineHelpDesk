using Application.Common.Messaging;

namespace Application.UseCases.Requests.Commands.CreateProcessForAssignees
{
    public sealed record CreateProcessCommand (string AccountId, string RequestId, string FacilityHeadId, string FullNameFacilityHeads) : ICommand;
}

