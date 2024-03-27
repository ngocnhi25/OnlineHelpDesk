using Application.UseCases.Requests.Commands.ProcessUpdateStatusRequest;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using System.Security.Claims;

namespace Web_Api.Endpoints.Requests.Assignees
{
    public class AssigneesProcessUpdateStatusRequest : EndpointBaseAsync
     .WithRequest<FieldUpdateRequest>
     .WithActionResult<Result>
    {

        private readonly IMediator Sender;

        public AssigneesProcessUpdateStatusRequest(IMediator sender)
        {
            Sender = sender;
        }


        [HttpPut("api/request/update-status")]
        [Authorize(Roles = "Assignees")]
        public override async Task<ActionResult<Result>> HandleAsync(
            [FromBody] FieldUpdateRequest command, 
            CancellationToken cancellationToken = default
        ) {
            var accountId = User.FindFirstValue(ClaimTypes.Sid);
            var fullName = User.FindFirstValue(ClaimTypes.Name);
            var status = await Sender.Send(new ProcessUpdateStatusRequestCommand(command.RequestId, command.RequestStatusId, command.Reason, accountId, fullName));
            return status;
        }
    }

    public class FieldUpdateRequest
    {
        public string RequestId { get; set; }
        public string RequestStatusId { get; set; }
        public string? Reason { get; set; }
    }
}
