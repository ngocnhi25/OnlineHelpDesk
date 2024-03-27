using Application.UseCases.Requests.Commands.CreateProcessForAssignees;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using System.Security.Claims;

namespace Web_Api.Endpoints.Requests
{
    public class CreateProcessForAssignees : EndpointBaseAsync
     .WithRequest<FieldCreateAssignees>
     .WithActionResult<Result>
    {
        private readonly IMediator Sender;

        public CreateProcessForAssignees(IMediator sender)
        {
            Sender = sender;
        }


        [HttpPost("api/request/CreateProcessForAssignees")]
        [Authorize(Roles = "Facility-Heads")]
        public async override Task<ActionResult<Result>> HandleAsync(
            FieldCreateAssignees command,
            CancellationToken cancellationToken = default)
        {
            var asigneeAccountId = User.FindFirstValue(ClaimTypes.Sid);
            var fullName = User.FindFirstValue(ClaimTypes.Name);
            var status = await Sender.Send(new CreateProcessCommand(command.AccountId, command.RequestId, asigneeAccountId, fullName));
            return status;
        }
    }

    public class FieldCreateAssignees
    {
        public string RequestId { get; set; }
        public string AccountId { get; set; }
    }
}

